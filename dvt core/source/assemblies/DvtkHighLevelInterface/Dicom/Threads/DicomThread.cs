using System;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

using DvtkHighLevelInterface.Messages;


namespace DvtkHighLevelInterface
{
	/// <summary>
	/// This class represents a single thread in which Dicom communication may be tested.
	/// </summary>
	public abstract class DicomThread: Thread
	{
		//
		// - Constant fields -
		//

		private const String alreadyInitializedErrorText = "Initialize has already been called and may not be called again.";

		private const String notInitializedErrorText = "Initialize must be called first before calling other methods and properties.";
		
		//
		// - Fields -
		//

		/// <summary>
		/// The event handler that will handle ActivityReportEvents that are 
		/// fired by the encapsulated dvtkScriptSession object.
		/// </summary>
		private Dvtk.Events.ActivityReportEventHandler activityReportEventHandler = null;

		/// <summary>
		/// See property DvtkScriptSession.
		/// </summary>
		private Dvtk.Sessions.ScriptSession dvtkScriptSession = null;
	
		/// <summary>
		/// Boolean indicating if one of the Initialize methods has already been called.
		/// </summary>
		private bool isInitialized = false;

		/// <summary>
		/// Boolean indicating if this object has been stopped by calling the Stop method.
		/// </summary>
		private bool isStopCalled = false;

		/// <summary>
		/// Indicates if writing to the results is allowed.
		/// </summary>
		private bool isWritingAllowed = false;

		/// <summary>
		/// See property Messages.
		/// </summary>
		private DicomProtocolMessageCollection messages = new DicomProtocolMessageCollection();

		/// <summary>
		/// Thread used to stop this object and sub Threads, in order to let the Stop method not
		/// wait.
		/// </summary>
		private System.Threading.Thread stopDotNetThread = null;



		//
		// - Properties -
		//

		/// <summary>
		/// The encapsulated ScriptSession from the Dvtk librbary.
		/// </summary>
		internal Dvtk.Sessions.ScriptSession DvtkScriptSession
		{
			get
			{
				return(this.dvtkScriptSession);
			}
			set
			{
				this.dvtkScriptSession = value;
			}
		}

		/// <summary>
		/// The messages that have been send en received by this object.
		/// </summary>
		internal DicomProtocolMessageCollection Messages
		{
			get
			{
				return(this.messages);
			}
		}

		/// <summary>
		/// Get the options for this object.
		/// </summary>
		public new DicomThreadOptions Options
		{
			get
			{
				// Initialize must be called first, so check for this.
				if (!this.isInitialized)
				{
					throw new HliException(notInitializedErrorText);
				}
				
				// this.threadOptions must be of type DicomThreadOptions,
				// because it is set in the constructor of this class.
				return(this.threadOptions as DicomThreadOptions);
			}
		}

		/// <summary>
		/// Get the current results filename.
		/// </summary>
		public System.String ResultsFileName
		{
			get
			{
				return this.resultsFileName;
			}
		}
		private System.String resultsFileName;



		//
		// - Methods -
		//
		/// <summary>
		/// Set the results filename from the given filename.
		/// </summary>
		/// <param name="filename">Base results filename.</param>
		private void SetResultsFilename(System.String filename)
		{
			this.resultsFileName = filename;
			if (Options.ResultsFilePerAssociation == true)
			{
				System.String filenameFormat = resultsFileName.Replace("res", "res{0}");
				this.resultsFileName = System.String.Format(filenameFormat, Options.NextSubResultsFileNameIndex);
			}
		}

		/// <summary>
		/// Check whether or not a new results file should be opened.
		/// If so stop the results gathering to the current results file
		/// and start the next one.
		/// </summary>
		protected void CheckForNewResultsFile()
		{
			if ((Options.StartAndEndResultsGathering) &&
				(Options.ResultsFilePerAssociation == true))
			{
				this.dvtkScriptSession.EndResultsGathering();

				SetResultsFilename(Options.ResultsFileName);
				this.dvtkScriptSession.StartResultsGathering(this.resultsFileName);
			}
		}

		/// <summary>
		/// In a descendant of this class, the actual test code is placed.
		/// </summary>
		abstract protected void Execute();

		private void HandleExeption(System.Exception exception)
		{
			if (!this.isStopCalled)
			{
				// Write the fatal error text.
				String errorText = "Fatal error of type " + exception.GetType().ToString() + "!";

				errorText+= "\r\n\r\nFatal error description:\r\n";
				errorText+= exception.Message;
				
				WriteError(errorText);

				// Write extra information about the fatal error.
				String extraInformation = "Extra information about the fatal error.";

				System.Exception innerException = exception.InnerException;

				while (innerException != null)
				{
					extraInformation = "\r\n\r\n" + "[F]\r\n" + innerException.Message;

					innerException = innerException.InnerException;
				}
				/*
				String messages = // InterfaceLogging.GetMessages();

				if (messages != "")
				{
					extraInformation = "\r\n\r\n" + messages;
				}
				*/

				extraInformation+= "\r\n\r\nStack trace:\r\n" + exception.StackTrace;

				WriteInformation(extraInformation);

				// If this is not the topmost thread, report the fatal error to it.
				if (TopmostThread != this)
				{
					TopmostThread.WriteInformationInternal("Fatal error in " + this.ToString() + " \"" + Options.Identifier + "\"!");
				}
			}

			// If the exception is a ThreadAbortException, we have to cancel the abort, otherwise
			// the .Net runtime will again throw this exception after the cath block.
			if (exception is System.Threading.ThreadAbortException)
			{
				System.Threading.Thread.ResetAbort();
			}
		}

