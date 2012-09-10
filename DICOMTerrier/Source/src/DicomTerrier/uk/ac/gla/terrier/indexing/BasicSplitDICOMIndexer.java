package uk.ac.gla.terrier.indexing;

import java.io.IOException;
import java.util.Arrays;
import java.util.Stack;
import uk.ac.gla.terrier.structures.FilePosition;
import uk.ac.gla.terrier.structures.CollectionStatistics;
import uk.ac.gla.terrier.structures.indexing.DirectDICOMIndexBuilder;
import uk.ac.gla.terrier.structures.indexing.DocumentIndexBuilder;
import uk.ac.gla.terrier.structures.indexing.InvertedDICOMIndexBuilder;
import uk.ac.gla.terrier.structures.indexing.ComparableInvertedDICOMIndexBuilder;
import uk.ac.gla.terrier.structures.indexing.LexiconBuilder;
import uk.ac.gla.terrier.structures.indexing.ComparableLexiconBuilder;
import uk.ac.gla.terrier.structures.indexing.TagLexiconBuilder;
import uk.ac.gla.terrier.structures.trees.FieldDocumentTree;
import uk.ac.gla.terrier.structures.trees.DICOMFieldDocumentTree;
import uk.ac.gla.terrier.structures.trees.DICOMFieldDocumentTreeNode;
import uk.ac.gla.terrier.structures.trees.ComparableDICOMFieldDocumentTree;
import uk.ac.gla.terrier.structures.trees.ComparableDICOMFieldDocumentTreeNode;
import uk.ac.gla.terrier.terms.TermPipeline;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.TermCodes;
import uk.ac.gla.terrier.utility.ComparableTermCodes;
import uk.ac.gla.terrier.utility.TagCodes;

import dicomxml.DICOMXMLReader;

/** 
 * BasicDICOMIndexer is the indexer for DICOM xml. It takes 
 * terms and tags from each Document object provided by the collection, and 
 * adds terms to temporary Lexicons, and into the DirectFile. 
 * It splits up comparable terms and non-comparable terms.
 * The documentIndex is updated to give the pointers into the Direct
 * file. The temporary lexicons are then merged into the main lexicon.
 * Inverted Index construction takes place as a second step.
 * <br>
 * 
 * @version 1.0 
 * @author Gerald Veldhuijsen
 */
public class BasicSplitDICOMIndexer extends Indexer
{
	/** This class implements an end of a TermPipeline that adds the
	 *  term to the DocumentTree. This TermProcessor does have field
	 *  support.
	 */
	private class FieldTermProcessor implements TermPipeline
	{
		public void processTerm(String term)
		{
			/* null means the term has been filtered out (eg stopwords) */
			if (term != null)
			{
				/* add term to Document tree */
				if (comparable){
					if(!term.equals(checkTerm)){
						/*The term is not equal to the original term
						 * but it is comparable, so we have to store it in the 
						 * normal index as well*/
						termsInDocument.insert(term, termFields);
						numOfTokensInDocument++;
						
						comparableTermsInDocument.insert(checkTerm,termFields);
					} else {
						comparableTermsInDocument.insert(term,termFields);
					}
					numOfComparableTokensInDocument++;
				} else{
					termsInDocument.insert(term,termFields);
					numOfTokensInDocument++;
				}					
			}else{
				if (comparable)
					comparableTermsInDocument.insert(checkTerm, termFields);					
			}
				
				
				
		}
	}
	
	/** 
	 * A private variable for storing the fields a term appears in.
	 */
	private Stack termFields;
	
	/**
	 * A variable to store the original term in
	 */
	private String checkTerm;
	
	/** 
	 * The structure that holds the terms found in a document.
	 */
	private DICOMFieldDocumentTree termsInDocument;
	
	/** 
	 * The structure that holds the comparable terms found in a document.
	 */
	private ComparableDICOMFieldDocumentTree comparableTermsInDocument;
	
	/** 
	 * The number of tokens found in the current document so far/
	 */
	private int numOfTokensInDocument = 0;
	private int numOfComparableTokensInDocument = 0;
	
	/**
	 * Boolean variable to test whether the term is comparable or not
	 */
	private boolean comparable = false;
	
	/**
	 * Index Builders
	 */
	private TagLexiconBuilder tagLexiconBuilder;
	private InvertedDICOMIndexBuilder invertedIndexBuilder;
	
