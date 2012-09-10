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
 * The Original Code is BlockLexiconTree.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.structures.trees;
import java.io.*;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.Compare;
/**
 * The binary tree used when constructing the lexicon with block information.
 * The tree nodes are instances of BlockLexiconTreeNode.
 * 
 * @author Douglas Johnson
 * @version $Revision: 1.1 $
 */
public class BlockLexiconTree {
	/** A buffer used to add zero bytes to entries writen in the file on disk.*/
	protected static byte[] zeroBuffer = new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** The root of the binary tree.*/
	protected BlockLexiconTreeNode treeRoot = null;
	/** The number of nodes in the tree.*/
	protected int numberOfNodes;
	/** The number of pointers in the tree.*/
	protected int numberOfPointers;
	/** A TreeNode buffer used in the traverseInOrder method.*/
	protected BlockLexiconTreeNode[] nodeBuffer;
	/** A data output stream for writing the content of the tree to a file. Used by storeToFile.*/
	protected DataOutputStream dos = null;
	/**
	 * Returns the number of nodes in the tree.
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
	 * Inserts a new ndoe in the block lexicon binary tree.
	 * @param node The node to be inserted.
	 */
	public void insert(BlockFieldDocumentTreeNode node) {
		if (treeRoot == null) {
			treeRoot = new BlockLexiconTreeNode(node.getTerm(), node.blockTree.numberOfNodes);
			numberOfNodes++;
			numberOfPointers++;
		} else {
			BlockLexiconTreeNode tmpNode = treeRoot;
			while (true) {
				//int lexicographicOrder = tmpNode.term.compareTo(node.term);
				int lexicographicOrder = Compare.compareWithNumeric(tmpNode.getTerm(), node.getTerm());
				if (lexicographicOrder == 0) {
					tmpNode.frequency++;
					tmpNode.blockFrequency+=node.blockTree.numberOfNodes;
					numberOfPointers++;
					break;
				} else if (lexicographicOrder > 0) {
					if (tmpNode.left == null) {
						tmpNode.left =
							new BlockLexiconTreeNode(node.getTerm(), node.blockTree.numberOfNodes);
						numberOfNodes++;
						numberOfPointers++;
						break;
					} else
						tmpNode = tmpNode.left;
				} else {
					if (tmpNode.right == null) {
						tmpNode.right =
							new BlockLexiconTreeNode(node.getTerm(), node.blockTree.numberOfNodes);
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
	 * Inserts to the tree the terms contained in the buffer.
	 * @param buffer a matrix of TreeNodes
	 */
	public void insertBuffer(final BlockFieldDocumentTreeNode[] buffer) {
		final int bufferLength = buffer.length;
		for (int i = 0; i < bufferLength; i++) {
			if (buffer[i] != null)
				insert(buffer[i]);
		}
	}
	/** 
	* Stores the lexicon tree to a file 
	* as a vector of entries.
	* @param filename The name of the file to store to.
	*/
	public void storeToFile(String filename) throws IOException {
		
		//the filename is temporary and it will be deleted on exit.
		File f = new File(filename);
		f.deleteOnExit();
		BufferedOutputStream bos =
			new BufferedOutputStream(new FileOutputStream(filename));
		if (treeRoot != null) {
			ByteArrayOutputStream buffer = new ByteArrayOutputStream();
			dos = new DataOutputStream(buffer);
			traverseAndStoreToBuffer(treeRoot);
			buffer.writeTo(bos);
		}
		bos.close();
	}
	/** 
	 * Stores an entry to a buffer. 
	 * @param node The node to insert to the buffer
	 */
	protected void traverseAndStoreToBuffer(BlockLexiconTreeNode node)
		throws IOException {
		if (node != null) {
			traverseAndStoreToBuffer(node.left);
			dos.writeBytes(node.getTerm());
			if (node.getTerm().length()> ApplicationSetup.STRING_BYTE_LENGTH )
				System.out.println("Node: " + node.getTerm().toString() + " " + node.getTerm().length());
			dos.write(
				zeroBuffer,
				0,
				ApplicationSetup.STRING_BYTE_LENGTH - node.getTerm().length());
			dos.writeInt(node.getTermCode());
			dos.writeInt(node.frequency);
			dos.writeInt(node.blockFrequency);
			
			//the next numbers correspond to the term frequency within
			//the collection, the offset in bytes, and the offset of 
			//the last bit of the terms information in the inverted file,
			//which we assume it is not yet available
			dos.writeInt(0);
			dos.writeLong(0);
			dos.writeByte(0);
			traverseAndStoreToBuffer(node.right);
		}
	}
}
