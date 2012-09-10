package uk.ac.gla.terrier.matching.tsms;

import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.structures.dicom.TagLexicon;
import uk.ac.gla.terrier.matching.models.tagmodel.TagWeight;
import gnu.trove.TIntHashSet;

/**
 * Class that modifies the scores for values that are part of a boolean search query.
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class TermInBoolDICOMFieldModifier extends TermInFieldModifier {

	String field;
	int fieldId;
	boolean found;	
	boolean requirement;
	TagWeight tagWeightModel;
	TIntHashSet rootTagIds = new TIntHashSet();

	public TermInBoolDICOMFieldModifier(){
		super("");
		tagWeightModel = new TagWeight();
	}
	
	public void setField(String field){
		this.field = field;		
	}
	
	public void setFieldId(int fieldId){
		this.fieldId = fieldId;
		if (fieldId<0) found = false;
		else found = true;
	}
	
	public void addRootTagId(int tagId){
		rootTagIds.add(tagId+1);
	}
	
	public void setRequired(boolean required){
		this.requirement= required;
	}
	
	public TermInBoolDICOMFieldModifier(String field, int fieldId ) {
		super(field);
		this.field = field;
		this.fieldId = fieldId;
		if (fieldId<0) found = false;
		else found = true;
		tagWeightModel = new TagWeight();
	}
	
	public TermInBoolDICOMFieldModifier(String field, boolean required) {
		super(field, required);
		this.field = field;
		this.requirement = required;
		TagLexicon TL = Index.getIndex().getTagLexicon();
		found = TL.findTag(field.toLowerCase());
		fieldId = TL.getTagId();
		tagWeightModel = new TagWeight();
	}
	
	public TermInBoolDICOMFieldModifier(String field, boolean required, int fieldId) {
		super(field, required);
		this.field = field;
		this.requirement = required;
		this.fieldId = fieldId;
		if (fieldId<0) found = false;
		else found = true;
		tagWeightModel = new TagWeight();
	}

	/**
	 * Resets the scores of documents for a particular term, based on the fields
	 * a term appears in documents.
	 * 
	 * @param scores
	 *            double[] the scores of the documents.
	 * @param pointers
	 *            int[][] the pointers read from the inverted file for a
	 *            particular query term.
	 * @return the number of documents for which the scores were modified.
	 */
	public int modifyScores(double[] scores, int[][] pointers) {
		int numOfModifiedDocs = 0;
		// check that there field scores have been retrieved
		if (pointers.length < 3 || pointers[2] == null)
			return numOfModifiedDocs;

		int[] numberOfTags = pointers[2];
		int[] tagIdentifiers = pointers[3];

		final int numOfPointers = numberOfTags.length;
				
		if (found) {
			// System.out.println("Tag found");
			// We have found the tag, so now we can compare it with the tags of
			// the terms

			if (requirement) { //the term should appear in the field
				int index = 0;
				for (int j = 0; j < numOfPointers; j++) {
					int foundIndex = tagIdentifiers.length;
					int foundNumberOfTags =0;
					boolean tagFound = false;
					int curNumberOfTags = numberOfTags[j];
					int m=0;
					//System.out.println("Curnumberoftags is " + curNumberOfTags);
					for (int k=0; k<curNumberOfTags; k += m){
						//System.out.print("Fields for pointer " + k + " (" + curNumberOfTags +" fields total):" );
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
						//System.out.println();
						if (foundMin)
							foundNumberOfTags = m;									
					}
					
					if (tagFound){
						scores[j] = tagWeightModel.modifyScore(foundNumberOfTags - foundIndex, scores[j]);
					} else {
						if (scores[j]>0.0d)
							numOfModifiedDocs++;
						scores[j] = 0.0d;					
					}
				}
			} else { //the term should not appear in the field
				//This will never happen
				int index = 0;
				for (int j = 0; j < numOfPointers; j++) {
					
					int foundIndex = tagIdentifiers.length;
					int foundNumberOfTags =0;
					boolean tagFound = false;
					int curNumberOfTags = numberOfTags[j];
					int m=0;
					
					for (int k=0; k<curNumberOfTags; k += m){
						boolean foundMin = false;
						m=0;
						index++;//Skip the '0' tag, which alway occurs first.
						m++;
						while((k+m<curNumberOfTags) && (!rootTagIds.contains(tagIdentifiers[index]+1)) ){
							if (tagIdentifiers[index++] == fieldId){
								tagFound = true;
								if (m<foundIndex){
									foundMin = true;
									foundIndex = m;
								}
							}
							m++;
						}
						
						if (foundMin)
							foundNumberOfTags = m;									
					}
					
					if (!tagFound){
						scores[j] += 1 * tagWeightModel.modifyScore(foundNumberOfTags - foundIndex, scores[j]);//TODO should this be 1?
					} else {
						if (scores[j]>0.0d)
							numOfModifiedDocs++;
						scores[j] = 0.0d;					
					}
				}
			}
		} else {
			// System.out.println("Tag not found");
			// We have not found the tag. All scores of terms that require this
			// field should be reset
			if (requirement) {
				for (int j = 0; j < numOfPointers; j++) {
					if (scores[j] != Double.NEGATIVE_INFINITY)
						numOfModifiedDocs++;
					scores[j] = Double.NEGATIVE_INFINITY;
				}
			}
		}

		return numOfModifiedDocs;
	}

	public String getName() {
		return "TermInDICOMFieldModifier(" + field + "," + requirement + ")";
	}
}
