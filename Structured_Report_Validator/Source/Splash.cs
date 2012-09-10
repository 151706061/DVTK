using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Dvtk.StructuredReportValidator
{
    public partial class Splash : Form
    {
        #region Properties
        /// <summary>
        ///     Get or set the status message displayed in the splash screen.
        /// </summary>
        public string StatusMessage
        {
            get
            {
                return labelStatus.Text;
            }
            set
            {
                labelStatus.Text = value;
            }
        }

        /// <summary>
        ///     Get or set the progress bar value. When set to 0 the progressbar becomes hidden.
        /// </summary>
        public int Progress
        {
            get
            {
                return progressBar.Value;
            }
            set
            {
                if (value == 0)
                {
                    progressBar.Visible = false;
                }
                else
                {
                    progressBar.Visible = true;
                }
                progressBar.Value = value;
            }
        }
        #endregion

        #region Public Functions
        /// <summary>
        ///     Create a new instance of the <see cref="Splash"/ > class.
        /// </summary>
        public Splash()
        {
            InitializeComponent();
        }
        #endregion
    }
}