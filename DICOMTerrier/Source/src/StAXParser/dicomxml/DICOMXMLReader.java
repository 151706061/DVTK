package dicomxml;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.io.Reader;
import java.io.StringReader;
import java.util.HashSet;
import java.util.Hashtable;
import java.util.Stack;

import javax.xml.stream.XMLInputFactory;
import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamReader;

import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * This class is used for parsing the DICOM XML files. It implements the FileDocument class of 
 * the DicomTerrier package.
 * For this it uses the specified index properties in the file
 * specified by the property <tt>index.propertie</tt>
 * 
 * @author Gerald van Veldhuijsen
 * 
 * @version 1.0
 */

public class DICOMXMLReader extends uk.ac.gla.terrier.indexing.FileDocument{
	
	/**TODO remove this hardcoded shortcut*/
	/**String pattern to skip*/
	protected static String endPatternToSkip = ".pix";
	protected boolean useSkipPattern = new Boolean(IndexSetup.getProperty("index.pixfiles.skip", "false")).booleanValue();
	
	/**Indicates the end of the document*/
	protected boolean endOfDocument = false;
	
	/**Indicates the end of the current string in a tag*/
	protected boolean endOfStringStream = true;
	
	/**The set of skippable tags, i.e. tags of which only the content will be indexed*/
	protected HashSet skippable_tags;
	
	/**The set of processable tags*/
	protected HashSet processable_tags;
	
	/**The set of tags of which the text can be skipped*/
	protected HashSet skippable_text_tags;
	
	/**The set of type mappings*/
	protected Hashtable typeMappings;	
	
	/**The type of the current term*/
	protected String currentType = "Text";
	
	/**Stack for maintaining tagstructure of the current term*/
	protected Stack tagStack = new Stack();
	
	/**Boolean whether the stack changed since last term*/
	protected boolean stackChanged = true;
	
	/**Stack to determine whether to add and pop tag or not*/
	protected Stack booleanStack = new Stack();
	
	/**Stack to determine whether we process text in the current structure*/
	protected Stack booleanTermStack = new Stack();
	
	/**XML reader*/
	protected XMLStreamReader reader;
	
	/**Maximum length of a term*/
	protected int MAX_TERM_LENGTH = ApplicationSetup.STRING_BYTE_LENGTH;
	
	/**Reader to read local strings*/
	protected StringReader sr;
	
	/**Whether the current term is a comparable term*/
	protected boolean comparable = false;
	
	/**The current character that is read*/
	protected int ch=-1;
	
	/**The previous returned term*/
	protected String prevTerm;
	
	
	public DICOMXMLReader(File f, InputStream docStream){
		super(f,docStream);
		try{
			//Create the Stax Reader
			if (docStream != null)
				reader = XMLInputFactory.newInstance().createXMLStreamReader(docStream);
			else
				reader = XMLInputFactory.newInstance().createXMLStreamReader(new FileInputStream(f));
			
			if (IndexSetup.skip)
				skippable_tags = IndexSetup.getSkippable();
			else
				processable_tags = IndexSetup.getProcessable();
			
			skippable_text_tags = IndexSetup.getSkippableTextTags();
						
			IndexSetup.getMappings();
			
			prevTerm = "";
			
			//read the types
			//typeMappings = IndexSetup.getTypeMappings();
			
		} catch (XMLStreamException e) {
			System.err.println("Error when initialising XML stream");
			System.err.println(e.getMessage());
		} catch (FileNotFoundException e){
			System.err.println("File not found");
			System.err.println(e.getMessage());
		}
	}	
	
