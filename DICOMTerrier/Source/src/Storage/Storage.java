import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.FlowLayout;
import java.awt.Font;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowAdapter;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileWriter;
import java.io.FilenameFilter;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.util.logging.Logger;

import javax.swing.ButtonGroup;
import javax.swing.DefaultBoundedRangeModel;
import javax.swing.JButton;
import javax.swing.JDialog;
import javax.swing.JFileChooser;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.JPanel;
import javax.swing.JProgressBar;
import javax.swing.JRadioButton;
import javax.swing.JTextField;
import javax.swing.JTextPane;

import dicomxml.XMLRenamer;

/**
 * Storage tool to automatically upload DICOM datasets to a specific location
 *  
 * @author Gerald van Veldhuijsen
 * @version 1.0
 * Testing
 */
public class Storage extends JFrame {
	
	private static final long serialVersionUID = -5926931368468022002L;

	private static Logger logger = Logger.getLogger("Storage Logger"); 
	
	private boolean errors = false;
	private boolean isDir = false;
		
	private JPanel jContentPane = null;

	private JMenuBar jJMenuBar = null;

	private JMenu fileMenu = null;

	private JMenu editMenu = null;

	private JMenu helpMenu = null;

	private JMenuItem exitMenuItem = null;

	private JMenuItem aboutMenuItem = null;

	private JMenuItem propertiesMenuItem = null;

	private JPanel jPanel2 = null;

	private JTextPane jTextPane = null;

	private JLabel jLabel = null;

	private JPanel jPanel4 = null;

	private JTextField jTextField1 = null;

	private JButton jButton1 = null;

	private JPanel jPanel5 = null;

	private JLabel jLabel1 = null;

	private JPanel jPanel = null;

	private JButton jButton = null;

	private JButton jButton2 = null;

	private JPanel jPanel3 = null;

	private JProgressBar jProgressBar = null;

	private JLabel jLabel2 = null;

	private JPanel jPanel6 = null;

	private JRadioButton jRadioButton = null;

	private JPanel jPanel8 = null;

	private JLabel jLabel3 = null;

	private JRadioButton jRadioButton1 = null;

	private JLabel jLabel4 = null;

	private ButtonGroup buttonGroup = null;

	private JDialog jDialog = null;  //  @jve:decl-index=0:visual-constraint="129,0"
	
	private JDialog jDialog2 = null;  //  @jve:decl-index=0:visual-constraint="26,176"
	
	private JDialog aboutDialog = null; 

	private JPanel jContentPane1 = null;

	private JLabel jLabel5 = null;
	
	private JPanel jPanel1 = null;

	private JButton jButton3 = null;

	private JButton jButton4 = null;

	private JPanel jPanel7 = null;

	private JLabel jLabel6 = null;

	private JTextField jTextField = null;

	private JPanel jContentPane2 = null;

	private JPanel jPanel9 = null;

	private JLabel jLabel7 = null;

	private JTextField jTextField2 = null;

	private JPanel jPanel10 = null;

	private JPanel jPanel11 = null;

	private JButton jButton5 = null;

	private JButton jButton6 = null;

	/**
	 * This method initializes jPanel2	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel2() {
		if (jPanel2 == null) {
			jLabel = new JLabel();
			jLabel.setText("Description");
			jLabel.setHorizontalTextPosition(javax.swing.SwingConstants.LEFT);
			jLabel.setHorizontalAlignment(javax.swing.SwingConstants.LEFT);
			jPanel2 = new JPanel();
			jPanel2.setLayout(new FlowLayout(FlowLayout.LEFT));
			jPanel2.setMaximumSize(new java.awt.Dimension(368,110));
			jPanel2.setPreferredSize(new java.awt.Dimension(350,110));
			jPanel2.add(jLabel, null);
			jPanel2.add(getJTextPane(), null);
			
		}
		return jPanel2;
	}

	/**
	 * This method initializes jTextPane	
	 * 	
	 * @return javax.swing.JTextPane	
	 */
	private JTextPane getJTextPane() {
		if (jTextPane == null) {
			jTextPane = new JTextPane();
			jTextPane.setMinimumSize(new java.awt.Dimension(300,100));
			jTextPane.setPreferredSize(new java.awt.Dimension(300,100));
		}
		return jTextPane;
	}

