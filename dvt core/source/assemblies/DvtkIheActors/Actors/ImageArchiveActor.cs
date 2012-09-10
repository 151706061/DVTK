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
	/// Summary description for ImageArchiveActor.
	/// </summary>
	public class ImageArchiveActor : BaseActor
	{
		public ImageArchiveActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.ImageArchive, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Modality Images Stored [RAD-8]
			AddDicomServer(new DicomStorageServer(this, ActorNameEnum.AcquisitionModality), actorConfig);

			// for receiving Creator Images Stored [RAD-18]
			AddDicomServer(new DicomStorageServer(this, ActorNameEnum.EvidenceCreator), actorConfig);

			// for receiving Query Images [RAD-14]
			// for receiving Retrieve Images [RAD-16]
			AddDicomServer(new DicomQueryRetrieveServer(this, ActorNameEnum.ImageDisplay), actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.AcquisitionModality:
					// received Modality Images Stored [RAD-8]
					break;
				case ActorNameEnum.EvidenceCreator:
					// received Creator Images Stored [RAD-18]
					break;
				case ActorNameEnum.ImageDisplay:
					// received Query Images [RAD-14] or
					// received Retrieve Images [RAD-16]
					break;
				default:
					break;
			}
		}
	}
}
