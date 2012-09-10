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
 * The Original Code is DesktopTerrier.java.
 *
 * The Original Code is Copyright (C) 2004, 2005 the University of Glasgow.
 * All Rights Reserved.
 *
 * Contributor(s):
 *  Craig Macdonald <craigm{a.}dcs.gla.ac.uk> (original author)
 *  Vassilis Plachouras <vassilis{a.}dcs.gla.ac.uk> 
 */
package uk.ac.gla.terrier.applications.desktop;

import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.Component;
import java.awt.GridLayout;
import java.awt.Point;
import java.awt.Toolkit;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.File;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintStream;
import java.io.PrintWriter;
import java.io.StringReader;
import java.util.ArrayList;
import java.util.Hashtable;
import java.util.Vector;

import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JOptionPane;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JSplitPane;
import javax.swing.JTabbedPane;
import javax.swing.JTable;
import javax.swing.JTextArea;
import javax.swing.JTextField;
import javax.swing.KeyStroke;
import javax.swing.SwingUtilities;
import javax.swing.table.DefaultTableCellRenderer;
import javax.swing.table.DefaultTableModel;
import javax.swing.table.TableColumn;
import javax.swing.table.TableModel;
import javax.swing.text.BadLocationException;

import uk.ac.gla.terrier.applications.desktop.filehandling.AssociationFileOpener;
import uk.ac.gla.terrier.applications.desktop.filehandling.FileOpener;
import uk.ac.gla.terrier.applications.desktop.filehandling.MacOSXFileOpener;

import uk.ac.gla.terrier.indexing.BasicSplitDICOMIndexer;
import uk.ac.gla.terrier.indexing.Collection;
import uk.ac.gla.terrier.indexing.Indexer;
import uk.ac.gla.terrier.indexing.SimpleFileCollection;
import uk.ac.gla.terrier.matching.ResultSet;
import uk.ac.gla.terrier.querying.Manager;
import uk.ac.gla.terrier.querying.SearchRequest;
import uk.ac.gla.terrier.querying.parser.Query;
import uk.ac.gla.terrier.querying.parser.TerrierFloatLexer;
import uk.ac.gla.terrier.querying.parser.TerrierLexer;
import uk.ac.gla.terrier.querying.parser.TerrierQueryParser;
import uk.ac.gla.terrier.structures.CollectionStatistics;
import uk.ac.gla.terrier.structures.dicom.DICOMCollectionStatistics;
import uk.ac.gla.terrier.structures.Index;
import uk.ac.gla.terrier.structures.ComparableLexicon;
import uk.ac.gla.terrier.structures.Lexicon;
import uk.ac.gla.terrier.structures.dicom.TagLexicon;
import uk.ac.gla.terrier.structures.dicom.InvertedTagIndex;
import uk.ac.gla.terrier.structures.TermTagLexicon;
import uk.ac.gla.terrier.structures.DocumentIndex;
import uk.ac.gla.terrier.structures.DirectDICOMIndexInputStream;
import uk.ac.gla.terrier.structures.indexing.InvertedTagBuilder;
import uk.ac.gla.terrier.utility.ApplicationSetup;
import uk.ac.gla.terrier.utility.Rounding;
import uk.ac.gla.terrier.utility.QSort;
import antlr.TokenStreamSelector;
import antlr.TokenStreamException;
import antlr.RecognitionException;

/**
 * An application that uses the Terrier DICOM retrieval platform, in order to search a DICOM
 * collection locally.
 * 
 * @author Gerald van Veldhuijsen
 * @version 1.0
 */
public class DesktopTerrier extends JFrame {
	/**
	 * 
	 */
	private static final long serialVersionUID = -2575215486039546977L;

	//Resultsize
	int resultSetSize;
	//Current point in the result set
	int resultPoint;
	//Number of results per page
	int resultsPerPage = 10;
	
	
	//the colors used for the file types.
	private FiletypeColors filetypeColors = null;
	
	//check whether query is running
	private boolean queryRunning = false;
	
	private DesktopTerrier me = this;
	//the file opener to use
	private FileOpener fOpener = null;
	//the folders to index
	private IndexFolders indexFolders = null;
	//the about dialog
	private AboutTerrier aboutTerrier = null;
	
	//the help dialog
	private HelpDialog helpDialog = null;

	private static String mModel = "Matching";
	private static String wModel = ApplicationSetup.getProperty("weighting.model","PL2");
	//private static String wModel = "IFB2";
	
	private ArrayList folderList;
	private Manager queryingManager;
	private Index diskIndex;
	
