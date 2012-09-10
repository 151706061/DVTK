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
 * The Original Code is Query.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.querying.parser;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.HashSet;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.terms.TermPipelineAccessor;
/**
 * An abstract class that models a query, that consists of 
 * subqueries and query terms. The subqueries can be phrase queries,
 * field queries, required queries, and combinations of those queries.
 * @author Vassilis Plachouras &amp; Craig Macdonald
 * @version $Revision: 1.1 $
 */
public abstract class Query {
	
	/** An encapsulated query. */
	protected Query child = null;
	
	/** 
	 * Returns all the query terms, in subqueries that
	 * are instances of a given class
	 * @param c Class a class of queries.
	 * @param alist ArrayList the list of query terms.
	 * @param req boolean indicates whether it the 
	 *        subqueries are required or not.
	 */
	public abstract void getTermsOf(Class c, ArrayList alist, boolean req);
	/** 
	 * Sets the subquery object of this query.
	 * @param q Query the subquery object.
	 */
	public void setChild(Query q)
	{
		child = q;
	}
	
	/**
	 * Applies a term pipeline in the query's terms, through
	 * the given term pipeline accessor.
	 * @param tpa TermPipelineAccessor the object that provides
	 *        access to the term pipeline.
	 * @return boolean true if the query is not empty, otherwise returns false.
	 */
	public boolean applyTermPipeline(TermPipelineAccessor tpa)
	{
		if (child!=null) 
			return child.applyTermPipeline(tpa);
		return false;
	}
	public boolean obtainControls(HashSet allowed, Hashtable controls)
	{
		if (child!= null)
			return child.obtainControls(allowed, controls);
		return false;
	}
	
	/**
	 * Stores the terms of the query in an structure used for matching
	 * documents to the query. 
	 * @param terms MatchingQueryTerms the structure that is used for 
	 *        modelling a query for matching.
	 */
	public void obtainQueryTerms(MatchingQueryTerms terms) {
		if (child != null)
			child.obtainQueryTerms(terms);
	}
	
	/**
	 * Stores the terms of the query in an structure used for matching
	 * documents to the query. 
	 * @param terms MatchingQueryTerms the structure that is used for 
	 *        modelling a query for matching.
	 * @param required boolean specifies whether the subqueries are 
	 *        required or not.
	 */
	public void obtainQueryTerms(MatchingQueryTerms terms, boolean required) {
		if (child != null)
			child.obtainQueryTerms(terms, required);
	}
	
	/**
	 * Returns the terms of the query.
	 * @param alist A structure that contains the query terms.
	 */
	protected void getTerms(ArrayList alist) {
		if (child != null)
			child.getTerms(alist);
	}
	
	public void obtainAllOf(Class c, ArrayList a)
	{
		if (c.isInstance(this))
		{
			a.add(this);
		}
		if (child != null)
			child.obtainAllOf(c, a);
	}
	
	/** An attribute used constructing arrays of the right type. */
	protected static final SingleTermQuery[] tmpSTQ = new SingleTermQuery[1];
}
