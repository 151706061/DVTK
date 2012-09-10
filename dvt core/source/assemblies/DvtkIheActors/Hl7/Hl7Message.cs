// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for Hl7Message.
	/// </summary>
	public class Hl7Message
	{
		private System.String _message;

		public Hl7Message(System.String message)
		{
			_message = message;
		}

		public System.String Message
		{
			get
			{
				return _message;
			}
		}
	}
}
