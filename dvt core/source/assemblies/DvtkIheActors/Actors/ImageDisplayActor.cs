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
	/// Summary description for ImageDisplayActor.
	/// </summary>
	public class ImageDisplayActor : BaseActor
	{
		public ImageDisplayActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.ImageDisplay, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Retrieve Images [RAD-16]
			AddDicomServer(new DicomStorageServer(this, ActorNameEnum.ImageArchive), actorConfig);

			// for sending Query Images [RAD-14]
			// for sending Retrieve Images [RAD-16]
			AddDicomClient(new DicomQueryRetrieveClient(this, ActorNameEnum.ImageArchive), actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.ImageArchive:
					// received Retrieve Images [RAD-16]
					break;
				default:
					break;
			}
		}	
	}
}
