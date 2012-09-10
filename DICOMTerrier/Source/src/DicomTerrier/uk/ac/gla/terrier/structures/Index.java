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
 * The Original Code is Index.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.structures;
import java.io.File;
import java.io.IOException;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.structures.dicom.TagLexicon;

/** 
 * This class encapsulates all the Indexes at retrieval time. 
 * It is loaded by giving a path. Note, if you create other types 
 * of index other than normal and Block, then you'll may
 * need to subclass this object. This class uses CollectionStatistics
 * .getClasses() to determine which classes to use for the Lexicon, 
 * DocumentIndex, DirectIndex &amp; the InvertedIndex. Fully qualified 
 * class names should be saved to the <b>second</b> line of the 
 * CollectionStatistics file (data.log)
 * @author Craig Macdonald &amp; Vassilis Plachouras
 * @version $Revision: 1.1 $ 
*/
public class Index
{
	/** Set to true if loading an index succeeds */
	protected boolean loadSuccess = false;
	/** The direct index to use for quering */
	protected DirectIndex DiI;
	
	/** The document index to use for retrieval */
	protected DocumentIndex DoI;
	
	/** The inverted index to use for retrieval */
	protected InvertedIndex II;
	
	/** The comparable inverted index to use for retrieval */
	protected InvertedIndex CII;
	
	/** The lexicon to use for retrieval */
	protected Lexicon L;
		
	
	/** The taglexicon */
	protected TagLexicon TL;
	
	/** The comparable lexicon */
	protected ComparableLexicon CL; 
	//protected Lexicon CL;
	
	private static Index ind;
	
	/**
	 * Closes the data structures of the index.
	 */
	public void close() {
		DiI.close();
		DoI.close();
		II.close();
		if (CII!=null)
			CII.close();
		if (CL!=null)
			CL.close();
		L.close();
		if (TL!=null)
			TL.close();


	}

	/**
	 * Constructs a new Index object. Don't call this method,
	 * call the createIndex(String) factory method to
	 * construct an Index object.
	 * @param path String the path in which the data structures
	 *		will be created.
	 * @param prefix String the prefix of the files to
	 *		be created.
	 */
	protected Index(String path, String prefix) {
		try{
			CollectionStatistics.initialise();
		}catch(IOException ioe) {}
		if (!(new File(path)).isAbsolute())
			path = ApplicationSetup.makeAbsolute(path, ApplicationSetup.TERRIER_VAR);
		String lexiconFilename = path + ApplicationSetup.FILE_SEPARATOR +
			 prefix + ApplicationSetup.LEXICONSUFFIX;
		String docidFilename = path + ApplicationSetup.FILE_SEPARATOR + 
			prefix + ApplicationSetup.DOC_INDEX_SUFFIX;
		String directFilename = path + ApplicationSetup.FILE_SEPARATOR + 
			prefix + ApplicationSetup.DF_SUFFIX;
		String invertedFilename = path + ApplicationSetup.FILE_SEPARATOR + 
			prefix + ApplicationSetup.IFSUFFIX;
		String tagLexiconFilename = path + ApplicationSetup.FILE_SEPARATOR +
		 	prefix + ApplicationSetup.TAGLEXICONSUFFIX;
		String comparableLexiconFilename = path + ApplicationSetup.FILE_SEPARATOR +
	 		prefix + ApplicationSetup.COMPARABLE_LEXICONSUFFIX;
		String comparableInvertedFilename = path + ApplicationSetup.FILE_SEPARATOR + 
			prefix + ApplicationSetup.COMPARABLE_IFSUFFIX;
		loadIndices(lexiconFilename, docidFilename, directFilename, invertedFilename, tagLexiconFilename, comparableLexiconFilename, comparableInvertedFilename);
	}


