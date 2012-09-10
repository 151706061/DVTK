// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Runtime.InteropServices;
using System.IO;
using Dvtk;
using DvtkScriptSupport;

using HtmlTextRange = mshtml.IHTMLTxtRange;
using HtmlDocument = mshtml.HTMLDocument;
using HtmlBody = mshtml.HTMLBody;
using HtmlSelection = mshtml.IHTMLSelectionObject;
using HtmlEventObject = mshtml.IHTMLEventObj;
 
namespace Dvt
{
	/// <summary>
	/// Summary description for ProjectForm.
	/// </summary>
    public class ProjectForm : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Panel TaskPanel;
        private System.Windows.Forms.TreeView SessionBrowser;
        private System.Windows.Forms.Splitter ViewSplitter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.LinkLabel linkLabel5;
        private System.Windows.Forms.LinkLabel linkLabel6;
        private System.Windows.Forms.LinkLabel linkLabel7;
        private System.Windows.Forms.LinkLabel linkLabel8;
        private System.Windows.Forms.LinkLabel linkLabel9;
        private System.Windows.Forms.LinkLabel linkLabel10;
        private System.Windows.Forms.ContextMenu ContextMenuSessionBrowser;
        private System.Windows.Forms.MenuItem CM_MarkIgnore;
        private System.Windows.Forms.MenuItem CM_UnmarkIgnore;
        private System.Windows.Forms.MenuItem CM_Properties;
        private System.Windows.Forms.OpenFileDialog DialogOpenSessionFile;
        private System.Windows.Forms.RichTextBox RichTextBoxScript;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox RichTextBoxInfo;
        private System.Windows.Forms.Panel PanelSessionPropertiesView;
        private System.Windows.Forms.VScrollBar VScrollBarSessionInfo;
        private System.Windows.Forms.Panel PanelSessionProperties;
        private System.Windows.Forms.Panel PanelDVTRoleSettingsContent;
        private System.Windows.Forms.Panel PanelDVTRoleSettingsTitle;
        private System.Windows.Forms.Panel PanelGeneralPropertiesContent;
        private System.Windows.Forms.Panel PanelGeneralPropertiesTitle;
        private System.Windows.Forms.Panel PanelSUTSettingContent;
        private System.Windows.Forms.Panel PanelSUTSettingTitle;
        private System.Windows.Forms.Panel PanelSecuritySettingsTitle;
        private System.Windows.Forms.Panel PanelSecuritySettingsContent;
        private System.Windows.Forms.TextBox TextBoxDVTAETitle;
        private System.Windows.Forms.Label LabelDVTAETitle;
        private System.Windows.Forms.Label LabelDVTListenPort;
        private System.Windows.Forms.Label LabelDVTSocketTimeOut;
        private System.Windows.Forms.Label LabelDVTMaxPDU;
        private System.Windows.Forms.Label LabelSelectSopClasses;
        private System.Windows.Forms.Button ButtonBrowseResultsDir;
        private System.Windows.Forms.Label LabelResultsDir;
        private System.Windows.Forms.ComboBox ComboBoxSessionType;
        private System.Windows.Forms.Label LabelSessionType;
        private System.Windows.Forms.Label LabelDate;
        private System.Windows.Forms.Label LabelSessionTitle;
        private System.Windows.Forms.Label LabelTestedBy;
        private System.Windows.Forms.TextBox TextBoxTestedBy;
        private System.Windows.Forms.Label LabelScriptsDir;
        private System.Windows.Forms.Button ButtonBrowseScriptsDir;
        private System.Windows.Forms.Button ButtonSpecifySOPCLasses;
        private System.Windows.Forms.Label LabelSUTAETitle;
        private System.Windows.Forms.Label LabelSUTTCPIPAddress;
        private System.Windows.Forms.Label LabelSUTListenPort;
        private System.Windows.Forms.TextBox TextBoxSUTAETitle;
        private System.Windows.Forms.Button ButtonCheckTCPIPAddress;
        private System.Windows.Forms.FolderBrowserDialog DialogBrowseFolder;
        private System.Windows.Forms.TextBox TextBoxResultsRoot;
        private System.Windows.Forms.TextBox TextBoxScriptRoot;
        private System.Windows.Forms.RichTextBox RichTextBoxActivityLogging;
        private System.Windows.Forms.RadioButton ButtonGeneralInformation;
        private System.Windows.Forms.RadioButton ButtonActivityLogging;
        private System.Windows.Forms.RadioButton ButtonDetailedValidation;
        private System.Windows.Forms.PictureBox MinGSPSettings;
        private System.Windows.Forms.PictureBox MinDVTRoleSettings;
        private System.Windows.Forms.PictureBox MinSUTSettings;
        private System.Windows.Forms.PictureBox MinSecuritySettings;
        private System.Windows.Forms.PictureBox MaxSecuritySettings;
        private System.Windows.Forms.PictureBox MaxSUTSettings;
        private System.Windows.Forms.PictureBox MaxDVTRoleSettings;
        private System.Windows.Forms.PictureBox MaxGSPSettings;
        private System.Windows.Forms.DataGrid SOPClasses;
        private System.Windows.Forms.Panel PanelSOPClasses;
        private System.Windows.Forms.Button ButtonReturnToSessionProperties;
        private System.Windows.Forms.Label LabelSpecifyDefinitionRoot;
        private System.Data.DataSet dataSetSOPClasses;
        private System.Data.DataTable dataTableSOPClasses;
        private System.Data.DataColumn dataColumnLoaded;
        private System.Data.DataColumn dataColumnFilename;
        private System.Data.DataColumn dataColumnSOPClassName;
        private System.Data.DataColumn dataColumnSOPClassUID;
        private System.Data.DataColumn dataColumnAETitleVersion;
        private System.Data.DataColumn dataColumnDefinitionRoot;
        private System.Windows.Forms.Button ButtonAddDefinitionRoot;
        private System.Windows.Forms.ListBox ListBoxDefinitionDirs;
        private System.Windows.Forms.Button ButtonRemoveDefinitionRoot;
        private System.Windows.Forms.Label LabelSelect1ItemMsg;
        private System.Windows.Forms.Label LabelCategories;
        private System.Windows.Forms.ListBox ListBoxSecuritySettings;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox GroupSecurityGeneral;
        private System.Windows.Forms.CheckBox CheckBoxCacheSecureSessions;
        private System.Windows.Forms.GroupBox GroupSecurityKeyExchange;
        private System.Windows.Forms.CheckBox CheckBoxKeyExchangeRSA;
        private System.Windows.Forms.CheckBox CheckBoxKeyExchangeDH;
        private System.Windows.Forms.GroupBox GroupSecurityAuthentication;
        private System.Windows.Forms.CheckBox CheckBoxAuthenticationRSA;
        private System.Windows.Forms.CheckBox CheckBoxAuthenticationDSA;
        private System.Windows.Forms.GroupBox GroupSecurityVersion;
        private System.Windows.Forms.CheckBox CheckBoxTLS;
        private System.Windows.Forms.CheckBox CheckBoxSSL;
        private System.Windows.Forms.GroupBox GroupSecurityEncryption;
        private System.Windows.Forms.CheckBox CheckBoxEncryptionNone;
        private System.Windows.Forms.CheckBox CheckBoxEncryptionTripleDES;
        private System.Windows.Forms.CheckBox CheckBoxEncryptionAES128;
        private System.Windows.Forms.CheckBox CheckBoxEncryptionAES256;
        private System.Windows.Forms.GroupBox GroupSecurityDataIntegrity;
        private System.Windows.Forms.CheckBox CheckBoxDataIntegritySHA;
        private System.Windows.Forms.CheckBox CheckBoxDataIntegrityMD5;
        private System.Windows.Forms.CheckBox CheckBoxSecureConnection;
        private System.Windows.Forms.OpenFileDialog DialogAddCredentialFiles;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox TextBoxSessionTitle;
        private System.Windows.Forms.NumericUpDown NumericSessonID;
        private System.Windows.Forms.MenuItem CM_Save;
        private System.Windows.Forms.MenuItem CM_Execute;
        private System.Windows.Forms.MenuItem CM_Sep_1;
        private System.Windows.Forms.MenuItem CM_Sep_2;
        private System.Windows.Forms.MenuItem CM_Sep_4;
        private System.Windows.Forms.MenuItem CM_Sep_3;
        private System.Windows.Forms.MenuItem CM_Display;
        private System.Windows.Forms.MenuItem CM_AddNewSession;
        private System.Windows.Forms.MenuItem CM_Remove;
        private System.Windows.Forms.MenuItem CM_AddExistingSession;
        private System.Windows.Forms.Label LabelSelectAETitleVersion;
        private System.Windows.Forms.ComboBox ComboBoxAETitleVersion;
        private System.Windows.Forms.DateTimePicker DateTested;
        private System.Windows.Forms.CheckBox CheckBoxCheckRemoteCertificates;
        private System.Windows.Forms.Button ButtonViewCertificates;
        private System.Windows.Forms.Button ButtonViewCredentials;
        private System.Windows.Forms.Button ButtonCreateCertificate;
        private System.Windows.Forms.Button ButtonSpecifyTransferSyntaxes;
        private System.Windows.Forms.Label LabelSpecifyTransferSyntaxes;
        private System.Windows.Forms.NumericUpDown NumericDVTListenPort;
        private System.Windows.Forms.NumericUpDown NumericDVTTimeOut;
        private System.Windows.Forms.NumericUpDown NumericDVTMaxPDU;
        private System.Windows.Forms.NumericUpDown NumericSUTListenPort;
        private System.Windows.Forms.MenuItem CM_StartEmulator;
        private System.Windows.Forms.MenuItem CM_ValidateMedia;
        private System.Windows.Forms.OpenFileDialog DialogOpenMediaFile;
        private AxSHDocVw.AxWebBrowser WebDescriptionView;
        private System.Windows.Forms.ContextMenu ContextMenuDetailedValidation;
        private System.Windows.Forms.MenuItem menuItem3;
        private System.Windows.Forms.MenuItem CM_Back;
        private System.Windows.Forms.MenuItem CM_Forward;
        private System.Windows.Forms.MenuItem CM_Find;
        private System.Windows.Forms.MenuItem CM_FindAgain;
        private System.Windows.Forms.Label LabelSUTMaxPDU;
        private System.Windows.Forms.NumericUpDown NumericSUTMaxPDU;
        private System.Windows.Forms.MenuItem CM_Edit;
        private System.Windows.Forms.Button ButtonBrowseDescriptionDir;
        private System.Windows.Forms.Label LabelDescriptionDir;
        private System.Windows.Forms.TextBox TextBoxDescriptionRoot;
        private System.Windows.Forms.TextBox TextBoxDVTImplClassUID;
        private System.Windows.Forms.TextBox TextBoxDVTImplVersionName;
        private System.Windows.Forms.Label LabelDVTImplClassUID;
        private System.Windows.Forms.Label LabelDVTImplVersionName;
        private System.Windows.Forms.Label LabelSUTImplClassUID;
        private System.Windows.Forms.Label LabelSUTImplVersionName;
        private System.Windows.Forms.TextBox TextBoxSUTImplClassUID;
        private System.Windows.Forms.TextBox TextBoxSUTImplVersionName;
        private System.Windows.Forms.MenuItem CM_Copy;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.ToolTip toolTip;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.TextBox TextBoxSUTTCPIPAddress;

        public event EventHandler RunningProcessEvent;

        public ProjectForm(Project project_data)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            // Set the callback for activity logging.
            _activity_handler = new Dvtk.Events.ActivityReportEventHandler(this.OnActivityReportEvent);

            // Set the event handler used when a process if finished executing.
            this.RunningProcessEvent += new EventHandler(FinishRunningProcessExecution);

            this.project = project_data;

            if (this.project.GetNrSessions () > 0)
            {
                this.UpdateSessionTreeView ();
                this.SessionBrowser.Select();
            }
            else
            {
                // No session file is available, this means that no session is selected.
                // Normally when selecting a session, all controls on the form are updated and
                // resized. Now we have to explicitly resize the form.
                this._active_page = ActivePage.session;
                this.UpdatePageVisibility ();
                //this.ResizeProjectForm ();
            }

