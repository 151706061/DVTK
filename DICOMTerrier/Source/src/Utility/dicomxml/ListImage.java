package dicomxml;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.SAXParseException;
import org.xml.sax.XMLReader;
import org.xml.sax.InputSource;
import org.xml.sax.helpers.DefaultHandler;
import org.xml.sax.helpers.XMLReaderFactory;
import java.util.ArrayList;
import java.io.IOException;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.BufferedInputStream;

/**
 * Class to read the contents in a XML file for specific tag or attribute
 * Used for listing the files contained in an XML file of a DICOMDIR
 * @author Gerald van Veldhuijsen
 *
 */

public class ListImage extends DefaultHandler {

    /** Default parser name. */
    protected static final String DEFAULT_PARSER_NAME = "org.apache.xerces.parsers.SAXParser";
    
    private boolean isDescription = false;
    private XMLReader parser = null;
    private String description;
    private ArrayList tempDescription;
    private String matchTag;
    private String referenceTag;
    private String tmp;
    
    /** Default constructor. */
    public ListImage() {
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
     * Parse the file
     * @param arg The file to parse
     * @param tag The tag or attribute name to match
     * @return A string containing the found data
     */
    public String parseFile(String file, String tag){
    	description = "";
    	tempDescription = new ArrayList();
    	matchTag = tag;
    	tmp ="";
    	try{
    		
    		BufferedInputStream is = new BufferedInputStream(new FileInputStream(file));
        	
    		//First check whether we have illegal content in the prolog
    		char ch;
    		is.mark(1);
    		if ( (ch = (char)is.read()) != '<' ){
        		System.out.println("WARNING, XML Document contains illegal content in prolog:");
    			is.mark(1);
        		System.out.print(ch);
        	    		
        		while ( (ch = (char)is.read()) != '<' ){
        			is.mark(1);
        			System.out.print(ch);
        		}
        		System.out.println();
    		}
        	is.reset();
        	
        	InputSource iS = new InputSource();
        	iS.setByteStream(is);
        	
        	parser.parse(iS);        	
        	
    	} catch (FileNotFoundException e) {
    		System.err.println("File " + file + " not found");
    	} catch (IOException e) {
        	System.out.println(e.getMessage());
            // ignore
        } catch (SAXException e) {
        	// ignore
        }
    	
    	return description;
    }

    //
    // Error handler methods
    //
    public void fatalError(SAXParseException e){
    	System.out.println("FATAL ERROR: " + e.getMessage());
    }
    
    //
    // ContentHandler methods
    //
    /** Start element. */
    public void startElement(String uri, String local, String raw,
                             Attributes attrs) throws SAXException {

    	   	if(local.trim().equalsIgnoreCase(matchTag)){
	    		//This is the tag we are looking for
	    		isDescription = true;
	    		referenceTag = local;
	    	}
	
	    	int length = attrs.getLength();
	    	for (int i =0; i<length; i++){
	    		if (attrs.getValue(i).equalsIgnoreCase(matchTag)){
	    			//This is the attribute we are looking for
	    			isDescription = true;
	    			referenceTag = local;
	    		}
	    	}    	
    }

    /** End element. */
    public void endElement(String uri, String local, String raw
                             ) throws SAXException {
    	
    	//End of a tag, so add the text
    	if (tmp.length()>0){
			tempDescription.add(tmp);
			tmp = "";
		}
    	
    	if(local.equals(referenceTag)){
    		//We used this tag, so process results
    		isDescription = false;
    		if (tempDescription.size()>0){
    			description += tempDescription.get(0);
	    		for (int i =1; i<tempDescription.size(); i++){
	    			String tmp = (String)tempDescription.get(i); 
    				description += "\\" + tmp;
	    		}
	    		description += "\n";
    		}
    		tempDescription.clear();
    	}
    }
    
    /** Characters. */
    public void characters(char ch[], int start, int length)
        throws SAXException {
    	
    	if (isDescription){
    		//This is text in the right tag, so process it
    		tmp += new String(ch, start, length).trim();        	
    	}
    } // characters(char[],int,int);


    //
    // MAIN
    //
    /** Main program entry point. */
    public static void main(String argv[]) {
    	    	
        // variables
        ListImage listImage = new ListImage();
        if (argv.length<2)
        	System.out.println("Usage: java ListImage XMLFileName (tagname|attributename)");
        else{
        	String arg = argv[0];
        	String name = argv[1];
        
        	System.out.print(listImage.parseFile(arg, name));
        }
    } // main(String[])
}

