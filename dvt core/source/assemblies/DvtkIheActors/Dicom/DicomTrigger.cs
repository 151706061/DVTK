// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for DicomTrigger.
	/// </summary>
	public class DicomTrigger : BaseTrigger
	{
		public DicomTrigger(TransactionNameEnum triggerName) : base(triggerName)
		{
		}

		public DicomMessage Trigger
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
		private DicomMessage _trigger;

	}
}
