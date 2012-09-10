package dicomxml;

/**
 * This interface is used to display information of an image in the web-interface
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public interface FileInfoReader {

	/**
	 * Get the information for this file
	 * @param fileName The file
	 * @return String with the information
	 */
	public String getInfo(String fileName);
	
	/**
	 * Get the information for this file, related to the query
	 * @param fileName The file
	 * @param query The query
	 * @return String with the information
	 */
	public String getInfo(String fileName, String query);
	
	
	
}
