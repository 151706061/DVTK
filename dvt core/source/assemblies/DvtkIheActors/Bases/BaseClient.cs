// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for BaseClient.
	/// </summary>
	public abstract class BaseClient
	{
		private BaseActor _parentActor;
		private ActorNameEnum _actorName;

		public BaseClient(BaseActor parentActor, ActorNameEnum actorName)
		{
			_parentActor = parentActor;
			_actorName = actorName;
		}

		public ActorNameEnum ActorName
		{
			get
			{
				return _actorName;
			}
		}

		public BaseActor ParentActor
		{
			get
			{
				return _parentActor;
			}
		}
	}
}
