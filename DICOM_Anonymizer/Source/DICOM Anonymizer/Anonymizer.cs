// ------------------------------------------------------
// DVTk - The Healthcare Validation Toolkit (www.dvtk.org)
// Copyright © 2009 DVTk
// ------------------------------------------------------
// This file is part of DVTk.
//
// DVTk is free software; you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License as published by the Free Software Foundation; either version 3.0
// of the License, or (at your option) any later version. 
// 
// DVTk is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even
// the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser
// General Public License for more details. 
// 
// You should have received a copy of the GNU Lesser General Public License along with this
// library; if not, see <http://www.gnu.org/licenses/>

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Xml;
using System.Threading;
using System.Text;

using DvtkData.Dimse;
using DvtkHighLevelInterface.Common.Threads;
using DvtkHighLevelInterface.Common.Compare;
//using DvtkHighLevelInterface.Common.Other;
using DvtkHighLevelInterface.Dicom.Files;
using DvtkHighLevelInterface.Dicom.Threads;
using AnonymizationUtility ;
using DvtkApplicationLayer;
using DvtkApplicationLayer.UserInterfaces;

namespace Anonymizer
{	
	using HLI = DvtkHighLevelInterface.Dicom.Other;
	using sequenceTag = DvtkData.Dimse.Tag;

	/// <summary>
	/// Summary description for DICOMAnonymize.
	/// </summary>
	public class DICOMAnonymize : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem File;
		private System.Windows.Forms.MenuItem Help;
		private System.Windows.Forms.MenuItem AnonymizeFile;
		private System.Windows.Forms.MenuItem Exit;
		private System.Windows.Forms.MenuItem AboutUs;
		private DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew dvtkWebBrowser;
		private System.Windows.Forms.Panel panelBrowser;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.ToolBar toolBarAnonymizer;
		private System.Windows.Forms.ToolBarButton toolBarButton1;
		private System.Windows.Forms.ToolBarButton toolBarButtonBack;
		private System.Windows.Forms.ToolBarButton toolBarButtonForward;
		private System.Windows.Forms.ImageList imageListAnonymizer;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Utility utility = null;
		public  HLI.DataSet dcmDataset = new HLI.DataSet();
		private MainThread mainThread = new MainThread();
		private ThreadManager threadMgr = new ThreadManager();
		private ArrayList allDCMFilesTemp = null;
		private string resultFileName = "";
		AttributeCollections datasets = null;
		string inputDirectory = "";
		string outputDirectory = "";
		private int counter = 0;
		private string  initialDirectory = "";
		DirectoryInfo dir = null;
		private System.Windows.Forms.ToolBarButton toolBarButtonRun;
		bool anonymizationMode = true;
		private System.Windows.Forms.MenuItem AnonymizeFileBasic;
		private System.Windows.Forms.MenuItem AnonymizeFileComplete;
		private System.Windows.Forms.MenuItem AnonymizeDirComplete;
		private System.Windows.Forms.MenuItem AnonymizeDir;
		private System.Windows.Forms.MenuItem AnonymizeDirBasic;
        private MenuItem CreateDICOMDIR;
		SelectMode mode = new SelectMode();

		public DICOMAnonymize()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            dvtkWebBrowser.BackwardFormwardEnabledStateChangeEvent += new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew.BackwardFormwardEnabledStateChangeEventHandler(dvtkWebBrowser_BackwardFormwardEnabledStateChangeEvent);

			mainThread.Initialize(threadMgr);

