package dicomxml;

import java.util.ArrayList;

import javax.xml.stream.XMLStreamReader;
/**
 * This class provides the names that should be used for indexing for a specific XML tag.
 * When providing the streamreader that currently reads (=points to) that element, it will 
 * return the correct names as a set of new names for this element.
 *   
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class ElementMapper {
	
	private ArrayList mappings;
	
	/**
	 * Constructor
	 */
	public ElementMapper(){
		mappings = new ArrayList();
	}
	
	/**
	 * Maps the element name onto a new set of names according to predefined rules.
	 * @param reader The xml stream reader that currently is reading the specific element
	 * The reader is assumed to be in the correct position. 
	 * @return An array of names.
	 */
	public String [] getNames(XMLStreamReader reader){
		ArrayList tmp = new ArrayList();
		int size= mappings.size();
		
		for (int i=0; i<size; i++){
			String target = "";
			String [] cur = (String[])mappings.get(i);
		
			for (int j=0; j<cur.length; j++){		
				String readValue = reader.getAttributeValue(null, cur[j]);
				//TODO remove this hardcoded check for ':' to somewhere else
				if (readValue != null && !readValue.trim().startsWith(":")){
					target += readValue.trim();	
				}
			}
			
			if (!target.equals("")){
				//Convert to normal strings
				tmp.add(target.replaceAll("[ \t\n\f\r,']+", "").toLowerCase());
			}
		}
		if (tmp.size()>0){
			String[] stringArray = new String [tmp.size()];
			stringArray = (String[])tmp.toArray(stringArray);
			return stringArray;
		}
		return new String [0];
	}
	
	/**
	 * Add a set of attribute names that should build up a new attribute name
	 * @param attributes
	 */
	public void addMapping(String[] attributes){
		mappings.add(attributes);
	}
	
}
