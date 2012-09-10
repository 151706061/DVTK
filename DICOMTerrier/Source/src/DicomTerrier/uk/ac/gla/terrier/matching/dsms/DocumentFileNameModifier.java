package uk.ac.gla.terrier.matching.dsms;
import uk.ac.gla.terrier.matching.MatchingQueryTerms;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.utility.FileList;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import java.io.File;

import java.util.HashSet;

/**
 * This class resets all scores of documents that do not a satisfy the filename constraints
 * 
 * @author Gerald Veldhuijsen
 */
public class DocumentFileNameModifier implements DocumentScoreModifier {
	
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.matching.dsms.DocumentScoreModifier#modifyScores(uk.ac.gla.terrier.structures.Index, uk.ac.gla.terrier.matching.MatchingQueryTerms, uk.ac.gla.terrier.matching.ResultSet)
	 */
	public boolean modifyScores(Index index, MatchingQueryTerms queryTerms, ResultSet resultSet) {
		System.out.print("Using Document FileName Score Modifier");
		long start = System.currentTimeMillis();
		
		double scores[] = resultSet.getScores();
		int docIds[] = resultSet.getDocids();
		int numOfDocs = resultSet.getResultSize(); 
		int numOfModifiedDocs = 0;
		
		String fileName;
		String path;
		//Load the skippable types like PATIENT, DICOMDIR, SERIES and STUDY
		String [] skipTypes = ApplicationSetup.getProperty("skippable.dicom.types", "").split("\\s*,\\s*");
		
		//The boolean whether to group the results for a dataset
		boolean unique = new Boolean(ApplicationSetup.getProperty("group.datasets", "false")).booleanValue();
		HashSet uniques = new HashSet(numOfDocs); 
		
		//We want to loop the two cases together so we have coded the three cases separately
		//Check for pattern AND group datasets
		if (skipTypes.length>0 && unique){
			System.out.print("; Filtering out [");
			for (int i=0; i<skipTypes.length; i++)
				System.out.print(skipTypes[i] +  " ");
			System.out.println("] and grouping datasets");
			
			//We have to skip the found names
			boolean skip;
			
			for (int i=0; i<numOfDocs; i++){
				skip = false;
				path =  FileList.getFileName(docIds[i]);
				fileName = new File(path).getName();
				String setName = "";
				
				//Get the setname
				int pos = path.lastIndexOf( ApplicationSetup.FILE_SEPARATOR + "representations");
				int pos2 = path.lastIndexOf(ApplicationSetup.FILE_SEPARATOR, pos-1);
				setName = path.substring(pos2+1, pos);
								
				String [] names = fileName.split("_");
				if (names.length>2){
					int j=0;
					while ( !skip && j<skipTypes.length){
						if (names[2].equals(skipTypes[j++]))
							skip = true;
					}
					if (skip){
						//Contains the pattern -> remove from result
						if (scores[i] >0.0d)
							numOfModifiedDocs++;
						scores[i]= Double.NEGATIVE_INFINITY;
					} else {
						if (uniques.contains(setName)){
							//We already have an image from this dataset -> remove
							if (scores[i] >0.0d)
								numOfModifiedDocs++;
							scores[i]= 0.0d;
						}
						else{
							if (pos>-1){
								uniques.add(setName);
							}
						}
					}						
				}
			}
		}
		
		//Check for patterns only
		else if (skipTypes.length>0){
			
			System.out.print("; Filtering out [");
			for (int i=0; i<skipTypes.length; i++)
				System.out.print(skipTypes[i] +  " ");
			System.out.println("]");
			//We have to skip the found names
			boolean skip;
					
			for (int i=0; i<numOfDocs; i++){
				skip = false;
				fileName = new File (FileList.getFileName(docIds[i])).getName();
				
				String [] names = fileName.split("_");
				if (names.length>2){
					int j=0;
					while ( !skip && j<skipTypes.length){
						if (names[2].equals(skipTypes[j++]))
							skip = true;
					}
					if (skip){
						if (scores[i] >0.0d)
							numOfModifiedDocs++;
						scores[i]= Double.NEGATIVE_INFINITY;
					}
				}
			}
		}	
		
		//Group datasets only
		else if (unique){
			System.out.println("; Grouping datasets");
			for (int i=0; i<numOfDocs; i++){
				
				path = FileList.getFileName(docIds[i]);
				fileName = new File(path).getName();
				String setName = "";
				
				//Get the setname
				int pos = path.lastIndexOf( ApplicationSetup.FILE_SEPARATOR + "representations");
				int pos2 = path.lastIndexOf(ApplicationSetup.FILE_SEPARATOR, pos-1);
				setName = path.substring(pos2+1, pos);
				
				if (uniques.contains(setName)){
					//We already have an image from this dataset -> remove
					if (scores[i] >0.0d)
						numOfModifiedDocs++;
					scores[i]= 0.0d;
				}
				else{
					if(pos>-1){
						uniques.add(setName);
					}
				}
			}
		}
		
		resultSet.setResultSize(numOfDocs - numOfModifiedDocs);
		System.out.println("Modified " + numOfModifiedDocs + " of " + numOfDocs + " in " + (System.currentTimeMillis()-start) + " milliseconds");
		
		if (numOfModifiedDocs>0)
			return true;
		return false;
	}

	public String getName() {
		return "Document FileName Modifier";
	}
}