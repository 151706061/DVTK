// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Xml;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for BaseConfig.
	/// </summary>
	public abstract class BaseConfig
	{
		private ActorNameEnum _actorName;

		public BaseConfig(ActorNameEnum actorName)
		{
			_actorName = actorName;
		}

		public ActorNameEnum ActorName
		{
			get
			{
				return _actorName;
			}
		}

		public abstract void WriteXmlConfig(XmlTextWriter writer);
	}
}
