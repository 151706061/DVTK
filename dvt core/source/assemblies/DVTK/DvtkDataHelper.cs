// Part of DVTK.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using Wrappers;
using DvtkData.Dimse;
using DvtkData.Media;

namespace Dvtk
{
	/// <summary>
	/// 
	/// </summary>
	public class DvtkDataHelper
	{
		// Don't instantiate.
		private DvtkDataHelper()
		{
		}

		/// <summary>
		/// Read the FMI from the given Media file.
		/// </summary>
		/// <param name="dataSetFileName">Media filename to read.</param>
		/// <returns>Imported FileMetaInformation.</returns>
		public static FileMetaInformation ReadFMIFromFile(string dataSetFileName)
		{
			if (dataSetFileName == null)
			{
				throw new System.ArgumentNullException("dataSetFileName");
			}

			return MDataSet.ReadFMI(dataSetFileName);
		}

        /// <summary>
        /// Read a DataSet from the given Media file.
        /// </summary>
        /// <param name="dataSetFileName">Media filename to read.</param>
        /// <returns>Imported Dataset.</returns>
		public static DataSet ReadDataSetFromFile(string dataSetFileName)
		{
			if (dataSetFileName == null)
			{
				throw new System.ArgumentNullException("dataSetFileName");
			}

			return MDataSet.ReadFile(dataSetFileName);
		}

		/// <summary>
		/// Write a dicom file object to a (persistent) Media Storage file.
		/// </summary>
		/// <param name="file">dicom file object to write</param>
		/// <param name="mediaFileName">file name to write to</param>
		/// <returns></returns>
		public static bool WriteDataSetToFile(
			DicomFile file,
			string mediaFileName)
		{
			if (file == null) throw new System.ArgumentNullException("file");
			if (mediaFileName == null) throw new System.ArgumentNullException("mediaFileName");

			return MDataSet.WriteFile(file, mediaFileName);
		}
	}
}
