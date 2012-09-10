// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using Dvtk.IheActors.Dicom;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for PpsManagerActor.
	/// </summary>
	public class PpsManagerActor : BaseActor
	{
		public PpsManagerActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.PerformedProcedureStepManager, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Modality Procedure Step In Progress [RAD-6]
			// for receiving Modality Procedure Step Completed [RAD-7]
			AddDicomServer(new DicomMppsServer(this, ActorNameEnum.AcquisitionModality), actorConfig);

			// for receiving Creator Procedure Step In Progress [RAD-20]
			// for receiving Creator Procedure Step Completed [RAD-21]
			AddDicomServer(new DicomMppsServer(this, ActorNameEnum.EvidenceCreator), actorConfig);

			// for sending Modality Procedure Step In Progress [RAD-6]
			// for sending Modality Procedure Step Completed [RAD-7]
			// for sending Creator Procedure Step In Progress [RAD-20]
			// for sending Creator Procedure Step Completed [RAD-21]
			AddDicomClient(new DicomMppsClient(this, ActorNameEnum.DssOrderFiller), actorConfig);

			// for sending Modality Procedure Step In Progress [RAD-6]
			// for sending Modality Procedure Step Completed [RAD-7]
			// for sending Creator Procedure Step In Progress [RAD-20]
			// for sending Creator Procedure Step Completed [RAD-21]
			AddDicomClient(new DicomMppsClient(this, ActorNameEnum.ImageManager), actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.AcquisitionModality:
					// received Modality Procedure Step In Progress [RAD-6] or
					// received Modality Procedur Step Completed [RAD-7]
				case ActorNameEnum.EvidenceCreator:
				{

					// received Creator Procedure Step In Progress [RAD-20] or
					// received Creator Procedure Step Completed [RAD-21]
					TransactionNameEnum transactionName = dicomTransaction.TransactionName;
					DicomMessage dicomMessage = (DicomMessage)dicomTransaction.DicomMessages[0];

					// make a trigger from the transaction message
					DicomTrigger dicomTrigger = new DicomTrigger(transactionName);
					dicomTrigger.Trigger = dicomMessage;

					// trigger the following actors
					TriggerActor(ActorNameEnum.DssOrderFiller, dicomTrigger);
					TriggerActor(ActorNameEnum.ImageManager, dicomTrigger);
					break;
				}
				default:
					break;
			}
		}
	}
}
