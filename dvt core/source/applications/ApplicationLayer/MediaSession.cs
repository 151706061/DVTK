// Part of ApplicationLayer.dll - .NET class library
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;
using System.Collections;
using DvtkSession = Dvtk.Sessions;


namespace DvtkApplicationLayer 
{
	/// <summary>
	/// Summary description for MediaSession.
	/// </summary>
	public class MediaSession : Session 
	{

		#region Private Members

		// The queue of media files to be validated
		private Queue mediaFilesToBeValidated = Queue.Synchronized(new Queue());
		private AsyncCallback callBackDelegate = null;
		private IList mediaFiles = new ArrayList();
		const string _SUMMARY_PREFIX = "summary_";
		const string _DETAIL_PREFIX = "detail_";

		#endregion
        
		#region Constructors
        /// <summary>
        /// Constructor of MediaSession.
        /// </summary>
        ///  <remarks>
        /// Validate DICOM media files.
        /// The Dicom Validation Tool (DVT) can also create and validate DICOM media files.
        /// </remarks>
		public MediaSession() {
			sessionType = SessionType.ST_MEDIA;            
		}
        /// <summary>
        /// Constructor of MediaSession.
        /// </summary>
        /// <param name="fileName">fileName represents the Session Name.</param>
        /// <remarks>
        /// Validate DICOM media files.
        /// The Dicom Validation Tool (DVT) can also create and validate DICOM media files.
        /// </remarks>
		public MediaSession(string fileName) {
			sessionType = SessionType.ST_MEDIA;
			sessionFileName = fileName;
            CreateSessionInstance(sessionFileName);
            
		}
        /// <summary>
        /// Property to access the Dvtk.Session.MediaSession 
        /// </summary>

        public DvtkSession.MediaSession MediaSessionImplementation {
            get {
                return (DvtkSession.MediaSession)implementation;
            }
        }

		#endregion

		#region Public Properties
        /// <summary>
        /// Represents a collection of mediaFiles in a Media Session.
        /// </summary>
		public IList MediaFiles 
		{
			get{ return mediaFiles ;}
			set { mediaFiles = value;}

		}

		#endregion

		#region Public Methods
        /// <summary>
        /// /// <summary>
        /// Method for executing a session in a synchronous manner.
        /// </summary>
        /// <param name="baseInput">baseInput Object (in this case mediaInput Object) </param>
        /// <returns></returns>
       	public override Result Execute(BaseInput baseInput ) 
		{
			CreateSessionInstance(SessionFileName);
			MediaInput mediaInput = (MediaInput)baseInput;
			string baseName = "";
			string[] mediaFileAsArray = 
				(string[])mediaInput.FileNames.ToArray(typeof(string)); 
			if (optionVerbose) 
			{
				implementation.ActivityReportEvent +=new Dvtk.Events.ActivityReportEventHandler(ActivityReportEventHandler);
			}
			string fileName = Path.GetFileName((string)mediaInput.FileNames[0]);
			if (fileName.ToLower() == "dicomdir") 
			{
				baseName = fileName;
			}
			else 
			{
				baseName = fileName + "_DCM";
				baseName = baseName.Replace(".", "_");			
			}
			string resultName = CreateResultFileName(baseName);
			implementation.StartResultsGathering(resultName);
			result = 
				((DvtkSession.MediaSession)implementation).ValidateMediaFiles(mediaFileAsArray);
			implementation.EndResultsGathering();
			return CreateResults(resultName);
		}
        /// <summary>
        /// Method for executing a session in an Asynchronous manner.
        /// </summary>
        /// <param name="baseInput">baseInput Object</param>
        /// <param name="cb">Asynchronous CallBack delegate name.,</param>
		public override void BeginExecute(BaseInput baseInput ) 
		{ 
			CreateSessionInstance(sessionFileName);
			MediaInput mediaInput = (MediaInput)baseInput;
			string[] mediaFileAsArray = 
				(string[])mediaInput.FileNames.ToArray(typeof(string)); 
			foreach(string fileName in mediaFileAsArray) 
			{
				mediaFilesToBeValidated.Enqueue(fileName);
			}
			ValidateMediaFiles();
		}        
        
