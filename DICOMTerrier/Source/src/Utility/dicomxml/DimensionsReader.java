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
 * Class to read image dimensions that are contained in the xml file of this image
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class DimensionsReader extends DefaultHandler implements FileInfoReader{

    /** Default parser name. */
    protected static final String DEFAULT_PARSER_NAME = "org.apache.xerces.parsers.SAXParser";
    
    private boolean isDescription = false;
    private XMLReader parser = null;
    private String description;
    private ArrayList tempDescription;
    private String matchTag;
    private String matchTag2;
    private String referenceTag;
    private String tmp;
    
    /** Default constructor. */
    public DimensionsReader() {
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
    public String parseFile(String file, String tag, String tag2){
    	description = "";
    	tempDescription = new ArrayList();
    	matchTag = tag;
    	matchTag2 = tag2;
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

    	if(matchTag2 == null){
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
    	} else {
    		int found=0;
    		if(local.trim().equalsIgnoreCase(matchTag)){
	    		//This is the tag we are looking for
	    		found++;	    		
	    	}
	
	    	int length = attrs.getLength();
	    	for (int i =0; i<length; i++){
	    		if (attrs.getValue(i).equalsIgnoreCase(matchTag)){
	    			//This is the attribute we are looking for
	    			found++;	    			
	    		}
	    	}
	    	if (found>1){
	    		referenceTag = local;
	    		isDescription = true;
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

    /*
     *  (non-Javadoc)
     * @see dicomxml.FileInfoReader#getInfo(java.lang.String)
     */
    public String getInfo(String fileName){
    	String[] rows = parseFile(fileName, "rows", null).split("\n");
    	String[] columns = parseFile(fileName, "columns", null).split("\n");
    	
    	if(rows.length >0 && columns.length>0){
    		String[] row = rows[0].split("=");
    		String width;
    		width = row[0];
    		
    		String[] column = columns[0].split("=");
    		String height;
   			height = column[0];
    		
    		if(width.length()>0 && height.length()>0){
    			if (width.charAt(1) == 'x')
    				width = Integer.parseInt(width.substring(2),16)+"";
    			if (height.charAt(1) == 'x')
    				height = Integer.parseInt(height.substring(2),16)+"";
    			
    			return ("Dimensions: " + width + "x" + height);
    		}
    		return "";
    	}
    	
    	return "";
    }
    
    /*
     *  (non-Javadoc)
     * @see dicomxml.FileInfoReader#getInfo(java.lang.String, java.lang.String)
     */
    public String getInfo(String fileName, String query){
    	return "";
    }
    
    //
    // MAIN
    //
    /** Main program entry point. */
    public static void main(String argv[]) {
    	    	
        // variables
        DimensionsReader listImage = new DimensionsReader();
        System.out.println(listImage.getInfo(argv[0]));

    } // main(String[])
}