	private ComparableLexiconBuilder comparableLexiconBuilder;
	private DocumentIndexBuilder comparableDocIndexBuilder;
	private DirectDICOMIndexBuilder comparableDirectIndexBuilder;
	private ComparableInvertedDICOMIndexBuilder comparableInvertedIndexBuilder;
	
	/** 
	 * Constructs an instance of a BasicIndexer, using the given path name
	 * for storing the data structures.
	 * @param pathname String the path where the datastructures will be created.
	 */
	public BasicSplitDICOMIndexer(String pathname) {
		super(pathname);
	}
	/** 
	 * Returns the end of the term pipeline, which corresponds to 
	 * an instance of either BasicIndexer.BasicTermProcessor, or 
	 * BasicIndexer.FieldTermProcessor, depending on whether 
	 * field information is stored.
	 * @return TermPipeline the end of the term pipeline.
	 */
	protected TermPipeline getEndOfPipeline()
	{
		return new FieldTermProcessor();		
	}
	
	/** 
	 * Creates the direct index, the document index and the lexicon.
	 * Loops through each document in each of the collections, 
	 * extracting terms and pushing these through the Term Pipeline 
	 * (eg stemming, stopping, lowercase).
	 * @param collections Collection[] the collections to be indexed.
	 */
	public void createDirectIndex(Collection[] collections)
	{
		//Initialise the index builders
		directIndexBuilder = new DirectDICOMIndexBuilder(fileNameNoExtension + ApplicationSetup.DF_SUFFIX);
		comparableDirectIndexBuilder = new DirectDICOMIndexBuilder(fileNameNoExtension + ApplicationSetup.COMPARABLE_DF_SUFFIX);
		tagLexiconBuilder = new TagLexiconBuilder(fileNameNoExtension + ApplicationSetup.TAGLEXICONSUFFIX);
		lexiconBuilder = new LexiconBuilder(fileNameNoExtension + ApplicationSetup.LEXICONSUFFIX);
		docIndexBuilder = new DocumentIndexBuilder(fileNameNoExtension + ApplicationSetup.DOC_INDEX_SUFFIX);
		comparableDocIndexBuilder = new DocumentIndexBuilder(fileNameNoExtension + ApplicationSetup.COMPARABLE_DOC_INDEX_SUFFIX);
		comparableLexiconBuilder = new ComparableLexiconBuilder(fileNameNoExtension + ApplicationSetup.COMPARABLE_LEXICONSUFFIX);
		
		//Statistics
		int numberOfDocuments = 0;
		int numberOfTokens = 0; 
		int numberOfComparableTokens=0; 
				
		final int collections_length = collections.length;
		for(int collectionNo = 0; collectionNo < collections_length; collectionNo++)
		{
			Collection collection = collections[collectionNo];
			long startCollection = System.currentTimeMillis();
			
			//Progress variables
			int size = collection.size()+1;
			int devider = size/100;
			if (devider==0)
				devider=1;			
			int devider_ten = size/1000;
			if (devider_ten==0)
				devider_ten=1;
			
			String[] markers = {"|", "/", "-", "\\"};
			int i = 1; int percentage=0; int mark =0;
			
			System.err.print("\b\b\b\b\b" + percentage + "% " + markers[mark++%4]);
			while(collection.nextDocument())
			{
				if((i%devider) == 0)
					percentage = i/devider;
				if (i%devider_ten ==0)					
					System.err.print("\b\b\b\b\b" + percentage + "% " + markers[mark++%4]);
				
				/* get the next document from the collection */
				String docid = collection.getDocid();
				Document doc = collection.getDocument();
				
				i++;
				
				if (doc == null)
					continue;
				
				numberOfDocuments++; 
				/* setup for parsing */
				termsInDocument = new DICOMFieldDocumentTree();
				comparableTermsInDocument = new ComparableDICOMFieldDocumentTree();
				
				String term; //term we're currently processing
				numOfTokensInDocument = 0;
				numOfComparableTokensInDocument = 0;
	
				//get each term in the document
				while (!doc.endOfDocument()) {
					comparable = false;
					if ((term = doc.getNextTerm())!=null && !term.equals("")) {
						checkTerm = term;
						
						if ( doc instanceof DICOMXMLReader){
							//Get the stack of fields that the term appears in
							termFields = ((DICOMXMLReader)doc).getTagStack();
							comparable = ((DICOMXMLReader)doc).isComparable();
						}						
						/* pass term into TermPipeline (stop, stem etc) */
						pipeline_first.processTerm(term);
						/* the term pipeline will eventually add the term to this object. */						
					}
					if (MAX_TOKENS_IN_DOCUMENT > 0 && 
							numOfTokensInDocument > MAX_TOKENS_IN_DOCUMENT)
							break;
				}
				
				//if we didn't index all tokens from document,
				//we need to get to the end of the document.
				while (!doc.endOfDocument()) 
					doc.getNextTerm();
								
				/* we now have all terms in the DocumentTree, so we save the document tree */
				if (termsInDocument.getNumberOfNodes() == 0 && comparableTermsInDocument.getNumberOfNodes()==0)
				{	/* this document is empty, add the minimum to the document index */
					indexEmpty(docid);
				}
				else
				{	/* index this document */
					numberOfTokens += numOfTokensInDocument;
					numberOfComparableTokens += numOfComparableTokensInDocument;
					
					if (termsInDocument.getNumberOfNodes() > 0)
						indexDocument(docid, numOfTokensInDocument, termsInDocument);
					else{
						//Make sure that the number of indexed documents is at least equal to
						//the number of comparable indexed documents
						pipeline_first.processTerm("null");
						indexDocument(docid, numOfTokensInDocument, termsInDocument);
					}
										
					/*index the comparable terms*/
					if (comparableTermsInDocument.getNumberOfNodes() > 0)
						indexDocumentComparable(docid, numOfComparableTokensInDocument, comparableTermsInDocument);
				}
			}
			long endCollection = System.currentTimeMillis();
			System.err.println("Collection #"+collectionNo+ " took "+((endCollection-startCollection)/1000)	+"seconds to index\n");
		}
		
		/*end of all the collections has been reached */
		/* flush the index buffers */
		directIndexBuilder.finishedCollections();
		comparableDirectIndexBuilder.finishedCollections();
		
		docIndexBuilder.finishedCollections();
		comparableDocIndexBuilder.finishedCollections();
				
		/* and then merge all the temporary lexicons */
		tagLexiconBuilder.finishedDirectIndexBuild();
		lexiconBuilder.finishedDirectIndexBuild();
		comparableLexiconBuilder.finishedDirectIndexBuild();
		
		/* reset the in-memory mapping of terms to term codes.*/
		TermCodes.reset();
		ComparableTermCodes.reset();
		TagCodes.reset();
	}

