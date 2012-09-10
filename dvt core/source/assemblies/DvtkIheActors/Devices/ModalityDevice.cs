// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for ModalityDevice.
	/// </summary>
	public class ModalityDevice : IBaseDevice
	{
		private ActorsTransactionLog _actorsTransactionLog = null;
		private AcquisitionModalityActor _acquisitionModality = null;

		public ModalityDevice()
		{
			_actorsTransactionLog = new ActorsTransactionLog();
			_acquisitionModality = new AcquisitionModalityActor(_actorsTransactionLog);
		}

		public void ConfigDevice(System.Collections.ArrayList configArray)
		{
			// config array will contain
			// - dss/order filler config
			// - pps manager config
			// - image manager config
			// - image archive config
			ActorConfig localConfig = new ActorConfig(ActorNameEnum.DssOrderFiller);
//			localConfig.Add(configArray[0]);
//			localConfig.Add(configArray[1]);
//			localConfig.Add(configArray[2]);
//			localConfig.Add(configArray[3]);
			_acquisitionModality.ConfigActor(localConfig);
		}

		public void StartDevice()
		{
			_acquisitionModality.StartActor();
		}

		public void StopDevice()
		{
			_acquisitionModality.StopActor();
		}
	}
}
