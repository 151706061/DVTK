package dicomxml;

import java.util.Hashtable;

import javax.xml.stream.XMLStreamReader;

/**
 * This class contains the rules for converting element names of an XML document onto new ones.
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class ElementMappings {
	
	private Hashtable mappings;
	private ElementMapper defMap;
	
	/**
	 * Constructor
	 *
	 */
	public ElementMappings(){
		mappings = new Hashtable();
	}
	
	/**
	 * Return the set of names for this element should be used for indexing
	 * @param el XML element name
	 * @param reader Streamreader that is currently reading this element
	 * @return Array of names that follow from the rules
	 */
	public String [] getNames(String el, XMLStreamReader reader){
		ElementMapper elMap = (ElementMapper) mappings.get(el);
		
		if (elMap != null){
			//return the names
			return elMap.getNames(reader);
		}
		else if (defMap != null){
			//Reading from default
			String [] tmp = defMap.getNames(reader);
			String [] tmp2 = new String[tmp.length+1];
			tmp2[tmp.length] = el.toLowerCase();
			System.arraycopy(tmp, 0, tmp2, 0, tmp.length);
			
			return tmp2;
		}
		else{ 
			String[] tmp = {el.toLowerCase()};
			return tmp;
		}
	}
	
	/**
	 * Add a mapping. This mapping consist of an element name and a array of 
	 * attribute names that should build up the new name 
	 * @param el The element name
	 * @param attrs The array of attribute names
	 */
	public void addMapping(String el, String[] attrs){
		ElementMapper elMap = (ElementMapper) mappings.get(el);
		
		if (elMap == null)
			elMap = new ElementMapper();

		elMap.addMapping(attrs);
		mappings.put(el, elMap);			
	}
	
	/**
	 * Add a default mapping. This mapping consist of an element name and a array of 
	 * attribute names that should build up the new name 
	 * @param el The element name
	 * @param attrs The array of attribute names
	 */
	public void addDefaultMapping(String[] attrs){
		defMap = new ElementMapper();

		defMap.addMapping(attrs);					
	}
	
}
