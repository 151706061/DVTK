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
using Dvtk.DvtkDicomEmulators.Bases;
using Dvtk.IheActors.Bases;
using Dvtk.IheActors.Actors;
using Dvtk.IheActors.Dicom;
using Dvtk.IheActors.Hl7;
using Dvtk.IheActors.IheFramework;

namespace DvtIheAcquisitionModalityWrapper
{
	/// <summary>
	/// Summary description for AcquisitionModalityWrapper.
	/// </summary>
	public class AcquisitionModalityWrapper
	{
		private System.String _ownId = null;
        private IheFramework _iheFramework = null;
		private System.String _configFilename = System.String.Empty;
		private AcquisitionModalityActor _acquisitionModality = null;

		/// <summary>
		/// Class constructor
		/// </summary>
		public AcquisitionModalityWrapper(System.String configFilename, System.String acquisitionModalityId)
		{
			//
			// Set up the IHE Framework
			//
            _iheFramework = new IheFramework("Acquisition Modality");
			_configFilename = configFilename;
			_ownId = acquisitionModalityId;
		}

		/// <summary>
		/// Initialize the acquisition modality actor
		/// </summary>
		public void Initialize()
		{
			//
            // Configure the ihe framework
			//
            _iheFramework.Config.Load(_configFilename);

			//
			// Apply the Configuration to actors
			//
            _iheFramework.ApplyConfig();

			//
			// Get the Acquisition Modality actor
            _acquisitionModality = (AcquisitionModalityActor)_iheFramework.GetActor(new ActorName(ActorTypeEnum.AcquisitionModality, _ownId));
			if (_acquisitionModality == null)
			{
				throw new System.SystemException("Acquisition Modality not configured");
			}

		    // Set up worklist item - storage directory mapping
		    _acquisitionModality.MapWorklistItemToStorageDirectory.MapTag = Tag.SCHEDULED_PROCEDURE_STEP_DESCRIPTION;
		    _acquisitionModality.MapWorklistItemToStorageDirectory.AddMapping("Default", "C:\\Program Files\\DVT20\\data\\storageData");

			//
			// Open the results after loading the configuration so that the results directory is defined
			//
            _iheFramework.OpenResults();

			//
            // Start the ihe framework test
			//
            _iheFramework.StartTest();
		}

		/// <summary>
		/// Terminate the Acquisition Modality actor
		/// </summary>
		public void Terminate()
		{
			//
            // Stop the ihe framework test
			//
            _iheFramework.StopTest();

			//
			// Evaluate the results
			//
            _iheFramework.EvaluateTest();
            _iheFramework.CleanUpCurrentWorkingDirectory();

			//
			// Close the results and get the final results filename
			//
            System.String resultsFilename = _iheFramework.CloseResults();
        }

        #region Events - Message Handling
        /// <summary>
        /// Subscribe to the Message Available Event.
        /// </summary>
        public void SubscribeMessageEvent()
        {
            _acquisitionModality.OnMessageAvailable += new AcquisitionModalityActor.MessageAvailableHandler(MessageIsAvailable);
        }

        /// <summary>
        /// Unsubscribe to the Message Available Event.
        /// </summary>
        public void UnsubscribeMessageEvent()
        {
            _acquisitionModality.OnMessageAvailable -= new AcquisitionModalityActor.MessageAvailableHandler(MessageIsAvailable);
        }

        /// <summary>
        /// Message is available.
        /// </summary>
        /// <param name="server">Event source.</param>
        /// <param name="messageAvailableEvent">Message Available Event Details.</param>
        private void MessageIsAvailable(object server, MessageAvailableEventArgs messageAvailableEvent)
        {
            // Publish the event to any interested parties.
            PublishMessageAvailableEvent(messageAvailableEvent);
        }

        /// <summary>
        /// Delegate the Message Available Event to a Handler.
        /// Available messages are either messages about to be sent from this server to the destination server or
        /// messages received by this server from the source server.
        /// </summary>
        public delegate void MessageAvailableHandler(object server, MessageAvailableEventArgs messageAvailableEvent);

