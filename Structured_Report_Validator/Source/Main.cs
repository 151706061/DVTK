using Dvtk;
using Dvtk.Dicom.StructuredReporting;
using Dvtk.Dicom.StructuredReporting.Specification;
using Dvtk.Dicom.StructuredReporting.Validation;
using DvtkHighLevelInterface.Dicom.Files;
using Dvtk.Xml.Transformation;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Dvtk.StructuredReportValidator.ResultsNavigator;
using Dvtk.StructuredReportValidator.BackgroundWorkers;

namespace Dvtk.StructuredReportValidator
{
    public partial class Main : Form
    {
        #region Internal Variables
        /// <summary>
        ///     Contains the path of the process folder.
        /// </summary>
        private string processFolder;
        
        /// <summary>
        ///     Contains the path of the Xml folder for the current DICOM SR Object.
        /// </summary>
        private string xmlFolder;

        /// <summary>
        ///     Contains the path of the Html folder for the current DICOM SR Object.
        /// </summary>
        private string htmlFolder;

        /// <summary>
        ///     Contains the filename of the current DICOM SR Object.
        /// </summary>
        private string srFilename;

        private string templatesFolder = string.Empty;

        private MainBackgroundWorker mainBackgroundWorker = null;

        #endregion



        #region Public Functions
        public Main()
        {
            InitializeComponent();
            positionProgressbar();


            // Create a temp folder for the current application process.
            processFolder = Path.GetTempPath();
            processFolder = Path.Combine(processFolder, "DVTk");
            processFolder = Path.Combine(processFolder, "SRV");
            processFolder = Path.Combine(processFolder, Process.GetCurrentProcess().Id.ToString());
            Directory.CreateDirectory(processFolder);

            string ucumFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles), @"DVTk\Definition Files\UCUM\UCUM Contents.xml");
            Ucum.Tools.InitUcum(ucumFolder);

            this.templatesFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            this.templatesFolder = Path.Combine(this.templatesFolder, "Templates");

            // Check if the templates folder actually exists.
            if (!Directory.Exists(this.templatesFolder))
            {
                MessageBox.Show(this, "The Templates folder was not found, please make sure the application is installed correctly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.mainBackgroundWorker = new MainBackgroundWorker(templatesFolder);

            this.mainBackgroundWorker.RunWorkerCompleted += MainBackgroundWorkerHandleRunWorkerCompleted;
            this.mainBackgroundWorker.ProgressChanged += MainBackgroundWorkerHandleProgressChanged;


        }
        #endregion



        #region Private Functions
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog.FileName = string.Empty;

            // Display Open File Dialog and wait for OK.
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                // Start the processing of the selected file using the Background Worker.
                //this.backgroundWorker.DoWork += backgroundWorker_DoWork;
                //this.backgroundWorker.ProgressChanged += backgroundWorker_ProgressChanged;
                //this.backgroundWorker.RunWorkerCompleted += backgroundWorker_RunWorkerCompleted;
                this.srFilename = Path.GetFileName(openFileDialog.FileName);
                initializeFolders();
                cleanFolders();
                //this.backgroundWorker.RunWorkerAsync(openFileDialog.FileName);

                MainBackgroundWorkerArgument argument = new MainBackgroundWorkerArgument();
                argument.structuredReportPath = srFilename;
                argument.xmlPath = this.xmlFolder;
                argument.htmlPath = this.htmlFolder;

                this.mainBackgroundWorker.RunWorkerAsync(argument);
            }
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            // Resize the Status Label to keep the progress bar aligned to the right.
            toolStripStatusLabel.Size = new Size(this.Size.Width - 320, toolStripStatusLabel.Size.Height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Main_SizeChanged(object sender, EventArgs e)
        {
            positionProgressbar();
        }

        private void positionProgressbar()
        {
            if (this.Width > 360)
            {
                toolStripStatusLabel.Width = this.Width - 360;
            }
        }

        private void UpdateProgressBar(int progress, string message)
        {
            // Update the status message.
            toolStripStatusLabel.Text = message;

            // When the progress is 100% then hide the Progress Bar, else update.
            if (progress == 100)
            {
                toolStripProgressBar.Visible = false;
                toolStripProgressBar.Value = 0;
            }
            else
            {
                toolStripProgressBar.Visible = true;
                toolStripProgressBar.Value = progress;
            }
        }
        
        private void initializeFolders()
        {
            // Generate the temp path for the current process for the current user.
            string folderPath = processFolder;
            folderPath = Path.Combine(folderPath, srFilename);

            // Generate the temp path for the XML and HTML results.
            xmlFolder = Path.Combine(folderPath, "XML");
            htmlFolder = Path.Combine(folderPath, "HTML");

            // Create the temp path for the XMl and HTML results.
            Directory.CreateDirectory(xmlFolder);
            Directory.CreateDirectory(htmlFolder);
        }

        /// <summary>
        ///     Cleans the <see cref="xmlFolder" /> and <see cref="htmlFolder" /> by removing all
        ///     files. Reports progress directly.
        /// </summary>
        /// <returns>
        ///     Total number of removed files.
        /// </returns>
        private int cleanFolders()
        {
            int totalFileCount;
            float progressCount = 0;
            int progress = 0;

            // Get the total number of files.
            totalFileCount = Directory.GetFiles(xmlFolder).Length;
            totalFileCount += Directory.GetFiles(htmlFolder).Length;

            foreach (string xmlFile in Directory.GetFiles(xmlFolder))
            {
                File.Delete(xmlFile);

                // Calculate and report progress.
                progressCount++;
                progress = Convert.ToInt32((progressCount / totalFileCount) * 100);
                UpdateProgressBar(progress, "Cleaning folders...");
            }

            foreach (string htmlFile in Directory.GetFiles(htmlFolder))
            {
                File.Delete(htmlFile);
                
                // Calculate and report progress.
                progressCount++;
                progress = Convert.ToInt32((progressCount / totalFileCount) * 100);
                UpdateProgressBar(progress, "Cleaning folders...");
            }

            // Report progress completed.
            UpdateProgressBar(0, "");
            
            return totalFileCount;
        }

        private void copyStylesheets()
        {
            string stylesheetsFolder;
            stylesheetsFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            stylesheetsFolder = Path.Combine(stylesheetsFolder, "Stylesheets");
            foreach (string filename in Directory.GetFiles(stylesheetsFolder))
            {
                File.Copy(filename, Path.Combine(htmlFolder, Path.GetFileName(filename)), true);
            }
        }
        
        /// <summary>
        /// Update the results navigator by adding new nodes representing newly validated 
        /// Structured Report(s) and associated views.
        /// </summary>
        private void UpdateResultsNavigator()
        {
            Dvtk.StructuredReportValidator.ResultsNavigator.StructuredReportNode structuredReportNode = new Dvtk.StructuredReportValidator.ResultsNavigator.StructuredReportNode(srFilename, this.htmlFolder);

            treeViewResults.Nodes.Add(structuredReportNode);

            structuredReportNode.SelectDefaultView();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Visible = false;
            Splash splashForm = new Splash();
            splashForm.StatusMessage = "Cleaning up temporary files...";
            splashForm.Show();
            splashForm.Progress = 50;
            Directory.Delete(processFolder, true);
            splashForm.Progress = 100;
            splashForm.Close();
        }

        #endregion

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DvtkApplicationLayer.UserInterfaces.AboutForm about = new DvtkApplicationLayer.UserInterfaces.AboutForm("Structured Report Validator");
            about.ShowDialog();
        }

