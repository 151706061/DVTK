using System;
using Dvtk.Dicom.InformationEntity;
using Dvtk.Dicom.InformationEntity.QueryRetrieve;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface.InformationEntity
{
	/// <summary>
	/// Summary description for QueryRetrievePatientRootInformationModel.
	/// Provides a wrapper class around the Dvtk.Dicom.InformationEntity.QueryRetrieve.PatientRootInformationModel class.
	/// </summary>
	public class QueryRetrievePatientRootInformationModel : BaseInformationModel, ICommitInformationModel, IRetrieveInformationModel
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public QueryRetrievePatientRootInformationModel() : base(new PatientRootInformationModel()) {}

		#region ICommitInformationModel
		/// <summary>
		/// Check if the given instance is present in the Information Model. The instance will be at the leaf nodes of the Information Model.
		/// </summary>
		/// <param name="sopClassUid">SOP Class UID to search for.</param>
		/// <param name="sopInstanceUid">SOP Instance UID to search for.</param>
		/// <returns>Boolean - true if instance found in the Information Model, otherwise false.</returns>
		public bool IsInstanceInInformationModel(System.String sopClassUid, System.String sopInstanceUid)
		{
			PatientRootInformationModel root = (PatientRootInformationModel)Root;
			return root.IsInstanceInInformationModel(sopClassUid, sopInstanceUid);
		}
		#endregion

		#region IRetrieveInformationModel
		/// <summary>
		/// Retrieve data from the Information Model using the given retrieve message.
		/// </summary>
		/// <param name="retrieveMessage">Message used to retrieve the Information Model.</param>
		/// <returns>File list - containing the filenames of all instances matching the retrieve dataset attributes.</returns>
		public DvtkData.Collections.StringCollection RetrieveInformationModel(DicomMessage retrieveMessage)
		{
			PatientRootInformationModel root = (PatientRootInformationModel)Root;
			return root.RetrieveInformationModel(retrieveMessage.DataSet.DvtkDataDataSet);
		}
		#endregion
	}
}
