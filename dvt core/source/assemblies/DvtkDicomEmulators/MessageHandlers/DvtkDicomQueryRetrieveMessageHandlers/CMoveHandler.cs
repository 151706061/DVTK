// Part of DvtkDicomQueryRetrieveMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using DvtkHighLevelInterface.InformationEntity;

namespace Dvtk.DvtkDicomEmulators.QueryRetrieveMessageHandlers
{
	/// <summary>
	/// Summary description for CMoveHandler.
	/// </summary>
	public class CMoveHandler : MessageHandler
	{
		private QueryRetrieveInformationModels _informationModels = null;

		public CMoveHandler() {}

		public CMoveHandler(QueryRetrieveInformationModels informationModels) 
		{
			_informationModels = informationModels;
		}

		public override bool HandleCMoveRequest(DicomMessage retrieveMessage)
		{
			if (_informationModels == null) return false;

			// Refresh the Information Models
			_informationModels.Refresh();

			// Validate the received message
			System.String iodName = DicomThread.GetIodNameFromDefinition(retrieveMessage);
			DicomThread.Validate(retrieveMessage, iodName);

			// try to get the SOP Class Uid so that we know which Information Model to use.
			DvtkHighLevelInterface.Values values = retrieveMessage.CommandSet.GetAttributeValues("0x00000002");
			System.String sopClassUid = values.GetString(1);
			DvtkData.Dul.AbstractSyntax abstractSyntax = new DvtkData.Dul.AbstractSyntax(sopClassUid);

			// try to get the Move Destination AE.
			values = retrieveMessage.CommandSet.GetAttributeValues("0x00000600");
			System.String moveDestinationAE = values.GetString(1);

			DvtkData.Collections.StringCollection retrieveList = null;

			// check if we should use the Patient Root Information Model
			if ((abstractSyntax.UID == DvtkData.Dul.AbstractSyntax.Patient_Root_Query_Retrieve_Information_Model_MOVE.UID) &&
				(_informationModels.PatientRoot != null))
			{
				retrieveList = _informationModels.PatientRoot.RetrieveInformationModel(retrieveMessage);
			}
				// check if we should use the Study Root Information Model 
			else if ((abstractSyntax.UID == DvtkData.Dul.AbstractSyntax.Study_Root_Query_Retrieve_Information_Model_MOVE.UID) &&
				(_informationModels.StudyRoot != null))
			{
				retrieveList = _informationModels.StudyRoot.RetrieveInformationModel(retrieveMessage);
			}
				// check if we should use the Patient Study Only Information Model
			else if ((abstractSyntax.UID == DvtkData.Dul.AbstractSyntax.Patient_Study_Only_Query_Retrieve_Information_Model_MOVE.UID) &&
				(_informationModels.PatientStudyOnly != null))
			{
				retrieveList = _informationModels.PatientStudyOnly.RetrieveInformationModel(retrieveMessage);
			}

			// process the retrieve list
			return ProcessRetrieveList(moveDestinationAE, retrieveList);
		}

		private bool ProcessRetrieveList(System.String moveDestinationAE, DvtkData.Collections.StringCollection retrieveList)
		{
			UInt16 status = 0x0000;
			UInt16 remainingSubOperations = (UInt16)retrieveList.Count;
			UInt16 completeSubOperations = 0;
			UInt16 failedSubOperations = 0;
			UInt16 warningSubOperations = 0;
			int subOperationIndex = 0;

			foreach(System.String dcmFilename in retrieveList)
			{
				status = 0xFF00;
				SendCMoveRsp(status,
					remainingSubOperations,
					completeSubOperations,
					failedSubOperations,
					warningSubOperations);

				if (HandleSubOperation(moveDestinationAE, dcmFilename, subOperationIndex) == true)
				{
					completeSubOperations++;
				}
				else
				{
					failedSubOperations++;
				}
				remainingSubOperations--;
				subOperationIndex++;
			}

			status = 0x0000;
			SendCMoveRsp(status,
				remainingSubOperations,
				completeSubOperations,
				failedSubOperations,
				warningSubOperations);

			// message handled
			return true;
		}

		private void SendCMoveRsp(UInt16 status,
			UInt16 remainingSubOperations,
			UInt16 completeSubOperations,
			UInt16 failedSubOperations,
			UInt16 warningSubOperations)
		{
			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.CMOVERSP);
			responseMessage.Set("0x00000900", DvtkData.Dimse.VR.US, status);
			responseMessage.Set("0x00001020", DvtkData.Dimse.VR.US, remainingSubOperations);
			responseMessage.Set("0x00001021", DvtkData.Dimse.VR.US, completeSubOperations);
			responseMessage.Set("0x00001022", DvtkData.Dimse.VR.US, failedSubOperations);
			responseMessage.Set("0x00001023", DvtkData.Dimse.VR.US, warningSubOperations);
			this.Send(responseMessage);
		}

		private bool HandleSubOperation(System.String moveDestinationAE, System.String dcmFilename, int subOperationIndex)
		{
			SCU storageScu = new SCU(true);

			storageScu.Initialize(DicomThread.ThreadManager);

			storageScu.Options.Identifier = "StorageSubOperationAsScu"; 

			if (DicomThread.Options.StartAndEndResultsGathering == true)
			{
				storageScu.Options.StartAndEndResultsGathering = true;
				storageScu.Options.ResultsFilePerAssociation = false;

				System.String filenameFormat = DicomThread.Options.ResultsFileName.Replace("_res", "_StorageSubOperationAsScu{0}_res");
				System.String resultsFilename = String.Format(filenameFormat, subOperationIndex);
				storageScu.Options.ResultsFileName = resultsFilename;
				storageScu.Options.ResultsDirectory = DicomThread.Options.ResultsDirectory;
			
				System.String message = String.Format("StorageSubOperation filename: {0}", storageScu.Options.ResultsDirectory + "Detail_" + storageScu.Options.ResultsFileName);
				DicomThread.WriteInformation(message);
			}
			else
			{
				storageScu.Options.StartAndEndResultsGathering = false;
			}

			storageScu.Options.DvtAeTitle = DicomThread.Options.DvtAeTitle;
			storageScu.Options.DvtPort = DicomThread.Options.DvtPort;

			storageScu.Options.SutAeTitle = moveDestinationAE;
			storageScu.Options.SutPort = DicomThread.Options.SutPort;
			storageScu.Options.SutIpAddress = DicomThread.Options.SutIpAddress;

			storageScu.Options.DataDirectory = DicomThread.Options.DataDirectory;
			storageScu.Options.StorageMode = Dvtk.Sessions.StorageMode.AsDataSet;

//			foreach (System.String filename in config.DefinitionFiles)
//			{
//				storageScu.Options.LoadDefinitionFile(filename);
//			}

			System.String sopClassUid = "1.2.840.10008.5.1.4.1.1.7";
			PresentationContext presentationContext 
				= new PresentationContext(sopClassUid, // Abstract Syntax Name
										"1.2.840.10008.1.2"); // Transfer Syntax Name(s)
			PresentationContext[] presentationContexts = new PresentationContext[1];
			presentationContexts[0] = presentationContext;
			
			DicomMessage storageMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.CSTORERQ);
			storageMessage.DataSet.Read(dcmFilename, storageScu);

			storageScu.NonThreadedStart();

			bool sendResult = false;
			try 
			{
				sendResult = storageScu.SendAssociation(storageMessage, presentationContexts);
			}
			catch (System.Exception)
			{
				DicomThread.WriteError("Storage Sub-Operation As SCU Failed");
			}
			finally
			{
				storageScu.NonThreadedStop();
			}

			return sendResult;
		}
	}
}
