package uk.ac.gla.terrier.matching.dsms;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.utility.FileList;
import gnu.trove.TIntDoubleHashMap;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import gnu.trove.TIntIntHashMap;

/**
 * This class increases the score of documents of which the meta file contains relevant information.
 * 
 * The multiplier <tt>meta.multiplier</tt> determines the weight of the meta score
 * 
 * @author Gerald Veldhuijsen
 * @version 1.0
 */


public class MetaModifier implements DocumentScoreModifier {
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.matching.dsms.DocumentScoreModifier#modifyScores(uk.ac.gla.terrier.structures.Index, uk.ac.gla.terrier.matching.MatchingQueryTerms, uk.ac.gla.terrier.matching.ResultSet)
	 */
	public boolean modifyScores(Index index, MatchingQueryTerms queryTerms, ResultSet resultSet) {
		System.out.println("Using meta Document Score Modifier");
		long start = System.currentTimeMillis();
		
		double metaMultiplier = new Double(ApplicationSetup.getProperty("meta.multiplier", "1.0")).doubleValue(); 
		
		TIntDoubleHashMap metaScores = new TIntDoubleHashMap();
		TIntIntHashMap metaMap = new TIntIntHashMap();
		double scores[] = resultSet.getScores();
		int docIds[] = resultSet.getDocids();
		short occurences[] = resultSet.getOccurrences();
		int numOfDocs = resultSet.getResultSize(); 
		int numOfModifiedDocs = 0;
		int numOfAddedDocs = 0;
		int numOfRemovedDocs = 0;
		
		//Build up set of meta file with a score
		for (int i=0; i<numOfDocs; i++) {
			if (FileList.isMeta(docIds[i])){
				metaScores.put(docIds[i], scores[i]+1);
				metaMap.put(docIds[i], i+1);
				scores[i] = Double.NEGATIVE_INFINITY;
				numOfRemovedDocs++;
				numOfModifiedDocs++;
			}
		}
		
		//Increase the scores of other documents
		if (metaMap.size()>0)
		for (int i=0; i<docIds.length; i++){
						
			int metaId = FileList.getMetaDocId(docIds[i]);
			int curMetaId = metaMap.get(metaId);
			
			if (curMetaId>0){
				double score = metaScores.get(metaId)-1;
				short occurence = occurences[curMetaId-1];
				
				if(score>0.0d){
					//System.out.println("Retrieved extra score " + score +  " for document " + docIds[i]);
					if(scores[i]==0.0d){
						numOfAddedDocs++;						
					}
					else
						numOfModifiedDocs++;
					scores[i] += metaMultiplier * score;
										
					occurences[i] |= occurence;					
				}
			}			
		}
		
		resultSet.setResultSize(numOfDocs + numOfAddedDocs - numOfRemovedDocs);
		System.out.println("Modified " + numOfModifiedDocs + " of " + numOfDocs + " documents and added " + numOfAddedDocs + " in " + (System.currentTimeMillis()-start) + " milliseconds");
		
		if (numOfModifiedDocs>0 || numOfAddedDocs>0)
			return true;
		return false;
	}
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.matching.dsms.DocumentScoreModifier#getName()
	 */
	public String getName() {
		return "Meta Modifier";
	}
}
