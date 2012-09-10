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
 * The Original Code is Idf.java.
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
import uk.ac.gla.terrier.structures.*;
/**
 * This class computes the idf values for specific terms in the collection.
 * @author Gianni Amati, Ben He, Vassilis Plachouras
 */
public final class Idf {
	
	/** The natural logarithm of 2, used to change the base of logarithms.*/
	public static final double LOG_2_OF_E = Math.log(2.0D);
	/** The reciprocal of CONSTANT, computed for efficiency.*/
	public static final double REC_LOG_2_OF_E = 1.0D / LOG_2_OF_E;
	/** The number of documents in the collection.*/
	private double numberOfDocuments;
	/** A default constructor.*/
	public Idf() {
		numberOfDocuments = CollectionStatistics.getNumberOfDocuments();
	}
	
	/** 
	 * A constructor specifying the number of documents in the collection.
	 * @param docs The number of documents.
	 */
	public Idf(double docs) {
		numberOfDocuments = docs;
	}
	
	/**
	 * Returns the idf of d.
	 * @param d The given term frequency
	 * @return the base 2 log of numberOfDocuments/d
	 */
	public final double idf(double d) {
		return (Math.log(numberOfDocuments/d) * REC_LOG_2_OF_E);
	}
	
	/**
	 * Returns the idf of the given number d.
	 * @param d the number for which the idf will be computed.
	 * @return the idf of the given number d.
	 */
	public final double idf(int d) {
		return (Math.log(numberOfDocuments/((double)d)) * REC_LOG_2_OF_E);
	}
	
	/**
	 * Returns the idf of d.
	 * @param d The given term frequency
	 * @return the base 2 log of numberOfDocuments/d
	 */
	public final double idfDFR(double d) {
		return (Math.log((numberOfDocuments+1)/(d+0.5)) * REC_LOG_2_OF_E);
	}
	
	/**
	 * Returns the idf of the given number d.
	 * @param d the number for which the idf will be computed.
	 * @return the idf of the given number d.
	 */
	public final double idfDFR(int d) {
		return (Math.log((numberOfDocuments+1)/((double)d+0.5d)) * REC_LOG_2_OF_E);
	}
	
	/**
	 * The INQUERY idf formula. We need to check again this formula, 
	 * as it seems that there is a bug in the expression
	 * numberOfDocuments - d / d.
	 * @param d the number for which the idf will be computed
	 * @return the INQUERY idf of the number d
	 */
	public final double idfENQUIRY(double d) {
		return (Math.log(numberOfDocuments - d / d) * REC_LOG_2_OF_E);
	}
	
	/**
	 * Return the normalised idf of the given number.
	 * @param d The number of which the idf is computed.
	 * @return the normalised idf of d
	 */
	public final double idfN(double d) {
		return (log(numberOfDocuments, d) / log(numberOfDocuments));
	}
	
	/**
	 * Return the normalised idf of the given number.
	 * @param d The number of which the idf is computed.
	 * @return the normalised idf of d
	 */
	public final double idfN(int d) {
		return (log(numberOfDocuments, (double)d) / log(numberOfDocuments));
	}
	
	/**
	 * The normalised INQUERY idf formula
	 * @param d the number for which we will compute the normalised idf
	 * @return the normalised INQUERY idf of d
	 */
	public final double idfNENQUIRY(double d) {
		return (log(numberOfDocuments + 1.0D, d + 0.5D) / log(numberOfDocuments+1.0D));
	}
	
	/**
	 * Returns the base 2 log of the given double precision number.
	 * @param d The number of which the log we will compute
	 * @return the base 2 log of the given number
	 */
	public final double log(double d) {
		return (Math.log(d) * REC_LOG_2_OF_E);
	}
	
	/**
	 * Returns the base 2 log of d1 over d2
	 * @param d1 the nominator
	 * @param d2 the denominator
	 * @return the base 2 log of d1/d2
	 */
	public final double log(double d1, double d2) {
		return (Math.log(d1/d2) * REC_LOG_2_OF_E);
	}
}
