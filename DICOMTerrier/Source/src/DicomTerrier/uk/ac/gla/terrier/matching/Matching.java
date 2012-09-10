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
 * The Original Code is Matching.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.matching;

import gnu.trove.TIntHashSet;

import java.util.StringTokenizer;
import java.util.ArrayList;
import uk.ac.gla.terrier.matching.dsms.DocumentScoreModifier;
import uk.ac.gla.terrier.matching.dsms.ConstraintModifier;
import uk.ac.gla.terrier.matching.models.WeightingModel;
import uk.ac.gla.terrier.matching.models.Simplest;
import uk.ac.gla.terrier.matching.tsms.TermInBoolDICOMFieldModifier;
import uk.ac.gla.terrier.matching.tsms.TermScoreModifier;
import uk.ac.gla.terrier.structures.CollectionStatistics;
import uk.ac.gla.terrier.structures.DocumentIndex;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.structures.InvertedIndex;
import uk.ac.gla.terrier.structures.Lexicon;
import uk.ac.gla.terrier.structures.ComparableLexicon;
import uk.ac.gla.terrier.structures.dicom.TagLexicon;
import uk.ac.gla.terrier.structures.TermTagLexicon;
import uk.ac.gla.terrier.structures.dicom.InvertedTagIndex;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.HeapSort;
import uk.ac.gla.terrier.utility.MergePointers;
import uk.ac.gla.terrier.querying.parser.*;

