// Part of DVTK.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;

namespace Dvtk.Sessions
{
    using Dvtk.Events;
    using Dvtk.Security;
	using DvtkData.Media;
	using DvtkData.ComparisonResults;

	//
	// Aliases for types
	//
	using MediaFileName = System.String;

    /// <summary>
    /// Access to commands that generate user output in the session results output.
    /// </summary>
    /// <remarks>
    /// May be used by the user to report user-specific results in the session results output.
    /// The user may perform additional validation checks which he/she wants to write to the
    /// results output.
    /// </remarks>
    public interface IUserOutput
    {
        /// <summary>
        /// Write a user specific error to the output results.
        /// </summary>
        /// <param name="text">text for the output</param>
        void WriteError(System.String text);
        /// <summary>
        /// Write a user specific warning to the output results.
        /// </summary>
        /// <param name="text">text for the output</param>
        void WriteWarning(System.String text);
        /// <summary>
        /// Write a user specific information to the output results.
        /// </summary>
        /// <param name="text">text for the output</param>
        void WriteInformation(System.String text);
		/// <summary>
		/// Write a user specific HTML text to the output results.
		/// </summary>
		/// <param name="htmlText">HTML text for the output</param>
		void WriteHtmlInformation(System.String htmlText);
    }

	/// <summary>
	/// Access to commands that generate validation output in the session results output.
	/// </summary>
	/// <remarks>
	/// May be used by the user to report validation-specific results in the session results output.
	/// The user may perform additional validation checks which he/she wants to write to the
	/// results output.
	/// </remarks>
	public interface IValidationOutput
	{
		/// <summary>
		/// Write the message comparison result to the output results.
		/// </summary>
		/// <param name="messageComparisonResults">Message comparison results to output.</param>
		void WriteMessageComparisonResults(MessageComparisonResults messageComparisonResults);
		/// <summary>
		/// Write a validation specific error to the output results.
		/// </summary>
		/// <param name="text">text for the output</param>
		void WriteValidationError(System.String text);
		/// <summary>
		/// Write a validation specific warning to the output results.
		/// </summary>
		/// <param name="text">text for the output</param>
		void WriteValidationWarning(System.String text);
		/// <summary>
		/// Write a validation specific information to the output results.
		/// </summary>
		/// <param name="text">text for the output</param>
		void WriteValidationInformation(System.String text);
	}

    /// <summary>
    /// Access to session file management.
    /// </summary>
    /// <remarks>
    /// Session settings may be written to a file with extension <c>.ses</c>. 
    /// These settings can later be used to load a new session by means of;
    /// <list type="bullet">
    /// <item><see cref="Dvtk.Sessions.ScriptSession.LoadFromFile"/></item>
    /// <item><see cref="Dvtk.Sessions.EmulatorSession.LoadFromFile"/></item>
    /// <item><see cref="Dvtk.Sessions.MediaSession.LoadFromFile"/></item>
    /// </list>
    /// </remarks>
    public interface ISessionFileManagement
    {
        /// <summary>
        /// Save session settings to file with extension <c>.ses</c>.
        /// </summary>
        bool SaveToFile();
    }

    /// <summary>
    /// Access to TCP/IP security settings, certificate handling and credential handling.
    /// </summary>
    public interface ISecure
    {
        /// <summary>
        /// TCP/IP security settings
        /// </summary>
        ISecuritySettings SecuritySettings { get; }
        /// <summary>
        /// Creates a class (interface) to handle certificate files for the current session.
        /// </summary>
        /// <returns>Interface to manipulate certificate files</returns>
        Dvtk.Security.ICertificateHandling CreateSecurityCertificateHandler();
        /// <summary>
        /// Creates a class (interface) to handle credential files for the current session.
        /// </summary>
        /// <returns>Interface to manipulate credential files</returns>
        Dvtk.Security.ICredentialHandling CreateSecurityCredentialHandler();
        /// <summary>
        /// Dispose a class (interface) to handle certificate files for the current session.
        /// </summary>
        /// <remarks>
        /// Signal the DVTK component that the security certificate handler is no 
        /// longer used by the application.
        /// </remarks>
        void DisposeSecurityCertificateHandler();
        /// <summary>
        /// Dispose a class (interface) to handle credential files for the current session.
        /// </summary>
        /// <remarks>
        /// Signal the DVTK component that the security credential handler is no 
        /// longer used by the application.
        /// </remarks>
        void DisposeSecurityCredentialHandler();
    }
    /// <summary>
    /// Access to Dicom Validation Tool (DVT) system settings.
    /// </summary>
    public interface IConfigurableDvt
    {
        /// <summary>
        /// Settings specific to the DVT machine node.
        /// </summary>
        IDvtSystemSettings DvtSystemSettings { get; }
    }
    /// <summary>
    /// Access to System Under Test (SUT) system settings.
    /// </summary>
    public interface IConfigurableSut
    {
        /// <summary>
        /// Settings specific to the SUT machine node.
        /// </summary>
        ISutSystemSettings SutSystemSettings { get; }
    }

