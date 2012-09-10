package dicomsearch.controllers;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.xml.transform.*;
import java.io.IOException;
import java.io.PrintWriter;
import javax.xml.transform.stream.*;


public class XmlTransformationServlet extends HttpServlet {
		public void doPost(HttpServletRequest req, 
                HttpServletResponse res)
throws ServletException, IOException
{
			PrintWriter out = res.getWriter();
			res.setContentType( "text/html" );
			String fileURL = req.getParameter("filename");
			String xslFile = "C:\\dvtcvs\\dvt_best\\dvt core\\distribution\\bin\\DVT_RESULTS.xslt";
			StreamSource xslSource = new StreamSource(xslFile);
			TransformerFactory factory = 
			TransformerFactory.newInstance();
			     try{
			     Transformer transformer = 
			          factory.newTransformer(xslSource);
			
			     transformer.transform( new StreamSource(fileURL)
			                          , new StreamResult(out)
			                          );
			     }
			 
			     catch(Exception ex)
					{
						System.out.println (ex.getMessage());
						ex.printStackTrace();
					}
			  
}
public void doGet(HttpServletRequest req, 
                HttpServletResponse res)
throws ServletException, IOException
{
	PrintWriter out = res.getWriter();
	res.setContentType( "text/html" );
	String fileURL = req.getParameter("filename");
	String xslFile = "C:\\dvtcvs\\dvt_best\\dvt core\\distribution\\bin\\DVT_RESULTS.xslt";
	StreamSource xslSource = new StreamSource(xslFile);
	TransformerFactory factory = 
	TransformerFactory.newInstance();
	     try{
	     Transformer transformer = 
	          factory.newTransformer(xslSource);
	
	     transformer.transform( new StreamSource(fileURL)
	                          , new StreamResult(out)
	                          );
	     }
	 
	     catch(Exception ex)
			{
				System.out.println (ex.getMessage());
				ex.printStackTrace();
			}
	  
}
 
 	public XmlTransformationServlet() {
		
	}

	/**
	 * @param args
	 */
	public static void main(String[] args) {
		

	}

}
