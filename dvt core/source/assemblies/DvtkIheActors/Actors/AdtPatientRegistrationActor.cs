// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for AdtPatientRegistrationActor.
	/// </summary>
	public class AdtPatientRegistrationActor : BaseActor
	{
		public AdtPatientRegistrationActor(ActorsTransactionLog actorsTransactionLog) 
			: base(ActorNameEnum.AdtPatientRegistration, actorsTransactionLog) {}

		protected override void ApplyConfig(ActorConfig actorConfig)
		{
			// for sending Patient Registration [RAD-1]
			// for sending Patient Update [Rad-12]
			AddHl7Client(ActorNameEnum.OrderPlacer, actorConfig);

			// for sending Patient Registration [RAD-1]
			// for sending Patient Update [Rad-12]
			AddHl7Client(ActorNameEnum.DssOrderFiller, actorConfig);
		}
	}
}
