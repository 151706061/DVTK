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
 * The Original Code is SingleTermQuery.java.
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
import uk.ac.gla.terrier.matching.tsms.RequiredTermModifier;
import uk.ac.gla.terrier.terms.TermPipelineAccessor;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * Models a query of a single term. 
 * The single term queries can be of the forms:<br>
 * term<br>
 * +term<br>
 * -term<br>
 * field:term<br>
 * The term can be optionally followed by a weight, as shown below:<br>
 * term^weight<br>
 * +term^weight<br>
 * -term^weight<br>
 * field:term^weight<br>
 * where weight is a real number. If no weight is specified, then the 
 * default weight is 1.0.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class SingleTermQuery extends Query {
	/** checks whether the terms should be lowercase. */
	protected static final boolean lowercase = (new Boolean(ApplicationSetup.getProperty("lowercase", "true"))).booleanValue();
	
	/** The query term string.*/
	private String term = null;
	
	/** 
	 * The optional required qualifier. It takes the values -1 
	 * that corresponds to '-', +1 that corresponds to '+' and
	 * 0 that corresponds to unspecified. The default value is 0
	 */
	int required = 0;
	
	/** The weight of a query term. */
	double weight = 1.0d;
	/** An empty default constructor.*/
	public SingleTermQuery() {}
	/** 
	 * Creates an instance of the class for the 
	 * given query term.
	 * @param t String one query term.
	 */
	public SingleTermQuery(String t) {
		if (lowercase) 
			//term = removeNonAlphaNumChars(t.toLowerCase());
			term = t.toLowerCase();
		else 
			//term = removeNonAlphaNumChars(t);
			term = t;
	}
	
	/**
	 * Creates an instance of the class for the
	 * given query term that should be either required
	 * or not.
	 * @param t String one query term.
	 * @param r int indicates whether the term is required
	 *        or not.
	 */
	public SingleTermQuery(String t, int r) {
		if (lowercase)
			//term = removeNonAlphaNumChars(t.toLowerCase());
			term = t.toLowerCase();
		else 
			//term = removeNonAlphaNumChars(t);
			term = t;
		required = r;
	}
	/**
	 * Sets the value of the required switch.
	 * @param r int the value of the required switch.
	 */
	public void setRequired(int r) {
		required = r;
	}
	/** 
	 * Sets the string of the term.
	 * @param t String the query term.
	 */
	public void setTerm(String t) {
		if (lowercase) 
			//term = removeNonAlphaNumChars(t.toLowerCase());
			term = t.toLowerCase();
		else 
			//term = removeNonAlphaNumChars(t);
			term = t;
	}
	/**
	 * Gets the query term.
	 * @return String the query term.
	 */
	public String getTerm() {
		return term;
	}
	/**
	 * Indicates whether the query term is required, or it 
	 * isn't required to appear in the retrieved documents. 
	 * A value of zero means that this has been left unspecified.
	 * @return int an indication of whether the query term should 
	 *         appear in the retrieved documents, or not. 
	 */
	public int getRequired() {
		return required;
	}
	
	/**
	 * Returns a string representation of the query term.
	 * @return String a string representation of the query term.
	 */
	public String toString() {
		String output = "";
		if (required==1)
			output += "+";
		else if (required==-1)
			output += "!";
		
		output += term;
		if (weight!=1.0d) 
			output += "^" + weight;
		return output;
	}
	/**
	 * Sets the weight of the query term.
	 * @param w double the weight of the query term.
	 */
	public void setWeight(double w) {
		weight = w;
	}
	/**
	 * Returns the weight of the query term.
	 * @return double the weight of the query term.
	 */
	public double getWeight() {
		return weight;
	}
	/**
	 * Puts the query term in the given 
	 * term pipeline, which is accessed through the 
	 * given accessor object.
	 * @param tpa TermPipelineAccessor the object that provides
	 *        the term pipeline.
	 */
	public boolean applyTermPipeline(TermPipelineAccessor tpa)
	{
		String t = tpa.pipelineTerm(term);
		if (t == null)
		{
			return false;
		}
		term = t;
		return true;
	}
	
	/**
	 * Stores the term of the single term query in the 
	 * given hash map.
	 * @param terms the hashmap in which to store the query terms.
	 */
	public void obtainQueryTerms(MatchingQueryTerms terms) {
		terms.setTermProperty(term);
		terms.addTermPropertyWeight(term, weight);
	}
	
	/**
	 * Stores the term of the single term query in the 
	 * given hash map.
	 * @param terms the hashmap in which to store the query terms.
	 * @param required indicates whether the query term is required or not.
	 */
	public void obtainQueryTerms(MatchingQueryTerms terms, boolean required) {
		terms.setTermProperty(term);
		terms.addTermPropertyWeight(term, weight);
		terms.setTermProperty(term, new RequiredTermModifier(required));
	}
	
	/**
	 * Adds the query term in the given list of query terms.
	 * @param alist ArrayList the array list that stores the
	 *        query terms. 
	 */
	protected void getTerms(ArrayList alist) {
		alist.add(this);
	}
	/** This object cannot contain any controls, so this method will always return false.
	  * @return false */
	public boolean obtainControls(HashSet allowed, Hashtable controls)
	{
		return false;
	}
	
	/** 
	 * An empty method because the single term query 
	 * cannot have children.
	 * @param c Class a class of queries.
	 * @param alist ArrayList the list of query terms.
	 * @param req boolean indicates whether the subqueries 
	 *        are required or not.
	 */
	public void getTermsOf(Class c, ArrayList alist, boolean req) {
		if (c.isInstance(this) && req)
			alist.add(this);
	}
	
	protected String removeNonAlphaNumChars(String t) {
		final int t_length = t.length();
		StringBuffer buffer = new StringBuffer(t_length);
		char c;
		for (int i=0; i<t_length; i++) {
			if (Character.isLetterOrDigit(c = t.charAt(i)))
				buffer.append(c);
		}
		return buffer.toString();
	}
}
