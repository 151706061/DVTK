// Part of DvtkCommonDataFormat.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.CommonDataFormat
{
	/// <summary>
	/// Summary description for CommonStringFormat.
	/// </summary>
	public class CommonStringFormat : BaseCommonDataFormat
	{
		private System.String _string = System.String.Empty;

		/// <summary>
		/// Class constructor.
		/// </summary>
		public CommonStringFormat() {}

		#region base class overrides
		/// <summary>
		/// Convert from Common Data Format to DICOM format.
		/// </summary>
		/// <returns>DICOM format.</returns>
		public override System.String ToDicomFormat()
		{
			return _string;
		}

		/// <summary>
		/// Convert from DICOM format to Common Data Format.
		/// </summary>
		/// <param name="dicomFormat">DICOM format.</param>
		public override void FromDicomFormat(System.String dicomFormat)
		{
			_string = dicomFormat.Trim();
		}

		/// <summary>
		/// Convert from Common Data Format to HL7 format.
		/// </summary>
		/// <returns>HL7 format.</returns>
		public override System.String ToHl7Format()
		{
			return _string;
		}

		/// <summary>
		/// Convert from HL7 format to Common Data Format.
		/// </summary>
		/// <param name="hl7Format">HL7 format.</param>
		public override void FromHl7Format(System.String hl7Format)
		{
			_string = hl7Format.Trim();
		}

		/// <summary>
		/// Check if the objects are equal.
		/// </summary>
		/// <param name="obj">Comparison object.</param>
		/// <returns>bool indicating true = equal or false  = not equal.</returns>
		public override bool Equals(object obj)
		{
			bool equals = false;
			if (obj is CommonStringFormat)
			{
				CommonStringFormat thatCommonString = (CommonStringFormat) obj;
				if (this.String == thatCommonString.String)
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
			Console.WriteLine("CommonStringFormat...");
			Console.WriteLine("String: \"{0}\"", _string);
			Console.WriteLine("DicomStringFormat...");
			Console.WriteLine("String: \"{0}\"", this.ToDicomFormat());
			Console.WriteLine("Hl7StringFormat...");
			Console.WriteLine("String: \"{0}\"", this.ToHl7Format());
		}
		#endregion

		#region properties
		/// <summary>
		/// Uid Property
		/// </summary>
		public System.String String
		{
			set
			{
				_string = value.Trim();
			}
			get
			{
				return _string;
			}
		}
		#endregion
	}
}
