package dicomsearch.controllers;

import java.io.IOException;
import java.io.PrintWriter;

import javax.naming.Context;
import javax.naming.InitialContext;
import javax.rmi.PortableRemoteObject;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import dicomsearch.search.Results;
import dicomsearch.search.SearchEngine2;
import dicomsearch.search.SearchEngine2Home;
import java.text.DecimalFormat;

/**
 * This servlet serves as the interface to the DICOM search engine.
 * It handles the user input and displays results on the screen.
 * 
 * TODO Remove the html code from this source
 * 
 * @author Gerald van Veldhuijsen
 * @version
 */
public class SearchEngineServlet extends HttpServlet {
	
	private static final long serialVersionUID = -2827911336412770644L;

	protected SearchEngine2Home searchEngineHome;
	
	protected SearchEngine2 searchEngine;
	
	private int nrOfObjectsInCollection = 0;
	
	private int resultsPerPage;
	
	/**
	 * This method will be called by the Servlet container when this servlet is
	 * being placed into service.<br>
	 * This method does a JNDI lookup, obtains the session bean's home interface
	 * and stores it in the instance attribute for future use.
	 * 
	 * @param config -
	 *            the <code>ServletConfig</code> object that contains
	 *            configutation information for this servlet
	 * @exception ServletException
	 *                if an exception occurs that interrupts the servlet's
	 *                operation
	 */
	public void init(ServletConfig config) throws ServletException {
		System.out.println("SearchEngineServlet: init()");
		try {
			Context context = new InitialContext();
			Object homeObject = context
			.lookup("java:comp/env/ejb/SearchEngineBeta");
			searchEngineHome = (SearchEngine2Home) PortableRemoteObject.narrow(
					homeObject, SearchEngine2Home.class);
			
			
			searchEngine = searchEngineHome.create();
			nrOfObjectsInCollection = searchEngine.create(); 
			if (nrOfObjectsInCollection<0)
				System.err.println("Failed to create search engine");
			
			nrOfObjectsInCollection = (nrOfObjectsInCollection/100) * 100;
			
		} catch (Exception exception) {
			exception.printStackTrace();
			throw new ServletException(
					"SearchEngineHome could not be created due to "
					+ exception.getMessage());
		}
	}
	
