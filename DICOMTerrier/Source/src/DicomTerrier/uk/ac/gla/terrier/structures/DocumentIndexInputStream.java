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
 * The Original Code is DocumentIndexInputStream.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author) 
 */
package uk.ac.gla.terrier.structures;
import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.EOFException;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * This class provides access to the document index 
 * file sequentially, as a stream. 
 * Each entry in the document index consists of a 
 * document id, the document number,  and the length 
 * of the document, that is the number of terms that 
 * make up the document.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class DocumentIndexInputStream {
	/** A byte array used as buffer.*/
	private byte[] buffer = new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** The buffer from which the file is document index file is read.*/
	protected DataInputStream dis = null;
	/** The last read document id */
	protected int docid;
	/** The last read document length */
	protected int docLength;
	/** The last read document number */
	protected String docno;
	/** The start byte offset in the direct file */
	protected long startOffset;
	/** The start bit offset in the direct file. */
	protected byte startBitOffset;
	/** The end byte offset in the direct file */
	protected long endOffset;
	/** The end bit offset in the direct file. */
	protected byte endBitOffset;
	/** 
	 * A constructor for the class.
	 * @param is java.io.InputStream The underlying input stream
	 */
	public DocumentIndexInputStream(InputStream is) {
		dis = new DataInputStream(is);
	}
	/** 
	 * A constructor of a document index, from a given filename.
	 * @param filename java.lang.String The name of the document index file.
	 */
	public DocumentIndexInputStream(String filename) {
		try {
			dis =
				new DataInputStream(
					new BufferedInputStream(new FileInputStream(filename)));
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception during opening the document index file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/** 
	 * A default constructor of a document index, from a given filename.
	 */
	public DocumentIndexInputStream() {
		try {
			dis =
				new DataInputStream(
					new BufferedInputStream(
						new FileInputStream(
							ApplicationSetup.DOCUMENT_INDEX_FILENAME)));
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception during opening the document index file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/** 
	 * A constructor of a document index, from a given filename.
	 * @param file java.io.File The document index file.
	 */
	public DocumentIndexInputStream(File file) {
		try {
			dis =
				new DataInputStream(
					new BufferedInputStream(new FileInputStream(file)));
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception during opening the document index file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Closes the stream.
	 */
	public void close() {
		try {
			dis.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while closing docIndex file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/** 
	 * Reads the next entry from the stream.
	 * @return the number of bytes read from the stream, or 
	 * 		   -1 if EOF has been reached.
	 * @throws java.io.IOException if an I/O error occurs.
	 */
	public int readNextEntry() throws IOException {
		try {
			docid = dis.readInt();
			docLength = dis.readInt();
			dis.read(buffer, 0, ApplicationSetup.STRING_BYTE_LENGTH);
			docno = (new String(buffer)).trim();
			endOffset = dis.readLong();
			endBitOffset = dis.readByte();
			return 17 + ApplicationSetup.STRING_BYTE_LENGTH;
		} catch (EOFException eofe) {
			return -1;
		}
	}
	/**
	 * Prints out to the standard error stream 
	 * the contents of the document index file.
	 */
	public void print() {
		int i = 0; //a counter
		try {
			while (readNextEntry() != -1) {
				System.out.println(
					""
						+ (i * DocumentIndex.entryLength)
						+ ", "
						+ docid
						+ ", "
						+ docLength
						+ ", "
						+ docno
						+ ", "
						+ endOffset
						+ ", "
						+ endBitOffset);
				i++;
			}
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading the document " +
				"index input stream. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Returns the document's id for the given docno.
	 * @return int The document's id
	 */
	public int getDocumentId() {
		return docid;
	}
	/**
	 * Return the length of the document with the given docno.
	 * @return int The document's length
	 */
	public int getDocumentLength() {
		return docLength;
	}
	/**
	 * Reading the docno for the i-th document.
	 * @return the document number of the i-th document.
	 */
	public String getDocumentNumber() {
		return docno;
	}
	/**
	 * Returns the bit offset in the ending byte in the direct 
	 * file's entry for this document
	 * @return byte the bit offset in the ending byte in 
	 * 				the direct file's entry for this document
	 */
	public byte getEndBitOffset() {
		return endBitOffset;
	}
	/**
	 * Returns the offset of the ending byte in the 
	 * direct file for this document
	 * @return long the offset of the ending byte in the 
	 * 				direct file for this document
	 */
	public long getEndOffset() {
		return endOffset;
	}
	/**
	 * Return the bit offset in the starting byte in the entry in 
	 * the direct file for this document.
	 * @return byte the bit offset in the starting byte in the entry in 
	 * 		   		the direct file.
	 */
	public byte getStartBitOffset() {
		return startBitOffset;
	}
	/**
	 * Return the starting byte in the direct file for this document.
	 * @return long the offset of the starting byte in the direct file
	 */
	public long getStartOffset() {
		return startOffset;
	}
}
