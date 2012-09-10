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
 * The Original Code is DocumentIndexBuilder.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.structures.indexing;
import java.io.BufferedOutputStream;
import java.io.DataOutputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import uk.ac.gla.terrier.structures.FilePosition;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * A builder for the document index. 
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class DocumentIndexBuilder {
	/** The stream to which we write the data. */
	protected DataOutputStream dos;
	/** The total number of entries in the document index.*/
	protected int numberOfDocumentIndexEntries;
	
	/** A default constructor for the class.*/
	public DocumentIndexBuilder() {
		try {
			numberOfDocumentIndexEntries = 0;
			dos = new DataOutputStream(new BufferedOutputStream(new FileOutputStream(ApplicationSetup.DOCUMENT_INDEX_FILENAME)));
		} catch (FileNotFoundException fnfe) {
			System.err.println(
				"Input/Output exception during opening the document index file. Stack trace follows.");
			fnfe.printStackTrace();
			System.exit(1);
		}	
	}
	/** 
	 * A constructor of a document index from a given filename.
	 * @param filename String the filename of the document index, 
	 *        with an extension
	 */
	public DocumentIndexBuilder(String filename) {
		try {
			numberOfDocumentIndexEntries = 0;
			dos = new DataOutputStream(new BufferedOutputStream(new FileOutputStream(filename)));
		} catch (FileNotFoundException fnfe) {
			System.err.println(
				"Input/Output exception during opening the document index file. Stack trace follows.");
			fnfe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Adds to the index a new entry, giving to it the next 
	 * available document id. The entry is writen first
	 * to the buffer, which afterwards has to be flushed to 
	 * the file on disk.
	 * @param docno String the document number.
	 * @param docLength int the number of indexed tokens in the document.
	 * @param directIndexOffset FilePosition the ending position of the 
	 *        document's entry in the direct index.
	 * @exception java.io.IOException Throws an exception in the 
	 *            case of an IO error.
	 */
	public void addEntryToBuffer(
		String docno,
		int docLength,
		FilePosition directIndexOffset)
		throws java.io.IOException {
	
		//writes the docid, length and the docno
		dos.writeInt(numberOfDocumentIndexEntries);
		dos.writeInt(docLength);
		dos.writeBytes(docno);
		dos.write(
			zeroBuffer,
			0,
			ApplicationSetup.STRING_BYTE_LENGTH - docno.length());
		dos.writeLong(directIndexOffset.Bytes);
		dos.writeByte(directIndexOffset.Bits);
		numberOfDocumentIndexEntries++;
	}
	/**
	 * Closes the random access file.
	 */
	public void close() {
		try {
			dos.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while closing docIndex file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Closes the underlying file after finished processing the collections.
	 */
	public void finishedCollections() {
		close();
	}
	
	/**
	 * A static buffer for writing zero values to the files.
	 */
	protected byte[] zeroBuffer = new byte[ApplicationSetup.STRING_BYTE_LENGTH];
}
