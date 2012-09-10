package metaxml;

import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.SAXParseException;
import org.xml.sax.XMLReader;
import org.xml.sax.helpers.DefaultHandler;
import org.xml.sax.helpers.XMLReaderFactory;

import dicomxml.FileInfoReader;

/**
 * This utility class can read the description of a dataset from the meta xml file
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class MetaFileReader extends DefaultHandler implements FileInfoReader {

    /** Default parser name. SAX parser will do */
    protected static final String DEFAULT_PARSER_NAME = "org.apache.xerces.parsers.SAXParser";
    
    private boolean isDescription = false;
    private XMLReader parser = null;
    private String description;
    private String matchTag;

    /** Default constructor. */
    public MetaFileReader() {
        // create parser
        try {
            parser = XMLReaderFactory.createXMLReader(DEFAULT_PARSER_NAME);
        }
        catch (Exception e) {
            System.err.println("error: Unable to instantiate parser ("+DEFAULT_PARSER_NAME+")");            
        }
        parser.setContentHandler(this);
        parser.setErrorHandler(this);
    	
    }

    
    /**
     * Parse the meta file given. Look for the given tag to process its contents
     * @param arg The file to parse
     * @param tag The tag to match
     * @return
     */
    public String parseMeta(String arg, String tag){
    	description = "";
    	matchTag = tag;
    	try{
      		parser.parse(arg);
    	} catch (SAXParseException e) {
            // ignore
        } catch (Exception e) {
        	System.out.println(e.getMessage());
            // ignore
        }
    	return description;
    }

    //
    // ContentHandler methods
    //
    /** Start element. */
    public void startElement(String uri, String local, String raw,
                             Attributes attrs) throws SAXException {

    	if(local.equals(matchTag))
    		//We have found the tag
    		isDescription = true;

    }

    /** End element. */
    public void endElement(String uri, String local, String raw
                             ) throws SAXException {

    	if(local.equals(matchTag)) isDescription = false;

    } 
    
    /** Characters. */
    public void characters(char ch[], int start, int length)
        throws SAXException {
    	
    	if (isDescription)
    		//We are in the correct tag 
    		description += new String(ch, start, length);

    }

    /*
     *  (non-Javadoc)
     * @see dicomxml.FileInfoReader#getInfo(java.lang.String)
     */
    public String getInfo(String fileName){    	
    	return parseMeta(fileName, "description");
    }
    
    /*
     *  (non-Javadoc)
     * @see dicomxml.FileInfoReader#getInfo(java.lang.String, java.lang.String)
     */
    public String getInfo(String fileName, String query){
    	return "";
    }
    
    //
    // MAIN for testing
    //

    /** Main program entry point. */
    public static void main(String argv[]) {

        // variables
        MetaFileReader metaReader = new MetaFileReader();
        String arg = argv[0];

        long timeBefore = System.currentTimeMillis();
        
        if(argv.length>1)
        	System.out.println(metaReader.parseMeta(arg, argv[1]));
        else
        	System.out.println(metaReader.parseMeta(arg, "description"));

        long timeAfter = System.currentTimeMillis();
        long time = timeAfter - timeBefore;
        System.out.println(time+ " miliseconds");
    } 
}

