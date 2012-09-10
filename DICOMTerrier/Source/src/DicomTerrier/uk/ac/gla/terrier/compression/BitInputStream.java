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
 * The Original Code is BitInputStream.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.compression;
import java.io.*;
/**
 * This class provides sequential stream access to a GammaCompressed file.
 * @author Gianni Amati, Vassilis Plachouras, Douglas Johnson
 * @version $Revision: 1.1 $
 */
public class BitInputStream {
	/**
	 * Reads a binary integer from the already read buffer.
	 * @param noBits the number of binary bits to read
	 * @throws IOException if an I/O error occurs
	 * @return the decoded integer
	 */
	public int readBinary(int noBits) throws IOException {
		int binary = 0;
		boolean endOfFileExpected = false;
		try {
			for (int i = 0; i < noBits; i++) {
				if ((byteRead & (1 << (bitOffset))) != 0) {
					binary = binary + (1 << i);
				}
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
					endOfFileExpected = true;
					byteRead = dis.readByte();
				}
			}
		} catch (EOFException eofe) {
			if (endOfFileExpected)
				return binary;
			else 
				return -1;
		}
		return binary;
	}
	/** The private input stream used internaly.*/
	private DataInputStream dis = null;
	/** The byte offset.*/
	private long byteOffset;
	/** The bit offset.*/
	private byte bitOffset;
	/** 
	 * A byte read from the stream. This byte should be 
	 * initialised during the construction of the class.
	 */
	private byte byteRead;
	/**
	 * Constructs an instance of the class for a given stream
	 * @param is java.io.InputStream the underlying input stream
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public BitInputStream(InputStream is) throws IOException {
		dis = new DataInputStream(is);
		byteOffset = 0;
		bitOffset = 0;
		byteRead = dis.readByte();
	}
	/** 
	 * Constructs an instance of the class for a given filename
	 * @param filename java.lang.String the name of the undelying file
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public BitInputStream(String filename) throws IOException {
		dis =
			new DataInputStream(
				new BufferedInputStream(new FileInputStream(filename)));
		byteOffset = 0;
		bitOffset = 0;
		byteRead = dis.readByte();
	}
	/**
	 * Constructs an instance of the class for a given file
	 * @param file java.io.File the underlying file
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public BitInputStream(File file) throws IOException {
		dis =
			new DataInputStream(
				new BufferedInputStream(new FileInputStream(file)));
		byteOffset = 0;
		bitOffset = 0;
		byteRead = dis.readByte();
	}
	/** 
	 * Closes the stream.
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public void close() throws IOException {
		dis.close();
	}
	/**
	 * Reads a unary encoded integer from the stream.
	 * @return the decoded integer, or -1 if EOF is found
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public int readUnary() throws IOException {
		int result = 0;
		boolean endOfFileExpected = false;
		try {
			while (true) {
				if ((byteRead & (1 << (bitOffset))) != 0) {
					result++;
					bitOffset++;
					if (bitOffset == 8) {
						bitOffset = 0;
						byteOffset++;
						byteRead = dis.readByte();
					}
				} else {
					result++;
					bitOffset++;
					if (bitOffset == 8) {
						bitOffset = 0;
						byteOffset++;
						endOfFileExpected = true;
						byteRead = dis.readByte();
					}
					break;
				}
			}
		} catch (EOFException eofe) {
			if (endOfFileExpected) 
				return result;
			else 
				return -1;
		}
		return result;
	}
	/**
	 * Reads a gamma encoded integer from the stream
	 * @return the decoded integer, or -1 if EOF is found
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public int readGamma() throws IOException {
		int result = 0;
		int binaryPart = 0;
		int sign = 0;
		boolean endOfFileExpected = false;
		int unaryPart = readUnary();
		if (unaryPart == -1) {
			return -1;
		}
		try {
			for (int i = 0; i < unaryPart - 1; i++) {
				if ((byteRead & (1 << (bitOffset))) != 0) {
					binaryPart = binaryPart + (1 << i);
				}
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
					endOfFileExpected = true;
					byteRead = dis.readByte();
				}
			}
			
			//read the sign bit
			sign = readBinary(1);
			result = binaryPart + (1 << (unaryPart - 1));
						
			if (sign>0)
				result = -result;
			
		} catch (EOFException eofe) {
			if (endOfFileExpected)
				if (sign>0)
					return -(binaryPart + (1 << (unaryPart - 1)));
				else
					return (binaryPart + (1 << (unaryPart - 1)));
			else 
				return -1;
		}
		return result;
				
	}
	/**
	 * Returns the byte offset of the stream. 
	 * It corresponds to the offset of the 
	 * byte from which the next bit will be read.
	 * @return the byte offset in the stream.
	 */
	public long getByteOffset() {
		return byteOffset;
	}
	/**
	 * Returns the bit offset in the last byte. It 
	 * corresponds to the next bit that it will be
	 * read.
	 * @return the bit offset in the stream.
	 */
	public byte getBitOffset() {
		return bitOffset;
	}
}