	/**
	 * This method initializes jPanel4	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel4() {
		if (jPanel4 == null) {
			FlowLayout flowLayout = new FlowLayout();
			flowLayout.setAlignment(java.awt.FlowLayout.LEFT);
			jPanel4 = new JPanel();
			jPanel4.setPreferredSize(new java.awt.Dimension(300,33));
			jPanel4.setLayout(flowLayout);
			jPanel4.add(getJTextField1(), null);
			jPanel4.add(getJButton1(), null);
		}
		return jPanel4;
	}

	/**
	 * This method initializes jTextField1	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getJTextField1() {
		if (jTextField1 == null) {
			jTextField1 = new JTextField();
			jTextField1.setPreferredSize(new java.awt.Dimension(270,20));
			jTextField1.setEditable(false);
		}
		return jTextField1;
	}

	/**
	 * This method initializes jButton1	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getJButton1() {
		if (jButton1 == null) {
			jButton1 = new JButton();
			jButton1.setText("Browse");
			jButton1.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					selectDirectory();
				}
			});
		}
		return jButton1;
	}
	
	/**
	 * Selects a file, using the component JFileChooser.
	 */
	private void selectDirectory() {
		try{
			JFileChooser fc = new JFileChooser();
			fc.setFileSelectionMode(JFileChooser.FILES_AND_DIRECTORIES);
			int returnVal = fc.showDialog(this, "Select");
			if (returnVal == JFileChooser.APPROVE_OPTION) {
	                	File file = fc.getSelectedFile(); 
	           if (file.exists()){    	
					if (file.isDirectory())				
					{				
						File file2 = new File(file.getCanonicalPath()+ "\\DICOMDIR");
						if (!file2.exists()){
							System.err.println("Directory doesn't contain \"DICOMDIR\"");
							getJDialog();
							jLabel5.setText("  Folder doesnot contain DICOMDIR!  ");
							jDialog.setVisible(true);						
						}
						else{
							jTextField1.setText(file.getCanonicalPath()+ "\\DICOMDIR");
							isDir = true;
						}					
					} else {
						jTextField1.setText(file.getCanonicalPath());
						if (file.getName().equals("DICOMDIR"))
							isDir = true;
						else
							isDir = false;					
					}
				}
			}
		} catch (IOException ioe) {
			System.err.println("IOException when adding folder : "+ioe.getMessage());
			ioe.printStackTrace(System.err);
		}
	}

