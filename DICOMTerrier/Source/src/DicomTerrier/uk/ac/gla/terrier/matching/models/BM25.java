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
 * The Original Code is BM25.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Ben He <ben{a.}dcs.gla.ac.uk> 
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.matching.models;
import uk.ac.gla.terrier.structures.CollectionStatistics;
/**
 * This class implements the BM25 weighting model. The
 * used parameters are:<br>
 * k_1 = 1.2d<br>
 * k_3 = 1000d<br>
 * b = 0.75d<br>
 * @author Gianni Amati, Ben He, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class BM25 extends WeightingModel {
	/** The constant k_1.*/
	private double k_1 = 1.2d;
	
	/** The constant k_3.*/
	private double k_3 = 1000d;
	
	/** The constant b.*/
	private double b = 0.75d;
	
	/** A default constructor.*/
	public BM25() {
		super();
		numberOfDocuments = CollectionStatistics.getNumberOfDocuments();
	}
	/**
	 * Returns the name of the model.
	 * @return the name of the model
	 */
	public final String getInfo() {
		return "BM25b0.75";
	}
	/**
	 * Uses BM25 to compute a weight for a term in a document.
	 * @param tf The term frequency in the document
	 * @param docLength the document's length
	 * @return the score assigned to a document with the given 
	 *         tf and docLength, and other preset parameters
	 */
	public final double score(double tf, double docLength) {
        double K = k_1 * ((1 - b) + b * docLength / averageDocumentLength) + tf;
        return (tf * (k_3 + 1d) * keyFrequency / ((k_3 + keyFrequency) * K))
                * i.log((numberOfDocuments - documentFrequency + 0.5d) / (documentFrequency + 0.5d));
	}
	/**
	 * Uses BM25 to compute a weight for a term in a document.
	 * @param tf The term frequency in the document
	 * @param docLength the document's length
	 * @param n_t The document frequency of the term
	 * @param F_t the term frequency in the collection
	 * @param keyFrequency the term frequency in the query
	 * @return the score assigned by the weighting model BM25.
	 */
	public final double score(
		double tf,
		double docLength,
		double n_t,
		double F_t,
		double keyFrequency) {
        double K = k_1 * ((1 - b) + b * docLength / averageDocumentLength) + tf;
        return (tf * (k_3 + 1d) * keyFrequency / ((k_3 + keyFrequency) * K))
                * i.log((numberOfDocuments - documentFrequency + 0.5d) / (documentFrequency + 0.5d));
	}
	
}
