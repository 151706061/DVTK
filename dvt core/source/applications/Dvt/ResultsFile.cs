// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.IO;
using System.Diagnostics;
using DvtTreeNodeTag;
using System.Windows.Forms;

namespace Dvt
{
	/// <summary>
	/// 
	/// </summary>
	public class ResultsFile
	{
		const string _SUMMARY_PREFIX = "summary_";
		const string _DETAIL_PREFIX = "detail_";


		private ResultsFile()
		{
			// 
			// TODO: Add constructor logic here
			//
		}

		private static void BackupFile(Dvtk.Sessions.Session theSession, string theFileToBackup)
		{
			string theSourceFullFileName = System.IO.Path.Combine(theSession.ResultsRootDirectory, theFileToBackup);
			string theDestinyFullFileName = theSourceFullFileName + "_backup";
			int theCounter = 1;

			try
			{
				while (File.Exists(theDestinyFullFileName + theCounter.ToString()))
				{
					theCounter++;
				}

				File.Copy(theSourceFullFileName, theDestinyFullFileName + theCounter.ToString());
			}
			catch
			{
				// Don't do anything in the release version.
				Debug.Assert(false);
			}
		}

		public static void BackupFiles(Dvtk.Sessions.Session theSession, ArrayList theFilesToBackup)
		{
			foreach (string theFileToBackup in theFilesToBackup)
			{
				BackupFile(theSession, theFileToBackup);
			}
		}

		/// <summary>
		/// Remove .xml files, and if exisiting, the corresponding .html files.
		/// </summary>
		/// <param name="theSession">The session.</param>
		/// <param name="theResultsFileNames">The results file names.</param>
		public static void Remove(Dvtk.Sessions.Session theSession, ArrayList theResultsFileNames)
		{
			foreach(string theResultsFileName in theResultsFileNames)
			{
				string theXmlResultsFullFileName = System.IO.Path.Combine(theSession.ResultsRootDirectory, theResultsFileName);
				string theHtmlResultsFullFileName = theXmlResultsFullFileName.ToLower().Replace(".xml", ".html");

				if (!File.Exists(theXmlResultsFullFileName))
				{
					// Sanity check.
					Debug.Assert(false);
				}
				else
				{
					try
					{
						File.Delete(theXmlResultsFullFileName);
					}
					catch
					{
						// In release mode, just continue.
						Debug.Assert(false);
					}
				}

				if (File.Exists(theHtmlResultsFullFileName))
				{
					try
					{
						File.Delete(theHtmlResultsFullFileName);
					}
					catch
					{
						// In release mode, just continue.
						Debug.Assert(false);
					}
				}
			}
		}

		/// <summary>
		/// From a list of results file names, return those results file names with the same session ID
		/// as the session ID of the supplied session.
		/// </summary>
		/// <param name="theSession">The session.</param>
		/// <param name="theResultsFileNames">Valid results file names.</param>
		/// <returns>List of results file names.</returns>
		public static ArrayList GetNamesForCurrentSessionId(Dvtk.Sessions.Session theSession, ArrayList theResultsFileNames)
		{
			ArrayList theNamesForCurrentSession = new ArrayList();
			string theSessionIdAsString = theSession.SessionId.ToString("000");

			foreach(string theResultsFileName in theResultsFileNames)
			{
				string theResultsFileSessionIdAsString = "";

				if (theResultsFileName.ToLower().StartsWith(_SUMMARY_PREFIX))
				{
					theResultsFileSessionIdAsString = theResultsFileName.Substring(_SUMMARY_PREFIX.Length, 3);
				}
				else if (theResultsFileName.ToLower().StartsWith(_DETAIL_PREFIX))
				{
					theResultsFileSessionIdAsString = theResultsFileName.Substring(_DETAIL_PREFIX.Length, 3);
				}
				else
				{
					// Sanity check.
					Debug.Assert(false);
				}
	
				if (theResultsFileSessionIdAsString == theSessionIdAsString)
				{
					theNamesForCurrentSession.Add(theResultsFileName);
				}
			}

			return(theNamesForCurrentSession);
		}


		private static ArrayList GetVisibleNames(ArrayList theResultsFileNames)
		{
			ArrayList theVisibleResultsFileNames = new ArrayList();

			foreach (string theResultsFileName in theResultsFileNames)
			{
				if (IsVisible(theResultsFileName))
				{
					theVisibleResultsFileNames.Add(theResultsFileName);
				}
			}

			return(theVisibleResultsFileNames);
		}

