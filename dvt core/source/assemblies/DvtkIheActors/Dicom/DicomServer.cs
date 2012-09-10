// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for DicomServer.
	/// </summary>
	public abstract class DicomServer : BaseServer, IServer
	{
		private SCP _scp = null;

		public DicomServer(BaseActor parentActor, ActorNameEnum actorName) : base(parentActor, actorName) {}

		public virtual void ApplyConfig(DicomConfig config)
		{
			if (_scp != null)
			{
				_scp.Initialize(ParentActor.DvtThreadManager);
				_scp.Options.Identifier = String.Format("{0:000}_To_{1}_From_{2}", 
					config.SessionId,  ActorNames.Name(ParentActor.ActorName), ActorNames.Name(ActorName));

				if (config.ResultsRootDirectory.Length != 0)
				{
					_scp.Options.StartAndEndResultsGathering = true;
					_scp.Options.ResultsFilePerAssociation = true;
					_scp.Options.ResultsFileName = String.Format("{0:000}_To_{1}_From_{2}_res.xml", 
						config.SessionId,  ActorNames.Name(ParentActor.ActorName), ActorNames.Name(ActorName));
					_scp.Options.ResultsDirectory = config.ResultsRootDirectory;
				}
				else
				{
					_scp.Options.StartAndEndResultsGathering = false;
				}

				_scp.Options.DvtAeTitle = config.DvtAeTitle;
				_scp.Options.DvtPort = config.DvtPortNumber;

				_scp.Options.SutAeTitle = config.SutAeTitle;
				_scp.Options.SutPort = config.SutPortNumber;
				_scp.Options.SutIpAddress = config.SutIpAddress;

				_scp.Options.DataDirectory = config.DataDirectory;
				if (config.StoreData == true)
				{
					_scp.Options.StorageMode = Dvtk.Sessions.StorageMode.AsMedia;
				}
				else
				{
					_scp.Options.StorageMode = Dvtk.Sessions.StorageMode.NoStorage;
				}

				foreach (System.String filename in config.DefinitionFiles)
				{
					_scp.Options.LoadDefinitionFile(filename);
				}
			}
		}

		protected SCP Scp
		{
			set
			{
				_scp = value;
			}
			get
			{
				return _scp;
			}
		}

		public void StartServer()
		{		
			Console.WriteLine("{0}: DICOM SCP from {1} - started on port: {2}", 
								ActorNames.Name(ParentActor.ActorName),
								ActorNames.Name(ActorName), 
								_scp.Options.DvtPort);
			SubscribeEvent();

			int delay = 0;
			if (_scp != null)
			{
				_scp.Start(delay);
			}
		}

		public void StopServer()
		{
			UnSubscribeEvent();

			if (_scp != null)
			{
				_scp.Stop();		
			}
		}

		private void SubscribeEvent()
		{
			if (_scp != null)
			{
				_scp.AssociationReleasedEvent += new SCP.AssociationReleasedEventHandler(AssociationReleasedEventHandler);
			}
		}

		private void UnSubscribeEvent()
		{
			if (_scp != null)
			{
				_scp.AssociationReleasedEvent -= new SCP.AssociationReleasedEventHandler(AssociationReleasedEventHandler);
			}
		}

		public virtual void AssociationReleasedEventHandler(DicomThread dicomThread)
		{
			DicomTransaction transaction = new DicomTransaction(TransactionNameEnum.RAD_10, TransactionDirectionEnum.TransactionReceived);

			foreach (DicomMessage dicomMessage in dicomThread.DataWarehouse.Messages(dicomThread).DicomMessages)
			{
				transaction.DicomMessages.Add(dicomMessage);
			}

			// publish the transaction event to any interested parties
			PublishEvent(ActorName, transaction);

			// get the next transaction number - needed to sort the
			// transactions correctly
			int transactionNumber = TransactionNumber.GetNextTransactionNumber();

			// save the transaction
			ActorsTransaction actorsTransaction = new ActorsTransaction(transactionNumber,
																		ParentActor.ActorName,
																		ActorName,
																		transaction, 
																		dicomThread.ResultsFileName,
																		dicomThread.NrOfErrors,
																		dicomThread.NrOfWarnings);
			ParentActor.ActorsTransactionLog.Add(actorsTransaction);

			dicomThread.DataWarehouse.ClearMessages(dicomThread);
		}
	}
}
