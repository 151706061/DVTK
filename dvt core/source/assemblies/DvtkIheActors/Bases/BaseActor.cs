// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using DvtkHighLevelInterface;

namespace Dvtk.IheActors
{
	#region actor names

	public enum ActorNameEnum
	{
		AdtPatientRegistration,
		OrderPlacer,
		DssOrderFiller,
		AcquisitionModality,
		ImageManager,
		ImageArchive,
		PerformedProcedureStepManager,
		ImageDisplay,
		EvidenceCreator,
		ReportManager,
		PrintComposer,
		PrintServer,
		Unknown
	}

	public class ActorNames
	{
		public static System.String Name(ActorNameEnum actorName)
		{
			System.String name = "Unknown";

			switch(actorName)
			{
				case ActorNameEnum.AdtPatientRegistration: name = "AdtPatientRegistration"; break;
				case ActorNameEnum.OrderPlacer: name = "OrderPlacer"; break;
				case ActorNameEnum.DssOrderFiller: name = "DssOrderFiller"; break;
				case ActorNameEnum.AcquisitionModality: name = "AcquisitionModality"; break;
				case ActorNameEnum.ImageManager: name = "ImageManager"; break;
				case ActorNameEnum.ImageArchive: name = "ImageArchive"; break;
				case ActorNameEnum.PerformedProcedureStepManager: name = "PerformedProcedureStepManager"; break;
				case ActorNameEnum.ImageDisplay: name = "ImageDisplay"; break;
				case ActorNameEnum.EvidenceCreator: name = "EvidenceCreator"; break;
				case ActorNameEnum.ReportManager: name = "ReportManager"; break;
				case ActorNameEnum.PrintComposer: name = "PrintComposer"; break;
				case ActorNameEnum.PrintServer: name = "PrintServer"; break;
				default:
					break;
			}

			return name;
		}

		public static ActorNameEnum NameEnum(System.String name)
		{
			ActorNameEnum nameEnum = ActorNameEnum.Unknown;

			if (name == "AdtPatientRegistration")
			{
				nameEnum = ActorNameEnum.AdtPatientRegistration;
			}
			else if (name == "OrderPlacer")
			{
				nameEnum = ActorNameEnum.OrderPlacer;
			}
			else if (name == "DssOrderFiller")
			{
				nameEnum = ActorNameEnum.DssOrderFiller;
			}
			else if (name == "AcquisitionModality")
			{
				nameEnum = ActorNameEnum.AcquisitionModality;
			}
			else if (name == "ImageManager")
			{
				nameEnum = ActorNameEnum.ImageManager;
			}
			else if (name == "ImageArchive")
			{
				nameEnum = ActorNameEnum.ImageArchive;
			}
			else if (name == "PerformedProcedureStepManager")
			{
				nameEnum = ActorNameEnum.PerformedProcedureStepManager;
			}
			else if (name == "ImageDisplay")
			{
				nameEnum = ActorNameEnum.ImageDisplay;
			}
			else if (name == "EvidenceCreator")
			{
				nameEnum = ActorNameEnum.EvidenceCreator;
			}
			else if (name == "ReportManager")
			{
				nameEnum = ActorNameEnum.ReportManager;
			}
			else if (name == "PrintComposer")
			{
				nameEnum = ActorNameEnum.PrintComposer;
			}
			else if (name == "PrintServer")
			{
				nameEnum = ActorNameEnum.PrintServer;
			}

			return nameEnum;
		}
	}
	#endregion

	/// <summary>
	/// Summary description for BaseActor.
	/// </summary>
	public abstract class BaseActor
	{
		#region actor states
		private enum ActorStateEnum
		{
			ActorCreated,	// created - but not configured
			ActorStarted,	// started - but configured
			ActorStopped,	// stopped - but configured
		}
		#endregion

		private ActorNameEnum _actorName;
		private ActorsTransactionLog _actorsTransactionLog = null;
		private ActorStateEnum _actorState;
		private System.Collections.Hashtable _hl7Servers = null;
		private System.Collections.Hashtable _hl7Clients = null;
		private System.Collections.Hashtable _dicomServers = null;
		private System.Collections.Hashtable _dicomClients = null;
		private DvtThreadManager _dvtThreadManager = null;

