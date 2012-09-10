// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for OrderPlacerActor.
	/// </summary>
	public class OrderPlacerActor : BaseActor
	{
		public OrderPlacerActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.OrderPlacer, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for receiving Patient Registration [RAD-1]
			// for receiving Patient Update [RAD-12]
			AddHl7Server(ActorNameEnum.AdtPatientRegistration, actorConfig);

			// for receiving Filler Order Management [RAD-3]
			// for receiving Appointment Notification [RAD-48]
			AddHl7Server(ActorNameEnum.DssOrderFiller, actorConfig);

			// for sending Placer Order Management [RAD-2]
			AddHl7Client(ActorNameEnum.DssOrderFiller, actorConfig);
		}

		public override void HandleTransactionFrom(ActorNameEnum actorName, Hl7Transaction hl7Transaction)
		{
			switch (actorName)
			{
				case ActorNameEnum.AdtPatientRegistration:
					// received Patient Registration [RAD-1] or
					// received Patient Update [RAD-12]
					break;
				case ActorNameEnum.DssOrderFiller:
					// received Filler Order Management [RAD-3] or
					// received Appointment Notification [RAD-48]
					break;
				default:
					break;
			}
		}
	}
}
