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
 * The Original Code is TermScoreModifier.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.matching.tsms;
/**
 * The interface that should be implemented by each class that assigns 
 * or modifies a score of a term for a document.
 * @author Vassilis Plachouras, Craig Macdonald
 * @version $Revision: 1.1 $
 */
public interface TermScoreModifier {
	/**
	 * Modifies the scores of the documents for a given 
	 * set of pointers, or postings.
	 * @param termScores double[] the scores of the documents.
	 * @param pointers int[][] the vectors that contain the pointers,
	 *        that is pairs of document identifiers and term frequencies.
	 * @return the number of documents for which the scores were modified.
	 */
	public int modifyScores(double[] termScores, int[][] pointers);
	
	/**
	 * Returns the name of the modifier.
	 * @return String the name of the modifier.
	 */
	public String getName();
}
