// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using Dvtk.DvtkDicomEmulators.MppsClientServers;
using Dvtk.DvtkDicomEmulators.MppsMessageHandlers;
using Dvtk.IheActors;

namespace Dvtk.IheActors.Dicom
{
	/// <summary>
	/// Summary description for DicomMppsServer.
	/// </summary>
	public class DicomMppsServer : DicomServer
	{
		public DicomMppsServer(BaseActor parentActor, ActorNameEnum actorName) : base (parentActor, actorName) {}

		public override void ApplyConfig(DicomConfig config)
		{
			MppsScp mppsScp = new MppsScp();
			mppsScp.AddDefaultMessageHandlers();
			Scp = mppsScp;
			base.ApplyConfig(config);
		}

		public override void AssociationReleasedEventHandler(DicomThread mppsScp)
		{
			// do SCP specific post association processing here

			// call base implementation to generate event, update transaction log and cleanup data warehouse
			base.AssociationReleasedEventHandler(mppsScp);
		}
	}
}
