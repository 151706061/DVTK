// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Dvt
{
	/// <summary>
	/// Summary description for PrintEmulatorStatusForm.
	/// </summary>
	public class PrintEmulatorStatusForm : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox TextBoxPrinterName;
        private System.Windows.Forms.Button ButtonUpdate;
        private System.Windows.Forms.ComboBox ComboBoxPrinterStatusInfo;
        private System.Windows.Forms.ComboBox ComboBoxPrinterStatus;
        private System.Windows.Forms.DateTimePicker DateTimeCalibrationTime;
        private System.Windows.Forms.DateTimePicker DateTimeCalibrationDate;
        private System.Windows.Forms.TextBox TextBoxManufacturer;
        private System.Windows.Forms.TextBox TextBoxModelName;
        private System.Windows.Forms.TextBox TextBoxSerialNumber;
        private System.Windows.Forms.TextBox TextBoxSoftwareVersions;
        private System.Windows.Forms.Button ButtonClose;
		private System.Windows.Forms.Button Ok;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PrintEmulatorStatusForm(Dvtk.Sessions.EmulatorSession emulator_session)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

            this.session = emulator_session;

            this.TextBoxManufacturer.Text = this.session.Printer.Manufacturer;
            this.TextBoxModelName.Text = this.session.Printer.ManufacturerModelName;
            this.TextBoxPrinterName.Text = this.session.Printer.PrinterName;
            this.TextBoxSerialNumber.Text = this.session.Printer.DeviceSerialNumber;
            this.TextBoxSoftwareVersions.Text = this.session.Printer.SoftwareVersions;
            this.DateTimeCalibrationDate.Value = this.session.Printer.DateOfLastCalibration.Date;
            this.DateTimeCalibrationTime.Value = this.session.Printer.TimeOfLastCalibration.ToLocalTime ();

            // add the 3 possible Printer Status values
            this.ComboBoxPrinterStatus.Items.Add("NORMAL");
            this.ComboBoxPrinterStatus.Items.Add("WARNING");
            this.ComboBoxPrinterStatus.Items.Add("ERROR");
            string status = this.session.Printer.Status.ToString();
            foreach (object o in this.ComboBoxPrinterStatus.Items)
            {
                if (o.ToString() == status) this.ComboBoxPrinterStatus.SelectedItem = o;
            }

            string statusInfo = this.session.Printer.StatusInfo.ToString();
            foreach (string info_dt in this.session.Printer.StatusInfoDefinedTerms)
            {
                this.ComboBoxPrinterStatusInfo.Items.Add (info_dt);
            }
            foreach (object o in this.ComboBoxPrinterStatusInfo.Items)
            {
                if (o.ToString() == statusInfo) this.ComboBoxPrinterStatusInfo.SelectedItem = o;
            }
        }

        private Dvtk.Sessions.EmulatorSession session;

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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ComboBoxPrinterStatusInfo = new System.Windows.Forms.ComboBox();
            this.DateTimeCalibrationTime = new System.Windows.Forms.DateTimePicker();
            this.DateTimeCalibrationDate = new System.Windows.Forms.DateTimePicker();
            this.ComboBoxPrinterStatus = new System.Windows.Forms.ComboBox();
            this.TextBoxPrinterName = new System.Windows.Forms.TextBox();
            this.TextBoxManufacturer = new System.Windows.Forms.TextBox();
            this.TextBoxModelName = new System.Windows.Forms.TextBox();
            this.TextBoxSerialNumber = new System.Windows.Forms.TextBox();
            this.TextBoxSoftwareVersions = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.ButtonUpdate = new System.Windows.Forms.Button();
            this.Ok = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 32);
            this.label1.Name = "label1";
            this.label1.TabIndex = 0;
            this.label1.Text = "Printer Name:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.TabIndex = 0;
            this.label2.Text = "Manufacturer:";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(16, 80);
            this.label6.Name = "label6";
            this.label6.TabIndex = 0;
            this.label6.Text = "Model Name:";
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(16, 104);
            this.label7.Name = "label7";
            this.label7.TabIndex = 0;
            this.label7.Text = "Serial Number:";
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 120);
            this.label8.Name = "label8";
            this.label8.TabIndex = 0;
            this.label8.Text = "Printer Status:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(336, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "Calibration Date:";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(336, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Software Version(s):";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(328, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(112, 23);
            this.label9.TabIndex = 0;
            this.label9.Text = "Printer Status Info:";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(336, 80);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 23);
            this.label10.TabIndex = 0;
            this.label10.Text = "Calibration Time:";
            // 
            // ComboBoxPrinterStatusInfo
            // 
            this.ComboBoxPrinterStatusInfo.Enabled = false;
            this.ComboBoxPrinterStatusInfo.Location = new System.Drawing.Point(440, 120);
            this.ComboBoxPrinterStatusInfo.Name = "ComboBoxPrinterStatusInfo";
            this.ComboBoxPrinterStatusInfo.Size = new System.Drawing.Size(200, 21);
            this.ComboBoxPrinterStatusInfo.TabIndex = 1;
            // 
            // DateTimeCalibrationTime
            // 
            this.DateTimeCalibrationTime.Enabled = false;
            this.DateTimeCalibrationTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.DateTimeCalibrationTime.Location = new System.Drawing.Point(448, 80);
            this.DateTimeCalibrationTime.Name = "DateTimeCalibrationTime";
            this.DateTimeCalibrationTime.TabIndex = 2;
            // 
            // DateTimeCalibrationDate
            // 
            this.DateTimeCalibrationDate.Enabled = false;
            this.DateTimeCalibrationDate.Location = new System.Drawing.Point(448, 56);
            this.DateTimeCalibrationDate.Name = "DateTimeCalibrationDate";
            this.DateTimeCalibrationDate.TabIndex = 2;
            // 
            // ComboBoxPrinterStatus
            // 
            this.ComboBoxPrinterStatus.Location = new System.Drawing.Point(112, 120);
            this.ComboBoxPrinterStatus.Name = "ComboBoxPrinterStatus";
            this.ComboBoxPrinterStatus.Size = new System.Drawing.Size(200, 21);
            this.ComboBoxPrinterStatus.TabIndex = 1;
            this.ComboBoxPrinterStatus.SelectedIndexChanged += new System.EventHandler(this.ComboBoxPrinterStatus_SelectedIndexChanged);
            // 
            // TextBoxPrinterName
            // 
            this.TextBoxPrinterName.Enabled = false;
            this.TextBoxPrinterName.Location = new System.Drawing.Point(120, 32);
            this.TextBoxPrinterName.Name = "TextBoxPrinterName";
            this.TextBoxPrinterName.Size = new System.Drawing.Size(200, 20);
            this.TextBoxPrinterName.TabIndex = 3;
            this.TextBoxPrinterName.Text = "";
            // 
            // TextBoxManufacturer
            // 
            this.TextBoxManufacturer.Enabled = false;
            this.TextBoxManufacturer.Location = new System.Drawing.Point(120, 56);
            this.TextBoxManufacturer.Name = "TextBoxManufacturer";
            this.TextBoxManufacturer.Size = new System.Drawing.Size(200, 20);
            this.TextBoxManufacturer.TabIndex = 3;
            this.TextBoxManufacturer.Text = "";
            // 
            // TextBoxModelName
            // 
            this.TextBoxModelName.Enabled = false;
            this.TextBoxModelName.Location = new System.Drawing.Point(120, 80);
            this.TextBoxModelName.Name = "TextBoxModelName";
            this.TextBoxModelName.Size = new System.Drawing.Size(200, 20);
            this.TextBoxModelName.TabIndex = 3;
            this.TextBoxModelName.Text = "";
            // 
            // TextBoxSerialNumber
            // 
            this.TextBoxSerialNumber.Enabled = false;
            this.TextBoxSerialNumber.Location = new System.Drawing.Point(120, 104);
            this.TextBoxSerialNumber.Name = "TextBoxSerialNumber";
            this.TextBoxSerialNumber.Size = new System.Drawing.Size(200, 20);
            this.TextBoxSerialNumber.TabIndex = 3;
            this.TextBoxSerialNumber.Text = "";
            // 
            // TextBoxSoftwareVersions
            // 
            this.TextBoxSoftwareVersions.Enabled = false;
            this.TextBoxSoftwareVersions.Location = new System.Drawing.Point(448, 32);
            this.TextBoxSoftwareVersions.Name = "TextBoxSoftwareVersions";
            this.TextBoxSoftwareVersions.Size = new System.Drawing.Size(200, 20);
            this.TextBoxSoftwareVersions.TabIndex = 3;
            this.TextBoxSoftwareVersions.Text = "";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ComboBoxPrinterStatus);
            this.groupBox1.Controls.Add(this.ComboBoxPrinterStatusInfo);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(648, 152);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Printer";
            // 
            // ButtonClose
            // 
            this.ButtonClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonClose.Location = new System.Drawing.Point(552, 168);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(96, 23);
            this.ButtonClose.TabIndex = 5;
            this.ButtonClose.Text = "Close";
            // 
            // ButtonUpdate
            // 
            this.ButtonUpdate.Location = new System.Drawing.Point(432, 168);
            this.ButtonUpdate.Name = "ButtonUpdate";
            this.ButtonUpdate.Size = new System.Drawing.Size(112, 23);
            this.ButtonUpdate.TabIndex = 5;
            this.ButtonUpdate.Text = "Send Status Event";
            this.ButtonUpdate.Click += new System.EventHandler(this.ButtonUpdate_Click);
            // 
            // Ok
            // 
            this.Ok.Location = new System.Drawing.Point(328, 168);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(96, 23);
            this.Ok.TabIndex = 6;
            this.Ok.Text = "OK";
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // PrintEmulatorStatusForm
            // 
            this.AcceptButton = this.ButtonClose;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.ButtonClose;
            this.ClientSize = new System.Drawing.Size(664, 200);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.ButtonClose);
            this.Controls.Add(this.TextBoxPrinterName);
            this.Controls.Add(this.DateTimeCalibrationTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.DateTimeCalibrationDate);
            this.Controls.Add(this.TextBoxManufacturer);
            this.Controls.Add(this.TextBoxModelName);
            this.Controls.Add(this.TextBoxSerialNumber);
            this.Controls.Add(this.TextBoxSoftwareVersions);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ButtonUpdate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PrintEmulatorStatusForm";
            this.ShowInTaskbar = false;
            this.Text = "Print Emulator Status";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }
		#endregion

        private void ComboBoxPrinterStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (
                this.ComboBoxPrinterStatus.SelectedItem.ToString() == "NORMAL" ||
                this.session.Printer.StatusInfoDefinedTerms.Length == 0
                )
            {
                this.ComboBoxPrinterStatusInfo.Enabled = false;
                this.ComboBoxPrinterStatusInfo.SelectedItem = null;
            }
            else
                this.ComboBoxPrinterStatusInfo.Enabled = true;
        }

        private void ButtonUpdate_Click(object sender, System.EventArgs e)
        {
            string selectedPrinterStatusInfo;
            if (!this.ComboBoxPrinterStatusInfo.Enabled)
            {
                selectedPrinterStatusInfo = string.Empty;
            }
            else
            {
                if (this.ComboBoxPrinterStatusInfo.SelectedItem != null)
                {
                    selectedPrinterStatusInfo =
                        this.ComboBoxPrinterStatusInfo.SelectedItem.ToString();
                }
                else selectedPrinterStatusInfo = string.Empty;
            }
            switch (this.ComboBoxPrinterStatus.SelectedIndex)
            {
                case 0:
                    this.session.Printer.ApplyStatus (Dvtk.Sessions.PrinterStatus.NORMAL, selectedPrinterStatusInfo, true);
                    break;
                case 1:
                    this.session.Printer.ApplyStatus (Dvtk.Sessions.PrinterStatus.WARNING, selectedPrinterStatusInfo, true);
                    break;
                case 2:
                    this.session.Printer.ApplyStatus (Dvtk.Sessions.PrinterStatus.FAILURE, selectedPrinterStatusInfo, true);
                    break;
                default:
                    break;
            }
        }

		private void Ok_Click(object sender, System.EventArgs e)
		{
			string selectedPrinterStatusInfo;
			bool sendStatusEvent = false;
			if (!this.ComboBoxPrinterStatusInfo.Enabled)
			{
				selectedPrinterStatusInfo = string.Empty;
			}
			else
			{
				if (this.ComboBoxPrinterStatusInfo.SelectedItem != null)
				{
					selectedPrinterStatusInfo =
						this.ComboBoxPrinterStatusInfo.SelectedItem.ToString();
				}
				else selectedPrinterStatusInfo = string.Empty;
			}
			switch (this.ComboBoxPrinterStatus.SelectedIndex)
			{
				case 0:
					this.session.Printer.ApplyStatus (Dvtk.Sessions.PrinterStatus.NORMAL, selectedPrinterStatusInfo, sendStatusEvent);
					break;
				case 1:
					this.session.Printer.ApplyStatus (Dvtk.Sessions.PrinterStatus.WARNING, selectedPrinterStatusInfo, sendStatusEvent);
					break;
				case 2:
					this.session.Printer.ApplyStatus (Dvtk.Sessions.PrinterStatus.FAILURE, selectedPrinterStatusInfo, sendStatusEvent);
					break;
				default:
					break;
			}
			this.Close();
		}
	}
}
