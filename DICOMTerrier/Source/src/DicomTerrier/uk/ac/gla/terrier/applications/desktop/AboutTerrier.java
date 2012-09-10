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
 * The Original Code is AboutTerrier.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *   Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> (original author)
 */
package uk.ac.gla.terrier.applications.desktop;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextPane;
import javax.swing.event.HyperlinkEvent;
import javax.swing.event.HyperlinkListener;

import uk.ac.gla.terrier.applications.desktop.filehandling.FileOpener;
import uk.ac.gla.terrier.applications.desktop.filehandling.MacOSXFileOpener;
import uk.ac.gla.terrier.applications.desktop.filehandling.WindowsFileOpener;
import uk.ac.gla.terrier.utility.ApplicationSetup;
/**
 * The about dialog for the desktop search application.
 * @author Vassilis Plachouras
 * @version $Revision: 1.1 $
 */
public class AboutTerrier extends JDialog {
	private String imagePath = null;
	
	private javax.swing.JPanel jContentPane = null;
	private JLabel jLabel = null;
	private JPanel jPanel = null;
	private JButton jButton = null;
	private JTextPane jTextPane = null;
	private JScrollPane jScrollPane = null;
	/**
	 * This is the default constructor
	 */
	public AboutTerrier() {
		super();
		imagePath = ApplicationSetup.TERRIER_SHARE;
		if (imagePath == null) 
			imagePath = "../share/";
		initialize();
	}
	public AboutTerrier(JFrame parent) {
		this();
		this.setLocationRelativeTo(parent);
	}
	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		this.setBackground(java.awt.SystemColor.window);
		this.setModal(true);
		this.setResizable(false);
		this.setTitle("About Terrier Desktop Search");
		this.setSize(276, 275);
		this.setContentPane(getJContentPane());
	}
	/**
	 * This method initializes jContentPane
	 * 
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJContentPane() {
		if (jContentPane == null) {
			jLabel = new JLabel();
			jContentPane = new javax.swing.JPanel();
			jContentPane.setLayout(new java.awt.BorderLayout());
			
			jLabel.setText("");
			String iconPath = ApplicationSetup.makeAbsolute("terrier-logo-web.jpg", ApplicationSetup.TERRIER_SHARE);
			try {
				jLabel.setIcon(new ImageIcon(iconPath, "Terrier logo"));
			} catch(NullPointerException npe) {
				System.err.println(npe);
				npe.printStackTrace();
			}
			jLabel.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
			jLabel.setBackground(java.awt.SystemColor.window);
			jContentPane.setBackground(java.awt.SystemColor.window);
			jContentPane.add(jLabel, java.awt.BorderLayout.NORTH);
			jContentPane.add(getJPanel(), java.awt.BorderLayout.SOUTH);
			jContentPane.add(getJScrollPane(), java.awt.BorderLayout.CENTER);
		}
		return jContentPane;
	}
	/**
	 * This method initializes jPanel
	 * 
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel() {
		if (jPanel == null) {
			jPanel = new JPanel();
			jPanel.setBackground(new java.awt.Color(204, 204, 204));
			jPanel.add(getJButton(), null);
		}
		return jPanel;
	}
	/**
	 * This method initializes jButton
	 * 
	 * @return javax.swing.JButton
	 */
	private JButton getJButton() {
		if (jButton == null) {
			jButton = new JButton();
			jButton.setText("OK");
			jButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					//System.out.println("actionPerformed()"); // TODO
					// Auto-generated Event stub actionPerformed()
					setVisible(false);
				}
			});
		}
		return jButton;
	}
	/**
	 * This method initializes jTextPane	
	 * 	
	 * @return javax.swing.JTextPane	
	 */    
	private JTextPane getJTextPane() {
		if (jTextPane == null) {
			jTextPane = new JTextPane();
			jTextPane.setContentType("text/html");
			jTextPane.setText(
					"<html><head></head>" +
					"<body><p>Terrier Desktop Search is an application " +
					"demonstrating how to use the Terrier Information " +
					"Retrieval Platform for desktop searching.</p> " +
					"<p>It is distributed under the terms of the " +
					"<a href=\"http://www.mozilla.org/MPL/MPL-1.1.html\">" +
					"Mozilla Public License (MPL)</a>.</p> " +
					"Homepage: <a href=\"http://ir.dcs.gla.ac.uk/terrier/\">http://ir.dcs.gla.ac.uk/terrier/</a><br>"+
					"<a href=\"http://ir.dcs.gla.ac.uk/\">Information Retrieval Group</a><br>"+
					"<a href=\"http://www.dcs.gla.ac.uk/\">Department of Computing Science</a><br>"+
					"Copyright (C) 2004, 2005 <a href=\"http://www.gla.ac.uk/\">University of Glasgow</a>. All Rights Reserved."+
					"</body></html>");
			jTextPane.setEditable(false);
			jTextPane.addHyperlinkListener(new HyperlinkListener() {
				public void hyperlinkUpdate(HyperlinkEvent e) {
					if (e.getEventType() == HyperlinkEvent.EventType.ACTIVATED) {
						try {
							String osname = System.getProperty("os.name").toLowerCase();
							if (osname.startsWith("windows")) {
								FileOpener winFileOpener = new WindowsFileOpener();
								winFileOpener.open(e.getURL().toString());
							} else if (osname.startsWith("mac")) {
								FileOpener winFileOpener = new MacOSXFileOpener();
								winFileOpener.open(e.getURL().toString());
							} else if (osname.startsWith("linux")) {
								Runtime.getRuntime().exec(new String[] {"/usr/bin/mozilla", 
										  								e.getURL().toString()});
							}
						} catch(Exception exc) {
							System.out.println(exc.toString());
							exc.printStackTrace();
						}

					}
				}
			});
			jTextPane.setComponentOrientation(java.awt.ComponentOrientation.LEFT_TO_RIGHT);
			jTextPane.setPreferredSize(new java.awt.Dimension(1,1));
		}
		return jTextPane;
	}
	/**
	 * This method initializes jScrollPane	
	 * 	
	 * @return javax.swing.JScrollPane	
	 */    
	private JScrollPane getJScrollPane() {
		if (jScrollPane == null) {
			jScrollPane = new JScrollPane();
			jScrollPane.setViewportView(getJTextPane());
			jScrollPane.setPreferredSize(new java.awt.Dimension(100,100));
		}
		return jScrollPane;
	}
  }  //  @jve:decl-index=0:visual-constraint="10,10"
