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
 * The Original Code is LexiconOutputStream.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author) 
 */
package uk.ac.gla.terrier.structures;
import java.io.BufferedOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * This class implements an output stream for the comparable lexicon structure.
 * @author Gerald Veldhuijsen
 */
public class ComparableLexiconOutputStream {
	/** A zero buffer for writing to the file.*/
	private static byte[] zeroBuffer =
		new byte[ApplicationSetup.STRING_BYTE_LENGTH];
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
	/** The offset in bits in the starting byte in the inverted file.*/
	protected byte endBitOffset;
	/** A data input stream to read from the bufferInput.*/
	protected DataOutputStream lexiconStream = null;
	/**
	 * A default constructor.
	 */
	public ComparableLexiconOutputStream() {
		try {
			lexiconStream =
				new DataOutputStream(
					new BufferedOutputStream(
						new FileOutputStream(
							ApplicationSetup.COMPARABLE_LEXICON_FILENAME)));
		} catch (IOException ioe) {
			System.err.println(
				"I/O error occured while opening the lexicon file. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * A constructor given the filename.
	 * @param filename java.lang.String the name of the lexicon file.
	 */
	public ComparableLexiconOutputStream(String filename) {
		try {
			lexiconStream =
				new DataOutputStream(
					new BufferedOutputStream(new FileOutputStream(filename)));
		} catch (IOException ioe) {
			System.err.println(
				"I/O error occured while opening the lexicon file. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * A constructor given the filename.
	 * @param file java.io.File the name of the lexicon file.
	 */
	public ComparableLexiconOutputStream(File file) {
		try {
			lexiconStream =
				new DataOutputStream(
					new BufferedOutputStream(new FileOutputStream(file)));
		} catch (IOException ioe) {
			System.err.println(
				"I/O error occured while opening the lexicon file. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * Closes the lexicon stream.
	 * @throws IOException if an I/O error occurs while closing the stream.
	 */
	public void close() throws IOException {
		lexiconStream.close();
	}
	/**
	 * Writes a lexicon entry.
	 * @return the number of bytes written to the file. 
	 * @throws java.io.IOException if an I/O error occurs
	 * @param _term the string representation of the term
	 * @param _termId the terms integer identifier
	 * @param _documentFrequency the term's document frequency in the collection
	 * @param _termFrequency the term's frequency in the collection
	 * @param _endOffset the term's ending byte offset in the inverted file
	 * @param _endBitOffset the term's ending byte bit-offset in the inverted file
	 */
	public int writeNextEntry(
		String _term,
		int _termId,
		int _documentFrequency,
		int _termFrequency,
		long _endOffset,
		byte _endBitOffset,
		long _startOffset,
		byte _startBitOffset)
		throws IOException {
		byte[] tmpBytes = _term.getBytes();
		int length = tmpBytes.length;
		lexiconStream.write(tmpBytes, 0, length);
		lexiconStream.write(
			zeroBuffer,
			0,
			ApplicationSetup.STRING_BYTE_LENGTH - length);
		lexiconStream.writeInt(_termId);
		lexiconStream.writeInt(_documentFrequency);
		lexiconStream.writeInt(_termFrequency);
		lexiconStream.writeLong(_endOffset);
		lexiconStream.writeByte(_endBitOffset);
		lexiconStream.writeLong(_startOffset);
		lexiconStream.writeByte(_startBitOffset);
		return ComparableLexicon.lexiconEntryLength;
	}
	/**
	 * Writes a lexicon entry.
	 * @return the number of bytes written.
	 * @throws java.io.IOException if an I/O error occurs
	 * @param _term the string representation of the term
	 * @param _termId the terms integer identifier
	 * @param _documentFrequency the term's document frequency in the collection
	 * @param _termFrequency the term's frequency in the collection
	 * @param _endOffset the term's ending byte offset in the inverted file
	 * @param _endBitOffset the term's ending byte bit-offset in the inverted file
	 */
	public int writeNextEntry(
		byte[] _term,
		int _termId,
		int _documentFrequency,
		int _termFrequency,
		long _endOffset,
		byte _endBitOffset,
		long _startOffset,
		byte _startBitOffset)
		throws IOException {
		lexiconStream.write(_term, 0, _term.length);
		lexiconStream.writeInt(_termId);
		lexiconStream.writeInt(_documentFrequency);
		lexiconStream.writeInt(_termFrequency);
		lexiconStream.writeLong(_endOffset);
		lexiconStream.writeByte(_endBitOffset);
		lexiconStream.writeLong(_startOffset);
		lexiconStream.writeByte(_startBitOffset);
		return ComparableLexicon.lexiconEntryLength;
	}
	/**
	 * Sets the bit offset in the last byte of the term's entry in the inverted file.
	 * @param _endBitOffset byte the bit offset in the last byte of the 
	 *        term's entry in the inverted file.
	 */
	public void setEndBitOffset(byte _endBitOffset) {
		endBitOffset = _endBitOffset;
	}
	/**
	 * Sets the ending offset of the term's entry in the inverted file.
	 * @param _endOffset long The ending byte of the term's 
	 *        entry in the inverted file.
	 */
	public void setEndOffset(long _endOffset) {
		endOffset = _endOffset;
	}
	/**
	 * Sets the document frequency for the given term.
	 * @param _Nt int The document frequency for the given term.
	 */
	public void setNt(int _Nt) {
		documentFrequency = _Nt;
	}
	/**
	 * Sets the string representation of the term.
	 * @param _term java.lang.String The string representation of 
	 *        the seeked term.
	 */
	public void setTerm(String _term) {
		term = _term;
	}
	/**
	 * Sets the term's id.
	 * @param _termId int the term's identifier.
	 */
	public void setTermId(int _termId) {
		termId = _termId;
	}
	/**
	 * Sets the term frequency for the already found term.
	 * @param _termFrequency int The term frequency in the collection.
	 */
	public void setTF(int _termFrequency) {
		termFrequency = _termFrequency;
	}
}