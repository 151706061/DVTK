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
 * The Original Code is Rounding.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Ben He <ben{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.utility;
import java.lang.Math;
/**
 * A class for performing the rounding of a number before 
 * it is printed.
 * @author Gianni Amati, Ben He
 * @version $Revision: 1.1 $
 */
public class Rounding {
	/**
	 * Rounds to place digits and transforms the double number d 
	 * to a string for printing
	 * @param d double the number to transform
	 * @param place int the number of decimal digits
	 * @return String the string representing the rounded number.
	 */
	public static String toString(double d, int place) {
		if (place <= 0)
			return "" + (int) (d + ((d > 0) ? 0.5 : -0.5));
		String s = "";
		if (d < 0) {
			s += "-";
			d = -d;
		}
		d += 0.5 * Math.pow(10, -place);
		if (d > 1) {
			int i = (int) d;
			s += i;
			d -= i;
		} else
			s += "0";
		if (d > 0) {
			d += 1.0;
			String f = "" + (int) (d * Math.pow(10, place));
			s += "." + f.substring(1);
		}
		return s;
	}
}
