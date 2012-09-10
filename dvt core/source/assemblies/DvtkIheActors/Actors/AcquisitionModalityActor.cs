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
	/// Summary description for AcquisitionModalityActor.
	/// </summary>
	public class AcquisitionModalityActor : BaseActor
	{
		public AcquisitionModalityActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.AcquisitionModality, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Storage Commitment [RAD-10]
			AddDicomServer(new DicomStorageCommitServer(this, ActorNameEnum.ImageManager), actorConfig);

			// for sending Query Modality Worklist [RAD-5]
			AddDicomClient(new DicomWorklistClient(this, ActorNameEnum.DssOrderFiller), actorConfig);

			// for sending Modality Procedure Step In Progress [RAD-6]
			// for sending Modality Procedure Step Completed [RAD-7]
			AddDicomClient(new DicomMppsClient(this, ActorNameEnum.PerformedProcedureStepManager), actorConfig);

			// for sending Modality Images Stored [RAD-8]
			AddDicomClient(new DicomStorageClient(this, ActorNameEnum.ImageArchive), actorConfig);

			// for sending Storage Commitment [RAD-10]
			AddDicomClient(new DicomStorageCommitClient(this, ActorNameEnum.ImageManager), actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.ImageManager:
					// received Storage Commitment [RAD-10]
					break;
				default:
					break;
			}
		}
	}
}
