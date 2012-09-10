// Part of DvtkCommonDataFormat.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.CommonDataFormat
{
	/// <summary>
	/// Summary description for CommonTimeFormat.
	/// </summary>
	public class CommonTimeFormat : BaseCommonDataFormat
	{
		private int _hour = -1;
		private int _minute = -1;
		private int _second = -1;

		/// <summary>
		/// Class constructor.
		/// </summary>
		public CommonTimeFormat() {}

		#region base class overrides
		/// <summary>
		/// Convert from Common Data Format to DICOM format.
		/// </summary>
		/// <returns>DICOM format.</returns>
		public override System.String ToDicomFormat()
		{
			System.String dicomTime = System.String.Empty;

			// Check on parts of the time that are defined
			// - we will explicitly not check for a valid time.
			if ((_hour == -1) &&
				(_minute == -1) &&
				(_second == -1))
			{
				// return some initial time
				dicomTime = "120000";
			}
			else if ((_minute == -1) &&
				(_second == -1))
			{
				// Format is HH
				dicomTime = String.Format("{0:00}", _hour);
			}
			else if (_second == -1)
			{
				// Format is HHMM
				dicomTime = String.Format("{0:00}{1:00}", _hour, _minute);
			}
			else
			{
				// Format is HHMMSS
				dicomTime = String.Format("{0:00}{1:00}{2:00}", _hour, _minute, _second);
			}

			return dicomTime;
		}

		/// <summary>
		/// Convert from DICOM format to Common Data Format.
		/// </summary>
		/// <param name="dicomFormat">DICOM format.</param>
		public override void FromDicomFormat(System.String dicomFormat)
		{
			// Get the hour
			if (dicomFormat.Length >= 2)
			{
				_hour = int.Parse(dicomFormat.Substring(0, 2));
				_minute = -1;
				_second = -1;
			}

			// Get the minute
			if (dicomFormat.Length >= 4)
			{
				_minute = int.Parse(dicomFormat.Substring(2, 2));
				_second = -1;
			}

			// Get the second
			if (dicomFormat.Length >= 6)
			{
				_second = int.Parse(dicomFormat.Substring(4, 2));
			}
		}

		/// <summary>
		/// Convert from Common Data Format to HL7 format.
		/// </summary>
		/// <returns>HL7 format.</returns>
		public override System.String ToHl7Format()
		{
			System.String hl7Time = System.String.Empty;

			return hl7Time;
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
			if (obj is CommonTimeFormat)
			{
				CommonTimeFormat thatCommonTime = (CommonTimeFormat) obj;
				if ((this.Hour == thatCommonTime.Hour) &&
					(this.Minute == thatCommonTime.Minute) &&
					(this.Second == thatCommonTime.Second))
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
			Console.WriteLine("CommonTimeFormat...");
			Console.WriteLine("Hour: \"{0}\"", _hour);
			Console.WriteLine("Minute: \"{0}\"", _minute);
			Console.WriteLine("Second: \"{0}\"", _second);
			Console.WriteLine("DicomTimeFormat...");
			Console.WriteLine("Time: \"{0}\"", this.ToDicomFormat());
			Console.WriteLine("Hl7TimeFormat...");
			Console.WriteLine("Time: \"{0}\"", this.ToHl7Format());
		}	
		#endregion

		#region properties
		/// <summary>
		/// Hour Property
		/// </summary>
		public int Hour
		{
			set
			{
				_hour = value;
			}
			get
			{
				return _hour;
			}
		}

		/// <summary>
		/// Minute Property
		/// </summary>
		public int Minute
		{
			set
			{
				_minute = value;
			}
			get
			{
				return _minute;
			}
		}

		/// <summary>
		/// Second Property
		/// </summary>
		public int Second
		{
			set
			{
				_second = value;
			}
			get
			{
				return _second;
			}
		}
		#endregion
	}
}
