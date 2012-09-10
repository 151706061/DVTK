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
 * The Original Code is TermTreeNode.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Ben He <ben{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.trees;
/** 
 * This class represents a node in the binary tree of a query.
 * The difference between a TreeNode and a TermTreeNode is that
 * the TermTreeNode contains a normalised frequency, in addition
 * to the standard integer frequency, to account for the normalised
 * frequencies of terms in expanded queries.
 * @author Ben He, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class TermTreeNode extends TreeNode {
	/** The left child of the node.*/
	public TermTreeNode left = null;
	/** The right child of the node.*/
	public TermTreeNode right = null;
	/** The normalised frequency of the term.*/
	public double normalisedFrequency = 1;
	/** 
	 * A constructor for a new node.
	 * @param newTerm The term that this node is representing.
	 */
	public TermTreeNode(String newTerm) {
		super(newTerm);
	}
}
