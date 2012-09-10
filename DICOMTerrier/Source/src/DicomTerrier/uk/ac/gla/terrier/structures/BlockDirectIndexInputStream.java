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
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * This class reads the block field direct index structure
 * sequentially, as an input stream.
 * @author Douglas Johnson, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class BlockDirectIndexInputStream extends DirectIndexInputStream {
	/**
	 * A default constructor for the class that opens
	 * the direct file and the document index file with the 
	 * default names
	 *
	 */
	public BlockDirectIndexInputStream() {
		super();
	}
	
	/**
	 * Prints out the block field direct index file.
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
							+ terms[2][j]
							+ ", "
							+ terms[3][j]);
					for (int k = 0; k < terms[3][j]; k++) {
						System.out.print(", " + terms[4][blockindex]);
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
	 *         term ids, block ids, and block frequencies.
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
		ArrayList temporaryBlockids = new ArrayList();
		boolean hasMore = false;
		int blockCount=0;
		while ((endByteOffset > gammaInputStream.getByteOffset())
			|| (endByteOffset == gammaInputStream.getByteOffset()
				&& endBitOffset > gammaInputStream.getBitOffset())) {
			int[] tmp = new int[4];
			tmp[0] = gammaInputStream.readGamma();
			tmp[1] = gammaInputStream.readUnary();
			tmp[2] = gammaInputStream.readBinary(FieldScore.FIELDS_COUNT);
			int blockfreq = gammaInputStream.readUnary();
			tmp[3] = blockfreq;
			int[] tmp2 = new int[blockfreq];
			for (int i = 0; i < blockfreq; i++) {
				tmp2[i] = gammaInputStream.readGamma();
				blockCount++;
			}
			temporaryTerms.add(tmp);
			temporaryBlockids.add(tmp2);
		}
		int[][] documentTerms = new int[5][];
		documentTerms[0] = new int[temporaryTerms.size()];
		documentTerms[1] = new int[temporaryTerms.size()];
		documentTerms[2] = new int[temporaryTerms.size()];
		documentTerms[3] = new int[temporaryTerms.size()];
		documentTerms[4] = new int[blockCount];
		documentTerms[0][0] = ((int[]) temporaryTerms.get(0))[0] - 1;
		documentTerms[1][0] = ((int[]) temporaryTerms.get(0))[1];
		documentTerms[2][0] = ((int[]) temporaryTerms.get(0))[2];
		documentTerms[3][0] = ((int[]) temporaryTerms.get(0))[3];
		int[] blockids = ((int[])temporaryBlockids.get(0));
		
		documentTerms[4][0] = blockids[0] - 1;
		for(int i=1; i<blockids.length; i++){
			documentTerms[4][i]= blockids[i] + documentTerms[4][i-1];
		}
		
		int blockindex = blockids.length;
		if (documentTerms[0].length > 1) {
			for (int i = 1; i < documentTerms[0].length; i++) {
				int[] tmpMatrix = (int[]) temporaryTerms.get(i);
				documentTerms[0][i] = tmpMatrix[0] + documentTerms[0][i - 1];
				documentTerms[1][i] = tmpMatrix[1];
				documentTerms[2][i] = tmpMatrix[2];
				documentTerms[3][i] = tmpMatrix[3];
				blockids = ((int[])temporaryBlockids.get(i));
				documentTerms[4][blockindex] = blockids[0] - 1;
				blockindex++;
				for(int j=1; j<blockids.length; j++){
					documentTerms[4][blockindex] = blockids[j] + documentTerms[4][blockindex-1];
					blockindex++;
				}
			}
		}
		return documentTerms;
	}
}
