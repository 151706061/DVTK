// Part of DvtkCommonDataFormat.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.CommonDataFormat
{
	/// <summary>
	/// Summary description for CommonIdFormat.
	/// </summary>
	public class CommonIdFormat : BaseCommonDataFormat
	{
		private System.String _id = System.String.Empty;

		/// <summary>
		/// Class constructor.
		/// </summary>
		public CommonIdFormat() {}

		#region base class overrides
		/// <summary>
		/// Convert from Common Data Format to DICOM format.
		/// </summary>
		/// <returns>DICOM format.</returns>
		public override System.String ToDicomFormat()
		{
			return _id;
		}

		/// <summary>
		/// Convert from DICOM format to Common Data Format.
		/// </summary>
		/// <param name="dicomFormat">DICOM format.</param>
		public override void FromDicomFormat(System.String dicomFormat)
		{
			_id = dicomFormat.Trim();
		}

		/// <summary>
		/// Convert from Common Data Format to HL7 format.
		/// </summary>
		/// <returns>HL7 format.</returns>
		public override System.String ToHl7Format()
		{
			return _id;
		}

		/// <summary>
		/// Convert from HL7 format to Common Data Format.
		/// </summary>
		/// <param name="hl7Format">HL7 format.</param>
		public override void FromHl7Format(System.String hl7Format)
		{
			_id = hl7Format.Trim();
		}

		/// <summary>
		/// Check if the objects are equal.
		/// </summary>
		/// <param name="obj">Comparison object.</param>
		/// <returns>bool indicating true = equal or false  = not equal.</returns>
		public override bool Equals(object obj)
		{
			bool equals = false;
			if (obj is CommonIdFormat)
			{
				CommonIdFormat thatCommonId = (CommonIdFormat) obj;
				if (this.Id == thatCommonId.Id)
					equals = true;
			}
			return equals;
		}

		/// <summary>
		/// Get HashCode.
		/// </summary>
		/// <returns>Base HashCode value.</returns>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Console Display the Common Data Format content - for debugging purposes.
		/// </summary>
		public override void ConsoleDisplay()
		{
			Console.WriteLine("CommonIdFormat...");
			Console.WriteLine("Id: \"{0}\"", _id);
			Console.WriteLine("DicomIdFormat...");
			Console.WriteLine("Id: \"{0}\"", this.ToDicomFormat());
			Console.WriteLine("Hl7IdFormat...");
			Console.WriteLine("Id: \"{0}\"", this.ToHl7Format());
		}
		#endregion

		#region properties
		/// <summary>
		/// Id Property
		/// </summary>
		public System.String Id
		{
			set
			{
				_id = value.Trim();
			}
			get
			{
				return _id;
			}
		}
		#endregion
	}
}