	/**
	 * This method initializes jPanel5	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel5() {
		if (jPanel5 == null) {
			GridLayout gridLayout8 = new GridLayout();
			gridLayout8.setRows(2);
			jLabel1 = new JLabel();
			jLabel1.setText("  Path to image or DICOMDIR");
			jLabel1.setVerticalAlignment(javax.swing.SwingConstants.BOTTOM);
			jPanel5 = new JPanel();
			jPanel5.setMinimumSize(new java.awt.Dimension(239,70));
			jPanel5.setPreferredSize(new java.awt.Dimension(365,70));
			jPanel5.setLayout(gridLayout8);
			jPanel5.add(jLabel1, null);
			jPanel5.add(getJPanel4(), null);
			
		}
		return jPanel5;
	}

	/**
	 * This method initializes jPanel	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel() {
		if (jPanel == null) {
			FlowLayout flowLayout2 = new FlowLayout();
			flowLayout2.setAlignment(java.awt.FlowLayout.RIGHT);
			jPanel = new JPanel();
			jPanel.setPreferredSize(new java.awt.Dimension(100,100));
			jPanel.setLayout(flowLayout2);
			jPanel.add(getJButton(), null);
			jPanel.add(getJButton2(), null);
			jPanel.add(getJButton6(), null);
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
			jButton.setText("Submit");
			jButton.setPreferredSize(new java.awt.Dimension(90,20));
			jButton.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
						submit();												
				}
			});
		}
		return jButton;
	}
	
	private void submit(){
		
		if (!jRadioButton.isSelected() && !jRadioButton1.isSelected()){
			getJDialog();
			jLabel5.setText("  \"Clinical\" button not selected  ");
			jDialog.setVisible(true);
			System.err.println("Clinical button not selected!");
		}
		else if(isDir && (jTextPane.getText()==null || jTextPane.getText().trim().equals(""))){
			getJDialog();
			jLabel5.setText("  Description is empty  ");
			jDialog.setVisible(true);
			System.err.println("Description is empty");
		} 
		else if (jTextField1.getText()==null || jTextField1.getText().trim().equals("")){
			getJDialog();
			jLabel5.setText("  Path not set  ");
			jDialog.setVisible(true);
			System.err.println("Path not set");
		} 
		else {
			//resetting progressbar and disabling buttons
			jProgressBar.setValue(0);
			jRadioButton.setEnabled(false);
			jRadioButton1.setEnabled(false);
			jTextPane.setEditable(false);
			jButton.setEnabled(false);
			jButton1.setEnabled(false);
			jProgressBar.setStringPainted(true);
			new Thread(){
				public void run(){					
										
					//Creating (clean) temporary directory
					String tempdir = ApplicationSetup.getProperty("tempdir", "tempresults");
					
					//Hardcoded for safety reasons
					File file = new File("tempresults"); 
					if (file.exists()){
						runApplication("emptydir.bat " + "tempresults");
					}
					file.mkdirs();
					jProgressBar.setValue(20);
					
					String destination = ApplicationSetup.getProperty("destination","");
					
					//Check whether we have to upload dataset or single file
					if (isDir){
						//Dataset

						//build up meta file
						jLabel2.setText("Building meta file");
						logger.info("Start Building meta file");
						String metaFile = "meta.xml";
						if(!buildMeta(metaFile)){
							logger.severe("An error occurred while building meta file");						
						}
						logger.info("Meta file created");
						jProgressBar.setValue(30);
						
						//Generate XML
						jLabel2.setText("Validating and creating XML...");
						logger.info("Start validating DICOMDIR");
						runApplication(ApplicationSetup.getProperty("dvt", "") + " -m Media.ses \"" +jTextField1.getText() + "\"");
						logger.info("Ended validating");
						jProgressBar.setValue(40);
						
						//Delete side effect files
						logger.info("Deleting pix and idx files");
						runApplication("delete.bat " + tempdir);
						jProgressBar.setValue(50);
										
						//Rename the files
						jLabel2.setText("Renaming XML files...");
						logger.info("Renaming xml files");
						XMLRenamer renamer = new XMLRenamer();
						File [] files = new File(tempdir).listFiles(new FilenameFilter(){
							public boolean accept(File file, String fileName){
								if (fileName.endsWith(".xml"))
									return true;
								return false;
							}
						});
						
						for (int i=0; i<files.length; i++){
							renamer.rename(files[i].getAbsolutePath());
							jProgressBar.setValue(50 + ((10*i)/files.length));
						}
						logger.info("Xml files renamed");
						jProgressBar.setValue(60);
						
						//Create directory structures
						logger.info("Creating directory structures");
						String dirName = "DATASET" + System.currentTimeMillis();
						
						if(!errors){
							if(!new File(destination+ "\\" + dirName ).mkdir())
								errors=true;
							if(!new File(destination+ "\\" + dirName + "\\representations" ).mkdir())
								errors=true;
							if(!new File(destination+ "\\" + dirName + "\\meta").mkdir() )
								errors=true;
							if(!new File(destination+ "\\" + dirName + "\\data").mkdir())
								errors=true;
							jProgressBar.setValue(70);						
						} else{
							logger.severe("There were errors, cannot create directories");
							logger.severe("Failed to create " + dirName + " and structures");
						}
						
						//If we don't have errors, we can copy
						if(!errors){
							jLabel2.setText("Copying data to destination (may take a while)");
							logger.info("Start copying to destination " + destination);
							logger.info("Copying original data");
							int slashIndex = jTextField1.getText().lastIndexOf('\\');
							String source = jTextField1.getText().substring(0,slashIndex);
							runApplication("copyfiles.bat \"" + source + "\" " + destination + "\\" + dirName + "\\data");
							jProgressBar.setValue(95);
							logger.info("Copying xml representations");
							runApplication("copyfiles.bat " + tempdir + " " + destination + "\\" + dirName + "\\representations");
							logger.info("Copying xml meta file");
							runApplication("copyfile.bat " + metaFile + " " + destination + "\\" + dirName + "\\meta");
						} else
							logger.severe("There were errors, cannot copy");
						
						jProgressBar.setValue(100);
						
					}else {
						//Single Image
						
						//build up meta file
						jLabel2.setText("Building meta file");
						logger.info("Start Building meta file");
						String imageName = new File(jTextField1.getText()).getName();
						String metaFile = imageName +"_meta.xml";
						if(!buildMeta(metaFile)){
							logger.severe("An error occurred while building meta file");						
						}
						logger.info("Meta file created");
						jProgressBar.setValue(30);
						
						//Generate XML
						jLabel2.setText("Validating and creating XML...");
						logger.info("Start validating file");
						runApplication(ApplicationSetup.getProperty("dvt", "") + " -m Media.ses \"" +jTextField1.getText()+ "\"");
						logger.info("Ended validating");
						jProgressBar.setValue(40);
						
						//Delete side effect files
						logger.info("Deleting pix and idx files");
						runApplicationWeek("delete.bat " + tempdir);
						jProgressBar.setValue(50);

						//If we don't have errors, we can copy
						if(!errors){
							jLabel2.setText("Copying data to destination ");
							logger.info("Start copying to destination " + destination);
							logger.info("Copying original data");
							runApplication("copyfile.bat \"" + jTextField1.getText() + "\" " + destination + "\\SINGLE_OBJECTS\\data");
							jProgressBar.setValue(95);
							logger.info("Copying xml representations");
							runApplication("copyfiles.bat " + tempdir + " " + destination + "\\SINGLE_OBJECTS\\representations");
							if (new File(metaFile).exists()){
								logger.info("Copying xml meta file");
								runApplication("copyfile.bat \"" + metaFile + "\" " + destination + "\\SINGLE_OBJECTS\\meta");
								new File(metaFile).delete();
							}
						}
						jProgressBar.setValue(100);
					}
								
					if (!errors)
						jLabel2.setText("Submit finished successfully");		
					else 
						jLabel2.setText("Submit finished with errors, see log file ");
					
					jButton2.setEnabled(true);
					jButton6.setVisible(true);
				}
			}.start();
		}
						
}
		
	
	private boolean buildMeta(String fileName){
		File file = new File(fileName);
		try{
			
			FileWriter fw = new FileWriter(file);
			fw.write("<?xml version=\"1.0\"?>\n<meta-info>");
			
			if(jTextPane.getText() != null && !jTextPane.getText().equals("")){
				fw.write("<description>");
				fw.write(jTextPane.getText());
				fw.write("</description>");
			}
			
			fw.write("<clinical>");
			fw.write(jRadioButton.isSelected() ? "yes" : "no");
			fw.write("</clinical>");
			
			fw.write("</meta-info>");
			fw.close();
			return true;
		} catch (IOException e){
			logger.severe(e.getMessage());
			errors = true;
			return false;
		}
	}
	
	private void runApplication(String app){
		try{
			Runtime rt = Runtime.getRuntime();
			Process p = rt.exec(app);
			
			InputStream stderr = p.getErrorStream();
			InputStreamReader isr = new InputStreamReader(stderr);
			BufferedReader br = new BufferedReader(isr);
			
			InputStream stdin = p.getInputStream();
			InputStreamReader isr2 = new InputStreamReader(stdin);
			BufferedReader br2 = new BufferedReader(isr2);
			
			String line = null;
			while ( (line = br2.readLine()) != null)
				if (!line.trim().equals(""))
					logger.info(line);
			
			line = null;
			while ( (line = br.readLine()) != null){
				if (!line.trim().equals(""))
					errors = true;
				logger.severe(line);
			}
			
			int exitValue = p.waitFor();
			if (exitValue==0){
				logger.info(app + "  ended with exit code " + exitValue);
				
			} else {
				logger.severe(app + " ended with exit code " + exitValue);
				errors = true;
				
			}
		} catch (IOException e){
			errors = true;
			logger.severe(e.getMessage());
			
		} catch (InterruptedException e){
			errors = true;
			logger.severe(e.getMessage());
			
		} 	
	}
	
	private boolean runApplicationWeek(String app){
		try{
			Runtime rt = Runtime.getRuntime();
			Process p = rt.exec(app);
			
			InputStream stderr = p.getErrorStream();
	        InputStreamReader isr = new InputStreamReader(stderr);
	        BufferedReader br = new BufferedReader(isr);
	        
	        InputStream stdin = p.getInputStream();
	        InputStreamReader isr2 = new InputStreamReader(stdin);
	        BufferedReader br2 = new BufferedReader(isr2);
	        
	        String line = null;
	        while ( (line = br2.readLine()) != null)
	        	if (!line.trim().equals(""))
	        		logger.info(line);
	        
	        line = null;
	        while ( (line = br.readLine()) != null){
	        	if (!line.trim().equals(""))
	        		logger.warning(line);
	        }
	       
			int exitValue = p.waitFor();
			if (exitValue==0){
				logger.info(app + "  ended with exit code " + exitValue);
				return true;
			} else {
				logger.warning(app + " ended with exit code " + exitValue);
				return false;
			}
		} catch (IOException e){
			errors = true;
			logger.severe(e.getMessage());
			return false;
		} catch (InterruptedException e){
			errors = true;
			logger.severe(e.getMessage());
			return false;
		} 
		
	}
	
	/**
	 * This method initializes jButton2	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getJButton2() {
		if (jButton2 == null) {
			jButton2 = new JButton();
			jButton2.setText("View Log");
			jButton2.setEnabled(false);
			jButton2.setPreferredSize(new java.awt.Dimension(90,20));
			jButton2.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					new Thread(){	
						public void run(){
							runApplication("notepad upload.log");
						}
					}.start();
				}
			});
			
		}
		return jButton2;
	}

	/**
	 * This method initializes jPanel3	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel3() {
		if (jPanel3 == null) {
			jLabel2 = new JLabel();
			jLabel2.setText("Ready");
			jLabel2.setBackground(java.awt.SystemColor.activeCaptionBorder);
			FlowLayout flowLayout3 = new FlowLayout();
			flowLayout3.setAlignment(java.awt.FlowLayout.LEFT);
			jPanel3 = new JPanel();
			jPanel3.setLayout(flowLayout3);
			jPanel3.add(getJProgressBar(), null);
			jPanel3.add(jLabel2, null);
		}
		return jPanel3;
	}

	/**
	 * This method initializes jProgressBar	
	 * 	
	 * @return javax.swing.JProgressBar	
	 */
	private JProgressBar getJProgressBar() {
		if (jProgressBar == null) {
			jProgressBar = new JProgressBar(new DefaultBoundedRangeModel());			
		}
		return jProgressBar;
	}

