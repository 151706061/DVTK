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
package uk.ac.gla.terrier.structures.dicom;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.util.ArrayList;
import uk.ac.gla.terrier.structures.TermTagLexicon;
import uk.ac.gla.terrier.structures.InvertedIndex;
import uk.ac.gla.terrier.structures.CollectionStatistics;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import gnu.trove.TIntHashSet;
import gnu.trove.TIntArrayList;
/**
 * This class implements the inverted dicom index 
 * for performing retrieval, with tag information
 * 
 * @author Gerald Veldhuijsen
 * @version Version 1.0
 */
public class InvertedTagIndex extends InvertedIndex {
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
	
	/** The file containing the patterns of the tagstructures*/
	protected RandomAccessFile tagStructuresFile;
	protected int entryLength = CollectionStatistics.getTagStructureMaxLength();
	protected byte[] bt = new byte[entryLength];
	
	/*Temp variables */
	protected int[] prevArray;
	protected int prevId = -1;
		
	/**
	 * Creates an instance of the InvertedTagIndex class using the lexicon.
	 * @param lexicon The lexicon used for retrieval
	 */
	public InvertedTagIndex(TermTagLexicon lexicon) {
		super(lexicon);
		try{
			tagStructuresFile = new RandomAccessFile(ApplicationSetup.INVERTED_TAG_FILENAME +"id","r");
		} catch (IOException ioe) {
			System.err.println(
				"Input/output exception while opening for reading the term tag lexicon file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	/**
	 * Creates an instance of the HtmlInvertedIndex class using the given
	 * lexicon.
	 * @param lexicon The lexicon used for retrieval
	 * @param filename The name of the inverted file
	 */
	public InvertedTagIndex(TermTagLexicon lexicon, String filename) {
		super(lexicon, filename);
		try{
			tagStructuresFile = new RandomAccessFile(ApplicationSetup.INVERTED_TAG_FILENAME +"id","r");
		} catch (IOException ioe) {
			System.err.println(
				"Input/output exception while opening for reading the term tag lexicon file. Stack trace follows");
			ioe.printStackTrace();
			System.exit(1);
		}
	}

	/**
	 * Returns a two dimensional array containing the document ids, term
	 * frequencies and field scores for the given documents. 	  
	 * @return int[][] the two dimensional [3][n] array containing the n 
	 *         document identifiers, frequencies and field scores.
	 * @param termid the identifier of the term whose documents we are looking for.
	 */
	public int[][] getDocuments(int termid, int tagid) {
		/* Coding is done separately for with Fields and without Fields, to keep
		 * if's out of loops. */
		
		String term = termid + "";
		String tag = tagid + "";
		
		int todo = 7- term.length();
		for (int i=todo; i>0; i--)
			term = "0" + term;
		
		todo = 5- tag.length();
		for (int i=todo; i>0; i--)
			tag = "0" + tag;
		
		
		boolean found = lexicon.findTerm(term+tag);
		if (!found) 
			return null;
				
		byte startBitOffset = lexicon.getStartBitOffset();
		long startOffset = lexicon.getStartOffset();
		byte endBitOffset = lexicon.getEndBitOffset();
		long endOffset = lexicon.getEndOffset();
				
		ArrayList temporaryTerms = null; //instantiate when we know roughly how big it should be
		ArrayList temporaryTagids = null;
		int tagCount=0;
		int[][] documentTerms = null;
		
		file.readReset(startOffset, startBitOffset, endOffset, endBitOffset);
				
		/* FIELD_LOAD_FACTOR provides a heuristical rough size need for the arraylist. */
		/* could probably do a better optimisation by considering the number of fields.*/
		temporaryTerms = new ArrayList((int)((endOffset-startOffset)*FIELD_LOAD_FACTOR));
		temporaryTagids = new ArrayList((int)((endOffset-startOffset)*FIELD_LOAD_FACTOR));
		TIntArrayList tmpTagids = new TIntArrayList();
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
			int[] tmp2 = new int[tagfreq];
			/*for (int i = 0; i < tagfreq; i++) {
				tmp2[i] = file.readGamma();
				//System.out.println("Read tagcode gamma " + tmp2[i]);
				tagCount++;
			}*/
			
			if(tagfreq>0){
				tmpTagids = new TIntArrayList();
				file.readGammas(tmp2);
				for(int j=0; j<tagfreq; j++){
					int [] tags = readStructureId(tmp2[j]-1);
					tmpTagids.add(tags);
				}
				tagCount += tmpTagids.size();
				tmp[2] = tmpTagids.size();
			}
			
			temporaryTagids.add(tmpTagids.toNativeArray());
			temporaryTerms.add(tmp);
			
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
		
		/*if(tagindex>0)
			documentTerms[3][0] = tagids[0] - 1;
			for(int i=1; i<tagids.length; i++){
				documentTerms[3][i]= tagids[i] + documentTerms[3][i-1];
		}*/
		
		for(int i=0; i<tagids.length; i++)
			documentTerms[3][i]= tagids[i];
		
		if (documentTerms[0].length > 1) {
			for (int i = 1; i < documentTerms[0].length; i++) {
				int[] tmpMatrix = (int[]) temporaryTerms.get(i);
				documentTerms[0][i] = tmpMatrix[0] + documentTerms[0][i - 1];
				documentTerms[1][i] = tmpMatrix[1];
				documentTerms[2][i] = tmpMatrix[2];
				tagids = ((int[])temporaryTagids.get(i));
				/*if (tagids.length>0){
					documentTerms[3][tagindex] = tagids[0] - 1;
					tagindex++;
					for(int j=1; j<tagids.length; j++){
						documentTerms[3][tagindex] = tagids[j] + documentTerms[3][tagindex-1];
						tagindex++;
					}
				}*/
				
				for(int j=0; j<tagids.length; j++){
					documentTerms[3][tagindex++] = tagids[j];
				}
			}
		}
		return documentTerms;
	}
	
	private int[] readStructureId(int id){
		if (id == prevId)
			return prevArray;
		
		String term = "";
		int [] result=null;
		try{
			tagStructuresFile.seek(id * entryLength);
			tagStructuresFile.read(bt,0,entryLength);
			term = new String(bt).trim();
			result = new int[term.length()/5];
			int k=0;
			int length = term.length();
			for (int i=0; i<length; i=i+5){
				result[k++] = Integer.parseInt(term.substring(i,i+5));				
			}
			
		} catch (IOException e){
			System.err.println(e.getMessage());
		}
		
		prevId =id;
		prevArray = result;
		return result;
	}
	
	/**
	 * The underlying bit file.
	 */
	//protected BitFile file;
	/**
	 * The lexicon used for retrieving documents.
	 */
	//protected Lexicon lexicon;
	
	
}