	/**
	 * This method handles the HTTP GET requests for this servlet.
	 * 
	 * @param request -
	 *            object that contains the request the client has made of the
	 *            servlet
	 * @param response -
	 *            object that contains the response the servlet sends to the
	 *            client
	 * @exception java.io.IOException -
	 *                if an input or output error is detected when the servlet
	 *                handles the GET request
	 * @exception ServletException -
	 *                if the GET request could not be handled
	 */
	public void doGet(HttpServletRequest request, HttpServletResponse response)
	throws ServletException, IOException {
		
		try {
			long time;
			int numberOfResults;
			StringBuffer sbuffer = new StringBuffer();
			PrintWriter out = response.getWriter();
			
			//Print the html header
			printHTMLHeader(out);
			
			// print the javascript
			printJavaScript(out);
			
			String advanced = request.getParameter("AdvancedQuery");
			
			if (advanced != null && !advanced.equals(""))
				printAdvanced(request, response);
			else {
				
				String start = request.getParameter("start");
				String query = request.getParameter("query");
				String sortCheck = request.getParameter("sort");
				boolean sort = true;
				if (sortCheck !=null && sortCheck.equals("unsort")) {
					sort = false;
					sortCheck = "&sort=unsort";
				} else
					sortCheck = "&sort=sort";
				
				//Some more HTML
				out.print("<form name=getForm action=\" " + response.encodeURL(request.getContextPath()	+ request.getServletPath())	+ "\" method=\"POST\">");
				
				out
				.println("<table><tr><td align=center><font size=3><B>Query</B></td><td align=center><font size=3><B></B></td></tr><tr><td>");
				
				if (query != null)
					out.print("<input type=\"text\" name=\"query\""
							+ "value=\""
							+ query
							+ "\" size=\"40\" onkeypressed=\"catchEnter(event)\">");
				else
					out.print("<input type=\"text\" name=\"query\""
							+ "value=\"\" size=\"40\" onkeypressed=\"catchEnter(event)\">");
				
				out.print("<input type=\"hidden\" "
						+ "value=\"\" name=\"submit\"> ");
				
				if (sort)
					out.print("<input type=\"hidden\" "
							+ "value=\"sort\" name=\"sort\"> ");
				else
					out.print("<input type=\"hidden\" "
							+ "value=\"unsort\" name=\"sort\"> ");
				
				out.print("<input type=\"submit\" "
						+ "value=\"Submit\" name=\"SubmitQuery\"> ");
				out.print("<input type=\"submit\" "
						+ "value=\"Advanced\" name=\"AdvancedQuery\"><BR> ");
				out.println("</td><td>");

				//Build up resultsfilter
				//This is needed to filter out the PATIENT, STUDY, SERIES and DICOMDIR files that are 
				// generated by DVT
				String filterString = request.getParameter("filterstring");
				if (filterString == null){
					filterString = "";
					
					String filter;
					filter = request.getParameter("patient");
					if (filter == null)
						filterString += ",PATIENT";
					filter = request.getParameter("study");
					if (filter == null)
						filterString += ",STUDY";
					filter = request.getParameter("series");
					if (filter == null)
						filterString += ",SERIES";
					filter = request.getParameter("dicomdir");
					if (filter == null)
						filterString += ",DICOMDIR";
				}

				out.println("</td></tr></table></form>");
				
				out.println("</td></tr><tr><td class=allresults>");

				if (start != null && start.trim().length() > 0) {
				
					//Execute the query
					int setStart = Integer.parseInt(start);
					Results results = searchEngine.processQuery(query, filterString, 2.0, sort, setStart);
					sbuffer = results.getBuffer();
					numberOfResults = results.getTotalNrOfResults();
					resultsPerPage  = results.getResultsPerPage();
					
					//Print the info message
					out.println("<span class=error>"+ results.getInfoMessage().replaceAll("<", "&lt;") +"</span>");					
					
					time = results.getQueryTime();
					out.println("Query took " + (time / 1000) + "." + (time % 1000) + " seconds");
					
					//Print the results
					if ((setStart + resultsPerPage) < numberOfResults)
						out.println("<BR>Displaying " + (setStart + 1) + "-"
								+ (setStart + resultsPerPage) + " from "
								+ numberOfResults + " results<BR>");
					else
						out.println("<BR>Displaying " + (setStart + 1) + "-"
								+ numberOfResults + " from " + numberOfResults
								+ " results<BR>");
					out.println(sbuffer);
					out.println("<BR>");
					if ((setStart - resultsPerPage) >= 0)
						out.print("<a href=\"" + response.encodeURL(request.getContextPath()
										+ request.getServletPath()) + "?start="
										+ (setStart - resultsPerPage) + "&query="
										+ query + sortCheck + "&filterstring="
										+ filterString + "\" >Previous </a>");
					
					int curind = setStart / resultsPerPage;
					int startind = curind - 2;
					int i = setStart - (2 * resultsPerPage);
					if (startind < 0) {
						startind = 0;
						i = 0;
					}
					int endind = startind + 5;
					while (i < numberOfResults && startind < endind) {
						out.print("<a href=\""
								+ response.encodeURL(request.getContextPath()
										+ request.getServletPath()) + "?start="
										+ (startind * resultsPerPage) + "&query="
										+ query + sortCheck + "&filterstring="
										+ filterString + "\" ");
						
						if (startind == curind)
							out.print("class=current>" + (startind + 1)
									+ " </a>");
						else
							out.print(">" + (startind + 1) + " </a>");
						
						startind++;
						i += resultsPerPage;
					}
					
					if ((setStart + resultsPerPage) < numberOfResults)
						out.println("<a href=\""
								+ response.encodeURL(request.getContextPath()
										+ request.getServletPath()) + "?start="
										+ (setStart + resultsPerPage) + "&query="
										+ query + sortCheck + "&filterstring="
										+ filterString + "\" >Next</a>");
										
					//Print the preview area
					out.println("</td><td class=previewarea>");
					out
					.println("<BR><BR><IMG border=0 src=\"/images/terrier-logo-web.jpg\" name=preview width=150>"
							+ "<BR>"
							+ "<a name=fullimage class=fullimagelink href=\"javascript:newwindow()\">View Large</a>");
				} else {
					DecimalFormat df = new DecimalFormat();
					df.setGroupingSize(3);
					//There were no results
					out.println("<i>Enter query, for example: "
							+ "\"mr monochrome 1000&lt;rows&lt;2000 \" </i><BR><BR>" +
									"<b>Currently holding approximately " + df.format((double)nrOfObjectsInCollection) + " DICOM objects in storage</b>");					
				}
			}
			
			out.close();
			
		} catch (Exception exception) {
			exception.printStackTrace();
		}
	}
	
