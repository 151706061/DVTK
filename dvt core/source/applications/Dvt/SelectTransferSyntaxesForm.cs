// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Dvtk;
using DvtkData;

namespace Dvt
{
	/// <summary>
	/// Summary description for SelectTransferSyntaxesForm.
	/// </summary>
	public class SelectTransferSyntaxesForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button ButtonOk;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.TreeView TreeViewSTS;
        private System.Windows.Forms.Label LabelSelectTS;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public bool _SelectionChanged = false;	

		public SelectTransferSyntaxesForm(Dvtk.Sessions.EmulatorSession session)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this._session = session;
            ts_list = new ArrayList();
            ts_list.Add (DvtkData.Dul.TransferSyntax.Implicit_VR_Little_Endian);
            ts_list.Add (DvtkData.Dul.TransferSyntax.Explicit_VR_Big_Endian);
            ts_list.Add (DvtkData.Dul.TransferSyntax.Explicit_VR_Little_Endian);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Baseline_Process_1);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Extended_Hierarchical_16_And_18);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Extended_Hierarchical_17_And_19);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Extended_Process_2_And_4);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Extended_Process_3_And_5);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Full_Progression_Hierarchical_24_And_26);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Full_Progression_Hierarchical_25_And_27);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Full_Progression_Non_Hierarchical_10_And_12);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Full_Progression_Non_Hierarchical_11_And_13);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Lossless_Hierarchical_28);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Lossless_Hierarchical_29);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Lossless_Non_Hierarchical_14);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Lossless_Non_Hierarchical_15);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Lossless_Non_Hierarchical_1st_Order_Prediction);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_LS_Lossless_Image_Compression);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_LS_Lossy_Image_Compression);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_2000_IC_Lossless_Only);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_2000_IC);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Spectral_Selection_Hierarchical_20_And_22);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Spectral_Selection_Hierarchical_21_And_23);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Spectral_Selection_Non_Hierarchical_6_And_8);
            ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_Spectral_Selection_Non_Hierarchical_7_And_9);
			ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_2000_Multicomponent_lossless2);
			ts_list.Add (DvtkData.Dul.TransferSyntax.JPEG_2000_Multicomponent2);
			ts_list.Add (DvtkData.Dul.TransferSyntax.JPIP_Referenced);
			ts_list.Add (DvtkData.Dul.TransferSyntax.JPIP_Referenced_Deflate);
			ts_list.Add (DvtkData.Dul.TransferSyntax.MPEG2_Main_Profile_Level);
			ts_list.Add (DvtkData.Dul.TransferSyntax.RFC_2557_Mime_Encapsulation);
			ts_list.Add (DvtkData.Dul.TransferSyntax.RLE_Lossless);
			ts_list.Add (DvtkData.Dul.TransferSyntax.Deflated_Explicit_VR_Little_Endian);

            foreach (DvtkData.Dul.TransferSyntax ts in ts_list)
            {
                TreeNode node = this.TreeViewSTS.Nodes.Add (ts.ToString());
                // Set the item to 'checked' if the transfer syntax is supported by the emulator
                if (this._session.SupportedTransferSyntaxSettings.SupportedTransferSyntaxes.Contains (ts))
                {
                    node.Checked = true;
                }
            }
        }

        private Dvtk.Sessions.EmulatorSession _session;

        private ArrayList ts_list;

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
            this.ButtonOk = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.TreeViewSTS = new System.Windows.Forms.TreeView();
            this.LabelSelectTS = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonOk
            // 
            this.ButtonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonOk.Location = new System.Drawing.Point(200, 182);
            this.ButtonOk.Name = "ButtonOk";
            this.ButtonOk.TabIndex = 0;
            this.ButtonOk.Text = "OK";
            this.ButtonOk.Click += new System.EventHandler(this.ButtonOk_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(288, 182);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.TabIndex = 0;
            this.ButtonCancel.Text = "Cancel";
            // 
            // TreeViewSTS
            // 
            this.TreeViewSTS.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.TreeViewSTS.CheckBoxes = true;
            this.TreeViewSTS.ImageIndex = -1;
            this.TreeViewSTS.Location = new System.Drawing.Point(16, 48);
            this.TreeViewSTS.Name = "TreeViewSTS";
            this.TreeViewSTS.SelectedImageIndex = -1;
            this.TreeViewSTS.ShowLines = false;
            this.TreeViewSTS.ShowPlusMinus = false;
            this.TreeViewSTS.ShowRootLines = false;
            this.TreeViewSTS.Size = new System.Drawing.Size(352, 118);
            this.TreeViewSTS.TabIndex = 2;
            // 
            // LabelSelectTS
            // 
            this.LabelSelectTS.Location = new System.Drawing.Point(16, 16);
            this.LabelSelectTS.Name = "LabelSelectTS";
            this.LabelSelectTS.Size = new System.Drawing.Size(368, 23);
            this.LabelSelectTS.TabIndex = 3;
            this.LabelSelectTS.Text = "Select the Transfer Syntaxes you want to be supported by the emulator:";
            // 
            // SelectTransferSyntaxesForm
            // 
            this.AcceptButton = this.ButtonOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(384, 222);
            this.Controls.Add(this.LabelSelectTS);
            this.Controls.Add(this.TreeViewSTS);
            this.Controls.Add(this.ButtonOk);
            this.Controls.Add(this.ButtonCancel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(392, 168);
            this.Name = "SelectTransferSyntaxesForm";
            this.ShowInTaskbar = false;
            this.Text = "Specify Supported Transfer Syntaxes";
            this.ResumeLayout(false);

        }
		#endregion

        private void ButtonOk_Click(object sender, System.EventArgs e)
        {
            this._session.SupportedTransferSyntaxSettings.SupportedTransferSyntaxes.Clear();
            foreach (DvtkData.Dul.TransferSyntax ts in ts_list)
            {
                foreach (TreeNode node in this.TreeViewSTS.Nodes)
                {
                    if ((ts.ToString() == node.Text) && (node.Checked))
                        this._session.SupportedTransferSyntaxSettings.SupportedTransferSyntaxes.Add (ts);
                }
            }
			
			_SelectionChanged = true;

            this.Close ();
        }
	}
}
