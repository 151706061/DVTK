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
 * The Original Code is DocumentTree.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.trees;

/**
 * This class represents the binary tree
 * formed by the terms in a document.
 * @author Gianni Amati, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class DocumentTree {
	/** The root of the binary tree.*/
	protected TreeNode treeRoot = null;
	/** The number of nodes in the tree.*/
	protected int numberOfNodes;
	/** The number of pointers in the tree.*/
	protected int numberOfPointers;
	/** A counter for using in the traversePreOrder method.*/
	private int counter;
	/** A TreeNode buffer used in the traversePreOrder method.*/
	private TreeNode[] nodeBuffer;
	/** 
	 * Empties the tree.
	 */
	public void empty() {
		treeRoot = null;
		numberOfPointers = 0;
		numberOfNodes = 0;
	}
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
				int lexicographicOrder = tmpNode.getTerm().compareTo(newTerm);
				//int lexicographicOrder = Compare.compareWithNumeric(tmpNode.term, newTerm);
				if (lexicographicOrder == 0) {
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
	 * Returns an array of the term nodes of the tree.
	 * @return the contents of the tree as a TreeNode array.
	 */
	public TreeNode[] toArray() {
		nodeBuffer = new TreeNode[numberOfNodes];
		counter = 0;
		traversePreOrder(treeRoot);
		return nodeBuffer;
	}
	/** 
	 * The helper for returning the buffer of terms of the document.
	 * @param node The node to insert to the buffer
	 */
	protected void traversePreOrder(TreeNode node) {
		if (node == null)
			return;
		nodeBuffer[counter] = node;
		counter++;
		traversePreOrder(node.left);
		traversePreOrder(node.right);
	}
}
