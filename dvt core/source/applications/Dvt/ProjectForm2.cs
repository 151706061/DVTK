// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using DvtTreeNodeTag;



namespace Dvt
{
	/// <summary>
	/// Summary description for ProjectForm2.
	/// </summary>
	public class ProjectForm2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabPage TabActivityLogging;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel PanelGeneralPropertiesTitle;
		private System.Windows.Forms.PictureBox MinGSPSettings;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.PictureBox MaxGSPSettings;
		private System.Windows.Forms.Panel PanelGeneralPropertiesContent;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.NumericUpDown NumericSessonID;
		private System.Windows.Forms.Label LabelResultsDir;
		private System.Windows.Forms.Label LabelSessionType;
		private System.Windows.Forms.Label LabelDate;
		private System.Windows.Forms.Label LabelSessionTitle;
		private System.Windows.Forms.Label LabelTestedBy;
		private System.Windows.Forms.TextBox TextBoxTestedBy;
		private System.Windows.Forms.TextBox TextBoxResultsRoot;
		private System.Windows.Forms.TextBox TextBoxScriptRoot;
		private System.Windows.Forms.Label LabelScriptsDir;
		private System.Windows.Forms.TextBox TextBoxSessionTitle;
		private System.Windows.Forms.Label label7;
		//private System.Windows.Forms.DateTimePicker DateTested;
		private System.Windows.Forms.Label LabelSpecifyTransferSyntaxes;
		private System.Windows.Forms.Label LabelDescriptionDir;
		private System.Windows.Forms.TextBox TextBoxDescriptionRoot;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button ButtonBrowseResultsDir;
		private System.Windows.Forms.Button ButtonBrowseScriptsDir;
		private System.Windows.Forms.Button ButtonSpecifyTransferSyntaxes;
		private System.Windows.Forms.Button ButtonBrowseDescriptionDir;
		private System.Windows.Forms.Panel PanelDVTRoleSettingsTitle;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox MinDVTRoleSettings;
		private System.Windows.Forms.PictureBox MaxDVTRoleSettings;
		private System.Windows.Forms.Panel PanelDVTRoleSettingsContent;
		private System.Windows.Forms.NumericUpDown NumericDVTListenPort;
		private System.Windows.Forms.TextBox TextBoxDVTAETitle;
		private System.Windows.Forms.Label LabelDVTAETitle;
		private System.Windows.Forms.Label LabelDVTListenPort;
		private System.Windows.Forms.Label LabelDVTSocketTimeOut;
		private System.Windows.Forms.Label LabelDVTMaxPDU;
		private System.Windows.Forms.NumericUpDown NumericDVTTimeOut;
		private System.Windows.Forms.NumericUpDown NumericDVTMaxPDU;
		private System.Windows.Forms.TextBox TextBoxDVTImplClassUID;
		private System.Windows.Forms.TextBox TextBoxDVTImplVersionName;
		private System.Windows.Forms.Label LabelDVTImplClassUID;
		private System.Windows.Forms.Label LabelDVTImplVersionName;
		private System.Windows.Forms.Panel PanelSUTSettingTitle;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox MinSUTSettings;
		private System.Windows.Forms.PictureBox MaxSUTSettings;
		private System.Windows.Forms.Panel PanelSUTSettingContent;
		private System.Windows.Forms.Label LabelSUTMaxPDU;
		private System.Windows.Forms.Label LabelSUTAETitle;
		private System.Windows.Forms.Label LabelSUTTCPIPAddress;
		private System.Windows.Forms.Label LabelSUTListenPort;
		private System.Windows.Forms.TextBox TextBoxSUTAETitle;
		private System.Windows.Forms.TextBox TextBoxSUTTCPIPAddress;
		private System.Windows.Forms.Button ButtonCheckTCPIPAddress;
		private System.Windows.Forms.NumericUpDown NumericSUTListenPort;
		private System.Windows.Forms.NumericUpDown NumericSUTMaxPDU;
		private System.Windows.Forms.Label LabelSUTImplClassUID;
		private System.Windows.Forms.Label LabelSUTImplVersionName;
		private System.Windows.Forms.TextBox TextBoxSUTImplClassUID;
		private System.Windows.Forms.TextBox TextBoxSUTImplVersionName;
		private System.Windows.Forms.Panel PanelSecuritySettingsTitle;
		private System.Windows.Forms.PictureBox MinSecuritySettings;
		private System.Windows.Forms.PictureBox MaxSecuritySettings;
		private System.Windows.Forms.CheckBox CheckBoxSecureConnection;
		private System.Windows.Forms.Panel PanelSecuritySettingsContent;
		private System.Windows.Forms.GroupBox GroupSecurityVersion;
		private System.Windows.Forms.CheckBox CheckBoxTLS;
		private System.Windows.Forms.CheckBox CheckBoxSSL;
		private System.Windows.Forms.GroupBox GroupSecurityKeyExchange;
		private System.Windows.Forms.CheckBox CheckBoxKeyExchangeRSA;
		private System.Windows.Forms.CheckBox CheckBoxKeyExchangeDH;
		private System.Windows.Forms.GroupBox GroupSecurityGeneral;
		private System.Windows.Forms.CheckBox CheckBoxCheckRemoteCertificates;
		private System.Windows.Forms.CheckBox CheckBoxCacheSecureSessions;
		private System.Windows.Forms.GroupBox GroupSecurityEncryption;
		private System.Windows.Forms.CheckBox CheckBoxEncryptionNone;
		private System.Windows.Forms.CheckBox CheckBoxEncryptionTripleDES;
		private System.Windows.Forms.CheckBox CheckBoxEncryptionAES128;
		private System.Windows.Forms.CheckBox CheckBoxEncryptionAES256;
		private System.Windows.Forms.GroupBox GroupSecurityDataIntegrity;
		private System.Windows.Forms.CheckBox CheckBoxDataIntegritySHA;
		private System.Windows.Forms.CheckBox CheckBoxDataIntegrityMD5;
		private System.Windows.Forms.GroupBox GroupSecurityAuthentication;
		private System.Windows.Forms.CheckBox CheckBoxAuthenticationRSA;
		private System.Windows.Forms.CheckBox CheckBoxAuthenticationDSA;
		private System.Windows.Forms.ListBox ListBoxSecuritySettings;
		private System.Windows.Forms.Label LabelCategories;
		private System.Windows.Forms.Label LabelSelect1ItemMsg;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Button ButtonViewCertificates;
		private System.Windows.Forms.Button ButtonViewCredentials;
		private System.Windows.Forms.Button ButtonCreateCertificate;
		private System.Windows.Forms.TreeView SessionTreeView;
		private System.Windows.Forms.TabPage TabSessionInformation;
		private System.Windows.Forms.TextBox TextBoxSessionType;
		private System.Windows.Forms.TabControl TabControl;
		private System.Windows.Forms.TabPage TabNoInformationAvailable;
		private System.Windows.Forms.Label LabelNoInformationAvailable;
		private System.Windows.Forms.TabPage TabScript;
		private System.Windows.Forms.RichTextBox RichTextBoxActivityLogging;
		private AxSHDocVw.AxWebBrowser WebDescriptionView;
		private System.Windows.Forms.TabPage TabValidationResults;
		private System.Windows.Forms.CheckBox CheckBoxGenerateDetailedValidationResults;
		private AxSHDocVw.AxWebBrowser axWebBrowserScript;
		private System.Windows.Forms.RichTextBox RichTextBoxScript;
		private System.Windows.Forms.FolderBrowserDialog TheFolderBrowserDialog;
		private System.Windows.Forms.TabPage TabSpecifySopClasses;
		private System.Windows.Forms.DataGrid DataGridSpecifySopClasses;
		private System.Windows.Forms.RichTextBox RichTextBoxSpecifySopClassesInfo;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.ListBox ListBoxSpecifySopClassesDefinitionFileDirectories;
		private System.Windows.Forms.Label LabelSpecifySopClassesDefinitionFileDirectories;
		private System.Windows.Forms.Label LabelSpecifySopClassesSelectAeTitle;
		private System.Windows.Forms.ComboBox ComboBoxSpecifySopClassesAeTitle;
		private System.Windows.Forms.Button ButtonSpecifySopClassesAddDirectory;
		private System.Windows.Forms.Button ButtonSpecifySopClassesRemoveDirectory;
		private System.Windows.Forms.Panel panel3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ContextMenu ContextMenuSessionTreeView;
		private System.Windows.Forms.MenuItem ContextMenu_Edit;
		private System.Windows.Forms.MenuItem ContextMenu_Execute;
		private System.Windows.Forms.MenuItem ContextMenu_None;
		private System.Windows.Forms.MenuItem ContextMenu_Remove;
		private System.Windows.Forms.MenuItem ContextMenu_AddNewSession;
		private System.Windows.Forms.MenuItem ContextMenu_RemoveAllResultsFiles;
		private System.Windows.Forms.MenuItem ContextMenu_RemoveSessionFromProject;
		private System.Windows.Forms.MenuItem ContextMenu_AddExistingSessions;

		//
		// Members that are not automatically added by the form designer.
		//

		private Project _Project = null;
		SessionTreeViewManager _SessionTreeViewManager;
		SopClassesManager _SopClassesManager;

		// Boolean indicating if the sabs are shown.
		private bool _TCM_SessionInformationTabShown = true;
		private bool _TCM_ActivityLoggingTabShown = true;
		private bool _TCM_DetailedValidationTabShown = true;
		private bool _TCM_ScriptTabShown = true;
		private bool _TCM_SpecifySopClassesTabShown = true;
		private bool _TCM_NoInformationAvailableTabShown = true;

		private Rectangle _PreviousBounds = Rectangle.Empty;

		string _HtmlFullFileNameToShow;

		// Manages the validation results tab.
		private ValidationResultsManager _TCM_ValidationResultsManager = null;
		private ValidationResultsManager _TCM_ValidationResultsManager1 = null;

		public enum ProjectFormState {IDLE, EXECUTING_SCRIPT, EXECUTING_STORAGE_SCP, EXECUTING_PRINT_SCP, EXECUTING_STORAGE_SCU, EXECUTING_MEDIA_VALIDATION};
		private ProjectFormState _State = ProjectFormState.IDLE;

		delegate void TCM_AppendTextToActivityLogging_ThreadSafe_Delegate(string theText);
		private TCM_AppendTextToActivityLogging_ThreadSafe_Delegate _TCM_AppendTextToActivityLogging_ThreadSafe_Delegate = null;
		private System.Windows.Forms.MenuItem ContextMenu_ExploreResultsDir;
		private System.Windows.Forms.MenuItem ContextMenu_ExploreScriptsDir;

		bool _TCM_ShowMinSecuritySettings = true;
		private System.Windows.Forms.MenuItem ContextMenu_SaveAs;
		private System.Windows.Forms.MenuItem ContextMenu_Save;
		private System.Windows.Forms.MenuItem ContextMenu_ValidateMediaFiles;
		private System.Windows.Forms.GroupBox GroupSecurityFiles;
		private System.Windows.Forms.Label LabelTrustedCertificatesFile;
		private System.Windows.Forms.Label LabelSecurityCredentialsFile;
		private System.Windows.Forms.Button ButtonSecurityCredentialsFile;
		private System.Windows.Forms.TextBox TextBoxTrustedCertificatesFile;
		private System.Windows.Forms.TextBox TextBoxSecurityCredentialsFile;
		private System.Windows.Forms.Button ButtonTrustedCertificatesFile;
		private System.Windows.Forms.Label labelStorageMode;
		private System.Windows.Forms.CheckBox CheckBoxLogRelation;
		private System.Windows.Forms.ComboBox ComboBoxStorageMode;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.MenuItem ContextMenu_ViewExpandedScript;
		private System.Windows.Forms.ContextMenu ContextMenuRichTextBox;
		private System.Windows.Forms.MenuItem ContextMenu_Copy;
		private System.Windows.Forms.CheckBox CheckBoxDefineSQLength;
		private System.Windows.Forms.CheckBox CheckBoxAddGroupLengths;
		private System.Windows.Forms.MenuItem ContextMenu_GenerateDICOMDIR;
        private System.Windows.Forms.MenuItem ContextMenu_ValidateDicomdirWithoutRefFile;
		public MainForm _MainForm = null;

		/// <summary>
		/// Get the state.
		/// </summary>
		/// <returns>The state.</returns>
		public ProjectFormState GetState()
		{
			return _State;
		}

		public bool IsExecuting()
		{
			bool isExecuting = false;

			if ( (GetState() == ProjectFormState.EXECUTING_SCRIPT) ||
				(GetState() == ProjectFormState.EXECUTING_STORAGE_SCP) ||
				(GetState() == ProjectFormState.EXECUTING_PRINT_SCP) ||
				(GetState() == ProjectFormState.EXECUTING_STORAGE_SCU)
				)
			{
				isExecuting = true;
			}

			return(isExecuting);
		}


		public enum ProjectFormActiveTab {SESSION_INFORMATION_TAB, VALIDATION_RESULTS_TAB, SPECIFY_SOP_CLASSES_TAB, ACTIVITY_LOGGING_TAB, SCRIPT_TAB, OTHER_TAB};

		/// <summary>
		/// Get the active tab of the tab control.
		/// </summary>
		/// <returns>The active tab.</returns>
		public ProjectFormActiveTab GetActiveTab()
		{
			ProjectFormActiveTab theActiveTab = ProjectFormActiveTab.OTHER_TAB;

			if (TabControl.SelectedTab == TabSessionInformation)
			{
				theActiveTab = ProjectFormActiveTab.SESSION_INFORMATION_TAB;
			}

			if (TabControl.SelectedTab == TabActivityLogging)
			{
				theActiveTab = ProjectFormActiveTab.ACTIVITY_LOGGING_TAB;
			}

			if (TabControl.SelectedTab == TabScript)
			{
				theActiveTab = ProjectFormActiveTab.SCRIPT_TAB;
			}

			if (TabControl.SelectedTab == TabValidationResults)
			{
				theActiveTab = ProjectFormActiveTab.VALIDATION_RESULTS_TAB;
			}

			if (TabControl.SelectedTab == TabSpecifySopClasses)
			{
				theActiveTab = ProjectFormActiveTab.SPECIFY_SOP_CLASSES_TAB;
			}

			return theActiveTab;
		}
 

		public ProjectForm2(Project theProject, MainForm theMainForm)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_ActivityReportEventHandler = new Dvtk.Events.ActivityReportEventHandler(TCM_OnActivityReportEvent);

			_Project = theProject;
			_MainForm = theMainForm;
			ListBoxSecuritySettings.SelectedIndex = 0;
			_SessionTreeViewManager = new SessionTreeViewManager(this, theProject, SessionTreeView);
			_SopClassesManager = new SopClassesManager(this, DataGridSpecifySopClasses, ComboBoxSpecifySopClassesAeTitle, ListBoxSpecifySopClassesDefinitionFileDirectories, RichTextBoxSpecifySopClassesInfo, _SessionTreeViewManager, ButtonSpecifySopClassesRemoveDirectory);
			_TCM_ValidationResultsManager = new ValidationResultsManager(WebDescriptionView);

			_TCM_ValidationResultsManager1 = new ValidationResultsManager(axWebBrowserScript);

			// Because the webbrowser navigation is "cancelled" when browsing to an .xml file
			// first another html file has to be shown to make this work under Windows 2000.
			_TCM_ValidationResultsManager.ShowHtml("about:blank");

			_TCM_AppendTextToActivityLogging_ThreadSafe_Delegate = new TCM_AppendTextToActivityLogging_ThreadSafe_Delegate(this.TCM_AppendTextToActivityLogging_ThreadSafe);


			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			// MK!!!
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		public ValidationResultsManager TCM_GetValidationResultsManager()
		{
			return(_TCM_ValidationResultsManager);
		}

		public ValidationResultsManager TCM_GetValidationResultsManager1()
		{
			return(_TCM_ValidationResultsManager1);
		}

		// Needed to be able to differentiate between controls changed by the user
		// and controls changed by an update method.
		// private bool _TCM_IsUpdatingTabControl = false;
		private int _TCM_UpdateCount = 0;
		
		private int _TCM_CountForControlsChange = 0;

		// Used to be able to tell which session is used to fill the Session Information Tab.
		private Dvtk.Sessions.Session _TCM_SessionUsedForContentsOfTabSessionInformation = null;
		
