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
 * The Original Code is ResultSet.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.matching;
/**
 * The interface that defines the functionalities of a 
 * result set. 
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public interface ResultSet {
	/**
	 * Adds a metadata value for a given document
	 * @param name the name of the metadata type. For example, it can be url for adding the URLs of documents.
	 * @param docid the document identifier of the document.
	 * @param value the metadata value.
	 */
	public void addMetaItem(String name, int docid, String value);
	/**
	 * Adds the metadata values for all the documents in the result set.
	 * The length of the metadata array values should be equal to the 
	 * length of the docids array.
	 *
	 * @param name the name of the metadata type. For example, it can be url for adding the URLs of documents.
	 * @param values the metadata values.
	 */
	public void addMetaItems(String name, String[] values);
	/**
	 * Returns the documents ids after retrieval
	 */
	public int[] getDocids();
	/**
	 * Returns the exact size of the result set.
	 * @return int the exact size of the result set
	 */
	public int getExactResultSize();
	/**
	 * Gets a metadata value for a given document. If the requested
	 * metadata information is not specified, then we return null.
	 * @param name the name of the metadata type. 
	 * @param docid the document identifier of the document.
	 * @return a string with the metadata information, or null of the metadata is not available.
	 */
	public String getMetaItem(String name, int docid);
	
	/**
	 * Gets the metadata information for all documents. If the requested
	 * metadata information is not specified, then we return null.
	 * @param name the name of the metadata type. 
	 * @return an array of strings with the metadata information, or null of the metadata is not available.
	 */
	public String[] getMetaItems(String name); 
	/**
	 * Returns the occurrences array.
	 * @return int[] the array the occurrences array.
	 */
	public short[] getOccurrences();
	
	/**
	 * Returns the effective size of the result set.
	 * @return int the effective size of the result set
	 */
	public int getResultSize();
	
	/**
	 * Returns the documents scores after retrieval
	 */
	public double[] getScores();
	
	/**
	 * Initialises the arrays prior of retrieval. 
	 */
	public void initialise();
	
	/**
	 * Initialises the result set with the given scores. If the 
	 * length of the given array is different than the length
	 * of the internal arrays, then we re-allocate memory
	 * and create the arrays.
	 * @param scs double[] the scores to initiliase the result set with.
	 */
	public void initialise(double[] scs);
	
	/**
	 * Sets the exact size of the result set, that is 
	 * the number of documents  that contain at least one query term.
	 * @param newExactResultSize int the effective size of the result set.
	 */
	public void setExactResultSize(int newExactResultSize);
	
	/**
	 * Sets the effective size of the result set, that 
	 * is the number of documents to be sorted after retrieval.
	 * @param newResultSize int the effective size of the result set.
	 */
	public void setResultSize(int newResultSize);
	
	/**
	 * Crops the existing result file and extracts a subset
	 * from the given starting point, with the given length.
	 * @param start the beginning of the subset.
	 * @param length the length of the subset.
	 * @return a subset of the current result set.
	 */
	public ResultSet getResultSet(int start, int length);
	
	/**
	 * Extracts a subset of the resultset given by the list parameter,
	 * which contains a list of <b>positions</b> in the resultset that
	 * should be saved.
	 * @param list the list of elements in the current list that should be kept.
	 * @return a subset of the current result set specified by the list.
	 */
	public ResultSet getResultSet(int[] list);
	
	/**
	 * Get the info message that was set during the process of building the resultset.
	 * This can be used to pass additional info
	 * @return String infoMessage
	 */
	public String getInfoMessage();
	
	/**
	 * Add the info message that was set during the process of building the resultset.
	 * This can be used to pass additional info
	 * @param msg String the message to pass
	 */
	public void addInfoMessage(String msg);
	
	/**
	 * Set the info message that was set during the process of building the resultset.
	 * This can be used to pass additional info
	 * @param msg String the message to pass
	 */
	public void setInfoMessage(String msg);
}
