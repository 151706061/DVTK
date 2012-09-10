// Part of DvtkCommonDataFormat.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.CommonDataFormat
{
	/// <summary>
	/// Summary description for CommonDateFormat.
	/// </summary>
	public class CommonDateFormat : BaseCommonDataFormat
	{
		private int _year = -1;
		private int _month = -1;
		private int _day = -1;

		/// <summary>
		/// Class constructor.
		/// </summary>
		public CommonDateFormat() {}

		#region base class overrides
		/// <summary>
		/// Convert from Common Data Format to DICOM format.
		/// </summary>
		/// <returns>DICOM format.</returns>
		public override System.String ToDicomFormat()
		{
			System.String dicomDate = System.String.Empty;

			// Check on parts of the date that are defined
			// - we will explicitly not check for a valid date.
			if ((_year == -1) &&
				(_month == -1) &&
				(_day == -1))
			{
				// return some initial date
				dicomDate = "19000101";
			}
			else if ((_month == -1) &&
				(_day == -1))
			{
				// Format is YYYY
				dicomDate = String.Format("{0:0000}", _year);
			}
			else if (_day == -1)
			{
				// Format is YYYYMM
				dicomDate = String.Format("{0:0000}{1:00}", _year, _month);
			}
			else
			{
				// Format is YYYYMMDD
				dicomDate = String.Format("{0:0000}{1:00}{2:00}", _year, _month, _day);
			}

			return dicomDate;
		}

		/// <summary>
		/// Convert from DICOM format to Common Data Format.
		/// </summary>
		/// <param name="dicomFormat">DICOM format.</param>
		public override void FromDicomFormat(System.String dicomFormat)
		{
			// Get the year
			if (dicomFormat.Length >= 4)
			{
				_year = int.Parse(dicomFormat.Substring(0, 4));
				_month = -1;
				_day = -1;
			}

			// Get the month
			if (dicomFormat.Length >= 6)
			{
				_month = int.Parse(dicomFormat.Substring(4, 2));
				_day = -1;
			}

			// Get the day
			if (dicomFormat.Length >= 8)
			{
				_day = int.Parse(dicomFormat.Substring(6, 2));
			}
		}

		/// <summary>
		/// Convert from Common Data Format to HL7 format.
		/// </summary>
		/// <returns>HL7 format.</returns>
		public override System.String ToHl7Format()
		{
			System.String hl7Date = System.String.Empty;

			return hl7Date;
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
			if (obj is CommonDateFormat)
			{
				CommonDateFormat thatCommonDate = (CommonDateFormat) obj;
				if ((this.Year == thatCommonDate.Year) &&
					(this.Month == thatCommonDate.Month) &&
					(this.Day == thatCommonDate.Day))
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
			Console.WriteLine("CommonDateFormat...");
			Console.WriteLine("Year: \"{0}\"", _year);
			Console.WriteLine("Month: \"{0}\"", _month);
			Console.WriteLine("Day: \"{0}\"", _day);
			Console.WriteLine("DicomDateFormat...");
			Console.WriteLine("Date: \"{0}\"", this.ToDicomFormat());
			Console.WriteLine("Hl7DateFormat...");
			Console.WriteLine("Date: \"{0}\"", this.ToHl7Format());
		}	
		#endregion

		#region properties
		/// <summary>
		/// Year Property
		/// </summary>
		public int Year
		{
			set
			{
				_year = value;
			}
			get
			{
				return _year;
			}
		}

		/// <summary>
		/// Month Property
		/// </summary>
		public int Month
		{
			set
			{
				_month = value;
			}
			get
			{
				return _month;
			}
		}

		/// <summary>
		/// Day Property
		/// </summary>
		public int Day
		{
			set
			{
				_day = value;
			}
			get
			{
				return _day;
			}
		}
		#endregion
	}
}
