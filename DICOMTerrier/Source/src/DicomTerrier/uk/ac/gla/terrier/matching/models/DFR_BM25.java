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
 * The Original Code is DFR_BM25.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 */
package uk.ac.gla.terrier.matching.models;
import uk.ac.gla.terrier.structures.CollectionStatistics;
/**
 * This class implements the DFR_BM25 weighting model.
 * This DFR model, if expanded in Taylor's series, provides the BM25 formula, when the parameter beta 
 *  is set to 1.
 * DFR_BM25 improves BM25 when setting beta to the optimal value (see Ben He & Iadh Ounis' technique)
 * 
 * @author Gianni Amati, Ben He
 * @version $Revision: 1.1 $
 */
public class DFR_BM25 extends WeightingModel {
	/** 
	 * A default constructor. This must be followed 
	 * by specifying the c value.
	 */
	public DFR_BM25() {
		super();
		numberOfDocuments = CollectionStatistics.getNumberOfDocuments();
	}
	/** 
	 * Constructs an instance of this class with the specified 
	 * value for the parameter c.
	 * @param c the term frequency normalisation parameter value.
	 */
	public DFR_BM25(double c) {
		super();
		this.c = c;
	}
	/**
	 * Returns the name of the model.
	 * @return the name of the model
	 */
	public final String getInfo() {
		return "DFR_BM25c" + c;
	}
	/**
	* Computes the score according to the model DFR_BM25.
	* @param tf The term frequency in the document
	* @param docLength the document's length
	* @return the score assigned to a document with the 
	*         given tf and docLength, and other preset parameters
	*/
	public final double score(
			double tf,
			double docLength) {
		double k_1 = 1.2d;
	    double k_3 = 1000d;
		double TF = tf * i.log(1.0d + (c * averageDocumentLength) / docLength);
		double NORM = 1d / (TF + k_1);
		return  ( (k_3 + 1d) * keyFrequency / (k_3 + keyFrequency)) * NORM 
				*TF * i.log((numberOfDocuments - documentFrequency + 0.5d) / 
				(documentFrequency + 0.5d));
	}
	/**
	* Computes the score according to the model DFR_BM25.
	* @param tf The term frequency in the document
	* @param docLength the document's length
	* @param documentFrequency The document frequency of the term
	* @param termFrequency the term frequency in the collection
	* @param keyFrequency the term frequency in the query
	* @return the score returned by the implemented weighting model.
	*/
	public final double score(
			double tf,
			double docLength,
			double documentFrequency,
			double termFrequency,
			double keyFrequency) {
		double k_1 = 1.2d;
	    double k_3 = 1000d;
		double TF = tf * i.log(1.0d + (c * averageDocumentLength) / docLength);
		double NORM = 1d / (TF + k_1);
		return  ( (k_3 + 1d) * keyFrequency / (k_3 + keyFrequency)) * NORM 
				* TF * i.log((numberOfDocuments - documentFrequency + 0.5d) / 
						(documentFrequency + 0.5d));
	}
}
