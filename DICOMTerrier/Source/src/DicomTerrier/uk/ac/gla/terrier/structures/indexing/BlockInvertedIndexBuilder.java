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
 * The Original Code is BlockInvertedIndexBuilder.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.indexing;
import gnu.trove.TIntArrayList;
import gnu.trove.TIntIntHashMap;

import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;

import uk.ac.gla.terrier.structures.BlockDirectIndexInputStream;
import uk.ac.gla.terrier.structures.BlockLexicon;
import uk.ac.gla.terrier.structures.BlockLexiconInputStream;
import uk.ac.gla.terrier.structures.DocumentIndex;
import uk.ac.gla.terrier.structures.LexiconOutputStream;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * Builds an inverted index using block information. It is optional to 
 * save field information as well. 
 * @author Douglas Johnson &amp; Vassilis Plachouras &amp; Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class BlockInvertedIndexBuilder extends InvertedIndexBuilder {
	/**
	 * This method creates the block html inverted index using the default
	 * name specified in the address_inv_file file. The approach used is described
	 * briefly: for a group of M terms from the lexicon we build the inverted file
	 * and save it on disk. In this way, the number of times we need to read the
	 * direct file is related to the parameter M, and consequently to the size
	 * of the available memory.
	 */
	public void createInvertedIndex() {
		try {
			
			System.err.println("creating block inverted index");
			BlockLexicon lexicon = new BlockLexicon();
			DocumentIndex docIndex = new DocumentIndex();
			long numberOfUniqueTerms = lexicon.getNumberOfLexiconEntries();
			int numberOfDocuments = docIndex.getNumberOfDocuments();
			long numberOfTokens = 0;
			long numberOfPointers = 0;
			lexicon.close();
			docIndex.close();
			BlockLexiconInputStream lexiconStream = new BlockLexiconInputStream();
			//A temporary file for storing the updated 
			//lexicon file, after creating the inverted file
			DataOutputStream dos =
				new DataOutputStream(
					new BufferedOutputStream(
						new FileOutputStream(
							ApplicationSetup.LEXICON_FILENAME.concat(
								".tmp2"))));
			//initialise the value of processTerms
			initialise();
			
			//if the set number of terms to process is higher than the available,
			if (processTerms > numberOfUniqueTerms)
				processTerms = (int) numberOfUniqueTerms;
			long startProcessingLexicon = 0;
			long endProcessingLexicon = 0;
			long startTraversingDirectFile = 0;
			long endTraversingDirectFile = 0;
			long startWritingInvertedFile = 0;
			long endWritingInvertedFile = 0;
			for (int i = 0; i < numberOfUniqueTerms; i = i + processTerms) {
				//set the number of terms to process from the lexicon
				if ((i + processTerms) > numberOfUniqueTerms)
					processTerms = (int) numberOfUniqueTerms - i;

				//start processing part of the lexicon
				startProcessingLexicon = System.currentTimeMillis();

				//preparing the data structures to store the data
				TIntArrayList[][] tmpStorage = new TIntArrayList[processTerms][];
				TIntIntHashMap codesHashMap = new TIntIntHashMap(processTerms);
				int numberOfPointersPerIteration = 0;
				for (int j = 0; j < processTerms; j++) {
					lexiconStream.readNextEntry();
					TIntArrayList[] tmpArray = new TIntArrayList[5];
					tmpArray[0] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[1] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[2] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[3] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[4] = new TIntArrayList(lexiconStream.getBlockFrequency());
					numberOfPointersPerIteration += lexiconStream.getNt();
					tmpStorage[j] = tmpArray;
					
					//the class TIntIntHashMap return zero when you look up for a
					//the value of a key that does not exist in the hash map.
					//For this reason, the values that will be inserted in the 
					//hash map are increased by one. 
					codesHashMap.put(lexiconStream.getTermId(), j + 1);
				}
				numberOfPointers += numberOfPointersPerIteration;
				endProcessingLexicon = System.currentTimeMillis();
				startTraversingDirectFile = System.currentTimeMillis();
				//scan the direct file
				BlockDirectIndexInputStream directInputStream =
					new BlockDirectIndexInputStream();
				int[][] documentTerms = null;
				int p = 0; //a document counter;
				while ((documentTerms = directInputStream.getNextTerms())
					!= null) {
					p += directInputStream.getDocumentsSkipped();
					//the two next vectors are used for reducing the number of references
					int[] documentTerms0 = documentTerms[0];
					int[] termfreqs = documentTerms[1];
					int[] htmlscores = documentTerms[2];
					int[] blockfreqs = documentTerms[3];
					int[] blockids = documentTerms[4];

					//scan the list of the j-th document's terms
					int length = documentTerms0.length;
					int blockfreq;
					int blockidstart;
					int blockidend;
					for (int k = 0; k < length; k++) {
						//if the k-th term of the document is to be indexed in this pass
						int codePairIndex = codesHashMap.get(documentTerms0[k]);

						if (codePairIndex > 0) {
							codePairIndex--;//need to decrease it because it has been already increased while storing in codesHashMap
							TIntArrayList[] tmpMatrix = tmpStorage[codePairIndex];

							tmpMatrix[0].add(p);
							tmpMatrix[1].add(termfreqs[k]);
							tmpMatrix[2].add(htmlscores[k]);
							blockfreq = blockfreqs[k];
							tmpMatrix[3].add(blockfreq);
							

							//computing the offsets in the 
							//tmpMatrix[4] of the block ids 
							blockidstart = 0;
							if (k > 0) {
								for (int l = 0; l < k; l++) 
									blockidstart += blockfreqs[l];
							}
							blockidend = blockidstart + blockfreq; 

							for (int l = blockidstart; l < blockidend; l++) {
								tmpMatrix[4].add(blockids[l]);
							}
						}
					}
					p++;
				}
				directInputStream.close();
				endTraversingDirectFile = System.currentTimeMillis();
				startWritingInvertedFile = System.currentTimeMillis();
				//write to the inverted file. We should note that the lexicon 
				//file should be updated as well with the term frequency and
				//the endOffset and endBitOffset.
				file.writeReset();
				int frequency;
				for (int j = 0; j < processTerms; j++) {
					frequency = 0; //the term frequency
					TIntArrayList[] tmpMatrix = tmpStorage[j];
					int[] tmpMatrix0 = tmpMatrix[0].toNativeArray();
					int[] tmpMatrix1 = tmpMatrix[1].toNativeArray();
					int[] tmpMatrix2 = tmpMatrix[2].toNativeArray();
					int[] tmpMatrix3 = tmpMatrix[3].toNativeArray();
					int[] tmpMatrix4 = tmpMatrix[4].toNativeArray();
					//write the first entry
					int docid = tmpMatrix0[0];
					file.writeGamma(docid + 1);
					int termfreq = tmpMatrix1[0];
					frequency += termfreq;
					file.writeUnary(termfreq);
					System.out.println("Writing fieldscore " + tmpMatrix2[0] + " in " + FieldScore.FIELDS_COUNT + " bits ");
					file.writeBinary(FieldScore.FIELDS_COUNT,
						tmpMatrix2[0]);
					int blockfreq = tmpMatrix3[0];
					file.writeUnary(blockfreq);
					int blockid = tmpMatrix4[0];
					file.writeGamma(blockid + 1);
					for (int l = 1; l < blockfreq; l++) {
						file.writeGamma(tmpMatrix4[l] - blockid);
						blockid = tmpMatrix4[l];
					}
					int blockindex = blockfreq;
					for (int k = 1; k < tmpMatrix0.length; k++) {
						file.writeGamma(tmpMatrix0[k] - docid);
						docid = tmpMatrix0[k];
						termfreq = tmpMatrix1[k];
						frequency += termfreq;
						file.writeUnary(termfreq);
						file.writeBinary(FieldScore.FIELDS_COUNT, 
							tmpMatrix2[k]);
						blockfreq = tmpMatrix3[k];
						file.writeUnary(blockfreq);
						blockid = tmpMatrix4[blockindex];
						file.writeGamma(blockid + 1);
						blockindex++;
						for (int l = 1; l < blockfreq; l++) {
							file.writeGamma(tmpMatrix4[blockindex] - blockid);
							blockid = tmpMatrix4[blockindex];
							blockindex++;
						}
					}
					long endOffset = file.getByteOffset();
					byte endBitOffset = file.getBitOffset();
					endBitOffset--;
					if (endBitOffset < 0 && endOffset > 0) {
						endBitOffset = 7;
						endOffset--;
					}
					numberOfTokens += frequency;
					dos.writeInt(frequency);
					dos.writeLong(endOffset);
					dos.writeByte(endBitOffset);
				}
				file.writeFlush();
				endWritingInvertedFile = System.currentTimeMillis();
				System.err.println(
					"time to process part of lexicon: "
						+ ((endProcessingLexicon - startProcessingLexicon)
							/ 1000D));
				System.err.println(
					"time to traverse direct file: "
						+ ((endTraversingDirectFile - startTraversingDirectFile)
							/ 1000D));
				System.err.println(
					"time to write inverted file: "
						+ ((endWritingInvertedFile - startWritingInvertedFile)
							/ 1000D));
				System.err.println(
					"time to perform one iteration: "
						+ ((endWritingInvertedFile - startProcessingLexicon)
							/ 1000D));
				System.err.println(
					"number of pointers processed: "
						+ numberOfPointersPerIteration);
			}

			this.numberOfDocuments = numberOfDocuments;
			this.numberOfUniqueTerms = numberOfUniqueTerms;
			this.numberOfTokens = numberOfTokens;
			this.numberOfPointers = numberOfPointers;

			lexiconStream.close();
			dos.close();
			//finalising the lexicon file with the updated information
			//on the frequencies and the offsets
			BlockLexiconInputStream lis =
				new BlockLexiconInputStream(ApplicationSetup.LEXICON_FILENAME);
			//reading the original lexicon
			LexiconOutputStream los =
				new LexiconOutputStream(
					ApplicationSetup.LEXICON_FILENAME.concat(".tmp3"));
			//the updated lexicon
			DataInputStream dis =
				new DataInputStream(
					new BufferedInputStream(
						new FileInputStream(
							ApplicationSetup.LEXICON_FILENAME.concat(
								".tmp2"))));
			//the temporary data
			while (lis.readNextEntry() != -1) {
				los
					.writeNextEntry(
						lis.getTermCharacters(),
						lis.getTermId(),
						lis.getNt(),
						//lis.getBlockFrequency(),
						dis.readInt(),
				//the term frequency
				dis.readLong(), //the ending byte offset
				dis.readByte());
			}
			lis.close();
			los.close();
			dis.close();
			if (!(new File(ApplicationSetup.LEXICON_FILENAME)).delete()) {
				System.err.println("delete file .lex failed!");
			}
			if (!(new File(ApplicationSetup.LEXICON_FILENAME.concat(".tmp2"))).delete()) {
				System.err.println("delete file .lex.tmp2 failed!");
			}
			if (!(new File(ApplicationSetup.LEXICON_FILENAME.concat(".tmp3"))).renameTo(new File(ApplicationSetup.LEXICON_FILENAME))) {
				System.err.println("rename file .lex.tmp3 to .lex failed!");
			}
		} catch (IOException ioe) {
			System.err.println(
				"IOException occured during creating the inverted file. Stack trace follows.");
			System.err.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * Creates an instance of the BlockInvertedIndex class. 
	 */
	public BlockInvertedIndexBuilder() {
		super();
	}
	/**
	 * Creates an instance of the BlockInvertedIndex class 
	 * using the given filename.
	 * @param filename the name of the inverted file
	 */
	public BlockInvertedIndexBuilder(String filename) {
		super(filename);
	}
}