		/// <summary>
		/// This method will be called when the encapsulated Dvtk ScriptSession object raises
		/// an ActivityReportEvent.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The content of the event.</param>
		private void HandleActivityReportEvent(object sender, Dvtk.Events.ActivityReportEventArgs e)
		{
			switch(e.ReportLevel)
			{
				case Dvtk.Events.ReportLevel.Error:
					TriggerErrorOutputEvent(e.Message);
					break;

				case Dvtk.Events.ReportLevel.Warning:
					TriggerWarningOutputEvent(e.Message);
					break;

				default:
					TriggerInformationOutputEvent(e.Message);
					break;
			}			
		}

		/// <summary>
		/// Common initialization code, called from the other Initialize methods from
		/// this class.
		/// </summary>
		private void Initialize()
		{
			this.threadOptions = new DicomThreadOptions(this);

			this.activityReportEventHandler = new Dvtk.Events.ActivityReportEventHandler(this.HandleActivityReportEvent);
		}

		/// <summary>
		/// Initialize this object as a DicomThread with no parent thread.
		/// </summary>
		/// <param name="threadManager">The threadManager.</param>
		public new void Initialize(ThreadManager threadManager)
		{
			// Initialize may only be called once, so check for this.
			if (this.isInitialized)
			{
				throw new HliException(alreadyInitializedErrorText);
			}

			base.Initialize(threadManager);
			this.dvtkScriptSession = new Dvtk.Sessions.ScriptSession();
			Initialize();
			this.isInitialized = true;
		}

		/// <summary>
		/// Initialize this object as a DicomThread with a parent thread.
		/// </summary>
		/// <param name="parent">The parent thread.</param>
		public new void Initialize(Thread parent)
		{
			// Initialize may only be called once, so check for this.
			if (this.isInitialized)
			{
				throw new HliException(alreadyInitializedErrorText);
			}

			base.Initialize(parent);
			this.dvtkScriptSession = new Dvtk.Sessions.ScriptSession();
			Initialize();
			this.isInitialized = true;
		}

		/// <summary>
		/// Stop this object and all child threads.
		/// 
		/// The caller of this method will not wait until the threads have ended.
		/// </summary>
		public override void Stop()
		{
			// Initialize must be called first, so check for this.
			if (!this.isInitialized)
			{
				throw new HliException(notInitializedErrorText);
			}

			this.isStopCalled = true;

			// Stop all child threads.
			foreach(Thread childThread in this.childs)
			{
				childThread.Stop();
			}

			// While the child threads are stopping, stop this object also.
			// This object will wait in the method ThreadEntryPoint until all
			// child threads have ended.
			bool terminateConnectionAndAbort = false;

			lock(commonThreadLock)
			{
				if (ThreadState == ThreadState.UnStarted)
				{
					ThreadManager.ChangeThreadState(this, ThreadState.Stopped);
				}
				else if (ThreadState == ThreadState.Running)
				{
					terminateConnectionAndAbort = true;
				}
			}

			if (terminateConnectionAndAbort)
			{
				this.stopDotNetThread = new System.Threading.Thread(new System.Threading.ThreadStart(TerminateConnectionAndAbortThreadEntryPoint));

				this.stopDotNetThread.Start();
			}
		}

		private void TerminateConnectionAndAbortThreadEntryPoint()
		{
			System.Threading.Thread.CurrentThread.Name = "TerminateConnectionAndAbort thread for Thread \"" + Options.Identifier + "\"";

			bool terminateConnectionAndAbort = false;

			// Extra check to see if this Thread is still running.
			lock(commonThreadLock)
			{
				if (ThreadState == ThreadState.Running)
				{
					terminateConnectionAndAbort = true;
				}
			}

			if (terminateConnectionAndAbort)
			{
				// If this thread is listening to a port, this will terminate it.
				// We need to do this because an .Net Abort will not always succeed when
				// this thread is listening to a port.
				this.dvtkScriptSession.TerminateConnection();

				// Give thread time to terminate the connection.
				System.Threading.Thread.Sleep(1000);

				lock(commonThreadLock)
				{
					// If the TerminateConnection call did not make the thread stop or the thread
					// didn't end by itself, do this with a .Net Thread Abort.
					if (ThreadState == ThreadState.Running)
					{
						this.dotNetThread.Abort();
					}
				}
			}
		}

		/// <summary>
		/// This method is called when this object is stopping.
		/// </summary>
		private void Stopping()
		{
			// Add all errors and warnings of the sub Threads to the totals of this object.
			PerformSubThreadsAdministration();

			String text = String.Format("DicomThread \"{0}\" has ended with {1} errors and {2} warnings.", Options.Identifier, this.dvtkScriptSession.NrOfErrors, this.dvtkScriptSession.NrOfWarnings);

			// If this Thread has a parent Thread, report the ending to it.
			if (this.parent != null)
			{
				this.parent.WriteInformationInternal(text);
			}
				// If this is the topmost thread make the ending through an
				// output event.
			else
			{
				if (this.dvtkScriptSession.NrOfErrors > 0)
				{
					TriggerErrorOutputEvent(text);
				}
				else if (this.dvtkScriptSession.NrOfWarnings > 0)
				{
					TriggerWarningOutputEvent(text);
				}
				else
				{
					TriggerInformationOutputEvent(text);
				}
			}

			this.isWritingAllowed = false;

			if (Options.StartAndEndResultsGathering)
			{

				this.dvtkScriptSession.EndResultsGathering();

			}

			this.dvtkScriptSession.ActivityReportEvent -= activityReportEventHandler;

			ThreadManager.ChangeThreadState(this, ThreadState.Stopped);

			if (Options.ShowResults)
			{
				ShowResults();
			}
		}

