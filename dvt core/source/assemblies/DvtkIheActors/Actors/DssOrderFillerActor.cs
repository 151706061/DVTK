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
	/// Summary description for DssOrderFillerActor.
	/// </summary>
	public class DssOrderFillerActor : BaseActor
	{
		public DssOrderFillerActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.DssOrderFiller, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Patient Registration [RAD-1]
			// for receiving Patient Update [RAD-12]
			AddHl7Server(ActorNameEnum.AdtPatientRegistration, actorConfig);

			// for receiving Placer Order Management [RAD-2]
			AddHl7Server(ActorNameEnum.OrderPlacer, actorConfig);

			// for receiving Query Modality Worklist [RAD-5]
			AddDicomServer(new DicomWorklistServer(this, ActorNameEnum.AcquisitionModality), actorConfig);

			// for receiving Modality Procedure Step In Progress [RAD-6]
			// for receiving Modality Procedure Step Completed [RAD-7]
			// for receiving Creator Procedure Step In Progress [RAD-20]
			// for receiving Creator Procedure Step Completed [RAD-21]
			AddDicomServer(new DicomMppsServer(this, ActorNameEnum.PerformedProcedureStepManager), actorConfig);

			// for receiving Instance Availability Notification [RAD-49]
//			AddDicomServer(ActorNameEnum.ImageManager, actorConfig);

			// for sending Filler Order Management [RAD-3]
			// for sending Appointment Notification [RAD-48]
			AddHl7Client(ActorNameEnum.OrderPlacer, actorConfig);

			// for sending Procedure Scheduled [RAD-4]
			// for sending Procedure Updated [RAD-13]
			AddHl7Client(ActorNameEnum.ImageManager, actorConfig);

			// for sending Image Availability Query [RAD-11]
			// for sending Performed Work Status Update [RAD-42]
//			AddDicomClient(ActorNameEnum.ImageManager, actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, Hl7Transaction hl7Transaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.AdtPatientRegistration:
					// received Patient Registration [RAD-1] or
					// received Patient Update [RAD-12]
					break;
				case ActorNameEnum.OrderPlacer:
					// received Placer Order Management [RAD-2]
					break;
				default:
					break;
			}
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, DicomTransaction dicomTransaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.AcquisitionModality:
					// received Query Modality Worklist [RAD-5]
					break;
				case ActorNameEnum.PerformedProcedureStepManager:
					// received Modality Procedure Step In Progress [RAD-6] or
					// received Modality Procedure Step Completed [RAD-7] or
					// received Creator Procedure Step In Progress [RAD-20] or
					// received Creator Procedure Step Completed [RAD-21]
					break;
				case ActorNameEnum.ImageManager:
					// received Instance Availability Notification [RAD-49]
					break;
				default:
					break;
			}
		}
	}
}
