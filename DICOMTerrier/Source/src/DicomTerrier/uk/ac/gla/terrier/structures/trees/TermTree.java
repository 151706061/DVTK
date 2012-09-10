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
 * The Original Code is TermTree.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 *   Ben He <ben{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.trees;

import uk.ac.gla.terrier.utility.Compare;

/**
 * This class represents the binary tree formed by a set 
 * of terms, such as the terms in a query. It uses the TermTreeNode
 * class for representing the tree nodes
 * @author Gianni Amati, Vassilis Plachouras, Ben He
 * @version $Revision: 1.1 $
 */
public class TermTree {
	/** The root of the binary tree.*/
	protected TermTreeNode treeRoot = null;
	/** The number of nodes in the tree.*/
	protected int numberOfNodes;
	/** The number of pointers in the tree.*/
	protected int numberOfPointers;
	/** A counter for using in the traversePreOrder method.*/
	protected int counter;
	/** A TermTreeNode buffer used in the traversePreOrder method.*/
	protected TermTreeNode[] nodeBuffer;
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
			treeRoot = new TermTreeNode(newTerm);
			numberOfNodes++;
			numberOfPointers++;
		} else {
			TermTreeNode tmpNode = treeRoot;
			while (true) {
				//int lexicographicOrder = tmpNode.term.compareTo(newTerm);
				int lexicographicOrder = Compare.compareWithNumeric(tmpNode.getTerm(), newTerm);
				if (lexicographicOrder == 0) {
					tmpNode.frequency++;
					tmpNode.normalisedFrequency++;
					numberOfPointers++;
					break;
				} else if (lexicographicOrder > 0) {
					if (tmpNode.left == null) {
						tmpNode.left = new TermTreeNode(newTerm);
						numberOfNodes++;
						numberOfPointers++;
						break;
					} else
						tmpNode = tmpNode.left;
				} else {
					if (tmpNode.right == null) {
						tmpNode.right = new TermTreeNode(newTerm);
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
	 * Returns an array of TermTreeNodes for the tree.
	 * @return the content of the tree as an array of TermTreeNode
	 */
	public TermTreeNode[] toArray() {
		nodeBuffer = new TermTreeNode[numberOfNodes];
		counter = 0;
		traversePreOrder(treeRoot);
		return nodeBuffer;
	}
	/** 
	 * The helper for returning the buffer of terms of the document.
	 * @param node The node to from which to begin the traversal.
	 */
	protected void traversePreOrder(TermTreeNode node) {
		if (node == null)
			return;
		nodeBuffer[counter] = node;
		counter++;
		traversePreOrder(node.left);
		traversePreOrder(node.right);
	}
}
