// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for IClient.
	/// </summary>
	public interface IClient
	{
		void StartClient();

		void TriggerClient(ActorNameEnum actorName, BaseTrigger trigger);

		void StopClient();
	}
}
