// Part of ApplicationLayer.dll - .NET class library
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.IO;
using DvtkData;
using System.Diagnostics;
using System.Windows.Forms;

namespace DvtkApplicationLayer {
    /// <summary>
    /// Represents a summary and/or detail results file.
    /// </summary>
    public class Result : PartOfSession {
        const string _SUMMARY_PREFIX = "summary_";
        const string _DETAIL_PREFIX = "detail_";

        #region Private Member Variables
        private ArrayList resultFiles = new ArrayList();
        private string summaryFile = "";
        private string detailFile = "";
        private ArrayList subSummaryResultFiles = new ArrayList();
        private ArrayList subDetailResultFiles = new ArrayList();
        private string date;
        private UInt32 nrOfErrors;
        private UInt32 nrOfWarnings;
        public string sessionId ;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errors">Nos of errors</param>
        /// <param name="warnings">Nos of warnings</param>
        /// <param name="detailFileName">Name of the detailed resultfile.</param>
        /// <param name="summaryFileName">Name of the summary resultfile.</param>
        /// <param name="resultFiles">Collection of resultFiles.</param>
        public Result(
            UInt32 errors, 
            UInt32 warnings, 
            string detailFileName,
            string summaryFileName, 
            ArrayList resultFiles
            ) {
            nrOfErrors = errors;
            nrOfWarnings = warnings;
            detailFile = detailFileName ;
            summaryFile = summaryFileName ;
            resultFiles = resultFiles;
        }
        /// <summary>
        /// Constructor.
        /// </summary>
        public Result(Session session): base(session) {
        }

        #endregion

        #region Public Properties
        /// <summary>
        /// Represents the date when the result was formed.
        /// </summary>
        public string Date {
            get {
                return date;
            }
            set {date = value;
            }
        }
        /// <summary>
        /// Collection of sub summary results of  validation.
        /// </summary>
        public ArrayList SubSummaryResultFiles {
            get {
                return subSummaryResultFiles;
            }
            set{
                subSummaryResultFiles = value;
            }

        }
        /// <summary>
        /// Collection of sub detail results of  validation.
        /// </summary>
        public ArrayList SubDetailResultFiles {
            get {
                return subDetailResultFiles;
            }
            set{
                subDetailResultFiles = value;
            }

        }
        /// <summary>
        /// Collection of ResultFiles.
        /// </summary>
        public ArrayList ResultFiles {
            get {
                return resultFiles;
            }
            set{
                resultFiles = value;
            }

        }
        /// <summary>
        /// Represents the summary result of validation.
        /// </summary>
        public string SummaryFile {
            get {
                return summaryFile;
            }
            set {summaryFile = value;
            }
        }
        /// <summary>
        /// Represents the detail result of validation.
        /// </summary>
        public string DetailFile {
            get {
                return detailFile;
            }
            set {detailFile = value;
            }
        }
        /// <summary>
        /// Number of errors.
        /// </summary>
        public System.UInt32 NrOfErrors {
            get { 
                return nrOfErrors;
            }
            set {nrOfErrors = value;
            }
        } 
        /// <summary>
        /// Number of warnings.
        /// </summary>
        public System.UInt32 NrOfWarnings {
            get { 
                return nrOfWarnings;
            }
            set {nrOfWarnings = value;
            }
        }
        /// <summary>
        /// Represents the sessionId.
        /// </summary>
        public string SessionId {
            get {
                return sessionId;
            }
            set {
                sessionId = value;
            }
        }
        #endregion

        # region Private Functions 

        private static string GetBaseNameForScriptFile(string theScriptFileName) {
			theScriptFileName = theScriptFileName.Replace(".", "_");
            return theScriptFileName;
        }
       
