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
 * The Original Code is Lexicon.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures;
import java.io.BufferedInputStream;
import java.io.ByteArrayInputStream;
import java.io.DataInputStream;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Arrays;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.Compare;
/**
 * The class that implements the lexicon structure. Apart from the lexicon file,
 * which contains the actual data about the terms, and takes its name from
 * ApplicationSetup.LEXICON_FILENAME, another file is created and
 * used, containing a mapping from the term's code to the offset of the term 
 * in the lexicon. The name of this file is given by 
 * ApplicationSetup.LEXICON_INDEX_FILENAME.
 * 
 * @see ApplicationSetup#LEXICON_FILENAME
 * @see ApplicationSetup#LEXICON_INDEX_FILENAME
 * @author Gerald van Veldhuijsen
 * @version Version 1.0
 */
public class Lexicon {
	/** The term represented as an array of bytes.*/
	protected byte[] termCharacters;
	/** The term represented as a string.*/
	protected String term;
	/** An integer representing the id of the term.*/
	protected int termId;
	/** The document frequency of the term.*/
	protected int documentFrequency;
	/** The term frequency of the term.*/
	protected int termFrequency;
	/** The offset in bytes in the inverted file of the term.*/
	protected long startOffset;
	/** The offset in bits in the starting byte in the inverted file.*/
	protected byte startBitOffset;
	/** The offset in bytes in the inverted file of the term.*/
	protected long endOffset;
	/** The offset in bits in the ending byte in the inverted file.*/
	protected byte endBitOffset;
	
	/** The number of the start enry in case of looking up a range*/
	protected int startEntry;
	/** The number of the end entry in case of looking up a range*/
	protected int endEntry;
	
	/** The array containing the result of a range */
	protected String[] rangeStringArray;
	protected int[][] rangeIntArray = new int[3][];
	protected long[] rangeLongArray ;
	protected byte[] rangeByteArray ;
	
	private int entryNumber = 0;
	
	//private int compareToResult = 0;
	
	/** 
	 * The size in bytes of an entry in the lexicon file.
	 * An entry corresponds to a string, an int (termCode), 
	 * an int (docf), an int (tf), a long (the offset of the end 
	 * of the term's entry in bytes in the inverted file) and
	 * a byte (the offset in bits of the last byte of the term's entry 
	 * in the inverted file.
	 */
	public static final int lexiconEntryLength =
		ApplicationSetup.STRING_BYTE_LENGTH //the string representation
		+12 //the three integers
		+8 //the long
		+1; //the byte
	/** The file containing the mapping from the codes to the offset in the lexicon file.*/
	protected RandomAccessFile idToOffsetFile;
	/** The actual lexicon file.*/
	protected RandomAccessFile lexiconFile;
	/** The number of entries in the lexicon file.*/
	protected long numberOfLexiconEntries;
	/** A buffer for reading from the lexicon file.*/
	protected byte[] buffer = new byte[512];
	/** A second buffer for finding terms.*/
	protected byte[] bt = new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** A byte input stream to read from the buffer.*/
	protected ByteArrayInputStream bufferInput =
		new ByteArrayInputStream(buffer);
	/** A data input stream to read from the bufferInput.*/
	protected DataInputStream dataInput = new DataInputStream(bufferInput);
	/** 
	 * A default constructor.
	 */
	//TODO Make sure no one uses this constructor
	public Lexicon() {
	}
	
