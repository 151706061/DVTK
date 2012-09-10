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
 * The Original Code is BitOutputStream.java.
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
 * This class creates sequentially a GammaCompressed file from a stream.
 * @author Gianni Amati, Vassilis Plachouras, Douglas Johnson
 * @version $Revision: 1.1 $
 */
public class BitOutputStream {
	/**
	 * Writes a binary integer to the already read buffer.
	 * @param bitsToWrite the number of bits to use for encoding
	 * @param n the integer
	 */
	public void writeBinary(int bitsToWrite, int n) throws IOException {
		byte rem;
		while (n != 0) {
			rem = (new Integer(n % 2)).byteValue();
			byteToWrite |= (rem << bitOffset);
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
				dos.writeByte(byteToWrite);
				dos.flush();
				byteToWrite = 0;
			}
			n = n / 2;
			bitsToWrite--;
		}
		while (bitsToWrite >0) {
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
				dos.write(byteToWrite);
				dos.flush();
				byteToWrite = 0;
			}
			bitsToWrite--;
		}
	}	
	/** The private output stream used internaly.*/
	private DataOutputStream dos = null;
	/** The byte offset.*/
	private long byteOffset;
	/** The bit offset.*/
	private byte bitOffset;
	/** A byte to write to the stream. */
	private byte byteToWrite;
	/** 
	 * The natural logarithm of two. It is used
	 * for changing the base of logarithm from
	 * e to 2.
	 */
	private static double LOG_2 = Math.log(2.0D);
	/**
	 * Constructs an instance of the class for a given stream
	 * @param is java.io.OutputStream the underlying input stream
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public BitOutputStream(OutputStream is) throws IOException {
		dos = new DataOutputStream(is);
		byteOffset = 0;
		bitOffset = 0;
		byteToWrite = (byte)0;
	}
	/** 
	 * Constructs an instance of the class for a given filename
	 * @param filename java.lang.String the name of the undelying file
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public BitOutputStream(String filename) throws IOException {
		dos = new DataOutputStream(new BufferedOutputStream(new FileOutputStream(filename)));
		byteOffset = 0;
		bitOffset = 0;
		byteToWrite = (byte)0;
	}
	/**
	 * Constructs an instance of the class for a given file
	 * @param file java.io.File the underlying file
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public BitOutputStream(File file) throws IOException {
		dos = new DataOutputStream(new BufferedOutputStream(new FileOutputStream(file)));
		byteOffset = 0;
		bitOffset = 0;
		byteToWrite = (byte)0;
	}
	/** 
	 * Closes the stream.
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public void close() throws IOException {
		if (bitOffset!=0)
			dos.writeByte(byteToWrite);
		dos.flush();
		dos.close();
	}
	/**
	 * Writes a unary encoded integer to the stream.
	 * @param n int the integer to write.
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public void writeUnary(int n) throws IOException {
		byte mask = 1;
		//write n-1 ones
		for (int i=0; i<n-1; i++) {
			byteToWrite |= (mask << bitOffset);
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
				dos.writeByte(byteToWrite);
				dos.flush();
				byteToWrite = 0;
			}
		}
		//and end with a zero
		bitOffset++;
		if (bitOffset == 8) {
			bitOffset = 0;
			byteOffset++;
			dos.writeByte(byteToWrite);
			dos.flush();
			byteToWrite = 0;
		}
	}
	/** 
	 * Writes a signed!! gamma encoded integer to the stream.
	 * @param n The integer to be encoded and saved in the buffer.
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public void writeGamma(int n) throws IOException {
		int x = Math.abs(n);
		byte mask = 1;
		int floor = (int) Math.floor(Math.log(x)/LOG_2);
		int firstPart = 1 + floor;
		int secondPart = (int) (x - Math.pow(2, floor));
		//write first part as a unary
		writeUnary(firstPart);
		
		//write the second part as binary
		int tmpPart = secondPart;
		for (int i=0; i<floor; i++) {
			if ((tmpPart & (1 << i)) != 0) {
				byteToWrite |= (mask << bitOffset);
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
					dos.writeByte(byteToWrite);
					byteToWrite = 0;
				}
			} else {
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
					dos.writeByte(byteToWrite);
					byteToWrite = 0;
				}				
			}
		}
		//write the sign bit
		if (n<0)
			writeBinary(1,1);
		else
			writeBinary(1,0);
			
	}
	/**
	 * Returns the byte offset of the stream.
	 * It corresponds to the position of the 
	 * byte in which the next bit will be written.
	 * @return the byte offset in the stream.
	 */
	public long getByteOffset() {
		return byteOffset;
	}
	/**
	 * Returns the bit offset in the last byte.
	 * It corresponds to the position in which
	 * the next bit will be written.
	 * @return the bit offset in the stream.
	 */
	public byte getBitOffset() {
		return bitOffset;
	}
}