	/**
	 * This method initializes jPanel6	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel6() {
		if (jPanel6 == null) {
			jPanel6 = new JPanel();
			jPanel6.setPreferredSize(new java.awt.Dimension(350,90));
			jPanel6.setLayout(new BorderLayout());
			jPanel6.add(getJPanel(), java.awt.BorderLayout.EAST);
			jPanel6.add(getJPanel5(), java.awt.BorderLayout.WEST);
		}
		return jPanel6;
	}

	/**
	 * This method initializes jRadioButton	
	 * 	
	 * @return javax.swing.JRadioButton	
	 */
	private JRadioButton getJRadioButton() {
		if (jRadioButton == null) {
			jRadioButton = new JRadioButton();			
		}
		return jRadioButton;
	}

	/**
	 * This method initializes jPanel8	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel8() {
		if (jPanel8 == null) {
			jLabel4 = new JLabel();
			jLabel4.setText("Non-Clinical");
			FlowLayout flowLayout1 = new FlowLayout();
			flowLayout1.setAlignment(java.awt.FlowLayout.LEFT);
			jLabel3 = new JLabel();
			jLabel3.setText("Clinical        ");
			jPanel8 = new JPanel();
			jPanel8.setPreferredSize(new java.awt.Dimension(100,34));
			jPanel8.setLayout(flowLayout1);
			buttonGroup = new ButtonGroup();
			jPanel8.add(getJRadioButton(), null);
			buttonGroup.add(getJRadioButton());
			jPanel8.add(jLabel3, null);
			jPanel8.add(getJRadioButton1(), null);
			buttonGroup.add(getJRadioButton1());
			jPanel8.add(jLabel4, null);
		}
		return jPanel8;
	}

	/**
	 * This method initializes jRadioButton1	
	 * 	
	 * @return javax.swing.JRadioButton	
	 */
	private JRadioButton getJRadioButton1() {
		if (jRadioButton1 == null) {
			jRadioButton1 = new JRadioButton();
		}
		return jRadioButton1;
	}

