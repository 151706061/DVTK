// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for CommunicationNode.
	/// </summary>
	public class CommunicationNode
	{
		private System.String _ipAddress;
		private System.UInt16 _portNumber;
		private System.String _aeTitle;

		public CommunicationNode()
		{
			//
			// Constructor activities
			//
			_ipAddress = "localhost";
			_portNumber = 104;
			_aeTitle = "AE_TITLE";
		}

		public CommunicationNode(System.String ipAddress, System.UInt16 portNumber, System.String aeTitle)
		{
			//
			// Constructor activities
			//
			_ipAddress = ipAddress;
			_portNumber = portNumber;
			_aeTitle = aeTitle;
		}

		public System.String IpAddress
		{
			get 
			{ 
				return _ipAddress; 
			}
			set 
			{ 
				_ipAddress = value; 
			}
		}

		public System.UInt16 PortNumber
		{
			get 
			{ 
				return _portNumber; 
			}
			set 
			{ 
				_portNumber = value; 
			}
		}

		public System.String AeTitle
		{
			get 
			{ 
				return _aeTitle; 
			}
			set 
			{ 
				_aeTitle = value; 
			}
		}
	}
}
