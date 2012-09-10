// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Threading;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for BaseServer.
	/// </summary>
	public abstract class BaseServer
	{
		private BaseActor _parentActor;
		private ActorNameEnum _actorName;

		public BaseServer(BaseActor parentActor, ActorNameEnum actorName)
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

		public delegate void TransactionAvailableHandler(object server, TransactionAvailableEventArgs transactionAvailable);

		public event TransactionAvailableHandler OnTransactionAvailable;

		public void PublishEvent(ActorNameEnum actorName, BaseTransaction transaction)
		{
			TransactionAvailableEventArgs transactionAvailableEvent = new TransactionAvailableEventArgs(actorName, transaction);

			if (OnTransactionAvailable != null)
			{
				OnTransactionAvailable(this, transactionAvailableEvent);
			}
		}
	}
}