        /// <summary>
        /// Message Available Event
        /// </summary>
        public event MessageAvailableHandler OnMessageAvailable;

        /// <summary>
        /// Publish the Message Available Event.
        /// </summary>
        /// <param name="messageAvailable">Available message.</param>
        private void PublishMessageAvailableEvent(MessageAvailableEventArgs messageAvailableEvent)
        {
            if (OnMessageAvailable != null)
            {
                OnMessageAvailable(this, messageAvailableEvent);
            }
        }
        #endregion Events - Message Handling

        #region Events - Transaction Handling
        /// <summary>
        /// Subscribe to the Transction Available Event.
        /// </summary>
        public void SubscribeTransactionEvent()
        {
            _acquisitionModality.OnTransactionAvailable += new AcquisitionModalityActor.TransactionAvailableHandler(TransactionIsAvailable);
        }

        /// <summary>
        /// Unsubscribe to the Transction Available Event.
        /// </summary>
        public void UnsubscribeTransactionEvent()
        {
            _acquisitionModality.OnTransactionAvailable -= new AcquisitionModalityActor.TransactionAvailableHandler(TransactionIsAvailable);
        }

        /// <summary>
        /// Transaction is available.
        /// </summary>
        /// <param name="server">Event source.</param>
        /// <param name="transactionAvailableEvent">Transaction Available Event Details.</param>
        private void TransactionIsAvailable(object server, TransactionAvailableEventArgs transactionAvailableEvent)
        {
            // Publish the event to any interested parties.
            PublishTransactionAvailableEvent(transactionAvailableEvent);
        }

        /// <summary>
        /// Delegate the Transaction Available Event to a Handler.
        /// </summary>
        public delegate void TransactionAvailableHandler(object server, TransactionAvailableEventArgs transactionAvailableEvent);

        /// <summary>
        /// Transaction Available Event
        /// </summary>
        public event TransactionAvailableHandler OnTransactionAvailable;

        /// <summary>
        /// Publish the Transaction Available Event.
        /// </summary>
        /// <param name="transactionAvailable">Available transaction.</param>
        private void PublishTransactionAvailableEvent(TransactionAvailableEventArgs transactionAvailableEvent)
        {
            if (OnTransactionAvailable != null)
            {
                OnTransactionAvailable(this, transactionAvailableEvent);
            }
        }
        #endregion Events - Transaction Handling

        #region DULP methods
        /// <summary>
		/// Set the Associate Reject Parameters for the given DICOM Server within the Acquisition Modality.
		/// </summary>
		/// <param name="fromActorName">Actor Name that makes a connection to this DICOM Server.</param>
		/// <param name="result">Reject Result.</param>
		/// <param name="source">Reject Source.</param>
		/// <param name="reason">Reject Reason.</param>
		/// <remarks>The fromActorName type is always the ImageManager at the moment - no other actors connect to the Acquisition
		/// Modality at the moment. The caller does need to supply fromActorName as the Id component is not known by default.</remarks>
		public void SetAssociationRejectParametersForDicomServer(ActorName fromActorName, byte result, byte source, byte reason)
		{
			//
			// Get the DICOM server within the acquisition modality that handles the DICOM association from the actor name.
			//
			DicomServer dicomServer = _acquisitionModality.GetDicomServer(fromActorName);
			if (dicomServer != null)
			{
				//
				// Set the reject parameters
				//
				dicomServer.SetRejectParameters(result, source, reason);
			}
		}

		/// <summary>
		/// Set the Associate Abort Parameters for the given DICOM Server within the Acquisition Modality.
		/// </summary>
		/// <param name="fromActorName">Actor Name that makes a connection to this DICOM Server.</param>
		/// <param name="source">Abort Source.</param>
		/// <param name="reason">Abort Reason.</param>
		/// <remarks>The fromActorName type is always the ImageManager at the moment - no other actors connect to the Acquisition
		/// Modality at the moment. The caller does need to supply fromActorName as the Id component is not known by default.</remarks>
		public void SetAssociationAbortParametersForDicomServer(ActorName fromActorName, byte source, byte reason)
		{
			//
			// Get the DICOM server within the acquisition modality that handles the DICOM association from the actor name.
			//
			DicomServer dicomServer = _acquisitionModality.GetDicomServer(fromActorName);
			if (dicomServer != null)
			{
				//
				// Set the abort parameters
				//
				dicomServer.SetAbortParameters(source, reason);
			}
		}

