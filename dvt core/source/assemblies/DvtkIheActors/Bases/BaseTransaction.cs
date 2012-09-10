// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	#region transaction names
	public enum TransactionNameEnum
	{
		RAD_1,
		RAD_2,
		RAD_3,
		RAD_4,
		RAD_5,
		RAD_6,
		RAD_7,
		RAD_8,
		RAD_9,
		RAD_10,
		RAD_11,
		RAD_12,
		RAD_13,
		RAD_14,
		RAD_15,
		RAD_16,
		RAD_17,
		RAD_18,
		RAD_19,
		RAD_20,
		RAD_21,
		RAD_23,
		RAD_42,
		RAD_48,
		RAD_49,
	}
	#endregion

	#region transaction directions
	public enum TransactionDirectionEnum
	{
		TransactionSent,
		TransactionReceived
	}
	#endregion

	/// <summary>
	/// Summary description for BaseTransaction.
	/// </summary>
	public abstract class BaseTransaction
	{
		private TransactionNameEnum _transactionName;
		private TransactionDirectionEnum _direction;

		public BaseTransaction(TransactionNameEnum transactionName, TransactionDirectionEnum direction)
		{
			_transactionName = transactionName;
			_direction = direction;
		}

		public TransactionDirectionEnum Direction
		{
			get
			{
				return _direction;
			}
		}

		public TransactionNameEnum TransactionName
		{
			get 
			{ 
				return _transactionName; 
			}
		} 
	}
}
