// Part of DvtkDicomMppsMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;


namespace Dvtk.DvtkDicomEmulators.MppsMessageHandlers
{
	/// <summary>
	/// Summary description for NActionHandler.
	/// </summary>
	public class NCreateHandler : MessageHandler
	{
		public NCreateHandler() {}

		public override bool HandleNCreateRequest(DicomMessage dicomMessage)
		{
			// Try to get the IOD Name
			System.String iodName = DicomThread.GetIodNameFromDefinition(dicomMessage);

			// Try to get the Patient Name			
			DvtkHighLevelInterface.Values attributeValues = dicomMessage.GetAttributeValues("0x00100010");
			System.String patientName = attributeValues.GetString(1);
			System.String messsage = String.Format("Processed N-CREATE-RQ {0}: \"{1}\"", iodName, patientName);
			DicomThread.WriteInformation(messsage);

			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.NCREATERSP);

			responseMessage.Set("0x00000900", DvtkData.Dimse.VR.US, 0);

			this.Send(responseMessage);
			return true;
		}
	}
}