		public void NonThreadedStart()
		{
			if (Options.StartAndEndResultsGathering)
			{
				SetResultsFilename(Options.ResultsFileName);
				this.dvtkScriptSession.StartResultsGathering(this.resultsFileName);
			}
		}

		public void NonThreadedStop()
		{
			if (Options.StartAndEndResultsGathering)
			{

				this.dvtkScriptSession.EndResultsGathering();
			}
		}

		/// <summary>
		/// Get the number of errors during this thread execution.
		/// </summary>
		public uint NrOfErrors
		{
			get
			{
				return this.dvtkScriptSession.NrOfErrors;
			}
		}

		/// <summary>
		/// Get the number of warnings during this thread execution.
		/// </summary>
		public uint NrOfWarnings
		{
			get
			{
				return this.dvtkScriptSession.NrOfWarnings;
			}
		}

		/// <summary>
		/// Add all errors and warnings of the sub Threads to the totals of this object.
		/// </summary>
		private void PerformSubThreadsAdministration()
		{
			//
			// Add all errors and warnings of the sub Threads to the totals of this object.
			//

			UInt32 nrOfGeneralErrorsToAdd = 0;
			UInt32 nrOfGeneralWarningsToAdd = 0;
			UInt32 nrOfUserErrorsToAdd = 0;
			UInt32 nrOfUserWarningsToAdd = 0;
			UInt32 nrOfValidationErrorsToAdd = 0;
			UInt32 nrOfValidationWarningsToAdd = 0;

			lock(this.commonThreadLock)
			{
				foreach (Thread childThread in this.childs)
				{
					if (childThread is DicomThread)
					{
						DicomThread childDicomThread = childThread as DicomThread;

						nrOfGeneralErrorsToAdd+= childDicomThread.DvtkScriptSession.NrOfGeneralErrors;
						nrOfGeneralWarningsToAdd+= childDicomThread.DvtkScriptSession.NrOfGeneralWarnings;
						nrOfUserErrorsToAdd+= childDicomThread.DvtkScriptSession.NrOfUserErrors;
						nrOfUserWarningsToAdd+= childDicomThread.DvtkScriptSession.NrOfUserWarnings;
						nrOfValidationErrorsToAdd+= childDicomThread.DvtkScriptSession.NrOfValidationErrors;
						nrOfValidationWarningsToAdd+= childDicomThread.DvtkScriptSession.NrOfValidationWarnings;
					}
				}
			}

			this.DvtkScriptSession.NrOfGeneralErrors+= nrOfGeneralErrorsToAdd;
			this.DvtkScriptSession.NrOfGeneralWarnings+= nrOfGeneralWarningsToAdd;
			this.DvtkScriptSession.NrOfUserErrors+= nrOfUserErrorsToAdd;
			this.DvtkScriptSession.NrOfUserWarnings+= nrOfUserWarningsToAdd;
			this.DvtkScriptSession.NrOfValidationErrors+= nrOfValidationErrorsToAdd;
			this.DvtkScriptSession.NrOfValidationWarnings+= nrOfValidationWarningsToAdd;

			//
			// Make a HTML table in which the different child Threads with hyperlinks (if possible) are stated.
			// If no child Threads exist, don't display the table.
			//

			WriteHtmlInformation(childs.GetStartedThreadsOverviewAsHTML("Started Child Threads Overview", "No child Threads have been started.", Options.ResultsDirectory));
		}

		/// <summary>
		/// The .Net thread will execute the code that is present in this method.
		/// </summary>
		protected override void ThreadEntryPoint()
		{
			// Determine the name.
			if (Options.Name == null)
			{
				Options.Name = this.GetType().Name;
			}

			// Determine the (unique) identifier.
			if (Options.Identifier == null)
			{
				Options.Identifier = ThreadManager.GetUniqueIdentifier(this);
			}

			this.dotNetThread.Name = Options.Identifier;
			
			// Do an initial wait if necessary.
			if (this.initialMillisecondsToWait > 0)
			{
				if (TopmostThread != this)
				{
					TopmostThread.WriteInformationInternal(String.Format("Waiting {0} milliseconds before starting DicomThread \"{1}\".", this.initialMillisecondsToWait.ToString(), Options.Identifier));
				}

				System.Threading.Thread.Sleep(this.initialMillisecondsToWait);
			}

			if (this.parent != null)
			{
				this.parent.WriteInformationInternal(String.Format("DicomThread \"{0}\" has started.", Options.Identifier));
			}

			// The results directory en results filename should be determined in this overriden method.
			ThreadManager.SetResultsOptions(this);

			if (Options.StartAndEndResultsGathering)
			{
				SetResultsFilename(Options.ResultsFileName);
				this.dvtkScriptSession.StartResultsGathering(this.resultsFileName);
			}

			this.isWritingAllowed = true;

			WriteHtmlInformation("<br /><b> DicomThread \"" + Options.Identifier + "\"</b><br />");

			this.dvtkScriptSession.ActivityReportEvent += activityReportEventHandler;

			// Now execute the actual testing code.
			try
			{
				Execute();

				ThreadManager.ChangeThreadState(this, ThreadState.Stopping);
			}
			catch (System.Exception exception)
			{
				// If an exception was thrown, the ThreadState is still running.
				ThreadManager.ChangeThreadState(this, ThreadState.Stopping);

				HandleExeption(exception);
			}
			finally
			{	
				// Wait untill all child threads are in a state unstarted or stopped.
				if (this.childs.Count > 0)
				{
					WriteInformation("Waiting for child Threads to complete...");
				}

				this.childs.WaitForCompletion();

				if (this.childs.Count > 0)
				{
					WriteInformation("...child Threads completed.");
				}

				Stopping();
			}
		}

