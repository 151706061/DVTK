// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for Hl7Transaction.
	/// </summary>
	public class Hl7Transaction : BaseTransaction
	{
		public Hl7Transaction(TransactionNameEnum transactionName, TransactionDirectionEnum direction) : base(transactionName, direction)
		{
		}

		public Hl7Message Request
		{
			get 
			{ 
				return _request; 
			}
			set
			{
				_request = value;
			}
		} 
		private Hl7Message _request;

		public Hl7Message Response
		{
			get 
			{ 
				return _response; 
			}
			set
			{
				_response = value;
			}
		} 
		private Hl7Message _response;
	}
}
