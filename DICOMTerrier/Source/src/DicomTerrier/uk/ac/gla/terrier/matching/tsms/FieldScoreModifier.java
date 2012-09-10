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
 * The Original Code is FieldScoreModifier.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.matching.tsms;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * Modifies the scores of a term for a document, given 
 * the tags the term appears in the document. This class
 * implements the TermScoreModifier interface.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $.
 */
public class FieldScoreModifier implements TermScoreModifier {
	/** 
	 * Modifies the scores of documents for a particular term, based on 
	 * the fields a term appears in documents.
	 * @param scores double[] the scores of the documents.
	 * @param pointers int[][] the pointers read from the inverted file 
	 *        for a particular query term.
	 * @return the number of documents for which the scores were modified. 
	 */
	public int modifyScores(double[] scores, int[][] pointers) {
		int[] fieldscores = pointers[2];
		final int numOfPointers = fieldscores.length;
		int numOfModifiedDocs = 0;
		//for each document that contains the query term, the score is computed.
		int fieldScore;
		for (int j = 0; j < numOfPointers; j++) {
			//apply the html modifiers for the term to this score
			fieldScore = fieldscores[j];
			if(fieldScore > 0) {
				if (scores[j]!=Double.NEGATIVE_INFINITY)
					numOfModifiedDocs++;
				scores[j] += FieldScore.applyFieldScoreModifier(fieldScore, scores[j]);
			}
		}
		return numOfModifiedDocs;
	}
	
	public String getName() {
		return "FieldScoreModifier";
	}
}