		private Dvtk.Events.ActivityReportEventHandler _ActivityReportEventHandler;



		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectForm2));
            this.SessionTreeView = new System.Windows.Forms.TreeView();
            this.ContextMenuSessionTreeView = new System.Windows.Forms.ContextMenu();
            this.ContextMenu_AddExistingSessions = new System.Windows.Forms.MenuItem();
            this.ContextMenu_AddNewSession = new System.Windows.Forms.MenuItem();
            this.ContextMenu_Edit = new System.Windows.Forms.MenuItem();
            this.ContextMenu_Execute = new System.Windows.Forms.MenuItem();
            this.ContextMenu_ExploreResultsDir = new System.Windows.Forms.MenuItem();
            this.ContextMenu_ExploreScriptsDir = new System.Windows.Forms.MenuItem();
            this.ContextMenu_None = new System.Windows.Forms.MenuItem();
            this.ContextMenu_Remove = new System.Windows.Forms.MenuItem();
            this.ContextMenu_RemoveAllResultsFiles = new System.Windows.Forms.MenuItem();
            this.ContextMenu_RemoveSessionFromProject = new System.Windows.Forms.MenuItem();
            this.ContextMenu_Save = new System.Windows.Forms.MenuItem();
            this.ContextMenu_SaveAs = new System.Windows.Forms.MenuItem();
            this.ContextMenu_ValidateMediaFiles = new System.Windows.Forms.MenuItem();
            this.ContextMenu_ViewExpandedScript = new System.Windows.Forms.MenuItem();
            this.ContextMenu_GenerateDICOMDIR = new System.Windows.Forms.MenuItem();
            this.ContextMenu_ValidateDicomdirWithoutRefFile = new System.Windows.Forms.MenuItem();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.TabSessionInformation = new System.Windows.Forms.TabPage();
            this.PanelSecuritySettingsContent = new System.Windows.Forms.Panel();
            this.ListBoxSecuritySettings = new System.Windows.Forms.ListBox();
            this.LabelCategories = new System.Windows.Forms.Label();
            this.LabelSelect1ItemMsg = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.GroupSecurityFiles = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBoxSecurityCredentialsFile = new System.Windows.Forms.TextBox();
            this.TextBoxTrustedCertificatesFile = new System.Windows.Forms.TextBox();
            this.ButtonSecurityCredentialsFile = new System.Windows.Forms.Button();
            this.ButtonTrustedCertificatesFile = new System.Windows.Forms.Button();
            this.LabelSecurityCredentialsFile = new System.Windows.Forms.Label();
            this.LabelTrustedCertificatesFile = new System.Windows.Forms.Label();
            this.ButtonCreateCertificate = new System.Windows.Forms.Button();
            this.ButtonViewCertificates = new System.Windows.Forms.Button();
            this.ButtonViewCredentials = new System.Windows.Forms.Button();
            this.GroupSecurityVersion = new System.Windows.Forms.GroupBox();
            this.CheckBoxTLS = new System.Windows.Forms.CheckBox();
            this.CheckBoxSSL = new System.Windows.Forms.CheckBox();
            this.GroupSecurityKeyExchange = new System.Windows.Forms.GroupBox();
            this.CheckBoxKeyExchangeRSA = new System.Windows.Forms.CheckBox();
            this.CheckBoxKeyExchangeDH = new System.Windows.Forms.CheckBox();
            this.GroupSecurityGeneral = new System.Windows.Forms.GroupBox();
            this.CheckBoxCheckRemoteCertificates = new System.Windows.Forms.CheckBox();
            this.CheckBoxCacheSecureSessions = new System.Windows.Forms.CheckBox();
            this.GroupSecurityEncryption = new System.Windows.Forms.GroupBox();
            this.CheckBoxEncryptionNone = new System.Windows.Forms.CheckBox();
            this.CheckBoxEncryptionTripleDES = new System.Windows.Forms.CheckBox();
            this.CheckBoxEncryptionAES128 = new System.Windows.Forms.CheckBox();
            this.CheckBoxEncryptionAES256 = new System.Windows.Forms.CheckBox();
            this.GroupSecurityDataIntegrity = new System.Windows.Forms.GroupBox();
            this.CheckBoxDataIntegritySHA = new System.Windows.Forms.CheckBox();
            this.CheckBoxDataIntegrityMD5 = new System.Windows.Forms.CheckBox();
            this.GroupSecurityAuthentication = new System.Windows.Forms.GroupBox();
            this.CheckBoxAuthenticationRSA = new System.Windows.Forms.CheckBox();
            this.CheckBoxAuthenticationDSA = new System.Windows.Forms.CheckBox();
            this.PanelSecuritySettingsTitle = new System.Windows.Forms.Panel();
            this.CheckBoxSecureConnection = new System.Windows.Forms.CheckBox();
            this.MinSecuritySettings = new System.Windows.Forms.PictureBox();
            this.MaxSecuritySettings = new System.Windows.Forms.PictureBox();
            this.PanelSUTSettingContent = new System.Windows.Forms.Panel();
            this.LabelSUTMaxPDU = new System.Windows.Forms.Label();
            this.LabelSUTAETitle = new System.Windows.Forms.Label();
            this.LabelSUTTCPIPAddress = new System.Windows.Forms.Label();
            this.LabelSUTListenPort = new System.Windows.Forms.Label();
            this.TextBoxSUTAETitle = new System.Windows.Forms.TextBox();
            this.TextBoxSUTTCPIPAddress = new System.Windows.Forms.TextBox();
            this.ButtonCheckTCPIPAddress = new System.Windows.Forms.Button();
            this.NumericSUTListenPort = new System.Windows.Forms.NumericUpDown();
            this.NumericSUTMaxPDU = new System.Windows.Forms.NumericUpDown();
            this.LabelSUTImplClassUID = new System.Windows.Forms.Label();
            this.LabelSUTImplVersionName = new System.Windows.Forms.Label();
            this.TextBoxSUTImplClassUID = new System.Windows.Forms.TextBox();
            this.TextBoxSUTImplVersionName = new System.Windows.Forms.TextBox();
            this.PanelSUTSettingTitle = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.MinSUTSettings = new System.Windows.Forms.PictureBox();
            this.MaxSUTSettings = new System.Windows.Forms.PictureBox();
            this.PanelDVTRoleSettingsContent = new System.Windows.Forms.Panel();
            this.NumericDVTListenPort = new System.Windows.Forms.NumericUpDown();
            this.TextBoxDVTAETitle = new System.Windows.Forms.TextBox();
            this.LabelDVTAETitle = new System.Windows.Forms.Label();
            this.LabelDVTListenPort = new System.Windows.Forms.Label();
            this.LabelDVTSocketTimeOut = new System.Windows.Forms.Label();
            this.LabelDVTMaxPDU = new System.Windows.Forms.Label();
            this.NumericDVTTimeOut = new System.Windows.Forms.NumericUpDown();
            this.NumericDVTMaxPDU = new System.Windows.Forms.NumericUpDown();
            this.TextBoxDVTImplClassUID = new System.Windows.Forms.TextBox();
            this.TextBoxDVTImplVersionName = new System.Windows.Forms.TextBox();
            this.LabelDVTImplClassUID = new System.Windows.Forms.Label();
            this.LabelDVTImplVersionName = new System.Windows.Forms.Label();
            this.PanelDVTRoleSettingsTitle = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.MinDVTRoleSettings = new System.Windows.Forms.PictureBox();
            this.MaxDVTRoleSettings = new System.Windows.Forms.PictureBox();
            this.PanelGeneralPropertiesContent = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.CheckBoxAddGroupLengths = new System.Windows.Forms.CheckBox();
            this.CheckBoxDefineSQLength = new System.Windows.Forms.CheckBox();
            this.ComboBoxStorageMode = new System.Windows.Forms.ComboBox();
            this.CheckBoxLogRelation = new System.Windows.Forms.CheckBox();
            this.labelStorageMode = new System.Windows.Forms.Label();
            this.CheckBoxGenerateDetailedValidationResults = new System.Windows.Forms.CheckBox();
            this.TextBoxSessionType = new System.Windows.Forms.TextBox();
            this.NumericSessonID = new System.Windows.Forms.NumericUpDown();
            this.LabelResultsDir = new System.Windows.Forms.Label();
            this.LabelSessionType = new System.Windows.Forms.Label();
            this.LabelDate = new System.Windows.Forms.Label();
            this.LabelSessionTitle = new System.Windows.Forms.Label();
            this.LabelTestedBy = new System.Windows.Forms.Label();
            this.TextBoxTestedBy = new System.Windows.Forms.TextBox();
            this.TextBoxResultsRoot = new System.Windows.Forms.TextBox();
            this.TextBoxScriptRoot = new System.Windows.Forms.TextBox();
            this.LabelScriptsDir = new System.Windows.Forms.Label();
            this.TextBoxSessionTitle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.LabelSpecifyTransferSyntaxes = new System.Windows.Forms.Label();
            this.LabelDescriptionDir = new System.Windows.Forms.Label();
            this.TextBoxDescriptionRoot = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ButtonBrowseResultsDir = new System.Windows.Forms.Button();
            this.ButtonBrowseScriptsDir = new System.Windows.Forms.Button();
            this.ButtonSpecifyTransferSyntaxes = new System.Windows.Forms.Button();
            this.ButtonBrowseDescriptionDir = new System.Windows.Forms.Button();
            this.PanelGeneralPropertiesTitle = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.MinGSPSettings = new System.Windows.Forms.PictureBox();
            this.MaxGSPSettings = new System.Windows.Forms.PictureBox();
            this.TabSpecifySopClasses = new System.Windows.Forms.TabPage();
            this.DataGridSpecifySopClasses = new System.Windows.Forms.DataGrid();
            this.RichTextBoxSpecifySopClassesInfo = new System.Windows.Forms.RichTextBox();
            this.ContextMenuRichTextBox = new System.Windows.Forms.ContextMenu();
            this.ContextMenu_Copy = new System.Windows.Forms.MenuItem();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ButtonSpecifySopClassesAddDirectory = new System.Windows.Forms.Button();
            this.ButtonSpecifySopClassesRemoveDirectory = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.ListBoxSpecifySopClassesDefinitionFileDirectories = new System.Windows.Forms.ListBox();
            this.LabelSpecifySopClassesDefinitionFileDirectories = new System.Windows.Forms.Label();
            this.LabelSpecifySopClassesSelectAeTitle = new System.Windows.Forms.Label();
            this.ComboBoxSpecifySopClassesAeTitle = new System.Windows.Forms.ComboBox();
            this.TabActivityLogging = new System.Windows.Forms.TabPage();
            this.RichTextBoxActivityLogging = new System.Windows.Forms.RichTextBox();
            this.TabValidationResults = new System.Windows.Forms.TabPage();
            this.WebDescriptionView = new AxSHDocVw.AxWebBrowser();
            this.TabScript = new System.Windows.Forms.TabPage();
            this.RichTextBoxScript = new System.Windows.Forms.RichTextBox();
            this.axWebBrowserScript = new AxSHDocVw.AxWebBrowser();
            this.TabNoInformationAvailable = new System.Windows.Forms.TabPage();
            this.LabelNoInformationAvailable = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.TheFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.TabControl.SuspendLayout();
            this.TabSessionInformation.SuspendLayout();
            this.PanelSecuritySettingsContent.SuspendLayout();
            this.GroupSecurityFiles.SuspendLayout();
            this.GroupSecurityVersion.SuspendLayout();
            this.GroupSecurityKeyExchange.SuspendLayout();
            this.GroupSecurityGeneral.SuspendLayout();
            this.GroupSecurityEncryption.SuspendLayout();
            this.GroupSecurityDataIntegrity.SuspendLayout();
            this.GroupSecurityAuthentication.SuspendLayout();
            this.PanelSecuritySettingsTitle.SuspendLayout();
            this.PanelSUTSettingContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSUTListenPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSUTMaxPDU)).BeginInit();
            this.PanelSUTSettingTitle.SuspendLayout();
            this.PanelDVTRoleSettingsContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDVTListenPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDVTTimeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDVTMaxPDU)).BeginInit();
            this.PanelDVTRoleSettingsTitle.SuspendLayout();
            this.PanelGeneralPropertiesContent.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSessonID)).BeginInit();
            this.panel2.SuspendLayout();
            this.PanelGeneralPropertiesTitle.SuspendLayout();
            this.TabSpecifySopClasses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataGridSpecifySopClasses)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            this.TabActivityLogging.SuspendLayout();
            this.TabValidationResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WebDescriptionView)).BeginInit();
            this.TabScript.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowserScript)).BeginInit();
            this.TabNoInformationAvailable.SuspendLayout();
            this.SuspendLayout();
            // 
            // SessionTreeView
            // 
            this.SessionTreeView.ContextMenu = this.ContextMenuSessionTreeView;
            this.SessionTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.SessionTreeView.HideSelection = false;
            this.SessionTreeView.ImageIndex = -1;
            this.SessionTreeView.Location = new System.Drawing.Point(0, 0);
            this.SessionTreeView.Name = "SessionTreeView";
            this.SessionTreeView.SelectedImageIndex = -1;
            this.SessionTreeView.Size = new System.Drawing.Size(256, 694);
            this.SessionTreeView.TabIndex = 0;
            this.SessionTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SessionTreeView_MouseDown);
            this.SessionTreeView.DoubleClick += new System.EventHandler(this.SessionTreeView_DoubleClick);
            this.SessionTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SessionTreeView_AfterSelect);
            this.SessionTreeView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.SessionTreeView_BeforeSelect);
            // 
            // ContextMenuSessionTreeView
            // 
            this.ContextMenuSessionTreeView.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                       this.ContextMenu_AddExistingSessions,
                                                                                                       this.ContextMenu_AddNewSession,
                                                                                                       this.ContextMenu_Edit,
                                                                                                       this.ContextMenu_Execute,
                                                                                                       this.ContextMenu_ExploreResultsDir,
                                                                                                       this.ContextMenu_ExploreScriptsDir,
                                                                                                       this.ContextMenu_None,
                                                                                                       this.ContextMenu_Remove,
                                                                                                       this.ContextMenu_RemoveAllResultsFiles,
                                                                                                       this.ContextMenu_RemoveSessionFromProject,
                                                                                                       this.ContextMenu_Save,
                                                                                                       this.ContextMenu_SaveAs,
                                                                                                       this.ContextMenu_ValidateMediaFiles,
                                                                                                       this.ContextMenu_ViewExpandedScript,
                                                                                                       this.ContextMenu_GenerateDICOMDIR,
                                                                                                       this.ContextMenu_ValidateDicomdirWithoutRefFile});
            this.ContextMenuSessionTreeView.Popup += new System.EventHandler(this.ContextMenuSessionTreeView_Popup);
            // 
            // ContextMenu_AddExistingSessions
            // 
            this.ContextMenu_AddExistingSessions.Index = 0;
            this.ContextMenu_AddExistingSessions.Text = "Add Existing Session(s)...";
            this.ContextMenu_AddExistingSessions.Click += new System.EventHandler(this.ContextMenu_AddExistingSession_Click);
            // 
            // ContextMenu_AddNewSession
            // 
            this.ContextMenu_AddNewSession.Index = 1;
            this.ContextMenu_AddNewSession.Text = "Add New Session...";
            this.ContextMenu_AddNewSession.Click += new System.EventHandler(this.ContextMenu_AddNewSession_Click);
            // 
            // ContextMenu_Edit
            // 
            this.ContextMenu_Edit.Index = 2;
            this.ContextMenu_Edit.Text = "Edit Script with Notepad...";
            this.ContextMenu_Edit.Click += new System.EventHandler(this.ContextMenu_Edit_Click);
            // 
            // ContextMenu_Execute
            // 
            this.ContextMenu_Execute.Index = 3;
            this.ContextMenu_Execute.Text = "Execute";
            this.ContextMenu_Execute.Click += new System.EventHandler(this.ContextMenu_Execute_Click);
            // 
            // ContextMenu_ExploreResultsDir
            // 
            this.ContextMenu_ExploreResultsDir.Index = 4;
            this.ContextMenu_ExploreResultsDir.Text = "Explore Results Directory...";
            this.ContextMenu_ExploreResultsDir.Click += new System.EventHandler(this.ContextMenu_ExploreResultsDir_Click);
            // 
            // ContextMenu_ExploreScriptsDir
            // 
            this.ContextMenu_ExploreScriptsDir.Index = 5;
            this.ContextMenu_ExploreScriptsDir.Text = "Explore Scripts Directory...";
            this.ContextMenu_ExploreScriptsDir.Click += new System.EventHandler(this.ContextMenu_ExploreScriptsDir_Click);
            // 
            // ContextMenu_None
            // 
            this.ContextMenu_None.Enabled = false;
            this.ContextMenu_None.Index = 6;
            this.ContextMenu_None.Text = "None";
            // 
            // ContextMenu_Remove
            // 
            this.ContextMenu_Remove.Index = 7;
            this.ContextMenu_Remove.Text = "Remove";
            this.ContextMenu_Remove.Click += new System.EventHandler(this.ContextMenu_Remove_Click);
            // 
            // ContextMenu_RemoveAllResultsFiles
            // 
            this.ContextMenu_RemoveAllResultsFiles.Index = 8;
            this.ContextMenu_RemoveAllResultsFiles.Text = "Remove all Results Files";
            this.ContextMenu_RemoveAllResultsFiles.Click += new System.EventHandler(this.ContextMenu_RemoveAllResultFiles_Click);
            // 
            // ContextMenu_RemoveSessionFromProject
            // 
            this.ContextMenu_RemoveSessionFromProject.Index = 9;
            this.ContextMenu_RemoveSessionFromProject.Text = "Remove from Project";
            this.ContextMenu_RemoveSessionFromProject.Click += new System.EventHandler(this.ContextMenu_RemoveSessionFromProject_Click);
            // 
            // ContextMenu_Save
            // 
            this.ContextMenu_Save.Index = 10;
            this.ContextMenu_Save.Text = "Save";
            this.ContextMenu_Save.Click += new System.EventHandler(this.ContextMenu_Save_Click);
            // 
            // ContextMenu_SaveAs
            // 
            this.ContextMenu_SaveAs.Index = 11;
            this.ContextMenu_SaveAs.Text = "Save As...";
            this.ContextMenu_SaveAs.Click += new System.EventHandler(this.ContextMenu_SaveAs_Click);
            // 
            // ContextMenu_ValidateMediaFiles
            // 
            this.ContextMenu_ValidateMediaFiles.Index = 12;
            this.ContextMenu_ValidateMediaFiles.Text = "Validate Media File(s)...";
            this.ContextMenu_ValidateMediaFiles.Click += new System.EventHandler(this.ContextMenu_ValidateMediaFiles_Click);
            // 
            // ContextMenu_ViewExpandedScript
            // 
            this.ContextMenu_ViewExpandedScript.Index = 13;
            this.ContextMenu_ViewExpandedScript.Text = "View Expanded Script with Notepad...";
            this.ContextMenu_ViewExpandedScript.Click += new System.EventHandler(this.ContextMenu_ViewExpandedScript_Click);
            // 
            // ContextMenu_GenerateDICOMDIR
            // 
            this.ContextMenu_GenerateDICOMDIR.Index = 14;
            this.ContextMenu_GenerateDICOMDIR.Text = "Create DICOMDIR";
            this.ContextMenu_GenerateDICOMDIR.Click += new System.EventHandler(this.ContextMenu_GenerateDICOMDIR_Click);
            // 
            // ContextMenu_ValidateDicomdirWithoutRefFile
            // 
            this.ContextMenu_ValidateDicomdirWithoutRefFile.Index = 15;
            this.ContextMenu_ValidateDicomdirWithoutRefFile.Text = "Validate Dicomdir Without Ref File...";
            this.ContextMenu_ValidateDicomdirWithoutRefFile.Click += new System.EventHandler(this.ContextMenu_ValidateDicomdirWithoutRefFile_Click);
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.TabSessionInformation);
            this.TabControl.Controls.Add(this.TabSpecifySopClasses);
            this.TabControl.Controls.Add(this.TabActivityLogging);
            this.TabControl.Controls.Add(this.TabValidationResults);
            this.TabControl.Controls.Add(this.TabScript);
            this.TabControl.Controls.Add(this.TabNoInformationAvailable);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(256, 0);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(712, 694);
            this.TabControl.TabIndex = 1;
            this.TabControl.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // TabSessionInformation
            // 
            this.TabSessionInformation.AutoScroll = true;
            this.TabSessionInformation.Controls.Add(this.PanelSecuritySettingsContent);
            this.TabSessionInformation.Controls.Add(this.PanelSecuritySettingsTitle);
            this.TabSessionInformation.Controls.Add(this.PanelSUTSettingContent);
            this.TabSessionInformation.Controls.Add(this.PanelSUTSettingTitle);
            this.TabSessionInformation.Controls.Add(this.PanelDVTRoleSettingsContent);
            this.TabSessionInformation.Controls.Add(this.PanelDVTRoleSettingsTitle);
            this.TabSessionInformation.Controls.Add(this.PanelGeneralPropertiesContent);
            this.TabSessionInformation.Controls.Add(this.PanelGeneralPropertiesTitle);
            this.TabSessionInformation.DockPadding.All = 15;
            this.TabSessionInformation.Location = new System.Drawing.Point(4, 22);
            this.TabSessionInformation.Name = "TabSessionInformation";
            this.TabSessionInformation.Size = new System.Drawing.Size(704, 668);
            this.TabSessionInformation.TabIndex = 0;
            this.TabSessionInformation.Text = "Session Information";
            this.TabSessionInformation.Click += new System.EventHandler(this.TabSessionInformation_Click);
            // 
            // PanelSecuritySettingsContent
            // 
            this.PanelSecuritySettingsContent.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelSecuritySettingsContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSecuritySettingsContent.Controls.Add(this.ListBoxSecuritySettings);
            this.PanelSecuritySettingsContent.Controls.Add(this.LabelCategories);
            this.PanelSecuritySettingsContent.Controls.Add(this.LabelSelect1ItemMsg);
            this.PanelSecuritySettingsContent.Controls.Add(this.label28);
            this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityFiles);
            this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityVersion);
            this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityKeyExchange);
            this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityGeneral);
            this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityEncryption);
            this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityDataIntegrity);
            this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityAuthentication);
            this.PanelSecuritySettingsContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSecuritySettingsContent.Location = new System.Drawing.Point(15, 768);
            this.PanelSecuritySettingsContent.Name = "PanelSecuritySettingsContent";
            this.PanelSecuritySettingsContent.Size = new System.Drawing.Size(657, 200);
            this.PanelSecuritySettingsContent.TabIndex = 11;
            // 
            // ListBoxSecuritySettings
            // 
            this.ListBoxSecuritySettings.Items.AddRange(new object[] {
                                                                         "General",
                                                                         "Version",
                                                                         "Authentication",
                                                                         "Key Exchange",
                                                                         "Data Integrity",
                                                                         "Encryption",
                                                                         "Keys"});
            this.ListBoxSecuritySettings.Location = new System.Drawing.Point(8, 32);
            this.ListBoxSecuritySettings.Name = "ListBoxSecuritySettings";
            this.ListBoxSecuritySettings.Size = new System.Drawing.Size(120, 121);
            this.ListBoxSecuritySettings.TabIndex = 19;
            this.ListBoxSecuritySettings.SelectedIndexChanged += new System.EventHandler(this.ListBoxSecuritySettings_SelectedIndexChanged);
            // 
            // LabelCategories
            // 
            this.LabelCategories.Location = new System.Drawing.Point(8, 8);
            this.LabelCategories.Name = "LabelCategories";
            this.LabelCategories.TabIndex = 18;
            this.LabelCategories.Text = "Categories:";
            // 
            // LabelSelect1ItemMsg
            // 
            this.LabelSelect1ItemMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSelect1ItemMsg.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelSelect1ItemMsg.ForeColor = System.Drawing.Color.Red;
            this.LabelSelect1ItemMsg.Location = new System.Drawing.Point(256, 8);
            this.LabelSelect1ItemMsg.Name = "LabelSelect1ItemMsg";
            this.LabelSelect1ItemMsg.Size = new System.Drawing.Size(393, 16);
            this.LabelSelect1ItemMsg.TabIndex = 17;
            this.LabelSelect1ItemMsg.Text = "At least 1 item must be selected!";
            this.LabelSelect1ItemMsg.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LabelSelect1ItemMsg.Visible = false;
            // 
            // label28
            // 
            this.label28.Location = new System.Drawing.Point(136, 8);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(48, 16);
            this.label28.TabIndex = 20;
            this.label28.Text = "Settings:";
            // 
            // GroupSecurityFiles
            // 
            this.GroupSecurityFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupSecurityFiles.Controls.Add(this.label3);
            this.GroupSecurityFiles.Controls.Add(this.TextBoxSecurityCredentialsFile);
            this.GroupSecurityFiles.Controls.Add(this.TextBoxTrustedCertificatesFile);
            this.GroupSecurityFiles.Controls.Add(this.ButtonSecurityCredentialsFile);
            this.GroupSecurityFiles.Controls.Add(this.ButtonTrustedCertificatesFile);
            this.GroupSecurityFiles.Controls.Add(this.LabelSecurityCredentialsFile);
            this.GroupSecurityFiles.Controls.Add(this.LabelTrustedCertificatesFile);
            this.GroupSecurityFiles.Controls.Add(this.ButtonCreateCertificate);
            this.GroupSecurityFiles.Controls.Add(this.ButtonViewCertificates);
            this.GroupSecurityFiles.Controls.Add(this.ButtonViewCredentials);
            this.GroupSecurityFiles.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupSecurityFiles.Location = new System.Drawing.Point(136, 24);
            this.GroupSecurityFiles.Name = "GroupSecurityFiles";
            this.GroupSecurityFiles.Size = new System.Drawing.Size(504, 160);
            this.GroupSecurityFiles.TabIndex = 22;
            this.GroupSecurityFiles.TabStop = false;
            this.GroupSecurityFiles.Text = "Files";
            this.GroupSecurityFiles.Visible = false;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(16, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(224, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Create Public\\Private key pair:";
            // 
            // TextBoxSecurityCredentialsFile
            // 
            this.TextBoxSecurityCredentialsFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSecurityCredentialsFile.Location = new System.Drawing.Point(16, 104);
            this.TextBoxSecurityCredentialsFile.Name = "TextBoxSecurityCredentialsFile";
            this.TextBoxSecurityCredentialsFile.ReadOnly = true;
            this.TextBoxSecurityCredentialsFile.Size = new System.Drawing.Size(384, 20);
            this.TextBoxSecurityCredentialsFile.TabIndex = 5;
            this.TextBoxSecurityCredentialsFile.Text = "";
            // 
            // TextBoxTrustedCertificatesFile
            // 
            this.TextBoxTrustedCertificatesFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxTrustedCertificatesFile.Location = new System.Drawing.Point(16, 46);
            this.TextBoxTrustedCertificatesFile.Name = "TextBoxTrustedCertificatesFile";
            this.TextBoxTrustedCertificatesFile.ReadOnly = true;
            this.TextBoxTrustedCertificatesFile.Size = new System.Drawing.Size(384, 20);
            this.TextBoxTrustedCertificatesFile.TabIndex = 4;
            this.TextBoxTrustedCertificatesFile.Text = "";
            // 
            // ButtonSecurityCredentialsFile
            // 
            this.ButtonSecurityCredentialsFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonSecurityCredentialsFile.Location = new System.Drawing.Point(264, 72);
            this.ButtonSecurityCredentialsFile.Name = "ButtonSecurityCredentialsFile";
            this.ButtonSecurityCredentialsFile.TabIndex = 3;
            this.ButtonSecurityCredentialsFile.Text = "Browse";
            this.ButtonSecurityCredentialsFile.Click += new System.EventHandler(this.ButtonSecurityCredentialsFile_Click);
            // 
            // ButtonTrustedCertificatesFile
            // 
            this.ButtonTrustedCertificatesFile.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonTrustedCertificatesFile.Location = new System.Drawing.Point(264, 16);
            this.ButtonTrustedCertificatesFile.Name = "ButtonTrustedCertificatesFile";
            this.ButtonTrustedCertificatesFile.TabIndex = 2;
            this.ButtonTrustedCertificatesFile.Text = "Browse";
            this.ButtonTrustedCertificatesFile.Click += new System.EventHandler(this.ButtonTrustedCertificatesFile_Click);
            // 
            // LabelSecurityCredentialsFile
            // 
            this.LabelSecurityCredentialsFile.Location = new System.Drawing.Point(16, 78);
            this.LabelSecurityCredentialsFile.Name = "LabelSecurityCredentialsFile";
            this.LabelSecurityCredentialsFile.Size = new System.Drawing.Size(248, 23);
            this.LabelSecurityCredentialsFile.TabIndex = 1;
            this.LabelSecurityCredentialsFile.Text = "File containing DVT Private Keys (credentials):";
            // 
            // LabelTrustedCertificatesFile
            // 
            this.LabelTrustedCertificatesFile.Location = new System.Drawing.Point(16, 22);
            this.LabelTrustedCertificatesFile.Name = "LabelTrustedCertificatesFile";
            this.LabelTrustedCertificatesFile.Size = new System.Drawing.Size(248, 32);
            this.LabelTrustedCertificatesFile.TabIndex = 0;
            this.LabelTrustedCertificatesFile.Text = "File containing SUT Public Keys (certificates):";
            // 
            // ButtonCreateCertificate
            // 
            this.ButtonCreateCertificate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCreateCertificate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonCreateCertificate.Location = new System.Drawing.Point(416, 128);
            this.ButtonCreateCertificate.Name = "ButtonCreateCertificate";
            this.ButtonCreateCertificate.TabIndex = 7;
            this.ButtonCreateCertificate.Text = "Create";
            this.ButtonCreateCertificate.Click += new System.EventHandler(this.ButtonCreateCertificate_Click);
            // 
            // ButtonViewCertificates
            // 
            this.ButtonViewCertificates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonViewCertificates.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonViewCertificates.Location = new System.Drawing.Point(416, 48);
            this.ButtonViewCertificates.Name = "ButtonViewCertificates";
            this.ButtonViewCertificates.TabIndex = 7;
            this.ButtonViewCertificates.Text = "Edit";
            this.ButtonViewCertificates.Click += new System.EventHandler(this.ButtonViewCertificates_Click);
            // 
            // ButtonViewCredentials
            // 
            this.ButtonViewCredentials.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonViewCredentials.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonViewCredentials.Location = new System.Drawing.Point(416, 96);
            this.ButtonViewCredentials.Name = "ButtonViewCredentials";
            this.ButtonViewCredentials.TabIndex = 7;
            this.ButtonViewCredentials.Text = "Edit";
            this.ButtonViewCredentials.Click += new System.EventHandler(this.ButtonViewCredentials_Click);
            // 
            // GroupSecurityVersion
            // 
            this.GroupSecurityVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupSecurityVersion.Controls.Add(this.CheckBoxTLS);
            this.GroupSecurityVersion.Controls.Add(this.CheckBoxSSL);
            this.GroupSecurityVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupSecurityVersion.Location = new System.Drawing.Point(136, 24);
            this.GroupSecurityVersion.Name = "GroupSecurityVersion";
            this.GroupSecurityVersion.Size = new System.Drawing.Size(377, 128);
            this.GroupSecurityVersion.TabIndex = 12;
            this.GroupSecurityVersion.TabStop = false;
            this.GroupSecurityVersion.Text = "Version";
            this.GroupSecurityVersion.Visible = false;
            // 
            // CheckBoxTLS
            // 
            this.CheckBoxTLS.Location = new System.Drawing.Point(16, 24);
            this.CheckBoxTLS.Name = "CheckBoxTLS";
            this.CheckBoxTLS.Size = new System.Drawing.Size(64, 24);
            this.CheckBoxTLS.TabIndex = 0;
            this.CheckBoxTLS.Text = "TLS v1";
            this.CheckBoxTLS.CheckedChanged += new System.EventHandler(this.CheckBoxTLS_CheckedChanged);
            // 
            // CheckBoxSSL
            // 
            this.CheckBoxSSL.Location = new System.Drawing.Point(16, 48);
            this.CheckBoxSSL.Name = "CheckBoxSSL";
            this.CheckBoxSSL.Size = new System.Drawing.Size(64, 24);
            this.CheckBoxSSL.TabIndex = 0;
            this.CheckBoxSSL.Text = "SSL v3";
            this.CheckBoxSSL.CheckedChanged += new System.EventHandler(this.CheckBoxSSL_CheckedChanged);
            // 
            // GroupSecurityKeyExchange
            // 
            this.GroupSecurityKeyExchange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupSecurityKeyExchange.Controls.Add(this.CheckBoxKeyExchangeRSA);
            this.GroupSecurityKeyExchange.Controls.Add(this.CheckBoxKeyExchangeDH);
            this.GroupSecurityKeyExchange.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupSecurityKeyExchange.Location = new System.Drawing.Point(136, 24);
            this.GroupSecurityKeyExchange.Name = "GroupSecurityKeyExchange";
            this.GroupSecurityKeyExchange.Size = new System.Drawing.Size(377, 128);
            this.GroupSecurityKeyExchange.TabIndex = 16;
            this.GroupSecurityKeyExchange.TabStop = false;
            this.GroupSecurityKeyExchange.Text = "Key Exchange";
            this.GroupSecurityKeyExchange.Visible = false;
            // 
            // CheckBoxKeyExchangeRSA
            // 
            this.CheckBoxKeyExchangeRSA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxKeyExchangeRSA.Location = new System.Drawing.Point(16, 24);
            this.CheckBoxKeyExchangeRSA.Name = "CheckBoxKeyExchangeRSA";
            this.CheckBoxKeyExchangeRSA.Size = new System.Drawing.Size(353, 24);
            this.CheckBoxKeyExchangeRSA.TabIndex = 0;
            this.CheckBoxKeyExchangeRSA.Text = "RSA";
            this.CheckBoxKeyExchangeRSA.CheckedChanged += new System.EventHandler(this.CheckBoxKeyExchangeRSA_CheckedChanged);
            // 
            // CheckBoxKeyExchangeDH
            // 
            this.CheckBoxKeyExchangeDH.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxKeyExchangeDH.Location = new System.Drawing.Point(16, 48);
            this.CheckBoxKeyExchangeDH.Name = "CheckBoxKeyExchangeDH";
            this.CheckBoxKeyExchangeDH.Size = new System.Drawing.Size(353, 24);
            this.CheckBoxKeyExchangeDH.TabIndex = 0;
            this.CheckBoxKeyExchangeDH.Text = "DH";
            this.CheckBoxKeyExchangeDH.CheckedChanged += new System.EventHandler(this.CheckBoxKeyExchangeDH_CheckedChanged);
            // 
            // GroupSecurityGeneral
            // 
            this.GroupSecurityGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupSecurityGeneral.Controls.Add(this.CheckBoxCheckRemoteCertificates);
            this.GroupSecurityGeneral.Controls.Add(this.CheckBoxCacheSecureSessions);
            this.GroupSecurityGeneral.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupSecurityGeneral.Location = new System.Drawing.Point(136, 24);
            this.GroupSecurityGeneral.Name = "GroupSecurityGeneral";
            this.GroupSecurityGeneral.Size = new System.Drawing.Size(377, 128);
            this.GroupSecurityGeneral.TabIndex = 21;
            this.GroupSecurityGeneral.TabStop = false;
            this.GroupSecurityGeneral.Text = "General";
            // 
            // CheckBoxCheckRemoteCertificates
            // 
            this.CheckBoxCheckRemoteCertificates.Location = new System.Drawing.Point(16, 24);
            this.CheckBoxCheckRemoteCertificates.Name = "CheckBoxCheckRemoteCertificates";
            this.CheckBoxCheckRemoteCertificates.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxCheckRemoteCertificates.TabIndex = 0;
            this.CheckBoxCheckRemoteCertificates.Text = "Check remote certificates";
            this.CheckBoxCheckRemoteCertificates.CheckedChanged += new System.EventHandler(this.CheckBoxCheckRemoteCertificates_CheckedChanged);
            // 
            // CheckBoxCacheSecureSessions
            // 
            this.CheckBoxCacheSecureSessions.Location = new System.Drawing.Point(16, 48);
            this.CheckBoxCacheSecureSessions.Name = "CheckBoxCacheSecureSessions";
            this.CheckBoxCacheSecureSessions.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxCacheSecureSessions.TabIndex = 0;
            this.CheckBoxCacheSecureSessions.Text = "Cache secure sessions";
            this.CheckBoxCacheSecureSessions.CheckedChanged += new System.EventHandler(this.CheckBoxCacheSecureSessions_CheckedChanged);
            // 
            // GroupSecurityEncryption
            // 
            this.GroupSecurityEncryption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupSecurityEncryption.Controls.Add(this.CheckBoxEncryptionNone);
            this.GroupSecurityEncryption.Controls.Add(this.CheckBoxEncryptionTripleDES);
            this.GroupSecurityEncryption.Controls.Add(this.CheckBoxEncryptionAES128);
            this.GroupSecurityEncryption.Controls.Add(this.CheckBoxEncryptionAES256);
            this.GroupSecurityEncryption.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupSecurityEncryption.Location = new System.Drawing.Point(136, 24);
            this.GroupSecurityEncryption.Name = "GroupSecurityEncryption";
            this.GroupSecurityEncryption.Size = new System.Drawing.Size(369, 128);
            this.GroupSecurityEncryption.TabIndex = 14;
            this.GroupSecurityEncryption.TabStop = false;
            this.GroupSecurityEncryption.Text = "Encryption";
            this.GroupSecurityEncryption.Visible = false;
            // 
            // CheckBoxEncryptionNone
            // 
            this.CheckBoxEncryptionNone.Location = new System.Drawing.Point(16, 24);
            this.CheckBoxEncryptionNone.Name = "CheckBoxEncryptionNone";
            this.CheckBoxEncryptionNone.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxEncryptionNone.TabIndex = 0;
            this.CheckBoxEncryptionNone.Text = "None";
            this.CheckBoxEncryptionNone.CheckedChanged += new System.EventHandler(this.CheckBoxEncryptionNone_CheckedChanged);
            // 
            // CheckBoxEncryptionTripleDES
            // 
            this.CheckBoxEncryptionTripleDES.Location = new System.Drawing.Point(16, 48);
            this.CheckBoxEncryptionTripleDES.Name = "CheckBoxEncryptionTripleDES";
            this.CheckBoxEncryptionTripleDES.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxEncryptionTripleDES.TabIndex = 0;
            this.CheckBoxEncryptionTripleDES.Text = "Triple DES";
            this.CheckBoxEncryptionTripleDES.CheckedChanged += new System.EventHandler(this.CheckBoxEncryptionTripleDES_CheckedChanged);
            // 
            // CheckBoxEncryptionAES128
            // 
            this.CheckBoxEncryptionAES128.Location = new System.Drawing.Point(16, 72);
            this.CheckBoxEncryptionAES128.Name = "CheckBoxEncryptionAES128";
            this.CheckBoxEncryptionAES128.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxEncryptionAES128.TabIndex = 0;
            this.CheckBoxEncryptionAES128.Text = "AES 128-bit";
            this.CheckBoxEncryptionAES128.CheckedChanged += new System.EventHandler(this.CheckBoxEncryptionAES128_CheckedChanged);
            // 
            // CheckBoxEncryptionAES256
            // 
            this.CheckBoxEncryptionAES256.Location = new System.Drawing.Point(16, 96);
            this.CheckBoxEncryptionAES256.Name = "CheckBoxEncryptionAES256";
            this.CheckBoxEncryptionAES256.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxEncryptionAES256.TabIndex = 0;
            this.CheckBoxEncryptionAES256.Text = "AES 256-bit";
            this.CheckBoxEncryptionAES256.CheckedChanged += new System.EventHandler(this.CheckBoxEncryptionAES256_CheckedChanged);
            // 
            // GroupSecurityDataIntegrity
            // 
            this.GroupSecurityDataIntegrity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupSecurityDataIntegrity.Controls.Add(this.CheckBoxDataIntegritySHA);
            this.GroupSecurityDataIntegrity.Controls.Add(this.CheckBoxDataIntegrityMD5);
            this.GroupSecurityDataIntegrity.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupSecurityDataIntegrity.Location = new System.Drawing.Point(136, 24);
            this.GroupSecurityDataIntegrity.Name = "GroupSecurityDataIntegrity";
            this.GroupSecurityDataIntegrity.Size = new System.Drawing.Size(377, 128);
            this.GroupSecurityDataIntegrity.TabIndex = 14;
            this.GroupSecurityDataIntegrity.TabStop = false;
            this.GroupSecurityDataIntegrity.Text = "Data Integrity";
            this.GroupSecurityDataIntegrity.Visible = false;
            // 
            // CheckBoxDataIntegritySHA
            // 
            this.CheckBoxDataIntegritySHA.Location = new System.Drawing.Point(16, 24);
            this.CheckBoxDataIntegritySHA.Name = "CheckBoxDataIntegritySHA";
            this.CheckBoxDataIntegritySHA.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxDataIntegritySHA.TabIndex = 0;
            this.CheckBoxDataIntegritySHA.Text = "SHA";
            this.CheckBoxDataIntegritySHA.CheckedChanged += new System.EventHandler(this.CheckBoxDataIntegritySHA_CheckedChanged);
            // 
            // CheckBoxDataIntegrityMD5
            // 
            this.CheckBoxDataIntegrityMD5.Location = new System.Drawing.Point(16, 48);
            this.CheckBoxDataIntegrityMD5.Name = "CheckBoxDataIntegrityMD5";
            this.CheckBoxDataIntegrityMD5.Size = new System.Drawing.Size(184, 24);
            this.CheckBoxDataIntegrityMD5.TabIndex = 0;
            this.CheckBoxDataIntegrityMD5.Text = "MD5";
            this.CheckBoxDataIntegrityMD5.CheckedChanged += new System.EventHandler(this.CheckBoxDataIntegrityMD5_CheckedChanged);
            // 
            // GroupSecurityAuthentication
            // 
            this.GroupSecurityAuthentication.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupSecurityAuthentication.Controls.Add(this.CheckBoxAuthenticationRSA);
            this.GroupSecurityAuthentication.Controls.Add(this.CheckBoxAuthenticationDSA);
            this.GroupSecurityAuthentication.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.GroupSecurityAuthentication.Location = new System.Drawing.Point(136, 24);
            this.GroupSecurityAuthentication.Name = "GroupSecurityAuthentication";
            this.GroupSecurityAuthentication.Size = new System.Drawing.Size(377, 128);
            this.GroupSecurityAuthentication.TabIndex = 14;
            this.GroupSecurityAuthentication.TabStop = false;
            this.GroupSecurityAuthentication.Text = "Authentication";
            this.GroupSecurityAuthentication.Visible = false;
            // 
            // CheckBoxAuthenticationRSA
            // 
            this.CheckBoxAuthenticationRSA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxAuthenticationRSA.Location = new System.Drawing.Point(16, 24);
            this.CheckBoxAuthenticationRSA.Name = "CheckBoxAuthenticationRSA";
            this.CheckBoxAuthenticationRSA.Size = new System.Drawing.Size(353, 24);
            this.CheckBoxAuthenticationRSA.TabIndex = 0;
            this.CheckBoxAuthenticationRSA.Text = "RSA";
            this.CheckBoxAuthenticationRSA.CheckedChanged += new System.EventHandler(this.CheckBoxAuthenticationRSA_CheckedChanged);
            // 
            // CheckBoxAuthenticationDSA
            // 
            this.CheckBoxAuthenticationDSA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.CheckBoxAuthenticationDSA.Location = new System.Drawing.Point(16, 48);
            this.CheckBoxAuthenticationDSA.Name = "CheckBoxAuthenticationDSA";
            this.CheckBoxAuthenticationDSA.Size = new System.Drawing.Size(353, 24);
            this.CheckBoxAuthenticationDSA.TabIndex = 0;
            this.CheckBoxAuthenticationDSA.Text = "DSA";
            this.CheckBoxAuthenticationDSA.CheckedChanged += new System.EventHandler(this.CheckBoxAuthenticationDSA_CheckedChanged);
            // 
            // PanelSecuritySettingsTitle
            // 
            this.PanelSecuritySettingsTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PanelSecuritySettingsTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSecuritySettingsTitle.Controls.Add(this.CheckBoxSecureConnection);
            this.PanelSecuritySettingsTitle.Controls.Add(this.MinSecuritySettings);
            this.PanelSecuritySettingsTitle.Controls.Add(this.MaxSecuritySettings);
            this.PanelSecuritySettingsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSecuritySettingsTitle.Location = new System.Drawing.Point(15, 736);
            this.PanelSecuritySettingsTitle.Name = "PanelSecuritySettingsTitle";
            this.PanelSecuritySettingsTitle.Size = new System.Drawing.Size(657, 32);
            this.PanelSecuritySettingsTitle.TabIndex = 12;
            // 
            // CheckBoxSecureConnection
            // 
            this.CheckBoxSecureConnection.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CheckBoxSecureConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.CheckBoxSecureConnection.Location = new System.Drawing.Point(8, 4);
            this.CheckBoxSecureConnection.Name = "CheckBoxSecureConnection";
            this.CheckBoxSecureConnection.Size = new System.Drawing.Size(208, 24);
            this.CheckBoxSecureConnection.TabIndex = 22;
            this.CheckBoxSecureConnection.Text = "Security Settings";
            this.CheckBoxSecureConnection.CheckedChanged += new System.EventHandler(this.CheckBoxSecureConnection_CheckedChanged);
            // 
            // MinSecuritySettings
            // 
            this.MinSecuritySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinSecuritySettings.Image = ((System.Drawing.Image)(resources.GetObject("MinSecuritySettings.Image")));
            this.MinSecuritySettings.Location = new System.Drawing.Point(613, 0);
            this.MinSecuritySettings.Name = "MinSecuritySettings";
            this.MinSecuritySettings.Size = new System.Drawing.Size(32, 24);
            this.MinSecuritySettings.TabIndex = 1;
            this.MinSecuritySettings.TabStop = false;
            this.MinSecuritySettings.Click += new System.EventHandler(this.MinSecuritySettings_Click);
            // 
            // MaxSecuritySettings
            // 
            this.MaxSecuritySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxSecuritySettings.Image = ((System.Drawing.Image)(resources.GetObject("MaxSecuritySettings.Image")));
            this.MaxSecuritySettings.Location = new System.Drawing.Point(613, 0);
            this.MaxSecuritySettings.Name = "MaxSecuritySettings";
            this.MaxSecuritySettings.Size = new System.Drawing.Size(32, 24);
            this.MaxSecuritySettings.TabIndex = 1;
            this.MaxSecuritySettings.TabStop = false;
            this.MaxSecuritySettings.Visible = false;
            this.MaxSecuritySettings.Click += new System.EventHandler(this.MaxSecuritySettings_Click);
            // 
            // PanelSUTSettingContent
            // 
            this.PanelSUTSettingContent.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelSUTSettingContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSUTSettingContent.Controls.Add(this.LabelSUTMaxPDU);
            this.PanelSUTSettingContent.Controls.Add(this.LabelSUTAETitle);
            this.PanelSUTSettingContent.Controls.Add(this.LabelSUTTCPIPAddress);
            this.PanelSUTSettingContent.Controls.Add(this.LabelSUTListenPort);
            this.PanelSUTSettingContent.Controls.Add(this.TextBoxSUTAETitle);
            this.PanelSUTSettingContent.Controls.Add(this.TextBoxSUTTCPIPAddress);
            this.PanelSUTSettingContent.Controls.Add(this.ButtonCheckTCPIPAddress);
            this.PanelSUTSettingContent.Controls.Add(this.NumericSUTListenPort);
            this.PanelSUTSettingContent.Controls.Add(this.NumericSUTMaxPDU);
            this.PanelSUTSettingContent.Controls.Add(this.LabelSUTImplClassUID);
            this.PanelSUTSettingContent.Controls.Add(this.LabelSUTImplVersionName);
            this.PanelSUTSettingContent.Controls.Add(this.TextBoxSUTImplClassUID);
            this.PanelSUTSettingContent.Controls.Add(this.TextBoxSUTImplVersionName);
            this.PanelSUTSettingContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSUTSettingContent.Location = new System.Drawing.Point(15, 576);
            this.PanelSUTSettingContent.Name = "PanelSUTSettingContent";
            this.PanelSUTSettingContent.Size = new System.Drawing.Size(657, 160);
            this.PanelSUTSettingContent.TabIndex = 9;
            // 
            // LabelSUTMaxPDU
            // 
            this.LabelSUTMaxPDU.Location = new System.Drawing.Point(8, 128);
            this.LabelSUTMaxPDU.Name = "LabelSUTMaxPDU";
            this.LabelSUTMaxPDU.Size = new System.Drawing.Size(100, 32);
            this.LabelSUTMaxPDU.TabIndex = 5;
            this.LabelSUTMaxPDU.Text = "Maximum PDU length to receive:";
            // 
            // LabelSUTAETitle
            // 
            this.LabelSUTAETitle.Location = new System.Drawing.Point(8, 8);
            this.LabelSUTAETitle.Name = "LabelSUTAETitle";
            this.LabelSUTAETitle.TabIndex = 5;
            this.LabelSUTAETitle.Text = "AE Title:";
            // 
            // LabelSUTTCPIPAddress
            // 
            this.LabelSUTTCPIPAddress.Location = new System.Drawing.Point(8, 104);
            this.LabelSUTTCPIPAddress.Name = "LabelSUTTCPIPAddress";
            this.LabelSUTTCPIPAddress.Size = new System.Drawing.Size(100, 32);
            this.LabelSUTTCPIPAddress.TabIndex = 5;
            this.LabelSUTTCPIPAddress.Text = "Remote TCP/IP address:";
            // 
            // LabelSUTListenPort
            // 
            this.LabelSUTListenPort.Location = new System.Drawing.Point(8, 80);
            this.LabelSUTListenPort.Name = "LabelSUTListenPort";
            this.LabelSUTListenPort.TabIndex = 5;
            this.LabelSUTListenPort.Text = "Listen port:";
            // 
            // TextBoxSUTAETitle
            // 
            this.TextBoxSUTAETitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSUTAETitle.Location = new System.Drawing.Point(120, 8);
            this.TextBoxSUTAETitle.MaxLength = 16;
            this.TextBoxSUTAETitle.Name = "TextBoxSUTAETitle";
            this.TextBoxSUTAETitle.Size = new System.Drawing.Size(391, 20);
            this.TextBoxSUTAETitle.TabIndex = 6;
            this.TextBoxSUTAETitle.Text = "";
            this.TextBoxSUTAETitle.TextChanged += new System.EventHandler(this.TextBoxSUTAETitle_TextChanged);
            // 
            // TextBoxSUTTCPIPAddress
            // 
            this.TextBoxSUTTCPIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSUTTCPIPAddress.Location = new System.Drawing.Point(120, 104);
            this.TextBoxSUTTCPIPAddress.MaxLength = 0;
            this.TextBoxSUTTCPIPAddress.Name = "TextBoxSUTTCPIPAddress";
            this.TextBoxSUTTCPIPAddress.Size = new System.Drawing.Size(391, 20);
            this.TextBoxSUTTCPIPAddress.TabIndex = 6;
            this.TextBoxSUTTCPIPAddress.Text = "";
            this.TextBoxSUTTCPIPAddress.TextChanged += new System.EventHandler(this.TextBoxSUTTCPIPAddress_TextChanged);
            // 
            // ButtonCheckTCPIPAddress
            // 
            this.ButtonCheckTCPIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCheckTCPIPAddress.Enabled = false;
            this.ButtonCheckTCPIPAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonCheckTCPIPAddress.Location = new System.Drawing.Point(529, 104);
            this.ButtonCheckTCPIPAddress.Name = "ButtonCheckTCPIPAddress";
            this.ButtonCheckTCPIPAddress.Size = new System.Drawing.Size(112, 24);
            this.ButtonCheckTCPIPAddress.TabIndex = 7;
            this.ButtonCheckTCPIPAddress.Text = "Check addresss";
            this.ButtonCheckTCPIPAddress.Visible = false;
            // 
            // NumericSUTListenPort
            // 
            this.NumericSUTListenPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericSUTListenPort.Location = new System.Drawing.Point(120, 80);
            this.NumericSUTListenPort.Maximum = new System.Decimal(new int[] {
                                                                                 65535,
                                                                                 0,
                                                                                 0,
                                                                                 0});
            this.NumericSUTListenPort.Name = "NumericSUTListenPort";
            this.NumericSUTListenPort.Size = new System.Drawing.Size(391, 20);
            this.NumericSUTListenPort.TabIndex = 7;
            this.NumericSUTListenPort.Leave += new System.EventHandler(this.NumericSUTListenPort_Leave);
            // 
            // NumericSUTMaxPDU
            // 
            this.NumericSUTMaxPDU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericSUTMaxPDU.Increment = new System.Decimal(new int[] {
                                                                               256,
                                                                               0,
                                                                               0,
                                                                               0});
            this.NumericSUTMaxPDU.Location = new System.Drawing.Point(120, 128);
            this.NumericSUTMaxPDU.Maximum = new System.Decimal(new int[] {
                                                                             1048576,
                                                                             0,
                                                                             0,
                                                                             0});
            this.NumericSUTMaxPDU.Name = "NumericSUTMaxPDU";
            this.NumericSUTMaxPDU.Size = new System.Drawing.Size(391, 20);
            this.NumericSUTMaxPDU.TabIndex = 7;
            this.NumericSUTMaxPDU.Leave += new System.EventHandler(this.NumericSUTMaxPDU_Leave);
            // 
            // LabelSUTImplClassUID
            // 
            this.LabelSUTImplClassUID.Location = new System.Drawing.Point(8, 32);
            this.LabelSUTImplClassUID.Name = "LabelSUTImplClassUID";
            this.LabelSUTImplClassUID.TabIndex = 5;
            this.LabelSUTImplClassUID.Text = "Impl. Class UID:";
            // 
            // LabelSUTImplVersionName
            // 
            this.LabelSUTImplVersionName.Location = new System.Drawing.Point(8, 56);
            this.LabelSUTImplVersionName.Name = "LabelSUTImplVersionName";
            this.LabelSUTImplVersionName.Size = new System.Drawing.Size(112, 23);
            this.LabelSUTImplVersionName.TabIndex = 5;
            this.LabelSUTImplVersionName.Text = "Impl. Version Name:";
            // 
            // TextBoxSUTImplClassUID
            // 
            this.TextBoxSUTImplClassUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSUTImplClassUID.Location = new System.Drawing.Point(120, 32);
            this.TextBoxSUTImplClassUID.MaxLength = 64;
            this.TextBoxSUTImplClassUID.Name = "TextBoxSUTImplClassUID";
            this.TextBoxSUTImplClassUID.Size = new System.Drawing.Size(391, 20);
            this.TextBoxSUTImplClassUID.TabIndex = 6;
            this.TextBoxSUTImplClassUID.Text = "";
            this.TextBoxSUTImplClassUID.TextChanged += new System.EventHandler(this.TextBoxSUTImplClassUID_TextChanged);
            // 
            // TextBoxSUTImplVersionName
            // 
            this.TextBoxSUTImplVersionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSUTImplVersionName.Location = new System.Drawing.Point(120, 56);
            this.TextBoxSUTImplVersionName.MaxLength = 16;
            this.TextBoxSUTImplVersionName.Name = "TextBoxSUTImplVersionName";
            this.TextBoxSUTImplVersionName.Size = new System.Drawing.Size(391, 20);
            this.TextBoxSUTImplVersionName.TabIndex = 6;
            this.TextBoxSUTImplVersionName.Text = "";
            this.TextBoxSUTImplVersionName.TextChanged += new System.EventHandler(this.TextBoxSUTImplVersionName_TextChanged);
            // 
            // PanelSUTSettingTitle
            // 
            this.PanelSUTSettingTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PanelSUTSettingTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelSUTSettingTitle.Controls.Add(this.label2);
            this.PanelSUTSettingTitle.Controls.Add(this.MinSUTSettings);
            this.PanelSUTSettingTitle.Controls.Add(this.MaxSUTSettings);
            this.PanelSUTSettingTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSUTSettingTitle.Location = new System.Drawing.Point(15, 544);
            this.PanelSUTSettingTitle.Name = "PanelSUTSettingTitle";
            this.PanelSUTSettingTitle.Size = new System.Drawing.Size(657, 32);
            this.PanelSUTSettingTitle.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "System Under Test Settings";
            // 
            // MinSUTSettings
            // 
            this.MinSUTSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinSUTSettings.Image = ((System.Drawing.Image)(resources.GetObject("MinSUTSettings.Image")));
            this.MinSUTSettings.Location = new System.Drawing.Point(613, 0);
            this.MinSUTSettings.Name = "MinSUTSettings";
            this.MinSUTSettings.Size = new System.Drawing.Size(32, 24);
            this.MinSUTSettings.TabIndex = 1;
            this.MinSUTSettings.TabStop = false;
            this.MinSUTSettings.Click += new System.EventHandler(this.MinSUTSettings_Click);
            // 
            // MaxSUTSettings
            // 
            this.MaxSUTSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxSUTSettings.Image = ((System.Drawing.Image)(resources.GetObject("MaxSUTSettings.Image")));
            this.MaxSUTSettings.Location = new System.Drawing.Point(613, 0);
            this.MaxSUTSettings.Name = "MaxSUTSettings";
            this.MaxSUTSettings.Size = new System.Drawing.Size(32, 24);
            this.MaxSUTSettings.TabIndex = 1;
            this.MaxSUTSettings.TabStop = false;
            this.MaxSUTSettings.Visible = false;
            this.MaxSUTSettings.Click += new System.EventHandler(this.MaxSUTSettings_Click);
            // 
            // PanelDVTRoleSettingsContent
            // 
            this.PanelDVTRoleSettingsContent.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelDVTRoleSettingsContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDVTRoleSettingsContent.Controls.Add(this.NumericDVTListenPort);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.TextBoxDVTAETitle);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.LabelDVTAETitle);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.LabelDVTListenPort);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.LabelDVTSocketTimeOut);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.LabelDVTMaxPDU);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.NumericDVTTimeOut);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.NumericDVTMaxPDU);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.TextBoxDVTImplClassUID);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.TextBoxDVTImplVersionName);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.LabelDVTImplClassUID);
            this.PanelDVTRoleSettingsContent.Controls.Add(this.LabelDVTImplVersionName);
            this.PanelDVTRoleSettingsContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelDVTRoleSettingsContent.Location = new System.Drawing.Point(15, 384);
            this.PanelDVTRoleSettingsContent.Name = "PanelDVTRoleSettingsContent";
            this.PanelDVTRoleSettingsContent.Size = new System.Drawing.Size(657, 160);
            this.PanelDVTRoleSettingsContent.TabIndex = 7;
            // 
            // NumericDVTListenPort
            // 
            this.NumericDVTListenPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericDVTListenPort.Location = new System.Drawing.Point(120, 80);
            this.NumericDVTListenPort.Maximum = new System.Decimal(new int[] {
                                                                                 65535,
                                                                                 0,
                                                                                 0,
                                                                                 0});
            this.NumericDVTListenPort.Name = "NumericDVTListenPort";
            this.NumericDVTListenPort.Size = new System.Drawing.Size(391, 20);
            this.NumericDVTListenPort.TabIndex = 7;
            this.NumericDVTListenPort.Validated += new System.EventHandler(this.NumericDVTListenPort_Validated);
            this.NumericDVTListenPort.ValueChanged += new System.EventHandler(this.NumericDVTListenPort_Validated);
            // 
            // TextBoxDVTAETitle
            // 
            this.TextBoxDVTAETitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDVTAETitle.Location = new System.Drawing.Point(120, 8);
            this.TextBoxDVTAETitle.MaxLength = 16;
            this.TextBoxDVTAETitle.Name = "TextBoxDVTAETitle";
            this.TextBoxDVTAETitle.Size = new System.Drawing.Size(391, 20);
            this.TextBoxDVTAETitle.TabIndex = 6;
            this.TextBoxDVTAETitle.Text = "";
            this.TextBoxDVTAETitle.TextChanged += new System.EventHandler(this.TextBoxDVTAETitle_TextChanged);
            // 
            // LabelDVTAETitle
            // 
            this.LabelDVTAETitle.Location = new System.Drawing.Point(8, 8);
            this.LabelDVTAETitle.Name = "LabelDVTAETitle";
            this.LabelDVTAETitle.TabIndex = 5;
            this.LabelDVTAETitle.Text = "AE Title:";
            // 
            // LabelDVTListenPort
            // 
            this.LabelDVTListenPort.Location = new System.Drawing.Point(8, 80);
            this.LabelDVTListenPort.Name = "LabelDVTListenPort";
            this.LabelDVTListenPort.TabIndex = 5;
            this.LabelDVTListenPort.Text = "Listen port:";
            // 
            // LabelDVTSocketTimeOut
            // 
            this.LabelDVTSocketTimeOut.Location = new System.Drawing.Point(8, 104);
            this.LabelDVTSocketTimeOut.Name = "LabelDVTSocketTimeOut";
            this.LabelDVTSocketTimeOut.TabIndex = 5;
            this.LabelDVTSocketTimeOut.Text = "Socket time-out:";
            // 
            // LabelDVTMaxPDU
            // 
            this.LabelDVTMaxPDU.Location = new System.Drawing.Point(8, 128);
            this.LabelDVTMaxPDU.Name = "LabelDVTMaxPDU";
            this.LabelDVTMaxPDU.Size = new System.Drawing.Size(100, 32);
            this.LabelDVTMaxPDU.TabIndex = 5;
            this.LabelDVTMaxPDU.Text = "Maximum PDU length to receive:";
            // 
            // NumericDVTTimeOut
            // 
            this.NumericDVTTimeOut.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericDVTTimeOut.Increment = new System.Decimal(new int[] {
                                                                                10,
                                                                                0,
                                                                                0,
                                                                                0});
            this.NumericDVTTimeOut.Location = new System.Drawing.Point(120, 104);
            this.NumericDVTTimeOut.Maximum = new System.Decimal(new int[] {
                                                                              3600,
                                                                              0,
                                                                              0,
                                                                              0});
            this.NumericDVTTimeOut.Name = "NumericDVTTimeOut";
            this.NumericDVTTimeOut.Size = new System.Drawing.Size(391, 20);
            this.NumericDVTTimeOut.TabIndex = 7;
            this.NumericDVTTimeOut.Validated += new System.EventHandler(this.NumericDVTTimeOut_Validated);
            this.NumericDVTTimeOut.ValueChanged += new System.EventHandler(this.NumericDVTTimeOut_Validated);
            // 
            // NumericDVTMaxPDU
            // 
            this.NumericDVTMaxPDU.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericDVTMaxPDU.Increment = new System.Decimal(new int[] {
                                                                               256,
                                                                               0,
                                                                               0,
                                                                               0});
            this.NumericDVTMaxPDU.Location = new System.Drawing.Point(120, 128);
            this.NumericDVTMaxPDU.Maximum = new System.Decimal(new int[] {
                                                                             1048576,
                                                                             0,
                                                                             0,
                                                                             0});
            this.NumericDVTMaxPDU.Name = "NumericDVTMaxPDU";
            this.NumericDVTMaxPDU.Size = new System.Drawing.Size(391, 20);
            this.NumericDVTMaxPDU.TabIndex = 7;
            this.NumericDVTMaxPDU.Validated += new System.EventHandler(this.NumericDVTMaxPDU_Validated);
            this.NumericDVTMaxPDU.ValueChanged += new System.EventHandler(this.NumericDVTMaxPDU_Validated);
            // 
            // TextBoxDVTImplClassUID
            // 
            this.TextBoxDVTImplClassUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDVTImplClassUID.Location = new System.Drawing.Point(120, 32);
            this.TextBoxDVTImplClassUID.MaxLength = 64;
            this.TextBoxDVTImplClassUID.Name = "TextBoxDVTImplClassUID";
            this.TextBoxDVTImplClassUID.Size = new System.Drawing.Size(391, 20);
            this.TextBoxDVTImplClassUID.TabIndex = 6;
            this.TextBoxDVTImplClassUID.Text = "";
            this.TextBoxDVTImplClassUID.TextChanged += new System.EventHandler(this.TextBoxDVTImplClassUID_TextChanged);
            // 
            // TextBoxDVTImplVersionName
            // 
            this.TextBoxDVTImplVersionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDVTImplVersionName.Location = new System.Drawing.Point(120, 56);
            this.TextBoxDVTImplVersionName.MaxLength = 16;
            this.TextBoxDVTImplVersionName.Name = "TextBoxDVTImplVersionName";
            this.TextBoxDVTImplVersionName.Size = new System.Drawing.Size(391, 20);
            this.TextBoxDVTImplVersionName.TabIndex = 6;
            this.TextBoxDVTImplVersionName.Text = "";
            this.TextBoxDVTImplVersionName.TextChanged += new System.EventHandler(this.TextBoxDVTImplVersionName_TextChanged);
            // 
            // LabelDVTImplClassUID
            // 
            this.LabelDVTImplClassUID.Location = new System.Drawing.Point(8, 32);
            this.LabelDVTImplClassUID.Name = "LabelDVTImplClassUID";
            this.LabelDVTImplClassUID.TabIndex = 5;
            this.LabelDVTImplClassUID.Text = "Impl. Class UID:";
            // 
            // LabelDVTImplVersionName
            // 
            this.LabelDVTImplVersionName.Location = new System.Drawing.Point(8, 56);
            this.LabelDVTImplVersionName.Name = "LabelDVTImplVersionName";
            this.LabelDVTImplVersionName.Size = new System.Drawing.Size(112, 23);
            this.LabelDVTImplVersionName.TabIndex = 5;
            this.LabelDVTImplVersionName.Text = "Impl. Version Name:";
            // 
            // PanelDVTRoleSettingsTitle
            // 
            this.PanelDVTRoleSettingsTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PanelDVTRoleSettingsTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelDVTRoleSettingsTitle.Controls.Add(this.label1);
            this.PanelDVTRoleSettingsTitle.Controls.Add(this.MinDVTRoleSettings);
            this.PanelDVTRoleSettingsTitle.Controls.Add(this.MaxDVTRoleSettings);
            this.PanelDVTRoleSettingsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelDVTRoleSettingsTitle.Location = new System.Drawing.Point(15, 352);
            this.PanelDVTRoleSettingsTitle.Name = "PanelDVTRoleSettingsTitle";
            this.PanelDVTRoleSettingsTitle.Size = new System.Drawing.Size(657, 32);
            this.PanelDVTRoleSettingsTitle.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "DVT Role Settings";
            // 
            // MinDVTRoleSettings
            // 
            this.MinDVTRoleSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinDVTRoleSettings.Image = ((System.Drawing.Image)(resources.GetObject("MinDVTRoleSettings.Image")));
            this.MinDVTRoleSettings.Location = new System.Drawing.Point(611, 0);
            this.MinDVTRoleSettings.Name = "MinDVTRoleSettings";
            this.MinDVTRoleSettings.Size = new System.Drawing.Size(32, 24);
            this.MinDVTRoleSettings.TabIndex = 1;
            this.MinDVTRoleSettings.TabStop = false;
            this.MinDVTRoleSettings.Click += new System.EventHandler(this.MinDVTRoleSettings_Click);
            // 
            // MaxDVTRoleSettings
            // 
            this.MaxDVTRoleSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxDVTRoleSettings.Image = ((System.Drawing.Image)(resources.GetObject("MaxDVTRoleSettings.Image")));
            this.MaxDVTRoleSettings.Location = new System.Drawing.Point(611, 0);
            this.MaxDVTRoleSettings.Name = "MaxDVTRoleSettings";
            this.MaxDVTRoleSettings.Size = new System.Drawing.Size(32, 24);
            this.MaxDVTRoleSettings.TabIndex = 1;
            this.MaxDVTRoleSettings.TabStop = false;
            this.MaxDVTRoleSettings.Visible = false;
            this.MaxDVTRoleSettings.Click += new System.EventHandler(this.MaxDVTRoleSettings_Click);
            // 
            // PanelGeneralPropertiesContent
            // 
            this.PanelGeneralPropertiesContent.BackColor = System.Drawing.SystemColors.ControlLight;
            this.PanelGeneralPropertiesContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelGeneralPropertiesContent.Controls.Add(this.panel1);
            this.PanelGeneralPropertiesContent.Controls.Add(this.panel2);
            this.PanelGeneralPropertiesContent.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelGeneralPropertiesContent.Location = new System.Drawing.Point(15, 47);
            this.PanelGeneralPropertiesContent.Name = "PanelGeneralPropertiesContent";
            this.PanelGeneralPropertiesContent.Size = new System.Drawing.Size(657, 305);
            this.PanelGeneralPropertiesContent.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.CheckBoxAddGroupLengths);
            this.panel1.Controls.Add(this.CheckBoxDefineSQLength);
            this.panel1.Controls.Add(this.ComboBoxStorageMode);
            this.panel1.Controls.Add(this.CheckBoxLogRelation);
            this.panel1.Controls.Add(this.labelStorageMode);
            this.panel1.Controls.Add(this.CheckBoxGenerateDetailedValidationResults);
            this.panel1.Controls.Add(this.TextBoxSessionType);
            this.panel1.Controls.Add(this.NumericSessonID);
            this.panel1.Controls.Add(this.LabelResultsDir);
            this.panel1.Controls.Add(this.LabelSessionType);
            this.panel1.Controls.Add(this.LabelDate);
            this.panel1.Controls.Add(this.LabelSessionTitle);
            this.panel1.Controls.Add(this.LabelTestedBy);
            this.panel1.Controls.Add(this.TextBoxTestedBy);
            this.panel1.Controls.Add(this.TextBoxResultsRoot);
            this.panel1.Controls.Add(this.TextBoxScriptRoot);
            this.panel1.Controls.Add(this.LabelScriptsDir);
            this.panel1.Controls.Add(this.TextBoxSessionTitle);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.LabelSpecifyTransferSyntaxes);
            this.panel1.Controls.Add(this.LabelDescriptionDir);
            this.panel1.Controls.Add(this.TextBoxDescriptionRoot);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 303);
            this.panel1.TabIndex = 10;
            // 
            // CheckBoxAddGroupLengths
            // 
            this.CheckBoxAddGroupLengths.Location = new System.Drawing.Point(248, 272);
            this.CheckBoxAddGroupLengths.Name = "CheckBoxAddGroupLengths";
            this.CheckBoxAddGroupLengths.Size = new System.Drawing.Size(136, 24);
            this.CheckBoxAddGroupLengths.TabIndex = 39;
            this.CheckBoxAddGroupLengths.Text = "Add Group Lengths";
            this.CheckBoxAddGroupLengths.CheckedChanged += new System.EventHandler(this.CheckBoxAddGroupLengths_CheckedChanged);
            // 
            // CheckBoxDefineSQLength
            // 
            this.CheckBoxDefineSQLength.Location = new System.Drawing.Point(248, 248);
            this.CheckBoxDefineSQLength.Name = "CheckBoxDefineSQLength";
            this.CheckBoxDefineSQLength.Size = new System.Drawing.Size(120, 24);
            this.CheckBoxDefineSQLength.TabIndex = 38;
            this.CheckBoxDefineSQLength.Text = "Define SQ Length";
            this.CheckBoxDefineSQLength.CheckedChanged += new System.EventHandler(this.CheckBoxDefineSQLength_CheckedChanged);
            // 
            // ComboBoxStorageMode
            // 
            this.ComboBoxStorageMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxStorageMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxStorageMode.Items.AddRange(new object[] {
                                                                     "As Data Set",
                                                                     "As Media",
                                                                     "No Storage"});
            this.ComboBoxStorageMode.Location = new System.Drawing.Point(120, 224);
            this.ComboBoxStorageMode.Name = "ComboBoxStorageMode";
            this.ComboBoxStorageMode.Size = new System.Drawing.Size(391, 21);
            this.ComboBoxStorageMode.TabIndex = 36;
            this.ComboBoxStorageMode.SelectedIndexChanged += new System.EventHandler(this.ComboBoxStorageMode_SelectedIndexChanged);
            // 
            // CheckBoxLogRelation
            // 
            this.CheckBoxLogRelation.Location = new System.Drawing.Point(8, 248);
            this.CheckBoxLogRelation.Name = "CheckBoxLogRelation";
            this.CheckBoxLogRelation.TabIndex = 35;
            this.CheckBoxLogRelation.Text = "Log Relation";
            this.CheckBoxLogRelation.CheckedChanged += new System.EventHandler(this.CheckBoxLogRelation_CheckedChanged);
            // 
            // labelStorageMode
            // 
            this.labelStorageMode.Location = new System.Drawing.Point(8, 224);
            this.labelStorageMode.Name = "labelStorageMode";
            this.labelStorageMode.TabIndex = 34;
            this.labelStorageMode.Text = "Storage Mode:";
            // 
            // CheckBoxGenerateDetailedValidationResults
            // 
            this.CheckBoxGenerateDetailedValidationResults.Location = new System.Drawing.Point(8, 272);
            this.CheckBoxGenerateDetailedValidationResults.Name = "CheckBoxGenerateDetailedValidationResults";
            this.CheckBoxGenerateDetailedValidationResults.Size = new System.Drawing.Size(208, 23);
            this.CheckBoxGenerateDetailedValidationResults.TabIndex = 32;
            this.CheckBoxGenerateDetailedValidationResults.Text = "Generate detailed validation results";
            this.CheckBoxGenerateDetailedValidationResults.CheckedChanged += new System.EventHandler(this.CheckBoxGenerateDetailedValidationResults_CheckedChanged);
            // 
            // TextBoxSessionType
            // 
            this.TextBoxSessionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSessionType.Location = new System.Drawing.Point(120, 8);
            this.TextBoxSessionType.MaxLength = 0;
            this.TextBoxSessionType.Name = "TextBoxSessionType";
            this.TextBoxSessionType.ReadOnly = true;
            this.TextBoxSessionType.Size = new System.Drawing.Size(391, 20);
            this.TextBoxSessionType.TabIndex = 31;
            this.TextBoxSessionType.Text = "";
            // 
            // NumericSessonID
            // 
            this.NumericSessonID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.NumericSessonID.Location = new System.Drawing.Point(120, 56);
            this.NumericSessonID.Maximum = new System.Decimal(new int[] {
                                                                            999,
                                                                            0,
                                                                            0,
                                                                            0});
            this.NumericSessonID.Name = "NumericSessonID";
            this.NumericSessonID.Size = new System.Drawing.Size(391, 20);
            this.NumericSessonID.TabIndex = 29;
            this.NumericSessonID.Validating += new System.ComponentModel.CancelEventHandler(this.NumericSessonID_Validating);
            this.NumericSessonID.ValueChanged += new System.EventHandler(this.NumericSessonID_Validated);
            // 
            // LabelResultsDir
            // 
            this.LabelResultsDir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelResultsDir.Location = new System.Drawing.Point(8, 104);
            this.LabelResultsDir.Name = "LabelResultsDir";
            this.LabelResultsDir.TabIndex = 25;
            this.LabelResultsDir.Text = "Results dir:";
            // 
            // LabelSessionType
            // 
            this.LabelSessionType.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelSessionType.Location = new System.Drawing.Point(8, 8);
            this.LabelSessionType.Name = "LabelSessionType";
            this.LabelSessionType.TabIndex = 14;
            this.LabelSessionType.Text = "Session type:";
            // 
            // LabelDate
            // 
            this.LabelDate.Location = new System.Drawing.Point(0, 0);
            this.LabelDate.Name = "LabelDate";
            this.LabelDate.TabIndex = 37;
            // 
            // LabelSessionTitle
            // 
            this.LabelSessionTitle.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelSessionTitle.Location = new System.Drawing.Point(8, 32);
            this.LabelSessionTitle.Name = "LabelSessionTitle";
            this.LabelSessionTitle.TabIndex = 16;
            this.LabelSessionTitle.Text = "Session title:";
            // 
            // LabelTestedBy
            // 
            this.LabelTestedBy.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelTestedBy.Location = new System.Drawing.Point(8, 80);
            this.LabelTestedBy.Name = "LabelTestedBy";
            this.LabelTestedBy.TabIndex = 15;
            this.LabelTestedBy.Text = "Tested by:";
            // 
            // TextBoxTestedBy
            // 
            this.TextBoxTestedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxTestedBy.Location = new System.Drawing.Point(120, 80);
            this.TextBoxTestedBy.MaxLength = 0;
            this.TextBoxTestedBy.Name = "TextBoxTestedBy";
            this.TextBoxTestedBy.Size = new System.Drawing.Size(391, 20);
            this.TextBoxTestedBy.TabIndex = 23;
            this.TextBoxTestedBy.Text = "";
            this.TextBoxTestedBy.TextChanged += new System.EventHandler(this.TextBoxTestedBy_TextChanged);
            // 
            // TextBoxResultsRoot
            // 
            this.TextBoxResultsRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxResultsRoot.Location = new System.Drawing.Point(120, 104);
            this.TextBoxResultsRoot.MaxLength = 0;
            this.TextBoxResultsRoot.Name = "TextBoxResultsRoot";
            this.TextBoxResultsRoot.ReadOnly = true;
            this.TextBoxResultsRoot.Size = new System.Drawing.Size(391, 20);
            this.TextBoxResultsRoot.TabIndex = 22;
            this.TextBoxResultsRoot.Text = "";
            // 
            // TextBoxScriptRoot
            // 
            this.TextBoxScriptRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxScriptRoot.Location = new System.Drawing.Point(120, 128);
            this.TextBoxScriptRoot.MaxLength = 0;
            this.TextBoxScriptRoot.Name = "TextBoxScriptRoot";
            this.TextBoxScriptRoot.ReadOnly = true;
            this.TextBoxScriptRoot.Size = new System.Drawing.Size(391, 20);
            this.TextBoxScriptRoot.TabIndex = 19;
            this.TextBoxScriptRoot.Text = "";
            // 
            // LabelScriptsDir
            // 
            this.LabelScriptsDir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelScriptsDir.Location = new System.Drawing.Point(8, 128);
            this.LabelScriptsDir.Name = "LabelScriptsDir";
            this.LabelScriptsDir.TabIndex = 26;
            this.LabelScriptsDir.Text = "Scripts dir:";
            // 
            // TextBoxSessionTitle
            // 
            this.TextBoxSessionTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxSessionTitle.Location = new System.Drawing.Point(120, 32);
            this.TextBoxSessionTitle.MaxLength = 0;
            this.TextBoxSessionTitle.Name = "TextBoxSessionTitle";
            this.TextBoxSessionTitle.Size = new System.Drawing.Size(391, 20);
            this.TextBoxSessionTitle.TabIndex = 21;
            this.TextBoxSessionTitle.Text = "";
            this.TextBoxSessionTitle.TextChanged += new System.EventHandler(this.TextBoxSessionTitle_TextChanged);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.ControlLight;
            this.label7.Location = new System.Drawing.Point(8, 56);
            this.label7.Name = "label7";
            this.label7.TabIndex = 17;
            this.label7.Text = "Session ID:";
            // 
            // LabelSpecifyTransferSyntaxes
            // 
            this.LabelSpecifyTransferSyntaxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelSpecifyTransferSyntaxes.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelSpecifyTransferSyntaxes.Location = new System.Drawing.Point(8, 200);
            this.LabelSpecifyTransferSyntaxes.Name = "LabelSpecifyTransferSyntaxes";
            this.LabelSpecifyTransferSyntaxes.Size = new System.Drawing.Size(511, 23);
            this.LabelSpecifyTransferSyntaxes.TabIndex = 27;
            this.LabelSpecifyTransferSyntaxes.Text = "(Un)select the supported Transfer Syntaxes:";
            // 
            // LabelDescriptionDir
            // 
            this.LabelDescriptionDir.BackColor = System.Drawing.SystemColors.ControlLight;
            this.LabelDescriptionDir.Location = new System.Drawing.Point(8, 152);
            this.LabelDescriptionDir.Name = "LabelDescriptionDir";
            this.LabelDescriptionDir.TabIndex = 24;
            this.LabelDescriptionDir.Text = "Description dir:";
            // 
            // TextBoxDescriptionRoot
            // 
            this.TextBoxDescriptionRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TextBoxDescriptionRoot.Location = new System.Drawing.Point(120, 152);
            this.TextBoxDescriptionRoot.MaxLength = 0;
            this.TextBoxDescriptionRoot.Name = "TextBoxDescriptionRoot";
            this.TextBoxDescriptionRoot.ReadOnly = true;
            this.TextBoxDescriptionRoot.Size = new System.Drawing.Size(391, 20);
            this.TextBoxDescriptionRoot.TabIndex = 20;
            this.TextBoxDescriptionRoot.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ButtonBrowseResultsDir);
            this.panel2.Controls.Add(this.ButtonBrowseScriptsDir);
            this.panel2.Controls.Add(this.ButtonSpecifyTransferSyntaxes);
            this.panel2.Controls.Add(this.ButtonBrowseDescriptionDir);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(535, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(120, 303);
            this.panel2.TabIndex = 11;
            // 
            // ButtonBrowseResultsDir
            // 
            this.ButtonBrowseResultsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBrowseResultsDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonBrowseResultsDir.Location = new System.Drawing.Point(4, 104);
            this.ButtonBrowseResultsDir.Name = "ButtonBrowseResultsDir";
            this.ButtonBrowseResultsDir.Size = new System.Drawing.Size(112, 24);
            this.ButtonBrowseResultsDir.TabIndex = 13;
            this.ButtonBrowseResultsDir.Text = "Browse";
            this.ButtonBrowseResultsDir.Click += new System.EventHandler(this.ButtonBrowseResultsDir_Click);
            // 
            // ButtonBrowseScriptsDir
            // 
            this.ButtonBrowseScriptsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBrowseScriptsDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonBrowseScriptsDir.Location = new System.Drawing.Point(4, 128);
            this.ButtonBrowseScriptsDir.Name = "ButtonBrowseScriptsDir";
            this.ButtonBrowseScriptsDir.Size = new System.Drawing.Size(112, 24);
            this.ButtonBrowseScriptsDir.TabIndex = 14;
            this.ButtonBrowseScriptsDir.Text = "Browse";
            this.ButtonBrowseScriptsDir.Click += new System.EventHandler(this.ButtonBrowseScriptsDir_Click);
            // 
            // ButtonSpecifyTransferSyntaxes
            // 
            this.ButtonSpecifyTransferSyntaxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonSpecifyTransferSyntaxes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonSpecifyTransferSyntaxes.Location = new System.Drawing.Point(4, 200);
            this.ButtonSpecifyTransferSyntaxes.Name = "ButtonSpecifyTransferSyntaxes";
            this.ButtonSpecifyTransferSyntaxes.Size = new System.Drawing.Size(112, 24);
            this.ButtonSpecifyTransferSyntaxes.TabIndex = 10;
            this.ButtonSpecifyTransferSyntaxes.Text = "Specify TS";
            this.ButtonSpecifyTransferSyntaxes.Click += new System.EventHandler(this.ButtonSpecifyTransferSyntaxes_Click);
            // 
            // ButtonBrowseDescriptionDir
            // 
            this.ButtonBrowseDescriptionDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonBrowseDescriptionDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ButtonBrowseDescriptionDir.Location = new System.Drawing.Point(4, 152);
            this.ButtonBrowseDescriptionDir.Name = "ButtonBrowseDescriptionDir";
            this.ButtonBrowseDescriptionDir.Size = new System.Drawing.Size(112, 24);
            this.ButtonBrowseDescriptionDir.TabIndex = 11;
            this.ButtonBrowseDescriptionDir.Text = "Browse";
            this.ButtonBrowseDescriptionDir.Click += new System.EventHandler(this.ButtonBrowseDescriptionDir_Click);
            // 
            // PanelGeneralPropertiesTitle
            // 
            this.PanelGeneralPropertiesTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.PanelGeneralPropertiesTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelGeneralPropertiesTitle.Controls.Add(this.label5);
            this.PanelGeneralPropertiesTitle.Controls.Add(this.MinGSPSettings);
            this.PanelGeneralPropertiesTitle.Controls.Add(this.MaxGSPSettings);
            this.PanelGeneralPropertiesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelGeneralPropertiesTitle.Location = new System.Drawing.Point(15, 15);
            this.PanelGeneralPropertiesTitle.Name = "PanelGeneralPropertiesTitle";
            this.PanelGeneralPropertiesTitle.Size = new System.Drawing.Size(657, 32);
            this.PanelGeneralPropertiesTitle.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label5.Location = new System.Drawing.Point(8, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "General Session Properties";
            // 
            // MinGSPSettings
            // 
            this.MinGSPSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MinGSPSettings.Image = ((System.Drawing.Image)(resources.GetObject("MinGSPSettings.Image")));
            this.MinGSPSettings.Location = new System.Drawing.Point(611, 0);
            this.MinGSPSettings.Name = "MinGSPSettings";
            this.MinGSPSettings.Size = new System.Drawing.Size(32, 24);
            this.MinGSPSettings.TabIndex = 1;
            this.MinGSPSettings.TabStop = false;
            this.MinGSPSettings.Click += new System.EventHandler(this.MinGSPSettings_Click);
            // 
            // MaxGSPSettings
            // 
            this.MaxGSPSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MaxGSPSettings.Image = ((System.Drawing.Image)(resources.GetObject("MaxGSPSettings.Image")));
            this.MaxGSPSettings.Location = new System.Drawing.Point(611, 0);
            this.MaxGSPSettings.Name = "MaxGSPSettings";
            this.MaxGSPSettings.Size = new System.Drawing.Size(32, 24);
            this.MaxGSPSettings.TabIndex = 1;
            this.MaxGSPSettings.TabStop = false;
            this.MaxGSPSettings.Visible = false;
            this.MaxGSPSettings.Click += new System.EventHandler(this.MaxGSPSettings_Click);
            // 
            // TabSpecifySopClasses
            // 
            this.TabSpecifySopClasses.Controls.Add(this.DataGridSpecifySopClasses);
            this.TabSpecifySopClasses.Controls.Add(this.RichTextBoxSpecifySopClassesInfo);
            this.TabSpecifySopClasses.Controls.Add(this.panel3);
            this.TabSpecifySopClasses.Controls.Add(this.panel5);
            this.TabSpecifySopClasses.Location = new System.Drawing.Point(4, 22);
            this.TabSpecifySopClasses.Name = "TabSpecifySopClasses";
            this.TabSpecifySopClasses.Size = new System.Drawing.Size(704, 668);
            this.TabSpecifySopClasses.TabIndex = 4;
            this.TabSpecifySopClasses.Text = "Specify SOP Classes";
            // 
            // DataGridSpecifySopClasses
            // 
            this.DataGridSpecifySopClasses.DataMember = "";
            this.DataGridSpecifySopClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGridSpecifySopClasses.HeaderForeColor = System.Drawing.SystemColors.ControlText;
            this.DataGridSpecifySopClasses.Location = new System.Drawing.Point(0, 152);
            this.DataGridSpecifySopClasses.Name = "DataGridSpecifySopClasses";
            this.DataGridSpecifySopClasses.Size = new System.Drawing.Size(704, 464);
            this.DataGridSpecifySopClasses.TabIndex = 6;
            this.DataGridSpecifySopClasses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DataGridSpecifySopClasses_MouseDown);
            this.DataGridSpecifySopClasses.MouseUp += new System.Windows.Forms.MouseEventHandler(this.DataGridSpecifySopClasses_MouseUp);
            this.DataGridSpecifySopClasses.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DataGridSpecifySopClasses_MouseMove);
            // 
            // RichTextBoxSpecifySopClassesInfo
            // 
            this.RichTextBoxSpecifySopClassesInfo.ContextMenu = this.ContextMenuRichTextBox;
            this.RichTextBoxSpecifySopClassesInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.RichTextBoxSpecifySopClassesInfo.Location = new System.Drawing.Point(0, 616);
            this.RichTextBoxSpecifySopClassesInfo.Name = "RichTextBoxSpecifySopClassesInfo";
            this.RichTextBoxSpecifySopClassesInfo.Size = new System.Drawing.Size(704, 52);
            this.RichTextBoxSpecifySopClassesInfo.TabIndex = 5;
            this.RichTextBoxSpecifySopClassesInfo.Text = "";
            // 
            // ContextMenuRichTextBox
            // 
            this.ContextMenuRichTextBox.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                   this.ContextMenu_Copy});
            this.ContextMenuRichTextBox.Popup += new System.EventHandler(this.ContextMenuRichTextBox_Popup);
            // 
            // ContextMenu_Copy
            // 
            this.ContextMenu_Copy.Index = 0;
            this.ContextMenu_Copy.Text = "Copy";
            this.ContextMenu_Copy.Click += new System.EventHandler(this.ContextMenu_Copy_Click);
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.ButtonSpecifySopClassesAddDirectory);
            this.panel3.Controls.Add(this.ButtonSpecifySopClassesRemoveDirectory);
            this.panel3.Location = new System.Drawing.Point(600, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(104, 152);
            this.panel3.TabIndex = 13;
            // 
            // ButtonSpecifySopClassesAddDirectory
            // 
            this.ButtonSpecifySopClassesAddDirectory.Location = new System.Drawing.Point(16, 24);
            this.ButtonSpecifySopClassesAddDirectory.Name = "ButtonSpecifySopClassesAddDirectory";
            this.ButtonSpecifySopClassesAddDirectory.TabIndex = 11;
            this.ButtonSpecifySopClassesAddDirectory.Text = "Add";
            this.ButtonSpecifySopClassesAddDirectory.Click += new System.EventHandler(this.ButtonSpecifySopClassesAddDirectory_Click);
            // 
            // ButtonSpecifySopClassesRemoveDirectory
            // 
            this.ButtonSpecifySopClassesRemoveDirectory.Location = new System.Drawing.Point(16, 64);
            this.ButtonSpecifySopClassesRemoveDirectory.Name = "ButtonSpecifySopClassesRemoveDirectory";
            this.ButtonSpecifySopClassesRemoveDirectory.TabIndex = 12;
            this.ButtonSpecifySopClassesRemoveDirectory.Text = "Remove";
            this.ButtonSpecifySopClassesRemoveDirectory.Click += new System.EventHandler(this.ButtonSpecifySopClassesRemoveDirectory_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.ListBoxSpecifySopClassesDefinitionFileDirectories);
            this.panel5.Controls.Add(this.LabelSpecifySopClassesDefinitionFileDirectories);
            this.panel5.Controls.Add(this.LabelSpecifySopClassesSelectAeTitle);
            this.panel5.Controls.Add(this.ComboBoxSpecifySopClassesAeTitle);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(704, 152);
            this.panel5.TabIndex = 14;
            // 
            // ListBoxSpecifySopClassesDefinitionFileDirectories
            // 
            this.ListBoxSpecifySopClassesDefinitionFileDirectories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.ListBoxSpecifySopClassesDefinitionFileDirectories.Location = new System.Drawing.Point(184, 16);
            this.ListBoxSpecifySopClassesDefinitionFileDirectories.Name = "ListBoxSpecifySopClassesDefinitionFileDirectories";
            this.ListBoxSpecifySopClassesDefinitionFileDirectories.Size = new System.Drawing.Size(400, 69);
            this.ListBoxSpecifySopClassesDefinitionFileDirectories.TabIndex = 14;
            this.ListBoxSpecifySopClassesDefinitionFileDirectories.SelectedIndexChanged += new System.EventHandler(this.ListBoxSpecifySopClassesDefinitionFileDirectories_SelectedIndexChanged);
            // 
            // LabelSpecifySopClassesDefinitionFileDirectories
            // 
            this.LabelSpecifySopClassesDefinitionFileDirectories.Location = new System.Drawing.Point(16, 24);
            this.LabelSpecifySopClassesDefinitionFileDirectories.Name = "LabelSpecifySopClassesDefinitionFileDirectories";
            this.LabelSpecifySopClassesDefinitionFileDirectories.Size = new System.Drawing.Size(128, 23);
            this.LabelSpecifySopClassesDefinitionFileDirectories.TabIndex = 13;
            this.LabelSpecifySopClassesDefinitionFileDirectories.Text = "Definition file directories:";
            // 
            // LabelSpecifySopClassesSelectAeTitle
            // 
            this.LabelSpecifySopClassesSelectAeTitle.Location = new System.Drawing.Point(16, 120);
            this.LabelSpecifySopClassesSelectAeTitle.Name = "LabelSpecifySopClassesSelectAeTitle";
            this.LabelSpecifySopClassesSelectAeTitle.Size = new System.Drawing.Size(160, 23);
            this.LabelSpecifySopClassesSelectAeTitle.TabIndex = 12;
            this.LabelSpecifySopClassesSelectAeTitle.Text = "Select AE title - version to use";
            // 
            // ComboBoxSpecifySopClassesAeTitle
            // 
            this.ComboBoxSpecifySopClassesAeTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.ComboBoxSpecifySopClassesAeTitle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxSpecifySopClassesAeTitle.Location = new System.Drawing.Point(184, 120);
            this.ComboBoxSpecifySopClassesAeTitle.Name = "ComboBoxSpecifySopClassesAeTitle";
            this.ComboBoxSpecifySopClassesAeTitle.Size = new System.Drawing.Size(400, 20);
            this.ComboBoxSpecifySopClassesAeTitle.TabIndex = 11;
            this.ComboBoxSpecifySopClassesAeTitle.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSpecifySopClassesAeTitle_SelectedIndexChanged);
            // 
            // TabActivityLogging
            // 
            this.TabActivityLogging.Controls.Add(this.RichTextBoxActivityLogging);
            this.TabActivityLogging.DockPadding.All = 15;
            this.TabActivityLogging.Location = new System.Drawing.Point(4, 22);
            this.TabActivityLogging.Name = "TabActivityLogging";
            this.TabActivityLogging.Size = new System.Drawing.Size(704, 668);
            this.TabActivityLogging.TabIndex = 1;
            this.TabActivityLogging.Text = " Activity Logging ";
            // 
            // RichTextBoxActivityLogging
            // 
            this.RichTextBoxActivityLogging.ContextMenu = this.ContextMenuRichTextBox;
            this.RichTextBoxActivityLogging.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBoxActivityLogging.HideSelection = false;
            this.RichTextBoxActivityLogging.Location = new System.Drawing.Point(15, 15);
            this.RichTextBoxActivityLogging.Name = "RichTextBoxActivityLogging";
            this.RichTextBoxActivityLogging.Size = new System.Drawing.Size(674, 638);
            this.RichTextBoxActivityLogging.TabIndex = 8;
            this.RichTextBoxActivityLogging.Text = "";
            // 
            // TabValidationResults
            // 
            this.TabValidationResults.Controls.Add(this.WebDescriptionView);
            this.TabValidationResults.DockPadding.All = 15;
            this.TabValidationResults.Location = new System.Drawing.Point(4, 22);
            this.TabValidationResults.Name = "TabValidationResults";
            this.TabValidationResults.Size = new System.Drawing.Size(704, 668);
            this.TabValidationResults.TabIndex = 2;
            this.TabValidationResults.Text = "Validation Results";
            // 
            // WebDescriptionView
            // 
            this.WebDescriptionView.ContainingControl = this;
            this.WebDescriptionView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WebDescriptionView.Enabled = true;
            this.WebDescriptionView.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.WebDescriptionView.Location = new System.Drawing.Point(15, 15);
            this.WebDescriptionView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebDescriptionView.OcxState")));
            this.WebDescriptionView.Size = new System.Drawing.Size(674, 638);
            this.WebDescriptionView.TabIndex = 12;
            this.WebDescriptionView.CommandStateChange += new AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEventHandler(this.WebDescriptionView_CommandStateChange);
            this.WebDescriptionView.NavigateComplete2 += new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(this.WebDescriptionView_NavigateComplete2);
            this.WebDescriptionView.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(this.WebDescriptionView_DocumentComplete);
            this.WebDescriptionView.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(this.WebDescriptionView_BeforeNavigate2);
            // 
            // TabScript
            // 
            this.TabScript.Controls.Add(this.RichTextBoxScript);
            this.TabScript.Controls.Add(this.axWebBrowserScript);
            this.TabScript.Location = new System.Drawing.Point(4, 22);
            this.TabScript.Name = "TabScript";
            this.TabScript.Size = new System.Drawing.Size(704, 668);
            this.TabScript.TabIndex = 3;
            this.TabScript.Text = "      Script      ";
            // 
            // RichTextBoxScript
            // 
            this.RichTextBoxScript.ContextMenu = this.ContextMenuRichTextBox;
            this.RichTextBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RichTextBoxScript.Location = new System.Drawing.Point(0, 0);
            this.RichTextBoxScript.Name = "RichTextBoxScript";
            this.RichTextBoxScript.ReadOnly = true;
            this.RichTextBoxScript.Size = new System.Drawing.Size(704, 668);
            this.RichTextBoxScript.TabIndex = 14;
            this.RichTextBoxScript.Text = "";
            // 
            // axWebBrowserScript
            // 
            this.axWebBrowserScript.ContainingControl = this;
            this.axWebBrowserScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axWebBrowserScript.Enabled = true;
            this.axWebBrowserScript.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.axWebBrowserScript.Location = new System.Drawing.Point(0, 0);
            this.axWebBrowserScript.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowserScript.OcxState")));
            this.axWebBrowserScript.Size = new System.Drawing.Size(704, 668);
            this.axWebBrowserScript.TabIndex = 13;
            this.axWebBrowserScript.CommandStateChange += new AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEventHandler(this.axWebBrowserScript_CommandStateChange);
            this.axWebBrowserScript.NavigateComplete2 += new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(this.axWebBrowserScript_NavigateComplete2);
            this.axWebBrowserScript.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(this.axWebBrowserScript_BeforeNavigate2);
            // 
            // TabNoInformationAvailable
            // 
            this.TabNoInformationAvailable.Controls.Add(this.LabelNoInformationAvailable);
            this.TabNoInformationAvailable.Location = new System.Drawing.Point(4, 22);
            this.TabNoInformationAvailable.Name = "TabNoInformationAvailable";
            this.TabNoInformationAvailable.Size = new System.Drawing.Size(704, 668);
            this.TabNoInformationAvailable.TabIndex = 5;
            // 
            // LabelNoInformationAvailable
            // 
            this.LabelNoInformationAvailable.Location = new System.Drawing.Point(296, 32);
            this.LabelNoInformationAvailable.Name = "LabelNoInformationAvailable";
            this.LabelNoInformationAvailable.Size = new System.Drawing.Size(144, 16);
            this.LabelNoInformationAvailable.TabIndex = 0;
            this.LabelNoInformationAvailable.Text = "No information available";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(256, 0);
            this.splitter1.MinSize = 200;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 694);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // ProjectForm2
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(968, 694);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.SessionTreeView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProjectForm2";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.ProjectForm2_Closing);
            this.Activated += new System.EventHandler(this.ProjectForm2_Activated);
            this.TabControl.ResumeLayout(false);
            this.TabSessionInformation.ResumeLayout(false);
            this.PanelSecuritySettingsContent.ResumeLayout(false);
            this.GroupSecurityFiles.ResumeLayout(false);
            this.GroupSecurityVersion.ResumeLayout(false);
            this.GroupSecurityKeyExchange.ResumeLayout(false);
            this.GroupSecurityGeneral.ResumeLayout(false);
            this.GroupSecurityEncryption.ResumeLayout(false);
            this.GroupSecurityDataIntegrity.ResumeLayout(false);
            this.GroupSecurityAuthentication.ResumeLayout(false);
            this.PanelSecuritySettingsTitle.ResumeLayout(false);
            this.PanelSUTSettingContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericSUTListenPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericSUTMaxPDU)).EndInit();
            this.PanelSUTSettingTitle.ResumeLayout(false);
            this.PanelDVTRoleSettingsContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericDVTListenPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDVTTimeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumericDVTMaxPDU)).EndInit();
            this.PanelDVTRoleSettingsTitle.ResumeLayout(false);
            this.PanelGeneralPropertiesContent.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.NumericSessonID)).EndInit();
            this.panel2.ResumeLayout(false);
            this.PanelGeneralPropertiesTitle.ResumeLayout(false);
            this.TabSpecifySopClasses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DataGridSpecifySopClasses)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.TabActivityLogging.ResumeLayout(false);
            this.TabValidationResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WebDescriptionView)).EndInit();
            this.TabScript.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWebBrowserScript)).EndInit();
            this.TabNoInformationAvailable.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion


		public void Notify(object theEvent)
		{
			if (theEvent is EndExecution)
			{
				_State = ProjectFormState.IDLE;
			}

			if (theEvent is StartExecution)
			{
				if (_SessionTreeViewManager.GetSelectedTreeNodeTag() is ScriptFileTag)
				{
					_State = ProjectFormState.EXECUTING_SCRIPT;
				}
				else if (_SessionTreeViewManager.GetSelectedTreeNodeTag() is EmulatorTag)
				{
					EmulatorTag theEmulatorTag = (EmulatorTag)_SessionTreeViewManager.GetSelectedTreeNodeTag();

					switch(theEmulatorTag._EmulatorType)
					{
						case EmulatorTag.EmulatorType.PRINT_SCP:
							_State = ProjectFormState.EXECUTING_PRINT_SCP;
							break;

						case EmulatorTag.EmulatorType.STORAGE_SCP:
							_State = ProjectFormState.EXECUTING_STORAGE_SCP;
							break;

						case EmulatorTag.EmulatorType.STORAGE_SCU:
							_State = ProjectFormState.EXECUTING_STORAGE_SCU;
							break;
					}
				}
				else if (_SessionTreeViewManager.GetSelectedTreeNodeTag() is MediaSessionTag)
				{
					_State = ProjectFormState.EXECUTING_MEDIA_VALIDATION;
				}
				else
				{
					// Sanity check.
					throw new System.ApplicationException("Not supposed to get here.");
				}
			}			

			if (this.ParentForm is MainForm)
			{
				((MainForm)this.ParentForm).Notify(this, theEvent);
			}
		}

		public void Update(object theSource, object theEvent)
		{
			// This event is required to get the focus on selected project form view
			// from multiple views of project form
			this.Enter += new EventHandler(ProjectForm2_Enter);

			// Update the Session Tree View.
			_SessionTreeViewManager.Update(theSource, theEvent);

			// Update the Tab Control.
			TCM_Update(theSource, theEvent);
			
			// Update the text (caption) of this Project Form for certain events.
			if ( (theEvent is SessionTreeViewSelectionChange) ||
				(theEvent is SessionRemovedEvent)
				)
			{
				if (GetSelectedSession() == null)
				{
					Text = "";
				}
				else
				{
					Text = GetSelectedSession().SessionFileName;
				}
			}
		}

		public void TCM_Update(object theSource, object theEvent)
		{
			_TCM_UpdateCount++;

			if (theEvent is UpdateAll)
			{
				// Don't do anything.
				// The Session tree view will be completely updated, and
				// as an indirect result, this tab control will be updated
				// (if another tree view node is selected).
			}

			if (theEvent is SessionTreeViewSelectionChange)
			{
				// Update the tab control only if this event is from the
				// associated tree view.
				if (theSource == this)
				{		
					_SopClassesManager.RemoveDynamicDataBinding();

					TCM_UpdateTabsShown();

					TCM_UpdateTabContents();
				}
			}

			if (theEvent is SessionChange)
			{			
				SessionChange theSessionChange = (SessionChange)theEvent;

				// The contents of a text box or numeric control has changed directly by the user.
				if (theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.OTHER)
				{
					// Invalidate the session used for updating the Session Information tab only if 
					// this event is from another tab control.
					if (theSource != this)
					{
						if (_TCM_SessionUsedForContentsOfTabSessionInformation == theSessionChange.Session)
						{
							// Invalidate this session, so the tab control will redraw when set as the active tab control.
							_TCM_SessionUsedForContentsOfTabSessionInformation = null;
						}
					}
				}

				// The session has changed by something else then a direct contents change of a text box or numeric control.
				if ( (theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.DESCRIPTION_DIR) ||
					(theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.RESULTS_DIR ) ||
					(theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.SCRIPTS_DIR ) )
				{
					if (_TCM_SessionUsedForContentsOfTabSessionInformation == theSessionChange.Session)
					{
						// Invalidate this session, so the tab control will redraw when set as the active tab control.
						_TCM_SessionUsedForContentsOfTabSessionInformation = null;
					}			
				}

				// The SOP classes manager is only interested in changes of SOP classes related settings.
				// This is not important for the other tabs.
				if (theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.SOP_CLASSES_OTHER)
				{
					_SopClassesManager.SetSessionChanged(theSessionChange.Session);
				}

				// The SOP classes manager is only interested in changes of SOP classes related settings.
				// This is not important for the other tabs.
				if (theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.SOP_CLASSES_AE_TITLE_VERSION)
				{
					if (theSource != this)
					{
						_SopClassesManager.SetSessionChanged(theSessionChange.Session);
					}
				}

				// The SOP classes manager is only interested in changes of SOP classes related settings.
				// This is not important for the other tabs.
				if (theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.SOP_CLASSES_LOADED_STATE)
				{
					if (theSource != this)
					{
						_SopClassesManager.SetSessionChanged(theSessionChange.Session);
					}
				}

				// Update the current visible tab control, only if necessary.
				TCM_UpdateTabContents();
			}

			if (theEvent is StartExecution)
			{
				if (theSource == this)
				{
					StartExecution theStartExecution = (StartExecution)theEvent;
					TreeNodeTag theTreeNodeTag = (TreeNodeTag)theStartExecution.TreeNode.Tag;

					TCM_ClearActivityLogging();

					theTreeNodeTag._Session.ActivityReportEvent += _ActivityReportEventHandler;

					// For some strange reason, if the session tree control is disabled and this statement
					// is not present, switching to another application and back will result in a disabled
					// application (when the StartEmulatorExecution is received).
					TabControl.Focus();
				}
			}

			if (theEvent is EndExecution)
			{
				if (theSource == this)
				{
					EndExecution theEndExecution = (EndExecution)theEvent;

					theEndExecution._Tag._Session.ActivityReportEvent -= _ActivityReportEventHandler;
				}
			}


			if (theEvent is SessionRemovedEvent)
			{		
				TCM_UpdateTabsShown();

				TCM_UpdateTabContents();
			}

			_TCM_UpdateCount--;
		}

		#region Min and max handling

		private void MinGSPSettings_Click(object sender, System.EventArgs e)
		{
			MinGSPSettings.Visible = false;
			MaxGSPSettings.Visible = true;
			PanelGeneralPropertiesContent.Visible = false;
		}

		private void MaxGSPSettings_Click(object sender, System.EventArgs e)
		{
			MaxGSPSettings.Visible = false;
			MinGSPSettings.Visible = true;
			PanelGeneralPropertiesContent.Visible = true;
		}


		private void MinDVTRoleSettings_Click(object sender, System.EventArgs e)
		{
			MinDVTRoleSettings.Visible = false;
			MaxDVTRoleSettings.Visible = true;
			PanelDVTRoleSettingsContent.Visible = false;
		}

		private void MaxDVTRoleSettings_Click(object sender, System.EventArgs e)
		{
			MaxDVTRoleSettings.Visible = false;
			MinDVTRoleSettings.Visible = true;
			PanelDVTRoleSettingsContent.Visible = true;		
		}

		private void MinSUTSettings_Click(object sender, System.EventArgs e)
		{
			MinSUTSettings.Visible = false;
			MaxSUTSettings.Visible = true;
			PanelSUTSettingContent.Visible = false;
		}
		private void MaxSUTSettings_Click(object sender, System.EventArgs e)
		{
			MaxSUTSettings.Visible = false;
			MinSUTSettings.Visible = true;
			PanelSUTSettingContent.Visible = true;
		}

		private void MinSecuritySettings_Click(object sender, System.EventArgs e)
		{
			_TCM_ShowMinSecuritySettings = false;
			MinSecuritySettings.Visible = false;
			MaxSecuritySettings.Visible = true;
			PanelSecuritySettingsContent.Visible = false;
		
		}

		private void MaxSecuritySettings_Click(object sender, System.EventArgs e)
		{
			_TCM_ShowMinSecuritySettings = true;
			MaxSecuritySettings.Visible = false;
			MinSecuritySettings.Visible = true;
			PanelSecuritySettingsContent.Visible = true;
		}

		#endregion

		private void ListBoxSecuritySettings_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				// Make all security setting groups invisible.
				GroupSecurityGeneral.Visible = false;
				GroupSecurityVersion.Visible = false;
				GroupSecurityAuthentication.Visible = false;
				GroupSecurityKeyExchange.Visible = false;
				GroupSecurityDataIntegrity.Visible = false;
				GroupSecurityEncryption.Visible = false;
				GroupSecurityFiles.Visible = false;

				// Make the selected security setting group visible only.
				switch (this.ListBoxSecuritySettings.SelectedIndex)
				{
					case 0:
						GroupSecurityGeneral.Visible = true;
						break;
					case 1:
						GroupSecurityVersion.Visible = true;
						break;
					case 2:
						GroupSecurityAuthentication.Visible = true;
						break;
					case 3:
						GroupSecurityKeyExchange.Visible = true;
						break;
					case 4:
						GroupSecurityDataIntegrity.Visible = true;
						break;
					case 5:
						GroupSecurityEncryption.Visible = true;
						break;
					case 6:
						GroupSecurityFiles.Visible = true;
						break;
				}
			}
		}

		private void SessionTreeView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			_SessionTreeViewManager.AfterSelect();
		}


		private void TCM_UpdateTabsShown()
		{
			_TCM_UpdateCount++;

			bool theSessionInformationTabShown = false;
			bool theActivityLoggingTabShown = false;
			bool theScriptTabShown = false;
			bool theDetailedValidationTabShown = false;
			bool theSpecifySopClassesTabShown = false;
			bool theNoInformationAvailableTabShown = false;

			TreeNodeTag theSelectedTag = GetSelectedTreeNodeTag();
			TreeNode theSelectedNode = GetSelectedNode();
			Dvtk.Sessions.Session theSelectedSession = GetSelectedSession();

			System.Windows.Forms.TabPage theTabWithFocus = null;


			// If no node (and indirectly a tag) is selected...
			if (theSelectedTag == null)
			{
				theNoInformationAvailableTabShown = true;
				theTabWithFocus = TabNoInformationAvailable;
			}
				// If the selected session is executing...
			else if (_MainForm.IsExecuting(theSelectedSession))
			{
				// If it is executing in this project form...
				if (theSelectedSession == GetExecutingSession())
				{
					theActivityLoggingTabShown = true;
					theTabWithFocus = TabActivityLogging;
				}
					// If it is not executing in this project form...
				else
				{
					theNoInformationAvailableTabShown = true;
					theTabWithFocus = TabNoInformationAvailable;
				}
			}
			else
				// Selected session is not executing.
			{
				if ( (theSelectedTag is SessionTag) ||
					(theSelectedTag is ResultsCollectionTag) ||
					(theSelectedTag is EmulatorTag)
					)
				{
					theSessionInformationTabShown = true;
					theSpecifySopClassesTabShown = true;
					theActivityLoggingTabShown = true;
					theTabWithFocus = TabSessionInformation;
				}

				if (theSelectedTag is ScriptFileTag)
				{
					theSessionInformationTabShown = true;
					theSpecifySopClassesTabShown = true;
					theActivityLoggingTabShown = true;
					theScriptTabShown = true;
					theTabWithFocus = TabScript;
				}

				if (theSelectedTag is ResultsFileTag)
				{
					// If the parent of this result file node is a script file, show a script tab.
					if (theSelectedNode.Parent.Tag is ScriptFileTag)
					{
						theScriptTabShown = true;
					}

					theSessionInformationTabShown = true;
					theSpecifySopClassesTabShown = true;
					theActivityLoggingTabShown = true;
					theDetailedValidationTabShown = true;
					theTabWithFocus = TabValidationResults;
				}
			}

			// Sanity check.
			if (theTabWithFocus == null)
			{
				throw new System.ApplicationException("tab with focus expected to be set.");
			}

			// Determine if the tabs shown need to be changed.
			if ((_TCM_SessionInformationTabShown != theSessionInformationTabShown) ||
				(_TCM_ActivityLoggingTabShown != theActivityLoggingTabShown) ||
				(_TCM_ScriptTabShown != theScriptTabShown) ||
				(_TCM_DetailedValidationTabShown != theDetailedValidationTabShown) ||
				(_TCM_SpecifySopClassesTabShown != theSpecifySopClassesTabShown) ||
				(_TCM_NoInformationAvailableTabShown != theNoInformationAvailableTabShown))
			{

				TabControl.Visible = false;

				TabControl.Controls.Clear();	
				
				if (theSessionInformationTabShown)
				{
					TabControl.Controls.Add(TabSessionInformation);
				}

				if (theSpecifySopClassesTabShown)
				{
					TabControl.Controls.Add(TabSpecifySopClasses);
				}

				if (theActivityLoggingTabShown)
				{
					TabControl.Controls.Add(TabActivityLogging);
				}

				if (theScriptTabShown)
				{
					TabControl.Controls.Add(TabScript);
				}

				if (theDetailedValidationTabShown)
				{
					TabControl.Controls.Add(TabValidationResults);
				}

				if (theNoInformationAvailableTabShown)
				{
					TabControl.Controls.Add(TabNoInformationAvailable);
				}

				if (theTabWithFocus != null)
				{
					TabControl.SelectedTab = theTabWithFocus;
				}

				TabControl.Visible = true;

				_TCM_SessionInformationTabShown = theSessionInformationTabShown;
				_TCM_ActivityLoggingTabShown = theActivityLoggingTabShown;
				_TCM_ScriptTabShown = theScriptTabShown;
				_TCM_DetailedValidationTabShown = theDetailedValidationTabShown;
				_TCM_SpecifySopClassesTabShown = theSpecifySopClassesTabShown;
				_TCM_NoInformationAvailableTabShown = theNoInformationAvailableTabShown;
			}

			if (theTabWithFocus != null)
			{
				TabControl.SelectedTab = theTabWithFocus;
			}

			_TCM_UpdateCount--;
		}
		
		public void TCM_UpdateTabContents()
		{
			_TCM_UpdateCount++;

			if (TabControl.SelectedTab == TabSessionInformation)
			{
				TCM_UpdateTabSessionInformation();
			}

			if (TabControl.SelectedTab == TabActivityLogging)
			{
				// The tab activity logging doesn't have to be updated explicitly.
				// It will be cleared when a script is started and text will be appended
				// during script execution.
			}

			if (TabControl.SelectedTab == TabScript)
			{
				TCM_UpdateTabScript();
			}

			if (TabControl.SelectedTab == TabValidationResults)
			{
				TCM_UpdateTabDetailedValidation();
			}

			if (TabControl.SelectedTab == TabSpecifySopClasses)
			{
				_SopClassesManager.Update();
			}

			_TCM_UpdateCount--;
		}


		// May only be called from UpdateTabContents.
		private void TCM_UpdateTabSessionInformation()
		{
			_TCM_UpdateCount++;

			bool Update = false;
			Dvtk.Sessions.Session theSelectedSession = GetSelectedSession();

			// If this session tab has not yet been filled, fill it.
			if (_TCM_SessionUsedForContentsOfTabSessionInformation == null)
			{
				Update = true;
			}
			else
			{
				// If the current session is not equal to the session used to fill the session information tab
				// last time, fill it again.
				if (_TCM_SessionUsedForContentsOfTabSessionInformation != theSelectedSession)
				{
					Update = true;
				}
			}

			// Update the complete contents of the session information tab.
			if (Update)
			{
				// General session properties.
				if (theSelectedSession is Dvtk.Sessions.ScriptSession)
				{
					TextBoxSessionType.Text = "Script";
				}

				if (theSelectedSession is Dvtk.Sessions.MediaSession)
				{
					TextBoxSessionType.Text = "Media";
				}

				if (theSelectedSession is Dvtk.Sessions.EmulatorSession)
				{
					TextBoxSessionType.Text = "Emulator";
				}

				TextBoxSessionTitle.Text = theSelectedSession.SessionTitle;
				NumericSessonID.Value = theSelectedSession.SessionId;
				TextBoxTestedBy.Text = theSelectedSession.TestedBy;
				//DateTested.Value = theSelectedSession.Date;
				TextBoxResultsRoot.Text = theSelectedSession.ResultsRootDirectory;

				if (theSelectedSession is Dvtk.Sessions.ScriptSession)
				{
					Dvtk.Sessions.ScriptSession theSelectedScriptSession;

					theSelectedScriptSession = (Dvtk.Sessions.ScriptSession)theSelectedSession;

					LabelScriptsDir.Visible = true;
					TextBoxScriptRoot.Visible = true;
					ButtonBrowseScriptsDir.Visible = true;
					LabelDescriptionDir.Visible = true;
					TextBoxDescriptionRoot.Visible = true;
					ButtonBrowseDescriptionDir.Visible = true;

					TextBoxScriptRoot.Text = theSelectedScriptSession.DicomScriptRootDirectory;
					TextBoxDescriptionRoot.Text = theSelectedScriptSession.DescriptionDirectory;
					CheckBoxDefineSQLength.Checked = theSelectedScriptSession.DefineSqLength;
					CheckBoxAddGroupLengths.Checked = theSelectedScriptSession.AddGroupLength;
				}
				else
				{
					LabelScriptsDir.Visible = false;
					TextBoxScriptRoot.Visible = false;
					ButtonBrowseScriptsDir.Visible = false;
					LabelDescriptionDir.Visible = false;
					TextBoxDescriptionRoot.Visible = false;
					ButtonBrowseDescriptionDir.Visible = false;
				}

				CheckBoxGenerateDetailedValidationResults.Checked = theSelectedSession.DetailedValidationResults;

				if (theSelectedSession is Dvtk.Sessions.EmulatorSession)
				{
					Dvtk.Sessions.EmulatorSession theSelectedEmulatorSession;
					theSelectedEmulatorSession = (Dvtk.Sessions.EmulatorSession)theSelectedSession;

					ButtonSpecifyTransferSyntaxes.Visible = true;
					LabelSpecifyTransferSyntaxes.Visible = true;
					CheckBoxDefineSQLength.Checked = theSelectedEmulatorSession.DefineSqLength;
					CheckBoxAddGroupLengths.Checked = theSelectedEmulatorSession.AddGroupLength;
				}
				else
				{
					ButtonSpecifyTransferSyntaxes.Visible = false;
					LabelSpecifyTransferSyntaxes.Visible = false;
				}

				if (theSelectedSession is Dvtk.Sessions.MediaSession)
				{
					CheckBoxDefineSQLength.Visible = false;
					CheckBoxAddGroupLengths.Visible = false;
				}
				else
				{
					CheckBoxDefineSQLength.Visible = true;
					CheckBoxAddGroupLengths.Visible = true;
				}

				switch(theSelectedSession.StorageMode)
				{
					case Dvtk.Sessions.StorageMode.AsDataSet:
						ComboBoxStorageMode.SelectedIndex = 0;								
						break;

					case Dvtk.Sessions.StorageMode.AsMedia:
						ComboBoxStorageMode.SelectedIndex = 1;								
						break;

					case Dvtk.Sessions.StorageMode.NoStorage:
						ComboBoxStorageMode.SelectedIndex = 2;								
						break;

					default:
						// Not supposed to get here.
						Debug.Assert(false);
						break;
				}

				CheckBoxLogRelation.Checked = (GetSelectedSession().LogLevelFlags & Dvtk.Sessions.LogLevelFlags.ImageRelation) == Dvtk.Sessions.LogLevelFlags.ImageRelation;


				// Dvt role settings.
				if (theSelectedSession is Dvtk.Sessions.IConfigurableDvt)
				{
					Dvtk.Sessions.IDvtSystemSettings theIDvtSystemSettings = null;
					theIDvtSystemSettings = (theSelectedSession as Dvtk.Sessions.IConfigurableDvt).DvtSystemSettings;

					TextBoxDVTImplClassUID.Text = theIDvtSystemSettings.ImplementationClassUid;
					TextBoxDVTImplVersionName.Text = theIDvtSystemSettings.ImplementationVersionName;
					TextBoxDVTAETitle.Text = theIDvtSystemSettings.AeTitle;
					NumericDVTListenPort.Value = theIDvtSystemSettings.Port;
					NumericDVTTimeOut.Value = theIDvtSystemSettings.SocketTimeout;
					NumericDVTMaxPDU.Value = theIDvtSystemSettings.MaximumLengthReceived;

					PanelDVTRoleSettingsTitle.Visible = true;
					if (MinDVTRoleSettings.Visible)
					{
						PanelDVTRoleSettingsContent.Visible = true;
					}
				}
				else
				{
					PanelDVTRoleSettingsTitle.Visible = false;
					PanelDVTRoleSettingsContent.Visible = false;
				}


				// System under test settings.
				if (theSelectedSession is Dvtk.Sessions.IConfigurableSut)
				{
					Dvtk.Sessions.ISutSystemSettings theISutSystemSettings = null;
					theISutSystemSettings = (theSelectedSession as Dvtk.Sessions.IConfigurableSut).SutSystemSettings;

					TextBoxSUTImplClassUID.Text = theISutSystemSettings.ImplementationClassUid;
					TextBoxSUTImplVersionName.Text = theISutSystemSettings.ImplementationVersionName;
					TextBoxSUTAETitle.Text = theISutSystemSettings.AeTitle;
					NumericSUTListenPort.Value = theISutSystemSettings.Port;
					TextBoxSUTTCPIPAddress.Text = theISutSystemSettings.HostName;
					NumericSUTMaxPDU.Value = theISutSystemSettings.MaximumLengthReceived;

					PanelSUTSettingTitle.Visible = true;
					if (MinSUTSettings.Visible)
					{
						PanelSUTSettingContent.Visible = true;
					}
				}
				else
				{
					PanelSUTSettingTitle.Visible = false;
					PanelSUTSettingContent.Visible = false;
				}


				// Security settings.
				if (theSelectedSession is Dvtk.Sessions.ISecure)
				{
					Dvtk.Sessions.ISecuritySettings theISecuritySettings = null;

					theISecuritySettings = (theSelectedSession as Dvtk.Sessions.ISecure).SecuritySettings;

					CheckBoxSecureConnection.Checked = theISecuritySettings.SecureSocketsEnabled;

					if (CheckBoxSecureConnection.Checked)
					{
						if (_TCM_ShowMinSecuritySettings)
						{
							PanelSecuritySettingsTitle.Visible = true;
							PanelSecuritySettingsContent.Visible = true;

							MinSecuritySettings.Visible = true;
							MaxSecuritySettings.Visible = false;

							CheckBoxCheckRemoteCertificates.Checked = theISecuritySettings.CheckRemoteCertificate;
							CheckBoxCacheSecureSessions.Checked = theISecuritySettings.CacheTlsSessions;
							CheckBoxTLS.Checked = ((theISecuritySettings.TlsVersionFlags & Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_TLSv1) != 0);
							CheckBoxSSL.Checked = ((theISecuritySettings.TlsVersionFlags & Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_SSLv3) != 0);
							CheckBoxAuthenticationDSA.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_DSA) != 0);
							CheckBoxAuthenticationRSA.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_RSA) != 0);
							CheckBoxDataIntegrityMD5.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_MD5) != 0);
							CheckBoxDataIntegritySHA.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_SHA1) != 0);
							CheckBoxEncryptionNone.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_NONE) != 0);
							CheckBoxEncryptionTripleDES.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_3DES) != 0);
							CheckBoxEncryptionAES128.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES128) != 0);
							CheckBoxEncryptionAES256.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES256) != 0);
							CheckBoxKeyExchangeDH.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_DH) != 0);
							CheckBoxKeyExchangeRSA.Checked = ((theISecuritySettings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_RSA) != 0);
							TextBoxTrustedCertificatesFile.Text = theISecuritySettings.CertificateFileName;
							TextBoxSecurityCredentialsFile.Text = theISecuritySettings.CredentialsFileName;
						}
						else
						{
							PanelSecuritySettingsContent.Visible = false;

							MinSecuritySettings.Visible = false;
							MaxSecuritySettings.Visible = true;
						}
					}
					else
					{
						PanelSecuritySettingsTitle.Visible = true;
						PanelSecuritySettingsContent.Visible = false;

						MinSecuritySettings.Visible = false;
						MaxSecuritySettings.Visible = false;
					}
				}
				else
				{
					PanelSecuritySettingsTitle.Visible = false;
					PanelSecuritySettingsContent.Visible = false;
				}

				_TCM_SessionUsedForContentsOfTabSessionInformation = theSelectedSession;
			}
		
			_TCM_UpdateCount--;
		}

		// May only be called from UpdateTabContents.
		private void TCM_UpdateTabScript()
		{
			_TCM_UpdateCount++;

			bool theHtmlDescriptionExists = false;
			ScriptFileTag theScriptFileTag = null;
			//MediaSessionTag theMediaSessionTag = null;
			string theHtmlDescriptionFileName = null;
			Dvtk.Sessions.ScriptSession theScriptSession = null;

			TreeNodeTag theSelectedTag = GetSelectedTreeNodeTag();
			TreeNode theSelectedNode = GetSelectedNode();

			if (theSelectedTag is ScriptFileTag)
			{
				theScriptFileTag = (ScriptFileTag)theSelectedTag;
			}
			else if (theSelectedTag is ResultsFileTag)
			{
				if (theSelectedNode.Parent.Tag is ScriptFileTag)
				{
					theScriptFileTag = (ScriptFileTag)theSelectedNode.Parent.Tag;
				}
			}

			if (theScriptFileTag == null)
			{
				// Not supposed to get here.
				throw new System.ApplicationException("Error: not expected to get here.");
			}

			theScriptSession = (Dvtk.Sessions.ScriptSession)GetSelectedSession();


			// Is the description directory not empty?
			if (theScriptSession.DescriptionDirectory != "")
			{
				DirectoryInfo theDirectoryInfo = new DirectoryInfo(theScriptSession.DescriptionDirectory);

				// Does the description directory exist?
				if (theDirectoryInfo.Exists)
				{
					theHtmlDescriptionFileName = System.IO.Path.Combine(theScriptSession.DescriptionDirectory, theScriptFileTag._ScriptFileName);
					theHtmlDescriptionFileName = theHtmlDescriptionFileName.Replace ('.', '_') + ".html";

					// Does the html description file exists?
					if (File.Exists(theHtmlDescriptionFileName))
					{
						// Now we know the html description file exists.
						theHtmlDescriptionExists = true;
					}
				}
			}
	
			if (theHtmlDescriptionExists)
			{
				object Zero = 0;
				object EmptyString = "";

				RichTextBoxScript.Visible = false;
				axWebBrowserScript.Visible = true;

				axWebBrowserScript.Navigate (theHtmlDescriptionFileName, ref Zero, ref EmptyString, ref EmptyString, ref EmptyString);
			}
			else
			{

				axWebBrowserScript.Visible = false;
				RichTextBoxScript.Visible = true;

				RichTextBoxScript.Clear();
					
				// If this is a Visual Basic Script...
				if (theScriptFileTag._ScriptFileName.ToLower().EndsWith(".vbs"))
				{
					// TODO!!!!!
					// The following code should be removed when the business layer is completely implemented!
					// For now, construct a business layer object.
					// BEGIN
					

					DvtkApplicationLayer.VisualBasicScript applicationLayerVisualBasicScript = 
						new DvtkApplicationLayer.VisualBasicScript(theScriptSession, theScriptFileTag._ScriptFileName);
					// END


					if (_MainForm._UserSettings.ExpandVisualBasicScript)
					{
						String includeErrors;
						String expandedContent = applicationLayerVisualBasicScript.GetExpandedContent(out includeErrors);

						// If include errors exist...
						if (includeErrors.Length > 0)
						{
							RichTextBoxScript.Text = includeErrors;
							RichTextBoxScript.SelectAll();
							RichTextBoxScript.SelectionColor = Color.Red;
							RichTextBoxScript.Select(0, 0);
							RichTextBoxScript.SelectionColor = Color.Black;
							RichTextBoxScript.AppendText(expandedContent);
						}
							// If no include errors exist...
						else
						{
							RichTextBoxScript.Text = expandedContent;
						}
					}
					else
					{
						RichTextBoxScript.Text = applicationLayerVisualBasicScript.GetContent();
					}

				}
					// If this is not a visual basic script...
				else
				{
					string theFullScriptFileName;

					theFullScriptFileName = System.IO.Path.Combine(theScriptSession.DicomScriptRootDirectory, theScriptFileTag._ScriptFileName);

					RichTextBoxScript.LoadFile(theFullScriptFileName, RichTextBoxStreamType.PlainText);
				}
			}

			_TCM_UpdateCount--;
		}

		// May only be called from UpdateTabContents.
		private void TCM_UpdateTabDetailedValidation()
		{
			if (_TCM_CountForControlsChange == 0)
			{
				_TCM_UpdateCount++;

				ResultsFileTag theResultsFileTag = _SessionTreeViewManager.GetSelectedTreeNodeTag() as ResultsFileTag;

				if ((theResultsFileTag != null) && (theResultsFileTag._ResultsFileName.ToLower().EndsWith(".xml")))
				{
					string theHtmlFileNameOnly = theResultsFileTag._ResultsFileName.ToLower().Replace(".xml", ".html");
					string theHtmlFullFileName = System.IO.Path.Combine(theResultsFileTag._Session.ResultsRootDirectory, theHtmlFileNameOnly);
					string theXmlFullFileName = System.IO.Path.Combine(theResultsFileTag._Session.ResultsRootDirectory, theResultsFileTag._ResultsFileName);

					// Show the HTML file.
					// The actual conversion from XML to HTML will be performed in the WebDescriptionView_BeforeNavigate2 method.


					// MK!!!! TESTEN _TCM_ValidationResultsManager.ShowHtml(theHtmlFullFileName);
					 _TCM_ValidationResultsManager.ShowHtml(theXmlFullFileName);
				}

				_TCM_UpdateCount--;
			}
		
		}

		private void TabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				_SopClassesManager.RemoveDynamicDataBinding();

				TCM_UpdateTabContents();

				Notify(new SelectedTabChangeEvent());
			}
		}

		private void TextBoxSessionTitle_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				GetSelectedSession().SessionTitle = TextBoxSessionTitle.Text;

				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}
		}

		private void SessionTreeView_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			// Workaround, to make sure that the NumericUpDown controls _Leave method is called
			// (when selected) before a new session is selected in the session tree.
			if (TabControl.SelectedTab == TabSessionInformation)
			{
				//TextBoxSessionTitle.Focus();
				//TextBoxDVTAETitle.Focus();
				//TextBoxSUTAETitle.Focus();

				TabControl.SelectedTab.Focus();
                
			}

		}

		private void TextBoxTestedBy_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				GetSelectedSession().TestedBy  = TextBoxTestedBy.Text;

				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}
		}