		/// <summary>
		/// Get the results file names of the files that need to be shown (in)directly under the tree node indicated by the tree node tag.
		/// Only valid results file names will be supplied by this method.
		/// </summary>
		/// <param name="theTreeNodeTag">The tree node tag.</param>
		/// <returns>The results file names to display under the tree node.</returns>
		public static ArrayList GetVisibleNamesForSession(Dvtk.Sessions.Session theSession)
		{
			ArrayList allResultsFileNames = GetAllNamesForSession(theSession);

			return(GetVisibleNames(allResultsFileNames));
		}

		public static ArrayList GetNamesForBaseName(string theBaseName, ArrayList theResultsFileNames)
		{
			ArrayList theNames = new ArrayList();
			string theLowerCaseBaseName = theBaseName.ToLower();

			foreach (string theResultsFileName in theResultsFileNames)
			{
				string theLowerCaseResultsFileBaseName = GetBaseNameNoCheck(theResultsFileName).ToLower();

				if (theLowerCaseBaseName == theLowerCaseResultsFileBaseName)
				{
					theNames.Add(theResultsFileName);
				}
			}

			return theNames;
		}

		/// <summary>
		/// Get the results file names of the files that need to be shown (in)directly under the tree node indicated by the tree node tag.
		/// </summary>
		/// <param name="theTreeNodeTag">The tree node tag.</param>
		/// <param name="theResultsFileNames">Valid results file names to choose from.</param>
		/// <returns>The results file names to display under the tree node.</returns>
		public static ArrayList GetNamesForScriptFile(string theScriptFileName, ArrayList theResultsFileNames)
		{
			ArrayList theNames = new ArrayList();

			string theLowerCaseScriptFileBaseName = GetBaseNameForScriptFile(theScriptFileName).ToLower();

			foreach (string theResultsFileName in theResultsFileNames)
			{
				string theLowerCaseResultsFileName = theResultsFileName.ToLower();
				string theLowerCaseResultsFileBaseName = GetBaseNameNoCheck(theLowerCaseResultsFileName);

				if ( (theLowerCaseResultsFileBaseName.IndexOf(theLowerCaseScriptFileBaseName) != -1) &&
					(theLowerCaseResultsFileName.IndexOf("_" + theLowerCaseScriptFileBaseName + "_") != -1)
					)
				{
					theNames.Add(theResultsFileName);
				}
			}

			return theNames;
		}

		public static ArrayList GetAllNamesForSession(Dvtk.Sessions.Session theSession)
		{
			ArrayList theResultsFiles = new ArrayList();
			DirectoryInfo theDirectoryInfo;
			FileInfo[] theFilesInfo;

			theDirectoryInfo = new DirectoryInfo (theSession.ResultsRootDirectory);

			if (theDirectoryInfo.Exists)
			{
				theFilesInfo = theDirectoryInfo.GetFiles ("*.xml");

				foreach (FileInfo theFileInfo in theFilesInfo)
				{
					string theResultsFileName = theFileInfo.Name;

					if (IsValid(theResultsFileName))
					{
						theResultsFiles.Add(theResultsFileName);
					}
				}
			}

			return theResultsFiles;
		}

		/// <summary>
		/// Get the base names from a list of results file names.
		/// </summary>
		/// <param name="theResultsFiles">The results file names.</param>
		/// <returns>The unique base names.</returns>
		public static ArrayList GetBaseNamesForResultsFiles(ArrayList theResultsFiles)
		{
			ArrayList theBaseNames = new ArrayList();

			foreach(string theResultsFile in theResultsFiles)
			{
				string theBaseName = GetBaseNameNoCheck(theResultsFile);
				
				if (!theBaseNames.Contains(theBaseName))
				{
					theBaseNames.Add(theBaseName);
				}
			}

			theBaseNames.Sort();

			return(theBaseNames);
		}

		/// <summary>
		/// Returns a boolean indicating if the supplied results file name is a valid one.
		/// </summary>
		/// <param name="theResultsFileName">The results file name.</param>
		/// <returns>Indicating if the supplied results file name is a valid one.</returns>
		private static bool IsValid(string theResultsFileName)
		{
			bool isValidName = true;
			string theBaseName = "";

			isValidName = GetBaseNameForResultsFile(theResultsFileName, ref theBaseName);

			return(isValidName);
		}

