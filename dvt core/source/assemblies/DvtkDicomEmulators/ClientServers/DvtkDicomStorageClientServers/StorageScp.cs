// Part of DvtkDicomStorageClientServers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.UserInterfaces;
using DvtkHighLevelInterface.InformationEntity;
using Dvtk.DvtkDicomEmulators.StorageMessageHandlers;

namespace Dvtk.DvtkDicomEmulators.StorageClientServers
{
	/// <summary>
	/// Summary description for StorageScp.
	/// </summary>
	public class StorageScp : SCP
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public StorageScp()		
		{
			HliConsole output = new HliConsole();
			output.Attach(this);
		}

		/// <summary>
		/// Add the default message handlers - include the Information Models that should be used.
		/// </summary>
		/// <param name="informationModels">Query Retrieve Information Models.</param>
		public void AddDefaultMessageHandlers(QueryRetrieveInformationModels informationModels)
		{
			// add the CStoreHandler with the Information Models
			AddToBack(new CStoreHandler(informationModels));
		}
	}
}