	/**
	 * This method initializes jDialog	
	 * 	
	 * @return javax.swing.JDialog	
	 */
	private JDialog getJDialog() {
		if (jDialog == null) {
			jDialog = new JDialog();
			jDialog.setTitle("Error!");
			jDialog.setSize(new java.awt.Dimension(236,110));
			jDialog.setPreferredSize(new java.awt.Dimension(65,80));
			jDialog.setLocationRelativeTo(this);
			jDialog.setContentPane(getJContentPane1());
		}
		jDialog.setVisible(true);
		return jDialog;
	}

	/**
	 * This method initializes jContentPane1	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJContentPane1() {
		if (jContentPane1 == null) {
			jLabel5 = new JLabel();
			jLabel5.setForeground(Color.WHITE);
			jLabel5.setHorizontalAlignment(javax.swing.SwingConstants.CENTER);
			jLabel5.setFont(new Font("Dialog", Font.BOLD, 12));
			jContentPane1 = new JPanel();
			jContentPane1.setBackground(Color.GRAY);
			jContentPane1.setLayout(new BorderLayout());
			jContentPane1.add(jLabel5, java.awt.BorderLayout.CENTER);
			jContentPane1.add(getJPanel11(), java.awt.BorderLayout.SOUTH);
		}
		return jContentPane1;
	}

	/**
	 * This method initializes jPanel1	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel1() {
		if (jPanel1 == null) {
			jPanel1 = new JPanel();
			jPanel1.add(getJButton3(), null);
			jPanel1.add(getJButton4(), null);
		}
		return jPanel1;
	}

	/**
	 * This method initializes jButton3	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getJButton3() {
		if (jButton3 == null) {
			jButton3 = new JButton();
			jButton3.setText("Ok");
			jButton3.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {
					ApplicationSetup.setProperty("dvt", jTextField.getText());
					ApplicationSetup.setProperty("destination", jTextField2.getText());
					jDialog2.setVisible(false);
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
			jButton4.setText("Cancel");
			jButton4.addActionListener(new java.awt.event.ActionListener() { 
				public void actionPerformed(java.awt.event.ActionEvent e) {    
					jDialog2.setVisible(false);
				}
			});
		}
		return jButton4;
	}

	/**
	 * This method initializes jPanel7	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel7() {
		if (jPanel7 == null) {
			jLabel6 = new JLabel();
			jLabel6.setText("Path to DVTCmd");
			jLabel6.setPreferredSize(new java.awt.Dimension(95,14));
			jPanel7 = new JPanel();
			jPanel7.setPreferredSize(new java.awt.Dimension(355,30));
			jPanel7.add(jLabel6, null);
			jPanel7.add(getJTextField(), null);
		}
		return jPanel7;
	}

	/**
	 * This method initializes jTextField	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getJTextField() {
		if (jTextField == null) {
			jTextField = new JTextField();
			jTextField.setPreferredSize(new java.awt.Dimension(250,20));
		}
		return jTextField;
	}

	/**
	 * This method initializes jFrame	
	 * 	
	 * @return javax.swing.JFrame	
	 */
	private JDialog getJDialog2() {
		if (jDialog2 == null) {
			jDialog2 = new JDialog();
			jDialog2.setSize(new java.awt.Dimension(391,161));
			jDialog2.setLocationRelativeTo(this);
			jDialog2.setTitle("Properties");
			jDialog2.setContentPane(getJContentPane2());			
		}
		return jDialog2;
	}

