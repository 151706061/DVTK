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
 * The Original Code is TagSet.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.utility;
import java.util.HashSet;
import java.util.StringTokenizer;
/**
 * A class that models a set of tags to process (white list),
 * a set of tags to skip (black list), a tag that is used as a
 * document delimiter, and a tag the contents of which are 
 * used as a unique identifier. The text within any tag encountered 
 * within the scope of a tag from the white list, is processed
 * by default, unless it is explicitly black listed. 
 * <br>
 * For example, in order to index all the text within
 * the DOC tag of a document from a typical TREC collection, 
 * without indexing the contents of the DOCHDR tag,, 
 * we could define in the properties file the following properties:
 * <br>
 * <tt>TrecDocTags.doctag=DOC</tt><br>
 * <tt>TrecDocTags.idtag=DOCNO</tt><br>
 * <tt>TrecDocTags.process=</tt><br>
 * <tt>TrecDocTags.skip=DOCHDR</tt><br>
 * <br>
 * In the source code, we would create an instance of
 * the class as follows:
 * <br>
 * <tt>TagSet TrecIndexToProcess = new TagSet("TrecDocTags");</tt>
 * <br>
 * All the tags are converted to uppercase, in order to check 
 * whether they belong to the specified set of tags.
 * 
 * @author Vassilis Plachouras, Craig Macdonald
 * @version $Revision: 1.1 $
 */
public class TagSet {
	/**
	 * A prefix for an empty set of tags, that is a set of tags
	 * that are not defined in the properties file.
	 */
	public final static String EMPTY_TAGS = "";
	
	/**
	 * The prefix for the TREC document tags. The corresponding
	 * properties in the setup file should start with <tt>TrecDocTags</tt>.
	 */
	public final static String TREC_DOC_TAGS = "TrecDocTags";
	
	/**
	 * The prefix for the TREC document exact tags. The corresponding
	 * properties in the setup file should start with <tt>TrecExactDocTags</tt>.
	 */
	public final static String TREC_EXACT_DOC_TAGS = "TrecExactDocTags";
	
	/**
	 * The prefix for the TREC topic tags. The corresponding
	 * properties in the setup file should start with <tt>TrecQueryTags</tt>.
	 */
	public final static String TREC_QUERY_TAGS = "TrecQueryTags";
	
	/**
	 * The prefix for the tags to consider as fields, during indexing. 
	 * The corresponding properties in the setup file should start with
	 * <tt>FieldTags</tt>.
	 */
	public final static String FIELD_TAGS = "FieldTags";
	
	/**
	 * The set of tags to process.
	 */
	protected HashSet whiteList;
	protected final int whiteListSize;
	
	/**
	 * A comma separated list of tags to process.
	 */
	protected String whiteListTags;
	
	/**
	 * The set of tags to skip.
	 */
	protected HashSet blackList;
	
	/**
	 * A comma separated list of tags to skip.
	 */
	protected String blackListTags;
	
	/**
	 * The tag that is used as a unique identifier.
	 */
	protected String idTag;
	
	/**
	 * The tag that is used for denoting the beginning of a
	 * document.
	 */
	protected String docTag;
	/** Returns true if whiteListSize &gt; 0.
	 *  @return Returns true if whiteListSize &gt; 0
	 */
	public boolean hasWhitelist()
	{
		return whiteListSize > 0;
	}
	
	/**
	 * Checks whether the tag should be processed. 
	 * @param tag String the tag to check.
	 * @return boolean true if the tag should be processed
	 */
	public boolean isTagToProcess(String tag) {
		return whiteList.contains(tag.toUpperCase());
	}
	
	/**
	 * Checks whether a tag should be skipped. You should
	 * use isTagToProcess as it checks the whitelist and blacklist.
	 * @param tag the tag to check.
	 * @return true if the tag is an identifier tag,
	 *         otherwise it returns false.
	 */
	public boolean isTagToSkip(String tag) {
		return blackList.contains(tag.toUpperCase());
	}
	
	/**
	 * Checks whether the given tag is a 
	 * unique identifier tag, that is the document
	 * number of a document, of the identifier of a
	 * topic.
	 * @param tag String the tag to check.
	 * @return boolean true if the tag is an identifier tag,
	 *         otherwise it returns false.
	 */
	public boolean isIdTag(String tag) {
		return idTag.equals(tag.toUpperCase());
	}
	/**
	 * Checks whether the given tag indicates
	 * the limits of a document.
	 * @param tag String the tag to check.
	 * @return boolean true if the tag is a document
	 *         delimiter tag, otherwise it returns false.
	 */
	public boolean isDocTag(String tag) {
		return docTag.equals(tag.toUpperCase());
	}
	
	/**
	 * Constructs the tag set for the given prefix,
	 * by reading the corresponding properties from
	 * the properties file.
	 * @param prefix the common prefix of the properties to read.
	 */
	public TagSet(String prefix) {
		whiteList = new HashSet();
		whiteListTags = ApplicationSetup.getProperty(prefix+".process","").toUpperCase();
		StringTokenizer tokens = new StringTokenizer(whiteListTags, ",");
		while (tokens.hasMoreTokens()) {
			whiteList.add(tokens.nextToken());
		}
		whiteListSize = whiteList.size();
		
		blackList = new HashSet();
		blackListTags = ApplicationSetup.getProperty(prefix+".skip","").toUpperCase();
		tokens = new StringTokenizer(blackListTags, ",");
		while (tokens.hasMoreTokens()) {
			blackList.add(tokens.nextToken());
		}
		
		idTag = ApplicationSetup.getProperty(prefix+".idtag","").toUpperCase();
		
		docTag = ApplicationSetup.getProperty(prefix+".doctag","").toUpperCase();
		/*the id and doc tags do not have to be specified in the whitelist, as 
		they are automatically added here
		whiteList.add(idTag);
		whiteList.add(docTag);*/
	}
	
	/**
	 * Returns a comma separated list of tags to process
	 * @return String the tags to process
	 */
	public String getTagsToProcess() {
		return whiteListTags;
	}
	
	/**
	 * Returns a comma separated list of tags to skip
	 * @return String the tags to skip
	 */
	public String getTagsToSkip() {
		return blackListTags;
	}
	
	/**
	 * Return the id tag.
	 * @return String the id tag
	 */
	public String getIdTag() {
		return idTag;
	}
	
	/**
	 * Return the document delimiter tag.
	 * @return String the document delimiter tag
	 */
	public String getDocTag() {
		return docTag;
	}
	
}