        private void treeViewResults_MouseUp(object sender, MouseEventArgs e)
        {
            TreeNode oldSelectedNode = null;

            // Show menu only if the right mouse button is clicked.
            if (e.Button == MouseButtons.Right)
            {
                // Point where the mouse is clicked.
                Point p = new Point(e.X, e.Y);

                // Get the node that the user has clicked.
                TreeNode node = treeViewResults.GetNodeAt(p);
                if (node != null)
                {
                    // Select the node the user has clicked.
                    // The node appears selected until the menu is displayed on the screen.
                    oldSelectedNode = treeViewResults.SelectedNode;
                    treeViewResults.SelectedNode = node;

                    // Find the appropriate ContextMenu depending on the selected node.
                    if (node is Dvtk.StructuredReportValidator.ResultsNavigator.StructuredReportNode)
                    {
                        Dvtk.StructuredReportValidator.ResultsNavigator.StructuredReportNode structuredReportNode = node as Dvtk.StructuredReportValidator.ResultsNavigator.StructuredReportNode;
                        structuredReportNode.ShowContextMenu(treeViewResults, p);
                    }

                    //// Highlight the selected node.
                    //treeViewResults.SelectedNode = oldSelectedNode;
                    //oldSelectedNode = null;
                }
            }
        }

        private void treeViewResults_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node is ViewNode)
            {
                ViewNode viewNode = e.Node as ViewNode;

                if (File.Exists(viewNode.Path))
                {
                    webBrowser.Navigate(viewNode.Path);
                }
            }
        }

        private void MainBackgroundWorkerHandleProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string message = "";

            if (e.UserState != null)
            {
                message = e.UserState.ToString();
            }

            UpdateProgressBar(e.ProgressPercentage, message);
        }

        private void MainBackgroundWorkerHandleRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message);
            }
            else if (e.Cancelled)
            {
                // Not yet implemented.
            }
            else
            {
                if (this.InvokeRequired)
                {
                    Invoke(new RunWorkerCompletedEventHandler(MainBackgroundWorkerHandleRunWorkerCompleted), sender, e);
                }
                else
                {
                    // xsltProcessor_ProgressChanged(this, new ProgressChangedEventArgs(0, null));
                    copyStylesheets();
                    UpdateResultsNavigator();
                }
            }
        }

    }
}