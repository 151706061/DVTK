// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for Hl7Client.
	/// </summary>
	public class Hl7Client : BaseClient, IClient
	{
		private Hl7Config _config;

		public Hl7Client(BaseActor parentActor, ActorNameEnum actorName, Hl7Config config) : base(parentActor, actorName)
		{
			_config = config;
		}

		public void StartClient()
		{
		}

		public void TriggerClient(ActorNameEnum actorName, BaseTrigger trigger)
		{
		}

		public void StopClient()
		{
		}
	}
}