		public BaseActor(ActorNameEnum actorName, ActorsTransactionLog actorsTransactionLog)
		{
			_actorName = actorName;
			_actorsTransactionLog = actorsTransactionLog;
			InitActor();
		}

		public ActorNameEnum ActorName
		{
			get
			{
				return _actorName;
			}
		}

		public ActorsTransactionLog ActorsTransactionLog
		{
			get
			{
				return _actorsTransactionLog;
			}
		}

		public DvtThreadManager DvtThreadManager
		{
			get
			{
				return _dvtThreadManager;
			}
		}

		private void InitActor()
		{
			_hl7Servers = new Hashtable();
			_hl7Clients = new Hashtable();
			_dicomServers = new Hashtable();
			_dicomClients = new Hashtable();
			_actorState = ActorStateEnum.ActorCreated;
			_dvtThreadManager = new DvtThreadManager(ActorNames.Name(_actorName));
		}

		public void ConfigActor(ActorConfig actorConfig)
		{
			switch (_actorState)
			{
				case ActorStateEnum.ActorCreated:
				case ActorStateEnum.ActorStopped:
					// initialize the actor - this allows an actor to be re-configured after being used
					InitActor();

					// call sub-class method to apply the actor specific config
					ApplyConfig(actorConfig);

					// set state to stopped
					_actorState = ActorStateEnum.ActorStopped;
					break;
				default:
					// actor is started - so cannot be (re)configured
					break;
			}
		}

		protected abstract void ApplyConfig(ActorConfig actorConfig);

		public void StartActor()
		{
			switch (_actorState)
			{
				case ActorStateEnum.ActorStopped:
				{
					ICollection hl7Servers = _hl7Servers.Values;
					foreach (Hl7Server hl7Server in hl7Servers)
					{
						hl7Server.StartServer();
					}

					ICollection hl7Clients = _hl7Clients.Values;
					foreach (Hl7Client hl7Client in hl7Clients)
					{
						hl7Client.StartClient();
					}

					ICollection dicomServers = _dicomServers.Values;
					foreach (DicomServer dicomServer in dicomServers)
					{
						dicomServer.StartServer();
					}

					ICollection dicomClients = _dicomClients.Values;
					foreach (DicomClient dicomClient in dicomClients)
					{
						dicomClient.StartClient();
					}

					_actorState = ActorStateEnum.ActorStarted;
					break;
				}
				default:
					// actor either not configured or already started
					break;
			}
		}

		public void StopActor()
		{
			switch (_actorState)
			{
				case ActorStateEnum.ActorStarted:
				{
					ICollection hl7Servers = _hl7Servers.Values;
					foreach (Hl7Server hl7Server in hl7Servers)
					{
						hl7Server.StopServer();
					}

					ICollection hl7Clients = _hl7Clients.Values;
					foreach (Hl7Client hl7Client in hl7Clients)
					{
						hl7Client.StopClient();
					}

					ICollection dicomServers = _dicomServers.Values;
					foreach (DicomServer dicomServer in dicomServers)
					{
						dicomServer.StopServer();
					}

					ICollection dicomClients = _dicomClients.Values;
					foreach (DicomClient dicomClient in dicomClients)
					{
						dicomClient.StopClient();
					}

					_dvtThreadManager.WaitForCompletionThreads();
					_actorState = ActorStateEnum.ActorStopped;
					break;
				}
				default:
					// actor either not configured or already stopped
					break;
			}
		}

		public void TriggerActor(ActorNameEnum actorName, BaseTrigger trigger)
		{
			// can only trigger an actor that is started
			if (_actorState == ActorStateEnum.ActorStarted)
			{
				if (trigger is Hl7Trigger)
				{
					Hl7Client hl7Client = GetHl7Client(actorName);
					if (hl7Client != null)
					{
						hl7Client.TriggerClient(actorName, trigger);
					}
				}
				else
				{
					DicomClient dicomClient = GetDicomClient(actorName);
					if (dicomClient != null)
					{
						dicomClient.TriggerClient(actorName, trigger);
					}
				}
			}
		}

