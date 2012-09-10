package uk.ac.gla.terrier.matching.models.tagmodel;

import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * This class provides the weighting model for terms that occur in a tag.
 * @author Gerald van Veldhuijsen
 *
 */

public class TagWeight {

	private double multiplier = new Double(ApplicationSetup.getProperty("tag.level.modifier", "0.6")).doubleValue();
		
	/**
	 * Calculate the score modifier for this depth
	 * @param depth Depth compared to this tag.
	 * @param score The current score
	 * @return
	 */		
	public final double modifyScore(int depth, double score){
		return (score *  Math.pow(multiplier,(depth-1)) );
	}
	
}
