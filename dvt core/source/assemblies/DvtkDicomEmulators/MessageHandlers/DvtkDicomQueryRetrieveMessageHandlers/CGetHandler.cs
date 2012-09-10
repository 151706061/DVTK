// Part of DvtkDicomQueryRetrieveMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using DvtkHighLevelInterface.InformationEntity;

namespace Dvtk.DvtkDicomEmulators.QueryRetrieveMessageHandlers
{
	/// <summary>
	/// Summary description for CGetHandler.
	/// </summary>
	public class CGetHandler : MessageHandler
	{
		private QueryRetrieveInformationModels _informationModels = null;

		public CGetHandler() {}

		public CGetHandler(QueryRetrieveInformationModels informationModels) 
		{
			_informationModels = informationModels;
		}

		public override bool HandleCGetRequest(DicomMessage dicomMessage)
		{
			// Validate the received message
			System.String iodName = DicomThread.GetIodNameFromDefinition(dicomMessage);
			DicomThread.Validate(dicomMessage, iodName);

			// Storage suboperations should be sent over the same association as the CGET.
			// The CGET SCU becomes a Storage SCP for the duration of the storage suboperations.
			// The CGET SCU should ensure that all the necessary storage SOP Classes are negotiated
			// during association establishment.

			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.CGETRSP);
			this.Send(responseMessage);
			return true;
		}
	}
}
