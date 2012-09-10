// Part of DvtkCommonDataFormat.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.CommonDataFormat
{
	/// <summary>
	/// Summary description for BaseCommonDataFormat.
	/// </summary>
	public abstract class BaseCommonDataFormat
	{
		/// <summary>
		/// Convert from Common Data Format to DICOM format.
		/// </summary>
		/// <returns>DICOM format.</returns>
		public abstract System.String ToDicomFormat();

		/// <summary>
		/// Convert from DICOM format to Common Data Format.
		/// </summary>
		/// <param name="dicomFormat">DICOM format.</param>
		public abstract void FromDicomFormat(System.String dicomFormat);

		/// <summary>
		/// Convert from Common Data Format to HL7 format.
		/// </summary>
		/// <returns>HL7 format.</returns>
		public abstract System.String ToHl7Format();

		/// <summary>
		/// Convert from HL7 format to Common Data Format.
		/// </summary>
		/// <param name="hl7Format">HL7 format.</param>
		public abstract void FromHl7Format(System.String hl7Format);

		/// <summary>
		/// Console Display the Common Data Format content - for debugging purposes.
		/// </summary>
		public abstract void ConsoleDisplay();
	}
}
