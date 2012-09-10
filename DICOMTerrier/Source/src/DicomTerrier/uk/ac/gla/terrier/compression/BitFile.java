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
 * The Original Code is BitFile.java.
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
 * This class encapsulates a random access file and provides
 * the functionalities to write binary encoded, unary encoded and gamma encoded
 * integers greater than zero, as well as specifying their offset in the file. It
 * is employed by the DirectFile and the InvertedFile classes.
 * The sequence of method calls to write a sequence of gamma encoded
 * and unary encoded numbers is:<br>
 *	file.writeReset();<br> 	
 *	long startByte1 = file.getByteOffset();<br>
 *  byte startBit1 = file.getBitOffset();<br>
 *  file.writeGamma(20000);<br>
 *  file.writeUnary(2);<br>
 *  file.writeGamma(35000);<br>
 *  file.writeUnary(1);<br>
 *  file.writeGamma(3);<br>
 *  file.writeUnary(2);<br>
 *  file.writeFlush();<br>
 *  long endByte1 = file.getByteOffset();<br>
 *  byte endBit1 = file.getBitOffset();<br>
 *  if (endBit1 == 0 && endByte1 > 0) {<br>
 *      endBit1 = 7;<br>
 *      endByte1--;<br>
 *  }<br>
 * while for reading a sequence of numbers the sequence of calls is:<br>
 *  file.readReset((long) startByte1, (byte) startBit1, (long) endByte1, (byte) endBit1);<br>
 *  int gamma = file.readGamma();<br>
 *	int unary = file.readUnary();<br>
 *  
 * @author Gianni Amati, Vassilis Plachouras, Douglas Johnson
 * @version $Revision: 1.1 $
 */
public class BitFile {
	/** A constant for saving time during writeGamma.*/
	protected static final double LOG_E_2 = Math.log(2.0d);
	
