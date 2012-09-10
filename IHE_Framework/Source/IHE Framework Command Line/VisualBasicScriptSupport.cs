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
using System.IO;

using DvtkScriptSupport;

namespace DvtIheCmd
{
	/// <summary>
	/// Summary description for VisualBasicScriptSupport.
	/// </summary>
	public class VisualBasicScriptSupport
	{
		public VisualBasicScriptSupport()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public void ExecuteVBScript(System.String scriptFile, System.String[] args)
		{
			Random randomValue = new Random();

			// Create a new script host in which the script will run
			DvtkScriptHost scriptHost = new DvtkScriptHost(
									DvtkScriptSupport.DvtkScriptLanguage.VB,
									"dvtk://session/"+randomValue.Next().ToString(), 
									System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase);

			// Create the event hanlder for handling the compiler error messages
			DvtkScriptSupport.CompilerErrorEventHandler compilerEventHandler = new CompilerErrorEventHandler(scriptHostCompilerErrorEvent);
			scriptHost.CompilerErrorEvent += compilerEventHandler;

			// Set the current active session in the script host
			scriptHost.Session = new Dvtk.Sessions.ScriptSession();

			// Assign the script content to the script host.
			scriptHost.SourceCode = ScriptContent(scriptFile);

			// Compile the script
			if (scriptHost.Compile())
			{
				// Run the script
				scriptHost.Invoke("DvtkIheScript", "DvtIheMain", args);
			}

			// Remove the event hanlder for handling the compiler error messages
			scriptHost.CompilerErrorEvent -= compilerEventHandler;
		}

		private System.String ScriptContent(System.String scriptFile)
		{

			FileInfo fileInfo = new FileInfo(scriptFile);
			if (!fileInfo.Exists)
			{
				// given script file does not exist.
				return "";
			}

			// The script file exists, read and return the content.
			System.IO.StreamReader reader = fileInfo.OpenText();
			System.String scriptContent = reader.ReadToEnd();
			reader.Close();

			return scriptContent;
		}

		private void scriptHostCompilerErrorEvent(object sender, CompilerErrorEventArgs e)
		{
			System.Console.WriteLine(e.LineText);
			System.String message = System.String.Format("Error on line {0}:\t{1}\n", e.Line, e.Description);
			System.Console.WriteLine(message);
		}
	}
}
