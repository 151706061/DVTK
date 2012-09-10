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
 * The Original Code is BooleanScoreModifier.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author) 
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.matching.dsms;
import java.util.ArrayList;
import java.util.HashSet;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.querying.parser.SingleTermQuery;
import uk.ac.gla.terrier.structures.Index;
/**
 * If not all the required query terms appear in a document, then this
 * modifier zeros the document's score.  
 * @author Vassilis Plachouras and Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class BooleanScoreModifier implements DocumentScoreModifier {
	/** 
	 * The terms to check. If this is null, then we 
	 * check for the whole query. This property can 
	 * only be set from the constructor.
	 */
	protected ArrayList terms = null;
	
	/**
	 * An empty default constructor. 
	 */
	public BooleanScoreModifier() {}
	
	/** 
	 * A constructor where we specify which of the 
	 * query terms should exist in the documents.
	 * @param ts ArrayList the query terms that should 
	 *        appear in the retrieved documents after
	 *        applying this modifier.
	 */
	public BooleanScoreModifier(ArrayList ts) {
		terms = ts;
	}
	
	/** 
	 * Returns the name of the document score modifier.
	 * @return String the name of the modifier.
	 */
	public String getName() {
		return "BooleanScoreModifier";
	}
	
	/**
	 * Zeros the scores of documents in which only some
	 * of the query terms appear.
	 * @param index Index the data structures used for retrieval.
	 * @param query TermTreeNode[] the array of the query terms.
	 * @param resultSet ResultSet the set of retrieved documents.
	 */
	public boolean modifyScores(Index index, MatchingQueryTerms query, ResultSet resultSet) {
		
		System.out.println("Using Boolean Document Score Modifier");
		
		short[] occurrences = resultSet.getOccurrences();
		double[] scores = resultSet.getScores();
		//int[] docids = resultSet.getDocids();
		int size = resultSet.getResultSize();
		int start = 0;
		int end = size;
		int numOfModifiedDocumentScores = 0;
		short queryLengthMask = 0;
		
		//set the bit mask
		if (terms !=null) {
			HashSet set = new HashSet();
			for (int i=0; i<terms.size(); i++) 
				set.add( ((SingleTermQuery)terms.get(i)).getTerm());
			String[] queryTerms = query.getTerms();
			for (int i=0; i < queryTerms.length; i++) {
				if (set.contains((String)queryTerms[i]))
					queryLengthMask |= (short)(1 << i);
			}
			
		} else {
			for (int i = 0; i < query.length(); i++) {
				queryLengthMask = (short)((queryLengthMask << 1) + 1);
			}
		}
		//modify the scores
		for (int i = start; i < end; i++) {
			//double s = scores[i];
			if ((occurrences[i] & queryLengthMask) != queryLengthMask) {
				if (scores[i] > Double.NEGATIVE_INFINITY){
					numOfModifiedDocumentScores++;					
				}
				scores[i] = Double.NEGATIVE_INFINITY;
			}
		}
		System.out.println("Modified " + numOfModifiedDocumentScores + " documents");
		if (numOfModifiedDocumentScores == 0)
			return false;
		resultSet.setResultSize(size -numOfModifiedDocumentScores);
		
		return true;
	}
	
}