	/**
	 * Reads a binary integer from the already read buffer.
	 * @param noBits the number of binary bits to read
	 * @return the decoded integer
	 */
	public int readBinary(int noBits) {
		int binary = 0;
		for (int i = 0; i < noBits; i++) {
			//if ((inBuffer[(int) byteOffset] & (1 << (bitOffset))) != 0) {
			if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0) {
				binary = binary + (1 << i);
			}
			readBitOffset++;
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
			}
		}
		return binary;
	}
	/**
	 * Writes a binary integer, of a given length, to the already read buffer.
	 * @param bitsToWrite the number of bits to write
	 * @param n the integer to write
	*/
	
	public void writeBinary(int bitsToWrite, int n)
	{
		byte rem;
		while (n != 0) {
			rem = (new Integer(n % 2)).byteValue();
			buffer |= (rem << bitOffset);
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
				outBuffer.write(buffer);
				buffer = 0;
			}
			n = n / 2;
			bitsToWrite--;
		}
		while (bitsToWrite > 0) {
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
				outBuffer.write(buffer);
				buffer = 0;
			}
			bitsToWrite--;
		}
	}
	/** A flag set to true if the last byte writen in the file is incomplete.*/
	protected boolean isLastByteIncomplete;
	
	/** The underlying random access file.*/
	protected RandomAccessFile file;
	/** 
	 * The current byte offset. This attribute has 
	 * two functionalities. While writing to the file, 
	 * it corresponds to the byte offset from the beginning of 
	 * the file. While reading entries from the file, it 
	 * corresponds to the byte offset from the beginning 
	 * of the buffer.*/
	protected long byteOffset;
	//protected long byteFileOffset;
	/** The current offset in the file in bits in the last byte.*/
	protected byte bitOffset;
	
	/** A buffer for storing processed bytes.*/
	protected ByteArrayOutputStream outBuffer;
	/** A buffer for reading bytes.*/
	protected byte[] inBuffer;
	/** A byte long buffer.*/
	protected byte buffer;
	/** The number of bits read in the inBuffer.*/
	protected int readBits;
	/** The number of bits read already from the buffer.*/
	protected int readBitOffset;
	/** A constuctor for an instance of this class, given an abstract file.*/
	public BitFile(File file) {
		try {
			this.file = new RandomAccessFile(file, "rw");
			bitOffset = 0;
			byteOffset = 0;
			
		} catch (IOException ioe) {
			System.err.println("Input/Output exception while creating GammaCompressedFile object.");
			ioe.printStackTrace();
			System.exit(1);
		}
			
	}
	
	/** A constuctor for an instance of this class, given an abstract file.*/
	public BitFile(File file, String access) {
		try {
			this.file = new RandomAccessFile(file, access);
			bitOffset = 0;
			byteOffset = 0;
			
		} catch (IOException ioe) {
			System.err.println("Input/Output exception while creating GammaCompressedFile object.");
			ioe.printStackTrace();
			System.exit(1);
		}
			
	}
	
	/** A constuctor for an instance of this class.*/
	public BitFile(String filename) {
		try {
			file = new RandomAccessFile(filename, "rw");
			bitOffset = 0;
			byteOffset = 0;
			
		} catch (IOException ioe) {
			System.err.println("Input/Output exception while creating GammaCompressedFile object.");
			ioe.printStackTrace();
			System.exit(1);
		}	
	}
	
	/** A constuctor for an instance of this class.*/
	public BitFile(String filename, String access) {
		try {
			file = new RandomAccessFile(filename, access);
			bitOffset = 0;
			byteOffset = 0;
			
		} catch (IOException ioe) {
			System.err.println("Input/Output exception while creating GammaCompressedFile object.");
			ioe.printStackTrace();
			System.exit(1);
		}	
	}
	
	/**
	 * Closes the random access file.
	 */
	public void close() {
		try {
			file.close();
		} catch(IOException ioe) {
			System.err.println("Input/Output exception while closing the file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Returns the bit offset of the last current byte in the buffer. 
	 * This offset corresponds to the position where the
	 * next bit is going to be written.
	 * @return the bit offset of the current byte in the buffer.
	 */
	public byte getBitOffset() {
		return bitOffset;
	}
	/** 
	 * Returns the byte offset in the buffer. This offset corresponds
	 * to the byte in which the next bit is going to be written or read from.
	 * @return the byte offset in the buffer.
	 */
	public long getByteOffset() {
		return byteOffset;
	}
	/**
	 * Reads and decodes a gamma encoded integer from the already read buffer.
	 * @return the decoded integer
	 */
	public int readGamma() {
		int result = 0;
		int unaryPart = readUnary();
		int sign = 0;
		int mask;
		
		int binaryPart = 0;
		for (int i=0; i<unaryPart-1; i++) {
		
			//if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
			if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
				binaryPart = binaryPart + (1 << i);
			readBitOffset++;
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
			}
		}
		
		//sign = readBinary(1);
		if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
			sign++;
		
		readBitOffset++;
		bitOffset++;
		if (bitOffset == 8) {
			bitOffset = 0;
			byteOffset++;
		}
		
		result = binaryPart + (1 << (unaryPart-1));
		
		mask = -sign;
		int result1 = (result^mask) + sign;
		/*
		int result2;
		if (sign>0)
			result2 = -result;
		else 
			result2 = result;
		
		if (result1 != result2){
			System.out.println("BIGERROR " + result1 + " is not " + result2 + " in bitfile gamma decoding" );
			System.out.println("result " + Integer.toBinaryString(result));
			System.out.println("sign " + Integer.toBinaryString(sign));
			System.out.println("mask " + Integer.toBinaryString(mask));
			System.out.println("result1 " + Integer.toBinaryString(result1));
			System.out.println("result2 " + Integer.toBinaryString(result2));
		}*/
			
		return result1;	
	}
	
	/**
	 * Reads and decodes multiple gamma encoded integers from the already read buffer.
	 * It already adds the gamma gaps resulting in the correct integers 
	 */
	public void readGammas (int[] gammas){
		int result;
		int unaryPart;
		int sign;
		int mask;
		int binaryPart;
		
		result = 0;
		unaryPart = readUnary();
		sign = 0;
		
		binaryPart = 0;
		for (int i=0; i<unaryPart-1; i++) {
		
			//if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
			if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
				binaryPart = binaryPart + (1 << i);
			readBitOffset++;
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
			}
		}
		
		//sign = readBinary(1);
		if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
			sign++;
		
		readBitOffset++;
		bitOffset++;
		if (bitOffset == 8) {
			bitOffset = 0;
			byteOffset++;
		}
		
		result = binaryPart + (1 << (unaryPart-1));
		
		mask = -sign;
		gammas[0] = ((result^mask) + sign) -1;		
		
		for (int k =1; k<gammas.length; k++){
			result = 0;
			unaryPart = readUnary();
			sign = 0;
			
			binaryPart = 0;
			for (int i=0; i<unaryPart-1; i++) {
			
				//if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
				if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
					binaryPart = binaryPart + (1 << i);
				readBitOffset++;
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
				}
			}
			
			//sign = readBinary(1);
			if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0)
				sign++;
			
			readBitOffset++;
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
			}
			
			result = binaryPart + (1 << (unaryPart-1));
			
			mask = -sign;
			gammas[k] = ((result^mask) + sign) + gammas[k-1];	
		}
	}
	
	
	/**
	 * Reads from the file a specific number of bytes and after this
	 * call, a sequence of read calls may follow. The offsets given 
	 * as arguments are inclusive. For example, if we call this method
	 * with arguments 0, 2, 1, 7, it will read in a buffer the contents 
	 * of the underlying file from the third bit of the first byte to the 
	 * last bit of the second byte.
	 * @param startByteOffset the starting byte to read from
	 * @param startBitOffset the bit offset in the starting byte
	 * @param endByteOffset the ending byte 
	 * @param endBitOffset the bit offset in the ending byte. 
	 *        This bit is the last bit of this entry.
	 */
	public void readReset(long startByteOffset, byte startBitOffset, long endByteOffset, byte endBitOffset) {
		try {
			file.seek(startByteOffset);
			inBuffer = new byte[(int)(endByteOffset - startByteOffset + 1)];
			file.read(inBuffer);
			readBits = (inBuffer.length * 8) - startBitOffset - (8 - endBitOffset) + startBitOffset;
			//TODO check whether it is better to set byteOffset to 0 or 
			//to set it equal to startByteOffset.
			byteOffset = 0;
			readBitOffset = startBitOffset;
			bitOffset = startBitOffset;			
		} catch(IOException ioe) {
			System.err.println("Input/Output exception while reading from a random access file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Reads a unary integer from the already read buffer.
	 * @return the decoded integer
	 */
	public int readUnary() {
		int result = 0;
		while (readBitOffset <= readBits) {
			//if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0 ) {
			if ((inBuffer[(int)byteOffset] & (1 << (bitOffset))) != 0 ) {
				result++;
				readBitOffset++;
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
				}
			} else {
				result++;
				readBitOffset++;
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
				}
				break;
			}
		}
		return result;
	}
	/** 
	 * Flushes the in-memory buffer to the file after 
	 * finishing a sequence of write calls.
	 */
	public void writeFlush() {
		try {
			if (bitOffset > 0)
				outBuffer.write(buffer);
			if (isLastByteIncomplete) {
				file.seek(file.length() - 1);
				file.write(outBuffer.toByteArray());
			} else
				file.write(outBuffer.toByteArray());
		} catch(IOException ioe) {
			System.err.println("Input/Output exception while writing to the file. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/** 
	 * Writes an gamma encoded integer in the buffer.
	 * @param n The integer to be encoded and saved in the buffer.
	 */
	public void writeGamma(int n) {
		int x=Math.abs(n);
		byte mask = 1;
		
		//int floor = (int) Math.floor(Math.log(n)/Math.log(2.0D));
		//reducing the number of calls to the maths library.
		int floor = (int) Math.floor(Math.log(x)/LOG_E_2);
		
		int firstPart = 1 + floor;
		//int secondPart = (int) (n - Math.pow(2, floor));
		//instead of raising 2 to the floor(!), we shift 1
		//floor places
		int secondPart = (int) (x - (1<<(floor)));
		//write first part as a unary
		writeUnary(firstPart);
		
		//write the second part as binary
		int tmpPart = secondPart;
		for (int i=0; i<floor; i++) {
			if ((tmpPart & (1 << i)) != 0) {
				buffer |= (mask << bitOffset);
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
					outBuffer.write(buffer);
					buffer = 0;
				}
			} else {
				bitOffset++;
				if (bitOffset == 8) {
					bitOffset = 0;
					byteOffset++;
					outBuffer.write(buffer);
					buffer = 0;
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
	 * Prepares for writing to the file unary or gamma encoded integers.
	 * It reads the last incomplete byte from the file, according to the
	 * bitOffset value
	 */
	public void writeReset() {
		outBuffer = new ByteArrayOutputStream();
		//if bitOffset is not equal 0, reads the last incomplete byte
		if (bitOffset != 0) {
			isLastByteIncomplete = true;
			try {
				byteOffset = file.length() - 1;
				file.seek(byteOffset);
				buffer = file.readByte();
			} catch(IOException ioe) {
				System.err.println("Input/output exception while reading file. Stack trace follows.");
				ioe.printStackTrace();
				System.exit(1);
			}
		} else {
			isLastByteIncomplete = false;
			buffer = 0;
		}
	}
	/** 
	 * Writes a unary integer to the buffer.
	 * @param n The integer to be encoded and writen in the buffer.
	 */
	public void writeUnary(int n) {
		byte mask = 1;
		
		//write n-1 ones
		for (int i=0; i<n-1; i++) {
			buffer |= (mask << bitOffset);
			bitOffset++;
			if (bitOffset == 8) {
				bitOffset = 0;
				byteOffset++;
				outBuffer.write(buffer);
				buffer = 0;
			}
		}
		//and end with 1 zero
		bitOffset++;
		if (bitOffset == 8) {
			bitOffset = 0;
			byteOffset++;
			outBuffer.write(buffer);
			buffer = 0;
		}
	}
}
