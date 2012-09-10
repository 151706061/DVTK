package dicomxml;

import java.io.FileInputStream;
import java.util.HashSet;
import java.util.Hashtable;
import java.util.Properties;

import uk.ac.gla.terrier.utility.ApplicationSetup;

public class IndexSetup {

	/** 
	 * The properties object in which the 
	 * properties from the file are read.
	 */
	protected static Properties indProperties;
	
	/**
	 * Indicates whether to skip specified tags or to only process specified tags
	 */
	protected static boolean skip; 
	
	/**
	 * Indicates where to find the type of the tag
	 */
	protected static String type_attribute;
	
	protected static HashSet comparables = new HashSet();
	
	private static HashSet skips = null;
	private static HashSet process = null;
	private static Hashtable mappings = null;
	private static Hashtable typeMappings = null;
	protected static ElementMappings elMappings = new ElementMappings();
	
	private static HashSet skipText = null;
	
	static{
		System.out.println("Loading index setup...");
		indProperties = new Properties();
		String propertiesFile = ApplicationSetup.TERRIER_ETC+ ApplicationSetup.FILE_SEPARATOR + ApplicationSetup.getProperty("index.properties", "index.properties");
				
		comparables.add("Float");
		comparables.add("Integer");
		comparables.add("String_comp");
		
		try{
			FileInputStream in = new FileInputStream(propertiesFile);
			indProperties.load(in);
			in.close();			
			
		} catch (java.io.FileNotFoundException fnfe) {
			System.out.println("WARNING: The file index.properties was not found at location "+ propertiesFile);
		} catch (java.io.IOException ioe) {
			System.err.println(
				"Input/Output Exception during initialization of ");
			System.err.println("stax.IndexSetup: "+ioe);
			System.err.println("Stack trace follows.");
			ioe.printStackTrace();
		}
		
		skip = (new Boolean(indProperties.getProperty("index.process.skip", "true"))).booleanValue();
		
		type_attribute = (String)indProperties.getProperty("index.type.attribute", "VR");
		
		System.err.println("IndexProperties loaded");
		System.out.println("IndexProperties loaded");
	}
	
	/**
	* Return the set of skippable tags that don't need to be indexed
	* Load it when it is not loaded yet
	* @return HashSet The set with tags
	*/
	public static HashSet getSkippable(){
		if (skips==null)
			loadSkippable();
		return skips;
	}
		
	private static void loadSkippable(){
		String skip = indProperties.getProperty("index.skippable", "");
		skips = new HashSet();
		
		String [] skippers = skip.split(";");
		
		for (int i=0; i<skippers.length; i++){
			skips.add(skippers[i].toLowerCase());
		}		
	}
	
	/**
	* Return the set of tags of which the contents don't need to be indexed
	* Load it when it is not loaded yet
	* @return HashSet The set with tags
	*/
	public static HashSet getSkippableTextTags(){
		if (skipText==null)
			loadSkippableTextTags();
		return skipText;
	}
		
	private static void loadSkippableTextTags(){
		String skip = indProperties.getProperty("index.text.skippable", "");
		skipText = new HashSet();
		
		String [] skippers = skip.split(";");
		
		for (int i=0; i<skippers.length; i++){
			skipText.add(skippers[i].toLowerCase());
		}		
	}
	
	
	/**
	* Return the set of skippable tags that don't need to be indexed
	* Load it when it is not loaded yet
	* @return HashSet The set with tags
	*/
	public static HashSet getProcessable(){
		if (process==null)
			loadProcessable();
		return process;
	}
		
	private static void loadProcessable(){
		String proc = (String)indProperties.getProperty("index.processable","");
		process = new HashSet();
		
		String [] processors = proc.split(";");
		
		for (int i=0; i<processors.length; i++){
			process.add(processors[i]);
		}		
	}
	
	/**
	 * Get the mappings for tags that have to get different name 
	 * @return The table containing the mappings 
	 */
	public static Hashtable getMappings(){
		if (mappings==null)
			loadMappings();
		return mappings;
	}
	
	/**
	 * Get the mappings for the types 
	 * @return The table containing the type mappings 
	 */
	public static Hashtable getTypeMappings(){
		if (typeMappings==null)
			loadTypeMappings();
		return typeMappings;
	}
	
	private static void loadMappings(){
		mappings = new Hashtable();
		
		String tags = indProperties.getProperty("index.map.tags" , "");
		String targets = indProperties.getProperty("index.map.targets", "");
		
		String[] readTags = tags.split(";");
		String[] readTargets = targets.split(";");
		
		/*try{
			for (int i=0; i<2; i++){
				String[] els = readTargets[i].split(",");
				MapTargets mapTargets = new MapTargets();
				for (int j=0; j<els.length; j++)
					mapTargets.add(els[j]);
				mappings.put(readTags[i], mapTargets);				
			}
			
		} catch(ArrayIndexOutOfBoundsException e){
			System.out.println("Error on initialising tag mappings");
			System.out.println("Probably different lengths of tags and targets");
		}*/
		
		try{
			for (int i=0; i<readTags.length; i++){
				String[] els = readTargets[i].split(",");
				elMappings.addMapping(readTags[i], els);
			}			
			
		} catch(ArrayIndexOutOfBoundsException e){
			System.out.println("Error on initialising tag mappings");
			System.out.println("Probably different lengths of tags and targets");
		}
		
		String defEls = indProperties.getProperty("index.map.default.targets", "");
		
		if (defEls.trim().length()>0){
			String [] els = defEls.split(",");
			elMappings.addDefaultMapping(els);
		}
		
		
	}
	
	private static void loadTypeMappings(){
		typeMappings = new Hashtable();
		
		String sourceTypes = indProperties.getProperty("index.type.sources", "");
		String targetTypes = indProperties.getProperty("index.type.targets", "");
		
		String[] readTypeSources = sourceTypes.split(";");
		String[] readTypeTargets = targetTypes.split(";");
		
		try{
			for (int i=0; i<readTypeSources.length; i++){
				typeMappings.put(readTypeSources[i], readTypeTargets[i]);
			}
			
		} catch(ArrayIndexOutOfBoundsException e){
			System.out.println("Error on initialising tag mappings");
			System.out.println("Probably different lengths of tags and targets");
		}
		
	}
	
	public static String getProperty(String propertyKey, String defaultValue) {
		return indProperties.getProperty(propertyKey, defaultValue);
	}
	
}
