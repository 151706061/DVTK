// Part of DvtkDicomMppsMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.DvtkDicomEmulators.MppsMessageHandlers
{
	/// <summary>
	/// Summary description for CStoreHandler.
	/// </summary>
	public class NSetHandler : MessageHandler
	{
		public NSetHandler() {}

		public override bool HandleNSetRequest(DicomMessage dicomMessage)
		{
			// Try to get the IOD Name
			System.String iodName = DicomThread.GetIodNameFromDefinition(dicomMessage);

			System.String messsage = String.Format("Processed N-SET-RQ {0}", iodName);
			DicomThread.WriteInformation(messsage);

			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.NSETRSP);

			responseMessage.Set("0x00000900", DvtkData.Dimse.VR.US, 0);

			this.Send(responseMessage);

			return true;
		}
	}
}
