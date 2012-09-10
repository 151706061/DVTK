// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Threading;
using System.Windows.Forms;

namespace Dvt
{ 
	/// <summary>
	/// The Error Handler class
	/// We need a class because event handling methods can't be static.
	/// </summary>
	internal class CustomExceptionHandler 
    {
		/// <summary>
		/// Handle the exception event.
		/// </summary>
		/// <param name="sender">The sender of the Exception.</param>
		/// <param name="t">The Exception event arguments.</param>
		public void OnThreadException(object sender, ThreadExceptionEventArgs t) 
        {
			if (showExceptions)
			{
				DialogResult result = DialogResult.Cancel;
				try 
				{
					result = this.ShowThreadExceptionDialog(t.Exception);
				}
				catch 
				{
					try 
					{
						MessageBox.Show("Fatal Error",
							"Fatal Error",
							MessageBoxButtons.AbortRetryIgnore,
							MessageBoxIcon.Stop);
					}
					finally 
					{
						Application.Exit();
					}
				}
				if (result == DialogResult.Abort)
					Application.Exit();
			}
        }

		/// <summary>
		/// The handler of the unhandled exception.
		/// </summary>
		/// <param name="sender">The sender of the Exception.</param>
		/// <param name="e">The Exception event arguments.</param>
        public void OnAppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
			if (showExceptions)
			{
				DialogResult result = DialogResult.Cancel;
				try 
				{
					result = this.ShowThreadExceptionDialog(e.ExceptionObject as Exception);
				}
				catch 
				{
					try 
					{
						MessageBox.Show("Fatal Error",
							"Fatal Error",
							MessageBoxButtons.AbortRetryIgnore,
							MessageBoxIcon.Stop);
					}
					finally 
					{
						Application.Exit();
					}
				}
				if (result == DialogResult.Abort)
					Application.Exit();
			}
        }

		/// <summary>
		/// The simple dialog that is displayed when this class catches and exception.
		/// </summary>
		/// <param name="e">The exception.</param>
		/// <returns>The dialog result.</returns>
        private DialogResult ShowThreadExceptionDialog(Exception e) 
        {
            string errorMsg = "An error occurred please contact the administrator with" +
                " the following information:\n\n";
            errorMsg += e.Message + "\n\nStack Trace:\n" + e.StackTrace;
            return MessageBox.Show(errorMsg,
                "Application Error",
                MessageBoxButtons.AbortRetryIgnore,
                MessageBoxIcon.Stop);
        }

		private bool showExceptions = true;

		public bool ShowExceptions
		{
			set
			{
				showExceptions = value;
			}
		}
    }
}
