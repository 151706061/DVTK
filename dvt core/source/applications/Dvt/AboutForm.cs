// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright (C) 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using Dvtk;

namespace Dvt
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.Label TextArea;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.TextArea = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(16, 16);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // ButtonOK
            // 
            this.ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonOK.Location = new System.Drawing.Point(312, 56);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.TabIndex = 3;
            this.ButtonOK.Text = "OK";
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // TextArea
            // 
            this.TextArea.Location = new System.Drawing.Point(56, 16);
            this.TextArea.Name = "TextArea";
            this.TextArea.Size = new System.Drawing.Size(248, 104);
            this.TextArea.TabIndex = 4;
            this.TextArea.Text = "TextArea";
            // 
            // AboutForm
            // 
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.ButtonOK;
            this.ClientSize = new System.Drawing.Size(402, 136);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TextArea);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm";
            this.ShowInTaskbar = false;
            this.Text = "AboutTitle";
            this.Load += new System.EventHandler(this.AboutForm_Load);
            this.ResumeLayout(false);

        }
		#endregion

        private void AboutForm_Load(object sender, System.EventArgs e)
        {
            /*
            Icon i			= Owner.Icon;
            this.Icon		= i;
            IconBox.Image	= i.ToBitmap();
            */

            Assembly ThisAssembly = Assembly.GetExecutingAssembly();

            AssemblyName ThisAssemblyName = ThisAssembly.GetName();

            string FriendlyVersion = 
                string.Format(
                "{0:D}.{1:D}.{2:D}" ,
                //                "{0:D}.{1:D}.{2:D}.{3:D}" ,
                ThisAssemblyName.Version.Major ,
                ThisAssemblyName.Version.Minor,
                ThisAssemblyName.Version.Build/*,*/
                //ThisAssemblyName.Version.Revision
                );

            Array Attributes = ThisAssembly.GetCustomAttributes( false );

            string Title	= string.Empty;
            string Copyright = string.Empty;
            foreach ( object o in Attributes )
            {
                if ( o is AssemblyTitleAttribute )
                {
                    Title = ((AssemblyTitleAttribute)o).Title;
                }
                else if ( o is AssemblyCopyrightAttribute )
                {
                    Copyright = ((AssemblyCopyrightAttribute)o).Copyright;
                }
            }

            this.Text = string.Format("About {0}", Title);

            System.Text.StringBuilder sb = new System.Text.StringBuilder("");

            sb.AppendFormat(
                "{0}\n"+
                "Version {1}\n"+
                "\n"+
                "DVT uses OpenSLL for secure communication\n"+
                "- see DVT User Guide for license details.\n"+
                "\n"+
                "{2}", Title, FriendlyVersion, Copyright);

            TextArea.Text = sb.ToString() ;
        }

        private void ButtonOK_Click(object sender, System.EventArgs e)
        {
            this.Close ();
        }
	}
}
