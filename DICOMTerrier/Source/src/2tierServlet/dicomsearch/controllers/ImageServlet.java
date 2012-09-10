package dicomsearch.controllers;

import java.awt.image.BufferedImage;
import java.awt.image.renderable.ParameterBlock;
import java.io.File;
import java.io.IOException;
import java.io.OutputStream;
import java.util.Iterator;

import javax.imageio.ImageIO;
import javax.imageio.ImageReader;
import javax.imageio.stream.ImageInputStream;
import javax.media.jai.InterpolationNearest;
import javax.media.jai.JAI;
import javax.media.jai.RenderedOp;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.dcm4che.imageio.plugins.DcmImageReadParam;

import com.sun.image.codec.jpeg.JPEGCodec;
import com.sun.image.codec.jpeg.JPEGImageEncoder;

/**
 * This servlet is used to display a dicom image in jpeg format
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */

public class ImageServlet extends HttpServlet {
	
	private static final long serialVersionUID = 6899020806066690320L;

	//TODO Make this hardcoded image path relative	
	private String notAvailableImage = "C:\\Sun\\AppServer\\domains\\domain1\\docroot\\images\\notavailable.jpg";
		
	protected void doGet(HttpServletRequest req, HttpServletResponse response) throws IOException, ServletException {
				
		String arg = req.getParameter("filename");
		String size = req.getParameter("size");
		boolean full = false;
		
		if (size!= null && size.equals("full"))
			full = true;
				
		long start = System.currentTimeMillis();
		long time2 = 0;

		//Open a DICOM image reader
		Iterator iter = ImageIO.getImageReadersByFormatName("DICOM");
		ImageReader reader = (ImageReader)iter.next();
		DcmImageReadParam param = (DcmImageReadParam) reader.getDefaultReadParam();
		ImageInputStream iis = ImageIO.createImageInputStream(new File(arg));
		BufferedImage image;
		
		try {
			reader.setInput(iis, false);
			
			int numImages = reader.getNumImages(false);
			if (numImages>0)
				//Take the middle image
				image = reader.read(numImages/2, param);
			else{
				image = ImageIO.read(new File(notAvailableImage));
				full = true;
			}
			if (image == null) {
				System.out.println("\nError: " + arg + " - couldn't read!");
				image = ImageIO.read(new File(notAvailableImage));
				full = true;
			}
		} catch (Exception e){
			image = ImageIO.read(new File(notAvailableImage) );
			full = true;
		} finally {
			try { iis.close(); } catch (Exception ignore) {}
		}
		
		long start2 = System.currentTimeMillis();
		if(!full){
			ParameterBlock pb = new ParameterBlock(); 
			pb.addSource(image);          					// The source image 
			pb.add((float)150/(float)image.getWidth());     // The xScale 
			pb.add((float)150/(float)image.getWidth());     // The yScale 
			pb.add(0.0F);          							// The x translation 
			pb.add(0.0F);          							// The y translation 
			pb.add(new InterpolationNearest()); 			// The interpolation 
			
			RenderedOp image2 = JAI.create("scale", pb, null); 
			image = image2.getAsBufferedImage();
		}
		//Set the correct content meta to jpeg
		response.setContentType("image/jpeg");
		
		//Write JPEG image to output stream
		OutputStream outputStream = response.getOutputStream();
		JPEGImageEncoder enc = JPEGCodec.createJPEGEncoder(outputStream);
		try{
			enc.encode(image);
		} catch (IOException ignore) {}
		outputStream.close();
		
		time2 = System.currentTimeMillis()-start2;
		System.out.println("JPEG fetching and scaling took " + time2 + " of " +(System.currentTimeMillis()-start) + " total"); 
	}
}
