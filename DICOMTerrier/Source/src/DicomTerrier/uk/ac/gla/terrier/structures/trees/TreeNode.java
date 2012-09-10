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
import uk.ac.gla.terrier.utility.TermCodes;
/**
 * This class represents a node in the binary tree representing either the
 * lexicon, or a document. In the case we are refering to a tree node in the
 * lexicon, then frequency is the document frequency of the term (number of
 * documents that contain the term), while if we refer to the binary tree that
 * represents a document, then frequency refers to the number of occurrences of
 * the term in the document. 
 * @author Gianni Amati, Vassilis Plachouras, Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class TreeNode implements Comparable {
	/** The left child of the node. */
	public TreeNode left = null;
	/** The right child of the node. */
	public TreeNode right = null;
	/** The frequency of the term. */
	public int frequency = 1;
	/** The term. */
	protected String term;
	/** The term's code. */
	protected int termCode;
	
	TreeNode(){
	}
	
	/**
	 * A constructor for a new node.
	 * 
	 * @param newTerm
	 *            The term that this node is representing.
	 */
	public TreeNode(String newTerm) {
		term = newTerm;
		//gets the term code for the given term. If we
		//haven't encountered the term before, we assign
		//a new term code, otherwise we assign to it the
		//already given term code to the previous occurrences
		//of the term.
		termCode = TermCodes.getCode(term);
	}
	/**
	 * Compares the term code of a given tree node with the
	 * term code of itself. This method is part of the 
	 * implementation of the Comparable interface.
	 * @param o1 Object the tree node to compare to.
	 * @return -1, zero, or 1 if the term code of this tree
	 *         node is less, equal to, or greater than the 
	 *         term code of the given tree node.
	 * @throws ClassCastException if the given argument is 
	 *         not an instance of the class TreeNode.
	 */
	public int compareTo(Object o1) throws ClassCastException {
		TreeNode TN1 = (TreeNode) o1;
		if (termCode < TN1.termCode)
			return -1;
		else if (termCode > TN1.termCode)
			return 1;
		return 0;
	}
	/**
	 * Compares the term code of the given tree node 
	 * with the term code of this node. 
	 * @param o Object the tree node to compare to.
	 * @return true if the given tree node has the same
	 *         term code as the current one, otherwise
	 *         it returns false.
	 * @throws ClassCastException if the object o is not
	 *         an instance of the class TreeNode.
	 */
	public boolean equals(Object o) throws ClassCastException {
		TreeNode TN1 = (TreeNode) o;
		return termCode == TN1.termCode;
	}
	
	public String getTerm(){
		return term;
	}
	
	public int getTermCode(){
		return termCode;
	}
}