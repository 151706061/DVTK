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
 * The Original Code is MacOSXFileOpener.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk>
 */
package uk.ac.gla.terrier.applications.desktop.filehandling;
import javax.swing.JOptionPane;
/**
 * This class implements the interface FileOpener, using 
 * /usr/bin/open to open documents that LaunchServices has
 * associations for. 
 * @author Craig Macdonald, Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class MacOSXFileOpener implements FileOpener {
	/** Set to <tt>/usr/bin/open</tt> which is a useful MacOS X
	  * application for opening document using their associated
	  * application. */
	public final static String application = "/usr/bin/open";
	/**
	 * Opens the file with the given name, using either a 
	 * pre-defined application. It assumes
	 * that an non-zero exit code means that LaunchServices
	 * didn't have an application for it.
	 * @param filename the name of the file to open.
	 */
	public void open(String filename) {
		try {
			Process p = Runtime.getRuntime().exec(new String[]{application, filename});
			if(p.waitFor() != 0)
			{
				JOptionPane.showMessageDialog(null,
					"Couldn't open "+filename+" - this probably means that your "+
					"system doesn't have an association for this type of file. \n"+
					"To fix this, try opening the file in Finder",
				    "Couldn't open file",
				    JOptionPane.ERROR_MESSAGE);
			}
		} catch(Exception ioe) {
			System.err.println("Exception while executing application " + application);
			System.err.println("File "+filename);
			System.err.println("Stack trace follows");
			ioe.printStackTrace();
		}
	}
	
	/** Does nothing, not used */
	public void load(){}
	/** Does nothing, not used */
	public void save(){}
}
