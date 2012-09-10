package uk.ac.gla.terrier.utility;
import gnu.trove.TIntHashSet;
import gnu.trove.TIntIntHashMap;
import gnu.trove.TObjectIntHashMap;
import gnu.trove.TIntObjectHashMap;
import gnu.trove.TIntArrayList;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.util.ArrayList;
import uk.ac.gla.terrier.structures.dicom.DICOMCollectionStatistics;

/**
 * This class provides the linking between normal files and meta files.
 * @author Gerald Veldhuijsen
 *
 */

public class FileList {
	
	private static ArrayList fileNames;
	private static TIntIntHashMap docToMeta;
	private static TIntHashSet metasSet;
	private static TIntObjectHashMap metaToDoc;
	
	static{
		initialise();
	}
	
	/**
	 * Loads document-metadocument mapping for a list of folders already 
	 * selected for indexing, from the given file.
	 * @param file the file from which to load the list of folders.
	 */
	public static void initialise() {
		metasSet = new TIntHashSet();
		docToMeta = new TIntIntHashMap(DICOMCollectionStatistics.getNumberOfDocuments());
		fileNames = new ArrayList(DICOMCollectionStatistics.getNumberOfDocuments());
		
		File file = new File(ApplicationSetup.makeAbsolute(
				ApplicationSetup.getProperty("desktop.directories.filelist",
				"data.filelist"), ApplicationSetup.TERRIER_INDEX_PATH));
		TObjectIntHashMap metasMap;
		metasMap = new TObjectIntHashMap();
		int i=1; //start at one!
		if (file == null || !file.exists())
			return;
		ArrayList out = new ArrayList();
		try {
			BufferedReader buf = new BufferedReader(new FileReader(file));
			String line;
			while ((line = buf.readLine()) != null) {
				//ignore empty lines, or lines starting with # from the methods
				// file.
				if (line.startsWith("#") || line.equals(""))
					continue;
				
				//Add the file name
				fileNames.add(line);
				
				//Store the path of the meta file and its id
				if (line.endsWith("meta.xml")){
					int pos = line.lastIndexOf("\\meta\\meta.xml");
					String tmp ="";
					if (pos>0)
						tmp = line.substring(0,pos);
					//TODO Remove this hardcoded path
					else if ( (pos = line.lastIndexOf("\\SINGLE_OBJECTS\\meta\\")) >0) {
						int pos2 = line.lastIndexOf("meta.xml");
						tmp = line.substring(pos+21,pos2-1);						
					}
					else{
						//skip this line
						continue;
					}
					metasMap.put(tmp, i);
					metasSet.add(i);
				}
				out.add(line.trim());
				i++;
			}
			buf.close();
		} catch (IOException ioe) {
		}
		
		metaToDoc = new TIntObjectHashMap(metasSet.size());
		
		//Now map the pathnames of the document to the id of its meta file.
		for (int j=0; j<out.size(); j++){
			String tmp = (String)out.get(j);
			int pos = tmp.indexOf("\\representations\\");
			if (pos>0){
				String searchString = tmp.substring(0,pos);
				if (searchString.endsWith("SINGLE_OBJECTS")){
					
					String [] parts = new File(tmp).getName().split("_");
					if (parts.length>4){
						searchString = tmp.substring(pos+28, tmp.length()-12);
					} else{						
						searchString = tmp.substring(pos+17, tmp.length()-4);						
					}
				}
				int m;
				if ( (m=metasMap.get(searchString)) != 0){
					TIntArrayList docList = (TIntArrayList)metaToDoc.get(m);
					if(docList == null){
						docList = new TIntArrayList(100);
					}
					docList.add(j);
										
					metaToDoc.put(m,docList);					
					docToMeta.put(j,m);					
				}				
			}			
		}		
	}
	
	/**
	 * Checks whether this is a meta file
	 * @param docId The id of the doc to check;
	 * @return true if this is a meta file
	 */
	public static boolean isMeta(int docId){
		return metasSet.contains(docId+1);
	}
	
	/**
	 * Retrieves the meta document for this document belongs to 
	 * @param docId The document identifier
	 * @return The docid of the meta file, or -1 if there isn't any
	 */
	public static int getMetaDocId(int docId){
		int id = docToMeta.get(docId);
		if (id!=0)
			return id-1;
		return -1;
	}
	
	/**
	 * Get the docs that this meta document describes
	 * @param docId Document identifier
	 * @return List of document identifiers for this meta document
	 */
	public static TIntArrayList getDocsForMeta(int docId){
		return (TIntArrayList) metaToDoc.get(docId);
	}
	
	/**
	 * Get the filename for this document identifier
	 * @param i Docid
	 * @return The filename of this document
	 */
	public static String getFileName(int i){
		return (String)fileNames.get(i);
	}	
}
