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
 * The Original Code is DocumentScoreModifier.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.matching.dsms;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.structures.Index;
/**
 * The interface that should be implemented by each class that assigns 
 * or modifies a score of a document.
 * @author Vassilis Plachouras, Craig Macdonald
 * @version $Revision: 1.1 $
 */
public interface DocumentScoreModifier {
	/**
	 * Modifies the scores of the documents for a given 
	 * query. This returns a boolean that allows the Matching class
	 * to determine if the scores of the documents have actually been
	 * altered. This is because the resultset has to be resorted after
	 * each one. 
	 * @param index Index the data structures used for retrieval.
	 * @param queryTerms TermTreeNodes[] the query terms
	 * @param resultSet ResultSet the current set of results.
	 * @return true if any scores have been altered
	 */
	public boolean modifyScores(Index index, MatchingQueryTerms queryTerms, ResultSet resultSet);
	
	/** 
	 * Returns the name of the document score modifier.
	 * @return String the name of the document score modifier.
	 */
	public String getName();
}
