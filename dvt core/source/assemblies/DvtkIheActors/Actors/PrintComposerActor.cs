// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using Dvtk.IheActors.Dicom;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for PrintComposerActor.
	/// </summary>
	public class PrintComposerActor  : BaseActor
	{
		public PrintComposerActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.PrintComposer, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for sending Print Request with Presentation LUT [RAD-23]
			AddDicomClient(new DicomPrintClient(this, ActorNameEnum.PrintServer), actorConfig);
		}
	}
}
