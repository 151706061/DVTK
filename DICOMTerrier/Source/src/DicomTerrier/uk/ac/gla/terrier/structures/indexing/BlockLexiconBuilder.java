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
 * The Original Code is BlockLexiconBuilder.java.
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
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.LinkedList;
import uk.ac.gla.terrier.structures.BlockLexicon;
import uk.ac.gla.terrier.structures.Lexicon;
import uk.ac.gla.terrier.structures.trees.BlockFieldDocumentTreeNode;
import uk.ac.gla.terrier.structures.trees.BlockLexiconTree;
import uk.ac.gla.terrier.structures.trees.FieldDocumentTreeNode;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.Compare;
/**
 * Builds a block lexicon using block frequencies.
 * @author Douglas Johnson &amp; Vassilis Plachouras &amp; Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class BlockLexiconBuilder extends LexiconBuilder
{
	/** The temporary lexicon tree that is kept in memory. */
	private BlockLexiconTree TempLex = new BlockLexiconTree();
	/**
	 * Adds the terms from one document to the current 
	 * temporary lexicon.
	 * @param terms FieldDocumentTreeNode[] the terms of a document.
	 */
	public void addDocumentTerms(FieldDocumentTreeNode[] terms)
	{
		TempLex.insertBuffer((BlockFieldDocumentTreeNode[])terms);
		DocCount++;
		if((DocCount % DocumentsPerLexicon) == 0)
		{
			System.err.println("flushing lexicon");
			try{
				TempLexDirCount = TempLexCount / TempLexPerDir;
				if (!(new File(TemporaryLexiconDirectory + TempLexDirCount)).exists()) {
					File tmpDir = new File(TemporaryLexiconDirectory + TempLexDirCount);
					tmpDir.deleteOnExit();
					tmpDir.mkdir();
				}
				String tmpLexName = TemporaryLexiconDirectory + TempLexDirCount + ApplicationSetup.FILE_SEPARATOR + (TempLexCount) + ApplicationSetup.LEXICONSUFFIX;
				TempLex.storeToFile(tmpLexName);
				tempLexFiles.addLast(tmpLexName);
			}catch(IOException ioe){
				System.err.println("Indexing failed to write a lexicon to disk : "+ioe);
				System.exit(1);
			}
			TempLexCount++;
			TempLex = new BlockLexiconTree();
		}
	}
	/**
	 * The method that performs processing of the lexicon after the
	 * creation of the direct index has been completed. It flushes to 
	 * disk the current temporary lexicon, and it starts the merging
	 * of the temporary lexicons and the creation of the lexicon index. 
	 */
	public void finishedDirectIndexBuild()
	{
		System.err.println("flushing block lexicon to disk after the direct index completed");
		try{
			TempLexDirCount = TempLexCount / TempLexPerDir;
			if (!(new File(TemporaryLexiconDirectory + TempLexDirCount)).exists()) {
				File tmpDir = new File(TemporaryLexiconDirectory + TempLexDirCount);
				tmpDir.deleteOnExit();
				tmpDir.mkdir();
			}
			String tmpLexName = TemporaryLexiconDirectory + TempLexDirCount + ApplicationSetup.FILE_SEPARATOR + (TempLexCount) + ApplicationSetup.LEXICONSUFFIX;
			TempLex.storeToFile(tmpLexName);
			tempLexFiles.addLast(tmpLexName);
		}catch(IOException ioe){
			System.err.println("Indexing failed to write a lexicon to disk : "+ioe);
			System.exit(1);
		}
		//merges the temporary lexicons
		try{
			merge(tempLexFiles);
			//creates the offsets file
			createLexiconIndex(); //TODO exception handling, filenames?
		} catch(IOException ioe){
            System.err.println("Indexing failed to merge temporary lexicons to disk : "+ioe);
            System.exit(1);
        }
		
	}
	/**
	 * Merges the intermediate lexicon files created during the indexing.
	 * @param filesToMerge java.util.LinkedList the list containing the
	 *        filenames of the temporary files.
	 * @throws IOException an input/output exception is throws
	 *         if a problem is encountered.
	 */
	public void merge(LinkedList filesToMerge) throws IOException {
		//now the merging of the files in the filesToMerge vector
		//must take place. We always take the first two entries of
		//the vector, merge them, store the new lexicon in the directory
		//of the first of the two merged lexicons, and put the filename
		//of the new lexicon file at the back of the vector. The first
		//two entries that were merged are removed from the vector. The
		//use of the vector is similar to a FIFO queue in this case.
		int StartFileCount = filesToMerge.size();
		if (StartFileCount == 1) {
			(new File((String)filesToMerge.removeFirst())).
				renameTo((new File(ApplicationSetup.LEXICON_FILENAME)));
		} else {
			System.err.println("begin merging "+ StartFileCount +" files...");
			long startTime = System.currentTimeMillis();
			int progressiveNumber = ApplicationSetup.MERGE_TEMP_NUMBER;
			String newMergedFile = null;
			while (filesToMerge.size() > 1) {
				String fileToMerge1 = (String) filesToMerge.removeFirst();
				String fileToMerge2 = (String) filesToMerge.removeFirst();
				
				if (filesToMerge.size() == 0) 
					newMergedFile = ApplicationSetup.LEXICON_FILENAME;
				else
					newMergedFile =
					(new File(fileToMerge1)).getParent()
						+ ApplicationSetup.FILE_SEPARATOR
						+ ApplicationSetup.MERGE_PREFIX
						+ String.valueOf(progressiveNumber++)
						+ ApplicationSetup.LEXICONSUFFIX;
	
				FileInputStream fis1 = new FileInputStream(fileToMerge1);
				BufferedInputStream bis1 = new BufferedInputStream(fis1);
				DataInputStream dis1 = new DataInputStream(bis1);
	
				FileInputStream fis2 = new FileInputStream(fileToMerge2);
				BufferedInputStream bis2 = new BufferedInputStream(fis2);
				DataInputStream dis2 = new DataInputStream(bis2);
	
				FileOutputStream fos = new FileOutputStream(newMergedFile);
				BufferedOutputStream bos = new BufferedOutputStream(fos);
				DataOutputStream dos = new DataOutputStream(bos);
	
				System.err.println(
					"merging "
						+ fileToMerge1
						+ " with "
						+ fileToMerge2
						+ " to "
						+ newMergedFile);
				int termLength = ApplicationSetup.STRING_BYTE_LENGTH;
				byte[] term1 = new byte[termLength];
				byte[] term2 = new byte[termLength];
				byte[] term = new byte[termLength];
				boolean hasMore1 = true;
				boolean hasMore2 = true;
				int termID1 = 0;
				int termID2 = 0;
				hasMore1 = (dis1.read(term1, 0, termLength) != -1);
				hasMore2 = (dis2.read(term2, 0, termLength) != -1);
				if (hasMore1)
					termID1 = dis1.readInt();
				if (hasMore2)
					 termID2 = dis2.readInt();
				while (hasMore1 && hasMore2) {
					int compareString = 0;
					if (termID1 != termID2)
					{
						String sTerm1 = new String(term1).trim();
						String sTerm2 = new String(term2).trim();
						//compareString = sTerm1.compareTo(sTerm2);
						compareString = Compare.compareWithNumeric(sTerm1, sTerm2);
						if (compareString == 0)//, but termids don't match
						{
							System.err.println("ERROR: Term "+sTerm1+" had two termids ("+
								termID1+","+termID2+")");
						}
					}
					
					if (compareString <0) {
						dos.write(term1, 0, termLength);
						dos.writeInt(termID1 /*dis1.readInt()*/);
						dos.writeInt(dis1.readInt());
						dos.writeInt(dis1.readInt());
						dos.writeInt(dis1.readInt());
						dos.writeLong(dis1.readLong());
						dos.writeByte(dis1.readByte());
						hasMore1 = (dis1.read(term1, 0, termLength) != -1);
						if (hasMore1)
							termID1 = dis1.readInt();
					} else if (compareString >0) {
						dos.write(term2, 0, termLength);
						dos.writeInt(termID2 /*dis2.readInt()*/);
						dos.writeInt(dis2.readInt());
						dos.writeInt(dis2.readInt());
						dos.writeInt(dis2.readInt());
						dos.writeLong(dis2.readLong());
						dos.writeByte(dis2.readByte());
						hasMore2 = (dis2.read(term2, 0, termLength) != -1);
						if (hasMore2)
							 termID2 = dis2.readInt();
					} else /*if (compareBytes == 0)*/ {
						dos.write(term1, 0, termLength);
						//its the same if we write term2 as well
						dos.writeInt(termID2 /*dis1.readInt()*/);
						//thats the code of the term, at this stage is zero
						//dis2.readInt(); //we could skip 4 bytes as well
						dos.writeInt(dis1.readInt() + dis2.readInt());
						//we need to add the doc frequencies
						dos.writeInt(dis1.readInt() + dis2.readInt());
						//we need to add the block frequencies
						dos.writeInt(dis1.readInt());
						dis2.readInt(); //we could skip 4 bytes as well
						dos.writeLong(dis1.readLong());
						dis2.readLong(); //we could skip 8 bytes as well
						dos.writeByte(dis1.readByte());
						dis2.readByte(); //we could skip 1 byte as well
	
						hasMore1 = (dis1.read(term1, 0, termLength) != -1);
						hasMore2 = (dis2.read(term2, 0, termLength) != -1);
						if (hasMore1)
							termID1 = dis1.readInt();
						if (hasMore2)
							termID2 = dis2.readInt();
					}
				}
				if (hasMore1) {
					dis2.close();
					bis2.close();
					fis2.close();	
					dos.write(term1, 0, termLength);
					dos.writeInt(termID1);
					dos.writeInt(dis1.readInt());
					dos.writeInt(dis1.readInt());
					dos.writeInt(dis1.readInt());
					dos.writeLong(dis1.readLong());
					dos.writeByte(dis1.readByte());
					hasMore1 = (dis1.read(term1, 0, termLength) != -1);
					while (hasMore1) {
						dos.write(term1, 0, termLength);
						dos.writeInt(dis1.readInt());
						dos.writeInt(dis1.readInt());
						dos.writeInt(dis1.readInt());
						dos.writeInt(dis1.readInt());
						dos.writeLong(dis1.readLong());
						dos.writeByte(dis1.readByte());
						hasMore1 = (dis1.read(term1, 0, termLength) != -1);
					}
	
					//closing all the streams related to a file
					dis1.close();
					bis1.close();
					fis1.close();
					//deleting the file
				} else if (hasMore2) {
					dis1.close();
					bis1.close();
					fis1.close();
					dos.write(term2, 0, termLength);
					dos.writeInt(termID2);
					dos.writeInt(dis2.readInt());
					dos.writeInt(dis2.readInt());
					dos.writeInt(dis2.readInt());
					dos.writeLong(dis2.readLong());
					dos.writeByte(dis2.readByte());
					hasMore2 = (dis2.read(term2, 0, termLength) != -1);
					while (hasMore2) {
						dos.write(term2, 0, termLength);
						dos.writeInt(dis2.readInt());
						dos.writeInt(dis2.readInt());
						dos.writeInt(dis2.readInt());
						dos.writeInt(dis2.readInt());
						dos.writeLong(dis2.readLong());
						dos.writeByte(dis2.readByte());
						hasMore2 = (dis2.read(term2, 0, termLength) != -1);
					}
					//closing all the streams related to a file
					dis2.close();
					bis2.close();
					fis2.close();
				}
				//closing all the streams related to the output file
				dos.close();
				bos.close();
				fos.close();
				filesToMerge.addLast(newMergedFile);
			}
			long endTime = System.currentTimeMillis();
			System.err.println("end of merging...("+((endTime-startTime)/1000.0D)+" seconds)");
		}
	}
	/**
	 * Creates the lexicon index file that contains a mapping from the 
	 * given term id to the offset in the lexicon, in order to 
	 * be able to retrieve the term information according to the 
	 * term identifier. This is necessary, because the terms in the lexicon 
	 * file are saved in lexicographical order, and we also want to have 
	 * fast access based on their term identifier.
	 * @exception java.io.IOException Throws an Input/Output exception if 
	 *            there is an input/output error. 
	 */
	public void createLexiconIndex() throws IOException {
		/* This method reads from the lexicon the term ids and stores the
		 * corresponding offsets in an array. Then this array is sorted 
		 * according to the term id.
		 */
		//TODO use the class LexiconInputStream
		File lexiconFile = new File(ApplicationSetup.LEXICON_FILENAME);
		int lexiconEntries = (int) lexiconFile.length() / BlockLexicon.lexiconEntryLength;
		DataInputStream lexicon =
			new DataInputStream(
				new BufferedInputStream(new FileInputStream(lexiconFile)));
		//the i-th element of offsets contains the offset in the
		//lexicon file of the term with term identifier equal to i.
		long[] offsets = new long[lexiconEntries];
		final int termLength = ApplicationSetup.STRING_BYTE_LENGTH;
		int termid;
		byte[] buffer = new byte[termLength];
		for (int i = 0; i < lexiconEntries; i++) {
			int read = lexicon.read(buffer, 0, termLength);
			termid = lexicon.readInt();
			int docFreq = lexicon.readInt();
			int blockFreq = lexicon.readInt(); //departure from parent #1
			int freq = lexicon.readInt();
			//offsets[termid] = i * BlockLexicon.lexiconEntryLength; //departure from parent #2
			offsets[termid] = i * Lexicon.lexiconEntryLength; //at the end we only use a Lexicon
			lexicon.readLong();
			lexicon.readByte();
		}
		lexicon.close();
		//save the offsets to a file with the same name as
		//the lexicon and extension .lexid
		File lexid = new File(ApplicationSetup.LEXICON_INDEX_FILENAME);
		DataOutputStream dosLexid =
			new DataOutputStream(
				new BufferedOutputStream(new FileOutputStream(lexid)));
		for (int i = 0; i < offsets.length; i++) {
			dosLexid.writeLong(offsets[i]);
		}
		dosLexid.close();
	}
	/**
	 * A default constructor of the class. 
	 */
	public BlockLexiconBuilder()
	{
		TempLex = new BlockLexiconTree();
	}
	/** 
	 * A default constructor which is given a pathname in which
	 * the temporary lexicons will be stored.
	 * @param pathname String the name of the path in which the temporary
	 *        lexicons will be stored.
	 */
	public BlockLexiconBuilder(String pathname) {
		this();
		TemporaryLexiconDirectory = pathname;
	}	
	
}