	/**
	 * This method initializes jContentPane2	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJContentPane2() {
		if (jContentPane2 == null) {
			jContentPane2 = new JPanel();
			jContentPane2.setLayout(new BorderLayout());
			jContentPane2.add(getJPanel1(), java.awt.BorderLayout.SOUTH);
			jContentPane2.add(getJPanel10(), java.awt.BorderLayout.NORTH);
		}
		return jContentPane2;
	}

	/**
	 * This method initializes jPanel9	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel9() {
		if (jPanel9 == null) {
			jLabel7 = new JLabel();
			jLabel7.setText("Destination Dir");
			jLabel7.setPreferredSize(new java.awt.Dimension(95,14));
			jPanel9 = new JPanel();
			jPanel9.setPreferredSize(new java.awt.Dimension(355,30));
			jPanel9.add(jLabel7, null);
			jPanel9.add(getJTextField2(), null);
		}
		return jPanel9;
	}

	/**
	 * This method initializes jTextField2	
	 * 	
	 * @return javax.swing.JTextField	
	 */
	private JTextField getJTextField2() {
		if (jTextField2 == null) {
			jTextField2 = new JTextField();
			jTextField2.setPreferredSize(new java.awt.Dimension(250,20));
		}
		return jTextField2;
	}

	/**
	 * This method initializes jPanel10	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel10() {
		if (jPanel10 == null) {
			FlowLayout flowLayout4 = new FlowLayout();
			flowLayout4.setAlignment(java.awt.FlowLayout.LEFT);
			jPanel10 = new JPanel();
			jPanel10.setLayout(flowLayout4);
			jPanel10.setPreferredSize(new java.awt.Dimension(710,80));
			jPanel10.add(getJPanel7(), null);
			jPanel10.add(getJPanel9(), null);
		}
		return jPanel10;
	}

	/**
	 * This method initializes jPanel11	
	 * 	
	 * @return javax.swing.JPanel	
	 */
	private JPanel getJPanel11() {
		if (jPanel11 == null) {
			jPanel11 = new JPanel();
			jPanel11.add(getJButton5(), null);
		}
		return jPanel11;
	}

