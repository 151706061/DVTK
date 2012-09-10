// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for TransactionAvailableEventArgs.
	/// </summary>
	public class TransactionAvailableEventArgs : EventArgs
	{
		private ActorNameEnum _actorName;
		private BaseTransaction _transaction;

		public TransactionAvailableEventArgs(ActorNameEnum actorName, BaseTransaction transaction)
		{
			_actorName = actorName;
			_transaction = transaction;
		}

		public ActorNameEnum ActorName
		{
			get
			{
				return _actorName;
			}
		}

		public BaseTransaction Transaction
		{
			get
			{
				return _transaction;
			}
		}
	}
}