	/** Given the filenames of the data structures for the lexicon, 
	  * documnent index, direct index and inverted index, instantiate them
	  * using the class names that were saved in the log file. 
	  * Using CollectionStatistics.getClasses(), <ul>
	  * <li>0: Lexicon class name</li><li>1: Document Index class name</li>
	  * <li>2: Direct Index class name</li><li>3: Inverted Index class name</li></ul>
	  * @see uk.ac.gla.terrier.structures.CollectionStatistics.getClasses() */
	protected void loadIndices(String lexiconFilename, String docidFilename,
		String directFilename, String invertedFilename, String tagLexiconFilename, String comparableLexiconFilename, String comparableInvertedFilename)
	{
		if (!loadSuccess){
			String [] classNames = CollectionStatistics.getClasses();
			/* 0: Lexicon class name
			   1: Document Index class name
			   2: Direct Index class name
			   3: Inverted Index class name
			   4: TagLexicon class name
			   5: ComparableLexicon class name
			*/
			if (classNames == null || classNames.length == 0)
			{
				System.err.println("WARNING: Could not load classNames from CollectionStatistics"+
					" properly");
				return;
			}
	
			//firstly load the Lexicon
			try
			{
				//load the class
				Class lex = Class.forName(classNames[0], false, this.getClass().getClassLoader());
				//get the correct constructor - an Index class in this case
				Class[] params = {String.class};
				Object[] params2 = {lexiconFilename};
				//and instantiate
				L = (Lexicon) (lex.getConstructor(params).newInstance(params2));				
			}
			catch(Exception e)
			{
				System.err.println("Problem loading Lexicon named: "+classNames[0]+" : "+e);
				e.printStackTrace();
				return;
			}
	
	
			//now load the document index
			try
			{
				//load the class
				Class di = Class.forName(classNames[1], false, this.getClass().getClassLoader());
				//get the correct constructor - an Index class in this case
				Class[] params = {String.class};
				Object[] params2 = {docidFilename};
				//and instantiate
				DoI = (DocumentIndex) (di.getConstructor(params).newInstance(params2));
			}
			catch(Exception e)
			{
				System.err.println("Problem loading DocumentIndex named: "+classNames[1]+" : "+e);
				e.printStackTrace();
				return;
			}
	
			//3rdly, the direct index
			try
			{
				//load the class
				Class di = Class.forName(classNames[2], false, this.getClass().getClassLoader());
				//get the correct constructor - an Index class in this case
				Class[] params = {DocumentIndex.class, String.class};
				Object[] params2 = {DoI, directFilename};
				//and instantiate
				DiI = (DirectIndex) (di.getConstructor(params).newInstance(params2));
			}
			catch(Exception e)
			{
				System.err.println("Problem loading DocumentIndex named: "+classNames[1]+" : "+e);
				e.printStackTrace();
				return;
			}
	
			//fourthly the inverted index
			try
			{
				//load the class
				Class ii = Class.forName(classNames[3], false, this.getClass().getClassLoader());
				//get the correct constructor - an Index class in this case
				Class[] params = {Lexicon.class, String.class};
				Object[] params2 = {L, invertedFilename};
				//and instantiate
				II = (InvertedIndex) (ii.getConstructor(params).newInstance(params2));
			}
			catch(Exception e)
			{
				System.err.println("Problem loading DocumentIndex named: "+classNames[1]+" : "+e);
				e.printStackTrace();
				return;
			}
			//lastly load the TagLexicon
			if (classNames.length>4){
				try
				{
					//load the class
					Class tagLex = Class.forName(classNames[4], false, this.getClass().getClassLoader());
					//get the correct constructor - an Index class in this case
					Class[] params = {String.class};
					Object[] params2 = {tagLexiconFilename};
					//and instantiate
					TL = (TagLexicon) (tagLex.getConstructor(params).newInstance(params2));
				}
				catch(Exception e)
				{
					System.err.println("Problem loading Lexicon named: "+classNames[4]+" : "+e);
					e.printStackTrace();
					return;
				}
			}
			//finally load the  Lexicon
			if (classNames.length>5){
				try
				{
					//load the class
					Class clex = Class.forName(classNames[5], false, this.getClass().getClassLoader());
					//get the correct constructor - an Index class in this case
					Class[] params = {String.class};
					Object[] params2 = {comparableLexiconFilename};
					//and instantiate
					CL = (ComparableLexicon) (clex.getConstructor(params).newInstance(params2));
					//CL = (ComparableLexicon) (clex.getConstructor(params).newInstance(params2));
				}
				catch(Exception e)
				{
					System.err.println("Problem loading Lexicon named: "+classNames[5]+" : "+e);
					e.printStackTrace();
					return;
				}
			}
			
			//Load comparable inverted index
			if(classNames.length>6){
				try
				{
					//load the class
					Class cii = Class.forName(classNames[3], false, this.getClass().getClassLoader());
					//get the correct constructor - an Index class in this case
					Class[] params = {Lexicon.class, String.class};
					Object[] params2 = {CL, comparableInvertedFilename};
					//and instantiate
					CII = (InvertedIndex) (cii.getConstructor(params).newInstance(params2));
				}
				catch(Exception e)
				{
					System.err.println("Problem loading Comparable Inverted Index named: "+classNames[6]+" : "+e);
					e.printStackTrace();
					return;
				}
			}
			
			loadSuccess = true;
			
		}
	}

