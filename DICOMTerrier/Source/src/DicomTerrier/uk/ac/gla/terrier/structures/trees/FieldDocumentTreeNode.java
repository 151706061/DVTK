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
 * The Original Code is FieldDocumentTreeNode.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Douglas Johnson <johnsoda{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures.trees;
import java.util.HashSet;
import uk.ac.gla.terrier.utility.FieldScore;
/**
 * The tree node used by the class FieldDocumentTree, 
 * when building the direct file with field information.
 * @see uk.ac.gla.terrier.structures.trees.FieldDocumentTree
 * @author Douglas Johnson, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class FieldDocumentTreeNode extends TreeNode {
	/** The left child of the node.*/
	public FieldDocumentTreeNode left = null;
	/** The right child of the node.*/
	public FieldDocumentTreeNode right = null;
	/** The html score for the term. */
	private FieldScore fieldScore;
	
	/**TODO hack*/
	FieldDocumentTreeNode() {
		super();
		fieldScore = new FieldScore();
	}
	
	
	/**
	 * Constructs a tree node for the given term.
	 * @param newTerm the term
	 */
	public FieldDocumentTreeNode(String newTerm) {
		super(newTerm);
		fieldScore = new FieldScore();
	}
	/**
	 * Constructs a tree node for the given term
	 * that appears within a field.
	 * @param newTerm the term
	 * @param field the field containing this term
	 */
	public FieldDocumentTreeNode(String newTerm, String field) {
		super(newTerm);
		fieldScore = new FieldScore();
		if (field!=null)
			fieldScore.insertField(field);
	}
	
	/**
	 * Constructs a tree node for the given term
	 * that appears within the given fields.
	 * @param fields HashSet the fields in which the term appears
	 */
	public FieldDocumentTreeNode(String newTerm, HashSet fields) {
		super(newTerm);
		fieldScore = new FieldScore();
		if (fields!=null) 
			fieldScore.insertFields(fields);
	}
		
	/**
	 * Adds the field to the field score of the tree node.
	 * @param field String the field to add to the field score
	 */
	public void addToFieldScore(String field) {
		if (field!=null) 
			fieldScore.insertField(field);
	}
	
	/**
	 * Adds the fields to the field score of the tree node.
	 * @param fields HashSet the fields to add to the field score
	 */
	public void addToFieldScore(HashSet fields) {
		fieldScore.insertFields(fields);
	}
	/**
	 * Gets the field score of the tree node.
	 * @return the field score
	 */
	public int getFieldScore() {
		return fieldScore.getFieldScore();
	}
}
