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
 * The Original Code is BlockInvertedIndex.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures;
import java.util.ArrayList;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * This class implements the block field inverted 
 * index for performing retrieval.
 * @author Douglas Johnson
 * @version $Revision: 1.1 $
 */
public class BlockInvertedIndex extends InvertedIndex {
	/**
	 * Creates an instance of the BlockInvertedIndex class 
	 * using the given lexicon.
	 * @param lexicon The lexicon used for retrieval
	 */
	public BlockInvertedIndex(Lexicon lexicon) {
		super(lexicon);
	}
	
	/**
	 * Creates an instance of the BlockInvertedIndex class 
	 * using the given lexicon.
	 * @param lexicon The lexicon used for retrieval
	 * @param filename the name of the inverted file
	 */
	public BlockInvertedIndex(Lexicon lexicon, String filename) {
		super(lexicon, filename);
	}
	/**
	 * Prints out the block inverted index file.
	 */
	public void print() {
		for (int i = 0; i < lexicon.getNumberOfLexiconEntries(); i++) {
			int[][] documents = getDocuments(i);
			int blockindex = 0;
			for (int j = 0; j < documents[0].length; j++) {
				System.out.print(
					"("
						+ documents[0][j]
						+ ", "
						+ documents[1][j]
						+ ", ");
				if (FieldScore.USE_FIELD_INFORMATION)
					System.out.print(documents[2][j] + ", ");
					System.out.print(documents[2][j]
						+ ", "
						+ documents[3][j]);
				for (int k = 0; k < documents[3][j]; k++) {
					System.out.print(", " + documents[4][blockindex]);
					blockindex++;
				}
				System.out.print(")");
			}
			System.out.println();
		}
	}
	/**
	 * Returns a five dimensional array containing the document ids, 
	 * the term frequencies, the field scores the block frequencies and 
	 * the block ids for the given documents. 
	 * @return int[][] the five dimensional [5][] array containing 
	 *         the document ids, frequencies, field scores and block 
	 *         frequencies, while the last vector contains the 
	 *         block identifiers and it has a different length from 
	 *         the document identifiers.
	 * @param termid the id of the term whose documents we are looking for.
	 */
	public int[][] getDocuments(int termid) {
		boolean found = lexicon.findTerm(termid);
		byte startBitOffset = lexicon.getStartBitOffset();
		long startOffset = lexicon.getStartOffset();
		byte endBitOffset = lexicon.getEndBitOffset();
		long endOffset = lexicon.getEndOffset();
		/* TODO use heuristics here like we do in InvertedIndex.java
		 * for setting a good guess of the arraylist sizes. */
		ArrayList temporaryTerms = new ArrayList();
		ArrayList temporaryBlockids = new ArrayList();
		int blockcount = 0;
		file.readReset(startOffset, startBitOffset, endOffset, endBitOffset);
		boolean hasMore = false;
		while (((file.getByteOffset() + startOffset) < endOffset)
			|| (((file.getByteOffset() + startOffset) == endOffset)
				&& (file.getBitOffset() < endBitOffset))) {
			System.out.println("Loading term iformation");
			int docId = file.readGamma();
			int[] tmp = new int[4];
			tmp[0] = docId;
			tmp[1] = file.readUnary();
			tmp[2] = file.readBinary(FieldScore.FIELDS_COUNT);
			System.out.println("Read " + tmp[2] + " from " + FieldScore.FIELDS_COUNT + " bits");
			int blockfreq = file.readUnary();
			tmp[3] = blockfreq;
			int[] tmp2 = new int[blockfreq];
			for (int i = 0; i < blockfreq; i++) {
				tmp2[i] += file.readGamma();
				blockcount++;
			}
			temporaryTerms.add(tmp);
			temporaryBlockids.add(tmp2);
		}
		int[][] documentTerms = new int[5][];
		documentTerms[0] = new int[temporaryTerms.size()];
		documentTerms[1] = new int[temporaryTerms.size()];
		documentTerms[2] = new int[temporaryTerms.size()];
		documentTerms[3] = new int[temporaryTerms.size()];
		documentTerms[4] = new int[blockcount];
		documentTerms[0][0] = ((int[]) temporaryTerms.get(0))[0] - 1;
		documentTerms[1][0] = ((int[]) temporaryTerms.get(0))[1];
		documentTerms[2][0] = ((int[]) temporaryTerms.get(0))[2];
		documentTerms[3][0] = ((int[]) temporaryTerms.get(0))[3];
		int[] blockids = ((int[]) temporaryBlockids.get(0));
		documentTerms[4][0] = blockids[0] - 1;
		for (int i = 1; i < blockids.length; i++) {
			documentTerms[4][i] = blockids[i] + documentTerms[4][i - 1];
		}
		int blockindex = blockids.length;
		if (documentTerms[0].length > 1) {
			for (int i = 1; i < documentTerms[0].length; i++) {
				int[] tmpMatrix = (int[]) temporaryTerms.get(i);
				documentTerms[0][i] = tmpMatrix[0] + documentTerms[0][i - 1];
				documentTerms[1][i] = tmpMatrix[1];
				documentTerms[2][i] = tmpMatrix[2];
				documentTerms[3][i] = tmpMatrix[3];
				blockids = ((int[]) temporaryBlockids.get(i));
				documentTerms[4][blockindex] = blockids[0] - 1;
				blockindex++;
				for (int j = 1; j < blockids.length; j++) {
					documentTerms[4][blockindex] =
						blockids[j] + documentTerms[4][blockindex - 1];
					blockindex++;
				}
			}
		}
		System.out.println("Fieldscores :" + documentTerms[2][0]);
		return documentTerms;
	}
}
