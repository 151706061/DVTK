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
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.Arrays;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.compression.BitFile;

/**
 * The class that implements the taglexicon structure. Apart from the taglexicon file,
 * which contains the actual data about the terms, and takes its name from
 * ApplicationSetup.TAGLEXICON_FILENAME, another file is created and
 * used, containing a mapping from the term's code to the offset of the term 
 * in the lexicon. The name of this file is given by 
 * ApplicationSetup.TAGLEXICON_INDEX_FILENAME.
 * 
 * @see ApplicationSetup#TERMTAGLEXICON_FILENAME
 * 
 * @author Gerald Veldhuijsen
 * @version Version 1.0
 */
public class TermTagLexicon extends Lexicon{
	
	/** The tag represented as a string.*/
	protected String termTag;
	
	/** An integer representing the document frequency*/
	protected int docFrequency;
	
	/** An integer representing the termTag frequency*/
	protected int termTagFrequency;
	
	protected long endOffset;
	protected byte endBitOffset;
	protected long startOffset;
	protected byte startBitOffset;

	public static final int termTagStringLength = 12;
	
	/** 
	 * The size in bytes of an entry in the lexicon file.
	 * An entry corresponds to a string, an int (termCode), 
	 * an int (docf), an int (tf), a long (the offset of the end 
	 * of the term's entry in bytes in the inverted file) and
	 * a byte (the offset in bits of the last byte of the term's entry 
	 * in the inverted file.
	 */
	public static final int termTagLexiconEntryLength =
		12 //the string representation
		+4	//the integer
		+4  //the integer
		+8  //the long
		+1  //the byte
		;
	
