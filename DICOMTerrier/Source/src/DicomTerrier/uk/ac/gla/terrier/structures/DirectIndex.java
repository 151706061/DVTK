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
 * The Original Code is DirectIndex.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures;
import java.io.IOException;
import java.util.ArrayList;
import uk.ac.gla.terrier.compression.BitFile;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * A class that implements the direct index and saves 
 * information about whether a term appears in 
 * one of the specified fields.
 * @author Douglas Johnson, Vassilis Plachouras, Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class DirectIndex {
	/** Indicates whether field information is indexed. */
	protected static final boolean saveTagInformation = 
		FieldScore.USE_FIELD_INFORMATION;
	
	/** The gamma compressed file containing the terms.*/
	protected BitFile file;
	/** The document index employed for retrieving the document offsets.*/
	protected DocumentIndex docIndex;
	
	/**
	 * Constructs an instance of the direct index 
	 * with the given document index, and a default 
	 * name for the underlying direct file.
	 * @param docIndex The document index to be used.
	 */
	public DirectIndex(DocumentIndex docIndex) {
		System.out.println("Instantiating direct index");
		file = new BitFile(ApplicationSetup.DIRECT_FILENAME);
		this.docIndex = docIndex;
	}
	/**
	 * Constructs an instance of the direct index
	 * with the given document index and a non-default
	 * name for the underlying direct file.
	 * @param docIndex The document index to be used
	 * @param filename the non-default filename used 
	 * 		  for the underlying direct file.
	 */
	public DirectIndex(DocumentIndex docIndex, String filename) {
		System.out.println("Instantiating direct index");
		file = new BitFile(filename);
		this.docIndex = docIndex;
	}
	/**
	 * Returns a two dimensional array containing the 
	 * term ids and the term frequencies for 
	 * the given document. 
	 * @return int[][] the two dimensional [n][3] array 
	 * 		   containing the term ids, frequencies and field scores. If
	 *         the given document identifier is not found in the document
	 *         index, then the method returns null. If fields are not used, 
	 *         then the dimension of the returned array are [n][2].
	 * @param docid the document identifier of the document which terms 
	 * 		  we retrieve.
	 */
	public int[][] getTerms(int docid) {
		try {
			boolean found = docIndex.seek(docid);
			if (!found)
				return null;
			FilePosition startOffset = docIndex.getDirectIndexStartOffset();
			FilePosition endOffset = docIndex.getDirectIndexEndOffset();
			final boolean loadTagInformation = FieldScore.USE_FIELD_INFORMATION;
			final int fieldTags = FieldScore.FIELDS_COUNT;
			ArrayList temporaryTerms = new ArrayList();
			int[][] documentTerms = null;
			file.readReset(startOffset.Bytes, startOffset.Bits, endOffset.Bytes, endOffset.Bits);

			if (loadTagInformation) { //if there is tag information to process
				while (((file.getByteOffset() + startOffset.Bytes) < endOffset.Bytes)
					|| (((file.getByteOffset() + startOffset.Bytes) == endOffset.Bytes)
						&& (file.getBitOffset() < endOffset.Bits))) {
					
					int[] tmp = new int[3];
					tmp[0] = file.readGamma();
					tmp[1] = file.readUnary();
					tmp[2] = file.readBinary(fieldTags);
					temporaryTerms.add(tmp);
				}
				documentTerms = new int[3][temporaryTerms.size()];
				int[] documentTerms0 = documentTerms[0];
				int[] documentTerms1 = documentTerms[1];
				int[] documentTerms2 = documentTerms[2];
				documentTerms0[0] = ((int[]) temporaryTerms.get(0))[0] - 1;
				documentTerms1[0] = ((int[]) temporaryTerms.get(0))[1];
				documentTerms2[0] = ((int[]) temporaryTerms.get(0))[2];
				if (documentTerms[0].length > 1) {
					final int documentTerms0Length = documentTerms[0].length;
					for (int i=1; i<documentTerms0Length; i++) {
						int[] tmpMatrix = (int[]) temporaryTerms.get(i);
						documentTerms0[i] = tmpMatrix[0] + documentTerms[0][i - 1];
						documentTerms1[i] = tmpMatrix[1];
						documentTerms2[i] = tmpMatrix[2];
					}
					
				}
			} else { //else if there is no tag information to process
				while (((file.getByteOffset() + startOffset.Bytes) < endOffset.Bytes)
						|| (((file.getByteOffset() + startOffset.Bytes) == endOffset.Bytes)
							&& (file.getBitOffset() < endOffset.Bits))) {
					int[] tmp = new int[2];
					tmp[0] = file.readGamma();
					tmp[1] = file.readUnary();
					temporaryTerms.add(tmp);
				}
				documentTerms = new int[2][temporaryTerms.size()];
				int[] documentTerms0 = documentTerms[0];
				int[] documentTerms1 = documentTerms[1];
				documentTerms0[0] = ((int[]) temporaryTerms.get(0))[0] - 1;
				documentTerms1[0] = ((int[]) temporaryTerms.get(0))[1];
				if (documentTerms0.length > 1) {
					final int documentTerms0Length = documentTerms0.length;
					for (int i=1; i<documentTerms0Length; i++) {
						int[] tmpMatrix = (int[]) temporaryTerms.get(i);
						documentTerms0[i] = tmpMatrix[0] + documentTerms[0][i - 1];
						documentTerms1[i] = tmpMatrix[1];
					}
					
				}
			}
			return documentTerms;
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while fetching the term ids for " +
				"a given document. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
		return null;
	}
	/**
	 * Prints out the direct index file.
	 */
	public void print() {
		for (int i = 0; i < docIndex.getNumberOfDocuments(); i++) {
			int[][] terms = getTerms(i);
			final int termColumns = terms.length;
			for (int j = 0; j < terms[0].length; j++) {
				System.out.print("(");
				for (int k = 0; k < termColumns-1; k++) {
					System.out.print(terms[k][j] + ", ");
				}
				System.out.print(terms[termColumns-1][j] + ") ");
			}
			System.out.println();
		}
	}
	
	/**
	 * Closes the underlying gamma compressed file.
	 */
	public void close() {
		file.close();
	}
}
