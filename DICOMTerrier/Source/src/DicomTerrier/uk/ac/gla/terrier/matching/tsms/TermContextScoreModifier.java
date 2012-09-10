package uk.ac.gla.terrier.matching.tsms;

import gnu.trove.TIntDoubleHashMap;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.structures.dicom.TagLexicon;

/**
 * This class is used to alter the score of a term when it appears in the right context
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class TermContextScoreModifier implements TermScoreModifier{
	
	private TIntDoubleHashMap contextScores;
	
	public TermContextScoreModifier(){
		contextScores = new TIntDoubleHashMap();
		String contextTag = ApplicationSetup.getProperty("context.tags", "");
		double addScore = new Double(ApplicationSetup.getProperty("context.tags.multiplier", "1")).doubleValue();
		
		String [] contextTags = contextTag.split("\\s*,\\s*");
		TagLexicon TL = Index.getIndex().getTagLexicon();
		
		for(int i=0; i<contextTags.length; i++){
			
			if(TL.findTag(contextTags[i].toLowerCase())){
				System.out.println("Adding tag " + contextTags[i]);
				contextScores.put(TL.getTagId(), addScore);
			}
		}		
	}
	
	/**
	 * Modifies the scores of documents for a particular term, based on the fields
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
		int nrOfModifiedDocuments = 0;
		
		int[] numberOfTags = pointers[2];
		int[] tagIdentifiers = pointers[3];
		
		final int numOfPointers = numberOfTags.length;
		
		int index = 0;
		for (int j = 0; j < numOfPointers; j++) {
			int curNumberOfTags = numberOfTags[j];
			
			for (int k=0; k<curNumberOfTags; k ++){
				double mp = contextScores.get(tagIdentifiers[index++]);
				if(mp>0.0d){
					scores[j] *= mp;
					//System.out.println("Increasing score because of tag " + tagIdentifiers[index-1]);
				}
			}			
		}
		
		return nrOfModifiedDocuments;
	}
	
	public String getName() {
		return "TermContextScoreModifier";
	}
}
