// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for TransactionNumber.
	/// </summary>
	internal class TransactionNumber
	{
		private static int _transactionNumber = 1;

		public static int GetNextTransactionNumber()
		{
			return _transactionNumber++;
		}
	}
}
