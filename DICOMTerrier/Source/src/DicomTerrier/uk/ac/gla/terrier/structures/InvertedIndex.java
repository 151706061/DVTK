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
 * The Original Code is InvertedIndex.java.
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
import gnu.trove.TIntHashSet;

import java.util.ArrayList;
import uk.ac.gla.terrier.compression.BitFile;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * This class implements the inverted index 
 * for performing retrieval, with field information
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class InvertedIndex {
	/** This is used during retrieval for a rough guess sizing of the temporaryTerms
	  * arraylist in getDocuments(). The higher this value, the less chance that the
	  * arraylist will have to be grown (growing is expensive), however more memory
	  * may be used unnecessarily. */
	public static final double NORMAL_LOAD_FACTOR = 1.0;
	/** This is used during retrieval for a rough guess sizing of the temporaryTerms
	  * arraylist in getDocuments() - retrieval with Fields. The higher this value, 
	  * the less chance that the arraylist will have to be grown (growing is expensive), 
	  * however more memory may be used unnecessarily. */
	public static final double FIELD_LOAD_FACTOR = 1.0;
	/** Indicates whether field information is used.*/
	final boolean useFieldInformation = FieldScore.USE_FIELD_INFORMATION;
	
	/**
	 * Creates an instance of the HtmlInvertedIndex class using the lexicon.
	 * @param lexicon The lexicon used for retrieval
	 */
	public InvertedIndex(Lexicon lexicon) {
		file = new BitFile(ApplicationSetup.INVERTED_FILENAME, "r");
		this.lexicon = lexicon;
	}
	/**
	 * Creates an instance of the HtmlInvertedIndex class using the given
	 * lexicon.
	 * @param lexicon The lexicon used for retrieval
	 * @param filename The name of the inverted file
	 */
	public InvertedIndex(Lexicon lexicon, String filename) {
		file = new BitFile(filename, "r");
		this.lexicon = lexicon;
	}
	/**
	 * Prints out the inverted index file.
	 */
	public void print() {
		for (int i = 0; i < lexicon.getNumberOfLexiconEntries(); i++) {
			int[][] documents = getDocuments(i);
			if (useFieldInformation) {
				for (int j = 0; j < documents[0].length; j++) {
					System.out.print("(" + documents[0][j] + ", " + documents[1][j]
							+ ", " + documents[2][j] + ") ");
				}
				System.out.println();				
			} else {
				for (int j = 0; j < documents[0].length; j++) {
					System.out.print("(" + documents[0][j] + ", " 
							             + documents[1][j] + ") ");
				}
				System.out.println();
			}
		}
	}
	/**
	 * Returns a two dimensional array containing the document ids, term
	 * frequencies and field scores for the given documents. 	  
	 * @return int[][] the two dimensional [3][n] array containing the n 
	 *         document identifiers, frequencies and field scores.
	 * @param termid the identifier of the term whose documents we are looking for.
	 */
	public int[][] getDocuments(int termid) {
		/* Coding is done separately for with Fields and without Fields, to keep
		 * if's out of loops. */
		boolean found = lexicon.findTerm(termid);
		if (!found) 
			return null;
		
		byte startBitOffset = lexicon.getStartBitOffset();
		long startOffset = lexicon.getStartOffset();
		byte endBitOffset = lexicon.getEndBitOffset();
		long endOffset = lexicon.getEndOffset();
		final int fieldCount = FieldScore.FIELDS_COUNT;
		final boolean loadTagInformation = FieldScore.USE_FIELD_INFORMATION;
		
		ArrayList temporaryTerms = null; //instantiate when we know roughly how big it should be
		int[][] documentTerms = null;
		file.readReset(startOffset, startBitOffset, endOffset, endBitOffset);
		
		if (loadTagInformation) { //if there are tag information to process
			/* FIELD_LOAD_FACTOR provides a heuristical rough size need for the arraylist. */
			/* could probably do a better optimisation by considering the number of fields.*/
			temporaryTerms = new ArrayList((int)((endOffset-startOffset)*FIELD_LOAD_FACTOR));
			while (((file.getByteOffset() + startOffset) < endOffset)
					|| (((file.getByteOffset() + startOffset) == endOffset) && (file
							.getBitOffset() < endBitOffset))) {
				int[] tmp = new int[3];
				//read documnent ID
				tmp[0] = file.readGamma();
				//read document frequency
				tmp[1] = file.readUnary();
				//read fields bitset (fieldCount bits long) 
				tmp[2] = file.readBinary(fieldCount);
				
				temporaryTerms.add(tmp);
			}
			documentTerms = new int[3][temporaryTerms.size()];
			int[] tmpDocumentTerms0 = documentTerms[0];
			int[] tmpDocumentTerms1 = documentTerms[1];
			int[] tmpDocumentTerms2 = documentTerms[2];
			tmpDocumentTerms0[0] = ((int[]) temporaryTerms.get(0))[0] - 1;
			tmpDocumentTerms1[0] = ((int[]) temporaryTerms.get(0))[1];
			tmpDocumentTerms2[0] = ((int[]) temporaryTerms.get(0))[2];
			if (documentTerms[0].length > 1) {
				for (int i = 1; i < documentTerms[0].length; i++) {
					int[] tmpMatrix = (int[]) temporaryTerms.get(i);
					tmpDocumentTerms0[i] = tmpMatrix[0] + documentTerms[0][i - 1];
					tmpDocumentTerms1[i] = tmpMatrix[1];
					tmpDocumentTerms2[i] = tmpMatrix[2];
				}
			}			
		} else { //no tag information to process
			
			/* NORMAL_LOAD_FACTOR provides a heuristical rough size need for the arraylist */
			temporaryTerms = new ArrayList((int)((endOffset-startOffset)*NORMAL_LOAD_FACTOR));
			while (((file.getByteOffset() + startOffset) < endOffset)
					|| (((file.getByteOffset() + startOffset) == endOffset) && (file
							.getBitOffset() < endBitOffset))) {
				int[] tmp = new int[2];
				//read document ID
				tmp[0] = file.readGamma();
				//read document frequency
				tmp[1] = file.readUnary();
				temporaryTerms.add(tmp);
			}
			documentTerms = new int[2][temporaryTerms.size()];
			int[] tmpDocumentTerms0 = documentTerms[0];
			int[] tmpDocumentTerms1 = documentTerms[1];
			tmpDocumentTerms0[0] = ((int[]) temporaryTerms.get(0))[0] - 1;
			tmpDocumentTerms1[0] = ((int[]) temporaryTerms.get(0))[1];
			if (documentTerms[0].length > 1) {
				for (int i = 1; i < documentTerms[0].length; i++) {
					int[] tmpMatrix = (int[]) temporaryTerms.get(i);
					tmpDocumentTerms0[i] = tmpMatrix[0] + documentTerms[0][i - 1];
					tmpDocumentTerms1[i] = tmpMatrix[1];
				}
			}			
		}
		return documentTerms;
	}
	
	
	/**
	 * Returns a two dimensional array containing the document ids, term
	 * frequencies and field scores for the given documents. 	  
	 * @return int[][] the two dimensional [3][n] array containing the n 
	 *         document identifiers, frequencies and field scores.
	 * @param termid the identifier of the term whose documents we are looking for.
	 */
	public int[][] getDocumentsFast(int termid, byte startBit, long startByte, byte endBit, long endByte) {
		/* Coding is done separately for with Fields and without Fields, to keep
		 * if's out of loops. */
		boolean found = lexicon.findTerm(termid);
		if (!found) 
			return null;
		
		byte startBitOffset = startBit;
		long startOffset = startByte;
		byte endBitOffset = endBit;
		long endOffset = endByte;
		final int fieldCount = FieldScore.FIELDS_COUNT;
		final boolean loadTagInformation = FieldScore.USE_FIELD_INFORMATION;
		
		ArrayList temporaryTerms = null; //instantiate when we know roughly how big it should be
		int[][] documentTerms = null;
		file.readReset(startOffset, startBitOffset, endOffset, endBitOffset);

		
		if (loadTagInformation) { //if there are tag information to process
			/* FIELD_LOAD_FACTOR provides a heuristical rough size need for the arraylist. */
			/* could probably do a better optimisation by considering the number of fields.*/
			temporaryTerms = new ArrayList((int)((endOffset-startOffset)*FIELD_LOAD_FACTOR));
			while (((file.getByteOffset() + startOffset) < endOffset)
					|| (((file.getByteOffset() + startOffset) == endOffset) && (file
							.getBitOffset() < endBitOffset))) {
				int[] tmp = new int[3];
				//read documnent ID
				tmp[0] = file.readGamma();
				//read document frequency
				tmp[1] = file.readUnary();
				//read fields bitset (fieldCount bits long) 
				tmp[2] = file.readBinary(fieldCount);
				
				temporaryTerms.add(tmp);
			}
			documentTerms = new int[3][temporaryTerms.size()];
			int[] tmpDocumentTerms0 = documentTerms[0];
			int[] tmpDocumentTerms1 = documentTerms[1];
			int[] tmpDocumentTerms2 = documentTerms[2];
			tmpDocumentTerms0[0] = ((int[]) temporaryTerms.get(0))[0] - 1;
			tmpDocumentTerms1[0] = ((int[]) temporaryTerms.get(0))[1];
			tmpDocumentTerms2[0] = ((int[]) temporaryTerms.get(0))[2];
			if (documentTerms[0].length > 1) {
				for (int i = 1; i < documentTerms[0].length; i++) {
					int[] tmpMatrix = (int[]) temporaryTerms.get(i);
					tmpDocumentTerms0[i] = tmpMatrix[0] + documentTerms[0][i - 1];
					tmpDocumentTerms1[i] = tmpMatrix[1];
					tmpDocumentTerms2[i] = tmpMatrix[2];
				}
			}			
		} else { //no tag information to process
			
			/* NORMAL_LOAD_FACTOR provides a heuristical rough size need for the arraylist */
			temporaryTerms = new ArrayList((int)((endOffset-startOffset)*NORMAL_LOAD_FACTOR));
			while (((file.getByteOffset() + startOffset) < endOffset)
					|| (((file.getByteOffset() + startOffset) == endOffset) && (file
							.getBitOffset() < endBitOffset))) {
				int[] tmp = new int[2];
				//read document ID
				tmp[0] = file.readGamma();
				//read document frequency
				tmp[1] = file.readUnary();
				temporaryTerms.add(tmp);
			}
			documentTerms = new int[2][temporaryTerms.size()];
			int[] tmpDocumentTerms0 = documentTerms[0];
			int[] tmpDocumentTerms1 = documentTerms[1];
			tmpDocumentTerms0[0] = ((int[]) temporaryTerms.get(0))[0] - 1;
			tmpDocumentTerms1[0] = ((int[]) temporaryTerms.get(0))[1];
			if (documentTerms[0].length > 1) {
				for (int i = 1; i < documentTerms[0].length; i++) {
					int[] tmpMatrix = (int[]) temporaryTerms.get(i);
					tmpDocumentTerms0[i] = tmpMatrix[0] + documentTerms[0][i - 1];
					tmpDocumentTerms1[i] = tmpMatrix[1];
				}
			}			
		}
		return documentTerms;
	}
	
	/**
	 * Returns a two dimensional array containing the document ids, term
	 * frequencies and field scores for the given documents. 
	 * The offsets are supplied in the arguments so these don't need to be read from the lexicon	  
	 * @return int[][] the two dimensional [3][n] array containing the n 
	 *         document identifiers, frequencies and field scores.
	 * @param termid the identifier of the term whose documents we are looking for.
	 */
	public int[][] getDocumentsFastWithTagId(int termid, byte startBit, long startByte, byte endBit, long endByte, int tagId) {
		/* Coding is done separately for with Fields and without Fields, to keep
		 * if's out of loops. */
		
		byte startBitOffset = startBit;
		long startOffset = startByte;
		byte endBitOffset = endBit;
		long endOffset = endByte;
		
		/*
		System.out.println("Reading new entry from Inverted File for term id: " + termid);
		System.out.println("Offsets: " + startBitOffset + "," + startOffset + "," + endBitOffset + "," + endOffset);
		*/
		
		ArrayList temporaryTerms = null; //instantiate when we know roughly how big it should be
		ArrayList temporaryTagids = null;
		int tagCount=0;
		int[][] documentTerms = null;
		file.readReset(startOffset, startBitOffset, endOffset, endBitOffset);

		
		/* FIELD_LOAD_FACTOR provides a heuristical rough size need for the arraylist. */
		/* could probably do a better optimisation by considering the number of fields.*/
		temporaryTerms = new ArrayList((int)((endOffset-startOffset)*FIELD_LOAD_FACTOR));
		temporaryTagids = new ArrayList((int)((endOffset-startOffset)*FIELD_LOAD_FACTOR));;
		while (((file.getByteOffset() + startOffset) < endOffset)
				|| (((file.getByteOffset() + startOffset) == endOffset) && (file
						.getBitOffset() < endBitOffset))) {
			int[] tmp = new int[3];
			//read documnent ID
			tmp[0] = file.readGamma();
			//read document frequency
			tmp[1] = file.readUnary();
			//read number of fields 
			int tagfreq = file.readUnary()-1;
			//System.out.println("Read tagfrequency unary " + tagfreq);
			tmp[2] = tagfreq;
			
			TIntHashSet tagIds = new TIntHashSet(tagfreq);
			
			int[] tmp2 = new int[tagfreq];
			
			if (tagfreq>0){
				tmp2[0] = file.readGamma();
				tagCount++;
				tagIds.add(tmp2[0]);
			}
			for (int i = 1; i < tagfreq; i++) {
				tmp2[i] = file.readGamma();
				//System.out.println("Read tagcode gamma " + tmp2[i]);
				tagCount++;
				tagIds.add(tmp2[i]);
			}
			
			if(tagIds.contains(tagId)){
				temporaryTagids.add(tmp2);
				temporaryTerms.add(tmp);
			}
			
		}
		documentTerms = new int[4][];
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
	 * The underlying bit file.
	 */
	protected BitFile file;
	/**
	 * The lexicon used for retrieving documents.
	 */
	protected Lexicon lexicon;
	/**
	 * Closes the underlying bit file.
	 */
	public void close() {
		file.close();
	}
	
	public BitFile getBitFile(){
		return file;
	}
	
}