	/**
	 * Constructs an instace of Lexicon and opens
	 * the corresponding file.
	 * 
	 * @param lexiconName the name of the lexicon file.
	 */
	public Lexicon(String lexiconName) {
		try {
			lexiconFile = new RandomAccessFile(lexiconName, "r");
			idToOffsetFile = new RandomAccessFile(lexiconName.substring(0,lexiconName.lastIndexOf(".")).concat(ApplicationSetup.LEXICON_INDEX_SUFFIX),"rw");
			numberOfLexiconEntries = lexiconFile.length() / lexiconEntryLength;
			bufferInput.mark(3 * lexiconEntryLength);
		} catch (IOException ioe) {
			System.err.println(
				"Input/output exception while opening for reading the lexicon file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	* Closes the lexicon and lexicon index files.
	*/
	public void close() {
		try {
			idToOffsetFile.close();
			lexiconFile.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while closing files idToOffsetFile and lexiconFile. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/** 
	 * Prints out the contents of the lexicon file. 
	 * Streams are used to read the lexicon file.
	 */
	public void print() {
		try {
			//open the streams for reading the lexicon file
			FileInputStream fis =
				new FileInputStream(ApplicationSetup.LEXICON_FILENAME);
			BufferedInputStream bis = new BufferedInputStream(fis);
			DataInputStream dis = new DataInputStream(bis);
			int i = 0; //a counter;

			//read each entry from the lexicon and 
			//print it to the standard output
			while (	dis.read(buffer, 0, ApplicationSetup.STRING_BYTE_LENGTH) != -1) {
				term = new String(buffer);
				termId = dis.readInt();
				documentFrequency = dis.readInt();
				termFrequency = dis.readInt();
				endOffset = dis.readLong();
				endBitOffset = dis.readByte();
				System.out.println(
					""
						+ (i * lexiconEntryLength)
						+ ", "
						+ term.trim()
						+ ", "
						+ termId
						+ ", "
						+ documentFrequency
						+ ", "
						+ termFrequency
						+ ", "
						+ endOffset
						+ ", "
						+ endBitOffset);
				i++; //incrementing the counter
			}
			//close the open streams
			dis.close();
			bis.close();
			fis.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading the lexicon file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Finds the term given its term code.
	 *
	 * @return true if the term is found, else return false
	 * @param _termId the term's identifier
	 */
	public boolean findTerm(int _termId) {
		try {
			idToOffsetFile.seek(_termId * 8);
			long lexiconOffset = idToOffsetFile.readLong();
			//System.out.println("LexiconOffset: " + lexiconOffset);
			entryNumber = (int) lexiconOffset/lexiconEntryLength;
			if (lexiconOffset == 0) {
				startOffset = 0;
				startBitOffset = 0;
				lexiconFile.seek(lexiconOffset);
				lexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(bt);
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
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
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				return true;
			}
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading the idToOffset file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return false;
	}
	/** 
	 * Performs a binary search in the lexicon
	 * in order to locate the given term.
	 * If the term is located, the properties
	 * termCharacters, documentFrequency,
	 * termFrequency, startOffset, startBitOffset,
	 * endOffset and endBitOffset contain the
	 * values related to the term.
	 * @param _term The term to search for.
	 * @return true if the term is found, and false otherwise.
	 */
	public boolean findTerm(String _term) {
		Arrays.fill(buffer, (byte) 0);
		Arrays.fill(bt, (byte) 0);
		byte[] bt = _term.getBytes();
		int termLength = ApplicationSetup.STRING_BYTE_LENGTH;			
		long low = -1;
		long high = numberOfLexiconEntries;
		long i;
		while (high-low>1) {
			
			i = (long)(high+low)/2;
			try {
				lexiconFile.seek((i * lexiconEntryLength));
				lexiconFile.read(buffer, 0, termLength);
			} catch (IOException ioe) {
				System.err.println(
					"Input/Output exception while reading from lexicon file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
			
			int compareResult = 0;
			compareResult = Compare.compareWithNumeric(_term, new String(buffer).trim());
			
			if (compareResult < 1)
				high = i;
			else
				low = i;			
		}
		if (high == numberOfLexiconEntries)
			return false;
		try {
			lexiconFile.seek((high * lexiconEntryLength));
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
	 * Returns the bit offset in the last byte of 
	 * the term's entry in the inverted file.
	 * 
	 * @return byte the bit offset in the last byte of 
	 *         the term's entry in the inverted file
	 */
	public byte getEndBitOffset() {
		return endBitOffset;
	}
	/**
	 * Returns the ending offset of the term's entry in the inverted file.
	 *
	 * @return long The ending byte of the term's entry in the inverted file.
	 */
	public long getEndOffset() {
		return endOffset;
	}
	/**
	 * Return the document frequency for the given term.
	 *
	 * @return int The document frequency for the given term
	 */
	public int getNt() {
		return documentFrequency;
	}
	/**
	 * Returns the number of entries in the lexicon.
	 * @return the number of entries in the lexicon.
	 */
	public long getNumberOfLexiconEntries() {
		return numberOfLexiconEntries;
	}
	/**
	 * The bit offset in the starting byte of 
	 * the entry in the inverted file.
	 *
	 * @return byte The number of bits in the first 
	 *         byte of the entry in the inverted file
	 */
	public byte getStartBitOffset() {
		return startBitOffset;
	}
	/**
	 * Returns the beginning of the term's entry in the inverted file.
	 *
	 * @return long the start offset (in bytes) in the inverted file
	 */
	public long getStartOffset() {
		return startOffset;
	}
	/**
	 * Insert the method's description here.
	 *
	 * @return java.lang.String The string representation of the seeked term.
	 */
	public String getTerm() {
		return this.term.trim();
	}
	/**
	 * Returns the term's id.
	 *
	 * @return int the term's id.
	 */
	public int getTermId() {
		return termId;
	}
	/**
	 * Returns the term frequency for the already seeked term.
	 *
	 * @return int The term frequency in the collection.
	 */
	public int getTF() {
		return termFrequency;
	}
	
	/**
	 * Seeks the i-th entry of the lexicon.
	 * TODO read a byte array from the file and decode it, 
	 * 		instead of reading the different pieces of 
	 *      information separately.
	 * @param i The index of the entry we are looking for.
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
				lexiconFile.read(
					buffer,
					0,
					ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(buffer);
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				return true;
			} else {
				lexiconFile.seek(
					i * lexiconEntryLength
						- lexiconEntryLength
						+ ApplicationSetup.STRING_BYTE_LENGTH
						+ 12);
				startOffset = lexiconFile.readLong();
				startBitOffset = lexiconFile.readByte();
				startBitOffset++;
				if (startBitOffset == 8) {
					startBitOffset = 0;
					startOffset++;
				}
				lexiconFile.read(
					buffer,
					0,
					ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(buffer);
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				return true;
			}
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading the idToOffset file. " +
				"Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return false;
	}
	/**
	 * Looks up a range of terms in the lexicon.
	 * @param first First String of the range
	 * @param last Last String of the range
	 * @return True if something is found, false otherwise
	 */
	public boolean findRange(String first, String last){
		
		this.findTermWeek(first);
		startEntry = entryNumber;
		while (Compare.compareWithNumericWeek(term.trim(), first)<=0 && startEntry<numberOfLexiconEntries){
			startEntry++;
			this.seekEntry(startEntry);		
		}	

		if (Compare.compareWithNumericWeek(term.trim(), first)<=0)
			return false;
		
		this.findTermWeek(last);
		endEntry = entryNumber;
		while (Compare.compareWithNumericWeek(term.trim(), last)>=0 && endEntry>0){
			endEntry--;
			this.seekEntry(endEntry);
		}
		
		if (Compare.compareWithNumericWeek(term.trim(), last)>=0)
			return false;		
		
		int nrOfEntries = (endEntry - startEntry)+1;
		
		System.out.println("Found " + nrOfEntries + "(" + endEntry + "-" + startEntry + ")" + " entries.");
		seekEntry(startEntry);
		System.out.println("Startentry is " + term.trim());
		seekEntry(endEntry);
		System.out.println("Endentry is " + term.trim());
		
		
		if (nrOfEntries>0){

			rangeStringArray = new String[nrOfEntries];
			rangeIntArray[0] = new int[nrOfEntries];
			rangeIntArray[1] = new int[nrOfEntries];
			rangeIntArray[2] = new int[nrOfEntries];
			rangeLongArray = new long[nrOfEntries + 1];
			rangeByteArray = new byte[nrOfEntries + 1];
			
			try{
				if(startEntry == 0){
					lexiconFile.seek(startEntry * lexiconEntryLength);
					rangeLongArray[0] = 0;
					rangeByteArray[0] = 0;
					rangeByteArray[0]--;
				}
				else {
					lexiconFile.seek(
							startEntry * lexiconEntryLength
								- lexiconEntryLength
								+ ApplicationSetup.STRING_BYTE_LENGTH
								+ 12);
					rangeLongArray[0] = lexiconFile.readLong();
					rangeByteArray[0] = lexiconFile.readByte();
				}
					
				for (int i=0; i<nrOfEntries; i++){
					lexiconFile.read(
						buffer,
						0,
						ApplicationSetup.STRING_BYTE_LENGTH);
					rangeStringArray[i] = new String(buffer);
					rangeIntArray[0][i] = lexiconFile.readInt();
					rangeIntArray[1][i] = lexiconFile.readInt();
					rangeIntArray[2][i] = lexiconFile.readInt();
					rangeLongArray[i+1] = lexiconFile.readLong();
					rangeByteArray[i+1] = lexiconFile.readByte();
				}
			} catch (IOException ioe) {
				System.err.println(
					"Input/Output exception while reading the idToOffset file. " +
					"Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}			
			return true;
		}
		else 
			return false;				
	}
	
	/**
	 * Get the start entry number of the range
	 * @return int start entry number
	 */
	public int getStartEntry(){
		return startEntry;
	}
	
	/**
	 * Get the end entry number of the range
	 * @return int end entry number
	 */
	public int getEndEntry(){
		return endEntry;
	}
	
	
	/**
	 * Get the array of term strings for a range
	 * @return the string array
	 */
	public String[] getRangeStringArray(){
		return rangeStringArray;
	}
	
	/**
	 * Get the matrix of term statistics for a range
	 * [0] the termid
	 * [1] the documentfrequency
	 * [2] the termfrequency
	 * @return the matrix
	 */
	public int[][] getRangeIntArray(){
		return rangeIntArray;
	}
	
	/**
	 * Get the byte end offsets for a range
	 * The first contains the end offset of the previous.
	 * @return array of byteoffsets
	 */
	public long[] getByteOffsets(){
		return rangeLongArray;
	}
	
	/**
	 * Get the bit end offsets for a range
	 * The first contains the end offset of the previous.
	 * @return array of bitoffsets
	 */
	public byte[] getBitOffsets(){
		return rangeByteArray;
	}
	
	/** 
	 * Performs a week binary search in the lexicon
	 * in order to locate the position closest to the given term.
	 * If the term is located, the properties
	 * termCharacters, documentFrequency,
	 * termFrequency, startOffset, startBitOffset,
	 * endOffset and endBitOffset contain the
	 * values related to the term.
	 * @param _term The term to search for.
	 * @return true if the term is found, and false otherwise.
	 */
	public boolean findTermWeek(String _term) {
		Arrays.fill(buffer, (byte) 0);
		Arrays.fill(bt, (byte) 0);
		byte[] bt2 = _term.getBytes();
		int termLength = ApplicationSetup.STRING_BYTE_LENGTH;			
		long low = -1;
		long high = numberOfLexiconEntries;
		long i;
		boolean found = false;
		while (high-low>1) {
			
			i = (long)(high+low)/2;
			try {
				lexiconFile.seek((i * lexiconEntryLength));
				lexiconFile.read(buffer, 0, termLength);
			} catch (IOException ioe) {
				System.err.println(
					"Input/Output exception while reading from lexicon file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
			
			int compareToResult = 0;
			compareToResult = Compare.compareWithNumeric(_term, new String(buffer).trim());
			
			if (compareToResult < 1)
				high = i;
			else
				low = i;			
		}

		if (high == numberOfLexiconEntries)
			high--;
		
		
		try {
			lexiconFile.seek((high * lexiconEntryLength));
			lexiconFile.read(buffer, 0, termLength);
		} catch (IOException ioe) {
			System.err.println(
				"FindWeekTerm (1): Input/Output exception while reading from lexicon file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}	
		found = true;
		int length = _term.length();
		for (int j = 0; j < length; j++) {
			if (buffer[j] != bt2[j]) {
				found = false;
				break;
			}
		}
		
		try {
			//findTerm(lexiconFile.readInt());
			long lexiconOffset = high * lexiconEntryLength;
			entryNumber = (int) lexiconOffset/lexiconEntryLength;
			if (lexiconOffset == 0) {
				startOffset = 0;
				startBitOffset = 0;
				lexiconFile.seek(lexiconOffset);
				lexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				term = new String(bt);
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				//return true;
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
				termId = lexiconFile.readInt();
				documentFrequency = lexiconFile.readInt();
				termFrequency = lexiconFile.readInt();
				endOffset = lexiconFile.readLong();
				endBitOffset = lexiconFile.readByte();
				//return true;
			}
			
			
		}catch(IOException ioe) {
			System.out.println("FindWeekTerm (2) Entry is " + high + " : Input/Output exception while reading from lexicon file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		
		return found;	
	}
	
	/** 
	 * Performs a binary search in the lexicon
	 * in order to locate the given term.
	 * This term must exactly match the term!
	 * If the term is located, the properties
	 * termCharacters, documentFrequency,
	 * termFrequency, startOffset, startBitOffset,
	 * endOffset and endBitOffset contain the
	 * values related to the term.
	 * @param _term The term to search for.
	 * @return true if the term is found, and false otherwise.
	 */
	public boolean findExactTerm(String _term) {
		Arrays.fill(buffer, (byte) 0);
		Arrays.fill(bt, (byte) 0);
		int termLength = ApplicationSetup.STRING_BYTE_LENGTH;			
		long low = -1;
		long high = numberOfLexiconEntries;
		long i;
		while (high-low>1) {
			
			i = (long)(high+low)/2;
			try {
				lexiconFile.seek((i * lexiconEntryLength));
				lexiconFile.read(buffer, 0, termLength);
			} catch (IOException ioe) {
				System.err.println(
					"Input/Output exception while reading from lexicon file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
			
			int compareResult = 0;
			compareResult = Compare.compareWithNumeric(_term, new String(buffer).trim());
			
			if (compareResult < 1)
				high = i;
			else
				low = i;			
		}
		if (high == numberOfLexiconEntries)
			return false;
		try {
			lexiconFile.seek((high * lexiconEntryLength));
			lexiconFile.read(buffer, 0, termLength);
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading from lexicon file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}	
		
		System.out.println("Matching " + _term + " with " + new String(buffer).trim());
		
		boolean found = _term.equals(new String(buffer).trim());
		
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
	 * In an already stored entry in the lexicon
	 * file, the information about the term frequency,
	 * the endOffset in bytes, and the endBitOffset in the last
	 * byte, is updated. The term is specified by the index of the entry.
	 *
	 * @return true if the information is updated properly, 
	 *         otherwise return false
	 * @param i the i-th entry
	 * @param frequency the term's Frequency
	 * @param endOffset the offset of the ending byte in the inverted file
	 * @param endBitOffset the offset in bits in the ending byte 
	 *        in the term's entry in inverted file
	 * @deprecated The Lexicon class is only used for reading the
	 *             lexicon file, and not for writing any information.
	 */
	public boolean updateEntry(
		int i,
		int frequency,
		long endOffset,
		byte endBitOffset) {
		try {
			long lexiconOffset = i * lexiconEntryLength;
			//we seek the offset where the frequency should be writen
			lexiconFile.seek(
				lexiconOffset + ApplicationSetup.STRING_BYTE_LENGTH + 8);
			lexiconFile.writeInt(frequency);
			lexiconFile.writeLong(endOffset);
			lexiconFile.writeByte(endBitOffset);
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while updating the lexicon file. " +
				"Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return false;
	}
}
