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
 * The Original Code is DumpTerm.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *  Gianni Amati <gba{a.}fub.it> (original author)
 *  Craig Macdonald <craigm{a.}dcs.gla.ac.uk>   
 */
package uk.ac.gla.terrier.terms;

/** Useful development phase TermPipeline object that allows 
  * prints every term that passes through it to System.err
  */
public class DumpTerm implements TermPipeline {
	TermPipeline next = null;
	
	/** Construct a new DumpTerm objecy */
	public DumpTerm(TermPipeline next)
	{
		this.next = next;
	}
	
	/**
	 * Displays the given on STDERR, then passes onto next pipeline object.
	 * @param t String the term to pass onto next pipeline object
	 */
	public void processTerm(String t)
	{
		if (t == null)
			return;
		System.err.println("term: "+t);
		next.processTerm(t);
	}

}
