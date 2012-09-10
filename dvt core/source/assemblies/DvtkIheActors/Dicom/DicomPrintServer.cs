// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using Dvtk.DvtkDicomEmulators.PrintClientServers;

namespace Dvtk.IheActors.Dicom
{
	/// <summary>
	/// Summary description for DicomPrintServer.
	/// </summary>
	public class DicomPrintServer : DicomServer
	{
		public DicomPrintServer(BaseActor parentActor, ActorNameEnum actorName) : base (parentActor, actorName) {}

		public override void ApplyConfig(DicomConfig config)
		{
			PrintScp printScp = new PrintScp();
			printScp.AddDefaultMessageHandlers();
			Scp = printScp;
			base.ApplyConfig(config);
		}

		public override void AssociationReleasedEventHandler(DicomThread printScp)
		{
			// do SCP specific post association processing here

			// call base implementation to generate event, update transaction log and cleanup data warehouse
			base.AssociationReleasedEventHandler(printScp);
		}
	}
}