        /// <summary>
        /// Converts a results file name to its corresponding base name.
        /// The base name is the result of removing "....._xxx_" at the beginning and removing
        /// "_res?.xml" at the end.
        /// </summary>
        /// <param name="theResultsFileName">The results file name</param>
        /// <param name="theBaseName">The returned base name. If the supplied results file name is invalid, this parameter will be set to "".</param>
        /// <returns>Indicates if the supplied results file name is a valid one.</returns>
        private static bool GetBaseNameForResultsFile(string theResultsFileName, ref string theBaseName) {
            bool isValidResultsFileName = true;
            theBaseName = theResultsFileName;

            // Remove the first prefix.
            if (theBaseName.ToLower().StartsWith(_SUMMARY_PREFIX)) {
                theBaseName = theBaseName.Remove(0, _SUMMARY_PREFIX.Length);
            }
            else if (theBaseName.ToLower().StartsWith(_DETAIL_PREFIX)) {
                theBaseName = theBaseName.Remove(0, _DETAIL_PREFIX.Length);
            }
            else {
                isValidResultsFileName = false;
            }

            // Remove the second prefix: session ID and underscore.
            if (isValidResultsFileName) {
                if (theBaseName.Length > 3) {
                    // Is long enough to contain the substring "xxx_".
                    try {
                        Int16 theInt16 = Convert.ToInt16(theBaseName.Substring(0, 3));
                    }
                    catch {
                        isValidResultsFileName = false;
                    }

                    theBaseName = theBaseName.Substring(3);

                    if (theBaseName[0] == '_') {	
                        theBaseName = theBaseName.Substring(1);
                    }
                    else {
                        isValidResultsFileName = false;
                    }
                }
                else {
                    isValidResultsFileName = false;
                }
            }

            // Remove the first postfix: ".xml"
            if (isValidResultsFileName) {
                if (theBaseName.ToLower().EndsWith(".xml")) {
                    theBaseName = theBaseName.Substring(0, theBaseName.Length - 4);
                }
                else {
                    isValidResultsFileName = false;
                }
            }

            // Remove all digits at the end of the string.
            if (isValidResultsFileName) {
                bool continueRemovingDigit = true;

                while (continueRemovingDigit) {
                    try {
                        Int16 theInt16 = Convert.ToInt16(theBaseName.Substring(theBaseName.Length - 1));
                    }
                    catch {
                        continueRemovingDigit = false;
                    }

                    if (continueRemovingDigit) {
                        theBaseName = theBaseName.Substring(0, theBaseName.Length - 1);
                    }
                }
            }

            // Remove the "_res" at the end.
            if (isValidResultsFileName) {
                if (theBaseName.ToLower().EndsWith("_res")) {
                    theBaseName = theBaseName.Substring(0, theBaseName.Length - 4);
                }
                else {
                    isValidResultsFileName = false;
                }
            }
			
            // Base name is only valid if it is not empty.
            if (isValidResultsFileName) {
                if (theBaseName.Length == 0) {
                    isValidResultsFileName = false;
                }
            }

            if (!isValidResultsFileName) {
                theBaseName = "";
            }
		
            return(isValidResultsFileName);
        }


        # endregion

