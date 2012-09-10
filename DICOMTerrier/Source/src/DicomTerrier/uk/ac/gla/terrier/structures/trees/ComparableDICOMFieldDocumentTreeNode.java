package uk.ac.gla.terrier.structures.trees;

import java.util.HashSet;
import java.util.Stack;
import java.util.ArrayList;
import java.util.Iterator;
import uk.ac.gla.terrier.utility.TagCodes;
import uk.ac.gla.terrier.utility.ComparableTermCodes;

/**
 * This class implements the node of a comparable term in the document tree of comparable terms
 * Used by the class ComparableDICOMFieldDocumentTree 
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class ComparableDICOMFieldDocumentTreeNode extends DICOMFieldDocumentTreeNode {
	/** The left child of the node.*/
	public ComparableDICOMFieldDocumentTreeNode left = null;
	/** The right child of the node.*/
	public ComparableDICOMFieldDocumentTreeNode right = null;
	/** The list of fields for the term. */
	private ArrayList fieldList;
	private ArrayList fieldIdList;
	
	/**
	 * Constructs a tree node for the given term.
	 * @param newTerm the term
	 */
	public ComparableDICOMFieldDocumentTreeNode(String newTerm) {
		term = newTerm;
		termCode = ComparableTermCodes.getCode(term);
		fieldList = new ArrayList();
		fieldIdList = new ArrayList();
	}
	/**
	 * Constructs a tree node for the given term
	 * that appears within a field.
	 * @param newTerm the term
	 * @param field the field containing this term
	 */
	public ComparableDICOMFieldDocumentTreeNode(String newTerm, String field) {
		term = newTerm;
		termCode = ComparableTermCodes.getCode(term);
		fieldList = new ArrayList();
		fieldIdList = new ArrayList();
		if (field!=null)
			add(field);
	}
	
	/**
	 * Constructs a tree node for the given term
	 * that appears within the given fields.
	 * @param fields HashSet the fields in which the term appears
	 */
	public ComparableDICOMFieldDocumentTreeNode(String newTerm, HashSet fields) {
		term = newTerm;
		termCode = ComparableTermCodes.getCode(term);
		fieldList = new ArrayList();
		fieldIdList = new ArrayList();
		if (fields!=null) 
			add(fields);			
	}
	
	/**
	 * Constructs a tree node for the given term
	 * that appears within the given fields.
	 * @param fields HashSet the fields in which the term appears
	 */
	public ComparableDICOMFieldDocumentTreeNode(String newTerm, Stack fields) {
		term = newTerm;
		termCode = ComparableTermCodes.getCode(term);
		fieldList = new ArrayList();
		fieldIdList = new ArrayList();
		if (fields!=null) 
			add(fields);			
	}
	
	/**
	 * Adds the field to the field score of the tree node.
	 * @param field String the field to add
	 */
	public void addToFieldScore(String field) {
		if (field!=null) 
			add(field);
	}
	
	/**
	 * Adds the fields to the field score of the tree node.
	 * @param fields HashSet the fields to add
	 */
	public void addToFieldScore(HashSet fields) {
		add(fields);
	}
	
	/**
	 * Adds the fields to the field score of the tree node.
	 * @param fields Stack with arrays of fields to add.
	 */
	public void addToFieldScore(Stack fields) {
		add(fields);
	}
	
	/**
	 * Gets the field score of the tree node.
	 * @return the list of field ids
	 */
	public ArrayList getFieldIdList() {
		return fieldIdList;
	}
	
	/**
	 * Gets the field score of the tree node.
	 * @return the list of fields
	 */
	public ArrayList getFieldList() {
		return fieldList;
	}
	
	/**
	 * Returns the number of fields this term appears in	 * 
	 * @return number of fields
	 */	
	public int numberOfFields(){
		return fieldList.size();
	}
	
	/**
	 * Returns the number of unique field ids this term appears in	 * 
	 * @return number of field ids
	 */	
	public int numberOfFieldIds(){
		return fieldIdList.size();
	}
	
	private void add(HashSet fields){
		Iterator ite = fields.iterator();
		while(ite.hasNext())
			add((String)ite.next());
	}

	/**
	 * Add a stack of tags to the tree
	 * @param fields A stack containing arrays of fields
	 */	
	private void add(Stack fields){
		Iterator ite = fields.iterator();
		int code1=0, code2;
		int level = fields.size()-1;
		while(ite.hasNext()){
			String [] tags = (String[])ite.next();
			String keyTag = null;
			
			//Check whether one of the names has already been assigned a code
			for (int i=0; i<tags.length; i++){
				if(TagCodes.hasCode(tags[i])){
					keyTag = tags[i];
					code1 = TagCodes.getCode(keyTag);
					break;
				}
					
			}
			
			//No code was found for the names, we take the first name to assign a code
			if(keyTag==null){
				keyTag = tags[0];
				code1 = TagCodes.getCode(keyTag);				
			}
				
			fieldIdList.add(new Integer(code1));
			
			//Add the first tag
			//fieldList.add(tags[0]);
			//code1 = TagCodes.getCode(tags[0]);
			//fieldIdList.add(new Integer(code1));
			//Assign to the rest the same code as the first
			
			//Now assign all other names the same code as the one that was found
			for (int i=0; i<tags.length; i++){
				Tag tag = new Tag(tags[i],level);
				fieldList.add( tag );
				code2 = TagCodes.assignCode(keyTag, tags[i]);
				//System.out.print("Assigning same code of " + tags[0] + " to " + tags[i]);
				if (code2!= code1){
					//System.out.print(", Failed");
					//We could not assign the same code to this tag so we need to store it explicitly
					fieldIdList.add(new Integer(code2));
				}				
			}
			level--;
		}
	}
	/*
	private void add(Stack fields){
		Iterator ite = fields.iterator();
		int code1, code2;
		while(ite.hasNext()){
			String [] tags = (String[])ite.next();

			//Add the first tag
			fieldList.add(tags[0]);
			code1 = TagCodes.getCode(tags[0]);
			fieldIdList.add(new Integer(code1));
			//Assign to the rest the same code as the first
			for (int i=1; i<tags.length; i++){
				fieldList.add(tags[i]);
				code2 = TagCodes.assignCode(tags[0], tags[i]);
				//System.out.print("Assigning same code of " + tags[0] + " to " + tags[i]);
				if (code2!= code1){
					//System.out.print(", Failed");
					//We could not assign the same code to this tag so we need to store it explicitly
					fieldIdList.add(new Integer(code2));
				}				
			}
		}			
	}*/
	
	public String getTerm(){
		return this.term;
	}
	
	public int getTermCode(){
		return this.termCode;
	}
	
	private void add(String field){
		fieldList.add(field);
		fieldIdList.add(new Integer(TagCodes.getCode(field)));
	}
	
	/**
	 * @deprecated
	 */
	public int getFieldScore() {
		return 0;
	}
}
