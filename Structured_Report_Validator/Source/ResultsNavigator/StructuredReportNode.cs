using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dvtk.StructuredReportValidator.ResultsNavigator
{
    /// <summary>
    /// Node in the results navigator that represents a Structured Report file.
    /// </summary>
    class StructuredReportNode: TreeNode
    {
        #region - Fields -
        // -----------------------
        // - Begin fields region -
        // -----------------------

        /// <summary>
        /// The context menu for this node.
        /// </summary>
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip = new System.Windows.Forms.ContextMenuStrip();

        /// <summary>
        /// See associated property.
        /// </summary>
        private string fileName = string.Empty;

        /// <summary>
        /// See associated property.
        /// </summary>
        private string resultsPath = string.Empty;

        /// <summary>
        /// Path of the directory where the results files will be saved when using the "Save Results Files" functionality.
        /// </summary>
        private string saveValidationResultsPath = string.Empty;

        // ---------------------
        // - End fields region -
        // ---------------------
        #endregion



        #region - Constructors -
        // -----------------------------
        // - Begin constructors region -
        // -----------------------------

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="path">Path of the Structured Report file that has been validated.</param>
        /// <param name="resultsPath">Path of the directory where the .html results are located.</param>
        public StructuredReportNode(string fileName, string resultsPath)
        {
            this.fileName = fileName;
            this.resultsPath = resultsPath;

            DateTime dateTime = DateTime.Now;
            this.Name = System.IO.Path.GetFileName(this.fileName) + " (validated on " + dateTime.ToString("yyyy-MM-dd HH.mm.ss") + ")";
            this.Text = this.Name;

            this.saveValidationResultsPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), @"DVTk\Structured Report Validator\" + this.Name);


            // 
            // "Save Validation Results" context menu item.
            // 

            System.Windows.Forms.ToolStripMenuItem saveValidationResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();

            saveValidationResultsToolStripMenuItem.Name = "saveValidationResultsToolStripMenuItem";
            saveValidationResultsToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
            saveValidationResultsToolStripMenuItem.Text = "Save Validation Results to \"" + this.saveValidationResultsPath + "\"";
            saveValidationResultsToolStripMenuItem.Click += new System.EventHandler(SaveValidationResults);


            //
            // Context menu.
            //

            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { saveValidationResultsToolStripMenuItem });
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(186, 48);


            //
            // Create the view nodes and add them as childs to this node.
            //

            foreach (string viewPath in System.IO.Directory.GetFiles(this.resultsPath, "*.html"))
            {
                ViewNode viewNode = new ViewNode(viewPath);
                Nodes.Add(viewNode);
            }
        }

        // ---------------------------
        // - End constructors region -
        // ---------------------------
        #endregion



        #region - Public properties -
        // ----------------------------------
        // - Begin public properties region -
        // ----------------------------------

        /// <summary>
        /// Gets the path of the Structured Report file that has been validated.
        /// </summary>
        public string FileName
        {
            get
            {
                return (this.fileName);
            }
        }

        /// <summary>
        /// Gets the path of the directory where the .html results are located.
        /// </summary>
        public string ResultsPath
        {
            get
            {
                return (this.resultsPath);
            }
        }

        // --------------------------------
        // - End public properties region -
        // --------------------------------
        #endregion



        #region - Public methods -
        // -------------------------------
        // - Begin public methods region -
        // -------------------------------

        /// <summary>
        /// Save the validation results to a subdirectory of the "My Documents" directory.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The event arguments.</param>
        private void SaveValidationResults(object sender, EventArgs e)
        {
            try
            {
                System.IO.Directory.CreateDirectory(this.saveValidationResultsPath);

                foreach (string resultsFilePath in System.IO.Directory.GetFiles(this.resultsPath))
                {
                    System.IO.File.Copy(resultsFilePath, System.IO.Path.Combine(this.saveValidationResultsPath, System.IO.Path.GetFileName(resultsFilePath)));
                }
            }
            catch
            {
                MessageBox.Show("Problem occured while saving results files to \"" + this.saveValidationResultsPath + "\"");
            }
        }

        public void SelectDefaultView()
        {
            foreach (ViewNode viewNode in Nodes)
            {
                if (System.IO.Path.GetFileNameWithoutExtension(viewNode.Path) == "Default")
                {
                    TreeView.SelectedNode = viewNode;
                    
                }
            }
        }

        /// <summary>
        /// Show the context menu of this node.
        /// </summary>
        /// <param name="resultsNavigator">The results navigator this node is part of.</param>
        /// <param name="point">The location on which to show the context menu.</param>
        public void ShowContextMenu(TreeView resultsNavigator, Point point)
        {
            this.contextMenuStrip.Show(resultsNavigator, point);
        }

        // -----------------------------
        // - End public methods region -
        // -----------------------------
        #endregion
    }
}
