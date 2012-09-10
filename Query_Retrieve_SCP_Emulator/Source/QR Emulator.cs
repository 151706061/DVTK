// ------------------------------------------------------
// DVTk - The Healthcare Validation Toolkit (www.dvtk.org)
// Copyright � 2009 DVTk
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
using System.IO;
using System.Diagnostics;

using DvtkApplicationLayer;
using DvtkApplicationLayer.UserInterfaces;
using DvtkApplicationLayer.StoredFiles;

using DvtkHighLevelInterface.Common.Threads;
using DvtkHighLevelInterface.Common.UserInterfaces;
using DvtkHighLevelInterface.Dicom.Threads;
using DvtkHighLevelInterface.Dicom.UserInterfaces;
using DvtkHighLevelInterface.InformationModel;
using DvtkHighLevelInterface.Dicom.Files;
using DataSet = DvtkHighLevelInterface.Dicom.Other.DataSet;
using Dvtk.Dicom.InformationEntity.CompositeInfoModel;
using System.Collections.Generic;
using Dvtk.Dicom.InformationEntity;

namespace QR_Emulator
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class QREmulator : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenuEmulator;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.Button buttonSave;
		private System.Windows.Forms.Button buttonForward;
		private System.Windows.Forms.Button buttonBackward;
		private System.Windows.Forms.Button buttonTop;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageQueryRetrieve;
        private DvtkHighLevelInterface.Dicom.UserInterfaces.DicomThreadOptionsUserControl dicomThreadOptionsUserControl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonExploreDicomFiles;
		private System.Windows.Forms.Button buttonShowInformationModel;
		private System.Windows.Forms.TabPage tabPageDCMEditor;
		private DvtkApplicationLayer.UserInterfaces.DCMEditor dcmEditorQREmulator;
		private System.Windows.Forms.TabPage tabPageResults;
        private DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew dvtkWebBrowserResults;
		private System.Windows.Forms.TabPage tabPageActivityLogging;
		private DvtkHighLevelInterface.Common.UserInterfaces.UserControlActivityLogging userControlActivityLogging;
		private System.Windows.Forms.Button buttonTS;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBoxPRInfoModel;
		private System.Windows.Forms.CheckBox checkBoxSRInfoModel;
		private System.Windows.Forms.CheckBox checkBoxPSRInfoModel;
		private System.Windows.Forms.Button buttonBrowse;
		private System.Windows.Forms.TextBox textBoxDataDir;
        private System.Windows.Forms.CheckBox checkBoxSelectDir;
        private MenuItem menuItem5;
        private MenuItem menuItemStoredFilesExploreValidationResults;
        private MenuItem menuItem7;
        private MenuItem menuItemStoredFilesOptions;
        private MenuItem menuItem6;
        private MenuItem menuConfigLoad;
        private MenuItem menuConfigSave;
        private ToolStrip toolStripQRSCPEmulator;
        private ToolStripButton toolBarButtonError;
        private ToolStripButton toolBarButtonWarning;
        private TabPage tabPageMoveDestinations;
        private Panel panel3;
        private AERegistrationControl aeRegistrationControl1;
        private GroupBox groupBox2;
        private CheckBox CS;
        private CheckBox AE;
        private CheckBox SH;
        private CheckBox LO;
        private CheckBox PN;
        private Button button1;
        private Label label2;
        private NumericUpDown socketTimeout;
        private Label label3;
        private IContainer components;

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QREmulator));
            this.mainMenuEmulator = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuConfigLoad = new System.Windows.Forms.MenuItem();
            this.menuConfigSave = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItem5 = new System.Windows.Forms.MenuItem();
            this.menuItemStoredFilesExploreValidationResults = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItemStoredFilesOptions = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStripQRSCPEmulator = new System.Windows.Forms.ToolStrip();
            this.toolBarButtonError = new System.Windows.Forms.ToolStripButton();
            this.toolBarButtonWarning = new System.Windows.Forms.ToolStripButton();
            this.buttonTS = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonForward = new System.Windows.Forms.Button();
            this.buttonBackward = new System.Windows.Forms.Button();
            this.buttonTop = new System.Windows.Forms.Button();
            this.buttonShowInformationModel = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageQueryRetrieve = new System.Windows.Forms.TabPage();
            this.socketTimeout = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.LO = new System.Windows.Forms.CheckBox();
            this.PN = new System.Windows.Forms.CheckBox();
            this.SH = new System.Windows.Forms.CheckBox();
            this.CS = new System.Windows.Forms.CheckBox();
            this.AE = new System.Windows.Forms.CheckBox();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.textBoxDataDir = new System.Windows.Forms.TextBox();
            this.checkBoxSelectDir = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxPSRInfoModel = new System.Windows.Forms.CheckBox();
            this.checkBoxSRInfoModel = new System.Windows.Forms.CheckBox();
            this.checkBoxPRInfoModel = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExploreDicomFiles = new System.Windows.Forms.Button();
            this.tabPageMoveDestinations = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabPageDCMEditor = new System.Windows.Forms.TabPage();
            this.tabPageActivityLogging = new System.Windows.Forms.TabPage();
            this.tabPageResults = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dicomThreadOptionsUserControl = new DvtkHighLevelInterface.Dicom.UserInterfaces.DicomThreadOptionsUserControl();
            this.aeRegistrationControl1 = new DvtkHighLevelInterface.Dicom.UserInterfaces.AERegistrationControl();
            this.dcmEditorQREmulator = new DvtkApplicationLayer.UserInterfaces.DCMEditor();
            this.userControlActivityLogging = new DvtkHighLevelInterface.Common.UserInterfaces.UserControlActivityLogging();
            this.dvtkWebBrowserResults = new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew();
            this.panel1.SuspendLayout();
            this.toolStripQRSCPEmulator.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabPageQueryRetrieve.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.socketTimeout)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPageMoveDestinations.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPageDCMEditor.SuspendLayout();
            this.tabPageActivityLogging.SuspendLayout();
            this.tabPageResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenuEmulator
            // 
            this.mainMenuEmulator.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem5,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem2});
            this.menuItem1.Text = "File";
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuConfigLoad,
            this.menuConfigSave});
            this.menuItem6.Text = "Config File";
            // 
            // menuConfigLoad
            // 
            this.menuConfigLoad.Index = 0;
            this.menuConfigLoad.Text = "Load";
            this.menuConfigLoad.Click += new System.EventHandler(this.menuConfigLoad_Click);
            // 
            // menuConfigSave
            // 
            this.menuConfigSave.Index = 1;
            this.menuConfigSave.Text = "Save";
            this.menuConfigSave.Click += new System.EventHandler(this.menuConfigSave_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "Exit";
            this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem5
            // 
            this.menuItem5.Index = 1;
            this.menuItem5.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemStoredFilesExploreValidationResults,
            this.menuItem7,
            this.menuItemStoredFilesOptions});
            this.menuItem5.Text = "Stored Files";
            // 
            // menuItemStoredFilesExploreValidationResults
            // 
            this.menuItemStoredFilesExploreValidationResults.Index = 0;
            this.menuItemStoredFilesExploreValidationResults.Text = "Explore Validation Results...";
            this.menuItemStoredFilesExploreValidationResults.Click += new System.EventHandler(this.menuItemStoredFilesExploreValidationResults_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "-";
            // 
            // menuItemStoredFilesOptions
            // 
            this.menuItemStoredFilesOptions.Index = 2;
            this.menuItemStoredFilesOptions.Text = "Options...";
            this.menuItemStoredFilesOptions.Click += new System.EventHandler(this.menuItemStoredFilesOptions_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 2;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem4});
            this.menuItem3.Text = "About";
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 0;
            this.menuItem4.Text = "About QR SCP Emulator";
            this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStripQRSCPEmulator);
            this.panel1.Controls.Add(this.buttonTS);
            this.panel1.Controls.Add(this.splitter1);
            this.panel1.Controls.Add(this.buttonStop);
            this.panel1.Controls.Add(this.buttonSave);
            this.panel1.Controls.Add(this.buttonForward);
            this.panel1.Controls.Add(this.buttonBackward);
            this.panel1.Controls.Add(this.buttonTop);
            this.panel1.Controls.Add(this.buttonShowInformationModel);
            this.panel1.Controls.Add(this.buttonStart);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(158, 556);
            this.panel1.TabIndex = 1;
            // 
            // toolStripQRSCPEmulator
            // 
            this.toolStripQRSCPEmulator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolBarButtonError,
            this.toolBarButtonWarning});
            this.toolStripQRSCPEmulator.Location = new System.Drawing.Point(0, 0);
            this.toolStripQRSCPEmulator.Name = "toolStripQRSCPEmulator";
            this.toolStripQRSCPEmulator.Size = new System.Drawing.Size(150, 25);
            this.toolStripQRSCPEmulator.TabIndex = 20;
            this.toolStripQRSCPEmulator.Text = "toolStripQRSCPEmulator";
            // 
            // toolBarButtonError
            // 
            this.toolBarButtonError.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBarButtonError.Enabled = false;
            this.toolBarButtonError.Image = ((System.Drawing.Image)(resources.GetObject("toolBarButtonError.Image")));
            this.toolBarButtonError.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBarButtonError.Name = "toolBarButtonError";
            this.toolBarButtonError.Size = new System.Drawing.Size(23, 22);
            this.toolBarButtonError.Text = "toolStripButton1";
            this.toolBarButtonError.ToolTipText = "Error";
            this.toolBarButtonError.Click += new System.EventHandler(this.toolBarButtonError_Click);
            // 
            // toolBarButtonWarning
            // 
            this.toolBarButtonWarning.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolBarButtonWarning.Enabled = false;
            this.toolBarButtonWarning.Image = ((System.Drawing.Image)(resources.GetObject("toolBarButtonWarning.Image")));
            this.toolBarButtonWarning.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBarButtonWarning.Name = "toolBarButtonWarning";
            this.toolBarButtonWarning.Size = new System.Drawing.Size(23, 22);
            this.toolBarButtonWarning.ToolTipText = "Warning";
            this.toolBarButtonWarning.Click += new System.EventHandler(this.toolBarButtonWarning_Click);
            // 
            // buttonTS
            // 
            this.buttonTS.Location = new System.Drawing.Point(35, 168);
            this.buttonTS.Name = "buttonTS";
            this.buttonTS.Size = new System.Drawing.Size(72, 23);
            this.buttonTS.TabIndex = 2;
            this.buttonTS.Text = "Specify TS";
            this.buttonTS.Click += new System.EventHandler(this.buttonTS_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(150, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitter1.Size = new System.Drawing.Size(8, 556);
            this.splitter1.TabIndex = 19;
            this.splitter1.TabStop = false;
            // 
            // buttonStop
            // 
            this.buttonStop.Location = new System.Drawing.Point(35, 120);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(72, 23);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.Text = "Stop";
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(35, 216);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 3;
            this.buttonSave.Text = "Save";
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // buttonForward
            // 
            this.buttonForward.Image = ((System.Drawing.Image)(resources.GetObject("buttonForward.Image")));
            this.buttonForward.Location = new System.Drawing.Point(71, 335);
            this.buttonForward.Name = "buttonForward";
            this.buttonForward.Size = new System.Drawing.Size(40, 23);
            this.buttonForward.TabIndex = 6;
            this.buttonForward.Click += new System.EventHandler(this.buttonForward_Click);
            // 
            // buttonBackward
            // 
            this.buttonBackward.Image = ((System.Drawing.Image)(resources.GetObject("buttonBackward.Image")));
            this.buttonBackward.Location = new System.Drawing.Point(31, 335);
            this.buttonBackward.Name = "buttonBackward";
            this.buttonBackward.Size = new System.Drawing.Size(40, 23);
            this.buttonBackward.TabIndex = 5;
            this.buttonBackward.Click += new System.EventHandler(this.buttonBackward_Click);
            // 
            // buttonTop
            // 
            this.buttonTop.Image = ((System.Drawing.Image)(resources.GetObject("buttonTop.Image")));
            this.buttonTop.Location = new System.Drawing.Point(47, 311);
            this.buttonTop.Name = "buttonTop";
            this.buttonTop.Size = new System.Drawing.Size(40, 23);
            this.buttonTop.TabIndex = 4;
            this.buttonTop.Click += new System.EventHandler(this.buttonTop_Click);
            // 
            // buttonShowInformationModel
            // 
            this.buttonShowInformationModel.Location = new System.Drawing.Point(4, 263);
            this.buttonShowInformationModel.Name = "buttonShowInformationModel";
            this.buttonShowInformationModel.Size = new System.Drawing.Size(143, 23);
            this.buttonShowInformationModel.TabIndex = 4;
            this.buttonShowInformationModel.Text = "View information model";
            this.buttonShowInformationModel.Click += new System.EventHandler(this.buttonShowInformationModel_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(35, 72);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(72, 23);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "Start";
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(158, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(656, 556);
            this.panel2.TabIndex = 17;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageQueryRetrieve);
            this.tabControl.Controls.Add(this.tabPageMoveDestinations);
            this.tabControl.Controls.Add(this.tabPageDCMEditor);
            this.tabControl.Controls.Add(this.tabPageActivityLogging);
            this.tabControl.Controls.Add(this.tabPageResults);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(656, 556);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabPageQueryRetrieve
            // 
            this.tabPageQueryRetrieve.Controls.Add(this.label3);
            this.tabPageQueryRetrieve.Controls.Add(this.socketTimeout);
            this.tabPageQueryRetrieve.Controls.Add(this.label2);
            this.tabPageQueryRetrieve.Controls.Add(this.button1);
            this.tabPageQueryRetrieve.Controls.Add(this.groupBox2);
            this.tabPageQueryRetrieve.Controls.Add(this.buttonBrowse);
            this.tabPageQueryRetrieve.Controls.Add(this.textBoxDataDir);
            this.tabPageQueryRetrieve.Controls.Add(this.checkBoxSelectDir);
            this.tabPageQueryRetrieve.Controls.Add(this.groupBox1);
            this.tabPageQueryRetrieve.Controls.Add(this.dicomThreadOptionsUserControl);
            this.tabPageQueryRetrieve.Controls.Add(this.label1);
            this.tabPageQueryRetrieve.Controls.Add(this.buttonExploreDicomFiles);
            this.tabPageQueryRetrieve.Location = new System.Drawing.Point(4, 22);
            this.tabPageQueryRetrieve.Name = "tabPageQueryRetrieve";
            this.tabPageQueryRetrieve.Size = new System.Drawing.Size(648, 530);
            this.tabPageQueryRetrieve.TabIndex = 1;
            this.tabPageQueryRetrieve.Text = "Configuration";
            // 
            // socketTimeout
            // 
            this.socketTimeout.Location = new System.Drawing.Point(151, 135);
            this.socketTimeout.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.socketTimeout.Name = "socketTimeout";
            this.socketTimeout.Size = new System.Drawing.Size(97, 20);
            this.socketTimeout.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(23, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 23);
            this.label2.TabIndex = 20;
            this.label2.Text = "Timeout";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(24, 490);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(195, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "Add Attributes to Information model";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.LO);
            this.groupBox2.Controls.Add(this.PN);
            this.groupBox2.Controls.Add(this.SH);
            this.groupBox2.Controls.Add(this.CS);
            this.groupBox2.Controls.Add(this.AE);
            this.groupBox2.Location = new System.Drawing.Point(24, 168);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 74);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Enable Case sensitivite Query";
            // 
            // LO
            // 
            this.LO.AutoSize = true;
            this.LO.Location = new System.Drawing.Point(163, 46);
            this.LO.Name = "LO";
            this.LO.Size = new System.Drawing.Size(101, 17);
            this.LO.TabIndex = 4;
            this.LO.Text = "Long string (LO)";
            this.LO.UseVisualStyleBackColor = true;
            this.LO.CheckedChanged += new System.EventHandler(this.IsChecked);
            // 
            // PN
            // 
            this.PN.AutoSize = true;
            this.PN.Location = new System.Drawing.Point(20, 46);
            this.PN.Name = "PN";
            this.PN.Size = new System.Drawing.Size(112, 17);
            this.PN.TabIndex = 3;
            this.PN.Text = "Person name (PN)";
            this.PN.UseVisualStyleBackColor = true;
            this.PN.CheckedChanged += new System.EventHandler(this.IsChecked);
            // 
            // SH
            // 
            this.SH.AutoSize = true;
            this.SH.Location = new System.Drawing.Point(286, 23);
            this.SH.Name = "SH";
            this.SH.Size = new System.Drawing.Size(105, 17);
            this.SH.TabIndex = 2;
            this.SH.Text = "Short String (SH)";
            this.SH.UseVisualStyleBackColor = true;
            this.SH.CheckedChanged += new System.EventHandler(this.IsChecked);
            // 
            // CS
            // 
            this.CS.AutoSize = true;
            this.CS.Location = new System.Drawing.Point(163, 23);
            this.CS.Name = "CS";
            this.CS.Size = new System.Drawing.Size(107, 17);
            this.CS.TabIndex = 1;
            this.CS.Text = "Code strings (CS)";
            this.CS.UseVisualStyleBackColor = true;
            this.CS.CheckedChanged += new System.EventHandler(this.IsChecked);
            // 
            // AE
            // 
            this.AE.AutoSize = true;
            this.AE.Location = new System.Drawing.Point(20, 23);
            this.AE.Name = "AE";
            this.AE.Size = new System.Drawing.Size(130, 17);
            this.AE.TabIndex = 0;
            this.AE.Text = "Application Entity (AE)";
            this.AE.UseVisualStyleBackColor = true;
            this.AE.CheckedChanged += new System.EventHandler(this.IsChecked);
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(448, 458);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.Size = new System.Drawing.Size(72, 23);
            this.buttonBrowse.TabIndex = 6;
            this.buttonBrowse.Text = "Browse....";
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // textBoxDataDir
            // 
            this.textBoxDataDir.Location = new System.Drawing.Point(24, 458);
            this.textBoxDataDir.Name = "textBoxDataDir";
            this.textBoxDataDir.ReadOnly = true;
            this.textBoxDataDir.Size = new System.Drawing.Size(416, 20);
            this.textBoxDataDir.TabIndex = 5;
            // 
            // checkBoxSelectDir
            // 
            this.checkBoxSelectDir.Location = new System.Drawing.Point(24, 426);
            this.checkBoxSelectDir.Name = "checkBoxSelectDir";
            this.checkBoxSelectDir.Size = new System.Drawing.Size(280, 24);
            this.checkBoxSelectDir.TabIndex = 17;
            this.checkBoxSelectDir.Text = "Select data directory for sending QR responses";
            this.checkBoxSelectDir.CheckedChanged += new System.EventHandler(this.checkBoxSelectDir_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxPSRInfoModel);
            this.groupBox1.Controls.Add(this.checkBoxSRInfoModel);
            this.groupBox1.Controls.Add(this.checkBoxPRInfoModel);
            this.groupBox1.Location = new System.Drawing.Point(24, 251);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 120);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Query Retrieve Information models";
            // 
            // checkBoxPSRInfoModel
            // 
            this.checkBoxPSRInfoModel.Location = new System.Drawing.Point(24, 88);
            this.checkBoxPSRInfoModel.Name = "checkBoxPSRInfoModel";
            this.checkBoxPSRInfoModel.Size = new System.Drawing.Size(224, 24);
            this.checkBoxPSRInfoModel.TabIndex = 2;
            this.checkBoxPSRInfoModel.Text = "Patient Study Root Information Model";
            this.checkBoxPSRInfoModel.CheckedChanged += new System.EventHandler(this.checkBoxPSRInfoModel_CheckedChanged);
            // 
            // checkBoxSRInfoModel
            // 
            this.checkBoxSRInfoModel.Location = new System.Drawing.Point(24, 56);
            this.checkBoxSRInfoModel.Name = "checkBoxSRInfoModel";
            this.checkBoxSRInfoModel.Size = new System.Drawing.Size(184, 24);
            this.checkBoxSRInfoModel.TabIndex = 1;
            this.checkBoxSRInfoModel.Text = "Study Root Information Model";
            this.checkBoxSRInfoModel.CheckedChanged += new System.EventHandler(this.checkBoxSRInfoModel_CheckedChanged);
            // 
            // checkBoxPRInfoModel
            // 
            this.checkBoxPRInfoModel.Location = new System.Drawing.Point(24, 24);
            this.checkBoxPRInfoModel.Name = "checkBoxPRInfoModel";
            this.checkBoxPRInfoModel.Size = new System.Drawing.Size(200, 24);
            this.checkBoxPRInfoModel.TabIndex = 0;
            this.checkBoxPRInfoModel.Text = "Patient Root Information Model";
            this.checkBoxPRInfoModel.CheckedChanged += new System.EventHandler(this.checkBoxPRInfoModel_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(184, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 23);
            this.label1.TabIndex = 12;
            this.label1.Text = "Import DICOM files for emulating QR responses.";
            // 
            // buttonExploreDicomFiles
            // 
            this.buttonExploreDicomFiles.Location = new System.Drawing.Point(24, 385);
            this.buttonExploreDicomFiles.Name = "buttonExploreDicomFiles";
            this.buttonExploreDicomFiles.Size = new System.Drawing.Size(152, 23);
            this.buttonExploreDicomFiles.TabIndex = 3;
            this.buttonExploreDicomFiles.Text = "Import Dicom files...";
            this.buttonExploreDicomFiles.Click += new System.EventHandler(this.buttonExploreDicomFiles_Click);
            // 
            // tabPageMoveDestinations
            // 
            this.tabPageMoveDestinations.Controls.Add(this.panel3);
            this.tabPageMoveDestinations.Location = new System.Drawing.Point(4, 22);
            this.tabPageMoveDestinations.Name = "tabPageMoveDestinations";
            this.tabPageMoveDestinations.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMoveDestinations.Size = new System.Drawing.Size(648, 530);
            this.tabPageMoveDestinations.TabIndex = 4;
            this.tabPageMoveDestinations.Text = "Move Destinations";
            this.tabPageMoveDestinations.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.aeRegistrationControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(642, 524);
            this.panel3.TabIndex = 0;
            // 
            // tabPageDCMEditor
            // 
            this.tabPageDCMEditor.Controls.Add(this.dcmEditorQREmulator);
            this.tabPageDCMEditor.Location = new System.Drawing.Point(4, 22);
            this.tabPageDCMEditor.Name = "tabPageDCMEditor";
            this.tabPageDCMEditor.Size = new System.Drawing.Size(648, 530);
            this.tabPageDCMEditor.TabIndex = 3;
            this.tabPageDCMEditor.Text = "Edit DCM Files";
            this.tabPageDCMEditor.Visible = false;
            // 
            // tabPageActivityLogging
            // 
            this.tabPageActivityLogging.Controls.Add(this.userControlActivityLogging);
            this.tabPageActivityLogging.Location = new System.Drawing.Point(4, 22);
            this.tabPageActivityLogging.Name = "tabPageActivityLogging";
            this.tabPageActivityLogging.Size = new System.Drawing.Size(648, 530);
            this.tabPageActivityLogging.TabIndex = 2;
            this.tabPageActivityLogging.Text = "Activity Logging";
            this.tabPageActivityLogging.Visible = false;
            // 
            // tabPageResults
            // 
            this.tabPageResults.Controls.Add(this.dvtkWebBrowserResults);
            this.tabPageResults.Location = new System.Drawing.Point(4, 22);
            this.tabPageResults.Name = "tabPageResults";
            this.tabPageResults.Size = new System.Drawing.Size(648, 530);
            this.tabPageResults.TabIndex = 0;
            this.tabPageResults.Text = "Results";
            this.tabPageResults.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(250, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 23);
            this.label3.TabIndex = 22;
            this.label3.Text = "Sec     (\'0\' for maximum timeout) ";
            // 
            // dicomThreadOptionsUserControl
            // 
            this.dicomThreadOptionsUserControl.DicomThreadOptions = null;
            this.dicomThreadOptionsUserControl.LocalAeTitle = "";
            this.dicomThreadOptionsUserControl.LocalAeTitleVisible = false;
            this.dicomThreadOptionsUserControl.LocalPort = "";
            this.dicomThreadOptionsUserControl.LocalPortVisible = false;
            this.dicomThreadOptionsUserControl.Location = new System.Drawing.Point(8, 8);
            this.dicomThreadOptionsUserControl.Name = "dicomThreadOptionsUserControl";
            this.dicomThreadOptionsUserControl.RemoteAeTitle = "";
            this.dicomThreadOptionsUserControl.RemoteAeTitleVisible = false;
            this.dicomThreadOptionsUserControl.RemoteIpAddress = "";
            this.dicomThreadOptionsUserControl.RemoteIpAddressVisible = false;
            this.dicomThreadOptionsUserControl.RemotePort = "";
            this.dicomThreadOptionsUserControl.RemotePortVisible = false;
            this.dicomThreadOptionsUserControl.Size = new System.Drawing.Size(432, 126);
            this.dicomThreadOptionsUserControl.TabIndex = 1;
            // 
            // aeRegistrationControl1
            // 
            this.aeRegistrationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aeRegistrationControl1.Location = new System.Drawing.Point(0, 0);
            this.aeRegistrationControl1.Margin = new System.Windows.Forms.Padding(4);
            this.aeRegistrationControl1.Name = "aeRegistrationControl1";
            this.aeRegistrationControl1.Size = new System.Drawing.Size(642, 524);
            this.aeRegistrationControl1.TabIndex = 0;
            // 
            // dcmEditorQREmulator
            // 
            this.dcmEditorQREmulator.AutoScroll = true;
            this.dcmEditorQREmulator.DCMFile = "";
            this.dcmEditorQREmulator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmEditorQREmulator.Location = new System.Drawing.Point(0, 0);
            this.dcmEditorQREmulator.Name = "dcmEditorQREmulator";
            this.dcmEditorQREmulator.Size = new System.Drawing.Size(648, 530);
            this.dcmEditorQREmulator.TabIndex = 0;
            // 
            // userControlActivityLogging
            // 
            this.userControlActivityLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.userControlActivityLogging.Interval = 250;
            this.userControlActivityLogging.Location = new System.Drawing.Point(0, 0);
            this.userControlActivityLogging.Name = "userControlActivityLogging";
            this.userControlActivityLogging.Size = new System.Drawing.Size(648, 530);
            this.userControlActivityLogging.TabIndex = 0;
            // 
            // dvtkWebBrowserResults
            // 
            this.dvtkWebBrowserResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvtkWebBrowserResults.Location = new System.Drawing.Point(0, 0);
            this.dvtkWebBrowserResults.Name = "dvtkWebBrowserResults";
            this.dvtkWebBrowserResults.Size = new System.Drawing.Size(648, 530);
            this.dvtkWebBrowserResults.TabIndex = 0;
            this.dvtkWebBrowserResults.XmlStyleSheetFullFileName = "";
            // 
            // QREmulator
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(814, 556);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenuEmulator;
            this.MinimumSize = new System.Drawing.Size(685, 590);
            this.Name = "QREmulator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QR SCP Emulator";
            this.Load += new System.EventHandler(this.QREmulator_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QREmulator_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStripQRSCPEmulator.ResumeLayout(false);
            this.toolStripQRSCPEmulator.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabPageQueryRetrieve.ResumeLayout(false);
            this.tabPageQueryRetrieve.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.socketTimeout)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPageMoveDestinations.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPageDCMEditor.ResumeLayout(false);
            this.tabPageActivityLogging.ResumeLayout(false);
            this.tabPageResults.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		//
		// - Fields -
		//
        SCP sourceQRScp = null;

		private bool isRunning = false;

        private bool isStopped = true;

		private bool hasUnsavedChanges = false;

		private String rootPath = "";

		public static String dataDirectory = "";

        public static String dataDirectoryForTempFiles = "";

		private String topXmlResults = "";

		private ThreadManager threadManager = null;

		private OverviewThread overviewThread = null;

		private ArrayList selectedTS = new ArrayList();

		private ThreadManager.ThreadsStateChangeEventHandler threadsStateChangeEventHandler = null;

		/// <summary>
		/// Alle configurable items.
		/// </summary>
		private Config config = null;

        private FileGroups fileGroups = null;

        private ValidationResultsFileGroup validationResultsFileGroup = null;

       
		//private HliForm hliForm = null;

		//
		// - Entry point -
		//
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
            // Initialize the Dvtk library
            Dvtk.Setup.Initialize();

            // Checks the version of both the application and the DVTk library.
            // If one or both are a non-official or Alpha version, a warning message box is displayed.
            DvtkApplicationLayer.VersionChecker.CheckVersion();
            DvtkApplicationLayer.DefinitionFilesChecker.CheckVersion("1.0.0", "2.0.0");

			Application.Run(new QREmulator());

            // Terminate the Dvtk library
            Dvtk.Setup.Terminate();

#if !DEBUG
			}
			catch(Exception exception)
			{
				CustomExceptionHandler.ShowThreadExceptionDialog(exception);
			}
#endif
		}

		//
		// - Constructors -
		//
		/// <summary>
		/// Default constructor.
		/// </summary>
		public QREmulator()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
           
            this.dvtkWebBrowserResults.ErrorWarningEnabledStateChangeEvent+=new DvtkWebBrowserNew.ErrorWarningEnabledStateChangeEventHandler(dvtkWebBrowserResults_ErrorWarningEnabledStateChangeEvent);

            if (!Directory.Exists(ConfigurationsDirectory))
            {
                Directory.CreateDirectory(ConfigurationsDirectory);
            }

            this.config = Config.Deserialize(Path.Combine(ConfigurationsDirectory, "Config.xml"));
            Config.ConfigFullFileName = Path.Combine(ConfigurationsDirectory, "Config.xml");
            
			//
			// Initialize browser control.
			//
			rootPath = Application.StartupPath;

			this.dvtkWebBrowserResults.XmlStyleSheetFullFileName = Path.Combine(rootPath, "DVT_RESULTS.xslt");

			dataDirectory = Path.Combine(rootPath, @"Data\QueryRetrieve\");

			//Update settings from the config XML
			if(config.DataDirectoryForEmulation != "")
				dataDirectory = config.DataDirectoryForEmulation;
			else
				config.DataDirectoryForEmulation = dataDirectory;

			checkBoxPRInfoModel.Checked = config.PatientRootInfoModelSupport;
			checkBoxSRInfoModel.Checked = config.StudyRootInfoModelSupport;
			checkBoxPSRInfoModel.Checked = config.PatientStudyRootInfoModelSupport;
            this.AE.Checked = this.config.IsCaseSensitiveAE;
            this.CS.Checked = this.config.IsCaseSensitiveCS;
            this.LO.Checked = this.config.IsCaseSensitiveLO;
            this.PN.Checked = this.config.IsCaseSensitivePN;
            this.SH.Checked = this.config.IsCaseSensitiveSH;
			if(config.TSILESupport)
				selectedTS.Add("1.2.840.10008.1.2");
			if(config.TSELESupport)
				selectedTS.Add("1.2.840.10008.1.2.1");
			if(config.TSEBESupport)
				selectedTS.Add("1.2.840.10008.1.2.2");

			//
			// Set the .Net thread name for debugging purposes.
			//
			System.Threading.Thread.CurrentThread.Name = "QR SCP Emulator";

            //
            // Stored files options.
            //
            this.fileGroups = new FileGroups("QR SCP Emulator");

            this.validationResultsFileGroup = new ValidationResultsFileGroup();
            this.validationResultsFileGroup.DefaultFolder = "Results";
            this.fileGroups.Add(validationResultsFileGroup);

            this.fileGroups.CreateDirectories();

            this.fileGroups.CheckIsConfigured("\"Stored Files\\Options...\" menu item");

			//
			// The dvtkThreadManager.
			//
			ThreadManager threadManager = new ThreadManager();
            
			//
			// Construct the source QR SCP.
			//
            this.sourceQRScp = new SCP();
			this.sourceQRScp.Initialize(threadManager);
            this.sourceQRScp.Options.LoadFromFile(Path.Combine(rootPath, "QR_SCP.ses"));
            this.sourceQRScp.Options.Identifier = "QR_SCP_Emulator";
			this.sourceQRScp.Options.AttachChildsToUserInterfaces = true;
			this.sourceQRScp.Options.StorageMode = Dvtk.Sessions.StorageMode.NoStorage;
			this.sourceQRScp.Options.LogThreadStartingAndStoppingInParent = false;
			this.sourceQRScp.Options.LogWaitingForCompletionChildThreads = false;
            this.sourceQRScp.Options.ResultsDirectory = validationResultsFileGroup.Directory;
            this.sourceQRScp.Options.DataDirectory = validationResultsFileGroup.Directory;
            this.socketTimeout.Value = this.sourceQRScp.Options.SocketTimeout;
            //
            //Initialize move destinations
            //
            aeRegistrationControl1.Initialize(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DVtk\\QR SCP Emulator\\Configurations\\MoveDestinations.xml",
                sourceQRScp.Options);
            aeRegistrationControl1.OnShowEchoResults += new AERegistrationControl.ShowEchoResults(ShowMoveDestinationResults);
            
			//
			// Initialize the DicomThreadOptionsControls
			//
			this.dicomThreadOptionsUserControl.DicomThreadOptions = this.sourceQRScp.Options;
			this.dicomThreadOptionsUserControl.LocalAeTitleVisible = true;
			this.dicomThreadOptionsUserControl.LocalPortVisible = true;
            this.dicomThreadOptionsUserControl.LocalAETitleName = "Local AE title\n(QR SCP&& Store SCU)";
			this.dicomThreadOptionsUserControl.RemoteAeTitleLabel = "QR SCU AE Title:";
			this.dicomThreadOptionsUserControl.RemotePortLabel = "Move destination Port:";
			this.dicomThreadOptionsUserControl.RemoteIpAddressLabel = "Move destination IP Addr:";
			this.dicomThreadOptionsUserControl.RemoteAeTitleVisible = true;
			this.dicomThreadOptionsUserControl.RemoteIpAddressVisible = false;
			this.dicomThreadOptionsUserControl.RemotePortVisible = false;
			this.dicomThreadOptionsUserControl.OptionChangedEvent+= new DicomThreadOptionsUserControl.OptionChangedEventHandler(this.HandleOptionChanged);
			this.dicomThreadOptionsUserControl.UpdateUserControl();

			// Save the config so next time no attempt will be made to again try to load the same settings
			this.config.Serialize();

			//
			// Set the Backward/forward button handler.
			//
            this.dvtkWebBrowserResults.BackwardFormwardEnabledStateChangeEvent += new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew.BackwardFormwardEnabledStateChangeEventHandler(BackwardFormwardEnabledStateChangeEvent); 
			UpdateButtons();

			this.threadsStateChangeEventHandler = new ThreadManager.ThreadsStateChangeEventHandler(this.HandleThreadsStateChangeEvent);

			// Load definition and DCM files
            dcmEditorQREmulator.DCMFileDataDirectory = validationResultsFileGroup.Directory;
            dcmEditorQREmulator.DefFile = sourceQRScp.Options.DvtkScriptSession.DefinitionManagement.DefinitionFileRootDirectory + "PatientRootQueryRetrieve-FIND.def";
			dcmEditorQREmulator.DCMFile = rootPath + @"\Data\QueryRetrieve\d1I00001.dcm";
            
			buttonBrowse.Visible = false;
			textBoxDataDir.Visible = false;

            dataDirectoryForTempFiles = validationResultsFileGroup.Directory;

            this.WindowState = FormWindowState.Maximized;
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

		//
		// - Methods -
		//
		/// <summary>
		/// Is called when the enable state of the back and forward buttons should change.
		/// </summary>
		private void BackwardFormwardEnabledStateChangeEvent()
		{
			UpdateButtons();
		}

		/// <summary>
		/// Go backward in the web browser.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		private void buttonBackward_Click(object sender, System.EventArgs e)
		{
			this.dvtkWebBrowserResults.Back();
		}

		/// <summary>
		/// Select DCM files to import in emulator.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		private void buttonExploreDicomFiles_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog openMediaFileDialog = new OpenFileDialog();
			openMediaFileDialog.Filter = "DICOM media files (*.dcm)|*.dcm|All files (*.*)|*.*";
			openMediaFileDialog.Multiselect = true;
			openMediaFileDialog.Title = "Select DICOM files to import";
			openMediaFileDialog.InitialDirectory = dataDirectory;
				
			if (openMediaFileDialog.ShowDialog (this) == DialogResult.OK) 
			{
				string[] files = openMediaFileDialog.FileNames;
				foreach(string file in files) 
				{
					FileInfo dcmFile = new FileInfo(file);
					string destFileName = Path.Combine(dataDirectory, dcmFile.Name);
                    try
                    {
                        dcmFile.CopyTo(destFileName, true);
                    }
                    catch (Exception exception)
                    {
                        string theErrorText = string.Format("Illegal operation.\n {1}\n\n", destFileName, exception.Message);
                        MessageBox.Show(theErrorText, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
				}
			}					
		}

		/// <summary>
		/// Go forward in the web browser.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		private void buttonForward_Click(object sender, System.EventArgs e)
		{
			this.dvtkWebBrowserResults.Forward();
		}

		/// <summary>
		/// Show the information model that is derived from the DCM files in the data directory.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		private void buttonShowInformationModel_Click(object sender, System.EventArgs e)
		{
            QueryRetrieveInformationModels queryRetrieveInformationModels = null;

			String informationModelsInformation = "";
            List<BaseCompositeInformationEntity> patientList=null, studyList=null, pateintStudyList=null;
			if(checkBoxPRInfoModel.Checked)
			{
                queryRetrieveInformationModels = CreateQueryRetrieveInformationModels(false, true, true, false, false, this.sourceQRScp);
                patientList = queryRetrieveInformationModels.PatientRoot.GetCompositeDataModel();
			}

			if(checkBoxSRInfoModel.Checked)
			{
                queryRetrieveInformationModels = CreateQueryRetrieveInformationModels(false, true, false, true, false, this.sourceQRScp);

                studyList = queryRetrieveInformationModels.StudyRoot.GetCompositeDataModel();
			}

			if(checkBoxPSRInfoModel.Checked)
			{
                queryRetrieveInformationModels = CreateQueryRetrieveInformationModels(false, true, false, false, true, this.sourceQRScp);

                pateintStudyList = queryRetrieveInformationModels.PatientStudyOnly.GetCompositeDataModel();
			}

            FormInformationModel formInformationModel = new FormInformationModel();
            formInformationModel.informationModelControl1.SetInfoModel(checkBoxPRInfoModel.Checked, checkBoxSRInfoModel.Checked, checkBoxPSRInfoModel.Checked);
            formInformationModel.informationModelControl1.LoadData(patientList, studyList, pateintStudyList);
			formInformationModel.ShowDialog();						
		}

		/// <summary>
		/// Start the emulator.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		private void buttonStart_Click(object sender, System.EventArgs e)
		{
			//
			// Make the Activity Logging Tab the onky Tab visible and clean it.
			//
			this.tabControl.Controls.Clear();
			this.tabControl.Controls.Add(this.tabPageActivityLogging);
			this.userControlActivityLogging.Clear();

			if((checkBoxPRInfoModel.Checked == false) &&
				(checkBoxSRInfoModel.Checked == false) &&
				(checkBoxPSRInfoModel.Checked == false))
			{
				MessageBox.Show("Pl select atleast one QR information model.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			//
			// Set the correct settings for the DicomThread.
			//
			String resultsFileBaseName = "QR_SCP_" + System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture);
            BaseInformationEntity.SetCaseSentive(config.IsCaseSensitiveAE, config.IsCaseSensitiveCS, config.IsCaseSensitivePN, config.IsCaseSensitiveSH, config.IsCaseSensitiveLO);
			this.threadManager = new ThreadManager();
			this.threadManager.ThreadsStateChangeEvent += this.threadsStateChangeEventHandler;
            if (socketTimeout.Value == 0)
                this.sourceQRScp.Options.SocketTimeout = 65535;
            else
                this.sourceQRScp.Options.SocketTimeout = (ushort)socketTimeout.Value;
            this.overviewThread = new OverviewThread(this.sourceQRScp, selectedTS, checkBoxPRInfoModel.Checked, checkBoxSRInfoModel.Checked, checkBoxPSRInfoModel.Checked,aeRegistrationControl1.RegisteredPeers);
			this.overviewThread.Initialize(threadManager);
            this.overviewThread.Options.ResultsDirectory = validationResultsFileGroup.Directory;
			this.overviewThread.Options.Identifier = resultsFileBaseName;
			this.overviewThread.Options.AttachChildsToUserInterfaces = true;
			this.overviewThread.Options.LogThreadStartingAndStoppingInParent = false;
			this.overviewThread.Options.LogWaitingForCompletionChildThreads = false;
			this.userControlActivityLogging.Attach(overviewThread);

            this.userControlActivityLogging.Attach(this.sourceQRScp);

			//
			// Attach the HliForm to the emulator.
			//
			// this.hliForm = new HliForm();

			// hliForm.Attach(overviewThread);

			//
			// Start the DicomThread.
			//
			this.overviewThread.Start();

			this.isRunning = true;
            this.isStopped = false;
			UpdateButtons();
		}

		/// <summary>
		/// Navigate to the main results.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
		private void buttonTop_Click(object sender, System.EventArgs e)
		{
			this.dvtkWebBrowserResults.Navigate(this.topXmlResults);
		}

        /// <summary>
        /// Create the QR information model on the fly
        /// </summary>
        /// <param name="randomizeFirst"></param>
        /// <param name="isCreationForDisplay"></param>
        /// <param name="patientRoot"></param>
        /// <param name="studyRoot"></param>
        /// <param name="patientStudyRoot"></param>
        /// <param name="dicomThread"></param>
        /// <returns></returns>
        public static QueryRetrieveInformationModels CreateQueryRetrieveInformationModels(bool randomizeFirst, bool isCreationForDisplay, bool patientRoot, bool studyRoot, bool patientStudyRoot, DicomThread dicomThread)
        {
            QueryRetrieveInformationModels queryRetrieveInformationModels = new QueryRetrieveInformationModels();

            //Specify directory for temp DCM files
            queryRetrieveInformationModels.DataDirectory = dataDirectoryForTempFiles;

            DirectoryInfo directoryInfo = new DirectoryInfo(dataDirectory);

            FileInfo[] fileInfos = directoryInfo.GetFiles();
            
            foreach (FileInfo fileInfo in fileInfos)
            {
                try
                {
                    DvtkHighLevelInterface.Dicom.Files.DicomFile dicomFile = new DvtkHighLevelInterface.Dicom.Files.DicomFile();

                    if (isCreationForDisplay)
                       dicomFile.DataSet.StoreOBOFOWValuesWhenReading = false;
                   
                    dicomFile.Read(fileInfo.FullName, dicomThread);
                    
                    if (randomizeFirst)
                        dicomFile.DataSet.Randomize("@");
                    
                    //queryRetrieveInformationModels.Add(dataSet, true);
                    
                    if (patientRoot)
                    queryRetrieveInformationModels.PatientRoot.AddToInformationModel(dicomFile, true);

                    if (studyRoot)
                        queryRetrieveInformationModels.StudyRoot.AddToInformationModel(dicomFile, true);

                    if (patientStudyRoot)
                        queryRetrieveInformationModels.PatientStudyOnly.AddToInformationModel(dicomFile, true);
                }
                catch (Exception)
                {
                    string theErrorText = string.Format("Invalid DICOM File - {0} will be skiped from QR information model.", fileInfo.FullName);
                    dicomThread.WriteInformation(theErrorText);
                }
            }

            return (queryRetrieveInformationModels);
        }

		/// <summary>
		/// Handle the event that an option has changed.
		/// This event is received from dicomThreadOptionsUserControl.
		/// </summary>
		private void HandleOptionChanged()
		{
			this.hasUnsavedChanges = true;
			UpdateButtons();
		}

		/// <summary>
		/// Called when the close cross is pressed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The event arguments.</param>
        private void QREmulator_FormClosing(object sender, System.EventArgs e)
		{
			if ((this.isRunning)|| (!isStopped))
                this.threadManager.Stop();
			
			Cleanup();

            fileGroups.RemoveFiles();
		}

		/// <summary>
		/// Cleanup all temp pix files
		/// </summary>
		private void Cleanup()
		{
			//Remove all temporary files generated during tool execution
			ArrayList theFilesToRemove = new ArrayList();
            DirectoryInfo theDirectoryInfo = new DirectoryInfo(validationResultsFileGroup.Directory);
            FileInfo[] thePixFilesInfo;
            FileInfo[] theIdxFilesInfo;
            FileInfo[] thedcmFilesInfo;

            if (theDirectoryInfo.Exists)
            {
                thePixFilesInfo = theDirectoryInfo.GetFiles("*.pix");
                theIdxFilesInfo = theDirectoryInfo.GetFiles("*.idx");
                thedcmFilesInfo = theDirectoryInfo.GetFiles("DVTK_IOM_TMP*.DCM");

                foreach (FileInfo theFileInfo in thePixFilesInfo)
                {
                    string thePixFileName = theFileInfo.FullName;

                    theFilesToRemove.Add(thePixFileName);
                }

                foreach (FileInfo theFileInfo in theIdxFilesInfo)
                {
                    string theIdxFileName = theFileInfo.FullName;

                    theFilesToRemove.Add(theIdxFileName);
                }

                foreach (FileInfo theFileInfo in thedcmFilesInfo)
                {
                    string thedcmFileName = theFileInfo.FullName;

                    theFilesToRemove.Add(thedcmFileName);
                }                
            }

			//Delete all pix, dcm & idx files
			foreach(string theFileName in theFilesToRemove)
			{
				if (File.Exists(theFileName))
				{
					try
					{
						File.Delete(theFileName);
					}
					catch(Exception exception)
					{
						string theErrorText = string.Format("Could not be delete the {0} temporary file.\n due to exception: {1}\n\n", theFileName, exception.Message);
						MessageBox.Show(theErrorText, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}				
			}
		}

		/// <summary>
		/// Update the buttons.
		/// </summary>
		private void UpdateButtons()
		{
			if (this.isRunning)
			{
				this.buttonStart.Enabled = false;
				this.buttonStop.Enabled = true;
				this.buttonSave.Enabled = false;

				this.buttonTop.Enabled = false;
				this.buttonBackward.Enabled = false;
				this.buttonForward.Enabled = false;
			}
			else
			{
				this.buttonStart.Enabled = true;
				this.buttonStop.Enabled = false;
				this.buttonSave.Enabled = this.hasUnsavedChanges;

				this.buttonTop.Enabled = (this.topXmlResults.Length > 0);
				this.buttonBackward.Enabled = this.dvtkWebBrowserResults.IsBackwardEnabled;
				this.buttonForward.Enabled = this.dvtkWebBrowserResults.IsForwardEnabled;
			}
		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
            sourceQRScp.Options.SaveToFile(Path.Combine(rootPath, "QR_SCP.ses"));
			this.hasUnsavedChanges = false;
			UpdateButtons();
		}

		private void buttonStop_Click(object sender, System.EventArgs e)
		{
			this.buttonStop.Enabled = false;
            this.isStopped = true;
			Cursor.Current = Cursors.WaitCursor;
			this.threadManager.Stop();
		}

       

        private void dvtkWebBrowserResults_ErrorWarningEnabledStateChangeEvent()
        {
            toolBarButtonError.Enabled = this.dvtkWebBrowserResults.ContainsErrors;
            toolBarButtonWarning.Enabled = this.dvtkWebBrowserResults.ContainsWarnings;
        }

		/// <summary>
		/// Handles a thread state change event of a Thread contained in a ThreadManager.
		/// Takes care that when all threads are stopped, the HandleExecutionCompleted method is called.
		/// </summary>
		/// <param name="thread">The thread which state changes.</param>
		/// <param name="oldThreadState">Old state of the thread.</param>
		/// <param name="newThreadState">New state of the thread.</param>
		/// <param name="numberOfUnStarted">Number of unstarted threads of the ThreadManager.</param>
		/// <param name="numberOfRunning">Number of running threads of the ThreadManager.</param>
		/// <param name="numberOfStopping">Number of stopping threads of the ThreadManager.</param>
		/// <param name="numberOfStopped">Number of stopped threads of the ThreadManager.</param>
		private void HandleThreadsStateChangeEvent(Thread thread, DvtkHighLevelInterface.Common.Threads.ThreadState oldThreadState, DvtkHighLevelInterface.Common.Threads.ThreadState newThreadState, int numberOfUnStarted, int numberOfRunning, int numberOfStopping, int numberOfStopped)
		{
			if ((numberOfRunning == 0) && (numberOfStopping == 0) && (numberOfStopped > 0))
			{
                if ((this.InvokeRequired) && (this.IsHandleCreated))
				    Invoke(new ExecutionCompletedHandler(this.HandleExecutionCompleted));
			}
		}


		private delegate void ExecutionCompletedHandler();

        private void ShowMoveDestinationResults(string filename,bool success)
        {
            this.dvtkWebBrowserResults.Navigate(filename);
            if(!success)
                this.tabControl.SelectedTab = this.tabPageResults;
        }

		private void HandleExecutionCompleted()
		{
			//
			// Make the other Tabs visible again.
			//
			this.tabControl.Controls.Clear();
			this.tabControl.Controls.Add(this.tabPageQueryRetrieve);
            this.tabControl.Controls.Add(this.tabPageMoveDestinations);
			this.tabControl.Controls.Add(this.tabPageDCMEditor);
			this.tabControl.Controls.Add(this.tabPageActivityLogging);
			this.tabControl.Controls.Add(this.tabPageResults);

			Cursor.Current = Cursors.Default;

			//
			// Show the new results.
			//
			this.topXmlResults = overviewThread.Options.DetailResultsFullFileName;
			this.dvtkWebBrowserResults.Navigate(topXmlResults);
			
			this.tabControl.SelectedTab = this.tabPageResults;

			this.isRunning = false;
            this.isStopped = true;
            
			UpdateButtons();
		
			// Clean up temporary files
			Cleanup();
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			//If emulator is running then stop it
			if((isRunning) || (!isStopped))
				this.threadManager.Stop();

			this.Close();
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			AboutForm about = new AboutForm("DVTk QR SCP Emulator");
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
				config.TSILESupport = false;
				config.TSELESupport = false;
				config.TSEBESupport = false;

				foreach (DvtkData.Dul.TransferSyntax ts in theSelectTransferSyntaxesForm.SupportedTransferSyntaxes)
				{
					selectedTS.Add(ts.UID);

					//Update the config XML
					if( ts.UID == DvtkData.Dul.TransferSyntax.Implicit_VR_Little_Endian.UID)
						config.TSILESupport = true;
					else if( ts.UID == DvtkData.Dul.TransferSyntax.Explicit_VR_Little_Endian.UID)
						config.TSELESupport = true;
					else if( ts.UID == DvtkData.Dul.TransferSyntax.Explicit_VR_Big_Endian.UID)
						config.TSEBESupport = true;
					else
					{}
				}

				config.Serialize();
			}
		}

		private void checkBoxPRInfoModel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxPRInfoModel.Checked)
				checkBoxPRInfoModel.Checked = true;
			else
				checkBoxPRInfoModel.Checked = false;

			//Update the config XML
			config.PatientRootInfoModelSupport = checkBoxPRInfoModel.Checked;
			config.Serialize();
		}

		private void checkBoxSRInfoModel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxSRInfoModel.Checked)
				checkBoxSRInfoModel.Checked = true;
			else
				checkBoxSRInfoModel.Checked = false;

			//Update the config XML
			config.StudyRootInfoModelSupport = checkBoxSRInfoModel.Checked;
			config.Serialize();
		}

		private void checkBoxPSRInfoModel_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxPSRInfoModel.Checked)
				checkBoxPSRInfoModel.Checked = true;
			else
				checkBoxPSRInfoModel.Checked = false;

			//Update the config XML
			config.PatientStudyRootInfoModelSupport = checkBoxPSRInfoModel.Checked;
			config.Serialize();
		}

		private void checkBoxSelectDir_CheckedChanged(object sender, System.EventArgs e)
		{
			if(checkBoxSelectDir.Checked)
			{
				buttonBrowse.Visible = true;
				textBoxDataDir.Visible = true;
				textBoxDataDir.Text = config.DataDirectoryForEmulation;
			}
			else
			{
				buttonBrowse.Visible = false;
				textBoxDataDir.Visible = false;
			}
		}

		private void buttonBrowse_Click(object sender, System.EventArgs e)
		{
			FolderBrowserDialog theFolderBrowserDialog = new FolderBrowserDialog();
			theFolderBrowserDialog.Description = "Select Data directory:";

			// Only if the current directory exists, set this directory in the dialog browser.
			if (textBoxDataDir.Text != "") 
			{
				DirectoryInfo theDirectoryInfo = new DirectoryInfo(textBoxDataDir.Text);

				if (theDirectoryInfo.Exists) 
				{
					theFolderBrowserDialog.SelectedPath = textBoxDataDir.Text;
				}

				if (theFolderBrowserDialog.ShowDialog (this) == DialogResult.OK) 
				{
					textBoxDataDir.Text = theFolderBrowserDialog.SelectedPath;
					DirectoryInfo theSelectedDir = new DirectoryInfo(textBoxDataDir.Text);
					dataDirectory = textBoxDataDir.Text;
                    //this.sourceQRScp.Options.DataDirectory = dataDirectory;

					//Update the config XML
					config.DataDirectoryForEmulation = textBoxDataDir.Text;
					config.Serialize();

					if(theSelectedDir.GetFiles().Length != 0)
					{
						FileInfo file = (FileInfo)(theSelectedDir.GetFiles().GetValue(0));
						dcmEditorQREmulator.DCMFile = file.FullName;
					}
				}
			}
		}

        private void menuItemStoredFilesExploreValidationResults_Click(object sender, EventArgs e)
        {
            validationResultsFileGroup.Explore();
        }

        private void menuItemStoredFilesOptions_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = fileGroups.ShowDialogFileGroupsConfigurator();

            if (dialogResult == DialogResult.OK)
            {
                this.sourceQRScp.Options.ResultsDirectory = validationResultsFileGroup.Directory;

                // Make sure the session files contain the same information as the Stored Files user settings.
                sourceQRScp.Options.SaveToFile(Path.Combine(rootPath, "QR_SCP.ses"));
            }
        }

        private String ConfigurationsDirectory
        {
            get
            {
                return (Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"DVTk\QR SCP Emulator\Configurations"));
            }
        }

        private void menuConfigLoad_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ConfigurationsDirectory))
            {
                Directory.CreateDirectory(ConfigurationsDirectory);
            }

            OpenFileDialog theOpenFileDialog = new OpenFileDialog();

            theOpenFileDialog.Filter = "Config files (*.xml) |*.xml";
            theOpenFileDialog.Multiselect = false;
            theOpenFileDialog.Title = "Load a config file";

            if (theOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                Config.ConfigFullFileName = theOpenFileDialog.FileName;
                this.config= Config.Deserialize(Config.ConfigFullFileName);
                
                selectedTS.Clear();
                if (config.TSILESupport)
                    selectedTS.Add("1.2.840.10008.1.2");
                if (config.TSELESupport)
                    selectedTS.Add("1.2.840.10008.1.2.1");
                if (config.TSEBESupport)
                    selectedTS.Add("1.2.840.10008.1.2.2");
                
                this.checkBoxPRInfoModel.Checked = this.config.PatientRootInfoModelSupport;
                this.checkBoxPSRInfoModel.Checked = this.config.PatientStudyRootInfoModelSupport;
                this.checkBoxSRInfoModel.Checked = this.config.StudyRootInfoModelSupport;
                this.textBoxDataDir.Text = this.config.DataDirectoryForEmulation;
                this.dicomThreadOptionsUserControl.LocalAeTitle = this.config.LocalAeTitle;
                this.dicomThreadOptionsUserControl.LocalPort = this.config.LocalPort;
                this.dicomThreadOptionsUserControl.RemoteAeTitle = this.config.RemoteAeTitle;
                this.dicomThreadOptionsUserControl.RemoteIpAddress = this.config.RemoteIpAddress;
                this.dicomThreadOptionsUserControl.RemotePort = this.config.RemotePort;
                this.AE.Checked = this.config.IsCaseSensitiveAE;
                this.CS.Checked = this.config.IsCaseSensitiveCS;
                this.LO.Checked = this.config.IsCaseSensitiveLO;
                this.PN.Checked = this.config.IsCaseSensitivePN;
                this.SH.Checked = this.config.IsCaseSensitiveSH;
            }
            
        }

        private void menuConfigSave_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(ConfigurationsDirectory))
            {
                Directory.CreateDirectory(ConfigurationsDirectory);
            }
            this.config.LocalAeTitle = this.dicomThreadOptionsUserControl.LocalAeTitle ;
            this.config.LocalPort = this.dicomThreadOptionsUserControl.LocalPort;
            this.config.RemoteAeTitle = this.dicomThreadOptionsUserControl.RemoteAeTitle;
            this.config.RemotePort = this.dicomThreadOptionsUserControl.RemotePort;
            this.config.RemoteIpAddress = this.dicomThreadOptionsUserControl.RemoteIpAddress;
            
            if(selectedTS.Contains("1.2.840.10008.1.2"))
            {
                config.TSILESupport=true;
            }
            if(selectedTS.Contains("1.2.840.10008.1.2.1"))
            {
                config.TSELESupport = true;
            }
            if (selectedTS.Contains("1.2.840.10008.1.2.2"))
            {
                config.TSEBESupport = true;
            }

            SaveFileDialog saveSetUpFileDlg = new SaveFileDialog();
            saveSetUpFileDlg.InitialDirectory = ConfigurationsDirectory;
            saveSetUpFileDlg.Title = "Save the current configuration";
            saveSetUpFileDlg.Filter = "Config files (*.xml) |*.xml";
            if (saveSetUpFileDlg.ShowDialog() == DialogResult.OK)
            {
                Config.ConfigFullFileName = saveSetUpFileDlg.FileName;
                this.config.Serialize();
            }
        }

        private void toolBarButtonError_Click(object sender, EventArgs e)
        {
            this.dvtkWebBrowserResults.FindNextText("Error:", true, true);
        }

        private void toolBarButtonLeft_Click(object sender, EventArgs e)
        {
            this.dvtkWebBrowserResults.Back();
        }

        private void toolBarButtonRight_Click(object sender, EventArgs e)
        {
            this.dvtkWebBrowserResults.Forward();
        }

        private void toolBarButtonWarning_Click(object sender, EventArgs e)
        {
            this.dvtkWebBrowserResults.FindNextText("Warning:", true, true);
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl.SelectedTab == tabPageResults)
            {
                toolBarButtonError.Visible = true;
                toolBarButtonWarning.Visible = true;
                
                toolBarButtonError.Enabled = this.dvtkWebBrowserResults.ContainsErrors;
                toolBarButtonWarning.Enabled = this.dvtkWebBrowserResults.ContainsWarnings;
            }
            else
            {
                toolBarButtonError.Visible = false;
                toolBarButtonWarning.Visible = false;
                
            }		
        }

        private void QREmulator_Load(object sender, EventArgs e)
        {
            toolBarButtonError.Visible = false;
            toolBarButtonWarning.Visible = false;
            
        }

        private void IsChecked(object sender, EventArgs e)
        {
            CheckBox c = (CheckBox)sender;
            if (c.Name == "AE")
            {
                config.IsCaseSensitiveAE = c.Checked;
            }
            else if (c.Name == "CS")
            {
                config.IsCaseSensitiveCS = c.Checked;
            }
            else if (c.Name == "SH")
            {
                config.IsCaseSensitiveLO = c.Checked;
            }
            else if (c.Name == "PN")
            { 
                config.IsCaseSensitivePN = c.Checked; 
            }
            else if (c.Name == "LO")
            {
                config.IsCaseSensitiveSH = c.Checked;
            }
            config.Serialize();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Reflection.Assembly a = System.Reflection.Assembly.GetEntryAssembly();
            string baseDir = System.IO.Path.GetDirectoryName(a.Location);
            Process.Start(new ProcessStartInfo(baseDir + @"\QueryAttributes.txt"));
        }

         
	}
}
