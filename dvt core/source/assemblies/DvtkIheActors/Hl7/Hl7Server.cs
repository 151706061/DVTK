// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for Hl7Server.
	/// </summary>
	public class Hl7Server : BaseServer, IServer
	{
		private Hl7Config _config;

		public Hl7Server(BaseActor parentActor, ActorNameEnum actorName, Hl7Config config) : base(parentActor, actorName)
		{
			_config = config;
		}

		public void StartServer()
		{
		}

		public void StopServer()
		{
		}
	}
}
