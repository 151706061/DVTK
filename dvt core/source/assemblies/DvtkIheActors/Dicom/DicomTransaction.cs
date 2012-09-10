// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using DvtkData.Dul;
using DvtkData.Dimse;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for DicomTransaction.
	/// </summary>
	public class DicomTransaction : BaseTransaction
	{
		public DicomTransaction(TransactionNameEnum transactionName, TransactionDirectionEnum direction) : base(transactionName, direction)
		{
		}

		public DvtkData.Dul.A_ASSOCIATE_RQ AssocRequest
		{
			get 
			{ 
				return _assocRequest; 
			}
			set
			{
				_assocRequest = value;
			}
		} 
		private DvtkData.Dul.A_ASSOCIATE_RQ _assocRequest;

		public DvtkData.Dul.A_ASSOCIATE_AC AssocAccept
		{
			get 
			{ 
				return _assocAccept; 
			}
			set
			{
				_assocAccept = value;
			}
		} 
		private DvtkData.Dul.A_ASSOCIATE_AC _assocAccept;

		public System.Collections.ArrayList DicomMessages
		{
			get
			{
				return _dicomMessages;
			}
		}
		private System.Collections.ArrayList _dicomMessages = new ArrayList();
	}
}