		protected void TriggerAssociationReleasedEvent(DicomThread dicomThread)
		{
			if (AssociationReleasedEvent != null)
			{
				AssociationReleasedEvent(dicomThread);
			}
		}

		/// <summary>
		/// Write an error to the results.
		/// </summary>
		/// <param name="text">The error text.</param>
		public override void WriteError(String text)
		{
			if (this.isWritingAllowed)
			{
				this.dvtkScriptSession.WriteError(text);
			}
			else
			{
				TriggerErrorOutputEvent(text);
			}
		}

		/// <summary>
		/// Write information to the results.
		/// </summary>
		/// <param name="text">The information text.</param>
		public override void WriteInformation(String text)
		{
			if (this.isWritingAllowed)
			{
				this.dvtkScriptSession.WriteInformation(text);
			}
			else
			{
				TriggerInformationOutputEvent(text);
			}
		}

		/// <summary>
		/// This method will only be used for now for displaying hyperlinks to the sub results.
		/// When it is needed in other places, re-check if the current implementation is sufficient.
		/// </summary>
		/// <param name="text"></param>
		public void WriteHtmlInformation(String html)
		{
			if (this.isWritingAllowed)
			{
				this.dvtkScriptSession.WriteHtmlInformation(html);
			}
		}

		/// <summary>
		/// Write a warning to the results.
		/// </summary>
		/// <param name="text">The warning text.</param>
		public override void WriteWarning(String text)
		{
			if (this.isWritingAllowed)
			{
				this.dvtkScriptSession.WriteWarning(text);
			}
			else
			{
				TriggerWarningOutputEvent(text);
			}

		}

		/// <summary>
		/// Get the definition IOD Name from the loaded definition files.
		/// </summary>
		/// <param name="dicomMessage">Dicom Message for which the IOD Name is being sought.</param>
		/// <returns>Iod Name for the matching DIMSE command and SOP Class UID found in the Dicom Message.</returns>
		public System.String GetIodNameFromDefinition(DicomMessage dicomMessage)
		{
			String iodName = String.Empty;

			// Get the DIMSE command
			DvtkData.Dimse.DimseCommand command = dicomMessage.CommandSet.DvtkDataCommandSet.CommandField;

			// Get the SOP Class UID.
			String sopClassUid = dicomMessage.CommandSet.GetSopClassUid();

			if (sopClassUid.Length > 0)
			{
				// Try to get the IOD Name from the loaded definition files
				iodName = this.dvtkScriptSession.DefinitionManagement.GetIodNameFromDefinition(command, sopClassUid);
			}

			// return the IOD Name - maybe empty if no match found
			return iodName;
		}

		/// <summary>
		/// Send a DicomMessage.
		/// </summary>
		/// <param name="dicomMessage">The DicomMessage.</param>
		public void Send(DicomMessage dicomMessage)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, dicomMessage);

			// Apply the (possible) outbound filters to add/remove/change attributes in the Dicom message to send.
			foreach(OutboundMessageFilter outboundMessageFilter in this.outboundMessageFilters)
			{
				outboundMessageFilter.Apply(dicomMessage);
			}

			Dvtk.Sessions.SendReturnCode sendReturnCode = DvtkScriptSession.Send(dicomMessage.DvtkDataDicomMessage);

			if (sendReturnCode != Dvtk.Sessions.SendReturnCode.Success)
			{
				DvtkHighLevelInterfaceException.Throw("Error sending dicom message (" + sendReturnCode.ToString() + ")"); 
			}
			else
			{	
				dicomMessage.IsSend = true;
				ThreadManager.DataWarehouse.AddMessage(this, dicomMessage);
			}

