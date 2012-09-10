// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for TriggerQueue.
	/// </summary>
	public class TriggerQueue
	{
		private System.Collections.Queue _queue = null;
 
		public TriggerQueue()
		{
			_queue = System.Collections.Queue.Synchronized(new System.Collections.Queue());
		}

		public void Enqueue(BaseTrigger trigger)
		{
			_queue.Enqueue(trigger);
		}

		public BaseTrigger Dequeue()
		{
			BaseTrigger trigger = (BaseTrigger)_queue.Dequeue();
			return trigger;
		}

		public bool IsEmpty()
		{
			bool isEmpty = (_queue.Count > 0) ? false : true;
			return isEmpty;
		}
	}
}
