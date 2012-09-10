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
 * The Original Code is TreeNode.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.trees;
import uk.ac.gla.terrier.utility.TagCodes;
import gnu.trove.TIntHashSet;

/**
 * This class represents a node in the binary tree representing the tag
 * lexicon. 
 * @author Gerald Veldhuijsen
 * @version 1.0
 */
public class TagTreeNode implements Comparable {
	/** The left child of the node. */
	public TagTreeNode left = null;
	/** The right child of the node. */
	public TagTreeNode right = null;

	/** The tag. */
	public String tag;
	/** The tag's code. */
	public int tagCode;
	/** The set of termids */
	public TIntHashSet termids;
	
	/**
	 * A constructor for a new node.
	 * 
	 * @param newTag
	 *            The tag that this node is representing.
	 */
	public TagTreeNode(String newTag) {
		tag = newTag;
		//gets the tag code for the given tag. If we
		//haven't encountered the tag before, we assign
		//a new tag code, otherwise we assign to it the
		//already given tag code to the previous occurrences
		//of the tag.
		tagCode = TagCodes.getCode(tag);
		termids = new TIntHashSet();
	}
	
	/**
	 * Add the code of the current term to this tag
	 * @param code The term code
	 */
	public void addTermCode(int code){
		termids.add(code+1);		
	}
	
	/**
	 * Compares the tag code of a given tree node with the
	 * tag code of itself. This method is part of the 
	 * implementation of the Comparable interface.
	 * @param o1 Object the tree node to compare to.
	 * @return -1, zero, or 1 if the tag code of this tree
	 *         node is less, equal to, or greater than the 
	 *         tag code of the given tree node.
	 * @throws ClassCastException if the given argument is 
	 *         not an instance of the class TreeNode.
	 */
	public int compareTo(Object o1) throws ClassCastException {
		TagTreeNode TN1 = (TagTreeNode) o1;
		if (tagCode < TN1.tagCode)
			return -1;
		else if (tagCode > TN1.tagCode)
			return 1;
		return 0;
	}
	/**
	 * Compares the tag code of the given tree node 
	 * with the tag code of this node. 
	 * @param o Object the tree node to compare to.
	 * @return true if the given tree node has the same
	 *         tag code as the current one, otherwise
	 *         it returns false.
	 * @throws ClassCastException if the object o is not
	 *         an instance of the class TreeNode.
	 */
	public boolean equals(Object o) throws ClassCastException {
		TagTreeNode TN1 = (TagTreeNode) o;
		return tagCode == TN1.tagCode;
	}
}