	/**
	 * A default constuctor that creates an instance of the index.
	 */
	protected Index() {
		try{
			CollectionStatistics.initialise();
		}catch (IOException ioe){}
		loadIndices(ApplicationSetup.LEXICON_FILENAME,
			ApplicationSetup.DOCUMENT_INDEX_FILENAME,
			ApplicationSetup.DIRECT_FILENAME,
			ApplicationSetup.INVERTED_FILENAME,
			ApplicationSetup.TAGLEXICON_FILENAME,
			ApplicationSetup.COMPARABLE_LEXICON_FILENAME,
			ApplicationSetup.COMPARABLE_INVERTED_FILENAME	
		);
	}
	/** 
	 * Return the direct index to use for retrieval. 
	 * @return DirectIndex the direct index used for retrieval.
	 */
	public DirectIndex getDirectIndex() { 
		return DiI;
	}
	
	/** 
	 * Return the document index to use for retrieval.
	 * @return DocumentIndex the document index used for retrieval. 
	 */
	public DocumentIndex getDocumentIndex(){
		return DoI;
	}
	
	/** 
	 * Return the inverted index to use for retrieval. 
	 * @return InvertedIndex the inverted index used for retrieval.
	 */
	public InvertedIndex getInvertedIndex() {
		return II;
	}
	
	/** 
	 * Return the lexicon to use for retrieval.
	 * @return Lexicon the lexicon used for retrieval. 
	 */
	public Lexicon getLexicon() {
		return L;
	}
	
	/** 
	 * Return the lexicon to use for retrieval.
	 * @return TagLexicon the tagLexicon used for retrieval. 
	 */
	public TagLexicon getTagLexicon() {
		return TL;
	}
	
	/** 
	 * Return the comparable lexicon to use for retrieval.
	 * @return ComparableLexicon the tagLexicon used for retrieval. 
	 */
	//public ComparableLexicon getComparableLexicon() {
	public ComparableLexicon getComparableLexicon() {
		return CL;
	}
	
	/** 
	 * Return the inverted index to use for retrieval. 
	 * @return InvertedIndex the inverted index used for retrieval.
	 */
	public InvertedIndex getComparableInvertedIndex() {
		return CII;
	}
	
	/**
	 * Factory method for creating an
	 * index. This method should be used in order to
	 * create an index in the applications.
	 * @param path String the path in which the 
	 *		data structures will be created. 
	 * @param prefix String the prefix of the files
	 *		to be created.
	 */
	public static Index createIndex(String path, String prefix) {
		if (!(new File(path)).isAbsolute())
			path = ApplicationSetup.makeAbsolute(path, ApplicationSetup.TERRIER_VAR);
		String lexiconFilename = path + ApplicationSetup.FILE_SEPARATOR + prefix + ApplicationSetup.LEXICONSUFFIX;
		String docidFilename = path + ApplicationSetup.FILE_SEPARATOR + prefix + ApplicationSetup.DOC_INDEX_SUFFIX;
		String directFilename = path + ApplicationSetup.FILE_SEPARATOR + prefix + ApplicationSetup.DF_SUFFIX;
		String invertedFilename = path + ApplicationSetup.FILE_SEPARATOR + prefix + ApplicationSetup.IFSUFFIX;
		
		
		if (! allExists(new String[] {
				lexiconFilename,
				docidFilename,
				directFilename,
				invertedFilename}))
		{
			return null;
		}

		Index i = new Index(path, prefix);
		if (! i.loadSuccess)
			return null;
		return i;
	}
	
	/** 
	 * Factory method for creating an 
	 * index. This method should be used in order to
	 * create an index in the applications.
	 */
	public static Index createIndex() {
		if (! allExists(new String[] {
			ApplicationSetup.LEXICON_FILENAME,
			ApplicationSetup.DOCUMENT_INDEX_FILENAME,
			ApplicationSetup.DIRECT_FILENAME,
			ApplicationSetup.INVERTED_FILENAME,
			ApplicationSetup.COMPARABLE_LEXICON_FILENAME,
			ApplicationSetup.COMPARABLE_INVERTED_FILENAME
		}))
		{
			return null;
		}			

		Index i = new Index();
		if (! i.loadSuccess)
			return null;
		ind = i;
		return i;
	}
	
	/**
	 * Method to get the current index.
	 */
	public static Index getIndex(){
		if (ind == null)
			return createIndex();
		return ind;
	}
	
	private static boolean allExists(String [] files)
	{
		for(int i=0;i<files.length;i++)
		{
			if (!(new File(files[i]).exists()))
				return false;
		}
		return true;
	}
}
