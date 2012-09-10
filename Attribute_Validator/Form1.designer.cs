namespace Attribute_Validator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControlSummary = new System.Windows.Forms.TabControl();
            this.Summary = new System.Windows.Forms.TabPage();
            this.tabPageDetailed = new System.Windows.Forms.TabPage();
            this.dvtkWebBrowserSummary = new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew();
            this.dvtkWebBrowserDetail = new DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew();
            this.menuStrip1.SuspendLayout();
            this.tabControlSummary.SuspendLayout();
            this.Summary.SuspendLayout();
            this.tabPageDetailed.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(759, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openFileToolStripMenuItem
            // 
            this.openFileToolStripMenuItem.Name = "openFileToolStripMenuItem";
            this.openFileToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openFileToolStripMenuItem.Text = "Open File";
            this.openFileToolStripMenuItem.Click += new System.EventHandler(this.openFileToolStripMenuItem_Click);
            // 
            // tabControlSummary
            // 
            this.tabControlSummary.Controls.Add(this.Summary);
            this.tabControlSummary.Controls.Add(this.tabPageDetailed);
            this.tabControlSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlSummary.Location = new System.Drawing.Point(0, 24);
            this.tabControlSummary.Name = "tabControlSummary";
            this.tabControlSummary.SelectedIndex = 0;
            this.tabControlSummary.Size = new System.Drawing.Size(759, 593);
            this.tabControlSummary.TabIndex = 1;
            // 
            // Summary
            // 
            this.Summary.Controls.Add(this.dvtkWebBrowserSummary);
            this.Summary.Location = new System.Drawing.Point(4, 22);
            this.Summary.Name = "Summary";
            this.Summary.Padding = new System.Windows.Forms.Padding(3);
            this.Summary.Size = new System.Drawing.Size(751, 567);
            this.Summary.TabIndex = 0;
            this.Summary.Text = "Summary";
            this.Summary.UseVisualStyleBackColor = true;
            // 
            // tabPageDetailed
            // 
            this.tabPageDetailed.Controls.Add(this.dvtkWebBrowserDetail);
            this.tabPageDetailed.Location = new System.Drawing.Point(4, 22);
            this.tabPageDetailed.Name = "tabPageDetailed";
            this.tabPageDetailed.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDetailed.Size = new System.Drawing.Size(751, 567);
            this.tabPageDetailed.TabIndex = 1;
            this.tabPageDetailed.Text = "Detailed";
            this.tabPageDetailed.UseVisualStyleBackColor = true;
            // 
            // dvtkWebBrowserSummary
            // 
            this.dvtkWebBrowserSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvtkWebBrowserSummary.Location = new System.Drawing.Point(3, 3);
            this.dvtkWebBrowserSummary.Name = "dvtkWebBrowserSummary";
            this.dvtkWebBrowserSummary.Size = new System.Drawing.Size(745, 561);
            this.dvtkWebBrowserSummary.TabIndex = 0;
            this.dvtkWebBrowserSummary.XmlStyleSheetFullFileName = "";
            // 
            // dvtkWebBrowserDetail
            // 
            this.dvtkWebBrowserDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvtkWebBrowserDetail.Location = new System.Drawing.Point(3, 3);
            this.dvtkWebBrowserDetail.Name = "dvtkWebBrowserDetail";
            this.dvtkWebBrowserDetail.Size = new System.Drawing.Size(745, 561);
            this.dvtkWebBrowserDetail.TabIndex = 0;
            this.dvtkWebBrowserDetail.XmlStyleSheetFullFileName = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 617);
            this.Controls.Add(this.tabControlSummary);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Ticket 1400";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControlSummary.ResumeLayout(false);
            this.Summary.ResumeLayout(false);
            this.tabPageDetailed.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControlSummary;
        private System.Windows.Forms.TabPage Summary;
        private DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew dvtkWebBrowserSummary;
        private System.Windows.Forms.TabPage tabPageDetailed;
        private DvtkApplicationLayer.UserInterfaces.DvtkWebBrowserNew dvtkWebBrowserDetail;
    }
}

