package dicomsearch.search;

import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.IOException;
import java.io.StringReader;
import java.util.ArrayList;
import javax.ejb.SessionBean;
import javax.ejb.SessionContext;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.querying.Manager;
import uk.ac.gla.terrier.querying.SearchRequest;
import uk.ac.gla.terrier.querying.parser.TerrierFloatLexer;
import uk.ac.gla.terrier.querying.parser.TerrierLexer;
import uk.ac.gla.terrier.querying.parser.TerrierQueryParser;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.structures.CollectionStatistics;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.FileList;
//import uk.ac.gla.terrier.utility.Rounding;
import antlr.TokenStreamSelector;
import dicomxml.FileInfoReader;
import javax.xml.transform.stream.*;
import javax.xml.*;
import javax.xml.transform.*;

/**
 * Bean that handles the queries. It executes the query on the Terrier framework,
 * processes the results and passes back this results.
 * 
 * TODO Not return results in html code, but in a dedicated class
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class SearchEngine2Bean implements SessionBean {

	private static final long serialVersionUID = -2656007656829862870L;

	//ejbMethods
	public void ejbCreate() {
	}

	public void setSessionContext(SessionContext ctx) {
	}

	public void ejbRemove() {
		System.out.println(this.hashCode() + ":Closing the index...");
		index.close();
	}

	public void ejbActivate() {
	}

	public void ejbPassivate() {
	}

	public void ejbLoad() {
	}

	public void ejbStore() {
	}
	//end of ejbMethods

	/*File list*/
	private ArrayList fileList;

	int resultsPerPage;
	
	/** The query manager. */
	protected Manager queryingManager;

	/** The weighting model used. */
	protected String wModel = ApplicationSetup.getProperty("weighting.model","PL2");

	/** The matching model used. */
	protected String mModel = "Matching";

	/** The data structures used. */
	protected Index index;
	
	/** Meta file reader */
	protected FileInfoReader metaFileReader;
	
	/** read meta */
	protected boolean readMeta = false;
	
	/** File info reader */
	protected FileInfoReader fileInfoReader;
	
	/** read info */
	protected boolean readInfo = false;
	
	String webcontext;
	
	protected String serverName = "";
	
	/**
	 * A constructor that initialises the index structures
	 */
	public int create() {
		System.out.println(" not able to instantiate the create function");
		long startLoading = System.currentTimeMillis();
		System.out.println(this.hashCode() + ":Loading the index...");
		
		//create the Index
		index = Index.createIndex();
				
		//intialise the filelist of the collection
		fileList = load_list(new File(ApplicationSetup.makeAbsolute(
				ApplicationSetup.getProperty("desktop.directories.filelist",
					"data.filelist"), ApplicationSetup.TERRIER_INDEX_PATH)));
		//fileList = load_list(new File("C:\\DicomTerrier\\var1\\index\\data.filelist"));
		System.err.println(" failing to open the file");
		
		if (index == null || fileList.size() == 0) {
			System.err.println("ERROR: Failed to load indexes. Perhaps index files are missing");
			return -1;
		}
		
		//Set the document score modifiers
		ApplicationSetup.setProperty("matching.dsms", "MetaModifier,DocumentFileNameModifier,BooleanFallback");
		
		queryingManager = new Manager(index);
		
		//Intialise to have faster access later
		FileList.isMeta(0);
		
		long endLoading = System.currentTimeMillis();
			
		resultsPerPage = new Integer(ApplicationSetup.getProperty("results.numberperpage", "10")).intValue();
		
		readInfo = new Boolean(ApplicationSetup.getProperty("display.file.info", "false")).booleanValue();
		readMeta = new Boolean(ApplicationSetup.getProperty("display.meta.info", "false")).booleanValue();
		String infoReader = ApplicationSetup.getProperty("file.info.class", "");
		String metaReader = ApplicationSetup.getProperty("meta.info.class", "");
		
		try{			
			if (readInfo){
				fileInfoReader = (FileInfoReader)Class.forName(infoReader).newInstance();
			}
			if (readMeta){
				metaFileReader = (FileInfoReader)Class.forName(metaReader).newInstance();
			}
		} catch (ClassNotFoundException e){
			System.err.println("Could not find class " + infoReader);
		} catch (InstantiationException e){
			System.err.println(e.getMessage());
		} catch (IllegalAccessException e){
			System.err.println(e.getMessage());
		}
				
		System.out.println("time to intialise indexes : " + ((endLoading - startLoading) / 1000.0D));
		
		webcontext = ApplicationSetup.getProperty("webcontext.postfix", "");
		
		return CollectionStatistics.getNumberOfDocuments();
	}

	private ArrayList load_list(File file) {
		
		if (file == null || !file.exists())
			return new ArrayList();
		
		ArrayList out = new ArrayList();
		
		try {
			BufferedReader buf = new BufferedReader(new FileReader(file));
			String line;
			System.out.println("File Name is : " + file.getAbsolutePath());
			while ((line = buf.readLine()) != null) {
				// ignore empty lines, or lines starting with # 
				if (line.startsWith("#") || line.equals(""))
					continue;
				out.add(line.trim());
			}
			buf.close();
		} catch (IOException ioe) {
		}
		return out;
	}
	private String ReadjavaFile() 
	{
		String docroot ;
		docroot = ApplicationSetup.getProperty("docroot","");
		String  src= docroot + "\\common\\ServerConfig.txt";
		try { 

			   	   FileReader fr = new FileReader(src);
		           BufferedReader br = new BufferedReader(fr);
		           serverName = br.readLine();		           

		        } catch (IOException e) { 
		           // catch possible io errors from readLine()
		           System.out.println("Got an IOException error!");
		           e.printStackTrace();
		        }
		        return serverName ;

		
	
	}

	/**
	 * Closes the used structures.
	 */
	public void close() {
		System.out.println(this.hashCode() + ":Closing the index...");
		index.close();
	}

	/**
	 * According to the given parameters, it sets up the correct matching class.
	 * The maximum number of returned results is determined by the property 
	 * <tt>matching.retrieved_set_size</tt> which defaults to 1000
	 * 
	 * @param query
	 *            String the query to process.
	 * @param filterString
	 *            String the type of results to filter out.
	 * @param cParameter
	 *            double the value of the parameter to use.
	 * @param sort
	 * 			  boolean whether to group datasets
	 * @param start
	 *            int the start result number
	 */
	public synchronized Results processQuery(String query, String filterString,
			double cParameter, boolean sort , int start ) {
		StringBuffer out = new StringBuffer();
		
		//Set the filterstring
		ApplicationSetup.setProperty("skippable.dicom.types", filterString);
		//Set the sort boolean
		ApplicationSetup.setProperty("group.datasets", ""+sort);
		
		//Create searchrequest		 
		if (queryingManager == null){
			System.err.println("Queryingmanager not initialized, doing it now");
			this.create();
		}		
		SearchRequest srq = queryingManager.newSearchRequest("WEBQUERY");

		try {
			//Terrier specific initialisation
			TerrierLexer lexer = new TerrierLexer(new StringReader(query));
			TerrierFloatLexer flexer = new TerrierFloatLexer(lexer.getInputState());
			
			TokenStreamSelector selector = new TokenStreamSelector();
			selector.addInputStream(lexer, "main");
			selector.addInputStream(flexer, "numbers");
			selector.select("main");
			TerrierQueryParser parser = new TerrierQueryParser(selector);
			parser.setSelector(selector);
			srq.setQuery(parser.query());
			
			System.out.println("ORGQUERY: "+srq.getQuery());
		} catch (Exception e) {
			out.append("Failed to process WEBQUERY: " + e);
			return null;
		}
		
		srq.setControl("c", Double.toString(cParameter));
		srq.setControl("start", start+"");
		srq.setControl("end", (start+resultsPerPage-1)+"");
		srq.addMatchingModel(mModel, wModel);
		
		Results results = new Results();
		
		long startTime = System.currentTimeMillis();
		queryingManager.runPreProcessing(srq);
		queryingManager.runMatching(srq);
		queryingManager.runPostProcessing(srq);
		
		results.setTotalNumberOfResults(srq.getResultSet().getResultSize());
		
		queryingManager.runPostFilters(srq);
		long endTime = System.currentTimeMillis();
		
		System.out.println("PARSEDQUERY: " + srq.getQuery());
		ResultSet set = srq.getResultSet();
				
		processResults(out, set, start);
		
		//Store into results object
		
		results.setBuffer(out);
		results.setQueryTime(endTime-startTime);
		results.setInfoMessage(set.getInfoMessage());
		results.setResultsPerPage(resultsPerPage);
		
		return results;
	}

	/**
	 * Process the results to create the results in 
	 * HTML code.  
	 * @param out Stringbuffer to write the results to
	 * @param set ResultSet containing the results
	 * @param start int startnumber to start the results
	 * @param end int endnumber to start the results
	 */
	private void processResults(StringBuffer out, ResultSet set, int start) {
		//try {
		
			//We need the doc identifiers and the scores
			int[] docids = set.getDocids();
			double[] scores = set.getScores();
			int size = set.getResultSize();
			String servername = ReadjavaFile() ;
			
			int slashindex;
			int i ;
			ArrayList  arrayforImages = new ArrayList();
			String path;
			StringBuffer sbuffer = new StringBuffer();
			
			//DocumentIndex docindex = index.getDocumentIndex();
			
			// even though we have single-threaded usage
			// in mind, the synchronized makes code faster
			// since each sbuffer.append() call does not
			// try to obtain a lock.
			synchronized (sbuffer) {
				
				//Print table header
				sbuffer.append("<table class=resultstable><tr><th class=results>&nbsp;</th><th class=results><b>Image</b></th><th class=results><b>Preview/Meta</b></th><th class=results><b>DocId</b></th><th class=results><b>Set identifier</b></th></tr>"); //<th class=results><b>Score</b></th></tr>");
				for (i = 0; i < size; i++) {
					String checkBoxName = "check" + i ; 

					//Print the result number
					sbuffer.append("<tr>");
					
					sbuffer.append("<td class=results>");
					sbuffer.append ("<input type='checkbox' name=\"check"+i+ "\" value='false' />");
					sbuffer.append(i + start + 1);
					sbuffer.append("</td><td class=results>");
					
					//Get the filename
					String f = (String) fileList.get(docids[i]);

					if (f == null)
						continue;
					
					//Remove ".xml" part
					int dotIndex = f.lastIndexOf(".");
					String dcmname = f.substring(0,dotIndex);
					
					//Store original filename
					String org = f;					
					//Get pathname
					slashindex = f.lastIndexOf(ApplicationSetup.FILE_SEPARATOR);
					f = f.substring(0,slashindex);
					
					//Get the setname
					int pos = f.lastIndexOf( ApplicationSetup.FILE_SEPARATOR + "representations");
					int pos2 = f.lastIndexOf(ApplicationSetup.FILE_SEPARATOR, pos-1);
					
					String setName = f.substring(pos2+1, pos);
					
					//Switch from data directory to representations directory
					int repIndex = f.indexOf("representation");
					int slashIndex = f.indexOf(ApplicationSetup.FILE_SEPARATOR, repIndex);
					if(slashIndex>0){
						String repString = f.substring(repIndex,slashIndex);
						f = f.replaceFirst(repString, "data");						
					}
					else
						//SINGLE_OBJECT
						f = f.replaceFirst("representations", "data");
															
					//Check for DVT file name
					dcmname = new File(dcmname).getName();
					String[] dcmnames = dcmname.split("_");
					if (dcmnames.length > 3) {
						//Build image name
						dcmname = "";
						if (dcmnames[dcmnames.length - 2].equals("DCM")
								|| dcmnames[dcmnames.length - 2].equals("DDIR")) {
							for (int k = 2; k < dcmnames.length - 2; k++)
								dcmname += dcmnames[k] + "_";
						} else if (dcmnames[dcmnames.length - 1].equals("DICOMDIR")){
							for (int k = 2; k < dcmnames.length - 1; k++)
								dcmname += dcmnames[k] + "_";
						} else {
							for (int k = 0; k < dcmnames.length; k++)
								dcmname += dcmnames[k] + "_";
						}
						dcmname = dcmname.substring(0, dcmname.length() - 1);
						
					}
					f = f + '\\' + dcmname;
										
					//Print image link
					//sbuffer.append("<u>" + new File(dcmname).getName() + "</u>");
					File tmp = new File(f);
					if (!tmp.exists()){
						//In some XML generators, the dot is converted to '_' too, so we have to change it back
						int point = f.lastIndexOf('_');
						if(point>-1){
							char [] chars = f.toCharArray();
							chars [point] = '.';
							f = new String(chars);
						}
					}
					path = f;
					String name = new File(dcmname).getName();
					//sbuffer.append("<a class=image  href =\"");
					sbuffer.append("<a class=image href=\"/dicomsearchbeta/xml?filename="+org+"\" >");
					sbuffer.append(new File(dcmname).getName());
					sbuffer.append("</a> ");
										
					//Remove the first part of the path, because webcontext start one lever higher
					//not necessary when shared disk is used
					slashindex = path.indexOf("\\", 2);
					String prefix = path.substring(0, slashindex)+ webcontext;
					String postfix = path.substring(slashindex);
					sbuffer.append("<input type='hidden' name=\"images"+i+ "\" value=" + f + " >");
					
					
					if(readInfo)
						sbuffer.append("<BR><SPAN class=description>" + fileInfoReader.getInfo(org) + "</SPAN>");
					sbuffer.append("</td><td class=results>");
					//sbuffer.append("<input type='hidden' name=\"image"+i+ "\" value=" +tempImagePath + " >");
					
					//Print the preview link
					//First construct filename path to pass to the image servlet
					path = f;
					path = path.replaceAll("\\\\", "\\\\\\\\");		
					sbuffer.append("<a href=\"#\" onclick=\"change('" + path + "', 'preview" + i +  "')\" name=preview" + i + " > [Preview] </a>");
					sbuffer.append("</td><td class=results>");
								
										
					
					//Print docid
					sbuffer.append(docids[i]);
					sbuffer.append("</td><td class=setname>");
					
					//Print the dataset identifier, or "Not applicable" if it is a single image
					if (!path.startsWith("\\data\\SINGLE_OBJECTS")){
						//Print the set name
						String temppath = org;
						temppath = temppath.substring(0 , repIndex);
						int index1 = temppath.indexOf("dicomterrier");
						temppath = "ftp:\\\\"+ servername + "\\" + temppath.substring(index1 +13 )+ "data\\";
						temppath = temppath.replace('\\','/');
						sbuffer.append("<a class=image href =\"");
						sbuffer.append(temppath);
						sbuffer.append("\">");
						sbuffer.append(setName);
						sbuffer.append("</a>");
											
						//Print the meta description
						if (readMeta){
							sbuffer.append("<BR><SPAN class=description>" + metaFileReader.getInfo(prefix+org.substring(slashindex,repIndex-1)+"/meta/meta.xml") + "</SPAN><BR><BR>");
						}
					}
					else
						sbuffer.append("not applicable");
					
					sbuffer.append("</td>");
					
					//Print the score
					//sbuffer.append("<td class=results>" + Rounding.toString(scores[i],2) + "<BR>" + docindex.getDocumentLength(docids[i]) + "</td>");
					
					sbuffer.append("</tr>");					
				}
				
				//Print end table
				sbuffer.append("</table>");
				out.append(sbuffer);
				out.append("<BR> </BR>");
			}
			}
	
	
	
	
	
}
