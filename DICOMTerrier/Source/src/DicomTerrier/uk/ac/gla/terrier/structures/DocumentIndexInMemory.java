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
 * The Original Code is DocumentIndexInMemory.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author) 
 */
package uk.ac.gla.terrier.structures;
import uk.ac.gla.terrier.utility.*;
import java.io.*;
import java.util.*;
/**
 * This class extends DocumentIndex, but instead of 
 * accessing the disk file each time, the data are 
 * loaded into memory, in order to decrease access time.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class DocumentIndexInMemory extends DocumentIndex {
	
	/** The index of the last retrieved entry.*/
	private int index;
	/** The docnos array.*/
	private static String[] docnos;
	/** The document length array.*/
	private static int[] doclen;
	/** The byte offset array.*/
	private static long[] byteOffset;
	/** The bit offset array.*/
	private static byte[] bitOffset;
	/**
	 * The default constructor for DocumentIndexInMemory. 
	 * Opens the document index file and reads its contents into memory.
	 */
	public DocumentIndexInMemory() {
		try {
			File documentIndexFile =
				new File(ApplicationSetup.DOCUMENT_INDEX_FILENAME);
			DataInputStream dis =
				new DataInputStream(
					new BufferedInputStream(
						new FileInputStream(documentIndexFile)));
			int numberOfDocumentIndexEntries =
				(int) documentIndexFile.length() / DocumentIndex.entryLength;
			loadIntoMemory(dis, numberOfDocumentIndexEntries);
			dis.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception during opening the document index file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * A constructor for DocumentIndexInMemory that specifies 
	 * the file to open. Opens the document index file and reads 
	 * its contents into memory.
 	 * For the document pointers file we replace the extension of the 
	 * document index file with the right default extension. 
	 * @param filename java.lang.String
	 */
	public DocumentIndexInMemory(String filename) {
		try {
			File documentIndexFile = new File(filename);
			DataInputStream dis =
				new DataInputStream(
					new BufferedInputStream(
						new FileInputStream(documentIndexFile)));
			int numberOfDocumentIndexEntries =
				(int) documentIndexFile.length() / DocumentIndex.entryLength;
			loadIntoMemory(dis, numberOfDocumentIndexEntries);
			dis.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception during opening the document index file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Prints to the standard error the document index 
	 * structure, which is loaded into memory.
	 */
	public void print() {
		for (int i = 0; i < numberOfDocumentIndexEntries; i++) {
			System.out.println(
				""
					+ (i * entryLength)
					+ ", "
					+ getDocumentId(docnos[i]) + ":"+i
					+ ", "
					+ doclen[i]
					+ ", "
					+ docnos[i]
					+ ", "
					+ byteOffset[i]
					+ ", "
					+ bitOffset[i]);
		}
	}
	/**
	 * Returns the id of a document with a given document number.
	 * @return int The document's id, or a negative number if a
	 * 			   document with the given number doesn't exist.
	 * @param docno java.lang.String The document's number
	 */
	public int getDocumentId(String docno) {
		index = Arrays.binarySearch(docnos, docno, sComp);
		return index;
	}
	/**
	 * Returns the length of a document with a given id.
	 * @return int The document's length
	 * @param docid the document's id
	 */
	public int getDocumentLength(int docid) {
		index = docid;
		return doclen[docid];
	}
	/**
	 * Returns the document length of the document 
	 * with a given document number .
	 * @return int The document's length
	 * @param docno java.lang.String The document's number
	 */
	public int getDocumentLength(String docno) {
		index = Arrays.binarySearch(docnos, docno, sComp);
		if (index >= 0)
			return doclen[index];
		return -1;
	}
	/**
	 * Returns the number of a document with a given id.
	 * @return java.lang.String The documents number
	 * @param docid int The documents id
	 */
	public String getDocumentNumber(int docid) {
		index = docid;
		return docnos[docid];
	}
	/**
	 * Returns the offset of the ending bit in the last byte
	 * of the document's entry in the direct file.
	 * @return byte the offset of the ending bit in the last byte
	 *              of the document's direct file entry.
	 */
	protected byte getEndBitOffset() {
		return (byte) bitOffset[index];
	}
	/**
	 * Returns the offset of the ending byte in the document's
	 * entry in the direct file.
	 * @return long the offset of the ending byte in the document's
	 * entry in the direct file.
	 */
	protected long getEndOffset() {
		return byteOffset[index];
	}
	/**
	 * Returns the ending offset of the current document's
	 * entry in the direct index.
	 * @return FilePosition an offset in the direct index.
	 */
	public FilePosition getDirectIndexEndOffset() {
		return new FilePosition(getEndOffset(), getEndBitOffset());
	}
	/**
	 * Returns the number of documents.
	 * @return int the total number of indexed documents.
	 */
	public int getNumberOfDocuments() {
		return docnos.length;
	}
	/**
	 * Returns the starting bit offset of the first
	 * byte in the document's entry
	 * in the direct file.
	 * @return byte the starting bit offset of the first byte in the
	 *              document's direct file.
	 */
	protected byte getStartBitOffset() {
		if (index == 0)
			return 0;
		byte startBitOffset = bitOffset[index - 1];
		if (startBitOffset == 7)
			return 0;
		return (byte) (startBitOffset + 1);
	}
	/**
	 * Returns the offset of the first byte in the document's 
	 * entry in the direct file.
	 * @return long the offset of the first byte in the document's
	 *         entry in the direct file.
	 */
	protected long getStartOffset() {
		if (index == 0)
			return 0;
		byte startBitOffset = bitOffset[index - 1];
		long startOffset = byteOffset[index - 1];
		if (startBitOffset == 7)
			return (startOffset + 1L);
		return startOffset;
	}
	
	/**
	 * Returns the starting offset of the current document's
	 * entry in the direct index.
	 * @return FilePosition an offset in the direct index.
	 */
	public FilePosition getDirectIndexStartOffset() {
		return new FilePosition(getStartOffset(), getStartBitOffset());
	}
	
	/**
	 * This method loads the data into memory.
	 * @param dis java.io.DataInputStream The input stream from which 
	 * the data are read,
	 * @param numOfEntries int The number of entries to read
	 * @exception java.io.IOException An input/output exception 
	 * 			  is thrown if there is any error while reading from disk.
	 */
	public void loadIntoMemory(DataInputStream dis, int numOfEntries)
		throws java.io.IOException {
		bitOffset = new byte[numOfEntries];
		byteOffset = new long[numOfEntries];
		doclen = new int[numOfEntries];
		docnos = new String[numOfEntries];
		final int termLength = ApplicationSetup.STRING_BYTE_LENGTH;
		byte[] buffer = new byte[termLength];
		for (int i = 0; i < numOfEntries; i++) {
			dis.readInt();
			doclen[i] = dis.readInt(); //read the document's length
			docnos[i] = (new String(buffer)).trim();
			byteOffset[i] = dis.readLong();
			bitOffset[i] = dis.readByte();
		}
	}
	/** 
	 * This method overrides the seek(int docid) method of
	 * DocumentIndex class.
	 * @param i the docid of the document we are looking for.
	 * @return always true because we are handling a stream and
	 *         arbitrary seeking is not allowed.
	 */
	public boolean seek(int i) {
		try {
			index = i;
			return true;
		} catch (Exception e) {
			return false;
		}
	}
	
}
