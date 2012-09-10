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

namespace Anonymizer
{
	/// <summary>
	/// Summary description for SelectMode.
	/// </summary>
	public class SelectMode : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton radioButtonBasic;
		private System.Windows.Forms.RadioButton radioButtonComplete;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton radioButtonFile;
		private System.Windows.Forms.RadioButton radioButtonDir;
		private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SelectMode()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SelectMode));
			this.radioButtonBasic = new System.Windows.Forms.RadioButton();
			this.radioButtonComplete = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.radioButtonFile = new System.Windows.Forms.RadioButton();
			this.radioButtonDir = new System.Windows.Forms.RadioButton();
			this.buttonOk = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// radioButtonBasic
			// 
			this.radioButtonBasic.Checked = true;
			this.radioButtonBasic.Location = new System.Drawing.Point(16, 24);
			this.radioButtonBasic.Name = "radioButtonBasic";
			this.radioButtonBasic.Size = new System.Drawing.Size(80, 24);
			this.radioButtonBasic.TabIndex = 0;
			this.radioButtonBasic.TabStop = true;
			this.radioButtonBasic.Text = "Basic";
			// 
			// radioButtonComplete
			// 
			this.radioButtonComplete.Location = new System.Drawing.Point(104, 24);
			this.radioButtonComplete.Name = "radioButtonComplete";
			this.radioButtonComplete.Size = new System.Drawing.Size(80, 24);
			this.radioButtonComplete.TabIndex = 1;
			this.radioButtonComplete.Text = "Complete";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.radioButtonBasic);
			this.groupBox1.Controls.Add(this.radioButtonComplete);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(192, 64);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Anonymization mode";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.radioButtonFile);
			this.groupBox2.Controls.Add(this.radioButtonDir);
			this.groupBox2.Location = new System.Drawing.Point(8, 80);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(192, 64);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Anonymize";
			// 
			// radioButtonFile
			// 
			this.radioButtonFile.Checked = true;
			this.radioButtonFile.Location = new System.Drawing.Point(16, 24);
			this.radioButtonFile.Name = "radioButtonFile";
			this.radioButtonFile.Size = new System.Drawing.Size(80, 24);
			this.radioButtonFile.TabIndex = 0;
			this.radioButtonFile.TabStop = true;
			this.radioButtonFile.Text = "File";
			// 
			// radioButtonDir
			// 
			this.radioButtonDir.Location = new System.Drawing.Point(104, 24);
			this.radioButtonDir.Name = "radioButtonDir";
			this.radioButtonDir.Size = new System.Drawing.Size(80, 24);
			this.radioButtonDir.TabIndex = 1;
			this.radioButtonDir.Text = "Directory";
			// 
			// buttonOk
			// 
			this.buttonOk.Location = new System.Drawing.Point(64, 160);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.TabIndex = 4;
			this.buttonOk.Text = "OK";
			this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
			// 
			// SelectMode
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(210, 192);
			this.Controls.Add(this.buttonOk);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectMode";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Mode";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void buttonOk_Click(object sender, System.EventArgs e)
		{
			this.Close();
			this.DialogResult = DialogResult.OK;
		}

		public bool AnonymizeMode
		{
			get
			{
				if((radioButtonBasic.Checked) ||(!radioButtonComplete.Checked))
					return true;
				else
					return false;
			}
		}

		public bool AnonymizeFile
		{
			get
			{
				if((radioButtonFile.Checked) || (!radioButtonDir.Checked))
					return true;
				else
					return false;
			}
		}
	}
}
