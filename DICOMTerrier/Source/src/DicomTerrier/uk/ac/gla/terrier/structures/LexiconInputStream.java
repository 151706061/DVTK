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
 * The Original Code is LexiconInputStream.java.
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
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * This class implements an input stream for the lexicon structure.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class LexiconInputStream {
	/** The term represented as an array of bytes.*/
	protected byte[] termCharacters =
		new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** The term represented as a string.*/
	protected String term;
	/** An integer representing the id of the term.*/
	protected int termId;
	/** The document frequency of the term.*/
	protected int documentFrequency;
	/** The term frequency of the term.*/
	protected int termFrequency;
	/** The offset in bytes in the inverted file of the term.*/
	protected long endOffset;
	/** The starting offset in bytes in the inverted file of the term.*/
	protected long startOffset;
	/** The starting bit offset in the inverted file of the term.*/
	protected byte startBitOffset;
	/** The offset in bits in the starting byte in the inverted file.*/
	protected byte endBitOffset;
	/** A data input stream to read from the bufferInput.*/
	protected DataInputStream lexiconStream = null;
	/**
	 * A default constructor.
	 */
	public LexiconInputStream() {
		try {
			lexiconStream =
				new DataInputStream(
					new BufferedInputStream(
						new FileInputStream(
							ApplicationSetup.LEXICON_FILENAME)));
		} catch (IOException ioe) {
			System.err.println(
				"I/O Exception occured while opening the lexicon file. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * A constructor given the filename.
	 * @param filename java.lang.String the name of the lexicon file.
	 */
	public LexiconInputStream(String filename) {
		try {
			lexiconStream =
				new DataInputStream(
					new BufferedInputStream(new FileInputStream(filename)));
		} catch (IOException ioe) {
			System.err.println(
				"I/O Exception occured while opening the lexicon file. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * A constructor given the filename.
	 * @param file java.io.File the name of the lexicon file.
	 */
	public LexiconInputStream(File file) {
		try {
			lexiconStream =
				new DataInputStream(
					new BufferedInputStream(new FileInputStream(file)));
		} catch (IOException ioe) {
			System.err.println(
				"I/O Exception occured while opening the lexicon file. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * Closes the lexicon stream.
	 * @throws IOException if an I/O error occurs
	 */
	public void close() throws IOException {
		lexiconStream.close();
	}
	/**
	 * Read the next lexicon entry.
	 * @return the number of bytes read if there is no error, 
	 *         otherwise returns -1 in case of EOF
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public int readNextEntry() throws IOException {
		try {
			startBitOffset = (byte) (endBitOffset + 1);
			startOffset = endOffset;
			if (startBitOffset == 8) {
				startOffset = endOffset + 1;
				startBitOffset = 0;
			}
			lexiconStream.read(
				termCharacters,
				0,
				ApplicationSetup.STRING_BYTE_LENGTH);
			term = (new String(termCharacters)).trim();
			termId = lexiconStream.readInt();
			documentFrequency = lexiconStream.readInt();
			termFrequency = lexiconStream.readInt();
			endOffset = lexiconStream.readLong();
			endBitOffset = lexiconStream.readByte();
			return Lexicon.lexiconEntryLength;
		} catch (EOFException eofe) {
			return -1;
		}
	}
	/**
	 * Prints out the contents of the lexicon file to check.
	 */
	public void print() {
		int i = 0; //counter
		int entryLength = Lexicon.lexiconEntryLength;
		try {
			while (readNextEntry() != -1) {
				System.out.println(
					""
						+ (i * entryLength)
						+ ", "
						+ term.trim()
						+ ", "
						+ termId
						+ ", "
						+ documentFrequency
						+ ", "
						+ term
						+ ", "
						+ endBitOffset);
				i++;
			}
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading the document index " +
				"input stream. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Returns the bit offset in the last byte of 
	 * the term's entry in the inverted file.
	 * @return byte the bit offset in the last byte of 
	 *         the term's entry in the inverted file
	 */
	public byte getEndBitOffset() {
		return endBitOffset;
	}
	/**
	 * Returns the ending offset of the term's 
	 * entry in the inverted file.
	 * @return long The ending byte of the term's 
	 *              entry in the inverted file.
	 */
	public long getEndOffset() {
		return endOffset;
	}
	/**
	 * Returns the bit offset in the first byte 
	 * of the term's entry in the inverted file.
	 * @return byte the bit offset in the first byte 
	 *         of the term's entry in the inverted file
	 */
	public byte getStartBitOffset() {
		return startBitOffset;
	}
	/**
	 * Returns the starting offset of the term's 
	 * entry in the inverted file.
	 * @return long The starting byte of the term's entry 
	 * 	            in the inverted file.
	 */
	public long getStartOffset() {
		return startOffset;
	}
	/**
	 * Return the document frequency for the given term.
	 * @return int The document frequency for the given term
	 */
	public int getNt() {
		return documentFrequency;
	}
	/**
	 * Returns the string representation of the term.
	 * @return the string representation of the already found term.
	 */
	public String getTerm() {
		return term;
	}
	/**
	 * Returns the term's id.
	 * @return the term's id.
	 */
	public int getTermId() {
		return termId;
	}
	/**
	 * Returns the term frequency for the already seeked term.
	 * @return the term frequency in the collection.
	 */
	public int getTF() {
		return termFrequency;
	}
	/** 
	 * Returns the bytes of the String.
	 * @return the byte array holding the term's byte representation
	 */
	public byte[] getTermCharacters() {
		return termCharacters;
	}
}