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
 * The Original Code is LexiconBuilder.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
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

import uk.ac.gla.terrier.structures.dicom.TagLexicon;
import uk.ac.gla.terrier.structures.trees.DICOMFieldDocumentTreeNode;
import uk.ac.gla.terrier.structures.trees.TagLexiconTree;
import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * Builds temporary tag lexicons during indexing a collection and
 * merges them when the indexing of a collection has finished.
 * @author Gerald van Veldhuijsen	
 * @version 1.0
 */
public class TagLexiconBuilder
{
	/** How many documents have been processed so far.*/
	protected int DocCount = 0;
	
	/** 
	 * The number of documents for which a temporary lexicon
	 * is created.
	 */
	protected static int DocumentsPerTagLexicon;
	/** The linkedlist in which the temporary lexicon filenames are stored.
	  * These are merged into a single Lexicon by the merge() method. 
	  * LinkedList is best List implementation for this, as all operations
	  * are either append element, or remove first element - making LinkedList
	  * ideal. */
	protected LinkedList tempTagLexFiles = new LinkedList();
	
	/** The lexicontree to write the current term stream to */
	private TagLexiconTree TempTagLex;
	
	/** The directory to write temporary lexicons to */
	protected static String TemporaryTagLexiconDirectory;
	
	/** How many temporary lexicons have been generated so far */
	protected int TempTagLexCount = 0;
	
	/** How many temporary directories have been generated so far */
	protected int TempTagLexDirCount = 0;
	
	/** How many temporary taglexicons per temporary directory */
	protected static int TempTagLexPerDir;
	/** 
	 * A static constructor that initialises the attributes 
	 * DocumentsPerTagLexicon from the property <tt>tag.bundle.size</tt>, 
	 * TemporaryLexiconDirectory from the property <tt>terrier.index.path</tt>
	 * and TempLexPerDir from the property <tt>lexicon.builder.templexperdir</tt>.
	 * The default value of the last property is <tt>100</tt>.
	 */
	static {
		DocumentsPerTagLexicon = new Integer(ApplicationSetup.getProperty("tag.bundle.size", "2500")).intValue();
		
		TemporaryTagLexiconDirectory = ApplicationSetup.TERRIER_INDEX_PATH 
								  + ApplicationSetup.FILE_SEPARATOR;
		TempTagLexPerDir = (new Integer(ApplicationSetup.getProperty("lexicon.builder.templexperdir", "100"))).intValue();
									
	}
	
	/**
	 * A default constructor of the class.
	 */
	public TagLexiconBuilder()
	{
		TempTagLex = new TagLexiconTree();
	}
	/** 
	 * Creates an instance of the class, given the path
	 * to save the temporary lexicons.
	 * @param pathname String the path to save the temporary lexicons.
	 */
	public TagLexiconBuilder(String pathname) {
		this();
		TemporaryTagLexiconDirectory = pathname;
	}
	/**
	 * Adds the terms of a document in the temporary lexicon in memory.
	 * @param terms FieldDocumentTreeNode[] the terms of the document to 
	 *		add in the temporary lexicon in memory.
	 */
	public void addDocumentTerms(DICOMFieldDocumentTreeNode[] terms)
	{
		TempTagLex.insertBuffer(terms);
		DocCount++;
		
		//Hopefully no flushing needed
		/*if((DocCount % DocumentsPerTagLexicon) == 0)
		{
			System.err.println("flushing taglexicon");
			try{
				TempTagLexDirCount = TempTagLexCount / TempTagLexPerDir;
				if (!(new File(TemporaryTagLexiconDirectory + TempTagLexDirCount)).exists()) {
					File tmpDir = new File(TemporaryTagLexiconDirectory + TempTagLexDirCount);
					//tmpDir.deleteOnExit();
					tmpDir.mkdir();
				}
				String tmpTagLexName = TemporaryTagLexiconDirectory + TempTagLexDirCount + ApplicationSetup.FILE_SEPARATOR + (TempTagLexCount) + ApplicationSetup.TAGLEXICONSUFFIX;
				TempTagLex.storeToFile(tmpTagLexName);
				tempTagLexFiles.addLast(tmpTagLexName);
			}catch(IOException ioe){
				System.err.println("Indexing failed to write a lexicon to disk : "+ioe);
				ioe.printStackTrace();
				System.exit(1);
			}
			TempTagLexCount++;
			TempTagLex = new TagLexiconTree();
		}*/
	}
	/**
	 * Processing the lexicon after finished creating the
	 * inverted index.
	 */
	public void finishedInvertedIndexBuild() {}
	
