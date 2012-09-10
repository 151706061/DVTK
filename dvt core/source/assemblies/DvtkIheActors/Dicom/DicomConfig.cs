// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.Xml;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for DicomConfig.
	/// </summary>
	public class DicomConfig : BaseConfig
	{
		private System.UInt16 _sessionId = 0;
		private CommunicationNode _dvtNode = new CommunicationNode();
		private CommunicationNode _sutNode = new CommunicationNode();
		private System.Collections.ArrayList _definitionFiles = new ArrayList();
		private System.String _dataDirectory = "";
		private bool _storeData = false;
		private System.String _resultsRootDirectory = "";

		public DicomConfig(ActorNameEnum actorName) : base(actorName)
		{
			_sessionId = 0;
			_dvtNode.AeTitle = "DVT_AE";
			_dvtNode.IpAddress = "localhost";
			_dvtNode.PortNumber = 104;
			_sutNode.AeTitle = "SUT_AE";
			_sutNode.IpAddress = "localhost";
			_sutNode.PortNumber = 104;
			_definitionFiles = new ArrayList();
			_dataDirectory = "";
			_storeData = false;
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

		public void AddDefinitionFile(System.String definitionFile)
		{
			_definitionFiles.Add(definitionFile);
		}

		public System.Collections.ArrayList DefinitionFiles
		{
			get
			{
				return _definitionFiles;
			}
		}

		public System.String DataDirectory
		{
			get 
			{ 
				return _dataDirectory; 
			}
			set 
			{ 
				_dataDirectory = value; 
			}
		}

		public bool StoreData
		{
			get 
			{ 
				return _storeData; 
			}
			set 
			{ 
				_storeData = value; 
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
			writer.WriteStartElement("PeerIheActorDicomConfiguration");
			writer.WriteElementString("ActorName", ActorNames.Name(ActorName));
			writer.WriteElementString("SessionId", _sessionId.ToString());
			writer.WriteStartElement("DvtNode");
			writer.WriteElementString("AeTitle", _dvtNode.AeTitle);
			writer.WriteElementString("IpAddress", _dvtNode.IpAddress);
			writer.WriteElementString("PortNumber", _dvtNode.PortNumber.ToString());
			writer.WriteEndElement();
			writer.WriteStartElement("SutNode");
			writer.WriteElementString("AeTitle", _sutNode.AeTitle);
			writer.WriteElementString("IpAddress", _sutNode.IpAddress);
			writer.WriteElementString("PortNumber", _sutNode.PortNumber.ToString());
			writer.WriteEndElement();
			writer.WriteElementString("DataDirectory", _dataDirectory);
			writer.WriteElementString("StoreData", _storeData.ToString());
			writer.WriteStartElement("DefinitionFiles");
			foreach (System.String defintionFilename in _definitionFiles)
			{
				writer.WriteElementString("DefinitionFile", defintionFilename);
			}
			writer.WriteEndElement();
			writer.WriteElementString("ResultsRootDirectory", _resultsRootDirectory);
			writer.WriteEndElement();
		}
	}
}
