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
 * The Original Code is ApplicationSetup.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Gianni Amati <gba{a.}fub.it> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 *   Ben He <ben{a.}dcs.gla.ac.uk>
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.utility;
import java.io.File;
import java.io.FileInputStream;
import java.util.Properties;

import javax.naming.NamingException;
import javax.naming.InitialContext;
import javax.naming.Context;


/** 
 * <p>This class retrieves and provides access
 * to all the constants and parameters for
 * the system. When it is statically initialized,
 * it loads the properties file specified by the system property
 * <tt>terrier.setup</tt>. If this is not specified, then the default value is 
 * the value of the <tt>terrier.home</tt> system property, appended by <tt>etc/terrier.properties</tt>.
 * <br/>
 * eg <tt>java -D terrier.home=$TERRIER_HOME -Dterrier.setup=$TERRIER_HOME/etc/terrier.properties TrecTerrier </tt>
 * </p><p>
 * <b>System Properties used:</b>
 * <table><tr><td>
 * <tt>terrier.setup</tt></td><td>Specifies where the terrier.properties file can be found.
 * </td></tr>
 * <tr><td><tt>terrier.home</tt></td><td>Specified where Terrier has been installed, if the terrier.properties
 * file cannot be found, or the terrier.properties file does not specify the <tt>terrier.home</tt> in it.
 * <br><b>NB:</b>In the future, this may further default to $TERRIER_HOME from the environment.
 * </td><tr><td><tt>file.separator</tt></td><td>What separates directory names in this platform. Set automatically by Java</td></tr>
 * <tr><td><tt>line.separator</tt></td><td>What separates lines in a file on this platform. Set automatically by Java</td>
 * </table>
 * </p><p>
 * In essence, for Terrier to function properly, you need to specify one of the following on the command line:
 * <ul><li><tt>terrier.setup</tt> pointing to a terrier.properties file containing a <tt>terrier.home</tt> value.
 * </li>OR<li><tt>terrier.home</tt>, and Terrier will use a properties file at etc/terrier.properties, if it finds one.</li></ul>
 * </p>
 * @author Gianni Amati, Vassilis Plachouras, Ben He, Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class ApplicationSetup {
	/** 
	 * The properties object in which the 
	 * properties from the file are read.
	 */
	protected static Properties appProperties;
	//Operating system dependent constants
	
	/**
	 * The file separator used by the operating system. Defaults to
	 * the system property <tt>file.separator</tt>.
	 */
	public static String FILE_SEPARATOR = System.getProperty("file.separator");
	
	/**
	 * The new line character used by the operating system. Defaults to
	 * the system property <tt>line.separator</tt>.
	 */
	public static String EOL = System.getProperty("line.separator");
	//Application specific constants. Should be specified in the properties file.
	
	/**
	 * The directory under which the application is installed.
	 * It corresponds to the property <tt>terrier.home</tt> and it
	 * should be set in the properties file, or as a property on the
	 * command line.
	 */
	public static final String TERRIER_HOME; 
	
	/**
	 * The directory under which the configuration files 
	 * of Terrier are stored. The corresponding property is 
	 * <tt>terrier.etc</tt> and it should be set
	 * in the properties file. If a relative path is given, 
	 * TERRIER_HOME will be prefixed. 
	 */
	public static final String TERRIER_ETC;
	/**
	 * The name of the directory where installation independant
	 * read-only data is stored. Files like stopword lists, and
	 * example and testing data are examples. The corresponding
	 * property is <tt>terrier.share</tt> and its default value is
	 * <tt>share</tt>. If a relative path is given, then TERRIER_HOME
	 * will be prefixed. */
	public static final String TERRIER_SHARE;
	/**
	 * The name of the directory where the data structures
	 * and the output of Terrier are stored. The corresponding 
	 * property is <tt>terrier.var</tt> and its default value is 
	 * <tt>var</tt>. If a relative path is given, 
	 * TERRIER_HOME will be prefixed.
	 */
	public static final String TERRIER_VAR;
	
	/**
	 * The name of the directory where the inverted
	 * file and other data structures are stored.
	 * The default value is InvFileCollection but it
	 * can be overriden with the property <tt>terrier.index.path</tt>.
	 * If a relative path is given, TERRIER_VAR will be prefixed.
	 */
	public static String TERRIER_INDEX_PATH;
	
	/**
	 * The name of the file that contains the
	 * list of resources to be processed during indexing.
	 * The contents of this file are collection implementation
	 * dependent. For example, for a TREC collection, this file
	 * must contain the list of files to index.
	 * The corresponding property is <tt>collection.spec</tt>
	 * and by default its value is <tt>collection.spec</tt>.
	 * If a relative path is given, TERRIER_ETC will be prefixed.
	 */
	public static String COLLECTION_SPEC;
	
	
	
	//TREC SPECIFIC setup
	/**
	 * The name of the directory where the results
	 * are stored. The corresponding property is 
	 * <tt>trec.results</tt> and the default value is 
	 * <tt>results</tt>. If a relative path is given, 
	 * TERRIER_VAR will be prefixed.
	 */
	public static String TREC_RESULTS;
	/**
	 * The name of the file that contains a list of
	 * files where queries are stored. The corresponding property
	 * is <tt>trec.topics.list</tt> and the default value
	 * is <tt>trec.topics.list</tt>. If a relative path is given, 
	 * TERRIER_ETC will be prefixed.
	 */
	public static String TREC_TOPICS_LIST;
	
	/**
	 * The name of the file that contains a list of qrels files 
	 * to be used for evaluation. The corresponding property is 
	 * <tt>trec.qrels</tt> and its default value is <tt>trec.qrels</tt>.  
	 * If a relative path is given, TERRIER_ETC will be prefixed.
	 */
	public static String TREC_QRELS;
	
	/** 
	 * The suffix of the files, where the results are stored.
	 * It corresponds to the property <tt>trec.results.suffix</tt>
	 * and the default value is <tt>.res</tt>. 
	 */
	public static String TREC_RESULTS_SUFFIX;
	
	/** 
	 * The filename of the file that contains 
	 * the weighting models to be used. The corresponding
	 * property is <tt>trec.models</tt> and the default value
	 * is <tt>trec.models</tt>. If a relative path is given, then
	 * it is prefixed with TERRIER_ETC.
	 */
	public static String TREC_MODELS;
	//end of TREC specific section
		
	/**
	 * The suffix of the inverted file. The corresponding
	 * property is <tt>if.suffix</tt> and by default
	 * the value of this property is <tt>.if</tt>
	 */
	public static String IFSUFFIX;
	
	/**
	 * The suffix of the tag inverted file. The corresponding
	 * property is <tt>tif.suffix</tt> and by default
	 * the value of this property is <tt>.tif</tt>
	 */
	public static String TIFSUFFIX;
	
	/**
	 * The suffix of the comparable inverted file. The corresponding
	 * property is <tt>comparable.if.suffix</tt> and by default
	 * the value of this property is <tt>.compif</tt>
	 */
	public static String COMPARABLE_IFSUFFIX;
	
	/**
	 * The suffix of the file that contains the
	 * lexicon. The corresponding property is 
	 * <tt>lexicon.suffix</tt> and by default 
	 * the value of this property is <tt>.lex</tt>
	 */
	public static String LEXICONSUFFIX;
	
	/**
	 * The suffix of the file that contains the
	 * lexicon. The corresponding property is 
	 * <tt>taglexicon.suffix</tt> and by default 
	 * the value of this property is <tt>.taglex</tt>
	 */
	public static String TAGLEXICONSUFFIX;
	
	/**
	 * The suffix of the file that contains the
	 * term-tag lexicon. The corresponding property is 
	 * <tt>termtaglexicon.suffix</tt> and by default 
	 * the value of this property is <tt>.ttlex</tt>
	 */
	public static String TERMTAGLEXICONSUFFIX;
	
	/**
	 * The suffix of the file that contains the
	 * document index. The corresponding property
	 * is <tt>doc.index.suffix</tt> and by default 
	 * the value of this property is <tt>.docid</tt>
	 */
	public static String DOC_INDEX_SUFFIX;
	
	/**
	 * The suffix of the file that contains the comparable
	 * document index. The corresponding property
	 * is <tt>comparable.doc.index.suffix</tt> and by default 
	 * the value of this property is <tt>.compdocid</tt>
	 */
	public static String COMPARABLE_DOC_INDEX_SUFFIX;
	
	/** 
	 * The suffix of the lexicon index file
	 * that contains the offset of each term 
	 * in the lexicon. The corresponding 
	 * property is <tt>lexicon.index.suffix</tt> and
	 * by default its value is .lexid.
	 */
	public static String LEXICON_INDEX_SUFFIX;
	
	/** 
	 * The suffix of the lexicon index file
	 * that contains the offset of each term 
	 * in the taglexicon. The corresponding 
	 * property is <tt>taglexicon.index.suffix</tt> and
	 * by default its value is .taglexid.
	 */
	public static String TAGLEXICON_INDEX_SUFFIX;
	
	/**
	 * The suffix of the file that contains 
	 * the collection statistics. It corresponds
	 * to the property <tt>log.suffix</tt> and 
	 * by default the value of this property is <tt>.log</tt>
	 */
	public static String LOG_SUFFIX;
	/**
	 * The suffix of the direct index. It corresponds
	 * to the property <tt>df.suffix</tt> and by default
	 * the value of this property is <tt>.df</tt>
	 */
	public static String DF_SUFFIX;
	
	/**
	 * The suffix of the comparable direct index. It corresponds
	 * to the property <tt>comparable.df.suffix</tt> and by default
	 * the value of this property is <tt>.compdf</tt>
	 */
	public static String COMPARABLE_DF_SUFFIX;
	
	/**
	 * The suffic of the comparable lexicon index It corresponds
	 * to the property <tt>lexicon.comp.index.suffix</tt> and by default
	 * the value of this property is <tt>.complexid</tt>
	 */	
	public static String COMPARABLE_LEXICON_INDEX_SUFFIX;
	
	/**
	 * The suffix of the file that contains the
	 * comparable lexicon. The corresponding property is 
	 * <tt>lexicon.comp.suffix</tt> and by default 
	 * the value of this property is <tt>.complex</tt>
	 */
	public static String COMPARABLE_LEXICONSUFFIX;
	
	/**
	 * The prefix of the temporary merged files, 
	 * which are created during merging the 
	 * lexicon files. It corresponds to the property 
	 * <tt>merge.prefix</tt> and the default value is <tt>MRG_</tt>.
	 */
	public static String MERGE_PREFIX;
	
	/**
	 * A progresive number which is assigned to the 
	 * temporary lexicon files built during the indexing.
	 * It is used to keep track of the order with which
	 * the temporary files were created. It corresponds to 
	 * the property <tt>merge.temp.number</tt> and the default value
	 * is <tt>100000</tt>
	 */
	public static int MERGE_TEMP_NUMBER;
	
	/**
	 * A progresive number which is assigned to the 
	 * temporary lexicon files built during the indexing.
	 * It is used to keep track of the order with which
	 * the temporary files were created. It corresponds to 
	 * the property <tt>tag.merge.temp.number</tt> and the default value
	 * is <tt>100000</tt>
	 */
	public static int TAGMERGE_TEMP_NUMBER;
	
	
	/**
	 * The number of documents to be processed as a group during indexing.
	 * For each such group of documents, a temporary lexicon is built,
	 * and after indexing, all temporary lexicons are merged in order to 
	 * create a single lexicon. It corresponds to the property 
	 * <tt>bundle.size</tt> and the default value is <tt>2000</tt>.
	 */
	public static int BUNDLE_SIZE;
	
	/**
	 * The number of bytes used to store a term, or a 
	 * document number. It corresponds to the property
	 * <tt>string.byte.length</tt> and the default value is 20.
	 * TODO at the moment, both indexed terms and document numbers
	 * have the same maximum length. This should change so that 
	 * the document numbers could have a different maximum length
	 * than the indexed terms.
	 */
	public static int STRING_BYTE_LENGTH;
	
	/** 
	 * Ignore or not empty documents. That is, if it is true, then a document 
	 * that does not contain any terms will have a corresponding entry in the 
	 * .docid file and the total number of documents in the statistics will be
	 * the total number of documents in the collection, even if some of them 
	 * are empty. It corresponds to the property <tt>ignore.empty.documents</tt>
	 * and the default value is false.
	 */
	public static boolean IGNORE_EMPTY_DOCUMENTS;
	/** 
	 * The prefix of the data structures' filenames. 
	 * It corresponds to the property <tt>terrier.index.prefix</tt>
	 * and the default value is <tt>data</tt>.
	 */
	public static String TERRIER_INDEX_PREFIX;
	
	/** The filename of the inverted file.*/
	public static String INVERTED_FILENAME;
	/** The filename of the inverted tag file.*/
	public static String INVERTED_TAG_FILENAME;
	/** The filename of the comparable inverted file.*/
	public static String COMPARABLE_INVERTED_FILENAME;
	/** The filename of the direct file.*/
	public static String DIRECT_FILENAME;
	/** The filename of the comparable direct file.*/
	public static String COMPARABLE_DIRECT_FILENAME;
	/** The filename of the document index.*/
	public static String DOCUMENT_INDEX_FILENAME;
	/** The filename of the comparable document index.*/
	public static String COMPARABLE_DOCUMENT_INDEX_FILENAME;
	/** The filename of the lexicon file.*/
	public static String LEXICON_FILENAME;
	/** The filename of the lexicon index file.*/
	public static String LEXICON_INDEX_FILENAME;
	/** The filename of the tag lexicon file.*/
	public static String TAGLEXICON_FILENAME;
	/** The filename of the term-tag lexicon file.*/
	public static String TERMTAGLEXICON_FILENAME;
	/** The filename of the tag lexicon index file.*/
	public static String TAGLEXICON_INDEX_FILENAME;
	/** The filename of the comparable lexicon file.*/
	public static String COMPARABLE_LEXICON_FILENAME;
	/** The filename of the comparable lexicon index file.*/
	public static String COMPARABLE_LEXICON_INDEX_FILENAME;
	
	/** The filename of the log (statistics) file.*/
	public static String LOG_FILENAME;
	
	//query expansion properties
	/** 
	 * The number of terms added to the original query. 
	 * The corresponding property is <tt>expansion.terms</tt>
	 * and the default value is <tt>10</tt>.
	 */
	public static int EXPANSION_TERMS;
		
	/**
	 * The number of top ranked documents considered for 
	 * expanding the query. The corresponding property is 
	 * <tt>expansion.documents</tt> and the default value is <tt>3</tt>.
	 */
	public static int EXPANSION_DOCUMENTS;
	
	/**
	 * The name of the file which contains the query expansion
	 * methods used. The corresponding property is 
	 * <tt>expansion.models</tt> and the default
	 * value is <tt>qemodels</tt>. If a relative path is given, 
	 * it is prefixed with TERRIER_ETC.
	 */
	public static String EXPANSION_MODELS;
	//block related properties
	/** 
	 * The size of a block of terms in a document.
	 * The corresponding property is block.size
	 * and the default value is 1.
	 */
	public static int BLOCK_SIZE;
	
	/**
	 * The maximum number of blocks in a document.
	 * The corresponding property is <tt>max.blocks</tt>
	 * and the default value is 100000.
	 */
	public static int MAX_BLOCKS;
	
	/** 
	 * Specifies whether block information will 
	 * be used for indexing. The corresponding property is
	 * <tt>block.indexing</tt> and the default value is false.
	 * The value of this property cannot be modifed after
	 * the index of a collection has been built.
	 */
	public static boolean BLOCK_INDEXING = false;
	
	/** 
	 * Specifies whether tag information will 
	 * be used for indexing. The corresponding property is
	 * <tt>tag.indexing</tt> and the default value is false.
	 * The value of this property cannot be modifed after
	 * the index of a collection has been built.
	 */
	public static boolean TAG_INDEXING = false;
	
	/** 
	 * Specifies whether the index will be split up during indexing. 
	 * The corresponding property is <tt>split.indexing</tt> and the default value is false.
	 * The value of this property cannot be modifed after
	 * the index of a collection has been built.
	 */
	public static boolean SPLIT_INDEXING = false;
	
	/** 
	 * Specifies whether fields will be used for querying. 
	 * The corresponding property is <tt>field.querying</tt> and 
	 * the default value is false.
	 */
	public static boolean FIELD_QUERYING = false;
	
	/** 
	 * Specifies the treshold value for the document frequency for a comparable term,
	 * to be in the inverted tag index.
	 * The corresponding property is <tt>inverttag.treshold</tt> and 
	 * the default value is 2000.
	 */
	public static int INVERTTAG_TRESHOLD;
	
	
	
	/* 
	 * Specifies whether block information will 
	 * be used for querying. The property is 
	 * <tt>block.querying</tt> and the default 
	 * value is false. 
	 */
	//public static boolean BLOCK_QUERYING = false;

	static {
		boolean useContext = new Boolean(System.getProperty("terrier.usecontext", "false")).booleanValue();

		appProperties = new Properties();
		String propertiesFile = null;
		String terrier_home = null;
		try {
			if (useContext)
			{
				 //
				Context initCtx = null; Context envCtx = null;
				try{
					initCtx = (Context)( new InitialContext());
					envCtx = (Context) initCtx.lookup("java:comp/env");
				}catch(NamingException ne) {
					System.err.println("NamingException loading an InitialContext or EnvironmentContext : "+ne);
					System.exit(1);
				}
				try{
					terrier_home = (String)envCtx.lookup("terrier.home");
				}catch(NamingException ne) {
					System.err.println("NamingException finding terrier variables from envCtx : "+ne);
				}
				try{
					propertiesFile = (String)envCtx.lookup("terrier.setup");
				}catch(NamingException ne) {
					System.err.println("NamingException finding terrier variables from envCtx : "+ne);			
				}
				if (propertiesFile == null)
					propertiesFile = "C:\\Terrier" +FILE_SEPARATOR+"etc"+FILE_SEPARATOR+"terrier.properties";
				
			}
			else
			{
				terrier_home = System.getProperty("terrier.home", "C:\\DicomTerrier");
				propertiesFile = System.getProperty("terrier.setup",
					terrier_home +FILE_SEPARATOR+"etc"+FILE_SEPARATOR+"terrier.properties");
			}
	
			//if systen property terrier.setup is specified, then it is 
			//assumed that the properties file is at ./etc/terrier.properties
			System.out.println("Loading properties file " + propertiesFile);
			FileInputStream in = new FileInputStream(propertiesFile);
			appProperties.load(in);			
			in.close();
		} catch (java.io.FileNotFoundException fnfe) {
			System.out.println("WARNING: The file terrier.properties was not found at location "+propertiesFile);
			System.out.println(" Assuming the value of terrier.home from the corresponding system property.");
		} catch (java.io.IOException ioe) {
			System.err.println(
				"Input/Output Exception during initialization of ");
			System.err.println("uk.ac.gla.terrier.utility.ApplicationSetup: "+ioe);
			System.err.println("Stack trace follows.");
			ioe.printStackTrace();
		}
		
		/* 
		 * The property terrier.home does not have a default value, so it has
		 * to be specified by the user in the terrier.properties file. If there
		 * is no terrier.properties specified, then we try to read a value from 
		 * the system property terrier.home. Ideally, the value of terrier.home
		 * would be $ENV{TERRIER_HOME} but java geniuses, in their infinite wisdom
		 * have deprecated System.getEnv() in Java 1.4. 
		 */
		TERRIER_HOME = appProperties.getProperty("terrier.home", "C:\\DicomTerrier");
		if (TERRIER_HOME.equals("")) {
			System.err.println("Please ensure that the property terrier.home");
			System.err.println("is specified in the file terrier.properties,");
			System.err.println("or as a system property in the command line.");
		}
		TERRIER_ETC = makeAbsolute( appProperties.getProperty("terrier.etc","etc"), TERRIER_HOME);
		TERRIER_VAR = makeAbsolute( appProperties.getProperty("terrier.var","var"), TERRIER_HOME);
		TERRIER_SHARE = makeAbsolute( appProperties.getProperty("terrier.share", "share"), TERRIER_HOME);
		TERRIER_INDEX_PATH = makeAbsolute(appProperties.getProperty("terrier.index.path", "index"), TERRIER_VAR); 
		TERRIER_INDEX_PREFIX = appProperties.getProperty("terrier.index.prefix", "data");
				
		//TREC specific
		TREC_TOPICS_LIST = makeAbsolute( appProperties.getProperty("trec.topics.list","trec.topics.list"), TERRIER_ETC);
		TREC_QRELS = makeAbsolute( appProperties.getProperty("trec.qrels","trec.qrels"), TERRIER_ETC);
		TREC_RESULTS = makeAbsolute(appProperties.getProperty("trec.results", "results"), TERRIER_VAR);
		TREC_MODELS = makeAbsolute(appProperties.getProperty("trec.models", "trec.models"), TERRIER_ETC);
		TREC_RESULTS_SUFFIX = appProperties.getProperty("trec.results.suffix", ".res");
			
		//The following properties specify the filenames and suffixes
		COLLECTION_SPEC = makeAbsolute(appProperties.getProperty("collection.spec", "collection.spec"), TERRIER_ETC);
	
		IFSUFFIX = appProperties.getProperty("if.suffix", ".if");
		TIFSUFFIX = appProperties.getProperty("tif.suffix", ".tif");
		COMPARABLE_IFSUFFIX = appProperties.getProperty("comparable.if.suffix", ".compif");
		LEXICONSUFFIX = appProperties.getProperty("lexicon.suffix", ".lex");
		LEXICON_INDEX_SUFFIX = appProperties.getProperty("lexicon.index.suffix", ".lexid");
		TAGLEXICONSUFFIX = appProperties.getProperty("taglexicon.suffix", ".taglex");
		TERMTAGLEXICONSUFFIX = appProperties.getProperty("termtaglexicon.suffix", ".ttlex");
		TAGLEXICON_INDEX_SUFFIX = appProperties.getProperty("taglexicon.index.suffix", ".taglexid");
		DOC_INDEX_SUFFIX = appProperties.getProperty("doc.index.suffix", ".docid");
		COMPARABLE_DOC_INDEX_SUFFIX = appProperties.getProperty("comparable.doc.index.suffix", ".compdocid");
		LOG_SUFFIX = appProperties.getProperty("log.suffix", ".log");
		DF_SUFFIX = appProperties.getProperty("df.suffix", ".df");
		COMPARABLE_DF_SUFFIX = appProperties.getProperty("comparable.df.suffix", ".compdf");
		
		COMPARABLE_LEXICONSUFFIX = appProperties.getProperty("lexicon.comp.suffix", ".complex");
		COMPARABLE_LEXICON_INDEX_SUFFIX = appProperties.getProperty("lexicon.comp.index.suffix", ".complexid");
		
		//the following two properties are related to the indexing of 
		//documents. The prefix mergepref and and the number prog.nr 
		//specify the names of the temporary lexicon created 
		//during creating a global lexicon.
		MERGE_PREFIX = appProperties.getProperty("merge.prefix", "MRG_");
		MERGE_TEMP_NUMBER = (new Integer(appProperties.getProperty("merge.temp.number", "100000"))).intValue();
		TAGMERGE_TEMP_NUMBER = (new Integer(appProperties.getProperty("tag.merge.temp.number", "100000"))).intValue();
		
		//if a document is empty, that is it does not contain any terms, 
		//we have the option to add it to the index, or not. By default, 
		//empty documents are added to the index.
		IGNORE_EMPTY_DOCUMENTS = (new Boolean(appProperties.getProperty("ignore.empty.documents", "false"))).booleanValue();
		
		//During the indexing process, we process and create temporary structures
		//for bundle.size files.
		BUNDLE_SIZE = (new Integer(appProperties.getProperty("bundle.size", "2500"))).intValue();
		//the maximum number of bytes used to store a term, or a document number.
		STRING_BYTE_LENGTH = (new Integer(appProperties.getProperty("string.byte.length", "20"))).intValue();
		//query expansion properties
		EXPANSION_TERMS = (new Integer(appProperties.getProperty("expansion.terms", "10"))).intValue();
		EXPANSION_DOCUMENTS = (new Integer(appProperties.getProperty("expansion.documents", "3"))).intValue();
		EXPANSION_MODELS = makeAbsolute(appProperties.getProperty("expansion.models", "qemodels"), TERRIER_ETC);
		//html tags and proximity related properties		
		BLOCK_INDEXING = (new Boolean(appProperties.getProperty("block.indexing", "false"))).booleanValue();
		TAG_INDEXING = (new Boolean(appProperties.getProperty("tag.indexing", "false"))).booleanValue();
		SPLIT_INDEXING = (new Boolean(appProperties.getProperty("split.indexing", "false"))).booleanValue();
		//BLOCK_QUERYING = (new Boolean(appProperties.getProperty("block.querying", "false"))).booleanValue();
		BLOCK_SIZE = (new Integer(appProperties.getProperty("blocks.size", "1"))).intValue();
		MAX_BLOCKS = (new Integer(appProperties.getProperty("blocks.max", "100000"))).intValue();
		FIELD_QUERYING = (new Boolean(appProperties.getProperty("field.querying", "false"))).booleanValue();
		INVERTTAG_TRESHOLD = (new Integer(appProperties.getProperty("inverttag.treshold", "2000"))).intValue();
		
		setupFilenames();
	}
	/** 
	 * Returns the value for the specified property, given 
	 * a default value, in case the property was not defined
	 * during the initialization of the system.
	 * @param propertyKey The property to be returned
	 * @param defaultValue The default value used, in case it is not defined
	 * @return the value for the given property.
	 */
	public static String getProperty(String propertyKey, String defaultValue) {
		return appProperties.getProperty(propertyKey, defaultValue);
	}
	
	/**
	 * Sets a value for the specified property. The properties
	 * set with this method are not saved in the properties file.
	 * @param propertyKey the name of the property to set.
	 * @param value the value of the property to set.
	 */
	public static void setProperty(String propertyKey, String value) {
		appProperties.setProperty(propertyKey, value);
	}
	
	/**
	 * Sets up the names of the inverted file, the direct file, 
	 * the document index file and the lexicon file.
	 */
	public static void setupFilenames() {
		String filenameTemplate = TERRIER_INDEX_PATH + FILE_SEPARATOR + TERRIER_INDEX_PREFIX;
		INVERTED_FILENAME = filenameTemplate + IFSUFFIX;
		INVERTED_TAG_FILENAME = filenameTemplate + TIFSUFFIX;
		COMPARABLE_INVERTED_FILENAME = filenameTemplate + COMPARABLE_IFSUFFIX;
		DIRECT_FILENAME = filenameTemplate + DF_SUFFIX;
		COMPARABLE_DIRECT_FILENAME = filenameTemplate + COMPARABLE_DF_SUFFIX;
		DOCUMENT_INDEX_FILENAME = filenameTemplate + DOC_INDEX_SUFFIX;
		COMPARABLE_DOCUMENT_INDEX_FILENAME = filenameTemplate + COMPARABLE_DOC_INDEX_SUFFIX;
		LEXICON_FILENAME = filenameTemplate + LEXICONSUFFIX;
		LEXICON_INDEX_FILENAME = filenameTemplate + LEXICON_INDEX_SUFFIX;
		TAGLEXICON_FILENAME = filenameTemplate + TAGLEXICONSUFFIX;
		TERMTAGLEXICON_FILENAME = filenameTemplate + TERMTAGLEXICONSUFFIX;
		TAGLEXICON_INDEX_FILENAME = filenameTemplate + TAGLEXICON_INDEX_SUFFIX;
		LOG_FILENAME = filenameTemplate + LOG_SUFFIX;
		
		COMPARABLE_LEXICON_FILENAME = filenameTemplate + COMPARABLE_LEXICONSUFFIX;
		COMPARABLE_LEXICON_INDEX_FILENAME = filenameTemplate + COMPARABLE_LEXICON_INDEX_SUFFIX;
	}
	
	/**
	 * Checks whether the given filename is absolute and if not, it 
	 * adds on the default path to make it absolute.
	 * @param filename String the filename to make absolute
	 * @param DefaultPath String the prefix to add
	 * @return the absolute filename
	 */
	public static String makeAbsolute(String filename, String DefaultPath)
	{
		if(filename == null)
			return null;
		if(filename.length() == 0)
			return filename;
		if ( new File(filename).isAbsolute() )
			return filename;
		if (! DefaultPath.endsWith(FILE_SEPARATOR))
		{
			DefaultPath = DefaultPath + FILE_SEPARATOR;
		}
		return DefaultPath+filename;
	}
}
