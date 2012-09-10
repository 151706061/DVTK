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
 * The Original Code is TermPipeline.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.terms;
/**
 * Models the concept of a component in a pipeline of term processors. 
 * Classes that implement this interface could be stemming algorithms, 
 * stopwords removers, or acronym expanders just to mention few examples.
 * @author Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 * @version $Revision: 1.1 $
 */
public interface TermPipeline
{
	/**
	 * Processes a term using the current term pipeline component and
	 * passes the output to the next pipeline component, if the 
	 * term has not been discarded.
	 * @param t String the term to process.
	 */
	public void processTerm(String t);
}
