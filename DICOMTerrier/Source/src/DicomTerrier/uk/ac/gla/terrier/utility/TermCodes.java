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
 * The Original Code is TermCodes.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.utility;
import gnu.trove.*;
/**
 * <p>This class is used for assigning codes to terms as we 
 * index a document collection.</p>
 * <p>It makes use of two properties from the default 
 * properties file. The first one is <tt>termcodes.initialcapacity</tt>, 
 * which specifies the initial capacity of the used hash map. The default 
 * value is 3000000.</p>
 * <p>The second property is <tt>termcodes.garbagecollect</tt>, 
 * which enables or disables garbage collection during the call 
 * of the method reset(). The default value is <tt>true</tt>.
 *
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class TermCodes {
	/** The initial capacity of the hashmap.*/
	private static int hashMapCapacity;
	
	/** 
	 * The hashmap that stores the mapping 
	 * from terms hash codes to code.
	 */
	private static TObjectIntHashMap map = new TObjectIntHashMap(hashMapCapacity);
	/** 
	 * The counter that represents the new 
	 * code for the next not already encountered term.
	 */
	private static int counter = 0;
	/** A buffer variable.*/
	private static int code = 0;
	/** 
	 * The property that enables or disables 
	 * garbage collection during reseting.
	 */
	private static boolean garbageCollection;
	/** 
	 * Static initialisation of the class properties from 
	 * the properties file. It calls the method initialise().
	 */
	static {
		initialise();
	}
	/** 
	 * Initialises the properties from the property file.
	 * The initial capacity of the hash map, is set to the 
	 * value of the property <tt>termcodes.initialcapacity</tt>.
	 * The default value is <tt>3000000</tt>. The second property 
	 * is related to the method reset() and enables or disables 
	 * garbage collection when the reset method is called. 
	 * The corresponding property is <tt>termcodes.garbagecollect</tt>, 
	 * and its default property is <tt>true</tt>.
	 */
	 public static void initialise() {
		hashMapCapacity = Integer.parseInt(
			ApplicationSetup.getProperty("termcodes.initialcapacity",
					                                  "3000000"));
		garbageCollection = 
			(new Boolean(ApplicationSetup.getProperty("termcodes.garbagecollect",
					                                  "true"))).booleanValue();
	 }
	
	/**
	 * Returns the code for a given term.
	 * @param term String the term for which 
	 *        the code will be returned.
	 * @return int the code for the given term
	 */
	public static int getCode(String term) {
		//if we have encountered a new term, add it to the
		//hash map and return the new term code, otherwise
		//return the already assigned term code 
		if (!map.containsKey(term)) {
			map.put(term, (code=counter));
			//System.out.println("Assigning " + term + " " + code);
			counter++;			
		} else
			code = map.get(term);		
		
		return code;
		
	}
	/**
	 * Resets the hashmap that contains the mapping 
	 * from the terms to the term ids. If the property 
	 * <tt>garbageCollection</tt> is <tt>true</tt>, 
	 * then it performs garbage collection in order to 
	 * free alocated memory. This method should be 
	 * called after the creation of the lexicon.
	 */
	public static void reset() {
		map.clear();
		map = null;
		map = new TObjectIntHashMap(hashMapCapacity);
		counter = 0;
		code = 0;
		if (garbageCollection==true) 
			System.gc();
	}
}
