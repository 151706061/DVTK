// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for IBaseDevice.
	/// </summary>
	public interface IBaseDevice
	{
		void ConfigDevice(System.Collections.ArrayList configArray);

		void StartDevice();

		void StopDevice();
	}
}