		/// <summary>
		/// Set the way in which the Acquisition Modality should respond to an Associate Request from the actor name.
		/// </summary>
		/// <param name="fromActorName">Actor Name that makes a connection to this DICOM Server.</param>
		/// <param name="respondEnum">Enum defining how to respond.</param>
		/// <remarks>The fromActorName type is always the ImageManager at the moment - no other actors connect to the Acquisition
		/// Modality at the moment. The caller does need to supply fromActorName as the Id component is not known by default.</remarks>
		public void SetRespondToAssociateRequestForDicomServer(ActorName fromActorName, HliScp.ScpRespondToAssociateRequestEnum respondEnum)
		{
			//
			// Get the DICOM server within the acquisition modality that handles the DICOM association from the actor name.
			//
			DicomServer dicomServer = _acquisitionModality.GetDicomServer(fromActorName);
			if (dicomServer != null)
			{
				//
				// Set how the DICOM Server should respond to the Associate Request from the actor name.
				//
				dicomServer.SetRespondToAssociateRequest(respondEnum);
			}
		}

		/// <summary>
		/// Clear the Transfer Syntaxes supported by the DICOM server handling the connection from the actor name.
		/// </summary>
		/// <param name="fromActorName">Actor Name that makes a connection to this DICOM Server.</param>
		/// <remarks>The fromActorName type is always the ImageManager at the moment - no other actors connect to the Acquisition
		/// Modality at the moment. The caller does need to supply fromActorName as the Id component is not known by default.
		/// 
		/// The caller is responsible for setting at least one new Transfer Syntax UID via the AddTransferSyntaxSupportForDicomServer()
		/// method after making this method call.</remarks>
		public void ClearTransferSyntaxSupportForDicomServer(ActorName fromActorName)
		{
			//
			// Get the DICOM server within the acquisition modality that handles the DICOM association from the actor name.
			//
			DicomServer dicomServer = _acquisitionModality.GetDicomServer(fromActorName);
			if (dicomServer != null)
			{
				//
				// Clear the transfer syntax support for the DICOM Server.
				//
				dicomServer.ClearTransferSyntaxes();
			}
		}

		/// <summary>
		/// Add a Transfer Syntax Uid that should be supported by the DICOM server handling the connection from the actor name.
		/// </summary>
		/// <param name="fromActorName">Actor Name that makes a connection to this DICOM Server.</param>
		/// <param name="transferSyntaxUid">Transfer Syntax UID - as string.</param>
		/// <remarks>The fromActorName type is always the ImageManager at the moment - no other actors connect to the Acquisition
		/// Modality at the moment. The caller does need to supply fromActorName as the Id component is not known by default.</remarks>
		public void AddTransferSyntaxSupportForDicomServer(ActorName fromActorName, System.String transferSyntaxUid)
		{
			//
			// Get the DICOM server within the acquisition modality that handles the DICOM association from the actor name.
			//
			DicomServer dicomServer = _acquisitionModality.GetDicomServer(fromActorName);
			if (dicomServer != null)
			{
				//
				// Add a transfer syntax that the DICOM Server should support.
				//
				dicomServer.AddTransferSyntax(transferSyntaxUid);
			}
		}

		/// <summary>
		/// Clear the Transfer Syntaxes proposed by the DICOM client making the connection to the actor name.
		/// </summary>
		/// <param name="toActorName">Actor Name that receives a connection to from DICOM Client.</param>
		/// <remarks>The caller is responsible for setting at least one new Transfer Syntax UID via the AddTransferSyntaxProposalForDicomClient()
		/// method after making this method call.</remarks>
		public void ClearTransferSyntaxProposalForDicomClient(ActorName toActorName)
		{
			//
			// Get the DICOM client within the acquisition modality that handles the DICOM association to the actor name.
			//
			DicomClient dicomClient = _acquisitionModality.GetDicomClient(toActorName);
			if (dicomClient != null)
			{
				//
				// Clear the transfer syntax proposed by the DICOM Client.
				//
				dicomClient.ClearTransferSyntaxes();
			}
		}