	/** 
	 * Processing the lexicon after finished creating the 
	 * direct and document indexes.
	 */
	public void finishedDirectIndexBuild()
	{
		System.err.println("flushing taglexicon to disk after the direct index completed");
		try{
			TempTagLexDirCount = TempTagLexCount / TempTagLexPerDir;
			if (!(new File(TemporaryTagLexiconDirectory + TempTagLexDirCount)).exists()) {
				File tmpDir = new File(TemporaryTagLexiconDirectory + TempTagLexDirCount);
				//tmpDir.deleteOnExit();
				tmpDir.mkdir();
			}
			String tmpTagLexName = TemporaryTagLexiconDirectory + TempTagLexDirCount + ApplicationSetup.FILE_SEPARATOR + (TempTagLexCount) + ApplicationSetup.TAGLEXICONSUFFIX;
			TempTagLex.storeToFile(tmpTagLexName);
			tempTagLexFiles.addLast(tmpTagLexName);
		}catch(IOException ioe){
			System.err.println("Indexing failed to write a lexicon to disk : "+ioe);
			ioe.printStackTrace();
			System.exit(1);
		}
		//merges the temporary lexicons
		try{
			merge(tempTagLexFiles);
			
			//creates the offsets file
			//createLexiconIndex(); //TODO exception handling, filenames?
		} catch(IOException ioe){
			System.err.println("Indexing failed to merge temporary lexicons to disk : "+ioe);
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	
	/**
	 * Merges the intermediate lexicon files created during the indexing.
	 * @param filesToMerge java.util.LinkedList the list containing the 
	 *		filenames of the temporary files.
	 * @throws IOException an input/output exception is throws 
	 *		 if a problem is encountered.
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
			String name =(String)filesToMerge.removeFirst();
			(new File(name)).renameTo((new File(ApplicationSetup.TAGLEXICON_FILENAME)));
			(new File(name + ApplicationSetup.getProperty("tag.termids.suffix", "tid"))).renameTo((new File(ApplicationSetup.TAGLEXICON_FILENAME + ApplicationSetup.getProperty("tag.termids.suffix", "tid"))));
		} 
		else {
			//TODO change this section, because it is not correct anymore
			//Though this should never be used.
			
			System.err.println("begin merging "+ StartFileCount +" files...");
			long startTime = System.currentTimeMillis();
			int progressiveNumber = ApplicationSetup.TAGMERGE_TEMP_NUMBER;
			String newMergedFile = null;
			while (filesToMerge.size() > 1) {
				String fileToMerge1 = (String) filesToMerge.removeFirst();
				String fileToMerge2 = (String) filesToMerge.removeFirst();
				
				//give the proper name to the final merged lexicon
				if (filesToMerge.size() == 0) 
					newMergedFile = ApplicationSetup.TAGLEXICON_FILENAME;
				else 
					newMergedFile =
						(new File(fileToMerge1)).getParent()
							+ ApplicationSetup.FILE_SEPARATOR
							+ ApplicationSetup.MERGE_PREFIX
							+ String.valueOf(progressiveNumber++)
							+ ApplicationSetup.TAGLEXICONSUFFIX;
	
				//The opening of the files needs to break into more steps, so that
				//all the open streams are closed after the completion of the 
				//operation, and eventually the intermediate files are deleted.
				FileInputStream fis1 = new FileInputStream(fileToMerge1);
				BufferedInputStream bis1 = new BufferedInputStream(fis1);
				DataInputStream dis1 = new DataInputStream(bis1);
	
				FileInputStream fis2 = new FileInputStream(fileToMerge2);
				BufferedInputStream bis2 = new BufferedInputStream(fis2);
				DataInputStream dis2 = new DataInputStream(bis2);
	
				//File f = new File(newMergedFile);
				//do not remove the last merged lexicon file
				//if (filesToMerge.size() > 0)
					//f.deleteOnExit();
				
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
				final int termLength = ApplicationSetup.STRING_BYTE_LENGTH;
				byte[] tag1 = new byte[termLength];
				byte[] tag2 = new byte[termLength];
				//byte[] tag = new byte[termLength];
				int tagID1 = 0;
				int tagID2 = 0;
				boolean hasMore1 = true;
				boolean hasMore2 = true;
				hasMore1 = (dis1.read(tag1, 0, termLength) != -1);
				hasMore2 = (dis2.read(tag2, 0, termLength) != -1);
				if (hasMore1)
					tagID1 = dis1.readInt();
				if (hasMore2)
					 tagID2 = dis2.readInt();
				while (hasMore1 && hasMore2) {
					int compareString = 0;
					if (tagID1 != tagID2)
					{
						String stag1 = new String(tag1).trim();
						String stag2 = new String(tag2).trim();
						compareString = stag1.compareTo(stag2);
						//compareString = Compare.compareWithNumeric(stag1, stag2);
						if (compareString == 0)//, but tagids don't match
						{
							System.err.println("ERROR: tag "+stag1+" had two tagids ("+
								tagID1+","+tagID2+")");
						}
					}
	
					if (compareString < 0) /* tag1 < tag2 */ {
						dos.write(tag1, 0, termLength);
						dos.writeInt(tagID1/*dis1.readInt()*/);
						//dos.writeInt(dis1.readInt());
						//dos.writeInt(dis1.readInt());
						//dos.writeLong(dis1.readLong());
						//dos.writeByte(dis1.readByte());
						hasMore1 = (dis1.read(tag1, 0, termLength) != -1);
						if (hasMore1)
							tagID1 = dis1.readInt();
					} else if (compareString > 0) /* tag2 < tag 1 */ {
						dos.write(tag2, 0, termLength);
						dos.writeInt(tagID2/*dis2.readInt()*/);
						//dos.writeInt(dis2.readInt());
						//dos.writeInt(dis2.readInt());
						//dos.writeLong(dis2.readLong());
						//dos.writeByte(dis2.readByte());
						hasMore2 = (dis2.read(tag2, 0, termLength) != -1);
						if (hasMore2)
							 tagID2 = dis2.readInt();
					} else /*if (compareBytes == 0)*/ {
						dos.write(tag1, 0, termLength);
						//its the same if we write tag2 as well
						dos.writeInt(tagID1/*dis1.readInt()*/);
						/*dis2.readInt();*/

						//we need to add the doc frequencies
						//dos.writeInt(dis1.readInt() + dis2.readInt());
						//dos.writeInt(dis1.readInt());
						//dis2.readInt();
						//dos.writeLong(dis1.readLong());
						//dis2.readLong();
						//dos.writeByte(dis1.readByte());
						//dis2.readByte();
	
						hasMore1 = (dis1.read(tag1, 0, termLength) != -1);
						hasMore2 = (dis2.read(tag2, 0, termLength) != -1);
						if (hasMore1)
							tagID1 = dis1.readInt();
						if (hasMore2)
							tagID2 = dis2.readInt();
					}
				}
				if (hasMore1) {
					dis2.close();
					bis2.close();
					fis2.close();
					dos.write(tag1, 0, termLength);
					dos.writeInt(tagID1);
					//dos.writeInt(dis1.readInt());
					//dos.writeInt(dis1.readInt());
					//dos.writeLong(dis1.readLong());
					//dos.writeByte(dis1.readByte());
					hasMore1 = (dis1.read(tag1, 0, termLength) != -1);
					while (hasMore1) {
						dos.write(tag1, 0, termLength);
						dos.writeInt(dis1.readInt());
						//dos.writeInt(dis1.readInt());
						//dos.writeInt(dis1.readInt());
						//dos.writeLong(dis1.readLong());
						//dos.writeByte(dis1.readByte());
						hasMore1 = (dis1.read(tag1, 0, termLength) != -1);
					}
	
					//closing all the streams related to a file
					dis1.close();
					bis1.close();
					fis1.close();
				} else if (hasMore2) {
					dis1.close();
					bis1.close();
					fis1.close();
					dos.write(tag2, 0, termLength);
					dos.writeInt(tagID2);
					//dos.writeInt(dis2.readInt());
					//dos.writeInt(dis2.readInt());
					//dos.writeLong(dis2.readLong());
					//dos.writeByte(dis2.readByte());
					hasMore2 = (dis2.read(tag2, 0, termLength) != -1);
					while (hasMore2) {
						dos.write(tag2, 0, termLength);
						dos.writeInt(dis2.readInt());
						//dos.writeInt(dis2.readInt());
						//dos.writeInt(dis2.readInt());
						//dos.writeLong(dis2.readLong());
						//dos.writeByte(dis2.readByte());
						hasMore2 = (dis2.read(tag2, 0, termLength) != -1);
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
	 * be able to retrieve the tag information according to the 
	 * tag identifier. This is necessary, because the tags in the lexicon 
	 * file are saved in lexicographical order, and we also want to have 
	 * fast access based on their tag identifier.
	 * @exception java.io.IOException Throws an Input/Output exception if 
	 *			there is an input/output error. 
	 */
	public void createLexiconIndex() throws IOException {
		/*
		 * This method reads from the lexicon the term ids and stores the
		 * corresponding offsets in an array. Then this array is sorted 
		 * according to the term id.
		 */
		
		//TODO use the class LexiconInputStream
		File tagLexiconFile = new File(ApplicationSetup.TAGLEXICON_FILENAME);
		int tagLexiconEntries = (int) tagLexiconFile.length() / TagLexicon.tagLexiconEntryLength;
		DataInputStream tagLexicon =
			new DataInputStream(
				new BufferedInputStream(new FileInputStream(tagLexiconFile)));
		//the i-th element of offsets contains the offset in the
		//lexicon file of the term with term identifier equal to i.
		long[] offsets = new long[tagLexiconEntries];		
		final int termLength = ApplicationSetup.STRING_BYTE_LENGTH;
		int termid;
		byte[] buffer = new byte[termLength];
		for (int i = 0; i < tagLexiconEntries; i++) {
			tagLexicon.read(buffer, 0, termLength);
			termid = tagLexicon.readInt();
			//int docFreq = lexicon.readInt();
			//int freq = lexicon.readInt();
			//if (termid>(offsets.length-1))
			//	System.out.println("ERROR: termid " + termid + " greater then number of " + tagLexiconEntries + " entries");
			//else
			offsets[termid] = i * TagLexicon.tagLexiconEntryLength;
			//lexicon.readLong();
			//lexicon.readByte();
		}
		tagLexicon.close();
		//save the offsets to a file with the same name as 
		//the lexicon and extension .lexid
		File tagLexid = new File(ApplicationSetup.TAGLEXICON_INDEX_FILENAME);
		DataOutputStream dosTagLexid =
			new DataOutputStream(
				new BufferedOutputStream(new FileOutputStream(tagLexid)));
		for (int i = 0; i < offsets.length; i++) {
			dosTagLexid.writeLong(offsets[i]);
		}
		dosTagLexid.close();
	}	
}
