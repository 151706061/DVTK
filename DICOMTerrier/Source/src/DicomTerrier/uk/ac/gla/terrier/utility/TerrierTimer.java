/*
 * Terrier - Terabyte Retriever 
 * Webpage: http://ir.dcs.gla.ac.uk/terrier 
 * Contact: terrier{a.}dcs.gla.ac.uk
 * University of Glasgow - Department of Computing Science
 * Information Retrieval Group
 * 
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
 * the License for the specific language governing rights and limitations
 * under the License.
 *
 * The Original Code is TerrierTimer.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Ben He <ben{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.utility;
/**
 * This class implements a timer.
 */
public class TerrierTimer {
	/** The starting system time in millisecond. */ 
	private long startingTime;
	
	/** The processing time in minutes. */
	private int minutes;
	
	/** The processing time in seconds. */
	private int seconds;
	
	/** The total number of items to process in a task. */
	protected double total;
	
	/**
	 * Default constructor.
	 *
	 */
	public TerrierTimer(){
		this.start();
	}
	
	/** Start the timer.
	 */
	public void start(){
		this.startingTime = System.currentTimeMillis();
		this.minutes = 0;
		this.seconds = 0;
	}
	
	/**
	 * Reset the timer.
	 *
	 */
	public void restart(){
		this.start();
	}
	/**
	 * Compute the processing time.
	 *
	 */
	public void setBreakPoint(){
		long processingEnd = System.currentTimeMillis();
		long processingTime = (processingEnd - this.startingTime) / 1000;
		minutes = (int) (processingTime / 60.0d);
		seconds = (int) (processingTime % 60.0d);
	}
	/** Get the processing time in minutes. */
	public int getMinutes(){
		return this.minutes;
	}
	/** Set the overall quantitative workload of the task. */
	public void setTotalNumber(double total){
		this.total = total;
	}
	/**
	 * Estimate the remaining time.
	 * @param finished The quantitative finished workload.
	 */
	public void setRemainingTime(double finished){
		long processingEnd = System.currentTimeMillis();
		long processingTime = (processingEnd - this.startingTime) / 1000;
		processingTime *= total/finished - 1;
		minutes = (int) (processingTime / 60.0d);
		seconds = (int) (processingTime % 60.0d);
	}
	
	/** Get the processing time in seconds. */
	public int getSeconds(){
		return this.seconds;
	}
	/** Get a string summarising the processing/remaining time in minutes and seconds. */
	public String toStringMinutesSeconds(){
		return getMinutes() + " minutes " + getSeconds() + " seconds";
	}
}
