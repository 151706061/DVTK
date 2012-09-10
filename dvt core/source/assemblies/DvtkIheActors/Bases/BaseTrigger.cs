// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for BaseTrigger.
	/// </summary>
	public abstract class BaseTrigger
	{
		public BaseTrigger(TransactionNameEnum triggerName)
		{
			_triggerName = triggerName;
		}

		public TransactionNameEnum TriggerName
		{
			get 
			{ 
				return _triggerName; 
			}
		} 
		private TransactionNameEnum _triggerName;
	}
}
