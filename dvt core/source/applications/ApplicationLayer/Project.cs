// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using Dvtk;

namespace DvtkApplicationLayer {
    /// <summary>
    /// Summary description for project.
    /// </summary>
    public class Project {

        #region Private Members

        private string projectFileName;
        private IList sessions = new ArrayList();
        private bool hasProjectChanged = false ;
        private int nosOfSessionsChanged ;
        SessionManager sessionManager = SessionManager.Instance;
        private bool isProjectConstructed = false;
        public bool hasUserCancelledLastOperation = false;
        public  CallBackMessageDisplay  display_message;
        public delegate void CallBackMessageDisplay (string message);
        

        # endregion

        #region Properties
        /// <summary>
        /// Represents the Project File Name.
        /// </summary>
        public string ProjectFileName {
            get { 
                return projectFileName;
            }
            
            set { 
                projectFileName = value; 
            }
        }
        /// <summary>
        /// Represents a collection of Sessions in a Project.
        /// </summary>
        public IList Sessions { 
            get { return sessions; }
        }
        /// <summary>
        /// Boolean that determines whether the last operation has been cancelled be the user.
        /// </summary>
        public bool HasCancelledLastOperation {
            get { 
                return hasUserCancelledLastOperation;
            }
            
            set { 
                hasUserCancelledLastOperation = value; 
            }
        }
        /// <summary>
        /// Boolean that represents whether the project has been changed.
        /// </summary>
        public bool HasProjectChanged {
            get { 
                return hasProjectChanged;
            }
            
            set { 
                hasProjectChanged = value; 
            }
        }
        /// <summary>
        /// Boolean that represents whether the project has been Constructed.
        /// </summary>
        public bool IsProjectConstructed {
            get {
                return isProjectConstructed;
            }
        }
        /// <summary>
        /// Property that represents the number of changed sessions.
        /// </summary>
        public int  NosOfSessionsChanged {
            get {
                return nosOfSessionsChanged;
            }
            set {
                nosOfSessionsChanged = value ;
            }
        }


        #endregion

