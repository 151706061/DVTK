using System;
using Dvtk.Dicom.InformationEntity;
using Dvtk.Dicom.InformationEntity.Worklist;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface.InformationEntity
{
	/// <summary>
	/// Summary description for ModalityWorklistInformationModel.
	/// Provides a wrapper class around the Dvtk.Dicom.InformationEntity.Worklist.ModalityWorklistInformationModel class.
	/// </summary>
	public class ModalityWorklistInformationModel : BaseInformationModel
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public ModalityWorklistInformationModel() : base(new Dvtk.Dicom.InformationEntity.Worklist.ModalityWorklistInformationModel()) {}
	}
}
