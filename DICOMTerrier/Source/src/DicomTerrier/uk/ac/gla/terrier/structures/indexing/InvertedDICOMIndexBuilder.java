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

import uk.ac.gla.terrier.structures.DirectDICOMIndexInputStream;
import uk.ac.gla.terrier.structures.Lexicon;
import uk.ac.gla.terrier.structures.LexiconInputStream;
import uk.ac.gla.terrier.structures.DocumentIndex;
import uk.ac.gla.terrier.structures.LexiconOutputStream;
import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * Builds an inverted index using block information. It is optional to 
 * save field information as well. 
 * @author Gerald van Veldhuijsen
 * @version Version 1.0
 */
public class InvertedDICOMIndexBuilder extends InvertedIndexBuilder {
	/**
	 * This method creates DICOM inverted index using the default
	 * name specified in the address_inv_file file. The approach used is described
	 * briefly: for a group of M terms from the lexicon we build the inverted file
	 * and save it on disk. In this way, the number of times we need to read the
	 * direct file is related to the parameter M, and consequently to the size
	 * of the available memory.
	 */
	public void createInvertedIndex() {
		try {
			
			System.err.println("creating DICOM inverted index");
			Lexicon lexicon = new Lexicon(ApplicationSetup.LEXICON_FILENAME);

			DocumentIndex docIndex = new DocumentIndex();
			long numberOfUniqueTerms = lexicon.getNumberOfLexiconEntries();
			int numberOfDocuments = docIndex.getNumberOfDocuments();
			long numberOfTokens = 0;
			long numberOfPointers = 0;
			lexicon.close();
			docIndex.close();
			LexiconInputStream lexiconStream = new LexiconInputStream();
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
			int reallyProcessed;
			for (int i = 0; i < numberOfUniqueTerms; i = i + reallyProcessed) {
				Runtime r = Runtime.getRuntime();
				System.out.println("Total free memory before " + r.freeMemory());
				System.gc();
				System.out.println("Total free memory after " + r.freeMemory());
				//set the number of terms to process from the lexicon
				if ((i + processTerms) > numberOfUniqueTerms)
					processTerms = (int) numberOfUniqueTerms - i;

				//start processing part of the lexicon
				startProcessingLexicon = System.currentTimeMillis();

				//preparing the data structures to store the data
				TIntArrayList[][] tmpStorage = new TIntArrayList[processTerms][];
				TIntIntHashMap codesHashMap = new TIntIntHashMap(processTerms);
				int numberOfPointersPerIteration = 0;
				reallyProcessed = processTerms;
				int j;
				for (j = 0; j < processTerms; j++) {
					lexiconStream.readNextEntry();
					TIntArrayList[] tmpArray = new TIntArrayList[5];
					tmpArray[0] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[1] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[2] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[3] = new TIntArrayList(lexiconStream.getNt());
					tmpArray[4] = new TIntArrayList(1);
					numberOfPointersPerIteration += lexiconStream.getNt();
										
					tmpStorage[j] = tmpArray;
					
					//the class TIntIntHashMap return zero when you look up for a
					//the value of a key that does not exist in the hash map.
					//For this reason, the values that will be inserted in the 
					//hash map are increased by one.
					codesHashMap.put(lexiconStream.getTermId(), j + 1);
					
					if(numberOfPointersPerIteration> totalDocFreqs)
						break;
				}
				
				if (j<processTerms)
					reallyProcessed = j+1;
								
				numberOfPointers += numberOfPointersPerIteration;
				endProcessingLexicon = System.currentTimeMillis();
				startTraversingDirectFile = System.currentTimeMillis();
				//scan the direct file
				DirectDICOMIndexInputStream directInputStream =
					new DirectDICOMIndexInputStream();
				int[][] documentTerms = null;
				int p = 0; //a document counter;
								
				while ((documentTerms = directInputStream.getNextTerms())
					!= null) {
					p += directInputStream.getDocumentsSkipped();
					//the two next vectors are used for reducing the number of references
					int[] documentTerms0 = documentTerms[0];
					int[] termfreqs = documentTerms[1];
					//int[] htmlscores = documentTerms[2];
					int[] tagfreqs = documentTerms[2];
					int[] tagids = documentTerms[3];
					
					//DEBUGGING
					/*System.out.println("DOC " + doc++ + " termids:");
					for(int m=0; m<documentTerms0.length; m++)
						System.out.print(documentTerms0[m] + " ");
					System.out.println();*/
					 
					//scan the list of the j-th document's terms
					int length = documentTerms0.length;
					int tagfreq;
					int tagidstart;
					int tagidend;
					for (int k = 0; k < length; k++) {
						//if the k-th term of the document is to be indexed in this pass
						
						int codePairIndex = codesHashMap.get(documentTerms0[k]);
													
						if (codePairIndex > 0) {
							codePairIndex--;//need to decrease it because it has been already increased while storing in codesHashMap

							TIntArrayList[] tmpMatrix = tmpStorage[codePairIndex];
																					
							tmpMatrix[0].add(p);
							tmpMatrix[1].add(termfreqs[k]);
							//tmpMatrix[2].add(htmlscores[k]);
							tagfreq = tagfreqs[k];
							tmpMatrix[3].add(tagfreq);

							//computing the offsets in the 
							//tmpMatrix[4] of the block ids 
							tagidstart = 0;
							if (k > 0) {
								for (int l = 0; l < k; l++) 
									tagidstart += tagfreqs[l];
							}
							tagidend = tagidstart + tagfreq; 

							for (int l = tagidstart; l < tagidend; l++) {
								tmpMatrix[4].add(tagids[l]);
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
				for (j = 0; j < reallyProcessed; j++) {
					
					frequency = 0; //the term frequency
					TIntArrayList[] tmpMatrix = tmpStorage[j];
					int[] tmpMatrix0 = tmpMatrix[0].toNativeArray();
					int[] tmpMatrix1 = tmpMatrix[1].toNativeArray();
					//int[] tmpMatrix2 = tmpMatrix[2].toNativeArray();
					int[] tmpMatrix3 = tmpMatrix[3].toNativeArray();
					int[] tmpMatrix4 = tmpMatrix[4].toNativeArray();
										
					//write the first entry
					int docid = tmpMatrix0[0];
					file.writeGamma(docid + 1);
					int termfreq = tmpMatrix1[0];
					frequency += termfreq;
					file.writeUnary(termfreq);
					
					//file.writeBinary(FieldScore.FIELDS_COUNT,tmpMatrix2[0]);
					int tagfreq = tmpMatrix3[0];
					file.writeUnary(tagfreq+1);
					int tagid;
					if (tagfreq>0){
						//Now write the sequence of tag identifiers
						tagid = tmpMatrix4[0];
						file.writeGamma(tagid + 1);
						for (int l = 1; l < tagfreq; l++) {
							file.writeGamma(tmpMatrix4[l] - tagid);
							tagid = tmpMatrix4[l];
					}
					}
					int tagindex = tagfreq;
					for (int k = 1; k < tmpMatrix0.length; k++) {
						file.writeGamma(tmpMatrix0[k] - docid);
						docid = tmpMatrix0[k];
						termfreq = tmpMatrix1[k];
						frequency += termfreq;
						file.writeUnary(termfreq);
						//file.writeBinary(FieldScore.FIELDS_COUNT,tmpMatrix2[k]);
						tagfreq = tmpMatrix3[k];
						file.writeUnary(tagfreq+1);
						if (tagfreq>0){
							tagid = tmpMatrix4[tagindex];
							file.writeGamma(tagid + 1);
							tagindex++;
							for (int l = 1; l < tagfreq; l++) {
								file.writeGamma(tmpMatrix4[tagindex] - tagid);
								tagid = tmpMatrix4[tagindex];
								tagindex++;
							}
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
			LexiconInputStream lis =
				new LexiconInputStream(ApplicationSetup.LEXICON_FILENAME);
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
									
			int i;
			long l;
			byte b;
									
			//the temporary data
			while (lis.readNextEntry() != -1) {
				i = dis.readInt();
				l = dis.readLong();
				b = dis.readByte();
				los.writeNextEntry(
						lis.getTermCharacters(),
						lis.getTermId(),
						lis.getNt(),
						//lis.getBlockFrequency(),
						i,
						//the term frequency
						l, //the ending byte offset
						b);
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
			System.out.println(
				"IOException occured during creating the inverted file. Stack trace follows.");
			System.out.println(ioe);
			System.exit(1);
		}
	}
	/**
	 * Creates an instance of the BlockInvertedIndex class. 
	 */
	public InvertedDICOMIndexBuilder() {
		super();
	}
	/**
	 * Creates an instance of the BlockInvertedIndex class 
	 * using the given filename.
	 * @param filename the name of the inverted file
	 */
	public InvertedDICOMIndexBuilder(String filename) {
		super(filename);
	}
}