		/// <summary>
		/// Converts a results file name to its corresponding base name.
		/// The base name is the result of removing "....._xxx_" at the beginning and removing
		/// "_res?.xml" at the end.
		/// </summary>
		/// <param name="theResultsFileName">The results file name</param>
		/// <param name="theBaseName">The returned base name. If the supplied results file name is invalid, this parameter will be set to "".</param>
		/// <returns>Indicates if the supplied results file name is a valid one.</returns>
		private static bool GetBaseNameForResultsFile(string theResultsFileName, ref string theBaseName)
		{
			bool isValidResultsFileName = true;
			theBaseName = theResultsFileName;

			// Remove the first prefix.
			if (theBaseName.ToLower().StartsWith(_SUMMARY_PREFIX))
			{
				theBaseName = theBaseName.Remove(0, _SUMMARY_PREFIX.Length);
			}
			else if (theBaseName.ToLower().StartsWith(_DETAIL_PREFIX))
			{
				theBaseName = theBaseName.Remove(0, _DETAIL_PREFIX.Length);
			}
			else
			{
				isValidResultsFileName = false;
			}

			// Remove the second prefix: session ID and underscore.
			if (isValidResultsFileName)
			{
				if (theBaseName.Length > 3)
					// Is long enough to contain the substring "xxx_".
				{
					try
					{
						Int16 theInt16 = Convert.ToInt16(theBaseName.Substring(0, 3));
					}
					catch
					{
						isValidResultsFileName = false;
					}

					theBaseName = theBaseName.Substring(3);

					if (theBaseName[0] == '_')
					{	
						theBaseName = theBaseName.Substring(1);
					}
					else
					{
						isValidResultsFileName = false;
					}
				}
				else
				{
					isValidResultsFileName = false;
				}
			}

			// Remove the first postfix: ".xml"
			if (isValidResultsFileName)
			{
				if (theBaseName.ToLower().EndsWith(".xml"))
				{
					theBaseName = theBaseName.Substring(0, theBaseName.Length - 4);
				}
				else
				{
					isValidResultsFileName = false;
				}
			}

			// Remove all digits at the end of the string.
			if (isValidResultsFileName)
			{
				bool continueRemovingDigit = true;

				while (continueRemovingDigit)
				{
					try
					{
						Int16 theInt16 = Convert.ToInt16(theBaseName.Substring(theBaseName.Length - 1));
					}
					catch
					{
						continueRemovingDigit = false;
					}

					if (continueRemovingDigit)
					{
						theBaseName = theBaseName.Substring(0, theBaseName.Length - 1);
					}
				}
			}

			// Remove the "_res" at the end.
			if (isValidResultsFileName)
			{
				if (theBaseName.ToLower().EndsWith("_res"))
				{
					theBaseName = theBaseName.Substring(0, theBaseName.Length - 4);
				}
				else
				{
					isValidResultsFileName = false;
				}
			}
			
			// Base name is only valid if it is not empty.
			if (isValidResultsFileName)
			{
				if (theBaseName.Length == 0)
				{
					isValidResultsFileName = false;
				}
			}

			if (!isValidResultsFileName)
			{
				theBaseName = "";
			}
		
			return(isValidResultsFileName);
		}

		public static string GetBaseNameNoCheck(string theResultsFileName)
		{
			string theBaseName = theResultsFileName;

			// Remove first prefix.
			theBaseName = theBaseName.Substring(theBaseName.IndexOf("_") + 1);

			// Remove second prefix: session ID and '_'.
			theBaseName = theBaseName.Substring(theBaseName.IndexOf("_") + 1);

			// Remove postfix.
			int indexOfLastUnderline = theBaseName.LastIndexOf("_");
			theBaseName = theBaseName.Substring(0, indexOfLastUnderline);

			return(theBaseName);
		}

		/// <summary>
		/// Method indicating if this result file must be shown in the Ui under the Session Tree View.
		/// </summary>
		/// <param name="theResultsFileName">A valid results file name.</param>
		/// <returns>Indicating if this result file must be shown in the Ui under the Session Tree View.</returns>
		private static bool IsVisible(string theResultsFileName)
		{
			return(theResultsFileName.ToLower().EndsWith("_res.xml"));
		}


