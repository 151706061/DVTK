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
	/// Summary description for PrintServerActor.
	/// </summary>
	public class PrintServerActor : BaseActor
	{
		public PrintServerActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.PrintServer, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Print Request with Presentation LUT [RAD-23]
			AddDicomServer(new DicomPrintServer(this, ActorNameEnum.PrintComposer), actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.PrintComposer:
					// received Print Request with Presentation LUT [RAD-23]
					break;
				default:
					break;
			}
		}	
	}
}