            // Initialize the tablestyle for the SOPClass view. This can be done
            // only once.
            this.SOPClasses.TableStyles.Add (this.CreateTableStyle());
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing )
        {
            if( disposing )
            {
            }
            base.Dispose( disposing );
        }

        public enum FormStatus : byte { no_process_running, script_running, storage_scp_running, print_scp_running };
        public enum ActivePage : byte { activity_reporting,
                                        description,
                                        detailed_validation,
                                        script,
                                        session,
                                        sop_classes_view,
                                        validation_results  };

        private Project                 project = null;
        private string                  selected_script = "";
        private string                  selected_description = "";
        private string                  selected_results = "";
        private Dvtk.Sessions.Session   selected_session = null;
        private bool                    show_general_session_properties = true;
        private bool                    show_dvt_role_settings = true;
        private bool                    show_sut_settings = true;
        private bool                    show_security_settings = true;
        private int                     border_width = 8;
        private int                     scrollbar_offset = 0;
        private MouseButtons            button_clicked;
        private int                     mouse_x = 0;
        private int                     mouse_y = 0;
        private ArrayList               definition_files;
        private int                     changed_cell = -1;
        private bool                    loaded_state;
        private bool                    _contains_errors = false;
        private bool                    _contains_warnings = false;
        private Thread                  script_thread;
        private FormStatus              _process_status = FormStatus.no_process_running;
        private ActivePage              _active_page = ActivePage.session;
        private Dvtk.Events.ActivityReportEventHandler  _activity_handler;
        private ArrayList               urlsVisited = new ArrayList();
        private int                     currentUrlIndex = -1;  // no sites visited initially
        private TreeNode                last_selected_session;
        private TreeNode                last_selected_script;
        private TreeNode                last_selected_result;
        private const string            UNCATEGORIZED_RESULTS = "Uncategorized results";

        public Dvtk.Sessions.Session active_session
        {
            get { return this.selected_session; }
        }

        public ActivePage active_page
        {
            get { return this._active_page; }
        }

        public bool contains_errors
        {
            get { return this._contains_errors; }
        }

        public bool contains_warnings
        {
            get { return this._contains_warnings; }
        }

        public string active_script
        {
            get { return this.selected_script; }
        }

        public FormStatus process_status
        {
            get { return this._process_status; }
        }

        public bool can_navigate_back
        {
            get
            {
//                if (this.selected_description == "")
//                    return false;
                // enable / disable the Back and Forward buttons
                return (currentUrlIndex > 0 ) ? true : false;
            }
        }

        public bool can_navigate_forward
        {
            get
            {
//                if (this.selected_description == "")
//                    return false;
                // enable / disable the Back and Forward buttons
                return (currentUrlIndex >= urlsVisited.Count-1) ? false : true;
            }
        }

        private void UpdateDetailedResultsView ()
        {
            if (this.selected_results != "")
            {
                object Zero = 0;
                object EmptyString = "";

                XslTransform xslt = new XslTransform ();
                xslt.Load (
                    System.IO.Path.Combine(
                    Application.StartupPath, 
                    "DVT_RESULTS.xslt")
                    );
                XPathDocument xpathdocument = new XPathDocument (this.selected_results);
                XmlTextWriter writer = new XmlTextWriter (this.selected_results.Replace (".xml", ".html"), System.Text.Encoding.UTF8);
                writer.Formatting = Formatting.None;
                xslt.Transform (xpathdocument, null, writer, null);
                writer.Flush ();
                writer.Close ();
                this.WebDescriptionView.Navigate (this.selected_results.Replace (".xml", ".html"), ref Zero, ref EmptyString, ref EmptyString, ref EmptyString);
            }
        }

        private void UpdateSessionPropertiesScrollbar ()
        {
            int height;
            // Calculate the total height of the panels in the session
            // properties view.

            height = this.PanelGeneralPropertiesTitle.Size.Height +
                this.PanelDVTRoleSettingsTitle.Size.Height +
                this.PanelSUTSettingTitle.Size.Height +
                this.PanelSecuritySettingsTitle.Size.Height +
                this.border_width * 4;
            if (this.show_general_session_properties &&
                (this.project.GetNrSessions() > 0))
                height += this.PanelGeneralPropertiesContent.Size.Height;
            if ((this.show_dvt_role_settings) &&
                !(this.selected_session is Dvtk.Sessions.MediaSession) &&
                (this.project.GetNrSessions() > 0))
                height += this.PanelDVTRoleSettingsContent.Size.Height;
            if ((this.show_sut_settings) &&
                !(this.selected_session is Dvtk.Sessions.MediaSession) &&
                (this.project.GetNrSessions() > 0))
                height += this.PanelSUTSettingContent.Size.Height;
            if ((this.CheckBoxSecureConnection.Checked) &&
                (this.show_security_settings) &&
                !(this.selected_session is Dvtk.Sessions.MediaSession) &&
                (this.project.GetNrSessions() > 0))
                height += this.PanelSecuritySettingsContent.Size.Height;

            if (height + this.scrollbar_offset < this.PanelSessionProperties.Size.Height)
                this.scrollbar_offset = this.PanelSessionProperties.Size.Height - height;

            if (this.scrollbar_offset > 0)
                this.scrollbar_offset = 0;

            this.VScrollBarSessionInfo.Value = -this.scrollbar_offset;

            this.VScrollBarSessionInfo.Minimum = 0;
            if (height < this.PanelSessionProperties.Size.Height)
            {
                this.VScrollBarSessionInfo.Enabled = false;
                this.VScrollBarSessionInfo.Maximum = 0;
            }
            else
            {
                this.VScrollBarSessionInfo.Enabled = true;
                this.VScrollBarSessionInfo.Maximum = height;
                this.VScrollBarSessionInfo.LargeChange = this.PanelSessionProperties.Size.Height;
            }
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProjectForm));
			this.TaskPanel = new System.Windows.Forms.Panel();
			this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel6 = new System.Windows.Forms.Panel();
			this.linkLabel3 = new System.Windows.Forms.LinkLabel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.linkLabel4 = new System.Windows.Forms.LinkLabel();
			this.linkLabel5 = new System.Windows.Forms.LinkLabel();
			this.linkLabel6 = new System.Windows.Forms.LinkLabel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.linkLabel7 = new System.Windows.Forms.LinkLabel();
			this.linkLabel8 = new System.Windows.Forms.LinkLabel();
			this.linkLabel9 = new System.Windows.Forms.LinkLabel();
			this.linkLabel10 = new System.Windows.Forms.LinkLabel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.SessionBrowser = new System.Windows.Forms.TreeView();
			this.ContextMenuSessionBrowser = new System.Windows.Forms.ContextMenu();
			this.CM_MarkIgnore = new System.Windows.Forms.MenuItem();
			this.CM_UnmarkIgnore = new System.Windows.Forms.MenuItem();
			this.CM_Sep_1 = new System.Windows.Forms.MenuItem();
			this.CM_Execute = new System.Windows.Forms.MenuItem();
			this.CM_StartEmulator = new System.Windows.Forms.MenuItem();
			this.CM_ValidateMedia = new System.Windows.Forms.MenuItem();
			this.CM_Sep_2 = new System.Windows.Forms.MenuItem();
			this.CM_Display = new System.Windows.Forms.MenuItem();
			this.CM_Edit = new System.Windows.Forms.MenuItem();
			this.CM_Sep_3 = new System.Windows.Forms.MenuItem();
			this.CM_AddNewSession = new System.Windows.Forms.MenuItem();
			this.CM_AddExistingSession = new System.Windows.Forms.MenuItem();
			this.CM_Remove = new System.Windows.Forms.MenuItem();
			this.CM_Save = new System.Windows.Forms.MenuItem();
			this.CM_Sep_4 = new System.Windows.Forms.MenuItem();
			this.CM_Properties = new System.Windows.Forms.MenuItem();
			this.ViewSplitter = new System.Windows.Forms.Splitter();
			this.RichTextBoxScript = new System.Windows.Forms.RichTextBox();
			this.RichTextBoxInfo = new System.Windows.Forms.RichTextBox();
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
			this.NumericSessonID = new System.Windows.Forms.NumericUpDown();
			this.ComboBoxSessionType = new System.Windows.Forms.ComboBox();
			this.LabelSelectSopClasses = new System.Windows.Forms.Label();
			this.ButtonBrowseResultsDir = new System.Windows.Forms.Button();
			this.LabelResultsDir = new System.Windows.Forms.Label();
			this.LabelSessionType = new System.Windows.Forms.Label();
			this.LabelDate = new System.Windows.Forms.Label();
			this.LabelSessionTitle = new System.Windows.Forms.Label();
			this.LabelTestedBy = new System.Windows.Forms.Label();
			this.TextBoxTestedBy = new System.Windows.Forms.TextBox();
			this.TextBoxResultsRoot = new System.Windows.Forms.TextBox();
			this.TextBoxScriptRoot = new System.Windows.Forms.TextBox();
			this.LabelScriptsDir = new System.Windows.Forms.Label();
			this.ButtonBrowseScriptsDir = new System.Windows.Forms.Button();
			this.ButtonSpecifySOPCLasses = new System.Windows.Forms.Button();
			this.TextBoxSessionTitle = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.DateTested = new System.Windows.Forms.DateTimePicker();
			this.ButtonSpecifyTransferSyntaxes = new System.Windows.Forms.Button();
			this.LabelSpecifyTransferSyntaxes = new System.Windows.Forms.Label();
			this.ButtonBrowseDescriptionDir = new System.Windows.Forms.Button();
			this.LabelDescriptionDir = new System.Windows.Forms.Label();
			this.TextBoxDescriptionRoot = new System.Windows.Forms.TextBox();
			this.PanelGeneralPropertiesTitle = new System.Windows.Forms.Panel();
			this.MinGSPSettings = new System.Windows.Forms.PictureBox();
			this.label5 = new System.Windows.Forms.Label();
			this.MaxGSPSettings = new System.Windows.Forms.PictureBox();
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
			this.DialogOpenSessionFile = new System.Windows.Forms.OpenFileDialog();
			this.PanelSessionPropertiesView = new System.Windows.Forms.Panel();
			this.ButtonGeneralInformation = new System.Windows.Forms.RadioButton();
			this.RichTextBoxActivityLogging = new System.Windows.Forms.RichTextBox();
			this.PanelSessionProperties = new System.Windows.Forms.Panel();
			this.PanelSecuritySettingsTitle = new System.Windows.Forms.Panel();
			this.MinSecuritySettings = new System.Windows.Forms.PictureBox();
			this.MaxSecuritySettings = new System.Windows.Forms.PictureBox();
			this.CheckBoxSecureConnection = new System.Windows.Forms.CheckBox();
			this.PanelSecuritySettingsContent = new System.Windows.Forms.Panel();
			this.GroupSecurityKeyExchange = new System.Windows.Forms.GroupBox();
			this.CheckBoxKeyExchangeRSA = new System.Windows.Forms.CheckBox();
			this.CheckBoxKeyExchangeDH = new System.Windows.Forms.CheckBox();
			this.GroupSecurityVersion = new System.Windows.Forms.GroupBox();
			this.CheckBoxTLS = new System.Windows.Forms.CheckBox();
			this.CheckBoxSSL = new System.Windows.Forms.CheckBox();
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
			this.ListBoxSecuritySettings = new System.Windows.Forms.ListBox();
			this.LabelCategories = new System.Windows.Forms.Label();
			this.LabelSelect1ItemMsg = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.ButtonViewCertificates = new System.Windows.Forms.Button();
			this.ButtonViewCredentials = new System.Windows.Forms.Button();
			this.ButtonCreateCertificate = new System.Windows.Forms.Button();
			this.VScrollBarSessionInfo = new System.Windows.Forms.VScrollBar();
			this.ButtonActivityLogging = new System.Windows.Forms.RadioButton();
			this.ButtonDetailedValidation = new System.Windows.Forms.RadioButton();
			this.PanelSOPClasses = new System.Windows.Forms.Panel();
			this.ComboBoxAETitleVersion = new System.Windows.Forms.ComboBox();
			this.LabelSelectAETitleVersion = new System.Windows.Forms.Label();
			this.ButtonRemoveDefinitionRoot = new System.Windows.Forms.Button();
			this.ListBoxDefinitionDirs = new System.Windows.Forms.ListBox();
			this.LabelSpecifyDefinitionRoot = new System.Windows.Forms.Label();
			this.ButtonAddDefinitionRoot = new System.Windows.Forms.Button();
			this.SOPClasses = new System.Windows.Forms.DataGrid();
			this.WebDescriptionView = new AxSHDocVw.AxWebBrowser();
			this.ButtonReturnToSessionProperties = new System.Windows.Forms.Button();
			this.dataSetSOPClasses = new System.Data.DataSet();
			this.dataTableSOPClasses = new System.Data.DataTable();
			this.dataColumnLoaded = new System.Data.DataColumn();
			this.dataColumnFilename = new System.Data.DataColumn();
			this.dataColumnSOPClassName = new System.Data.DataColumn();
			this.dataColumnSOPClassUID = new System.Data.DataColumn();
			this.dataColumnAETitleVersion = new System.Data.DataColumn();
			this.dataColumnDefinitionRoot = new System.Data.DataColumn();
			this.DialogBrowseFolder = new System.Windows.Forms.FolderBrowserDialog();
			this.DialogAddCredentialFiles = new System.Windows.Forms.OpenFileDialog();
			this.DialogOpenMediaFile = new System.Windows.Forms.OpenFileDialog();
			this.ContextMenuDetailedValidation = new System.Windows.Forms.ContextMenu();
			this.CM_Back = new System.Windows.Forms.MenuItem();
			this.CM_Forward = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.CM_Copy = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.CM_Find = new System.Windows.Forms.MenuItem();
			this.CM_FindAgain = new System.Windows.Forms.MenuItem();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.TaskPanel.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.PanelDVTRoleSettingsContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericDVTListenPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericDVTTimeOut)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericDVTMaxPDU)).BeginInit();
			this.PanelDVTRoleSettingsTitle.SuspendLayout();
			this.PanelGeneralPropertiesContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericSessonID)).BeginInit();
			this.PanelGeneralPropertiesTitle.SuspendLayout();
			this.PanelSUTSettingContent.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NumericSUTListenPort)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericSUTMaxPDU)).BeginInit();
			this.PanelSUTSettingTitle.SuspendLayout();
			this.PanelSessionPropertiesView.SuspendLayout();
			this.PanelSessionProperties.SuspendLayout();
			this.PanelSecuritySettingsTitle.SuspendLayout();
			this.PanelSecuritySettingsContent.SuspendLayout();
			this.GroupSecurityKeyExchange.SuspendLayout();
			this.GroupSecurityVersion.SuspendLayout();
			this.GroupSecurityGeneral.SuspendLayout();
			this.GroupSecurityEncryption.SuspendLayout();
			this.GroupSecurityDataIntegrity.SuspendLayout();
			this.GroupSecurityAuthentication.SuspendLayout();
			this.PanelSOPClasses.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.SOPClasses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.WebDescriptionView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSetSOPClasses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTableSOPClasses)).BeginInit();
			this.SuspendLayout();
			// 
			// TaskPanel
			// 
			this.TaskPanel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(123)), ((System.Byte)(151)), ((System.Byte)(223)));
			this.TaskPanel.Controls.Add(this.vScrollBar1);
			this.TaskPanel.Controls.Add(this.panel5);
			this.TaskPanel.Controls.Add(this.panel6);
			this.TaskPanel.Controls.Add(this.panel2);
			this.TaskPanel.Controls.Add(this.panel1);
			this.TaskPanel.Controls.Add(this.panel3);
			this.TaskPanel.Controls.Add(this.panel4);
			this.TaskPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.TaskPanel.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.TaskPanel.Location = new System.Drawing.Point(0, 0);
			this.TaskPanel.Name = "TaskPanel";
			this.TaskPanel.Size = new System.Drawing.Size(224, 746);
			this.TaskPanel.TabIndex = 0;
			this.TaskPanel.Visible = false;
			// 
			// vScrollBar1
			// 
			this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
			this.vScrollBar1.Location = new System.Drawing.Point(207, 0);
			this.vScrollBar1.Name = "vScrollBar1";
			this.vScrollBar1.Size = new System.Drawing.Size(17, 746);
			this.vScrollBar1.TabIndex = 4;
			this.vScrollBar1.Visible = false;
			// 
			// panel5
			// 
			this.panel5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(218)), ((System.Byte)(226)), ((System.Byte)(245)));
			this.panel5.Location = new System.Drawing.Point(16, 328);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(184, 240);
			this.panel5.TabIndex = 3;
			this.panel5.Paint += new System.Windows.Forms.PaintEventHandler(this.TaskPanelPaint);
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.SystemColors.Menu;
			this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
			this.panel6.Controls.Add(this.linkLabel3);
			this.panel6.Location = new System.Drawing.Point(16, 296);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(184, 32);
			this.panel6.TabIndex = 2;
			// 
			// linkLabel3
			// 
			this.linkLabel3.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel3.Location = new System.Drawing.Point(16, 8);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new System.Drawing.Size(128, 23);
			this.linkLabel3.TabIndex = 0;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Details";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(218)), ((System.Byte)(226)), ((System.Byte)(245)));
			this.panel2.Controls.Add(this.linkLabel4);
			this.panel2.Controls.Add(this.linkLabel5);
			this.panel2.Controls.Add(this.linkLabel6);
			this.panel2.Location = new System.Drawing.Point(16, 48);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(184, 80);
			this.panel2.TabIndex = 1;
			this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.TaskPanelPaint);
			// 
			// linkLabel4
			// 
			this.linkLabel4.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel4.Enabled = false;
			this.linkLabel4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.linkLabel4.Location = new System.Drawing.Point(40, 8);
			this.linkLabel4.Name = "linkLabel4";
			this.linkLabel4.TabIndex = 0;
			this.linkLabel4.TabStop = true;
			this.linkLabel4.Text = "Execute script";
			// 
			// linkLabel5
			// 
			this.linkLabel5.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel5.Enabled = false;
			this.linkLabel5.Location = new System.Drawing.Point(40, 32);
			this.linkLabel5.Name = "linkLabel5";
			this.linkLabel5.Size = new System.Drawing.Size(136, 23);
			this.linkLabel5.TabIndex = 0;
			this.linkLabel5.TabStop = true;
			this.linkLabel5.Text = "Find next script to execute";
			// 
			// linkLabel6
			// 
			this.linkLabel6.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel6.Enabled = false;
			this.linkLabel6.Location = new System.Drawing.Point(40, 56);
			this.linkLabel6.Name = "linkLabel6";
			this.linkLabel6.Size = new System.Drawing.Size(120, 23);
			this.linkLabel6.TabIndex = 0;
			this.linkLabel6.TabStop = true;
			this.linkLabel6.Text = "Find a specific script";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Menu;
			this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
			this.panel1.Controls.Add(this.linkLabel1);
			this.panel1.Location = new System.Drawing.Point(16, 16);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(184, 32);
			this.panel1.TabIndex = 0;
			// 
			// linkLabel1
			// 
			this.linkLabel1.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(16, 8);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(128, 23);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Script Tasks";
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(218)), ((System.Byte)(226)), ((System.Byte)(245)));
			this.panel3.Controls.Add(this.linkLabel7);
			this.panel3.Controls.Add(this.linkLabel8);
			this.panel3.Controls.Add(this.linkLabel9);
			this.panel3.Controls.Add(this.linkLabel10);
			this.panel3.Location = new System.Drawing.Point(16, 176);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(184, 104);
			this.panel3.TabIndex = 1;
			this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.TaskPanelPaint);
			// 
			// linkLabel7
			// 
			this.linkLabel7.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel7.Enabled = false;
			this.linkLabel7.Location = new System.Drawing.Point(40, 8);
			this.linkLabel7.Name = "linkLabel7";
			this.linkLabel7.Size = new System.Drawing.Size(128, 23);
			this.linkLabel7.TabIndex = 0;
			this.linkLabel7.TabStop = true;
			this.linkLabel7.Text = "Find next passed result";
			// 
			// linkLabel8
			// 
			this.linkLabel8.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel8.Enabled = false;
			this.linkLabel8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.linkLabel8.Location = new System.Drawing.Point(40, 32);
			this.linkLabel8.Name = "linkLabel8";
			this.linkLabel8.Size = new System.Drawing.Size(128, 23);
			this.linkLabel8.TabIndex = 0;
			this.linkLabel8.TabStop = true;
			this.linkLabel8.Text = "Find next failed result";
			// 
			// linkLabel9
			// 
			this.linkLabel9.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel9.Enabled = false;
			this.linkLabel9.Location = new System.Drawing.Point(40, 56);
			this.linkLabel9.Name = "linkLabel9";
			this.linkLabel9.Size = new System.Drawing.Size(128, 23);
			this.linkLabel9.TabIndex = 0;
			this.linkLabel9.TabStop = true;
			this.linkLabel9.Text = "Open error summary";
			// 
			// linkLabel10
			// 
			this.linkLabel10.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel10.Enabled = false;
			this.linkLabel10.Location = new System.Drawing.Point(40, 80);
			this.linkLabel10.Name = "linkLabel10";
			this.linkLabel10.Size = new System.Drawing.Size(128, 23);
			this.linkLabel10.TabIndex = 0;
			this.linkLabel10.TabStop = true;
			this.linkLabel10.Text = "Open warning summary";
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.SystemColors.Menu;
			this.panel4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel4.BackgroundImage")));
			this.panel4.Controls.Add(this.linkLabel2);
			this.panel4.Location = new System.Drawing.Point(16, 144);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(184, 32);
			this.panel4.TabIndex = 0;
			// 
			// linkLabel2
			// 
			this.linkLabel2.BackColor = System.Drawing.Color.Transparent;
			this.linkLabel2.Enabled = false;
			this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel2.Location = new System.Drawing.Point(16, 8);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(128, 23);
			this.linkLabel2.TabIndex = 0;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Browse Results";
			// 
			// SessionBrowser
			// 
			this.SessionBrowser.BackColor = System.Drawing.SystemColors.Window;
			this.SessionBrowser.ContextMenu = this.ContextMenuSessionBrowser;
			this.SessionBrowser.Dock = System.Windows.Forms.DockStyle.Left;
			this.SessionBrowser.HideSelection = false;
			this.SessionBrowser.ImageIndex = -1;
			this.SessionBrowser.Location = new System.Drawing.Point(224, 0);
			this.SessionBrowser.Name = "SessionBrowser";
			this.SessionBrowser.PathSeparator = "|";
			this.SessionBrowser.SelectedImageIndex = -1;
			this.SessionBrowser.Size = new System.Drawing.Size(256, 746);
			this.SessionBrowser.TabIndex = 1;
			this.SessionBrowser.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SessionBrowser_MouseDown);
			this.SessionBrowser.DoubleClick += new System.EventHandler(this.SessionBrowser_DoubleClick);
			this.SessionBrowser.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.SessionBrowser_AfterSelect);
			// 
			// ContextMenuSessionBrowser
			// 
			this.ContextMenuSessionBrowser.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																									  this.CM_MarkIgnore,
																									  this.CM_UnmarkIgnore,
																									  this.CM_Sep_1,
																									  this.CM_Execute,
																									  this.CM_StartEmulator,
																									  this.CM_ValidateMedia,
																									  this.CM_Sep_2,
																									  this.CM_Display,
																									  this.CM_Edit,
																									  this.CM_Sep_3,
																									  this.CM_AddNewSession,
																									  this.CM_AddExistingSession,
																									  this.CM_Remove,
																									  this.CM_Save,
																									  this.CM_Sep_4,
																									  this.CM_Properties});
			// 
			// CM_MarkIgnore
			// 
			this.CM_MarkIgnore.Index = 0;
			this.CM_MarkIgnore.Text = "Mark results as Ignore";
			this.CM_MarkIgnore.Visible = false;
			this.CM_MarkIgnore.Click += new System.EventHandler(this.CM_MarkIgnore_Click);
			// 
			// CM_UnmarkIgnore
			// 
			this.CM_UnmarkIgnore.Index = 1;
			this.CM_UnmarkIgnore.Text = "Unmark results as Ignore";
			this.CM_UnmarkIgnore.Visible = false;
			this.CM_UnmarkIgnore.Click += new System.EventHandler(this.CM_UnmarkIgnore_Click);
			// 
			// CM_Sep_1
			// 
			this.CM_Sep_1.Index = 2;
			this.CM_Sep_1.Text = "-";
			this.CM_Sep_1.Visible = false;
			// 
			// CM_Execute
			// 
			this.CM_Execute.Index = 3;
			this.CM_Execute.Text = "Execute";
			this.CM_Execute.Click += new System.EventHandler(this.CM_Execute_Click);
			// 
			// CM_StartEmulator
			// 
			this.CM_StartEmulator.Index = 4;
			this.CM_StartEmulator.Text = "Start emulator";
			this.CM_StartEmulator.Click += new System.EventHandler(this.CM_StartEmulator_Click);
			// 
			// CM_ValidateMedia
			// 
			this.CM_ValidateMedia.Index = 5;
			this.CM_ValidateMedia.Text = "Validate media file";
			this.CM_ValidateMedia.Click += new System.EventHandler(this.CM_ValidateMedia_Click);
			// 
			// CM_Sep_2
			// 
			this.CM_Sep_2.Index = 6;
			this.CM_Sep_2.Text = "-";
			// 
			// CM_Display
			// 
			this.CM_Display.Index = 7;
			this.CM_Display.Text = "Display";
			this.CM_Display.Click += new System.EventHandler(this.CM_Display_Click);
			// 
			// CM_Edit
			// 
			this.CM_Edit.Index = 8;
			this.CM_Edit.Text = "Edit";
			this.CM_Edit.Click += new System.EventHandler(this.CM_Edit_Click);
			// 
			// CM_Sep_3
			// 
			this.CM_Sep_3.Index = 9;
			this.CM_Sep_3.Text = "-";
			// 
			// CM_AddNewSession
			// 
			this.CM_AddNewSession.Index = 10;
			this.CM_AddNewSession.Text = "Add New Session";
			this.CM_AddNewSession.Click += new System.EventHandler(this.CM_AddNewSession_Click);
			// 
			// CM_AddExistingSession
			// 
			this.CM_AddExistingSession.Index = 11;
			this.CM_AddExistingSession.Text = "Add Existing Session";
			this.CM_AddExistingSession.Click += new System.EventHandler(this.CM_AddExistingSession_Click);
			// 
			// CM_Remove
			// 
			this.CM_Remove.Index = 12;
			this.CM_Remove.Text = "Remove";
			this.CM_Remove.Click += new System.EventHandler(this.CM_Remove_Click);
			// 
			// CM_Save
			// 
			this.CM_Save.Index = 13;
			this.CM_Save.Text = "Save";
			this.CM_Save.Click += new System.EventHandler(this.CM_Save_Click);
			// 
			// CM_Sep_4
			// 
			this.CM_Sep_4.Index = 14;
			this.CM_Sep_4.Text = "-";
			// 
			// CM_Properties
			// 
			this.CM_Properties.Index = 15;
			this.CM_Properties.Text = "Properties";
			this.CM_Properties.Click += new System.EventHandler(this.CM_Properties_Click);
			// 
			// ViewSplitter
			// 
			this.ViewSplitter.Location = new System.Drawing.Point(480, 0);
			this.ViewSplitter.Name = "ViewSplitter";
			this.ViewSplitter.Size = new System.Drawing.Size(8, 746);
			this.ViewSplitter.TabIndex = 2;
			this.ViewSplitter.TabStop = false;
			this.ViewSplitter.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.ViewSplitter_SplitterMoved);
			// 
			// RichTextBoxScript
			// 
			this.RichTextBoxScript.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.RichTextBoxScript.Location = new System.Drawing.Point(944, 224);
			this.RichTextBoxScript.Name = "RichTextBoxScript";
			this.RichTextBoxScript.ReadOnly = true;
			this.RichTextBoxScript.Size = new System.Drawing.Size(32, 40);
			this.RichTextBoxScript.TabIndex = 0;
			this.RichTextBoxScript.Text = "";
			this.RichTextBoxScript.Visible = false;
			this.RichTextBoxScript.WordWrap = false;
			// 
			// RichTextBoxInfo
			// 
			this.RichTextBoxInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.RichTextBoxInfo.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(218)), ((System.Byte)(226)), ((System.Byte)(245)));
			this.RichTextBoxInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.RichTextBoxInfo.DetectUrls = false;
			this.RichTextBoxInfo.ForeColor = System.Drawing.SystemColors.Highlight;
			this.RichTextBoxInfo.Location = new System.Drawing.Point(518, 494);
			this.RichTextBoxInfo.Name = "RichTextBoxInfo";
			this.RichTextBoxInfo.ReadOnly = true;
			this.RichTextBoxInfo.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.RichTextBoxInfo.Size = new System.Drawing.Size(480, 56);
			this.RichTextBoxInfo.TabIndex = 0;
			this.RichTextBoxInfo.Text = "";
			// 
			// PanelDVTRoleSettingsContent
			// 
			this.PanelDVTRoleSettingsContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
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
			this.PanelDVTRoleSettingsContent.Location = new System.Drawing.Point(0, 336);
			this.PanelDVTRoleSettingsContent.Name = "PanelDVTRoleSettingsContent";
			this.PanelDVTRoleSettingsContent.Size = new System.Drawing.Size(440, 160);
			this.PanelDVTRoleSettingsContent.TabIndex = 4;
			// 
			// NumericDVTListenPort
			// 
			this.NumericDVTListenPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.NumericDVTListenPort.Location = new System.Drawing.Point(136, 80);
			this.NumericDVTListenPort.Maximum = new System.Decimal(new int[] {
																				 65535,
																				 0,
																				 0,
																				 0});
			this.NumericDVTListenPort.Name = "NumericDVTListenPort";
			this.NumericDVTListenPort.Size = new System.Drawing.Size(160, 20);
			this.NumericDVTListenPort.TabIndex = 7;
			this.toolTip.SetToolTip(this.NumericDVTListenPort, "Listen port for the DVT system.");
			this.NumericDVTListenPort.Validated += new System.EventHandler(this.NumericDVTListenPort_Validated);
			// 
			// TextBoxDVTAETitle
			// 
			this.TextBoxDVTAETitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxDVTAETitle.Location = new System.Drawing.Point(136, 8);
			this.TextBoxDVTAETitle.MaxLength = 16;
			this.TextBoxDVTAETitle.Name = "TextBoxDVTAETitle";
			this.TextBoxDVTAETitle.Size = new System.Drawing.Size(160, 20);
			this.TextBoxDVTAETitle.TabIndex = 6;
			this.TextBoxDVTAETitle.Text = "";
			this.toolTip.SetToolTip(this.TextBoxDVTAETitle, "Application Entity Title for the DVT system.");
			this.TextBoxDVTAETitle.TextChanged += new System.EventHandler(this.TextBoxDVTAETitle_TextChanged);
			// 
			// LabelDVTAETitle
			// 
			this.LabelDVTAETitle.Location = new System.Drawing.Point(16, 8);
			this.LabelDVTAETitle.Name = "LabelDVTAETitle";
			this.LabelDVTAETitle.TabIndex = 5;
			this.LabelDVTAETitle.Text = "AE Title:";
			// 
			// LabelDVTListenPort
			// 
			this.LabelDVTListenPort.Location = new System.Drawing.Point(16, 80);
			this.LabelDVTListenPort.Name = "LabelDVTListenPort";
			this.LabelDVTListenPort.TabIndex = 5;
			this.LabelDVTListenPort.Text = "Listen port:";
			// 
			// LabelDVTSocketTimeOut
			// 
			this.LabelDVTSocketTimeOut.Location = new System.Drawing.Point(16, 104);
			this.LabelDVTSocketTimeOut.Name = "LabelDVTSocketTimeOut";
			this.LabelDVTSocketTimeOut.TabIndex = 5;
			this.LabelDVTSocketTimeOut.Text = "Socket time-out:";
			// 
			// LabelDVTMaxPDU
			// 
			this.LabelDVTMaxPDU.Location = new System.Drawing.Point(16, 128);
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
			this.NumericDVTTimeOut.Location = new System.Drawing.Point(136, 104);
			this.NumericDVTTimeOut.Maximum = new System.Decimal(new int[] {
																			  1000,
																			  0,
																			  0,
																			  0});
			this.NumericDVTTimeOut.Name = "NumericDVTTimeOut";
			this.NumericDVTTimeOut.Size = new System.Drawing.Size(160, 20);
			this.NumericDVTTimeOut.TabIndex = 7;
			this.toolTip.SetToolTip(this.NumericDVTTimeOut, "Socket time-out for the DVT system.");
			this.NumericDVTTimeOut.Validated += new System.EventHandler(this.NumericDVTTimeOut_Validated);
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
			this.NumericDVTMaxPDU.Location = new System.Drawing.Point(136, 128);
			this.NumericDVTMaxPDU.Maximum = new System.Decimal(new int[] {
																			 1048576,
																			 0,
																			 0,
																			 0});
			this.NumericDVTMaxPDU.Name = "NumericDVTMaxPDU";
			this.NumericDVTMaxPDU.Size = new System.Drawing.Size(160, 20);
			this.NumericDVTMaxPDU.TabIndex = 7;
			this.toolTip.SetToolTip(this.NumericDVTMaxPDU, "Maximum receivable PDU length for the DVT system.");
			this.NumericDVTMaxPDU.Validated += new System.EventHandler(this.NumericDVTMaxPDU_Validated);
			// 
			// TextBoxDVTImplClassUID
			// 
			this.TextBoxDVTImplClassUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxDVTImplClassUID.Location = new System.Drawing.Point(136, 32);
			this.TextBoxDVTImplClassUID.MaxLength = 64;
			this.TextBoxDVTImplClassUID.Name = "TextBoxDVTImplClassUID";
			this.TextBoxDVTImplClassUID.Size = new System.Drawing.Size(160, 20);
			this.TextBoxDVTImplClassUID.TabIndex = 6;
			this.TextBoxDVTImplClassUID.Text = "";
			this.toolTip.SetToolTip(this.TextBoxDVTImplClassUID, "Implementation Class UID for the DVT system.");
			this.TextBoxDVTImplClassUID.TextChanged += new System.EventHandler(this.TextBoxDVTImplClassUID_TextChanged);
			// 
			// TextBoxDVTImplVersionName
			// 
			this.TextBoxDVTImplVersionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxDVTImplVersionName.Location = new System.Drawing.Point(136, 56);
			this.TextBoxDVTImplVersionName.MaxLength = 16;
			this.TextBoxDVTImplVersionName.Name = "TextBoxDVTImplVersionName";
			this.TextBoxDVTImplVersionName.Size = new System.Drawing.Size(160, 20);
			this.TextBoxDVTImplVersionName.TabIndex = 6;
			this.TextBoxDVTImplVersionName.Text = "";
			this.toolTip.SetToolTip(this.TextBoxDVTImplVersionName, "Implementation Version Name for the DVT system.");
			this.TextBoxDVTImplVersionName.TextChanged += new System.EventHandler(this.TextBoxDVTImplVersionName_TextChanged);
			// 
			// LabelDVTImplClassUID
			// 
			this.LabelDVTImplClassUID.Location = new System.Drawing.Point(16, 32);
			this.LabelDVTImplClassUID.Name = "LabelDVTImplClassUID";
			this.LabelDVTImplClassUID.TabIndex = 5;
			this.LabelDVTImplClassUID.Text = "Impl. Class UID:";
			// 
			// LabelDVTImplVersionName
			// 
			this.LabelDVTImplVersionName.Location = new System.Drawing.Point(16, 56);
			this.LabelDVTImplVersionName.Name = "LabelDVTImplVersionName";
			this.LabelDVTImplVersionName.Size = new System.Drawing.Size(112, 23);
			this.LabelDVTImplVersionName.TabIndex = 5;
			this.LabelDVTImplVersionName.Text = "Impl. Version Name:";
			// 
			// PanelDVTRoleSettingsTitle
			// 
			this.PanelDVTRoleSettingsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.PanelDVTRoleSettingsTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.PanelDVTRoleSettingsTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelDVTRoleSettingsTitle.Controls.Add(this.label1);
			this.PanelDVTRoleSettingsTitle.Controls.Add(this.MinDVTRoleSettings);
			this.PanelDVTRoleSettingsTitle.Controls.Add(this.MaxDVTRoleSettings);
			this.PanelDVTRoleSettingsTitle.Location = new System.Drawing.Point(0, 304);
			this.PanelDVTRoleSettingsTitle.Name = "PanelDVTRoleSettingsTitle";
			this.PanelDVTRoleSettingsTitle.Size = new System.Drawing.Size(440, 32);
			this.PanelDVTRoleSettingsTitle.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(24, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "DVT Role Settings";
			// 
			// MinDVTRoleSettings
			// 
			this.MinDVTRoleSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MinDVTRoleSettings.Image = ((System.Drawing.Image)(resources.GetObject("MinDVTRoleSettings.Image")));
			this.MinDVTRoleSettings.Location = new System.Drawing.Point(392, 4);
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
			this.MaxDVTRoleSettings.Location = new System.Drawing.Point(352, 4);
			this.MaxDVTRoleSettings.Name = "MaxDVTRoleSettings";
			this.MaxDVTRoleSettings.Size = new System.Drawing.Size(32, 24);
			this.MaxDVTRoleSettings.TabIndex = 1;
			this.MaxDVTRoleSettings.TabStop = false;
			this.MaxDVTRoleSettings.Click += new System.EventHandler(this.MaxDVTRoleSettings_Click);
			// 
			// PanelGeneralPropertiesContent
			// 
			this.PanelGeneralPropertiesContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.PanelGeneralPropertiesContent.BackColor = System.Drawing.SystemColors.ControlLight;
			this.PanelGeneralPropertiesContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelGeneralPropertiesContent.Controls.Add(this.NumericSessonID);
			this.PanelGeneralPropertiesContent.Controls.Add(this.ComboBoxSessionType);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelSelectSopClasses);
			this.PanelGeneralPropertiesContent.Controls.Add(this.ButtonBrowseResultsDir);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelResultsDir);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelSessionType);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelDate);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelSessionTitle);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelTestedBy);
			this.PanelGeneralPropertiesContent.Controls.Add(this.TextBoxTestedBy);
			this.PanelGeneralPropertiesContent.Controls.Add(this.TextBoxResultsRoot);
			this.PanelGeneralPropertiesContent.Controls.Add(this.TextBoxScriptRoot);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelScriptsDir);
			this.PanelGeneralPropertiesContent.Controls.Add(this.ButtonBrowseScriptsDir);
			this.PanelGeneralPropertiesContent.Controls.Add(this.ButtonSpecifySOPCLasses);
			this.PanelGeneralPropertiesContent.Controls.Add(this.TextBoxSessionTitle);
			this.PanelGeneralPropertiesContent.Controls.Add(this.label7);
			this.PanelGeneralPropertiesContent.Controls.Add(this.DateTested);
			this.PanelGeneralPropertiesContent.Controls.Add(this.ButtonSpecifyTransferSyntaxes);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelSpecifyTransferSyntaxes);
			this.PanelGeneralPropertiesContent.Controls.Add(this.ButtonBrowseDescriptionDir);
			this.PanelGeneralPropertiesContent.Controls.Add(this.LabelDescriptionDir);
			this.PanelGeneralPropertiesContent.Controls.Add(this.TextBoxDescriptionRoot);
			this.PanelGeneralPropertiesContent.DockPadding.All = 25;
			this.PanelGeneralPropertiesContent.Location = new System.Drawing.Point(0, 32);
			this.PanelGeneralPropertiesContent.Name = "PanelGeneralPropertiesContent";
			this.PanelGeneralPropertiesContent.Size = new System.Drawing.Size(440, 264);
			this.PanelGeneralPropertiesContent.TabIndex = 3;
			// 
			// NumericSessonID
			// 
			this.NumericSessonID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.NumericSessonID.Location = new System.Drawing.Point(136, 56);
			this.NumericSessonID.Maximum = new System.Decimal(new int[] {
																			999,
																			0,
																			0,
																			0});
			this.NumericSessonID.Name = "NumericSessonID";
			this.NumericSessonID.Size = new System.Drawing.Size(160, 20);
			this.NumericSessonID.TabIndex = 11;
			this.toolTip.SetToolTip(this.NumericSessonID, "The session identifier will be displayed in the validation results.");
			this.NumericSessonID.Validated += new System.EventHandler(this.NumericSessonID_Validated);
			// 
			// ComboBoxSessionType
			// 
			this.ComboBoxSessionType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBoxSessionType.Items.AddRange(new object[] {
																	 "Script",
																	 "Media",
																	 "Emulator"});
			this.ComboBoxSessionType.Location = new System.Drawing.Point(136, 8);
			this.ComboBoxSessionType.Name = "ComboBoxSessionType";
			this.ComboBoxSessionType.Size = new System.Drawing.Size(160, 21);
			this.ComboBoxSessionType.TabIndex = 5;
			this.toolTip.SetToolTip(this.ComboBoxSessionType, "Select the type of session. Script sessions are specific for running scripts, emu" +
				"lator sessions you can use for quick system emulation and media sessions are nee" +
				"ded for validating media files.");
			this.ComboBoxSessionType.SelectedIndexChanged += new System.EventHandler(this.ComboBoxSessionType_SelectedIndexChanged);
			// 
			// LabelSelectSopClasses
			// 
			this.LabelSelectSopClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.LabelSelectSopClasses.Location = new System.Drawing.Point(16, 200);
			this.LabelSelectSopClasses.Name = "LabelSelectSopClasses";
			this.LabelSelectSopClasses.Size = new System.Drawing.Size(280, 16);
			this.LabelSelectSopClasses.TabIndex = 10;
			this.LabelSelectSopClasses.Text = "(Un)select the SOP classes relevant for this session:";
			// 
			// ButtonBrowseResultsDir
			// 
			this.ButtonBrowseResultsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonBrowseResultsDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonBrowseResultsDir.Location = new System.Drawing.Point(312, 128);
			this.ButtonBrowseResultsDir.Name = "ButtonBrowseResultsDir";
			this.ButtonBrowseResultsDir.Size = new System.Drawing.Size(112, 24);
			this.ButtonBrowseResultsDir.TabIndex = 9;
			this.ButtonBrowseResultsDir.Text = "Browse";
			this.toolTip.SetToolTip(this.ButtonBrowseResultsDir, "This is the location where the validation results files will be stored. Press the" +
				" Browse button to change to a different folder.");
			this.ButtonBrowseResultsDir.Click += new System.EventHandler(this.ButtonBrowseResultsDir_Click);
			// 
			// LabelResultsDir
			// 
			this.LabelResultsDir.Location = new System.Drawing.Point(16, 128);
			this.LabelResultsDir.Name = "LabelResultsDir";
			this.LabelResultsDir.TabIndex = 8;
			this.LabelResultsDir.Text = "Results dir:";
			// 
			// LabelSessionType
			// 
			this.LabelSessionType.Location = new System.Drawing.Point(16, 8);
			this.LabelSessionType.Name = "LabelSessionType";
			this.LabelSessionType.TabIndex = 1;
			this.LabelSessionType.Text = "Session type:";
			// 
			// LabelDate
			// 
			this.LabelDate.Location = new System.Drawing.Point(16, 104);
			this.LabelDate.Name = "LabelDate";
			this.LabelDate.TabIndex = 1;
			this.LabelDate.Text = "Date:";
			// 
			// LabelSessionTitle
			// 
			this.LabelSessionTitle.Location = new System.Drawing.Point(16, 32);
			this.LabelSessionTitle.Name = "LabelSessionTitle";
			this.LabelSessionTitle.TabIndex = 1;
			this.LabelSessionTitle.Text = "Session title:";
			// 
			// LabelTestedBy
			// 
			this.LabelTestedBy.Location = new System.Drawing.Point(16, 80);
			this.LabelTestedBy.Name = "LabelTestedBy";
			this.LabelTestedBy.TabIndex = 1;
			this.LabelTestedBy.Text = "Tested by:";
			// 
			// TextBoxTestedBy
			// 
			this.TextBoxTestedBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxTestedBy.Location = new System.Drawing.Point(136, 80);
			this.TextBoxTestedBy.MaxLength = 0;
			this.TextBoxTestedBy.Name = "TextBoxTestedBy";
			this.TextBoxTestedBy.Size = new System.Drawing.Size(160, 20);
			this.TextBoxTestedBy.TabIndex = 6;
			this.TextBoxTestedBy.Text = "";
			this.toolTip.SetToolTip(this.TextBoxTestedBy, "The name of the tester will be displayed in the validation results.");
			this.TextBoxTestedBy.TextChanged += new System.EventHandler(this.TextBoxTestedBy_TextChanged);
			// 
			// TextBoxResultsRoot
			// 
			this.TextBoxResultsRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxResultsRoot.Location = new System.Drawing.Point(136, 128);
			this.TextBoxResultsRoot.MaxLength = 0;
			this.TextBoxResultsRoot.Name = "TextBoxResultsRoot";
			this.TextBoxResultsRoot.ReadOnly = true;
			this.TextBoxResultsRoot.Size = new System.Drawing.Size(160, 20);
			this.TextBoxResultsRoot.TabIndex = 6;
			this.TextBoxResultsRoot.Text = "";
			this.toolTip.SetToolTip(this.TextBoxResultsRoot, "This is the location where the validation results files will be stored. Press the" +
				" Browse button to change to a different folder.");
			// 
			// TextBoxScriptRoot
			// 
			this.TextBoxScriptRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxScriptRoot.Location = new System.Drawing.Point(136, 152);
			this.TextBoxScriptRoot.MaxLength = 0;
			this.TextBoxScriptRoot.Name = "TextBoxScriptRoot";
			this.TextBoxScriptRoot.ReadOnly = true;
			this.TextBoxScriptRoot.Size = new System.Drawing.Size(160, 20);
			this.TextBoxScriptRoot.TabIndex = 6;
			this.TextBoxScriptRoot.Text = "";
			this.toolTip.SetToolTip(this.TextBoxScriptRoot, "This is the location where the scripts can be found. The scripts will be displaye" +
				"d in the session tree browser on the left.");
			// 
			// LabelScriptsDir
			// 
			this.LabelScriptsDir.Location = new System.Drawing.Point(16, 152);
			this.LabelScriptsDir.Name = "LabelScriptsDir";
			this.LabelScriptsDir.TabIndex = 8;
			this.LabelScriptsDir.Text = "Scripts dir:";
			// 
			// ButtonBrowseScriptsDir
			// 
			this.ButtonBrowseScriptsDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonBrowseScriptsDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonBrowseScriptsDir.Location = new System.Drawing.Point(312, 152);
			this.ButtonBrowseScriptsDir.Name = "ButtonBrowseScriptsDir";
			this.ButtonBrowseScriptsDir.Size = new System.Drawing.Size(112, 24);
			this.ButtonBrowseScriptsDir.TabIndex = 9;
			this.ButtonBrowseScriptsDir.Text = "Browse";
			this.toolTip.SetToolTip(this.ButtonBrowseScriptsDir, "This is the location where the scripts can be found. The scripts will be displaye" +
				"d in the session tree browser on the left.");
			this.ButtonBrowseScriptsDir.Click += new System.EventHandler(this.ButtonBrowseScriptsDir_Click);
			// 
			// ButtonSpecifySOPCLasses
			// 
			this.ButtonSpecifySOPCLasses.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSpecifySOPCLasses.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonSpecifySOPCLasses.Location = new System.Drawing.Point(312, 200);
			this.ButtonSpecifySOPCLasses.Name = "ButtonSpecifySOPCLasses";
			this.ButtonSpecifySOPCLasses.Size = new System.Drawing.Size(112, 24);
			this.ButtonSpecifySOPCLasses.TabIndex = 9;
			this.ButtonSpecifySOPCLasses.Text = "Specify SOP classes";
			this.toolTip.SetToolTip(this.ButtonSpecifySOPCLasses, "Selection of SOP Classes to be used during validation.");
			this.ButtonSpecifySOPCLasses.Click += new System.EventHandler(this.ButtonSpecifySOPCLasses_Click);
			// 
			// TextBoxSessionTitle
			// 
			this.TextBoxSessionTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSessionTitle.Location = new System.Drawing.Point(136, 32);
			this.TextBoxSessionTitle.MaxLength = 0;
			this.TextBoxSessionTitle.Name = "TextBoxSessionTitle";
			this.TextBoxSessionTitle.Size = new System.Drawing.Size(160, 20);
			this.TextBoxSessionTitle.TabIndex = 6;
			this.TextBoxSessionTitle.Text = "";
			this.toolTip.SetToolTip(this.TextBoxSessionTitle, "The session title will be displayed in the validation results.");
			this.TextBoxSessionTitle.TextChanged += new System.EventHandler(this.TextBoxSessionTitle_TextChanged);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(16, 56);
			this.label7.Name = "label7";
			this.label7.TabIndex = 1;
			this.label7.Text = "Session ID:";
			// 
			// DateTested
			// 
			this.DateTested.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.DateTested.Location = new System.Drawing.Point(136, 104);
			this.DateTested.Name = "DateTested";
			this.DateTested.Size = new System.Drawing.Size(160, 20);
			this.DateTested.TabIndex = 12;
			this.toolTip.SetToolTip(this.DateTested, "The session date will be displayed in the validation results.");
			this.DateTested.ValueChanged += new System.EventHandler(this.DateTested_ValueChanged);
			// 
			// ButtonSpecifyTransferSyntaxes
			// 
			this.ButtonSpecifyTransferSyntaxes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonSpecifyTransferSyntaxes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonSpecifyTransferSyntaxes.Location = new System.Drawing.Point(312, 224);
			this.ButtonSpecifyTransferSyntaxes.Name = "ButtonSpecifyTransferSyntaxes";
			this.ButtonSpecifyTransferSyntaxes.Size = new System.Drawing.Size(112, 24);
			this.ButtonSpecifyTransferSyntaxes.TabIndex = 9;
			this.ButtonSpecifyTransferSyntaxes.Text = "Specify TS";
			this.toolTip.SetToolTip(this.ButtonSpecifyTransferSyntaxes, "Selection of Transfer Syntaxes to be supported during communication.");
			this.ButtonSpecifyTransferSyntaxes.Click += new System.EventHandler(this.ButtonSpecifyTransferSyntaxes_Click);
			// 
			// LabelSpecifyTransferSyntaxes
			// 
			this.LabelSpecifyTransferSyntaxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.LabelSpecifyTransferSyntaxes.Location = new System.Drawing.Point(16, 224);
			this.LabelSpecifyTransferSyntaxes.Name = "LabelSpecifyTransferSyntaxes";
			this.LabelSpecifyTransferSyntaxes.Size = new System.Drawing.Size(280, 16);
			this.LabelSpecifyTransferSyntaxes.TabIndex = 10;
			this.LabelSpecifyTransferSyntaxes.Text = "(Un)select the supported Transfer Syntaxes:";
			// 
			// ButtonBrowseDescriptionDir
			// 
			this.ButtonBrowseDescriptionDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonBrowseDescriptionDir.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonBrowseDescriptionDir.Location = new System.Drawing.Point(312, 176);
			this.ButtonBrowseDescriptionDir.Name = "ButtonBrowseDescriptionDir";
			this.ButtonBrowseDescriptionDir.Size = new System.Drawing.Size(112, 24);
			this.ButtonBrowseDescriptionDir.TabIndex = 9;
			this.ButtonBrowseDescriptionDir.Text = "Browse";
			this.toolTip.SetToolTip(this.ButtonBrowseDescriptionDir, "This is the location where the descriptions related to the scripts can be found. " +
				"When a script is selected and a description is available with the same name, thi" +
				"s description will be shown instead of the script.");
			this.ButtonBrowseDescriptionDir.Click += new System.EventHandler(this.ButtonBrowseDescriptionDir_Click);
			// 
			// LabelDescriptionDir
			// 
			this.LabelDescriptionDir.Location = new System.Drawing.Point(16, 176);
			this.LabelDescriptionDir.Name = "LabelDescriptionDir";
			this.LabelDescriptionDir.TabIndex = 8;
			this.LabelDescriptionDir.Text = "Description dir:";
			// 
			// TextBoxDescriptionRoot
			// 
			this.TextBoxDescriptionRoot.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxDescriptionRoot.Location = new System.Drawing.Point(136, 176);
			this.TextBoxDescriptionRoot.MaxLength = 0;
			this.TextBoxDescriptionRoot.Name = "TextBoxDescriptionRoot";
			this.TextBoxDescriptionRoot.ReadOnly = true;
			this.TextBoxDescriptionRoot.Size = new System.Drawing.Size(160, 20);
			this.TextBoxDescriptionRoot.TabIndex = 6;
			this.TextBoxDescriptionRoot.Text = "";
			this.toolTip.SetToolTip(this.TextBoxDescriptionRoot, "This is the location where the descriptions related to the scripts can be found. " +
				"When a script is selected and a description is available with the same name, thi" +
				"s description will be shown instead of the script.");
			// 
			// PanelGeneralPropertiesTitle
			// 
			this.PanelGeneralPropertiesTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.PanelGeneralPropertiesTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.PanelGeneralPropertiesTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelGeneralPropertiesTitle.Controls.Add(this.MinGSPSettings);
			this.PanelGeneralPropertiesTitle.Controls.Add(this.label5);
			this.PanelGeneralPropertiesTitle.Controls.Add(this.MaxGSPSettings);
			this.PanelGeneralPropertiesTitle.Location = new System.Drawing.Point(0, 0);
			this.PanelGeneralPropertiesTitle.Name = "PanelGeneralPropertiesTitle";
			this.PanelGeneralPropertiesTitle.Size = new System.Drawing.Size(440, 32);
			this.PanelGeneralPropertiesTitle.TabIndex = 4;
			// 
			// MinGSPSettings
			// 
			this.MinGSPSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MinGSPSettings.Image = ((System.Drawing.Image)(resources.GetObject("MinGSPSettings.Image")));
			this.MinGSPSettings.Location = new System.Drawing.Point(392, 4);
			this.MinGSPSettings.Name = "MinGSPSettings";
			this.MinGSPSettings.Size = new System.Drawing.Size(32, 24);
			this.MinGSPSettings.TabIndex = 1;
			this.MinGSPSettings.TabStop = false;
			this.MinGSPSettings.Click += new System.EventHandler(this.MinGSPSettings_Click);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(24, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(184, 23);
			this.label5.TabIndex = 0;
			this.label5.Text = "General Session Properties";
			// 
			// MaxGSPSettings
			// 
			this.MaxGSPSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MaxGSPSettings.Image = ((System.Drawing.Image)(resources.GetObject("MaxGSPSettings.Image")));
			this.MaxGSPSettings.Location = new System.Drawing.Point(352, 4);
			this.MaxGSPSettings.Name = "MaxGSPSettings";
			this.MaxGSPSettings.Size = new System.Drawing.Size(32, 24);
			this.MaxGSPSettings.TabIndex = 1;
			this.MaxGSPSettings.TabStop = false;
			this.MaxGSPSettings.Click += new System.EventHandler(this.MaxGSPSettings_Click);
			// 
			// PanelSUTSettingContent
			// 
			this.PanelSUTSettingContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
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
			this.PanelSUTSettingContent.Location = new System.Drawing.Point(0, 536);
			this.PanelSUTSettingContent.Name = "PanelSUTSettingContent";
			this.PanelSUTSettingContent.Size = new System.Drawing.Size(440, 160);
			this.PanelSUTSettingContent.TabIndex = 4;
			// 
			// LabelSUTMaxPDU
			// 
			this.LabelSUTMaxPDU.Location = new System.Drawing.Point(16, 128);
			this.LabelSUTMaxPDU.Name = "LabelSUTMaxPDU";
			this.LabelSUTMaxPDU.Size = new System.Drawing.Size(100, 32);
			this.LabelSUTMaxPDU.TabIndex = 5;
			this.LabelSUTMaxPDU.Text = "Maximum PDU length to receive:";
			// 
			// LabelSUTAETitle
			// 
			this.LabelSUTAETitle.Location = new System.Drawing.Point(16, 8);
			this.LabelSUTAETitle.Name = "LabelSUTAETitle";
			this.LabelSUTAETitle.TabIndex = 5;
			this.LabelSUTAETitle.Text = "AE Title:";
			// 
			// LabelSUTTCPIPAddress
			// 
			this.LabelSUTTCPIPAddress.Location = new System.Drawing.Point(16, 104);
			this.LabelSUTTCPIPAddress.Name = "LabelSUTTCPIPAddress";
			this.LabelSUTTCPIPAddress.Size = new System.Drawing.Size(100, 32);
			this.LabelSUTTCPIPAddress.TabIndex = 5;
			this.LabelSUTTCPIPAddress.Text = "Remote TCP/IP address:";
			// 
			// LabelSUTListenPort
			// 
			this.LabelSUTListenPort.Location = new System.Drawing.Point(16, 80);
			this.LabelSUTListenPort.Name = "LabelSUTListenPort";
			this.LabelSUTListenPort.TabIndex = 5;
			this.LabelSUTListenPort.Text = "Listen port:";
			// 
			// TextBoxSUTAETitle
			// 
			this.TextBoxSUTAETitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSUTAETitle.Location = new System.Drawing.Point(136, 8);
			this.TextBoxSUTAETitle.MaxLength = 16;
			this.TextBoxSUTAETitle.Name = "TextBoxSUTAETitle";
			this.TextBoxSUTAETitle.Size = new System.Drawing.Size(160, 20);
			this.TextBoxSUTAETitle.TabIndex = 6;
			this.TextBoxSUTAETitle.Text = "";
			this.toolTip.SetToolTip(this.TextBoxSUTAETitle, "Application Entity Title for the System Under Test.");
			this.TextBoxSUTAETitle.TextChanged += new System.EventHandler(this.TextBoxSUTAETitle_TextChanged);
			// 
			// TextBoxSUTTCPIPAddress
			// 
			this.TextBoxSUTTCPIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSUTTCPIPAddress.Location = new System.Drawing.Point(136, 104);
			this.TextBoxSUTTCPIPAddress.MaxLength = 0;
			this.TextBoxSUTTCPIPAddress.Name = "TextBoxSUTTCPIPAddress";
			this.TextBoxSUTTCPIPAddress.Size = new System.Drawing.Size(160, 20);
			this.TextBoxSUTTCPIPAddress.TabIndex = 6;
			this.TextBoxSUTTCPIPAddress.Text = "";
			this.toolTip.SetToolTip(this.TextBoxSUTTCPIPAddress, "TCP/IP address for the System Under Test.");
			this.TextBoxSUTTCPIPAddress.TextChanged += new System.EventHandler(this.TextBoxSUTTCPIPAddress_TextChanged);
			// 
			// ButtonCheckTCPIPAddress
			// 
			this.ButtonCheckTCPIPAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCheckTCPIPAddress.Enabled = false;
			this.ButtonCheckTCPIPAddress.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonCheckTCPIPAddress.Location = new System.Drawing.Point(312, 104);
			this.ButtonCheckTCPIPAddress.Name = "ButtonCheckTCPIPAddress";
			this.ButtonCheckTCPIPAddress.Size = new System.Drawing.Size(112, 24);
			this.ButtonCheckTCPIPAddress.TabIndex = 7;
			this.ButtonCheckTCPIPAddress.Text = "Check addresss";
			this.toolTip.SetToolTip(this.ButtonCheckTCPIPAddress, "Check TCP/IP address.");
			// 
			// NumericSUTListenPort
			// 
			this.NumericSUTListenPort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.NumericSUTListenPort.Location = new System.Drawing.Point(136, 80);
			this.NumericSUTListenPort.Maximum = new System.Decimal(new int[] {
																				 65535,
																				 0,
																				 0,
																				 0});
			this.NumericSUTListenPort.Name = "NumericSUTListenPort";
			this.NumericSUTListenPort.Size = new System.Drawing.Size(160, 20);
			this.NumericSUTListenPort.TabIndex = 7;
			this.toolTip.SetToolTip(this.NumericSUTListenPort, "Listen port for the System Under Test.");
			this.NumericSUTListenPort.Validated += new System.EventHandler(this.NumericSUTListenPort_Validated);
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
			this.NumericSUTMaxPDU.Location = new System.Drawing.Point(136, 128);
			this.NumericSUTMaxPDU.Maximum = new System.Decimal(new int[] {
																			 1048576,
																			 0,
																			 0,
																			 0});
			this.NumericSUTMaxPDU.Name = "NumericSUTMaxPDU";
			this.NumericSUTMaxPDU.Size = new System.Drawing.Size(160, 20);
			this.NumericSUTMaxPDU.TabIndex = 7;
			this.toolTip.SetToolTip(this.NumericSUTMaxPDU, "Maximum receivable PDU length for the System Under Test.");
			this.NumericSUTMaxPDU.Validated += new System.EventHandler(this.NumericSUTMaxPDU_Validated);
			// 
			// LabelSUTImplClassUID
			// 
			this.LabelSUTImplClassUID.Location = new System.Drawing.Point(16, 32);
			this.LabelSUTImplClassUID.Name = "LabelSUTImplClassUID";
			this.LabelSUTImplClassUID.TabIndex = 5;
			this.LabelSUTImplClassUID.Text = "Impl. Class UID:";
			// 
			// LabelSUTImplVersionName
			// 
			this.LabelSUTImplVersionName.Location = new System.Drawing.Point(16, 56);
			this.LabelSUTImplVersionName.Name = "LabelSUTImplVersionName";
			this.LabelSUTImplVersionName.Size = new System.Drawing.Size(112, 23);
			this.LabelSUTImplVersionName.TabIndex = 5;
			this.LabelSUTImplVersionName.Text = "Impl. Version Name:";
			// 
			// TextBoxSUTImplClassUID
			// 
			this.TextBoxSUTImplClassUID.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSUTImplClassUID.Location = new System.Drawing.Point(136, 32);
			this.TextBoxSUTImplClassUID.MaxLength = 64;
			this.TextBoxSUTImplClassUID.Name = "TextBoxSUTImplClassUID";
			this.TextBoxSUTImplClassUID.Size = new System.Drawing.Size(160, 20);
			this.TextBoxSUTImplClassUID.TabIndex = 6;
			this.TextBoxSUTImplClassUID.Text = "";
			this.toolTip.SetToolTip(this.TextBoxSUTImplClassUID, "Implementation Class UID for the System Under Test.");
			this.TextBoxSUTImplClassUID.TextChanged += new System.EventHandler(this.TextBoxSUTImplClassUID_TextChanged);
			// 
			// TextBoxSUTImplVersionName
			// 
			this.TextBoxSUTImplVersionName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.TextBoxSUTImplVersionName.Location = new System.Drawing.Point(136, 56);
			this.TextBoxSUTImplVersionName.MaxLength = 16;
			this.TextBoxSUTImplVersionName.Name = "TextBoxSUTImplVersionName";
			this.TextBoxSUTImplVersionName.Size = new System.Drawing.Size(160, 20);
			this.TextBoxSUTImplVersionName.TabIndex = 6;
			this.TextBoxSUTImplVersionName.Text = "";
			this.toolTip.SetToolTip(this.TextBoxSUTImplVersionName, "Implementation Version Name for the System Under Test.");
			this.TextBoxSUTImplVersionName.TextChanged += new System.EventHandler(this.TextBoxSUTImplVersionName_TextChanged);
			// 
			// PanelSUTSettingTitle
			// 
			this.PanelSUTSettingTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.PanelSUTSettingTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.PanelSUTSettingTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelSUTSettingTitle.Controls.Add(this.label2);
			this.PanelSUTSettingTitle.Controls.Add(this.MinSUTSettings);
			this.PanelSUTSettingTitle.Controls.Add(this.MaxSUTSettings);
			this.PanelSUTSettingTitle.Location = new System.Drawing.Point(0, 504);
			this.PanelSUTSettingTitle.Name = "PanelSUTSettingTitle";
			this.PanelSUTSettingTitle.Size = new System.Drawing.Size(440, 32);
			this.PanelSUTSettingTitle.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(24, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(184, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "System Under Test Settings";
			// 
			// MinSUTSettings
			// 
			this.MinSUTSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MinSUTSettings.Image = ((System.Drawing.Image)(resources.GetObject("MinSUTSettings.Image")));
			this.MinSUTSettings.Location = new System.Drawing.Point(392, 4);
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
			this.MaxSUTSettings.Location = new System.Drawing.Point(352, 4);
			this.MaxSUTSettings.Name = "MaxSUTSettings";
			this.MaxSUTSettings.Size = new System.Drawing.Size(32, 24);
			this.MaxSUTSettings.TabIndex = 1;
			this.MaxSUTSettings.TabStop = false;
			this.MaxSUTSettings.Click += new System.EventHandler(this.MaxSUTSettings_Click);
			// 
			// DialogOpenSessionFile
			// 
			this.DialogOpenSessionFile.Filter = "Session files (*.ses) |*.ses";
			this.DialogOpenSessionFile.Multiselect = true;
			this.DialogOpenSessionFile.Title = "Open Session File(s)";
			// 
			// PanelSessionPropertiesView
			// 
			this.PanelSessionPropertiesView.Controls.Add(this.ButtonGeneralInformation);
			this.PanelSessionPropertiesView.Controls.Add(this.RichTextBoxActivityLogging);
			this.PanelSessionPropertiesView.Controls.Add(this.PanelSessionProperties);
			this.PanelSessionPropertiesView.Controls.Add(this.VScrollBarSessionInfo);
			this.PanelSessionPropertiesView.Controls.Add(this.RichTextBoxScript);
			this.PanelSessionPropertiesView.Controls.Add(this.ButtonActivityLogging);
			this.PanelSessionPropertiesView.Controls.Add(this.ButtonDetailedValidation);
			this.PanelSessionPropertiesView.Controls.Add(this.RichTextBoxInfo);
			this.PanelSessionPropertiesView.Controls.Add(this.PanelSOPClasses);
			this.PanelSessionPropertiesView.Controls.Add(this.WebDescriptionView);
			this.PanelSessionPropertiesView.Controls.Add(this.ButtonReturnToSessionProperties);
			this.PanelSessionPropertiesView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PanelSessionPropertiesView.Location = new System.Drawing.Point(0, 0);
			this.PanelSessionPropertiesView.Name = "PanelSessionPropertiesView";
			this.PanelSessionPropertiesView.Size = new System.Drawing.Size(1028, 746);
			this.PanelSessionPropertiesView.TabIndex = 4;
			// 
			// ButtonGeneralInformation
			// 
			this.ButtonGeneralInformation.Appearance = System.Windows.Forms.Appearance.Button;
			this.ButtonGeneralInformation.Checked = true;
			this.ButtonGeneralInformation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonGeneralInformation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ButtonGeneralInformation.ForeColor = System.Drawing.SystemColors.ControlText;
			this.ButtonGeneralInformation.Location = new System.Drawing.Point(496, 8);
			this.ButtonGeneralInformation.Name = "ButtonGeneralInformation";
			this.ButtonGeneralInformation.Size = new System.Drawing.Size(120, 24);
			this.ButtonGeneralInformation.TabIndex = 8;
			this.ButtonGeneralInformation.TabStop = true;
			this.ButtonGeneralInformation.Text = "General Information!";
			this.ButtonGeneralInformation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip.SetToolTip(this.ButtonGeneralInformation, "General information.");
			this.ButtonGeneralInformation.Click += new System.EventHandler(this.ButtonGeneralInformation_Click);
			this.ButtonGeneralInformation.CheckedChanged += new System.EventHandler(this.ButtonGeneralInformation_CheckedChanged);
			// 
			// RichTextBoxActivityLogging
			// 
			this.RichTextBoxActivityLogging.Location = new System.Drawing.Point(944, 272);
			this.RichTextBoxActivityLogging.Name = "RichTextBoxActivityLogging";
			this.RichTextBoxActivityLogging.Size = new System.Drawing.Size(32, 40);
			this.RichTextBoxActivityLogging.TabIndex = 6;
			this.RichTextBoxActivityLogging.Text = "";
			this.RichTextBoxActivityLogging.Visible = false;
			// 
			// PanelSessionProperties
			// 
			this.PanelSessionProperties.Controls.Add(this.PanelGeneralPropertiesTitle);
			this.PanelSessionProperties.Controls.Add(this.PanelGeneralPropertiesContent);
			this.PanelSessionProperties.Controls.Add(this.PanelSUTSettingTitle);
			this.PanelSessionProperties.Controls.Add(this.PanelDVTRoleSettingsTitle);
			this.PanelSessionProperties.Controls.Add(this.PanelSecuritySettingsTitle);
			this.PanelSessionProperties.Controls.Add(this.PanelSecuritySettingsContent);
			this.PanelSessionProperties.Controls.Add(this.PanelDVTRoleSettingsContent);
			this.PanelSessionProperties.Controls.Add(this.PanelSUTSettingContent);
			this.PanelSessionProperties.Location = new System.Drawing.Point(496, 40);
			this.PanelSessionProperties.Name = "PanelSessionProperties";
			this.PanelSessionProperties.Size = new System.Drawing.Size(440, 912);
			this.PanelSessionProperties.TabIndex = 5;
			// 
			// PanelSecuritySettingsTitle
			// 
			this.PanelSecuritySettingsTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.PanelSecuritySettingsTitle.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.PanelSecuritySettingsTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelSecuritySettingsTitle.Controls.Add(this.MinSecuritySettings);
			this.PanelSecuritySettingsTitle.Controls.Add(this.MaxSecuritySettings);
			this.PanelSecuritySettingsTitle.Controls.Add(this.CheckBoxSecureConnection);
			this.PanelSecuritySettingsTitle.Location = new System.Drawing.Point(0, 704);
			this.PanelSecuritySettingsTitle.Name = "PanelSecuritySettingsTitle";
			this.PanelSecuritySettingsTitle.Size = new System.Drawing.Size(440, 32);
			this.PanelSecuritySettingsTitle.TabIndex = 4;
			// 
			// MinSecuritySettings
			// 
			this.MinSecuritySettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.MinSecuritySettings.Image = ((System.Drawing.Image)(resources.GetObject("MinSecuritySettings.Image")));
			this.MinSecuritySettings.Location = new System.Drawing.Point(392, 4);
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
			this.MaxSecuritySettings.Location = new System.Drawing.Point(352, 4);
			this.MaxSecuritySettings.Name = "MaxSecuritySettings";
			this.MaxSecuritySettings.Size = new System.Drawing.Size(32, 24);
			this.MaxSecuritySettings.TabIndex = 1;
			this.MaxSecuritySettings.TabStop = false;
			this.MaxSecuritySettings.Click += new System.EventHandler(this.MaxSecuritySettings_Click);
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
			// PanelSecuritySettingsContent
			// 
			this.PanelSecuritySettingsContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.PanelSecuritySettingsContent.BackColor = System.Drawing.SystemColors.ControlLight;
			this.PanelSecuritySettingsContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityKeyExchange);
			this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityGeneral);
			this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityEncryption);
			this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityDataIntegrity);
			this.PanelSecuritySettingsContent.Controls.Add(this.GroupSecurityAuthentication);
			this.PanelSecuritySettingsContent.Controls.Add(this.ListBoxSecuritySettings);
			this.PanelSecuritySettingsContent.Controls.Add(this.LabelCategories);
			this.PanelSecuritySettingsContent.Controls.Add(this.LabelSelect1ItemMsg);
			this.PanelSecuritySettingsContent.Controls.Add(this.label28);
			this.PanelSecuritySettingsContent.Controls.Add(this.ButtonViewCertificates);
			this.PanelSecuritySettingsContent.Controls.Add(this.ButtonViewCredentials);
			this.PanelSecuritySettingsContent.Controls.Add(this.ButtonCreateCertificate);
			this.PanelSecuritySettingsContent.Location = new System.Drawing.Point(0, 736);
			this.PanelSecuritySettingsContent.Name = "PanelSecuritySettingsContent";
			this.PanelSecuritySettingsContent.Size = new System.Drawing.Size(440, 168);
			this.PanelSecuritySettingsContent.TabIndex = 4;
			// 
			// GroupSecurityKeyExchange
			// 
			this.GroupSecurityKeyExchange.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.GroupSecurityKeyExchange.Controls.Add(this.CheckBoxKeyExchangeRSA);
			this.GroupSecurityKeyExchange.Controls.Add(this.CheckBoxKeyExchangeDH);
			this.GroupSecurityKeyExchange.Controls.Add(this.GroupSecurityVersion);
			this.GroupSecurityKeyExchange.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GroupSecurityKeyExchange.Location = new System.Drawing.Point(136, 24);
			this.GroupSecurityKeyExchange.Name = "GroupSecurityKeyExchange";
			this.GroupSecurityKeyExchange.Size = new System.Drawing.Size(160, 128);
			this.GroupSecurityKeyExchange.TabIndex = 16;
			this.GroupSecurityKeyExchange.TabStop = false;
			this.GroupSecurityKeyExchange.Text = "Key Exchange";
			// 
			// CheckBoxKeyExchangeRSA
			// 
			this.CheckBoxKeyExchangeRSA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.CheckBoxKeyExchangeRSA.Checked = true;
			this.CheckBoxKeyExchangeRSA.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CheckBoxKeyExchangeRSA.Location = new System.Drawing.Point(16, 24);
			this.CheckBoxKeyExchangeRSA.Name = "CheckBoxKeyExchangeRSA";
			this.CheckBoxKeyExchangeRSA.Size = new System.Drawing.Size(136, 24);
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
			this.CheckBoxKeyExchangeDH.Size = new System.Drawing.Size(136, 24);
			this.CheckBoxKeyExchangeDH.TabIndex = 0;
			this.CheckBoxKeyExchangeDH.Text = "DH";
			this.CheckBoxKeyExchangeDH.CheckedChanged += new System.EventHandler(this.CheckBoxKeyExchangeDH_CheckedChanged);
			// 
			// GroupSecurityVersion
			// 
			this.GroupSecurityVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.GroupSecurityVersion.Controls.Add(this.CheckBoxTLS);
			this.GroupSecurityVersion.Controls.Add(this.CheckBoxSSL);
			this.GroupSecurityVersion.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GroupSecurityVersion.Location = new System.Drawing.Point(0, 0);
			this.GroupSecurityVersion.Name = "GroupSecurityVersion";
			this.GroupSecurityVersion.Size = new System.Drawing.Size(160, 128);
			this.GroupSecurityVersion.TabIndex = 12;
			this.GroupSecurityVersion.TabStop = false;
			this.GroupSecurityVersion.Text = "Version";
			// 
			// CheckBoxTLS
			// 
			this.CheckBoxTLS.Checked = true;
			this.CheckBoxTLS.CheckState = System.Windows.Forms.CheckState.Checked;
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
			// GroupSecurityGeneral
			// 
			this.GroupSecurityGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.GroupSecurityGeneral.Controls.Add(this.CheckBoxCheckRemoteCertificates);
			this.GroupSecurityGeneral.Controls.Add(this.CheckBoxCacheSecureSessions);
			this.GroupSecurityGeneral.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.GroupSecurityGeneral.Location = new System.Drawing.Point(136, 24);
			this.GroupSecurityGeneral.Name = "GroupSecurityGeneral";
			this.GroupSecurityGeneral.Size = new System.Drawing.Size(160, 128);
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
			this.GroupSecurityEncryption.Size = new System.Drawing.Size(152, 128);
			this.GroupSecurityEncryption.TabIndex = 14;
			this.GroupSecurityEncryption.TabStop = false;
			this.GroupSecurityEncryption.Text = "Encryption";
			// 
			// CheckBoxEncryptionNone
			// 
			this.CheckBoxEncryptionNone.Checked = true;
			this.CheckBoxEncryptionNone.CheckState = System.Windows.Forms.CheckState.Checked;
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
			this.GroupSecurityDataIntegrity.Size = new System.Drawing.Size(160, 128);
			this.GroupSecurityDataIntegrity.TabIndex = 14;
			this.GroupSecurityDataIntegrity.TabStop = false;
			this.GroupSecurityDataIntegrity.Text = "Data Integrity";
			// 
			// CheckBoxDataIntegritySHA
			// 
			this.CheckBoxDataIntegritySHA.Checked = true;
			this.CheckBoxDataIntegritySHA.CheckState = System.Windows.Forms.CheckState.Checked;
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
			this.GroupSecurityAuthentication.Size = new System.Drawing.Size(160, 128);
			this.GroupSecurityAuthentication.TabIndex = 14;
			this.GroupSecurityAuthentication.TabStop = false;
			this.GroupSecurityAuthentication.Text = "Authentication";
			// 
			// CheckBoxAuthenticationRSA
			// 
			this.CheckBoxAuthenticationRSA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.CheckBoxAuthenticationRSA.Checked = true;
			this.CheckBoxAuthenticationRSA.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CheckBoxAuthenticationRSA.Location = new System.Drawing.Point(16, 24);
			this.CheckBoxAuthenticationRSA.Name = "CheckBoxAuthenticationRSA";
			this.CheckBoxAuthenticationRSA.Size = new System.Drawing.Size(136, 24);
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
			this.CheckBoxAuthenticationDSA.Size = new System.Drawing.Size(136, 24);
			this.CheckBoxAuthenticationDSA.TabIndex = 0;
			this.CheckBoxAuthenticationDSA.Text = "DSA";
			this.CheckBoxAuthenticationDSA.CheckedChanged += new System.EventHandler(this.CheckBoxAuthenticationDSA_CheckedChanged);
			// 
			// ListBoxSecuritySettings
			// 
			this.ListBoxSecuritySettings.Items.AddRange(new object[] {
																		 "General",
																		 "Version",
																		 "Authentication",
																		 "Key Exchange",
																		 "Data Integrity",
																		 "Encryption"});
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
			this.LabelSelect1ItemMsg.Size = new System.Drawing.Size(176, 16);
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
			// ButtonViewCertificates
			// 
			this.ButtonViewCertificates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonViewCertificates.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonViewCertificates.Location = new System.Drawing.Point(312, 32);
			this.ButtonViewCertificates.Name = "ButtonViewCertificates";
			this.ButtonViewCertificates.Size = new System.Drawing.Size(112, 24);
			this.ButtonViewCertificates.TabIndex = 7;
			this.ButtonViewCertificates.Text = "View Certificates";
			this.toolTip.SetToolTip(this.ButtonViewCertificates, "View certificates.");
			this.ButtonViewCertificates.Click += new System.EventHandler(this.ButtonViewCertificates_Click);
			// 
			// ButtonViewCredentials
			// 
			this.ButtonViewCredentials.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonViewCredentials.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonViewCredentials.Location = new System.Drawing.Point(312, 72);
			this.ButtonViewCredentials.Name = "ButtonViewCredentials";
			this.ButtonViewCredentials.Size = new System.Drawing.Size(112, 24);
			this.ButtonViewCredentials.TabIndex = 7;
			this.ButtonViewCredentials.Text = "View Credential";
			this.toolTip.SetToolTip(this.ButtonViewCredentials, "View credentials.");
			this.ButtonViewCredentials.Click += new System.EventHandler(this.ButtonViewCredentials_Click);
			// 
			// ButtonCreateCertificate
			// 
			this.ButtonCreateCertificate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonCreateCertificate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonCreateCertificate.Location = new System.Drawing.Point(312, 112);
			this.ButtonCreateCertificate.Name = "ButtonCreateCertificate";
			this.ButtonCreateCertificate.Size = new System.Drawing.Size(112, 24);
			this.ButtonCreateCertificate.TabIndex = 7;
			this.ButtonCreateCertificate.Text = "Create Certificate";
			this.toolTip.SetToolTip(this.ButtonCreateCertificate, "Create a certificate.");
			this.ButtonCreateCertificate.Click += new System.EventHandler(this.ButtonCreateCertificate_Click);
			// 
			// VScrollBarSessionInfo
			// 
			this.VScrollBarSessionInfo.Location = new System.Drawing.Point(944, 40);
			this.VScrollBarSessionInfo.Name = "VScrollBarSessionInfo";
			this.VScrollBarSessionInfo.SmallChange = 10;
			this.VScrollBarSessionInfo.TabIndex = 1;
			this.VScrollBarSessionInfo.Scroll += new System.Windows.Forms.ScrollEventHandler(this.VScrollBarSessionInfo_Scroll);
			// 
			// ButtonActivityLogging
			// 
			this.ButtonActivityLogging.Appearance = System.Windows.Forms.Appearance.Button;
			this.ButtonActivityLogging.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonActivityLogging.Location = new System.Drawing.Point(616, 8);
			this.ButtonActivityLogging.Name = "ButtonActivityLogging";
			this.ButtonActivityLogging.Size = new System.Drawing.Size(120, 24);
			this.ButtonActivityLogging.TabIndex = 8;
			this.ButtonActivityLogging.Text = "Activity Logging";
			this.ButtonActivityLogging.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip.SetToolTip(this.ButtonActivityLogging, "Activity progress of test run.");
			this.ButtonActivityLogging.CheckedChanged += new System.EventHandler(this.ButtonActivityLogging_CheckedChanged);
			// 
			// ButtonDetailedValidation
			// 
			this.ButtonDetailedValidation.Appearance = System.Windows.Forms.Appearance.Button;
			this.ButtonDetailedValidation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.ButtonDetailedValidation.Location = new System.Drawing.Point(736, 8);
			this.ButtonDetailedValidation.Name = "ButtonDetailedValidation";
			this.ButtonDetailedValidation.Size = new System.Drawing.Size(120, 24);
			this.ButtonDetailedValidation.TabIndex = 8;
			this.ButtonDetailedValidation.Text = "Detailed Validation";
			this.ButtonDetailedValidation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip.SetToolTip(this.ButtonDetailedValidation, "Detailed validation information of the test run.");
			this.ButtonDetailedValidation.Click += new System.EventHandler(this.ButtonDetailedValidation_Click);
			this.ButtonDetailedValidation.CheckedChanged += new System.EventHandler(this.ButtonDetailedValidation_CheckedChanged);
			// 
			// PanelSOPClasses
			// 
			this.PanelSOPClasses.Controls.Add(this.ComboBoxAETitleVersion);
			this.PanelSOPClasses.Controls.Add(this.LabelSelectAETitleVersion);
			this.PanelSOPClasses.Controls.Add(this.ButtonRemoveDefinitionRoot);
			this.PanelSOPClasses.Controls.Add(this.ListBoxDefinitionDirs);
			this.PanelSOPClasses.Controls.Add(this.LabelSpecifyDefinitionRoot);
			this.PanelSOPClasses.Controls.Add(this.ButtonAddDefinitionRoot);
			this.PanelSOPClasses.Controls.Add(this.SOPClasses);
			this.PanelSOPClasses.Location = new System.Drawing.Point(496, 528);
			this.PanelSOPClasses.Name = "PanelSOPClasses";
			this.PanelSOPClasses.Size = new System.Drawing.Size(480, 120);
			this.PanelSOPClasses.TabIndex = 10;
			this.PanelSOPClasses.Visible = false;
			// 
			// ComboBoxAETitleVersion
			// 
			this.ComboBoxAETitleVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ComboBoxAETitleVersion.Location = new System.Drawing.Point(232, 64);
			this.ComboBoxAETitleVersion.Name = "ComboBoxAETitleVersion";
			this.ComboBoxAETitleVersion.Size = new System.Drawing.Size(248, 21);
			this.ComboBoxAETitleVersion.TabIndex = 17;
			// 
			// LabelSelectAETitleVersion
			// 
			this.LabelSelectAETitleVersion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.LabelSelectAETitleVersion.Location = new System.Drawing.Point(0, 64);
			this.LabelSelectAETitleVersion.Name = "LabelSelectAETitleVersion";
			this.LabelSelectAETitleVersion.Size = new System.Drawing.Size(216, 23);
			this.LabelSelectAETitleVersion.TabIndex = 16;
			this.LabelSelectAETitleVersion.Text = "Select AE Title - Version to use:";
			// 
			// ButtonRemoveDefinitionRoot
			// 
			this.ButtonRemoveDefinitionRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonRemoveDefinitionRoot.Location = new System.Drawing.Point(376, 32);
			this.ButtonRemoveDefinitionRoot.Name = "ButtonRemoveDefinitionRoot";
			this.ButtonRemoveDefinitionRoot.Size = new System.Drawing.Size(104, 23);
			this.ButtonRemoveDefinitionRoot.TabIndex = 15;
			this.ButtonRemoveDefinitionRoot.Text = "Remove selected";
			this.toolTip.SetToolTip(this.ButtonRemoveDefinitionRoot, "Remove definition root.");
			this.ButtonRemoveDefinitionRoot.Click += new System.EventHandler(this.ButtonRemoveDefinitionRoot_Click);
			// 
			// ListBoxDefinitionDirs
			// 
			this.ListBoxDefinitionDirs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.ListBoxDefinitionDirs.Location = new System.Drawing.Point(144, 0);
			this.ListBoxDefinitionDirs.Name = "ListBoxDefinitionDirs";
			this.ListBoxDefinitionDirs.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ListBoxDefinitionDirs.Size = new System.Drawing.Size(224, 56);
			this.ListBoxDefinitionDirs.TabIndex = 14;
			// 
			// LabelSpecifyDefinitionRoot
			// 
			this.LabelSpecifyDefinitionRoot.Location = new System.Drawing.Point(0, 0);
			this.LabelSpecifyDefinitionRoot.Name = "LabelSpecifyDefinitionRoot";
			this.LabelSpecifyDefinitionRoot.Size = new System.Drawing.Size(136, 23);
			this.LabelSpecifyDefinitionRoot.TabIndex = 12;
			this.LabelSpecifyDefinitionRoot.Text = "Definition file directories:";
			// 
			// ButtonAddDefinitionRoot
			// 
			this.ButtonAddDefinitionRoot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ButtonAddDefinitionRoot.Location = new System.Drawing.Point(376, 0);
			this.ButtonAddDefinitionRoot.Name = "ButtonAddDefinitionRoot";
			this.ButtonAddDefinitionRoot.Size = new System.Drawing.Size(104, 23);
			this.ButtonAddDefinitionRoot.TabIndex = 10;
			this.ButtonAddDefinitionRoot.Text = "Add new";
			this.toolTip.SetToolTip(this.ButtonAddDefinitionRoot, "Add a definition root.");
			this.ButtonAddDefinitionRoot.Click += new System.EventHandler(this.ButtonAddDefinitionRoot_Click);
			// 
			// SOPClasses
			// 
			this.SOPClasses.AllowNavigation = false;
			this.SOPClasses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.SOPClasses.CaptionVisible = false;
			this.SOPClasses.DataMember = "";
			this.SOPClasses.HeaderForeColor = System.Drawing.SystemColors.ControlText;
			this.SOPClasses.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.SOPClasses.Location = new System.Drawing.Point(0, 96);
			this.SOPClasses.Name = "SOPClasses";
			this.SOPClasses.RowHeadersVisible = false;
			this.SOPClasses.Size = new System.Drawing.Size(480, 24);
			this.SOPClasses.TabIndex = 9;
			this.SOPClasses.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SOPClasses_MouseDown);
			this.SOPClasses.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SOPClasses_MouseUp);
			this.SOPClasses.MouseMove += new System.Windows.Forms.MouseEventHandler(this.SOPClasses_MouseMove);
			// 
			// WebDescriptionView
			// 
			this.WebDescriptionView.ContainingControl = this;
			this.WebDescriptionView.Enabled = true;
			this.WebDescriptionView.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.WebDescriptionView.Location = new System.Drawing.Point(944, 328);
			this.WebDescriptionView.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("WebDescriptionView.OcxState")));
			this.WebDescriptionView.Size = new System.Drawing.Size(32, 32);
			this.WebDescriptionView.TabIndex = 11;
			this.WebDescriptionView.Visible = false;
			this.WebDescriptionView.NavigateComplete2 += new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(this.WebDescriptionView_NavigateComplete2);
			this.WebDescriptionView.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(this.WebDescriptionView_DocumentComplete);
			this.WebDescriptionView.ProgressChange += new AxSHDocVw.DWebBrowserEvents2_ProgressChangeEventHandler(this.WebDescriptionView_ProgressChange);
			this.WebDescriptionView.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(this.WebDescriptionView_BeforeNavigate2);
			// 
			// ButtonReturnToSessionProperties
			// 
			this.ButtonReturnToSessionProperties.Location = new System.Drawing.Point(864, 8);
			this.ButtonReturnToSessionProperties.Name = "ButtonReturnToSessionProperties";
			this.ButtonReturnToSessionProperties.Size = new System.Drawing.Size(224, 23);
			this.ButtonReturnToSessionProperties.TabIndex = 13;
			this.ButtonReturnToSessionProperties.Text = "Return to session properties page";
			this.toolTip.SetToolTip(this.ButtonReturnToSessionProperties, "Session Properties.");
			this.ButtonReturnToSessionProperties.Click += new System.EventHandler(this.ButtonReturnToSessionProperties_Click);
			// 
			// dataSetSOPClasses
			// 
			this.dataSetSOPClasses.DataSetName = "NewDataSet";
			this.dataSetSOPClasses.Locale = new System.Globalization.CultureInfo("en-US");
			this.dataSetSOPClasses.Tables.AddRange(new System.Data.DataTable[] {
																				   this.dataTableSOPClasses});
			// 
			// dataTableSOPClasses
			// 
			this.dataTableSOPClasses.Columns.AddRange(new System.Data.DataColumn[] {
																					   this.dataColumnLoaded,
																					   this.dataColumnFilename,
																					   this.dataColumnSOPClassName,
																					   this.dataColumnSOPClassUID,
																					   this.dataColumnAETitleVersion,
																					   this.dataColumnDefinitionRoot});
			this.dataTableSOPClasses.TableName = "TableSOPClasses";
			// 
			// dataColumnLoaded
			// 
			this.dataColumnLoaded.ColumnName = "Loaded";
			this.dataColumnLoaded.DataType = typeof(bool);
			// 
			// dataColumnFilename
			// 
			this.dataColumnFilename.ColumnName = "Filename";
			// 
			// dataColumnSOPClassName
			// 
			this.dataColumnSOPClassName.ColumnName = "SOP class name";
			// 
			// dataColumnSOPClassUID
			// 
			this.dataColumnSOPClassUID.ColumnName = "SOP class UID";
			// 
			// dataColumnAETitleVersion
			// 
			this.dataColumnAETitleVersion.ColumnName = "AE title - version";
			// 
			// dataColumnDefinitionRoot
			// 
			this.dataColumnDefinitionRoot.ColumnName = "Definition root";
			// 
			// DialogAddCredentialFiles
			// 
			this.DialogAddCredentialFiles.Filter = "PEM Certificate files (*.pem;*.cer)|*.def;*.cer|DER Certificate files (*.cer)|*.c" +
				"er|PKCS#12 files (*.p12;*.pfx)|*.p12;*.pfx|PKCS#7 files (*.p7b;*.p7c)|*.p7b;*.p7" +
				"c";
			// 
			// DialogOpenMediaFile
			// 
			this.DialogOpenMediaFile.Filter = "DICOM media files (*.dcm)|*.dcm|All files (*.*)|*.*";
			this.DialogOpenMediaFile.FilterIndex = 2;
			this.DialogOpenMediaFile.Multiselect = true;
			this.DialogOpenMediaFile.ReadOnlyChecked = true;
			this.DialogOpenMediaFile.Title = "Select media file to validate";
			// 
			// ContextMenuDetailedValidation
			// 
			this.ContextMenuDetailedValidation.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																										  this.CM_Back,
																										  this.CM_Forward,
																										  this.menuItem3,
																										  this.CM_Copy,
																										  this.menuItem2,
																										  this.CM_Find,
																										  this.CM_FindAgain});
			// 
			// CM_Back
			// 
			this.CM_Back.Enabled = false;
			this.CM_Back.Index = 0;
			this.CM_Back.Text = "Back";
			this.CM_Back.Click += new System.EventHandler(this.CM_Back_Click);
			// 
			// CM_Forward
			// 
			this.CM_Forward.Enabled = false;
			this.CM_Forward.Index = 1;
			this.CM_Forward.Text = "Forward";
			this.CM_Forward.Click += new System.EventHandler(this.CM_Forward_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// CM_Copy
			// 
			this.CM_Copy.Index = 3;
			this.CM_Copy.Text = "Copy selection";
			this.CM_Copy.Click += new System.EventHandler(this.CM_Copy_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 4;
			this.menuItem2.Text = "-";
			// 
			// CM_Find
			// 
			this.CM_Find.Index = 5;
			this.CM_Find.Shortcut = System.Windows.Forms.Shortcut.CtrlF;
			this.CM_Find.Text = "Find";
			this.CM_Find.Click += new System.EventHandler(this.CM_Find_Click);
			// 
			// CM_FindAgain
			// 
			this.CM_FindAgain.Index = 6;
			this.CM_FindAgain.Shortcut = System.Windows.Forms.Shortcut.F3;
			this.CM_FindAgain.Text = "Find Again";
			this.CM_FindAgain.Click += new System.EventHandler(this.CM_FindAgain_Click);
			// 
			// ProjectForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1028, 746);
			this.Controls.Add(this.ViewSplitter);
			this.Controls.Add(this.SessionBrowser);
			this.Controls.Add(this.TaskPanel);
			this.Controls.Add(this.PanelSessionPropertiesView);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(640, 200);
			this.Name = "ProjectForm";
			this.Text = "ProjectForm";
			this.Resize += new System.EventHandler(this.ProjectForm_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.ProjectForm_Closing);
			this.Load += new System.EventHandler(this.ProjectForm_Load);
			this.TaskPanel.ResumeLayout(false);
			this.panel6.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.PanelDVTRoleSettingsContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NumericDVTListenPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericDVTTimeOut)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericDVTMaxPDU)).EndInit();
			this.PanelDVTRoleSettingsTitle.ResumeLayout(false);
			this.PanelGeneralPropertiesContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NumericSessonID)).EndInit();
			this.PanelGeneralPropertiesTitle.ResumeLayout(false);
			this.PanelSUTSettingContent.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NumericSUTListenPort)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NumericSUTMaxPDU)).EndInit();
			this.PanelSUTSettingTitle.ResumeLayout(false);
			this.PanelSessionPropertiesView.ResumeLayout(false);
			this.PanelSessionProperties.ResumeLayout(false);
			this.PanelSecuritySettingsTitle.ResumeLayout(false);
			this.PanelSecuritySettingsContent.ResumeLayout(false);
			this.GroupSecurityKeyExchange.ResumeLayout(false);
			this.GroupSecurityVersion.ResumeLayout(false);
			this.GroupSecurityGeneral.ResumeLayout(false);
			this.GroupSecurityEncryption.ResumeLayout(false);
			this.GroupSecurityDataIntegrity.ResumeLayout(false);
			this.GroupSecurityAuthentication.ResumeLayout(false);
			this.PanelSOPClasses.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SOPClasses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.WebDescriptionView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataSetSOPClasses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataTableSOPClasses)).EndInit();
			this.ResumeLayout(false);

		}
        #endregion

        private void ProjectForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MainForm parent = (MainForm)this.ParentForm;
            
			/* MK!!!!!
            if (!parent.DeleteProjectView (this))
            {
                // Deleting the current project form view has been cancelled.
                e.Cancel = true;
            }
			
			*/ // MK!!!
			
            this.TerminateRunningProcess ();

            if (this.selected_session != null)
                this.selected_session.ActivityReportEvent -= this._activity_handler;
        }

        #region ResizeProjectForm
        public void ResizeProjectForm ()
        {
            if (this.project == null)
                return;

            // We have to resize the controls in the information and summary
            // pages ourselves.
            this.PositionPageViewButtons ();
            switch (this._active_page)
            {
                case ActivePage.session:
                    this.ResizeSessionPropertiesView ();
                    break;
                case ActivePage.description:
                    this.WebDescriptionView.Visible = true;
                    this.RichTextBoxScript.Visible = false;
                    this.ResizeWebView ();
                    break;
                case ActivePage.script:
                    this.WebDescriptionView.Visible = false;
                    this.RichTextBoxScript.Visible = true;
                    this.ResizeScriptView ();
                    break;
                case ActivePage.activity_reporting:
                    this.ResizeActivityLoggingView ();
                    break;
                case ActivePage.validation_results:
                    break;
                case ActivePage.detailed_validation:
                    this.ResizeWebView ();
                    break;
                case ActivePage.sop_classes_view:
                    this.ResizeSOPClassesView ();
                    break;
                default:
                    break;
            }
        }

        private void ProjectForm_Resize(object sender, System.EventArgs e)
        {
            // Don't allow the session properties view to be smaller than the
            // minimum size.
            if (this.PanelSessionProperties.Size.Width < 432)
                this.ViewSplitter.SplitPosition -= (432 - this.PanelSessionProperties.Size.Width);

            this.ResizeProjectForm ();
        }

        private void PositionPageViewButtons ()
        {
            System.Drawing.Point    point;

            // Calculate the top-left location of the first button in the button bar.
            point = this.ButtonGeneralInformation.Location;
            point.X = this.ViewSplitter.SplitPosition + border_width;

            if (this._active_page == ActivePage.sop_classes_view)
            {
                this.ButtonReturnToSessionProperties.Location = point;
                this.ButtonReturnToSessionProperties.Visible = true;

                this.ButtonGeneralInformation.Visible = false;
                this.ButtonActivityLogging.Visible = false;
                this.ButtonDetailedValidation.Visible = false;
            }
            else
            {
                this.ButtonGeneralInformation.Location = point;

                point = this.ButtonActivityLogging.Location;
                point.X = this.ButtonGeneralInformation.Location.X +
                    this.ButtonGeneralInformation.Size.Width;
                this.ButtonActivityLogging.Location = point;

                point = this.ButtonDetailedValidation.Location;
                point.X = this.ButtonActivityLogging.Location.X +
                    this.ButtonActivityLogging.Size.Width;
                this.ButtonDetailedValidation.Location = point;

                this.ButtonGeneralInformation.Visible = true;
                this.ButtonActivityLogging.Visible = true;
                this.ButtonDetailedValidation.Visible = true;

                this.ButtonReturnToSessionProperties.Visible = false;

                if (this.project.GetNrSessions() == 0)
                {
                    this.ButtonGeneralInformation.Enabled = false;
                    this.ButtonActivityLogging.Enabled = false;
                    this.ButtonDetailedValidation.Enabled = false;
                }
                else
                {
                    this.ButtonGeneralInformation.Enabled = true;
                    this.ButtonActivityLogging.Enabled = true;
                    this.ButtonDetailedValidation.Enabled = true;
                }
            }
        }

        private void ResizeSessionPropertiesView ()
        {
            System.Drawing.Point    point;
            System.Drawing.Size     size;

            if (this.ShowRichTextBoxInfo)
            {
                // Position the information text box.
                // The height of the text box is not adjusted.
                point = this.RichTextBoxInfo.Location;
                point.X = this.ButtonGeneralInformation.Location.X;
                point.Y = this.PanelSessionPropertiesView.Size.Height -
                    this.RichTextBoxInfo.Size.Height -
                    border_width;    // The bottom spacing is the same as the one on the right of the textbox.
                this.RichTextBoxInfo.Location = point;
                size = this.RichTextBoxInfo.Size;
                size.Width = this.PanelSessionPropertiesView.Size.Width -
                    this.ButtonGeneralInformation.Location.X -
                    border_width;
                this.RichTextBoxInfo.Size = size;
            }

            // Position the vertical Scrollbar.
            // The width and y-coordinate are not adjusted.
            point = this.VScrollBarSessionInfo.Location;
            point.X = this.PanelSessionPropertiesView.Size.Width -
                this.VScrollBarSessionInfo.Size.Width -
                border_width;
            this.VScrollBarSessionInfo.Location = point;
            size = this.VScrollBarSessionInfo.Size;
            if (this.ShowRichTextBoxInfo)
                size.Height = this.RichTextBoxInfo.Location.Y -
                    point.Y -
                    border_width;
            else
                size.Height = this.PanelSessionPropertiesView.Size.Height -
                    point.Y -
                    border_width;
            this.VScrollBarSessionInfo.Size = size;

            // Position the PanelSessionProperties.
            point = this.PanelSessionProperties.Location;
            point.X = this.ButtonGeneralInformation.Location.X;
            this.PanelSessionProperties.Location = point;

            // Resize the PanelSessionProperties.
            size = this.PanelSessionProperties.Size;
            size.Width = this.VScrollBarSessionInfo.Location.X -
                this.PanelSessionProperties.Location.X -
                border_width;
            if (this.ShowRichTextBoxInfo)
                size.Height = this.RichTextBoxInfo.Location.Y -
                    this.PanelSessionProperties.Location.Y -
                    border_width;
            else
                size.Height = this.PanelSessionPropertiesView.Size.Height -
                    this.PanelSessionProperties.Location.Y -
                    border_width;
            this.PanelSessionProperties.Size = size;

            this.UpdateSessionPropertiesScrollbar ();

            this.ResizePropertiesGeneralProperties (this.PanelSessionProperties.Size.Width);
            this.ResizePropertiesDVTRoleSettings (this.PanelSessionProperties.Size.Width);
            this.ResizePropertiesSUTSettings (this.PanelSessionProperties.Size.Width);
            this.ResizePropertiesSecuritySettings (this.PanelSessionProperties.Size.Width);
        }

        private void ResizePropertiesGeneralProperties (int panel_width)
        {
            System.Drawing.Point    point;

            point = this.PanelGeneralPropertiesTitle.Location;
            point.Y = this.scrollbar_offset + this.border_width;
            this.PanelGeneralPropertiesTitle.Location = point;

            if (this.project.GetNrSessions() == 0)
            {
                this.PanelGeneralPropertiesContent.Visible = false;
                this.MinGSPSettings.Visible = false;
                this.MaxGSPSettings.Visible = false;
            }
            else
            {
                if (this.show_general_session_properties)
                {
                    this.PanelGeneralPropertiesContent.Visible = true;

                    this.MinGSPSettings.Visible = true;
                    this.MaxGSPSettings.Visible = false;
                    point = this.MinGSPSettings.Location;
                    point.X = panel_width - this.MinGSPSettings.Image.Width - this.border_width * 2;
                    this.MinGSPSettings.Location = point;

                    point = this.PanelGeneralPropertiesContent.Location;
                    point.Y = this.PanelGeneralPropertiesTitle.Location.Y +
                        this.PanelGeneralPropertiesTitle.Size.Height - 1;
                    this.PanelGeneralPropertiesContent.Location = point;
                }
                else
                {
                    this.PanelGeneralPropertiesContent.Visible = false;

                    this.MinGSPSettings.Visible = false;
                    this.MaxGSPSettings.Visible = true;
                    point = this.MaxGSPSettings.Location;
                    point.X = panel_width - this.MaxGSPSettings.Image.Width - this.border_width * 2;
                    this.MaxGSPSettings.Location = point;
                }
            }
        }

        private void ResizePropertiesDVTRoleSettings (int panel_width)
        {
            System.Drawing.Point    point;

            point = this.PanelDVTRoleSettingsTitle.Location;
            if (this.show_general_session_properties &&
                (this.project.GetNrSessions() > 0))
                point.Y = this.PanelGeneralPropertiesContent.Location.Y +
                    this.PanelGeneralPropertiesContent.Size.Height +
                    this.border_width;
            else
                point.Y = this.PanelGeneralPropertiesTitle.Location.Y +
                    this.PanelGeneralPropertiesTitle.Size.Height +
                    this.border_width;
            this.PanelDVTRoleSettingsTitle.Location = point;

            if (this.selected_session is Dvtk.Sessions.MediaSession ||
                (this.project.GetNrSessions() == 0))
            {
                this.PanelDVTRoleSettingsContent.Visible = false;
                this.MinDVTRoleSettings.Visible = false;
                this.MaxDVTRoleSettings.Visible = false;
            }
            else
            {
                if (this.show_dvt_role_settings)
                {
                    this.PanelDVTRoleSettingsContent.Visible = true;

                    this.MinDVTRoleSettings.Visible = true;
                    this.MaxDVTRoleSettings.Visible = false;
                    point = this.MinDVTRoleSettings.Location;
                    point.X = panel_width - this.MinDVTRoleSettings.Image.Width - this.border_width * 2;
                    this.MinDVTRoleSettings.Location = point;

                    point = this.PanelDVTRoleSettingsContent.Location;
                    point.Y = this.PanelDVTRoleSettingsTitle.Location.Y +
                        this.PanelDVTRoleSettingsTitle.Size.Height - 1;
                    this.PanelDVTRoleSettingsContent.Location = point;
                }
                else
                {
                    this.PanelDVTRoleSettingsContent.Visible = false;

                    this.MinDVTRoleSettings.Visible = false;
                    this.MaxDVTRoleSettings.Visible = true;
                    point = this.MaxDVTRoleSettings.Location;
                    point.X = panel_width - this.MaxDVTRoleSettings.Image.Width - this.border_width * 2;
                    this.MaxDVTRoleSettings.Location = point;
                }
            }
        }

        private void ResizePropertiesSUTSettings (int panel_width)
        {
            System.Drawing.Point    point;

            point = this.PanelSUTSettingTitle.Location;
            if ((this.show_dvt_role_settings) &&
                !(this.selected_session is Dvtk.Sessions.MediaSession) &&
                (this.project.GetNrSessions() > 0))
                point.Y = this.PanelDVTRoleSettingsContent.Location.Y +
                    this.PanelDVTRoleSettingsContent.Size.Height +
                    this.border_width;
            else
                point.Y = this.PanelDVTRoleSettingsTitle.Location.Y +
                    this.PanelDVTRoleSettingsTitle.Size.Height +
                    this.border_width;
            this.PanelSUTSettingTitle.Location = point;

            if (this.selected_session is Dvtk.Sessions.MediaSession ||
                (this.project.GetNrSessions() == 0))
            {
                this.PanelSUTSettingContent.Visible = false;
                this.MinSUTSettings.Visible = false;
                this.MaxSUTSettings.Visible = false;
            }
            else
            {
                if (this.show_sut_settings)
                {
                    this.PanelSUTSettingContent.Visible = true;

                    this.MinSUTSettings.Visible = true;
                    this.MaxSUTSettings.Visible = false;
                    point = this.MinSUTSettings.Location;
                    point.X = panel_width - this.MinSUTSettings.Image.Width - this.border_width * 2;
                    this.MinSUTSettings.Location = point;

                    point = this.PanelSUTSettingContent.Location;
                    point.Y = this.PanelSUTSettingTitle.Location.Y +
                        this.PanelSUTSettingTitle.Size.Height - 1;
                    this.PanelSUTSettingContent.Location = point;
                }
                else
                {
                    this.PanelSUTSettingContent.Visible = false;

                    this.MinSUTSettings.Visible = false;
                    this.MaxSUTSettings.Visible = true;
                    point = this.MaxSUTSettings.Location;
                    point.X = panel_width - this.MaxSUTSettings.Image.Width - this.border_width * 2;
                    this.MaxSUTSettings.Location = point;
                }
            }
        }

        private void ResizePropertiesSecuritySettings (int panel_width)
        {
            System.Drawing.Point    point;

            point = this.PanelSecuritySettingsTitle.Location;
            if ((this.show_sut_settings) &&
                !(this.selected_session is Dvtk.Sessions.MediaSession) &&
                (this.project.GetNrSessions() > 0))
                point.Y = this.PanelSUTSettingContent.Location.Y +
                    this.PanelSUTSettingContent.Size.Height +
                    this.border_width;
            else
                point.Y = this.PanelSUTSettingTitle.Location.Y +
                    this.PanelSUTSettingTitle.Size.Height +
                    this.border_width;
            this.PanelSecuritySettingsTitle.Location = point;

            if ((!this.CheckBoxSecureConnection.Checked) ||
                (this.selected_session is Dvtk.Sessions.MediaSession) ||
                (this.project.GetNrSessions() == 0))
            {
                this.MaxSecuritySettings.Visible = false;
                this.MinSecuritySettings.Visible = false;
                this.PanelSecuritySettingsContent.Visible = false;
                if (this.project.GetNrSessions() == 0)
                    this.CheckBoxSecureConnection.Enabled = false;
                else
                    this.CheckBoxSecureConnection.Enabled = true;
            }
            else
            {
                this.CheckBoxSecureConnection.Enabled = true;
                if (this.show_security_settings)
                {
                    this.PanelSecuritySettingsContent.Visible = true;

                    this.MinSecuritySettings.Visible = true;
                    this.MaxSecuritySettings.Visible = false;
                    point = this.MinSecuritySettings.Location;
                    point.X = panel_width - this.MinSecuritySettings.Image.Width - this.border_width * 2;
                    this.MinSecuritySettings.Location = point;

                    point = this.PanelSecuritySettingsContent.Location;
                    point.Y = this.PanelSecuritySettingsTitle.Location.Y +
                        this.PanelSecuritySettingsTitle.Size.Height - 1;
                    this.PanelSecuritySettingsContent.Location = point;
                }
                else
                {
                    this.PanelSecuritySettingsContent.Visible = false;

                    this.MinSecuritySettings.Visible = false;
                    this.MaxSecuritySettings.Visible = true;
                    point = this.MaxSecuritySettings.Location;
                    point.X = panel_width - this.MaxSecuritySettings.Image.Width - this.border_width * 2;
                    this.MaxSecuritySettings.Location = point;
                }
            }
        }

        private void ViewSplitter_SplitterMoved(object sender, System.Windows.Forms.SplitterEventArgs e)
        {
            // Don't allow the session properties view to be smaller than the
            // minimum size.
            if (this.ViewSplitter.SplitPosition + 432 > this.PanelSessionPropertiesView.Size.Width)
                this.ViewSplitter.SplitPosition = this.PanelSessionPropertiesView.Size.Width - 432;

            this.PositionPageViewButtons ();
            switch (this._active_page)
            {
                case ActivePage.session:
                    this.ResizeSessionPropertiesView ();
                    break;
                case ActivePage.description:
                    this.WebDescriptionView.Visible = true;
                    this.RichTextBoxScript.Visible = false;
                    this.ResizeWebView ();
                    break;
                case ActivePage.script:
                    this.WebDescriptionView.Visible = false;
                    this.RichTextBoxScript.Visible = true;
                    this.ResizeScriptView ();
                    break;
                case ActivePage.activity_reporting:
                    this.ResizeActivityLoggingView ();
                    break;
                case ActivePage.validation_results:
                    break;
                case ActivePage.detailed_validation:
                    this.ResizeWebView ();
                    break;
                case ActivePage.sop_classes_view:
                    this.ResizeSOPClassesView ();
                    break;
                default:
                    break;
            }
        }

        private void ResizeWebView ()
        {
            System.Drawing.Point    point;
            System.Drawing.Size     size;

            point = new Point (this.ButtonGeneralInformation.Location.X,
                this.ButtonGeneralInformation.Location.Y +
                this.ButtonGeneralInformation.Size.Height +
                this.border_width);

            size = new Size (this.PanelSessionPropertiesView.Size.Width -
                this.ButtonGeneralInformation.Location.X -
                this.border_width,
                this.PanelSessionPropertiesView.Size.Height -
                point.Y -
                this.border_width);

            this.WebDescriptionView.Location = point;
            this.WebDescriptionView.Size = size;
        }

        private void ResizeScriptView ()
        {
            System.Drawing.Point    point;
            System.Drawing.Size     size;

            point = new Point (this.ButtonGeneralInformation.Location.X,
                               this.ButtonGeneralInformation.Location.Y +
                               this.ButtonGeneralInformation.Size.Height +
                               this.border_width);

            size = new Size (this.PanelSessionPropertiesView.Size.Width -
                             this.ButtonGeneralInformation.Location.X -
                             this.border_width,
                             this.PanelSessionPropertiesView.Size.Height -
                             point.Y -
                             this.border_width);

            this.RichTextBoxScript.Location = point;
            this.RichTextBoxScript.Size = size;
        }

        private void ResizeActivityLoggingView ()
        {
            System.Drawing.Point    point;
            System.Drawing.Size     size;

            point = new Point (this.ButtonGeneralInformation.Location.X,
                this.ButtonGeneralInformation.Location.Y +
                this.ButtonGeneralInformation.Size.Height +
                this.border_width);

            size = new Size (this.PanelSessionPropertiesView.Size.Width -
                this.ButtonGeneralInformation.Location.X -
                this.border_width,
                this.PanelSessionPropertiesView.Size.Height -
                point.Y -
                this.border_width);

            this.RichTextBoxActivityLogging.Location = point;
            this.RichTextBoxActivityLogging.Size = size;
        }

        private void ResizeSOPClassesView ()
        {
            System.Drawing.Point    point;
            System.Drawing.Size     size;
            int                     height;

            // Position the panel containing the controls for the definition files.
            point = new Point (this.ButtonReturnToSessionProperties.Location.X,
                               this.ButtonReturnToSessionProperties.Location.Y +
                               this.ButtonReturnToSessionProperties.Size.Height +
                               this.border_width);
            this.PanelSOPClasses.Location = point;

            height = this.PanelSessionPropertiesView.Size.Height - point.Y - this.border_width;
            if (this.ShowRichTextBoxInfo)
                height -= (this.RichTextBoxInfo.Size.Height + this.border_width);

            // Resize the panel containing the controls for the definition files.
            size = new Size (this.PanelSessionPropertiesView.Size.Width -
                             this.PanelSOPClasses.Location.X -
                             this.border_width,
                             height);
            this.PanelSOPClasses.Size = size;

            if (this.ShowRichTextBoxInfo)
            {
                // Position the information text box.
                // The height of the text box is not adjusted.
                point = new Point (this.ButtonReturnToSessionProperties.Location.X + border_width,
                                   this.PanelSessionPropertiesView.Size.Height -
                                   this.RichTextBoxInfo.Size.Height -
                                   this.border_width);
                this.RichTextBoxInfo.Location = point;

                // Adjust the width of the rich text box. The height is not adjusted.
                size = this.RichTextBoxInfo.Size;
                size.Width = this.PanelSessionPropertiesView.Size.Width -
                    this.ButtonReturnToSessionProperties.Location.X -
                    this.border_width * 2;
                this.RichTextBoxInfo.Size = size;
            }
        }
        #endregion

        #region SessionPropertiesView

        private void UpdateSessionProperties ()
        {
			/*
            this.ClearNavigationHistory ();

            // Remember the state of the session changed property.
            // Because all controls are now updated in the form, the changed state automatically will
            // be set to 'true' (changed settings). This is not true however, only the view is changed,
            // not the content of the actual session.
            bool session_changed = this.project.GetSessionChanged (this.selected_session.SessionFileName);

            Dvt.MainForm parent = (Dvt.MainForm) this.ParentForm;
            if (this.project.GetNrSessions() > 0)
            {
                parent.file_session_save.Text = 
                    string.Format(
                    "Save session \"{0}\"",
                    this.selected_session.SessionFileName);
                parent.file_session_remove.Text = 
                    string.Format(
                    "Remove session \"{0}\"",
                    this.selected_session.SessionFileName);
            }
            else
            {
                parent.file_session_save.Text = "Save session";
                parent.file_session_remove.Text = "Remove session";
            }

            if ((this._active_page != ActivePage.session) &&
                (this._active_page != ActivePage.sop_classes_view))
                return;

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                this.ComboBoxSessionType.SelectedIndex = 0;
            else if (this.selected_session is Dvtk.Sessions.MediaSession)
                this.ComboBoxSessionType.SelectedIndex = 1;
            else // Emulator Session
                this.ComboBoxSessionType.SelectedIndex = 2;

            this.TextBoxSessionTitle.Text = this.selected_session.SessionTitle;
            this.NumericSessonID.Value = this.selected_session.SessionId;
            this.TextBoxTestedBy.Text = this.selected_session.TestedBy;
            this.DateTested.Value = this.selected_session.Date;
            this.TextBoxResultsRoot.Text = this.selected_session.ResultsRootDirectory;
            if (this.selected_session is Dvtk.Sessions.ScriptSession)
            {
                Dvtk.Sessions.ScriptSession scriptSession;

                scriptSession = (Dvtk.Sessions.ScriptSession) this.selected_session;
                this.ButtonBrowseScriptsDir.Enabled = true;
                this.TextBoxScriptRoot.Text = scriptSession.DicomScriptRootDirectory;

                this.ButtonBrowseDescriptionDir.Enabled = true;
                this.TextBoxDescriptionRoot.Text = scriptSession.DescriptionDirectory;
            }
            else
            {
                this.ButtonBrowseScriptsDir.Enabled = false;
                this.TextBoxScriptRoot.Text = "";

                this.ButtonBrowseDescriptionDir.Enabled = false;
                this.TextBoxDescriptionRoot.Text = "";
            }

            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                this.ButtonSpecifyTransferSyntaxes.Enabled = true;
            else
                this.ButtonSpecifyTransferSyntaxes.Enabled = false;

            if (this.selected_session is Dvtk.Sessions.MediaSession)
            {
                this.CheckBoxSecureConnection.Checked = false;
                this.CheckBoxSecureConnection.Enabled = false;
            }
            else
            {
                this.CheckBoxSecureConnection.Enabled = true;

                if (this.selected_session is Dvtk.Sessions.ScriptSession)
                {
                    Dvtk.Sessions.ScriptSession session = (Dvtk.Sessions.ScriptSession) this.selected_session;
                    this.TextBoxDVTImplClassUID.Text = session.DvtSystemSettings.ImplementationClassUid;
                    this.TextBoxDVTImplVersionName.Text = session.DvtSystemSettings.ImplementationVersionName;
                    this.TextBoxDVTAETitle.Text = session.DvtSystemSettings.AeTitle;
                    this.NumericDVTListenPort.Value = session.DvtSystemSettings.Port;
                    this.NumericDVTTimeOut.Value = session.DvtSystemSettings.SocketTimeout;
                    this.NumericDVTMaxPDU.Value = session.DvtSystemSettings.MaximumLengthReceived;
                    this.TextBoxSUTImplClassUID.Text = session.SutSystemSettings.ImplementationClassUid;
                    this.TextBoxSUTImplVersionName.Text = session.SutSystemSettings.ImplementationVersionName;
                    this.TextBoxSUTAETitle.Text = session.SutSystemSettings.AeTitle;
                    this.NumericSUTListenPort.Value = session.SutSystemSettings.Port;
                    this.TextBoxSUTTCPIPAddress.Text = session.SutSystemSettings.HostName;
                    this.NumericSUTMaxPDU.Value = session.SutSystemSettings.MaximumLengthReceived;
                }
                if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                {
                    Dvtk.Sessions.EmulatorSession session = (Dvtk.Sessions.EmulatorSession) this.selected_session;
                    this.TextBoxDVTImplClassUID.Text = session.DvtSystemSettings.ImplementationClassUid;
                    this.TextBoxDVTImplVersionName.Text = session.DvtSystemSettings.ImplementationVersionName;
                    this.TextBoxDVTAETitle.Text = session.DvtSystemSettings.AeTitle;
                    this.NumericDVTListenPort.Value = session.DvtSystemSettings.Port;
                    this.NumericDVTTimeOut.Value = session.DvtSystemSettings.SocketTimeout;
                    this.NumericDVTMaxPDU.Value = session.DvtSystemSettings.MaximumLengthReceived;
                    this.TextBoxSUTImplClassUID.Text = session.SutSystemSettings.ImplementationClassUid;
                    this.TextBoxSUTImplVersionName.Text = session.SutSystemSettings.ImplementationVersionName;
                    this.TextBoxSUTAETitle.Text = session.SutSystemSettings.AeTitle;
                    this.NumericSUTListenPort.Value = session.SutSystemSettings.Port;
                    this.TextBoxSUTTCPIPAddress.Text = session.SutSystemSettings.HostName;
                    this.NumericSUTMaxPDU.Value = session.SutSystemSettings.MaximumLengthReceived;
                }

                if (this.selected_session is Dvtk.Sessions.ISecure)
                {
                    Dvtk.Sessions.ISecuritySettings security_settings = null;

                    security_settings = (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings;

                    this.CheckBoxSecureConnection.Checked = security_settings.SecureSocketsEnabled;
                    this.ListBoxSecuritySettings.SelectedIndex = 0;

                    this.CheckBoxCheckRemoteCertificates.Checked = security_settings.CheckRemoteCertificate;
                    this.CheckBoxCacheSecureSessions.Checked = security_settings.CacheTlsSessions;
                    this.CheckBoxTLS.Checked = ((security_settings.TlsVersionFlags & Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_TLSv1) != 0);
                    this.CheckBoxSSL.Checked = ((security_settings.TlsVersionFlags & Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_SSLv3) != 0);
                    this.CheckBoxAuthenticationDSA.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_DSA) != 0);
                    this.CheckBoxAuthenticationRSA.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_RSA) != 0);
                    this.CheckBoxDataIntegrityMD5.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_MD5) != 0);
                    this.CheckBoxDataIntegritySHA.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_SHA1) != 0);
                    this.CheckBoxEncryptionNone.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_NONE) != 0);
                    this.CheckBoxEncryptionTripleDES.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_3DES) != 0);
                    this.CheckBoxEncryptionAES128.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES128) != 0);
                    this.CheckBoxEncryptionAES256.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES256) != 0);
                    this.CheckBoxKeyExchangeDH.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_DH) != 0);
                    this.CheckBoxKeyExchangeRSA.Checked = ((security_settings.CipherFlags & Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_RSA) != 0);
                }
                else
                    this.CheckBoxSecureConnection.Checked = false;
            }

            // Restore the session_changed property.
            //////this.project.SetSessionChanged (this.selected_session.SessionFileName, session_changed);
			*/
        }

        private void VScrollBarSessionInfo_Scroll(object sender, System.Windows.Forms.ScrollEventArgs e)
        {
            this.scrollbar_offset = -e.NewValue;
            this.ResizeSessionPropertiesView ();
        }

        #endregion // SessionPropertiesView

        #region SessionBrowser
        public void SessionAddNew ()
        {
            WizardNew   wizard;
        
            wizard = new WizardNew (WizardNew.StartPage.session);
            wizard.ShowDialog(this);
            if (wizard.has_created_session)
            {
                this.project.AddSession (wizard.GetSession());
                this.UpdateSessionTreeView ();
                this.SessionBrowser.Select ();
            }
        }

        public void SessionAddExisting ()
        {
            // TODO: Set initial directory.
            // this.DialogOpenSessionFile.InitialDirectory;
            try 
            {
                if (this.DialogOpenSessionFile.ShowDialog () == DialogResult.OK)
                {
                    ArrayList skipped_files = new ArrayList ();
                    foreach (object file in this.DialogOpenSessionFile.FileNames)
                    {
                        if (this.project.ContainsSession (file.ToString()))
                            skipped_files.Add (file);
                        else
                        {
                            this.project.AddSession (file.ToString());
                        }
                    }
                    if (skipped_files.Count > 0)
                    {
                        string text = "Skipped the following session files because they already exist:";
                        foreach (object file in skipped_files)
                            text += "\n" + file.ToString();
                        MessageBox.Show (this, text, "Skipped files", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.UpdateSessionTreeView ();
                    this.SessionBrowser.Select ();
                }
            }
            catch
            {
                MessageBox.Show (this,
                    "Please select less session files.\nIf you want to add more session files, you can add them with a second Add action.",
                    "Too many session files selected",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        public void SessionRemove ()
        {
            TreeNode selected_item = this.SessionBrowser.SelectedNode;
            if (selected_item == null)
                return;

            //if (this.project.GetSessionChanged (this.SessionBrowser.SelectedNode.Text))
            {
                MessageBox.Show (this, "Changed session content.");
            }
            string msg =
                string.Format(
                "Are you sure you want to remove the session file: {0}\n",
                this.SessionBrowser.SelectedNode.Text
                );
            if (MessageBox.Show (this,
                msg,
                "Remove session from project?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // Remove the activity handler from the current selected session.
                if (this.selected_session != null)
                    this.selected_session.ActivityReportEvent -= this._activity_handler;

                //this.project.RemoveSession (this.SessionBrowser.SelectedNode.Text);
                this.UpdateSessionTreeView ();
                this.ResizeProjectForm ();
                // TODO: Update the result view if needed.
            }

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        public void SessionSave ()
        {
            if (this.selected_session == null)
                return;

            this.selected_session.SaveToFile ();

            ////////this.project.SetSessionChanged (this.selected_session.SessionFileName, false);
        }

        bool        _session_browser_updating = false;
        ArrayList   _result_files;

        public void RefreshSessionTreeView ()
        {
            this.SessionBrowser.Nodes.Clear ();
            this.UpdateSessionTreeView ();
        }

        public void UpdateSessionTreeView ()
        {
            // Strategy to update the treeview:
            // - If no project is loaded, the treeview must be empty.
            // - For each session present in the treeview, check if that
            //   session is (still) present in the loaded project. If not,
            //   delete that entire node (ie. remove all scripts, results,
            //   references to other files, ...)
            //   If the session is present, use that node to update the
            //   session properties specific for the session type.
            //   Currently we have script, media and emulator session types.
            // - Check if a session in the loaded project is present in the
            //   tree view. If it's present, the previous rule already made
            //   sure the contents are updated. If it's not present, add the
            //   new session including all scripts, results and references
            //   to other files.

            if (this._session_browser_updating)
                return;

            this._session_browser_updating = true;

            Cursor.Current = Cursors.WaitCursor;

            if (this.project == null)
                this.SessionBrowser.Nodes.Clear();
            else
            {
                if (this.project.GetNrSessions() == 0)
                    this.SessionBrowser.Nodes.Clear();
                else
                {
                    this.SessionBrowser.BeginUpdate ();

                    foreach (TreeNode node in this.SessionBrowser.Nodes)
                    {
                        // Check if the session presented by the node is (still)
                        // present in the project.
                        if (this.project.ContainsSession (node.Text))
                        {
                            // Update the session node.
                            //Dvtk.Sessions.Session   session = this.project.GetSession (node.Text);

                            // First collect all result files.
                            //this.CollectSessionResultFiles (session);
/*
                            // Now update the session node.
                            if (session is Dvtk.Sessions.ScriptSession)
                                this.UpdateScriptSessionNode (node, (Dvtk.Sessions.ScriptSession) session);
                            if (session is Dvtk.Sessions.MediaSession)
                                this.UpdateMediaSessionNode (node, (Dvtk.Sessions.MediaSession) session);
                            if (session is Dvtk.Sessions.EmulatorSession)
                                this.UpdateEmulatorSessionNode (node, (Dvtk.Sessions.EmulatorSession) session);
                            this.AddUncategorizedResultFiles (node);
							*/
                        }
                        else
                        {
                            // The session is not present in the project, remove
                            // it from the treeview.
                            this.SessionBrowser.Nodes.Remove (node);
                        }
                    }
                    for (int index=0 ; index<project.GetNrSessions() ; index++)
                    {
                        Dvtk.Sessions.Session   session = this.project.GetSession (index);
                        bool                    session_present = false;

                        // If the session is not present in the treeview, create
                        // a new node and add it to the tree.
                        foreach (TreeNode node in this.SessionBrowser.Nodes)
                        {
                            if (session.SessionFileName == node.Text)
                                session_present = true;
                        }
                        if (!session_present)
                        {
                            // This session file is not yet present in the
                            // tree view; add it.

                            // First collect all result files.
                            this.CollectSessionResultFiles (session);

                            TreeNode node = new TreeNode(session.SessionFileName);
                            this.SessionBrowser.Nodes.Add (node);
                            if (session is Dvtk.Sessions.ScriptSession)
                                this.UpdateScriptSessionNode (node, (Dvtk.Sessions.ScriptSession) session);
                            if (session is Dvtk.Sessions.MediaSession)
                                this.UpdateMediaSessionNode (node, (Dvtk.Sessions.MediaSession) session);
                            if (session is Dvtk.Sessions.EmulatorSession)
                                this.UpdateEmulatorSessionNode (node, (Dvtk.Sessions.EmulatorSession) session);
                            this.AddUncategorizedResultFiles (node);
                        }
                    }
                    this.SessionBrowser.EndUpdate ();
                }
            }
            Cursor.Current = Cursors.Default;
            this._session_browser_updating = false;
        }

        private void CollectSessionResultFiles (Dvtk.Sessions.Session session)
        {
            System.IO.DirectoryInfo   di;
            System.IO.FileInfo[]      files;

            this._result_files = new ArrayList ();
            di = new DirectoryInfo (session.ResultsRootDirectory);
            if (di.Exists)
            {
                files = di.GetFiles ("*.xml");
                foreach (FileInfo file in files)
                    this._result_files.Add (file.Name);
            }
        }

        private void AddUncategorizedResultFiles (TreeNode parent)
        {
            TreeNode    results_node;

            if (this._result_files.Count == 0)
                return;

            results_node = new TreeNode(UNCATEGORIZED_RESULTS);
            parent.Nodes.Add (results_node);

            foreach (string file in this._result_files)
                results_node.Nodes.Add (file);

            this._result_files.Clear ();
        }

        private void UpdateScriptSessionNode (TreeNode parent, Dvtk.Sessions.ScriptSession session)
        {
            DirectoryInfo   di;
            DirectoryInfo   results_dir;

            // Get the script directory. If the directory returned, is the same
            // as the current directory, replace the directory name with the
            // directory where the .ses file resides in.
            if (session.DicomScriptRootDirectory == Environment.CurrentDirectory)
            {
                string  path = System.IO.Path.GetDirectoryName (session.SessionFileName);
                session.DicomScriptRootDirectory = path;
                di = new DirectoryInfo (path);
            }
            else
                di = new DirectoryInfo (session.DicomScriptRootDirectory);

            if (di.Exists)
            {
                FileInfo[] files = di.GetFiles();
                results_dir = new DirectoryInfo (session.ResultsRootDirectory);

                // Check if a script is already present in the tree. If that's the
                // case, don't add it, just update the content.
                foreach (TreeNode node in parent.Nodes)
                {
                    bool found = false;
                    foreach (FileInfo file in files)
                    {
                        if (node.Text == file.Name)
                        {
                            found = true;
                            // Update the script node.
                            this.UpdateScriptNode (node, file);
                        }
                    }
                    if (!found)
                    {
                        // The script represented in the node is not present
                        // in the script directory.
                        parent.Nodes.Remove (node);
                    }
                }

                // Check if a script is not yet present in the tree. If that's the
                // case, add it including references to result files.
                foreach (FileInfo file in files)
                {
                    string ext = file.Extension;
                    if ((ext == ".ts") ||
                        (ext == ".ds") ||
                        (ext == ".dss") ||
                        (ext == ".vbs") ||
                        (ext == ".js"))
                    {
                        bool found = false;
                        foreach (TreeNode node in parent.Nodes)
                        {
                            if (node.Text == file.Name)
                                found = true;
                        }
                        if (!found)
                        {
                            // The script is not yet present in the tree; add it.
                            TreeNode    node = new TreeNode (file.Name);
                            parent.Nodes.Add (node);
                            this.UpdateScriptNode (node, file);
                        }
                    }
                }
            }
        }

        private void UpdateMediaSessionNode (TreeNode parent, Dvtk.Sessions.MediaSession session)
        {
			/*
            //MediaSession med_ses = this.project.GetMediaSession(session.SessionFileName);
            foreach (TreeNode node in parent.Nodes)
            {
                bool found = false;
                foreach (string med_file in med_ses.media_files)
                {
                    if (node.Text == med_file)
                    {
                        found = true;
                        this.UpdateMediaFileNode (node, session);
                    }
                }
                if (!found)
                {
                    // The media file represented in the node is not
                    // present in the media session.
                    parent.Nodes.Remove (node);
                }
			}
            foreach (string med_file in med_ses.media_files)
            {
                bool found = false;
                foreach (TreeNode node in parent.Nodes)
                    if (node.Text == med_file)
                        found = true;
                if (!found)
                {
                    // The results file is not yet present in the tree; add it.
                    TreeNode node = new TreeNode (med_file);
                    this.UpdateMediaFileNode (node, session);
                    parent.Nodes.Add (node);
                }
            }
			*/
            return;
        }

        private void UpdateMediaFileNode (TreeNode parent, Dvtk.Sessions.MediaSession session)
        {
            ArrayList   registered_results = new ArrayList();
            FileInfo    med_file = new FileInfo (parent.Text);
            string      results_name = med_file.Name.Replace ('.', '_');

            foreach (TreeNode node in parent.Nodes)
            {
                bool found = false;
                foreach (string file in this._result_files)
                {
                    if (file.IndexOf (results_name + "_res.xml") > 0)
                    {
                        // This results file is generated from the current media file.
                        if (node.Text == file)
                        {
                            found = true;

                            // Store the results name in a list that needs to be removed from
                            // all results files.
                            registered_results.Add (file);
                        }
                    }
                }
                if (!found)
                {
                    // The results file represented in the node is not
                    // present in the results directory.
                    parent.Nodes.Remove (node);
                }
            }

            // Check if a results file is not yet present in the tree. If that's the
            // case, add it including references to result files.
            foreach (string file in this._result_files)
            {
                if (file.IndexOf (results_name) > 0)
                {
                    bool found = false;
                    // This results file is generated from the current media file.
                    foreach (TreeNode node in parent.Nodes)
                    {
                        if (file == node.Text)
                            found = true;
                    }

                    if ((!found) &&
                        (file.IndexOf (results_name + "_res.xml") > 0))
                    {
                        // We only want to add the TOC file to the session browser tree.
                        // That's why we filter the name through an additional string compare.
                        parent.Nodes.Add (file);
                    }

                    // Store the results name in a list that needs to be removed from
                    // all results files.
                    registered_results.Add (file);
                }
            }
            // Remove the registered result files from the pre-generated list of
            // result files. We don't want them to show up in the list
            // of 'uncategorized results'.
            foreach (string file in registered_results)
                this._result_files.Remove (file);
        }

        private void UpdateEmulatorSessionNode (TreeNode parent, Dvtk.Sessions.EmulatorSession session)
        {
            UpdateEmulatorNode (parent, "Storage SCP Emulator", "st_scp_em");
            UpdateEmulatorNode (parent, "Storage SCU Emulator", "st_scu_em");
            UpdateEmulatorNode (parent, "Print SCP Emulator", "pr_scp_em");
        }

        private void UpdateEmulatorNode (TreeNode parent, string emulator, string short_name)
        {
            bool found = false;
            foreach (TreeNode node in parent.Nodes)
            {
                if (node.Text == emulator)
                {
                    found = true;
                    UpdateEmulatorResultsNode (node, short_name);
                }
            }
            if (!found)
            {
                TreeNode node = new TreeNode (emulator);
                UpdateEmulatorResultsNode (node, short_name);
                parent.Nodes.Add (node);
            }
        }

        private void UpdateEmulatorResultsNode (TreeNode parent, string short_name)
        {
            ArrayList   registered_results = new ArrayList();

            // Check if a results file is already present in the tree. If that's the
            // case, don't add it, just update the content.
            foreach (TreeNode node in parent.Nodes)
            {
                bool found = false;
                foreach (string file in this._result_files)
                {
                    string filename = "_" + short_name + "_res";
                    if (file.IndexOf (filename + ".xml") > 0)
                        if (node.Text == file)
                        {
                            found = true;

                            // Store the results name in a list that needs to be removed from
                            // all results files.
                            registered_results.Add (file);
                        }
                }
                if (!found)
                {
                    // The results file represented in the node is not
                    // present in the results directory.
                    parent.Nodes.Remove (node);
                }
            }

            // Check if a results file is not yet present in the tree. If that's the
            // case, add it including references to result files.
            foreach (string file in this._result_files)
            {
                string filename = "_" + short_name + "_res";
                if (file.IndexOf (filename) > 0)
                {
                    bool found = false;
                    foreach (TreeNode node in parent.Nodes)
                        if (node.Text == file)
                            found = true;
                    if ((!found) &&
                        (file.IndexOf (filename + ".xml") > 0))
                    {
                        // We only want to add the TOC file to the session browser tree.
                        // That's why we filter the name through an additional string compare.

                        // The results file is not yet present in the tree; add it.
                        parent.Nodes.Add (file);
                    }

                    // Store the results name in a list that needs to be removed from
                    // all results files.
                    registered_results.Add (file);
                }
            }
            // Remove the registered result files from the pre-generated list of
            // result files. We don't want them to show up in the list
            // of 'uncategorized results'.
            foreach (string file in registered_results)
                this._result_files.Remove (file);
        }

        private void UpdateScriptNode (TreeNode parent, System.IO.FileInfo script_file)
        {
            // Search for result files that are relevant for this script.
            // The results file name is built like:
            // 000_xxxxxxx_ds_res.xml
            // with 000 being the index for the results for the same script
            // xxxxxxx is the filename of the script (without extension)
            // ds is the script file extension
            // res.xml indicating it's a results file.

            ArrayList   registered_results = new ArrayList();

            // Check if a results file is already present in the tree. If that's the
            // case, don't add it, just update the content.
            foreach (TreeNode node in parent.Nodes)
            {
                bool found = false;
                foreach (string file in this._result_files)
                {
                    string filename = "_" +
                        script_file.Name.Replace (".", "_") +
                        "_res";
                    if (file.IndexOf (filename) > 0)
                        if (node.Text == file)
                        {
                            found = true;

                            // Store the results name in a list that needs to be removed from
                            // all results files.
                            registered_results.Add (file);
                        }
                }
                if (!found)
                {
                    // The results file represented in the node is not
                    // present in the results directory.
                    parent.Nodes.Remove (node);
                }
            }

            // Check if a results file is not yet present in the tree. If that's the
            // case, add it including references to result files.
            foreach (string file in this._result_files)
            {
                string filename = "_" +
                    script_file.Name.Replace (".", "_") +
                    "_res";
                if (file.IndexOf (filename) > 0)
                {
                    bool found = false;
                    foreach (TreeNode node in parent.Nodes)
                        if (node.Text == file)
                            found = true;
                    if (!found)
                    {
                        // The results file is not yet present in the tree; add it.
                        parent.Nodes.Add (file);
                    }

                    // Store the results name in a list that needs to be removed from
                    // all results files.
                    registered_results.Add (file);
                }
            }
            // Remove the registered result files from the pre-generated list of
            // result files. We don't want them to show up in the list
            // of 'uncategorized results'.
            foreach (string file in registered_results)
                this._result_files.Remove (file);
        }

        private void SelectAndDisplayGeneratedResults ()
        {
            // We want to display the generated result file that is associated
            // with the current selected script, emulator or media file.
            TreeNode    node;
            string      script = "";
            string      emulator = "";
            string      media_file = "";
            string      result = "";
            string      filename;

            node = this.SessionBrowser.SelectedNode;
            node.Expand ();

            // Generate the expected name of the results file.
            ushort id = this.selected_session.SessionId;
            this.GetNodeProperties (node, out script, out emulator, out media_file, out result);
            if (script != "")
                filename = id.ToString ("000") + "_" + new FileInfo(script).Name.Replace (".", "_") + "_res.xml";




            else if (emulator != "")
            {
                if (emulator == "Start Storage SCP Emulator")
                    filename = id.ToString ("000") + "_st_scp_em_res.xml";



                else if (emulator == "Start Storage SCU Emulator")
                    filename = id.ToString ("000") + "_st_scu_em_res.xml";



                else /* emulator == "Start Print SCP Emulator" */
                    filename = id.ToString ("000") + "_pr_scp_em_res.xml";



            }
            else /* media_file != "" */
                filename = id.ToString ("000") + "_" + new FileInfo (media_file).Name.Replace (".", "_") + "_res.xml";




            /* Find the expected results name in the session browse tree. */
            foreach (TreeNode res_node in node.Nodes)
            {
                if (res_node.Text == filename)
                {
                    this.SessionBrowser.SelectedNode = res_node;
                }
            }
        }

        private void SessionBrowser_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            // It's possible that the user selected another session or an item from another session.
            // Remove the activity handler from the current selected session.
            if (this.selected_session != null)
                this.selected_session.ActivityReportEvent -= this._activity_handler;

            if (this._active_page == ActivePage.sop_classes_view)
            {
                // Before we switch session properties, we must load/unload
                // the necessary definition files.
                this.LoadUnloadDefinitionFiles ();
            }
            TreeNode tree_node = this.SessionBrowser.SelectedNode;
            if (tree_node != null)
            {
                string script = "";
                string emulator = "";
                string media_file = "";
                string results = "";

                this.selected_description = "";
                this.selected_results = "";
                this.selected_script = "";

                this.GetNodeProperties (tree_node, out script, out emulator, out media_file, out results);

                // We now know what session is selected in the browser. Set the activity handler to the
                // current selected session.
                this.selected_session.ActivityReportEvent += this._activity_handler;

                this.ClearNavigationHistory ();

                if (this.selected_session is Dvtk.Sessions.ScriptSession)
                {
                    Dvtk.Sessions.ScriptSession scriptSession = this.selected_session as Dvtk.Sessions.ScriptSession;
                    if (script != "")
                    {
                        string html_file = script.Replace ('.', '_') + ".html";
                        this.selected_script = 
                            System.IO.Path.Combine(
                            scriptSession.DicomScriptRootDirectory,
                            script);
                        this.last_selected_script = tree_node;

                        /* A script is selected. If a description dir is available, check if the */
                        /* description dir contains a file named like the script with the extension */
                        /* of the script replaced with .html */
                        if (scriptSession.DescriptionDirectory != "")
                        {
                            DirectoryInfo   dir_info = new DirectoryInfo (scriptSession.DescriptionDirectory);

                            // Reset the selected description file
                            this.selected_description = "";

                            if (dir_info.Exists)
                            {
                                foreach (FileInfo file in dir_info.GetFiles())
                                {
                                    if (file.Name == html_file)
                                        this.selected_description = file.FullName;
                                }
                            }
                        }

                        if (this.selected_description != "")
                        {
                            object Zero = 0;
                            object EmptyString = "";

                            this.WebDescriptionView.Navigate (this.selected_description, ref Zero, ref EmptyString, ref EmptyString, ref EmptyString);
                            this._active_page = ActivePage.description;
                        }
                        else
                        {
                            this.ClearNavigationHistory ();

                            this.RichTextBoxScript.LoadFile (this.selected_script, RichTextBoxStreamType.PlainText);
                            this._active_page = ActivePage.script;
                        }
                        this.ButtonGeneralInformation.Checked = true;
                    }
                    else
                    {
                        if (results != "")
                        {
                            this._active_page = ActivePage.detailed_validation;
                            this.last_selected_result = tree_node;
                            this.selected_results = 
                                System.IO.Path.Combine(
                                scriptSession.ResultsRootDirectory,
                                results);
                            this.ButtonDetailedValidation.Checked = true;

                            this.ClearNavigationHistory ();

                            this.UpdateDetailedResultsView ();
                        }
                        else
                        {
                            // The session file has been clicked. The view can be either
                            // the sop classes view or the session properties view. Don't change
                            // the view when the current view is sop classes view.
                            if (this._active_page != ActivePage.sop_classes_view)
                                this._active_page = ActivePage.session;
                            this.last_selected_session = tree_node;
                        }
                    }
                }
                if (this.selected_session is Dvtk.Sessions.MediaSession)
                {
                    Dvtk.Sessions.MediaSession media_session = (Dvtk.Sessions.MediaSession) this.selected_session;
                    if (results != "")
                    {
                        this._active_page = ActivePage.detailed_validation;
                        this.last_selected_result = tree_node;
                        this.selected_results = 
                            System.IO.Path.Combine(
                            media_session.ResultsRootDirectory,
                            results);

                        this.ClearNavigationHistory ();

                        this.UpdateDetailedResultsView ();
                        this.ButtonDetailedValidation.Checked = true;
                    }
                    else
                    {
                        // The session file has been clicked. The view can be either
                        // the sop classes view or the session properties view. Don't change
                        // the view when the current view is sop classes view.
                        if (this._active_page != ActivePage.sop_classes_view)
                            this._active_page = ActivePage.session;
                        this.last_selected_session = tree_node;
                    }
                }
                if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                {
                    Dvtk.Sessions.EmulatorSession emulator_session = (Dvtk.Sessions.EmulatorSession) this.selected_session;
                    if (results != "")
                    {
                        this._active_page = ActivePage.detailed_validation;
                        this.last_selected_result = tree_node;
                        this.selected_results = 
                            System.IO.Path.Combine(
                            emulator_session.ResultsRootDirectory,
                            results);

                        this.ClearNavigationHistory ();

                        this.UpdateDetailedResultsView ();
                        this.ButtonDetailedValidation.Checked = true;
                    }
                    else
                    {
                        // The session file has been clicked. The view can be either
                        // the sop classes view or the session properties view. Don't change
                        // the view when the current view is sop classes view.
                        if (this._active_page != ActivePage.sop_classes_view)
                            this._active_page = ActivePage.session;
                        this.last_selected_session = tree_node;
                    }
                }

                // Update the title bar of the project form
                this.Text = string.Format("Active session: {0}", this.selected_session.SessionFileName);

                if (this._active_page == ActivePage.sop_classes_view)
                    this.UpdateSOPClassesView ();

                this.UpdateSessionProperties ();
                this.UpdatePageVisibility ();
                this.ResizeProjectForm ();

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
        }

        private void SessionBrowser_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.button_clicked = e.Button;
            this.mouse_x = e.X;
            this.mouse_y = e.Y;

            if (e.Button == MouseButtons.Right)
            {
                TreeNode    node = this.SessionBrowser.GetNodeAt (e.X, e.Y);
                this.SessionBrowser.SelectedNode = node;
                if (node != null)
                {
                    string  script = "";
                    string  emulator = "";
                    string  media_file = "";
                    string  results = "";
                    bool    has_results = false;

                    this.GetNodeProperties (node, out script, out emulator, out media_file, out results);
                    has_results = this.HasResults (node);

                    if (has_results)
                    {
                        this.CM_MarkIgnore.Enabled = true;
                        this.CM_UnmarkIgnore.Enabled = true;
                    }
                    else
                    {
                        this.CM_MarkIgnore.Enabled = false;
                        this.CM_UnmarkIgnore.Enabled = false;
                    }

                    if (results != "")
                    {
                        // The user pressed the RMB on a results file.
                        this.CM_Execute.Visible = false;
                        this.CM_Execute.DefaultItem = false;
                        this.CM_StartEmulator.Visible = false;
                        this.CM_StartEmulator.DefaultItem = false;
                        this.CM_ValidateMedia.Visible = false;
                        this.CM_ValidateMedia.DefaultItem = false;
                        this.CM_Sep_2.Visible = false;
                        this.CM_Display.DefaultItem = true;
                        this.CM_Display.Visible = true;
                        this.CM_Edit.Visible = false;
                        this.CM_Sep_3.Visible = false;
                        this.CM_AddNewSession.Visible = false;
                        this.CM_AddExistingSession.Visible = false;
                        this.CM_Remove.Visible = false;
                        this.CM_Save.Visible = false;
                        this.CM_Sep_4.Visible = false;
                        this.CM_Properties.DefaultItem = false;
                        this.CM_Properties.Visible = false;
                    }
                    else if (script != "")
                    {
                        // The user pressed the RMB on a script file.
                        this.CM_Execute.Visible = true;
                        this.CM_Execute.DefaultItem = true;
                        this.CM_StartEmulator.Visible = false;
                        this.CM_StartEmulator.DefaultItem = false;
                        this.CM_ValidateMedia.Visible = false;
                        this.CM_ValidateMedia.DefaultItem = false;
                        this.CM_Sep_2.Visible = true;
                        this.CM_Display.DefaultItem = false;
                        this.CM_Display.Visible = true;
                        this.CM_Edit.Visible = true;
                        this.CM_Sep_3.Visible = false;
                        this.CM_AddNewSession.Visible = false;
                        this.CM_AddExistingSession.Visible = false;
                        this.CM_Remove.Visible = false;
                        this.CM_Save.Visible = false;
                        this.CM_Sep_4.Visible = false;
                        this.CM_Properties.DefaultItem = false;
                        this.CM_Properties.Visible = false;
                    }
                    else if (emulator != "")
                    {
                        // The user pressed the RMB on an emulator.
                        this.CM_Execute.Visible = false;
                        this.CM_Execute.DefaultItem = false;
                        this.CM_StartEmulator.Visible = true;
						// MK!!
						/*
                        if (this.project.IsRunningEmulatorSession (this.selected_session.SessionFileName))
                        {
                            this.CM_StartEmulator.DefaultItem = false;
                            this.CM_StartEmulator.Enabled = false;
                        }
                        else
                        {
                            this.CM_StartEmulator.DefaultItem = true;
                            this.CM_StartEmulator.Enabled = true;
                        }
						*/
                        this.CM_StartEmulator.Text = string.Format("Start {0}", emulator);
                        this.CM_ValidateMedia.Visible = false;
                        this.CM_ValidateMedia.DefaultItem = false;
                        this.CM_Sep_2.Visible = false;
                        this.CM_Display.DefaultItem = false;
                        this.CM_Display.Visible = false;
                        this.CM_Edit.Visible = false;
                        this.CM_Sep_3.Visible = false;
                        this.CM_AddNewSession.Visible = false;
                        this.CM_AddExistingSession.Visible = false;
                        this.CM_Remove.Visible = false;
                        this.CM_Save.Visible = false;
                        this.CM_Sep_4.Visible = true;
						// MK!!
						/*
                        if (this.project.IsRunningEmulatorSession (this.selected_session.SessionFileName))
                            this.CM_Properties.DefaultItem = true;
                        else
                            this.CM_Properties.DefaultItem = false;
							*/
                        this.CM_Properties.Visible = true;
                    }
                    else if (media_file != "")
                    {
                        // The user pressed the RMB on a media file.
                        this.CM_Execute.Visible = false;
                        this.CM_Execute.DefaultItem = false;
                        this.CM_StartEmulator.Visible = false;
                        this.CM_StartEmulator.DefaultItem = false;
                        this.CM_ValidateMedia.Visible = true;
                        this.CM_ValidateMedia.DefaultItem = true;
                        this.CM_ValidateMedia.Text = string.Format("Validate {0}", media_file);
                        this.CM_Sep_2.Visible = false;
                        this.CM_Display.DefaultItem = false;
                        this.CM_Display.Visible = false;
                        this.CM_Edit.Visible = false;
                        this.CM_Sep_3.Visible = false;
                        this.CM_AddNewSession.Visible = false;
                        this.CM_AddExistingSession.Visible = false;
                        this.CM_Remove.Visible = false;
                        this.CM_Save.Visible = false;
                        this.CM_Sep_4.Visible = true;
                        this.CM_Properties.DefaultItem = false;
                        this.CM_Properties.Visible = true;
                    }
                    else
                    {
                        // The user pressed the RMB on a session file.
                        this.CM_Execute.Visible = false;
                        this.CM_Execute.DefaultItem = false;
                        this.CM_StartEmulator.Visible = false;
                        this.CM_StartEmulator.DefaultItem = false;
                        this.CM_ValidateMedia.DefaultItem = false;
                        if (this.selected_session is Dvtk.Sessions.MediaSession)
                        {
                            this.CM_ValidateMedia.Visible = true;
                            this.CM_ValidateMedia.Text = "Validate media file";
                            this.CM_Sep_2.Visible = true;
                        }
                        else
                        {
                            this.CM_ValidateMedia.Visible = false;
                            this.CM_Sep_2.Visible = false;
                        }
                        this.CM_Display.DefaultItem = false;
                        this.CM_Display.Visible = false;
                        this.CM_Edit.Visible = false;
                        this.CM_Sep_3.Visible = false;
                        this.CM_AddNewSession.Visible = false;
                        this.CM_AddExistingSession.Visible = false;
                        this.CM_Remove.Visible = true;
                        this.CM_Save.Visible = true;
                        this.CM_Sep_4.Visible = true;
                        this.CM_Properties.DefaultItem = true;
                        this.CM_Properties.Visible = true;
                    }
                }
                else
                {
                    // The user pressed the RMB outside any of the tree items.
                    this.CM_Execute.Visible = false;
                    this.CM_Execute.DefaultItem = false;
                    this.CM_StartEmulator.Visible = false;
                    this.CM_StartEmulator.DefaultItem = false;
                    this.CM_ValidateMedia.Visible = false;
                    this.CM_ValidateMedia.DefaultItem = false;
                    this.CM_Sep_2.Visible = false;
                    this.CM_Display.DefaultItem = false;
                    this.CM_Display.Visible = false;
                    this.CM_Edit.Visible = false;
                    this.CM_Sep_3.Visible = false;
                    this.CM_AddNewSession.Visible = true;
                    this.CM_AddExistingSession.Visible = true;
                    this.CM_Remove.Visible = false;
                    this.CM_Save.Visible = false;
                    this.CM_Sep_4.Visible = false;
                    this.CM_Properties.DefaultItem = false;
                    this.CM_Properties.Visible = false;
                }
            }
        }

        // Callback method must have the same signature as the
        // AsyncCallback delegate.
        private void OnScriptDone(IAsyncResult ar) 
        {
            ((Dvtk.Sessions.ScriptSession)this.selected_session).EndExecuteScript(ar);
            // Notify the root process that the script has finished executing
            this.RunningProcessEvent (this, null);
        }

        private void SessionBrowser_DoubleClick(object sender, System.EventArgs e)
        {
            if (this.button_clicked == MouseButtons.Left)
            {
                string  script="";
                string  emulator = "";
                string  media_file = "";
                string  results="";

                TreeNode tree_node = this.SessionBrowser.SelectedNode;
                if (tree_node != null)
                {
                    // An item in the tree has been selected.
                    this.GetNodeProperties (tree_node, out script, out emulator, out media_file, out results);
                    if (results != "")
                    {
//                        this.last_selected_result = tree_node;
                        this.ButtonDetailedValidation.Checked = true;
                    }
                    else if (script != "")
                    {
                        if (this.selected_session is Dvtk.Sessions.ScriptSession)
                        {
//                            this.last_selected_script = tree_node;

                            FileInfo fileInfo = new FileInfo (script);

                            this._process_status = FormStatus.script_running;

                            // Update the UI to reflect that a script is executing
                            this.UpdateUIProcessRunning ();

                            //this.selected_session.StartResultsGathering (this.selected_session.SessionId.ToString ("000") + '_' + file.Name.Replace ('.', '_') + "_res.xml");
                            this.selected_session.StartResultsGatheringWithExpandedFileNaming(fileInfo.Name);

                            // Execute the selected script. This needs to be
                            // done in a seperate thread; we don't want the
                            // entire application to stall.
                            this.selected_script = 
                                System.IO.Path.Combine(
                                (this.selected_session as Dvtk.Sessions.ScriptSession).DicomScriptRootDirectory,
                                script);

                            if ((fileInfo.Extension == ".ts") ||
                                (fileInfo.Extension == ".ds") ||
                                (fileInfo.Extension == ".dss"))
                            {
                                this.script_thread = null;
                                System.AsyncCallback cb = new AsyncCallback(this.OnScriptDone);
                                System.IAsyncResult ar =
                                ((Dvtk.Sessions.ScriptSession)this.selected_session).BeginExecuteScript(
                                    this.selected_script,
                                    false, 
                                    cb);
                            }
                            else if (fileInfo.Extension == ".js")
                            {
                                this.script_thread = new Thread (new ThreadStart (this.ExecuteJSScript));
                                this.script_thread.Start();
                            }
                            else if (fileInfo.Extension == ".vbs")
                            {
                                this.script_thread = new Thread (new ThreadStart (this.ExecuteVBSScript));
                                this.script_thread.Start();
                            }
                        }
                    }
                    else
                    {
                        this._active_page = ActivePage.session;
//                        this.last_selected_session = tree_node;
                        this.ButtonGeneralInformation.Checked = true;
                        this.UpdateSessionProperties ();
                        this.UpdatePageVisibility ();
                    }
                }
            }
        }

        public void FinishRunningProcessExecution (object sender, EventArgs e)
        {
            this.FinishRunningProcessCallback (null);
        }

        private void ExecuteHostScript (DvtkScriptHost script_host)
        {
            DvtkScriptSupport.CompilerErrorEventHandler     compiler_event_handler;

            // Create the event hanlder for handling the compiler error messages
            compiler_event_handler = new CompilerErrorEventHandler(script_host_CompilerErrorEvent);
            script_host.CompilerErrorEvent += compiler_event_handler;

            // Set the current active session in the script host
            script_host.Session = (Dvtk.Sessions.ScriptSession)this.selected_session;

            // Assign the script content to the script host.
            script_host.SourceCode = this.ScriptContent (this.selected_script);

            // Compile the script
            if (script_host.Compile ())
            {
                // Run the script
                script_host.Invoke ("DvtkScript", "Main", null);
            }

            // Notify the root process that the script has finished executing
            this.RunningProcessEvent (this, null);

            // Remove the event hanlder for handling the compiler error messages
            script_host.CompilerErrorEvent -= compiler_event_handler;
        }

        private void ExecuteJSScript ()
        {
            FileInfo    script_file = new FileInfo (this.selected_script);
            Random      random_value = new Random ();

            // Create a new script host in which the script will run
            DvtkScriptHost  script_host = 
                new DvtkScriptHost(
                /*language*/ DvtkScriptSupport.DvtkScriptLanguage.JScript,
                /*moniker*/  "dvtk://session/"+script_file.Name+random_value.Next().ToString(), 
                /*importedAssemblyDir*/ Application.StartupPath);

            this.ExecuteHostScript (script_host);
        }

        private void ExecuteVBSScript ()
        {
            FileInfo    script_file = new FileInfo (this.selected_script);
            Random      random_value = new Random ();

            // Create a new script host in which the script will run
            DvtkScriptHost  script_host = 
                new DvtkScriptHost (
                DvtkScriptSupport.DvtkScriptLanguage.VB,
                "dvtk://session/"+script_file.Name+random_value.Next().ToString(), 
                Application.StartupPath);

            this.ExecuteHostScript (script_host);
        }

        private string ScriptContent (string script_file)
        {
            System.IO.StreamReader  reader;
            string                  script_content;

            FileInfo fileInfo = new FileInfo (script_file);
            if (!fileInfo.Exists)
            {
                // given script file does not exist.
                return "";
            }

            // The script file exists, read and return the content.
            reader = fileInfo.OpenText ();
            script_content = reader.ReadToEnd ();
            reader.Close ();

            return script_content;
        }

        public void TerminateRunningProcess ()
        {
            // Close the socket. This will result in continuation of the script execution
            // thread, but an error will occur. The result is that the thread will terminate
            // nicely (close log file). The ExecuteDSScript() function will continue with
            // calling the ScriptFinishedEvent() as if nothing has happened.
            switch (this._process_status)
            {
                case FormStatus.print_scp_running:
                    //this.project.RemoveRunningEmulatorSession (this.selected_session.SessionFileName);
                    ((Dvtk.Sessions.EmulatorSession)this.selected_session).TerminateConnection ();
                    break;
                case FormStatus.script_running:
                    ((Dvtk.Sessions.ScriptSession)this.selected_session).TerminateConnection ();
                    break;
                case FormStatus.storage_scp_running:
                    //this.project.RemoveRunningEmulatorSession (this.selected_session.SessionFileName);
                    ((Dvtk.Sessions.EmulatorSession)this.selected_session).TerminateConnection ();
                    break;
                case FormStatus.no_process_running:
                default: // Fall through
                    break;
            }
        }

        private void FinishRunningProcessCallback (IAsyncResult ar)
        {
            if (this.InvokeRequired)
            {
                // We're not in the main thread and cannot make updates to
                // the UI. Call the delegate on the UI thread so we can
                // update controls.
                IAsyncResult arx = this.BeginInvoke (new AsyncCallback (FinishRunningProcessCallback), new object[] {ar});
                this.EndInvoke (arx);

                // The main thread has been notified. Now we must end this
                // function.
                return;
            }

            this.selected_session.EndResultsGathering ();

            this._process_status = FormStatus.no_process_running;

            this.ResizeWebView ();
            this.UpdateSessionTreeView ();

            // Select the results file in the session browser
            this.SelectAndDisplayGeneratedResults ();

            // Enable all relevant controls on the Project form.
            this.SessionBrowser.Enabled = true;
            this.ButtonGeneralInformation.Enabled = true;
            this.ButtonActivityLogging.Enabled = true;
            this.ButtonDetailedValidation.Enabled = true;

            // Switch to the detailed validation results.
            this.ButtonDetailedValidation.Checked = true;

            this.WebDescriptionView.Visible = true;

            this.PanelSOPClasses.Visible = false;
            this.VScrollBarSessionInfo.Visible = false;
            this.PanelSessionProperties.Visible = false;
            this.RichTextBoxInfo.Visible = false;
            this.RichTextBoxScript.Visible = false;
            this.RichTextBoxActivityLogging.Visible = false;

            if (this.ParentForm != null)
            {
                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
        }

        private void GetNodeProperties (TreeNode node,
            out string script,
            out string emulator,
            out string media_file,
            out string result)
        {

            string path = node.FullPath;
            int idx = path.IndexOf ("|");

            script = "";
            emulator = "";
            media_file = "";
            result = "";
			/*
            this.selected_description = "";
            this.selected_results = "";
            this.selected_script = "";

            if (idx < 0)
            {
                // The node selected, is the parent node - which in this case
                // is a session file.
                //this.selected_session = this.project.GetSession (path);
            }
            else
            {
                string node_path = path.Substring (0, path.IndexOf ("|"));

                //if (this.project.GetSession (node_path) == null)
                {
                    // Some weird selection has been made.
                    // Return without doing anything.
                    return;
                }
                //this.selected_session  = this.project.GetSession (node_path);

                if (this.selected_session is Dvtk.Sessions.ScriptSession)
                {
                    // In a script session, the child node of a session is a script.
                    // The child node of a script is a results file.
                    int idx2 = path.IndexOf ("|", idx + 1);
                    if (idx2 < 0)
                    {
                        // The node selected, is the script node.
                        script = path.Substring (idx+1, path.Length-idx-1);
                        if (script == UNCATEGORIZED_RESULTS)
                        {
                            // The actual item selected is not a script.
                            script = "";
                        }
                    }
                    else
                    {
                        // The node selected, is the results node.
                        result = path.Substring (idx2+1, path.Length-idx2-1);
                    }
                }
                if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                {
                    // In an emulator session, the child node of a session is an emulator.
                    // The child node of an emulator is a results file.
                    int idx2 = path.IndexOf ("|", idx + 1);
                    if (idx2 < 0)
                    {
                        // The node selected, is the script node.
                        emulator = path.Substring (idx+1, path.Length-idx-1);
                        if (emulator == UNCATEGORIZED_RESULTS)
                        {
                            // The actual item selected is not an emulator.
                            emulator = "";
                        }
                    }
                    else
                    {
                        // The node selected, is the results node.
                        result = path.Substring (idx2+1, path.Length-idx2-1);
                    }
                }
                if (this.selected_session is Dvtk.Sessions.MediaSession)
                {
                    // In a media session, the child node of a session is a media file.
                    // The child node of a media file is a results file.
                    int idx2 = path.IndexOf ("|", idx + 1);
                    if (idx2 < 0)
                    {
                        // The node selected, is the script node.
                        media_file = path.Substring (idx+1, path.Length-idx-1);
                        if (media_file == UNCATEGORIZED_RESULTS)
                        {
                            // The actual item selected is not a media file.
                            media_file = "";
                        }
                    }
                    else
                    {
                        // The node selected, is the results node.
                        result = path.Substring (idx2+1, path.Length-idx2-1);
                    }
                }
            }
			*/
        }

        private bool HasResults (TreeNode node)
        {
            string  emulator = "";
            string  media_file = "";
            string  script = "";
            string  results = "";

            this.GetNodeProperties (node, out script, out emulator, out media_file, out results);
            if (results != "")
                return true;
            if (script != "")
            {
                if (node.GetNodeCount(true) > 0)
                    return true;
            }
            else
            {
                // session node
                foreach (TreeNode script_node in node.Nodes)
                {
                    if (script_node.GetNodeCount(true) > 0)
                        return true;
                }
            }
            return false;
        }

        #endregion // SessionBrowser

        #region ViewButtons

        private void UpdatePageVisibility ()
        {
            if (this.ParentForm != null)
            {
                // Update the controls of the mainform (toolbar, menubar, title bar)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }

            if (((this._active_page == ActivePage.session)             && (this.PanelSessionProperties.Visible == true))     ||
                ((this._active_page == ActivePage.script)              && (this.RichTextBoxScript.Visible == true))          ||
                ((this._active_page == ActivePage.description)         && (this.WebDescriptionView.Visible == true))         ||
                ((this._active_page == ActivePage.activity_reporting)  && (this.RichTextBoxActivityLogging.Visible == true)) ||
                ((this._active_page == ActivePage.detailed_validation) && (this.WebDescriptionView.Visible == true))         ||
                ((this._active_page == ActivePage.sop_classes_view)    && (this.PanelSOPClasses.Visible == true))            ||
                ((this._active_page == ActivePage.validation_results)  && (this.WebDescriptionView.Visible == true)))
            {
                // We're already on the correct page with the correct content.
                return;
            }

            this.PositionPageViewButtons ();

            switch (this._active_page)
            {
                case ActivePage.session:
                    this.ButtonGeneralInformation.Checked = true;
                    this.VScrollBarSessionInfo.Visible = true;
                    this.PanelSessionProperties.Visible = true;
                    this.RichTextBoxInfo.Visible = this.ShowRichTextBoxInfo;

                    this.PanelSOPClasses.Visible = false;
                    this.WebDescriptionView.Visible = false;
                    this.RichTextBoxScript.Visible = false;
                    this.RichTextBoxActivityLogging.Visible = false;

                    this.ResizeSessionPropertiesView ();
                    break;
                case ActivePage.script:
                    this.ButtonGeneralInformation.Checked = true;
                    this.RichTextBoxScript.Visible = true;

                    this.WebDescriptionView.Visible = false;
                    this.PanelSOPClasses.Visible = false;
                    this.VScrollBarSessionInfo.Visible = false;
                    this.PanelSessionProperties.Visible = false;
                    this.RichTextBoxInfo.Visible = false;
                    this.RichTextBoxActivityLogging.Visible = false;

                    this.ResizeScriptView ();
                    break;
                case ActivePage.detailed_validation:    // fall through
                case ActivePage.validation_results:
                    this.ButtonDetailedValidation.Checked = true;
                    this.WebDescriptionView.Visible = true;

                    this.RichTextBoxScript.Visible = false;
                    this.PanelSOPClasses.Visible = false;
                    this.VScrollBarSessionInfo.Visible = false;
                    this.PanelSessionProperties.Visible = false;
                    this.RichTextBoxInfo.Visible = false;
                    this.RichTextBoxActivityLogging.Visible = false;

                    this.ResizeWebView ();
                    break;
                case ActivePage.description:
                    this.WebDescriptionView.Visible = true;

                    this.RichTextBoxScript.Visible = false;
                    this.PanelSOPClasses.Visible = false;
                    this.VScrollBarSessionInfo.Visible = false;
                    this.PanelSessionProperties.Visible = false;
                    this.RichTextBoxInfo.Visible = false;
                    this.RichTextBoxActivityLogging.Visible = false;

                    this.ResizeWebView ();
                    break;
                case ActivePage.activity_reporting:
                    this.ButtonActivityLogging.Checked = true;
                    this.RichTextBoxActivityLogging.Visible = true;

                    this.RichTextBoxScript.Visible = false;
                    this.PanelSOPClasses.Visible = false;
                    this.VScrollBarSessionInfo.Visible = false;
                    this.PanelSessionProperties.Visible = false;
                    this.RichTextBoxInfo.Visible = false;
                    this.WebDescriptionView.Visible = false;

                    this.ResizeActivityLoggingView ();
                    break;
                case ActivePage.sop_classes_view:
                    this.PanelSOPClasses.Visible = true;
                    this.RichTextBoxInfo.Visible = this.ShowRichTextBoxInfo;

                    this.RichTextBoxScript.Visible = false;
                    this.VScrollBarSessionInfo.Visible = false;
                    this.PanelSessionProperties.Visible = false;
                    this.WebDescriptionView.Visible = false;
                    this.RichTextBoxActivityLogging.Visible = false;

                    this.ResizeSOPClassesView ();
                    break;
                default:
                    break;
            }
        }

        private void ButtonGeneralInformation_CheckedChanged(object sender, System.EventArgs e)
        {
            if (ButtonGeneralInformation.Checked == false)
                return;

            this.ClearNavigationHistory ();

            if (this.selected_description != "")
            {
                this._active_page = ActivePage.description;
                this.SessionBrowser.SelectedNode = this.last_selected_script;
            }
            else if (this.selected_script != "")
            {
                this._active_page = ActivePage.script;
                this.SessionBrowser.SelectedNode = this.last_selected_script;
            }
            else
            {
                this._active_page = ActivePage.session;
                this.SessionBrowser.SelectedNode = this.last_selected_session;
            }
            this.UpdatePageVisibility ();
        }

        private void ButtonGeneralInformation_Click(object sender, System.EventArgs e)
        {
            if ((this.selected_description != "") || (this.selected_script != ""))
            {
                if (this.last_selected_script != null)
                    this.SessionBrowser.SelectedNode = this.last_selected_script;
            }
            else
            {
                if (this.last_selected_session != null)
                    this.SessionBrowser.SelectedNode = this.last_selected_session;
            }
        }

        private void ButtonActivityLogging_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ButtonActivityLogging.Checked == false)
                return;

            this.ClearNavigationHistory ();

            this._active_page = ActivePage.activity_reporting;

            this.UpdatePageVisibility ();
        }

        private void ButtonDetailedValidation_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ButtonDetailedValidation.Checked == false)
                return;

            this.ClearNavigationHistory ();

            this._active_page = ActivePage.detailed_validation;

            this.UpdatePageVisibility ();
        }

        private void ButtonDetailedValidation_Click(object sender, System.EventArgs e)
        {
            if (this.last_selected_result != null)
                this.SessionBrowser.SelectedNode = this.last_selected_result;
        }
        #endregion

        #region StorageSCPEmulator

        // Callback method must have the same signature as the
        // AsyncCallback delegate.
        private void OnEmulatorScpDone(IAsyncResult ar)
        {
            ((Dvtk.Sessions.EmulatorSession)this.selected_session).EndEmulateSCP(ar);
            // Notify the root process that the emulator has finished executing
            this.RunningProcessEvent (this, null);
        }

        public void StartStorageSCPEmulator ()
        {
            // We know that the emulator session is already selected. Now we need
            // to make sure that the Storage SCP Emulator item is selected.
            foreach (TreeNode node in this.SessionBrowser.SelectedNode.Nodes)
            {
                if (node.Text == "Storage SCP Emulator")
                    this.SessionBrowser.SelectedNode = node;
            }
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
            {
                this._process_status = FormStatus.storage_scp_running;
                Dvtk.Sessions.EmulatorSession emulatorSession =
                    this.selected_session as Dvtk.Sessions.EmulatorSession;
                //
                // Set proper emulator type before starting.
                // This type is reflected in header of results.
                //
                emulatorSession.ScpEmulatorType = DvtkData.Results.ScpEmulatorType.Storage;
                emulatorSession.ScuEmulatorType = DvtkData.Results.ScuEmulatorType.Unknown;
                //this.project.AddRunningEmulatorSession(emulatorSession.SessionFileName);
                this.UpdateUIProcessRunning();
                string fileName = string.Empty;
//                    emulatorSession.SessionId.ToString ("000") + "_st_scp_em_res.xml";
                /*
                    string.Format(
                    "{0:000}{1}",
                    emulatorSession.SessionId,
                    "_st_scp_em_res.xml");
                    */
                this.selected_session.StartResultsGatheringWithExpandedFileNaming(fileName);
                //
                // Update the UI to reflect that an emulator is executing
                //
                this.UpdateUIProcessRunning ();
                System.AsyncCallback cb = new AsyncCallback(this.OnEmulatorScpDone);
                System.IAsyncResult ar = emulatorSession.BeginEmulateSCP(cb);
            }
        }
        #endregion

        #region PrintSCPEmulator
        public void StartPrintSCPEmulator ()
        {
            // We know that the emulator session is already selected. Now we need
            // to make sure that the Storage SCP Emulator item is selected.
            foreach (TreeNode node in this.SessionBrowser.SelectedNode.Nodes)
            {
                if (node.Text == "Print SCP Emulator")
                    this.SessionBrowser.SelectedNode = node;
            }
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
            {
                this._process_status = FormStatus.print_scp_running;
                Dvtk.Sessions.EmulatorSession emulatorSession =
                    this.selected_session as Dvtk.Sessions.EmulatorSession;
                //
                // Set proper emulator type before starting.
                // This type is reflected in header of results.
                //
                emulatorSession.ScpEmulatorType = DvtkData.Results.ScpEmulatorType.Printing;
                emulatorSession.ScuEmulatorType = DvtkData.Results.ScuEmulatorType.Unknown;
                //this.project.AddRunningEmulatorSession(emulatorSession.SessionFileName);
                this.UpdateUIProcessRunning();
                string fileName = string.Empty;
//                    this.selected_session.SessionId.ToString ("000") + "_pr_scp_em_res.xml";
                /*
                    string.Format(
                    "{0:000}{1}",
                    emulatorSession.SessionId,
                    "_pr_scp_em_res.xml");
                    */
                this.selected_session.StartResultsGatheringWithExpandedFileNaming(fileName);
                //
                // Update the UI to reflect that an emulator is executing
                //
                this.UpdateUIProcessRunning ();
                System.AsyncCallback cb = new AsyncCallback(this.OnEmulatorScpDone);
                System.IAsyncResult ar = emulatorSession.BeginEmulateSCP(cb);
            }
        }
        #endregion

        // Create a new Mutex. The creating thread does not own the
        // Mutex.
        private static System.Threading.Mutex mut = new System.Threading.Mutex();
        private void _RichTextBoxActivityLoggingAppendText(string text)
        {
            // Wait until it is safe to enter.
            mut.WaitOne();
            this.RichTextBoxActivityLogging.AppendText(text);
            // Release the Mutex.
            mut.ReleaseMutex();
        }

        private void OnActivityReportEvent(object sender, Dvtk.Events.ActivityReportEventArgs e)
        {
            this._RichTextBoxActivityLoggingAppendText (e.Message + '\n');
            this.RichTextBoxActivityLogging.ScrollToCaret();
        }

        private void CallBackMessageDisplay(string message)
        {
            this.ButtonActivityLogging.Checked = true;
            this._RichTextBoxActivityLoggingAppendText (message + '\n');
            this.RichTextBoxActivityLogging.ScrollToCaret();
        }

        private void UpdateUIProcessRunning ()
        {
            // Clear the content of the activity logging page.
            this.RichTextBoxActivityLogging.Clear();

            // Switch to the activity logging page.
            this.ButtonActivityLogging.Checked = true;

            // Disable all relevant controls on the Project form.
            this.SessionBrowser.Enabled = false;
            this.ButtonGeneralInformation.Enabled = false;
            this.ButtonActivityLogging.Enabled = false;
            this.ButtonDetailedValidation.Enabled = false;

            // Update the controls of the mainform (toolbar, menubar, title bar)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        private void TaskPanelPaint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            System.Windows.Forms.Panel panel_ctrl = (System.Windows.Forms.Panel) sender;
            Graphics panel = e.Graphics;

            Pen pen = new Pen (Color.FromArgb (255, 255, 255));
            panel.DrawLine (pen, 0, 0, 0, panel_ctrl.Size.Height);
            panel.DrawLine (pen, panel_ctrl.Size.Width-1, panel_ctrl.Size.Height-1, 0, panel_ctrl.Size.Height-1);
            panel.DrawLine (pen, panel_ctrl.Size.Width-1, 0, panel_ctrl.Size.Width-1, panel_ctrl.Size.Height);
        }

        public void NavigateBack ()
        {
            this.WebDescriptionView.GoBack ();

            // Update the controls of the mainform (toolbar, menubar, title bar)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        public void NavigateForward ()
        {
            this.WebDescriptionView.GoForward ();

            // Update the controls of the mainform (toolbar, menubar, title bar)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        private void ClearNavigationHistory ()
        {
            // Clear navigation history
            this.urlsVisited.Clear ();
            this.currentUrlIndex = 0;
        }

        private void WebDescriptionView_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
        {
            Cursor.Current = Cursors.WaitCursor;
            if (e.uRL.ToString().EndsWith (".xml"))
            {
                e.cancel = true;
                this.selected_results = e.uRL.ToString ();
                this.UpdateDetailedResultsView ();
            }
        }

        private void WebDescriptionView_ProgressChange(object sender, AxSHDocVw.DWebBrowserEvents2_ProgressChangeEvent e)
        {
            if (e.progress < e.progressMax)
                Cursor.Current = Cursors.WaitCursor;
            else
                Cursor.Current = Cursors.Default;
        }

        private void WebDescriptionView_DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
        {
            Cursor.Current = Cursors.Default;
            /*
            // update the URL displayed in the address bar
            String s = e.uRL.ToString();

            // update the list of visited URLs
            int i = urlsVisited.IndexOf(s);
            if (i >= 0)
                currentUrlIndex = i;
            else 
                currentUrlIndex = urlsVisited.Add(s);
            */

            // get access to the HTMLDocument
            this.document = (HtmlDocument)this.WebDescriptionView.Document;
            this.document_body = (HtmlBody)document.body;

            // at this point the document and body has been loaded
            // so define the event handler as the same class
            ((mshtml.DispHTMLDocument)document).oncontextmenu = this;
            ((mshtml.DispHTMLDocument)document).onkeypress = this;
            ((mshtml.DispHTMLDocument)document).onkeydown = this;
            ((mshtml.DispHTMLDocument)document).onkeyup = this;

            /*
            // Update the context sensitive menu for the browser
            this.CM_Back.Enabled = this.can_navigate_back;
            this.CM_Forward.Enabled = this.can_navigate_forward;
            */
            //this.CM_FindAgain.Enabled = (((MainForm)this.ParentForm).search_string != "");

            // the search options value '6' maps to: 'match case' & 'match whole words'.
            this.FindReset();
            this._contains_errors = this.find_range.findText ("Error:", this.find_range.text.Length, 6);

            this.FindReset();
            this._contains_warnings = this.find_range.findText ("Warning:", this.find_range.text.Length, 6);

            this.FindReset();

            // Update the controls of the mainform (toolbar, menubar, title bar)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #region TaskPanel
        private bool task_panel_visible = false;

        public bool ShowTaskPanel
        {
            get { return this.task_panel_visible; }
            set
            {
                this.task_panel_visible = value;
                this.TaskPanel.Visible = this.task_panel_visible;
            }
        }
        #endregion

        #region RichTextBoxInfo
        private bool show_rich_text_box_info = true;

        public bool ShowRichTextBoxInfo
        {
            get { return this.show_rich_text_box_info; }
            set
            {
                this.show_rich_text_box_info = value;
                this.RichTextBoxInfo.Visible = this.show_rich_text_box_info;
            }
        }
        #endregion

        #region DetailedValidationBrowsing

        // find and replace internal text range
        private HtmlTextRange   find_range;

        // document and body elements
        private HtmlDocument    document;
        private HtmlBody        document_body;

        // COM Event Handler for HTML Element Events
        [DispId(0)]
        public void DefaultMethod()
        {
            // obtain the event object and ensure a context menu has been applied to the document
            HtmlEventObject event_object = document.parentWindow.@event;
            string event_type = event_object.type;

            if (event_type == "contextmenu")
            {
                // Call the custom Web Browser HTML event 
                ContextMenuShow(this, event_object);
            }
            if (event_type == "keydown")
            {
                if ((event_object.keyCode == System.Windows.Forms.Keys.F.GetHashCode()) && (event_object.ctrlKey == true))
                {
                    ((MainForm)this.ParentForm).ActionFind ();

                    // Clean any keyCode and thus event handling of this key.
                    event_object.returnValue = false;
                    event_object.cancelBubble = true;
                    event_object.keyCode = 0;
                }
                if (event_object.keyCode == System.Windows.Forms.Keys.F3.GetHashCode())
                {
                    ((MainForm)this.ParentForm).ActionFindAgain ();

                    // Clean any keyCode and thus event handling of this key.
                    event_object.returnValue = false;
                    event_object.cancelBubble = true;
                    event_object.keyCode = 0;
                }
            }
        }

        // method to perform the process of showing the context menus
        private void ContextMenuShow(object sender, HtmlEventObject e)
        {
            System.Drawing.Point loc = new Point(e.x + this.WebDescriptionView.Location.X, e.y + this.WebDescriptionView.Location.Y);
            this.ContextMenuDetailedValidation.Show (this, loc);

            // cancel the standard menu and event bubbling
            e.returnValue = false;
            e.cancelBubble = true;
        } //ContextMenuShow

        public bool FindNext(string find_text, bool match_whole, bool match_case)
        {
            if (FindText (find_text, match_whole, match_case) != null)
                return true;
            else
                return false;
        }

        // reset the find and replace options to initialize a new search
        public void FindReset()
        {
            // reset the range being worked with
            this.find_range = (HtmlTextRange)this.document_body.createTextRange();
            ((HtmlSelection)this.document.selection).empty();
        }

        // function to perform the actual find of the given text
        private HtmlTextRange FindText(string find_text, bool match_whole, bool match_case)
        {
            // define the search options
            int search_options = 0;
            if (match_whole)
                search_options += 2;

            if (match_case)
                search_options += 4;

            // Sanity checks
            if ((this.find_range == null) ||
                (this.find_range.text == null))
            {
                // reset the find ranges
                this.FindReset();
                // no text found so return null range
                return null;
            }

            // perform the search operation
            if (this.find_range.findText(find_text, this.find_range.text.Length, search_options))
            {
                // select the found text within the document
                this.find_range.select();
                // limit the new find range to be from the newly found text
                HtmlTextRange found_range = (HtmlTextRange)this.document.selection.createRange();
                this.find_range = (HtmlTextRange)this.document_body.createTextRange();
                this.find_range.setEndPoint("StartToEnd", found_range);
                // text found so return this selection
                return found_range;
            }
            else
            {
                // reset the find ranges
                this.FindReset();
                // no text found so return null range
                return null;
            }

        } //FindText 

        #endregion

        private void script_host_CompilerErrorEvent(object sender, CompilerErrorEventArgs e)
        {
            this._RichTextBoxActivityLoggingAppendText (e.LineText);
            this._RichTextBoxActivityLoggingAppendText (
                string.Format("Error on line {0}:\t{1}\n", e.Line, e.Description));
            this.RichTextBoxActivityLogging.ScrollToCaret();
        }

        #region GeneralSessionProperties

        #region MinMaxGSPSettings
        private void MinGSPSettings_Click(object sender, System.EventArgs e)
        {
            this.show_general_session_properties = false;
            this.ResizeSessionPropertiesView ();
        }

        private void MaxGSPSettings_Click(object sender, System.EventArgs e)
        {
            this.show_general_session_properties = true;
            this.ResizeSessionPropertiesView ();
        }
        #endregion

        #region ComboBoxSessionType

        private void ComboBoxSessionType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if ((this.ComboBoxSessionType.SelectedItem.ToString() == "Script") &&
                !(this.selected_session is Dvtk.Sessions.ScriptSession))
            {
                // We only want to replace the selected session when the user
                // has changed the session type.
                Dvtk.Sessions.ScriptSession    session;
                session = new Dvtk.Sessions.ScriptSession();

                session.SessionFileName = this.selected_session.SessionFileName;

                session.SessionTitle = this.TextBoxSessionTitle.Text;
                session.SessionId = Convert.ToUInt16 (this.NumericSessonID.Value);
                session.TestedBy = this.TextBoxTestedBy.Text;
                session.Date = this.DateTested.Value;
                if (this.TextBoxResultsRoot.Text == "")
                    session.ResultsRootDirectory = ".";
                else
                    session.ResultsRootDirectory = this.TextBoxResultsRoot.Text;

                if (this.TextBoxScriptRoot.Text == "")
                    session.DicomScriptRootDirectory = ".";
                else
                    session.DicomScriptRootDirectory = this.TextBoxScriptRoot.Text;

                session.DvtSystemSettings.AeTitle = this.TextBoxDVTAETitle.Text;
                session.DvtSystemSettings.Port = (ushort)this.NumericDVTListenPort.Value;
                session.DvtSystemSettings.SocketTimeout = (ushort)this.NumericDVTTimeOut.Value;
                session.DvtSystemSettings.MaximumLengthReceived = (uint)this.NumericDVTMaxPDU.Value;

                session.SutSystemSettings.AeTitle = this.TextBoxSUTAETitle.Text;
                session.SutSystemSettings.Port = (ushort)this.NumericSUTListenPort.Value;
                session.SutSystemSettings.HostName = this.TextBoxSUTTCPIPAddress.Text;
                session.SutSystemSettings.MaximumLengthReceived = (uint)this.NumericSUTMaxPDU.Value;

                session.SecuritySettings.SecureSocketsEnabled = this.CheckBoxSecureConnection.Checked;
                if (this.CheckBoxSecureConnection.Checked)
                {
                    session.SecuritySettings.CacheTlsSessions = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CacheTlsSessions;
                    session.SecuritySettings.CertificateFileName = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CertificateFileName;
                    session.SecuritySettings.CheckRemoteCertificate = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CheckRemoteCertificate;
                    session.SecuritySettings.CipherFlags = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CipherFlags;
                    session.SecuritySettings.CredentialsFileName = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CredentialsFileName;
                    session.SecuritySettings.TlsCacheTimeout = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.TlsCacheTimeout;
                    session.SecuritySettings.TlsPassword = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.TlsPassword;
                    session.SecuritySettings.TlsVersionFlags = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.TlsVersionFlags;
                }

                // Copy the definition settings (SOP Classes, AE title/version and definition dirs)
                session.DefinitionManagement.ApplicationEntityName = this.selected_session.DefinitionManagement.ApplicationEntityName;
                session.DefinitionManagement.ApplicationEntityVersion = this.selected_session.DefinitionManagement.ApplicationEntityVersion;
                session.DefinitionManagement.DefinitionFileDirectoryList.Clear ();
                foreach (string def_dir in this.selected_session.DefinitionManagement.DefinitionFileDirectoryList)
                    session.DefinitionManagement.DefinitionFileDirectoryList.Add (def_dir);
                session.DefinitionManagement.DefinitionFileRootDirectory = this.selected_session.DefinitionManagement.DefinitionFileRootDirectory;
                foreach (string def_file in this.selected_session.DefinitionManagement.LoadedDefinitionFileNames)
                    session.DefinitionManagement.LoadDefinitionFile (def_file);
                this.selected_session.DefinitionManagement.UnLoadDefinitionFiles ();

                //this.project.ReplaceSession (this.selected_session, session, this._activity_handler);
                foreach (TreeNode node in this.SessionBrowser.Nodes)
                {
                    // Clear the tree node representing the old session.
                    if (node.Text == session.SessionFileName)
                        node.Nodes.Clear ();
                }

                this.selected_session = session;
                this.UpdateSessionProperties ();
                ////////this.project.SetSessionChanged (session.SessionFileName, true);
                this.ResizeSessionPropertiesView ();
            }
            if ((this.ComboBoxSessionType.SelectedItem.ToString() == "Media") &&
                !(this.selected_session is Dvtk.Sessions.MediaSession))
            {
                // We only want to replace the selected session when the user
                // has changed the session type.
                Dvtk.Sessions.MediaSession  session;
                session = new Dvtk.Sessions.MediaSession ();

                session.SessionFileName = this.selected_session.SessionFileName;

                session.SessionTitle = this.TextBoxSessionTitle.Text;
                session.SessionId = Convert.ToUInt16 (this.NumericSessonID.Value);
                session.TestedBy = this.TextBoxTestedBy.Text;
                session.Date = this.DateTested.Value;
                if (this.TextBoxResultsRoot.Text == "")
                    session.ResultsRootDirectory = ".";
                else
                    session.ResultsRootDirectory = this.TextBoxResultsRoot.Text;

                // Copy the definition settings (SOP Classes, AE title/version and definition dirs)
                session.DefinitionManagement.ApplicationEntityName = this.selected_session.DefinitionManagement.ApplicationEntityName;
                session.DefinitionManagement.ApplicationEntityVersion = this.selected_session.DefinitionManagement.ApplicationEntityVersion;
                session.DefinitionManagement.DefinitionFileDirectoryList.Clear ();
                foreach (string def_dir in this.selected_session.DefinitionManagement.DefinitionFileDirectoryList)
                    session.DefinitionManagement.DefinitionFileDirectoryList.Add (def_dir);
                session.DefinitionManagement.DefinitionFileRootDirectory = this.selected_session.DefinitionManagement.DefinitionFileRootDirectory;
                foreach (string def_file in this.selected_session.DefinitionManagement.LoadedDefinitionFileNames)
                    session.DefinitionManagement.LoadDefinitionFile (def_file);
                this.selected_session.DefinitionManagement.UnLoadDefinitionFiles ();

                //this.project.ReplaceSession (this.selected_session, session, this._activity_handler);
                foreach (TreeNode node in this.SessionBrowser.Nodes)
                {
                    // Clear the tree node representing the old session.
                    if (node.Text == session.SessionFileName)
                        node.Nodes.Clear ();
                }

                this.selected_session = session;
                this.UpdateSessionProperties ();
                //////this.project.SetSessionChanged (session.SessionFileName, true);
                this.ResizeSessionPropertiesView ();
            }
            if ((this.ComboBoxSessionType.SelectedItem.ToString() == "Emulator") &&
                !(this.selected_session is Dvtk.Sessions.EmulatorSession))
            {
                // We only want to replace the selected session when the user
                // has changed the session type.
                Dvtk.Sessions.EmulatorSession   session;
                session = new Dvtk.Sessions.EmulatorSession ();

                session.SessionFileName = this.selected_session.SessionFileName;

                session.SessionTitle = this.TextBoxSessionTitle.Text;
                session.SessionId = Convert.ToUInt16 (this.NumericSessonID.Value);
                session.TestedBy = this.TextBoxTestedBy.Text;
                session.Date = this.DateTested.Value;
                if (this.TextBoxResultsRoot.Text == "")
                    session.ResultsRootDirectory = ".";
                else
                    session.ResultsRootDirectory = this.TextBoxResultsRoot.Text;

                session.DvtSystemSettings.AeTitle = this.TextBoxDVTAETitle.Text;
                session.DvtSystemSettings.Port = (ushort)this.NumericDVTListenPort.Value;
                session.DvtSystemSettings.SocketTimeout = (ushort)this.NumericDVTTimeOut.Value;
                session.DvtSystemSettings.MaximumLengthReceived = (uint)this.NumericDVTMaxPDU.Value;

                session.SutSystemSettings.AeTitle = this.TextBoxSUTAETitle.Text;
                session.SutSystemSettings.Port = (ushort)this.NumericSUTListenPort.Value;
                session.SutSystemSettings.HostName = this.TextBoxSUTTCPIPAddress.Text;
                session.SutSystemSettings.MaximumLengthReceived = (uint)this.NumericSUTMaxPDU.Value;

                session.SecuritySettings.SecureSocketsEnabled = this.CheckBoxSecureConnection.Checked;
                if (this.CheckBoxSecureConnection.Checked)
                {
                    session.SecuritySettings.CacheTlsSessions = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CacheTlsSessions;
                    session.SecuritySettings.CertificateFileName = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CertificateFileName;
                    session.SecuritySettings.CheckRemoteCertificate = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CheckRemoteCertificate;
                    session.SecuritySettings.CipherFlags = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CipherFlags;
                    session.SecuritySettings.CredentialsFileName = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.CredentialsFileName;
                    session.SecuritySettings.TlsCacheTimeout = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.TlsCacheTimeout;
                    session.SecuritySettings.TlsPassword = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.TlsPassword;
                    session.SecuritySettings.TlsVersionFlags = ((Dvtk.Sessions.ISecure)this.selected_session).SecuritySettings.TlsVersionFlags;
                }

                // Copy the definition settings (SOP Classes, AE title/version and definition dirs)
                session.DefinitionManagement.ApplicationEntityName = this.selected_session.DefinitionManagement.ApplicationEntityName;
                session.DefinitionManagement.ApplicationEntityVersion = this.selected_session.DefinitionManagement.ApplicationEntityVersion;
                session.DefinitionManagement.DefinitionFileDirectoryList.Clear ();
                foreach (string def_dir in this.selected_session.DefinitionManagement.DefinitionFileDirectoryList)
                    session.DefinitionManagement.DefinitionFileDirectoryList.Add (def_dir);
                session.DefinitionManagement.DefinitionFileRootDirectory = this.selected_session.DefinitionManagement.DefinitionFileRootDirectory;
                foreach (string def_file in this.selected_session.DefinitionManagement.LoadedDefinitionFileNames)
                    session.DefinitionManagement.LoadDefinitionFile (def_file);
                this.selected_session.DefinitionManagement.UnLoadDefinitionFiles ();

                //this.project.ReplaceSession (this.selected_session, session, this._activity_handler);
                foreach (TreeNode node in this.SessionBrowser.Nodes)
                {
                    // Clear the tree node representing the old session.
                    if (node.Text == session.SessionFileName)
                        node.Nodes.Clear ();
                }

                this.selected_session = session;
                this.UpdateSessionProperties ();
                //////this.project.SetSessionChanged (session.SessionFileName, true);
                this.ResizeSessionPropertiesView ();
            }

            // Update the session tree browser
            this.UpdateSessionTreeView ();

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region TextBoxSessionTitle

        private void TextBoxSessionTitle_TextChanged(object sender, System.EventArgs e)
        {
            ////this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            this.selected_session.SessionTitle = this.TextBoxSessionTitle.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region NumericSessionID

        private void NumericSessonID_Validated(object sender, System.EventArgs e)
        {
            if (this.selected_session.SessionId != Convert.ToUInt16 (this.NumericSessonID.Value))
            {
                // We update the UI to reflect that a setting has been changed.
                // The actual session properties are updated when the control has lost focus
                //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                // This function clips the value in the Session ID numeric input control.
                // The minimum value is 0, the maximum is 999.
                if (this.NumericSessonID.Value < 0)
                    this.NumericSessonID.Value = 0;
                if (this.NumericSessonID.Value > 999)
                    this.NumericSessonID.Value = 999;

                this.selected_session.SessionId = Convert.ToUInt16 (this.NumericSessonID.Value);

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
        }

        #endregion

        #region TextBoxTestedBy

        private void TextBoxTestedBy_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            this.selected_session.TestedBy = this.TextBoxTestedBy.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region DateTested

        private void DateTested_ValueChanged(object sender, System.EventArgs e)
        {
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            this.selected_session.Date = this.DateTested.Value;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region ButtonBrowseResultsDir

        private void ButtonBrowseResultsDir_Click(object sender, System.EventArgs e)
        {
            this.DialogBrowseFolder.Description = "Select the root directory where the result files should be stored.";
            if (this.TextBoxResultsRoot.Text != "")
                this.DialogBrowseFolder.SelectedPath = this.TextBoxResultsRoot.Text;

            if (this.DialogBrowseFolder.ShowDialog (this) == DialogResult.OK)
            {
                this.TextBoxResultsRoot.Text = this.DialogBrowseFolder.SelectedPath;
                if (this.selected_session.ResultsRootDirectory != this.DialogBrowseFolder.SelectedPath)
                {
                    this.selected_session.ResultsRootDirectory = this.DialogBrowseFolder.SelectedPath;
                    //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                    // Update the session tree browser
                    this.UpdateSessionTreeView ();

                    // Update the mainform controls (menu, toolbar, title)
                    ((MainForm)this.ParentForm).UpdateUIControls ();
                }
            }
        }
 
        #endregion

        #region ButtonBrowseScriptsDir

        private void ButtonBrowseScriptsDir_Click(object sender, System.EventArgs e)
        {
            this.DialogBrowseFolder.Description = "Select the root directory containing the script files.";
            if (this.TextBoxScriptRoot.Text != "")
                this.DialogBrowseFolder.SelectedPath = this.TextBoxScriptRoot.Text;

            if (this.DialogBrowseFolder.ShowDialog (this) == DialogResult.OK)
            {
                Dvtk.Sessions.ScriptSession session = (Dvtk.Sessions.ScriptSession) this.selected_session;
                this.TextBoxScriptRoot.Text = this.DialogBrowseFolder.SelectedPath;
                if (session.DicomScriptRootDirectory != this.DialogBrowseFolder.SelectedPath)
                {
                    session.DicomScriptRootDirectory = this.DialogBrowseFolder.SelectedPath;
                    //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                    // Update the session tree browser
                    this.UpdateSessionTreeView ();

                    // Update the mainform controls (menu, toolbar, title)
                    ((MainForm)this.ParentForm).UpdateUIControls ();
                }
            }
        }

        #endregion

        #region ButtonBrowseDescriptionDir

        private void ButtonBrowseDescriptionDir_Click(object sender, System.EventArgs e)
        {
            this.DialogBrowseFolder.Description = "Select the root directory containing the description (html) files.";
            if (this.TextBoxDescriptionRoot.Text != "")
                this.DialogBrowseFolder.SelectedPath = this.TextBoxDescriptionRoot.Text;

            if (this.DialogBrowseFolder.ShowDialog (this) == DialogResult.OK)
            {
                Dvtk.Sessions.ScriptSession session = (Dvtk.Sessions.ScriptSession) this.selected_session;
                this.TextBoxDescriptionRoot.Text = this.DialogBrowseFolder.SelectedPath;
                if (session.DescriptionDirectory != this.DialogBrowseFolder.SelectedPath)
                {
                    session.DescriptionDirectory = this.DialogBrowseFolder.SelectedPath;
                    //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                    // Update the session tree browser
                    this.UpdateSessionTreeView ();

                    // Update the mainform controls (menu, toolbar, title)
                    ((MainForm)this.ParentForm).UpdateUIControls ();
                }
            }
        }

        #endregion

        #region ButtonSpecifySOPClasses

        private void ButtonSpecifySOPCLasses_Click(object sender, System.EventArgs e)
        {
            this._active_page = ActivePage.sop_classes_view;

            // Create and fill in the datagrid containing all information on available
            // and selected SOP classes
            this.UpdateSOPClassesView ();

            this.UpdatePageVisibility ();
        }

        #endregion

        #region ButtonSpecifyTransferSyntaxes
        private void ButtonSpecifyTransferSyntaxes_Click(object sender, System.EventArgs e)
        {
            SelectTransferSyntaxesForm  transfer_syntax = new SelectTransferSyntaxesForm((Dvtk.Sessions.EmulatorSession)this.selected_session);
            transfer_syntax.ShowDialog (this);
        }
        #endregion

        #endregion

        #region DVTSettings

        #region MinMaxDVTSettings
        private void MinDVTRoleSettings_Click(object sender, System.EventArgs e)
        {
            this.show_dvt_role_settings = false;
            this.ResizeSessionPropertiesView ();
        }

        private void MaxDVTRoleSettings_Click(object sender, System.EventArgs e)
        {
            this.show_dvt_role_settings = true;
            this.ResizeSessionPropertiesView ();
        }
        #endregion

        #region TextBoxDVTImplClassUID

        private void TextBoxDVTImplClassUID_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).DvtSystemSettings.ImplementationClassUid = this.TextBoxDVTImplClassUID.Text;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).DvtSystemSettings.ImplementationClassUid = this.TextBoxDVTImplClassUID.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region TextBoxDVTImplVersionName

        private void TextBoxDVTImplVersionName_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).DvtSystemSettings.ImplementationVersionName = this.TextBoxDVTImplVersionName.Text;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).DvtSystemSettings.ImplementationVersionName = this.TextBoxDVTImplVersionName.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region TextBoxDVTAETitle

        private void TextBoxDVTAETitle_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).DvtSystemSettings.AeTitle = this.TextBoxDVTAETitle.Text;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).DvtSystemSettings.AeTitle = this.TextBoxDVTAETitle.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region NumericDVTListenPort

        private void NumericDVTListenPort_Validated(object sender, System.EventArgs e)
        {
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
            {
                ushort portNumber = (ushort)this.NumericDVTListenPort.Value;
                ((Dvtk.Sessions.ScriptSession)this.selected_session).DvtSystemSettings.Port = portNumber;
            }
            else if (this.selected_session is Dvtk.Sessions.EmulatorSession)
            {
                ushort portNumber = (ushort)this.NumericDVTListenPort.Value;
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).DvtSystemSettings.Port = portNumber;
            }

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region DVTTimeOut

        private void NumericDVTTimeOut_Validated(object sender, System.EventArgs e)
        {
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).DvtSystemSettings.SocketTimeout = (ushort)this.NumericDVTTimeOut.Value;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).DvtSystemSettings.SocketTimeout = (ushort)this.NumericDVTTimeOut.Value;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region NumericDVTMaxPDU

        private void NumericDVTMaxPDU_Validated(object sender, System.EventArgs e)
        {
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).DvtSystemSettings.MaximumLengthReceived = (uint)this.NumericDVTMaxPDU.Value;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).DvtSystemSettings.MaximumLengthReceived = (uint)this.NumericDVTMaxPDU.Value;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #endregion

        #region SystemUnderTestSettings

        #region MinMaxSUTSettings
        private void MinSUTSettings_Click(object sender, System.EventArgs e)
        {
            this.show_sut_settings = false;
            this.ResizeSessionPropertiesView ();
        }

        private void MaxSUTSettings_Click(object sender, System.EventArgs e)
        {
            this.show_sut_settings = true;
            this.ResizeSessionPropertiesView ();
        }
        #endregion

        #region TextBoxSUTImplClassUID

        private void TextBoxSUTImplClassUID_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).SutSystemSettings.ImplementationClassUid = this.TextBoxSUTImplClassUID.Text;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).SutSystemSettings.ImplementationClassUid = this.TextBoxSUTImplClassUID.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region TextBoxSUTImplVersionName

        private void TextBoxSUTImplVersionName_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).SutSystemSettings.ImplementationVersionName = this.TextBoxSUTImplVersionName.Text;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).SutSystemSettings.ImplementationVersionName = this.TextBoxSUTImplVersionName.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region TextBoxSUTAETitle

        private void TextBoxSUTAETitle_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).SutSystemSettings.AeTitle = this.TextBoxSUTAETitle.Text;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).SutSystemSettings.AeTitle = this.TextBoxSUTAETitle.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region NumericSUTListenPort

        private void NumericSUTListenPort_Validated(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).SutSystemSettings.Port = (ushort)this.NumericSUTListenPort.Value;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).SutSystemSettings.Port = (ushort)this.NumericSUTListenPort.Value;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #region TextBoxSUTTCPIPAddress

        private void TextBoxSUTTCPIPAddress_TextChanged(object sender, System.EventArgs e)
        {
            // We update the UI to reflect that a setting has been changed.
            // The actual session properties are updated when the control has lost focus
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).SutSystemSettings.HostName = this.TextBoxSUTTCPIPAddress.Text;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).SutSystemSettings.HostName = this.TextBoxSUTTCPIPAddress.Text;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }
 
        #endregion

        #region ButtonCheckTCPIPAddress

        #endregion

        #region NumericSUTMaxPDU

        private void NumericSUTMaxPDU_Validated(object sender, System.EventArgs e)
        {
            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

            if (this.selected_session is Dvtk.Sessions.ScriptSession)
                ((Dvtk.Sessions.ScriptSession)this.selected_session).SutSystemSettings.MaximumLengthReceived = (uint)this.NumericSUTMaxPDU.Value;
            if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                ((Dvtk.Sessions.EmulatorSession)this.selected_session).SutSystemSettings.MaximumLengthReceived = (uint)this.NumericSUTMaxPDU.Value;

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        #endregion

        #endregion

        #region SecuritySettings

        #region MinMaxSecuritySettings
        private void MinSecuritySettings_Click(object sender, System.EventArgs e)
        {
            this.show_security_settings = false;
            this.ResizeSessionPropertiesView ();
        }

        private void MaxSecuritySettings_Click(object sender, System.EventArgs e)
        {
            this.show_security_settings = true;
            this.ResizeSessionPropertiesView ();
        }
        #endregion

        #region CheckBoxSecureConnection
        private void CheckBoxSecureConnection_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.selected_session is Dvtk.Sessions.ISecure)
                if ((this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.SecureSocketsEnabled != this.CheckBoxSecureConnection.Checked)
                {
                    (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.SecureSocketsEnabled = this.CheckBoxSecureConnection.Checked;
                    //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                    // Update the mainform controls (menu, toolbar, title)
                    ((MainForm)this.ParentForm).UpdateUIControls ();
                }
            this.ResizeSessionPropertiesView ();
        }
        #endregion

        #region ButtonViewCertificates

        private void ButtonViewCertificates_Click(object sender, System.EventArgs e)
        {
            CredentialsCertificatesForm cred_cert_form = new CredentialsCertificatesForm (this.selected_session, false);

            if (cred_cert_form.ShowDialog () == DialogResult.OK)
            {
                //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
        }

        #endregion

        #region ButtonViewCredentials

        private void ButtonViewCredentials_Click(object sender, System.EventArgs e)
        {
            CredentialsCertificatesForm cred_cert_form = new CredentialsCertificatesForm (this.selected_session, true);

            if (cred_cert_form.ShowDialog () == DialogResult.OK)
            {
                //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
        }

        #endregion

        #region ButtonCreateCertificate

        private void ButtonCreateCertificate_Click(object sender, System.EventArgs e)
        {
            WizardCreateCertificate wizard = new WizardCreateCertificate();
            wizard.ShowDialog(this);
        }

        #endregion

        #region ListBoxSecuritySettings

        private void ListBoxSecuritySettings_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            // Make all security setting groups invisible.
            this.GroupSecurityGeneral.Visible = false;
            this.GroupSecurityVersion.Visible = false;
            this.GroupSecurityAuthentication.Visible = false;
            this.GroupSecurityKeyExchange.Visible = false;
            this.GroupSecurityDataIntegrity.Visible = false;
            this.GroupSecurityEncryption.Visible = false;

            // Make the selected security setting group visible only.
            switch (this.ListBoxSecuritySettings.SelectedIndex)
            {
                case 0:
                    this.GroupSecurityGeneral.Visible = true;
                    break;
                case 1:
                    this.GroupSecurityVersion.Visible = true;
                    break;
                case 2:
                    this.GroupSecurityAuthentication.Visible = true;
                    break;
                case 3:
                    this.GroupSecurityKeyExchange.Visible = true;
                    break;
                case 4:
                    this.GroupSecurityDataIntegrity.Visible = true;
                    break;
                case 5:
                    this.GroupSecurityEncryption.Visible = true;
                    break;
            }
        }

        #endregion

        #region UpdateFlagFunctions
        private void DisableNavigation ()
        {
            this.LabelSelect1ItemMsg.Visible = true;
            this.SessionBrowser.Enabled = false;
            this.ButtonGeneralInformation.Enabled = false;
            this.ButtonActivityLogging.Enabled = false;
            this.ButtonDetailedValidation.Enabled = false;
            this.ListBoxSecuritySettings.Enabled = false;
        }

        private void EnableNavigation ()
        {
            this.LabelSelect1ItemMsg.Visible = false;
            this.SessionBrowser.Enabled = true;
            this.ButtonGeneralInformation.Enabled = true;
            this.ButtonActivityLogging.Enabled = true;
            this.ButtonDetailedValidation.Enabled = true;
            this.ListBoxSecuritySettings.Enabled = true;
        }

        private bool UpdateSecurityFlag (Dvtk.Sessions.CipherFlags flag, bool enabled)
        {
            bool    success = true;

            if (this.selected_session is Dvtk.Sessions.ISecure)
            {
                try
                {
                    if (enabled)
                        (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.CipherFlags |= flag;
                    else
                        (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.CipherFlags &= ~flag;
                    this.EnableNavigation ();
                }
                catch (Exception e)
                {
                    this.LabelSelect1ItemMsg.Text = e.Message;
                    this.DisableNavigation();
                    success = false;
                }
            }
            if (success)
            {
                //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
            return success;
        }

        private bool UpdateVersionFlag (Dvtk.Sessions.TlsVersionFlags flag, bool enabled)
        {
            bool    success = true;

            if (this.selected_session is Dvtk.Sessions.ISecure)
            {
                try
                {
                    if (enabled)
                        (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.TlsVersionFlags |= flag;
                    else
                        (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.TlsVersionFlags &= ~flag;
                    this.EnableNavigation ();
                }
                catch (Exception e)
                {
                    this.LabelSelect1ItemMsg.Text = e.Message;
                    this.DisableNavigation();
                    success = false;
                }
            }
            if (success)
            {
                //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
            return success;
        }
        #endregion

        // Security Settings - General group settings
        #region CheckBoxCheckRemoteCertificates
        private void CheckBoxCheckRemoteCertificates_CheckedChanged(object sender, System.EventArgs e)
        {
//MK!!            if ((this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.CheckRemoteCertificate != this.Checked)
//MK!!            {
//MK!!                (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.CheckRemoteCertificate = this.CheckBoxCheckRemoteCertificates.Checked;
//MK!!                //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);
//MK!!
//MK!!                // Update the mainform controls (menu, toolbar, title)
//MK!!                ((MainForm)this.ParentForm).UpdateUIControls ();
//MK!!            }
        }
        #endregion

        #region CheckBoxCacheSecureSessions
        private void CheckBoxCacheSecureSessions_CheckedChanged(object sender, System.EventArgs e)
        {
            if ((this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.CacheTlsSessions != this.CheckBoxCacheSecureSessions.Checked)
            {
                (this.selected_session as Dvtk.Sessions.ISecure).SecuritySettings.CacheTlsSessions = this.CheckBoxCacheSecureSessions.Checked;
                //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
        }
        #endregion

        // Security Settings - Version group settings
        #region CheckBoxTLS
        private void CheckBoxTLS_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.UpdateVersionFlag (Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_TLSv1, this.CheckBoxTLS.Checked))
                this.UpdateVersionFlag (Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_SSLv3, this.CheckBoxSSL.Checked);
        }
        #endregion

        #region CheckBoxSSL
        private void CheckBoxSSL_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.UpdateVersionFlag (Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_SSLv3, this.CheckBoxSSL.Checked))
                this.UpdateVersionFlag (Dvtk.Sessions.TlsVersionFlags.TLS_VERSION_TLSv1, this.CheckBoxTLS.Checked);
        }
        #endregion

        // Security Settings - Authentication group settings
        #region CheckBoxAuthenticationRSA
        private void CheckBoxAuthenticationRSA_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_RSA, this.CheckBoxAuthenticationRSA.Checked))
                UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_DSA, this.CheckBoxAuthenticationDSA.Checked);
        }
        #endregion

        #region CheckBoxAuthenticationDSA
        private void CheckBoxAuthenticationDSA_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_DSA, this.CheckBoxAuthenticationDSA.Checked))
                UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_AUTHENICATION_METHOD_RSA, this.CheckBoxAuthenticationRSA.Checked);
        }
        #endregion

        // Security Settings - Key Exchange group settings
        #region CheckBoxKeyExchangeRSA
        private void CheckBoxKeyExchangeRSA_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_RSA, this.CheckBoxKeyExchangeRSA.Checked))
                UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_DH, this.CheckBoxKeyExchangeDH.Checked);
        }
        #endregion

        #region CheckBoxKeyExchangeDH
        private void CheckBoxKeyExchangeDH_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_DH, this.CheckBoxKeyExchangeDH.Checked))
                UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_KEY_EXCHANGE_METHOD_RSA, this.CheckBoxKeyExchangeRSA.Checked);
        }
        #endregion

        // Security Settings - Data Integrity group settings
        #region CheckBoxDataIntegritySHA
        private void CheckBoxDataIntegritySHA_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_SHA1, this.CheckBoxDataIntegritySHA.Checked))
                UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_MD5, this.CheckBoxDataIntegrityMD5.Checked);
        }
        #endregion

        #region CheckBoxDataIntegrityMD5
        private void CheckBoxDataIntegrityMD5_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_MD5, this.CheckBoxDataIntegrityMD5.Checked))
                UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_DATA_INTEGRITY_METHOD_SHA1, this.CheckBoxDataIntegritySHA.Checked);
        }
        #endregion

        // Security Settings - Encryption group settings
        #region CheckBoxEncryptionNone
        private void CheckBoxEncryptionNone_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_NONE, this.CheckBoxEncryptionNone.Checked))
                if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_3DES, this.CheckBoxEncryptionTripleDES.Checked))
                    if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES128, this.CheckBoxEncryptionAES128.Checked))
                        UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES256, this.CheckBoxEncryptionAES256.Checked);
        }
        #endregion

        #region CheckBoxEncryptionTripleDES
        private void CheckBoxEncryptionTripleDES_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_3DES, this.CheckBoxEncryptionTripleDES.Checked))
                if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_NONE, this.CheckBoxEncryptionNone.Checked))
                    if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES128, this.CheckBoxEncryptionAES128.Checked))
                        UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES256, this.CheckBoxEncryptionAES256.Checked);
        }
        #endregion

        #region CheckBoxEncryptionAES128
        private void CheckBoxEncryptionAES128_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES128, this.CheckBoxEncryptionAES128.Checked))
                if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_NONE, this.CheckBoxEncryptionNone.Checked))
                    if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_3DES, this.CheckBoxEncryptionTripleDES.Checked))
                        UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES256, this.CheckBoxEncryptionAES256.Checked);
        }
        #endregion

        #region CheckBoxEncryptionAES256
        private void CheckBoxEncryptionAES256_CheckedChanged(object sender, System.EventArgs e)
        {
            if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES256, this.CheckBoxEncryptionAES256.Checked))
                if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_NONE, this.CheckBoxEncryptionNone.Checked))
                    if (UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_3DES, this.CheckBoxEncryptionTripleDES.Checked))
                        UpdateSecurityFlag (Dvtk.Sessions.CipherFlags.TLS_ENCRYPTION_METHOD_AES128, this.CheckBoxEncryptionAES128.Checked);
        }
        #endregion

        #endregion

        #region ContextMenuSessionBrowser
        private void CM_MarkIgnore_Click(object sender, System.EventArgs e)
        {
        }

        private void CM_UnmarkIgnore_Click(object sender, System.EventArgs e)
        {
        }

        private void CM_Execute_Click(object sender, System.EventArgs e)
        {
            string  script="";
            string  emulator = "";
            string  media_file = "";
            string  results="";

            this.GetNodeProperties (this.SessionBrowser.SelectedNode, out script, out emulator, out media_file, out results);

            this._process_status = FormStatus.script_running;
            this.UpdateUIProcessRunning ();

            FileInfo    fileInfo = new FileInfo (script);
//            this.selected_session.StartResultsGathering(this.selected_session.SessionId.ToString ("000") + '_' + file.Name.Replace ('.', '_') + "_res.xml");
            this.selected_session.StartResultsGatheringWithExpandedFileNaming(fileInfo.Name);

            // Execute the selected script. This needs to be
            // done in a seperate thread; we don't want the
            // entire application to stall.
            this.selected_script = 
                System.IO.Path.Combine(
                (this.selected_session as Dvtk.Sessions.ScriptSession).DicomScriptRootDirectory,
                script);
            if ((fileInfo.Extension == ".ts") ||
                (fileInfo.Extension == ".ds") ||
                (fileInfo.Extension == ".dss"))
            {
                this.script_thread = null;
                System.AsyncCallback cb = new AsyncCallback(this.OnScriptDone);
                System.IAsyncResult ar =
                    ((Dvtk.Sessions.ScriptSession)this.selected_session).BeginExecuteScript(
                    this.selected_script,
                    false, 
                    cb);
            }
            else if (fileInfo.Extension == ".js")
            {
                this.script_thread = new Thread (new ThreadStart (this.ExecuteJSScript));
                this.script_thread.Start ();
            }
            else if (fileInfo.Extension == ".vbs")
            {
                this.script_thread = new Thread (new ThreadStart (this.ExecuteVBSScript));
                this.script_thread.Start ();
            }
        }

        private void CM_StartEmulator_Click(object sender, System.EventArgs e)
        {
			/*
            if (((System.Windows.Forms.MenuItem)sender).Text == "Start Storage SCP Emulator")
            {
                this.StartStorageSCPEmulator ();
            }
            if (((System.Windows.Forms.MenuItem)sender).Text == "Start Storage SCU Emulator")
            {
                // Switch to the activity logging page.
                this.ButtonActivityLogging.Checked = true;

                if (this.selected_session is Dvtk.Sessions.EmulatorSession)
                {
                    Dvtk.Sessions.EmulatorSession emulatorSession =
                        this.selected_session as Dvtk.Sessions.EmulatorSession;
                    //
                    // Set proper emulator type before starting.
                    // This type is reflected in header of results.
                    //
                    emulatorSession.ScuEmulatorType = DvtkData.Results.ScuEmulatorType.Storage;
                    emulatorSession.ScpEmulatorType = DvtkData.Results.ScpEmulatorType.Unknown;
                    string fileName = string.Empty;

                    //this.selected_session.StartResultsGathering (this.selected_session.SessionId.ToString ("000") + "_st_scu_em_res.xml");
                    emulatorSession.StartResultsGatheringWithExpandedFileNaming(fileName);

                    StorageSCUEmulatorForm scu_emu = new StorageSCUEmulatorForm (emulatorSession);
                    scu_emu.ShowDialog (this);

                    emulatorSession.EndResultsGathering ();

                    this.ResizeWebView ();
                    this.UpdateSessionTreeView ();

                    // Select the results file in the session browser
                    this.SelectAndDisplayGeneratedResults ();

                    if (this.ParentForm != null)
                    {
                        // Update the mainform controls (menu, toolbar, title)
                        ((MainForm)this.ParentForm).UpdateUIControls ();
                    }
                }
            }

            if (((System.Windows.Forms.MenuItem)sender).Text == "Start Print SCP Emulator")
            {
                this.StartPrintSCPEmulator ();
            }
			*/
        }

        private void CM_ValidateMedia_Click(object sender, System.EventArgs e)
        {
            string  media_file = "";

            if (((System.Windows.Forms.MenuItem)sender).Text == "Validate media file")
            {
                // Let the user specify the media file to validate
                if (this.DialogOpenMediaFile.ShowDialog (this) == DialogResult.OK)
                {
                    // Set the media file that needs to be selected later on in the session browser to the first
                    // loaded file.
                    media_file = this.DialogOpenMediaFile.FileName;

                    foreach (string m_file in this.DialogOpenMediaFile.FileNames)
                    {
                        FileInfo fileInfo = new FileInfo (m_file);

                        // Switch to the activity logging page.
                        this.ButtonActivityLogging.Checked = true;

                        if (this.selected_session is Dvtk.Sessions.MediaSession)
                        {
                            Dvtk.Sessions.MediaSession mediaSession = this.selected_session as Dvtk.Sessions.MediaSession;
                            mediaSession.StartResultsGatheringWithExpandedFileNaming(fileInfo.Name);
                            string[] mediaFilesToValidate = new string[] { m_file };
                            mediaSession.ValidateMediaFiles (mediaFilesToValidate);
                            mediaSession.EndResultsGathering ();

                            // Add the validated media file to the media session
                            //if (!this.project.GetMediaSession (mediaSession.SessionFileName).media_files.Contains (m_file))
                            {
                                //this.project.GetMediaSession (mediaSession.SessionFileName).media_files.Add (m_file);
                            }
                        }
                    }
                }
                else
                    return;
            }
            else
            {
                string  script = "";
                string  emulator = "";
                string  results = "";
                FileInfo fileInfo;

                this.GetNodeProperties (this.SessionBrowser.SelectedNode, out script, out emulator, out media_file, out results);

                fileInfo = new FileInfo (media_file);

                // Switch to the activity logging page.
                this.ButtonActivityLogging.Checked = true;

                if (this.selected_session is Dvtk.Sessions.MediaSession)
                {
                    Dvtk.Sessions.MediaSession mediaSession = this.selected_session as Dvtk.Sessions.MediaSession;
                    mediaSession.StartResultsGatheringWithExpandedFileNaming(fileInfo.Name);
                    string[] mediaFilesToValidate = new string[] { media_file };
                    mediaSession.ValidateMediaFiles (mediaFilesToValidate);
                    mediaSession.EndResultsGathering ();

                    // Add the validated media file to the media session
                    //if (!this.project.GetMediaSession (mediaSession.SessionFileName).media_files.Contains (media_file))
                    {
                        //this.project.GetMediaSession (mediaSession.SessionFileName).media_files.Add (media_file);
                    }
                }
            }

            this.UpdateSessionTreeView ();

            /* Select the media file in the session browse tree view. */
            foreach (TreeNode node in this.SessionBrowser.SelectedNode.Nodes)
            {
                if (node.Text == media_file)
                    this.SessionBrowser.SelectedNode = node;
            }

            this.SelectAndDisplayGeneratedResults ();
        }

        private void CM_Display_Click(object sender, System.EventArgs e)
        {
            // The Display functionality can be executed on script level and
            // on results level. We need to figure out which level has been
            // accessed and update the UI accordingly.
            string script = "";
            string emulator = "";
            string media_file = "";
            string results = "";

            this.GetNodeProperties (this.SessionBrowser.SelectedNode, out script, out emulator, out media_file, out results);
            if (results != "")
            {
                this._active_page = ActivePage.detailed_validation;
                this.selected_results = 
                    System.IO.Path.Combine(
                    this.selected_session.ResultsRootDirectory,
                    results);
                this.ButtonDetailedValidation.Checked = true;

                this.ClearNavigationHistory ();
                this.UpdateDetailedResultsView ();
            }
            else
            {
                Dvtk.Sessions.ScriptSession scriptSession = this.selected_session as Dvtk.Sessions.ScriptSession;

                this.selected_script = 
                    System.IO.Path.Combine(
                    scriptSession.DicomScriptRootDirectory,
                    script);
                /* If the directory in which the script is located contains a html directory named 'html',
                 * we need to look for a .html file with the same base name as the script.
                 * If such a file exists, we want to display the html file instead of the raw script file.
                 */
                DirectoryInfo   dir_info = new DirectoryInfo (scriptSession.DicomScriptRootDirectory);
                DirectoryInfo[]   html_dir = dir_info.GetDirectories("html");

                // Reset the selected description file
                this.selected_description = "";

                if (html_dir.Length > 0)
                {
                    string html_file = script.Replace ('.', '_') + ".html";
                    dir_info = (DirectoryInfo)html_dir.GetValue (0);
                    foreach (FileInfo file in dir_info.GetFiles())
                    {
                        if (file.Name == html_file)
                            this.selected_description = 
                                System.IO.Path.Combine(
                                scriptSession.DicomScriptRootDirectory,
                                "html\\"+html_file);
                    }
                }

                if (this.selected_description != "")
                {
                    object Zero = 0;
                    object EmptyString = "";

                    this.WebDescriptionView.Navigate (this.selected_description, ref Zero, ref EmptyString, ref EmptyString, ref EmptyString);
                    this._active_page = ActivePage.description;
                }
                else
                {
                    this.ClearNavigationHistory ();
                    this.RichTextBoxScript.LoadFile (this.selected_script, RichTextBoxStreamType.PlainText);
                    this._active_page = ActivePage.script;
                }
                this.ButtonGeneralInformation.Checked = true;
            }
        }

        private void CM_Edit_Click(object sender, System.EventArgs e)
        {
            string script = "";
            string emulator = "";
            string media_file = "";
            string results = "";

            this.GetNodeProperties (this.SessionBrowser.SelectedNode, out script, out emulator, out media_file, out results);

            this.selected_script = 
                System.IO.Path.Combine(
                ((Dvtk.Sessions.ScriptSession)this.selected_session).DicomScriptRootDirectory,
                script);

            this.EditSelectedScript ();
        }

        private void OnExitEventEditor(object sender, EventArgs e)
        {
            // Only update the UI when the current page is a script view.
            if (this._active_page == ActivePage.script)
            {
                this.ClearNavigationHistory ();
                this.RichTextBoxScript.LoadFile (this.selected_script, RichTextBoxStreamType.PlainText);
            }
        }

        public void EditCopySelection ()
        {
            if ((this._active_page == ActivePage.description) ||
                (this._active_page == ActivePage.detailed_validation) ||
                (this._active_page == ActivePage.validation_results))
            {
                // These pages are displaying web based content. Use appropriate selection functions.
                mshtml.IHTMLTxtRange selected_text = (mshtml.IHTMLTxtRange)this.document.selection.createRange ();

                if (selected_text.text != "")
                    Clipboard.SetDataObject (selected_text.text);
            }
        }

        public void EditSelectedScript ()
        {
            System.Diagnostics.Process p  = new System.Diagnostics.Process();

            //Register for the event that notifies the process exit
            p.Exited+=new EventHandler(OnExitEventEditor);
            p.StartInfo.FileName= "Notepad.exe";
            p.StartInfo.Arguments = this.selected_script;

            //you must enable this property or you won't get the event
            p.EnableRaisingEvents=true;

            p.Start();
        }

        private void CM_AddNewSession_Click(object sender, System.EventArgs e)
        {
            this.SessionAddNew ();

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        private void CM_AddExistingSession_Click(object sender, System.EventArgs e)
        {
            this.SessionAddExisting ();

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        private void CM_Remove_Click(object sender, System.EventArgs e)
        {
            this.SessionRemove ();

            // Update the mainform controls (menu, toolbar, title)
            ((MainForm)this.ParentForm).UpdateUIControls ();
        }

        private void CM_Save_Click(object sender, System.EventArgs e)
        {
            string msg = 
                string.Format(
                "Do you want to save the session file: {0}\n", this.selected_session.SessionFileName);
            if (MessageBox.Show (this,
                msg,
                "Save session file?",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                this.SessionSave ();

                // Update the mainform controls (menu, toolbar, title)
                ((MainForm)this.ParentForm).UpdateUIControls ();
            }
        }

        private void CM_Properties_Click(object sender, System.EventArgs e)
        {
            // Switch to the General Information view.
            this._active_page = ActivePage.session;
            this.ButtonGeneralInformation.Checked = true;
            this.UpdatePageVisibility ();
        }
        #endregion

        #region ContextMenuDetailedValidation
		/*
        public void UpdateCMDetailedValidation ()
        {
            this.CM_FindAgain.Enabled = (((MainForm)this.ParentForm).search_string != "");
        }
		*/

        private void CM_Back_Click(object sender, System.EventArgs e)
        {
            this.NavigateBack ();
        }

        private void CM_Forward_Click(object sender, System.EventArgs e)
        {
            this.NavigateForward ();
        }

        private void CM_Copy_Click(object sender, System.EventArgs e)
        {
            this.EditCopySelection ();
        }

        private void CM_Find_Click(object sender, System.EventArgs e)
        {
            ((MainForm)this.ParentForm).ActionFind ();
        }

        private void CM_FindAgain_Click(object sender, System.EventArgs e)
        {
            ((MainForm)this.ParentForm).ActionFindAgain ();
        }
        #endregion

        #region SOPClassesView
        #region SOPClasses
        private void SOPClasses_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // The reason we handle the mouseup event ourselves, is because with the normal
            // grid, you first have to select a cell, and only then you can enable/disable a
            // checkbox. We want to enable/disable the checkbox with 1 click.
            DataGrid    sop_classes = (DataGrid) sender;
            System.Windows.Forms.DataGrid.HitTestInfo   hti;

            hti = sop_classes.HitTest (e.X, e.Y);

            switch (hti.Type)
            {
                case System.Windows.Forms.DataGrid.HitTestType.Cell:
                    if (hti.Column == 0)
                    {
                        // Remember the cell we've changed. We don't want to change it when we move the mouse.
                        this.changed_cell = hti.Row;

                        DataGridColumnStyle dg_col;
                        dg_col = this.SOPClasses.TableStyles[0].GridColumnStyles[0];

                        this.SOPClasses.BeginEdit(dg_col, hti.Row);
                        DefinitionFile def_file=(DefinitionFile)this.definition_files[hti.Row];
                        def_file.Loaded = !def_file.Loaded;
                        this.loaded_state = def_file.Loaded;
                        this.SOPClasses.EndEdit (dg_col, hti.Row, false);
                    }
                    break;
            }
        }

        private void SOPClasses_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DataGrid    sop_classes = (DataGrid) sender;
                System.Windows.Forms.DataGrid.HitTestInfo   hti;

                hti = sop_classes.HitTest (e.X, e.Y);
                switch (hti.Type)
                {
                    case System.Windows.Forms.DataGrid.HitTestType.Cell:
                        if (hti.Column == 0)
                        {
                            if (hti.Row != this.changed_cell)
                            {
                                DataGridColumnStyle dg_col;
                                dg_col = this.SOPClasses.TableStyles[0].GridColumnStyles[0];

                                this.SOPClasses.BeginEdit(dg_col, hti.Row);
                                DefinitionFile def_file=(DefinitionFile)this.definition_files[hti.Row];
                                def_file.Loaded = this.loaded_state;
                                this.SOPClasses.EndEdit (dg_col, hti.Row, false);

                                // Remember the cell we've changed. We don't want to change the loaded
                                // state with each minor mouse move.
                                this.changed_cell = hti.Row;
                            }
                        }
                        break;
                }
            }
        }

        private void SOPClasses_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.changed_cell = -1;
        }
        #endregion

        #region ButtonAddDefinitionRoot
        private void ButtonAddDefinitionRoot_Click(object sender, System.EventArgs e)
        {
            this.DialogBrowseFolder.Description = "Select the root directory where definition files are located.";
            if (this.TextBoxResultsRoot.Text != "")
                this.DialogBrowseFolder.SelectedPath = this.selected_session.DefinitionManagement.DefinitionFileRootDirectory;

            if (this.DialogBrowseFolder.ShowDialog (this) == DialogResult.OK)
            {
                // First Load/Unload the selected definition files.
                this.LoadUnloadDefinitionFiles ();

                if (this.ListBoxDefinitionDirs.Items.Contains (this.DialogBrowseFolder.SelectedPath))
                    MessageBox.Show (this,
                        "The directory specified is already present in\nthe list of definition root directories.",
                        "Directory not added",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                else
                {
                    this.selected_session.DefinitionManagement.DefinitionFileDirectoryList.Add (this.DialogBrowseFolder.SelectedPath);
                    this.UpdateSOPClassesView ();
                    //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                    // Update the controls of the mainform (toolbar, menubar, title bar)
                    ((MainForm)this.ParentForm).UpdateUIControls ();
                }
            }
        }
        #endregion

        #region ButtonRemoveDefinitionRoot
        private void ButtonRemoveDefinitionRoot_Click(object sender, System.EventArgs e)
        {
            int nr_items = this.ListBoxDefinitionDirs.SelectedItems.Count;
            if (nr_items > 0)
            {
                if (MessageBox.Show (this,
                    "Are you sure you want to delete the selected definition root directories?",
                    "Remove selected root directories?",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    // First Load/Unload the selected definition files.
                    this.LoadUnloadDefinitionFiles ();

                    // Create a copy of the selected items. This is needed because the
                    // selected items list is dynamically updated.
                    ArrayList list = new ArrayList (this.ListBoxDefinitionDirs.SelectedItems);
                    foreach (string item in list)
                        this.selected_session.DefinitionManagement.DefinitionFileDirectoryList.Remove (item);

                    if (this.ListBoxDefinitionDirs.Items.Count == 0)
                        this.ButtonRemoveDefinitionRoot.Enabled = false;

                    this.UpdateSOPClassesView ();
                    //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                    // Update the controls of the mainform (toolbar, menubar, title bar)
                    ((MainForm)this.ParentForm).UpdateUIControls ();
                }
            }
        }
        #endregion

        #region ButtonReturnToSessionProperties
        /// <summary>
        /// This function closes the SOP classes view and shows the session properties page.
        /// </summary>
        /// <todo>
        /// Check with definition roots before (un)loading def files.
        /// </todo>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonReturnToSessionProperties_Click(object sender, System.EventArgs e)
        {
            this.LoadUnloadDefinitionFiles ();

            this._active_page = ActivePage.session;
            this.VScrollBarSessionInfo.Visible = true;
            this.PanelSessionProperties.Visible = true;
            this.RichTextBoxInfo.Visible = this.ShowRichTextBoxInfo;

            this.PanelSOPClasses.Visible = false;
            this.RichTextBoxScript.Visible = false;
            this.WebDescriptionView.Visible = false;

            this.PositionPageViewButtons ();
            this.ResizeSessionPropertiesView ();
        }
        #endregion

        /// <summary>
        /// This function loads and unloads the necessary definition files
        /// </summary>
        private void LoadUnloadDefinitionFiles ()
        {
            // Load and unload the necessary definition files.
            foreach (DefinitionFile def_file in this.definition_files)
            {
                if (def_file.Loaded)
                {
                    // Check if the def file is already present in the session file.
                    bool found = false;

                    string[] loaded_files = this.selected_session.DefinitionManagement.LoadedDefinitionFileNames;
                    foreach (string file in loaded_files)
                    {
                        if ((def_file.Filename == file) ||
                            (
                            System.IO.Path.Combine(
                            def_file.DefinitionRoot,
                            def_file.Filename) == file))
                        {
                            // The definition file is already present in the session
                            // file; do nothing.
                            found = true;
                        }
                    }
                    if (!found)
                    {
                        this.selected_session.DefinitionManagement.LoadDefinitionFile (
                            System.IO.Path.Combine(
                            def_file.DefinitionRoot,
                            def_file.Filename)
                            );
                        //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                        // Update the controls of the mainform (toolbar, menubar, title bar)
                        ((MainForm)this.ParentForm).UpdateUIControls ();
                    }
                }
                else
                {
                    // Check if the definition file is present in the session file.
                    // If that's the case, we need to unload the definition file.
                    string[] loaded_files = this.selected_session.DefinitionManagement.LoadedDefinitionFileNames;
                    foreach (string file in loaded_files)
                    {
                        if (
                            System.IO.Path.Combine(def_file.DefinitionRoot,def_file.Filename) == file)
                        {
                            this.selected_session.DefinitionManagement.UnLoadDefinitionFile (
                                System.IO.Path.Combine(def_file.DefinitionRoot,def_file.Filename)
                                );
                            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                            // Update the controls of the mainform (toolbar, menubar, title bar)
                            ((MainForm)this.ParentForm).UpdateUIControls ();
                        }
                        if (def_file.Filename == file)
                        {
                            this.selected_session.DefinitionManagement.UnLoadDefinitionFile (def_file.Filename);
                            //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                            // Update the controls of the mainform (toolbar, menubar, title bar)
                            ((MainForm)this.ParentForm).UpdateUIControls ();
                        }
                    }
                }
            }

            if (this.ComboBoxAETitleVersion.SelectedItem != null)
            {
                if (this.ComboBoxAETitleVersion.SelectedItem.ToString() != this.selected_session.DefinitionManagement.ApplicationEntityName + " - " + this.selected_session.DefinitionManagement.ApplicationEntityVersion)
                {
                    // The user selected another AE Title-Version item. Update the session properties.
                    for (int i=0 ; i<this.ae_titles.Count ; i++)
                    {
                        if (this.ComboBoxAETitleVersion.SelectedItem.ToString() == this.ae_titles[i] + " - " + this.ae_versions[i])
                        {
                            this.selected_session.DefinitionManagement.ApplicationEntityName = this.ae_titles[i].ToString();
                            this.selected_session.DefinitionManagement.ApplicationEntityVersion = this.ae_versions[i].ToString();
                        }
                    }
                    //this.project.SetSessionChanged (this.selected_session.SessionFileName, true);

                    // Update the controls of the mainform (toolbar, menubar, title bar)
                    ((MainForm)this.ParentForm).UpdateUIControls ();
                }
            }
        }

        private void UpdateSOPClassesView ()
        {
            DirectoryInfo   definition_dir;

            Cursor.Current = Cursors.WaitCursor;

            // When updating the SOP classes view, all fields need to be reset.
            this.ListBoxDefinitionDirs.Items.Clear ();

            // Clear the Info text box. This box will contain possible errors with
            // loading incorrect definition files.
            this.RichTextBoxInfo.Clear ();

            // Create and fill in the array containing all definition files found in the
            // definition root.
            this.definition_files = new ArrayList();
            foreach (string def_root in this.selected_session.DefinitionManagement.DefinitionFileDirectoryList)
            {
                string mod_root = def_root;

                // strip any leading / or \ from the def_root
                if ((def_root[def_root.Length-1] == '/') ||
                    (def_root[def_root.Length-1] == '\\'))
                    mod_root = def_root.Remove (def_root.Length-1, 1);

                // Add the definition root directory to the definition root list view.
                this.ListBoxDefinitionDirs.Items.Add (mod_root);

                definition_dir = new DirectoryInfo (mod_root);

                if (definition_dir.Exists)
                {
                    FileInfo[] files = definition_dir.GetFiles();
                    foreach (FileInfo file in files)
                    {
                        Dvtk.Sessions.DefinitionFileDetails def_details;
                        try
                        {
                            def_details = this.selected_session.DefinitionManagement.GetDefinitionFileDetails (file.FullName);
                            DefinitionFile  def_file = new DefinitionFile (false,
                                file.Name,
                                def_details.SOPClassName,
                                def_details.SOPClassUID,
                                def_details.ApplicationEntityName,
                                def_details.ApplicationEntityVersion,
                                mod_root);
                            this.definition_files.Add (def_file);
                        }
                        catch
                        {
                            this.RichTextBoxInfo.Text += 
                                string.Format(
                                "In definitionroot: {0} the file {1} is not a correct definition file.\n",
                                definition_dir.FullName,
                                file.FullName
                                );
                        }
                    }
                }
            }

            this.AddSelectLoadedSOPClasses ();
            if (this.definition_files.Count > 0)
            {
                // We can only select items when there is at least 1 definition file loaded.
                this.FillAETitleVersionComboBox ();
                this.SOPClasses.SetDataBinding (definition_files, "");
            }

            Cursor.Current = Cursors.Default;
        }

        private DataGridTableStyle CreateTableStyle ()
        {
            DataGridTableStyle style = new DataGridTableStyle();

            style.AllowSorting = true;
            style.RowHeadersVisible = false;
            style.MappingName = "ArrayList";

            // We set the column to readonly and handle the mouse events ourselves
            // in the MouseUp event handler. We want to circumvent the select cell
            // first before you can enable/disable a checkbox.
            DataGridBoolColumn column_loaded = new DataGridBoolColumn();
            column_loaded.MappingName = "Loaded";
            column_loaded.HeaderText = "Loaded";
            column_loaded.Width = 50;
            column_loaded.AllowNull = false;
            column_loaded.ReadOnly = true;
            style.GridColumnStyles.Add (column_loaded);
      
            DataGridColumnStyle column_filename = new DataGridTextBoxColumn();
            column_filename.MappingName = "Filename";
            column_filename.HeaderText = "Definition filename";
            column_filename.Width = 250;
            column_filename.ReadOnly = true;
            style.GridColumnStyles.Add(column_filename);

            DataGridColumnStyle column_sop_class_name = new DataGridTextBoxColumn();
            column_sop_class_name.MappingName = "SOPClassName";
            column_sop_class_name.HeaderText = "SOP class name";
            column_sop_class_name.Width = 125;
            column_sop_class_name.ReadOnly = true;
            style.GridColumnStyles.Add(column_sop_class_name);

            DataGridColumnStyle column_sop_class_uid = new DataGridTextBoxColumn();
            column_sop_class_uid.MappingName = "SOPClassUID";
            column_sop_class_uid.HeaderText = "SOP class UID";
            column_sop_class_uid.Width = 125;
            column_sop_class_uid.ReadOnly = true;
            style.GridColumnStyles.Add(column_sop_class_uid);

            DataGridColumnStyle column_ae_title = new DataGridTextBoxColumn();
            column_ae_title.MappingName = "AETitle";
            column_ae_title.HeaderText = "AE title";
            column_ae_title.Width = 100;
            column_ae_title.ReadOnly = true;
            style.GridColumnStyles.Add(column_ae_title);

            DataGridColumnStyle column_ae_version = new DataGridTextBoxColumn();
            column_ae_version.MappingName = "AEVersion";
            column_ae_version.HeaderText = "AE version";
            column_ae_version.Width = 50;
            column_ae_version.ReadOnly = true;
            style.GridColumnStyles.Add(column_ae_version);

            DataGridColumnStyle column_definition_root = new DataGridTextBoxColumn();
            column_definition_root.MappingName = "DefinitionRoot";
            column_definition_root.HeaderText = "Definition root";
            column_definition_root.Width = 200;
            column_definition_root.ReadOnly = true;
            style.GridColumnStyles.Add(column_definition_root);

            return style;
        }

        private void AddSelectLoadedSOPClasses ()
        {
            string[]    files;

            files = this.selected_session.DefinitionManagement.LoadedDefinitionFileNames;

            foreach (string file in files)
            {
                FileInfo    fileInfo = new FileInfo (file);
                bool        found = false;
                foreach (DefinitionFile def_file in this.definition_files)
                {
                    if (file == fileInfo.Name)
                    {
                        // No directory element has been specified. Use the definition root
                        // as has been set in the Session file.
                        if ((file == def_file.Filename) &&
                            (this.selected_session.DefinitionManagement.DefinitionFileRootDirectory == def_file.DefinitionRoot))
                        {
                            def_file.Loaded = true;
                            found = true;
                        }
                    }
                    else
                    {
                        if ((fileInfo.Name == def_file.Filename) &&
                            (fileInfo.DirectoryName == def_file.DefinitionRoot))
                        {
                            def_file.Loaded = true;
                            found = true;
                        }
                    }
                }
                if (!found)
                {
                    DefinitionFile  def_file;
                    Dvtk.Sessions.DefinitionFileDetails def_details;

                    try
                    {

                        def_details = this.selected_session.DefinitionManagement.GetDefinitionFileDetails (fileInfo.FullName);
                        if (file == fileInfo.Name)
                        {
                            // No directory element has been specified. Use the definition root
                            // as has been set in the Session file.
                            def_file = new DefinitionFile (true,
                                fileInfo.Name,
                                def_details.SOPClassName,
                                def_details.SOPClassUID,
                                def_details.ApplicationEntityName,
                                def_details.ApplicationEntityVersion,
                                this.selected_session.DefinitionManagement.DefinitionFileRootDirectory);
                        }
                        else
                        {
                            def_file = new DefinitionFile (true,
                                fileInfo.Name,
                                def_details.SOPClassName,
                                def_details.SOPClassUID,
                                def_details.ApplicationEntityName,
                                def_details.ApplicationEntityVersion,
                                fileInfo.DirectoryName);
                        }
                        definition_files.Add (def_file);
                    }
                    catch
                    {
                        this.RichTextBoxInfo.Text += 
                        string.Format(
                            "Definition file {0} could not be loaded.\n",
                            fileInfo.FullName);
                    }
                }
            }
        }

        private ArrayList   ae_titles;
        private ArrayList   ae_versions;

        private void FillAETitleVersionComboBox ()
        {
            this.ae_titles = new ArrayList ();
            this.ae_versions = new ArrayList ();
            this.ComboBoxAETitleVersion.Items.Clear ();

            foreach (DefinitionFile def_file in this.definition_files)
            {
                if (!this.ComboBoxAETitleVersion.Items.Contains (def_file.AETitle + " - " + def_file.AEVersion))
                {
                    this.ComboBoxAETitleVersion.Items.Add (def_file.AETitle + " - " + def_file.AEVersion);
                    this.ae_titles.Add (def_file.AETitle);
                    this.ae_versions.Add (def_file.AEVersion);
                }
            }
            for (int i=0 ; i<this.ComboBoxAETitleVersion.Items.Count ; i++)
            {
                if (this.ComboBoxAETitleVersion.Items[i].ToString() == this.selected_session.DefinitionManagement.ApplicationEntityName + " - " + this.selected_session.DefinitionManagement.ApplicationEntityVersion)
                    this.ComboBoxAETitleVersion.SelectedIndex = i;
            }
        }
        #endregion

        private void ProjectForm_Load(object sender, System.EventArgs e)
        {
            // Force correct tooltip display for NumericUpDown controls.
            // MS bug!
            foreach (Control c in this.NumericSessonID.Controls)
            {
                this.toolTip.SetToolTip(c, this.toolTip.GetToolTip(this.NumericSessonID));
            }
            foreach (Control c in this.NumericDVTListenPort.Controls)
            {
                this.toolTip.SetToolTip(c, this.toolTip.GetToolTip(this.NumericDVTListenPort));
            }
            foreach (Control c in this.NumericDVTTimeOut.Controls)
            {
                this.toolTip.SetToolTip(c, this.toolTip.GetToolTip(this.NumericDVTTimeOut));
            }
            foreach (Control c in this.NumericDVTMaxPDU.Controls)
            {
                this.toolTip.SetToolTip(c, this.toolTip.GetToolTip(this.NumericDVTMaxPDU));
            }
            foreach (Control c in this.NumericSUTListenPort.Controls)
            {
                this.toolTip.SetToolTip(c, this.toolTip.GetToolTip(this.NumericSUTListenPort));
            }
            foreach (Control c in this.NumericSUTMaxPDU.Controls)
            {
                this.toolTip.SetToolTip(c, this.toolTip.GetToolTip(this.NumericSUTMaxPDU));
            }
        }

        private void WebDescriptionView_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
        {
            // update the URL displayed in the address bar
            String s = e.uRL.ToString();
            // textBoxAddress.Text = s;
            // update the list of visited URLs
            int i = urlsVisited.IndexOf(s);
            if (i >= 0)
                currentUrlIndex = i;
            else 
                currentUrlIndex = urlsVisited.Add(s);
            // enable / disable the Back and Forward buttons
            // Update the context sensitive menu for the browser
            this.CM_Back.Enabled = this.can_navigate_back;
            this.CM_Forward.Enabled = this.can_navigate_forward;
        }
    }
}