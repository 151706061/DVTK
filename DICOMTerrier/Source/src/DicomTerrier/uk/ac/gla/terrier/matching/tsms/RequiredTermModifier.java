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
 * The Original Code is RequiredTermModifier.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.matching.tsms;
/**
 * Resets the scores of documents according to whether a term is required 
 * or not, and whether it appears in the retrieved documents. This class 
 * implements the TermScoreModifier interface.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $ 
 */
public class RequiredTermModifier implements TermScoreModifier {
	/**
	 * Indicates whether a query term is required, not required,
	 * or whether this has been left unspecified.
	 */
	boolean required; 
	
	/**
	 * Constructs an instance of a TermInFieldModifier given the
	 * requirement that the term should, or should not appear
	 * in the query. 
	 * @param r boolean indicates whether the term is required
	 *        to appear in the retrieved documents.
	 */
	public RequiredTermModifier(boolean r) {
		required = r;
	}
	
	/** 
	 * Resets the scores of documents for a particular term, based on 
	 * the requirement of appearance of the corresponding term.
	 * @param scores double[] the scores of the documents.
	 * @param pointers int[][] the pointers read from the inverted file 
	 *        for a particular query term.
	 * @return the number of documents for which the scores were modified.
	 */
	public int modifyScores(double[] scores, int[][] pointers) {
		final int numOfPointers = scores.length;
		int numOfModifiedDocs = 0;
		//for each document that contains the query term, the score is modified.
		//double score;
		for (int j = 0; j < numOfPointers; j++) {
			//filter out results for which the 
			//requirement for the query term are not met. 
			//if( ((score=scores[j])>0.0d && !required ) || ((score == 0.0d) && required) ) {
			if(!required) {	
				if (scores[j]!=Double.NEGATIVE_INFINITY)
					numOfModifiedDocs++;
				scores[j] = Double.NEGATIVE_INFINITY;
			}
		}
		return numOfModifiedDocs;
	}
	
	public String getName() {
		return "RequiredTermModifier("+required+")";
	}
}
