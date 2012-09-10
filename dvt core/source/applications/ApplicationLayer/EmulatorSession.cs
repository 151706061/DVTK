using System;
using DvtkData;
using System.Diagnostics ;
using System.Collections;
using DvtkSession = Dvtk.Sessions;
using System.IO;


namespace DvtkApplicationLayer {
    /// <summary>
    /// Summary description for EmulatorSession.
    /// </summary>
    public class EmulatorSession : Session {
        /// <summary>
        /// Types of ScpEmulator .
        /// </summary>
        public enum ScpEmulatorType {
            Printing,               
            Storage,                
            Unknown         
        }
        /// <summary>
        /// Types of ScuEmulator.
        /// </summary>
        public enum ScuEmulatorType {
            Storage,                
            Unknown         
        }
        /// <summary>
        /// Printer device status.
        /// </summary>
        public enum PrinterStatus {
            NORMAL,
            WARNING,
            FAILURE
        }
        public enum EmulatorType {
           StorageSCUEmulator ,
           StorageSCPEmulator ,
           PrintScpEmulator 
        }



        # region Private Members

        private string dvtAeTitle = "DVT_AE";
        private UInt16 dvtPort = 104;
        private UInt16 dvtSocketTimeout = 90;
        private UInt32 dvtMaximumLengthReceived = 16384;
        private string sutAeTitle = "SUT_AE";
        private UInt16 sutPort = 104;
        private String sutHostName = "localhost";
        private UInt32 sutMaximumLengthReceived = 16384;
        private Boolean secureSocketsEnabled = false;
        private ScuEmulatorType scuEmulator = ScuEmulatorType.Unknown;
        private ScpEmulatorType scpEmulator = ScpEmulatorType.Unknown;
        private string sutimplementationVersionName = "DVT 2.1";
        private string sutimplementationClassUid = "";
        private string dvtimplementationVersionName = "";
        private string dvtimplementationClassUid = "";
        private AsyncCallback callBackDelegate = null;
		private IList emulators = new ArrayList(); 
        protected EmulatorType emulatorType ;
        private bool addGroupLength = false ;
        private bool defineSqLength = false ;

        

        #endregion

