package dicomxml;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.InputStream;
import java.io.StringReader;
import java.util.Iterator;

import javax.xml.stream.XMLStreamException;
import javax.xml.stream.XMLStreamReader;

import uk.ac.gla.terrier.utility.ApplicationSetup;

/**
 * This class extends the DICOM XML parser, by including the
 * structure as text during parsing.
 *  
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class DICOMXMLReaderPlus extends DICOMXMLReader{
	
	private String [] targets;
	private int structureIndex;
	private boolean processStructure = false;
	
	/*Constructor*/
	public DICOMXMLReaderPlus(File f, InputStream docStream){
		super(f,docStream);
	}	
	
	//TODO exception handling when term exceed maximum length
	public String getNextTerm(){
		String s = null;
		String type;
		
		if (endOfDocument) return null;
		
		if (processStructure){
			if (structureIndex<targets.length)
				return targets[structureIndex++];
			else{ 
				tagStack.add(targets);
				comparable = true;
				processStructure = false;
			}
		}
		
		if (!endOfStringStream){
			if(!comparable || ch!='='){
				comparable=false;
			}
			
			if ((s = getStringReaderTerm())!=null && !s.equals("")){
				
				if(useSkipPattern){
					if (!s.endsWith(endPatternToSkip))
						return s;
				}
				else 
					return s;
			}
		}
		
		try{
			while (reader.hasNext()){
				reader.next();
				
				/*detect type of XML input*/
				int event = reader.getEventType();
				
				/*Start of Element*/
				if (event == XMLStreamReader.START_ELEMENT){
					String name = reader.getLocalName();
					targets = IndexSetup.elMappings.getNames(name, reader);
					structureIndex = 0;
					boolean processContents = true;
					for(int i=0; i<targets.length; i++){
						processContents &= checkTagForContents(targets[i]);
					}
					
					boolean previousTag = true;
					if (booleanTermStack.size()>0)
						previousTag = ((Boolean)booleanTermStack.peek()).booleanValue();
					
					/*Check whether we process the content of this tag*/
					if(checkTagForContents(name) && processContents && previousTag){
						
						booleanTermStack.add(new Boolean(true));
						
						boolean process = true;						
						for(int i=0; i<targets.length; i++){
							process &= checkTag(targets[i]);
						}
						
						/*Check whether we process this tag*/
						if (checkTag(name) && process){
							//comparable=true;
	
							////tagStack.add(targets);
							booleanStack.add(new Boolean(true));
							
							if ((type = reader.getAttributeValue(null, IndexSetup.type_attribute)) !=null){
								currentType = (String) typeMappings.get(type);							
							}
							else 
								currentType = "Text";
							
							//if (targets.length>1)
							processStructure = true;
							comparable = false;
							return targets[structureIndex++];
						}
						else{
							processStructure = false;
							comparable = true;
							booleanStack.add(new Boolean(false));
						}
					} 
					else {
						processStructure = false;
						comparable =true;
						booleanTermStack.add(new Boolean(false));
						booleanStack.add(new Boolean(false));
					}
					
					/*End of Element*/
				} else if (event == XMLStreamReader.END_ELEMENT){
					booleanTermStack.pop();
					boolean pop = ((Boolean)booleanStack.pop()).booleanValue();
					if (pop)
						tagStack.pop();					
					
					/*Text*/
				} else if (event == XMLStreamReader.CHARACTERS){
					boolean process = ((Boolean)booleanTermStack.peek()).booleanValue();
					String text = reader.getText().trim();
					if (text.length()>0){
						
						sr= new StringReader(text);
						endOfStringStream = false;
						
						s = null;
						if ((s = getStringReaderTerm())!=null && !s.equals("")){						
							if(!endOfStringStream){
								if( ch != '=')
									comparable = false;
							}
							if (process)
								if(useSkipPattern){
									if (!s.endsWith(endPatternToSkip))
										return s;
								}
								else 
									return s;
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
			//ignore
		}	
		
		/*Should not be possible to reach through here*/
		return null;
	}
	
	/*MAIN*/
	public static void main(String[] args) {
		final String arg = args[0];
		try{
			DICOMXMLReaderPlus rdr = new DICOMXMLReaderPlus(new File(arg), new FileInputStream(arg));
			
			long start = System.currentTimeMillis();
			while (!rdr.endOfDocument()){
				String term;
				if ((term = rdr.getNextTerm())!=null && !term.equals("")) {
					System.out.println(term + (rdr.isComparable() ? " (c)" : ""));
					Iterator ite = rdr.getTagStack().iterator();
					System.out.print("[");
					while (ite.hasNext()){
						String [] tags = (String [])ite.next();
						System.out.print("[");
						for (int i=0; i<tags.length; i++)
							System.out.print(tags[i] + " ");
						System.out.print("]");
					}
					System.out.println("]");
					
				}
			}
			long end = System.currentTimeMillis();
			long diff = end-start;
			System.out.println("Parsetime: " + diff );
			System.out.println(ApplicationSetup.getProperty("index.properties", "hello"));
		}
		catch (FileNotFoundException fnf){
			System.out.println(fnf);
		}	
	}	
}
