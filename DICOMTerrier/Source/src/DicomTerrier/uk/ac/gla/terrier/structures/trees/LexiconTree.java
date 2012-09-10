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
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.Compare;
/**
 * This class represents the binary tree
 * formed by the terms in the lexicon.
 * @author Gianni Amati, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class LexiconTree {
	/** The maximum number of characters of a term.*/
	protected static final int stringByteLength = ApplicationSetup.STRING_BYTE_LENGTH;
	
	/** A buffer used to add zero bytes to entries writen in the file on disk.*/
	protected static byte[] zeroBuffer =
		new byte[ApplicationSetup.STRING_BYTE_LENGTH];
	/** The root of the binary tree.*/
	protected TreeNode treeRoot = null;
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
	public void insert(String newTerm) {
		if (treeRoot == null) {
			treeRoot = new TreeNode(newTerm);
			numberOfNodes++;
			numberOfPointers++;
		} else {
			TreeNode tmpNode = treeRoot;
			while (true) {
				//int lexicographicOrder = tmpNode.term.compareTo(newTerm);
				int lexicographicOrder = Compare.compareWithNumeric(tmpNode.getTerm(), newTerm);
				if (lexicographicOrder == 0) {
					if (tmpNode.getTerm().compareTo(newTerm)!=0)
						System.out.println("BIGERROR: " + tmpNode.getTerm() + " not equal to " + newTerm);
					tmpNode.frequency++;
					numberOfPointers++;
					break;
				} else if (lexicographicOrder > 0) {
					if (tmpNode.left == null) {
						tmpNode.left = new TreeNode(newTerm);
						numberOfNodes++;
						numberOfPointers++;
						break;
					} else
						tmpNode = tmpNode.left;
				} else {
					if (tmpNode.right == null) {
						tmpNode.right = new TreeNode(newTerm);
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
	public void insertBuffer(final TreeNode[] buffer) {
		final int bufferLength = buffer.length;
		for (int i = 0; i < bufferLength; i++){
			//System.out.println("Inserting " + buffer[i].getTerm() + "," + buffer[i].getTermCode());
			insert(buffer[i].getTerm());
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
		System.out.println("Storing to file " + filename);
		File f = new File(filename);
		f.deleteOnExit();
		BufferedOutputStream bos =
			new BufferedOutputStream(new FileOutputStream(f));
		if (treeRoot != null) {
			ByteArrayOutputStream buffer = new ByteArrayOutputStream();
			dos = new DataOutputStream(buffer);
			traverseAndStoreToBuffer(treeRoot);
			buffer.writeTo(bos);
		}
		bos.close();
	}
	
	/** 
	 * Traverses in order a tree starting from 
	 * a given node and streams its contents
	 * in a data output stream.  
	 * @param node The node from which to start traversing in order
	 */
	protected void traverseAndStoreToBuffer(TreeNode node) throws IOException {
		if (node != null) {
			traverseAndStoreToBuffer(node.left);
			//System.out.println("Writing " + node.getTerm() + "," + node.getTermCode());
			//if (node.frequency>0){
				dos.writeBytes(node.getTerm());
				dos.write(
					zeroBuffer,
					0,
					stringByteLength - node.getTerm().length());
				dos.writeInt(node.getTermCode());
				dos.writeInt(node.frequency);
				//the next numbers correspond to the term frequency within
				//the collection, the offset in bytes, and the offset of 
				//the last bit of the terms information in the inverted file,
				//which we assume it is not yet available
				dos.writeInt(0); 
				dos.writeLong(0);
				dos.writeByte(0);
			//}
			//else 
			//	System.out.println("BIGERROR: Term " + node.getTerm() + " has frequency zero; skipping" );
			traverseAndStoreToBuffer(node.right);			
		}		
	}
}