	/** 
	 * Adds the terms and field information of a document in the current lexicon 
	 * and in the direct index. It also updates the document index.
	 * @param docid String the identifier of the document to index.
	 * @param numOfTokensInDocument int the number of indexed tokens in the document.
	 * @param termsInDocument FieldDocumentTree the terms of the document to add to 
	 *        the direct index and to the lexicon. 
	 */
	protected void indexFieldDocument(String docid, int numOfTokensInDocument, FieldDocumentTree termsInDocument)
	{
		DICOMFieldDocumentTreeNode[] termBuffer = (DICOMFieldDocumentTreeNode[]) termsInDocument.toArray();
		
		try{
			/* add words to lexicontree */
			lexiconBuilder.addDocumentTerms(termBuffer);
			
			/* add tags to taglexicontree */
			tagLexiconBuilder.addDocumentTerms(termBuffer);
			
			/* sort the term buffer by termcode */
			Arrays.sort(termBuffer);
			
			/* add doc postings to the direct index */
			FilePosition dirIndexPost = directIndexBuilder.addDocument(termBuffer);
			
			/* add doc to documentindex */
			docIndexBuilder.addEntryToBuffer(docid, numOfTokensInDocument, dirIndexPost);
		}
		catch (IOException ioe)
		{
			System.err.println("Failed to index "+docid+" because "+ioe);
		}
	}
	/** 
	 * Adds the terms and the field information of a document in 
	 * the current lexicon and in the direct index. It also updates the document index.
	 * @param docid String the identifier of the document to index.
	 * @param numOfTokensInDocument int the number of indexed tokens in the document.
	 * @param termsInDocument FieldDocumentTree the temrs of the document to add to 
	 *        the direct index and to the lexicon. 
	 */
	protected void indexDocument(String docid, int numOfTokensInDocument, FieldDocumentTree termsInDocument) {
		indexFieldDocument(docid, numOfTokensInDocument, termsInDocument);		
	}
	