        # region Properties
        /// <summary>
        /// Represents a collection of emulators in an EmulatorSession.
        /// Typically here (StorageScu ,StorageScp ,PrintScp).  
        /// </summary>
		public IList Emulators 
		{
			get{ return emulators ;}
			set { emulators = value;}

		}
        /// <summary>
        /// The DVT AE Title is the application entity name of the DVT machine in the test.
        /// </summary>
        public string DvtAeTitle {
            get { 
                if(!isLoaded) {
                    LoadSession();
                }
                return dvtAeTitle;
            }
            set { 
                dvtAeTitle = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The port that the Dicom Validation Tool (DVT) should use when making a connection to 
        /// System Under Test (SUT).
        /// </summary>
        public UInt16 DvtPort {
            get{
                if(!isLoaded) {
                    LoadSession();
                }
                return dvtPort ;
            }
            set{
                dvtPort = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The period that the Dicom Validation Tool (DVT) will listen for incomming messages
        /// on the TCP/IP connection before automatically aborting the session.
        /// </summary>
        public UInt16 DvtSocketTimeout {
            get{ 
                if(!isLoaded) {
                    LoadSession();
                }
                return dvtSocketTimeout;
            }
            set{
                dvtSocketTimeout = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The maximum length of message fragment (P-DATA-TF PDU) 
        /// that the Dicom Validation Tool (DVT) can receive from the System Under Test (SUT).
        /// </summary>
        /// <remarks>
        /// DICOM DIMSE-messages are split into P-DATA-TF PDU fragments - e.g., C-STORE-RQ of a modality image.
        /// </remarks>
        public UInt32 DvtMaximumLengthReceived {
            get{
                if(!isLoaded) {
                    LoadSession();
                }
                return dvtMaximumLengthReceived;
            }
            set{
                dvtMaximumLengthReceived = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// This is the implementation version name for the Dicom Validation Tool (DVT) implementation.
        /// </summary>
        /// <remarks>
        /// The version is composed of the folloqing items<br></br>
        /// [dvt][version_major].[version_minor]<br></br>
        /// <c>dvtx.x</c>
        /// </remarks>
        public String DvtImplementationVersionName {
            get { 
                return dvtimplementationVersionName; 
            }
            set { 
                dvtimplementationVersionName = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// This is the unique identifier (UID) for the Dicom Validation Tool (DVT) implementation.
        /// </summary>
        /// <remarks>
        /// <p>
        /// The UID identifies the release of the Dicom Validation Tool (DVT).
        /// </p>
        /// <p>
        /// The Dicom Validation Tool (DVT) sents this UID during communication with the System Under Test (SUT).
        /// </p>
        /// <p>
        /// The number starts is composed of the following items<br></br>
        /// [ASCII(d)].[ASCII(v)].[ASCII(t)].[year].[version_major].[version_minor]<br></br>
        /// <c>100.118.116.xxxx.x.x</c>
        /// </p>
        /// </remarks>
        public String DvtImplementationClassUid {
            get { 
                return dvtimplementationClassUid; 
            }
            set { 
                dvtimplementationClassUid = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// Enable or disable secure socket communication
        /// </summary>
        public Boolean SecureSocketsEnabled {
            get {
                if(!isLoaded) {
                    LoadSession();
                }
                return secureSocketsEnabled;
            }
            set {
                secureSocketsEnabled = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The AE Title is the application entity name of the SUT machine in the test.
        /// </summary>
        public string SutAeTitle {
            get { 
                if(!isLoaded) {
                    LoadSession();
                }
                return sutAeTitle;
            }
            set { 
                sutAeTitle = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The port number that the Dicom Validation Tool (DVT) should use when making a 
        /// connection to the product machine of the System Under Test (SUT).
        /// </summary>
        /// <remarks>
        /// Also known as the remote connect port.
        /// </remarks>
        public UInt16 SutPort {
            get{
                if(!isLoaded) {
                    LoadSession();
                }
                return sutPort ;
            }
            set{
                sutPort = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The name that the Dicom Validation Tool (DVT) should use when making a connection to the 
        /// product machine of the System Under Test (SUT).
        /// </summary>
        /// <remarks>
        /// It is best to enter the Internet Address of the Product (in dot notation). 
        /// </remarks>
        public String SutHostName {
            get{ 
                if(!isLoaded) {
                    LoadSession();
                }
                return sutHostName;
            }
            set{
                sutHostName = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The maximum length of message fragment (P-DATA-TF PDU) 
        /// that the System Under Test (SUT) can receive from the Dicom Validation Tool (DVT).
        /// </summary>
        /// <remarks>
        /// DICOM DIMSE-messages are split into P-DATA-TF PDU fragments - e.g., C-STORE-RQ of a modality image.
        /// </remarks>
        public UInt32 SutMaximumLengthReceived {
            get{
                if(!isLoaded) {
                    LoadSession();
                }
                return sutMaximumLengthReceived;
            }
            set{
                sutMaximumLengthReceived = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The version name given by the Manufacturer to the Product implementation to identify it internally. 
        /// DVT checks that the value sent by the Product matches the values given here. 
        /// </summary>
        /// <remarks>
        /// The implementation version name is an optional field - 
        /// when the Product does not send this value leave this entry blank.
        /// </remarks>
        public String SutImplementationVersionName {
            get { 
                if(!isLoaded) {
                    LoadSession();
                }
                return sutimplementationVersionName; 
            }
            set { 
                sutimplementationVersionName = value;
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// This is the unique identifier (UID) for the Product's implementation of the System Under Test (SUT).
        /// </summary>
        /// <remarks>
        /// <p>
        /// The UID is assigned by the Manufacturer to the Product implementation to identify it.
        /// The manufacturer publishes this UID in the product DICOM conformance statement.
        /// </p>
        /// <p>
        /// The Dicom Validation Tool (DVT) checks that the value sent by the Product matches the value given here.
        /// </p>
        /// </remarks>
        public String SutImplementationClassUid {
            get {
                if(!isLoaded) {
                    LoadSession();
                }
                return sutimplementationClassUid; 
            }
            set { 
                sutimplementationClassUid = value; 
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The DefineSqLength option, 
        /// is used to make DVT encode explicit length Sequences when sending messages. 
        /// </summary>
        /// <remarks>
        /// <p>
        /// Explicit lengths are computed for both the Sequence and each Item 
        /// present within the Sequence.
        /// </p>
        /// <p>
        /// By default DVT uses the undefined length encoding.
        /// </p>
        /// </remarks>
        public bool DefineSqLength {
            get { 
                if(!isLoaded) {
                    LoadSession();
                }
                return defineSqLength; 
            }
            set { 
                defineSqLength = value; 
                hasSessionchanged = true;
            }
        }
        /// <summary>
        /// The AddGroupLength option, 
        /// is used to have DVT add Group Length attributes to all 
        /// Groups found in each message sent. 
        /// </summary>
        /// <remarks>
        /// By default DVT does not encode Group Length attributes 
        /// (except for the Command Group Length).
        /// </remarks>
        public bool AddGroupLength {
            get {
                if(!isLoaded) {
                    LoadSession();
                }
                return addGroupLength; 
            }
            set { 
                addGroupLength = value; 
                hasSessionchanged = true;
            }
        }
       
        # endregion
        /// <summary>
        /// Method to get & set the ScpEmulator type.
        /// </summary>
        public ScpEmulatorType scpEmulatorType {
            get {
                return scpEmulator;
            }
            set {
                scpEmulator = value;
            }
        }
        /// <summary>
        /// Method to get & set the ScuEmulator type.
        /// </summary>
        public ScuEmulatorType scuEmulatorType {
            get {
                return scuEmulator;
            }
            set {
                scuEmulator = value;
            }
        }
        /// <summary>
        /// Returns the type of emulator.
        /// </summary>
        public EmulatorType EmulatorTypes {
            get {
                return emulatorType;
            }
            set {
                emulatorType = value;
            }
        }



        # region Constructor
        /// <summary>
        /// Constructor of Emulator Session.
        /// Emulation for Verification, Storage and Print SOP Classes.
        /// </summary>
        /// <remarks>
        /// The Dicom Validation Tool (DVT) can be used as either 
        /// Service Class User (SCU) or Service Class Provider (SCP) with a direct 
        /// connection to the System Under Test (SUT)Product (via TCP/IP). 
        /// DVT acts as an emulator for the DICOM Service classes being tested. 
        /// DVT can also create and validate DICOM media files.
        /// </remarks>
        public EmulatorSession() {
            sessionType = SessionType.ST_EMULATOR;
        }
        /// <summary>
        /// Constructor of Emulator Session.
        /// Emulation for Verification, Storage and Print SOP Classes.
        /// </summary>
        /// <param name="fileName">fileName represents the Session Name.</param>
        /// <remarks>
        /// The Dicom Validation Tool (DVT) can be used as either 
        /// Service Class User (SCU) or Service Class Provider (SCP) with a direct 
        /// connection to the System Under Test (SUT)Product (via TCP/IP). 
        /// DVT acts as an emulator for the DICOM Service classes being tested. 
        /// DVT can also create and validate DICOM media files.
        /// </remarks>
       
        public EmulatorSession(string fileName) {
            sessionType = SessionType.ST_EMULATOR;
            sessionFileName = fileName;
            CreateSessionInstance(sessionFileName);
        }
        /// <summary>
        /// Property to access the Dvtk.Session.EmulatorSession 
        /// </summary>
        
        public DvtkSession.EmulatorSession EmulatorSessionImplementation{
            get {
                return (DvtkSession.EmulatorSession)implementation ;
            }
        }

        # endregion

        # region Public Methods
        /// <summary>
        /// /// <summary>
        /// Method for executing a session in a synchronous manner.
        /// </summary>
        /// <param name="baseInput">baseInput Object (in this case EmulatorInput Object) </param>
        /// <returns></returns>
        public override Result Execute(BaseInput baseInput) {
            CreateSessionInstance(sessionFileName);
            string filename ;
            EmulatorInput  emulatorInput = (EmulatorInput)baseInput;
            string resultFileName = "";
            if (optionVerbose) {
                implementation.ActivityReportEvent +=new Dvtk.Events.ActivityReportEventHandler(ActivityReportEventHandler);
            }
            if(scpEmulator == ScpEmulatorType.Storage) {
                filename = "St_Scp_Em";
                ((DvtkSession.EmulatorSession)implementation).ScpEmulatorType = DvtkData.Results.ScpEmulatorType.Storage;
                resultFileName = CreateResultFileName(filename);
                implementation.StartResultsGathering(resultFileName);
                result = ((DvtkSession.EmulatorSession)implementation).EmulateSCP();
            } 

            else if (scpEmulator == ScpEmulatorType.Printing){
                filename = "Pr_Scp_Em";
                ((DvtkSession.EmulatorSession)implementation).ScpEmulatorType = DvtkData.Results.ScpEmulatorType.Printing;
                resultFileName = CreateResultFileName(filename);
                implementation.StartResultsGathering(resultFileName);
                result = ((DvtkSession.EmulatorSession)implementation).EmulateSCP();
            }
            else {
            }
            if(scuEmulator == ScuEmulatorType.Storage) {

                string[] theEmulatorFileAsArray = 
                    (string[]) emulatorInput.FileNames.ToArray(typeof(string));  
                filename = "St_Scu_Em";
                ((DvtkSession.EmulatorSession)implementation).ScuEmulatorType = DvtkData.Results.ScuEmulatorType.Storage;
                resultFileName = CreateResultFileName(filename);
                implementation.StartResultsGathering(resultFileName);
                result = ((DvtkSession.EmulatorSession)implementation).EmulateStorageSCU(
                    theEmulatorFileAsArray, 
                    emulatorInput.ModeOfAssociation,
                    emulatorInput.ValidateOnImport,
                    emulatorInput.DataUnderNewStudy,
                    emulatorInput.NosOfRepetitions
                    );
                implementation.EndResultsGathering();
            } 
            else {
            }
            return CreateResults(resultFileName);
        }
        /// <summary>
        /// Method for executing a session in an Asynchronous manner.
        /// </summary>
        /// <param name="baseInput">baseInput Object</param>
        /// <param name="dlgt">Asynchronous CallBack delegate name.,</param>
        public override void BeginExecute(BaseInput baseInput ){
            
            CreateSessionInstance(sessionFileName);
            string filename ;
            EmulatorInput  emulatorInput = (EmulatorInput)baseInput;
            if(
                (scpEmulator == ScpEmulatorType.Printing) ||
                (scpEmulator == ScpEmulatorType.Storage)
                ) {
                if(scpEmulator == ScpEmulatorType.Printing ) {
                    filename = "Pr_Scp_Em";
                } else {
                    filename = "St_Scp_Em";
                }
                implementation.StartResultsGathering(CreateResultFileName(filename));
                // Perform the actual execution of the script.
                AsyncCallback asyncCallback = new AsyncCallback(this.ResultEmulatorScpAsyn);
                ((DvtkSession.EmulatorSession)implementation).BeginEmulateSCP(asyncCallback);
            }
            // If this is the storage SCU emulator...
            if (scuEmulator == ScuEmulatorType.Storage ) {
                string[] theEmulatorFileAsArray = 
                    (string[]) emulatorInput.FileNames.ToArray(typeof(string));  
                filename = "St_Scu_Em";
                implementation.StartResultsGathering(CreateResultFileName(filename));
                AsyncCallback StorageScuAsyncCallback = 
                    new AsyncCallback(this.ResultEmulatorStorageScuAsyn);
                ((DvtkSession.EmulatorSession)implementation).BeginEmulateStorageSCU(
                    theEmulatorFileAsArray,
                    emulatorInput.ModeOfAssociation,
                    emulatorInput.ValidateOnImport,
                    emulatorInput.DataUnderNewStudy,
                    emulatorInput.NosOfRepetitions ,
                    StorageScuAsyncCallback
                );
            }
        }
        
        /// <summary>
        /// Method to terminate the connection and call end result gathering.
        /// </summary>
        public void StopingEmulator() {
            if(implementation != null) {
                ((DvtkSession.EmulatorSession)implementation).TerminateConnection();
			
                //
                // End results gathering.
                //
                implementation.EndResultsGathering();
            }
        }
        /// <summary>
        /// set and save EmulatorSession settings to file with extension <c>.ses</c>.
        /// </summary>
        public override bool Save () {
            CreateSessionInstance();
            SetAllProperties();            
            return base.Save();
        }
        /// <summary>
        /// Apply a new printer status and corresponding status info.
        /// </summary>
        /// <param name="printerStatus">Printer device status.</param>
        /// <param name="statusInfo">Additional information about Printer Status.</param>
        /// <param name="sendStatusEvent">Indicates the status event should be sent to SUT</param>
        public void ApplyStatus(PrinterStatus printerStatus, System.String statusInfo, bool sendStatusEvent) {
            printerStatus = printerStatus;
            string printerStatusInfo = statusInfo;
            ((DvtkSession.EmulatorSession)implementation).Printer.ApplyStatus (Convert(printerStatus), printerStatusInfo,true);
        }
        /// <summary>
        /// Method to create result for a emulator and set its properties.
        /// </summary>
        public void CreateEmulatorFiles(){
            if(emulators == null) {
                emulators = new ArrayList();
            } else {
                emulators.Clear();
            }
            ArrayList emulatorFilesBaseName = new ArrayList();
            emulatorFilesBaseName.Add("Pr_Scp_Em");
            emulatorFilesBaseName.Add("St_Scp_Em");
            emulatorFilesBaseName.Add("St_Scu_Em");
            Emulator emulator = null;
            DirectoryInfo directoryInfo = new DirectoryInfo(((DvtkSession.EmulatorSession)implementation).ResultsRootDirectory);
            foreach ( string emulatorFileBaseName in emulatorFilesBaseName) {
                    if (emulatorFileBaseName == "Pr_Scp_Em" ) {
                        Emulator tempEmulator = new Emulator(this , "Print Scp Emulator");
                        tempEmulator.EmulatorType = Emulator.EmulatorTypes.PRINT_SCP ;
                        emulator = tempEmulator ;
                    }
                    else if (emulatorFileBaseName == "St_Scp_Em"){
                       Emulator tempEmulator = new Emulator(this , "Storage Scp Emulator");	
                       tempEmulator.EmulatorType = Emulator.EmulatorTypes.STORAGE_SCP;
                       emulator = tempEmulator ;	
                    }
                    else { 
                        Emulator tempEmulator = new Emulator(this , "Storage Scu Emulator");
                        tempEmulator.EmulatorType = Emulator.EmulatorTypes.STORAGE_SCU;
                        emulator = tempEmulator ;
                       
                    }
                    ArrayList emulatorFile = new ArrayList();
                    FileInfo[] filesInfo = directoryInfo.GetFiles("*" + emulatorFileBaseName + "*.xml");
					
                    foreach (FileInfo fileInfo in filesInfo) {
                        Result correctResult = null;
                        String sessionId = GetSessionId (fileInfo.Name);

                        foreach(Result result in emulator.Result) {
                            if (result.SessionId == sessionId) {
                                correctResult = result;
                                break;
                            }
                        }
						
                        if (correctResult == null) {
                            correctResult = new Result(this);
                            correctResult.SessionId = sessionId;

                            emulator.Result.Add(correctResult);
                        }

                        bool isSummaryFile = true;
                        bool isMainResult = true;

                        if (fileInfo.Name.ToLower().StartsWith("summary_")) {
                            isSummaryFile = true;
                        }
                        else {
                            isSummaryFile = false;
                        }
		
                        if (fileInfo.Name.ToLower().EndsWith(emulatorFileBaseName.ToLower() +"_res.xml")) {
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
                    Emulators.Add(emulator);
                }
            }

        # endregion

        #region Protected Methods
        /// <summary>
        /// Method to create the instance of a EmulatorSession.
        /// </summary>
        protected override void CreateSessionInstance() {
            if (implementation == null) {
                implementation = new DvtkSession.EmulatorSession();
            }
        }
        /// <summary>
        /// Method to create the instance of a session. 
        /// </summary>
        /// <param name="sessionFileName"> FileName of the EmulatorSession.</param>
        protected override void CreateSessionInstance(string sessionFileName) {
            if (implementation == null) {
                implementation = new DvtkSession.EmulatorSession();
                LoadSession();
            }
        }
        /// <summary>
        /// Method to get the Session Properties from a loaded session.
        /// </summary>
        protected override void GetAllProperties() {
			base.GetAllProperties();
            dvtAeTitle = ((DvtkSession.EmulatorSession)implementation).DvtSystemSettings.AeTitle;
            dvtPort = ((DvtkSession.EmulatorSession)implementation).DvtSystemSettings.Port;
            dvtSocketTimeout = 
                ((DvtkSession.EmulatorSession)implementation).DvtSystemSettings.SocketTimeout;
            dvtMaximumLengthReceived = ((DvtkSession.EmulatorSession)implementation).
                DvtSystemSettings.MaximumLengthReceived;
            sutAeTitle = ((DvtkSession.EmulatorSession)implementation).SutSystemSettings.AeTitle; 
            sutPort = ((DvtkSession.EmulatorSession)implementation).SutSystemSettings.Port ;
            sutHostName = ((DvtkSession.EmulatorSession)implementation).SutSystemSettings.HostName;
            sutMaximumLengthReceived = ((DvtkSession.EmulatorSession)implementation).
                SutSystemSettings.MaximumLengthReceived;
            secureSocketsEnabled = ((DvtkSession.EmulatorSession)implementation).
                SecuritySettings.SecureSocketsEnabled;
            sutimplementationVersionName = ((DvtkSession.EmulatorSession)implementation).
                SutSystemSettings.ImplementationVersionName;
            sutimplementationClassUid = ((DvtkSession.EmulatorSession)implementation).
                DvtSystemSettings.ImplementationClassUid;
            dvtimplementationClassUid = ((DvtkSession.EmulatorSession)implementation).
                SutSystemSettings.ImplementationClassUid;
            dvtimplementationVersionName = ((DvtkSession.EmulatorSession)implementation).
                DvtSystemSettings.ImplementationVersionName;
            defineSqLength = ((DvtkSession.EmulatorSession)implementation).DefineSqLength;
            addGroupLength = ((DvtkSession.EmulatorSession)implementation).AddGroupLength ;
        }
        /// <summary>
        /// Method to set the values of Session Properties in a SessionFile.
        /// </summary>
        public override void SetAllProperties() {
			base.SetAllProperties();
            ((DvtkSession.EmulatorSession)implementation).DvtSystemSettings.AeTitle = dvtAeTitle;
            ((DvtkSession.EmulatorSession)implementation).DvtSystemSettings.Port = dvtPort;
            ((DvtkSession.EmulatorSession)implementation).DvtSystemSettings.SocketTimeout = 
                dvtSocketTimeout;
            ((DvtkSession.EmulatorSession)implementation).DvtSystemSettings.MaximumLengthReceived =
                dvtMaximumLengthReceived;
            ((DvtkSession.EmulatorSession)implementation).SutSystemSettings.AeTitle = sutAeTitle;
            ((DvtkSession.EmulatorSession)implementation).SutSystemSettings.Port = sutPort;
            ((DvtkSession.EmulatorSession)implementation).SutSystemSettings.HostName = sutHostName;
            ((DvtkSession.EmulatorSession)implementation).SutSystemSettings.MaximumLengthReceived =
                sutMaximumLengthReceived;
            ((DvtkSession.EmulatorSession)implementation).SecuritySettings.SecureSocketsEnabled =
                secureSocketsEnabled;
            ((DvtkSession.EmulatorSession)implementation).
                SutSystemSettings.ImplementationVersionName = sutimplementationVersionName;
            ((DvtkSession.EmulatorSession)implementation).
                SutSystemSettings.ImplementationClassUid = sutimplementationClassUid;
            ((DvtkSession.EmulatorSession)implementation).
                DvtSystemSettings.ImplementationVersionName = dvtimplementationVersionName;
            ((DvtkSession.EmulatorSession)implementation).
                DvtSystemSettings.ImplementationClassUid = dvtimplementationClassUid;
            ((DvtkSession.EmulatorSession)implementation).DefineSqLength = defineSqLength;
            ((DvtkSession.EmulatorSession)implementation).AddGroupLength = addGroupLength;

        }

        #endregion

        # region Private Methods
        
        private void ResultEmulatorScpAsyn(IAsyncResult iAsyncResult) {
            ((DvtkSession.EmulatorSession)implementation).EndEmulateSCP(iAsyncResult);
            // Save the results.
            implementation.EndResultsGathering();
            callBackDelegate(null);
        }

        private  void ResultEmulatorStorageScuAsyn(IAsyncResult iAsyncResult) {
            ((DvtkSession.EmulatorSession)implementation).EndEmulateStorageSCU(iAsyncResult);
            // Save the results.
            implementation.EndResultsGathering();
            callBackDelegate(null);
        }

        private DvtkSession.PrinterStatus Convert(DvtkApplicationLayer.EmulatorSession.PrinterStatus value) {
            switch (value) {
                case PrinterStatus.NORMAL:
                    return DvtkSession.PrinterStatus.NORMAL;
                case PrinterStatus.WARNING:
                    return DvtkSession.PrinterStatus.WARNING;
                case PrinterStatus.FAILURE:
                    return DvtkSession.PrinterStatus.FAILURE;
                default:
                    throw new System.NotSupportedException();
            }
        }
        
        
    
        #endregion
    }
                    
}