	/** The actual lexicon file.*/
	protected RandomAccessFile termTagLexiconFile;
	/** The bitfile containing the termids */
	//protected BitFile bitFile;	
	/** The number of entries in the taglexicon file.*/
	protected long numberOfTermTagLexiconEntries;
	/** A buffer for reading from the taglexicon file.*/
	protected byte[] buffer = new byte[512];
	/** A second buffer for finding terms.*/
	protected byte[] bt = new byte[12];
	/** A byte input stream to read from the buffer.*/
	protected ByteArrayInputStream bufferInput =
		new ByteArrayInputStream(buffer);
	/** A data input stream to read from the bufferInput.*/
	protected DataInputStream dataInput = new DataInputStream(bufferInput);
	/** 
	 * A default constructor.
	 */
	public TermTagLexicon() {
		try {
			termTagLexiconFile =
				new RandomAccessFile(
					new File(ApplicationSetup.TERMTAGLEXICON_FILENAME), "r");
			//bitFile = new BitFile(ApplicationSetup.TAGLEXICON_FILENAME + ApplicationSetup.getProperty("tag.termids.suffix", "tid"));
			numberOfTermTagLexiconEntries = termTagLexiconFile.length() / termTagLexiconEntryLength;
			bufferInput.mark(3 * termTagLexiconEntryLength);
		} catch (IOException ioe) {
			System.err.println(
				"Input/output exception while opening for reading the lexicon file." +
				" Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Constructs an instace of Lexicon and opens
	 * the corresponding file.
	 * 
	 * @param lexiconName the name of the lexicon file.
	 */
	public TermTagLexicon(String termTagLexiconName) {
		try {
			termTagLexiconFile = new RandomAccessFile(termTagLexiconName, "r");
			//idToOffsetFile = new RandomAccessFile(tagLexiconName.substring(0,tagLexiconName.lastIndexOf(".")).concat(ApplicationSetup.TAGLEXICON_INDEX_SUFFIX),"rw");
			//bitFile = new BitFile(termTagLexiconName + ApplicationSetup.getProperty("tag.termids.suffix", "tid"));
			numberOfTermTagLexiconEntries = termTagLexiconFile.length() / termTagLexiconEntryLength;
			bufferInput.mark(3 * termTagLexiconEntryLength);
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
			//idToOffsetFile.close();
			termTagLexiconFile.close();
			//bitFile.close();
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
				new FileInputStream(ApplicationSetup.TERMTAGLEXICON_FILENAME);
			BufferedInputStream bis = new BufferedInputStream(fis);
			DataInputStream dis = new DataInputStream(bis);
			int i = 0; //a counter;
			int numberOfTerms;
			//int [] termids;
			startOffset = 0;
			startBitOffset = 0;
			
			//read each entry from the lexicon and 
			//print it to the standard output
			while (	dis.read(buffer, 0, termTagStringLength)!= -1) {
				termTag = new String(buffer);
				docFrequency = dis.readInt();
				termTagFrequency = dis.readInt();
				endOffset = dis.readLong();
				endBitOffset = dis.readByte();
				
				
				System.out.println(
					""
						//+ (i * termTagLexiconEntryLength)
						//+ ", "
						+ termTag.trim()
						+ ", "
						+ docFrequency
						+ ", "
						+ termTagFrequency
						+ ", "
						+ endOffset
						+ ", "
						+ endBitOffset
						);

				i++; //incrementing the counter
				//Set start offset
				startOffset = endOffset;
				startBitOffset = endBitOffset;
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
	 * @return true if the tag is found, else return false
	 * @param _tagId the tag's identifier
	 */
	/*public boolean findTag(int _tagId) {
		try {
			idToOffsetFile.seek(_tagId * 8);
			long tagLexiconOffset = idToOffsetFile.readLong();
			if (tagLexiconOffset == 0) {
				//startOffset = 0;
				//startBitOffset = 0;
				tagLexiconFile.seek(tagLexiconOffset);
				tagLexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				tag = new String(bt);
				tagId = tagLexiconFile.readInt();
				return true;
			} else {
				tagLexiconFile.seek(tagLexiconOffset);
				tagLexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
				tag = new String(bt);
				tagId = tagLexiconFile.readInt();
				return true;
			}
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading the idToOffset file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return false;
	}*/
	
	/** 
	 * Performs a binary search in the termtaglexicon
	 * in order to locate the given tag.
	 * @param _termTag The termtag combination to search for.
	 * @return true if the term-tag is found, and false otherwise.
	 */
	public boolean findTerm(String _termTag) {
		Arrays.fill(buffer, (byte) 0);
		Arrays.fill(bt, (byte) 0);
		byte[] bt2 = _termTag.getBytes();
		int termLength = termTagStringLength;			
		long low = -1;
		long high = numberOfTermTagLexiconEntries;
		long i;
		while (high-low>1) {
			
			i = (long)(high+low)/2;
			try {
				termTagLexiconFile.seek((i * termTagLexiconEntryLength));
				termTagLexiconFile.read(buffer, 0, termLength);
			} catch (IOException ioe) {
				System.err.println(
					"Input/Output exception while reading from lexicon file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
			
			int compareResult = 0;
			compareResult = _termTag.compareTo(new String(buffer).trim());
			//compareResult = Compare.compareWithNumeric(_tag, new String(buffer).trim());
			
			
			if (compareResult < 1)
				high = i;
			else
				low = i;			
		}
		if (high == numberOfTermTagLexiconEntries)
			return false;
		try {
			termTagLexiconFile.seek((high * termTagLexiconEntryLength));
			termTagLexiconFile.read(buffer, 0, termLength);
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading from lexicon file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}	
		boolean found = true;
		int length = _termTag.length();
		for (int j = 0; j < length; j++) {
			if (buffer[j] != bt2[j]) {
				found = false;
				break;
			}
		}
		if (found) {
			try {
				long tagLexiconOffset = high * termTagLexiconEntryLength;
				if (tagLexiconOffset == 0){
					startOffset = 0;
					startBitOffset = 0;
					termTagLexiconFile.seek(tagLexiconOffset);
					termTagLexiconFile.read(bt, 0,termTagStringLength);
					termTag = new String(bt);
					docFrequency = termTagLexiconFile.readInt();
					termTagFrequency = termTagLexiconFile.readInt();
					endOffset = termTagLexiconFile.readLong();
					endBitOffset = termTagLexiconFile.readByte();
				} else{
					termTagLexiconFile.seek(tagLexiconOffset-9);
					startOffset = termTagLexiconFile.readLong();
					startBitOffset = termTagLexiconFile.readByte();
					termTagLexiconFile.read(bt, 0, termTagStringLength);
					termTag = new String(bt);
					docFrequency = termTagLexiconFile.readInt();
					termTagFrequency = termTagLexiconFile.readInt();
					endOffset = termTagLexiconFile.readLong();
					endBitOffset = termTagLexiconFile.readByte();					
				}				
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
	 * Returns the number of entries in the lexicon.
	 * @return the number of entries in the lexicon.
	 */
	public long getNumberOfTermTagLexiconEntries() {
		return numberOfTermTagLexiconEntries;
	}
	
	/**
	 * Insert the method's description here.
	 *
	 * @return java.lang.String The string representation of the seeked term.
	 */
	public String getTag() {
		return this.termTag.trim();
	}
	/**
	 * Returns the termtag's docFrequency.
	 *
	 * @return int the termtag's docFrequency.
	 */
	public int getDocFrequency() {
		return docFrequency;
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
	 * Get the possible termids for this term;
	 * 
	 * @return an array containing the possible termids
	 */
	/*public int[] getTermIds(){
		bitFile.readReset(startByteOffset,startBitOffset,byteOffset,bitOffset);
		int numberOfTerms = bitFile.readUnary()-1;
				
		int[] termids = new int[numberOfTerms];
		if (numberOfTerms>0)
			bitFile.readGammas(termids);
		
		return termids;
	}*/
	
	
	/**
	 * Seeks the i-th entry of the lexicon.
	 * TODO read a byte array from the file and decode it, 
	 * 		instead of reading the different pieces of 
	 *      information separately.
	 * @param i The index of the entry we are looking for.
	 * @return true if the entry was found, false otherwise.
	 */
	public boolean seekEntry(int i) {
		System.out.println("Seeking entry for " + i);
		try {
			if (i > numberOfTermTagLexiconEntries)
				return false;
			if (i == 0) {
				startOffset = 0;
				startBitOffset = 0;
				termTagLexiconFile.seek(i * termTagLexiconEntryLength);
				termTagLexiconFile.read(
					buffer,
					0,
					termTagStringLength);
				termTag = new String(buffer);
				docFrequency = termTagLexiconFile.readInt();
				termTagFrequency = termTagLexiconFile.readInt();
				endOffset = termTagLexiconFile.readLong();
				endBitOffset = termTagLexiconFile.readByte();
				return true;
			} else {
				termTagLexiconFile.seek(
					i * termTagLexiconEntryLength
						- termTagLexiconEntryLength
						+ termTagStringLength
						+ 4
						- 9);
				startOffset = termTagLexiconFile.readLong();
				startBitOffset = termTagLexiconFile.readByte();
				termTagLexiconFile.read(
					buffer,
					0,
					termTagStringLength);
				termTag = new String(buffer);
				docFrequency = termTagLexiconFile.readInt();
				termTagFrequency = termTagLexiconFile.readInt();
				endOffset = termTagLexiconFile.readLong();
				endBitOffset = termTagLexiconFile.readByte();
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
}
