// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using System.IO;

namespace Dvtk.Dicom.InformationEntity
{
	/// <summary>
	/// Summary description for IRetrieveInformationModel.
	/// </summary>
	public interface IRetrieveInformationModel
	{
		/// <summary>
		/// Retrieve a list of filenames from the Information Model. The filenames match the
		/// individual instances matching the retrieve dataset attributes.
		/// </summary>
		/// <param name="retrieveDataset">Retrive dataset.</param>
		/// <returns>File list - containing the filenames of all instances matching the retrieve dataset attributes.</returns>
		DvtkData.Collections.StringCollection RetrieveInformationModel(DataSet retrieveDataset);
	}
}
