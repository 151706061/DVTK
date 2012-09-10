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
 * The Original Code is BitFile.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.indexing;
/** 
 * This interface encapsulates the most fundamental concept to 
 * indexing with Terrier - a Collection. Anyone using Terrier to 
 * encapuslate a new source of data (a corpus, colllection etc)
 * needs to create an object which implements this Collection interface.
 * <br>
 * The Collection interface is essentially an Iterator over a series of 
 * documents. It generates Document objects for each next document requested 
 * from the collection. It is aware of the type of Document objects available, 
 * and how to instantiate them.
 * <br>
 * Terrier core provides two Collection implementation: TRECCollection and 
 * SimpleFileCollection.
 * @author Craig Macdonald
 * @version $Revision: 1.1 $
 */
public interface Collection
{
	/** 
	 * Move the collection to the start of the next document.
	 * @return boolean true if there exists another document in the collection,
	 *         otherwise it returns false. 
	 */
	public boolean nextDocument();
	
	/** 
	 * Get the document object representing the current document.
	 * @return Document the current document;
	 */
	public Document getDocument();
	
	/** 
	 * Get the String document identifier of the current document.
	 * @return String the document identifier of a document.
	 */
	public String getDocid();
	/** 
	 * Returns true if the end of the collection has been reached
	 * @return boolean true if the end of collection has been reached, 
	 *         otherwise it returns false.
	 */
	public boolean endOfCollection();
	
	/** 
	 * Resets the Collection iterator to the start of the collection.
	 */
	public void reset();
	
	/**
	 * The size of the collection
	 * @return
	 */
	public int size();
}
