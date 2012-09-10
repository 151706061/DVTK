// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for PrinterDevice.
	/// </summary>
	public class PrinterDevice : IBaseDevice
	{
		private ActorsTransactionLog _actorsTransactionLog = null;
		private PrintServerActor _printServer = null;

		public PrinterDevice()
		{
			_actorsTransactionLog = new ActorsTransactionLog();
			_printServer = new PrintServerActor(_actorsTransactionLog);
		}

		public void ConfigDevice(System.Collections.ArrayList configArray)
		{
			// config array will contain
			// - print server config
			ActorConfig localConfig = new ActorConfig(ActorNameEnum.PrintServer);
//			localConfig.Add(configArray[0]);
			_printServer.ConfigActor(localConfig);
		}

		public void StartDevice()
		{
			_printServer.StartActor();
		}

		public void StopDevice()
		{
			_printServer.StopActor();
		}
	}
}
