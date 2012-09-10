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
 * The Original Code is BlockLexicon.java.
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
import java.util.Arrays;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.Compare;;
/**
 * A lexicon class that saves the number of
 * different blocks a term appears in. It is used only during 
 * creating the block inverted index. After the block inverted
 * index has been created, the block lexicon is transformed into 
 * a lexicon.
 * @author Douglas Johnson, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class BlockLexicon extends Lexicon {
	/** The block frequency of the term */
	protected int blockFrequency;
	/**
	 * The size in bytes of an entry in the lexicon file. An entry corresponds
	 * to a string, an int (termCode), an int (docf), an int (tf), a long (the
	 * offset of the end of the term's entry in bytes in the inverted file) and
	 * a byte (the offset in bits of the last byte of the term's entry in the
	 * inverted file.
	 */
	public static final int lexiconEntryLength = 
		ApplicationSetup.STRING_BYTE_LENGTH //the string representation
		+ 16 //the four integers
		+ 8 //the long
		+ 1; //the byte
	/**
	 * A default constructor.
	 */
	public BlockLexicon() {
		super();
		
		try {
			numberOfLexiconEntries = lexiconFile.length() / BlockLexicon.lexiconEntryLength;
			bufferInput.mark(3 * lexiconEntryLength);
		} catch (IOException ioe) {
			System.err
					.println("Input/output exception while opening for reading the lexicon file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Constructs an instace of BlockLexicon and opens the corresponding file.
	 * @param lexiconName the name of the lexicon file.
	 */
	public BlockLexicon(String lexiconName) {
		super(lexiconName);
		try {
			numberOfLexiconEntries = lexiconFile.length() / BlockLexicon.lexiconEntryLength;
			bufferInput.mark(3 * lexiconEntryLength);
		} catch (IOException ioe) {
			System.err.println("Input/output exception while opening for reading the " +
							"lexicon file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Prints out the contents of the lexicon file. Streams are used to read the
	 * lexicon file.
	 */
	public void print() {
		try {
			//open the streams for reading the lexicon file
			FileInputStream fis = new FileInputStream(
					ApplicationSetup.LEXICON_FILENAME);
			BufferedInputStream bis = new BufferedInputStream(fis);
			DataInputStream dis = new DataInputStream(bis);
			int i = 0; //a counter;
			int isEOF = 0;
			//read each entry from the lexicon and
			//print it to the standard output
			while ((isEOF = dis.read(buffer, 0,
					ApplicationSetup.STRING_BYTE_LENGTH)) != -1) {
				term = new String(buffer);
				termId = dis.readInt();
				documentFrequency = dis.readInt();
				blockFrequency = dis.readInt();
				termFrequency = dis.readInt();
				endOffset = dis.readLong();
				endBitOffset = dis.readByte();
				System.out.println("" + (i * lexiconEntryLength) + ", "
						+ term.trim() + ", " + termId + ", "
						+ documentFrequency + ", " + blockFrequency + ", "
						+ termFrequency + ", " + endOffset + ", "
						+ endBitOffset);
				i++; //incrementing the counter
			}
			//close the open streams
			dis.close();
			bis.close();
			fis.close();
		} catch (IOException ioe) {
			System.err
					.println("Input/Output exception while reading the lexicon file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Finds the term given its term code.
	 * 
	 * @return true if the term is found, else return false
	 * @param termId
	 *            the term's id
	 */
	public boolean findTerm(int termId) {
		try {
			idToOffsetFile.seek(termId * 8);
			long lexiconOffset = idToOffsetFile.readLong();
			if (lexiconOffset == 0) {
				startOffset = 0;
				startBitOffset = 0;
				lexiconFile.seek(lexiconOffset);
				lexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(bt);
				this.termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				blockFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				return true;
			} else {
				lexiconFile.seek(lexiconOffset - 9);
				//goes to the lexicon offset minus the long offset and a byte
				startOffset = lexiconFile.readLong();
				startBitOffset = lexiconFile.readByte();
				startBitOffset++;
				if (startBitOffset == 8) {
					startBitOffset = 0;
					startOffset++;
				}
				lexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(bt);
				this.termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				blockFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				return true;
			}
		} catch (IOException ioe) {
			System.err
					.println("Input/Output exception while reading the idToOffset file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return false;
	}
	/**
	 * Performs a binary search in the lexicon in order to locate the given
	 * term. If the term is located, the properties termCharacters,
	 * documentFrequency, termFrequency, startOffset, startBitOffset, endOffset
	 * and endBitOffset contain the values related to the term.
	 * 
	 * @param _term the term to search for.
	 * @return true if the term is found, and false otherwise.
	 */
	public boolean findTerm(String _term) {
		Arrays.fill(buffer, (byte) 0);
		Arrays.fill(bt, (byte) 0);
		byte[] bt = _term.getBytes();
		int termLength = ApplicationSetup.STRING_BYTE_LENGTH;			
		int _termId = 0;
		long low = -1;
		long high = numberOfLexiconEntries;
		long i;
		while (high-low>1) {
			
			i = (long)(high+low)/2;
			try {
				lexiconFile.seek((i * BlockLexicon.lexiconEntryLength));
				lexiconFile.read(buffer, 0, termLength);
			} catch (IOException ioe) {
				System.err.println(
					"Input/Output exception while reading from lexicon file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
			
			int compareResult = 0;
			//compareResult = _term.compareTo(new String(buffer).trim());
			compareResult = Compare.compareWithNumeric(_term, new String(buffer).trim());
			
			if (compareResult < 1)
				high = i;
			else
				low = i;			
		}
		if (high == numberOfLexiconEntries)
			return false;
		try {
			lexiconFile.seek((high * BlockLexicon.lexiconEntryLength));
			lexiconFile.read(buffer, 0, termLength);
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading from lexicon file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}	
		boolean found = true;
		int length = _term.length();
		for (int j = 0; j < length; j++) {
			if (buffer[j] != bt[j]) {
				found = false;
				break;
			}
		}
		if (found) {
			try {
				findTerm(lexiconFile.readInt());
				return true;
			}catch(IOException ioe) {
				System.out.println("Input/Output exception while reading from lexicon file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
		}
		return false;	
	}
	/**
	 * Returns the block frequency for the given term
	 * @return int The block frequency for the given term
	 */
	public int getBlockFrequency() {
		return blockFrequency;
	}
	/**
	 * Seeks the i-th entry of the lexicon.
	 * @param i
	 *            The index of the entry we are looking for.
	 * @return true if the entry was found, false otherwise.
	 */
	public boolean seekEntry(int i) {
		try {
			if (i > numberOfLexiconEntries)
				return false;
			if (i == 0) {
				lexiconFile.seek(i * lexiconEntryLength);
				startOffset = 0;
				startBitOffset = 0;
				lexiconFile
						.read(buffer, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(buffer);
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				blockFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				return true;
			} else {
				lexiconFile.seek(i * lexiconEntryLength - lexiconEntryLength
						+ ApplicationSetup.STRING_BYTE_LENGTH + 12);
				startOffset = lexiconFile.readLong();
				startBitOffset = lexiconFile.readByte();
				startBitOffset++;
				if (startBitOffset == 8) {
					startBitOffset = 0;
					startOffset++;
				}
				lexiconFile
						.read(buffer, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(buffer);
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				blockFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				return true;
			}
		} catch (IOException ioe) {
			System.err
					.println("Input/Output exception while reading the idToOffset file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return false;
	}
	
	/**
	 * In an already stored entry in the lexicon file, the information about the
	 * term frequency, the endOffset in bytes, and the endBitOffset in the last
	 * byte, is updated. The term is specified by the index of the entry.
	 * 
	 * @return true if the information is updated properly, otherwise return
	 *         false
	 * @param i the i-th entry
	 * @param frequency the term's Frequency
	 * @param endOffset the offset of the ending byte in the inverted file
	 * @param endBitOffset the offset in bits in the ending byte in the term's entry in
	 *            inverted file
	 * @deprecated The BlockLexicon is used during indexing, but not during
	 *             retrieval.
	 */
	public boolean updateEntry(int i, int frequency, long endOffset,
			byte endBitOffset) {
		try {
			long lexiconOffset = i * lexiconEntryLength;
			//we seek the offset where the frequency should be writen
			lexiconFile.seek(lexiconOffset
					+ ApplicationSetup.STRING_BYTE_LENGTH + 8);
			lexiconFile.writeInt(frequency);
			lexiconFile.writeLong(endOffset);
			lexiconFile.writeByte(endBitOffset);
		} catch (IOException ioe) {
			System.err
					.println("Input/Output exception while updating the lexicon file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return false;
	}
}