	/*
	 * (non-Javadoc)
	 * @see uk.ac.gla.terrier.indexing.Document#getReader()
	 */
	public Reader getReader(){
		return (Reader) reader;		
	}
	
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.indexing.Document#getNextTerm()
	 */
	//TODO exception handling when term exceed maximum length
	public String getNextTerm(){
		String s = null;
		//String type;
		
		if (endOfDocument) return null;
		
		if (!endOfStringStream){
			//We still have some text to process in this tag
			if(!comparable || ch!='='){
				comparable=false;
			}
			
			//Read a new term
			if ((s = getStringReaderTerm())!=null && !s.equals("")){
				if(useSkipPattern){
					if (!s.endsWith(endPatternToSkip)){
						if(!comparable || !prevTerm.equals(s) || stackChanged){
							prevTerm = s;
							stackChanged = false;
							return s;
						}
					}
				}
				else{ 
					if(!comparable || !prevTerm.equals(s) || stackChanged){
						prevTerm = s;
						stackChanged = false;
						return s;
					}
				}
			}
		}
		
		try{
			while (reader.hasNext()){
				
				//Read the next XML event
				reader.next();
				
				/*detect type of XML input*/
				int event = reader.getEventType();
				
				/*Start of Element*/
				if (event == XMLStreamReader.START_ELEMENT){
					
					//Get the name of the tag
					String name = reader.getLocalName();
					
					//Retrieve the names to use for indexing
					String [] targets = IndexSetup.elMappings.getNames(name, reader);
					
					//Check whether we process the contents
					boolean processContents = true;
					for(int i=0; i<targets.length; i++){
						processContents &= checkTagForContents(targets[i]);
					}
					
					//Check whether we are processing the structure that this tag is in
					boolean previousTag = true;
					if (booleanTermStack.size()>0)
						previousTag = ((Boolean)booleanTermStack.peek()).booleanValue();
					
					/*Check whether we process the content of this tag*/
					if(checkTagForContents(name) && processContents && previousTag){
						
						//Add true to the processstack
						booleanTermStack.add(new Boolean(true));
						
						boolean process = true;						
						for(int i=0; i<targets.length; i++){
							process &= checkTag(targets[i]);
						}
						
						/*Check whether we index this tag*/
						if (checkTag(name) && process){
							comparable=true;
							
							/*Add indexnames to the structure stack*/							
							tagStack.add(targets);
							stackChanged = true;
							booleanStack.add(new Boolean(true));
				
						}
						else
							booleanStack.add(new Boolean(false));
					} 
					else {
						booleanTermStack.add(new Boolean(false));
						booleanStack.add(new Boolean(false));
					}
					
					/*End of Element*/
				} else if (event == XMLStreamReader.END_ELEMENT){
					//First check whether we add content on the stack for this tag
					//Then pop the structure
					booleanTermStack.pop();
					boolean pop = ((Boolean)booleanStack.pop()).booleanValue();
					if (pop){
						tagStack.pop();
						stackChanged = true;
					}
					
					/*Text*/
				} else if (event == XMLStreamReader.CHARACTERS){
					
					//Check whether we process content
					boolean process = ((Boolean)booleanTermStack.peek()).booleanValue();
					String text = reader.getText().trim();
					
					if (text.length()>0){						
						sr= new StringReader(text);
						endOfStringStream = false;
						
						s = null;
						if ((s = getStringReaderTerm())!=null && !s.equals("")){						
							if(!endOfStringStream)
								if( ch != '=') //'=' means actually the same term, so still comparable
									comparable = false;
							if (process)
								if(useSkipPattern){
									if (!s.endsWith(endPatternToSkip)){
										if(!comparable || !prevTerm.equals(s) || stackChanged){
											prevTerm = s;
											stackChanged = false;
											return s;
										}
									}
								}
								else{
									if(!comparable || !prevTerm.equals(s) || stackChanged){
										prevTerm = s;
										stackChanged = false;
										return s;
									}
								}
							
							/*For debugging							 
							 else{ 
							 	System.out.println("Skipping term " + s);
							 	while (!endOfStringStream){
							 		if ((s = getStringReaderTerm())!=null && !s.equals("")){
							 			System.out.println("Skipping term" + s);
							 		}
							    }
							 }*/
						}
					}
				}
				
				/*End of document, close it*/
				else if (event == XMLStreamReader.END_DOCUMENT){
					endOfDocument = true;
					reader.close();
					return null;
				}
				
				//Skip all other eventtypes
				/*
				 else if (event == XMLStreamReader.SPACE)
				 else if (event == XMLStreamReader.ATTRIBUTE)
				 else if (event == XMLStreamReader.CDATA)
				 else if (event == XMLStreamReader.COMMENT)
				 else if (event == XMLStreamReader.DTD)
				 else if (event == XMLStreamReader.ENTITY_DECLARATION)
				 else if (event == XMLStreamReader.ENTITY_REFERENCE)
				 else if (event == XMLStreamReader.NAMESPACE)
				 else if (event == XMLStreamReader.NOTATION_DECLARATION)
				 else if (event == XMLStreamReader.PROCESSING_INSTRUCTION)
				 else if (event == XMLStreamReader.START_DOCUMENT)
				 */
			}
			
			endOfDocument = true;
			
		} catch (XMLStreamException e){
			endOfDocument = true;
		}
		
		/*Should not be possible to reach through here*/
		return null;
	}
	