/**
 * Performs the matching of documents with a query, by
 * first assigning scores to documents for each query term
 * and modifying these scores with the appropriate modifiers.
 * Then, a series of document score modifiers are applied
 * if necessary.
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class Matching {
	
	/**
	 * The default namespace for the term score modifiers that are
	 * specified in the properties file.
	 */
	protected static String tsmNamespace = "uk.ac.gla.terrier.matching.tsms.";
	/**
	 * The default namespace for the document score modifiers that are
	 * specified in the properties file.
	 */
	protected static String dsmNamespace = "uk.ac.gla.terrier.matching.dsms.";
	
	/** 
	 * The maximum number of documents in the final retrieved set.
	 * It corresponds to the property matching.retrieved_set_size,
	 * which default value is 1000. 
	 */
	protected static int RETRIEVED_SET_SIZE;
	
	/** 
	 * The maximum term frequency that is considered within a 
	 * document. This helps to deal with 'spamming' in documents,
	 * where a term appears suspiciously too many times. The 
	 * corresponding property is <tt>frequency.upper.threshold</tt>
	 * and the default value is 0, meaning that there is no 
	 * threshold. 
	 */
	protected static int FREQUENCY_UPPER_THRESHOLD;
	
	/**
	 * A property that enables to ignore the terms with a low
	 * IDF. In the match method, we check whether the frequency
	 * of a term in the collection is higher than the number of
	 * documents. If this is true, then by default we don't assign
	 * scores to documents that contain this term. We can change
	 * this default behavious by altering the corresponding property
	 * <tt>ignore.low.idf.terms</tt>, the default value of which is
	 * true.
	 */
	protected static boolean IGNORE_LOW_IDF_TERMS;
	
	/**
	 * A property that when it is true, it allows matching all documents
	 * to an empty query. In this case the ordering of documents is 
	 * random. More specifically, it is the ordering of documents in 
	 * the document index. The corresponding property is 
	 * <tt>match.empty.query</tt> and the default value is <tt>false</tt>.
	 */
	protected static boolean MATCH_EMPTY_QUERY;
	
	/** The number of retrieved documents for a query.*/
	protected int numberOfRetrievedDocuments;
	protected boolean foundResults;
		
	/** the pointers read from the inverted file */
	int[][] pointers;
	
	/** the number of term score modifiers */
	int numOfTermModifiers;
	
	/** the number of document score modifiers */
	int numOfDocModifiers;
	
	/** number of modified scores */
	int numberOfModifiedDocumentScores;
	
	/** the scores */
	double[] scores;
	
	/** the occursences */ 
	short[] occurences;	
		
	/** term score modifiers */
	TermScoreModifier[] tsms;
	
	/**
	 * The index used for retrieval. 
	 */ 
	protected Index index;
	
	/** The document index used.*/
	protected DocumentIndex docIndex;
	/** The lexicon used.*/
	protected Lexicon lexicon;
	/** The inverted file.*/
	protected InvertedIndex invertedIndex;
	
	/** The comparable inverted file.*/
	protected InvertedIndex comparableInvertedIndex;
	
	/** The tag lexicon*/
	protected TagLexicon tagLexicon;
	
	/** The comparable lexicon */
	protected ComparableLexicon comparableLexicon;
	
	/** The inverted tag index*/
	protected InvertedTagIndex iiT;
	
	
	/** The result set.*/
	protected ResultSet resultSet;
	
	/**
	 * Contains the term score modifiers to be
	 * applied for a query.
	 */
	protected ArrayList termModifiers;
	
	/**
	 * Contains the document score modifiers
	 * to be applied for a query.
	 */
	protected ArrayList documentModifiers;
	
	/** The weighting model used for retrieval.*/
	protected WeightingModel wmodel;
	
	/** 
	 * A default constructor that creates 
	 * the CollectionResultSet and initialises
	 * the document and term modifier containers.
	 * @param index the object that encapsulates the basic
	 *        data structures used for retrieval.
	 */
	public Matching(Index index) {
		resultSet = new CollectionResultSet();
		termModifiers = new ArrayList();
		documentModifiers = new ArrayList();
		this.index = index;
		this.docIndex = index.getDocumentIndex();
		this.lexicon = index.getLexicon();
		this.invertedIndex = index.getInvertedIndex();
		this.tagLexicon = index.getTagLexicon();
		this.comparableLexicon = index.getComparableLexicon();
		this.comparableInvertedIndex = index.getComparableInvertedIndex();
		//TODO also get this structure from index
		this.iiT = new InvertedTagIndex(new TermTagLexicon(),ApplicationSetup.INVERTED_TAG_FILENAME);
		
		//adding document and term score modifiers that will be 
		//used for all queries, independently of the query operators
		//only modifiers with default constructors can be used in this way.
		String defaultTSMS = ApplicationSetup.getProperty("matching.tsms","");
		String defaultDSMS = ApplicationSetup.getProperty("matching.dsms","");
		
		StringTokenizer tokens = new StringTokenizer(defaultTSMS, ",");
		String modifierName = null;
		try {
			while (tokens.hasMoreTokens()) {
				modifierName = tokens.nextToken();
				if (modifierName.indexOf('.') == -1) 
					modifierName = tsmNamespace + modifierName;				
				
				System.out.println(modifierName);
				TermScoreModifier tsm = (TermScoreModifier)Class.forName(modifierName).newInstance(); 
				//TermScoreModifier tsm = (TermScoreModifier) new uk.ac.gla.terrier.matching.tsms.FieldScoreModifier();
				addTermScoreModifier( tsm);
			}
			
			tokens = new StringTokenizer(defaultDSMS, ",");
			while (tokens.hasMoreTokens()) {
				modifierName = tokens.nextToken();
				if (modifierName.indexOf('.') == -1)
					modifierName = dsmNamespace + modifierName;
				addDocumentScoreModifier((DocumentScoreModifier)Class.forName(modifierName).newInstance());
			}
		} catch(Exception e) {
			System.err.println("Exception while initialising default modifiers.");
			System.err.println("Please check the name of the modifiers in the configuration file.");
			System.err.println(e);
		}
	}
	
	/**
	 * Returns the result set.
	 */
	public ResultSet getResultSet() {
		return resultSet;
	}
	/**
	 * Initialises the arrays prior of retrieval. Only the first time it is called,
	 * it will allocate memory for the arrays.
	 */
	protected void initialise() {
		resultSet.initialise();
		RETRIEVED_SET_SIZE = (new Integer(ApplicationSetup.getProperty("matching.retrieved_set_size", "1000"))).intValue();
		FREQUENCY_UPPER_THRESHOLD = (new Integer(ApplicationSetup.getProperty("frequency.upper.threshold", "0"))).intValue();
		IGNORE_LOW_IDF_TERMS = (new Boolean(ApplicationSetup.getProperty("ignore.low.idf.terms","true"))).booleanValue();
		MATCH_EMPTY_QUERY = (new Boolean(ApplicationSetup.getProperty("match.empty.query","false"))).booleanValue();
	}
	/**
	 * Initialises the arrays prior of retrieval, with 
	 * the given scores. Only the first time it is called,
	 * it will allocate memory for the arrays.
	 * @param scs double[] the scores to initialise the result set with.
	 */
	protected void initialise(double[] scs) {
		resultSet.initialise(scs);
		RETRIEVED_SET_SIZE = (new Integer(ApplicationSetup.getProperty("matching.retrieved_set_size", "1000"))).intValue();
		FREQUENCY_UPPER_THRESHOLD = (new Integer(ApplicationSetup.getProperty("frequency.upper.threshold", "0"))).intValue();
		IGNORE_LOW_IDF_TERMS = (new Boolean(ApplicationSetup.getProperty("ignore.low.idf.terms","true"))).booleanValue();
		MATCH_EMPTY_QUERY = (new Boolean(ApplicationSetup.getProperty("match.empty.query","false"))).booleanValue();
	}
	
	/**
	 * Registers a term score modifier. If more than one modifiers
	 * are registered, then they applied in the order they were registered.
	 * @param termScoreModifier TermScoreModifier the score modifier to be
	 *        applied.
	 */
	public void addTermScoreModifier(TermScoreModifier termScoreModifier) {
		termModifiers.add(termScoreModifier);
	}
	
	/**
	 * Returns the i-th registered term score modifier.
	 * @return the i-th registered term score modifier.
	 */
	public TermScoreModifier getTermScoreModifier(int i) {
		return (TermScoreModifier)termModifiers.get(i);
	}
	
	/**
	 * Registers a document score modifier. If more than one modifiers
	 * are registered, then they applied in the order they were registered.
	 * @param documentScoreModifier DocumentScoreModifier the score modifier to be
	 *        applied. 
	 */
	public void addDocumentScoreModifier(DocumentScoreModifier documentScoreModifier) {
		documentModifiers.add(documentScoreModifier);
	}
	
	/**
	 * Returns the i-th registered document score modifier.
	 * @return the i-th registered document score modifier.
	 */
	public DocumentScoreModifier getDocumentScoreModifier(int i) {
		return (DocumentScoreModifier)documentModifiers.get(i);
	}
	/**
	 * Sets the weihting model used for retrieval.
	 * @param model the weighting model used for retrieval
	 */
	public void setModel(Model model) {
		wmodel = (WeightingModel)model;
	}
	
	/**
	 * Returns a descriptive string for the retrieval process performed.
	 */
	public String getInfo() {
		return wmodel.getInfo();
	}
	
	/**
	 * Implements the matching of a query with the documents.
	 * @param queryNumber the identifier of the processed query.
	 * @param queryTerms the query terms to be processed.
	 */
	public void match(String queryNumber, MatchingQueryTerms queryTerms) {
		
		System.err.println("MATCHING:" + queryTerms.getQuery().getClass().getSimpleName());
		long startTime = System.currentTimeMillis();
					
		//the first step is to initialise the arrays of scores and document ids.
		initialise();
	
		String[] queryTermStrings = queryTerms.getTerms();
		
		//check whether we need to match an empty query.
		//if so, then return the existing result set.
		if (MATCH_EMPTY_QUERY && queryTermStrings.length == 0) {
			resultSet.setExactResultSize(CollectionStatistics.getNumberOfDocuments());
			resultSet.setResultSize(CollectionStatistics.getNumberOfDocuments());
			return;
		}
		
		//in order to save the time from references to the arrays, we create local references
		int[] docids = resultSet.getDocids();
		scores = resultSet.getScores();
		occurences = resultSet.getOccurrences();
		
		//the number of documents with non-zero score.
		numberOfRetrievedDocuments = 0;
		
		//the number of term score modifiers
		numOfTermModifiers = termModifiers.size();
		
		//the number of document score modifiers
		numOfDocModifiers = documentModifiers.size();
		
		//number of modified scores
		numberOfModifiedDocumentScores =0;
		
		//whether we have found results
		foundResults = false;
				
		if (queryTermStrings != null){
			//for each query term in the query
			final int queryLength = queryTermStrings.length;
			for (int i = 0; i < queryLength; i++) {
				
				//we seek the query term in the lexicon
				boolean foundL = lexicon.findTerm(queryTermStrings[i]);
				boolean foundCL = comparableLexicon.findTerm(queryTermStrings[i]);
				//and if it is not found, we continue with the next term
				if (!foundL && !foundCL)
					continue;
				
				//Merge term statistics
				double TfreqL = 0;
				double TfreqCL = 0;
				if (foundL){
					//System.out.println("Found term " + queryTermStrings[i]);
					TfreqL = (double)lexicon.getTF();
				}
				if (foundCL){
					//System.out.println("Found comparable term " + queryTermStrings[i]);
					TfreqCL = (double)comparableLexicon.getTF();
				}
				
				int idL = lexicon.getTermId();
				int idCL = comparableLexicon.getTermId();
				
				//because when the TreeNode is created, the term code assigned is taken from
				//the TermCodes class, the assigned term code is only valid during the indexing
				//process. Therefore, at this point, the term code should be updated with the one
				//stored in the lexicon file.	
				queryTerms.setTermProperty(queryTermStrings[i], idL);
													
				//check if the IDF is very low.
				if (IGNORE_LOW_IDF_TERMS==true && docIndex.getNumberOfDocuments() < (TfreqL+TfreqCL)) {
					System.err.println("query term " + queryTermStrings[i] + " has low idf (TF = " + TfreqL+"+"+TfreqCL + " )- ignored from scoring.");
					continue;
				}
				
				long startOff = comparableLexicon.getStartOffset();
				byte startbitOff = comparableLexicon.getStartBitOffset();
				long endOff = comparableLexicon.getEndOffset();
				byte endbitOff = comparableLexicon.getEndBitOffset();
				
				//the postings are beign read from the inverted file.
				System.out.println("Reading Pointer from inverted file...");
				
				if(foundL && foundCL){
					//Found in both comparable as normal lexicon.
					//We need to merge the postings
					int [][] pI = invertedIndex.getDocuments(queryTerms.getTermCode(queryTermStrings[i]));
					int [][] pCI = comparableInvertedIndex.getDocumentsFast(idCL, startbitOff, startOff, endbitOff, endOff);
					long start = System.currentTimeMillis();
					pointers = MergePointers.merge(pI, pCI);
					System.err.println("Merging of pointers took " + (System.currentTimeMillis()-start));
					System.err.print("" + (i + 1) + ": " + queryTermStrings[i].trim());
					System.err.println(		" with "
						+ pointers[0].length + "(" + pI[0].length + "," + pCI[0].length + ")" 
						+ " documents (TF is "
						+ (TfreqL + TfreqCL)
						+ ")."
					);
				} else if (foundL){
					//Found only in the 'normal' lexicon
					pointers = invertedIndex.getDocuments(queryTerms.getTermCode(queryTermStrings[i]));
					System.err.print("" + (i + 1) + ": " + queryTermStrings[i].trim());
					System.err.println(		" with "
						+ pointers[0].length  
						+ " documents (TF is "
						+ (TfreqL + TfreqCL)
						+ ")."
					);
				} else{
					//Found only in the comparable lexicon
					pointers = comparableInvertedIndex.getDocumentsFast(idCL, startbitOff, startOff, endbitOff, endOff);
					System.err.print("" + (i + 1) + ": " + queryTermStrings[i].trim());
					System.err.println(		" with "
						+ pointers[0].length  
						+ " documents (TF is "
						+ (TfreqL + TfreqCL)
						+ ")."
					);
				} 				
				
				//the weighting model is prepared for assigning scores to documents
				wmodel.setKeyFrequency(queryTerms.getTermWeight(queryTermStrings[i]));
				wmodel.setDocumentFrequency(pointers[0].length);
				wmodel.setTermFrequency(TfreqL + TfreqCL);
				wmodel.setAverageDocumentLength((double)CollectionStatistics.getAverageDocumentLength());
				
				//the scores for the particular term
				double[] termScores = new double[pointers[0].length];
				
				//assign scores to documents for a term
				assignScores(termScores, pointers);
				
				//application dependent modification of scores
				//of documents for a term
				numberOfModifiedDocumentScores = 0;
				for (int t = 0; t < numOfTermModifiers; t++)
					((TermScoreModifier)termModifiers.get(t)).modifyScores(termScores, pointers);
				//application dependent modification of scores
				//of documents for a term. These are predefined by the query
				tsms = queryTerms.getTermScoreModifiers(queryTermStrings[i]);
				if (tsms!=null) {
					for (int t=0; t<tsms.length; t++){
						if (tsms[t]!=null)						
							System.out.println("Modified " + tsms[t].modifyScores(termScores, pointers) + " with termscoremodifier");
					}
				}
				
				//finally setting the scores of documents for a term
				//a mask for setting the occurrences
				short mask = 0;
				if (i<16)
					mask = (short)(1 << i);
				
				int docid;
				int[] pointers0 = pointers[0];
				//int[] pointers1 = pointers[1];
				final int numberOfPointers = pointers0.length;
				for (int k = 0; k < numberOfPointers; k++) {
					docid = pointers0[k];
					if ((scores[docid] == 0.0d) && (termScores[k] > 0.0d)) {
						numberOfRetrievedDocuments++;
					} else if ((scores[docid] > 0.0d) && (termScores[k] < 0.0d)) {
						numberOfRetrievedDocuments--;
					}
					scores[docid] += termScores[k];
					occurences[docid] |= mask;
				}			
			}
		}
		
		if(numberOfRetrievedDocuments>0)
			foundResults = true;
		
		//Store the original weighting model
		WeightingModel temp = wmodel;
		wmodel = new Simplest();
		
		///Now continue with the EQ queries part, if there is any
		matchEqual(queryTerms);
		
		if(numberOfRetrievedDocuments>0)
			foundResults = true;
				
		///Now continue with the GT queries part, if there is any
		matchGreaterThan(queryTerms);
		
		if(numberOfRetrievedDocuments>0)
			foundResults = true;
		
		///Now continue with the LT queries part, if there is any
		matchLessThan(queryTerms);
						
		//Restore weighting model
		wmodel = temp;
		
		//load in the dsms
		DocumentScoreModifier[] dsms; int NumberOfQueryDSMs = 0;
		dsms = queryTerms.getDocumentScoreModifiers();
		if (dsms!=null)
			NumberOfQueryDSMs = dsms.length;
		System.out.println("Number of documentModifiers =" + NumberOfQueryDSMs);
				
		//sort in descending score order the top RETRIEVED_SET_SIZE documents
		long sortingStart = System.currentTimeMillis();
		//we need to sort at most RETRIEVED_SET_SIZE, or if we have retrieved
		//less documents than RETRIEVED_SET_SIZE then we need to find the top 
		//numberOfRetrievedDocuments.
		int set_size = Math.min(RETRIEVED_SET_SIZE, numberOfRetrievedDocuments);
		if (set_size == 0) 
			set_size = numberOfRetrievedDocuments;
				
		//sets the effective size of the result set.
		resultSet.setExactResultSize(numberOfRetrievedDocuments);
		
		//sets the actual size of the result set.
		resultSet.setResultSize(set_size);
		
		HeapSort.descendingHeapSort(scores, docids, occurences, set_size);
		long sortingEnd = System.currentTimeMillis();
		
		System.out.println("First sorting took " + (sortingEnd-sortingStart));
				
		/*we apply the application dependent document score modifiers first and then 
		we apply the filter document score modifiers (Constraint and filename). Further the 
		BooleanFallback modifier should be applied as late as possible.
		
		/*dsms each require resorts of the result list. This is expensive, so should
		   be avoided if possible. Sorting is only done if the dsm actually altered any scores */
		
		/*application dependent modification of scores
		of documents for a query, based on a static set by the client code
		sorting the result set after applying each DSM*/
		if(queryTerms.length()>0)
		for (int t = 0; t < numOfDocModifiers; t++) {
			if (((DocumentScoreModifier)documentModifiers.get(t)).modifyScores(
					index, 
					queryTerms, 
					resultSet))
				HeapSort.descendingHeapSort(scores, docids, occurences, resultSet.getResultSize());
		}
		
		/*filter modification of scores
		of documents for a query, defined by this query*/
		for (int t = NumberOfQueryDSMs-1; t >= 0; t--) {
			if (dsms[t].modifyScores(index, queryTerms, resultSet))
				HeapSort.descendingHeapSort(scores, docids, occurences, resultSet.getResultSize());
		}
		
		System.err.println("number of retrieved documents: " + resultSet.getResultSize() + " in " + (System.currentTimeMillis()-startTime) + " milliseconds");

	}
	/**
	 * Assigns scores to documents for a particular term.
	 * @param scores double[] the scores of the documents for the query term.
	 * @param pointers int[][] the pointers read from the inverted file
	 *        for a particular query term.
	 */
	protected void assignScores(double[] scores, int[][] pointers) {
		int[] pointers1 = pointers[0];
		int[] pointers2 = pointers[1];
		final int numOfPointers = pointers1.length;
		//for each document that contains 
		//the query term, the score is computed.
		int frequency;
		int docLength;
		double score;
		for (int j = 0; j < numOfPointers; j++) {
			frequency = pointers2[j];
			docLength = docIndex.getDocumentLength(pointers1[j]);
			
			//checking whether we have setup an upper threshold
			//for within document frequencies. If yes, we check 
			//whether we need to change the current term's frequency.
			if (FREQUENCY_UPPER_THRESHOLD > 0 && frequency > FREQUENCY_UPPER_THRESHOLD) {
				frequency = FREQUENCY_UPPER_THRESHOLD;	
			}
			
			//compute the score
			score = wmodel.score(1.0D * frequency, 1.0D * docLength);
			
			//increase the number of retrieved documents if the
			//previous score was zero and the added score is positive
			//sometimes negative scores occur due to very low
			//probabilities
			if (score > 0)
				scores[j] = score;
		}
	}
	
	private void matchEqual(MatchingQueryTerms queryTerms){
		Query q;
		ArrayList alist = new ArrayList();
		TermInBoolDICOMFieldModifier boolFieldModifier = new TermInBoolDICOMFieldModifier();
		String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
		for (int i=0; i<rootTags.length; i++){
			if (tagLexicon.findTag(rootTags[i])){	
				boolFieldModifier.addRootTagId(tagLexicon.getTagId());
			}
		}
		
		MatchingQueryTerms equalTerms = new MatchingQueryTerms();
		q = queryTerms.getQuery();
		q.obtainAllOf(uk.ac.gla.terrier.querying.parser.EQFieldQuery.class, alist);
		for(int i=0; i<alist.size(); i++){
			//The set that contains the docids that satisfy the constraint
			TIntHashSet satisfying = new TIntHashSet();
			EQFieldQuery eq = (EQFieldQuery)alist.get(i);
			eq.obtainQueryEqualTerms(equalTerms);
			if(equalTerms.length()>1)
				System.err.println("Warning: Specified more then one query term in equality query. Only using one.");
			String term = equalTerms.getTerms()[0];
			
			if (tagLexicon.findTag(eq.getField().toLowerCase())){
				
				int fieldId = tagLexicon.getTagId();
				
				//we seek the value term in the comparable lexicon
				boolean found = comparableLexicon.findExactTerm(term);
				//and if it is not found, we continue with the next term
				if (!found)
					continue;
				
				//because when the TreeNode is created, the term code assigned is taken from
				//the TermCodes class, the assigned term code is only valid during the indexing
				//process. Therefore, at this point, the term code should be updated with the one
				//stored in the lexicon file.	
				equalTerms.setTermProperty(term, comparableLexicon.getTermId());
				System.err.print("" + (i + 1) + ": " + term.trim());
				
				//the weighting model is prepared for assigning scores to documents
				wmodel.setKeyFrequency(equalTerms.getTermWeight(term));
				wmodel.setDocumentFrequency((double)comparableLexicon.getNt());
				wmodel.setTermFrequency((double)comparableLexicon.getTF());
				wmodel.setAverageDocumentLength(
						(double)CollectionStatistics.getAverageDocumentLength());
				System.err.println(
						" with "
						+ comparableLexicon.getNt()
						+ " documents (TF is "
						+ comparableLexicon.getTF()
						+ ").");
				
				//check if the IDF is very low.
				/*if (IGNORE_LOW_IDF_TERMS==true && docIndex.getNumberOfDocuments() < comparableLexicon.getTF()) {
					System.err.println("query term " + term + " has low idf - ignored from scoring.");
					continue;
				}*/
				
				long startOff = comparableLexicon.getStartOffset();
				byte startbitOff = comparableLexicon.getStartBitOffset();
				long endOff = comparableLexicon.getEndOffset();
				byte endbitOff = comparableLexicon.getEndBitOffset();
				
				//the postings are beign read from the inverted file.
				System.out.println("Reading Pointer from inverted file...");
												
				if(comparableLexicon.getNt()>ApplicationSetup.INVERTTAG_TRESHOLD){
					pointers = iiT.getDocuments(equalTerms.getTermCode(term), fieldId);
					System.out.println("Found in inverted tag index");
					if (pointers==null)
						continue;
				} else
					pointers = comparableInvertedIndex.getDocumentsFast(equalTerms.getTermCode(term), startbitOff, startOff, endbitOff, endOff);
				
				//the scores for the particular term
				double[] termScores = new double[pointers[0].length];
				
				//assign scores to documents for a term
				assignScores(termScores, pointers);
				
				//application dependent modification of scores
				//of documents for a term
				numberOfModifiedDocumentScores = 0;
				//for (int t = 0; t < numOfTermModifiers; t++)
				//	((TermScoreModifier)termModifiers.get(t)).modifyScores(termScores, pointers);
				//application dependent modification of scores
				//of documents for a term. These are predefined by the query
				
				boolFieldModifier.setField(eq.getField());
				boolFieldModifier.setFieldId(fieldId);
				boolFieldModifier.setRequired(true);
				
				numberOfModifiedDocumentScores += boolFieldModifier.modifyScores(termScores, pointers);
				
				//finally setting the scores of documents for a term
				//a mask for setting the occurrences
				short mask = 0;
				if (i<16)
					mask = (short)(1 << i);
				
				int docid;
				int[] pointers0 = pointers[0];
				//int[] pointers1 = pointers[1];
				final int numberOfPointers = pointers0.length;
				
				//If we already have retrieved documents then this part serves only as a constraint
				//Else, we need to also retrieve documents based on this constraint.
				if (foundResults){
					for (int k = 0; k < numberOfPointers; k++) {
						docid = pointers0[k];
						if(termScores[k] > 0.0d){
							satisfying.add(docid);
							//if ((scores[docid] == 0.0d) && (termScores[k] > 0.0d)) 
							//	numberOfRetrievedDocuments++;
						}// else if ((scores[docid] > 0.0d) && (termScores[k] < 0.0d)) {
						//	numberOfRetrievedDocuments--;
						//}
						
						
						//only increase the score, not introduce it.
						if (scores[docid] >0.0d){
							scores[docid] += termScores[k];
							//occurences[docid] |= mask;
						}						
					}
				} else{
					for (int k = 0; k < numberOfPointers; k++) {
						docid = pointers0[k];
						if(termScores[k] > 0.0d)
							satisfying.add(docid);
						if ((scores[docid] == 0.0d) && (termScores[k] > 0.0d)) 
							numberOfRetrievedDocuments++;
						else if ((scores[docid] > 0.0d) && (termScores[k] < 0.0d)) {
							numberOfRetrievedDocuments--;
						}
						scores[docid] += termScores[k];
						occurences[docid] |= mask;
						
					}
				}
				
				queryTerms.addDocumentScoreModifier(new ConstraintModifier(satisfying));
			} 
			else{
				System.err.println("Tag not found, disregarding " + eq.toString());
				resultSet.addInfoMessage("Tag '" + eq.getField().toLowerCase() + "' not found, disregarding " + eq.toString().toLowerCase());
			}
		}
	}
	
	private void matchGreaterThan(MatchingQueryTerms queryTerms){
		Query q;
		q = queryTerms.getQuery();
		ArrayList alist = new ArrayList();
		TermInBoolDICOMFieldModifier boolFieldModifier = new TermInBoolDICOMFieldModifier();
		String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
		for (int i=0; i<rootTags.length; i++){
		if (tagLexicon.findTag(rootTags[i]))		
			boolFieldModifier.addRootTagId(tagLexicon.getTagId());
		}
		
		//Get the GT query
		q.obtainAllOf(uk.ac.gla.terrier.querying.parser.GTFieldQuery.class,alist);
		for(int i=0; i<alist.size(); i++){
			TIntHashSet satisfying = new TIntHashSet();
			GTFieldQuery eq = (GTFieldQuery)alist.get(i);
			System.out.println(eq.toString());

			//Set the range boundaries
			String start = eq.getStartTerm();
			String end = eq.getEndTerm();
			
			//TODO remove this end range
			if (end==null)
				end = "Infinity";
			int fieldId = -1;
			if (tagLexicon.findTag(eq.getField().toLowerCase())){
				//Tag found
				
				TIntHashSet termids = new TIntHashSet(tagLexicon.getTermIds());				
				fieldId = tagLexicon.getTagId();
				System.out.println("Using (" + start + "," + end + ") as range");
				boolean range = comparableLexicon.findRange(start, end);
				if (range){
					double avgDocLength = (double)CollectionStatistics.getAverageDocumentLength();
					
					String [] terms = comparableLexicon.getRangeStringArray();
					int[][] termStats = comparableLexicon.getRangeIntArray();
					long[] byteOffsets = comparableLexicon.getByteOffsets();
					byte[] bitOffsets = comparableLexicon.getBitOffsets();
										
					MatchingQueryTerms matchingTerms = new MatchingQueryTerms();
					
					for (int j=0; j<terms.length; j++){
						
						//Current value
						String term = terms[j];
						matchingTerms.setTermProperty(term, termStats[0][j]);
						if (!termids.contains( termStats[0][j] )){
							//System.out.println(term.trim() + " with id " + matchingTerms.getTermCode(term) +" never occurs in this tag");
							continue;							
						}
						
						//the weighting model is prepared for assigning scores to documents
						wmodel.setKeyFrequency(matchingTerms.getTermWeight(term));
						wmodel.setDocumentFrequency((double)termStats[1][j]);
						wmodel.setTermFrequency((double)termStats[2][j]);
						wmodel.setAverageDocumentLength(avgDocLength);
						
						//check if the IDF is very low.
						/*if (IGNORE_LOW_IDF_TERMS==true && docIndex.getNumberOfDocuments() < termStats[2][j]) {
							System.err.println("query term " + term + " has low idf - ignored from scoring.");
							continue;
						}*/
						
						//Offsets in the comparable inverted index 
						long startOffset = byteOffsets[j];
						byte startBitOffset = bitOffsets[j]; 
						long endOffset = byteOffsets[j+1];
						byte endBitOffset = bitOffsets[j+1];
						
						//Modulo 8
						startBitOffset++;
						if (startBitOffset == 8) {
							startBitOffset = 0;
							startOffset++;
						}
						
						//the postings are being read from the inverted file.
						//System.out.println("Reading Pointer from inverted file...");
						if(termStats[1][j]>ApplicationSetup.INVERTTAG_TRESHOLD){
							pointers = iiT.getDocuments(matchingTerms.getTermCode(term), fieldId);							
							if (pointers==null)
								continue;
						} else
							pointers = comparableInvertedIndex.getDocumentsFast(matchingTerms.getTermCode(term), startBitOffset, startOffset, endBitOffset, endOffset);
																		
						//the scores for the particular value
						double[] termScores = new double[pointers[0].length];
						
						//assign scores to documents for a term
						assignScores(termScores, pointers);
						
						//application dependent modification of scores
						//of documents for a term
						numberOfModifiedDocumentScores = 0;
						//for (int t = 0; t < numOfTermModifiers; t++)
						//	numberOfModifiedDocumentScores += ((TermScoreModifier)termModifiers.get(t)).modifyScores(termScores, pointers);
						
						//application dependent modification of scores
						//of documents for a term. These are predefined by the query
						boolFieldModifier.setField(eq.getField());
						boolFieldModifier.setFieldId(fieldId);
						boolFieldModifier.setRequired(true);
						
						numberOfModifiedDocumentScores += boolFieldModifier.modifyScores(termScores, pointers);
						
						int docid;
						int[] pointers0 = pointers[0];
						final int numberOfPointers = pointers0.length;
						
						//If we already have retrieved documents then this part serves only as a constraint
						//Else, we need to also retrieve documents based on this constraint.
						if(foundResults){
							for (int k = 0; k < numberOfPointers; k++) {
								docid = pointers0[k];
								if (termScores[k] > 0.0d){
									satisfying.add(docid);
								//	if ((scores[docid] == 0.0d) ) {
								//		numberOfRetrievedDocuments++;
								//	}
								}/*else if ((scores[docid] > 0.0d) && (termScores[k] < 0.0d)) {
									numberOfRetrievedDocuments--;
								}*/
								
								//System.out.println(k + " Adding greather than score: " + termScores[k]);
								
								//only increase the score, not introduce it.
								if (scores[docid] >0.0d){
									scores[docid] += termScores[k];
									//occurences[docid] |= mask;
								}
							}
						}else{
							for (int k = 0; k < numberOfPointers; k++) {
								docid = pointers0[k];
								if (termScores[k] > 0.0d){
									satisfying.add(docid);
									if ((scores[docid] == 0.0d) )
										numberOfRetrievedDocuments++;
								}else if ((scores[docid] > 0.0d) && (termScores[k] < 0.0d)) {
									numberOfRetrievedDocuments--;
								}
																
								scores[docid] += termScores[k];
								//occurences[docid] |= mask;								
							}
						}						
					}					
				}
				
				queryTerms.addDocumentScoreModifier(new ConstraintModifier(satisfying));
			} else{
				System.err.println("Tag not found, disregarding " + eq.toString());
				resultSet.addInfoMessage("Tag '" + eq.getField().toLowerCase() + "' not found, disregarding " + eq.toString().toLowerCase());
			}
		}
	}
	
	private void matchLessThan(MatchingQueryTerms queryTerms){
		Query q;
		q = queryTerms.getQuery();
		ArrayList alist = new ArrayList();
		TermInBoolDICOMFieldModifier boolFieldModifier = new TermInBoolDICOMFieldModifier();
		String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
		for (int i=0; i<rootTags.length; i++){
		if (tagLexicon.findTag(rootTags[i]))		
			boolFieldModifier.addRootTagId(tagLexicon.getTagId());
		}
		
		//Get the LT query
		q.obtainAllOf(uk.ac.gla.terrier.querying.parser.LTFieldQuery.class,alist);
		for(int i=0; i<alist.size(); i++){
			//The set containing the documents satisfying the constraint
			TIntHashSet satisfying = new TIntHashSet();
			LTFieldQuery eq = (LTFieldQuery)alist.get(i);
			System.out.println(eq.toString());
			
			//Set the range boundaries
			String end = eq.getEndTerm();
			String start = eq.getStartTerm();
			
			//TODO remove this startboundary
			if(start==null)
				start ="-Infinity";
			int fieldId = -1;
						
			if (tagLexicon.findTag(eq.getField().toLowerCase())){
				//Tag found
				TIntHashSet termids = new TIntHashSet(tagLexicon.getTermIds());
				fieldId = tagLexicon.getTagId();
				boolean range = comparableLexicon.findRange(start, end);
				if (range){
					System.err.println("Using (" + start + "," + end + ") as range");
					double avgDocLength = (double)CollectionStatistics.getAverageDocumentLength();
					
					String [] terms = comparableLexicon.getRangeStringArray();
					int[][] termStats = comparableLexicon.getRangeIntArray();
					long[] byteOffsets = comparableLexicon.getByteOffsets();
					byte[] bitOffsets = comparableLexicon.getBitOffsets();
					
					MatchingQueryTerms matchingTerms = new MatchingQueryTerms();
					
					for (int j=0; j<terms.length; j++){
						
						//The current value
						String term = terms[j];
						matchingTerms.setTermProperty(term, termStats[0][j]);
						if (!termids.contains( termStats[0][j] )){
							//System.out.println(term.trim() + " with id " + matchingTerms.getTermCode(term) +" never occurs in this tag");
							continue;							
						}
						
						wmodel.setKeyFrequency(matchingTerms.getTermWeight(term));
						wmodel.setDocumentFrequency((double) termStats[1][j]);
						wmodel.setTermFrequency((double) termStats[2][j]);
						wmodel.setAverageDocumentLength( avgDocLength );

						//check if the IDF is very low.
						/*if (IGNORE_LOW_IDF_TERMS==true && docIndex.getNumberOfDocuments() <  termStats[2][j]) {
							System.err.println("query term " + term + " has low idf - ignored from scoring.");
							continue;
						}*/
						
						//The offsets in the comparable inverted index
						long startOffset = byteOffsets[j];
						byte startBitOffset = bitOffsets[j]; 
						long endOffset = byteOffsets[j+1];
						byte endBitOffset = bitOffsets[j+1];
												
						//Modulo 8
						startBitOffset++;
						if (startBitOffset == 8) {
							startBitOffset = 0;
							startOffset++;
						}
						
						//the postings are being read from the inverted file.
						if(termStats[1][j]>ApplicationSetup.INVERTTAG_TRESHOLD){
							pointers = iiT.getDocuments(matchingTerms.getTermCode(term), fieldId);
							if (pointers==null)
								continue;
						} else
							pointers = comparableInvertedIndex.getDocumentsFast(matchingTerms.getTermCode(term), startBitOffset, startOffset, endBitOffset, endOffset);
	
						//the scores for the particular value												
						double[] termScores = new double[pointers[0].length];
						
						//assign scores to documents for a term
						assignScores(termScores, pointers);
						
						//application dependent modification of scores
						//of documents for a term
						numberOfModifiedDocumentScores = 0;
						/*for (int t = 0; t < numOfTermModifiers; t++)
							((TermScoreModifier)termModifiers.get(t)).modifyScores(termScores, pointers);
						*/						
						boolFieldModifier.setField(eq.getField());
						boolFieldModifier.setFieldId(fieldId);
						boolFieldModifier.setRequired(true);
						
						numberOfModifiedDocumentScores += boolFieldModifier.modifyScores(termScores, pointers);
						
						int docid;
						int[] pointers0 = pointers[0];
						
						final int numberOfPointers = pointers0.length;
						
						//If we already have retrieved documents then this part serves only as a constraint
						//Else, we need to also retrieve documents based on this constraint.
						if (foundResults){
							for (int k = 0; k < numberOfPointers; k++) {
								docid = pointers0[k];
								if (termScores[k] > 0.0d){
									satisfying.add(docid);
								//	if ((scores[docid] == 0.0d) ) {
								//		numberOfRetrievedDocuments++;
								//	}
								}//else if ((scores[docid] > 0.0d) && (termScores[k] < 0.0d)) {
								//	numberOfRetrievedDocuments--;
								//}						
								
								//only increase the score, not introduce it.
								if (scores[docid] >0.0d){
									scores[docid] += termScores[k];
									//occurences[docid] |= mask;
								}
							}
						} else{
							for (int k = 0; k < numberOfPointers; k++) {
								docid = pointers0[k];
								if (termScores[k] > 0.0d){
									satisfying.add(docid);
									if ((scores[docid] == 0.0d) )
										numberOfRetrievedDocuments++;
								}else if ((scores[docid] > 0.0d) && (termScores[k] < 0.0d)) {
									numberOfRetrievedDocuments--;
								}
	
								scores[docid] += termScores[k];
							}
						}						
					}
				}
				
				queryTerms.addDocumentScoreModifier(new ConstraintModifier(satisfying));
			} else{
				System.err.println("Tag not found, disregarding " + eq.toString());
				resultSet.addInfoMessage("Tag '" + eq.getField().toLowerCase() + "' not found, disregarding " + eq.toString().toLowerCase());
			}
		}
	}
}
