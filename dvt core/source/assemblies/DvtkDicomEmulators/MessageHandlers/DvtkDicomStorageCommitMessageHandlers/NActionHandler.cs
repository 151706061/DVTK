// Part of DvtkDicomStorageCommitMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.DvtkDicomEmulators.StorageCommitMessageHandlers
{
	/// <summary>
	/// Summary description for NActionHandler.
	/// </summary>
	public class NActionHandler : MessageHandler
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public NActionHandler() {}

		/// <summary>
		/// Overridden N-ACTION-RQ message handler.
		/// </summary>
		/// <param name="queryMessage">N-ACTION-RQ and Dataset.</param>
		/// <returns>Boolean - true if dicomMessage handled here.</returns>
		public override bool HandleNActionRequest(DicomMessage dicomMessage)
		{
			// Validate the received message
			System.String iodName = DicomThread.GetIodNameFromDefinition(dicomMessage);
			DicomThread.Validate(dicomMessage, iodName);

			// set up the default N-ACTION-RSP with a successful status
			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.NACTIONRSP);
			responseMessage.Set("0x00000900", DvtkData.Dimse.VR.US, 0);

			// send the response
			this.Send(responseMessage);

			// message handled
			return true;
		}
	}
}
