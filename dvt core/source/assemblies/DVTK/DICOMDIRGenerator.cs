using System;
using Wrappers;

namespace Dvtk
{
	/// <summary>
	/// Summary description for DICOMDIRGenerator.
	/// </summary>
	public class DICOMDIRGenerator
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public DICOMDIRGenerator()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Create a DICOMDIR from the given DCM files.
		/// </summary>
		/// <param name="dcmFiles">DCM files to read.</param>
		/// <returns>Imported Dataset.</returns>
		public static bool WGenerateDICOMDIR(string[] dcmFiles)
		{
			if (dcmFiles == null)
			{
				throw new System.ArgumentNullException("dcmFiles");
			}

			return MDICOMDIRGenerator.WCreateDICOMDIR(dcmFiles);
		}
	}
}
