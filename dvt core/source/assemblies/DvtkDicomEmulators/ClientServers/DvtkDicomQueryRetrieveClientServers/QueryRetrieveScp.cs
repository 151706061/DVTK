// Part of DvtkDicomQueryRetrieveClientServers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.InformationEntity;
using DvtkHighLevelInterface.UserInterfaces;
using Dvtk.DvtkDicomEmulators.QueryRetrieveMessageHandlers;

namespace Dvtk.DvtkDicomEmulators.QueryRetrieveClientServers
{
	/// <summary>
	/// Summary description for QueryRetrieveScp.
	/// </summary>
	public class QueryRetrieveScp : SCP
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public QueryRetrieveScp()		
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
			// add the CFindHandler with the Information Models
			CFindHandler cFindHandler = new CFindHandler(informationModels);
			AddToBack(cFindHandler);

			// add the CMoveHandler with the Information Models
			CMoveHandler cMoveHandler = new CMoveHandler(informationModels);
			AddToBack(cMoveHandler);

			// add the CGetHandler with the Information Models
			CGetHandler cGetHandler = new CGetHandler(informationModels);
			AddToBack(cGetHandler);
		}
	}
}