	private ArrayList fileList;
	private class WinHandler extends WindowAdapter {
		public void windowClosing(WindowEvent we) {
			if (folderList != null) {
				String dirList = ApplicationSetup.makeAbsolute(
						ApplicationSetup.getProperty(
								"desktop.directories.spec", "desktop.spec"),
						ApplicationSetup.TERRIER_VAR);
				save_list(new File(dirList), folderList);
			}
			//save the used file associations
			fOpener.save();
			dispose(); // Frees program frame resources.
			System.exit(0); // Stops the program normally.
		} //windowClosing
	} //class WinHandler
	private javax.swing.JPanel jContentPane = null;
	private JMenuBar jJMenuBar = null;
	private JMenu jMenuFile = null;
	private JMenu jMenu = null;
	private JMenuItem jMenuItem = null;
	private JMenuItem jMenuItem1 = null;
	private JMenuItem jMenuItem2 = null;
	private JTabbedPane jTabbedPane = null;
	private JPanel searchPanel = null;
	private JPanel jPanel1 = null;
	private JTextField jTextField = null;
	private JButton jButton = null;
	private JPanel jPanel2 = null;
	private JPanel jPanel7 = null;
	private JTable jTable = null;
	private JScrollPane jScrollPane = null;
	private JButton jButton1 = null;
	private JButton jButton2 = null;
	private JButton jButton3 = null;
	private JButton jButton4 = null;
	private JTextArea jTextArea = null;
	/**
	 * This method initializes jJMenuBar
	 * 
	 * @return javax.swing.JMenuBar
	 */
	private JMenuBar getJJMenuBar() {
		if (jJMenuBar == null) {
			jJMenuBar = new JMenuBar();
			jJMenuBar.add(getJMenuFile());
			jJMenuBar.add(getJMenu());
		}
		return jJMenuBar;
	}
	/**
	 * This method initializes jMenu
	 * 
	 * @return javax.swing.JMenu
	 */
	private JMenu getJMenuFile() {
		if (jMenuFile == null) {
			jMenuFile = new JMenu();
			jMenuFile.setText("File");
			jMenuFile.setMnemonic('F');
			jMenuFile.add(getJMenuItem2());
		}
		return jMenuFile;
	}
	/**
	 * This method initializes jMenu
	 * 
	 * @return javax.swing.JMenu
	 */
	private JMenu getJMenu() {
		if (jMenu == null) {
			jMenu = new JMenu();
			jMenu.setText("Help");
			jMenu.setMnemonic('H');
			jMenu.add(getJMenuItem1());
			jMenu.add(getJMenuItem());
		}
		return jMenu;
	}
	/**
	 * This method initializes jMenuItem
	 * 
	 * @return javax.swing.JMenuItem
	 */
	private JMenuItem getJMenuItem() {
		if (jMenuItem == null) {
			jMenuItem = new JMenuItem();
			jMenuItem.setText("About");
			jMenuItem.setMnemonic('A');
			jMenuItem.setAccelerator(KeyStroke.getKeyStroke('A', Toolkit
					.getDefaultToolkit().getMenuShortcutKeyMask()));
			jMenuItem.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					aboutTerrier.setVisible(true);
				}
			});
		}
		return jMenuItem;
	}
	/**
	 * This method initializes jMenuItem1
	 * 
	 * @return javax.swing.JMenuItem
	 */
	private JMenuItem getJMenuItem1() {
		if (jMenuItem1 == null) {
			jMenuItem1 = new JMenuItem();
			jMenuItem1.setText("Desktop Search Help");
			jMenuItem1.setMnemonic('D');
			jMenuItem1.setAccelerator(KeyStroke.getKeyStroke("F1"));
			jMenuItem1.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					helpDialog.setVisible(true);
				}
			});
		}
		return jMenuItem1;
	}
	/**
	 * This method initializes jMenuItem2
	 * 
	 * @return javax.swing.JMenuItem
	 */
	private JMenuItem getJMenuItem2() {
		if (jMenuItem2 == null) {
			jMenuItem2 = new JMenuItem();
			jMenuItem2.setText("Quit");
			jMenuItem2.setMnemonic('Q');
			jMenuItem2.setAccelerator(KeyStroke.getKeyStroke('Q', Toolkit
					.getDefaultToolkit().getMenuShortcutKeyMask()));
			jMenuItem2.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if (folderList != null) {
						String dirList = ApplicationSetup.makeAbsolute(
								ApplicationSetup.getProperty(
										"desktop.directories.spec",
										"desktop.spec"),
								ApplicationSetup.TERRIER_VAR);
						save_list(new File(dirList), folderList);
					}
					//save the used file associations
					fOpener.save();
					dispose(); // Frees program frame resources.
					System.exit(0); // Stops the program normally.
				}
			});
		}
		return jMenuItem2;
	}
	/**
	 * This method initializes jTabbedPane
	 * 
	 * @return javax.swing.JTabbedPane
	 */
	private JTabbedPane getJTabbedPane() {
		if (jTabbedPane == null) {
			jTabbedPane = new JTabbedPane();
			jTabbedPane.addTab("Search", null, getSearchPanel(), null);
			jTabbedPane.addTab("Index", null, getJPanel1(), null);
		}
		return jTabbedPane;
	}
	/**
	 * This method initializes searchPanel
	 * 
	 * @return javax.swing.JPanel
	 */
	private JPanel getSearchPanel() {
		if (searchPanel == null) {
			searchPanel = new JPanel();
			searchPanel.setLayout(new BorderLayout());
			searchPanel.add(getJPanel2(), java.awt.BorderLayout.NORTH);
			searchPanel.add(getJScrollPane(), java.awt.BorderLayout.CENTER);
			searchPanel.add(getJPanel7(), java.awt.BorderLayout.SOUTH);
		}
		return searchPanel;
	}
	/**
	 * This method initializes jPanel1
	 * 
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel1() {
		if (jPanel1 == null) {
			jPanel1 = new JPanel();
			jPanel1.setLayout(new BorderLayout());
			jPanel1.add(getJPanel3(), java.awt.BorderLayout.NORTH);
			jPanel1.add(getJPanel(), java.awt.BorderLayout.CENTER);
		}
		return jPanel1;
	}
	/**
	 * This method initializes jTextField
	 * 
	 * @return javax.swing.JTextField
	 */
	private JTextField getJTextField() {
		if (jTextField == null) {
			jTextField = new JTextField();
			jTextField.setColumns(40);
			jTextField.addActionListener( new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					runQuery_thread(0,9);
				}
			});
		}
		return jTextField;
	}
	/**
	 * This method initializes jButton
	 * 
	 * @return javax.swing.JButton
	 */
	private JButton getJButton() {
		if (jButton == null) {
			jButton = new JButton();
			jButton.setText("Search");
			jButton.setMnemonic('S');
			String iconPath = ApplicationSetup.makeAbsolute(
					"terrier-desktop-search.gif",
					ApplicationSetup.TERRIER_SHARE);
			try {
				jButton.setIcon(new ImageIcon(iconPath, "Terrier icon"));
			} catch (NullPointerException npe) {
				System.err.println(npe);
				npe.printStackTrace();
			}
			jButton
					.setHorizontalTextPosition(javax.swing.SwingConstants.LEADING);
			jButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					runQuery_thread(0,9);
				}
			});
		}
		return jButton;
	}
	/**
	 * This method initializes jPanel2
	 * 
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel2() {
		if (jPanel2 == null) {
			jPanel2 = new JPanel();
			jPanel2.add(getJTextField(), null);
			jPanel2.add(getJButton(), null);
		}
		return jPanel2;
	}
	
	/**
	 * This method initializes jPanel7
	 * 
	 * @return javax.swing.JPanel
	 */
	private JPanel getJPanel7() {
		if (jPanel7 == null) {
			jPanel7 = new JPanel();
			jPanel7.add(getJButton3(), null);
			jPanel7.add(getJButton4(), null);
		}
		return jPanel7;
	}
	
	/**
	 * This method initializes jButton3
	 * 
	 * @return javax.swing.JButton
	 */
	private JButton getJButton3() {
		if (jButton3 == null) {
			jButton3 = new JButton();
			jButton3.setText("Previous");
			jButton3.setMnemonic('P');
			jButton3.setHorizontalTextPosition(javax.swing.SwingConstants.LEADING);
			jButton3.setEnabled(false);
			
			jButton3.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					runQuery_thread((resultPoint-2*resultsPerPage) , resultPoint-resultsPerPage-1);
				}
			});
		}
		return jButton3;
	}
	
	/**
	 * This method initializes jButton4
	 * 
	 * @return javax.swing.JButton
	 */
	private JButton getJButton4() {
		if (jButton4 == null) {
			jButton4 = new JButton();
			jButton4.setText("Next");
			jButton4.setMnemonic('N');
			jButton4.setHorizontalTextPosition(javax.swing.SwingConstants.LEADING);
			jButton4.setEnabled(false);
			jButton4.setPreferredSize(jButton3.getPreferredSize());
			
			jButton4.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					runQuery_thread(resultPoint, (resultPoint+resultsPerPage-1));
				}
			});
		}
		return jButton4;
	}
	
	/**
	 * This method initializes jTable
	 * 
	 * @return javax.swing.JTable
	 */
	private JTable getJTable() {
		if (jTable == null) {
			jTable = new JTable();
			jTable.setDoubleBuffered(true);
			jTable.addMouseListener(new MouseAdapter() {
				public void mousePressed(MouseEvent me) {
					JTable table = (JTable) me.getSource();
					Point p = me.getPoint();
					int row = table.rowAtPoint(p);
					int col = table.columnAtPoint(p);
					if ((me.getClickCount() == 2)) {
						try {
							// Open the file with associated viewer
							String filename = ""
									+ table.getValueAt(row, 2);
							fOpener.open(filename);
						} catch (Exception e) {
							e.printStackTrace();
						}
					}
				}
			});
			jTable
					.setSelectionMode(javax.swing.ListSelectionModel.SINGLE_SELECTION);
		}
		return jTable;
	}
	/**
	 * This method initializes jScrollPane
	 * 
	 * @return javax.swing.JScrollPane
	 */
	private JScrollPane getJScrollPane() {
		if (jScrollPane == null) {
			jScrollPane = new JScrollPane();
			jScrollPane.setViewportView(getJTable());
			jScrollPane
					.setHorizontalScrollBarPolicy(javax.swing.JScrollPane.HORIZONTAL_SCROLLBAR_AS_NEEDED);
			jScrollPane
					.setVerticalScrollBarPolicy(javax.swing.JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED);
			jScrollPane.setSize(new java.awt.Dimension(500,500));
			jScrollPane.setPreferredSize(new java.awt.Dimension(500,500));
		}
		return jScrollPane;
	}
	/**
	 * This method initializes jButton1
	 * 
	 * @return javax.swing.JButton
	 */
	private JButton getJButton1() {
		if (jButton1 == null) {
			jButton1 = new JButton();
			jButton1.setText("Select Folders...");
			jButton1.setMnemonic('F');
			jButton1.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					indexFolders.setFolders(folderList);
					indexFolders.renderFolders();
					indexFolders.setVisible(true);
					folderList = indexFolders.getFolders();
				}
			});
		}
		return jButton1;
	}
	/**
	 * This method initializes jButton2
	 * 
	 * @return javax.swing.JButton
	 */
	private JButton getJButton2() {
		if (jButton2 == null) {
			jButton2 = new JButton();
			jButton2.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					jButton1.setEnabled(false);//disable select folders
					jButton.setEnabled(false);//disable search
					jButton2.setEnabled(false);//disable index button
					jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), false);
					(new Thread() { 
						public void run() { 
							this.setPriority(Thread.NORM_PRIORITY-1); 
							runIndex();
							jButton1.setEnabled(true);
							jButton.setEnabled(true);
							jButton2.setEnabled(true);
							jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), true);
						}
					}).start();
				}
			});
			jButton2.setText("Create Index");
			jButton2.setMnemonic('I');
		}
		return jButton2;
	}
	/**
	 * This method initializes jTextArea
	 * 
	 * @return javax.swing.JTextArea
	 */
	private JTextArea getJTextArea() {
		if (jTextArea == null) {
			jTextArea = new JTextArea();
			jTextArea.setEditable(false);
			jTextArea.setWrapStyleWord(true);
		}
		return jTextArea;
	}
	/**
	 * This method initializes jScrollPane1
	 * 
	 * @return javax.swing.JScrollPane
	 */
	private JScrollPane getJScrollPane1() {
		if (jScrollPane1 == null) {
			jScrollPane1 = new JScrollPane();
			jScrollPane1.setViewportView(getJTextArea());
			jScrollPane1.setBorder(javax.swing.BorderFactory.createLineBorder(
					java.awt.Color.gray, 1));
			jScrollPane1.setPreferredSize(new java.awt.Dimension(2, 48));
			jScrollPane1
					.setVerticalScrollBarPolicy(javax.swing.JScrollPane.VERTICAL_SCROLLBAR_ALWAYS);
		}
		return jScrollPane1;
	}
	/**
	 * This method initializes jSplitPane
	 * 
	 * @return javax.swing.JSplitPane
	 */
	private JSplitPane getJSplitPane() {
		if (jSplitPane == null) {
			jSplitPane = new JSplitPane();
			jSplitPane.setOrientation(javax.swing.JSplitPane.VERTICAL_SPLIT);
			jSplitPane.setTopComponent(getJTabbedPane());
			jSplitPane.setBottomComponent(getJScrollPane1());
			jSplitPane.setDividerLocation(280);
			jSplitPane.setPreferredSize(new java.awt.Dimension(460,390));
			jSplitPane.setDividerSize(8);
		}
		return jSplitPane;
	}
	/**
	 * This method initializes jPanel3	
	 * 	
	 * @return javax.swing.JPanel	
	 */	
	private JPanel getJPanel3() {
		if (jPanel3 == null) {
			jPanel3 = new JPanel();
			jPanel3.add(getJButton1(), null);
			jPanel3.add(getJButton2(), null);
		}
		return jPanel3;
	}
	/**
	 * This method initializes jPanel	
	 * 	
	 * @return javax.swing.JPanel	
	 */	
	private JPanel getJPanel() {
		if (jPanel == null) {
			jLabel7 = new JLabel();
			jLabel6 = new JLabel();
			jLabel5 = new JLabel();
			jLabel4 = new JLabel();
			jLabel3 = new JLabel();
			jLabel2 = new JLabel();
			jLabel1 = new JLabel();
			jLabel = new JLabel();
			jPanel = new JPanel();
			jPanel.setLayout(new BorderLayout());
			jLabel.setText("Number of Documents: " + CollectionStatistics.getNumberOfDocuments());
			jLabel1.setText("Number of Tokens: " + CollectionStatistics.getNumberOfTokens());
			jLabel2.setText("Number of Unique Terms: " + CollectionStatistics.getNumberOfUniqueTerms());
			jLabel3.setText("Number of Pointers: " + CollectionStatistics.getNumberOfPointers());
			jLabel4.setText("Number of Comp Documents: " + CollectionStatistics.getNumberOfCompDocuments());
			jLabel5.setText("Number of Comp Tokens: " + CollectionStatistics.getNumberOfCompTokens());
			jLabel6.setText("Number of Comp Unique Terms: " + CollectionStatistics.getNumberOfCompUniqueTerms());
			jLabel7.setText("Number of Comp Pointers: " + CollectionStatistics.getNumberOfCompPointers());
			jPanel.add(getJPanel6(), java.awt.BorderLayout.NORTH);
		}
		return jPanel;
	}
	
	private JPanel getJPanel6(){
		if (jPanel6 == null) {
			BorderLayout borderLayout = new BorderLayout();
			jPanel6 = new JPanel();
			jPanel6.setLayout(borderLayout);
			jPanel6.add(getJPanel4(), java.awt.BorderLayout.WEST);
			jPanel6.add(getJPanel5(), java.awt.BorderLayout.EAST);
		}
		return jPanel6;
	}
	
	/**
	 * This method initializes jPanel4	
	 * 	
	 * @return javax.swing.JPanel	
	 */	
	private JPanel getJPanel4() {
		if (jPanel4 == null) {
			GridLayout gridLayout1 = new GridLayout();
			jPanel4 = new JPanel();
			jPanel4.setLayout(gridLayout1);
			gridLayout1.setRows(4);
			jPanel4.add(jLabel, null);
			jPanel4.add(jLabel2, null);
			jPanel4.add(jLabel3, null);
			jPanel4.add(jLabel1, null);
		}
		return jPanel4;
	}
	
	/**
	 * This method initializes jPanel4	
	 * 	
	 * @return javax.swing.JPanel	
	 */	
	private JPanel getJPanel5() {
		if (jPanel5 == null) {
			GridLayout gridLayout1 = new GridLayout();
			jPanel5 = new JPanel();
			jPanel5.setLayout(gridLayout1);
			gridLayout1.setRows(4);
			jPanel5.add(jLabel4, null);
			jPanel5.add(jLabel6, null);
			jPanel5.add(jLabel7, null);
			jPanel5.add(jLabel5, null);			
		}
		return jPanel5;
	}
	
	
	 	public static void main(String[] args) {
		DesktopTerrier dTerrier = new DesktopTerrier();
		if (args.length == 1 && args[0].equals("--runindex")) {
			dTerrier.runIndex();
			System.exit(0);
		} else if (args.length == 1 && args[0].equals("--runinvertedindex")) {
			dTerrier.runInvertedIndex();
			System.exit(0);
		} else if (args.length == 1 && args[0].equals("--rundirectindex")) {
			dTerrier.runDirectIndex();
			System.exit(0);
		} else if (args.length == 1 && args[0].equals("--runinvertedtagindex")) {
				dTerrier.runInvertedTagIndex();
				System.exit(0);		
		} else 
		{
			System.setErr(dTerrier.getOutputLog());
			if (args.length == 1 && args[0].equals("--debug")) {
				dTerrier.setDebug(true);
			}
			dTerrier.makeVisible();
		}
	}
	protected boolean desktop_debug = false;
	public void setDebug(boolean in)
	{
		desktop_debug = in;
	}
	/** The log stream. */
	private PrintStream outputLog = new PrintStream(System.err) {
		
		final int LINE_LIMIT = 1000;
		
		int lineCount = 0;
		
		public void print(Object o) {
			print(o == null ? "null" : o.toString());
		}
		public void println(Object o) {
			println(o == null ? "null" : o.toString());
		}		
		public void println(String x) {
			print(x+"\n");
		}
		public void print(String x) {
			if (desktop_debug)
				System.out.print(x);
			
			try {
				
				if ((lineCount = jTextArea.getLineCount()) > LINE_LIMIT) {
					jTextArea.replaceRange("", 0, jTextArea.getLineStartOffset(lineCount - LINE_LIMIT));
				}
				jTextArea.append(x);
				
				String text = jTextArea.getText();
				jTextArea.setCaretPosition( text == null ? 0 : text.length() );
			} catch(BadLocationException ble) {
				if (desktop_debug) {
					System.out.println("Bad location exception:");
					System.out.println(ble.toString());
				}
			}
		}
	};
	private JScrollPane jScrollPane1 = null;
	private JSplitPane jSplitPane = null;
	private JPanel jPanel3 = null;
	private JPanel jPanel = null;
	private JLabel jLabel = null;
	private JLabel jLabel1 = null;
	private JLabel jLabel2 = null;
	private JLabel jLabel3 = null;
	private JLabel jLabel4 = null;
	private JLabel jLabel5 = null;
	private JLabel jLabel6 = null;
	private JLabel jLabel7 = null;
	private JPanel jPanel4 = null;
	private JPanel jPanel5 = null;
	private JPanel jPanel6 = null;	
	
	private PrintStream getOutputLog() {
		return outputLog;
	}


	/** Shows the main window. Will ask user if they wish to index the documentation if no
	  * index can be successfully loaded. */
	public void makeVisible()
	{
		this.setVisible(true);
		if(folderList.size() == 0 && diskIndex == null)
		{
			int n = JOptionPane.showConfirmDialog(
				this,
				"It appears that this is the first time you have used Desktop Terrier.\nIf you "+
				"would like Terrier to index its own documentation, press \"Yes\".\n"+
				"You can change the folders Terrier indexes using \"Select Folders\".", 
				"Desktop Terrier",
				JOptionPane.YES_NO_OPTION);
			if (n == JOptionPane.YES_OPTION)
			{
				folderList.add(ApplicationSetup.TERRIER_HOME+
					ApplicationSetup.FILE_SEPARATOR+"doc");
				System.err.println(ApplicationSetup.TERRIER_HOME+ApplicationSetup.FILE_SEPARATOR+"doc");
				jButton1.setEnabled(false);//disable select folders
				jButton.setEnabled(false);//disable search
				jButton2.setEnabled(false);//disable index button
				SwingUtilities.invokeLater(new Thread() {
					public void run() {
						this.setPriority(Thread.NORM_PRIORITY-1);
						runIndex();
						jButton1.setEnabled(true);
						jButton.setEnabled(true);
						jButton2.setEnabled(true);
					}
				});
				
			}
		}	
	}

	/**
	 * This is the default constructor
	 */
	public DesktopTerrier() {
		super();
		//setting properties for the application
		//ApplicationSetup.BLOCK_INDEXING = true;
		ApplicationSetup.setProperty("indexing.max.tokens", "20000");
		//ApplicationSetup.setProperty("invertedfile.processterms","20000");
		//ApplicationSetup.setProperty("ignore.low.idf.terms","false");
		ApplicationSetup.setProperty("matching.dsms", "MetaModifier,DocumentFileNameModifier,BooleanFallback");
		filetypeColors = new FiletypeColors();
		initialize();
		addWindowListener(new WinHandler());
		//load in the directory list.
		String dirList = ApplicationSetup.makeAbsolute(ApplicationSetup
				.getProperty("desktop.directories.spec", "desktop.spec"),
				ApplicationSetup.TERRIER_VAR);
		folderList = load_list(new File(dirList));
		indexFolders = new IndexFolders(folderList, me);
		aboutTerrier = new AboutTerrier(this);
		helpDialog = new HelpDialog(this);
		//deciding which file opener to use based on the operating system
		String osName = System.getProperty("os.name").toLowerCase();
		//System.out.println("os.name="+osName);
		/*if (osName.startsWith("windows"))
		{
			//System.out.println("Using Windows associations");
			fOpener = new WindowsFileOpener();
		}
		else*/ if (osName.startsWith("mac"))
		{
			//System.out.println("using mac associations");
			fOpener = new MacOSXFileOpener();
		}
		else {
			//System.out.println("using default associations");
			fOpener = new AssociationFileOpener();
		}
		fOpener.load();
		if (loadIndices()) {
			//indices were loaded successfully - focus is on the search text field
			jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), true);
			jTabbedPane.setSelectedIndex(jTabbedPane.indexOfTab("Search"));
			try {
				if (ApplicationSetup.TAG_INDEXING)
					DICOMCollectionStatistics.initialise();
				else
					CollectionStatistics.initialise();
			} catch (IOException ioe) {
				System.err.println("Warning: Collection statistics failed to initialise.");
			}
			getJTextField().requestFocusInWindow(); 
			
		} else {
			//indices failed to load, probably because we've not indexed
			// anything yet
			jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), false);
			jTabbedPane.setSelectedIndex(jTabbedPane.indexOfTab("Index"));
		}
	}
	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		String iconPath = ApplicationSetup.makeAbsolute(
				"terrier-desktop-search.gif", ApplicationSetup.TERRIER_SHARE);
		try {
			this.setIconImage(Toolkit.getDefaultToolkit().getImage(iconPath));
			//this.setIconImage(new Image(iconPath,"Terrier icon"));
		} catch (NullPointerException npe) {
			System.err.println(npe);
			npe.printStackTrace();
		}
		this.setResizable(true);
		this.setJMenuBar(getJJMenuBar());
		this.setSize(753, 410);
		this.setContentPane(getJContentPane());
		this.setTitle("Terrier Desktop Search");
		this.setLocationRelativeTo(null);
	}
	/**
	 * This method initializes jContentPane
	 * 
	 * @return javax.swing.JPanel
	 */
	private javax.swing.JPanel getJContentPane() {
		if (jContentPane == null) {
			jContentPane = new javax.swing.JPanel();
			jContentPane.setLayout(new java.awt.BorderLayout());
			jContentPane.setPreferredSize(new java.awt.Dimension(0,0));
			jContentPane.add(getJSplitPane(), java.awt.BorderLayout.CENTER);
		}
		return jContentPane;
	}
	private void runQuery_thread(int start, int end) {
		final int start2 = start;
		final int end2 = end;
		jButton.setEnabled(false);
		jButton3.setEnabled(false);
		jButton4.setEnabled(false);
		SwingUtilities.invokeLater(new Thread() {
			public void run() {
				if (queryRunning)
					return;
				queryRunning = true;
				this.setPriority(Thread.NORM_PRIORITY-1);
				runQuery(start2, end2);
				jButton.setEnabled(true);
				getJTextField().requestFocusInWindow(); 
				queryRunning = false;
			}
		});
	}

	/** Parse the query */
	private Query parseQuery(String q)
	{
		Query rtr = null;
		try{
			TerrierLexer lexer = new TerrierLexer(new StringReader(q));
			TerrierFloatLexer flexer = new TerrierFloatLexer(lexer.getInputState());

			TokenStreamSelector selector = new TokenStreamSelector();
			selector.addInputStream(lexer, "main");
			selector.addInputStream(flexer, "numbers");
			selector.select("main");
			TerrierQueryParser parser = new TerrierQueryParser(selector);
			parser.setSelector(selector);
			rtr = parser.query();
			if (rtr == null)
			{
				System.err.println("Error parsing query: #"+ q +"#");
			}
		}catch (TokenStreamException e) {
			System.err.println("Exception parsing query: #"+ q +"# :");
			System.err.println(e.toString());
		} catch (RecognitionException e) {
			System.err.println("Exception parsing query: #"+ q +"# :");
			System.err.println(e.toString());
		}
		return rtr;
	}


	/**
	 * Processes the query and returns the results.
	 */
	private void runQuery(int start, int end) {
		System.out.println("Query from " + start + " to " + end);
		
		String query = jTextField.getText();
		if (query == null || query.length() == 0)
			return;
		try {
			Query q = parseQuery(query);	
			if (q == null)
			{
				//century kludge!
				//remove everything except character and spaces, and retry
				//query = query.replaceAll("[^a-zA-Z0-9 ]", "");

				q = parseQuery(query);
				if (q == null)
				{
					//give up
					return;
				}
			}
			System.err.println(q.getClass().getName());
			jTextField.setText(q.toString());	
			SearchRequest srq = queryingManager.newSearchRequest();
			srq.setQuery(q);
			srq.addMatchingModel(mModel, wModel);
			srq.setControl("c", "2.0d");
			srq.setControl("start", start + "");
			srq.setControl("end", end + "");
			queryingManager.runPreProcessing(srq);
			queryingManager.runMatching(srq);
			queryingManager.runPostProcessing(srq);
			resultSetSize = srq.getResultSet().getResultSize();
			
			resultPoint = start;
			
			if (resultPoint>0)
				jButton3.setEnabled(true);
			else
				jButton3.setEnabled(false);
			
			queryingManager.runPostFilters(srq);
			renderResults(srq.getResultSet());
			
			resultPoint = end+1;
			
			if (resultPoint<resultSetSize)
				jButton4.setEnabled(true);
			else
				jButton4.setEnabled(false);
			
		} catch (Exception e) {
			System.err.println("Exception parsing query: #"+query +"# :");
			System.err.println(e.toString());
			e.printStackTrace();
		}

	}
	
	/**
	 * Loads and returns a list of folders already 
	 * selected for indexing, from the given file.
	 * @param file the file from which to load 
	 *		the list of folders.
	 * @return the list of folders to index.
	 */
	private ArrayList load_list(File file) {
		if (file == null || !file.exists())
			return new ArrayList();
		ArrayList out = new ArrayList();
		try {
			BufferedReader buf = new BufferedReader(new FileReader(file));
			String line;
			while ((line = buf.readLine()) != null) {
				//ignore empty lines, or lines starting with # from the methods
				// file.
				if (line.startsWith("#") || line.equals(""))
					continue;
				out.add(line.trim());
			}
			buf.close();
		} catch (IOException ioe) {
		}
		return out;
	}
	/**
	 * Saves the list of selected folders to index
	 * in the file with the given name.
	 * @param file the name of the file in which to 
	 *		save the selected folders.
	 * @param list the list of selected folders to save.
	 */
	private void save_list(File file, ArrayList list) {
		try {
			PrintWriter writer = new PrintWriter(new BufferedWriter(
					new FileWriter(file)));
			for (int i = 0; i < list.size(); i++) {
				writer.println(list.get(i));
			}
			writer.close();
		} catch (IOException ioe) {
			System.err.println("Error writing file : " + file + " : " + ioe);
			return;
		}
	}
	
	private boolean deleteFiles(File dir)
	{
		boolean succes = true;
		String[] files = dir.list();
		for (int i = 0; i < files.length; i++) {
			File f = new File(dir, files[i]);
			if (f.exists()) 
			{
				if (f.isFile())
				{
					boolean del = f.delete();
					System.err.println("Deleting: "+f+": "+ del);
					succes &= del;
				}
				else if (f.isDirectory() && ! f.getName().equals("CVS"))
				{
					deleteFiles(f);
					boolean del = f.delete();
					System.err.println("Deleting: "+f+": "+del);
					succes &= del;
				}
			}
		}
		return succes;
	}
	
	private int getFolderSize(File file){
		File thisFile = file;
		int size = 0;
		if (thisFile.isDirectory()){
			File [] folderContent = thisFile.listFiles();
			for (int i=0; i<folderContent.length; i++)
				size+= getFolderSize(folderContent[i]);
		} else if (thisFile.exists())
			size++;
		
		return size;
	}
	
	/**
	 * Runs the direct indexing process for the documents 
	 * in the selected folders.
	 */
	private void runDirectIndex() {		
			
		if (folderList == null || folderList.size() == 0)
		{
			System.err.println("No folders to index");
			return;
		}
		
		//Determine collection size
		int totalsize = 0;
		for(int i = 0 ; i< folderList.size(); i++){
			totalsize += getFolderSize(new File((String)folderList.get(i)));
		}
		System.err.println("Collection size is " + totalsize );
	
		try {
			//deleting existing files
			if (diskIndex!=null) {
				diskIndex.close();
				diskIndex = null;
			}
				
			if(!deleteFiles(new File(ApplicationSetup.TERRIER_INDEX_PATH))){
				System.err.println("Could not delete all old indexes. Stopping");
				System.err.println("Maybe the web application is still deployed?");
			}
			else{
				
				Indexer indexer;
				System.out.println("Using BasicSplitDICOMIndexer");
				indexer = new BasicSplitDICOMIndexer(ApplicationSetup.TERRIER_INDEX_PATH);
				
				SimpleFileCollection sfc = new SimpleFileCollection(folderList, true);
				sfc.setSize(totalsize);
				long start = System.currentTimeMillis();
				indexer.createDirectIndex(new Collection[] { sfc });
				fileList = sfc.getFileList();
				save_list(new File(ApplicationSetup.makeAbsolute(ApplicationSetup
						.getProperty("desktop.directories.filelist", "data.filelist"),
						ApplicationSetup.TERRIER_INDEX_PATH)), fileList);
				long end = System.currentTimeMillis();
				
				System.out.println("Total direct indexing process took " + (end-start)/1000 + " seconds");
			}
			
		} catch(Exception e) {
			System.err.println("Exception while indexing.");
			e.printStackTrace();
		}
	}
	
	/**
	 * Runs the indexing process for the documents 
	 * in the selected folders.
	 */
	private void runIndex() {		
		jLabel.setText("Number of Documents: ");
		jLabel1.setText("Number of Tokens: ");
		jLabel2.setText("Number of Unique Terms: ");
		jLabel3.setText("Number of Pointers: ");
		jLabel4.setText("Number of Comp Documents: ");
		jLabel5.setText("Number of Comp Tokens: ");
		jLabel6.setText("Number of Comp Unique Terms: ");
		jLabel7.setText("Number of Comp Pointers: ");
	
		if (folderList == null || folderList.size() == 0)
		{
			System.err.println("No folders to index");
			return;
		}
		
		//Determine collection size
		int totalsize = 0;
		for(int i = 0 ; i< folderList.size(); i++){
			totalsize += getFolderSize(new File((String)folderList.get(i)));
		}
		System.err.println("Collection size is " + totalsize );
	
		try {
			//deleting existing files
			if (diskIndex!=null) {
				diskIndex.close();
				diskIndex = null;
			}
				
			if(!deleteFiles(new File(ApplicationSetup.TERRIER_INDEX_PATH))){
				System.err.println("Could not delete all old indexes. Stopping");
				System.err.println("Maybe the web application is still deployed?");
			}
			else{
				
				Indexer indexer;
				System.out.println("Using BasicSplitDICOMIndexer");
				indexer = new BasicSplitDICOMIndexer(ApplicationSetup.TERRIER_INDEX_PATH);
				
				SimpleFileCollection sfc = new SimpleFileCollection(folderList, true);
				sfc.setSize(totalsize);
				long start = System.currentTimeMillis();
				indexer.createDirectIndex(new Collection[] { sfc });
				fileList = sfc.getFileList();
				save_list(new File(ApplicationSetup.makeAbsolute(ApplicationSetup
						.getProperty("desktop.directories.filelist", "data.filelist"),
						ApplicationSetup.TERRIER_INDEX_PATH)), fileList);
				indexer = null;
				indexer = new BasicSplitDICOMIndexer(ApplicationSetup.TERRIER_INDEX_PATH);
				System.gc();
				indexer.createInvertedIndex();
				long end = System.currentTimeMillis();
				
				System.err.println("Total indexing process took " + (end-start)/1000 + " seconds");
				
				//load in the indexes
				if (loadIndices()) {
					//indices loaded
					jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), true);
					jTabbedPane.setSelectedIndex(jTabbedPane.indexOfTab("Search"));
					if (ApplicationSetup.TAG_INDEXING)
						DICOMCollectionStatistics.initialise();
					else
						CollectionStatistics.initialise();
					getJTextField().requestFocusInWindow(); 
					
				} else { //indices failed to load, probably because we've not indexed
						 // anything yet
					jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), false);
					jTabbedPane.setSelectedIndex(jTabbedPane.indexOfTab("Index"));
				}
				jLabel.setText("Number of Documents: " + CollectionStatistics.getNumberOfDocuments());
				jLabel1.setText("Number of Tokens: " + CollectionStatistics.getNumberOfTokens());
				jLabel2.setText("Number of Unique Terms: " + CollectionStatistics.getNumberOfUniqueTerms());
				jLabel3.setText("Number of Pointers: " + CollectionStatistics.getNumberOfPointers());
				jLabel4.setText("Number of Comp Documents: " + CollectionStatistics.getNumberOfCompDocuments());
				jLabel5.setText("Number of Comp Tokens: " + CollectionStatistics.getNumberOfCompTokens());
				jLabel6.setText("Number of Comp Unique Terms: " + CollectionStatistics.getNumberOfCompUniqueTerms());
				jLabel7.setText("Number of Comp Pointers: " + CollectionStatistics.getNumberOfCompPointers());
			}
			
		} catch(Exception e) {
			System.err.println("Exception while indexing.");
			e.printStackTrace();
		}
	}
	
	/**
	 * Runs the inverted indexing process for the documents 
	 * in the selected folders.
	 */
	private void runInvertedIndex() {		
	
		if (folderList == null || folderList.size() == 0)
		{
			System.err.println("No folders to index");
			return;
		}
			
		try {
			//deleting existing files
			if (diskIndex!=null) {
				diskIndex.close();
				diskIndex = null;
			}
				long start = System.currentTimeMillis();
				Indexer indexer;
				System.out.println("Using BasicSplitDICOMIndexer");
				indexer = new BasicSplitDICOMIndexer(ApplicationSetup.TERRIER_INDEX_PATH);
				System.gc();
				indexer.createInvertedIndex();
				long end = System.currentTimeMillis();
				
				System.out.println("Total inverted indexing process took " + (end-start)/1000 + " seconds");
				
				//load in the indexes
				if (loadIndices()) {
					//indices loaded
					jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), true);
					jTabbedPane.setSelectedIndex(jTabbedPane.indexOfTab("Search"));
					if (ApplicationSetup.TAG_INDEXING)
						DICOMCollectionStatistics.initialise();
					else
						CollectionStatistics.initialise();
					getJTextField().requestFocusInWindow(); 
					
				} else { //indices failed to load, probably because we've not indexed
						 // anything yet
					jTabbedPane.setEnabledAt(jTabbedPane.indexOfTab("Search"), false);
					jTabbedPane.setSelectedIndex(jTabbedPane.indexOfTab("Index"));
				}
		} catch(Exception e) {
			System.err.println("Exception while indexing.");
			e.printStackTrace();
		}
	}
	
	/**
	 * Runs the inverted indexing process for the documents 
	 * in the selected folders.
	 */
	private void runInvertedTagIndex() {		
				
		try {
				//load in the indexes
				if (loadIndices()) {
					ComparableLexicon CL = diskIndex.getComparableLexicon(); 
					TagLexicon TL = diskIndex.getTagLexicon();
					
					//Get term for the given treshold
					CL.getTermsWithFreq(ApplicationSetup.INVERTTAG_TRESHOLD);
					String [] terms = CL.getTermArray();
					int[] termIds = CL.getTermIdArray();
						
					//Sort the term per id
					new QSort().sort(termIds, terms);
						
					//Invert the tag
					InvertedTagBuilder iiTagBuilder = new InvertedTagBuilder();
					String [] rootTags = ApplicationSetup.getProperty("root.tag.name", "dvtdetailedresultsfile").split("\\s*,\\s*");
					for (int i=0; i<rootTags.length; i++){
					if (TL.findTag(rootTags[i]))
						System.out.println("Adding rootTagId " + TL.getTagId());
						iiTagBuilder.addRootTagId(TL.getTagId());
					}
					long start = System.currentTimeMillis();
					iiTagBuilder.invertTags(terms);
					System.out.println("Total time to invert tags: " + (System.currentTimeMillis()-start)/1000 + " seconds");
				} 
		} catch(Exception e) {
			System.err.println("Exception while indexing.");
			e.printStackTrace();
		}
	}
	
	/**
	 * Returns true if loading the index succeeded and the system is ready for
	 * querying. If false, then the collection needs to be indexed first.
	 */
	private boolean loadIndices() {
			diskIndex = Index.createIndex();
			fileList = load_list(new File(ApplicationSetup.makeAbsolute(
					ApplicationSetup.getProperty("desktop.directories.filelist",
							"data.filelist"), ApplicationSetup.TERRIER_INDEX_PATH)));
			if (diskIndex == null || fileList.size() == 0)
			{	

				if (diskIndex != null)
					diskIndex.close();
				diskIndex = null;
			
				return false;
			}
			queryingManager = new Manager(diskIndex);
	    
		//System.out.println("Dumping TagLexicon");
		//TagLexicon TL = diskIndex.getTagLexicon();
		//TL.print();
						
		/*
		System.out.println("Dumping document index");	
		DocumentIndex D = diskIndex.getDocumentIndex();
		D.print();*/	
		
		
	    //System.out.println("Dumping comparable lexicon");
		//ComparableLexicon CL = diskIndex.getComparableLexicon(); 
		//CL.print();
		
		/*
		System.out.println("Dumping lexicon");
		Lexicon L = diskIndex.getLexicon();
		//System.out.println("LexiconEntries:" + L.getNumberOfLexiconEntries());
		L.print();*/
		
		return true;
	}
	/**
	 * Render the given resultset into the tblResults.
	 * @param rs The result set to render
	 */
	private void renderResults(ResultSet rs) {
		Vector HeaderRow = new Vector(4);
		HeaderRow.add(" ");
		//HeaderRow.add("File Type");
		HeaderRow.add("Filename");
		HeaderRow.add("Directory");
		HeaderRow.add("Score");
		int ResultsSize = rs.getResultSize();
		int startPoint = resultPoint+1;
		
		System.err.print(rs.getInfoMessage());
		
		int[] docids = rs.getDocids();
		double[] scores = rs.getScores();
		Vector rows = new Vector(ResultsSize);
		for (int i = 0; i < ResultsSize; i++) {
			Vector thisRow = new Vector(4);
			thisRow.add("" + (i + startPoint));
			String f = (String) fileList.get(docids[i]);
			
			if (f == null)
				continue;
			int dotIndex = f.lastIndexOf('.');
			String extension = f.substring(dotIndex+1);
			String[] dcmnames = new File(f).getName().split("_");
			String dcmname = f;
			if (dcmnames.length>1){
				dcmname = "";
				extension = "dcm";
				if (dcmnames[dcmnames.length - 2].equals("DCM") || dcmnames[dcmnames.length - 2].equals("DDIR")){
					for(int k=2; k<dcmnames.length-2; k++)
						dcmname += dcmnames[k] + "_";
				}
				else{
					for(int k=2; k<dcmnames.length-1; k++)
						dcmname += dcmnames[k] + "_";
				}
				if (dcmname.length()>0){
					dcmname = dcmname.substring(0, dcmname.length()-1);
					int slashindex = f.lastIndexOf('\\');
					f = f.substring(0,slashindex) + '\\' + dcmname;
				}
				f = f.replaceFirst("representations", "data");
				
			}
			//thisRow.add(filetypeColors.getFiletype(extension));
			thisRow.add(new File(dcmname).getName());
			//System.out.println(f);
			File tmp = new File(f);
			if (!tmp.exists()){
				int point = f.lastIndexOf('_');
				if (point>-1){
					char [] chars = f.toCharArray();
					chars [point] = '.';
					f = new String(chars);
				}
			}
			thisRow.add(new File(f).getPath());
			thisRow.add(Rounding.toString(scores[i], 4));
			rows.add(thisRow);
		}
		TableModel model = new NonEditableTableModel(rows, HeaderRow);
		jTable.setModel(model);
		TableColumn col = jTable.getColumnModel().getColumn(0);
		col.setPreferredWidth(30);
		col.setMinWidth(20);
		col.setMaxWidth(100);
		col = jTable.getColumnModel().getColumn(1);
		col.setPreferredWidth(75);
		col.setMinWidth(50);
		col.setMaxWidth(100);
		//col.setCellRenderer(new CustomTableCellRenderer(filetypeColors));
		col = jTable.getColumnModel().getColumn(3);
		col.setPreferredWidth(50);
		col.setMinWidth(20);
		col.setMaxWidth(100);

	}
}  //  @jve:decl-index=0:visual-constraint="10,10"
/**
 * Extends the default table model by overriding the isEditable method.
 * @author Vassilis Plachouras
 */