			// Load the Definition Files
            string definitionDir = Environment.GetEnvironmentVariable("COMMONPROGRAMFILES") + @"\DVTk\Definition Files\DICOM\";
            DirectoryInfo theDefDirectoryInfo = new DirectoryInfo(definitionDir);
			if(theDefDirectoryInfo.Exists)
			{
				FileInfo[] theDefFilesInfo = theDefDirectoryInfo.GetFiles();
                bool ok = true;
				foreach (FileInfo defFile in theDefFilesInfo)
				{
                    if (ok)
                    {
					    ok = mainThread.Options.LoadDefinitionFile(defFile.FullName);
					    if(!ok)
					    {
						    string theWarningText = "The Definition files could not be loaded.";
						    MessageBox.Show(theWarningText, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					    }
                    }
				}
			}

			// Set the Results & Data directory
			initialDirectory = Application.StartupPath + @"\results";
			dir = new DirectoryInfo(Application.StartupPath + @"\results");
			if (!dir.Exists)
			{
				dir.Create();
			}
			mainThread.Options.StorageMode = Dvtk.Sessions.StorageMode.NoStorage;
			mainThread.Options.ResultsDirectory = initialDirectory;
			mainThread.Options.DataDirectory = initialDirectory;
		    utility = new Utility();
            CreateDICOMDIR.Enabled = false;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DICOMAnonymize));
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.File = new System.Windows.Forms.MenuItem();
            this.AnonymizeFile = new System.Windows.Forms.MenuItem();
            this.AnonymizeFileBasic = new System.Windows.Forms.MenuItem();
            this.AnonymizeFileComplete = new System.Windows.Forms.MenuItem();
            this.AnonymizeDir = new System.Windows.Forms.MenuItem();
            this.AnonymizeDirBasic = new System.Windows.Forms.MenuItem();
            this.AnonymizeDirComplete = new System.Windows.Forms.MenuItem();
            this.CreateDICOMDIR = new System.Windows.Forms.MenuItem();
            this.Exit = new System.Windows.Forms.MenuItem();
            this.Help = new System.Windows.Forms.MenuItem();
            this.AboutUs = new System.Windows.Forms.MenuItem();
            this.panelBrowser = new System.Windows.Forms.Panel();
            this.dvtkWebBrowser = new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew();
            this.toolBarAnonymizer = new System.Windows.Forms.ToolBar();
            this.toolBarButtonRun = new System.Windows.Forms.ToolBarButton();
            this.toolBarButton1 = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonBack = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonForward = new System.Windows.Forms.ToolBarButton();
            this.imageListAnonymizer = new System.Windows.Forms.ImageList(this.components);
            this.panelBrowser.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.File,
            this.Help});
            // 
            // File
            // 
            this.File.Index = 0;
            this.File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AnonymizeFile,
            this.AnonymizeDir,
            this.CreateDICOMDIR,
            this.Exit});
            this.File.Text = "File";
            // 
            // AnonymizeFile
            // 
            this.AnonymizeFile.Index = 0;
            this.AnonymizeFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AnonymizeFileBasic,
            this.AnonymizeFileComplete});
            this.AnonymizeFile.Text = "Anonymize DICOM File";
            // 
            // AnonymizeFileBasic
            // 
            this.AnonymizeFileBasic.Index = 0;
            this.AnonymizeFileBasic.Text = "Basic";
            this.AnonymizeFileBasic.Click += new System.EventHandler(this.AnonymizeFileBasic_Click);
            // 
            // AnonymizeFileComplete
            // 
            this.AnonymizeFileComplete.Index = 1;
            this.AnonymizeFileComplete.Text = "Complete";
            this.AnonymizeFileComplete.Click += new System.EventHandler(this.AnonymizeFileComplete_Click);
            // 
            // AnonymizeDir
            // 
            this.AnonymizeDir.Index = 1;
            this.AnonymizeDir.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AnonymizeDirBasic,
            this.AnonymizeDirComplete});
            this.AnonymizeDir.Text = "Anonymize the DICOM Directory";
            // 
            // AnonymizeDirBasic
            // 
            this.AnonymizeDirBasic.Index = 0;
            this.AnonymizeDirBasic.Text = "Basic";
            this.AnonymizeDirBasic.Click += new System.EventHandler(this.AnonymizeDirBasic_Click);
            // 
            // AnonymizeDirComplete
            // 
            this.AnonymizeDirComplete.Index = 1;
            this.AnonymizeDirComplete.Text = "Complete";
            this.AnonymizeDirComplete.Click += new System.EventHandler(this.AnonymizeDirComplete_Click);
            // 
            // CreateDICOMDIR
            // 
            this.CreateDICOMDIR.Index = 2;
            this.CreateDICOMDIR.Text = "Create DICOMDIR";
            this.CreateDICOMDIR.Click += new System.EventHandler(this.CreateDICOMDIR_Click);
            // 
            // Exit
            // 
            this.Exit.Index = 3;
            this.Exit.Text = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Help
            // 
            this.Help.Index = 1;
            this.Help.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.AboutUs});
            this.Help.Text = "About";
            // 
            // AboutUs
            // 
            this.AboutUs.Index = 0;
            this.AboutUs.Text = "About DICOM Anonymizer";
            this.AboutUs.Click += new System.EventHandler(this.AboutUs_Click);
            // 
            // panelBrowser
            // 
            this.panelBrowser.BackColor = System.Drawing.SystemColors.Control;
            this.panelBrowser.Controls.Add(this.dvtkWebBrowser);
            this.panelBrowser.Controls.Add(this.toolBarAnonymizer);
            this.panelBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBrowser.Location = new System.Drawing.Point(0, 0);
            this.panelBrowser.Name = "panelBrowser";
            this.panelBrowser.Size = new System.Drawing.Size(686, 437);
            this.panelBrowser.TabIndex = 2;
            // 
            // dvtkWebBrowser
            // 
            this.dvtkWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvtkWebBrowser.Location = new System.Drawing.Point(0, 28);
            this.dvtkWebBrowser.Name = "dvtkWebBrowser";
            this.dvtkWebBrowser.Size = new System.Drawing.Size(686, 409);
            this.dvtkWebBrowser.TabIndex = 0;
            this.dvtkWebBrowser.XmlStyleSheetFullFileName = "";
            // 
            // toolBarAnonymizer
            // 
            this.toolBarAnonymizer.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarAnonymizer.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonRun,
            this.toolBarButton1,
            this.toolBarButtonBack,
            this.toolBarButtonForward});
            this.toolBarAnonymizer.ButtonSize = new System.Drawing.Size(39, 24);
            this.toolBarAnonymizer.DropDownArrows = true;
            this.toolBarAnonymizer.ImageList = this.imageListAnonymizer;
            this.toolBarAnonymizer.Location = new System.Drawing.Point(0, 0);
            this.toolBarAnonymizer.Name = "toolBarAnonymizer";
            this.toolBarAnonymizer.ShowToolTips = true;
            this.toolBarAnonymizer.Size = new System.Drawing.Size(686, 28);
            this.toolBarAnonymizer.TabIndex = 1;
            this.toolBarAnonymizer.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarAnonymizer_ButtonClick);
            // 
            // toolBarButtonRun
            // 
            this.toolBarButtonRun.ImageIndex = 2;
            this.toolBarButtonRun.Name = "toolBarButtonRun";
            this.toolBarButtonRun.ToolTipText = "Start Anonymization";
            // 
            // toolBarButton1
            // 
            this.toolBarButton1.Name = "toolBarButton1";
            this.toolBarButton1.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonBack
            // 
            this.toolBarButtonBack.Enabled = false;
            this.toolBarButtonBack.ImageIndex = 0;
            this.toolBarButtonBack.Name = "toolBarButtonBack";
            // 
            // toolBarButtonForward
            // 
            this.toolBarButtonForward.Enabled = false;
            this.toolBarButtonForward.ImageIndex = 1;
            this.toolBarButtonForward.Name = "toolBarButtonForward";
            // 
            // imageListAnonymizer
            // 
            this.imageListAnonymizer.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListAnonymizer.ImageStream")));
            this.imageListAnonymizer.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListAnonymizer.Images.SetKeyName(0, "");
            this.imageListAnonymizer.Images.SetKeyName(1, "");
            this.imageListAnonymizer.Images.SetKeyName(2, "");
            // 
            // DICOMAnonymize
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(686, 437);
            this.Controls.Add(this.panelBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "DICOMAnonymize";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DCM Anonymizer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.DICOMAnonymize_Closing);
            this.panelBrowser.ResumeLayout(false);
            this.panelBrowser.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
