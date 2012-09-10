// Part of DvtkDicomWorklistClientServers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.InformationEntity;
using DvtkHighLevelInterface.UserInterfaces;
using Dvtk.DvtkDicomEmulators.WorklistMessageHandlers;

namespace Dvtk.DvtkDicomEmulators.WorklistClientServers
{
	/// <summary>
	/// Summary description for WorklistScp.
	/// </summary>
	public class WorklistScp : SCP
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public WorklistScp()
		{
			HliConsole output = new HliConsole();
			output.Attach(this);
		}

		/// <summary>
		/// Add the default message handlers - include the Information Model that should be used.
		/// </summary>
		/// <param name="modalityWorklistInformationModel">Modality Worklist Information Model.</param>
		public void AddDefaultMessageHandlers(ModalityWorklistInformationModel modalityWorklistInformationModel)
		{
			// add the CFindHandler with the Information Models
			AddToBack(new CFindHandler(modalityWorklistInformationModel));
		}
	}
}