        /// <summary>
        /// Method to get the basename for a resultFile.
        /// </summary>
        /// <param name="theResultsFileName"> FullName of a resultFile.</param>
        /// <returns></returns>
        public static string GetBaseNameNoCheck(string theResultsFileName) {
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
        /// Get the results file names of the files that need to be shown (in)directly under the tree node indicated by the tree node tag.
        /// </summary>
        /// <param name="theTreeNodeTag">The tree node tag.</param>
        /// <param name="theResultsFileNames">Valid results file names to choose from.</param>
        /// <returns>The results file names to display under the tree node.</returns>
        public static ArrayList GetNamesForScriptFile(string theScriptFileName, ArrayList theResultsFileNames) {
            ArrayList theNames = new ArrayList();

            string theLowerCaseScriptFileBaseName = GetBaseNameForScriptFile(theScriptFileName).ToLower();

            foreach (string theResultsFileName in theResultsFileNames) {
                string theLowerCaseResultsFileName = theResultsFileName.ToLower();
                string theLowerCaseResultsFileBaseName = GetBaseNameNoCheck(theLowerCaseResultsFileName);

                if ( (theLowerCaseResultsFileBaseName.IndexOf(theLowerCaseScriptFileBaseName) != -1) &&
                    (theLowerCaseResultsFileName.IndexOf("_" + theLowerCaseScriptFileBaseName + "_") != -1)
                    ) {
                    theNames.Add(theResultsFileName);
                }
            }

            return theNames;
        }

        /// <summary>
        /// From a list of results file names, return those results file names with the same session ID
        /// as the session ID of the supplied session.
        /// </summary>
        /// <param name="theSession">The session.</param>
        /// <param name="theResultsFileNames">Valid results file names.</param>
        /// <returns>List of results file names.</returns>
        public static ArrayList GetNamesForCurrentSessionId(DvtkApplicationLayer.Session  theSession, ArrayList theResultsFileNames) {
            ArrayList theNamesForCurrentSession = new ArrayList();
            string theSessionIdAsString = theSession.SessionId.ToString("000");

            foreach(string theResultsFileName in theResultsFileNames) {
                string theResultsFileSessionIdAsString = "";

                if (theResultsFileName.ToLower().StartsWith(_SUMMARY_PREFIX)) {
                    theResultsFileSessionIdAsString = theResultsFileName.Substring(_SUMMARY_PREFIX.Length, 3);
                }
                else if (theResultsFileName.ToLower().StartsWith(_DETAIL_PREFIX)) {
                    theResultsFileSessionIdAsString = theResultsFileName.Substring(_DETAIL_PREFIX.Length, 3);
                }
                else {
                    // Sanity check.
                    Debug.Assert(false);
                }
	
                if (theResultsFileSessionIdAsString == theSessionIdAsString) {
                    theNamesForCurrentSession.Add(theResultsFileName);
                }
            }

            return(theNamesForCurrentSession);
        }

        /// <summary>
        /// Remove .xml files, and if exisiting, the corresponding .html files.
        /// </summary>
        /// <param name="theSession">The session.</param>
        /// <param name="theResultsFileNames">The results file names.</param>
        public static void Remove(DvtkApplicationLayer.Session theSession, ArrayList theResultsFileNames) {
            foreach(string theResultsFileName in theResultsFileNames) {
                string theXmlResultsFullFileName = System.IO.Path.Combine(theSession.ResultsRootDirectory, theResultsFileName);
                string theHtmlResultsFullFileName = theXmlResultsFullFileName.ToLower().Replace(".xml", ".html");

                if (!File.Exists(theXmlResultsFullFileName)) {
                    // Sanity check.
                    Debug.Assert(false);
                }
                else {
                    try {
                        File.Delete(theXmlResultsFullFileName);
                    }
                    catch(Exception eh) {
                        Exception ex = new Exception();
                        MessageBox.Show(ex.Message);
                        // In release mode, just continue.
                        Debug.Assert(false);
                    }
                }

                if (File.Exists(theHtmlResultsFullFileName)) {
                    try {
                        File.Delete(theHtmlResultsFullFileName);
                    }
                    catch(Exception eh) {
                        // In release mode, just continue.
                        Exception ex = new Exception();
                        MessageBox.Show(ex.Message);
                        Debug.Assert(false);
                    }
                }
            }
        }
        /// <summary>
        /// Method to backUp the Arraylist of files in a session.
        /// </summary>
        /// <param name="theSession">represents the Session.</param>
        /// <param name="theFilesToBackup"></param>
        public static void BackupFiles(DvtkApplicationLayer.Session theSession, ArrayList theFilesToBackup) {
            foreach (string theFileToBackup in theFilesToBackup) {
                BackupFile(theSession, theFileToBackup);
            }
        }

        private static void BackupFile(DvtkApplicationLayer.Session theSession, string theFileToBackup) {
            string theSourceFullFileName = System.IO.Path.Combine(theSession.ResultsRootDirectory, theFileToBackup);
            string theDestinyFullFileName = theSourceFullFileName + "_backup";
            int theCounter = 1;

            try {
                while (File.Exists(theDestinyFullFileName + theCounter.ToString())) {
                    theCounter++;
                }

                File.Copy(theSourceFullFileName, theDestinyFullFileName + theCounter.ToString());
            }
            catch {
                // Don't do anything in the release version.
                Debug.Assert(false);
            }
        }
        public static string GetBaseNameForMediaFile(string theMediaFullFileName) {
            string theMediaFileName = System.IO.Path.GetFileName(theMediaFullFileName);
            string theBaseName = "";

            if (theMediaFileName.ToLower() == "dicomdir") {
                theBaseName = theMediaFileName;
            }
            else {
                theBaseName = theMediaFileName + "_DCM";
                theBaseName = theBaseName.Replace(".", "_");			
            }

            return(theBaseName);
        }

        public static ArrayList GetNamesForBaseName(string theBaseName, ArrayList theResultsFileNames) {
            ArrayList theNames = new ArrayList();
            string theLowerCaseBaseName = theBaseName.ToLower();

            foreach (string theResultsFileName in theResultsFileNames) {
                string theLowerCaseResultsFileBaseName = GetBaseNameNoCheck(theResultsFileName).ToLower();

                if (theLowerCaseBaseName == theLowerCaseResultsFileBaseName) {
                    theNames.Add(theResultsFileName);
                }
            }

            return theNames;
        }
        public static ArrayList GetAllNamesForSession(DvtkApplicationLayer.Session theSession) {
            ArrayList theResultsFiles = new ArrayList();
            DirectoryInfo theDirectoryInfo;
            FileInfo[] theFilesInfo;

            theDirectoryInfo = new DirectoryInfo (theSession.ResultsRootDirectory);

            if (theDirectoryInfo.Exists) {
                theFilesInfo = theDirectoryInfo.GetFiles ("*.xml");

                foreach (FileInfo theFileInfo in theFilesInfo) {
                    string theResultsFileName = theFileInfo.Name;

                    if (IsValid(theResultsFileName)) {
                        theResultsFiles.Add(theResultsFileName);
                    }
                }
            }

            return theResultsFiles;
        }
        public static string GetSummaryNameForScriptFile(DvtkApplicationLayer.Session theSession, string theScriptFileName) {
            return("Summary_" + GetExpandedNameForScriptFile(theSession, theScriptFileName));
        }
        public static string GetSummaryNameForEmulator(DvtkApplicationLayer.Session theSession, Emulator.EmulatorTypes theEmulatorType) {
            return("Summary_" + GetExpandedNameForEmulator(theSession, theEmulatorType));
        }
        public static string GetSummaryNameForMediaFile(DvtkApplicationLayer.Session theSession, string theMediaFullFileName) {
            return("Summary_" + GetExpandedNameForMediaFile(theSession, theMediaFullFileName));
        }
        public static string GetExpandedNameForScriptFile(DvtkApplicationLayer.Session theSession, string theScriptFileName) {
            return(GetExpandedName(theSession, GetBaseNameForScriptFile(theScriptFileName)));
        }
        public static string GetExpandedNameForEmulator(DvtkApplicationLayer.Session theSession, Emulator.EmulatorTypes theEmulatorType) {
            return(GetExpandedName(theSession, GetBaseNameForEmulator(theEmulatorType)));
        }
        public static string GetExpandedNameForMediaFile(DvtkApplicationLayer.Session theSession, string theMediaFullFileName) {
            return(GetExpandedName(theSession, GetBaseNameForMediaFile(theMediaFullFileName)));
        }
        private static string GetExpandedName(DvtkApplicationLayer.Session theSession, string theString) {
            return(theSession.SessionId.ToString("000") + '_' + theString + "_res.xml");
        }
        public static string GetBaseNameForEmulator(Emulator.EmulatorTypes theEmulatorType) {
            string theBaseName = "";

            switch(theEmulatorType) {
                case Emulator.EmulatorTypes.PRINT_SCP:
                    theBaseName = "Pr_Scp_Em";
                    break;

                case Emulator.EmulatorTypes.STORAGE_SCP:
                    theBaseName = "St_Scp_Em";
                    break;

                case Emulator.EmulatorTypes.STORAGE_SCU:
                    theBaseName = "St_Scu_Em";
                    break;

                default:
                    // Not implemented.
                    Debug.Assert(false);
                    break;
            }

            return(theBaseName);
        }
        public bool ContainsResultsFile(String resultFileName) {
            bool containsResultsFile = false;
            foreach ( string resultName in resultFiles) {
                if (resultName == resultFileName) {
                    containsResultsFile = true;
                    break;
                }
            }

            return(containsResultsFile);
        }

        /// <summary>
        /// Returns a boolean indicating if the supplied results file name is a valid one.
        /// </summary>
        /// <param name="theResultsFileName">The results file name.</param>
        /// <returns>Indicating if the supplied results file name is a valid one.</returns>
        public static bool IsValid(string theResultsFileName) {
            bool isValidName = true;
            string theBaseName = "";

            isValidName = GetBaseNameForResultsFile(theResultsFileName, ref theBaseName);

            return(isValidName);
        }
    }
}
