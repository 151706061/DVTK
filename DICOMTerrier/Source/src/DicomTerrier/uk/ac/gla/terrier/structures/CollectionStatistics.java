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
 * The Original Code is CollectionStatistics.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 *   Craig Macdonald <craig{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * This class provides basic statistics for the indexed
 * collection of documents, such as the average length of documents,
 * or the total number of documents in the collection. <br />
 * After indexing, statistics are saved in the PREFIX.log file, along
 * with the classes that should be used for the Lexicon, the DocumentIndex,
 * the DirectIndex and the InvertedIndex. This means that an index knows
 * how it was build and how it should be opened again.
 *
 * @author Gianni Amati, Vassilis Plachouras, Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class CollectionStatistics {
	/** The total number of documents in the collection.*/
	protected static int numberOfDocuments;
	
	/** The total number of tokens in the collection.*/
	protected static long numberOfTokens;
	/** 
	 * The total number of pointers in the inverted file.
	 * This corresponds to the sum of the document frequencies for
	 * the terms in the lexicon.
	 */
	protected static long numberOfPointers;
	/**
	 * The total number of unique terms in the collection.
	 * This corresponds to the number of entries in the lexicon.
	 */
	protected static long numberOfUniqueTerms;
	/**
	 * The average length of a document in the collection.
	 */
	protected static double averageDocumentLength;

	/** The total number of comparable documents in the collection.*/
	protected static int numberOfCompDocuments;
	
	/** The total number of comparable tokens in the collection.*/
	protected static long numberOfCompTokens;
	/** 
	 * The total number of pointers in the comparable inverted file.
	 * This corresponds to the sum of the document frequencies for
	 * the terms in the comparable lexicon.
	 */
	protected static long numberOfCompPointers;
	/**
	 * The total number of unique comparable terms in the collection.
	 * This corresponds to the number of entries in the lexicon.
	 */
	protected static long numberOfCompUniqueTerms;
	/**
	 * The average length of a (comparable) document in the collection.
	 */
	protected static double averageCompDocumentLength;
	
	/**
	 * The maximum length of a tag structure pattern.
	 */
	protected static int maxTagStructureLength;
	
	
	/**
	 * New log files also contain the class names of the indices that should be
	 * used to open this Index.
	 */
	protected static String[] Classes;

	/** If we fail to find a classes line in the .log files, then use these as the
	  * default classes. */
	protected static final String[] defaultClasses = 
		{ "uk.ac.gla.terrier.structures.Lexicon", 
			"uk.ac.gla.terrier.structures.DocumentIndexEncoded", 
			"uk.ac.gla.terrier.structures.DirectIndex", 
			"uk.ac.gla.terrier.structures.InvertedIndex"};

	static {
		try {
			initialise();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while initialising the CollectionStatistics class. " +
				"Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	
	/**
	 * Reads the collection statistics from a file.
	 * @throws IOException throws an input output exception
	 *         if there is any problem while reading the file.
	 */
	public static void initialise() throws IOException {
		File file = new File(ApplicationSetup.LOG_FILENAME);
		if (file.exists()) {
			BufferedReader br = new BufferedReader(new FileReader(file));
			String inputLine = br.readLine();
			String[] stats = inputLine.split(" +");
			numberOfDocuments = Integer.parseInt(stats[0]);
			numberOfTokens = Long.parseLong(stats[1]);
			numberOfUniqueTerms = Long.parseLong(stats[2]);
			numberOfPointers = Long.parseLong(stats[3]);
				
			if (stats.length>4){
				numberOfCompDocuments = Integer.parseInt(stats[4]);
				numberOfCompTokens = Long.parseLong(stats[5]);
				numberOfCompUniqueTerms = Long.parseLong(stats[6]);
				numberOfCompPointers = Long.parseLong(stats[7]);
			}
				
			if (numberOfDocuments != 0)
				averageDocumentLength =
					(1.0D * numberOfTokens + numberOfCompTokens) / (1.0D * numberOfDocuments);
			else
				averageDocumentLength = 0.0D;

			inputLine = br.readLine();
			if (inputLine != null)
			{
				Classes = inputLine.split(" +");
				inputLine = br.readLine();
				if (inputLine != null && inputLine.length()>1)
					maxTagStructureLength = Integer.parseInt(inputLine);
			}
			else
			{
				Classes = defaultClasses;	
			}
			
			br.close();
		}
	}
	
	/**
	 * Given the collection statistics, it stores them in a file with 
	 * a standard name.
	 * 
	 * @param docs The number of documents in the collection
	 * @param tokens The number of tokens in the collection
	 * @param terms The number of terms in the collection
	 * @param pointers The number of pointers in the collection
	 */
	public static void createCollectionStatistics(
		int docs,
		long tokens,
		long terms,
		long pointers, String[] classes) {

		try {
			PrintWriter pw = new PrintWriter(new FileWriter(ApplicationSetup.LOG_FILENAME));
			numberOfDocuments = docs;
			numberOfTokens = tokens;
			numberOfUniqueTerms = terms;
			numberOfPointers = pointers;
			if (numberOfDocuments != 0)
				averageDocumentLength =
					(1.0D * numberOfTokens) / (1.0D * numberOfDocuments);
			else
				averageDocumentLength = 0.0D;
			pw.print(docs);
			pw.print(" ");
			pw.print(tokens);
			pw.print(" ");
			pw.print(terms);
			pw.print(" ");
			pw.println(pointers);
			
			//now write out the classes string
			for(int i=0;i<classes.length;i++)
			{
				pw.print(classes[i]);
				//no space at the end of the line
				if (i < classes.length -1)
					pw.print(" ");
			}
			pw.println("");
			pw.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while initialising the " +
				"CollectionStatistics class. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	
	/**
	 * Given the collection statistics, it stores them in a file with 
	 * a standard name.
	 * 
	 * @param docs The number of documents in the collection
	 * @param tokens The number of tokens in the collection
	 * @param terms The number of terms in the collection
	 * @param pointers The number of pointers in the collection
	 */
	public static void createAdvancedCollectionStatistics(
		int docs,
		long tokens,
		long terms,
		long pointers, 
		int compDocs,
		long compTokens,
		long compTerms,
		long compPointers,
		String[] classes) {

		try {
			PrintWriter pw = new PrintWriter(new FileWriter(ApplicationSetup.LOG_FILENAME));
			numberOfDocuments = docs;
			numberOfTokens = tokens;
			numberOfUniqueTerms = terms;
			numberOfPointers = pointers;
			
			numberOfCompDocuments = compDocs;
			numberOfCompTokens = compTokens;
			numberOfCompUniqueTerms = compTerms;
			numberOfCompPointers = compPointers;
			
			if (numberOfDocuments != 0)
				averageDocumentLength =
					(1.0D * numberOfTokens + numberOfCompTokens) / (1.0D * numberOfDocuments);
			else
				averageDocumentLength = 0.0D;
			
			pw.print(docs);
			pw.print(" ");
			pw.print(tokens);
			pw.print(" ");
			pw.print(terms);
			pw.print(" ");
			pw.print(pointers);
			pw.print(" ");
			pw.print(compDocs);
			pw.print(" ");
			pw.print(compTokens);
			pw.print(" ");
			pw.print(compTerms);
			pw.print(" ");
			pw.println(compPointers);
			
			//now write out the classes string
			for(int i=0;i<classes.length;i++)
			{
				pw.print(classes[i]);
				//no space at the end of the line
				if (i < classes.length -1)
					pw.print(" ");
			}
			pw.println("");
			pw.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while initialising the " +
				"CollectionStatistics class. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
	
	/**
	 * Returns the documents' average length.
	 * @return the average length of the documents in the collection.
	 */
	public static double getAverageDocumentLength() {
		return averageDocumentLength;
	}
	/**
	 * Returns the total number of documents in the collection.
	 * @return the total number of documents in the collection
	 */
	public static int getNumberOfDocuments() {
		return numberOfDocuments;
	}
	/**
	 * Returns the total number of pointers in the collection.
	 * @return the total number of pointers in the collection
	 */
	public static long getNumberOfPointers() {
		return numberOfPointers;
	}
	/**
	 * Returns the total number of tokens in the collection.
	 * @return the total number of tokens in the collection
	 */
	public static long getNumberOfTokens() {
		return numberOfTokens;
	}
	/**
	 * Returns the total number of unique terms in the lexicon.
	 * @return the total number of unique terms in the lexicon
	 */
	public static long getNumberOfUniqueTerms() {
		return numberOfUniqueTerms;
	}

	/**
	 * Returns the documents' average length.
	 * @return the average length of the documents in the collection.
	 */
	public static double getAverageCompDocumentLength() {
		return averageCompDocumentLength;
	}
	/**
	 * Returns the total number of 'comparable' documents in the collection.
	 * Just for debugging
	 * @return the total number of 'comparable' documents in the collection
	 */
	public static int getNumberOfCompDocuments() {
		return numberOfCompDocuments;
	}
	/**
	 * Returns the total number of comparable pointers in the collection.
	 * @return the total number of comparable pointers in the collection
	 */
	public static long getNumberOfCompPointers() {
		return numberOfCompPointers;
	}
	/**
	 * Returns the total number of comparable tokens in the collection.
	 * @return the total number of comparable tokens in the collection
	 */
	public static long getNumberOfCompTokens() {
		return numberOfCompTokens;
	}
	/**
	 * Returns the total number of unique comparable terms in the lexicon.
	 * @return the total number of unique comparable terms in the lexicon
	 */
	public static long getNumberOfCompUniqueTerms() {
		return numberOfCompUniqueTerms;
	}
	
	/**
	 * Returns the classes line given in the log file. Used by the
	 * Index to determine which classes it should load for this Index.
	 */
	public static String[] getClasses() {
		return Classes;
	}	
	
	/**
	 * Returns the maximum length of a tag structure pattern. .
	 */
	public static int getTagStructureMaxLength() {
		return maxTagStructureLength;
	}	
	
	/**
	 * Add the maximum length of a tag structure pattern to the statistics.
	 */
	public static void addTagStructureMaxlength(int length){
		try{
			PrintWriter pw = new PrintWriter(new FileWriter(ApplicationSetup.LOG_FILENAME, true));
			pw.println(length);
			pw.close();
		} catch (IOException ioe) {
			System.err.println(
				"Input/Output exception while initialising the " +
				"CollectionStatistics class. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}
}
