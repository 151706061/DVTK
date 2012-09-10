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
 * The Original Code is LexiconTree.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.trees;
import java.io.BufferedOutputStream;
import java.io.ByteArrayOutputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.ArrayList;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.compression.BitOutputStream;
import gnu.trove.TIntHashSet;
import gnu.trove.TIntIterator;

/**
 * This class represents the binary tree
 * formed by the tags in the lexicon.
 * @author Gerald Veldhuijsen
 * @version 1.0
 */
public class TagLexiconTree {
	/** The maximum number of characters of a tag.*/
	protected static final int stringByteLength = ApplicationSetup.STRING_BYTE_LENGTH;
	
	/** The maximum depth for terms to be comparable */
	protected int tagLevelDepth = Integer.parseInt(ApplicationSetup.getProperty("comp.max.level.depth", "2"));
	
	/** A buffer used to add zero bytes to entries writen in the file on disk.*/
	protected static byte[] zeroBuffer =
		new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** The root of the binary tree.*/
	protected TagTreeNode treeRoot = null;
	/** The number of nodes in the tree.*/
	protected int numberOfNodes;
	/** The number of pointers in the tree.*/
	protected int numberOfPointers;
	/** 
	 * A data output stream for writing the content of 
	 * the tree to a file. Used by the method storeToFile.
	 */
	protected DataOutputStream dos = null;
	/**
	 * A bit file output stream for storing the termids for each tag.
	 */
	protected BitOutputStream termids = null;
	
	long byteOffset;
	byte bitOffset;
	
	/**
	* Returns the numbe of nodes in the tree.
	* @return int the number of nodes in the tree.
	*/
	public int getNumberOfNodes() {
		return numberOfNodes;
	}
	/**
	 * Returns the number of pointers in the tree.
	 * @return int the number of pointers in the tree.
	 */
	public int getNumberOfPointers() {
		return numberOfPointers;
	}
	/**
	 * Inserts a new term in the lexicon binary tree.
	 * @param newTerm The term to be inserted.
	 */
	public void insert(String newTag, int termCode) {
		if (treeRoot == null) {
			treeRoot = new TagTreeNode(newTag);
			treeRoot.addTermCode(termCode);
			numberOfNodes++;
			numberOfPointers++;
		} else {
			TagTreeNode tmpNode = treeRoot;
			while (true) {
				int lexicographicOrder = tmpNode.tag.compareTo(newTag);
				//int lexicographicOrder = Compare.compareWithNumeric(tmpNode.tag, newTag);
				if (lexicographicOrder == 0) {
					//tmpNode.frequency++;
					tmpNode.addTermCode(termCode);
					numberOfPointers++;
					break;
				} else if (lexicographicOrder > 0) {
					if (tmpNode.left == null) {
						tmpNode.left = new TagTreeNode(newTag);
						tmpNode.left.addTermCode(termCode);
						numberOfNodes++;
						numberOfPointers++;
						break;
					} else
						tmpNode = tmpNode.left;
				} else {
					if (tmpNode.right == null) {
						tmpNode.right = new TagTreeNode(newTag);
						tmpNode.right.addTermCode(termCode);
						numberOfNodes++;
						numberOfPointers++;
						break;
					} else
						tmpNode = tmpNode.right;
				}
			}
		}
	}
	
	/**
	 * Inserts a new term in the lexicon binary tree.
	 * @param newTerm The term to be inserted.
	 */
	public void insert(String newTag) {
		if (treeRoot == null) {
			treeRoot = new TagTreeNode(newTag);
			numberOfNodes++;
			numberOfPointers++;
		} else {
			TagTreeNode tmpNode = treeRoot;
			while (true) {
				int lexicographicOrder = tmpNode.tag.compareTo(newTag);
				//int lexicographicOrder = Compare.compareWithNumeric(tmpNode.tag, newTag);
				if (lexicographicOrder == 0) {
					//tmpNode.frequency++;
					numberOfPointers++;
					break;
				} else if (lexicographicOrder > 0) {
					if (tmpNode.left == null) {
						tmpNode.left = new TagTreeNode(newTag);
						numberOfNodes++;
						numberOfPointers++;
						break;
					} else
						tmpNode = tmpNode.left;
				} else {
					if (tmpNode.right == null) {
						tmpNode.right = new TagTreeNode(newTag);
						numberOfNodes++;
						numberOfPointers++;
						break;
					} else
						tmpNode = tmpNode.right;
				}
			}
		}
	}
	
	/**
	 * Inserts to the tree the tag contained in the buffer.
	 * @param buffer a matrix of TreeNodes
	 */
	public void insertBuffer(final DICOMFieldDocumentTreeNode[] buffer) {
		Tag tag;
		final int bufferLength = buffer.length;
		for (int i = 0; i < bufferLength; i++){
			int numberOfFields = buffer[i].numberOfFields();
			ArrayList fieldList = buffer[i].getFieldList();
			for (int j=0; j<numberOfFields; j++){
				tag = (Tag)(fieldList.get(j));
				if(tag.level < tagLevelDepth){
					insert(tag.name,buffer[i].getTermCode());
					//System.out.println("Inserting tag: " + tag.name + ", term: " + buffer[i].getTerm() + ", termcode: " + buffer[i].getTermCode() );
				}			
				else
					insert(tag.name);
			}
		}
	}
	/** 
	 * Stores the lexicon tree to a file 
	 * as sequence of entries. The binary tree
	 * is traversed in order, by calling the
	 * method traverseAndStoreToBuffer.
	 * @param filename The name of the file to store to.
	 */
	public void storeToFile(String filename) throws IOException {
		File f = new File(filename);
		termids = new BitOutputStream(filename + "tid");
		f.deleteOnExit();
		BufferedOutputStream bos =
			new BufferedOutputStream(new FileOutputStream(f));
		if (treeRoot != null) {
			ByteArrayOutputStream buffer = new ByteArrayOutputStream();
			dos = new DataOutputStream(buffer);
			traverseAndStoreToBuffer(treeRoot);
			buffer.writeTo(bos);
		}
		termids.close();
		bos.close();
	}
	/** 
	 * Traverses in order a tree starting from 
	 * a given node and streams its contents
	 * in a data output stream.  
	 * @param node The node from which to start traversing in order
	 */
	protected void traverseAndStoreToBuffer(TagTreeNode node) throws IOException {
		if (node != null) {
			traverseAndStoreToBuffer(node.left);
			
			
			TIntHashSet ids = node.termids;
			TIntIterator iter = ids.iterator();
			int size = ids.size();
			//System.out.print("Tag " + node.tag + ", writing ids(" + size  + "): ");
			int prev = -1;
			int cur;
			termids.writeUnary(size+1);
			for (int i=0; i<size; i++){
				cur = iter.next()-1;
				//System.out.print(cur + ",");
				termids.writeGamma(cur-prev);
				prev = cur;
			}
			//System.out.println();
			
			/*Current file position*/
			byteOffset = termids.getByteOffset();
			bitOffset = termids.getBitOffset();
			
			dos.writeBytes(node.tag);
			dos.write(
				zeroBuffer,
				0,
				stringByteLength - node.tag.length());
			dos.writeInt(node.tagCode);
			dos.writeLong(byteOffset);
			dos.writeByte(bitOffset);
			
			traverseAndStoreToBuffer(node.right);
		}
	}
}