//		private void DateTested_ValueChanged(object sender, System.EventArgs e)
//		{
//			// Only react when the user has made changes, not when the TCM_Update method has been called.
//			if (_TCM_UpdateCount == 0)
//			{
//				GetSelectedSession().Date  = DateTested.Value;
//
//				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
//				Notify(theSessionChange);
//			}
//		}

		private void TextBoxDVTAETitle_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableDvt)
				{
					Dvtk.Sessions.IDvtSystemSettings theIDvtSystemSettings = null;
					theIDvtSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableDvt).DvtSystemSettings;

					theIDvtSystemSettings.AeTitle = TextBoxDVTAETitle.Text;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}
		}
		private void TextBoxDVTImplClassUID_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableDvt)
				{
					Dvtk.Sessions.IDvtSystemSettings theIDvtSystemSettings = null;
					theIDvtSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableDvt).DvtSystemSettings;

					theIDvtSystemSettings.ImplementationClassUid  = TextBoxDVTImplClassUID.Text;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}
		}

		private void TextBoxDVTImplVersionName_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableDvt)
				{
					Dvtk.Sessions.IDvtSystemSettings theIDvtSystemSettings = null;
					theIDvtSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableDvt).DvtSystemSettings;

					theIDvtSystemSettings.ImplementationVersionName  = TextBoxDVTImplVersionName.Text;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}		
		}

		private void TextBoxSUTAETitle_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableSut)
				{
					Dvtk.Sessions.ISutSystemSettings theISutSystemSettings = null;
					theISutSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableSut).SutSystemSettings;

					theISutSystemSettings.AeTitle  = TextBoxSUTAETitle.Text;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}								
		}

		private void TextBoxSUTImplClassUID_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableSut)
				{
					Dvtk.Sessions.ISutSystemSettings theISutSystemSettings = null;
					theISutSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableSut).SutSystemSettings;

					theISutSystemSettings.ImplementationClassUid  = TextBoxSUTImplClassUID.Text;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}								
		
		}

		private void TextBoxSUTImplVersionName_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableSut)
				{
					Dvtk.Sessions.ISutSystemSettings theISutSystemSettings = null;
					theISutSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableSut).SutSystemSettings;

					theISutSystemSettings.ImplementationVersionName  = TextBoxSUTImplVersionName.Text;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}									
		}

		private void NumericSUTListenPort_Leave(object sender, System.EventArgs e)
		{
			// touch it to repair known MS bug
			object obj = NumericSUTListenPort.Value;
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableSut)
				{
					Dvtk.Sessions.ISutSystemSettings theISutSystemSettings = null;
					theISutSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableSut).SutSystemSettings;

					theISutSystemSettings.Port = (ushort)NumericSUTListenPort.Value;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}											
		}

		private void TextBoxSUTTCPIPAddress_TextChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableSut)
				{
					Dvtk.Sessions.ISutSystemSettings theISutSystemSettings = null;
					theISutSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableSut).SutSystemSettings;

					theISutSystemSettings.HostName = TextBoxSUTTCPIPAddress.Text;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}													
		}

		private void NumericSUTMaxPDU_Leave(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableSut)
				{
					Dvtk.Sessions.ISutSystemSettings theISutSystemSettings = null;
					theISutSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableSut).SutSystemSettings;

					theISutSystemSettings.MaximumLengthReceived = (uint)NumericSUTMaxPDU.Value;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}															
		}


		private void NumericDVTListenPort_Validated(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableDvt)
				{
					Dvtk.Sessions.IDvtSystemSettings theIDvtSystemSettings = null;
					theIDvtSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableDvt).DvtSystemSettings;

					theIDvtSystemSettings.Port = (ushort)NumericDVTListenPort.Value;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}						
		}

		private void NumericDVTTimeOut_Validated(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableDvt)
				{
					Dvtk.Sessions.IDvtSystemSettings theIDvtSystemSettings = null;
					theIDvtSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableDvt).DvtSystemSettings;

					theIDvtSystemSettings.SocketTimeout = (ushort)NumericDVTTimeOut.Value;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}						
		}

		private void NumericDVTMaxPDU_Validated(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.IConfigurableDvt)
				{
					Dvtk.Sessions.IDvtSystemSettings theIDvtSystemSettings = null;
					theIDvtSystemSettings = (GetSelectedSession() as Dvtk.Sessions.IConfigurableDvt).DvtSystemSettings;

					theIDvtSystemSettings.MaximumLengthReceived = (uint)NumericDVTMaxPDU.Value;

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				else
				{
					// Not supposed to get here.
					throw new System.ApplicationException("Error: not expected to get here.");
				}
			}								
		}

		private void NumericSessonID_Validated(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				GetSelectedSession().SessionId = Convert.ToUInt16 (NumericSessonID.Value);

				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}		
		}

		private void CheckBoxGenerateDetailedValidationResults_CheckedChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				// Update the session member.
				GetSelectedSession().DetailedValidationResults = CheckBoxGenerateDetailedValidationResults.Checked;

				// Notift the rest of the world that the session has been changed.
				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}
		}

		private void SessionTreeView_DoubleClick(object sender, System.EventArgs e)
		{
			_SessionTreeViewManager.DoubleClick();
		}

		public void TCM_AppendTextToActivityLogging(string theText)
		{
			// More then one thread may be writting indirectly to the activity logging tab,
			// when a Visual Basic Script is executed.

			if (RichTextBoxActivityLogging.InvokeRequired)
			{
				RichTextBoxActivityLogging.Invoke(_TCM_AppendTextToActivityLogging_ThreadSafe_Delegate, new object[] {theText});
			}
			else
			{
				TCM_AppendTextToActivityLogging_ThreadSafe(theText);
			}
		}

		private void TCM_AppendTextToActivityLogging_ThreadSafe(string theText)
		{
			RichTextBoxActivityLogging.AppendText(theText);
			// ScrollToCaret will work, regardless whether the RichTextControl has the focus or not,
			// because the HideSelection property is set to false.
			RichTextBoxActivityLogging.ScrollToCaret();
		}

		public void TCM_ClearActivityLogging()
		{
			RichTextBoxActivityLogging.Clear();

		}

		private void TCM_OnActivityReportEvent(object sender, Dvtk.Events.ActivityReportEventArgs theArgs)
		{
			TCM_AppendTextToActivityLogging(theArgs.Message + '\n');
		}

		public bool IsActive()
		{
			return(ParentForm.ActiveMdiChild == this);
		}

		private void TabSessionInformation_Click(object sender, System.EventArgs e)
		{
		
		}

		private void ButtonBrowseResultsDir_Click(object sender, System.EventArgs e)
		{			
			TheFolderBrowserDialog.Description = "Select the root directory where the result files should be stored.";

			// Only if the current directory exists, set this directory in the dialog browser.
			if (TextBoxResultsRoot.Text != "")
			{
				DirectoryInfo theDirectoryInfo = new DirectoryInfo(TextBoxResultsRoot.Text);

				if (theDirectoryInfo.Exists)
				{
					TheFolderBrowserDialog.SelectedPath = TextBoxResultsRoot.Text;
				}
			}

			if (TheFolderBrowserDialog.ShowDialog (this) == DialogResult.OK)
			{
				// MK!!! TextBoxResultsRoot.Text = TheFolderBrowserDialog.SelectedPath;
				if (GetSelectedSession().ResultsRootDirectory != TheFolderBrowserDialog.SelectedPath)
				{
					GetSelectedSession().ResultsRootDirectory = TheFolderBrowserDialog.SelectedPath;

					// Notify the rest of the world of the change.
					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.RESULTS_DIR);
					Notify(theSessionChange);
				}
			}
		}

		private void ButtonBrowseScriptsDir_Click(object sender, System.EventArgs e)
		{	
			Dvtk.Sessions.ScriptSession theScriptSession = null;

			if (GetSelectedSession() is Dvtk.Sessions.ScriptSession)
			{
				theScriptSession = (Dvtk.Sessions.ScriptSession)GetSelectedSession();
			}
			else
			{
				// Not supposed to get here.
				throw new System.ApplicationException("Error: not expected to get here.");
			}

			TheFolderBrowserDialog.Description = "Select the root directory containing the script files.";

			// Only if the current directory exists, set this directory in the dialog browser.
			if (TextBoxScriptRoot.Text != "")
			{
				DirectoryInfo theDirectoryInfo = new DirectoryInfo(TextBoxScriptRoot.Text);

				if (theDirectoryInfo.Exists)
				{
					TheFolderBrowserDialog.SelectedPath = TextBoxScriptRoot.Text;
				}
			}

			if (TheFolderBrowserDialog.ShowDialog (this) == DialogResult.OK)
			{
				//MK!! TextBoxScriptRoot.Text = TheFolderBrowserDialog.SelectedPath;
				if (theScriptSession.DicomScriptRootDirectory != TheFolderBrowserDialog.SelectedPath)
				{
					theScriptSession.DicomScriptRootDirectory = TheFolderBrowserDialog.SelectedPath;

					// Notify the rest of the world of the change.
					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.SCRIPTS_DIR);
					Notify(theSessionChange);
				}
			}		
		}

		private void ButtonBrowseDescriptionDir_Click(object sender, System.EventArgs e)
		{
			Dvtk.Sessions.ScriptSession theScriptSession = null;

			if (GetSelectedSession() is Dvtk.Sessions.ScriptSession)
			{
				theScriptSession = (Dvtk.Sessions.ScriptSession)GetSelectedSession();
			}
			else
			{
				// Not supposed to get here.
				throw new System.ApplicationException("Error: not expected to get here.");
			}

			TheFolderBrowserDialog.Description = "Select the root directory containing the description (html) files.";

			// Only if the current directory exists, set this directory in the dialog browser.
			if (TextBoxDescriptionRoot.Text != "")
			{
				DirectoryInfo theDirectoryInfo = new DirectoryInfo(TextBoxDescriptionRoot.Text);

				if (theDirectoryInfo.Exists)
				{
					TheFolderBrowserDialog.SelectedPath = TextBoxDescriptionRoot.Text;
				}
			}

			if (TheFolderBrowserDialog.ShowDialog (this) == DialogResult.OK)
			{
				//MK!! TextBoxDescriptionRoot.Text = TheFolderBrowserDialog.SelectedPath;
				if (theScriptSession.DescriptionDirectory != TheFolderBrowserDialog.SelectedPath)
				{
					theScriptSession.DescriptionDirectory = TheFolderBrowserDialog.SelectedPath;

					// Notify the rest of the world of the change.
					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.DESCRIPTION_DIR);
					Notify(theSessionChange);
				}
			}				
		}

		private void ButtonSpecifySopClassesAddDirectory_Click(object sender, System.EventArgs e)
		{
			_SopClassesManager.AddDefinitionFileDirectory();
		}

		private void ButtonSpecifySopClassesRemoveDirectory_Click(object sender, System.EventArgs e)
		{
			_SopClassesManager.RemoveDefinitionFileDirectory();
		}

		private void ComboBoxSpecifySopClassesAeTitle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			_SopClassesManager.SelectedAeTitleVersionChanged();
		}

		private void DataGridSpecifySopClasses_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			_SopClassesManager.DataGrid_MouseDown(e);
		}

		private void DataGridSpecifySopClasses_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			_SopClassesManager.DataGrid_MouseMove(e);		
		}

		private void DataGridSpecifySopClasses_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			_SopClassesManager.DataGrid_MouseUp(e);
		}

		/// <summary>
		/// Get the selected tree node.
		/// </summary>
		/// <returns>Selected tree node.</returns>
		public TreeNode GetSelectedNode()
		{
			return _SessionTreeViewManager.GetSelectedNode();
		}

		/// <summary>
		/// Get the selected session of the session tree view of the project form.
		/// </summary>
		/// <returns>Selected session.</returns>
		public Dvtk.Sessions.Session GetSelectedSession()
		{
			return _SessionTreeViewManager.GetSelectedSession();
		}

		/// <summary>
		/// Get the selected tree node tag of the session tree view of the project form.
		/// </summary>
		/// <returns>Selected tree node tag.</returns>
		public TreeNodeTag GetSelectedTreeNodeTag()
		{
			return _SessionTreeViewManager.GetSelectedTreeNodeTag();
		}

		private void ProjectForm2_Activated(object sender, System.EventArgs e)
		{
			this.Focus();
			Notify(new ProjectFormGetsFocusEvent());		
		}

		private void NumericSessonID_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				GetSelectedSession().SessionId = Convert.ToUInt16 (NumericSessonID.Value);

				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}		
		}

		private void ButtonSpecifyTransferSyntaxes_Click(object sender, System.EventArgs e)
		{
			SelectTransferSyntaxesForm  theSelectTransferSyntaxesForm = new SelectTransferSyntaxesForm((Dvtk.Sessions.EmulatorSession)_SessionTreeViewManager.GetSelectedSession());
			theSelectTransferSyntaxesForm.ShowDialog (this);

			if (theSelectTransferSyntaxesForm._SelectionChanged)
			{
				Notify(new SessionChange(_SessionTreeViewManager.GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER));
			}
		}

		/// <summary>
		/// This method is called before the Web control will navigate to a HTML page.
		/// In this method, the conversion from XML to HTML will be performed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void WebDescriptionView_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
		{
			string theFullFileName = _TCM_ValidationResultsManager.GetFullFileNameFromHtmlLink(e.uRL.ToString());

			string theDirectory = System.IO.Path.GetDirectoryName(theFullFileName);
			string theFileNameOnly = System.IO.Path.GetFileName(theFullFileName);

			if (e.uRL.ToString().ToLower().IndexOf(".xml") != -1)
				// The user has selected a results file tag or has pressed a link in a HTML file.
				// Convert the XML file to HTML, cancel this request and request viewing of the generated HTML file.
			{
				// Cancel it. We want to show the generated HTML file.
				// As a result of calling _TCM_ValidationResultsManager.ShowHtml(e.uRL.ToString()), this method will
				// be called again.
				e.cancel = true;

				string theHtmlFileNameOnly = theFileNameOnly.ToLower().Replace(".xml", ".html");
				string theHtmlFullFileName = System.IO.Path.Combine(theDirectory, theHtmlFileNameOnly);
				
				// Do the actual conversion from XML to HTML.
				_TCM_ValidationResultsManager.ConvertXmlToHtml(theDirectory, theFileNameOnly, theHtmlFileNameOnly);		

				_HtmlFullFileNameToShow = e.uRL.ToString().ToLower().Replace(".xml",".html");

				_TCM_ValidationResultsManager.ShowHtml(_HtmlFullFileNameToShow);
			}
			else
			{
				// Do nothing, This is a HTML file and will be automatically shown.				
			}

		}

		private void WebDescriptionView_CommandStateChange(object sender, AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent e)
		{
			_TCM_ValidationResultsManager.CommandStateChange(sender, e);
		}

		public SessionTreeViewManager GetSessionTreeViewManager()
		{
			return(_SessionTreeViewManager);
		}

		private void WebDescriptionView_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
		{
			_TCM_ValidationResultsManager.NavigateComplete2Handler();
			Notify(new WebNavigationComplete());
		}

		MainForm GetMainForm()
		{
			MainForm theMainForm = null;

			theMainForm = ParentForm as MainForm;

			if (theMainForm == null)
			{
				// Sanity check.
				Debug.Assert(false);
			}

			return theMainForm;
		}

		////////////////////
		#region Context Menu

		private void SessionTreeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				// Right button has been pressed.
			{
				// Make the node that is below the mouse the selected one.
				SessionTreeView.SelectedNode = SessionTreeView.GetNodeAt(e.X, e.Y);
				Refresh();
			}
		}

		private void ContextMenuSessionTreeView_Popup(object sender, System.EventArgs e)
		{
			TreeNodeTag theSelectedTreeNodeTag = _SessionTreeViewManager.GetSelectedTreeNodeTag();

			ContextMenu_AddNewSession.Visible = false;
			ContextMenu_AddNewSession.DefaultItem = false;
			ContextMenu_AddExistingSessions.Visible = false;
			ContextMenu_AddExistingSessions.DefaultItem = false;
			ContextMenu_Edit.Visible = false;
			ContextMenu_Edit.DefaultItem = false;
			ContextMenu_Execute.Visible = false;
			ContextMenu_Execute.DefaultItem = false;
			ContextMenu_ExploreResultsDir.Visible = false;
			ContextMenu_ExploreResultsDir.DefaultItem = false;
			ContextMenu_ExploreScriptsDir.Visible = false;
			ContextMenu_ExploreScriptsDir.DefaultItem = false;
			ContextMenu_None.Visible = false;
			ContextMenu_None.DefaultItem = false;
			ContextMenu_Remove.Visible = false;
			ContextMenu_Remove.DefaultItem = false;
			ContextMenu_RemoveAllResultsFiles.Visible = false;
			ContextMenu_RemoveAllResultsFiles.DefaultItem = false;
			ContextMenu_RemoveSessionFromProject.Visible = false;
			ContextMenu_RemoveSessionFromProject.DefaultItem = false;
			ContextMenu_Save.Visible = false;
			ContextMenu_Save.DefaultItem = false;
			ContextMenu_SaveAs.Visible = false;
			ContextMenu_SaveAs.DefaultItem = false;
			ContextMenu_ValidateMediaFiles.Visible = false;
			ContextMenu_ValidateMediaFiles.DefaultItem = false;
            ContextMenu_ValidateDicomdirWithoutRefFile.Visible = false;
            ContextMenu_ValidateDicomdirWithoutRefFile.DefaultItem = false;
			ContextMenu_ViewExpandedScript.Visible = false;
			ContextMenu_ViewExpandedScript.DefaultItem = false;
			ContextMenu_GenerateDICOMDIR.Visible = false;
			ContextMenu_GenerateDICOMDIR.DefaultItem = false;

			if (theSelectedTreeNodeTag == null)
			{
				ContextMenu_AddNewSession.Visible = true;
				ContextMenu_AddExistingSessions.Visible = true;
				_MainForm.MenuItem_FileSessionRemove.Enabled = false;
			}
			else if (theSelectedTreeNodeTag is SessionTag)
			{
				ContextMenu_ExploreResultsDir.Visible = true;
				ContextMenu_RemoveSessionFromProject.Visible = true;
				ContextMenu_Save.Visible = true;
				ContextMenu_SaveAs.Visible = true;
				
				if (_Project.GetSessionChanged(theSelectedTreeNodeTag._Session))
				{
					ContextMenu_Save.Enabled = true;
				}
				else
				{
					ContextMenu_Save.Enabled = false;
				}

				if (theSelectedTreeNodeTag is MediaSessionTag)
				{
					ContextMenu_ValidateMediaFiles.Visible = true;
					ContextMenu_ValidateMediaFiles.DefaultItem = true;	
					ContextMenu_GenerateDICOMDIR.Visible = true;
                    ContextMenu_ValidateDicomdirWithoutRefFile.Visible = true;
                   // ContextMenu_ValidateDicomdirWithoutRefFile.DefaultItem = true;
				}

				if (theSelectedTreeNodeTag is ScriptSessionTag)
				{
					ContextMenu_ExploreScriptsDir.Visible = true;
				}
			}
			else if (theSelectedTreeNodeTag is EmulatorTag)
			{
				ContextMenu_Execute.Visible = true;
				ContextMenu_Execute.DefaultItem = true;
				ContextMenu_RemoveAllResultsFiles.Visible = true;

			}
			else if (theSelectedTreeNodeTag is ScriptFileTag)
			{
				ContextMenu_Edit.Visible = true;
				ContextMenu_Execute.Visible = true;
				ContextMenu_Execute.DefaultItem = true;
				ContextMenu_RemoveAllResultsFiles.Visible = true;
				
				ScriptFileTag scriptFileTag = theSelectedTreeNodeTag as ScriptFileTag;
				if (scriptFileTag._ScriptFileName.ToLower().EndsWith(".vbs"))
				{
					ContextMenu_ViewExpandedScript.Visible = true;
				}
				
			}
			else if (theSelectedTreeNodeTag is ResultsFileTag)
			{
				ContextMenu_Remove.Visible = true;
				ContextMenu_Remove.DefaultItem = true;
			}
			else if (theSelectedTreeNodeTag is ResultsCollectionTag)
			{
				ContextMenu_RemoveAllResultsFiles.Visible = true;
			}
			else
			{
				// Sanity check.
				// Unexpected tag.
				Debug.Assert(false);
			}
		}

		private void ContextMenu_AddExistingSession_Click(object sender, System.EventArgs e)
		{
            GetMainForm().AddExistingSessions();
        }

        private void ContextMenu_AddNewSession_Click(object sender, System.EventArgs e) {
            GetMainForm().AddNewSession();
        }

        private void ContextMenu_Edit_Click(object sender, System.EventArgs e) {
            _SessionTreeViewManager.EditSelectedScriptFile();
        }

        private void ContextMenu_Execute_Click(object sender, System.EventArgs e) {
            _SessionTreeViewManager.Execute();
        }

        private void ContextMenu_Remove_Click(object sender, System.EventArgs e) {
            ResultsFileTag resultfiletag = (ResultsFileTag)GetSelectedTreeNodeTag();
            ArrayList theResultsFilesToRemove = new ArrayList();
            if (resultfiletag == null) {
                // Sanity check.
                System.Diagnostics.Debug.Assert(false, "No selected result file.");
            }
            else {
                string theWarningText = string.Format("Are you sure you want to remove the ResultFile and its associated result files\n\n{0}\n\nfrom the Session?",resultfiletag._ResultsFileName);
                if (MessageBox.Show (this,
                    theWarningText,
                    "Remove ResultFile from Session?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2)== DialogResult.Yes) {
                    DirectoryInfo theDirectoryInfo;
                    FileInfo[] theFilesInfo = new FileInfo[0];

                    theDirectoryInfo = new DirectoryInfo (resultfiletag._Session.ResultsRootDirectory);

                    if (theDirectoryInfo.Exists) {
                        string resultFileName = resultfiletag._ResultsFileName;
                        string baseName = ResultsFile.GetBaseNameNoCheck(resultFileName);

                        // If the baseNamePrefix cannot be found, no files will be removed.
                        string baseNamePrefix = "__________________________________";

                        if (resultFileName.ToLower().StartsWith("summary")) {
                            baseNamePrefix = resultFileName.Substring(0, 12); 
                        }
                        else if (resultFileName.ToLower().StartsWith("detail")) {
                            baseNamePrefix = resultFileName.Substring(0, 11); 
                        }
                        else {
                            // Sanity check.
                            Debug.Assert(false);
                        }

                        theFilesInfo = theDirectoryInfo.GetFiles (baseNamePrefix + baseName + "*");

                        foreach (FileInfo theFileInfo in theFilesInfo) {
                            string theResultsFileName = theFileInfo.Name;

                            theResultsFilesToRemove.Add(theResultsFileName);						
                        }
                    }
			
                    ResultsFile.Remove(resultfiletag._Session, theResultsFilesToRemove);
			
                    this._MainForm.ActionRefreshSessionTree();			
                
                }
            }
        }

        private void ContextMenu_RemoveAllResultFiles_Click(object sender, System.EventArgs e) {	
            TreeNodeTag theSelectedTreeNodeTag = (TreeNodeTag)GetSelectedTreeNodeTag();
            ArrayList theResultsFilesToRemove = new ArrayList();
            DialogResult theDialogResult;
            if (theSelectedTreeNodeTag == null) {
                // Sanity check.
                System.Diagnostics.Debug.Assert(false, "No selected results file.");
            }
            else {
                string theWarningText = "Are you sure you want to remove the ResultFiles ?";
                if (MessageBox.Show (this,
                    theWarningText,
                    "Remove ResultFiles ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2)== DialogResult.Yes) {
			
                    if (theSelectedTreeNodeTag is ScriptFileTag) {				
                        ScriptFileTag scriptTag = (ScriptFileTag)GetSelectedTreeNodeTag();
                        theResultsFilesToRemove = ResultsFile.GetAllNamesForSession(scriptTag._Session);
                        theResultsFilesToRemove = ResultsFile.GetNamesForScriptFile(scriptTag._ScriptFileName, theResultsFilesToRemove);
                        ResultsFile.Remove(scriptTag._Session, theResultsFilesToRemove);
                    }
                    else if (theSelectedTreeNodeTag is EmulatorTag) {
                        EmulatorTag emulatorTag = (EmulatorTag)GetSelectedTreeNodeTag();
                        theResultsFilesToRemove = ResultsFile.GetAllNamesForSession(emulatorTag._Session);
                        string theEmulatorBaseName = ResultsFile.GetBaseNameForEmulator(emulatorTag._EmulatorType);
                        theResultsFilesToRemove = ResultsFile.GetNamesForBaseName(theEmulatorBaseName, theResultsFilesToRemove);
                        ResultsFile.Remove(emulatorTag._Session, theResultsFilesToRemove);
                    }					
                    else {
                        ResultsCollectionTag resultscollectiontag = (ResultsCollectionTag)GetSelectedTreeNodeTag();
                        theResultsFilesToRemove = ResultsFile.GetAllNamesForSession(resultscollectiontag._Session);
                        string mediaFileName = resultscollectiontag._ResultsCollectionName.ToUpper().Replace("_DCM","");
                        string theMediaFullFileName = System.IO.Path.Combine(resultscollectiontag._Session.ResultsRootDirectory, mediaFileName);
                        string theMediaFileBaseName = ResultsFile.GetBaseNameForMediaFile(theMediaFullFileName);
                        theResultsFilesToRemove = ResultsFile.GetNamesForBaseName(theMediaFileBaseName, theResultsFilesToRemove);
                        ResultsFile.Remove(resultscollectiontag._Session, theResultsFilesToRemove);
                    }

                    this._MainForm.ActionRefreshSessionTree();
                }
                else {
                }
            }
        }

		private void ContextMenu_RemoveSessionFromProject_Click(object sender, System.EventArgs e)
		{
			GetMainForm().RemoveSelectedSession();
		}

		private void ContextMenu_ValidateFiles_Click(object sender, System.EventArgs e)
		{
			_SessionTreeViewManager.Execute();		
		}

		#endregion


		/// <summary>
		/// This method will be called is this project form is closed or when
		/// the main form is closed.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ProjectForm2_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			bool theMainFormIsClosing = true;

			// We don't know if calling of this method this is a consequence of closing this project form or
			// as a consequence of closing the main form.
			// The following code is needed to find this out (see the Form class Closing event in MSDN)

			if (((MainForm)ParentForm)._IsClosing)
				// Closing is done through the Exit menu item.
			{
				theMainFormIsClosing = true;
			}
			else
				// The X from the MainForm or ProjectForm2 is pressed.
			{
		
				// The position of the cursor in screen coordinates.
				Point theCursorPosition = Cursor.Position;

				if ((ParentForm.Top + SystemInformation.CaptionHeight) < theCursorPosition.Y)
				{
					theMainFormIsClosing = false;
				}
				else
				{
					theMainFormIsClosing = true;
				}
			}

			// Close if allowed.
			if (theMainFormIsClosing)
			{
				// Don't do anything.
				// The Main form will also receive a closing event and handle the closing of the application.
			}
			else
			{
				e.Cancel = false;
				MainForm theMainForm = (MainForm)ParentForm;
				DialogResult theDialogResult;

				if (IsExecuting())
				{
					// Ask the user if execution needs to be stopped.
					theDialogResult = MessageBox.Show("Executing is still going on.\nStop execution?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

					if (theDialogResult == DialogResult.Yes)
					{
						// Stop the execution. 
						Notify(new StopExecution(this));

						// Because execution is performed in a different thread, wait some
						// time to enable the execution to stop. Also give feedback to the user
						// of this waiting by showing a form stating this.
						TimerMessageForm theTimerMessageForm = new TimerMessageForm();

						theTimerMessageForm.ShowDialogSpecifiedTime("Stopping execution...", 3000);

						// Find out if execution really has stopped.
						if (IsExecuting())
						{
							theDialogResult = MessageBox.Show("Failed to stop execution.\n This form will not be closed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);	

							e.Cancel = true;
						}
					}
					else
					{
						e.Cancel = true;
					}
				}

				if (ParentForm.MdiChildren.Length == 1)
					// This is the last project form that is being closed,
					// so the project needs to be closed.
				{
					if (!e.Cancel)
					{
						// e.Cancel is false, so no execution takes place any more.
						// Close the project.
						// If unsaved changes exist, give the user the possibility to save them.
						_Project.Close(true);

						if (_Project.HasUserCancelledLastOperation())
							// The user has cancelled the close operation.
						{
							// Cancel the closing of this last project form.
							e.Cancel = true;
						}
						else
							// The user has not cancelled the close operation.
						{
							_Project = null;
							Notify(new ProjectClosed());
						}
					}
				}
			}
		}

		private void ButtonViewCertificates_Click(object sender, System.EventArgs e)
		{
			try
			{
				CredentialsCertificatesForm cred_cert_form = null;

				cred_cert_form = new CredentialsCertificatesForm (GetSelectedSession(), false);

				if (cred_cert_form.ShowDialog () == DialogResult.OK)
				{
					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}		
			}
			catch (Exception theException)
			{
				MessageBox.Show(theException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}

		}

		private void ButtonViewCredentials_Click(object sender, System.EventArgs e)
		{
			try
			{
				CredentialsCertificatesForm cred_cert_form = null;

				cred_cert_form = new CredentialsCertificatesForm (GetSelectedSession(), true);

				if (cred_cert_form.ShowDialog () == DialogResult.OK)
				{
					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
			}
			catch (Exception theException)
			{
				MessageBox.Show(theException.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				return;
			}
		}

		private void ButtonCreateCertificate_Click(object sender, System.EventArgs e)
		{
			WizardCreateCertificate wizard = new WizardCreateCertificate();
			wizard.ShowDialog(this);		
		}

		private void CheckBoxSecureConnection_CheckedChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				Dvtk.Sessions.ISecure theISecure = GetSelectedSession() as Dvtk.Sessions.ISecure;

				if (theISecure == null)
				{
					// Sanity check.
					Debug.Assert(false);
				}
				else
				{
					theISecure.SecuritySettings.SecureSocketsEnabled = CheckBoxSecureConnection.Checked;

					_TCM_SessionUsedForContentsOfTabSessionInformation = null;
					TCM_UpdateTabSessionInformation();

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
			}										
		}

		private void CheckBoxCheckRemoteCertificates_CheckedChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.ISecure)
				{
					Dvtk.Sessions.ISecuritySettings theISecuritySettings = null;
					theISecuritySettings = (GetSelectedSession() as Dvtk.Sessions.ISecure).SecuritySettings;

					bool isCurrentlyChecked = theISecuritySettings.CheckRemoteCertificate;

					try
					{
						theISecuritySettings.CheckRemoteCertificate = CheckBoxCheckRemoteCertificates.Checked;

						SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
						Notify(theSessionChange);
					}
					catch (Exception theException)
					{
						// Put the state of the check box back to the unchanged setting of the session.
						CheckBoxCheckRemoteCertificates.Checked = isCurrentlyChecked;

						MessageBox.Show(theException.Message + "\n\nThe change for checkbox \"" + CheckBoxCheckRemoteCertificates.Text + "\" is not allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					// Sanity check.
					Debug.Assert(false);
				}	
			}
		}

		private void CheckBoxCacheSecureSessions_CheckedChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (GetSelectedSession() is Dvtk.Sessions.ISecure)
				{
					Dvtk.Sessions.ISecuritySettings theISecuritySettings = null;
					theISecuritySettings = (GetSelectedSession() as Dvtk.Sessions.ISecure).SecuritySettings;

					bool isCurrentlyChecked = theISecuritySettings.CacheTlsSessions;

					try
					{
						theISecuritySettings.CacheTlsSessions = CheckBoxCacheSecureSessions.Checked;

						SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
						Notify(theSessionChange);
					}
					catch (Exception theException)
					{
						// Put the state of the check box back to the unchanged setting of the session.
						CheckBoxCacheSecureSessions.Checked = isCurrentlyChecked;

						MessageBox.Show(theException.Message + "\n\nThe change for checkbox \"" + CheckBoxCacheSecureSessions.Text + "\" is not allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					// Sanity check.
					Debug.Assert(false);
				}	
			}
		}

		private void UpdateCipherFlag(Dvtk.Sessions.CipherFlags theCipherFlag, CheckBox theCheckBox)
		{
			if (GetSelectedSession() is Dvtk.Sessions.ISecure)
			{
				Dvtk.Sessions.ISecuritySettings theISecuritySettings = null;
				theISecuritySettings = (GetSelectedSession() as Dvtk.Sessions.ISecure).SecuritySettings;

				bool isCurrentlyChecked = ((theISecuritySettings.CipherFlags & theCipherFlag) != 0);

				try
				{
					if (theCheckBox.Checked)
					{
						theISecuritySettings.CipherFlags |= theCipherFlag;
					}
					else
					{
						theISecuritySettings.CipherFlags &= ~theCipherFlag;
					}

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				catch (Exception theException)
				{
					// Put the state of the check box back to the unchanged setting of the session.
					theCheckBox.Checked = isCurrentlyChecked;

					MessageBox.Show(theException.Message + "\n\nThe change for checkbox \"" + theCheckBox.Text + "\" is not allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				// Sanity check.
				Debug.Assert(false);
			}
		}										

		private void UpdateTlsVersionFlag(Dvtk.Sessions.TlsVersionFlags theVersionFlag, CheckBox theCheckBox)
		{
			if (GetSelectedSession() is Dvtk.Sessions.ISecure)
			{
				Dvtk.Sessions.ISecuritySettings theISecuritySettings = null;
				theISecuritySettings = (GetSelectedSession() as Dvtk.Sessions.ISecure).SecuritySettings;

				bool isCurrentlyChecked = ((theISecuritySettings.TlsVersionFlags & theVersionFlag) != 0);

				try
				{
					if (theCheckBox.Checked)
					{
						theISecuritySettings.TlsVersionFlags |= theVersionFlag;
					}
					else
					{
						theISecuritySettings.TlsVersionFlags &= ~theVersionFlag;
					}

					SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
					Notify(theSessionChange);
				}
				catch (Exception theException)
				{
					// Put the state of the check box back to the unchanged setting of the session.
					theCheckBox.Checked = isCurrentlyChecked;

					MessageBox.Show(theException.Message + "\n\nThe change for checkbox \"" + theCheckBox.Text + "\" is not allowed.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				// Sanity check.
				Debug.Assert(false);
			}
		}										

		private void CheckBoxTLS_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateTlsVersionFlag(Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_TLSv1, CheckBoxTLS);
			}										
		}

		private void CheckBoxSSL_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateTlsVersionFlag(Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_SSLv3, CheckBoxSSL);
			}										
		}

		private void CheckBoxAuthenticationRSA_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_RSA, CheckBoxAuthenticationRSA);
			}
		}

		private void CheckBoxAuthenticationDSA_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_DSA, CheckBoxAuthenticationDSA);
			}										
		}

		private void CheckBoxKeyExchangeRSA_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_RSA, CheckBoxKeyExchangeRSA);
			}										
		}

		private void CheckBoxKeyExchangeDH_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_DH, CheckBoxKeyExchangeDH);
			}										
		}

		private void CheckBoxDataIntegritySHA_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_SHA1, CheckBoxDataIntegritySHA);
			}										
		}

		private void CheckBoxDataIntegrityMD5_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_MD5, CheckBoxDataIntegrityMD5);
			}										
		}

		private void CheckBoxEncryptionNone_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_NONE, CheckBoxEncryptionNone);
			}										
		}

		private void CheckBoxEncryptionTripleDES_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_3DES, CheckBoxEncryptionTripleDES);
			}										
		}

		private void CheckBoxEncryptionAES128_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES128, CheckBoxEncryptionAES128);
			}										
		}

		private void CheckBoxEncryptionAES256_CheckedChanged(object sender, System.EventArgs e)
		{
			if (_TCM_UpdateCount == 0)
			{
				UpdateCipherFlag(Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES256, CheckBoxEncryptionAES256);
			}												
		}

		public void TCM_CopySelectedTextToClipboard()
		{
			switch(GetActiveTab())
			{
				case ProjectForm2.ProjectFormActiveTab.SCRIPT_TAB:
				{
					if (axWebBrowserScript.Visible)
					{
						mshtml.HTMLDocument theHtmlDocument = (mshtml.HTMLDocument)axWebBrowserScript.Document;
						mshtml.IHTMLTxtRange theHtmlTextRange = (mshtml.IHTMLTxtRange)theHtmlDocument.selection.createRange ();

						if (theHtmlTextRange.text != null)
						{
							if (theHtmlTextRange.text != "")
							{
								// Sets the data into the Clipboard.
								Clipboard.SetDataObject(theHtmlTextRange.text);
							}
						}
					}
					else
					{
						RichTextBoxScript.Copy();
						//Clipboard.SetDataObject(RichTextBoxScript.SelectedText);
					}
				}

					break;

				case ProjectForm2.ProjectFormActiveTab.VALIDATION_RESULTS_TAB:
				{
					mshtml.HTMLDocument theHtmlDocument = (mshtml.HTMLDocument)WebDescriptionView.Document;
					mshtml.IHTMLTxtRange theHtmlTextRange = (mshtml.IHTMLTxtRange)theHtmlDocument.selection.createRange ();

					if (theHtmlTextRange.text != null)
					{
						if (theHtmlTextRange.text != "")
						{
							Clipboard.SetDataObject(theHtmlTextRange.text);
						}
					}
				}
					break;


				case ProjectForm2.ProjectFormActiveTab.ACTIVITY_LOGGING_TAB:
					RichTextBoxActivityLogging.Copy();
					
					break;
				case ProjectForm2.ProjectFormActiveTab.SPECIFY_SOP_CLASSES_TAB:
					RichTextBoxSpecifySopClassesInfo.Copy();
					
					break;

				default:
					// Do nothing.
					break;
			}
		}

		private void ContextMenu_ExploreResultsDir_Click(object sender, System.EventArgs e)
		{
			if (GetSelectedSession() != null)
			{
				System.Diagnostics.Process theProcess  = new System.Diagnostics.Process();

				theProcess.StartInfo.FileName= "Explorer.exe";
				theProcess.StartInfo.Arguments = GetSelectedSession().ResultsRootDirectory;

				theProcess.Start();
			}
		}

		private void ContextMenu_ExploreScriptsDir_Click(object sender, System.EventArgs e)
		{
			if (GetSelectedSession() != null)
			{
				Dvtk.Sessions.ScriptSession theScriptSession = GetSelectedSession() as Dvtk.Sessions.ScriptSession;

				if (theScriptSession != null)
				{
					System.Diagnostics.Process theProcess  = new System.Diagnostics.Process();

					theProcess.StartInfo.FileName= "Explorer.exe";
					theProcess.StartInfo.Arguments = theScriptSession.DicomScriptRootDirectory;

					theProcess.Start();
				}
			}		
		}

		/// <summary>
		/// Get (if exisiting) the session that is executed by this Project Form.
		/// </summary>
		/// <returns>Session that is executed by this Project Form, null if this Project Form is not executing a session.</returns>
		public Dvtk.Sessions.Session GetExecutingSession()
		{
			return(_SessionTreeViewManager.GetExecutingSession());
		}

		private void ContextMenu_SaveAs_Click(object sender, System.EventArgs e)
		{	
			Dvtk.Sessions.Session theCurrentSession = GetSelectedSession();
			Dvtk.Sessions.Session theNewSession = _Project.SaveSessionAs(theCurrentSession);

			if (theNewSession != null)
			{
				Notify(new SessionReplaced(theCurrentSession, theNewSession));
			}		
		}

		private void ContextMenu_Save_Click(object sender, System.EventArgs e)
		{
			TreeNodeTag theSelectedTreeNodeTag = GetSelectedTreeNodeTag();

			if (theSelectedTreeNodeTag is SessionTag)
			{
				_Project.SaveSession(theSelectedTreeNodeTag._Session.SessionFileName);
				Notify(new Saved());
				_MainForm.UpdateUIControls();
			}
			else
			{
				// Sanity check.
				Debug.Assert(false);
			}
		}

		private void ContextMenu_ValidateMediaFiles_Click(object sender, System.EventArgs e)
		{
			// Set the session object member to default value.
			GetSelectedSession().ValidateReferencedFile = true;

			_SessionTreeViewManager.Execute();		
		}

		private void ButtonTrustedCertificatesFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog theOpenFileDialog = new OpenFileDialog();

			theOpenFileDialog.Filter = "PEM Certificate files (*.pem;*.cer)|*.pem;*.cer";

			theOpenFileDialog.Title = "Select the file containing the SUT Public Keys (certificates)";

			theOpenFileDialog.CheckFileExists = false;

			// Only if the current file exists, set this file in the file browser.
			if (TextBoxTrustedCertificatesFile.Text != "")
			{
				if (File.Exists(TextBoxTrustedCertificatesFile.Text))
				{
					theOpenFileDialog.FileName = TextBoxTrustedCertificatesFile.Text;
				}
			}

			if (theOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				TextBoxTrustedCertificatesFile.Text = theOpenFileDialog.FileName;
				((Dvtk.Sessions.ISecure)GetSelectedSession()).SecuritySettings.CertificateFileName = theOpenFileDialog.FileName;

				// Notify the rest of the world of the change.
				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}
		}

		private void ButtonSecurityCredentialsFile_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog theOpenFileDialog = new OpenFileDialog();

			theOpenFileDialog.Filter = "PEM Certificate files (*.pem;*.cer)|*.pem;*.cer";

			theOpenFileDialog.Title = "Select the file containing the DVT Private Keys (credentials)";

			theOpenFileDialog.CheckFileExists = false;

			// Only if the current file exists, set this file in the file browser.
			if (TextBoxSecurityCredentialsFile.Text != "")
			{
				if (File.Exists(TextBoxSecurityCredentialsFile.Text))
				{
					theOpenFileDialog.FileName = TextBoxSecurityCredentialsFile.Text;
				}
			}

			if (theOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				TextBoxSecurityCredentialsFile.Text = theOpenFileDialog.FileName;
				((Dvtk.Sessions.ISecure)GetSelectedSession()).SecuritySettings.CredentialsFileName = theOpenFileDialog.FileName;

				// Notify the rest of the world of the change.
				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}		
		}

		private void ComboBoxStorageMode_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				switch(ComboBoxStorageMode.SelectedIndex)
				{
					case 0:
						GetSelectedSession().StorageMode = Dvtk.Sessions.StorageMode.AsDataSet; 
						break;

					case 1:
						GetSelectedSession().StorageMode = Dvtk.Sessions.StorageMode.AsMedia; 
						break;

					case 2:
						GetSelectedSession().StorageMode = Dvtk.Sessions.StorageMode.NoStorage; 
						break;

					default:
						// Not supposed to get here.
						Debug.Assert(false);
						break;
				}

				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}			
		}

		private void CheckBoxLogRelation_CheckedChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				if (CheckBoxLogRelation.Checked)
				{
					GetSelectedSession().LogLevelFlags |= Dvtk.Sessions.LogLevelFlags.ImageRelation;
				}
				else
				{
					GetSelectedSession().LogLevelFlags ^= Dvtk.Sessions.LogLevelFlags.ImageRelation;
				}

				SessionChange theSessionChange = new SessionChange(GetSelectedSession(), SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}		
		}
		protected override void OnResize(EventArgs e)
		{

			//MessageBox.Show("!");

			if (Size.Height < 100)
			{
				if (_PreviousBounds != Rectangle.Empty)
				{
					Bounds = _PreviousBounds;
				}
			}

			base.OnResize(e);

			_PreviousBounds = Bounds;
		}

		private void ListBoxSpecifySopClassesDefinitionFileDirectories_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			_SopClassesManager.UpdateRemoveButton();
		}

		private void axWebBrowserScript_CommandStateChange(object sender, AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent e)
		{
			_TCM_ValidationResultsManager1.CommandStateChange(sender, e);
		}

		private void axWebBrowserScript_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
		{
			string theFullFileName = _TCM_ValidationResultsManager1.GetFullFileNameFromHtmlLink(e.uRL.ToString());

			string theDirectory = System.IO.Path.GetDirectoryName(theFullFileName);
			string theFileNameOnly = System.IO.Path.GetFileName(theFullFileName);
		}

		private void axWebBrowserScript_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
		{
			Notify(new axWebNavigationComplete());
		}

		private void ContextMenu_ViewExpandedScript_Click(object sender, System.EventArgs e)
		{
			_SessionTreeViewManager.ViewExpandedScriptFile();
		}

		private void ContextMenuRichTextBox_Popup(object sender, System.EventArgs e)
		{
			ContextMenu_Copy.Visible = false;
			if (GetActiveTab()==ProjectForm2.ProjectFormActiveTab.SCRIPT_TAB||GetActiveTab()==ProjectForm2.ProjectFormActiveTab.ACTIVITY_LOGGING_TAB||GetActiveTab()==ProjectForm2.ProjectFormActiveTab.SPECIFY_SOP_CLASSES_TAB)
			{
				ContextMenu_Copy.Visible = true;
			}
		}

		private void ContextMenu_Copy_Click(object sender, System.EventArgs e)
		{
			GetMainForm().ActionEditCopy();		
		}

		private void CheckBoxDefineSQLength_CheckedChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				Dvtk.Sessions.Session theSelectedSession = GetSelectedSession();

				// Update the session member.
				if (theSelectedSession is Dvtk.Sessions.ScriptSession)
				{
					Dvtk.Sessions.ScriptSession theScriptSession = (Dvtk.Sessions.ScriptSession)theSelectedSession;
					theScriptSession.DefineSqLength = CheckBoxDefineSQLength.Checked;
				}
				if (theSelectedSession is Dvtk.Sessions.EmulatorSession)
				{
					Dvtk.Sessions.EmulatorSession theEmulatorSession = (Dvtk.Sessions.EmulatorSession)theSelectedSession;
					theEmulatorSession.DefineSqLength = CheckBoxDefineSQLength.Checked;
				}

				// Notift the rest of the world that the session has been changed.
				SessionChange theSessionChange = new SessionChange(theSelectedSession, SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}		
		}

		private void CheckBoxAddGroupLengths_CheckedChanged(object sender, System.EventArgs e)
		{
			// Only react when the user has made changes, not when the TCM_Update method has been called.
			if (_TCM_UpdateCount == 0)
			{
				Dvtk.Sessions.Session theSelectedSession = GetSelectedSession();

				// Update the session member.
				if (theSelectedSession is Dvtk.Sessions.ScriptSession)
				{
					Dvtk.Sessions.ScriptSession theScriptSession = (Dvtk.Sessions.ScriptSession)theSelectedSession;
					theScriptSession.AddGroupLength = CheckBoxAddGroupLengths.Checked;
				}
				if (theSelectedSession is Dvtk.Sessions.EmulatorSession)
				{
					Dvtk.Sessions.EmulatorSession theEmulatorSession = (Dvtk.Sessions.EmulatorSession)theSelectedSession;
					theEmulatorSession.AddGroupLength = CheckBoxAddGroupLengths.Checked;
				}

				// Notift the rest of the world that the session has been changed.
				SessionChange theSessionChange = new SessionChange(theSelectedSession, SessionChange.SessionChangeSubTypEnum.OTHER);
				Notify(theSessionChange);
			}		
		}

		private void ProjectForm2_Enter(object sender, System.EventArgs e)
		{
			Select();
		}

		private void ContextMenu_GenerateDICOMDIR_Click(object sender, System.EventArgs e)
		{
			_SessionTreeViewManager.GenerateDICOMDIR();		
		}

        private void ContextMenu_ValidateDicomdirWithoutRefFile_Click(object sender, System.EventArgs e) 
		{
            // Update the session object member.
            GetSelectedSession().ValidateReferencedFile = false;

            _SessionTreeViewManager.Execute();			
        }

        private void WebDescriptionView_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e) {
           string s = e.uRL.ToString();
           string directory = Path.GetDirectoryName(s);
           directory = directory + @"\" ;
           string resultFileName = Path.GetFileName(s);
           resultFileName =  resultFileName.Replace(".html" , ".xml");
            _SessionTreeViewManager.SearchAndSelectResultNode(directory ,resultFileName);

        
        }

        
	}
}
