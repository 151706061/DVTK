package uk.ac.gla.terrier.matching.dsms;

import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.structures.Index;
import gnu.trove.TIntHashSet;
import uk.ac.gla.terrier.utility.FileList;

/**
 * This class resets all scores of documents that do not a satisfy the constraints
 * 
 * @author Gerald Veldhuijsen
 */

public class ConstraintModifier implements DocumentScoreModifier {

	//The set of doc ids satisfying the constraints
	private TIntHashSet satisfiers;
	
	public ConstraintModifier(TIntHashSet satisfiers){
		this.satisfiers = satisfiers;
	}	
	
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.matching.dsms.DocumentScoreModifier#modifyScores(uk.ac.gla.terrier.structures.Index, uk.ac.gla.terrier.matching.MatchingQueryTerms, uk.ac.gla.terrier.matching.ResultSet)
	 */
	public boolean modifyScores(Index index, MatchingQueryTerms queryTerms, ResultSet resultSet) {
		System.out.println("Using Constraint Document Score Modifier");
		long start = System.currentTimeMillis();
		
		double scores[] = resultSet.getScores();
		int docIds[] = resultSet.getDocids();
		int numOfDocs = resultSet.getResultSize(); 
		int numOfModifiedDocs = 0;
		
		for (int i=0; i<numOfDocs; i++) {
			if (!satisfiers.contains(docIds[i]) && !satisfiers.contains(FileList.getMetaDocId(docIds[i])) && !FileList.isMeta(docIds[i]) ) {
				numOfModifiedDocs++;
				scores[i] = Double.NEGATIVE_INFINITY;
			}
		}
		resultSet.setResultSize(numOfDocs - numOfModifiedDocs);
		
		System.out.println("Modified " + numOfModifiedDocs + " of " + numOfDocs + " documents in " + (System.currentTimeMillis()-start) + " milliseconds");
		
		if (numOfModifiedDocs>0)
			return true;
		return false;
	}
	
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.matching.dsms.DocumentScoreModifier#getName()
	 */
	public String getName() {
		return "Constraint Modifier";
	}

}
