package dicomsearch.search;

import java.io.Serializable;

/**
 * This class is used to transfer the query results
 * from the search bean to the search servlet
 *   
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class Results implements Serializable {
	
	private static final long serialVersionUID = -2627286946379756174L;
	private StringBuffer sb;
	private int nrOfResults;
	private int nrOfResultsPerPage;
	private long qTime;
	private String info;
	
	/**
	 * Set the stringbuffer with the text
	 * @param sb Stringbuffer containing restults
	 */
	public void setBuffer(StringBuffer sb){
		this.sb=sb;
	}
	
	/**
	 * Set the totalnumber of results of the query
	 * @param nrOfResults Number of results
	 */
	public void setTotalNumberOfResults(int nrOfResults){
		this.nrOfResults = nrOfResults;
	}
	
	/**
	 * Set the time it took to execute the query
	 * @param qTime Query time
	 */
	public void setQueryTime(long qTime){
		this.qTime = qTime;
	}
	
	/**
	 * Get the results 
	 * @return Stringbuffer
	 */
	public StringBuffer getBuffer(){
		return sb;
	}
	
	/**
	 * Get the number of results
	 * @return number of resutls
	 */
	public int getTotalNrOfResults(){
		return nrOfResults;
	}
	
	/**
	 * Get the query time
	 * @return Query time
	 */
	public long getQueryTime(){
		return qTime;
	}
	
	/**
	 * Set the info message
	 * @param msg The message
	 */
	public void setInfoMessage(String msg){
		info = msg;
	}
	
	/**
	 * Get the info message
	 * @return String the message
	 */
	public String getInfoMessage(){
		return info;
	}
	
	/**
	 * Set the number of results per page
	 * @param resPPage the number of results to show per page
	 */
	public void setResultsPerPage(int resPPage){
		nrOfResultsPerPage = resPPage;
	}
	
	/**
	 * Get the number of results per page
	 * @return int the number of results per page
	 */
	public int getResultsPerPage(){
		return nrOfResultsPerPage;
	}
	
}