	/**
	 * This method initializes jButton5	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getJButton5() {
		if (jButton5 == null) {
			jButton5 = new JButton();
			jButton5.setText("Ok");
			jButton5.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					if (jDialog!=null)
						jDialog.setVisible(false);
					if (aboutDialog!=null)
						aboutDialog.setVisible(false);
				}
			});
		}
		return jButton5;
	}

	/**
	 * This method initializes jButton6	
	 * 	
	 * @return javax.swing.JButton	
	 */
	private JButton getJButton6() {
		if (jButton6 == null) {
			jButton6 = new JButton();
			jButton6.setText("Clear");
			jButton6.setPreferredSize(new java.awt.Dimension(90,20));
			jButton6.setVisible(false);
			jButton6.addActionListener(new java.awt.event.ActionListener() {
				public void actionPerformed(java.awt.event.ActionEvent e) {
					clear(); // TODO Auto-generated Event stub actionPerformed()
				}
			});
		}
		return jButton6;
	}

	public void clear(){
		jRadioButton.setEnabled(true);
		jRadioButton1.setEnabled(true);
		
		buttonGroup.remove(jRadioButton);
		jRadioButton.setSelected(false);
		buttonGroup.add(jRadioButton);
		
		buttonGroup.remove(jRadioButton1);
		jRadioButton1.setSelected(false);		
		buttonGroup.add(jRadioButton1);
		
		jTextPane.setText("");
		jTextField1.setText("");
		jProgressBar.setValue(0);
		jLabel2.setText("Ready");
		
		jButton.setEnabled(true);
		jButton1.setEnabled(true);
		jTextPane.setEditable(true);
		jButton2.setEnabled(false);
		jButton6.setVisible(false);		
	}
	/**
	 * @param args
	 */
	public static void main(String[] args) {
		// TODO Auto-generated method stub
		Storage application = new Storage();
		application.setVisible(true);
	}

	/**
	 * This is the default constructor
	 */
	public Storage() {
		super();
		initialize();
	}

	/**
	 * This method initializes this
	 * 
	 * @return void
	 */
	private void initialize() {
		this.setDefaultCloseOperation(JFrame.DO_NOTHING_ON_CLOSE);
		
		WindowListener windowListener = new WindowAdapter()
		   {
		   // anonymous WindowAdapter class
		   public void windowClosing ( WindowEvent w )
		      {	      
			   exit();
		      } // end windowClosing
		   };// end anonymous class
		this.addWindowListener( windowListener );
		
		this.setJMenuBar(getJJMenuBar());
		this.setSize(500, 310);
		this.setResizable(false);
		this.setContentPane(getJContentPane());
		this.setTitle("Application");
		this.setLocation(100,100);
	}
	
	private void exit(){
		ApplicationSetup.saveProperties();
		System.exit(0);
	}

