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
	/// Summary description for CFindHandler.
	/// </summary>
	public class CFindHandler : MessageHandler
	{
		private QueryRetrieveInformationModels _informationModels = null;

		/// <summary>
		/// Class Constructor
		/// </summary>
		public CFindHandler() {}

		/// <summary>
		/// Class constructor with the Information Models provided.
		/// </summary>
		/// <param name="informationModels">Query Retrieve Information Models.</param>
		public CFindHandler(QueryRetrieveInformationModels informationModels) 
		{
			_informationModels = informationModels;
		}

		/// <summary>
		/// Overridden C-FIND-RQ message handler that makes use of the appropriate Information Model to handle the query.
		/// </summary>
		/// <param name="queryMessage">C-FIND-RQ Identifier (Dataset) containing query attributes.</param>
		/// <returns>Boolean - true if dicomMessage handled here.</returns>
		public override bool HandleCFindRequest(DicomMessage queryMessage)
		{
			if (_informationModels == null) return false;

			// Refresh the Information Models
			_informationModels.Refresh();

			// Validate the received message
			System.String iodName = DicomThread.GetIodNameFromDefinition(queryMessage);
			DicomThread.Validate(queryMessage, iodName);

			// try to get the SOP Class Uid so that we know which Information Model to use.
			DvtkHighLevelInterface.Values values = queryMessage.CommandSet.GetAttributeValues("0x00000002");
			System.String sopClassUid = values.GetString(1);
			DvtkData.Dul.AbstractSyntax abstractSyntax = new DvtkData.Dul.AbstractSyntax(sopClassUid);

			// check if we should use the Patient Root Information Model
			if ((abstractSyntax.UID == DvtkData.Dul.AbstractSyntax.Patient_Root_Query_Retrieve_Information_Model_FIND.UID) &&
				(_informationModels.PatientRoot != null))
			{
				DicomMessageCollection responseMessages = _informationModels.PatientRoot.QueryInformationModel(queryMessage);

				foreach (DicomMessage responseMessage in responseMessages)
				{
					this.Send(responseMessage);
				}
			}
			// check if we should use the Study Root Information Model 
			else if ((abstractSyntax.UID == DvtkData.Dul.AbstractSyntax.Study_Root_Query_Retrieve_Information_Model_FIND.UID) &&
				(_informationModels.StudyRoot != null))
			{
				DicomMessageCollection responseMessages = _informationModels.StudyRoot.QueryInformationModel(queryMessage);

				foreach (DicomMessage responseMessage in responseMessages)
				{
					this.Send(responseMessage);
				}
			}
			// check if we should use the Patient Study Only Information Model
			else if ((abstractSyntax.UID == DvtkData.Dul.AbstractSyntax.Patient_Study_Only_Query_Retrieve_Information_Model_FIND.UID) &&
				(_informationModels.PatientStudyOnly != null))
			{
				DicomMessageCollection responseMessages = _informationModels.PatientStudyOnly.QueryInformationModel(queryMessage);

				foreach (DicomMessage responseMessage in responseMessages)
				{
					this.Send(responseMessage);
				}
			}
			else
			{
				// should never get here - but send a final CFINDRSP anyway
				DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.CFINDRSP);
				this.Send(responseMessage);
			}

			// message handled
			return true;
		}
	}
}
