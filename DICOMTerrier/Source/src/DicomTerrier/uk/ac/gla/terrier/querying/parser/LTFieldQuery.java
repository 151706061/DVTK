package uk.ac.gla.terrier.querying.parser;

import java.util.ArrayList;
import java.util.HashSet;
import java.util.Hashtable;

import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.terms.TermPipelineAccessor;

/**
 * This class represents the range part of a query consisting of less-than.
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class LTFieldQuery extends Query {

	private String startTerm=null;
	private String endTerm=null;
	
	/** The optional field.*/
	String field = null;
	
	/**
	 * An empty default constructor
	 */
	public LTFieldQuery(){}
	
	/**
	 * Constructs a field query from the given query.
	 * @param q the query that is qualified with the field operator.
	 */
	public LTFieldQuery(Query q) 	{
		child = q;
	}
	/**
	 * Constructs a field query from the given query and 
	 * the given field.
	 * @param q Query the query that is qualified with the field operator.
	 * @param f String the field in which the given query should appear in.
	 */
	public LTFieldQuery(Query q, String f)
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
	 * Returns a string representation of the query.
	 * @return String the string of the query.
	 */
	public String toString() {
		if (startTerm==null)
			return field + "<" +endTerm+ "";
		else
			return startTerm + "<" + field + "<" +endTerm+ "";
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
	public void obtainQueryTermsForRange(MatchingQueryTerms terms) {
		//System.out.println("FieldQuery: obtainQueryTerms");
		obtainQueryTermsForRange(terms, true);
	}
	
	/**
	 * Prepares the query for matching by transforming
	 * the query objects to a set of query terms.
	 * @param terms MatchingQueryTerms the object which holds the 
	 *        query terms and their modifiers.
	 * @param required boolean indicates whether the field query
	 *        is required or not.     
	 */
	public void obtainQueryTermsForRange(MatchingQueryTerms terms, boolean required) {
		//System.out.println("FieldQuery: obtainQueryTerms with " + required);
		//child.obtainQueryTerms(terms);
		if (child!=null){
			ArrayList alist = new ArrayList();
			child.getTerms(alist);
			SingleTermQuery[] queryTerms = (SingleTermQuery[])alist.toArray(tmpSTQ);
			for (int i=0; i<queryTerms.length; i++)
				terms.setTermProperty(queryTerms[i].getTerm());
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
		//we don't have terms orchildren
		return true;
	}
	
	public boolean applyTermPipeline(TermPipelineAccessor tpa)
	{
		//we don't have terms orchildren
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
		if(child!=null){
			if (c.isInstance(this) && req)
				child.getTerms(alist);
			
			child.getTermsOf(c, alist, req);
		}
	}
	
	/**
	 * Get the start of the range
	 * @return Start of the range
	 */
	public String getStartTerm(){
		return startTerm;
	}
	
	/**
	 * Set the start of the range
	 * @param startTerm Start of the range
	 */
	public void setStartTerm(String startTerm){
		this.startTerm = startTerm;
	}	
	
	/**
	 * Get the end of the range 
	 * @return The end of the range
	 */
	public String getEndTerm(){
		return endTerm;
	}
	
	/**
	 * Set the end of the range
	 * @param endTerm The end of the range
	 */
	public void setEndTerm(String endTerm){
		this.endTerm = endTerm;
	}	
	
}