			// End Interface logging.
			// InterfaceLogging.End();
		}


		/// <summary>
		/// Send a A-ASSOCIATE-RQ.
		/// </summary>
		/// <param name="presentationContexts">One or more PresentationContext objects.</param>
		/// <returns>The A-ASSOCIATE-RQ</returns>
		public AssociateRq SendAssociateRq(params PresentationContext[] presentationContexts)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			AssociateRq associateRq = null;

			// Create a DvtkData ASSOCIATE request and set default values.
			DvtkData.Dul.A_ASSOCIATE_RQ dvtkDataAssociateRq  = new DvtkData.Dul.A_ASSOCIATE_RQ();
			dvtkDataAssociateRq.CallingAETitle = DvtkScriptSession.DvtSystemSettings.AeTitle;
			dvtkDataAssociateRq.CalledAETitle = DvtkScriptSession.SutSystemSettings.AeTitle;
			dvtkDataAssociateRq.UserInformation.MaximumLength.MaximumLengthReceived = Convert.ToUInt32(16384);
			dvtkDataAssociateRq.UserInformation.ImplementationClassUid.UID = "100.118.116.2003.1.4";
			
			// Parse all parameters.
			foreach(PresentationContext presentationContext in presentationContexts)
			{
				if (!presentationContext.InterpretAsPcForAssociateRq())
				{
					DvtkHighLevelInterfaceException.Throw("Error while interpreting parameters for PresentationContext");
				}

				DvtkData.Dul.RequestedPresentationContext requestedPresentationContext = new DvtkData.Dul.RequestedPresentationContext();

				requestedPresentationContext.AbstractSyntax = new DvtkData.Dul.AbstractSyntax(presentationContext.SopClass);

				foreach (String transferSyntax in presentationContext.TransferSyntaxes)
				{
					requestedPresentationContext.AddTransferSyntaxes(new DvtkData.Dul.TransferSyntax(transferSyntax));
				}

				dvtkDataAssociateRq.AddPresentationContexts(requestedPresentationContext);	
			}

			Dvtk.Sessions.SendReturnCode sendReturnCode = DvtkScriptSession.Send(dvtkDataAssociateRq);

			if (sendReturnCode != Dvtk.Sessions.SendReturnCode.Success)
			{
				DvtkHighLevelInterfaceException.Throw("Error sending associate request (" + sendReturnCode.ToString() + ")"); 
			}
			else
			{
				associateRq = new AssociateRq(dvtkDataAssociateRq);

				associateRq.IsSend = true;
				ThreadManager.DataWarehouse.AddMessage(this, associateRq);
			}

			// End Interface logging.
			// InterfaceLogging.End(associateRq);

			return(associateRq);
		}


		// Return boolean geeft aan of associatie wel of niet geaccept is.
		// This method only expects one response.
		public bool SendAssociation(DicomMessage dicomMessage, params PresentationContext[] presentationContexts)
		{
			DicomMessageCollection dicomMessages = new DicomMessageCollection();

			dicomMessages.Add(dicomMessage);

			return(SendAssociation(dicomMessages, presentationContexts));
		}



		// Return boolean geeft aan of associatie wel of niet geaccept is.
		// This method only expects one response.
		public bool SendAssociation(DicomMessageCollection dicomMessages, params PresentationContext[] presentationContexts)
		{
			bool isAssociationAccepted = true;

			// Send the associate request.
			SendAssociateRq(presentationContexts);

			// Receive the associate repsonse (may be an accept or reject).
			DulMessage associateRp = ReceiveAssociateRp();

			// If an associate accept was received, send the collection of DicomMessages, receive a response and
			// release the association.
			if (associateRp is AssociateAc)
			{

				foreach(DicomMessage dicomMessage in dicomMessages)
				{
					Send(dicomMessage);
				}

				ReceiveDicomMessage();

				SendReleaseRq();

				ReceiveReleaseRp();
			}

			return(isAssociationAccepted);
		}

		public AssociateAc ReceiveAssociateAc()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			AssociateAc receivedAssociateAc = null;

			Message receivedMessage = ReceiveMessage();

			if (receivedMessage.IsAssociateAc)
			{
				receivedAssociateAc = receivedMessage.AssociateAc;
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Script expects AssociateAc. " + receivedMessage.ToString() + " however received.");
			}

			Validate(receivedAssociateAc);

			// End Interface logging.
			// InterfaceLogging.End(receivedAssociateAc);

			return receivedAssociateAc;
		}

		public AssociateRj ReceiveAssociateRj()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			AssociateRj receivedAssociateRj = null;

			Message receivedMessage = ReceiveMessage();

			if (receivedMessage.IsAssociateRj)
			{
				receivedAssociateRj = receivedMessage.AssociateRj;
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Script expects AssociateRj. " + receivedMessage.ToString() + " however received.");
			}

			Validate(receivedAssociateRj);

			// End Interface logging.
			// InterfaceLogging.End(receivedAssociateRj);

			return receivedAssociateRj;
		}


		public DulMessage ReceiveAssociateRp()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			DulMessage receivedDulMessage = null;

			Message receivedMessage = ReceiveMessage();

			if (receivedMessage.IsAssociateRj || receivedMessage.IsAssociateAc)
			{
				receivedDulMessage = receivedMessage as DulMessage;
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Script expects AssociateAc or AssociateRj. " + receivedMessage.ToString() + " however received.");
			}

			Validate(receivedDulMessage);

			// End Interface logging.
			// InterfaceLogging.End(receivedAssociateRj);

			return receivedDulMessage;
		}

		/// <summary>
		/// Send a A-RELEASE-RQ.
		/// </summary>
		/// <returns>The A-RELEASE-RQ.</returns>
		public ReleaseRq SendReleaseRq()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			ReleaseRq releaseRq = new ReleaseRq(new DvtkData.Dul.A_RELEASE_RQ());

			Dvtk.Sessions.SendReturnCode sendReturnCode = DvtkScriptSession.Send(releaseRq.DvtkDataDulMessage);

			if (sendReturnCode != Dvtk.Sessions.SendReturnCode.Success)
			{
				DvtkHighLevelInterfaceException.Throw("Error while trying to send a A_RELEASE_RQ");
			}
			else
			{
				releaseRq.IsSend = true;
				ThreadManager.DataWarehouse.AddMessage(this, releaseRq);

				if (AssociationReleasedEvent != null)
				{
					AssociationReleasedEvent(this);
				}
			}

			// End Interface logging.
			// InterfaceLogging.End(releaseRq);

			return(releaseRq);
		}

		public ReleaseRp ReceiveReleaseRp()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			ReleaseRp receivedReleaseRp = null;

			Message receivedMessage = ReceiveMessage();

			if (receivedMessage.IsReleaseRp)
			{
				receivedReleaseRp = receivedMessage.ReleaseRp;
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Script expects ReleaseRp. " + receivedMessage.ToString() + " however received.");
			}

			Validate(receivedReleaseRp);

			// End Interface logging.
			// InterfaceLogging.End(receivedReleaseRp);

			return receivedReleaseRp;
		}

		/// <summary>
		/// Receive a message (can be a Dicom or Dul message).
		/// 
		/// An exception is thrown when receiving of a message fails.
		/// </summary>
		/// <returns>The received message.</returns>
		public Message ReceiveMessage()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			DicomProtocolMessage receivedMessage = null;

			DvtkData.Message dvtkDataMessage = null;

			WriteInformation("Receive message...");

			Dvtk.Sessions.ReceiveReturnCode receiveReturnCode = DvtkScriptSession.Receive(out dvtkDataMessage);

			if (receiveReturnCode != Dvtk.Sessions.ReceiveReturnCode.Success)
			{
				DvtkHighLevelInterfaceException.Throw("Error while trying to receive a Message. Error code " + receiveReturnCode.ToString() + ".");
			}
			else
			{
				if (dvtkDataMessage is DvtkData.Dimse.DicomMessage)
				{
					receivedMessage = new DicomMessage(dvtkDataMessage as DvtkData.Dimse.DicomMessage);
				}
				else if (dvtkDataMessage is DvtkData.Dul.A_ASSOCIATE_RQ)
				{
					lastAssociateRq = new AssociateRq(dvtkDataMessage as DvtkData.Dul.A_ASSOCIATE_RQ);
					receivedMessage = lastAssociateRq;
				}
				else if (dvtkDataMessage is DvtkData.Dul.A_ASSOCIATE_AC)
				{
					receivedMessage = new AssociateAc(dvtkDataMessage as DvtkData.Dul.A_ASSOCIATE_AC);
				}
				else if (dvtkDataMessage is DvtkData.Dul.A_ASSOCIATE_RJ)
				{
					receivedMessage = new AssociateRj(dvtkDataMessage as DvtkData.Dul.A_ASSOCIATE_RJ);
				}
				else if (dvtkDataMessage is DvtkData.Dul.A_RELEASE_RQ)
				{
					receivedMessage = new ReleaseRq(dvtkDataMessage as DvtkData.Dul.A_RELEASE_RQ);
				}
				else if (dvtkDataMessage is DvtkData.Dul.A_RELEASE_RP)
				{
					receivedMessage = new ReleaseRp(dvtkDataMessage as DvtkData.Dul.A_RELEASE_RP);
				}
				else if (dvtkDataMessage is DvtkData.Dul.A_ABORT)
				{
					receivedMessage = new Abort(dvtkDataMessage as DvtkData.Dul.A_ABORT);
				}
				else
				{
					Debug.Assert(true, "Unexpected DvtkData Message descendant type.");
				}

				// If the options AutoValidate is true, try to validate as much 
				// as possible for the received message.
				if (Options.AutoValidate)
				{
					Validate(receivedMessage);
				}

				receivedMessage.IsReceived = true;
				ThreadManager.DataWarehouse.AddMessage(this, receivedMessage);

				if (receivedMessage is ReleaseRq)
				{
					if (AssociationReleasedEvent != null)
					{
						AssociationReleasedEvent(this);
					}
				}
			}

			// End Interface logging.
			// InterfaceLogging.End(receivedMessage);

			return(receivedMessage);
		}

		/// <summary>
		/// Receive a Dicom Message.
		/// </summary>
		/// <returns>The received Dicom Message.</returns>
		public DicomMessage ReceiveDicomMessage()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			DicomMessage receivedDicomMessage = null;

			Message receivedMessage = ReceiveMessage();

			if (receivedMessage.IsDicomMessage)
			{
				receivedDicomMessage = receivedMessage.DicomMessage;
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Script expects DicomMessage. " + receivedMessage.ToString() + " however received.");
			}

			// End Interface logging.
			// InterfaceLogging.End(receivedDicomMessage);

			return receivedDicomMessage;
		}

		/// <summary>
		/// Receive a Dicom Message and validate it against a definition file.
		/// </summary>
		/// <param name="iodId">The IOD ID to use, implicitly determining which definition file to use.</param>
		/// <returns>The received Dicom Message.</returns>
		public DicomMessage ReceiveDicomMessage(String iodId)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			DicomMessage receivedDicomMessage = ReceiveDicomMessage();

			Validate(receivedDicomMessage, iodId);

			// End Interface logging.
			// InterfaceLogging.End(receivedDicomMessage);

			return receivedDicomMessage;
		}
		
		AssociateRq lastAssociateRq = null;

		public AssociateRq ReceiveAssociateRq()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			AssociateRq receivedAssociateRq = null;

			Message receivedMessage = ReceiveMessage();

			if (receivedMessage.IsAssociateRq)
			{
				receivedAssociateRq = receivedMessage.AssociateRq;
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Script expects AssociateRq. " + receivedMessage.ToString() + " however received.");
			}
	
			Validate(receivedAssociateRq);

			// End Interface logging.
			// InterfaceLogging.End(receivedAssociateRq);

			return receivedAssociateRq;
		}

		public ReleaseRq ReceiveReleaseRq()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			ReleaseRq receivedReleaseRq = null;

			Message receivedMessage = ReceiveMessage();

			if (receivedMessage.IsReleaseRq)
			{
				receivedReleaseRq = receivedMessage.ReleaseRq;
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Script expects ReleaseRq. " + receivedMessage.ToString() + " however received.");
			}

			Validate(receivedReleaseRq);

			// End Interface logging.
			// InterfaceLogging.End(receivedReleaseRq);

			return receivedReleaseRq;
		}

		/// <summary>
		/// Send a A-RELEASE-RSP.
		/// </summary>
		/// <returns>The A-RELEASE-RSP.</returns>
		public ReleaseRp SendReleaseRp()
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this);

			ReleaseRp releaseRp = new ReleaseRp(new DvtkData.Dul.A_RELEASE_RP());

			Dvtk.Sessions.SendReturnCode sendReturnCode = DvtkScriptSession.Send(releaseRp.DvtkDataDulMessage);

			if (sendReturnCode != Dvtk.Sessions.SendReturnCode.Success)
			{
				DvtkHighLevelInterfaceException.Throw("Error while trying to send a A_RELEASE_RP");
			}
			else
			{
				releaseRp.IsSend = true;
				ThreadManager.DataWarehouse.AddMessage(this, releaseRp);
			}

			// End Interface logging.
			// InterfaceLogging.End(releaseRp);

			return(releaseRp);
		}

		private void SetAcceptedPresentationContexts(AssociateRq associateRq, AssociateAc associateAc, params Object[] parameters)
		{
			TransferSyntaxes transferSyntaxes = null;
			SopClasses sopClasses = null;

			if (parameters.Length == 1)
			{
				transferSyntaxes = parameters[0] as TransferSyntaxes;

				if (transferSyntaxes == null)
				{
					DvtkHighLevelInterfaceException.Throw("Error while interpreting parameters.");
				}
			}
			else if (parameters.Length == 2)
			{
				transferSyntaxes = parameters[0] as TransferSyntaxes;

				if (transferSyntaxes == null)
				{
					sopClasses = parameters[0] as SopClasses;

					if (sopClasses == null)
					{
						DvtkHighLevelInterfaceException.Throw("Error while interpreting parameters.");
					}
					else
					{
						transferSyntaxes = parameters[1] as TransferSyntaxes;

						if (transferSyntaxes == null)
						{
							DvtkHighLevelInterfaceException.Throw("Error while interpreting parameters.");
						}
					}
				}
				else
				{
					sopClasses = parameters[1] as SopClasses;

					if (sopClasses == null)
					{
						DvtkHighLevelInterfaceException.Throw("Error while interpreting parameters.");
					}
				}
			}
			else
			{
				DvtkHighLevelInterfaceException.Throw("Error while interpreting parameters.");
			}

			foreach(DvtkData.Dul.RequestedPresentationContext dvtkDataRequestedPresentationContext in associateRq.DvtkDataAssociateRq.PresentationContexts)
			{
				String sopClassToAccept = null;
				String transferSyntaxToAccept = null;

				if (sopClasses == null)
				{
					sopClassToAccept = dvtkDataRequestedPresentationContext.AbstractSyntax.UID;
				}
				else
				{
					foreach(String acceptedSopClass in sopClasses.list)
					{
						if (acceptedSopClass == dvtkDataRequestedPresentationContext.AbstractSyntax.UID)
						{
							sopClassToAccept = dvtkDataRequestedPresentationContext.AbstractSyntax.UID;
							break;
						}
					}
				}

				if (sopClassToAccept != null)
				{
					foreach(String acceptedTransferSyntax in transferSyntaxes.list)
					{
						foreach(DvtkData.Dul.TransferSyntax requestedDvtkDataTransferSyntax in dvtkDataRequestedPresentationContext.TransferSyntaxes)
						{
							if (acceptedTransferSyntax == requestedDvtkDataTransferSyntax.UID)
							{
								transferSyntaxToAccept = requestedDvtkDataTransferSyntax.UID;
								break;
							}
						}

						if (transferSyntaxToAccept != null)
						{
							break;
						}
					}

					if (transferSyntaxToAccept != null)
					{
						DvtkData.Dul.AcceptedPresentationContext dvtkDataAcceptedPresentationContext = new DvtkData.Dul.AcceptedPresentationContext();

						dvtkDataAcceptedPresentationContext.AbstractSyntax = new DvtkData.Dul.AbstractSyntax(sopClassToAccept);
						dvtkDataAcceptedPresentationContext.TransferSyntax = new DvtkData.Dul.TransferSyntax(transferSyntaxToAccept);
						dvtkDataAcceptedPresentationContext.Result = 0;

						associateAc.DvtkDataAssociateAc.AddPresentationContexts(dvtkDataAcceptedPresentationContext);
					}
				}
			}
		}

		// TODO!!!: split up in three different methods and add code comments.
		public AssociateAc SendAssociateAc(params Object[] parameters)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, parameters);

			AssociateAc associateAc = new AssociateAc(new DvtkData.Dul.A_ASSOCIATE_AC());

			associateAc.DvtkDataAssociateAc.CallingAETitle = DvtkScriptSession.SutSystemSettings.AeTitle;
			associateAc.DvtkDataAssociateAc.CalledAETitle = DvtkScriptSession.DvtSystemSettings.AeTitle;
			associateAc.DvtkDataAssociateAc.UserInformation.MaximumLength.MaximumLengthReceived = DvtkScriptSession.DvtSystemSettings.MaximumLengthReceived;
			associateAc.DvtkDataAssociateAc.UserInformation.ImplementationClassUid.UID = DvtkScriptSession.DvtSystemSettings.ImplementationClassUid;

			if (!(parameters[0] is PresentationContext))
			{
				if (this.lastAssociateRq == null)
				{
					DvtkHighLevelInterfaceException.Throw("No associate request received.");
				}
				else
				{
					SetAcceptedPresentationContexts(this.lastAssociateRq, associateAc, parameters);
				}
			}
			else
			{
				// Parse all parameters of this method.
				foreach(Object parameter in parameters)
				{
					PresentationContext presentationContext = parameter as PresentationContext;

					// If parameter is of type PresentationContext...
					if (presentationContext != null)
					{
						// If the presentation context can be interpreted as a PC for an A_ASSOCIATE_AC...
						if (presentationContext.InterpretAsPcForAssociateAc())
						{
							DvtkData.Dul.AcceptedPresentationContext acceptedPresentationContext = new DvtkData.Dul.AcceptedPresentationContext();

							acceptedPresentationContext.AbstractSyntax = new DvtkData.Dul.AbstractSyntax(presentationContext.SopClass);
							acceptedPresentationContext.TransferSyntax = new DvtkData.Dul.TransferSyntax(presentationContext.TransferSyntaxes[0] as String);
							acceptedPresentationContext.Result = presentationContext.Result;

							associateAc.DvtkDataAssociateAc.AddPresentationContexts(acceptedPresentationContext);
						}
						else
						{
							DvtkHighLevelInterfaceException.Throw("Error while interpreting parameters for PresentationContext");						
						}
					}
					else
					{
						DvtkHighLevelInterfaceException.Throw("Not all arguments could be used");
					}
				}
			}

			Dvtk.Sessions.SendReturnCode sendReturnCode = DvtkScriptSession.Send(associateAc.DvtkDataAssociateAc);

			if (sendReturnCode != Dvtk.Sessions.SendReturnCode.Success)
			{
				DvtkHighLevelInterfaceException.Throw("Error sending associate accept (" + sendReturnCode.ToString() + ")"); 
			}
			else
			{
				associateAc.IsSend = true;
				ThreadManager.DataWarehouse.AddMessage(this, associateAc);
			}

			// End Interface logging.
			// InterfaceLogging.End(associateAc);

			return(associateAc);
		}



		//
		// - Methods -
		//


		//
		// - Other -
		//


		/*
		/// <summary>
		/// Show the detailed results, only if this ScriptSession has started and ended the results gathering.
		/// </summary>
		internal void ShowDetailResults()
		{
			if (this.startAndEndResultsGathering)
			{
				ShowResults("Detail_" + this.resultsFileName);
			}
		}

		/// <summary>
		/// Show the summary results, only if this ScriptSession has started and ended the results gathering.
		/// </summary>
		internal void ShowSummaryResults()
		{
			ShowResults("Summary_" + this.resultsFileName);
		}
*/
		/// <summary>
		/// Convert the .xml file to .html and show it.
		/// </summary>
		/// <param name="xmlFileName">The results file name (.xml file).</param>
		private void ShowResults()
		{
			String xmlFileName = "Detail_" + Options.ResultsFileName;

			String xmlFullFileName = System.IO.Path.Combine(DvtkScriptSession.ResultsRootDirectory, xmlFileName);

			if (File.Exists(xmlFullFileName))
			{
				String htmlFullFileName = xmlFullFileName.Replace(".xml", ".html");
				String resultsStyleSheetFullFileName = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "DVT_RESULTS.xslt");

				// Convert the xml file to html.
				XslTransform xslTransform = new XslTransform();
				xslTransform.Load(resultsStyleSheetFullFileName);
				XPathDocument xPathDocument = new XPathDocument(xmlFullFileName);

				FileStream fileStream = new FileStream(htmlFullFileName, FileMode.Create, FileAccess.ReadWrite);
				xslTransform.Transform(xPathDocument, null, fileStream, null);

				fileStream.Close();

				// Show the html file.
				System.Diagnostics.Process process  = new System.Diagnostics.Process();
				process.StartInfo.FileName = htmlFullFileName;
				process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized; 
				process.Start();
			}
		}


		public void Validate(DicomProtocolMessage dicomProtocolMessage)
		{
			this.validator.Validate(this, dicomProtocolMessage);
		}


		/// <summary>
		/// Validate the Dul Message by inspecting the VR's and the definition.
		/// </summary>
		/// <param name="dulMessage">The Dul Message.</param>
		public void Validate(DulMessage dulMessage)
		{
			this.validator.Validate(this, dulMessage);
		}

		/// <summary>
		/// Validate the Dicom Message by inspecting the VR's.
		/// </summary>
		/// <param name="dicomMessage">The Dicom Message.</param>
		public void Validate(DicomMessage dicomMessage)
		{
			this.validator.Validate(this, dicomMessage);
		}

		/// <summary>
		/// Validate the Dicom Message using a definition file.
		/// </summary>
		/// <param name="dicomMessage">The Dicom Message.</param>
		/// <param name="iodId">The IOD ID to use, implicitly determining which definition file to use.</param>
		public void Validate(DicomMessage dicomMessage, String iodName)
		{
			this.validator.Validate(this, dicomMessage, iodName);
		}

		/// <summary>
		/// Validate the Dicom Message using a reference Dicom message.
		/// </summary>
		/// <param name="dicomMessage1">The Dicom message.</param>
		/// <param name="dicomMessage2">The reference Dicom message.</param>
		public void Validate(DicomMessage dicomMessage1, DicomMessage dicomMessage2, String iodName)
		{
			this.validator.Validate(this, dicomMessage1, dicomMessage2, iodName);
		}

		public void Validate(DicomMessage dicomMessage1, DicomMessage dicomMessage2)
		{
			this.validator.Validate(this, dicomMessage1, dicomMessage2);
		}

		private ArrayList outboundMessageFilters = new ArrayList();

		public void AddToBack(OutboundMessageFilter outboundMessageFilter)
		{
			this.outboundMessageFilters.Add(outboundMessageFilter);
		}

		public void AddToFront(OutboundMessageFilter outboundMessageFilter)
		{
			this.outboundMessageFilters.Insert(0, outboundMessageFilter);
		}

		public delegate void AssociationReleasedEventHandler(DicomThread dicomThread);

		public event AssociationReleasedEventHandler AssociationReleasedEvent;
	}
}
