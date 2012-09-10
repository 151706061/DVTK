// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for PacsDevice.
	/// </summary>
	public class PacsDevice : IBaseDevice
	{
		private ActorsTransactionLog _actorsTransactionLog = null;
		private ImageManagerActor _imageManager = null;
		private ImageArchiveActor _imageArchive = null;

		public PacsDevice()
		{
			_actorsTransactionLog = new ActorsTransactionLog();
			_imageManager = new ImageManagerActor(_actorsTransactionLog);
			_imageArchive = new ImageArchiveActor(_actorsTransactionLog);
		}

		public void ConfigDevice(System.Collections.ArrayList configArray)
		{
			// config array will contain
			// - image manager config
			ActorConfig localConfig = new ActorConfig(ActorNameEnum.ImageManager);
//			localConfig.Add(configArray[0]);
			_imageManager.ConfigActor(localConfig);

			// - image archive (storage) config
			// - image archive (query/retrieve) config
			localConfig.RemoveAt(0);
//			localConfig.Add(configArray[1]);
//			localConfig.Add(configArray[2]);
			_imageArchive.ConfigActor(localConfig);
		}

		public void StartDevice()
		{
			_imageManager.StartActor();
			_imageArchive.StartActor();
		}

		public void StopDevice()
		{
			_imageManager.StopActor();
			_imageArchive.StopActor();
		}
	}
}
