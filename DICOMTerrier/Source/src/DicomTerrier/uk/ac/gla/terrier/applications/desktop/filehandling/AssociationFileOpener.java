/*
 * Terrier - Terabyte Retriever 
 * Webpage: http://ir.dcs.gla.ac.uk/terrier 
 * Contact: terrier{a.}dcs.gla.ac.uk
 * University of Glasgow - Department of Computing Science
 * Information Retrieval Group
 * 
 * The contents of this file are subject to the Mozilla Public License
 * Version 1.1 (the "License"); you may not use this file except in
 * compliance with the License. You may obtain a copy of the License at
 * http://www.mozilla.org/MPL/
 *
 * Software distributed under the License is distributed on an "AS IS"
 * basis, WITHOUT WARRANTY OF ANY KIND, either express or implied. See
 * the License for the specific language governing rights and limitations
 * under the License.
 *
 * The Original Code is AssociationFileOpener.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.applications.desktop.filehandling;
import java.io.BufferedInputStream;
import java.io.BufferedOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Properties;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * This class implements the interface FileOpener, using a properties
 * file for storing the file associations.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class AssociationFileOpener implements FileOpener {
	
	/** The application selector dialog.*/
	protected ApplicationSelector appSelector = new ApplicationSelector();
	
	/** The properties which correspond to the file associations.*/
	protected Properties fileAssoc = new Properties();
	
	/** The header information describing the properties file.*/
	protected final String description = "The file assocciations used for opening files";
	
	/** The name of the properties file in which the assocciations are stored.*/
	protected final String propFilename = ApplicationSetup.makeAbsolute(ApplicationSetup.getProperty("desktop.file.assocciations", "desktop.fileassoc"),ApplicationSetup.TERRIER_VAR);
	
	/**
	 * Opens the file with the given name, using either a 
	 * pre-defined, or a user-defined application. It assumes
	 * that 
	 * @param filename the name of the file to open.
	 */
	public void open(String filename) {
		System.out.println("Opening file.... " + filename );
		String ext = null;
		int extIndex = filename.lastIndexOf('.');
		//if there is no extension, then consider it as text
		if (extIndex < 0) {
			//ext = "txt";
			ext = "dcm";
			System.out.println("The extension is dcm");
		} else {
			ext = filename.substring(extIndex+1);
		}
		String application = fileAssoc.getProperty(ext);
		//does the given extension correspond to an application?
		if (application == null) {
			application = ApplicationSetup.getProperty("dicom.viewer", "");
			System.out.println(application);
		}
				
		//is the application set?
		if (application != null) {
			try {
				Runtime.getRuntime().exec(new String[]{application, filename});
			} catch(IOException ioe) {
				System.err.println("Input/output exception while executing application " + application);
				System.err.println("Stack trace follows");
				ioe.printStackTrace();
			}
		}
	}
	
	/**
	 * Saves the used file assocciations to a properties file.
	 */
	public void save() {
		try {
			BufferedOutputStream bos = new BufferedOutputStream(new FileOutputStream(propFilename));
			fileAssoc.store(bos, description);
			bos.close();
		} catch(IOException ioe) {
			System.err.println("Input/output exception while saving the file assocciations to a file.");
			System.err.println("Stack trace follows.");
			ioe.printStackTrace();
		}
	}
	/**
	 * Loads the file assocciations to use from a properties file.
	 */
	public void load() {
		try {
			System.out.println("Loading associtions from " + propFilename);
			File propFile = new File(propFilename);
			if (propFile.exists()) {
				BufferedInputStream bis = new BufferedInputStream(new FileInputStream(propFile));
				fileAssoc.load(bis);
				bis.close();
				System.err.println("The extensions are loaded");
			}
			else
				System.err.println("The extensions are not loaded");
		} catch(IOException ioe) {
			System.err.println("Input/output exception while loading the file assocciations from a file.");
			System.err.println("Stack trace follows.");
			ioe.printStackTrace();
		}
	}
}
