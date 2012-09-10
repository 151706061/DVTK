// ------------------------------------------------------
// DVTk - The Healthcare Validation Toolkit (www.dvtk.org)
// Copyright © 2010 DVTk
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
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace Dvtk.StructuredReportValidator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if !DEBUG
			try
			{
#endif
            // Initialize the Dvtk library
            Dvtk.Setup.Initialize();

            //// Checks the version of both the application and the DVTk library.
            //// If one or both are a non-official or Alpha version, a warning message box is displayed.
            //DvtkApplicationLayer.VersionChecker.CheckVersion();

            Application.Run(new Main());

            // Terminate the Dvtk library
            Dvtk.Setup.Terminate();

#if !DEBUG
			}
			catch(Exception exception)
			{
                DvtkApplicationLayer.CustomExceptionHandler.ShowThreadExceptionDialog(exception);
			}
#endif

        }
    }
}