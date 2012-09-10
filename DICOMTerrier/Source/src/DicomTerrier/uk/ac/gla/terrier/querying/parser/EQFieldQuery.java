package uk.ac.gla.terrier.querying.parser;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.Hashtable;

import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.tsms.TermInBoolDICOMFieldModifier;
import uk.ac.gla.terrier.terms.TermPipelineAccessor;

/**
 * This class represents the equality part of a query.
 * It contains the tag and the field of the equality
 * @author Gerald van Veldhuijsen
 * @version 1.0 
 */

public class EQFieldQuery extends Query {

	/** The field.*/
	String field = null;
	
	/**
	 * An empty default constructor
	 */
	public EQFieldQuery(){}
	
	/**
	 * Constructs a field query from the given query.
	 * @param q the query that is qualified with the field operator.
	 */
	public EQFieldQuery(Query q) 	{
		child = q;
	}
	/**
	 * Constructs a field query from the given query and 
	 * the given field.
	 * @param q Query the query that is qualified with the field operator.
	 * @param f String the field in which the given query should appear in.
	 */
	public EQFieldQuery(Query q, String f)
	{
		child = q;
		field = f.toUpperCase();
	}
	/**
	 * Gets the field that the query term should appear.
	 * @return String the field that the query term should
	 *         appear.
	 */
	public String getField() {
		return field;
	}
	/** 
	 * Sets the value of the field.
	 * @param f String the value of the field.
	 */
	public void setField(String f) {
		field = f.toUpperCase();
	}
	
	/**
	 * Applies a term pipeline in the query's terms, through
	 * the given term pipeline accessor.
	 * Off course we don't do anything here
	 * @param tpa TermPipelineAccessor the object that provides
	 *        access to the term pipeline.
	 * @return boolean true if the query is not empty, otherwise returns false.
	 */
	public boolean applyTermPipeline(TermPipelineAccessor tpa)
	{
		return true;
	}
	
	/**
	 * Returns a string representation of the query.
	 * @return String the string of the query.
	 */
	public String toString() {
		return getField() + "=[" +child.toString()+ "]";
	}
	
	/**
	 * Prepares the query for matching by transforming
	 * the query objects to a set of query terms.
	 * @param terms MatchingQueryTerms the object which holds the 
	 *        query terms and their modifiers.
	 */
	public void obtainQueryTerms(MatchingQueryTerms terms) {
		//System.out.println("FieldQuery: obtainQueryTerms");
		obtainQueryTerms(terms, true);
	}
	
	/**
	 * Must be overridden to do nothing 
	 * @param terms MatchingQueryTerms the structure that is used for 
	 *        modelling a query for matching.
	 * @param required boolean specifies whether the subqueries are 
	 *        required or not.
	 */
	public void obtainQueryTerms(MatchingQueryTerms terms, boolean required) {		
	}
	
	
	/**
	 * Prepares the query for matching by transforming
	 * the query objects to a set of query terms.
	 * @param terms MatchingQueryTerms the object which holds the 
	 *        query terms and their modifiers.
	 */
	public void obtainQueryEqualTerms(MatchingQueryTerms terms) {
		obtainQueryEqualTerms(terms, true);
	}
	
	/**
	 * Prepares the query for matching by transforming
	 * the query objects to a set of query terms.
	 * @param terms MatchingQueryTerms the object which holds the 
	 *        query terms and their modifiers.
	 * @param required boolean indicates whether the field query
	 *        is required or not.     
	 */
	public void obtainQueryEqualTerms(MatchingQueryTerms terms, boolean required) {
		child.obtainQueryTerms(terms);
		ArrayList alist = new ArrayList();
		child.getTerms(alist);
		SingleTermQuery[] queryTerms = (SingleTermQuery[])alist.toArray(tmpSTQ);
		for (int i=0; i<queryTerms.length; i++){
			terms.setTermProperty(queryTerms[i].getTerm(), new TermInBoolDICOMFieldModifier(field, required));
		}
	}
	
	/** Checks to see if field name is a valid control name, as specified by
	  * allowed, and if so adds it to the controls table and returns true to 
	  * specify that this Query object is now dead. 
	  * @param allowed A hashset of lowercase control names that may be set by user queries.
	  * @param controls The hashtable to add the found controls to 
	  * @return true if this node should now be removed, false otherwise */
	public boolean obtainControls(HashSet allowed, Hashtable controls)
	{
		return true;
	}
	
	/** 
	 * Returns all the query terms, in subqueries that
	 * are instances of a given class
	 * @param c Class a class of queries.
	 * @param alist ArrayList the list of query terms.
	 * @param req boolean indicates whether the subqueries 
	 *        are required or not.
	 */
	public void getTermsOf(Class c, ArrayList alist, boolean req) {		
		if (c.isInstance(this) && req)
			child.getTerms(alist);
		
		child.getTermsOf(c, alist, req);
	}
	
}
