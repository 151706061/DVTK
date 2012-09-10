package uk.ac.gla.terrier.matching.tsms;

import gnu.trove.TIntHashSet;
import uk.ac.gla.terrier.matching.models.tagmodel.TagWeight;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.structures.dicom.TagLexicon;
import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * This class modifies the scores of terms that should appear only within a particular tag
 * 
 * @author Gerald van Veldhuijsen
 * @version
 */
public class TermInDICOMFieldModifier extends TermInFieldModifier {
	
	String field;
	boolean requirement;
	Index index;
	TagWeight tagWeightModel;
	TIntHashSet rootTagIds = new TIntHashSet();
	
	public TermInDICOMFieldModifier(String field){
		super(field);
		this.field = field;
		tagWeightModel = new TagWeight();
		index = Index.getIndex();
		
		TagLexicon tagLexicon = index.getTagLexicon();		
		String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
		for (int j=0; j<rootTags.length; j++){
		if (tagLexicon.findTag(rootTags[j]))		
			rootTagIds.add(tagLexicon.getTagId()+1);
		}
	}

	public TermInDICOMFieldModifier(String field, Index i){
		super(field);
		this.field = field;
		index = i;
		tagWeightModel = new TagWeight();
		
		TagLexicon tagLexicon = index.getTagLexicon();		
		String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
		for (int j=0; j<rootTags.length; j++){
		if (tagLexicon.findTag(rootTags[j]))		
			rootTagIds.add(tagLexicon.getTagId()+1);
		}
	}
	
	public TermInDICOMFieldModifier(String field, boolean required){
		super(field, required);
		this.field = field;
		this.requirement = required;
		tagWeightModel = new TagWeight();
		
		index = Index.getIndex();
		
		TagLexicon tagLexicon = index.getTagLexicon();		
		String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
		for (int j=0; j<rootTags.length; j++){
		if (tagLexicon.findTag(rootTags[j]))		
			rootTagIds.add(tagLexicon.getTagId()+1);
		}
	}
	
	public TermInDICOMFieldModifier(String field, boolean required, Index i){
		super(field, required);
		this.field = field;
		this.requirement = required;
		index = i;
		tagWeightModel = new TagWeight();
		
		TagLexicon tagLexicon = index.getTagLexicon();		
		String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
		for (int j=0; j<rootTags.length; j++){
		if (tagLexicon.findTag(rootTags[j]))		
			rootTagIds.add(tagLexicon.getTagId()+1);
		}
	}
	
	/** 
	 * Resets the scores of documents for a particular term, based on 
	 * the fields a term appears in documents.
	 * @param scores double[] the scores of the documents.
	 * @param pointers int[][] the pointers read from the inverted file 
	 *        for a particular query term.
	 * @return the number of documents for which the scores were modified. 
	 */
	public int modifyScores(double[] scores, int[][] pointers) {
		
		int numOfModifiedDocs=0;
		
		int[] numberOfTags = pointers[2];
		int[] tagIdentifiers = pointers[3];

		final int numOfPointers = numberOfTags.length;

		//index = Index.getIndex();
		TagLexicon TL = index.getTagLexicon();
				
		if(TL.findTag(field.toLowerCase())){
			//We have found the tag, so now we can compare it with the tags of the terms			
			
			//The id of the tag
			int fieldId = TL.getTagId();
								
			if (requirement) { //the term should appear in the field
				int index = 0;
				for (int j = 0; j < numOfPointers; j++) {
					int foundIndex = tagIdentifiers.length;
					int foundNumberOfTags = 0;
					boolean tagFound = false;
					int curNumberOfTags = numberOfTags[j];
					int m=0;
					
					for (int k=0; k<curNumberOfTags; k += m){
						boolean foundMin = false;
						m=0;
						index++;//Skip the '0' tag, which alway occurs first.
						m++;	
						
						while((k+m<curNumberOfTags) && ( !rootTagIds.contains(tagIdentifiers[index]+1)) ){
							if (tagIdentifiers[index++] == fieldId){
								tagFound = true;
								if (m<foundIndex){
									foundMin = true;
									foundIndex = m;
								}
							}
							m++;
						}
						System.out.println();
						if (foundMin)
							foundNumberOfTags = m;
					}
					
					if(!tagFound){
						if (scores[j]> 0.0d)
							numOfModifiedDocs++;
						scores[j] = 0.0d;
					} else 
						scores[j] = tagWeightModel.modifyScore(foundNumberOfTags - foundIndex, scores[j]);
				}
			} else { //the term should not appear in the field
				//This will not happen
				int index = 0;
				for (int j = 0; j < numOfPointers; j++) {
					int foundIndex = tagIdentifiers.length;
					boolean tagFound = false;
					int curNumberOfTags = numberOfTags[j];
					int m=0;
					
					for (int k=0; k<curNumberOfTags; k += m){
						m=0;
						index++;//Skip the '0' tag, which alway occurs first.
						m++;
						while((k+m<curNumberOfTags) && ( !rootTagIds.contains(tagIdentifiers[index]+1)) ){
							if (tagIdentifiers[index++] == fieldId){
								tagFound = true;
								if (m<foundIndex)
									foundIndex = m;
							}
							m++;
						}
					}
					
					if(tagFound){
						if (scores[j]>0.0d)
							numOfModifiedDocs++;
						scores[j] = 0.0d;
					}					
				}
			}
		}
		else{
			//System.out.println("Tag not found");
			//We have not found the tag. All scores of terms that require this field should be reset 
			if(requirement){
				for (int j = 0; j < numOfPointers; j++) {
					if (scores[j]!=Double.NEGATIVE_INFINITY)
						numOfModifiedDocs++;
					scores[j] = Double.NEGATIVE_INFINITY;
				}
			}
		}
				
		return numOfModifiedDocs;
	}
	
	public String getName() {
		return "TermInDICOMFieldModifier("+field+","+requirement+")";
	}
}
