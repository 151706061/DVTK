using System;

namespace DvtkHighLevelInterface.InformationEntity
{
	/// <summary>
	/// Summary description for QueryRetrieveInformationModels.
	/// </summary>
	public class QueryRetrieveInformationModels
	{
		private QueryRetrievePatientRootInformationModel _patientRootInformationModel = null;
		private QueryRetrieveStudyRootInformationModel _studyRootInformationModel = null;
		private QueryRetrievePatientStudyOnlyInformationModel _patientStudyOnlyInformationModel = null;

		/// <summary>
		/// Class Constructor.
		/// </summary>
		public QueryRetrieveInformationModels()
		{
			_patientRootInformationModel = new QueryRetrievePatientRootInformationModel();
			_studyRootInformationModel = new QueryRetrieveStudyRootInformationModel();
			_patientStudyOnlyInformationModel = new QueryRetrievePatientStudyOnlyInformationModel();
		}

		/// <summary>
		/// Get Query/Retrieve Patient Root Information Model.
		/// </summary>
		public QueryRetrievePatientRootInformationModel PatientRoot
		{
			get
			{
				return _patientRootInformationModel;
			}
		}

		/// <summary>
		/// Get Query/Retrieve Study Root Information Model
		/// </summary>
		public QueryRetrieveStudyRootInformationModel StudyRoot
		{
			get
			{
				return _studyRootInformationModel;
			}
		}

		/// <summary>
		/// Get Query/Retrieve Patient/Study Only Information Model
		/// </summary>
		public QueryRetrievePatientStudyOnlyInformationModel PatientStudyOnly
		{
			get
			{
				return _patientStudyOnlyInformationModel;
			}
		}

		/// <summary>
		/// Load the Information Model from the contents of the Data Directory. Look for all .DCM and .RAW files
		/// and load them.
		/// </summary>
		/// <param name="dataDirectory">Data directory containing the .DCM and .RAW files.</param>
		public void Load(System.String dataDirectory)
		{
			// set up and load the information models
			_patientRootInformationModel.LoadInformationModel(dataDirectory);
			_studyRootInformationModel.LoadInformationModel(dataDirectory);
			_patientStudyOnlyInformationModel.LoadInformationModel(dataDirectory);
		}

		/// <summary>
		/// Refresh the Information Model contents
		/// </summary>
		public void Refresh()
		{
			// refresh the information models
			_patientRootInformationModel.RefreshInformationModel();
			_studyRootInformationModel.RefreshInformationModel();
			_patientStudyOnlyInformationModel.RefreshInformationModel();
		}

		/// <summary>
		/// Add data to the Information Model from the given dataset.
		/// </summary>
		/// <param name="dataset">Dataset used to populate the Information Model.</param>
		public void Add(DataSet dataset)
		{
			if (dataset != null)
			{
				// add the dataset details to the information models
				_patientRootInformationModel.AddToInformationModel(dataset);
				_studyRootInformationModel.AddToInformationModel(dataset);
				_patientStudyOnlyInformationModel.AddToInformationModel(dataset);
			}
		}

		/// <summary>
		/// Copy the default dataset attributes to the Information Entities in the Information
		/// Models that define them. Do not overrule any attribute with the same tag as the default
		/// attribute that may already be in the Information Entity.
		/// </summary>
		/// <param name="parameters">Default attribute - tag, vr, value(s).</param>
		public void AddDefaultAttribute(params Object[] parameters)
		{
			// add default attributes to the information models
			_patientRootInformationModel.AddDefaultAttributeToInformationModel(parameters);
			_studyRootInformationModel.AddDefaultAttributeToInformationModel(parameters);
			_patientStudyOnlyInformationModel.AddDefaultAttributeToInformationModel(parameters);
		}

		/// <summary>
		/// Add the attributes in this additional dataset to all Information Entities in the Information
		/// Models. Do not overrule any attribute with the same tag as the additional attribute that may
		/// already be in the Information Entity.
		/// </summary>
		/// <param name="parameters">Additional attribute - tag, vr, value(s).</param>
		public void AddAdditionalAttribute(params Object[] parameters)
		{
			// add additional attributes to the information models
			_patientRootInformationModel.AddAdditionalAttributeToInformationModel(parameters);
			_studyRootInformationModel.AddAdditionalAttributeToInformationModel(parameters);
			_patientStudyOnlyInformationModel.AddAdditionalAttributeToInformationModel(parameters);
		}
	}
}
