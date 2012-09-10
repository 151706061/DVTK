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
 * The Original Code is BooleanFallback.java.
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
import java.util.Iterator;

import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.querying.parser.Query;
import uk.ac.gla.terrier.querying.parser.RequirementQuery;
import uk.ac.gla.terrier.querying.parser.SingleTermQuery;
import uk.ac.gla.terrier.structures.Index;

/**
 * This class provides a boolean fallback document score modifier for 
 * matching. In other words, if there any of the retrieved documents
 * contain all undecorated query terms (ie query terms without any operators),
 * then we remove from the result set documents that do not contain all
 * undecorated query terms. Otherwise, we do nothing.
 * 
 * @author Vassilis Plachouras &amp; Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class BooleanFallback implements DocumentScoreModifier {

	/** Builds a hashset containing all terms that are required NOT
	 * to be in the query
	 * @param q The original query as was used to generate MatchingQueryTerms
	 * @return See above
	 */
	protected HashSet getMinusTerms(Query q)
	{
		ArrayList requirements = new ArrayList();
		q.obtainAllOf(RequirementQuery.class, requirements);
		Iterator i = requirements.iterator();
		ArrayList terms = new ArrayList();
		while(i.hasNext())
		{
			RequirementQuery r = (RequirementQuery)i.next();
			if (! r.getRequired())
				r.obtainAllOf(SingleTermQuery.class, terms);
		}
		i = terms.iterator();
		HashSet rtr = new HashSet(terms.size());
		while(i.hasNext())
		{
			
			String t = ((SingleTermQuery)i.next()).getTerm(); 
			//rtr.add(((SingleTermQuery)i.next()).getTerm());
			
			rtr.add(t);
			System.err.println("-"+t);
		}
		return rtr;
	}
	
	/**
	 * Applies boolean fallback to the given result set.
	 * @param index The data structures used for retrieval.
	 * @param queryTerms the terms of the query.
	 * @param resultSet the set of retrieved documents for the query.
	 * @return true if any scores have been altered
	 */
	public boolean modifyScores(Index index, MatchingQueryTerms queryTerms, ResultSet resultSet) {
		
		System.out.println("Using Boolean Fallback Document Score Modifier");
		long start = System.currentTimeMillis();
		
		/* generate the query mask */
		short queryMask = (short)0;
		//get all the query terms
		String[] queryTermStrings = queryTerms.getTerms();
		
		if (queryTermStrings.length < 2)
			return false;

		//get all the "requirement false" (ie -ve) terms
		HashSet reqs = getMinusTerms(queryTerms.getQuery());
		
		for (int i=0;i<queryTermStrings.length;i++) //for each (all terms)
		{
			
			//mask 1 IFF terms does NOT occur in -ve terms
			if (!reqs.contains(queryTermStrings[i]))
				queryMask = (short) (queryMask | (1 << i)); 
		}
		
		//creating local references to faraway arrays
		short[] occurrences = resultSet.getOccurrences();
		double[] scores = resultSet.getScores();
		
		/* see if any documents match the query mask */
		boolean applyFilter = false;
		final int numOfDocs = resultSet.getResultSize();
		for (int i=0; i<numOfDocs; i++) {
			if (scores[i]>0.0d && ((occurrences[i] & queryMask) == queryMask)) {
				applyFilter = true;
				break;
			}
		}
		
		/* if any documents do match the query mask, remove any documents
		that do not the query mask */
		int numOfModifiedDocs = 0;
		if (applyFilter) {
			for (int i=0; i<numOfDocs; i++) {
				if (scores[i] > 0.0d && ((occurrences[i] & queryMask) != queryMask)) {
					numOfModifiedDocs++;
					scores[i] = Double.NEGATIVE_INFINITY;
				}
			}
			resultSet.setResultSize(numOfDocs -numOfModifiedDocs);
			System.out.println("Modified " + numOfModifiedDocs + " documents in " + (System.currentTimeMillis()-start) + " milliseconds");
			return true;
		}
		System.out.println("Modified " + numOfModifiedDocs + " documents in " + (System.currentTimeMillis()-start) + " milliseconds");
		return false;		
	}

	/** 
	 * Returns the name of the modifier, which is BooleanFallback.
	 * @return the name of the modifier.
	 */
	public String getName() {
		return "BooleanFallback";
	}

}
