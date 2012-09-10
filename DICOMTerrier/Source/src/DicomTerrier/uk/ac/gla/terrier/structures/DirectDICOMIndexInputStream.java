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
 * The Original Code is BlockDirectIndexInputStream.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures;
import java.io.IOException;
import java.util.ArrayList;

import uk.ac.gla.terrier.compression.BitInputStream;
import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * This class reads the DICOM direct index structure
 * sequentially, as an input stream.
 * @author Douglas Johnson, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class DirectDICOMIndexInputStream {
	/**
	 * A default constructor for the class that opens
	 * the direct file and the document index file with the 
	 * default names
	 *
	 */
	public DirectDICOMIndexInputStream() {
		try {
			gammaInputStream =
				new BitInputStream(
					ApplicationSetup.DIRECT_FILENAME);
			documentIndexStream =
				new DocumentIndexInputStream(
					ApplicationSetup.DOCUMENT_INDEX_FILENAME);
		} catch (IOException ioe) {
			System.err.println(
				"I/O Exception occured while opening the direct file for reading. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	
	/**
	 * A default constructor for the class that opens
	 * the direct file and the document index file with the 
	 * default names
	 *
	 */
	public DirectDICOMIndexInputStream(String directFileName, String docFileName) {
		try {
			gammaInputStream =
				new BitInputStream(
					directFileName);
			documentIndexStream =
				new DocumentIndexInputStream(
					docFileName);
		} catch (IOException ioe) {
			System.err.println(
				"I/O Exception occured while opening the direct file for reading. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	
	/**
	 * Prints out the DICOM direct index file.
	 */
	public void print() {
		int[][] terms = null;
		try {
			int counter = 0;
			while ((terms = getNextTerms()) != null) {
				int blockindex=0;
				for (int j = 0; j < terms[0].length; j++) {
					System.out.print(
						"("
							+ terms[0][j]
							+ ", "
							+ terms[1][j]
							+ ", "
							+ terms[2][j]);
					for (int k = 0; k < terms[2][j]; k++) {
						System.out.print(", " + terms[3][blockindex]);
						blockindex++;
					}
					System.out.print(")");
				}
				counter++;
				System.out.println();
			}
		} catch (IOException ioe) {
			System.err.println(
				"IOException occured while reading the direct file. Exiting.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Reads the next document's entry from the direct file.
	 * @return int[][] the two dimensional array containing the 
	 *         term ids, tagids, and tag frequencies.
	 * @throws java.io.IOException if an I/O error occurs
	 */
	public int[][] getNextTerms() throws IOException {
		documentsSkipped = 0;
		int n = documentIndexStream.readNextEntry();
		while (n != -1 && documentIndexStream.getDocumentLength() == 0) {
			n = documentIndexStream.readNextEntry();
			documentsSkipped++;
		}
			
		if (System.getProperty("debug","").equals("true"))
			System.err.println("docs skipped: " + documentsSkipped);
			
		if (n == -1) { //if the end of file has been reached then return null
			return null;
		}
		long endByteOffset = documentIndexStream.getEndOffset();
		byte endBitOffset = documentIndexStream.getEndBitOffset();
		if (System.getProperty("debug","").equals("true")) {
			System.err.println("docs skipped: " + documentsSkipped);
			System.err.println("endByteOffset: " + endByteOffset);
			System.err.println("endBitOffset: " + endBitOffset);
		}
			
		ArrayList temporaryTerms = new ArrayList();
		ArrayList temporaryTagids = new ArrayList();
		int tagCount=0;
		//System.out.println("Reading next document");
		while ((endByteOffset > gammaInputStream.getByteOffset())
			|| (endByteOffset == gammaInputStream.getByteOffset()
				&& endBitOffset > gammaInputStream.getBitOffset())) {
			int[] tmp = new int[3];
			tmp[0] = gammaInputStream.readGamma();
			//System.out.println("Read termcode gamma " + tmp[0]);
			tmp[1] = gammaInputStream.readUnary();
			//System.out.println("Read frequency unary " + tmp[1]);
			int tagfreq = gammaInputStream.readUnary()-1;
			//System.out.println("Read tagfrequency unary " + tagfreq);
			tmp[2] = tagfreq;
			int[] tmp2 = new int[tagfreq];
			for (int i = 0; i < tagfreq; i++) {
				tmp2[i] = gammaInputStream.readGamma();
				//System.out.println("Read tagcode gamma " + tmp2[i]);
				tagCount++;
			}
			
			temporaryTagids.add(tmp2);
			temporaryTerms.add(tmp);
			
		}
		int[][] documentTerms = new int[4][];
		documentTerms[0] = new int[temporaryTerms.size()];
		documentTerms[1] = new int[temporaryTerms.size()];
		documentTerms[2] = new int[temporaryTerms.size()];
		documentTerms[3] = new int[tagCount];
		documentTerms[0][0] = ((int[]) temporaryTerms.get(0))[0] - 1;
		documentTerms[1][0] = ((int[]) temporaryTerms.get(0))[1];
		documentTerms[2][0] = ((int[]) temporaryTerms.get(0))[2];
		int[] tagids = ((int[])temporaryTagids.get(0));
		
		int tagindex = tagids.length;
		
		if(tagindex>0)
			documentTerms[3][0] = tagids[0] - 1;
			for(int i=1; i<tagids.length; i++){
				documentTerms[3][i]= tagids[i] + documentTerms[3][i-1];
		}		
		
		if (documentTerms[0].length > 1) {
			for (int i = 1; i < documentTerms[0].length; i++) {
				int[] tmpMatrix = (int[]) temporaryTerms.get(i);
				documentTerms[0][i] = tmpMatrix[0] + documentTerms[0][i - 1];
				documentTerms[1][i] = tmpMatrix[1];
				documentTerms[2][i] = tmpMatrix[2];
				tagids = ((int[])temporaryTagids.get(i));
				if (tagids.length>0){
					documentTerms[3][tagindex] = tagids[0] - 1;
					tagindex++;
					for(int j=1; j<tagids.length; j++){
						documentTerms[3][tagindex] = tagids[j] + documentTerms[3][tagindex-1];
						tagindex++;
					}
				}
			}
		}
		return documentTerms;
	}
	
	/**
	 * A document index stream.
	 */
	protected DocumentIndexInputStream documentIndexStream;
	/**
	 * The number of zero-length documents that were skipped during a call of the getNextTerms()
	 */
	protected int documentsSkipped = 0;
	/**
	 * The gamma compressed file containing the terms.
	 */
	protected BitInputStream gammaInputStream;
	/**
	 * Closes the underlying gamma compressed file.
	 */
	public void close() {
		try {
			gammaInputStream.close();
			documentIndexStream.close();
		} catch(IOException ioe) {
			System.err.println("Input/Output exception while closing the direct index input stream.");
			System.err.println("Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Returns the value of the documents that were skipped during
	 * a call to the method getNextTerms()
	 * @return int the number of documents skipped.
	 */
	public int getDocumentsSkipped() {
		return documentsSkipped;
	}
}
