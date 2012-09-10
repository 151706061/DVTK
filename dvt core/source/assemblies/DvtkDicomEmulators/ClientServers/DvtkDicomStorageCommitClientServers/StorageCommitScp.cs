// Part of DvtkDicomStorageCommitClientServers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.UserInterfaces;
using Dvtk.DvtkDicomEmulators.StorageCommitMessageHandlers;

namespace Dvtk.DvtkDicomEmulators.StorageCommitClientServers
{
	/// <summary>
	/// Summary description for StorageCommitScp.
	/// </summary>
	public class StorageCommitScp : SCP
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public StorageCommitScp()
		{
			HliConsole output = new HliConsole();
			output.Attach(this);
		}

		/// <summary>
		/// Add the default message handlers - include the Information Models that should be used.
		/// </summary>
		public void AddDefaultMessageHandlers()
		{
			// add the NActionHandler
			AddToBack(new NActionHandler());
		}
	}
}