	/// <summary>
	/// Access to commands that read and write Dicom(DCM) files.
	/// </summary>
	public interface IDicomFile
	{
		/// <summary>
		/// Read a (persistent) Media Storage file into dicom file object.
		/// </summary>
		/// <remarks>
		/// The file typically has the extension <c>.DCM</c>.
		/// </remarks>
		/// <param name="mediaFileName">file name to read from</param>
		/// <returns>dicom file object read</returns>
		/// <exception cref="System.ArgumentNullException">Argument <c>mediaFileName</c> is a <see langword="null"/> reference.</exception>
		DicomFile ReadFile(
			MediaFileName mediaFileName);
		/// <summary>
		/// Write a dicom file object to a (persistent) Media Storage file.
		/// </summary>
		/// <param name="file">dicom file object to write</param>
		/// <param name="mediaFileName">file name to write to</param>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException">Argument <c>file</c> is a <see langword="null"/> reference.</exception>
		/// <exception cref="System.ArgumentNullException">Argument <c>mediaFileName</c> is a <see langword="null"/> reference.</exception>
		bool WriteFile(
			DicomFile file,
			MediaFileName mediaFileName);
	}

    /// <summary>
    /// Base-class for test sessions within DVT.
    /// </summary>
    /// <remarks>
    /// Forms the base-class for sub-classes
    /// <list type="bullet">
    /// <item><see cref="Dvtk.Sessions.ScriptSession"/></item>
    /// <item><see cref="Dvtk.Sessions.MediaSession"/></item>
    /// <item><see cref="Dvtk.Sessions.EmulatorSession"/></item>
    /// </list>
    /// </remarks>
	public abstract class Session
		: IUserOutput
		, IGeneralSessionSettings
		, ISessionFileManagement
		, IValidationSettings
		, IDicomFile
	{
		private Serializer m_topSerializer = null;
		private CountManager m_topCountManager = null;
		private ActivityReporter m_activityReporter = null;
		private DefinitionManagement m_definitionManagement = null;
		internal DvtSystemSettings m_dvtSystemSettings = null;
		internal SutSystemSettings m_sutSystemSettings = null;
		internal SecuritySettings m_securitySettings = null;

		internal abstract Wrappers.MBaseSession m_MBaseSession
		{
			get;
		}

		/// <summary>
		/// Pause the gathering of validation results output.
		/// </summary>
		/// <remarks><see langword="true"/> to pause and <see langword="false"/> to continue.</remarks>
		public System.Boolean ResultsGatheringPaused
		{
			get 
			{ 
				return this.m_resultsGatheringPaused; 
			}
			set 
			{ 
				this.m_resultsGatheringPaused = value; 
			}
		} 
		private System.Boolean m_resultsGatheringPaused = false;

		/// <summary>
		/// Should be called by specialisation sub-class from the end of constructor. 
		/// </summary>
		/// <remarks>
		/// Precondition: m_MBaseSession should have been set by sub-class.
		/// </remarks>
		virtual internal void _Initialize()
		{
			this.m_definitionManagement = new DefinitionManagement(this.m_MBaseSession);
			this.m_dvtSystemSettings = new DvtSystemSettings(this.m_MBaseSession);
			this.m_sutSystemSettings = new SutSystemSettings(this.m_MBaseSession);
			this.m_securitySettings = new SecuritySettings(this.m_MBaseSession);
			//
			// Apply current date.
			//
			this.Date = System.DateTime.Now;
		}

		#region Serialization

		/// <summary>
		/// Expand Results File Name : Helper
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		public virtual string GetExpandedResultsFileNameHelper(
			string fileName)
		{
			System.UInt16 sessionId = this.SessionId;
			string dir = System.IO.Path.GetDirectoryName(fileName);
			string tempFileName = System.IO.Path.GetFileName(fileName);
			string sessionIdString = sessionId.ToString("000");
			tempFileName = tempFileName.Replace('.', '_');
			const string postfix = "res";
			const string extension = ".xml";
			System.Collections.ArrayList fileNameElementsArray =
				new System.Collections.ArrayList();
			if (sessionIdString != string.Empty)    fileNameElementsArray.Add(sessionIdString);
			if (tempFileName != string.Empty)       fileNameElementsArray.Add(tempFileName);
			if (postfix != string.Empty)            fileNameElementsArray.Add(postfix);
			string[] fileNameElements = 
				(string[])fileNameElementsArray.ToArray(typeof(string));
			tempFileName = string.Join("_", fileNameElements);
			tempFileName = System.IO.Path.ChangeExtension(tempFileName, extension);
			tempFileName = System.IO.Path.Combine(dir, tempFileName);
			return tempFileName;
		}

		/// <summary>
		/// Start the gathering of validation results output.
		/// </summary>
		/// <param name="fileName">The location of the file where you want to save.</param>
		/// <remarks>
		/// <p>
		/// Generates filename expansions that conform to the filenaming used within
		/// the XSLT of DVT.
		/// </p>
		/// <p>
		/// This ensures that results are browsable within the
		/// GUI application.
		/// </p>
		/// </remarks>
		public void StartResultsGatheringWithExpandedFileNaming(string fileName)
		{
			string expandedFileName =
				this.GetExpandedResultsFileNameHelper(fileName);
			this.StartResultsGathering(expandedFileName);
		}

		/// <summary>
		/// Start the gathering of validation results output.
		/// </summary>
		/// <param name="fileName">The location of the file where you want to save.</param>
		public void StartResultsGathering(string fileName)
		{
			//
			// Setup call-back interfaces for ;
			// - serialization
			// - counting
			// - activity reporting.
			//
			this.m_topSerializer = new Serializer(null, this, true, Wrappers.WrappedSerializerNodeType.TopParent);
			this.m_topCountManager = new CountManager(this);
			this.m_activityReporter = new ActivityReporter(this);
			//
			// Couple into adapter layer between unmanaged and managed code.
			//
			this.m_MBaseSession.InitTopSerializationAndCountingAndActivityReportingTargets(
				this.m_topSerializer, this.m_topCountManager, this.m_activityReporter);
			this.m_topSerializer.StartTopSerializer(fileName);
			this.m_topCountManager.SerializeEnabled = true;
		}

		/// <summary>
		/// Save the gathered validation results output to the specified stream.
		/// </summary>
		public void EndResultsGathering()
		{
			//
			// Forcefully end the serialization process.
			// End serialization process in a child to parent fashion.
			//
			this.m_topCountManager.SerializeEnabled = false;
			this.m_topSerializer.StopTopSerializer();
		}

		#endregion Serialization

		#region Events Sources (Publishers)
		/// <summary>
		/// Occurs when a activity report is generated by the application.
		/// </summary>
		public event ActivityReportEventHandler ActivityReportEvent;
		//
		// The protected OnProgress method raises the event by invoking 
		// the delegates. The sender is always this, the current instance 
		// of the class.
		//
		internal void _OnActivityReport(ActivityReportEventArgs e)
		{
			// Invoke the delegates
			if (ActivityReportEvent != null) 
				ActivityReportEvent(this, e);
		}
		#endregion

		/// <summary>
		/// Access to counting target.
		/// </summary>
		public Wrappers.ICountingTarget CountingTarget
		{
			get
			{ 
				return this.m_topCountManager; 
			}
		} 

		/// <summary>
		/// Number of Errors
		/// </summary>
		public System.UInt32 NrOfErrors
		{
			get
			{
				System.UInt32 nrOfErrors = 0;
				if (this.m_topCountManager != null)
				{
					nrOfErrors = this.m_topCountManager.NrOfErrors;
				}
				return nrOfErrors;
			}
		}

		/// <summary>
		/// Number of Warnings
		/// </summary>
		public System.UInt32 NrOfWarnings
		{
			get
			{
				System.UInt32 nrOfWarnings = 0;
				if (this.m_topCountManager != null)
				{
					nrOfWarnings = this.m_topCountManager.NrOfWarnings;
				}
				return nrOfWarnings;
			}
		}

		/// <summary>
		/// Number of NrOfValidationErrors
		/// </summary>
		public System.UInt32 NrOfValidationErrors
		{
			get
			{
				System.UInt32 NrOfValidationErrors = 0;
				if (this.m_topCountManager != null)
				{
					NrOfValidationErrors = this.m_topCountManager.NrOfValidationErrors;
				}
				return NrOfValidationErrors;
			}
			set
			{
				if (this.m_topCountManager != null)
				{
					this.m_topCountManager.NrOfValidationErrors = value;
				}
			}
		}

		/// <summary>
		/// Number of NrOfUserErrors
		/// </summary>
		public System.UInt32 NrOfUserErrors
		{
			get
			{
				System.UInt32 NrOfUserErrors = 0;
				if (this.m_topCountManager != null)
				{
					NrOfUserErrors = this.m_topCountManager.NrOfUserErrors;
				}
				return NrOfUserErrors;
			}
			set
			{
				if (this.m_topCountManager != null)
				{
					this.m_topCountManager.NrOfUserErrors = value;
				}
			}
		}

		/// <summary>
		/// Number of NrOfGeneralErrors
		/// </summary>
		public System.UInt32 NrOfGeneralErrors
		{
			get
			{
				System.UInt32 NrOfGeneralErrors = 0;
				if (this.m_topCountManager != null)
				{
					NrOfGeneralErrors = this.m_topCountManager.NrOfGeneralErrors;
				}
				return NrOfGeneralErrors;
			}
			set
			{
				if (this.m_topCountManager != null)
				{
					this.m_topCountManager.NrOfGeneralErrors = value;
				}
			}
		}

		/// <summary>
		/// Number of NrOfValidationWarnings
		/// </summary>
		public System.UInt32 NrOfValidationWarnings
		{
			get
			{
				System.UInt32 NrOfValidationWarnings = 0;
				if (this.m_topCountManager != null)
				{
					NrOfValidationWarnings = this.m_topCountManager.NrOfValidationWarnings;
				}
				return NrOfValidationWarnings;
			}
			set
			{
				if (this.m_topCountManager != null)
				{
					this.m_topCountManager.NrOfValidationWarnings = value;
				}
			}
		}

		/// <summary>
		/// Number of NrOfUserWarnings
		/// </summary>
		public System.UInt32 NrOfUserWarnings
		{
			get
			{
				System.UInt32 NrOfUserWarnings = 0;
				if (this.m_topCountManager != null)
				{
					NrOfUserWarnings = this.m_topCountManager.NrOfUserWarnings;
				}
				return NrOfUserWarnings;
			}
			set
			{
				if (this.m_topCountManager != null)
				{
					this.m_topCountManager.NrOfUserWarnings = value;
				}
			}
		}

		/// <summary>
		/// Number of NrOfGeneralWarnings
		/// </summary>
		public System.UInt32 NrOfGeneralWarnings
		{
			get
			{
				System.UInt32 NrOfGeneralWarnings = 0;
				if (this.m_topCountManager != null)
				{
					NrOfGeneralWarnings = this.m_topCountManager.NrOfGeneralWarnings;
				}
				return NrOfGeneralWarnings;
			}
			set
			{
				if (this.m_topCountManager != null)
				{
					this.m_topCountManager.NrOfGeneralWarnings = value;
				}
			}
		}

		/// <summary>
		/// Access to definition file management.
		/// </summary>
		public IDefinitionManagement DefinitionManagement
		{
			get 
			{ 
				return this.m_definitionManagement; 
			}
		} 

		#region IUserOutput
		/// <summary>
		/// <see cref="IUserOutput.WriteError"/>
		/// </summary>
		public void WriteError(System.String text)
		{
			if (text == null) throw new System.ArgumentNullException();
			this.m_activityReporter.ReportActivity(Wrappers.WrappedValidationMessageLevel.Error, text);
			this.m_topSerializer.SerializeUserReport(Wrappers.WrappedValidationMessageLevel.Error, text);
			this.m_topCountManager.Increment(Wrappers.CountGroup.User, Wrappers.CountType.Error);
		}
		/// <summary>
		/// <see cref="IUserOutput.WriteWarning"/>
		/// </summary>
		public void WriteWarning(System.String text)
		{
			if (text == null) throw new System.ArgumentNullException();
			this.m_activityReporter.ReportActivity(Wrappers.WrappedValidationMessageLevel.Warning, text);
			this.m_topSerializer.SerializeUserReport(Wrappers.WrappedValidationMessageLevel.Warning, text);
			this.m_topCountManager.Increment(Wrappers.CountGroup.User, Wrappers.CountType.Warning);
		}
		/// <summary>
		/// <see cref="IUserOutput.WriteInformation"/>
		/// </summary>
		public void WriteInformation(System.String text)
		{
			if (text == null) throw new System.ArgumentNullException();
			this.m_activityReporter.ReportActivity(Wrappers.WrappedValidationMessageLevel.Information, text);
			this.m_topSerializer.SerializeUserReport(Wrappers.WrappedValidationMessageLevel.Information, text);
		}

		/// <summary>
		/// <see cref="IUserOutput.WriteHtmlInformation"/>
		/// </summary>
		public void WriteHtmlInformation(System.String htmlText)
		{
			if (htmlText == null) throw new System.ArgumentNullException();
			this.m_activityReporter.ReportActivity(Wrappers.WrappedValidationMessageLevel.Information, "Writing HTML information (not displayed)");
			this.m_topSerializer.SerializeHtmlUserReport(Wrappers.WrappedValidationMessageLevel.Information, htmlText);
		}

		#endregion IUserOutput

		#region IValidationOutput
		/// <summary>
		/// <see cref="IValidationOutput.WriteMessageComparisonResults"/>
		/// </summary>
		public void WriteMessageComparisonResults(MessageComparisonResults messageComparisonResults)
		{
			if (messageComparisonResults == null) throw new System.ArgumentNullException();

			// set the IOD names from the session definition files
			messageComparisonResults.IodName1 = this.DefinitionManagement.GetIodNameFromDefinition(messageComparisonResults.Command1, messageComparisonResults.SopClassUid1);
			messageComparisonResults.IodName2 = this.DefinitionManagement.GetIodNameFromDefinition(messageComparisonResults.Command2, messageComparisonResults.SopClassUid2);

			// set the Attribute name from the session definition files
			foreach (AttributeComparisonResults acr in messageComparisonResults)
			{
				acr.Name = this.DefinitionManagement.GetAttributeNameFromDefinition(acr.Tag);

				// if any messages are associated with the result - increment the error count
				if (acr.Messages.Count > 0)
				{
					this.m_topCountManager.Increment(Wrappers.CountGroup.Validation, Wrappers.CountType.Error);
				}
			}

			this.m_topSerializer.SerializeMessageComparisonResults(messageComparisonResults);
		}

		/// <summary>
		/// <see cref="IValidationOutput.WriteValidationError"/>
		/// </summary>
		public void WriteValidationError(System.String text)
		{
			if (text == null) throw new System.ArgumentNullException();
			this.m_topSerializer.SerializeValidationReport(Wrappers.WrappedValidationMessageLevel.Error, text);
			this.m_topCountManager.Increment(Wrappers.CountGroup.Validation, Wrappers.CountType.Error);
		}

		/// <summary>
		/// <see cref="IValidationOutput.WriteValidationWarning"/>
		/// </summary>
		public void WriteValidationWarning(System.String text)
		{
			if (text == null) throw new System.ArgumentNullException();
			this.m_topSerializer.SerializeValidationReport(Wrappers.WrappedValidationMessageLevel.Warning, text);
			this.m_topCountManager.Increment(Wrappers.CountGroup.Validation, Wrappers.CountType.Warning);
		}

		/// <summary>
		/// <see cref="IValidationOutput.WriteValidationInformation"/>
		/// </summary>
		public void WriteValidationInformation(System.String text)
		{
			if (text == null) throw new System.ArgumentNullException();
			this.m_topSerializer.SerializeValidationReport(Wrappers.WrappedValidationMessageLevel.Information, text);
		}
		#endregion IValidationOutput

        #region IGeneralSessionSettings
        /// <summary>
        /// <see cref="IGeneralSessionSettings.SessionFileName"/>
        /// </summary>
        public String SessionFileName
        {
            get
            { 
                return this.m_MBaseSession.SessionFileName; 
            }
            set 
            { 
                if (value == null) throw new System.ArgumentNullException();
                this.m_MBaseSession.SessionFileName = value; 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.ResultsRootDirectory"/>
        /// </summary>
        public System.String ResultsRootDirectory
        {
            get 
            { 
                return this.m_MBaseSession.ResultsRootDirectory; 
            }
            set 
            { 
                if (value == null) throw new System.ArgumentNullException();
                if (value == string.Empty) 
                    throw new System.ArgumentException(
                        "ResultsRootDirectory may not be an empty string.\n"+
                        "Use \".\" for current directory.");
                this.m_MBaseSession.ResultsRootDirectory = value; 
            }
        }
		/// <summary>
		/// <see cref="IGeneralSessionSettings.DataDirectory"/>
		/// </summary>
		public System.String DataDirectory
		{
			get 
			{ 
				return this.m_MBaseSession.DataDirectory; 
			}
			set 
			{ 
				if (value == null) throw new System.ArgumentNullException();
				if (value == string.Empty) 
					throw new System.ArgumentException(
						"DataDirectory may not be an empty string.\n"+
						"Use \".\" for current directory.");
				this.m_MBaseSession.DataDirectory = value; 
			}
		}
        /// <summary>
        /// <see cref="IGeneralSessionSettings.Date"/>
        /// </summary>
        public System.DateTime Date
        {
            get 
            {
                //
                // Retrieve date as char* of format yyyyMMdd
                //
                System.String dateString = this.m_MBaseSession.Date;
                String format = "yyyyMMdd";
                return System.DateTime.ParseExact(dateString, format, null);
            }
            set
            {
                //
                // The year in four digits, including the century.
                // The numeric month. Single-digit months will have a leading zero.
                // The day of the month. Single-digit days will have a leading zero.
                //
                String format = "yyyyMMdd";
                String date = value.ToString(format);
                this.m_MBaseSession.Date = date; 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.SessionTitle"/>
        /// </summary>
        public System.String SessionTitle
        {
            get 
            { 
                return this.m_MBaseSession.SessionTitle; 
            }
            set 
            { 
                if (value == null) throw new System.ArgumentNullException();
                this.m_MBaseSession.SessionTitle = value; 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.SessionId"/>
        /// </summary>
        public System.UInt16 SessionId
        {
            get 
            { 
                return this.m_MBaseSession.SessionId; 
            }
            set 
            { 
                this.m_MBaseSession.SessionId = value; 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.SoftwareVersions"/>
        /// </summary>
        public System.String SoftwareVersions
        {
            get 
            { 
                return this.m_MBaseSession.SoftwareVersions; 
            }
            set 
            { 
                if (value == null) throw new System.ArgumentNullException();
                this.m_MBaseSession.SoftwareVersions = value; 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.TestedBy"/>
        /// </summary>
        public System.String TestedBy
        {
            get 
            { 
                return this.m_MBaseSession.TestedBy; 
            }
            set 
            { 
                if (value == null) throw new System.ArgumentNullException();
                this.m_MBaseSession.TestedBy = value; 
            }
        }
        /// <summary>
        /// Conversion Dvtk type => Wrappers type
        /// </summary>
        /// <param name="value">in</param>
        /// <returns>out</returns>
        static private Wrappers.StorageMode
            _Convert(Dvtk.Sessions.StorageMode value)
        {
            switch (value)
            {
                case Dvtk.Sessions.StorageMode.AsDataSet:
                    return Wrappers.StorageMode.StorageModeAsDataSet;
                case Dvtk.Sessions.StorageMode.AsMedia:
                    return Wrappers.StorageMode.StorageModeAsMedia;
                case Dvtk.Sessions.StorageMode.NoStorage:
                    return Wrappers.StorageMode.StorageModeNoStorage;
                case Dvtk.Sessions.StorageMode.TemporaryPixelOnly:
                    return Wrappers.StorageMode.StorageModeTemporaryPixelOnly;
                default:
                    // Unknown Dvtk.Sessions.StorageMode
                    throw new System.NotImplementedException();
            }
        }
        /// <summary>
        /// Conversion Wrappers type => Dvtk type
        /// </summary>
        /// <param name="value">in</param>
        /// <returns>out</returns>
        static private Dvtk.Sessions.StorageMode
            _Convert(Wrappers.StorageMode value)
        {
            switch (value)
            {
                case Wrappers.StorageMode.StorageModeAsDataSet:
                    return Dvtk.Sessions.StorageMode.AsDataSet;
                case Wrappers.StorageMode.StorageModeAsMedia:
                    return Dvtk.Sessions.StorageMode.AsMedia;
                case Wrappers.StorageMode.StorageModeNoStorage:
                    return Dvtk.Sessions.StorageMode.NoStorage;
                case Wrappers.StorageMode.StorageModeTemporaryPixelOnly:
                    return Dvtk.Sessions.StorageMode.TemporaryPixelOnly;
                default:
                    // Unknown Dvtk.Sessions.StorageMode
                    throw new System.NotImplementedException();
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.StorageMode"/>
        /// </summary>
        public StorageMode StorageMode
        {
            get 
            { 
                return _Convert(this.m_MBaseSession.StorageMode); 
            }
            set 
            { 
                this.m_MBaseSession.StorageMode = _Convert(value); 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.Manufacturer"/>
        /// </summary>
        public System.String Manufacturer
        {
            get 
            { 
                return this.m_MBaseSession.Manufacturer; 
            }
            set 
            { 
                if (value == null) throw new System.ArgumentNullException();
                this.m_MBaseSession.Manufacturer = value; 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.ModelName"/>
        /// </summary>
        public System.String ModelName
        {
            get 
            { 
                return this.m_MBaseSession.ModelName; 
            }
            set 
            { 
                if (value == null) throw new System.ArgumentNullException();
                this.m_MBaseSession.ModelName = value; 
            }
        }
        /// <summary>
        /// <see cref="IGeneralSessionSettings.LogLevelFlags"/>
        /// </summary>
        public LogLevelFlags LogLevelFlags
        {
            get 
            { 
                LogLevelFlags logLevel = new LogLevelFlags();

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.None))
				{
					logLevel |= LogLevelFlags.None;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.Error))
				{
					logLevel |= LogLevelFlags.Error;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.Debug))
				{
					logLevel |= LogLevelFlags.Debug;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.Warning))
				{
					logLevel |= LogLevelFlags.Warning;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.Info))
				{
					logLevel |=  LogLevelFlags.Info;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.Script))
				{
					logLevel |= LogLevelFlags.Script;
				}
				
				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.ScriptName))
				{
					logLevel |= LogLevelFlags.ScriptName;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.PduBytes))
				{
					logLevel |= LogLevelFlags.PduBytes;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.DulpFsm))
				{
					logLevel |= LogLevelFlags.DulpFsm;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.ImageRelation))
				{
					logLevel |= LogLevelFlags.ImageRelation;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.Print))
				{
					logLevel |= LogLevelFlags.Print;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.Label))
				{
					logLevel |= LogLevelFlags.Label;
				}

				if (this.m_MBaseSession.get_LogLevelEnabled((uint)LogLevelFlags.MediaFilename))
				{
					logLevel |= LogLevelFlags.MediaFilename;
				}

                return logLevel;
            }
            set 
            {
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.None, ((value & LogLevelFlags.None) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.Error, ((value & LogLevelFlags.Error) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.Debug, ((value & LogLevelFlags.Debug) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.Warning, ((value & LogLevelFlags.Warning) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.Info, ((value & LogLevelFlags.Info) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.Script, ((value & LogLevelFlags.Script) != 0));
				this.m_MBaseSession.set_LogLevelEnabled(
					(uint)LogLevelFlags.ScriptName, ((value & LogLevelFlags.ScriptName) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.PduBytes, ((value & LogLevelFlags.PduBytes) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.DulpFsm, ((value & LogLevelFlags.DulpFsm) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.ImageRelation, ((value & LogLevelFlags.ImageRelation) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.Print, ((value & LogLevelFlags.Print) != 0));
                this.m_MBaseSession.set_LogLevelEnabled(
                    (uint)LogLevelFlags.Label, ((value & LogLevelFlags.Label) != 0));
				this.m_MBaseSession.set_LogLevelEnabled(
					(uint)LogLevelFlags.MediaFilename, ((value & LogLevelFlags.MediaFilename) != 0));
			}
        }
		/// <summary>
		/// <see cref="IGeneralSessionSettings.AutoCreateDirectory"/>
		/// </summary>
		public System.Boolean AutoCreateDirectory
		{
			get 
			{ 
				return this.m_MBaseSession.AutoCreateDirectory; 
			}
			set 
			{ 
				this.m_MBaseSession.AutoCreateDirectory = value; 
			}
		}
		/// <summary>
		/// <see cref="IGeneralSessionSettings.ContinueOnError"/>
		/// </summary>
		public System.Boolean ContinueOnError
		{
			get 
			{ 
				return this.m_MBaseSession.ContinueOnError; 
			}
			set 
			{ 
				this.m_MBaseSession.ContinueOnError = value; 
			}
		}

        #endregion

        #region ISessionFileManagement
        /// <summary>
        /// <see cref="ISessionFileManagement.SaveToFile()"/>
        /// </summary>
        public bool SaveToFile()
        {
            return this.m_MBaseSession.Save();
        }
        #endregion

		#region IValidationSettings
		/// <summary>
		/// <see cref="IValidationSettings.StrictValidation"/>
		/// </summary>
		public System.Boolean StrictValidation
		{
			get 
			{ 
				return this.m_MBaseSession.StrictValidation; 
			}
			set 
			{ 
				this.m_MBaseSession.StrictValidation = value; 
			}
		}
		/// <summary>
		/// <see cref="IValidationSettings.DetailedValidationResults"/>
		/// </summary>
		public System.Boolean DetailedValidationResults
		{
			get 
			{ 
				return this.m_MBaseSession.DetailedValidationResults; 
			}
			set 
			{ 
				this.m_MBaseSession.DetailedValidationResults = value; 
			}
		}
		/// <summary>
		/// <see cref="IValidationSettings.SummaryValidationResults"/>
		/// </summary>
		public System.Boolean SummaryValidationResults
		{
			get 
			{ 
				return this.m_MBaseSession.SummaryValidationResults; 
			}
			set 
			{ 
				this.m_MBaseSession.SummaryValidationResults = value; 
			}
		}
		/// <summary>
		/// <see cref="IValidationSettings.IncludeType3NotPresentInResults"/>
		/// </summary>
		public System.Boolean IncludeType3NotPresentInResults
		{
			get 
			{ 
				return this.m_MBaseSession.IncludeType3NotPresentInResults; 
			}
			set 
			{ 
				this.m_MBaseSession.IncludeType3NotPresentInResults = value; 
			}
		}
		/// <summary>
		/// <see cref="IValidationSettings.ValidateReferencedFile"/>
		/// </summary>
		public System.Boolean ValidateReferencedFile
		{
			get 
			{ 
				return this.m_MBaseSession.ValidateReferencedFile; 
			}
			set 
			{ 
				this.m_MBaseSession.ValidateReferencedFile = value; 
			}
		}
		#endregion

		#region IDicomFile
		/// <summary>
		/// <see cref="IDicomFile.WriteFile"/>
		/// </summary>
		public bool WriteFile(
			DicomFile file,
			string mediaFileName)
		{
			if (file == null) throw new System.ArgumentNullException("file");
			if (mediaFileName == null) throw new System.ArgumentNullException("mediaFileName");
			return this.m_MBaseSession.WriteMedia(file, mediaFileName);
		}
		/// <summary>
		/// <see cref="IDicomFile.ReadFile"/>
		/// </summary>
		public DicomFile ReadFile(
			string mediaFileName)
		{
			if (mediaFileName == null) throw new System.ArgumentNullException("mediaFileName");
			return this.m_MBaseSession.ReadMedia(mediaFileName);
		}

		#endregion
    }
}