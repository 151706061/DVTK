// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.Xml;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for Hl7Config.
	/// </summary>
	public class Hl7Config : BaseConfig
	{
		private System.UInt16 _sessionId = 0;
		private CommunicationNode _dvtNode = new CommunicationNode();
		private CommunicationNode _sutNode = new CommunicationNode();
		private System.String _resultsRootDirectory = "";

		public Hl7Config(ActorNameEnum actorName) : base(actorName)
		{
			_sessionId = 0;
			_dvtNode.AeTitle = "HL7_AE";
			_dvtNode.IpAddress = "localhost";
			_dvtNode.PortNumber = 104;
			_sutNode.AeTitle = "SUT_AE";
			_sutNode.IpAddress = "localhost";
			_sutNode.PortNumber = 104;
			_resultsRootDirectory = "";
		}

		public System.UInt16 SessionId
		{
			get 
			{ 
				return _sessionId; 
			}
			set 
			{ 
				_sessionId = value; 
			}
		}

		public System.String DvtAeTitle
		{
			get 
			{ 
				return _dvtNode.AeTitle; 
			}
			set 
			{ 
				_dvtNode.AeTitle = value; 
			}
		}

		public System.String DvtIpAddress
		{
			get 
			{ 
				return _dvtNode.IpAddress; 
			}
			set 
			{ 
				_dvtNode.IpAddress = value; 
			}
		}

		public System.UInt16 DvtPortNumber
		{
			get 
			{ 
				return _dvtNode.PortNumber; 
			}
			set 
			{ 
				_dvtNode.PortNumber = value; 
			}
		}

		public System.String SutAeTitle
		{
			get 
			{ 
				return _sutNode.AeTitle; 
			}
			set 
			{ 
				_sutNode.AeTitle = value; 
			}
		}

		public System.String SutIpAddress
		{
			get 
			{ 
				return _sutNode.IpAddress; 
			}
			set 
			{ 
				_sutNode.IpAddress = value; 
			}
		}

		public System.UInt16 SutPortNumber
		{
			get 
			{ 
				return _sutNode.PortNumber; 
			}
			set 
			{ 
				_sutNode.PortNumber = value; 
			}
		}

		public System.String ResultsRootDirectory
		{
			get 
			{ 
				return _resultsRootDirectory; 
			}
			set 
			{ 
				_resultsRootDirectory = value; 
			}
		}

		public override void WriteXmlConfig(XmlTextWriter writer)
		{
		}
	}
}
