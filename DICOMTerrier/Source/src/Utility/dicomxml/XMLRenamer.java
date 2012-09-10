package dicomxml;

import java.io.File;
import java.io.FilenameFilter;
import java.io.IOException;
import org.w3c.dom.Document;
import org.w3c.dom.Element;
import org.w3c.dom.NodeList;
import org.xml.sax.SAXException;
import org.xml.sax.SAXParseException;
import java.util.logging.Logger;
import dom.*;

public class XMLRenamer {
	
	static Logger logger = Logger.getLogger("XMLRenamer");
	
	/** Default parser name (dom.wrappers.Xerces). */
    protected static final String DEFAULT_PARSER_NAME = "dom.wrappers.Xerces";
	
    //TODO move these variables to a configuration
    private String dirName = "";
    private String GroupNumber = "0004";
    private String ElementNumber = "1500";
    private String TypeGroupNumber = "0004";
    private String TypeElementNumber = "1430";
    private String ElementName = "Attribute";
    private String GroupAttributeName = "Group";
    private String ElementAttributeName = "Element";
    
    private String FileSeparator = System.getProperty("file.separator");
    
    ParserWrapper parser = null;
    
    public XMLRenamer(){
    	//create parser
        try {
            parser = (ParserWrapper)Class.forName(DEFAULT_PARSER_NAME).newInstance();
        }
        catch (Exception e) {
            logger.severe("error: Unable to instantiate parser ("+DEFAULT_PARSER_NAME+")");
        }
    }
        
    public boolean rename(String arg){
    	String imageFileName = null;
		String newFileName = null; 
        
        //parse file
        try {
            Document document = parser.parse(arg);
            imageFileName = getFileName(document, ElementName, GroupAttributeName, ElementAttributeName);
            
        }
        catch (SAXParseException e) {
            // ignore
        }
        catch (Exception e) {
            logger.severe("error: Parse error occurred - "+e.getMessage());
            if (e instanceof SAXException) {
                Exception nested = ((SAXException)e).getException();
                if (nested != null) {
                    e = nested;
                }
            }
            e.printStackTrace(System.err);
        }
        
        //Rename the file
        if (imageFileName != null){
        	try{
        	//build upnew filename
        	File orgFile = new File(arg);
        	String path = orgFile.getCanonicalPath();
        	int slashIndex = path.lastIndexOf(FileSeparator);
        	String prefix = path.substring(0,slashIndex+1);
        	String postfix = path.substring(slashIndex+1, path.length());
        	
        	String[] fileNameParts = postfix.split("_");
        	imageFileName = imageFileName.trim();
        	if (fileNameParts.length != 4){
        		logger.severe("Filename is not of structure Detail_xxx_DICOMDIR_resxxx.xml");
        	}else{
        		newFileName = fileNameParts[0] + "_" + fileNameParts[1] + "_" + imageFileName + "_DDIR_" + fileNameParts[3];
        		        		
        		if (!(new File(prefix + dirName).exists()) ){
        			logger.info("Creating " + prefix+dirName);
        			new File(prefix + dirName).mkdirs();
        		}
        		if (!orgFile.renameTo(new File(prefix + dirName + newFileName)))
        			logger.severe("Renaming failed");
        		}
        	}
        	catch (IOException e){
        		logger.severe(e.getMessage());        		
        	}
        }
        else{
        //	System.out.println("File not renamed");
        }
    	return true;
    }
    
    /** Prints the specified elements in the given document. */
    private String getFileName(Document document,
                             String elementName, String attributeName1, String attributeName2) {

    	String fileName = null;
    	String alternate = null;
    	
        // get elements that match
        NodeList elements = document.getElementsByTagName(elementName);
        
        // is there anything to do?
        if (elements == null) {
            return null;
        }

        // check attributes
        if ((attributeName1 == null) || (attributeName2 == null)) {
            return null;
        }

        // resolve fileName
        else {
        	dirName = "";
            int elementCount = elements.getLength();
            for (int i = 0; i < elementCount; i++) {
                Element      element    = (Element)elements.item(i);
                if (element.getAttribute(attributeName1).equals(GroupNumber) && element.getAttribute(attributeName2).equals(ElementNumber)){
                	NodeList valuesList = element.getElementsByTagName("Values");
                	Element values = (Element) valuesList.item(0);
                	NodeList valueList = values.getElementsByTagName("Value");
                	int j;
                	for (j=0; j<valueList.getLength()-1; j++){
                		Element value = (Element) valueList.item(j);
                		dirName += value.getFirstChild().getNodeValue() + FileSeparator;
                	}
                	if (valueList.getLength()>0){
                		Element value = (Element) valueList.item(j);
            			fileName = value.getFirstChild().getNodeValue();
            			return fileName;
                	}
                } else if (element.getAttribute(attributeName1).equals(TypeGroupNumber) && element.getAttribute(attributeName2).equals(TypeElementNumber)){
                	NodeList valuesList = element.getElementsByTagName("Values");
                	Element values = (Element) valuesList.item(0);
                	NodeList valueList = values.getElementsByTagName("Value"); 
                	String type = valueList.item(0).getFirstChild().getNodeValue();
                	if (!type.trim().equals("IMAGE"))
                		alternate = type;
                }                
            }
            if (fileName != null)
            	return fileName;
            return alternate;
        }

    }     
    
	/**
	 * @param args
	 */
	public static void main(String[] args) {
					
		//is there anything to do?
        if (args.length < 2) {
            printUsage();
            System.exit(1);
        } else if (args[0].equals("-f")){
        	String arg = args[1];
        	if (arg.endsWith(".xml"))
        		new XMLRenamer().rename(arg);
        	
        } else if (args[0].equals("-d")){
        	String dir = args[1];
	        XMLRenamer renamer = new XMLRenamer();
	        File [] files;
			files = new File(dir).listFiles(new FilenameFilter(){
				public boolean accept(File file, String fileName){
					if (fileName.endsWith(".xml"))
						return true;
					return false;
				}
			});
			
			if (files != null){
				System.out.println("Read " + files.length + " files from " + dir);
			
				for (int i=0; i<files.length; i++)
					renamer.rename(files[i].getAbsolutePath());
			}else
				System.out.println("Read 0 files from " + dir);
			
        } else {
        	printUsage();
            System.exit(1);
        }
	}
	
	private static void printUsage() {
		System.err.println("Usage: XMLRenamer (-f <xmlfile> | -d <xmldir>)");
	}
	

}
