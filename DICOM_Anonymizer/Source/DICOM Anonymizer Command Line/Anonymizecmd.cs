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
using System.Collections;
using System.IO;
using System.Windows.Forms;

using DvtkData.Dimse;
using DvtkHighLevelInterface.Common.Threads;
using DvtkHighLevelInterface.Common.Other;
using DvtkHighLevelInterface.Dicom.Files;
using DvtkHighLevelInterface.Dicom.Threads;
using ThreadState = DvtkHighLevelInterface.Common.Threads.ThreadState;
using AnonymizationUtility ;

namespace Anonymizercmd
{
	using HLI = DvtkHighLevelInterface.Dicom.Other;
	using sequenceTag = DvtkData.Dimse.Tag;
	/// <summary>
	/// Summary description for Anonymizecmd.
	/// </summary>
	class Anonymizecmd
	{
		private HLI.DataSet dcmDataset = new HLI.DataSet();
		private static MainThread mainThread = new MainThread();
		private static ThreadManager threadMgr = new ThreadManager();
		private ArrayList allDCMFilesTemp ;
		private Utility utility = new Utility();
		static string  initialDirectory = "c:\\temp\\" + Guid.NewGuid().ToString().Replace("-","p").Substring(1,5);
		static string tempDirectory = initialDirectory + "\\anonymize\\" + System.DateTime.Now.Year + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.TimeOfDay.Hours.ToString("00") + System.DateTime.Now.TimeOfDay.Minutes.ToString("00") + System.DateTime.Now.TimeOfDay.Seconds.ToString("00") ;
		static DirectoryInfo dir = null;
		
		[STAThread]
		static void Main(string[] arguments)
		{
			Anonymizecmd anonymizeCmd = new Anonymizecmd();
			try 
			{
				string inputDirectory = null ;
				mainThread.Initialize(threadMgr);

				// Load the Definition Files
                string definitionDir = Environment.GetEnvironmentVariable("COMMONPROGRAMFILES") + @"\DVTk\Definition Files\DICOM\";
                DirectoryInfo theDefDirectoryInfo = new DirectoryInfo(definitionDir);
				if(theDefDirectoryInfo.Exists)
				{
					FileInfo[] theDefFilesInfo = theDefDirectoryInfo.GetFiles();
					foreach (FileInfo defFile in theDefFilesInfo)
					{
						bool ok = mainThread.Options.LoadDefinitionFile(defFile.FullName);
						if(!ok)
						{
							string theWarningText = string.Format("The Definition file {0} could not be loaded.", defFile.FullName);
							Console.WriteLine(theWarningText);
						}
					}
				}
				
				// Set the Results & Data directory
				dir = new DirectoryInfo(tempDirectory);
				if (!dir.Exists)
				{
					dir.Create();
				}
				mainThread.Options.StorageMode = Dvtk.Sessions.StorageMode.NoStorage;
				mainThread.Options.ResultsDirectory = tempDirectory;
				mainThread.Options.DataDirectory = tempDirectory ;
				string argument = (string)arguments[0];
				Console.WriteLine();
				Console.WriteLine("> Executing Dicom Anonymizer");
				//
				// Initialise dvtk library.
				//
				Dvtk.Setup.Initialize();
				if (argument.StartsWith("-"))
				{
					string optionsString = argument.TrimStart('-');
					optionsString = optionsString.ToLower();
					if ((optionsString == "basicanonymize") || (optionsString == "completeanonymize"))
					{
						DirectoryInfo dirGalaxyInfo = new DirectoryInfo((string)arguments[1]);
						FileSystemInfo[] infos = dirGalaxyInfo.GetFileSystemInfos();
						if(optionsString == "completeanonymize")
						{
							anonymizeCmd.utility.AnonymizationType = false ;
						}
						else
						{
							anonymizeCmd.utility.AnonymizationType = true ;
						}
						if (dirGalaxyInfo.Exists)
						{
							foreach ( FileSystemInfo f in infos)
							{
								if (arguments.Length == 2)
								{
									inputDirectory = arguments[1];
                                    
									//anonymizeCmd.utility.AnonymizationType = true ;
									anonymizeCmd.RetrievingFilesFromDirectory(f.FullName);
								}
								else
								{
									ShowCommandLineArguments();
								}
							}
						}
						else if (optionsString == "h")
						{
							ShowCommandLineArguments();
						}
						else 
						{
							ShowCommandLineArguments();
						}
					}
					else 
					{
						ShowCommandLineArguments();
					}
					Dvtk.Setup.Terminate();
					Directory.Delete(initialDirectory, true);
				}
			}
			catch ( Exception ex)
			{
				Console.WriteLine(ex.Message);
			}			
		}