	/**
	 * Checks whether we index this tag
	 * @param tag The tag to check
	 * @return True if it needs to be indexed, false otherwise
	 */
	protected boolean checkTag(String tag){
		if (IndexSetup.skip)
			return !skippable_tags.contains(tag.toLowerCase());
		else
			return processable_tags.contains(tag.toLowerCase());
	}
	
	/**
	 * Checks whether we index the contents/childs of this tag tag
	 * @param tag The tag to check
	 * @return True if it needs to be indexed, false otherwise
	 */
	protected boolean checkTagForContents(String tag){
		return !skippable_text_tags.contains(tag.toLowerCase());
		
	}
	
	/**
	 * Read the next term out of a tag
	 * @return The term
	 */
	protected String getStringReaderTerm(){
		
		ch=0;
		StringBuffer sw = new StringBuffer(MAX_TERM_LENGTH);
		String s = null;
		
		try{
			/* skip non-alphanumeric charaters */
			while (ch != -1 && (ch < 'A' || ch > 'Z') && (ch < 'a' || ch > 'z')
					&& (ch < '0' || ch > '9')							 
			) {
				ch = sr.read();							
			}
			
			//now accept all alphanumeric charaters
			while (ch != -1 && (
					((ch >= 'A') && (ch <= 'Z'))
					|| ((ch >= 'a') && (ch <= 'z'))
					|| ((ch >= '0') && (ch <= '9'))
					|| ch=='.'
						//|| ch=='_'
					|| ch=='-'))
			{
				//transforms the uppercase character to lowercase
				if ((ch >= 'A') && (ch <= 'Z') /*&& lowercase*/)
					ch += 32;
				
				/* add character to word so far */
				sw.append((char)ch);
				ch = sr.read();								
			}
		} catch(IOException ioe){
			System.err.println("FileDocument got an IOException - skipping document"+ioe);
			ch = -1;
		}
		
		if (ch == -1)
		{
			//End of stream
			endOfStringStream = true;
			sr.close();			
		}
				
		s = sw.toString();		
		if (s.length() > MAX_TERM_LENGTH){
			//System.err.println("Skipping term \"" + s + "\". Too many characters ");
			return s.substring(0,MAX_TERM_LENGTH);
		}
		else
			return s;				
	}
	
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.indexing.Document#getFields()
	 */
	public HashSet getFields(){
		return new HashSet(tagStack);		
	}
	
	/**
	 * Get the structure stack of the current term
	 * @return The stack containing the structure
	 */
	public Stack getTagStack(){
		return tagStack;
	}
	
	/*
	 *  (non-Javadoc)
	 * @see uk.ac.gla.terrier.indexing.Document#endOfDocument()
	 */
	public boolean endOfDocument(){
		return endOfDocument;		
	}
	
	/**
	 * Get the type of the term (if used)
	 * @return The type
	 */
	public String getType(){
		return currentType;
	}
	
	/**
	 * Check whether the current term is a comparable term
	 * @return True if comparable, false otherwise
	 */
	public boolean isComparable(){
		return comparable;
	}
	
	/*MAIN*/
	public static void main(String[] args) {
		final String arg = args[0];
		new Thread(){
			public void run(){					
				
				try{
					DICOMXMLReader rdr = new DICOMXMLReader(new File(arg), new FileInputStream(arg));
					
					long start = System.currentTimeMillis();
					while (!rdr.endOfDocument()){
						String term;
						if ((term = rdr.getNextTerm())!=null && !term.equals("")) {
							System.out.println(term);
							//DEBUGGING
							/*Iterator ite = rdr.getFields().iterator();
							 System.out.print("[");
							 while (ite.hasNext())				
							 System.out.print((String)ite.next() + " ");
							 System.out.println("]");*/
						}
					}
					long end = System.currentTimeMillis();
					long diff = end-start;
					System.out.println("Parsetime: " + diff );					
				}
				catch (FileNotFoundException fnf){
					System.out.println(fnf);
				}
			}
		}.start();		
	}	
}
