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
using System.Xml;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.InformationModel;
using DvtkData.Dimse;
using Dvtk.Hl7.Messages;
using Dvtk.IheActors.Bases;
using Dvtk.IheActors.Actors;
using Dvtk.IheActors.Dicom;
using Dvtk.IheActors.Hl7;
using Dvtk.IheActors.IheFramework;

namespace DvtIheCmd
{
	/// <summary>
	/// Summary description for DvtIheCmd.
	/// </summary>
	class DvtIheCmd
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			try
			{
				//
				// Initialise dvtk library.
				//
				Dvtk.Setup.Initialize();

				if (args.GetLength(0) == 2)
				{
					// check file existance - config file
					if (CheckFileExits(args[0]) == false) return;

					// check file existance - script file
					if (CheckFileExits(args[1]) == false) return;

					// second argument is a VBScript
					VisualBasicScriptSupport visualBasicScriptSupport = new VisualBasicScriptSupport();
					visualBasicScriptSupport.ExecuteVBScript(args[1], args);
				}
				else
				{
					// check file existance - config file
					if (CheckFileExits(args[0]) == false) return;

					//
                    // Get a Scheduled Workflow ihe framework
					//
                    IheFramework iheFramework = new IheFramework("Scheduled Workflow");

					//
                    // Configure the ihe framework
					//
                    iheFramework.Config.Load(args[0]);

					//
					// Apply the Configuration to actors
					//
                    iheFramework.ApplyConfig();

					//
					// Open the results after loading the configuration so that the results directory is defined
					//
                    iheFramework.OpenResults();

					//
					// Add filters (as Tag Values) to the Comparator
					//
					// Universal Match on these DICOM Tags
                    //			iheFramework.AddComparisonTagValueFilter(new DicomTagValue(Tag.PATIENT_ID));
                    //			iheFramework.AddComparisonTagValueFilter(new DicomTagValue(Tag.ACCESSION_NUMBER));

					// Single Value Match on these DICOM Tags
                    //			iheFramework.AddComparisonTagValueFilter(new DicomTagValue(Tag.PATIENTS_BIRTH_DATE, "19000101"));
                    //			iheFramework.AddComparisonTagValueFilter(new DicomTagValue(Tag.REQUESTED_PROCEDURE_ID, "RPQ2"));

					//
                    // Start the ihe framework test
					//
                    iheFramework.StartTest();

					Console.WriteLine("");
					Console.WriteLine("Press enter to stop...");
					Console.ReadLine();

					//
                    // Stop the ihe framework test
					//
                    iheFramework.StopTest();

					//
					// Evaluate the results
					//
                    iheFramework.EvaluateTest();
                    iheFramework.CleanUpCurrentWorkingDirectory();

					//
					// Close the results and get the final results filename
					//
                    System.String resultsFilename = iheFramework.CloseResults();

					//
					// Invoke IE to display the results file
					//
					System.Diagnostics.Process process = new System.Diagnostics.Process();
					process.StartInfo.FileName = resultsFilename;
					process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
					process.Start();	
				}

				//
				// Terminate the setup
				//
				Dvtk.Setup.Terminate();
			}
			catch(System.Exception e)
			{
				Console.WriteLine("{0} - {1}", e.Message, e.StackTrace);
			}
		}

		static private bool CheckFileExits(System.String filename)
		{
			bool exists = true;

			FileInfo fileInfo = new FileInfo(filename);
			if (fileInfo.Exists == false)
			{
				Console.WriteLine("File: \"{0}\" not found.", filename);
				exists = false;
			}

			return exists;
		}
	}
}