class NonEditableTableModel extends DefaultTableModel {
	/**
	 * A constructor that calls the constructor of the super class.
	 * @param rows a vector containing the data.
	 * @param columnNames a vector containing the column names
	 */
	public NonEditableTableModel(Vector rows, Vector columnNames) {
		super(rows, columnNames);
	}
	/**
	 * Makes the cells of the table non-editable, by default.
	 */
	public boolean isCellEditable(int row, int column) {
		return false;
	}
}

/**
 * Used for assigning a color to each filetype.
 * @author Vassilis Plachouras
 */
class FiletypeColors {
	
	Hashtable typeColors = new Hashtable();
	
	Hashtable filetypes = new Hashtable();
	/**
	 * A default constructor that reads the property
	 * <tt>desktopsearch.filetype.colors</tt> and assigns the
	 * colors used for the file types.
	 */
	public FiletypeColors() {
		String staticMappings = 
			ApplicationSetup.getProperty("desktopsearch.filetype.colors",
					"Text:(221 221 221),TeX:(221 221 221),Bib:(221 221 221),PDF:(236 67 69),"+
					"HTML:(177 228 250),Word:(100 100 255),Powerpoint:(250 110 49),Excel:(38 183 78),"+
					"XHTML:(177 228 250),XML:(177 228 250)");
		String staticTypes = 
			ApplicationSetup.getProperty("desktopsearch.filetype.types",
			"txt:Text,text:Text,tex:TeX,bib:Bib,pdf:PDF,html:HTML,htm:HTML,xhtml:XHTML,xml:XML,doc:Word,ppt:Powerpoint,xls:Excel");
		if (staticMappings.length() > 0) {
			String[] mappings = staticMappings.split("\\s*,\\s*");
			for(int i=0;i<mappings.length;i++)
			{
				if (mappings[i].indexOf(":") < 1)
					continue;
				String[] mapping = mappings[i].split(":");
				if (mapping.length == 2 && mapping[0].length() > 0 && mapping[1].length() > 0) {
					String[] colorComponents = mapping[1].substring(1,mapping[1].length()-1).split("\\s* \\s*");
					if (colorComponents.length==3) {
						Color c = new Color(Integer.parseInt(colorComponents[0]),
					            			Integer.parseInt(colorComponents[1]),
											Integer.parseInt(colorComponents[2]));
						typeColors.put(mapping[0], c);
						
					}
				}
			}				
		}
		if (staticTypes.length() > 0) {
			String[] mappings = staticTypes.split("\\s*,\\s*");
			for(int i=0;i<mappings.length;i++)
			{
				if (mappings[i].indexOf(":") < 1)
					continue;
				String[] mapping = mappings[i].split(":");
				if (mapping.length == 2 && mapping[0].length() > 0 && mapping[1].length() > 0) {
					filetypes.put(mapping[0], mapping[1]);
				}
			}		 
		}
	}
	/**
	 * Returns the color associated with a file type.
	 * @param fileType the type of the file we need to get a color for.
	 * @return Color the color associated with the file type.
	 */
	public Color getColor(String fileType) {
		Color rtrColor = (Color)typeColors.get(fileType);
		if (rtrColor==null) 
			return Color.GRAY;
		return rtrColor;
	}
	
	/**
	 * Returns the a string denoting the file type for a given extension.
	 * @param fileExtension the extension of the file.
	 * @return String the type of the file with the given extension.
	 */
	public String getFiletype(String fileExtension) {
		String rtrType = (String)filetypes.get(fileExtension);
		if (rtrType==null)
			return "Unknown";
		return rtrType;
	}
}

/**
 * Implements a custom renderer for the cells of the
 * table that contain the file type information.
 * @author Vassilis Plachouras
 */
class CustomTableCellRenderer extends DefaultTableCellRenderer {
    
	FiletypeColors filetypeColors = null;
	/**
	 * A constructor for setting the file types and associations of the cells of a table.
	 * @param filetypeColors the associated colors and types for the file extensions
	 */
	public CustomTableCellRenderer(FiletypeColors filetypeColors) {
		super();
		this.filetypeColors = filetypeColors; 
	}
	public Component getTableCellRendererComponent(JTable table, Object value, boolean isSelected,
       boolean hasFocus, int row, int column) {
        Component cell = super.getTableCellRendererComponent
           (table, value, isSelected, hasFocus, row, column);

        if( value instanceof String ) {
            String type = (String) value;
            cell.setBackground(filetypeColors.getColor(type));
        }

        return cell;
    }
}

 
