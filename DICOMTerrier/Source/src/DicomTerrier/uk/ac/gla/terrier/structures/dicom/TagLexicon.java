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
package uk.ac.gla.terrier.structures.dicom;
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
 * @see ApplicationSetup#TAGLEXICON_FILENAME
 * @see ApplicationSetup#TAGLEXICON_INDEX_FILENAME
 * @author Gerald Veldhuijsen
 * @version Version 1.0
 */
public class TagLexicon {
	/** The tag represented as an array of bytes.*/
	protected byte[] tagCharacters;
	/** The tag represented as a string.*/
	protected String tag;
	/** An integer representing the id of the tag.*/
	protected int tagId;
	
	protected long byteOffset;
	protected byte bitOffset;
	protected long startByteOffset;
	protected byte startBitOffset;

	/** 
	 * The size in bytes of an entry in the lexicon file.
	 * An entry corresponds to a string, an int (termCode), 
	 * an int (docf), an int (tf), a long (the offset of the end 
	 * of the term's entry in bytes in the inverted file) and
	 * a byte (the offset in bits of the last byte of the term's entry 
	 * in the inverted file.
	 */
	public static final int tagLexiconEntryLength =
		ApplicationSetup.STRING_BYTE_LENGTH //the string representation
		+4	//the integer
		+8  //the long
		+1  //the byte
		;
	///** The file containing the mapping from the codes to the offset in the lexicon file.*/
	//protected RandomAccessFile idToOffsetFile;
	/** The actual lexicon file.*/
	protected RandomAccessFile tagLexiconFile;
	/** The bitfile containing the termids */
	protected BitFile bitFile;	
	/** The number of entries in the taglexicon file.*/
	protected long numberOfTagLexiconEntries;
	/** A buffer for reading from the taglexicon file.*/
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
	public TagLexicon() {
		try {
			tagLexiconFile =
				new RandomAccessFile(
					new File(ApplicationSetup.TAGLEXICON_FILENAME), "r");
			//idToOffsetFile = 
			//	new RandomAccessFile(
			//		new File(ApplicationSetup.TAGLEXICON_INDEX_FILENAME), "r");
			bitFile = new BitFile(ApplicationSetup.TAGLEXICON_FILENAME + ApplicationSetup.getProperty("tag.termids.suffix", "tid"));
			numberOfTagLexiconEntries = tagLexiconFile.length() / tagLexiconEntryLength;
			bufferInput.mark(3 * tagLexiconEntryLength);
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
	public TagLexicon(String tagLexiconName) {
		try {
			tagLexiconFile = new RandomAccessFile(tagLexiconName, "r");
			//idToOffsetFile = new RandomAccessFile(tagLexiconName.substring(0,tagLexiconName.lastIndexOf(".")).concat(ApplicationSetup.TAGLEXICON_INDEX_SUFFIX),"rw");
			bitFile = new BitFile(tagLexiconName + ApplicationSetup.getProperty("tag.termids.suffix", "tid"));
			numberOfTagLexiconEntries = tagLexiconFile.length() / tagLexiconEntryLength;
			bufferInput.mark(3 * tagLexiconEntryLength);
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
			tagLexiconFile.close();
			bitFile.close();
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
				new FileInputStream(ApplicationSetup.TAGLEXICON_FILENAME);
			BufferedInputStream bis = new BufferedInputStream(fis);
			DataInputStream dis = new DataInputStream(bis);
			int i = 0; //a counter;
			int numberOfTerms;
			int [] termids;
			startByteOffset = 0;
			startBitOffset = 0;
			
			//read each entry from the lexicon and 
			//print it to the standard output
			while (	dis.read(buffer, 0, ApplicationSetup.STRING_BYTE_LENGTH)!= -1) {
				tag = new String(buffer);
				tagId = dis.readInt();
				byteOffset = dis.readLong();
				bitOffset = dis.readByte();
				
				
				System.out.print(
					""
						+ (i * tagLexiconEntryLength)
						+ ", "
						+ tag.trim()
						+ ", "
						+ tagId	
						
						);
				
				termids = getTermIds();
				
				System.out.print(" (" + termids.length + ")[");
				for (int j=0; j<termids.length; j++)
						System.out.print(termids[j]+", ");
				System.out.println("]");
				i++; //incrementing the counter
				//Set start offset
				startByteOffset = byteOffset;
				startBitOffset = bitOffset;
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
	 * Performs a binary search in the taglexicon
	 * in order to locate the given tag.
	 * If the tag is located, the propertie
	 * tagCharacters contains the
	 * value related to the tag
	 * @param _tag The tag to search for.
	 * @return true if the tag is found, and false otherwise.
	 */
	public boolean findTag(String _tag) {
		Arrays.fill(buffer, (byte) 0);
		Arrays.fill(bt, (byte) 0);
		byte[] bt2 = _tag.getBytes();
		int termLength = ApplicationSetup.STRING_BYTE_LENGTH;			
		long low = -1;
		long high = numberOfTagLexiconEntries;
		long i;
		while (high-low>1) {
			
			i = (long)(high+low)/2;
			try {
				tagLexiconFile.seek((i * tagLexiconEntryLength));
				tagLexiconFile.read(buffer, 0, termLength);
			} catch (IOException ioe) {
				System.err.println(
					"Input/Output exception while reading from lexicon file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
			
			int compareResult = 0;
			compareResult = _tag.compareTo(new String(buffer).trim());
			//compareResult = Compare.compareWithNumeric(_tag, new String(buffer).trim());
			
			
			if (compareResult < 1)
				high = i;
			else
				low = i;			
		}
		if (high == numberOfTagLexiconEntries)
			return false;
		try {
			tagLexiconFile.seek((high * tagLexiconEntryLength));
			tagLexiconFile.read(buffer, 0, termLength);
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while reading from lexicon file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}	
		boolean found = true;
		int length = _tag.length();
		for (int j = 0; j < length; j++) {
			if (buffer[j] != bt2[j]) {
				found = false;
				break;
			}
		}
		if (found) {
			try {
				long tagLexiconOffset = high * tagLexiconEntryLength;
				if (tagLexiconOffset == 0){
					startByteOffset = 0;
					startBitOffset = 0;
					tagLexiconFile.seek(tagLexiconOffset);
					tagLexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
					tag = new String(bt);
					tagId = tagLexiconFile.readInt();
					byteOffset = tagLexiconFile.readLong();
					bitOffset = tagLexiconFile.readByte();
				} else{
					tagLexiconFile.seek(tagLexiconOffset-9);
					startByteOffset = tagLexiconFile.readLong();
					startBitOffset = tagLexiconFile.readByte();
					tagLexiconFile.read(bt, 0, ApplicationSetup.STRING_BYTE_LENGTH);
					tag = new String(bt);
					tagId = tagLexiconFile.readInt();
					byteOffset = tagLexiconFile.readLong();
					bitOffset = tagLexiconFile.readByte();					
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
	public long getNumberOfTagLexiconEntries() {
		return numberOfTagLexiconEntries;
	}
	
	/**
	 * Insert the method's description here.
	 *
	 * @return java.lang.String The string representation of the seeked term.
	 */
	public String getTag() {
		return this.tag.trim();
	}
	/**
	 * Returns the tag's id.
	 *
	 * @return int the tag's id.
	 */
	public int getTagId() {
		return tagId;
	}
	
	/**
	 * Get the possible termids for this term;
	 * 
	 * @return an array containing the possible termids
	 */
	public int[] getTermIds(){
		bitFile.readReset(startByteOffset,startBitOffset,byteOffset,bitOffset);
		int numberOfTerms = bitFile.readUnary()-1;
				
		int[] termids = new int[numberOfTerms];
		if (numberOfTerms>0)
			bitFile.readGammas(termids);
		
		return termids;
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
		System.out.println("Seeking entry for " + i);
		try {
			if (i > numberOfTagLexiconEntries)
				return false;
			if (i == 0) {
				startByteOffset = 0;
				startBitOffset = 0;
				tagLexiconFile.seek(i * tagLexiconEntryLength);
				tagLexiconFile.read(
					buffer,
					0,
					ApplicationSetup.STRING_BYTE_LENGTH);
				tag = new String(buffer);
				tagId = tagLexiconFile.readInt();
				byteOffset = tagLexiconFile.readLong();
				bitOffset = tagLexiconFile.readByte();
				return true;
			} else {
				tagLexiconFile.seek(
					i * tagLexiconEntryLength
						- tagLexiconEntryLength
						+ ApplicationSetup.STRING_BYTE_LENGTH
						+ 4
						- 9);
				startByteOffset = tagLexiconFile.readLong();
				startBitOffset = tagLexiconFile.readByte();
				tagLexiconFile.read(
					buffer,
					0,
					ApplicationSetup.STRING_BYTE_LENGTH);
				tag = new String(buffer);
				tagId = tagLexiconFile.readInt();
				byteOffset = tagLexiconFile.readLong();
				bitOffset = tagLexiconFile.readByte();
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
