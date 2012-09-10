// Part of DvtkDicomStorageMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using DvtkHighLevelInterface.InformationEntity;

namespace Dvtk.DvtkDicomEmulators.StorageMessageHandlers
{
	/// <summary>
	/// Summary description for CStoreHandler.
	/// </summary>
	public class CStoreHandler : MessageHandler
	{
		private QueryRetrieveInformationModels _informationModels = null;

		/// <summary>
		/// Class constructor.
		/// </summary>
		public CStoreHandler() {}

		/// <summary>
		/// Class constructor with the Information Models provided.
		/// </summary>
		/// <param name="informationModels">Query Retrieve Information Models.</param>
		public CStoreHandler(QueryRetrieveInformationModels informationModels) 
		{
			_informationModels = informationModels;
		}

		/// <summary>
		/// Overridden C-STORE-RQ message handler that makes use of the appropriate Information Model to handle the storage.
		/// </summary>
		/// <param name="queryMessage">C-STORE-RQ and Dataset.</param>
		/// <returns>Boolean - true if dicomMessage handled here.</returns>
		public override bool HandleCStoreRequest(DicomMessage dicomMessage)
		{
			// Validate the received message
			System.String iodName = DicomThread.GetIodNameFromDefinition(dicomMessage);
			DicomThread.Validate(dicomMessage, iodName);

			// update the information models
			if (_informationModels != null)
			{
				// add this dataset to the information models
				_informationModels.Add(dicomMessage.DataSet);
			}

			// set up the default C-STORE-RSP with a successful status
			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.CSTORERSP);
			responseMessage.Set("0x00000900", DvtkData.Dimse.VR.US, 0);

			// send the response
			this.Send(responseMessage);

			// message handled
			return true;
		}
	}
}
