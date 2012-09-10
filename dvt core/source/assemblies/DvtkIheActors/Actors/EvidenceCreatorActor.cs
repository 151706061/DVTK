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
	/// Summary description for EvidenceCreatorActor.
	/// </summary>
	public class EvidenceCreatorActor : BaseActor
	{
		public EvidenceCreatorActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.EvidenceCreator, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Storage Commitment [RAD-10]
			AddDicomServer(new DicomStorageCommitServer(this, ActorNameEnum.ImageManager), actorConfig);

			// for sending Creator Procedure Step In Progress [RAD-20]
			// for sending Creator Procedure Step Completed [RAD-21]
			AddDicomClient(new DicomMppsClient(this, ActorNameEnum.PerformedProcedureStepManager), actorConfig);

			// for sending Creator Images Stored [RAD-18]
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