		private static string GetExpandedName(Dvtk.Sessions.Session theSession, string theString)
		{
			return(theSession.SessionId.ToString("000") + '_' + theString + "_res.xml");
		}

		private static string GetBaseNameForScriptFile(string theScriptFileName)
		{
			return(theScriptFileName.Replace(".", "_"));
		}

		public static string GetExpandedNameForScriptFile(Dvtk.Sessions.Session theSession, string theScriptFileName)
		{
			return(GetExpandedName(theSession, GetBaseNameForScriptFile(theScriptFileName)));
		}

		public static string GetBaseNameForEmulator(EmulatorTag.EmulatorType theEmulatorType)
		{
			string theBaseName = "";

			switch(theEmulatorType)
			{
				case EmulatorTag.EmulatorType.PRINT_SCP:
					theBaseName = "Pr_Scp_Em";
					break;

				case EmulatorTag.EmulatorType.STORAGE_SCP:
					theBaseName = "St_Scp_Em";
					break;

				case EmulatorTag.EmulatorType.STORAGE_SCU:
					theBaseName = "St_Scu_Em";
					break;

				default:
					// Not implemented.
					Debug.Assert(false);
					break;
			}

			return(theBaseName);
		}

		public static string GetExpandedNameForEmulator(Dvtk.Sessions.Session theSession, EmulatorTag.EmulatorType theEmulatorType)
		{
			return(GetExpandedName(theSession, GetBaseNameForEmulator(theEmulatorType)));
		}

		public static string GetBaseNameForMediaFile(string theMediaFullFileName)
		{
			string theMediaFileName = System.IO.Path.GetFileName(theMediaFullFileName);
			string theBaseName = "";

			if (theMediaFileName.ToLower() == "dicomdir")
			{
				theBaseName = theMediaFileName;
			}
			else
			{
				theBaseName = theMediaFileName + "_DCM";
				theBaseName.Replace(".", "_");			
			}

			return(theBaseName);
		}

		public static string GetExpandedNameForMediaFile(Dvtk.Sessions.Session theSession, string theMediaFullFileName)
		{
			return(GetExpandedName(theSession, GetBaseNameForMediaFile(theMediaFullFileName)));
		}

		public static string GetSummaryNameForScriptFile(Dvtk.Sessions.Session theSession, string theScriptFileName)
		{
			return("Summary_" + GetExpandedNameForScriptFile(theSession, theScriptFileName));
		}
		public static string GetDetailNameForScriptFile(Dvtk.Sessions.Session theSession, string theScriptFileName)
		{
			return("Detail_" + GetExpandedNameForScriptFile(theSession, theScriptFileName));
		}
		
		public static string GetSummaryNameForEmulator(Dvtk.Sessions.Session theSession, EmulatorTag.EmulatorType theEmulatorType)
		{
			return("Summary_" + GetExpandedNameForEmulator(theSession, theEmulatorType));
		}

		public static string GetDetailNameForEmulator(Dvtk.Sessions.Session theSession, EmulatorTag.EmulatorType theEmulatorType)
		{
			return("Detail_" + GetExpandedNameForEmulator(theSession, theEmulatorType));
		}
		public static string GetSummaryNameForMediaFile(Dvtk.Sessions.Session theSession, string theMediaFullFileName)
		{
			return("Summary_" + GetExpandedNameForMediaFile(theSession, theMediaFullFileName));
		}
		public static string GetDetailNameForMediaFile(Dvtk.Sessions.Session theSession, string theMediaFullFileName)
		{
			return("Detail_" + GetExpandedNameForMediaFile(theSession, theMediaFullFileName));
		}
	
		public static ArrayList FilterOutResultsFileNames(ArrayList theOriginalResultsFileNames, ArrayList theResultsFileNamesToFilterOut)
		{
			ArrayList theNewResultsFileNames = new ArrayList();

			foreach(string theOriginalResultsFileName in theOriginalResultsFileNames)
			{
				if (!theResultsFileNamesToFilterOut.Contains (theOriginalResultsFileName))
				{
					theNewResultsFileNames.Add(theOriginalResultsFileName);
				}
			}

			return(theNewResultsFileNames);
		}
	}
}
