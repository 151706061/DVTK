// Part of DvtkDicomMppsClientServers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using DvtkHighLevelInterface.UserInterfaces;
using Dvtk.DvtkDicomEmulators.MppsMessageHandlers;

namespace Dvtk.DvtkDicomEmulators.MppsClientServers
{
	/// <summary>
	/// Summary description for MppsScp.
	/// </summary>
	public class MppsScp : SCP
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public MppsScp()
		{
			HliConsole output = new HliConsole();
			output.Attach(this);
		}

		/// <summary>
		/// Add the default message handlers.
		/// </summary>
		public void AddDefaultMessageHandlers()
		{
			AddToBack(new NSetHandler());
			AddToBack(new NCreateHandler());
		}
	}
}
