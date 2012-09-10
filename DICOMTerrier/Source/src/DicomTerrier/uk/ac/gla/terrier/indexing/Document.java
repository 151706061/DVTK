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
 * The Original Code is Document.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.indexing;
import java.util.HashSet;
import java.io.Reader;
/** 
 * This interface encapsulates the concept of a document. 
 * @author Craig Macdonald, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public interface Document
{
	/** 
	 * Gets the next term of the document. 
	 * <B>NB:</B>Null string returned from getNextTerm() should
	 * be ignored. They do not signify the lack of any more terms.
	 * endOfDocument() should be used to check that.
	 * @return String the next term of the document. Null returns should be
	 * ignored.         
	 */
	public String getNextTerm();

	/** 
	 * Returns a list of the fields the current term appears in.
	 * @return HashSet a set of the terms that the current term appears in. 
	 */
	public HashSet getFields();
	
	/** 
	 * Returns true when the end of the document has been reached, and there
	 * are no other terms to be retrieved from it.
	 * @return boolean true if there are no more terms in the document, otherwise
	 *         it returns false.
     */
	public boolean endOfDocument();

	/** Returns a Reader object so client code can tokenise the document
	 * or deal with the document itself. Examples might be extracting URLs,
	 * language detection. */
	public Reader getReader();
}