	/** 
	 * Adds the terms and the field information of the comparable terms in a document in 
	 * the current lexicon and in the direct index. It also updates the document index.
	 * @param docid String the identifier of the document to index.
	 * @param numOfTokensInDocument int the number of indexed tokens in the document.
	 * @param termsInDocument FieldDocumentTree the temrs of the document to add to 
	 *        the direct index and to the lexicon. 
	 */
	protected void indexDocumentComparable(String docid, int numOfTokensInDocument, ComparableDICOMFieldDocumentTree termsInDocument) {
		indexComparableFieldDocument(docid, numOfTokensInDocument, termsInDocument);		
	}
	
	/** 
	 * Adds the terms and field information of a document in the current lexicon 
	 * and in the direct index. It also updates the document index.
	 * @param docid String the identifier of the document to index.
	 * @param numOfTokensInDocument int the number of indexed tokens in the document.
	 * @param termsInDocument FieldDocumentTree the terms of the document to add to 
	 *        the direct index and to the lexicon. 
	 */
	protected void indexComparableFieldDocument(String docid, int numOfTokensInDocument, ComparableDICOMFieldDocumentTree termsInDocument)
	{
		ComparableDICOMFieldDocumentTreeNode[] termBuffer = comparableTermsInDocument.toComparableArray();
		
		try{
			/* add words to lexicontree */
			comparableLexiconBuilder.addDocumentTerms(termBuffer);
			
			/* add tags to taglexicontree */
			tagLexiconBuilder.addDocumentTerms(termBuffer);
			
			/* sort the term buffer by termcode */
			Arrays.sort(termBuffer);
			
			/* add doc postings to the direct index */
			FilePosition dirIndexPost = comparableDirectIndexBuilder.addDocument(termBuffer);
			
			/* add doc to documentindex */
			comparableDocIndexBuilder.addEntryToBuffer(docid, numOfTokensInDocument, dirIndexPost);
		}
		catch (IOException ioe)
		{
			System.err.println("Failed to index "+docid+" because "+ioe);
		}
	}
	
	/**
	 * Creates the inverted index after having created the 
	 * direct index, document index and lexicon.
	 */
	public void createInvertedIndex() {
		System.err.println("Started building the inverted index...");
		long beginTimestamp = System.currentTimeMillis();

		//generate the inverted index
		invertedIndexBuilder = new InvertedDICOMIndexBuilder(fileNameNoExtension + ApplicationSetup.IFSUFFIX);
		invertedIndexBuilder.createInvertedIndex();

		comparableInvertedIndexBuilder = new ComparableInvertedDICOMIndexBuilder(fileNameNoExtension + ApplicationSetup.COMPARABLE_IFSUFFIX);
		comparableInvertedIndexBuilder.createInvertedIndex();
		
		//and finally, the collection statistics
		CollectionStatistics.createAdvancedCollectionStatistics(
			invertedIndexBuilder.numberOfDocuments, invertedIndexBuilder.numberOfTokens,
			invertedIndexBuilder.numberOfUniqueTerms, invertedIndexBuilder.numberOfPointers,
			comparableInvertedIndexBuilder.numberOfDocuments, comparableInvertedIndexBuilder.numberOfTokens,
			comparableInvertedIndexBuilder.numberOfUniqueTerms, comparableInvertedIndexBuilder.numberOfPointers,
			new String[] {"uk.ac.gla.terrier.structures.Lexicon",
				"uk.ac.gla.terrier.structures.DocumentIndexEncoded",
				"uk.ac.gla.terrier.structures.DirectIndex",
				"uk.ac.gla.terrier.structures.dicom.InvertedDICOMIndex", 
				"uk.ac.gla.terrier.structures.dicom.TagLexicon",
				"uk.ac.gla.terrier.structures.ComparableLexicon", //Comparable Lexicon
				"uk.ac.gla.terrier.structures.dicom.InvertedDICOMIndex" //Comparable Inverted Dicom index 
				}
			);
		invertedIndexBuilder.close();
		comparableInvertedIndexBuilder.close();
		long endTimestamp = System.currentTimeMillis();
		System.err.println("Finished building the inverted index...");
		long seconds = (endTimestamp - beginTimestamp) / 1000;
		System.err.println("Time elapsed for inverted file: " + seconds);
	}
}