	/**
	 * This method handles the HTTP POST requests for this servlet.
	 * 
	 * @param request -
	 *            object that contains the request the client has made of the
	 *            servlet
	 * @param response -
	 *            object that contains the response the servlet sends to the
	 *            client
	 * @exception java.io.IOException -
	 *                if an input or output error is detected when the servlet
	 *                handles the POST request
	 * @exception ServletException -
	 *                if the POST request could not be handled
	 */
	public void doPost(HttpServletRequest request, HttpServletResponse response)
	throws ServletException, IOException {
		
		try {
			long time;
			int numberOfResults;
			StringBuffer sbuffer = new StringBuffer();
			PrintWriter out = response.getWriter();

			//Print html header
			printHTMLHeader(out);
			
			// print the javascript
			printJavaScript(out);
			
			String advanced = request.getParameter("AdvancedQuery");
			String submit = "" + request.getParameter("SubmitQuery") +  request.getParameter("submit");
		
			if (advanced != null && !advanced.equals(""))
				//Print the advance search screen
				printAdvanced(request, response);
			else {
				String query = request.getParameter("query");
				String sortCheck = request.getParameter("sort");
								
				boolean sort = true;
				if (sortCheck == null && advanced != null) {
					sort = false;
					sortCheck = "&sort=unsort";
				} else if (sortCheck != null && sortCheck.equals("unsort")){
					sort = false;
					sortCheck = "&sort=unsort";
				} else 
					sortCheck = "&sort=sort";
				
				//Get the clinical parameter
				char clinical = '2';
				String clinString = request.getParameter("clinical");
				if (clinString != null)
					clinical = clinString.charAt(0);
				
				//Form header
				out.print("<form action=\" " + response.encodeURL(request.getContextPath()	+ request.getServletPath())	+ "\" method=\"POST\">");
				
				//Table header
				out.println("<table><tr><td align=center><font size=3><B>Query</B></td><td align=center><font size=3><B></B></td></tr><tr><td>");
				
				//Some more html
				if (query != null)
					out.print("<input type=\"text\" name=\"query\""
							+ "value=\"" + query + "\" size=\"40\">");
				else
					out.print("<input type=\"text\" name=\"query\""
							+ "value=\"\" size=\"40\">");
				
				if (sort)
					out.print("<input type=\"hidden\" "
							+ "value=\"sort\" name=\"sort\"> ");
				else
					out.print("<input type=\"hidden\" "
							+ "value=\"unsort\" name=\"sort\"> ");
				
				out.print("<input type=\"submit\" "
						+ "value=\"Submit\" name=\"SubmitQuery\"> ");
				out.print("<input type=\"submit\" "
						+ "value=\"Advanced\" name=\"AdvancedQuery\"><BR> ");
				out.println("</td><td>");
				
				//Build up resultsfilter
				//This is needed to filter out the PATIENT, STUDY, SERIES and DICOMDIR files that are 
				// generated by DVT
				String filterString = request.getParameter("filterstring");
				if (filterString == null){
					filterString = "";
				
					String filter;
					filter = request.getParameter("patient");
					if (filter == null)
						filterString += ",PATIENT";
					filter = request.getParameter("study");
					if (filter == null)
						filterString += ",STUDY";
					filter = request.getParameter("series");
					if (filter == null)
						filterString += ",SERIES";
					filter = request.getParameter("dicomdir");
					if (filter == null)
						filterString += ",DICOMDIR";
				}
				
				out.println("</td></tr></table></form>");
				out.println("</td></tr><tr><td class=allresults>");
				
				if (query == null)
					query = "";
				
				String tag;
				String content;
				String content2;
				if ((content = request.getParameter("uid")) != null)
					query += " " + content;
				
				//Process query parameters
				if (clinical == '0')
					query += " clinical=yes";
				else if (clinical == '1')
					query += " clinical=no";
				
				for (int i = 1; i < 5; i++) {
					tag = request.getParameter("tag1_" + (i));
					content = request.getParameter("containment" + (i));
					if (tag != null && !tag.trim().equals("")
							&& content != null && !content.trim().equals(""))
						query += " " + tag + ":" + content;
				}
				
				for (int i = 1; i < 5; i++) {
					tag = request.getParameter("tag2_" + (i));
					content = request.getParameter("equal" + (i));
					if (tag != null && !tag.trim().equals("")
							&& content != null && !content.trim().equals(""))
						query += " " + tag + "=" + content;
				}
				
				for (int i = 1; i < 5; i++) {
					tag = request.getParameter("tag3_" + (i));
					content = request.getParameter("rangestart" + (i));
					content2 = request.getParameter("rangeend" + (i));
					if (tag != null && !tag.trim().equals("")
							&& content != null && !content.trim().equals("")
							&& content2 != null && !content2.trim().equals(""))
						query += " " + content + "<" + tag + "<" + content2;
				}
				if (query.trim().length() > 0 && submit != null
						&& !submit.equals("")) {
					
					//Execute the query
					Results results = searchEngine.processQuery(query,filterString, 2.0, sort, 0);
					
					//Print the info message
					out.println("<span class=error>"+ results.getInfoMessage().replaceAll("<", "&lt;") +"</span>");
					
					time = results.getQueryTime();
					out.println("<br>Query took " + (time / 1000) + "."	+ (time % 1000) + " seconds");
					
					numberOfResults = results.getTotalNrOfResults();
					resultsPerPage = results.getResultsPerPage();
					sbuffer = results.getBuffer();
					
					//Print the results
					if (numberOfResults > 0) {
						if (resultsPerPage < numberOfResults)
							out.println("<BR>Displaying 1-" + resultsPerPage
									+ " from " + numberOfResults
									+ " results<BR>");
						else
							out.println("<BR>Displaying 1-" + numberOfResults
									+ " from " + numberOfResults
									+ " results<BR>");
					} else
						out.append("<BR>No results<BR>");
					out.println(sbuffer);
					out.println("<BR>");
					
					int i = 0;
					int ind = 0;
					while (i < numberOfResults && ind < 5) {
						out.print("<a href=\""
								+ response.encodeURL(request.getContextPath()
										+ request.getServletPath()) + "?start="
										+ (ind * resultsPerPage) + "&query=" 
										+ query	+ sortCheck + "&filterstring="
										+ filterString + "\" ");
						if (ind == 0)
							out.print("class=current>" + (ind + 1) + " </a>");
						else
							out.print(">" + (ind + 1) + " </a>");
						
						ind++;
						i += resultsPerPage;
					}
					
					if (resultsPerPage < numberOfResults)
						out.println("<a href=\""
								+ response.encodeURL(request.getContextPath()
										+ request.getServletPath()) + "?start="
										+ resultsPerPage + "&query=" 
										+ query	+ sortCheck + "&filterstring="
										+ filterString + "\" >Next</a>");
					
					//print the preview area
					out.println("</td><td class=previewarea>");
					out.println("<BR><BR><IMG border=0 src=\"/images/terrier-logo-web.jpg\" name=preview width=150 onLoad=\"checkLarge(event)\">"
							+ "<BR>"
							+ "<a name=fullimage class=fullimagelink href=\"javascript:newwindow()\">View Large</a>");
					
				} else {
					DecimalFormat df = new DecimalFormat();
					df.setGroupingSize(3);
					out.println("<i>Enter query, for example: "
							+ "\"mr monochrome 1000&lt;rows&lt;2000 \" </i><BR><BR>" +
									"<b>Currently holding approximately " + df.format((double)nrOfObjectsInCollection)  + " DICOM objects in storage</b>");					
				}
			}			
			
			//Print footer
			printHTMLFooter(out);
			
			out.close();
			
		} catch (Exception exception) {
			exception.printStackTrace();
		}
	}
	
