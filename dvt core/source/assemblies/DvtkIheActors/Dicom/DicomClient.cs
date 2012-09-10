// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for DicomClient.
	/// </summary>
	public abstract class DicomClient : BaseClient, IClient
	{

		protected PresentationContext[] _presentationContexts = null;
		private DicomScu _scu = null;

		public DicomClient(BaseActor parentActor, ActorNameEnum actorName) : base(parentActor, actorName) 
		{
			_scu = new DicomScu(this);
		}

		public void ApplyConfig(DicomConfig config)
		{
			_scu.Initialize(ParentActor.DvtThreadManager);
			_scu.Options.Identifier = String.Format("{0:000}_From_{1}_To_{2}", 
				config.SessionId, ActorNames.Name(ParentActor.ActorName), ActorNames.Name(ActorName));

			if (config.ResultsRootDirectory.Length != 0)
			{
				_scu.Options.StartAndEndResultsGathering = true;
				_scu.Options.ResultsFilePerAssociation = true;
				_scu.Options.ResultsFileName = String.Format("{0:000}_From_{1}_To_{2}_res.xml", 
					config.SessionId, ActorNames.Name(ParentActor.ActorName), ActorNames.Name(ActorName));
				_scu.Options.ResultsDirectory = config.ResultsRootDirectory;
			}
			else
			{
				_scu.Options.StartAndEndResultsGathering = false;
			}

			_scu.Options.DvtAeTitle = config.DvtAeTitle;
			_scu.Options.DvtPort = config.DvtPortNumber;

			_scu.Options.SutAeTitle = config.SutAeTitle;
			_scu.Options.SutPort = config.SutPortNumber;
			_scu.Options.SutIpAddress = config.SutIpAddress;

			_scu.Options.DataDirectory = config.DataDirectory;
			if (config.StoreData == true)
			{
				_scu.Options.StorageMode = Dvtk.Sessions.StorageMode.AsMedia;
			}
			else
			{
				_scu.Options.StorageMode = Dvtk.Sessions.StorageMode.NoStorage;
			}

			foreach (System.String filename in config.DefinitionFiles)
			{
				_scu.Options.LoadDefinitionFile(filename);
			}

			_scu.LoopDelay = 500;
		}

		public void StartClient()
		{
			_scu.Start();
		}

		public void TriggerClient(ActorNameEnum actorName, BaseTrigger trigger)
		{
			DicomTrigger dicomTrigger = (DicomTrigger) trigger;
			_scu.Trigger(dicomTrigger.Trigger, _presentationContexts);
		}

		public void StopClient()
		{
			_scu.Stop();
		}
	}

	internal class DicomScu : SCU
	{
		private DicomClient _dicomClient = null;
		private int _transactionNumber = 0;

		public DicomScu(DicomClient dicomClient) : base(false)
		{
			_dicomClient = dicomClient;
		}

		public override void BeforeProcessTrigger(Object trigger)
		{
			// get the next transaction number - needed to sort the
			// transactions correctly
			_transactionNumber = TransactionNumber.GetNextTransactionNumber();
		}

		public override void AfterProcessTrigger(Object trigger)
		{
			// save the transaction
			DicomTransaction transaction = new DicomTransaction(TransactionNameEnum.RAD_10, TransactionDirectionEnum.TransactionSent);
			foreach (DicomMessage dicomMessage in this.DataWarehouse.Messages(this).DicomMessages)
			{
				transaction.DicomMessages.Add(dicomMessage);
			}

			ActorsTransaction actorsTransaction = new ActorsTransaction(_transactionNumber,
				_dicomClient.ParentActor.ActorName, 
				_dicomClient.ActorName, 
				transaction,
				this.ResultsFileName,
				this.NrOfErrors,
				this.NrOfWarnings);
			_dicomClient.ParentActor.ActorsTransactionLog.Add(actorsTransaction);

			this.DataWarehouse.ClearMessages(this);
		}
	}
}
