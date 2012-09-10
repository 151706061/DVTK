package uk.ac.gla.terrier.matching.models;

import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * Basic weighting model that set all scores to a default value.
 * 
 * @author Gerald van Veldhuijsen
 * @version
 */
public class Simplest extends WeightingModel {

	private double score = new Double(ApplicationSetup.getProperty("basic.boolean.tag.weight", "0.5")).doubleValue();
	
	public String getInfo() {
		return "Simplest";
	}

	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.matching.models.WeightingModel#score(double, double)
	 */
	public double score(double tf, double docLength) {
		return score;
	}

	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.matching.models.WeightingModel#score(double, double, double, double, double)
	 */
	public double score(double tf, double docLength, double n_t, double F_t,
			double keyFrequency) {
		return score;
	}

}
