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
 * The Original Code is BlockLexiconTreeNode.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.structures.trees;
/**
 * The tree node used when building a lexicon 
 * with block information.  
 * @author Douglas Johnson
 * @version $Revision: 1.1 $
 */
public class BlockLexiconTreeNode extends TreeNode {
	/** The left child of the node.*/
	public BlockLexiconTreeNode left = null;
	/** The right child of the node.*/
	public BlockLexiconTreeNode right = null;
	/** The block frequency */
	protected int blockFrequency;
	/**
	 * Constructs a tree node for a given term and 
	 * the specified frequency within a block. 
	 * @param newTerm the term
	 * @param blockFrequency the block frequency
	 */
	public BlockLexiconTreeNode(String newTerm, int blockFrequency) {
		super(newTerm);
		this.blockFrequency = blockFrequency;
	}
}
