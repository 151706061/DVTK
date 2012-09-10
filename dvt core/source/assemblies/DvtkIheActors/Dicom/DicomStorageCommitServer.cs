// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using DvtkHighLevelInterface.InformationEntity;
using Dvtk.DvtkDicomEmulators.StorageCommitClientServers;
using Dvtk.DvtkDicomEmulators.StorageCommitMessageHandlers;
using Dvtk.IheActors;

namespace Dvtk.IheActors.Dicom
{
	/// <summary>
	/// Summary description for DicomStorageCommitServer.
	/// </summary>
	public class DicomStorageCommitServer : DicomServer
	{
		private QueryRetrieveInformationModels _informationModels = null;

		public DicomStorageCommitServer(BaseActor parentActor, ActorNameEnum actorName) : base (parentActor, actorName)
		{
			// set up the Query/Retrieve information models
			_informationModels = new QueryRetrieveInformationModels();
		}

		public override void ApplyConfig(DicomConfig config)
		{
			// load the information models
			_informationModels.Load(config.DataDirectory);

			// add any default attribute values to the information models
			_informationModels.AddDefaultAttribute("0x00080005", DvtkData.Dimse.VR.CS, "ISO IR 6");
			_informationModels.AddDefaultAttribute("0x00080090", DvtkData.Dimse.VR.PN, "Referring^Physician^Dr");

			// set up the storage commit SCP
			StorageCommitScp storageCommitScp = new StorageCommitScp();
			Scp = storageCommitScp;

			// apply the remaining configuration
			base.ApplyConfig(config);

			// add the default message handlers
			storageCommitScp.AddDefaultMessageHandlers();
		}

		public override void AssociationReleasedEventHandler(DicomThread storageCommitScp)
		{
			// do SCP specific post association processing here
			// iterate over all the dicomMessages in the data warehouse
			foreach (DicomMessage dicomMessage in storageCommitScp.DataWarehouse.Messages(storageCommitScp).DicomMessages)
			{
				// check for the N-ACTION-RQ
				if (dicomMessage.CommandSet.DimseCommand == DvtkData.Dimse.DimseCommand.NACTIONRQ)
				{
					// produce a DICOM trigger for the Storage Commitment SCU - N-EVENT-REPORT-RQ
					DicomTrigger storageCommitTrigger = GenerateTrigger(dicomMessage);
					ParentActor.TriggerActor(ActorName, storageCommitTrigger);
				}
			}

			// call base implementation to generate event, update transaction log and cleanup data warehouse
			base.AssociationReleasedEventHandler(storageCommitScp);
		}

		private DicomTrigger GenerateTrigger(DicomMessage dicomMessage)
		{
			DicomTrigger storageCommitTrigger = new DicomTrigger(Dvtk.IheActors.TransactionNameEnum.RAD_10);
			storageCommitTrigger.Trigger = TempGenerateTriggers.MakeStorageCommitEvent(_informationModels, dicomMessage);
			return storageCommitTrigger;
		}
	}
}
