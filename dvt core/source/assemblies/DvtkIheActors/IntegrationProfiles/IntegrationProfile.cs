// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

using DvtkHighLevelInterface;
using Dvtk.Results;
using Dvtk.Comparator;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for IntegrationProfile.
	/// </summary>
	public class IntegrationProfile
	{
		private System.String _profileName;
		private ActorCollection _actors = null;
		private ActorConfigCollection _actorConfigs = null;
		private ActorsTransactionLog _actorsTransactionLog = null;

		private System.String _resultsDirectory = @"C:\Program Files\DVT20\results";
		private System.String _applicationDirectory = System.String.Empty;

		private ResultsReporter _resultsReporter = null;
		private System.String _resultsFilename = System.String.Empty;

		/// <summary>
		/// Class Constructor.
		/// </summary>
		/// <param name="profileName">Integration Profile Name.</param>
		public IntegrationProfile(System.String profileName)
		{
			_profileName = profileName;
			_actors = new ActorCollection();
			_actorConfigs = new ActorConfigCollection();
			_actorsTransactionLog = new ActorsTransactionLog();
			_applicationDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

			_resultsReporter = new ResultsReporter(_resultsDirectory);
		}

		public void OpenResults()
		{
			_resultsFilename = _profileName.Replace(" ","") + "ResultsIndex.xml";
			_resultsReporter.Start(_resultsFilename);
		}

		public void CloseResults()
		{
			_resultsReporter.Stop();
			ConvertResult();
		}

		public System.String ResultsFilename
		{
			get
			{
				System.String resultsFilename = _resultsDirectory + "\\Detail_" + _resultsFilename;
				resultsFilename = resultsFilename.Replace(".xml", ".html");
				return resultsFilename;
			}
		}

		public void LoadConfiguration(System.String configurationFilename)
		{
			XmlTextReader reader = new XmlTextReader(configurationFilename);
			while (reader.EOF == false)
			{
				reader.ReadStartElement("IheIntegrationProfile");
				_profileName = reader.ReadElementString("IntegrationProfileName");

				while (reader.IsStartElement())
				{
					reader.ReadStartElement("IheActorConfiguration");
					System.String actorNameString = reader.ReadElementString("ActorName");

					BaseActor actor = CreateActor(actorNameString);
					ActorConfig actorConfig = new ActorConfig(actor.ActorName);
					
					while (reader.IsStartElement())
					{
						reader.ReadStartElement("PeerIheActorDicomConfiguration");
						System.String peerActorNameString = reader.ReadElementString("ActorName");

						ActorNameEnum peerActorName = ActorNames.NameEnum(peerActorNameString);
						DicomConfig dicomConfig = new DicomConfig(peerActorName);

						dicomConfig.SessionId = UInt16.Parse(reader.ReadElementString("SessionId"));
						reader.ReadStartElement("DvtNode");
						dicomConfig.DvtAeTitle = reader.ReadElementString("AeTitle");
						dicomConfig.DvtIpAddress = reader.ReadElementString("IpAddress");
						dicomConfig.DvtPortNumber = UInt16.Parse(reader.ReadElementString("PortNumber"));
						reader.ReadEndElement();
						reader.ReadStartElement("SutNode");
						dicomConfig.SutAeTitle = reader.ReadElementString("AeTitle");
						dicomConfig.SutIpAddress = reader.ReadElementString("IpAddress");
						dicomConfig.SutPortNumber = UInt16.Parse(reader.ReadElementString("PortNumber"));
						reader.ReadEndElement();
						dicomConfig.DataDirectory = reader.ReadElementString("DataDirectory");
						System.String storeData = reader.ReadElementString("StoreData");
						if (storeData == "True")
						{
							dicomConfig.StoreData = true;
						}
						else
						{
							dicomConfig.StoreData = false;
						}
						reader.ReadStartElement("DefinitionFiles");

						bool readingDefinitions = true;
						while (readingDefinitions == true)
						{
							dicomConfig.AddDefinitionFile(reader.ReadElementString("DefinitionFile"));
							reader.Read();
							if ((reader.NodeType == XmlNodeType.EndElement) &&
								(reader.Name == "DefinitionFiles"))
							{
								reader.Read();
								readingDefinitions = false;
							}
						}
						
						dicomConfig.ResultsRootDirectory = reader.ReadElementString("ResultsRootDirectory");
		
						_resultsDirectory = dicomConfig.ResultsRootDirectory;

						reader.ReadEndElement();

						actorConfig.Add(dicomConfig);
					}
					reader.ReadEndElement();

					actor.ConfigActor(actorConfig);
					_actors.Add(actor);
				}
				reader.ReadEndElement();
			}

			reader.Close();
		}

		public void SaveConfiguration(System.String configurationFilename)
		{
			XmlTextWriter writer = new XmlTextWriter(configurationFilename, System.Text.Encoding.ASCII);
			writer.Formatting = Formatting.Indented;
			writer.WriteStartDocument(true);
			writer.WriteStartElement("IheIntegrationProfile");
			writer.WriteElementString("IntegrationProfileName", _profileName);

			foreach(ActorConfig actorConfig in _actorConfigs)
			{
				actorConfig.WriteXmlConfig(writer);
			}

			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Flush();
			writer.Close();
		}

		private BaseActor CreateActor(System.String actorNameString)
		{
			BaseActor actor = null;
			ActorNameEnum actorName = ActorNames.NameEnum(actorNameString);
			switch(actorName)
			{
				case ActorNameEnum.AdtPatientRegistration:
					actor = new AdtPatientRegistrationActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.OrderPlacer:
					actor = new OrderPlacerActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.DssOrderFiller:
					actor = new DssOrderFillerActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.AcquisitionModality:
					actor = new AcquisitionModalityActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.ImageManager:
					actor = new ImageManagerActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.ImageArchive:
					actor = new ImageArchiveActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.PerformedProcedureStepManager:
					actor = new PpsManagerActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.EvidenceCreator:
					actor = new EvidenceCreatorActor(_actorsTransactionLog);
					break;
//				case ActorNameEnum.ReportManager:
//					actor = new ReportManagerActor(_actorsTransactionLog);
//					break;
				case ActorNameEnum.PrintComposer:
					actor = new PrintComposerActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.PrintServer:
					actor = new PrintServerActor(_actorsTransactionLog);
					break;
				case ActorNameEnum.Unknown:
				default:
					break;
			}

			return actor;
		}

		/// <summary>
		/// Start the integration profile test by starting up all the
		/// configured actors.
		/// </summary>
		public void StartTest()
		{
			// start all the actors
			foreach (BaseActor actor in _actors)
			{
				actor.StartActor();
			}
		}

		/// <summary>
		/// Stop the integration profile test by stopping all the configured
		/// actors.
		/// </summary>
		public void StopTest()
		{
			// stop all actors
			foreach (BaseActor actor in _actors)
			{
				actor.StopActor();
			}
		}

		/// <summary>
		/// Evaluate the integration profile test.
		/// </summary>
		public void EvaluateTest()
		{
			ReportTransactions();

			_actorsTransactionLog.Evaluate(_resultsReporter);

//			_actorsTransactionLog.ConsoleDisplay();
		}

		private void ReportTransactions()
		{
			System.String message = System.String.Format("<h1>IHE {0} Integration Profile</h1>", _profileName);
			_resultsReporter.WriteHtmlInformation(message);
			_resultsReporter.WriteHtmlInformation("<h2>DICOM Actor Transactions</h2>");
			for (int i = 0; i < _actorsTransactionLog.Count; i++)
			{
				foreach (ActorsTransaction actorsTransaction in _actorsTransactionLog)
				{				
					if (actorsTransaction.TransactionNumber == i + 1)
					{
						actorsTransaction.ConvertResult(_applicationDirectory, _resultsDirectory);
						System.String resultsFilename = _resultsDirectory + "\\Detail_" + actorsTransaction.ResultsFilename.Replace(".xml", ".html");
						switch(actorsTransaction.Direction)
						{
							case TransactionDirectionEnum.TransactionReceived:
								message = System.String.Format("{0:000} - <a href=\"{1}\">Received by {2} from {3}</a><br/>",
									actorsTransaction.TransactionNumber,
									resultsFilename,
									ActorNames.Name(actorsTransaction.FromActorName),
									ActorNames.Name(actorsTransaction.ToActorName));
								_resultsReporter.WriteHtmlInformation(message);
								break;
							case TransactionDirectionEnum.TransactionSent:
								message = System.String.Format("{0:000} - <a href=\"{1}\">Sent from {2} to {3}</a><br/>",
									actorsTransaction.TransactionNumber,
									resultsFilename,
									ActorNames.Name(actorsTransaction.FromActorName),
									ActorNames.Name(actorsTransaction.ToActorName));
								_resultsReporter.WriteHtmlInformation(message);
								break;
							default:
								break;
						}
						break;
					}
				}
			}
		}

		private void ConvertResult()
		{
			XslTransform xslt = new XslTransform();
			xslt.Load(_applicationDirectory + "DVT_RESULTS.xslt");

			System.String resultsFilename = _resultsDirectory + "\\Summary_" + _resultsFilename;
			XPathDocument xpathdocument = new XPathDocument(resultsFilename);
			XmlTextWriter writer = new XmlTextWriter(resultsFilename.Replace(".xml", ".html"), System.Text.Encoding.UTF8);
			writer.Formatting = Formatting.None;
			xslt.Transform(xpathdocument, null, writer, null);
			writer.Flush();
			writer.Close();

			resultsFilename = _resultsDirectory + "\\Detail_" + _resultsFilename;
			xpathdocument = new XPathDocument(resultsFilename);
			writer = new XmlTextWriter(resultsFilename.Replace(".xml", ".html"), System.Text.Encoding.UTF8);
			writer.Formatting = Formatting.None;
			xslt.Transform(xpathdocument, null, writer, null);
			writer.Flush();
			writer.Close();
		}
	}
}
