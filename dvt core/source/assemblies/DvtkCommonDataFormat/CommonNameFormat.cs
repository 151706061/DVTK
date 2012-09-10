// Part of DvtkCommonDataFormat.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.CommonDataFormat
{
	/// <summary>
	/// Summary description for CommonNameFormat.
	/// </summary>
	public class CommonNameFormat : BaseCommonDataFormat
	{
		private System.String _surname = System.String.Empty;
		private System.String _firstName = System.String.Empty;
		private System.String _middleName = System.String.Empty;
		private System.String _prefix = System.String.Empty;
		private System.String _suffix = System.String.Empty;

		/// <summary>
		/// Class constructor.
		/// </summary>
		public CommonNameFormat() {}

		#region base class overrides
		/// <summary>
		/// Convert from Common Data Format to DICOM format.
		/// </summary>
		/// <returns>DICOM format.</returns>
		public override System.String ToDicomFormat()
		{
			// format the name using all the components
			System.String nameComponents = _surname + "^" +
									_firstName + "^" +
									_middleName + "^" +
									_prefix + "^" +
									_suffix;

			// trim off any trailing component delimiters
			System.String dicomName = nameComponents.TrimEnd('^');

			return dicomName;
		}

		/// <summary>
		/// Convert from DICOM format to Common Data Format.
		/// </summary>
		/// <param name="dicomFormat">DICOM format.</param>
		public override void FromDicomFormat(System.String dicomFormat)
		{
			// remove any trailing spaces from the name
			dicomFormat = dicomFormat.TrimEnd(' ');

			// split the incoming dicom format into the three component groups
			System.String[] nameComponentGroups = new System.String[3];
			nameComponentGroups = dicomFormat.Split('=');

			// split the first component group into the five components
			if (nameComponentGroups.Length > 0)
			{
				System.String[] nameComponents = new System.String[5];
				nameComponents = nameComponentGroups[0].Split('^');

				// save the individual components
				switch(nameComponents.Length)
				{
					case 1:
						_surname = nameComponents[0];
						break;
					case 2:
						_surname = nameComponents[0];
						_firstName = nameComponents[1];
						break;
					case 3:
						_surname = nameComponents[0];
						_firstName = nameComponents[1];
						_middleName = nameComponents[2];
						break;
					case 4:
						_surname = nameComponents[0];
						_firstName = nameComponents[1];
						_middleName = nameComponents[2];
						_prefix = nameComponents[3];
						break;
					case 5:
						_surname = nameComponents[0];
						_firstName = nameComponents[1];
						_middleName = nameComponents[2];
						_prefix = nameComponents[3];
						_suffix = nameComponents[4];
						break;
					default:
						break;
				}
			}
		}

		/// <summary>
		/// Convert from Common Data Format to HL7 format.
		/// </summary>
		/// <returns>HL7 format.</returns>
		public override System.String ToHl7Format()
		{
			System.String hl7Name = System.String.Empty;

			return hl7Name;
		}

		/// <summary>
		/// Convert from HL7 format to Common Data Format.
		/// </summary>
		/// <param name="hl7Format">HL7 format.</param>
		public override void FromHl7Format(System.String hl7Format)
		{
		}

		/// <summary>
		/// Check if the objects are equal.
		/// </summary>
		/// <param name="obj">Comparison object.</param>
		/// <returns>bool indicating true = equal or false  = not equal.</returns>
		public override bool Equals(object obj)
		{
			bool equals = false;
			if (obj is CommonNameFormat)
			{
				CommonNameFormat thatCommonName = (CommonNameFormat) obj;
				if ((this.Surname == thatCommonName.Surname) &&
					(this.FirstName == thatCommonName.FirstName) &&
					(this.MiddleName == thatCommonName.MiddleName) &&
					(this.Prefix == thatCommonName.Prefix) &&
					(this.Suffix == thatCommonName.Suffix))
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
			Console.WriteLine("CommonNameFormat...");
			Console.WriteLine("Surname: \"{0}\"", _surname);
			Console.WriteLine("FirstName: \"{0}\"", _firstName);
			Console.WriteLine("MiddleName: \"{0}\"", _middleName);
			Console.WriteLine("Prefix: \"{0}\"", _prefix);
			Console.WriteLine("Suffix: \"{0}\"", _suffix);
			Console.WriteLine("DicomNameFormat...");
			Console.WriteLine("PersonName: \"{0}\"", this.ToDicomFormat());
			Console.WriteLine("Hl7NameFormat...");
			Console.WriteLine("PersonName: \"{0}\"", this.ToHl7Format());
		}
		#endregion

		#region properties
		/// <summary>
		/// Surname Property
		/// </summary>
		public System.String Surname
		{
			set
			{
				_surname = value.Trim();
			}
			get
			{
				return _surname;
			}
		}

		/// <summary>
		/// FirstName Property
		/// </summary>
		public System.String FirstName
		{
			set
			{
				_firstName = value.Trim();
			}
			get
			{
				return _firstName;
			}
		}

		/// <summary>
		/// MiddleName Property
		/// </summary>
		public System.String MiddleName
		{
			set
			{
				_middleName = value.Trim();
			}
			get
			{
				return _middleName;
			}
		}

		/// <summary>
		/// Prefix Property
		/// </summary>
		public System.String Prefix
		{
			set
			{
				_prefix = value.Trim();
			}
			get
			{
				return _prefix;
			}
		}

		/// <summary>
		/// Suffix Property
		/// </summary>
		public System.String Suffix
		{
			set
			{
				_suffix = value.Trim();
			}
			get
			{
				return _suffix;
			}
		}
		#endregion
	}
}