	/**
	 * This method will be called by the Servlet Container when this servlet is
	 * being taken out of service
	 */
	public void destroy() {
		System.out.println("SearchEngineServlet: destroy()");
		try {
			if (searchEngine != null)
				searchEngine.close();
		} catch (Exception exception) {
			exception.printStackTrace();
		}
	}
	
	private void printJavaScript(PrintWriter out) {
		out.println("<script language=\"Javascript\" src=\"/common/dicomsearch.js\"></script> ");		
	}
	
	private void printAdvanced(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {
		
		//This will print the advanced inputscreen 
		
		PrintWriter out = response.getWriter();
		int moreFields = 0;
		String value;
		if ((value = request.getParameter("moreFields")) != null) {
			moreFields = Integer.parseInt(value);
		}
		
		char clinical = '2';
		String clinString = request.getParameter("clinical");
		if (clinString != null)
			clinical = clinString.charAt(0);
		String sortCheck = request.getParameter("sort");
		String query = request.getParameter("query");
		
		out.println("<form name=\"advancedform\" action=\""
				+ response.encodeURL(request.getContextPath()
						+ request.getServletPath())
						+ "\"  method=\"POST\">"
						+ "<input type = \"hidden\" name =\"moreFields\" value="
						+ moreFields
						+ ">"
						+ "<input type = \"hidden\" name =\"AdvancedQuery\" value=\"\">"
						
						+ "<table class=advancedouter cellspacing=0><tr><td>"
						+ "<table class=advancedinner width=100% cellspacing=0>"
						+ "<tr><td></td><td class=betweensmall colspan=5 ></td></tr>"
						+ "<tr><td></td><td ><font size=3 color=white><B>Query Parts</B></td><td  colspan=4></td></tr>"
						+ "<tr><td></td><td class=betweennormal colspan=5></td></tr>"
						+ "<tr><td></td><td class=inputrow><b>&nbsp;Keywords<b></td> <td colspan=3 class=inputrow ><input type=text name=query	value=\""
						+ query
						+ "\" size=80></td></tr>"
						+ "<tr><td></td><td class=betweenlarge colspan=5></td></tr>"
						+ "<tr><td></td><td class=inputrow><b>&nbsp;Term</b></td><td class=inputrow colspan=3><input type=text name=containment1 value=\"\" size=40> <b>&nbsp;must be contained in&nbsp;</b> <input type=text name=tag1_1 value=\"\" size=40>&nbsp;</td></tr>");
		
		//More containment fields		
		if ((moreFields & 1) != 0) {
			for (int i = 2; i < 6; i++)
				out
				.println("<tr><td></td><td class=inputrow><b>&nbsp;Term</b></td><td class=inputrow colspan=3 ><input type=text name=containment"
						+ i
						+ " value=\"\" size=40> <b>&nbsp;must be contained in&nbsp;</b> <input type=text name=tag1_"
						+ i + " value=\"\" size=40>&nbsp;</td></tr>");
			out
			.println("<tr><td></td><td class=betweenlarge colspan=5><a href=\"#\" onclick=\"javascript:dosubmit("
					+ (moreFields - 1)
					+ ")\" >less&lt;&lt;&lt;</a></td></tr>");
		
		//One containment field
		} else
			out
			.println("<tr><td></td><td class=betweenlarge colspan=5><a href=\"#\" onclick=\"javascript:dosubmit("
					+ (moreFields + 1) + ")\" >more>>></a></td></tr>");
		
		out
		.println("<tr><td></td><td class=inputrow><b>&nbsp;Tag</b></td><td class=inputrow colspan=3><input type=text name=tag2_1 value=\"\" size=45> <span style=\"FONT-WEIGHT:900; FONT-FAMILY:Arial; FONT-SIZE:16px\">&nbsp; == &nbsp;</span> <input type=text name=equal1 value=\"\" size=45>&nbsp;</td></tr>");
		
		//more equal fields
		if ((moreFields & 2) != 0) {
			for (int i = 2; i < 6; i++)
				out
				.println("<tr><td></td><td class=inputrow><b>&nbsp;Tag</b></td><td class=inputrow colspan=3><input type=text name=tag2_"
						+ i
						+ " value=\"\" size=45> <span style=\"FONT-WEIGHT:900; FONT-FAMILY:Arial; FONT-SIZE:16px\">&nbsp; == &nbsp;</span> <input type=text name=equal"
						+ i + " value=\"\" size=45>&nbsp;</td></tr>");
			out
			.println("<tr><td></td><td class=betweenlarge colspan=5><a href=\"#\" onclick=\"javascript:dosubmit("
					+ (moreFields - 2)
					+ ")\" >less&lt;&lt;&lt;</a></td></tr>");
		//one equal field	
		} else
			out
			.println("<tr><td></td><td colspan=5 class=betweenlarge><a href=\"#\" onclick=\"javascript:dosubmit("
					+ (moreFields + 2) + ")\" >more>>></a></td></tr>");
		
		out
		.println("<tr><td></td><td class=inputrow><b>&nbsp;Tag</b></td><td class=inputrow colspan=3><input type=text name=rangestart1 value=\"\" size=18> <font size=3><b>&nbsp; &lt; &nbsp;</b></font> <input type=text name=tag3_1 value=\"\" size=44> <font size=3 family=Arial><b>&nbsp; &lt; &nbsp;</b></font> <input type=text name=rangeend1 value=\"\" size=17>&nbsp;</td></tr>");
		
		//more range fields
		if ((moreFields & 4) != 0) {
			for (int i = 2; i < 6; i++)
				out
				.println("<tr><td></td><td class=inputrow><b>&nbsp;Tag</b></td><td class=inputrow colspan=3><input type=text name=rangestart"
						+ i
						+ " value=\"\" size=18> <font size=3><b>&nbsp; &lt; &nbsp;</b></font> <input type=text name=tag3_"
						+ i
						+ " value=\"\" size=44> <font size=3 family=Arial><b>&nbsp; &lt; &nbsp;</b></font> <input type=text name=rangeend"
						+ i + " value=\"\" size=17>&nbsp;</td></tr>");
			out
			.println("<tr><td></td><td colspan=5 class=betweenlarge><a href=\"#\" onclick=\"javascript:dosubmit("
					+ (moreFields - 4)
					+ ")\" >less&lt;&lt;&lt;</a></td></tr>");
			
		//one rangefield
		} else
			out
			.println("<tr><td></td><td colspan=5 class=betweenlarge><a href=\"#\" onclick=\"javascript:dosubmit("
					+ (moreFields + 4) + ")\" >more>>></a></td></tr>");
		
		out
		.println("<tr><td></td><td class=inputrow><b>&nbsp;UID<b></td> <td class=inputrow colspan=3><input type=text name=uid value=\"\" size=40></td></tr>"
				+ "<tr><td></td><td height=15></td></tr>"
				+ "<tr><td></td><td bgcolor=white height=10></td><td colspan=4 bgcolor=white></td></tr>"
				+ "<tr><td></td><td bgcolor=white><font size=3><B>Options</B></td><td colspan=4 bgcolor=white></td></tr>"
				+ "<tr><td></td><td bgcolor=white height=10></td><td colspan=4 bgcolor=white ></td></tr>"
				+ "<tr><td></td><td bgcolor=white><b>&nbsp;Dataset Grouping</b></td><td bgcolor=white>");
		
		if (sortCheck != null && !sortCheck.equals("unsort"))
			out
			.print("<input type=\"checkbox\" name=\"sort\" value=\"Sort\" checked>");
		else
			out
			.print("<input type=\"checkbox\" name=\"sort\" value=\"Sort\" >");
		
		out
		.println("1 result per dataset</td><td colspan=3 bgcolor=white></td></tr>"
				+ "<tr><td></td><td bgcolor=white height=8></td><td colspan=4 bgcolor=white ></td></tr>"
				+ "<tr><td></td><td bgcolor=white><b>&nbsp;Clinical Images</b></td><td bgcolor=white>");
		
		switch (clinical) {
		case '0':
			out
			.print("<input type=\"radio\" name=\"clinical\" value=\"0\" checked>");
			out.print("Only ");
			out.print("<input type=\"radio\" name=\"clinical\" value=\"1\" >");
			out.print("Non ");
			out.print("<input type=\"radio\" name=\"clinical\" value=\"2\" >");
			out.print("Both ");
			break;
		case '1':
			out.print("<input type=\"radio\" name=\"clinical\" value=\"0\" >");
			out.print("Only ");
			out
			.print("<input type=\"radio\" name=\"clinical\" value=\"1\" checked>");
			out.print("Non ");
			out.print("<input type=\"radio\" name=\"clinical\" value=\"2\" >");
			out.print("Both ");
			break;
		case '2':
			out.print("<input type=\"radio\" name=\"clinical\" value=\"0\" >");
			out.print("Only ");
			out.print("<input type=\"radio\" name=\"clinical\" value=\"1\" >");
			out.print("Non ");
			out
			.print("<input type=\"radio\" name=\"clinical\" value=\"2\" checked>");
			out.print("Both ");
			break;
		default:
			out.print("<input type=\"radio\" name=\"clinical\" value=\"0\" >");
		out.print("Only ");
		out.print("<input type=\"radio\" name=\"clinical\" value=\"1\" >");
		out.print("Non ");
		out
		.print("<input type=\"radio\" name=\"clinical\" value=\"2\" checked>");
		out.print("Both ");
		}
		
		out
		.println("</td><td bgcolor=white colspan=3></td></tr>"
				+ "<tr><td></td><td bgcolor=white height=8><b></b></td><td bgcolor=white></td><td colspan=3 bgcolor=white></td></tr>"
				+ "<tr><td></td><td bgcolor=white><b>&nbsp;Include Types</b></td><td colspan =3 bgcolor=white><input type=checkbox name=dicomdir value=DicomDir> DICOMDIR &nbsp; <input type=checkbox name=patient value=Patient> PATIENT &nbsp; <input type=checkbox name=series value=Series> SERIES &nbsp; <input type=checkbox name=study value=Study> STUDY &nbsp;<i> (No images, DVT related)</i></td><td bgcolor=white></td></tr>"
				+ "<tr><td></td><td bgcolor=white colspan=5 height=8 style=\"border-bottom: 3px solid #2f5376;\"></td></tr>"
				+ "</table><br>"
				+ "<center><input type=submit value=Submit name=SubmitQuery> <input type=submit value=Simple name=SimpleQuery><BR><BR></center>"
				+ "</td></tr></table>" + "</form>");
	}
	
	private void printHTMLHeader(PrintWriter out) {
		out.println("<html>");
		out.println("<head>");
		out
		.println("<link rel=stylesheet type=\"text/css\" media=screen href=\"/common/xoops.css\">");
		out
		.println("<link rel=stylesheet type=\"text/css\" media=screen href=\"/common/style.css\">");
					
		String title = "DICOM Image Search Engine (Under Development)";
		
		out.println("<title>" + title + "</title>");
		out.println("</head>");
		out.println("<body>");		
		
		//Print logo and table header
		out.println("<BR>");
		out.println("<center><br>");
		
		out.println("<table class=total><tr><td align=center>");
	}
	
	private void printHTMLFooter(PrintWriter out){
		out.println("</td></tr></table>");
		out.println("</center>");
		out.println("</body>");
		out.println("</html>");
	}
	
}
