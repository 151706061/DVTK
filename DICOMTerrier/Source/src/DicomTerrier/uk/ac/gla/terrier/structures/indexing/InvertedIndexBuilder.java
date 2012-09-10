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
 * The Original Code is InvertedIndexBuilder.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.indexing;
import gnu.trove.TIntIntHashMap;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import uk.ac.gla.terrier.compression.BitFile;
import uk.ac.gla.terrier.structures.DirectIndexInputStream;
import uk.ac.gla.terrier.structures.DocumentIndex;
import uk.ac.gla.terrier.structures.Lexicon;
import uk.ac.gla.terrier.structures.LexiconInputStream;
import uk.ac.gla.terrier.structures.LexiconOutputStream;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * Builds an inverted index, using field information optionally.
 * @author Craig Macdonald &amp; Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class InvertedIndexBuilder {
	
	/** The number of unique terms in the vocabulary.*/
	public long numberOfUniqueTerms;
	
	/** The number of documents in the collection.*/
	public int numberOfDocuments;
	
	/** The number of tokens in the collection.*/
	public long numberOfTokens = 0;
	
	/** The number of pointers in the inverted file.*/
	public long numberOfPointers = 0;

	/** Indicates whether field information is used. */
	final boolean useFieldInformation = FieldScore.USE_FIELD_INFORMATION;
	/**
	 * The underlying bit file.
	 */
	protected BitFile file;

	/**
	 * A default constructor of the class InvertedIndex.
	 */
	public InvertedIndexBuilder() {
		file = new BitFile(ApplicationSetup.INVERTED_FILENAME, "rw");
	}
	/**
	 * Creates an instance of the InvertedIndex
	 * class using the given filename.
	 * @param filename The name of the inverted file
	 */
	public InvertedIndexBuilder(String filename) {
		file = new BitFile(filename, "rw");
	}

	/**
	 * Closes the underlying bit file.
	 */
	public void close() {
		file.close();
	}

	/**
	 * Creates the inverted index using the already created direct index,
	 * document index and lexicon.
	 */
	public void createInvertedIndex() {
		try {
			Lexicon lexicon = new Lexicon(ApplicationSetup.LEXICON_FILENAME);
			DocumentIndex docIndex = new DocumentIndex();
			long numberOfUniqueTerms = lexicon.getNumberOfLexiconEntries();
			int numberOfDocuments = docIndex.getNumberOfDocuments();
			long numberOfTokens = 0;
			long numberOfPointers = 0;
			lexicon.close();
			docIndex.close();
			LexiconInputStream lexiconStream = new LexiconInputStream();
			final int fieldsCount = FieldScore.FIELDS_COUNT;
			//A temporary file for storing the updated lexicon file, after
			// creating the inverted file
			DataOutputStream dos = new DataOutputStream(
					new BufferedOutputStream(new FileOutputStream(
							ApplicationSetup.LEXICON_FILENAME.concat(".tmp2"))));
			//initialise the value of processTerms
			initialise();

			//if the set number of terms to process is higher than the
			// available,
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
				int[] indices = new int[processTerms];
				int[][][] tmpStorage = new int[processTerms][][];
				TIntIntHashMap codesHashMap = new TIntIntHashMap(processTerms);
				int numberOfPointersPerIteration = 0;

				int numOfFields = 2;
				if (useFieldInformation)
					numOfFields = 3;

				for (int j = 0; j < processTerms; j++) {
					lexiconStream.readNextEntry();
					//int[][] tmpArray = new int[numOfFields][lexiconStream.getNt()];
					numberOfPointersPerIteration += lexiconStream.getNt();
					//tmpStorage.add(tmpArray);
					tmpStorage[j] = new int[numOfFields][lexiconStream.getNt()];
					//the class TIntIntHashMap return zero when you look up for
					// a the value of a key that does not exist in the hash map.
					//For this reason, the values that will be inserted in the
					//hash map are increased by one.
					codesHashMap.put(lexiconStream.getTermId(), j + 1);
				}
				numberOfPointers += numberOfPointersPerIteration;
				endProcessingLexicon = System.currentTimeMillis();
				startTraversingDirectFile = System.currentTimeMillis();
				//scan the direct file
				//uses indices, tmpStorage and codesHashMap
				traverseDirectFile(tmpStorage, indices, codesHashMap);
				//end of traversing the
				endTraversingDirectFile = System.currentTimeMillis();
				startWritingInvertedFile = System.currentTimeMillis();
				//write to the inverted file. We should note that the lexicon
				//file should be updated as well with the term frequency and
				//the endOffset and endBitOffset.
				file.writeReset();
				int frequency;
				int[][] tmpMatrix = null;
				int[] tmpMatrix0 = null;
				int[] tmpMatrix1 = null;

				for (int j = 0; j < processTerms; j++) {
					frequency = 0; //the term frequency
					//tmpMatrix = (int[][]) tmpStorage.elementAt(j);
					tmpMatrix = tmpStorage[j];
					tmpMatrix0 = tmpMatrix[0];
					tmpMatrix1 = tmpMatrix[1];

					//we do not need to sort because the documents are read in
					//order of docid, and therefore the arrays are already
					// sorted.
					if (useFieldInformation) {
						int[] tmpMatrix2 = tmpMatrix[2];
						//write the first entry
						file.writeGamma(tmpMatrix0[0] + 1);
						frequency += tmpMatrix1[0];
						file.writeUnary(tmpMatrix1[0]);
						file.writeBinary(fieldsCount, tmpMatrix2[0]);
						final int tmpMatrix0Length = tmpMatrix0.length;
						for (int k = 1; k < tmpMatrix0Length; k++) {
							file.writeGamma(tmpMatrix0[k] - tmpMatrix0[k - 1]);
							frequency += tmpMatrix1[k];
							file.writeUnary(tmpMatrix1[k]);
							file.writeBinary(fieldsCount, tmpMatrix2[k]);
						}
					} else {
						//write the first entry
						file.writeGamma(tmpMatrix0[0] + 1);
						frequency += tmpMatrix1[0];
						file.writeUnary(tmpMatrix1[0]);
						final int tmpMatrix0Length = tmpMatrix0.length;
						for (int k = 1; k < tmpMatrix0Length; k++) {
							file.writeGamma(tmpMatrix0[k] - tmpMatrix0[k - 1]);
							frequency += tmpMatrix1[k];
							file.writeUnary(tmpMatrix1[k]);
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

				System.err.println("time to process part of lexicon: "
					+ ((endProcessingLexicon - startProcessingLexicon) / 1000D));
				System.err.println("time to traverse direct file: "
					+ ((endTraversingDirectFile - startTraversingDirectFile) / 1000D));
				System.err.println("time to write inverted file: "
					+ ((endWritingInvertedFile - startWritingInvertedFile) / 1000D));
				System.err.println("time to perform one iteration: "
					+ ((endWritingInvertedFile - startProcessingLexicon) / 1000D));
				System.err.println("number of pointers processed: "
					+ numberOfPointersPerIteration);
			}

			/* CollectionStatistics.createCollectionStatistics(numberOfDocuments,
					numberOfTokens, numberOfUniqueTerms, numberOfPointers);*/
			this.numberOfDocuments = numberOfDocuments;
			this.numberOfTokens = numberOfTokens;
			this.numberOfUniqueTerms = numberOfUniqueTerms;
			this.numberOfPointers = numberOfPointers;

			lexiconStream.close();
			dos.close();
			//finalising the lexicon file with the updated information
			//on the frequencies and the offsets
			LexiconInputStream lis = new LexiconInputStream(
					ApplicationSetup.LEXICON_FILENAME);
			//reading the original lexicon
			LexiconOutputStream los = new LexiconOutputStream(
					ApplicationSetup.LEXICON_FILENAME.concat(".tmp3"));
			//the updated lexicon
			DataInputStream dis = new DataInputStream(new BufferedInputStream(
					new FileInputStream(ApplicationSetup.LEXICON_FILENAME
							.concat(".tmp2"))));
			//the temporary data
			while (lis.readNextEntry() != -1) {
				los.writeNextEntry(lis.getTermCharacters(), lis.getTermId(),
						lis.getNt(), dis.readInt(),
						//the term frequency
						dis.readLong(), //the ending byte offset
						dis.readByte());
			}
			lis.close();
			los.close();
			dis.close();
			(new File(ApplicationSetup.LEXICON_FILENAME)).delete();
			(new File(ApplicationSetup.LEXICON_FILENAME.concat(".tmp2")))
					.delete();
			(new File(ApplicationSetup.LEXICON_FILENAME.concat(".tmp3")))
					.renameTo(new File(ApplicationSetup.LEXICON_FILENAME));
		} catch (IOException ioe) {
			System.err
					.println("IOException occured during creating the inverted file. Stack trace follows.");
			System.err.println(ioe);
			ioe.printStackTrace();
			System.exit(1);
		}
	}

	/**
	 * Traverses the direct index and creates the inverted index entries 
	 * for the terms specified in the codesHashMap and tmpStorage.
	 * @param tmpStorage int[][][] an array of the inverted index entries to store
	 * @param indices int[] an array of values that indicate how many occurrences
	 *        of a particular term have been found so far. 
	 * @param codesHashMap a mapping from the term identifiers to the index 
	 *        in the tmpStorage matrix. 
	 * @throws IOException if there is a problem while traversing the direct index.
	 */
	private void traverseDirectFile(int[][][] tmpStorage, int[] indices, TIntIntHashMap codesHashMap) throws IOException {
		DirectIndexInputStream directInputStream = new DirectIndexInputStream();
		int[][] documentTerms = null;
		int[] documentTerms0 = null;
		int[] documentTerms1 = null;
		int[] documentTerms2 = null;
		int[][] tmpMatrix = null;
		int codePairIndex;
		int p = 0; //a counter;
		int tmp_indices_codePairIndex; //a temporary variable
		while ((documentTerms = directInputStream.getNextTerms()) != null) {
			p += directInputStream.getDocumentsSkipped();
			//the two next vectors are used for reducing the number of
			// references
			documentTerms0 = documentTerms[0];
			documentTerms1 = documentTerms[1];
			//scan the list of the j-th document's terms
			final int length = documentTerms0.length;
			if (useFieldInformation) { //if we are processing tag information
				documentTerms2 = documentTerms[2];
				
				for (int k = 0; k < length; k++) {
					//if the k-th term of the document is to be indexed in
					// this pass
					
					if ((codePairIndex = codesHashMap.get(documentTerms0[k])) > 0) {
						codePairIndex--; //need to decrease it because it has been increased while storing to the codesHashMap
						tmpMatrix = tmpStorage[codePairIndex];
						tmp_indices_codePairIndex = indices[codePairIndex];
						tmpMatrix[0][tmp_indices_codePairIndex] = p;
						tmpMatrix[1][tmp_indices_codePairIndex] = documentTerms1[k];
						tmpMatrix[2][tmp_indices_codePairIndex] = documentTerms2[k];
						indices[codePairIndex]++;
					}
				}						
			} else { //if we are not processing tag information
				for (int k = 0; k < length; k++) {
					//if the k-th term of the document is to be indexed in
					// this pass
					if ((codePairIndex = codesHashMap.get(documentTerms0[k])) > 0) {
						codePairIndex--;
						//tmpMatrix = (int[][]) tmpStorage.elementAt(codePairIndex);
						//tmpMatrix = tmpStorage[codePairIndex];
						tmp_indices_codePairIndex = indices[codePairIndex];
						//tmpMatrix[0][tmp_indices_codePairIndex] = p;
						//tmpMatrix[1][tmp_indices_codePairIndex] = documentTerms1[k];
						indices[codePairIndex]++;
					}
				}
			}
			p++;
		}
		directInputStream.close();
	}
	
	/**
	 * The number of terms for which the inverted file 
	 * is built each time. The corresponding property
	 * is <tt>invertedfile.processterms</tt> and the 
	 * default value is <tt>75000</tt>. The higher the
	 * value, the greater the requirements for memory are, 
	 * but the less time it takes to invert the direct 
	 * file. 
	 */
	protected static int processTerms;
	
	/**
	 * The number of total document frequence for which the inverted file 
	 * is built each time. The corresponding property
	 * is <tt>invertedfile.docfrequency</tt> and the 
	 * default value is <tt>500000</tt>. The higher the
	 * value, the greater the requirements for memory are, 
	 * but the less time it takes to invert the direct 
	 * file. 
	 */
	protected static int totalDocFreqs;
	
	static {
		initialise();
	}
	
	/**
	 * Reads from the properties the value of the property
	 * <tt>invertedfile.processterms</tt>.
	 */
	protected static void initialise() {
		processTerms = Integer.parseInt(
				ApplicationSetup.getProperty("invertedfile.processterms", "75000")
			);
		totalDocFreqs = Integer.parseInt(
				ApplicationSetup.getProperty("invertedfile.docfrequency", "350000")
			);
		
	}
}
