// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using Dvtk.IheActors.Dicom;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for ImageManagerActor.
	/// </summary>
	public class ImageManagerActor : BaseActor
	{
		public ImageManagerActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.ImageManager, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Procedure Scheduled [RAD-4]
			// for receiving Procedure Updated [RAD-13]
			AddHl7Server(ActorNameEnum.DssOrderFiller, actorConfig);

			// for receiving Image Availability Query [RAD-11]
			// for receiving Performed Work Status Update [RAD-42]
//			AddDicomServer(ActorNameEnum.DssOrderFiller, actorConfig);

			// for receiving Modality Procedure Step In Progress [RAD-6]
			// for receiving Modality Procedure Step Completed [RAD-7]
			// for receiving Creator Procedure Step In Progress [RAD-20]
			// for receiving Creator Procedure Step Completed [RAD-21]
			AddDicomServer(new DicomMppsServer(this, ActorNameEnum.PerformedProcedureStepManager), actorConfig);

			// for receiving Storage Commitment [RAD-10]
			AddDicomServer(new DicomStorageCommitServer(this, ActorNameEnum.AcquisitionModality), actorConfig);

			// for receiving Storage Commitment [RAD-10]
			AddDicomServer(new DicomStorageCommitServer(this, ActorNameEnum.EvidenceCreator), actorConfig);

			// for receiving Image Availability Query [RAD-11]
			AddDicomServer(new DicomQueryRetrieveServer(this, ActorNameEnum.ReportManager), actorConfig);

			// for sending Performed Work Status Update [RAD-42]
			// for sending Instance Availability Notification [RAD-49]
//			AddDicomClient(ActorNameEnum.DssOrderFiller, actorConfig);

			// for sending Storage Commitment [RAD-10]
			AddDicomClient(new DicomStorageCommitClient(this, ActorNameEnum.AcquisitionModality), actorConfig);

			// for sending Storage Commitment [RAD-10]
			AddDicomClient(new DicomStorageCommitClient(this, ActorNameEnum.EvidenceCreator), actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, Hl7Transaction hl7Transaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.DssOrderFiller:
					// received Procedure Scheduled [RAD-4] or
					// received Procedure Updated [RAD-13]
					break;
				default:
					break;
			}
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.DssOrderFiller:
					// received Image Availability Query [RAD-11] or
					// received Performed Work Status Update [RAD-42]
					break;
				case ActorNameEnum.PerformedProcedureStepManager:
					// received Modality Procedure Step In Progress [RAD-6] or
					// received Modality Procedure Step Completed [RAD-7] or
					// received Creator Procedure Step In Progress [RAD-20] or
					// received Creator Procedure Step Completed [RAD-21]
					break;
				case ActorNameEnum.AcquisitionModality:
					// received Storage Commitment [RAD-10]
					break;
				case ActorNameEnum.EvidenceCreator:
					// received Storage Commitment [RAD-10]
					break;
				case ActorNameEnum.ReportManager:
					// received Image Availability Query [RAD-11]
					break;
				default:
					break;
			}
		}	
	}
}



