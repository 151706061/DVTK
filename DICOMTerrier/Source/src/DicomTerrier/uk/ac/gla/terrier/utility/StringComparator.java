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
 * The Original Code is StringComparator.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.utility;
import java.util.Comparator;
/**
 * Compares two strings which may have fixed length fields separated 
 * with a dash, and a last field which corresponds to an integer. Two
 * examples of such strings are <tt>XXX-XXX-012389</tt> and 
 * <tt>XXX-XXX-1242</tt>.
 * 
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class StringComparator implements Comparator {
	
	/**
	 * Compares two Strings, which have a number of fields that
	 * are separated by one or more non-alphanumeric characters.
	 * @param d1 Object the first string object to compare.
	 * @param d2 Object the second string object to compare.
	 * @return int -1, zero, or 1 if the first argument is 
	 *         less than, equal to, or greater than the second 
	 *         argument, respectively.
	 */
	public int compare(Object d1, Object d2) {
		String s1 = (String)d1;
		String s2 = (String)d2;
		
		//we assume fields are separated by one or more 
		//non-alphanumeric characters
		String[] f1 = s1.split("\\W+");
		String[] f2 = s2.split("\\W+");
		
		int numOfFields = Math.min(f1.length, f2.length);
		int compareResult = 0;
		int i1; 
		int i2;
		for (int i=0; i<numOfFields; i++) {
			//if the fields are of different lengths
			//then check whether the fields contain only 
			//numerical digits
			if (f1[i].length()!=f2[i].length()) {
				//if the fields are numerical, then compare
				//their numerical values
				if (f1[i].matches("^\\d+$") && f2[i].matches("^\\d+$")) {
					i1 = Integer.parseInt(f1[i]);
					i2 = Integer.parseInt(f2[i]);
					if (i1 == i2) 
						return 0;
					else if (i1 > i2) 
						return 1;
					else if (i1 < i2)
						return -1;
				} else { //otherwise compare them as strings
					compareResult = f1[i].compareTo(f2[i]);
				}
			} else { //otherwise compare them as strings
				compareResult = f1[i].compareTo(f2[i]);
			}
			if (compareResult!=0)
				return compareResult;
		}
		return 0;
	}
	
	public static void main (String[] args){
		StringComparator SC = new StringComparator(); 
		int res = SC.compare(args[0], args[1]); 
		System.out.println( res > 0 ? (args[0] + " " + args[1]) : (args[1] + " " + args[0]) );
	}
} 
