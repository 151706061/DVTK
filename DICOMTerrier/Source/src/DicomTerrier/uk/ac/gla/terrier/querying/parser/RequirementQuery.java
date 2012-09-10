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
 * The Original Code is RequirementQuery.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.querying.parser;
import java.util.ArrayList;
import java.util.HashSet;
import java.util.Hashtable;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
/**
 * Models a query where the query terms have been qualified
 * with a requirement operator, either plus, or minus.
 * @author Vassilis Plachouras &amp; Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class RequirementQuery extends Query {
	/** 
	 * The query requirement. The default value is true. */
	private boolean MustHave = true;
	
	/** An empty default constructor. */
	public RequirementQuery(){}
	
	/** 
	 * Sets whether the query is required or not.
	 * @param needed boolean indicates whether the query is required or not.
	 */
	public void setRequired(boolean needed) {
		MustHave = needed;
	}
	
	/** Returns True if the subquery is REQUIRED to exist, or
	 * false if it REQUIRED to NOT exit.
	 * @return See above.
	 */
	public boolean getRequired()
	{
		return MustHave;
	}
	
	/**
	 * Returns a string representation of the query.
	 * @return String a string representation of the query.
	 */
	public String toString() {
		if (child instanceof MultiTermQuery)
		{
			return (MustHave ? "+" : "!") + "(" +child.toString()+ ")";
		}
		return (MustHave ? "+" : "!") + child.toString();
	}
	
	/**
	 * Stores the terms of the query in the given structure, which 
	 * is used for matching documents to the query. 
	 * @param terms MatchingQueryTerms the structure that holds the query
	 *        terms for matching to documents. 
	 */
	public void obtainQueryTerms(MatchingQueryTerms terms) {
		child.obtainQueryTerms(terms, MustHave);
	}
    /** 
     * This object cannot contain any controls, 
     * so this method will always return false.
     * @return false 
     */
	public boolean obtainControls(HashSet allowed, Hashtable controls)
	{
		return false;
	}
	
	/** 
	 * Returns all the query terms, in subqueries that
	 * are instances of a given class.
	 * @param c Class a class of queries.
	 * @param alist ArrayList the list of query terms.
	 * @param req boolean indicates whether the subqueries 
	 *        are required or not.
	 */
	public void getTermsOf(Class c, ArrayList alist, boolean req) {		
		if (PhraseQuery.class.isInstance(child) && MustHave == false )
			return;
		if (c.isInstance(this) && req == MustHave)
			this.getTerms(alist);
		
		child.getTermsOf(c, alist, (req == MustHave));
	}
}