#if !DEBUG
			try
			{
#endif

            // Checks the version of both the application and the DVTk library.
            // If one or both are a non-official or Alpha version, a warning message box is displayed.
            DvtkApplicationLayer.VersionChecker.CheckVersion();
            DvtkApplicationLayer.DefinitionFilesChecker.CheckVersion("1.0.0", "2.0.0");

			Application.Run(new DICOMAnonymize());

#if !DEBUG
			}
			catch(Exception exception)
			{
				CustomExceptionHandler.ShowThreadExceptionDialog(exception);
			}
#endif
        }

		private void AnonymizeDCMFile()
		{
			try
			{
				utility.AnonymizationType = anonymizationMode;
                CreateDICOMDIR.Enabled = true;

				OpenFileDialog fileDialog = new OpenFileDialog();
				fileDialog.Multiselect = false;
				fileDialog.ReadOnlyChecked = true;
				fileDialog.Title = "Select DCM File";
				fileDialog.Filter = "DCM files (*.dcm) |*.dcm|All files (*.*)|*.*";
				
				// Show the file dialog.
				// If the user pressed the OK button...
				if (fileDialog.ShowDialog() == DialogResult.OK) 
				{
					DicomFile dcmFile = new DicomFile();
					dcmFile.Read(fileDialog.FileName, mainThread);

					// Get the Data set from the selected DCM file
					HLI.DataSet srcDataset = dcmFile.DataSet;

					SaveFileDialog saveDialog = new SaveFileDialog();
					FileInfo saveFileInfo = null;
					saveDialog.Filter = "DCM files (*.dcm) |*.dcm|All files (*.*)|*.*";
					if (saveDialog.ShowDialog() == DialogResult.OK)
					{
						HLI.DataSet annonymizedDataset = srcDataset.Clone();

						utility.PatientAttributes(annonymizedDataset);
						for ( int number = 0; number < annonymizedDataset.Count; number++)
						{
							HLI.Attribute attribute =  annonymizedDataset[number];
							if (attribute.VR == VR.SQ)
							{
								utility.SequenceAttribute_recursive(attribute);
							}
							else 
							{
								utility.CacheAndRepairIdentifyingAttribute(attribute);
								utility.UpdateAnonymizedAttributes(attribute);
							}
						}

						datasets = new AttributeCollections();
						datasets.Add(srcDataset);
						datasets.Add(annonymizedDataset);

						//Save annonymized data to selected file
						saveFileInfo = new FileInfo(saveDialog.FileName);
						dcmFile.DataSet = annonymizedDataset;
						dcmFile.Write(saveFileInfo.FullName);
			
						//Cleanup temp file
						utility.cleanup();						
					}
					else
					{
						return;
					}

					string htmlFileName = initialDirectory + "\\" + saveFileInfo.Name + ".html";
					XmlTextWriter writer = new XmlTextWriter(htmlFileName, System.Text.Encoding.UTF8);
					writer.WriteStartElement("b");
					writer.WriteRaw(@"<b><font size='3' color='#ff0000'>DCM File Anonymization Started...</font></b>");
					writer.WriteEndElement();
					writer.Close();
					resultFileName = htmlFileName;
					System.Threading.Thread t = new System.Threading.Thread(new ThreadStart(CreateTable));
					dvtkWebBrowser.Navigate(htmlFileName);
					t.Start();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void CreateTable()
		{
			StaticDicomCompare compare = new StaticDicomCompare();
			FlagsDicomAttribute flags = FlagsDicomAttribute.Compare_values | FlagsDicomAttribute.Compare_present | FlagsDicomAttribute.Include_sequence_items;			
			StringCollection descriptions = new StringCollection();
			descriptions.Add("Src Dataset");
			descriptions.Add("Annonymized Dataset");
			CompareResults compareResults = compare.CompareAttributeSets("DataSet compare results", datasets, descriptions, flags);				
			string tableString = compareResults.Table.ConvertToHtml();
			StreamWriter writer = new StreamWriter(resultFileName);
			
			if(utility.AnonymizationType)
				writer.Write("<b><font size='3' color='#0000ff'>Anonymized DCM File (Basic)</font></b>");
			else
				writer.Write("<b><font size='3' color='#0000ff'>Anonymized DCM File (Complete)</font></b>");

			writer.Write(tableString);
			writer.Write("<br/>");
			writer.Close();

			dvtkWebBrowser.Navigate(resultFileName);
		}

		private void AnonymizeFileBasic_Click(object sender, System.EventArgs e)
		{
			anonymizationMode = true;
			AnonymizeDCMFile();
		}

		private void AnonymizeFileComplete_Click(object sender, System.EventArgs e)
		{
			anonymizationMode = false;
			AnonymizeDCMFile();
		}

		private void AnonynimizeDirectory()
		{
			utility.AnonymizationType = anonymizationMode;
            CreateDICOMDIR.Enabled = true;

			FolderBrowserDialog directoryDialog = new FolderBrowserDialog();
			directoryDialog.Description = "Select the directory with DICOM files:";
            directoryDialog.ShowNewFolderButton = false;
			if (directoryDialog.ShowDialog() == DialogResult.OK) 
			{
				inputDirectory = directoryDialog.SelectedPath;
				String indexFileName = initialDirectory + @"/index.htm";
				XmlTextWriter writer = new XmlTextWriter(indexFileName, System.Text.Encoding.UTF8);
				writer.WriteStartElement("b");
				writer.WriteRaw(@"<b><font size='3' color='#ff0000'>Anonymization Started...</font></b>");
				writer.WriteEndElement();
				writer.Close();

				FolderBrowserDialog outputDirDialog = new FolderBrowserDialog();
				outputDirDialog.Description = "Select the directory for storing annonymized files:";
				if (outputDirDialog.ShowDialog() == DialogResult.OK) 
				{
					outputDirectory = outputDirDialog.SelectedPath;
					System.Threading.Thread t = new System.Threading.Thread(new ThreadStart(RetrievingFilesFromDirectory));
					dvtkWebBrowser.Navigate(indexFileName);
					t.Start();
				}
			}
		}

		private void RetrievingFilesFromDirectory()
		{
			try 
			{
				String indexFileName = initialDirectory + @"/index1.htm";
				StringBuilder indexContent = new StringBuilder();
				indexContent.Append("<center><font size='5' color='#0000ff'>");
				if(utility.AnonymizationType)
				{
					indexContent.Append("Anonymized DCM files (Basic)</font></center>");
				}
				else
				{
					indexContent.Append("Anonymized DCM files (Complete)</font></center>");
				}

				indexContent.Append("<left><font color='black' size = '4'><br></br> Input Directory: " + inputDirectory);
				indexContent.Append("</font>");
				indexContent.Append("</left>");
				indexContent.Append("<br></br>");
				indexContent.Append("<font size='3'>");

				HLI.DataSet srcDataset = null;
				DicomFile dcmFile = null;
				
				allDCMFilesTemp = new ArrayList();
				FileInfo mediaInputFileInfo = null;
				string mediaFileDir = inputDirectory;
				DirectoryInfo theDirectoryInfo = new DirectoryInfo(mediaFileDir);
				allDCMFilesTemp = utility.GetFilesRecursively(theDirectoryInfo);
				foreach ( string fileName in allDCMFilesTemp)
				{
					mediaInputFileInfo = new FileInfo(fileName);
					
					counter++;
					
					dcmFile = new DicomFile();
					dcmFile.Read(fileName, mainThread);

					// Get the Data set from the selected DCM file
					srcDataset = dcmFile.DataSet;

					indexContent.Append("<li><a href='" + mediaInputFileInfo.Name + "_" + counter.ToString() +".html'>" + fileName.Substring(inputDirectory.Length) + "</a></li>");

					HLI.DataSet annonymizedDataset = srcDataset.Clone();

					utility.PatientAttributes(annonymizedDataset);
					for ( int number = 0; number < annonymizedDataset.Count; number++)
					{
						HLI.Attribute attribute =  annonymizedDataset[number];
						if (attribute.VR == VR.SQ)
						{
							utility.SequenceAttribute_recursive(attribute);
						}
						else 
						{
							utility.CacheAndRepairIdentifyingAttribute(attribute);
							utility.UpdateAnonymizedAttributes(attribute);
						}
					}

					//Save annonymized data to selected directory
					string savedFileName = "";
					if(mediaInputFileInfo.Extension != "")
					{
						string srcFileNameWithoutExtn = mediaInputFileInfo.Name.Substring(0,(mediaInputFileInfo.Name.Length-4));
						savedFileName = outputDirectory + "\\" + srcFileNameWithoutExtn + "_an";
					}
					else
						savedFileName = outputDirectory + "\\" + mediaInputFileInfo.Name + "_an";

					dcmFile.DataSet = annonymizedDataset;
					dcmFile.Write(savedFileName);
			
					datasets = new AttributeCollections();
					datasets.Add(srcDataset);
					datasets.Add(annonymizedDataset);
		
					//Cleanup temp file
					utility.cleanup();
					
					resultFileName = mediaInputFileInfo.Name;

					CreateTableWithoutNav();
				}

				StreamWriter writer = new StreamWriter(indexFileName);
				indexContent.Append("</font>");
				indexContent.Append("<left><font color='black' size = '4'><br></br> Output Directory: " + outputDirectory);				

				indexContent.Append("</font>");
				indexContent.Append("</left>");
				writer.Write(indexContent.ToString());
				writer.Close();

				dvtkWebBrowser.Navigate(indexFileName);
			}
			catch ( Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		void CreateTableWithoutNav()
		{
			StaticDicomCompare compare = new StaticDicomCompare();
			FlagsDicomAttribute flags = FlagsDicomAttribute.Compare_values | FlagsDicomAttribute.Compare_present | FlagsDicomAttribute.Include_sequence_items;
			StringCollection descriptions = new StringCollection();
			descriptions.Add("Src Dataset");
			descriptions.Add("Annonymized Dataset");
			CompareResults compareResults = compare.CompareAttributeSets("DataSet compare results", datasets, descriptions, flags);
			
			StringBuilder tableString = new StringBuilder();
			tableString.Append("<left><font color='#0000ff' size = '4'>");
			tableString.Append(string.Format("Comparing the DICOM file {0} with anonymized file",resultFileName));
			tableString.Append("</font>");
			tableString.Append("</left>");
			tableString.Append("<br></br>");
			tableString.Append(compareResults.Table.ConvertToHtml());

			StreamWriter writer = new StreamWriter(initialDirectory + "\\" + resultFileName + "_" + counter.ToString() +".html");
			writer.Write(tableString.ToString());
			writer.Close();
		}

		private void AnonymizeDirBasic_Click(object sender, System.EventArgs e)
		{
			anonymizationMode = true;
			AnonynimizeDirectory();
		}

		private void AnonymizeDirComplete_Click(object sender, System.EventArgs e)
		{
			anonymizationMode = false;
			AnonynimizeDirectory();
		}

		private void Exit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void AboutUs_Click(object sender, System.EventArgs e)
		{
			AboutForm form = new AboutForm("DICOM Anonymizer");
			form.Show();
		}

		private void DICOMAnonymize_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			ClearTempFiles();
		}

		private void ClearTempFiles()
		{
			try
			{
				Directory.Delete(initialDirectory,true);
			}
			catch(Exception e)
			{
				MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void toolBarAnonymizer_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if( e.Button == toolBarButtonRun)
			{
				if (mode.ShowDialog() == DialogResult.OK)
				{
					if(!mode.AnonymizeMode)
					{
						anonymizationMode = false;
					}
					else
					{
						anonymizationMode = true;
					}
					if(mode.AnonymizeFile)
					{
						AnonymizeDCMFile();
					}
					else
					{
						AnonynimizeDirectory();
					}
				}
			}
			else if( e.Button == toolBarButtonBack)
			{
				this.dvtkWebBrowser.Back();
			}
			else if( e.Button == toolBarButtonForward)
			{
				this.dvtkWebBrowser.Forward();
			}
			else{}
		}

		private void dvtkWebBrowser_BackwardFormwardEnabledStateChangeEvent()
		{
			this.toolBarButtonBack.Enabled = this.dvtkWebBrowser.IsBackwardEnabled;
			this.toolBarButtonForward.Enabled = this.dvtkWebBrowser.IsForwardEnabled;
		}

        private void CreateDICOMDIR_Click(object sender, EventArgs e)
        {
            try
            {
                CreateDICOMDIR.Enabled = false;

                // Load the Media session
                string sessionFileName = Application.StartupPath + "\\Media.ses";
                Dvtk.Sessions.MediaSession mediaSession = Dvtk.Sessions.MediaSession.LoadFromFile(sessionFileName);
                mediaSession.ResultsRootDirectory = outputDirectory;
                mediaSession.StorageMode = Dvtk.Sessions.StorageMode.NoStorage;

                // Move all DCM files to directory "DICOM" in result root directory.
                DirectoryInfo theDirectoryInfo = new DirectoryInfo(outputDirectory + "\\DICOM");

                // Create "DICOM" directory if it doesn't exist
                if (!theDirectoryInfo.Exists)
                {
                    theDirectoryInfo.Create();
                }
                else
                {
                    // Remove existing DCM files from "DICOM" directory
                    FileInfo[] files = theDirectoryInfo.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        file.Delete();
                    }
                }

                int i = 0;
                string[] dcmFiles = new string[allDCMFilesTemp.Count];
                foreach (string dcmFile in allDCMFilesTemp)
                {
                    FileInfo theFileInfo = new FileInfo(dcmFile);
                    string destFileName = theDirectoryInfo.FullName + "\\" + theFileInfo.Name;
                    theFileInfo.CopyTo(destFileName, true);
                    dcmFiles.SetValue(destFileName, i);
                    i++;
                }

                if (mediaSession.GenerateDICOMDIR(dcmFiles))
                {
                    string msg = string.Format("DICOMDIR created successfully in directory:{0}", outputDirectory);
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exp)
            {
                string msg = "Exception in DICOMDIR generation:" + exp.Message + "\n";
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }		
	}

	public class MainThread : DicomThread
	{
		public MainThread()
		{}

		protected override void Execute()
		{}
	}
}
