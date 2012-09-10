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
 * The Original Code is BlockLexiconInputStream.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures;
import java.io.*;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * An input stream for accessing sequentially the entries
 * of a block lexicon.
 * @author Douglas Johnson, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class BlockLexiconInputStream extends LexiconInputStream {
	/** A zero buffer for writing to the file.*/
	private static byte[] zeroBuffer =
		new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** 
	 * The total number of different blocks a term appears in.
	 */
	protected int blockFrequency;
	/**
	 * A default constructor.
	 */
	public BlockLexiconInputStream() {
		super();
	}
	/**
	 * A constructor given the filename.
	 * @param filename java.lang.String the name of the lexicon file.
	 */
	public BlockLexiconInputStream(String filename) {
		super(filename);
	}
	/**
	 * A constructor given the filename.
	 * @param file java.io.File the name of the lexicon file.
	 */
	public BlockLexiconInputStream(File file) {
		super(file);
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
			term = new String(termCharacters);
			termId = lexiconStream.readInt();
			documentFrequency = lexiconStream.readInt();
			blockFrequency = lexiconStream.readInt();
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
						+ blockFrequency
						+ ", "
						+ termFrequency
						+ ", "
						+ endBitOffset);
				i++;
			}
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading the document index input stream. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Sets the block frequency for the given term
	 * @param blockFrequency the new block frequency
	 */
	public void setBF(int blockFrequency) {
		this.blockFrequency = blockFrequency;
	}
	/**
	 * Returns the block frequency for the currently processed term.
	 * @return int The block frequency for the currently processed term
	 */
	public int getBlockFrequency() {
		return blockFrequency;
	}
}
