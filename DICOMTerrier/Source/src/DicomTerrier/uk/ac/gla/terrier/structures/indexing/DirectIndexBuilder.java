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
 * The Original Code is DirectIndexBuilder.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>(original author)
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.structures.indexing;
import uk.ac.gla.terrier.compression.BitFile;
import uk.ac.gla.terrier.structures.FilePosition;
import uk.ac.gla.terrier.structures.trees.FieldDocumentTreeNode;
import uk.ac.gla.terrier.structures.trees.TreeNode;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * Builds a direct index, using field information optionally.
 * @author Vassilis Plachouras &amp; Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class DirectIndexBuilder {
	/** The gamma compressed file containing the terms. */
	protected BitFile file;
	/**
	 * Adds a document in the direct index, using field information
	 * optionally. The addition of the document's terms in the
	 * data structure is handled by the private methods addFieldDocument or 
	 * addNoFieldDocument.
	 * @param terms FieldDocumentTreeNode[] the array that contains the 
	 *        document's terms to index.
	 * @return FilePosition the offset in the direct index after adding the
	 *         terms.
	 */
	public FilePosition addDocument(FieldDocumentTreeNode[] terms)
	{
		if (saveTagInformation) {
			addFieldDocument((FieldDocumentTreeNode[])terms);
		} else {
			addNoFieldDocument(terms);
		}
		
		/* find out where we are */
		FilePosition rtr = getLastEndOffset();
		
		/* flush to disk if necessary */
		if (DocumentsSinceFlush++ >= DocumentsPerFlush)
		{
			flushBuffer();
			resetBuffer();
			DocumentsSinceFlush = 0;
		}
		/* and then return where the position of the last 
		 * write to the DirectIndex */
		return rtr;
	}
	/**
	 * Adds a document in the direct index, using field information.
	 * @param terms FieldDocumentTreeNode[] the array that contains the 
	 *        document's terms to index.
	 * @return FilePosition the offset in the direct index after adding the
	 *         terms.
	 */
	private void addFieldDocument(FieldDocumentTreeNode[] terms) {
				
		FieldDocumentTreeNode treeNode1 = terms[0];
		/* write the first entry to the DirectIndex */
		int termCode = treeNode1.getTermCode();
		file.writeGamma(termCode + 1);
		file.writeUnary(treeNode1.frequency);
		file.writeBinary(fieldTags, treeNode1.getFieldScore());
		int prevTermCode = termCode;
		
		final int length = terms.length;
		if (length > 1) {
			for (int termNo = 1; termNo < length; termNo++) {
				treeNode1 = terms[termNo];
				termCode = treeNode1.getTermCode();
				file.writeGamma(termCode - prevTermCode);
				file.writeUnary(treeNode1.frequency);
				file.writeBinary(fieldTags, treeNode1.getFieldScore());
				prevTermCode = termCode;
			}
		}	
	}
	/**
	 * Adds a document in the direct index, without using field information
	 * @param terms FieldDocumentTreeNode[] the array that contains the 
	 *        document's terms to index.
	 * @return FilePosition the offset in the direct index after adding the
	 *         terms.
	 */
	private void addNoFieldDocument(TreeNode[] terms) {
	
		TreeNode treeNode1 = terms[0];
		/* write the first entry to the DirectIndex */
		int termCode = treeNode1.getTermCode();
		file.writeGamma(termCode + 1);
		file.writeUnary(treeNode1.frequency);
		int prevTermCode = termCode;
		
		final int length = terms.length;
		if (length > 1) {
			for (int termNo = 1; termNo < length; termNo++) {
				treeNode1 = terms[termNo];
				termCode = treeNode1.getTermCode();
				file.writeGamma(termCode - prevTermCode);
				file.writeUnary(treeNode1.frequency);
				prevTermCode = termCode;
			}
		}	
	}
	/**
	 * When the indexing has reached the end of all collections,
	 * this method writes the buffers on disk and closes the 
	 * corresponding files.
	 */
	public void finishedCollections()
	{
		flushBuffer();
		resetBuffer();
		DocumentsSinceFlush = 0;
		System.err.println("flush direct index");
		close();
	}
	/** 
	 * Flushes the data to disk.
	 */
	public void flushBuffer() {
		file.writeFlush();
	}
	/** 
	 * Returns the current offset in the direct index.
	 * @return FilePosition the offset in the direct index.
	 */
	public FilePosition getLastEndOffset()
	{
		/* where the current position of the DirectIndex, minus 1 bit */
		long endByte = file.getByteOffset();
		byte endBit = file.getBitOffset();
		endBit--;
	
		if (endBit < 0 && endByte > 0) {
			endBit = 7;
			endByte--;
		}
	
		return new FilePosition(endByte, endBit);
	}
	/**
	 * Resets the internal buffer for writing data. This method should
	 * be called before adding any documents to the direct index.
	 */
	public void resetBuffer() {
		file.writeReset();
	}
	/** The number of documents to be indexed before flushing the data to disk.*/
	protected static int DocumentsPerFlush = ApplicationSetup.BUNDLE_SIZE;
	
	/** The number of different fields that are used for indexing field information.*/
	protected static final int fieldTags = FieldScore.FIELDS_COUNT;
	
	/** Indicates whether field information is used. */
	protected static final boolean saveTagInformation = FieldScore.USE_FIELD_INFORMATION;
	
	/** The number of documents indexed since the last flush to disk.*/
	protected int DocumentsSinceFlush = 0;
	/**
	 * Closes the underlying gamma compressed file.
	 */
	public void close() {
		file.close();
	}
	/**
	 * Constructs an instance of the direct index 
	 * with a default name for the underlying direct file.
	 */
	public DirectIndexBuilder() {
		file = new BitFile(ApplicationSetup.DIRECT_FILENAME, "rw");
		resetBuffer();
	}
	/**
	 * Constructs an instance of the direct index
	 * with a non-default name for the underlying direct file.
	 * @param filename the non-default filename used 
	 * 		  for the underlying direct file.
	 */
	public DirectIndexBuilder(String filename) {
		file = new BitFile(filename,"rw");
		resetBuffer();
	}
}
