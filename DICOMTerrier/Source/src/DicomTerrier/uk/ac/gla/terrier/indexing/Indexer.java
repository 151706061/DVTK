/*
 * Terrier - Terabyte Retriever 
 * Webpage: http://ir.dcs.gla.ac.uk/terrier 
 * Contact: terrier{a.}dcs.gla.ac.uk
 * University of Glasgow - Department of Computing Science
 * Information Retrieval Group
 * 
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
 * the License for the specific language governing rights and limitations
 * under the License.
 *
 * The Original Code is Indexer.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (Original author) 
 */
package uk.ac.gla.terrier.indexing;
import java.io.IOException;
import uk.ac.gla.terrier.structures.indexing.DirectIndexBuilder;
import uk.ac.gla.terrier.structures.indexing.DocumentIndexBuilder;
import uk.ac.gla.terrier.structures.indexing.InvertedIndexBuilder;
import uk.ac.gla.terrier.structures.indexing.LexiconBuilder;
import uk.ac.gla.terrier.structures.trees.FieldDocumentTree;
import uk.ac.gla.terrier.terms.TermPipeline;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * 
 * @author Craig Macdonald
 * @version $Revision: 1.1 $
 */
public abstract class Indexer
{
	
	/** 
	 * The maximum number of tokens in a document. 
	 * If it is set to zero, then there is no limit 
	 * in the number of tokens indexed for a document.
	 */
	protected final static int MAX_TOKENS_IN_DOCUMENT = 
		Integer.parseInt(ApplicationSetup.getProperty("indexing.max.tokens", "0"));
	
	/**
	 * The default namespace for the term pipeline classes.
	 */
	private final static String PIPELINE_NAMESPACE = "uk.ac.gla.terrier.terms.";
	/**
	 * The first component of the term pipeline.
	 */
	protected TermPipeline pipeline_first;
	/**
	 * Indicates whether an entry for empty documents is stored in the 
	 * document index, or empty documents should be ignored.
	 */
	protected boolean IndexEmptyDocuments = !ApplicationSetup.IGNORE_EMPTY_DOCUMENTS;
	
	/**
	 * The builder that creates the direct index.
	 */
	protected DirectIndexBuilder directIndexBuilder;
	
	/**
	 * The builder that creates the document index.
	 */
	protected DocumentIndexBuilder docIndexBuilder;
	
	/**
	 * The builder that creates the inverted index.
	 */
	protected InvertedIndexBuilder invertedIndexBuilder;
	
	/**
	 * The builder that creates the lexicon.
	 */
	protected LexiconBuilder lexiconBuilder;
	
	/**
	 * The common prefix of the data structures filenames. 
	 */
	protected String fileNameNoExtension;
	/**
	 * Creates an instance of the class. The generated data structures
	 * will be saved in the given path. The default name of the data 
	 * structures is read from the property <tt>datastructures.prefix</tt>.
	 * @param path String the path where the generated data structures will
	 *        be saved.
	 */
	public Indexer(String path) {
		String prefix = ApplicationSetup.TERRIER_INDEX_PREFIX;
		fileNameNoExtension = path + ApplicationSetup.FILE_SEPARATOR + prefix;
		
		//construct pipeline using list specified in terrier.properties
		//this object should be the last item in the pipeline
		load_pipeline();
	}
	/**
	 * An abstract method for creating the direct index, the document index
	 * and the lexicon for the given collections.
	 * @param collections Collection[] An array of collections to index
	 */
	public abstract void createDirectIndex(Collection[] collections);
	/**
	 * An abstract method for creating the inverted index, given that the
	 * the direct index, the document index and the lexicon have
	 * already been created.
	 */
	public abstract void createInvertedIndex();
	
	/**
	 * An abstract method that returns the last component 
	 * of the term pipeline.
	 * @return TermPipeline the end of the term pipeline.
	 */
	protected abstract TermPipeline getEndOfPipeline();
	/** 
	 * Creates the term pipeline, as specified by the
	 * property <tt>termpipelines</tt> in the properties
	 * file. The default value of the property <tt>termpipelines</tt>
	 * is <tt>Stopwords,PorterStemmer</tt>. This means that we first
	 * remove stopwords and then apply Porter's stemming algorithm.
	 */
	protected void load_pipeline()
	{
		String[] pipes = ApplicationSetup.getProperty(
				"termpipelines", "Stopwords,PorterStemmer").trim()
				.split("\\s*,\\s*");
		
		TermPipeline next = getEndOfPipeline();
		TermPipeline tmp;
		for(int i=pipes.length-1; i>=0; i--)//TODO check this works for length = 0
		{
			try{
				String className = pipes[i];
				if (className.length() == 0)
					continue;
				if (className.indexOf(".") < 0 )
					className = PIPELINE_NAMESPACE + className;
				Class pipeClass = Class.forName(className, false, this.getClass().getClassLoader());
				tmp = (TermPipeline) (pipeClass.getConstructor(new Class[]{TermPipeline.class}).newInstance(new Object[] {next}));
				next = tmp;
			}catch (Exception e){
				System.err.println("TermPipeline object "+PIPELINE_NAMESPACE+pipes[i]+" not found: "+e);
			}
		}
		pipeline_first = next;
	}
	
	/**
	 * An abstract method that adds the terms for a document with a given identifier
	 * to the direct index, document index and lexicon.
	 * @param docid String the identifier for the given document.
	 * @param numOfTokensInDocument int the number of indexed tokens for the document.
	 * @param termsInDocument FieldDocumentTree the terms contained in the given document.
	 */
	protected abstract void indexDocument(String docid, int numOfTokensInDocument, FieldDocumentTree termsInDocument);
	/** Adds an entry to document index for empty document @param docid, only if
		IndexEmptyDocuments is set to true.
	*/
	protected void indexEmpty(String docid)
	{
		/* add doc to documentindex, even though it's empty */
		if(IndexEmptyDocuments)
			try
			{
				System.err.println("WARNING: Adding empty document "+docid);
				docIndexBuilder.addEntryToBuffer(docid, 0, directIndexBuilder.getLastEndOffset());
			}
			catch (IOException ioe)
			{
				System.err.println("Failed to index empty document "+docid+" because "+ioe);
			}
	}
	
}
