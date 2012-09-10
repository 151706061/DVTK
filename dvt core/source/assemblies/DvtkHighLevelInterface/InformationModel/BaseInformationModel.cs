using System;
using Dvtk.Dicom.InformationEntity;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for BaseInformationModel.
	/// </summary>
	public abstract class BaseInformationModel
	{
		private Dvtk.Dicom.InformationEntity.BaseInformationModel _root = null;

		public BaseInformationModel(Dvtk.Dicom.InformationEntity.BaseInformationModel root)
		{
			_root = root;
		}

		protected Dvtk.Dicom.InformationEntity.BaseInformationModel Root
		{
			get
			{
				return _root;
			}
		}

		/// <summary>
		/// Loads the Information Model with the appropriate data from .DCM and .RAW
		/// files found in the given dataDirectory.
		/// </summary>
		/// <param name="dataDirectory">Location of the .DCM and .RAW files used to populate the Information Model.</param>
		public void LoadInformationModel(System.String dataDirectory)
		{
			_root.LoadInformationModel(dataDirectory);
		}

		/// <summary>
		/// Refresh the Information Model contents.
		/// </summary>
		public void RefreshInformationModel()
		{
			_root.RefreshInformationModel();
		}

		/// <summary>
		/// Add data to the Information Model from the given dataset.
		/// </summary>
		/// <param name="dataset">Dataset used to populate the Information Model.</param>
		public void AddToInformationModel(DataSet dataset)
		{
			_root.AddToInformationModel(dataset.DvtkDataDataSet);
		}

		/// <summary>
		/// Copy the default dataset attributes to the Information Entities in the Information
		/// Models that define them. Do not overrule any attribute with the same tag as the default
		/// attribute that may already be in the Information Entity.
		/// </summary>
		/// <param name="parameters">Default attribute - tag, vr, value(s).</param>
		public void AddDefaultAttributeToInformationModel(params Object[] parameters)
		{
			// need DicomMessage to be able to set the attribute in the dataset
			DicomMessage dicomMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.UNDEFINED);
			dicomMessage.Set(parameters);
			_root.AddDefaultAttributesToInformationModel(dicomMessage.DataSet.DvtkDataDataSet);
		}

		/// <summary>
		/// Add the attributes in this additional dataset to all Information Entities in the Information
		/// Models. Do not overrule any attribute with the same tag as the additional attribute that may
		/// already be in the Information Entity.
		/// </summary>
		/// <param name="parameters">Additional attribute - tag, vr, value(s).</param>
		public void AddAdditionalAttributeToInformationModel(params Object[] parameters)
		{
			// need DicomMessage to be able to set the attribute in the dataset
			DicomMessage dicomMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.UNDEFINED);
			dicomMessage.Set(parameters);
			_root.AddAdditionalAttributesToInformationModel(dicomMessage.DataSet.DvtkDataDataSet);
		}

		/// <summary>
		/// Query the Information Model using the given query message.
		/// </summary>
		/// <param name="queryMessage">Message used to query the Information Model.</param>
		/// <returns>DicomMessageCollection - containing the query responses. The final query response (without a dataset) is also included.</returns>
		public DicomMessageCollection QueryInformationModel(DicomMessage queryMessage)
		{
			DicomMessageCollection responseMessages = new DicomMessageCollection();

			DvtkHighLevelInterface.Values values = queryMessage.CommandSet.GetAttributeValues("0x00000002");
			System.String sopClassUid = values.GetString(1);

			DataSet queryDataset = queryMessage.DataSet;
			DataSetCollection queryResponses = _root.QueryInformationModel(queryDataset.DvtkDataDataSet);

			DvtkData.Dimse.DicomMessage dvtkDicomMessage = null;
			DvtkData.Dimse.CommandSet dvtkCommand = null;
			DicomMessage responseMessage = null;

			foreach (DvtkData.Dimse.DataSet dvtkDataset in queryResponses)
			{
				dvtkDicomMessage = new DvtkData.Dimse.DicomMessage();
				dvtkCommand = new DvtkData.Dimse.CommandSet(DvtkData.Dimse.DimseCommand.CFINDRSP);
				dvtkCommand.AddAttribute(0x0000, 0x0002, DvtkData.Dimse.VR.UI, sopClassUid);
				dvtkCommand.AddAttribute(0x0000, 0x0900, DvtkData.Dimse.VR.US, 0xFF00);

				dvtkDicomMessage.Apply(dvtkCommand, dvtkDataset);
				responseMessage = new DicomMessage(dvtkDicomMessage);
				responseMessages.Add(responseMessage);
			}

			dvtkDicomMessage = new DvtkData.Dimse.DicomMessage();
			dvtkCommand = new DvtkData.Dimse.CommandSet(DvtkData.Dimse.DimseCommand.CFINDRSP);
			dvtkCommand.AddAttribute(0x0000, 0x0002, DvtkData.Dimse.VR.UI, sopClassUid);
			dvtkCommand.AddAttribute(0x0000, 0x0900, DvtkData.Dimse.VR.US, 0x0000);

			dvtkDicomMessage.Apply(dvtkCommand);
			responseMessage = new DicomMessage(dvtkDicomMessage);
			responseMessages.Add(responseMessage);

			return responseMessages;
		}
	}
}