        /// <summary>
        /// set and save MediaSession settings to file with extension <c>.ses</c>.
        /// </summary>
		public override bool Save () 
		{
			CreateSessionInstance();
            base.SetAllProperties();
			return base.Save();
		}
        /// <summary>
        /// Method to create result for a mediaFile and set its properties.
        /// </summary>
        public void CreateMediaFiles(){
            if(mediaFiles == null) {
                mediaFiles = new ArrayList();
            } else {
                mediaFiles.Clear();
            }
            ArrayList allMediaFiles = GetFileNamesforSession();
            ArrayList mediaFilesBaseNames = GetBaseNamesForResultsFiles(allMediaFiles);
            DirectoryInfo directoryInfo = new DirectoryInfo(((DvtkSession.MediaSession)implementation).ResultsRootDirectory);
            foreach ( string mediaFilesBaseName in mediaFilesBaseNames) {	
                MediaFile mFile = new MediaFile(this , mediaFilesBaseName );
                mFile.MediaFileName = mediaFilesBaseName;
                ArrayList mediaFile = new ArrayList();
                FileInfo[] filesInfo = directoryInfo.GetFiles("*" + mediaFilesBaseName + "*.xml");
					
                foreach (FileInfo fileInfo in filesInfo) {
                    Result correctResult = null;
                    String sessionId = GetSessionId (fileInfo.Name);

                    foreach(Result result in mFile.Result) {
                        if (result.SessionId == sessionId) {
                            correctResult = result;
                            break;
                        }
                    }
						
                    if (correctResult == null) {
                        correctResult = new Result(this);
                        correctResult.SessionId = sessionId;

                        mFile.Result.Add(correctResult);
                    }

                    bool isSummaryFile = true;
                    bool isMainResult = true;

                    if (fileInfo.Name.ToLower().StartsWith("summary_")) {
                        isSummaryFile = true;
                    }
                    else {
                        isSummaryFile = false;
                    }
		
                    if (fileInfo.Name.ToLower().EndsWith(mediaFilesBaseName.ToLower() +"_res.xml")) {
                        isMainResult = true;
                    }
                    else {
                        isMainResult = false;
                    }

                    if (isSummaryFile) {
                        if (isMainResult) {
                            correctResult.SummaryFile = fileInfo.Name;
                            correctResult.ResultFiles.Add(fileInfo.Name);
                        }
                        else {
                            correctResult.SubSummaryResultFiles.Add(fileInfo.Name);
                                                    }
                    }
                    else {
                        if (isMainResult) {
                            correctResult.DetailFile = fileInfo.Name;
                            correctResult.ResultFiles.Add(fileInfo.Name);
                        }
                        else {
                            correctResult.SubDetailResultFiles.Add(fileInfo.Name);
                                                    }
                    }
                }
                MediaFiles.Add(mFile);
            }
        }
        #endregion

		#region Protected Methods
        /// <summary>
        /// Method to create the instance of a MediaSession.
        /// </summary>
        protected override void CreateSessionInstance() {
            if (implementation == null) {
                implementation = new DvtkSession.MediaSession();
            }
        }

        /// <summary>
        /// Method to create the instance of a session. 
        /// </summary>
        /// <param name="sessionFileName"> FileName of the MediaSession.</param>
		protected override void CreateSessionInstance(string sessionFileName) {
            if (implementation == null) {
                implementation = new DvtkSession.MediaSession();
                LoadSession();
            }
        }
		

		#endregion
        
		#region Private Methods
        /// <summary>
        /// Validate Media Storage Files.
        /// </summary>
        /// <remarks>
        /// Typically these files have the file-extension DCM. DVT does not check the file-extension.
        /// The file should have an internal byte-prefix with byte-values 'DICOM'.
        /// </remarks>
		private void ValidateMediaFiles() {
            lock (this) {
                if (mediaFilesToBeValidated.Count > 0) {
                    string fullFileName = (string)mediaFilesToBeValidated.Dequeue();
                    string baseName = "";
                    string fileName = Path.GetFileName(fullFileName);
                    if (fileName.ToLower() == "dicomdir") {
                        baseName = fileName;
                    }
                    else {
                        baseName = fileName + "_DCM";
                        baseName = baseName.Replace(".", "_");	
                    }
                    implementation.StartResultsGathering(CreateResultFileName(baseName));        
                    string[] mediaFilesToValidate = new string[] {fullFileName};
                    // Perform the actual execution of the script.
                    AsyncCallback mediaFilesAsyncCallback = 
                        new AsyncCallback(this.ResultsFromAsyncValidation);
                    ((DvtkSession.MediaSession)implementation).BeginValidateMediaFiles(
                        mediaFilesToValidate, mediaFilesAsyncCallback
                        );
                }
            }
        }

		private void ResultsFromAsyncValidation(IAsyncResult iAsyncResult) 
		{
			((DvtkSession.MediaSession)implementation).EndValidateMediaFiles(iAsyncResult);
			implementation.EndResultsGathering();
			if (mediaFilesToBeValidated.Count > 0) 
			{
				ValidateMediaFiles();
			}
			callBackDelegate(null);
		}
		
		#endregion
	}

}
    