		private void RetrievingFilesFromDirectory(string inputDirectory )
		{
			try 
			{
				allDCMFilesTemp = new ArrayList();
				FileInfo mediaInputFileInfo = null;
				string mediaFile = inputDirectory;
				mediaInputFileInfo = new FileInfo(mediaFile);
			
				if(mediaFile == "")
				{
					Console.WriteLine("Warning : Provide proper arguments. It is not a proper directory or a DCM File.\n");
					return;
				}
				else if (mediaInputFileInfo.Exists)
				{
					Console.WriteLine(mediaInputFileInfo.FullName);
					DeIdentifactionMethod( mediaInputFileInfo , mediaInputFileInfo.FullName);
				}
				else
				{
					DirectoryInfo theDirectoryInfo = new DirectoryInfo(mediaFile);
					if (!theDirectoryInfo.Exists)
					{
						Console.WriteLine("Error : Input Directory  mentioned does not exists.\n");
					}
					else 
					{
						allDCMFilesTemp = utility.GetFilesRecursively(theDirectoryInfo);
						foreach ( string fileName in allDCMFilesTemp)
						{
							mediaInputFileInfo = new FileInfo(fileName);
							if (mediaInputFileInfo.Exists)
							{
								Console.WriteLine(mediaInputFileInfo.FullName);
								DeIdentifactionMethod( mediaInputFileInfo , mediaInputFileInfo.FullName);
							}
						}
					}
				}
				utility.cleanup();
			}
			catch ( Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		private void DeIdentifactionMethod(FileInfo  fileName , string saveFileNametemp)
		{
			try 
			{
				string _DCMFileName = fileName.FullName ;

				// Read the DCM File
				DicomFile dcmFile = new DicomFile();
				dcmFile.Read(_DCMFileName, mainThread);

				// Get the Data set from the selected DCM file
				dcmDataset = dcmFile.DataSet;
				int numberOfAttribute = dcmDataset.Count ;
				utility.PatientAttributes(dcmDataset);
				for ( int number = 0 ; number<numberOfAttribute ; number++)
				{
					HLI.Attribute attribute =  dcmDataset[number];
					if (attribute.VR == VR.SQ)
					{
						utility.SequenceAttribute_recursive(attribute);
					}
					else 
					{
						utility.CacheAndRepairIdentifyingAttribute(attribute);
						utility.UpdateAnonymizedAttributes(attribute);
					}
				}
				dcmFile.DataSet = dcmDataset ;
				dcmFile.Write(saveFileNametemp);
				
			}
			catch( Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
		private const string commandLineInfo =
			@"
  Usage:
  AnonymizerCmd -h
		 Provides the Help File.
  AnonymizerCmd -basicanonymize
         <InputFileName or Input Directory>
         or
  AnonymizerCmd -completeanonymize
         <InputFileName or Input Directory>\n \n"
			;
		private static void ShowCommandLineArguments()
		{
			Console.Write(commandLineInfo);
		}
	}

	public  class MainThread : DicomThread
	{
		public  MainThread()
		{}

		protected override void Execute()
		{}
	}
}
