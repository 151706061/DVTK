// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using System.IO;

namespace Dvtk.Dicom.InformationEntity
{
	/// <summary>
	/// Summary description for ICommitInformationModel.
	/// </summary>
	public interface ICommitInformationModel
	{
		/// <summary>
		/// Check if the given instance is present in the Information Model. The instance will be at the leave nodes of the Information Model.
		/// </summary>
		/// <param name="sopClassUid">SOP Class UID to search for.</param>
		/// <param name="sopInstanceUid">SOP Instance UID to search for.</param>
		/// <returns>Boolean - true if instance found in the Information Model, otherwise false.</returns>
		bool IsInstanceInInformationModel(System.String sopClassUid, System.String sopInstanceUid);
	}
}
