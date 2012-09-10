package uk.ac.gla.terrier.structures.trees;

/**
 * Simple class to represent a tagname and level of occurence
 * @author Gerald
 * @version 1.0
 */

public class Tag {
	
	public String name;
	public int level;
	
	public Tag(String name, int level){
		this.name = name;
		this.level = level;
	}
	
}
