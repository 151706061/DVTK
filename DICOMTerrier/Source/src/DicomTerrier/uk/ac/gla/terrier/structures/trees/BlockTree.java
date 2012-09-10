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
 * The Original Code is BlockTree.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.structures.trees;
/**
 * The binary tree used when creating the direct file with block information.
 * 
 * @author Douglas Johnson
 * @version $Revision: 1.1 $
 */
public class BlockTree {
	/** The root of the binary tree. */
	protected BlockTreeNode treeRoot = null;
	/** The number of nodes in the tree. */
	protected int numberOfNodes;
	/** The number of pointers in the tree. */
	protected int numberOfPointers;
	/** A counter for using in the traverseInOrder method. */
	protected int counter;
	/** A TreeNode buffer used in the traverseInOrder method. */
	protected BlockTreeNode[] nodeBuffer;
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
	 * Inserts a new blockid in the block binary tree.
	 * @param blockId The blockid to be inserted.
	 */
	public boolean insert(int blockId) {
		if (treeRoot == null) {
			treeRoot = new BlockTreeNode(blockId);
			numberOfNodes++;
			numberOfPointers++;
			return true;
		} else {
			BlockTreeNode tmpNode = treeRoot;
			while (true) {
				if (tmpNode.blockId == blockId) {
					tmpNode.blockFrequency++;
					numberOfPointers++;
					return false;
				} else if (tmpNode.blockId > blockId) {
					if (tmpNode.left == null) {
						tmpNode.left = new BlockTreeNode(blockId);
						numberOfNodes++;
						numberOfPointers++;
						return true;
					} else
						tmpNode = tmpNode.left;
				} else {
					if (tmpNode.right == null) {
						tmpNode.right = new BlockTreeNode(blockId);
						numberOfNodes++;
						numberOfPointers++;
						return true;
					} else
						tmpNode = tmpNode.right;
				}
			}
		}
	}
	/**
	 * Returns an array of the block id nodes of the tree.
	 * @return an array of the block id nodes of the tree
	 */
	public BlockTreeNode[] toArray() {
		nodeBuffer = new BlockTreeNode[numberOfNodes];
		counter = 0;
		traversePreOrder(treeRoot);
		return nodeBuffer;
	}
	/**
	 * The helper for returning the buffer of block id of the document.
	 * @param node The node to insert to the buffer
	 */
	protected void traversePreOrder(BlockTreeNode node) {
		if (node == null)
			return;
		nodeBuffer[counter] = node;
		counter++;
		traversePreOrder(node.left);
		traversePreOrder(node.right);
	}
}