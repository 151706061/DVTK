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
 * The Original Code is StopWords.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.terms;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import java.util.HashSet;
import java.io.IOException;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.FileInputStream;
/** 
 * Implements stopword removal, as a TermPipeline object. 
 * @author Craig Macdonald <craigm{a.}dcs.gla.ac.uk> 
 * @version $Revision: 1.1 $
 */
public class Stopwords implements TermPipeline
{
	/** The next component in the term pipeline. */
	protected TermPipeline next = null;

	/** The hashset that contains all the stop words.*/
	protected HashSet stopWords;
	/** 
	 * Makes a new stopword termpipeline object. The stopwords 
	 * file is loaded from the application setup file, 
	 * under the property <tt>stopwords.filename</tt>.
	 * @param next TermPipeline the next component in the term pipeline.
	 */
	public Stopwords(TermPipeline next)
	{
		this(next, ApplicationSetup.getProperty("stopwords.filename", "stopword-list.txt"));
	}

	/** Makes a new stopword term pipeline object. The stopwords file
	  * is loaded from the filename parameter. If the filename is not absolute, it is assumed
	  * to be in TERRIER_SHARE.
	  * @param next TermPipeline the next component in the term pipeline
	  * @param StopwordsFile The filename of the file to use as the stopwords list.
	  */	
	public Stopwords(TermPipeline next, String StopwordsFile)
	{
		this.next = next;
		loadStopwordsList(StopwordsFile);
	}

	/** Loads the specified stopwords file. Used internally by Stopwords(TermPipelinem, String).
	  * @param stopwordsFilename The filename of the file to use as the stopwords list. */
	public void loadStopwordsList(String stopwordsFilename)
	{
		//get the absolute filename
		stopwordsFilename = ApplicationSetup.makeAbsolute(stopwordsFilename, ApplicationSetup.TERRIER_SHARE);
		try {
			BufferedReader br = new BufferedReader(
				new InputStreamReader(new FileInputStream(stopwordsFilename)));
			stopWords = new HashSet();
			String word;
			while ((word = br.readLine()) != null)
				stopWords.add(word.trim());
			br.close();
		} catch (IOException ioe) {
			System.err.println("Input/Output Exception while reading stop word list. Stack trace follows.");
			ioe.printStackTrace();
			System.exit(1);
		}
	}

	
	/** 
	 * Checks to see if term t is a stopword. If so, then the TermPipeline
	 * is existed. Otherwise, the term is passed on to the next TermPipeline
	 * object.
	 * @param t The term to be checked.
	 */
	public void processTerm(String t)
	{
		if (stopWords.contains(t))			
			next.processTerm(null);
		else
			next.processTerm(t);
	}
}