	/**
	 * This method initializes jContentPane
	 * 
	 * @return javax.swing.JPanel
	 */
	private JPanel getJContentPane() {
		if (jContentPane == null) {
			jContentPane = new JPanel();
			jContentPane.setLayout(new BorderLayout());
			jContentPane.setComponentOrientation(java.awt.ComponentOrientation.UNKNOWN);
			//jContentPane.add(getJPanel1(), java.awt.BorderLayout.NORTH);
			jContentPane.add(getJPanel2(), java.awt.BorderLayout.WEST);
			jContentPane.add(getJPanel6(), java.awt.BorderLayout.NORTH);
			jContentPane.add(getJPanel3(), java.awt.BorderLayout.SOUTH);
			jContentPane.add(getJPanel8(), java.awt.BorderLayout.EAST);
		}
		return jContentPane;
	}

	/**
	 * This method initializes jJMenuBar	
	 * 	
	 * @return javax.swing.JMenuBar	
	 */
	private JMenuBar getJJMenuBar() {
		if (jJMenuBar == null) {
			jJMenuBar = new JMenuBar();
			jJMenuBar.add(getFileMenu());
			jJMenuBar.add(getEditMenu());
			jJMenuBar.add(getHelpMenu());
		}
		return jJMenuBar;
	}

	/**
	 * This method initializes jMenu	
	 * 	
	 * @return javax.swing.JMenu	
	 */
	private JMenu getFileMenu() {
		if (fileMenu == null) {
			fileMenu = new JMenu();
			fileMenu.setText("File");
			//fileMenu.add(getSaveMenuItem());
			fileMenu.add(getExitMenuItem());
		}
		return fileMenu;
	}

	/**
	 * This method initializes jMenu	
	 * 	
	 * @return javax.swing.JMenu	
	 */
	private JMenu getEditMenu() {
		if (editMenu == null) {
			editMenu = new JMenu();
			editMenu.setText("Edit");
			editMenu.add(getPropertiesMenuItem());
			//editMenu.add(getCutMenuItem());
			//editMenu.add(getCopyMenuItem());
			//editMenu.add(getPasteMenuItem());
		}
		return editMenu;
	}

	/**
	 * This method initializes jMenu	
	 * 	
	 * @return javax.swing.JMenu	
	 */
	private JMenu getHelpMenu() {
		if (helpMenu == null) {
			helpMenu = new JMenu();
			helpMenu.setText("Help");
			helpMenu.add(getAboutMenuItem());
		}
		return helpMenu;
	}

	/**
	 * This method initializes jMenuItem	
	 * 	
	 * @return javax.swing.JMenuItem	
	 */
	private JMenuItem getExitMenuItem() {
		if (exitMenuItem == null) {
			exitMenuItem = new JMenuItem();
			exitMenuItem.setText("Exit");
			exitMenuItem.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					System.exit(0);
				}
			});
		}
		return exitMenuItem;
	}

	/**
	 * This method initializes jMenuItem	
	 * 	
	 * @return javax.swing.JMenuItem	
	 */
	private JMenuItem getAboutMenuItem() {
		if (aboutMenuItem == null) {
			aboutMenuItem = new JMenuItem();
			aboutMenuItem.setText("About");
			aboutMenuItem.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					showAboutDialog();
				}
			});
		}
		return aboutMenuItem;
	}
	
	private void showAboutDialog(){
		aboutDialog = new JDialog(Storage.this, "About", true);
		aboutDialog.setContentPane(getJContentPane1());
		aboutDialog.setSize(new java.awt.Dimension(236,110));
		aboutDialog.setLocationRelativeTo(this);
		jLabel5.setText("   DICOM upload tool  ");
		aboutDialog.setVisible(true);
	}
	
	/**
	 * This method initializes jMenuItem	
	 * 	
	 * @return javax.swing.JMenuItem	
	 */
	private JMenuItem getPropertiesMenuItem() {
		if (propertiesMenuItem == null) {
			propertiesMenuItem = new JMenuItem();
			propertiesMenuItem.setText("Properties");
			propertiesMenuItem.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					showProperties();
				}
			});
		}
		return propertiesMenuItem;
	}
	
	private void showProperties(){
		JDialog prop = getJDialog2();
		jTextField.setText(ApplicationSetup.getProperty("dvt", "C:\\Program Files\\DVT\\DvtCmd.exe"));
		jTextField2.setText(ApplicationSetup.getProperty("destination", "\\\\galaxy\\IMGDB"));
		prop.setVisible(true);
	}

}  //  @jve:decl-index=0:visual-constraint="354,-5"
