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
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Media;
using System.IO;
using System.Net.NetworkInformation;
using System.Net;
using System.Xml.XPath;
using System.Xml.Xsl;
using DvtkHighLevelInterface.Common.Threads;
using DvtkHighLevelInterface.Dicom.Files;
using DvtkHighLevelInterface.Dicom.Messages;
using DvtIheAcquisitionModalityWrapper;
using Dvtk.IheActors.Dicom;
using Dvtk.IheActors.Bases;
using DvtkApplicationLayer;
using DvtkApplicationLayer.UserInterfaces;

namespace ModalityEmulator
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class ModalityEmulator : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenuEmulator;
		private System.Windows.Forms.MenuItem menuItemFile;
		private System.Windows.Forms.MenuItem menuItemHelp;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.MenuItem menuItemConfigSystem;
		private System.Windows.Forms.MenuItem menuItemConfigEmulator;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.TabControl tabControlEmulator;
		private System.Windows.Forms.TabPage tabPageControl;
		private System.Windows.Forms.TabPage tabPageConfigSystems;
		private System.Windows.Forms.TabPage tabPageConfigEmulator;
		private System.Windows.Forms.TabPage tabPageWLM;
		private System.Windows.Forms.TabPage tabPageMPPSCreate;
		private System.Windows.Forms.TabPage tabPageMPPSSet;
		private System.Windows.Forms.TabPage tabPageImageStorage;
		private System.Windows.Forms.GroupBox groupBoxRIS;
		private System.Windows.Forms.GroupBox groupBoxPACS;
		private System.Windows.Forms.PictureBox pictureBoxRIS;
		private System.Windows.Forms.PictureBox pictureBoxPACS;
		private System.Windows.Forms.Button buttonPingRIS;
		private System.Windows.Forms.Button buttonEchoRIS;
		private System.Windows.Forms.Button buttonReqWL;
		private System.Windows.Forms.Button buttonPingPACS;
		private System.Windows.Forms.Button buttonEchoPACS;
		private System.Windows.Forms.Button buttonStoreImage;
		private System.Windows.Forms.Label labelHintPACS;
		private System.Windows.Forms.Label labelHintRIS;
		private System.Windows.Forms.Label labelRIS;
		private System.Windows.Forms.Label labelPACS;
		private System.Windows.Forms.GroupBox groupBoxConfigRIS;
		private System.Windows.Forms.GroupBox groupBoxConfigPACS;
		private System.Windows.Forms.Label labelRISIPAddress;
		private System.Windows.Forms.Label labelRISAE;
        private System.Windows.Forms.Label labelRISPort;
        private MaskedTextBox textBoxRISIPAddress;
		private System.Windows.Forms.TextBox textBoxRISAETitle;
        private System.Windows.Forms.TextBox textBoxPACSAETitle;
        private MaskedTextBox textBoxPACSIPAddress;
		private System.Windows.Forms.Label labelPACSPort;
		private System.Windows.Forms.Label labelPACSAE;
		private System.Windows.Forms.Label labelPACSIPAddress;
        private System.Windows.Forms.TextBox textBoxAETitle;
		private System.Windows.Forms.TextBox textBoxImplUID;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label labelEmulatorAE;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBoxImplName;
		private System.Windows.Forms.TextBox textBoxSystemName;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button buttonMPPSMsg;
		private System.Windows.Forms.GroupBox groupBoxConfigMPPS;
        private System.Windows.Forms.TextBox textBoxMPPSAETitle;
        private MaskedTextBox textBoxMPPSIPAddress;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TabPage tabPageResults;
		private System.Windows.Forms.Label labelReqWLM;
		private System.Windows.Forms.Label labelMPPSMsg;
		private System.Windows.Forms.Label labelStoreImage;
		private System.Windows.Forms.Label labelStoreCommit;
		private System.Windows.Forms.TabPage tabPageActivityLogging;
		private Dvtk.IheActors.UserInterfaces.UserControlActivityLogging userControlActivityLoggingEmulator;
		private DCMEditor dcmEditorWLM;
		private DCMEditor dcmEditorMPPSCreate;
		private DCMEditor dcmEditorMPPSSet;
		private DCMEditor dcmEditorStorage;
        private DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew dvtkWebBrowserEmulator;
		private System.Windows.Forms.Button buttonStorageCommit;
		private System.Windows.Forms.ToolBar toolBarEmulator;
		private System.Windows.Forms.ImageList imageListEmulator;
		private System.Windows.Forms.ToolBarButton toolBarButtonLeft;
		private System.Windows.Forms.ToolBarButton toolBarButtonUp;
		private System.Windows.Forms.ToolBarButton toolBarButtonright;
		private System.Windows.Forms.ToolBarButton toolBarButtonResults;
		private System.Windows.Forms.ToolBarButton toolBarButtonLog;
		private System.Windows.Forms.ToolBarButton toolBarButtonStop;
		private System.Windows.Forms.TabPage tabPageDummyPatient;
		private DCMEditor dcmEditorDummyPatient;
        private System.Windows.Forms.Label label8;
		private System.ComponentModel.IContainer components;
        private System.Windows.Forms.Label labelEchoRIS;
        private System.Windows.Forms.Label labelEchoPACS;
        private System.Windows.Forms.Label labelPingRIS;
        private System.Windows.Forms.Label labelPingPACS;
        private System.Windows.Forms.ToolBarButton toolBarButtonConfigSys;
        private System.Windows.Forms.ToolBarButton toolBarButtonSep;
        private System.Windows.Forms.ToolBarButton toolBarButtonConfigEmulator;
        private TextBox textBoxPACSCommitAETitle;
        private MaskedTextBox textBoxPACSCommitIPAddress;
        private Label label9;
        private Label label10;
        private Label label11;
        private GroupBox groupBoxStorePACS;
        private GroupBox groupBoxCommitPACS;
        private Button buttonTS;
        private ComboBox emulatorIPAddress;
        private Label label12;
        private GroupBox groupBox1;
        private RadioButton radioButtonDiff;
        private RadioButton radioButtonSingle;
        private MaskedTextBox textBoxTimeOut;
        private MaskedTextBox textBoxPort;
        private MaskedTextBox textBoxRISPort;
        private MaskedTextBox textBoxMPPSPort;
        private MaskedTextBox textBoxPACSPort;
        private MaskedTextBox textBoxPACSCommitPort;
        private TabPage tabPageMPPSDiscontinued;
        private DCMEditor dcmEditorDiscontinued;

		AcquisitionModalityWrapper wrapper = null;
		string wlmRqDataDirectory = "";
		string wlmRspDataDirectory = "";
		string mppsDataDirectory = "";
		string storageDataDirectory = "";
		bool isCreated = false;
		bool isInitialized = false;
		bool isTerminated = false;
        bool isUIUpdateReqd = true;
        bool isSyncCommit = true;
		DicomQueryItem selectedMWLItem = null;
        private ArrayList selectedTS = new ArrayList();

        string echoResultFile = "";
        string lastRISIPAddress;
        string lastRISAETitle;
        string lastMPPSIPAddress;
        string lastMPPSAETitle;
        string lastPACSIPAddress;
        string lastPACSAETitle;
        string lastPACSCommitIPAddress;
        string lastPACSCommitAETitle;
        string lastPort;
        string lastRISPort;
        string lastPACSPort;
        string lastPACSCommitPort;
        string lastMPPSPort;

        // Needed to be able to differentiate between controls changed by the user
        // and controls changed by an UpdateConfig method.
        private int updateCount = 0;
        
		string userConfigFilePath = Application.StartupPath + @"\" + "UserConfig.txt";

		public ModalityEmulator()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//Hide the tab pages
			tabControlEmulator.Controls.Remove(tabPageConfigSystems);
			tabControlEmulator.Controls.Remove(tabPageConfigEmulator);
			tabControlEmulator.Controls.Remove(tabPageWLM);
			tabControlEmulator.Controls.Remove(tabPageMPPSCreate);
			tabControlEmulator.Controls.Remove(tabPageMPPSSet);
            tabControlEmulator.Controls.Remove(tabPageMPPSDiscontinued);
			tabControlEmulator.Controls.Remove(tabPageImageStorage);
			tabControlEmulator.Controls.Remove(tabPageResults);
			tabControlEmulator.Controls.Remove(tabPageActivityLogging);
			tabControlEmulator.Controls.Remove(tabPageDummyPatient);

			buttonReqWL.Enabled = true;
			buttonStoreImage.Enabled = false;
			buttonMPPSMsg.Enabled = false;
			buttonStorageCommit.Enabled = false;
			//buttonTS.Enabled = false;

            string definitionDir = Environment.GetEnvironmentVariable("COMMONPROGRAMFILES") + @"\DVTk\Definition Files\DICOM\";

			wlmRqDataDirectory = Application.StartupPath + @"\data\worklist\WLM RQ\";
			wlmRspDataDirectory = Application.StartupPath + @"\data\worklist\WLM RSP\";
			mppsDataDirectory = Application.StartupPath + @"\data\mpps\";
			storageDataDirectory = Application.StartupPath + @"\data\acquisitionModality\default\";

			// Load definition files
            dcmEditorWLM.DefFile = definitionDir + "Modality Worklist Information Model - Find.def";
            dcmEditorMPPSCreate.DefFile = definitionDir + "Modality Performed Procedure Step.def";
            dcmEditorMPPSSet.DefFile = definitionDir + "Modality Performed Procedure Step.def";
            dcmEditorDiscontinued.DefFile = definitionDir + "Modality Performed Procedure Step.def";
            dcmEditorStorage.DefFile = definitionDir + "Secondary Capture Image Storage.def";
            dcmEditorDummyPatient.DefFile = definitionDir + "Modality Worklist Information Model - Find.def";

            //dcmEditorWLM.DefFile = Application.StartupPath + @"\definitions\Allotherattributes.def";
            //dcmEditorMPPSCreate.DefFile = Application.StartupPath + @"\definitions\Allotherattributes.def";
            //dcmEditorMPPSSet.DefFile = Application.StartupPath + @"\definitions\Allotherattributes.def";
            //dcmEditorDiscontinued.DefFile = Application.StartupPath + @"\definitions\Allotherattributes.def";
            //dcmEditorStorage.DefFile = Application.StartupPath + @"\definitions\Allotherattributes.def";
            //dcmEditorDummyPatient.DefFile = Application.StartupPath + @"\definitions\Allotherattributes.def";

			// Set data directory for temp pix files
			dcmEditorWLM.DCMFileDataDirectory = Application.StartupPath + @"\results\";
			dcmEditorMPPSCreate.DCMFileDataDirectory = Application.StartupPath + @"\results\";
			dcmEditorMPPSSet.DCMFileDataDirectory = Application.StartupPath + @"\results\";
            dcmEditorDiscontinued.DCMFileDataDirectory = Application.StartupPath + @"\results\";
			dcmEditorStorage.DCMFileDataDirectory = Application.StartupPath + @"\results\";
			dcmEditorDummyPatient.DCMFileDataDirectory = Application.StartupPath + @"\results\";

			// Display the DCM file
			dcmEditorDummyPatient.DCMFile = wlmRspDataDirectory + "DummyPatient.dcm";

			// Set dcm file from user config file
			StreamReader reader = new StreamReader(userConfigFilePath);
			if(reader != null)
			{
				string line = reader.ReadLine().Trim();
				if(line != "")
				{
					if(Path.IsPathRooted(line))
						dcmEditorWLM.DCMFile = line;
					else
						dcmEditorWLM.DCMFile = Application.StartupPath + line;
				}
				else
					dcmEditorWLM.DCMFile = wlmRqDataDirectory + "worklistquery2.dcm";

				line = reader.ReadLine().Trim();
				if(line != "")
				{
					if(Path.IsPathRooted(line))
						dcmEditorMPPSCreate.DCMFile = line;
					else
						dcmEditorMPPSCreate.DCMFile = Application.StartupPath + line;
				}
				else
					dcmEditorMPPSCreate.DCMFile = mppsDataDirectory + "mpps-inprogress1.dcm";

				line = reader.ReadLine().Trim();
				if(line != "")
				{
					if(Path.IsPathRooted(line))
						dcmEditorStorage.DCMFile = line;
					else
						dcmEditorStorage.DCMFile = Application.StartupPath + line;
				}
				else
					dcmEditorStorage.DCMFile = storageDataDirectory + "1I00001.dcm";

				line = reader.ReadLine().Trim();
				if(line != "")
				{
					if(Path.IsPathRooted(line))
						dcmEditorMPPSSet.DCMFile = line;
					else
						dcmEditorMPPSSet.DCMFile = Application.StartupPath + line;
				}
				else
					dcmEditorMPPSSet.DCMFile = mppsDataDirectory + "mpps-completed1.dcm";

                line = reader.ReadLine().Trim();
                if (line != "")
                {
                    if (Path.IsPathRooted(line))
                        dcmEditorDiscontinued.DCMFile = line;
                    else
                        dcmEditorDiscontinued.DCMFile = Application.StartupPath + line;
                }
                else
                    dcmEditorDiscontinued.DCMFile = mppsDataDirectory + "mpps-discontinued1.dcm";

				reader.Close();
			}

			this.dvtkWebBrowserEmulator.XmlStyleSheetFullFileName = Application.StartupPath + "\\DVT_RESULTS.xslt";
            this.dvtkWebBrowserEmulator.BackwardFormwardEnabledStateChangeEvent += new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew.BackwardFormwardEnabledStateChangeEventHandler(dvtkWebBrowserEmulator_BackwardFormwardEnabledStateChangeEvent);

            selectedTS.Add("1.2.840.10008.1.2");

            //Get the IP Address of the current machine
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            if (addr != null)
            {
                foreach (IPAddress address in addr)
                {
                    emulatorIPAddress.Items.Add(address.ToString());
                }
                emulatorIPAddress.SelectedItem = emulatorIPAddress.Items[0];
            }

			//Create instance of integration profile
            CreateIntegrationProfile();
			isCreated = true;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModalityEmulator));
            this.mainMenuEmulator = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemFile = new System.Windows.Forms.MenuItem();
            this.menuItemConfigSystem = new System.Windows.Forms.MenuItem();
            this.menuItemConfigEmulator = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.tabControlEmulator = new System.Windows.Forms.TabControl();
            this.tabPageControl = new System.Windows.Forms.TabPage();
            this.groupBoxPACS = new System.Windows.Forms.GroupBox();
            this.labelPingPACS = new System.Windows.Forms.Label();
            this.labelEchoPACS = new System.Windows.Forms.Label();
            this.labelStoreCommit = new System.Windows.Forms.Label();
            this.labelStoreImage = new System.Windows.Forms.Label();
            this.buttonStorageCommit = new System.Windows.Forms.Button();
            this.labelPACS = new System.Windows.Forms.Label();
            this.labelHintPACS = new System.Windows.Forms.Label();
            this.buttonStoreImage = new System.Windows.Forms.Button();
            this.buttonEchoPACS = new System.Windows.Forms.Button();
            this.buttonPingPACS = new System.Windows.Forms.Button();
            this.pictureBoxPACS = new System.Windows.Forms.PictureBox();
            this.groupBoxRIS = new System.Windows.Forms.GroupBox();
            this.labelPingRIS = new System.Windows.Forms.Label();
            this.labelEchoRIS = new System.Windows.Forms.Label();
            this.labelMPPSMsg = new System.Windows.Forms.Label();
            this.labelReqWLM = new System.Windows.Forms.Label();
            this.labelRIS = new System.Windows.Forms.Label();
            this.labelHintRIS = new System.Windows.Forms.Label();
            this.buttonMPPSMsg = new System.Windows.Forms.Button();
            this.buttonReqWL = new System.Windows.Forms.Button();
            this.buttonEchoRIS = new System.Windows.Forms.Button();
            this.buttonPingRIS = new System.Windows.Forms.Button();
            this.pictureBoxRIS = new System.Windows.Forms.PictureBox();
            this.tabPageConfigSystems = new System.Windows.Forms.TabPage();
            this.groupBoxConfigMPPS = new System.Windows.Forms.GroupBox();
            this.textBoxMPPSPort = new System.Windows.Forms.MaskedTextBox();
            this.textBoxMPPSAETitle = new System.Windows.Forms.TextBox();
            this.textBoxMPPSIPAddress = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxConfigPACS = new System.Windows.Forms.GroupBox();
            this.textBoxPACSCommitAETitle = new System.Windows.Forms.TextBox();
            this.textBoxPACSCommitIPAddress = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxPACSAETitle = new System.Windows.Forms.TextBox();
            this.textBoxPACSIPAddress = new System.Windows.Forms.MaskedTextBox();
            this.labelPACSPort = new System.Windows.Forms.Label();
            this.labelPACSAE = new System.Windows.Forms.Label();
            this.labelPACSIPAddress = new System.Windows.Forms.Label();
            this.groupBoxStorePACS = new System.Windows.Forms.GroupBox();
            this.textBoxPACSPort = new System.Windows.Forms.MaskedTextBox();
            this.groupBoxCommitPACS = new System.Windows.Forms.GroupBox();
            this.textBoxPACSCommitPort = new System.Windows.Forms.MaskedTextBox();
            this.groupBoxConfigRIS = new System.Windows.Forms.GroupBox();
            this.textBoxRISPort = new System.Windows.Forms.MaskedTextBox();
            this.textBoxRISAETitle = new System.Windows.Forms.TextBox();
            this.textBoxRISIPAddress = new System.Windows.Forms.MaskedTextBox();
            this.labelRISPort = new System.Windows.Forms.Label();
            this.labelRISAE = new System.Windows.Forms.Label();
            this.labelRISIPAddress = new System.Windows.Forms.Label();
            this.tabPageConfigEmulator = new System.Windows.Forms.TabPage();
            this.textBoxPort = new System.Windows.Forms.MaskedTextBox();
            this.textBoxTimeOut = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonDiff = new System.Windows.Forms.RadioButton();
            this.radioButtonSingle = new System.Windows.Forms.RadioButton();
            this.emulatorIPAddress = new System.Windows.Forms.ComboBox();
            this.buttonTS = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxSystemName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxImplName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAETitle = new System.Windows.Forms.TextBox();
            this.textBoxImplUID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelEmulatorAE = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPageWLM = new System.Windows.Forms.TabPage();
            this.dcmEditorWLM = new DvtkApplicationLayer.UserInterfaces.DCMEditor();
            this.tabPageMPPSCreate = new System.Windows.Forms.TabPage();
            this.dcmEditorMPPSCreate = new DvtkApplicationLayer.UserInterfaces.DCMEditor();
            this.tabPageMPPSSet = new System.Windows.Forms.TabPage();
            this.dcmEditorMPPSSet = new DvtkApplicationLayer.UserInterfaces.DCMEditor();
            this.tabPageMPPSDiscontinued = new System.Windows.Forms.TabPage();
            this.dcmEditorDiscontinued = new DvtkApplicationLayer.UserInterfaces.DCMEditor();
            this.tabPageImageStorage = new System.Windows.Forms.TabPage();
            this.dcmEditorStorage = new DvtkApplicationLayer.UserInterfaces.DCMEditor();
            this.tabPageDummyPatient = new System.Windows.Forms.TabPage();
            this.dcmEditorDummyPatient = new DvtkApplicationLayer.UserInterfaces.DCMEditor();
            this.tabPageActivityLogging = new System.Windows.Forms.TabPage();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.dvtkWebBrowserEmulator = new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew();
            this.toolBarEmulator = new System.Windows.Forms.ToolBar();
            this.toolBarButtonConfigEmulator = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonConfigSys = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonResults = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonLog = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonStop = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonSep = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonLeft = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonUp = new System.Windows.Forms.ToolBarButton();
            this.toolBarButtonright = new System.Windows.Forms.ToolBarButton();
            this.imageListEmulator = new System.Windows.Forms.ImageList(this.components);
            this.userControlActivityLoggingEmulator = new Dvtk.IheActors.UserInterfaces.UserControlActivityLogging();
            this.tabControlEmulator.SuspendLayout();
            this.tabPageControl.SuspendLayout();
            this.groupBoxPACS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPACS)).BeginInit();
            this.groupBoxRIS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRIS)).BeginInit();
            this.tabPageConfigSystems.SuspendLayout();
            this.groupBoxConfigMPPS.SuspendLayout();
            this.groupBoxConfigPACS.SuspendLayout();
            this.groupBoxStorePACS.SuspendLayout();
            this.groupBoxCommitPACS.SuspendLayout();
            this.groupBoxConfigRIS.SuspendLayout();
            this.tabPageConfigEmulator.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageWLM.SuspendLayout();
            this.tabPageMPPSCreate.SuspendLayout();
            this.tabPageMPPSSet.SuspendLayout();
            this.tabPageMPPSDiscontinued.SuspendLayout();
            this.tabPageImageStorage.SuspendLayout();
            this.tabPageDummyPatient.SuspendLayout();
            this.tabPageActivityLogging.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuEmulator
            // 
            this.mainMenuEmulator.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemFile,
            this.menuItemHelp});
            // 
            // menuItemFile
            // 
            this.menuItemFile.Index = 0;
            this.menuItemFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemConfigSystem,
            this.menuItemConfigEmulator,
            this.menuItemExit});
            this.menuItemFile.Text = "&File";
            // 
            // menuItemConfigSystem
            // 
            this.menuItemConfigSystem.Index = 0;
            this.menuItemConfigSystem.Text = "Configure Systems";
            this.menuItemConfigSystem.Click += new System.EventHandler(this.menuItemConfigSystem_Click);
            // 
            // menuItemConfigEmulator
            // 
            this.menuItemConfigEmulator.Index = 1;
            this.menuItemConfigEmulator.Text = "Configure Emulator";
            this.menuItemConfigEmulator.Click += new System.EventHandler(this.menuItemConfigEmulator_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 2;
            this.menuItemExit.Text = "&Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 1;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAbout});
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 0;
            this.menuItemAbout.Text = "About Modality Emulator";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // tabControlEmulator
            // 
            this.tabControlEmulator.Controls.Add(this.tabPageControl);
            this.tabControlEmulator.Controls.Add(this.tabPageConfigSystems);
            this.tabControlEmulator.Controls.Add(this.tabPageConfigEmulator);
            this.tabControlEmulator.Controls.Add(this.tabPageWLM);
            this.tabControlEmulator.Controls.Add(this.tabPageMPPSCreate);
            this.tabControlEmulator.Controls.Add(this.tabPageMPPSSet);
            this.tabControlEmulator.Controls.Add(this.tabPageMPPSDiscontinued);
            this.tabControlEmulator.Controls.Add(this.tabPageImageStorage);
            this.tabControlEmulator.Controls.Add(this.tabPageDummyPatient);
            this.tabControlEmulator.Controls.Add(this.tabPageActivityLogging);
            this.tabControlEmulator.Controls.Add(this.tabPageResults);
            this.tabControlEmulator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlEmulator.Location = new System.Drawing.Point(0, 28);
            this.tabControlEmulator.Name = "tabControlEmulator";
            this.tabControlEmulator.SelectedIndex = 0;
            this.tabControlEmulator.Size = new System.Drawing.Size(766, 469);
            this.tabControlEmulator.TabIndex = 1;
            // 
            // tabPageControl
            // 
            this.tabPageControl.Controls.Add(this.groupBoxPACS);
            this.tabPageControl.Controls.Add(this.groupBoxRIS);
            this.tabPageControl.Location = new System.Drawing.Point(4, 22);
            this.tabPageControl.Name = "tabPageControl";
            this.tabPageControl.Size = new System.Drawing.Size(758, 443);
            this.tabPageControl.TabIndex = 0;
            this.tabPageControl.Text = "Control";
            this.tabPageControl.UseVisualStyleBackColor = true;
            // 
            // groupBoxPACS
            // 
            this.groupBoxPACS.Controls.Add(this.labelPingPACS);
            this.groupBoxPACS.Controls.Add(this.labelEchoPACS);
            this.groupBoxPACS.Controls.Add(this.labelStoreCommit);
            this.groupBoxPACS.Controls.Add(this.labelStoreImage);
            this.groupBoxPACS.Controls.Add(this.buttonStorageCommit);
            this.groupBoxPACS.Controls.Add(this.labelPACS);
            this.groupBoxPACS.Controls.Add(this.labelHintPACS);
            this.groupBoxPACS.Controls.Add(this.buttonStoreImage);
            this.groupBoxPACS.Controls.Add(this.buttonEchoPACS);
            this.groupBoxPACS.Controls.Add(this.buttonPingPACS);
            this.groupBoxPACS.Controls.Add(this.pictureBoxPACS);
            this.groupBoxPACS.Location = new System.Drawing.Point(0, 240);
            this.groupBoxPACS.Name = "groupBoxPACS";
            this.groupBoxPACS.Size = new System.Drawing.Size(752, 216);
            this.groupBoxPACS.TabIndex = 1;
            this.groupBoxPACS.TabStop = false;
            this.groupBoxPACS.Text = "PACS/Workstation Systems";
            // 
            // labelPingPACS
            // 
            this.labelPingPACS.Location = new System.Drawing.Point(506, 24);
            this.labelPingPACS.Name = "labelPingPACS";
            this.labelPingPACS.Size = new System.Drawing.Size(240, 23);
            this.labelPingPACS.TabIndex = 23;
            // 
            // labelEchoPACS
            // 
            this.labelEchoPACS.Location = new System.Drawing.Point(507, 64);
            this.labelEchoPACS.Name = "labelEchoPACS";
            this.labelEchoPACS.Size = new System.Drawing.Size(237, 23);
            this.labelEchoPACS.TabIndex = 21;
            // 
            // labelStoreCommit
            // 
            this.labelStoreCommit.Location = new System.Drawing.Point(507, 141);
            this.labelStoreCommit.Name = "labelStoreCommit";
            this.labelStoreCommit.Size = new System.Drawing.Size(170, 32);
            this.labelStoreCommit.TabIndex = 20;
            // 
            // labelStoreImage
            // 
            this.labelStoreImage.Location = new System.Drawing.Point(507, 104);
            this.labelStoreImage.Name = "labelStoreImage";
            this.labelStoreImage.Size = new System.Drawing.Size(237, 23);
            this.labelStoreImage.TabIndex = 19;
            // 
            // buttonStorageCommit
            // 
            this.buttonStorageCommit.Location = new System.Drawing.Point(360, 144);
            this.buttonStorageCommit.Name = "buttonStorageCommit";
            this.buttonStorageCommit.Size = new System.Drawing.Size(136, 23);
            this.buttonStorageCommit.TabIndex = 15;
            this.buttonStorageCommit.Text = "Storage Commitment";
            this.buttonStorageCommit.Click += new System.EventHandler(this.buttonStorageCommit_Click);
            // 
            // labelPACS
            // 
            this.labelPACS.Location = new System.Drawing.Point(392, 178);
            this.labelPACS.Name = "labelPACS";
            this.labelPACS.Size = new System.Drawing.Size(352, 31);
            this.labelPACS.TabIndex = 14;
            // 
            // labelHintPACS
            // 
            this.labelHintPACS.Location = new System.Drawing.Point(362, 178);
            this.labelHintPACS.Name = "labelHintPACS";
            this.labelHintPACS.Size = new System.Drawing.Size(32, 16);
            this.labelHintPACS.TabIndex = 5;
            this.labelHintPACS.Text = "Hint:";
            // 
            // buttonStoreImage
            // 
            this.buttonStoreImage.Location = new System.Drawing.Point(360, 104);
            this.buttonStoreImage.Name = "buttonStoreImage";
            this.buttonStoreImage.Size = new System.Drawing.Size(136, 23);
            this.buttonStoreImage.TabIndex = 4;
            this.buttonStoreImage.Text = "Store Image";
            this.buttonStoreImage.Click += new System.EventHandler(this.buttonStoreImage_Click);
            // 
            // buttonEchoPACS
            // 
            this.buttonEchoPACS.Location = new System.Drawing.Point(360, 64);
            this.buttonEchoPACS.Name = "buttonEchoPACS";
            this.buttonEchoPACS.Size = new System.Drawing.Size(136, 23);
            this.buttonEchoPACS.TabIndex = 3;
            this.buttonEchoPACS.Text = "DICOM Echo";
            this.buttonEchoPACS.Click += new System.EventHandler(this.buttonEchoPACS_Click);
            // 
            // buttonPingPACS
            // 
            this.buttonPingPACS.Location = new System.Drawing.Point(360, 24);
            this.buttonPingPACS.Name = "buttonPingPACS";
            this.buttonPingPACS.Size = new System.Drawing.Size(136, 23);
            this.buttonPingPACS.TabIndex = 1;
            this.buttonPingPACS.Text = "Ping PACS/Workstation";
            this.buttonPingPACS.Click += new System.EventHandler(this.buttonPingPACS_Click);
            // 
            // pictureBoxPACS
            // 
            this.pictureBoxPACS.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxPACS.Image")));
            this.pictureBoxPACS.Location = new System.Drawing.Point(8, 16);
            this.pictureBoxPACS.Name = "pictureBoxPACS";
            this.pictureBoxPACS.Size = new System.Drawing.Size(339, 189);
            this.pictureBoxPACS.TabIndex = 0;
            this.pictureBoxPACS.TabStop = false;
            // 
            // groupBoxRIS
            // 
            this.groupBoxRIS.Controls.Add(this.labelPingRIS);
            this.groupBoxRIS.Controls.Add(this.labelEchoRIS);
            this.groupBoxRIS.Controls.Add(this.labelMPPSMsg);
            this.groupBoxRIS.Controls.Add(this.labelReqWLM);
            this.groupBoxRIS.Controls.Add(this.labelRIS);
            this.groupBoxRIS.Controls.Add(this.labelHintRIS);
            this.groupBoxRIS.Controls.Add(this.buttonMPPSMsg);
            this.groupBoxRIS.Controls.Add(this.buttonReqWL);
            this.groupBoxRIS.Controls.Add(this.buttonEchoRIS);
            this.groupBoxRIS.Controls.Add(this.buttonPingRIS);
            this.groupBoxRIS.Controls.Add(this.pictureBoxRIS);
            this.groupBoxRIS.Location = new System.Drawing.Point(0, 8);
            this.groupBoxRIS.Name = "groupBoxRIS";
            this.groupBoxRIS.Size = new System.Drawing.Size(752, 224);
            this.groupBoxRIS.TabIndex = 0;
            this.groupBoxRIS.TabStop = false;
            this.groupBoxRIS.Text = "RIS System";
            // 
            // labelPingRIS
            // 
            this.labelPingRIS.Location = new System.Drawing.Point(504, 24);
            this.labelPingRIS.Name = "labelPingRIS";
            this.labelPingRIS.Size = new System.Drawing.Size(240, 23);
            this.labelPingRIS.TabIndex = 19;
            // 
            // labelEchoRIS
            // 
            this.labelEchoRIS.Location = new System.Drawing.Point(504, 64);
            this.labelEchoRIS.Name = "labelEchoRIS";
            this.labelEchoRIS.Size = new System.Drawing.Size(240, 23);
            this.labelEchoRIS.TabIndex = 18;
            // 
            // labelMPPSMsg
            // 
            this.labelMPPSMsg.Location = new System.Drawing.Point(504, 144);
            this.labelMPPSMsg.Name = "labelMPPSMsg";
            this.labelMPPSMsg.Size = new System.Drawing.Size(240, 32);
            this.labelMPPSMsg.TabIndex = 17;
            // 
            // labelReqWLM
            // 
            this.labelReqWLM.Location = new System.Drawing.Point(504, 104);
            this.labelReqWLM.Name = "labelReqWLM";
            this.labelReqWLM.Size = new System.Drawing.Size(240, 23);
            this.labelReqWLM.TabIndex = 16;
            // 
            // labelRIS
            // 
            this.labelRIS.Location = new System.Drawing.Point(389, 179);
            this.labelRIS.Name = "labelRIS";
            this.labelRIS.Size = new System.Drawing.Size(355, 39);
            this.labelRIS.TabIndex = 11;
            // 
            // labelHintRIS
            // 
            this.labelHintRIS.Location = new System.Drawing.Point(360, 178);
            this.labelHintRIS.Name = "labelHintRIS";
            this.labelHintRIS.Size = new System.Drawing.Size(36, 16);
            this.labelHintRIS.TabIndex = 6;
            this.labelHintRIS.Text = "Hint:";
            // 
            // buttonMPPSMsg
            // 
            this.buttonMPPSMsg.Location = new System.Drawing.Point(360, 144);
            this.buttonMPPSMsg.Name = "buttonMPPSMsg";
            this.buttonMPPSMsg.Size = new System.Drawing.Size(136, 23);
            this.buttonMPPSMsg.TabIndex = 4;
            this.buttonMPPSMsg.Text = "Send MPPS Progress";
            this.buttonMPPSMsg.Click += new System.EventHandler(this.buttonMPPSMsg_Click);
            // 
            // buttonReqWL
            // 
            this.buttonReqWL.Location = new System.Drawing.Point(360, 104);
            this.buttonReqWL.Name = "buttonReqWL";
            this.buttonReqWL.Size = new System.Drawing.Size(136, 23);
            this.buttonReqWL.TabIndex = 3;
            this.buttonReqWL.Text = "Request Worklist";
            this.buttonReqWL.Click += new System.EventHandler(this.buttonReqWL_Click);
            // 
            // buttonEchoRIS
            // 
            this.buttonEchoRIS.Location = new System.Drawing.Point(360, 64);
            this.buttonEchoRIS.Name = "buttonEchoRIS";
            this.buttonEchoRIS.Size = new System.Drawing.Size(136, 23);
            this.buttonEchoRIS.TabIndex = 2;
            this.buttonEchoRIS.Text = "DICOM Echo";
            this.buttonEchoRIS.Click += new System.EventHandler(this.buttonEchoRIS_Click);
            // 
            // buttonPingRIS
            // 
            this.buttonPingRIS.Location = new System.Drawing.Point(360, 24);
            this.buttonPingRIS.Name = "buttonPingRIS";
            this.buttonPingRIS.Size = new System.Drawing.Size(136, 23);
            this.buttonPingRIS.TabIndex = 1;
            this.buttonPingRIS.Text = "Ping RIS";
            this.buttonPingRIS.Click += new System.EventHandler(this.buttonPingRIS_Click);
            // 
            // pictureBoxRIS
            // 
            this.pictureBoxRIS.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxRIS.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxRIS.Image")));
            this.pictureBoxRIS.Location = new System.Drawing.Point(8, 16);
            this.pictureBoxRIS.Name = "pictureBoxRIS";
            this.pictureBoxRIS.Size = new System.Drawing.Size(339, 188);
            this.pictureBoxRIS.TabIndex = 0;
            this.pictureBoxRIS.TabStop = false;
            // 
            // tabPageConfigSystems
            // 
            this.tabPageConfigSystems.Controls.Add(this.groupBoxConfigMPPS);
            this.tabPageConfigSystems.Controls.Add(this.groupBoxConfigPACS);
            this.tabPageConfigSystems.Controls.Add(this.groupBoxConfigRIS);
            this.tabPageConfigSystems.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfigSystems.Name = "tabPageConfigSystems";
            this.tabPageConfigSystems.Size = new System.Drawing.Size(758, 464);
            this.tabPageConfigSystems.TabIndex = 1;
            this.tabPageConfigSystems.Text = "Configure Remote Systems";
            this.tabPageConfigSystems.UseVisualStyleBackColor = true;
            // 
            // groupBoxConfigMPPS
            // 
            this.groupBoxConfigMPPS.Controls.Add(this.textBoxMPPSPort);
            this.groupBoxConfigMPPS.Controls.Add(this.textBoxMPPSAETitle);
            this.groupBoxConfigMPPS.Controls.Add(this.textBoxMPPSIPAddress);
            this.groupBoxConfigMPPS.Controls.Add(this.label5);
            this.groupBoxConfigMPPS.Controls.Add(this.label6);
            this.groupBoxConfigMPPS.Controls.Add(this.label7);
            this.groupBoxConfigMPPS.Location = new System.Drawing.Point(0, 158);
            this.groupBoxConfigMPPS.Name = "groupBoxConfigMPPS";
            this.groupBoxConfigMPPS.Size = new System.Drawing.Size(752, 144);
            this.groupBoxConfigMPPS.TabIndex = 1;
            this.groupBoxConfigMPPS.TabStop = false;
            this.groupBoxConfigMPPS.Text = "MPPS Manager";
            // 
            // textBoxMPPSPort
            // 
            this.textBoxMPPSPort.BeepOnError = true;
            this.textBoxMPPSPort.Location = new System.Drawing.Point(88, 60);
            this.textBoxMPPSPort.Name = "textBoxMPPSPort";
            this.textBoxMPPSPort.Size = new System.Drawing.Size(38, 20);
            this.textBoxMPPSPort.TabIndex = 6;
            this.textBoxMPPSPort.Text = "106";
            this.textBoxMPPSPort.TextChanged += new System.EventHandler(this.textBoxMPPSPort_TextChanged);
            // 
            // textBoxMPPSAETitle
            // 
            this.textBoxMPPSAETitle.Location = new System.Drawing.Point(88, 104);
            this.textBoxMPPSAETitle.Name = "textBoxMPPSAETitle";
            this.textBoxMPPSAETitle.Size = new System.Drawing.Size(142, 20);
            this.textBoxMPPSAETitle.TabIndex = 2;
            this.textBoxMPPSAETitle.Text = "MPPS";
            this.textBoxMPPSAETitle.TextChanged += new System.EventHandler(this.textBoxMPPSAETitle_TextChanged);
            // 
            // textBoxMPPSIPAddress
            // 
            this.textBoxMPPSIPAddress.Location = new System.Drawing.Point(88, 24);
            this.textBoxMPPSIPAddress.Name = "textBoxMPPSIPAddress";
            this.textBoxMPPSIPAddress.Size = new System.Drawing.Size(142, 20);
            this.textBoxMPPSIPAddress.TabIndex = 0;
            this.textBoxMPPSIPAddress.Text = "localhost";
            this.textBoxMPPSIPAddress.TextChanged += new System.EventHandler(this.textBoxMPPSIPAddress_TextChanged);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(8, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Remote Port:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "AE Title:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 24);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 3;
            this.label7.Text = "IP Address:";
            // 
            // groupBoxConfigPACS
            // 
            this.groupBoxConfigPACS.Controls.Add(this.textBoxPACSCommitAETitle);
            this.groupBoxConfigPACS.Controls.Add(this.textBoxPACSCommitIPAddress);
            this.groupBoxConfigPACS.Controls.Add(this.label9);
            this.groupBoxConfigPACS.Controls.Add(this.label10);
            this.groupBoxConfigPACS.Controls.Add(this.label11);
            this.groupBoxConfigPACS.Controls.Add(this.textBoxPACSAETitle);
            this.groupBoxConfigPACS.Controls.Add(this.textBoxPACSIPAddress);
            this.groupBoxConfigPACS.Controls.Add(this.labelPACSPort);
            this.groupBoxConfigPACS.Controls.Add(this.labelPACSAE);
            this.groupBoxConfigPACS.Controls.Add(this.labelPACSIPAddress);
            this.groupBoxConfigPACS.Controls.Add(this.groupBoxStorePACS);
            this.groupBoxConfigPACS.Controls.Add(this.groupBoxCommitPACS);
            this.groupBoxConfigPACS.Location = new System.Drawing.Point(0, 309);
            this.groupBoxConfigPACS.Name = "groupBoxConfigPACS";
            this.groupBoxConfigPACS.Size = new System.Drawing.Size(752, 145);
            this.groupBoxConfigPACS.TabIndex = 2;
            this.groupBoxConfigPACS.TabStop = false;
            this.groupBoxConfigPACS.Text = "PACS/Workstation Systems";
            // 
            // textBoxPACSCommitAETitle
            // 
            this.textBoxPACSCommitAETitle.Location = new System.Drawing.Point(461, 110);
            this.textBoxPACSCommitAETitle.Name = "textBoxPACSCommitAETitle";
            this.textBoxPACSCommitAETitle.Size = new System.Drawing.Size(137, 20);
            this.textBoxPACSCommitAETitle.TabIndex = 8;
            this.textBoxPACSCommitAETitle.Text = "PACS";
            this.textBoxPACSCommitAETitle.TextChanged += new System.EventHandler(this.textBoxPACSCommitAETitle_TextChanged);
            // 
            // textBoxPACSCommitIPAddress
            // 
            this.textBoxPACSCommitIPAddress.Location = new System.Drawing.Point(461, 30);
            this.textBoxPACSCommitIPAddress.Name = "textBoxPACSCommitIPAddress";
            this.textBoxPACSCommitIPAddress.Size = new System.Drawing.Size(137, 20);
            this.textBoxPACSCommitIPAddress.TabIndex = 6;
            this.textBoxPACSCommitIPAddress.Text = "localhost";
            this.textBoxPACSCommitIPAddress.TextChanged += new System.EventHandler(this.textBoxPACSCommitIPAddress_TextChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(381, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 10;
            this.label9.Text = "Remote Port:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(383, 110);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 11;
            this.label10.Text = "AE Title:";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(382, 30);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 9;
            this.label11.Text = "IP Address:";
            // 
            // textBoxPACSAETitle
            // 
            this.textBoxPACSAETitle.Location = new System.Drawing.Point(88, 110);
            this.textBoxPACSAETitle.Name = "textBoxPACSAETitle";
            this.textBoxPACSAETitle.Size = new System.Drawing.Size(142, 20);
            this.textBoxPACSAETitle.TabIndex = 2;
            this.textBoxPACSAETitle.Text = "PACS";
            this.textBoxPACSAETitle.TextChanged += new System.EventHandler(this.textBoxPACSAETitle_TextChanged);
            // 
            // textBoxPACSIPAddress
            // 
            this.textBoxPACSIPAddress.Location = new System.Drawing.Point(88, 30);
            this.textBoxPACSIPAddress.Name = "textBoxPACSIPAddress";
            this.textBoxPACSIPAddress.Size = new System.Drawing.Size(142, 20);
            this.textBoxPACSIPAddress.TabIndex = 0;
            this.textBoxPACSIPAddress.Text = "localhost";
            this.textBoxPACSIPAddress.TextChanged += new System.EventHandler(this.textBoxPACSIPAddress_TextChanged);
            // 
            // labelPACSPort
            // 
            this.labelPACSPort.Location = new System.Drawing.Point(8, 70);
            this.labelPACSPort.Name = "labelPACSPort";
            this.labelPACSPort.Size = new System.Drawing.Size(72, 16);
            this.labelPACSPort.TabIndex = 4;
            this.labelPACSPort.Text = "Remote Port:";
            // 
            // labelPACSAE
            // 
            this.labelPACSAE.Location = new System.Drawing.Point(8, 110);
            this.labelPACSAE.Name = "labelPACSAE";
            this.labelPACSAE.Size = new System.Drawing.Size(72, 16);
            this.labelPACSAE.TabIndex = 5;
            this.labelPACSAE.Text = "AE Title:";
            // 
            // labelPACSIPAddress
            // 
            this.labelPACSIPAddress.Location = new System.Drawing.Point(8, 30);
            this.labelPACSIPAddress.Name = "labelPACSIPAddress";
            this.labelPACSIPAddress.Size = new System.Drawing.Size(72, 16);
            this.labelPACSIPAddress.TabIndex = 3;
            this.labelPACSIPAddress.Text = "IP Address:";
            // 
            // groupBoxStorePACS
            // 
            this.groupBoxStorePACS.Controls.Add(this.textBoxPACSPort);
            this.groupBoxStorePACS.Location = new System.Drawing.Point(6, 13);
            this.groupBoxStorePACS.Name = "groupBoxStorePACS";
            this.groupBoxStorePACS.Size = new System.Drawing.Size(362, 128);
            this.groupBoxStorePACS.TabIndex = 12;
            this.groupBoxStorePACS.TabStop = false;
            this.groupBoxStorePACS.Text = "Storage Config";
            // 
            // textBoxPACSPort
            // 
            this.textBoxPACSPort.BeepOnError = true;
            this.textBoxPACSPort.Location = new System.Drawing.Point(82, 57);
            this.textBoxPACSPort.Name = "textBoxPACSPort";
            this.textBoxPACSPort.Size = new System.Drawing.Size(38, 20);
            this.textBoxPACSPort.TabIndex = 0;
            this.textBoxPACSPort.Text = "107";
            this.textBoxPACSPort.TextChanged += new System.EventHandler(this.textBoxPACSPort_TextChanged);
            // 
            // groupBoxCommitPACS
            // 
            this.groupBoxCommitPACS.Controls.Add(this.textBoxPACSCommitPort);
            this.groupBoxCommitPACS.Location = new System.Drawing.Point(374, 13);
            this.groupBoxCommitPACS.Name = "groupBoxCommitPACS";
            this.groupBoxCommitPACS.Size = new System.Drawing.Size(372, 128);
            this.groupBoxCommitPACS.TabIndex = 13;
            this.groupBoxCommitPACS.TabStop = false;
            this.groupBoxCommitPACS.Text = "Store Commit Config";
            // 
            // textBoxPACSCommitPort
            // 
            this.textBoxPACSCommitPort.BeepOnError = true;
            this.textBoxPACSCommitPort.Location = new System.Drawing.Point(87, 54);
            this.textBoxPACSCommitPort.Name = "textBoxPACSCommitPort";
            this.textBoxPACSCommitPort.Size = new System.Drawing.Size(41, 20);
            this.textBoxPACSCommitPort.TabIndex = 0;
            this.textBoxPACSCommitPort.Text = "107";
            this.textBoxPACSCommitPort.TextChanged += new System.EventHandler(this.textBoxPACSCommitPort_TextChanged);
            // 
            // groupBoxConfigRIS
            // 
            this.groupBoxConfigRIS.Controls.Add(this.textBoxRISPort);
            this.groupBoxConfigRIS.Controls.Add(this.textBoxRISAETitle);
            this.groupBoxConfigRIS.Controls.Add(this.textBoxRISIPAddress);
            this.groupBoxConfigRIS.Controls.Add(this.labelRISPort);
            this.groupBoxConfigRIS.Controls.Add(this.labelRISAE);
            this.groupBoxConfigRIS.Controls.Add(this.labelRISIPAddress);
            this.groupBoxConfigRIS.Location = new System.Drawing.Point(0, 6);
            this.groupBoxConfigRIS.Name = "groupBoxConfigRIS";
            this.groupBoxConfigRIS.Size = new System.Drawing.Size(752, 144);
            this.groupBoxConfigRIS.TabIndex = 0;
            this.groupBoxConfigRIS.TabStop = false;
            this.groupBoxConfigRIS.Text = "RIS System";
            // 
            // textBoxRISPort
            // 
            this.textBoxRISPort.BeepOnError = true;
            this.textBoxRISPort.Location = new System.Drawing.Point(88, 60);
            this.textBoxRISPort.Name = "textBoxRISPort";
            this.textBoxRISPort.Size = new System.Drawing.Size(38, 20);
            this.textBoxRISPort.TabIndex = 6;
            this.textBoxRISPort.Text = "105";
            this.textBoxRISPort.TextChanged += new System.EventHandler(this.textBoxRISPort_TextChanged);
            // 
            // textBoxRISAETitle
            // 
            this.textBoxRISAETitle.Location = new System.Drawing.Point(88, 104);
            this.textBoxRISAETitle.Name = "textBoxRISAETitle";
            this.textBoxRISAETitle.Size = new System.Drawing.Size(142, 20);
            this.textBoxRISAETitle.TabIndex = 2;
            this.textBoxRISAETitle.Text = "RIS";
            this.textBoxRISAETitle.TextChanged += new System.EventHandler(this.textBoxRISAETitle_TextChanged);
            // 
            // textBoxRISIPAddress
            // 
            this.textBoxRISIPAddress.Location = new System.Drawing.Point(88, 24);
            this.textBoxRISIPAddress.Name = "textBoxRISIPAddress";
            this.textBoxRISIPAddress.Size = new System.Drawing.Size(142, 20);
            this.textBoxRISIPAddress.TabIndex = 0;
            this.textBoxRISIPAddress.Text = "localhost";
            this.textBoxRISIPAddress.TextChanged += new System.EventHandler(this.textBoxRISIPAddress_TextChanged);
            // 
            // labelRISPort
            // 
            this.labelRISPort.Location = new System.Drawing.Point(8, 64);
            this.labelRISPort.Name = "labelRISPort";
            this.labelRISPort.Size = new System.Drawing.Size(72, 16);
            this.labelRISPort.TabIndex = 4;
            this.labelRISPort.Text = "Remote Port:";
            // 
            // labelRISAE
            // 
            this.labelRISAE.Location = new System.Drawing.Point(8, 104);
            this.labelRISAE.Name = "labelRISAE";
            this.labelRISAE.Size = new System.Drawing.Size(72, 16);
            this.labelRISAE.TabIndex = 5;
            this.labelRISAE.Text = "AE Title:";
            // 
            // labelRISIPAddress
            // 
            this.labelRISIPAddress.Location = new System.Drawing.Point(8, 24);
            this.labelRISIPAddress.Name = "labelRISIPAddress";
            this.labelRISIPAddress.Size = new System.Drawing.Size(72, 16);
            this.labelRISIPAddress.TabIndex = 3;
            this.labelRISIPAddress.Text = "IP Address:";
            // 
            // tabPageConfigEmulator
            // 
            this.tabPageConfigEmulator.Controls.Add(this.textBoxPort);
            this.tabPageConfigEmulator.Controls.Add(this.textBoxTimeOut);
            this.tabPageConfigEmulator.Controls.Add(this.label12);
            this.tabPageConfigEmulator.Controls.Add(this.groupBox1);
            this.tabPageConfigEmulator.Controls.Add(this.emulatorIPAddress);
            this.tabPageConfigEmulator.Controls.Add(this.buttonTS);
            this.tabPageConfigEmulator.Controls.Add(this.label8);
            this.tabPageConfigEmulator.Controls.Add(this.textBoxSystemName);
            this.tabPageConfigEmulator.Controls.Add(this.label4);
            this.tabPageConfigEmulator.Controls.Add(this.textBoxImplName);
            this.tabPageConfigEmulator.Controls.Add(this.label2);
            this.tabPageConfigEmulator.Controls.Add(this.textBoxAETitle);
            this.tabPageConfigEmulator.Controls.Add(this.textBoxImplUID);
            this.tabPageConfigEmulator.Controls.Add(this.label1);
            this.tabPageConfigEmulator.Controls.Add(this.labelEmulatorAE);
            this.tabPageConfigEmulator.Controls.Add(this.label3);
            this.tabPageConfigEmulator.Location = new System.Drawing.Point(4, 22);
            this.tabPageConfigEmulator.Name = "tabPageConfigEmulator";
            this.tabPageConfigEmulator.Size = new System.Drawing.Size(758, 464);
            this.tabPageConfigEmulator.TabIndex = 2;
            this.tabPageConfigEmulator.Text = "Configure Emulator";
            this.tabPageConfigEmulator.UseVisualStyleBackColor = true;
            // 
            // textBoxPort
            // 
            this.textBoxPort.BeepOnError = true;
            this.textBoxPort.Location = new System.Drawing.Point(167, 220);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(44, 20);
            this.textBoxPort.TabIndex = 30;
            this.textBoxPort.Text = "104";
            this.textBoxPort.TextChanged += new System.EventHandler(this.textBoxPort_TextChanged);
            // 
            // textBoxTimeOut
            // 
            this.textBoxTimeOut.BeepOnError = true;
            this.textBoxTimeOut.Location = new System.Drawing.Point(296, 396);
            this.textBoxTimeOut.Mask = "00";
            this.textBoxTimeOut.Name = "textBoxTimeOut";
            this.textBoxTimeOut.Size = new System.Drawing.Size(29, 20);
            this.textBoxTimeOut.TabIndex = 29;
            this.textBoxTimeOut.Text = "1";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(18, 399);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(258, 13);
            this.label12.TabIndex = 28;
            this.label12.Text = "Wait time for N-EVENT-REPORT from PACS (in sec):";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonDiff);
            this.groupBox1.Controls.Add(this.radioButtonSingle);
            this.groupBox1.Location = new System.Drawing.Point(11, 261);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(314, 107);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Storage Commit Mode";
            // 
            // radioButtonDiff
            // 
            this.radioButtonDiff.AutoSize = true;
            this.radioButtonDiff.Location = new System.Drawing.Point(26, 71);
            this.radioButtonDiff.Name = "radioButtonDiff";
            this.radioButtonDiff.Size = new System.Drawing.Size(231, 17);
            this.radioButtonDiff.TabIndex = 1;
            this.radioButtonDiff.Text = "In Different Association (Async commitment)";
            this.radioButtonDiff.UseVisualStyleBackColor = true;
            this.radioButtonDiff.CheckedChanged += new System.EventHandler(this.radioButtonDiff_CheckedChanged);
            // 
            // radioButtonSingle
            // 
            this.radioButtonSingle.AutoSize = true;
            this.radioButtonSingle.Checked = true;
            this.radioButtonSingle.Location = new System.Drawing.Point(26, 30);
            this.radioButtonSingle.Name = "radioButtonSingle";
            this.radioButtonSingle.Size = new System.Drawing.Size(215, 17);
            this.radioButtonSingle.TabIndex = 0;
            this.radioButtonSingle.TabStop = true;
            this.radioButtonSingle.Text = "In Single Association (Sync commitment)";
            this.radioButtonSingle.UseVisualStyleBackColor = true;
            this.radioButtonSingle.CheckedChanged += new System.EventHandler(this.radioButtonSingle_CheckedChanged);
            // 
            // emulatorIPAddress
            // 
            this.emulatorIPAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.emulatorIPAddress.FormattingEnabled = true;
            this.emulatorIPAddress.Location = new System.Drawing.Point(168, 176);
            this.emulatorIPAddress.Name = "emulatorIPAddress";
            this.emulatorIPAddress.Size = new System.Drawing.Size(157, 21);
            this.emulatorIPAddress.TabIndex = 24;
            // 
            // buttonTS
            // 
            this.buttonTS.Location = new System.Drawing.Point(401, 220);
            this.buttonTS.Name = "buttonTS";
            this.buttonTS.Size = new System.Drawing.Size(170, 23);
            this.buttonTS.TabIndex = 23;
            this.buttonTS.Text = "Specify TS for Storage Commit";
            this.buttonTS.Visible = false;
            this.buttonTS.Click += new System.EventHandler(this.buttonTS_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 176);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 16);
            this.label8.TabIndex = 10;
            this.label8.Text = "Local IP Address:";
            // 
            // textBoxSystemName
            // 
            this.textBoxSystemName.Location = new System.Drawing.Point(168, 16);
            this.textBoxSystemName.Name = "textBoxSystemName";
            this.textBoxSystemName.Size = new System.Drawing.Size(157, 20);
            this.textBoxSystemName.TabIndex = 0;
            this.textBoxSystemName.Text = "Modality";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(8, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "System Name:";
            // 
            // textBoxImplName
            // 
            this.textBoxImplName.Location = new System.Drawing.Point(168, 136);
            this.textBoxImplName.Name = "textBoxImplName";
            this.textBoxImplName.ReadOnly = true;
            this.textBoxImplName.Size = new System.Drawing.Size(157, 20);
            this.textBoxImplName.TabIndex = 3;
            this.textBoxImplName.Text = "ModalityEmulator";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(8, 136);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(160, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Implementation Version Name:";
            // 
            // textBoxAETitle
            // 
            this.textBoxAETitle.Location = new System.Drawing.Point(168, 56);
            this.textBoxAETitle.Name = "textBoxAETitle";
            this.textBoxAETitle.Size = new System.Drawing.Size(157, 20);
            this.textBoxAETitle.TabIndex = 1;
            this.textBoxAETitle.Text = "MODALITY";
            // 
            // textBoxImplUID
            // 
            this.textBoxImplUID.Location = new System.Drawing.Point(168, 96);
            this.textBoxImplUID.Name = "textBoxImplUID";
            this.textBoxImplUID.ReadOnly = true;
            this.textBoxImplUID.Size = new System.Drawing.Size(157, 20);
            this.textBoxImplUID.TabIndex = 2;
            this.textBoxImplUID.Text = "1.2.826.0.1.3680043.2.1545.6";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "Listen Port:";
            // 
            // labelEmulatorAE
            // 
            this.labelEmulatorAE.Location = new System.Drawing.Point(8, 56);
            this.labelEmulatorAE.Name = "labelEmulatorAE";
            this.labelEmulatorAE.Size = new System.Drawing.Size(72, 16);
            this.labelEmulatorAE.TabIndex = 7;
            this.labelEmulatorAE.Text = "AE Title:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(144, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Implementation Class UID:";
            // 
            // tabPageWLM
            // 
            this.tabPageWLM.Controls.Add(this.dcmEditorWLM);
            this.tabPageWLM.Location = new System.Drawing.Point(4, 22);
            this.tabPageWLM.Name = "tabPageWLM";
            this.tabPageWLM.Size = new System.Drawing.Size(758, 464);
            this.tabPageWLM.TabIndex = 3;
            this.tabPageWLM.Text = "Worklist Query";
            this.tabPageWLM.UseVisualStyleBackColor = true;
            // 
            // dcmEditorWLM
            // 
            this.dcmEditorWLM.AutoScroll = true;
            this.dcmEditorWLM.DCMFile = "";
            this.dcmEditorWLM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmEditorWLM.Location = new System.Drawing.Point(0, 0);
            this.dcmEditorWLM.Name = "dcmEditorWLM";
            this.dcmEditorWLM.Size = new System.Drawing.Size(758, 464);
            this.dcmEditorWLM.TabIndex = 0;
            // 
            // tabPageMPPSCreate
            // 
            this.tabPageMPPSCreate.Controls.Add(this.dcmEditorMPPSCreate);
            this.tabPageMPPSCreate.Location = new System.Drawing.Point(4, 22);
            this.tabPageMPPSCreate.Name = "tabPageMPPSCreate";
            this.tabPageMPPSCreate.Size = new System.Drawing.Size(758, 464);
            this.tabPageMPPSCreate.TabIndex = 4;
            this.tabPageMPPSCreate.Text = "MPPS-Progress";
            this.tabPageMPPSCreate.UseVisualStyleBackColor = true;
            // 
            // dcmEditorMPPSCreate
            // 
            this.dcmEditorMPPSCreate.AutoScroll = true;
            this.dcmEditorMPPSCreate.DCMFile = "";
            this.dcmEditorMPPSCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmEditorMPPSCreate.Location = new System.Drawing.Point(0, 0);
            this.dcmEditorMPPSCreate.Name = "dcmEditorMPPSCreate";
            this.dcmEditorMPPSCreate.Size = new System.Drawing.Size(758, 464);
            this.dcmEditorMPPSCreate.TabIndex = 0;
            // 
            // tabPageMPPSSet
            // 
            this.tabPageMPPSSet.Controls.Add(this.dcmEditorMPPSSet);
            this.tabPageMPPSSet.Location = new System.Drawing.Point(4, 22);
            this.tabPageMPPSSet.Name = "tabPageMPPSSet";
            this.tabPageMPPSSet.Size = new System.Drawing.Size(758, 464);
            this.tabPageMPPSSet.TabIndex = 5;
            this.tabPageMPPSSet.Text = "MPPS-Completed";
            this.tabPageMPPSSet.UseVisualStyleBackColor = true;
            // 
            // dcmEditorMPPSSet
            // 
            this.dcmEditorMPPSSet.AutoScroll = true;
            this.dcmEditorMPPSSet.DCMFile = "";
            this.dcmEditorMPPSSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmEditorMPPSSet.Location = new System.Drawing.Point(0, 0);
            this.dcmEditorMPPSSet.Name = "dcmEditorMPPSSet";
            this.dcmEditorMPPSSet.Size = new System.Drawing.Size(758, 464);
            this.dcmEditorMPPSSet.TabIndex = 0;
            // 
            // tabPageMPPSDiscontinued
            // 
            this.tabPageMPPSDiscontinued.Controls.Add(this.dcmEditorDiscontinued);
            this.tabPageMPPSDiscontinued.Location = new System.Drawing.Point(4, 22);
            this.tabPageMPPSDiscontinued.Name = "tabPageMPPSDiscontinued";
            this.tabPageMPPSDiscontinued.Size = new System.Drawing.Size(758, 464);
            this.tabPageMPPSDiscontinued.TabIndex = 10;
            this.tabPageMPPSDiscontinued.Text = "MPPS-Discontinued";
            this.tabPageMPPSDiscontinued.UseVisualStyleBackColor = true;
            // 
            // dcmEditorDiscontinued
            // 
            this.dcmEditorDiscontinued.AutoScroll = true;
            this.dcmEditorDiscontinued.DCMFile = "";
            this.dcmEditorDiscontinued.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmEditorDiscontinued.Location = new System.Drawing.Point(0, 0);
            this.dcmEditorDiscontinued.Name = "dcmEditorDiscontinued";
            this.dcmEditorDiscontinued.Size = new System.Drawing.Size(758, 464);
            this.dcmEditorDiscontinued.TabIndex = 0;
            // 
            // tabPageImageStorage
            // 
            this.tabPageImageStorage.Controls.Add(this.dcmEditorStorage);
            this.tabPageImageStorage.Location = new System.Drawing.Point(4, 22);
            this.tabPageImageStorage.Name = "tabPageImageStorage";
            this.tabPageImageStorage.Size = new System.Drawing.Size(758, 464);
            this.tabPageImageStorage.TabIndex = 6;
            this.tabPageImageStorage.Text = "Image Storage";
            this.tabPageImageStorage.UseVisualStyleBackColor = true;
            // 
            // dcmEditorStorage
            // 
            this.dcmEditorStorage.AutoScroll = true;
            this.dcmEditorStorage.DCMFile = "";
            this.dcmEditorStorage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmEditorStorage.Location = new System.Drawing.Point(0, 0);
            this.dcmEditorStorage.Name = "dcmEditorStorage";
            this.dcmEditorStorage.Size = new System.Drawing.Size(758, 464);
            this.dcmEditorStorage.TabIndex = 0;
            // 
            // tabPageDummyPatient
            // 
            this.tabPageDummyPatient.Controls.Add(this.dcmEditorDummyPatient);
            this.tabPageDummyPatient.Location = new System.Drawing.Point(4, 22);
            this.tabPageDummyPatient.Name = "tabPageDummyPatient";
            this.tabPageDummyPatient.Size = new System.Drawing.Size(758, 464);
            this.tabPageDummyPatient.TabIndex = 9;
            this.tabPageDummyPatient.Text = "Dummy Patient";
            this.tabPageDummyPatient.UseVisualStyleBackColor = true;
            // 
            // dcmEditorDummyPatient
            // 
            this.dcmEditorDummyPatient.AutoScroll = true;
            this.dcmEditorDummyPatient.DCMFile = "";
            this.dcmEditorDummyPatient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmEditorDummyPatient.Location = new System.Drawing.Point(0, 0);
            this.dcmEditorDummyPatient.Name = "dcmEditorDummyPatient";
            this.dcmEditorDummyPatient.Size = new System.Drawing.Size(758, 464);
            this.dcmEditorDummyPatient.TabIndex = 0;
            // 
            // tabPageActivityLogging
            // 
            this.tabPageActivityLogging.Controls.Add(this.userControlActivityLoggingEmulator);
            this.tabPageActivityLogging.Location = new System.Drawing.Point(4, 22);
            this.tabPageActivityLogging.Name = "tabPageActivityLogging";
            this.tabPageActivityLogging.Size = new System.Drawing.Size(758, 464);
            this.tabPageActivityLogging.TabIndex = 8;
            this.tabPageActivityLogging.Text = "Activity Logging";
            this.tabPageActivityLogging.UseVisualStyleBackColor = true;
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.dvtkWebBrowserEmulator);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Size = new System.Drawing.Size(758, 464);
            this.tabPageResults.TabIndex = 7;
            this.tabPageResults.Text = "Final Result";
            this.tabPageResults.UseVisualStyleBackColor = true;
            // 
            // dvtkWebBrowserEmulator
            // 
            this.dvtkWebBrowserEmulator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvtkWebBrowserEmulator.Location = new System.Drawing.Point(0, 0);
            this.dvtkWebBrowserEmulator.Name = "dvtkWebBrowserEmulator";
            this.dvtkWebBrowserEmulator.Size = new System.Drawing.Size(758, 464);
            this.dvtkWebBrowserEmulator.TabIndex = 0;
            this.dvtkWebBrowserEmulator.XmlStyleSheetFullFileName = "";
            // 
            // toolBarEmulator
            // 
            this.toolBarEmulator.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.toolBarEmulator.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.toolBarButtonConfigEmulator,
            this.toolBarButtonConfigSys,
            this.toolBarButtonResults,
            this.toolBarButtonLog,
            this.toolBarButtonStop,
            this.toolBarButtonSep,
            this.toolBarButtonLeft,
            this.toolBarButtonUp,
            this.toolBarButtonright});
            this.toolBarEmulator.ButtonSize = new System.Drawing.Size(39, 22);
            this.toolBarEmulator.DropDownArrows = true;
            this.toolBarEmulator.ImageList = this.imageListEmulator;
            this.toolBarEmulator.Location = new System.Drawing.Point(0, 0);
            this.toolBarEmulator.Name = "toolBarEmulator";
            this.toolBarEmulator.ShowToolTips = true;
            this.toolBarEmulator.Size = new System.Drawing.Size(766, 28);
            this.toolBarEmulator.TabIndex = 1;
            this.toolBarEmulator.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBarEmulator_ButtonClick);
            // 
            // toolBarButtonConfigEmulator
            // 
            this.toolBarButtonConfigEmulator.ImageIndex = 7;
            this.toolBarButtonConfigEmulator.Name = "toolBarButtonConfigEmulator";
            this.toolBarButtonConfigEmulator.ToolTipText = "Configure Emulator";
            // 
            // toolBarButtonConfigSys
            // 
            this.toolBarButtonConfigSys.ImageIndex = 3;
            this.toolBarButtonConfigSys.Name = "toolBarButtonConfigSys";
            this.toolBarButtonConfigSys.ToolTipText = "Configure Systems";
            // 
            // toolBarButtonResults
            // 
            this.toolBarButtonResults.Enabled = false;
            this.toolBarButtonResults.ImageIndex = 4;
            this.toolBarButtonResults.Name = "toolBarButtonResults";
            this.toolBarButtonResults.ToolTipText = "Display results";
            // 
            // toolBarButtonLog
            // 
            this.toolBarButtonLog.ImageIndex = 5;
            this.toolBarButtonLog.Name = "toolBarButtonLog";
            this.toolBarButtonLog.ToolTipText = "Show logging";
            // 
            // toolBarButtonStop
            // 
            this.toolBarButtonStop.Enabled = false;
            this.toolBarButtonStop.ImageIndex = 6;
            this.toolBarButtonStop.Name = "toolBarButtonStop";
            this.toolBarButtonStop.ToolTipText = "Stop emulator";
            // 
            // toolBarButtonSep
            // 
            this.toolBarButtonSep.Name = "toolBarButtonSep";
            this.toolBarButtonSep.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
            // 
            // toolBarButtonLeft
            // 
            this.toolBarButtonLeft.Enabled = false;
            this.toolBarButtonLeft.ImageIndex = 0;
            this.toolBarButtonLeft.Name = "toolBarButtonLeft";
            this.toolBarButtonLeft.ToolTipText = "Go back";
            // 
            // toolBarButtonUp
            // 
            this.toolBarButtonUp.Enabled = false;
            this.toolBarButtonUp.ImageIndex = 2;
            this.toolBarButtonUp.Name = "toolBarButtonUp";
            this.toolBarButtonUp.ToolTipText = "Go to top";
            // 
            // toolBarButtonright
            // 
            this.toolBarButtonright.Enabled = false;
            this.toolBarButtonright.ImageIndex = 1;
            this.toolBarButtonright.Name = "toolBarButtonright";
            this.toolBarButtonright.ToolTipText = "Go forward";
            // 
            // imageListEmulator
            // 
            this.imageListEmulator.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListEmulator.ImageStream")));
            this.imageListEmulator.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListEmulator.Images.SetKeyName(0, "");
            this.imageListEmulator.Images.SetKeyName(1, "");
            this.imageListEmulator.Images.SetKeyName(2, "");
            this.imageListEmulator.Images.SetKeyName(3, "");
            this.imageListEmulator.Images.SetKeyName(4, "");
            this.imageListEmulator.Images.SetKeyName(5, "");
            this.imageListEmulator.Images.SetKeyName(6, "");
            this.imageListEmulator.Images.SetKeyName(7, "");
            // 
            // userControlActivityLoggingEmulator
            // 
            this.userControlActivityLoggingEmulator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlActivityLoggingEmulator.Interval = 250;
            this.userControlActivityLoggingEmulator.Location = new System.Drawing.Point(0, 0);
            this.userControlActivityLoggingEmulator.Name = "userControlActivityLoggingEmulator";
            this.userControlActivityLoggingEmulator.Size = new System.Drawing.Size(758, 464);
            this.userControlActivityLoggingEmulator.TabIndex = 0;
            // 
            // ModalityEmulator
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(766, 497);
            this.Controls.Add(this.tabControlEmulator);
            this.Controls.Add(this.toolBarEmulator);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenuEmulator;
            this.Name = "ModalityEmulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modality Emulator";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ModalityEmulator_Closing);
            this.tabControlEmulator.ResumeLayout(false);
            this.tabPageControl.ResumeLayout(false);
            this.groupBoxPACS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxPACS)).EndInit();
            this.groupBoxRIS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxRIS)).EndInit();
            this.tabPageConfigSystems.ResumeLayout(false);
            this.groupBoxConfigMPPS.ResumeLayout(false);
            this.groupBoxConfigMPPS.PerformLayout();
            this.groupBoxConfigPACS.ResumeLayout(false);
            this.groupBoxConfigPACS.PerformLayout();
            this.groupBoxStorePACS.ResumeLayout(false);
            this.groupBoxStorePACS.PerformLayout();
            this.groupBoxCommitPACS.ResumeLayout(false);
            this.groupBoxCommitPACS.PerformLayout();
            this.groupBoxConfigRIS.ResumeLayout(false);
            this.groupBoxConfigRIS.PerformLayout();
            this.tabPageConfigEmulator.ResumeLayout(false);
            this.tabPageConfigEmulator.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPageWLM.ResumeLayout(false);
            this.tabPageMPPSCreate.ResumeLayout(false);
            this.tabPageMPPSSet.ResumeLayout(false);
            this.tabPageMPPSDiscontinued.ResumeLayout(false);
            this.tabPageImageStorage.ResumeLayout(false);
            this.tabPageDummyPatient.ResumeLayout(false);
            this.tabPageActivityLogging.ResumeLayout(false);
            this.tabPageResults.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

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

			Application.Run(new ModalityEmulator());