		private int Hl7ConfigPresent(ActorNameEnum actorName, ActorConfig actorConfig)
		{
			int index = 0;
			foreach (BaseConfig baseConfig in actorConfig)
			{
				if ((baseConfig is Hl7Config) &&
					(baseConfig.ActorName == actorName))
				{
					return index;
				}
				index++;
			}

			return -1;
		}

		private int DicomConfigPresent(ActorNameEnum actorName, ActorConfig actorConfig)
		{
			int index = 0;
			foreach (BaseConfig baseConfig in actorConfig)
			{
				if ((baseConfig is DicomConfig) &&
					(baseConfig.ActorName == actorName))
				{
					return index;
				}
				index++;
			}

			return -1;
		}

		public Hl7Server GetHl7Server(ActorNameEnum actorName)
		{
			return (Hl7Server)_hl7Servers[actorName];
		}

		protected void AddHl7Server(ActorNameEnum actorName, ActorConfig actorConfig)
		{
			int index = Hl7ConfigPresent(actorName, actorConfig);
			if (index != -1)
			{
				Hl7Server hl7Server = new Hl7Server(this, actorName, (Hl7Config)actorConfig[index]);
				SubscribeEvent(hl7Server);
				_hl7Servers.Add(actorName, hl7Server);
			}
		}

		public Hl7Client GetHl7Client(ActorNameEnum actorName)
		{
			return (Hl7Client)_hl7Clients[actorName];
		}

		protected void AddHl7Client(ActorNameEnum actorName, ActorConfig actorConfig)
		{
			int index = Hl7ConfigPresent(actorName, actorConfig);
			if (index != -1)
			{
				Hl7Client hl7Client = new Hl7Client(this, actorName, (Hl7Config)actorConfig[index]);
				_hl7Clients.Add(actorName, hl7Client);
			}
		}

		public DicomServer GetDicomServer(ActorNameEnum actorName)
		{
			return (DicomServer)_dicomServers[actorName];
		}

		protected void AddDicomServer(DicomServer dicomServer, ActorConfig actorConfig)
		{
			int index = DicomConfigPresent(dicomServer.ActorName, actorConfig);
			if (index != -1)
			{
				dicomServer.ApplyConfig((DicomConfig)actorConfig[index]);
				SubscribeEvent(dicomServer);
				_dicomServers.Add(dicomServer.ActorName, dicomServer);
			}
		}

		public DicomClient GetDicomClient(ActorNameEnum actorName)
		{
			return (DicomClient)_dicomClients[actorName];
		}

		protected void AddDicomClient(DicomClient dicomClient, ActorConfig actorConfig)
		{
			int index = DicomConfigPresent(dicomClient.ActorName, actorConfig);
			if (index != -1)
			{
				dicomClient.ApplyConfig( (DicomConfig)actorConfig[index]);
				_dicomClients.Add(dicomClient.ActorName, dicomClient);
			}
		}

		private void SubscribeEvent(DicomServer dicomServer)
		{
			dicomServer.OnTransactionAvailable += new DicomServer.TransactionAvailableHandler(TransactionIsAvailable);
		}

		private void SubscribeEvent(Hl7Server hl7Server)
		{
			hl7Server.OnTransactionAvailable += new Hl7Server.TransactionAvailableHandler(TransactionIsAvailable);
		}

		public void TransactionIsAvailable(object server, TransactionAvailableEventArgs transactionAvailableEvent)
		{
			// handle the new transaction
			if (transactionAvailableEvent.Transaction is Hl7Transaction)
			{
				HandleTransactionFrom(transactionAvailableEvent.ActorName, (Hl7Transaction)transactionAvailableEvent.Transaction);
			}
			else
			{
				HandleTransactionFrom(transactionAvailableEvent.ActorName, (DicomTransaction)transactionAvailableEvent.Transaction);
			}
		}

		public virtual void HandleTransactionFrom(ActorNameEnum actorName, Hl7Transaction hl7Transaction) {}

		public virtual void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction) {}
	}  
}
