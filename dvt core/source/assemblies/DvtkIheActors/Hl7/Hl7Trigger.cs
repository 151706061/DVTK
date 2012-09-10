// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for Hl7Trigger.
	/// </summary>
	public class Hl7Trigger : BaseTrigger
	{
		public Hl7Trigger(TransactionNameEnum triggerName) : base(triggerName)
		{
		}

		public Hl7Message Trigger
		{
			get 
			{ 
				return _trigger; 
			}
			set
			{
				_trigger = value;
			}
		} 
		private Hl7Message _trigger;
	}
}