#if !DEBUG
			}
			catch(Exception exception)
			{
				CustomExceptionHandler.ShowThreadExceptionDialog(exception);
			}
#endif
		}

		private void CreateIntegrationProfile()
		{
			wrapper = new AcquisitionModalityWrapper(Application.StartupPath + "\\IheAcquisitionModality.xml");

            wrapper.OnMessageAvailable += new AcquisitionModalityWrapper.MessageAvailableHandler(wrapper_OnMessageAvailable);
            wrapper.OnTransactionAvailable += new AcquisitionModalityWrapper.TransactionAvailableHandler(wrapper_OnTransactionAvailable);

			//Used for debugging
			//Dvtk.IheActors.UserInterfaces.Form form = new Dvtk.IheActors.UserInterfaces.Form();
			//form.Attach(wrapper.ModalityIntegrationProfile);
			userControlActivityLoggingEmulator.Attach(wrapper.ModalityIntegrationProfile);

            if (isUIUpdateReqd)
            {
                //Update UI from the values read from config XML file
                foreach (DicomPeerToPeerConfig peerToPeerConfig in wrapper.ModalityIntegrationProfile.Config.PeerToPeerConfig)
                {
                    //peerToPeerConfig.FromActorName fromActor = new ActorName
                    if ((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) &&
                        (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.DssOrderFiller))
                    {
                        textBoxAETitle.Text = peerToPeerConfig.FromActorAeTitle;
                        textBoxImplUID.Text = peerToPeerConfig.ActorOption2;
                        textBoxImplName.Text = peerToPeerConfig.ActorOption3;
                        textBoxRISAETitle.Text = peerToPeerConfig.ToActorAeTitle;
                        textBoxRISIPAddress.Text = peerToPeerConfig.ToActorIpAddress;
                        textBoxRISPort.Text = peerToPeerConfig.PortNumber.ToString();
                    }
                    
                    if ((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) &&
                        (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.PerformedProcedureStepManager))
                    {
                        textBoxMPPSAETitle.Text = peerToPeerConfig.ToActorAeTitle;
                        textBoxMPPSIPAddress.Text = peerToPeerConfig.ToActorIpAddress;
                        textBoxMPPSPort.Text = peerToPeerConfig.PortNumber.ToString();
                    }

                    if ((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) &&
                        (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.ImageArchive))
                    {
                        textBoxPACSAETitle.Text = peerToPeerConfig.ToActorAeTitle;
                        textBoxPACSIPAddress.Text = peerToPeerConfig.ToActorIpAddress;
                        textBoxPACSPort.Text = peerToPeerConfig.PortNumber.ToString();
                    }

                    if ((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) &&
                        (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.ImageManager))
                    {
                        textBoxPACSCommitAETitle.Text = peerToPeerConfig.ToActorAeTitle;
                        textBoxPACSCommitIPAddress.Text = peerToPeerConfig.ToActorIpAddress;
                        textBoxPACSCommitPort.Text = peerToPeerConfig.PortNumber.ToString();
                    }

                    if (((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.ImageManager) ||
                        (peerToPeerConfig.FromActorName.Type == ActorTypeEnum.ImageArchive)) &&
                        (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.AcquisitionModality))
                    {
                        string localIpAddr = emulatorIPAddress.SelectedItem.ToString();
                        if (localIpAddr != peerToPeerConfig.ToActorIpAddress)
                        {
                            emulatorIPAddress.Items.Add(peerToPeerConfig.ToActorIpAddress);
                        }
                        textBoxPort.Text = peerToPeerConfig.PortNumber.ToString();
                    }
                }
            }
		}

        void wrapper_OnTransactionAvailable(object server, TransactionAvailableEventArgs transactionAvailableEvent)
        {
            if (transactionAvailableEvent.Transaction.Transaction is DicomTransaction)
            {
                DicomTransaction dicomTransaction = (DicomTransaction)transactionAvailableEvent.Transaction.Transaction;
                for (int i = 0; i < dicomTransaction.DicomMessages.Count; i++)
                {
                    DicomMessage dicomMessage = (DicomMessage)dicomTransaction.DicomMessages[i];
                    if (dicomMessage.CommandSet.DimseCommand == DvtkData.Dimse.DimseCommand.CECHORSP)
                    {
                        echoResultFile = transactionAvailableEvent.Transaction.ResultsFilename;
                    }
                }
            }
        }

        void wrapper_OnMessageAvailable(object server, MessageAvailableEventArgs messageAvailableEvent)
        {
        }		

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void menuItemConfigSystem_Click(object sender, System.EventArgs e)
		{
			menuItemConfigSystem.Checked = !menuItemConfigSystem.Checked;
			if(menuItemConfigSystem.Checked)
			{
				tabControlEmulator.Controls.Add(tabPageConfigSystems);
			}
			else
			{
				tabControlEmulator.Controls.Remove(tabPageConfigSystems);
			}
		}

		private void menuItemConfigEmulator_Click(object sender, System.EventArgs e)
		{
			menuItemConfigEmulator.Checked = !menuItemConfigEmulator.Checked;
			if(menuItemConfigEmulator.Checked)
			{
				tabControlEmulator.Controls.Add(tabPageConfigEmulator);
				tabControlEmulator.Controls.Add(tabPageWLM);
				tabControlEmulator.Controls.Add(tabPageMPPSCreate);
                tabControlEmulator.Controls.Add(tabPageMPPSDiscontinued);
				tabControlEmulator.Controls.Add(tabPageMPPSSet);
				tabControlEmulator.Controls.Add(tabPageImageStorage);
				tabControlEmulator.Controls.Add(tabPageDummyPatient);
			}
			else
			{
				tabControlEmulator.Controls.Remove(tabPageConfigEmulator);
				tabControlEmulator.Controls.Remove(tabPageWLM);
				tabControlEmulator.Controls.Remove(tabPageMPPSCreate);
                tabControlEmulator.Controls.Remove(tabPageMPPSDiscontinued);
				tabControlEmulator.Controls.Remove(tabPageMPPSSet);
				tabControlEmulator.Controls.Remove(tabPageImageStorage);
				tabControlEmulator.Controls.Remove(tabPageDummyPatient);
			}
		}

		private void buttonPingRIS_Click(object sender, System.EventArgs e)
		{
            buttonPingRIS.Enabled = false;
            if (textBoxRISIPAddress.Text == null || textBoxRISIPAddress.Text.Length == 0)
            {
                MessageBox.Show("Specify RIS IP Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonPingRIS.Enabled = true;
                return;
            }

            ProcessPingResponse(textBoxRISIPAddress.Text, labelPingRIS);
            buttonPingRIS.Enabled = true;	
		}

		private void buttonPingPACS_Click(object sender, System.EventArgs e)
		{
            buttonPingPACS.Enabled = false;
            if (textBoxPACSIPAddress.Text == null || textBoxPACSIPAddress.Text.Length == 0)
            {
                MessageBox.Show("Specify PACS IP Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonPingPACS.Enabled = true;
                return;
            }

            ProcessPingResponse(textBoxPACSIPAddress.Text, labelPingPACS);
            buttonPingPACS.Enabled = true;	
		}

		private void buttonEchoRIS_Click(object sender, System.EventArgs e)
		{
            //buttonEchoRIS.Enabled = false;
            //labelRIS.Text = "";
            //labelEchoRIS.Text = "";
            this.Invoke(new CleanUp(ClearInformation), new string[] { "Echo RIS" });
            if (!checkRISConfig())
            {
                buttonEchoRIS.Enabled = true;
                return;
            }

			if(ProcessEchoCommand(ActorTypeEnum.DssOrderFiller))
			{
				labelEchoRIS.Text = "DICOM Echo successful.";
			}
			else
			{
				labelEchoRIS.Text = "DICOM Echo failed";
				labelRIS.Text = "User can request dummy patient by pressing 'Request Worklist' button";
                SystemSounds.Beep.Play();
			}
            buttonEchoRIS.Enabled = true;
		}

        private bool checkRISConfig()
        {
            bool ok = true;

            if (textBoxRISAETitle.Text == "")
            {
                MessageBox.Show("Specify RIS AE Title", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            if (textBoxRISIPAddress.Text == "")
            {
                MessageBox.Show("Specify RIS IP Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false; 
            }

            if (textBoxRISPort.Text == "")
            {
                MessageBox.Show("Specify RIS Port", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            return ok;
        }

		private void buttonEchoPACS_Click(object sender, System.EventArgs e)
		{
            //labelPACS.Text = "";
            //labelEchoPACS.Text = "";
            //buttonEchoPACS.Enabled = false;
            this.Invoke(new CleanUp(ClearInformation), new string[] { "Echo PACS" });
            if (!checkPACSConfig())
            {
                buttonEchoPACS.Enabled = true;
                return;
            }

			if(ProcessEchoCommand(ActorTypeEnum.ImageArchive))
				labelEchoPACS.Text = "DICOM Echo successful.";
			else
			{
				labelEchoPACS.Text = "DICOM Echo failed";
				labelPACS.Text = "Check DICOM Association with PACS.";
                SystemSounds.Beep.Play();
			}
            buttonEchoPACS.Enabled = true;
		}

        private bool checkPACSConfig()
        {
            bool ok = true;

            if (textBoxPACSAETitle.Text == "")
            {
                MessageBox.Show("Specify PACS AE Title", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            if (textBoxPACSIPAddress.Text == "")
            {
                MessageBox.Show("Specify PACS IP Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            if (textBoxPACSPort.Text == "")
            {
                MessageBox.Show("Specify PACS Port", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            return ok;
        }

		private void ProcessPingResponse(string hostname, Label label)
		{
            string msg = "";
            label.Text = "";
            PingReply reply = null;
            bool ok = false;
            string ipAddr = "";

            System.Threading.Thread.Sleep(250);

            try
            {
                ipAddr = hostname.Trim();

                Ping pingSender = new Ping();
                reply = pingSender.Send(ipAddr, 4);
            }
            catch (Exception exp)
            {
                msg = string.Format("Error in pinging to {0}: {1}", hostname, exp.Message);                
            }

            if (reply != null)
            {
                switch (reply.Status)
                {
                    case IPStatus.Success:
                        msg = "Ping successful.";
                        ok = true;
                        break;
                    case IPStatus.TimedOut:
                        msg = "Ping Timeout.";
                        break;
                    case IPStatus.IcmpError:
                        msg = "The ICMP echo request failed because of an ICMP protocol error.";
                        break;
                    case IPStatus.BadRoute:
                        msg = "The ICMP echo request failed because there is no valid route between the source and destination computers.";
                        break;
                    case IPStatus.DestinationProhibited:
                        msg = "The ICMP echo request failed because contact with the destination computer is administratively prohibited.";
                        break;
                    case IPStatus.DestinationNetworkUnreachable:
                    case IPStatus.DestinationHostUnreachable:
                    case IPStatus.DestinationPortUnreachable:
                    case IPStatus.DestinationUnreachable:
                        msg = "Destination host Unreachable.";
                        break;
                    case IPStatus.Unknown:
                        msg = "The ICMP echo request failed for an unknown reason.";
                        break;
                }
            }

            if (ok)
                label.Text = msg;
            else
            {
                label.Text = "Ping failed.";
                SystemSounds.Beep.Play();
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
		}

		private bool ProcessEchoCommand(ActorTypeEnum actorType)
		{
			bool isOk = true;
			toolBarButtonLog.Enabled = true;
			toolBarButtonStop.Enabled = true;

            System.Threading.Thread.Sleep(250);

			if(!isCreated)
			{
				CreateIntegrationProfile();
				isCreated = true;
			}

            if(!isInitialized)
			{
                //Apply updated settings
                if (!UpdateConfig())
                    return false;

				wrapper.Initialize();
				isInitialized = true;
				isTerminated = false;
			}
			
			try
			{
                isOk = wrapper.SendVerification(actorType);
			}
			catch(Exception except)
			{
				string msg = string.Format("Error in DICOM Echo from due to {0}.",except.Message);
				isOk = false;
			}

            return isOk;
		}

        private bool UpdateConfig()
		{
            updateCount++;
		    wrapper.ModalityIntegrationProfile.Config.CommonConfig.RootedBaseDirectory = Application.StartupPath;
		    foreach(DicomPeerToPeerConfig peerToPeerConfig in wrapper.ModalityIntegrationProfile.Config.PeerToPeerConfig)
		    {
			    if((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) && 
				    (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.DssOrderFiller))
			    {
				    peerToPeerConfig.FromActorAeTitle = textBoxAETitle.Text;
				    peerToPeerConfig.ToActorAeTitle = textBoxRISAETitle.Text;
				    peerToPeerConfig.ToActorIpAddress = textBoxRISIPAddress.Text;
                    
                    try
                    {
                        peerToPeerConfig.PortNumber = System.UInt16.Parse(textBoxRISPort.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Specify proper port number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

				    peerToPeerConfig.ActorOption2 = textBoxImplUID.Text;
				    peerToPeerConfig.ActorOption3 = textBoxImplName.Text;
				    peerToPeerConfig.SourceDataDirectory = wlmRqDataDirectory;
			    }

			    if((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) && 
				    (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.PerformedProcedureStepManager))
			    {
				    peerToPeerConfig.FromActorAeTitle = textBoxAETitle.Text;
				    peerToPeerConfig.ToActorAeTitle = textBoxMPPSAETitle.Text;
				    peerToPeerConfig.ActorOption2 = textBoxImplUID.Text;
				    peerToPeerConfig.ActorOption3 = textBoxImplName.Text;
				    peerToPeerConfig.ToActorIpAddress = textBoxMPPSIPAddress.Text;

                    try
                    {
                        peerToPeerConfig.PortNumber = System.UInt16.Parse(textBoxMPPSPort.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Specify proper port number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

				    peerToPeerConfig.SourceDataDirectory = mppsDataDirectory;
			    }

                if ((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) &&
                    (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.ImageArchive))
                {
                    peerToPeerConfig.FromActorAeTitle = textBoxAETitle.Text;
                    peerToPeerConfig.ToActorAeTitle = textBoxPACSAETitle.Text;
                    peerToPeerConfig.ActorOption2 = textBoxImplUID.Text;
                    peerToPeerConfig.ActorOption3 = textBoxImplName.Text;
                    peerToPeerConfig.ToActorIpAddress = textBoxPACSIPAddress.Text;

                    try
                    {
                        peerToPeerConfig.PortNumber = System.UInt16.Parse(textBoxPACSPort.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Specify proper port number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }

                    peerToPeerConfig.SourceDataDirectory = storageDataDirectory;
                }

                if ((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.AcquisitionModality) &&
                    (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.ImageManager))
                {
                    peerToPeerConfig.FromActorAeTitle = textBoxAETitle.Text;
                    peerToPeerConfig.ToActorAeTitle = textBoxPACSCommitAETitle.Text;

                    if (isSyncCommit)
                        peerToPeerConfig.ActorOption1 = "DO_STORAGE_COMMITMENT_ON_SINGLE_ASSOCIATION";
                    else
                        peerToPeerConfig.ActorOption1 = "";

                    peerToPeerConfig.ActorOption2 = textBoxImplUID.Text;
                    peerToPeerConfig.ActorOption3 = textBoxImplName.Text;
                    peerToPeerConfig.ToActorIpAddress = textBoxPACSCommitIPAddress.Text;

                    try
                    {
                        peerToPeerConfig.PortNumber = System.UInt16.Parse(textBoxPACSCommitPort.Text);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Specify proper port number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }

			    if(((peerToPeerConfig.FromActorName.Type == ActorTypeEnum.ImageManager) ||
				    (peerToPeerConfig.FromActorName.Type == ActorTypeEnum.ImageArchive)) && 
				    (peerToPeerConfig.ToActorName.Type == ActorTypeEnum.AcquisitionModality))
			    {
                    peerToPeerConfig.ToActorIpAddress = emulatorIPAddress.SelectedItem.ToString();

                    if (textBoxPort.Text == "")
                        MessageBox.Show("Specify Emulator Port", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                    {
                        try
                        {
                            peerToPeerConfig.PortNumber = System.UInt16.Parse(textBoxPort.Text);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Specify proper port number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    if (isSyncCommit)
                        peerToPeerConfig.ActorOption1 = "DO_STORAGE_COMMITMENT_ON_SINGLE_ASSOCIATION";
                    else
                        peerToPeerConfig.ActorOption1 = "";

			    }
		    }

            lastRISIPAddress = textBoxRISIPAddress.Text;
            lastRISAETitle = textBoxRISAETitle.Text;
            lastMPPSIPAddress = textBoxMPPSIPAddress.Text;
            lastMPPSAETitle = textBoxMPPSAETitle.Text;
            lastPACSIPAddress = textBoxPACSIPAddress.Text;
            lastPACSAETitle = textBoxPACSAETitle.Text;
            lastPACSCommitIPAddress = textBoxPACSCommitIPAddress.Text;
            lastPACSCommitAETitle = textBoxPACSCommitAETitle.Text;
            lastPort = textBoxPort.Text;
            lastRISPort = textBoxRISPort.Text;
            lastPACSPort = textBoxPACSPort.Text;
            lastPACSCommitPort = textBoxPACSCommitPort.Text;
            lastMPPSPort = textBoxMPPSPort.Text;

		    //Save the config
		    wrapper.ModalityIntegrationProfile.Config.Save(Application.StartupPath + "\\IheAcquisitionModality.xml");
            updateCount--;

            return true;
		}

		private void buttonReqWL_Click(object sender, System.EventArgs e)
		{
            ///Moved to ClearInformation
            //buttonStorageCommit.Enabled = false;
            ////buttonTS.Enabled = false;
            //buttonMPPSMsg.Enabled = false;
            //toolBarButtonLog.Enabled = true;
            //toolBarButtonStop.Enabled = true;
            ////buttonReqWL.Enabled = false;
            //buttonStoreImage.Enabled = false;
            //labelMPPSMsg.Text = "";
            //labelStoreCommit.Text = "";
            //labelEchoRIS.Text = "";
            //labelPingRIS.Text = "";
            //labelReqWLM.Text = "";

            this.Invoke(new CleanUp(ClearInformation), new string[] { "Req MWL" });
            bool isSendOk = false;			

			//If result tab is present, remove it
			if(tabControlEmulator.Controls.Contains(tabPageResults))
			{
				tabControlEmulator.Controls.Remove(tabPageResults);
			}

			try
			{
                if (!checkRISConfig())
                    return;

                if (!isCreated)
                {
                    CreateIntegrationProfile();
                    isCreated = true;
                }

                if (!isInitialized)
                {
                    //Apply updated settings
                    if (!UpdateConfig())
                        return;

                    wrapper.Initialize();
                    isInitialized = true;
                    isTerminated = false;
                }

				string wlmRqDcmFile = dcmEditorWLM.DCMFile;
				string scheduledProcStepStartDate = System.DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
				if(wlmRqDcmFile != "")
                    isSendOk = wrapper.GetWorklist(wlmRqDcmFile, scheduledProcStepStartDate);

				//isRISConnected = true;
                if (isSendOk)
                {
                    if (wrapper.ModalityActor.ModalityWorklistItems.Count > 0)
                    {
                        labelReqWLM.Text = string.Format("Received {0} worklist items successfully.", wrapper.ModalityActor.ModalityWorklistItems.Count);
                        toolBarButtonResults.Enabled = true;
                        foreach (DicomQueryItem mwlItem in wrapper.ModalityActor.ModalityWorklistItems)
                        {
                            DicomFile dcmFile = new DicomFile();

                            // Save dataset to DCM file
                            dcmFile.DataSet = mwlItem.DicomMessage.DataSet;
                            string mwlRspFileName = string.Format("wlmRsp{0:0000}", mwlItem.Id);

                            // create the sub mwlrsp directory
                            string subMwlRspDirectory = System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
                            string fullPath = wlmRspDataDirectory + subMwlRspDirectory;
                            DirectoryInfo directoryInfo = Directory.CreateDirectory(fullPath);

                            dcmFile.Write(fullPath + "\\" + mwlRspFileName);
                        }
                    }
                    else
                    {
                        labelReqWLM.Text = "No worklist item received.";
                        labelRIS.Text = "Only Dummy patient can be selected.";
                    }                    
                }
                else
                {
                    labelReqWLM.Text = "Fail to send C-FIND-RQ message to RIS.";
                    labelRIS.Text = "Is RIS on the network? Only Dummy patient can be selected.";
                    buttonReqWL.Enabled = true;
                    SystemSounds.Beep.Play();
                }

                // Select patient
                selectPatient();
			}
			catch(Exception except)
			{
				string msg = string.Format("Error: No worklist items received from {0} due to {1}.",textBoxRISAETitle.Text, except.Message);
				labelReqWLM.Text = msg;

				buttonStorageCommit.Enabled = false;
				//buttonTS.Enabled = false;
				buttonStoreImage.Enabled = false;
			}
		}

        private void selectPatient()
        {
            // Pop up MWL rsp dialog
            MWLResponse dlg = new MWLResponse(wrapper.ModalityActor.ModalityWorklistItems);
            DialogResult result = dlg.ShowDialog();

            if (result == DialogResult.Cancel)
            {
                buttonReqWL.Enabled = true;
                toolBarButtonStop.Enabled = true;
                buttonStoreImage.Enabled = false;
                buttonMPPSMsg.Enabled = false;
                toolBarButtonConfigSys.Enabled = true;
            }
            else
            {
                selectedMWLItem = dlg.SelectedPatient;

                if (selectedMWLItem == null)
                {
                    buttonReqWL.Enabled = true;
                    toolBarButtonLog.Enabled = false;
                    toolBarButtonStop.Enabled = false;
                    buttonStoreImage.Enabled = false;
                    buttonMPPSMsg.Enabled = false;
                    toolBarButtonConfigSys.Enabled = true;
                    toolBarButtonResults.Enabled = false;
                }
                else
                {
                    //buttonReqWL.Enabled = false;
                    buttonStoreImage.Enabled = true;
                    buttonMPPSMsg.Enabled = true;
                    buttonMPPSMsg.Text = "Send MPPS Progress";
                }
            }
        }

        private bool checkMPPSConfig()
        {
            bool ok = true;

            if (textBoxMPPSAETitle.Text == "")
            {
                MessageBox.Show("Specify MPPS AE Title", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            if (textBoxMPPSIPAddress.Text == "")
            {
                MessageBox.Show("Specify MPPS IP Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            if (textBoxMPPSPort.Text == "")
            {
                MessageBox.Show("Specify MPPS Port", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            return ok;
        }

		private void buttonMPPSMsg_Click(object sender, System.EventArgs e)
		{
            //Moved to ClearInformation
            //toolBarButtonConfigSys.Enabled = true;
            //buttonReqWL.Enabled = true;
            //labelMPPSMsg.Text = "";

            this.Invoke(new CleanUp(ClearInformation), new string[] { buttonMPPSMsg.Text });
            bool isSendOk = false;

			try
			{
                if (!checkMPPSConfig())
                    return;

                if(buttonMPPSMsg.Text == "Send MPPS Progress")
				{
					string mppsCreateDcmFile = dcmEditorMPPSCreate.DCMFile;
                    if (mppsCreateDcmFile != "")
                    {
                        if (wrapper.SendMppsInProgress(selectedMWLItem, mppsCreateDcmFile))
                        {
                            labelMPPSMsg.Text = "Send MPPS progress message successfully.";
                            buttonMPPSMsg.Text = "Send MPPS discontinued";
                            buttonStoreImage.Enabled = true;
                            isSendOk = true;
                        }
                        else
                        {
                            labelMPPSMsg.Text = "Fail to send N-CREATE-RQ message to MPPS.";
                            labelRIS.Text = "Is MPPS manager on the network?";
                        }
                    }
				}
				else if(buttonMPPSMsg.Text == "Send MPPS completed")
				{
					string mppsSetDcmFile = dcmEditorMPPSSet.DCMFile;
                    if (mppsSetDcmFile != "")
                    {
                        if (wrapper.SendMppsCompleted(mppsSetDcmFile))
                        {
                            labelMPPSMsg.Text = "Send MPPS completed message successfully.";
                            buttonMPPSMsg.Text = "Send MPPS Progress";
                            buttonMPPSMsg.Enabled = false;
                            buttonStorageCommit.Enabled = true;
                            //buttonTS.Enabled = true;
                            isSendOk = true;
                        }
                        else
                        {
                            labelMPPSMsg.Text = "Fail to send N-SET Completed message to MPPS.";
                            labelRIS.Text = "Is MPPS manager on the network?";
                        }
                    }
				}
				else
				{
                    string mppsDiscontinuedDcmFile = dcmEditorDiscontinued.DCMFile;
                    if (mppsDiscontinuedDcmFile != "")
                    {
                        if (wrapper.SendMppsDiscontinued(mppsDiscontinuedDcmFile))
                        {
                            labelMPPSMsg.Text = "Send MPPS discontinued message successfully.";
                            buttonMPPSMsg.Text = "Send MPPS Progress";

                            buttonStorageCommit.Enabled = false;
                            //buttonTS.Enabled = false;
                            toolBarButtonResults.Enabled = true;
                            toolBarButtonStop.Enabled = true;
                            buttonStoreImage.Enabled = false;
                            buttonMPPSMsg.Enabled = false;
                            isSendOk = true;
                            labelRIS.Text = "";
                        }
                        else
                        {
                            labelMPPSMsg.Text = "Fail to send N-SET Discontinued message to MPPS.";
                            labelRIS.Text = "Is MPPS manager on the network?";
                        }
                    }                    
				}
			}
			catch(Exception except)
			{
				string msg = string.Format("Error: No response from {0} due to {1}.",textBoxRISAETitle.Text, except.Message);
				labelMPPSMsg.Text = msg;

				buttonMPPSMsg.Enabled = false;
			}

            if(!isSendOk)
                buttonStoreImage.Enabled = false;
		}

		private void buttonStoreImage_Click(object sender, System.EventArgs e)
		{
            //Moved to ClearInformationFunction
            //buttonReqWL.Enabled = true;
            //buttonStoreImage.Enabled = false;			
            //buttonStorageCommit.Enabled = false;
            ////buttonTS.Enabled = false;
            //toolBarButtonConfigSys.Enabled = true;
            //labelStoreImage.Text = "";
            //labelEchoPACS.Text = "";
            //labelPingPACS.Text = "";
            //labelRIS.Text = "";
            this.Invoke(new CleanUp(ClearInformation), new string[] { "Storage Image" });
			try
			{
                if (!checkPACSConfig())
                    return;

				// Set up worklist item - storage directory mapping
				FileInfo storageDCMFile = new FileInfo(dcmEditorStorage.DCMFile);
				if((storageDCMFile.DirectoryName + "\\") != storageDataDirectory)
				{
					if(wrapper.ModalityActor.MapWorklistItemToStorageDirectory.IsExistingMapping("Default"))
					{
						wrapper.ModalityActor.MapWorklistItemToStorageDirectory.RemoveMapping("Default");					
					}
					wrapper.ModalityActor.MapWorklistItemToStorageDirectory.AddMapping("Default", storageDCMFile.DirectoryName);
				}

				ActorName actor = new ActorName(ActorTypeEnum.ImageArchive, "PACS");
				wrapper.ClearTransferSyntaxProposalForDicomClient(actor);

                if (wrapper.SendImages(selectedMWLItem, true))
                {
                    labelStoreImage.Text = string.Format("Send Images to {0} sucessfully.", textBoxPACSAETitle.Text);

                    buttonMPPSMsg.Enabled = true;
                    //buttonStorageCommit.Enabled = true;
                    buttonMPPSMsg.Text = "Send MPPS completed";                    
                }
                else
                {
                    labelStoreImage.Text = "Fail to send C-STORE-RQ message to PACS.";
                    labelPACS.Text = "Does Communication failure happen with PACS?";
                    buttonStoreImage.Enabled = true;
                    buttonStorageCommit.Enabled = false;
                    //buttonTS.Enabled = false;
                    SystemSounds.Beep.Play();
                }
			}
			catch(Exception except)
			{
				string msg = string.Format("Error: No response from {0} due to {1}.",textBoxPACSAETitle.Text, except.Message);
				labelStoreImage.Text = msg;

				buttonStorageCommit.Enabled = false;
				//buttonTS.Enabled = false;
			}
		}
        delegate void CleanUp(string text);
        private void ClearInformation(string buttonText)
        {
            if (buttonText == "Storage Image")
            {
                buttonReqWL.Enabled = true;
                buttonStoreImage.Enabled = false;
                buttonStorageCommit.Enabled = false;
                //buttonTS.Enabled = false;
                toolBarButtonConfigSys.Enabled = true;
                labelStoreImage.Text = "Sending images...";
                labelEchoPACS.Text = "";
                labelPingPACS.Text = "";
                labelRIS.Text = "";
            }
            else if (buttonText == "Storage Commit")
            {
                buttonStorageCommit.Enabled = false;
                toolBarButtonStop.Enabled = true;
                toolBarButtonConfigSys.Enabled = true;
                buttonReqWL.Enabled = true;
                buttonStoreImage.Enabled = false;
                buttonMPPSMsg.Enabled = false;
                labelStoreCommit.Text = "Asking Storage commitment...";
                labelStoreImage.Text = "";
                labelMPPSMsg.Text = "";

            }
            else if (buttonText == "Echo PACS")
            {
                labelPACS.Text = "";
                labelEchoPACS.Text = "";
                buttonEchoPACS.Enabled = false;
            }
            else if (buttonText == "Echo RIS")
            {
                buttonEchoRIS.Enabled = false;
                labelRIS.Text = "";
                labelEchoRIS.Text = "";
            }
            else if (buttonText == "Send MPPS Progress")
            {
                toolBarButtonConfigSys.Enabled = true;
                buttonReqWL.Enabled = true;
                labelMPPSMsg.Text = "Sending MPPS Progress...";
            }
            else if (buttonText == "Send MPPS completed")
            {
                toolBarButtonConfigSys.Enabled = true;
                buttonReqWL.Enabled = true;
                labelMPPSMsg.Text = "Sending MPPS completed...";
            }
            else if (buttonText == "Send MPPS discontinued")
            {
                toolBarButtonConfigSys.Enabled = true;
                buttonReqWL.Enabled = true;
                labelMPPSMsg.Text = "Sending MPPS discontinued...";
            }
            else if (buttonText == "Req MWL")
            {
                buttonStorageCommit.Enabled = false;
                //buttonTS.Enabled = false;
                buttonMPPSMsg.Enabled = false;
                toolBarButtonLog.Enabled = true;
                toolBarButtonStop.Enabled = true;
                //buttonReqWL.Enabled = false;
                buttonStoreImage.Enabled = false;
                labelMPPSMsg.Text = "";
                labelStoreCommit.Text = "";
                labelEchoRIS.Text = "";
                labelPingRIS.Text = "";
                labelReqWLM.Text = "Requesting Worklist...";
            }
            this.Update();
        }

        private bool checkCommitConfig()
        {
            bool ok = true;

            if (textBoxPACSCommitAETitle.Text == "")
            {
                MessageBox.Show("Specify PACS Commit AE Title", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            if (textBoxPACSCommitIPAddress.Text == "")
            {
                MessageBox.Show("Specify PACS Commit IP Address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            if (textBoxPACSCommitPort.Text == "")
            {
                MessageBox.Show("Specify PACS Commit Port", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ok = false;
            }

            return ok;
        }

		private void buttonStorageCommit_Click(object sender, System.EventArgs e)
		{
            //buttonStorageCommit.Enabled = false;	
            //toolBarButtonStop.Enabled = true;
            //toolBarButtonConfigSys.Enabled = true;
            //buttonReqWL.Enabled = true;
            //buttonStoreImage.Enabled = false;
            //buttonMPPSMsg.Enabled = false;
            //labelStoreCommit.Text = "";
            //labelStoreImage.Text = "";
            //labelMPPSMsg.Text = "";

            this.Invoke(new CleanUp(ClearInformation), new string[] { "Storage Commit" });
            try
			{
                if (!checkCommitConfig())
                    return;

				ActorName actor = new ActorName(ActorTypeEnum.ImageManager, "IMCommit");

                //Clear TS support
                if (isSyncCommit)
                    wrapper.ClearTransferSyntaxProposalForDicomClient(actor);
                else
                    wrapper.ClearTransferSyntaxSupportForDicomServer(actor);

                //Configure selected TS support
				foreach (string ts in selectedTS)
				{
                    if (isSyncCommit)
                        wrapper.AddTransferSyntaxProposalForDicomClient(actor, ts);
                    else
                        wrapper.AddTransferSyntaxSupportForDicomServer(actor, ts);
				}

                int timeout = 1;
                try
                {
                    timeout = int.Parse(textBoxTimeOut.Text);
                }
                catch (Exception)
                {
                    MessageBox.Show("Specify proper timeout.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (wrapper.SendStorageCommitment(timeout))
                {
                    labelStoreCommit.Text = "Send Storage Commitment message successfully.";
                    //buttonMPPSMsg.Enabled = true;
                }
                else
                {
                    labelStoreCommit.Text = "Fail to send Storage Commitment message.";
                    labelPACS.Text = "Check the network configuration?";
                }
			}
			catch(Exception except)
			{
				string msg = string.Format("Error: No response from {0} due to {1}.",textBoxPACSAETitle.Text, except.Message);
				labelStoreCommit.Text = msg;
			}
		}

        private void toolBarEmulator_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if( e.Button == toolBarButtonLeft)
			{
				this.dvtkWebBrowserEmulator.Back();
			}
			else if( e.Button == toolBarButtonright)
			{
				this.dvtkWebBrowserEmulator.Forward();
			}			
			else if( e.Button == toolBarButtonUp)
			{
				this.dvtkWebBrowserEmulator.Navigate(wrapper.FinalResultFile);				
			}
			else if( e.Button == toolBarButtonLog)
			{
				bool isTabAdded = tabControlEmulator.Controls.Contains(tabPageActivityLogging);
				if(!isTabAdded)
					tabControlEmulator.Controls.Add(tabPageActivityLogging);
				else
					tabControlEmulator.Controls.Remove(tabPageActivityLogging);
			}
			else if( e.Button == toolBarButtonResults)
			{
				if(!isTerminated)
				{
					wrapper.Terminate();
					isTerminated = true;
					isInitialized = false;
					isCreated = false;
					toolBarButtonStop.Enabled = false;
					buttonStoreImage.Enabled = false;
					buttonMPPSMsg.Enabled = false;
					buttonStorageCommit.Enabled = false;
					//buttonTS.Enabled = false;

					labelRIS.Text = "";
					labelPACS.Text = "";
					labelReqWLM.Text = "";
					labelMPPSMsg.Text = "";
					labelStoreImage.Text = "";
					labelStoreCommit.Text = "";
				}

				bool isTabAdded = tabControlEmulator.Controls.Contains(tabPageResults);
				if(!isTabAdded)
				{
					toolBarButtonLeft.Enabled = true;
					toolBarButtonUp.Enabled = true;
					toolBarButtonright.Enabled = true;

					tabControlEmulator.Controls.Add(tabPageResults);
					dvtkWebBrowserEmulator.Navigate(wrapper.FinalResultFile);
				}
				else
				{
					toolBarButtonLeft.Enabled = false;
					toolBarButtonUp.Enabled = false;
					toolBarButtonright.Enabled = false;

					tabControlEmulator.Controls.Remove(tabPageResults);
				}
			}
			else if( e.Button == toolBarButtonConfigSys)
			{
				tabControlEmulator.SelectedTab = tabPageConfigSystems;
				menuItemConfigSystem_Click(sender, null);				
			}
			else if( e.Button == toolBarButtonConfigEmulator)
			{
				tabControlEmulator.SelectedTab = tabPageConfigEmulator;
				menuItemConfigEmulator_Click(sender, null);
			}
			else if( e.Button == toolBarButtonStop)
			{
				toolBarButtonResults.Enabled = true;
				toolBarButtonStop.Enabled = false;
				toolBarButtonConfigSys.Enabled = true;
				buttonReqWL.Enabled = true;
				buttonStoreImage.Enabled = false;
				buttonMPPSMsg.Enabled = false;
				buttonStorageCommit.Enabled = false;
				//buttonTS.Enabled = false;

				labelRIS.Text = "";
				labelPACS.Text = "";
				labelReqWLM.Text = "";
				labelMPPSMsg.Text = "";
				labelStoreImage.Text = "";
				labelStoreCommit.Text = "";
				labelEchoPACS.Text = "";
				labelEchoRIS.Text = "";
                labelPingRIS.Text = "";
                labelPingPACS.Text = "";

				if(!isTerminated)
				{
					wrapper.Terminate();
					isTerminated = true;
					isInitialized = false;
					isCreated = false;
                    isUIUpdateReqd = false;

                    //Apply updated settings
                    UpdateConfig();
				}				
			}
			else{}
		}

		private void dvtkWebBrowserEmulator_BackwardFormwardEnabledStateChangeEvent()
		{
			this.toolBarButtonLeft.Enabled = this.dvtkWebBrowserEmulator.IsBackwardEnabled;
			this.toolBarButtonright.Enabled = this.dvtkWebBrowserEmulator.IsForwardEnabled;
		}

		private void ModalityEmulator_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			//Save the current config parameters
			UpdateConfig();
			
			if(isInitialized && (!isTerminated))
				wrapper.Terminate();

			//Save the last DCM files used by the user
			StreamWriter writer = new StreamWriter(userConfigFilePath);
			writer.WriteLine(dcmEditorWLM.DCMFile);
			writer.WriteLine(dcmEditorMPPSCreate.DCMFile);
			writer.WriteLine(dcmEditorStorage.DCMFile);
			writer.WriteLine(dcmEditorMPPSSet.DCMFile);
            writer.WriteLine(dcmEditorDiscontinued.DCMFile);
			writer.Close();

			//Remove all temporary files generated during execution
			cleanUp();
		}

		private void cleanUp()
		{
			ArrayList theFilesToRemove = new ArrayList();
            string resultDir = Application.StartupPath + @"\results";
            DirectoryInfo theDirectoryInfo = new DirectoryInfo(resultDir);
			FileInfo[] thePixFilesInfo;
			FileInfo[] theIdxFilesInfo;

			if (theDirectoryInfo.Exists)
			{
				thePixFilesInfo = theDirectoryInfo.GetFiles("*.pix");
				theIdxFilesInfo = theDirectoryInfo.GetFiles("*.idx");

				foreach (FileInfo theFileInfo in thePixFilesInfo)
				{
					string thePixFileName = theFileInfo.Name;

					theFilesToRemove.Add(thePixFileName);
				}
				foreach (FileInfo theFileInfo in theIdxFilesInfo)
				{
					string theIdxFileName = theFileInfo.Name;

					theFilesToRemove.Add(theIdxFileName);
				}
			}

			//Delete all pix & idx files
			foreach(string theFileName in theFilesToRemove)
			{
                string theFullFileName = Path.Combine(resultDir, theFileName);

				if (File.Exists(theFullFileName))
				{
					try
					{
						File.Delete(theFullFileName);
					}
					catch(Exception exception)
					{
						string theErrorText;

						theErrorText = string.Format("Could not be delete the {0} temporary file.\n due to exception: {1}\n\n", theFullFileName, exception.Message);
					}
				}				
			}
		}

		private void menuItemAbout_Click(object sender, System.EventArgs e)
		{
			AboutForm about = new AboutForm("Modality Emulator");
			about.ShowDialog();
		}

		private void buttonTS_Click(object sender, System.EventArgs e)
		{
			SelectTransferSyntaxesForm  theSelectTransferSyntaxesForm = new SelectTransferSyntaxesForm();

			ArrayList tsList = new ArrayList();
			tsList.Add (DvtkData.Dul.TransferSyntax.Implicit_VR_Little_Endian);
			tsList.Add (DvtkData.Dul.TransferSyntax.Explicit_VR_Big_Endian);
			tsList.Add (DvtkData.Dul.TransferSyntax.Explicit_VR_Little_Endian);

			if(selectedTS.Count != 0)
			{
				foreach (string ts in selectedTS)
				{
					theSelectTransferSyntaxesForm.SupportedTransferSyntaxes.Add(new DvtkData.Dul.TransferSyntax(ts));
				}
			}
			else
			{
				theSelectTransferSyntaxesForm.SupportedTransferSyntaxes.Add(new DvtkData.Dul.TransferSyntax("1.2.840.10008.1.2"));
			}

			theSelectTransferSyntaxesForm.DefaultTransferSyntaxesList = tsList;
			theSelectTransferSyntaxesForm.DisplaySelectAllButton = false;
			theSelectTransferSyntaxesForm.DisplayDeSelectAllButton = false;
			
			theSelectTransferSyntaxesForm.selectSupportedTS();
			theSelectTransferSyntaxesForm.ShowDialog (this);

			if(theSelectTransferSyntaxesForm.SupportedTransferSyntaxes.Count == 0)
			{
				string theWarningText = "No Transfer Syntax is selected, default ILE will be supported.";
				MessageBox.Show(theWarningText, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			else
			{
				selectedTS.Clear();
				
				foreach (DvtkData.Dul.TransferSyntax ts in theSelectTransferSyntaxesForm.SupportedTransferSyntaxes)
				{
					selectedTS.Add(ts.UID);					
				}
			}
        }

        private void radioButtonSingle_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSingle.Checked)
            {
                isSyncCommit = true;
            }
            else
            {
                isSyncCommit = false;
            }
        }

        private void radioButtonDiff_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonDiff.Checked)
            {
                isSyncCommit = false;
            }
            else
            {
                isSyncCommit = true;
            }
        }

        private void textBoxRISIPAddress_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                textBoxRISIPAddress.BeepOnError = true;
                //textBoxRISIPAddress.Mask = "999.999.999.999";

                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxRISIPAddress.Text != lastRISIPAddress)
                    {
                        updateCount++;
                        textBoxRISIPAddress.Text = lastRISIPAddress;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxRISAETitle_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxRISAETitle.Text != lastRISAETitle)
                    {
                        updateCount++;
                        textBoxRISAETitle.Text = lastRISAETitle;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxMPPSIPAddress_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                textBoxMPPSIPAddress.BeepOnError = true;
                //textBoxMPPSIPAddress.Mask = "000.000.000.000";

                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxMPPSIPAddress.Text != lastMPPSIPAddress)
                    {
                        updateCount++;
                        textBoxMPPSIPAddress.Text = lastMPPSIPAddress;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxMPPSAETitle_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxMPPSAETitle.Text != lastMPPSAETitle)
                    {
                        updateCount++;
                        textBoxMPPSAETitle.Text = lastMPPSAETitle;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxPACSIPAddress_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                textBoxPACSIPAddress.BeepOnError = true;
                //textBoxPACSIPAddress.Mask = "000.000.000.000";

                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxPACSIPAddress.Text != lastPACSIPAddress)
                    {
                        updateCount++;
                        textBoxPACSIPAddress.Text = lastPACSIPAddress;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxPACSAETitle_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxPACSAETitle.Text != lastPACSAETitle)
                    {
                        updateCount++;
                        textBoxPACSAETitle.Text = lastPACSAETitle;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxPACSCommitIPAddress_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                textBoxPACSCommitIPAddress.BeepOnError = true;
                //textBoxPACSCommitIPAddress.Mask = "000.000.000.000";

                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxPACSCommitIPAddress.Text != lastPACSCommitIPAddress)
                    {
                        updateCount++;
                        textBoxPACSCommitIPAddress.Text = lastPACSCommitIPAddress;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxPACSCommitAETitle_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxPACSCommitAETitle.Text != lastPACSCommitAETitle)
                    {
                        updateCount++;
                        textBoxPACSCommitAETitle.Text = lastPACSCommitAETitle;
                        updateCount--;
                    }
                }
            }
        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxPort.Text != lastPort)
                    {
                        updateCount++;
                        textBoxPort.Text = lastPort;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxRISPort_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxRISPort.Text != lastRISPort)
                    {
                        updateCount++;
                        textBoxRISPort.Text = lastRISPort;
                        updateCount--;
                    }
                }
            }            
        }

        private void textBoxMPPSPort_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxMPPSPort.Text != lastMPPSPort)
                    {
                        updateCount++;
                        textBoxMPPSPort.Text = lastMPPSPort;
                        updateCount--;
                    }
                }
            }
        }

        private void textBoxPACSPort_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxPACSPort.Text != lastPACSPort)
                    {
                        updateCount++;
                        textBoxPACSPort.Text = lastPACSPort;
                        updateCount--;
                    }
                }
            }
        }

        private void textBoxPACSCommitPort_TextChanged(object sender, EventArgs e)
        {
            // Only react when the user has made changes, not when the UpdateConfig method has been called.
            if (updateCount == 0)
            {
                if (isInitialized)
                {
                    string msg = "Restart the emulator to take effect configuration changes\n by pressing Stop button.";
                    MessageBox.Show(msg, "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (textBoxPACSCommitPort.Text != lastPACSCommitPort)
                    {
                        updateCount++;
                        textBoxPACSCommitPort.Text = lastPACSCommitPort;
                        updateCount--;
                    }
                }
            }
        }        
	}
}
