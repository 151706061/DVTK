// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for RisDevice.
	/// </summary>
	public class RisDevice : IBaseDevice
	{
		private ActorsTransactionLog _actorsTransactionLog = null;
		private DssOrderFillerActor _dssOrderFiller = null;
		private PpsManagerActor _ppsManager = null;

		public RisDevice()
		{
			_actorsTransactionLog = new ActorsTransactionLog();
			_dssOrderFiller = new DssOrderFillerActor(_actorsTransactionLog);
			_ppsManager = new PpsManagerActor(_actorsTransactionLog);
		}

		public void ConfigDevice(System.Collections.ArrayList configArray)
		{
			// config array will contain
			// - dss/order filler (worklist) config
			ActorConfig localConfig = new ActorConfig(ActorNameEnum.DssOrderFiller);
//			localConfig.Add(configArray[0]);
			_dssOrderFiller.ConfigActor(localConfig);

			// - pps manager (mpps) config
			localConfig.RemoveAt(0);
//			localConfig.Add(configArray[1]);
			_ppsManager.ConfigActor(localConfig);
		}

		public void StartDevice()
		{
			_dssOrderFiller.StartActor();
			_ppsManager.StartActor();
		}

		public void StopDevice()
		{
			_dssOrderFiller.StopActor();
			_ppsManager.StopActor();
		}
	}
}