        # region Constructor
        public Project() {
            projectFileName = "";
            hasProjectChanged = false;
            hasUserCancelledLastOperation = false;
            isProjectConstructed = false;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Method to Load the Project.
        /// </summary>
        /// <param name="projFileName">FullFileName of the Project.</param>
        /// <returns></returns>
        public bool Load(string projFileName) {
            bool success = false;
            projectFileName = projFileName;
            // Check that the file exists.
            FileInfo projectFileInfo = new FileInfo(projectFileName);
            if (!projectFileInfo.Exists) {
                string msg =
                    string.Format(
                    "Failed to load project file: {0}\n. File does not exist.", projectFileName);
                success = false;
            } else {
                XmlTextReader reader = null;
                try {
                    reader = new XmlTextReader(projectFileName);
                    string sessionFileName = "";
                    reader.WhitespaceHandling = WhitespaceHandling.None;
                    reader.MoveToContent();
                    do {
                        sessionFileName = reader.ReadElementString ();
                        if (sessionFileName != "") { 
                            if (PathUtils.IsRelativePath(sessionFileName)) {
                                sessionFileName = PathUtils.ConvertToAbsolutePath(Path.GetDirectoryName(this.projectFileName), sessionFileName);
                            }
                            // If the session file name was already a full file name, or the conversion
                            // from a relative filename to an absolute one succeeded, load the session.
                            if (sessionFileName != ""){
                                Session session = sessionManager.CreateSession(sessionFileName);
                                session.ParentProject = this ;
                                session.BaseLocation = 
                                    System.IO.Path.GetDirectoryName(projectFileName);
                                sessions.Add(session);                              
                            }
                        }
                    } while (reader.Name == "Session");
                    success = true;
                }
                catch (Exception e) {
                    string msg =
                        string.Format(
                        "Failed to load project file: {0}. Due to exception:{1}\n", 
                        this.projectFileName,
                        e.Message);
                    success = false;
                }
                finally {
                    if (reader != null) reader.Close();
                }
            }
            if (success) {
                isProjectConstructed = true;
                hasProjectChanged = false;
                hasUserCancelledLastOperation = false;
            } else {
                Close(false);
            }
            return success;
        }
        
        /// <summary>
        /// Save the Project to the specified Project file name. 
        /// The current project file name will also change to the specified project file name.
        /// Sessions will not be saved.
        /// </summary>
        /// <param name="theProjectFileName">The name of the Project file ot save to.</param>
        /// <returns>Boolean indicating if the saving was successfull.</returns>
        public bool SaveProject(string theProjectFileName) {
            projectFileName = theProjectFileName;
            return(SaveProject());
        }

        /// <summary>
        /// Save the Project to the current Project file name.
        /// Sessions will not be saved.
        /// </summary>
        /// <returns>Boolean indicating if the saving was successfull.</returns>
        public bool SaveProject() {
            bool success = false;
            XmlTextWriter writer = null;
            try {
                writer = new XmlTextWriter(this.projectFileName, System.Text.Encoding.ASCII);
                // The written .xml file will be more readable
                writer.Formatting = Formatting.Indented;
                // Start the document
                writer.WriteStartDocument (true);
                // Write the session element containing all session files
                writer.WriteStartElement ("Sessions"); 
                //Write the session filenames to the document
                for (int i = 0; i < sessions.Count ; i++)
                    writer.WriteElementString ("Session", ((Session)sessions[i]).SessionFileName);
                // End the sessions element
                writer.WriteEndElement ();
                // End the document
                writer.WriteEndDocument ();
                success = true;
            }
            catch (Exception e) {
                string msg =
                    string.Format(
                    "Failed to write project file: {0}. Due to exception:{1}\n", 
                    this.projectFileName,
                    e.Message);
                success = false;
            }
            finally {
                if (writer != null) writer.Close();
            }

            if (success) {
                hasProjectChanged = false;
                hasUserCancelledLastOperation = false;
                
            }
            return success;
        }
        public bool HasUserCancelledLastOperation() {
            return(hasUserCancelledLastOperation);
        }
        /// <summary>
        /// Method to close the Project.
        /// </summary>
        /// <param name="saveChanges"></param>
        public void Close(bool saveChanges) {
            
            hasUserCancelledLastOperation = false;

            if (AreProjectOrSessionsChanged() && saveChanges) {
                ///***Save(true);
                // SaveProject();
            }

            if (!hasUserCancelledLastOperation) {
                hasProjectChanged = false;
                hasUserCancelledLastOperation = false;
                isProjectConstructed = false;
                sessions.Clear();
                projectFileName = "";
            }
        }
        /// <summary>
        /// Construct a new empty Project that has not been saved yet to the supplied Project file.
        /// 
        /// Precondition: no project is constructed.
        /// </summary>
        /// <param name="theProjectFileName">The name of the Project file.</param>
        public void New(string theProjectFileName) {
            Debug.Assert(isProjectConstructed == false, "Project is constructed when calling Project.New(...)");

            hasProjectChanged = true;
            hasUserCancelledLastOperation = false;
            isProjectConstructed = true;
            sessions.Clear();
            projectFileName = theProjectFileName;
        }
        /// <summary>
        /// Method to add a session to an existing Project.
        /// </summary>
        /// <param name="theSessionFullFileName"></param>
        public void AddSession(string theSessionFullFileName) {
            Session theLoadedSession =  sessionManager.CreateSession(theSessionFullFileName);
            theLoadedSession.ParentProject = this;
            theLoadedSession.SessionFileName = theSessionFullFileName;

            if (theLoadedSession != null) {
                // Search the sessions loaded in project file for same ResultsRootDirectory 
                foreach(Session session in sessions) {
                    if (session.ResultsRootDirectory == theLoadedSession.ResultsRootDirectory) {
                        session.ParentProject = this ;
                        string msg =
                            string.Format(
                            "The {0} have the same results directory \n as session {1} in the project.", 
                            theLoadedSession.SessionFileName,session.SessionFileName);

                        MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        
                    }
                }

                sessions.Add(theLoadedSession);
                hasProjectChanged = true;
            }
        }
        /// <summary>
        /// Method to determine whether the project or session has been changed.
        /// </summary>
        /// <returns>True : if project or session has been changed else false.</returns>
        public bool AreProjectOrSessionsChanged() {
            bool areChanged = false;
			
            if (HasProjectChanged) {
                areChanged = true;
            }
            else if (GetNumberOfChangedSessions() != 0) {
                areChanged = true;
            }

            return (areChanged);
        }
        /// <summary>
        /// Method to determine whether a particular session exist in a list of given sessions.
        /// </summary>
        /// <param name="session_file"></param>
        /// <returns></returns>
        public bool ContainsSession (string session_file) {
            bool theReturnValue = false;

            foreach(Session session in sessions) {
                if (session.SessionFileName == session_file) {
                    theReturnValue = true;
                }
            }

            return theReturnValue;
        }
        /// <summary>
        /// Method to determine the number of changed sessions
        /// </summary>
        /// <returns>Returns the number of changed sessions</returns>
        private int GetNumberOfChangedSessions() {
            int theNumberOfChangedSessions = 0;

            for (int i = 0 ; i < sessions.Count ; i++) {
                Session tempSession = (Session)sessions[i];
                tempSession.ParentProject = this ;
                if (tempSession.GetSessionChanged(tempSession)) {
                    theNumberOfChangedSessions++;
                }
            }
            nosOfSessionsChanged = theNumberOfChangedSessions;

            return(theNumberOfChangedSessions);
        }

        
        # endregion


       

    }
}
        