using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Windows.Forms;
using System.Data;

namespace DCMCompare
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class DCMCompareForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem MenuItem_Help;
		private System.Windows.Forms.MenuItem MenuItem_FileOpen;
		private System.Windows.Forms.MenuItem MenuItem_FileExit;
		private System.Windows.Forms.MenuItem MenuItem_File;
		private System.Windows.Forms.MenuItem MenuItem_AboutDCMCompare;
		private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.TabPage TabCompareSummaryResults;
		private System.Windows.Forms.TabPage TabCompareDetailResults;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private AxSHDocVw.AxWebBrowser WebBrowserDetail;
		private AxSHDocVw.AxWebBrowser WebBrowserResults;
		public static string firstDCMFile;
		private System.Windows.Forms.MenuItem MenuItem_FileShow;
		public static string secondDCMFile;

		public DCMCompareForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DCMCompareForm));
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.MenuItem_File = new System.Windows.Forms.MenuItem();
			this.MenuItem_FileOpen = new System.Windows.Forms.MenuItem();
			this.MenuItem_FileExit = new System.Windows.Forms.MenuItem();
			this.MenuItem_Help = new System.Windows.Forms.MenuItem();
			this.MenuItem_AboutDCMCompare = new System.Windows.Forms.MenuItem();
			this.TabControl = new System.Windows.Forms.TabControl();
			this.TabCompareSummaryResults = new System.Windows.Forms.TabPage();
			this.WebBrowserResults = new AxSHDocVw.AxWebBrowser();
			this.TabCompareDetailResults = new System.Windows.Forms.TabPage();
			this.WebBrowserDetail = new AxSHDocVw.AxWebBrowser();
			this.MenuItem_FileShow = new System.Windows.Forms.MenuItem();
			this.TabControl.SuspendLayout();
			this.TabCompareSummaryResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.WebBrowserResults)).BeginInit();
			this.TabCompareDetailResults.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.WebBrowserDetail)).BeginInit();
			this.SuspendLayout();
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.MenuItem_File,
																					  this.MenuItem_Help});
			// 
			// MenuItem_File
			// 
			this.MenuItem_File.Index = 0;
			this.MenuItem_File.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.MenuItem_FileOpen,
																						  this.MenuItem_FileShow,
																						  this.MenuItem_FileExit});
			this.MenuItem_File.Text = "File";
			// 
			// MenuItem_FileOpen
			// 
			this.MenuItem_FileOpen.Index = 0;
			this.MenuItem_FileOpen.Text = "&Open";
			this.MenuItem_FileOpen.Click += new System.EventHandler(this.Open_Click);
			// 
			// MenuItem_FileExit
			// 
			this.MenuItem_FileExit.Index = 2;
			this.MenuItem_FileExit.Text = "&Exit";
			this.MenuItem_FileExit.Click += new System.EventHandler(this.MenuItem_FileExit_Click);
			// 
			// MenuItem_Help
			// 
			this.MenuItem_Help.Index = 1;
			this.MenuItem_Help.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.MenuItem_AboutDCMCompare});
			this.MenuItem_Help.Text = "Help";
			// 
			// MenuItem_AboutDCMCompare
			// 
			this.MenuItem_AboutDCMCompare.Index = 0;
			this.MenuItem_AboutDCMCompare.Text = "About DCM Compare";
			// 
			// TabControl
			// 
			this.TabControl.Controls.Add(this.TabCompareSummaryResults);
			this.TabControl.Controls.Add(this.TabCompareDetailResults);
			this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TabControl.Location = new System.Drawing.Point(0, 0);
			this.TabControl.Name = "TabControl";
			this.TabControl.SelectedIndex = 0;
			this.TabControl.Size = new System.Drawing.Size(640, 505);
			this.TabControl.TabIndex = 0;
			// 
			// TabCompareSummaryResults
			// 
			this.TabCompareSummaryResults.AutoScroll = true;
			this.TabCompareSummaryResults.Controls.Add(this.WebBrowserResults);
			this.TabCompareSummaryResults.Location = new System.Drawing.Point(4, 22);
			this.TabCompareSummaryResults.Name = "TabCompareSummaryResults";
			this.TabCompareSummaryResults.Size = new System.Drawing.Size(632, 479);
			this.TabCompareSummaryResults.TabIndex = 0;
			this.TabCompareSummaryResults.Text = "Compare Results Overview";
			// 
			// WebBrowserResults
			// 
			this.WebBrowserResults.ContainingControl = this;
			this.WebBrowserResults.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebBrowserResults.Enabled = true;
			this.WebBrowserResults.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.WebBrowserResults.Location = new System.Drawing.Point(0, 0);
			this.WebBrowserResults.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebBrowserResults.OcxState")));
			this.WebBrowserResults.Size = new System.Drawing.Size(632, 479);
			this.WebBrowserResults.TabIndex = 0;
			// 
			// TabCompareDetailResults
			// 
			this.TabCompareDetailResults.AutoScroll = true;
			this.TabCompareDetailResults.Controls.Add(this.WebBrowserDetail);
			this.TabCompareDetailResults.Location = new System.Drawing.Point(4, 22);
			this.TabCompareDetailResults.Name = "TabCompareDetailResults";
			this.TabCompareDetailResults.Size = new System.Drawing.Size(632, 479);
			this.TabCompareDetailResults.TabIndex = 1;
			this.TabCompareDetailResults.Text = "Detail Compare Results";
			// 
			// WebBrowserDetail
			// 
			this.WebBrowserDetail.ContainingControl = this;
			this.WebBrowserDetail.Dock = System.Windows.Forms.DockStyle.Fill;
			this.WebBrowserDetail.Enabled = true;
			this.WebBrowserDetail.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.WebBrowserDetail.Location = new System.Drawing.Point(0, 0);
			this.WebBrowserDetail.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebBrowserDetail.OcxState")));
			this.WebBrowserDetail.Size = new System.Drawing.Size(632, 479);
			this.WebBrowserDetail.TabIndex = 0;
			// 
			// MenuItem_FileShow
			// 
			this.MenuItem_FileShow.Index = 1;
			this.MenuItem_FileShow.Text = "&Show";
			this.MenuItem_FileShow.Click += new System.EventHandler(this.MenuItem_FileShow_Click);
			// 
			// DCMCompareForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(640, 505);
			this.Controls.Add(this.TabControl);
			this.Menu = this.mainMenu1;
			this.Name = "DCMCompareForm";
			this.Text = "DCM Compare";
			this.TabControl.ResumeLayout(false);
			this.TabCompareSummaryResults.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.WebBrowserResults)).EndInit();
			this.TabCompareDetailResults.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.WebBrowserDetail)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion


		private void Open_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog theOpenFileDialog = new OpenFileDialog();

			theOpenFileDialog.Filter = "All files (*.*)|*.*";
			theOpenFileDialog.Title = "Select first DCM file";
			theOpenFileDialog.Multiselect = false;
			theOpenFileDialog.ReadOnlyChecked = true;

			// Show the file dialog.
			// If the user pressed the OK button...
			if (theOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				// Add all DCM files selected.
				firstDCMFile = theOpenFileDialog.FileName;
				theOpenFileDialog.Filter = "All files (*.*)|*.*";
				theOpenFileDialog.Title = "Select second DCM file";
				theOpenFileDialog.Multiselect = false;
				theOpenFileDialog.ReadOnlyChecked = true;

				if (theOpenFileDialog.ShowDialog() == DialogResult.OK)
				{
					secondDCMFile = theOpenFileDialog.FileName;
				}
				this.Close();
			}		
		}

		private void MenuItem_FileExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void MenuItem_FileShow_Click(object sender, System.EventArgs e)
		{			
			if (TabControl.SelectedTab == TabCompareSummaryResults)
			{
				ShowResultHtml(ConvertXmlToHtml("Summary_001_" + MainSession.SCRIPT_FILE_NAME + "_res.xml"));
			}

			if (TabControl.SelectedTab == TabCompareDetailResults)
			{
				ShowDetailHtml(ConvertXmlToHtml("Detail_001_" + MainSession.SCRIPT_FILE_NAME + "_res.xml"));
			}
		}

		private string ConvertXmlToHtml(string theXmlFileNameOnly)
		{
			string theXmlFullFileName;
			string theHtmlFullFileName;
			string theResultsStyleSheetFullFileName;

			theXmlFullFileName = System.IO.Path.Combine( Application.StartupPath + "\\Results\\", theXmlFileNameOnly);
			theHtmlFullFileName = theXmlFullFileName.Replace(".xml", ".html");
			theResultsStyleSheetFullFileName = Application.StartupPath + "\\DVT_RESULTS.xslt";

			XslTransform xslt = new XslTransform ();

			xslt.Load(theResultsStyleSheetFullFileName);

			XPathDocument xpathdocument = new XPathDocument (theXmlFullFileName);

			XmlTextWriter writer = new XmlTextWriter (theHtmlFullFileName, System.Text.Encoding.UTF8);
			writer.Formatting = Formatting.None;
			xslt.Transform (xpathdocument, null, writer, null);
			writer.Flush ();
			writer.Close ();
			return theHtmlFullFileName;
		}

		private void ShowResultHtml(string theHtmlFullFileName)
		{
			object Zero = 0;
			object EmptyString = "";

			WebBrowserResults.Navigate (theHtmlFullFileName, ref Zero, ref EmptyString, ref EmptyString, ref EmptyString);		
		}

		private void ShowDetailHtml(string theHtmlFullFileName)
		{
			object Zero = 0;
			object EmptyString = "";

			WebBrowserDetail.Navigate (theHtmlFullFileName, ref Zero, ref EmptyString, ref EmptyString, ref EmptyString);		
		}
	}
}
