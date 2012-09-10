using System;
using System.Collections;
using System.IO;

namespace DvtkApplicationLayer {
    /// <summary>
    /// Summary description for SessionManager.
    /// </summary>
    public class SessionManager {
        IList sessionObjects = new ArrayList();
        #region Private Members

        private static SessionManager singleInstance = null;
                       
        # endregion

        # region Properties
        /// <summary>
        /// Method to create an instance of session manager.
        /// </summary>
        public static SessionManager Instance {
            get {
                if(singleInstance == null) {
                    singleInstance = new SessionManager();
                }

                return singleInstance;
            }
        }
        
        # endregion

        # region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        private SessionManager() {
        }
        
        # endregion

        # region Public Methods
        /// <summary>
        /// Create the instance of a seesion according to the session type.
        /// </summary>
        /// <param name="fileName">represents the sessionfile name.</param>
        /// <returns>instance of a session.</returns>
        public Session CreateSession(string fileName) {
            Session session = null;

            // Open the file and determine the SessionType
            Session.SessionType sessionType = DetermineSessionType(fileName);

            switch(sessionType) {
                case Session.SessionType.ST_MEDIA:
                    session = new MediaSession(fileName);
                    break;
                case Session.SessionType.ST_SCRIPT:
                    session = new ScriptSession(fileName);
                    break;
                case Session.SessionType.ST_EMULATOR:
                    session = new EmulatorSession(fileName);
                    break;
                case Session.SessionType.ST_UNKNOWN:
                    break;
            }
            sessionObjects.Add(session);
            return session;
        }

        # endregion

        #region Private Methods

        // This is file Specific Code if the Format 
        //of file changes it need to be altered in that way.
        private Session.SessionType DetermineSessionType(string filename) {
            Session.SessionType sessionType = Session.SessionType.ST_UNKNOWN ;
            using (StreamReader sr = new StreamReader(filename)) {
                string line = "";
                while(!line.StartsWith("SESSION-TYPE")) {
                    line = sr.ReadLine();
                }
                sr.Close();
                string[] lineStrings = line.Split(new char[1] {' '});
                string sessionTypeName = lineStrings[1];
                
                switch(sessionTypeName) {
                    case "media":
                        sessionType = Session.SessionType.ST_MEDIA;
                        break;
                    case "script":
                        sessionType = Session.SessionType.ST_SCRIPT;
                        break;
                    case "emulator":
                        sessionType = Session.SessionType.ST_EMULATOR;
                        break;
                    case "unknown":
                        sessionType = Session.SessionType.ST_UNKNOWN;
                        break;
                }
                return sessionType ;
            }    
        }
        #endregion
    }
}

    