		/// <summary>
		/// Add a Transfer Syntax Uid that should be proposed by the local DICOM client handling the connection to the actor name.
		/// </summary>
		/// <param name="toActorName">Actor Name that receives a connection from this DICOM Client.</param>
		/// <param name="transferSyntaxUid">Transfer Syntax UID - as string.</param>
		public void AddTransferSyntaxProposalForDicomClient(ActorName toActorName, System.String transferSyntaxUid)
		{
			//
			// Get the DICOM client within the acquisition modality that handles the DICOM association to the actor name.
			//
			DicomClient dicomClient = _acquisitionModality.GetDicomClient(toActorName);
			if (dicomClient != null)
			{
				//
				// Add a transfer syntax that the DICOM Client should propose.
				//
				dicomClient.AddTransferSyntax(transferSyntaxUid);
			}
		}

		#endregion DULP methods

		#region DICOM methhods
		/// <summary>
		/// Send a C-ECHO-RQ Verification SOP Class to the given actor type
		/// </summary>
        /// <returns>Boolean indicating success or failure.</returns>
        public bool SendVerification(ActorTypeEnum actorType)
		{
			// send the Verification SOP Class
			return _acquisitionModality.TriggerActorDicomClientVerificationInstances(actorType);
		}

		/// <summary>
		/// Get a worklist - called from GUI WORKLIST button
		/// </summary>
        /// <returns>Boolean indicating success or failure.</returns>
        public bool GetWorklist(System.String modalityWorklistDcmFilename, System.String dateRange)
		{
			// send the worklist query
            return _acquisitionModality.SendQueryModalityWorklist(modalityWorklistDcmFilename, dateRange);

			// iterate over _acquisitionModality.ModalityWorklistItems to display worklist items in GUI
		}

		/// <summary>
		/// Send MPPS IN-PROGRESS - called from GUI IN-PROGRESS button
		/// </summary>
        /// <returns>Boolean indicating success or failure.</returns>
        public bool SendMppsInProgress(DicomQueryItem modalityWorklistItem, System.String mppsInProgressDcmFilename)
		{
			// send the mpps in-progress
            return _acquisitionModality.SendNCreateModalityProcedureStepInProgress(mppsInProgressDcmFilename, modalityWorklistItem);
		}

		/// <summary>
		/// Send MPPS COMPLETED - called from GUI COMPLETED button
		/// </summary>
        /// <returns>Boolean indicating success or failure.</returns>
        public bool SendMppsCompleted(System.String mppsCompletedDcmFilename)
		{
			// send the mpps completed
            return _acquisitionModality.SendNSetModalityProcedureStepCompleted(mppsCompletedDcmFilename);
		}

		/// <summary>
		/// Send MPPS DISCONTINUED - called from GUI DISCONTINUED button
		/// </summary>
        /// <returns>Boolean indicating success or failure.</returns>
        public bool SendMppsDiscontinued(System.String mppsDiscontinuedDcmFilename)
		{
			// Send the mpps discontinued
            return _acquisitionModality.SendNSetModalityProcedureStepDiscontinued(mppsDiscontinuedDcmFilename);
		}

		/// <summary>
		/// Send Images - called from GUI IMAGES button
		/// </summary>
        /// <returns>Boolean indicating success or failure.</returns>
        public bool SendImages(DicomQueryItem modalityWorklistItem, bool withSingleAssociation)
		{
			// Send the images - read images based on worklist map to storage directory
            return _acquisitionModality.SendModalityImagesStored(modalityWorklistItem, withSingleAssociation);
		}

		/// <summary>
		/// Send Storage Commit - called from GUI COMMIT button
		/// </summary>
        /// <returns>Boolean indicating success or failure.</returns>
        public bool SendStorageCommitment()
		{
			// Send the storage commitment
            return _acquisitionModality.SendStorageCommitment(false, 0);
		}
		#endregion DICOM methhods
	}
}
