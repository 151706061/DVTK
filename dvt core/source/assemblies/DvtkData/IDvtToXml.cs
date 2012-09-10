// Part of DvtkData.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;

namespace DvtkData.DvtDetailToXml
{
	/// <summary>
	/// Dvt Detail To Xml Interface. Defines a method to serialize DVT Detail Data to Xml.
	/// </summary>
	public interface IDvtDetailToXml
	{
		/// <summary>
		/// Serialize DVT Detail Data to Xml.
		/// </summary>
		/// <param name="streamWriter">Stream writer to serialize to.</param>
		/// <param name="level">Recursion level. 0 = Top.</param> 
		/// <returns>bool - success/failure</returns> 
		bool DvtDetailToXml(StreamWriter streamWriter, int level);
	}

	/// <summary>
	/// Dvt Summary To Xml Interface. Defines a method to serialize DVT Summary Data to Xml.
	/// </summary>
	public interface IDvtSummaryToXml
	{
		/// <summary>
		/// Serialize DVT Summary Data to Xml.
		/// </summary>
		/// <param name="streamWriter">Stream writer to serialize to.</param>
		/// <param name="level">Recursion level. 0 = Top.</param> 
		/// <returns>bool - success/failure</returns> 
		bool DvtSummaryToXml(StreamWriter streamWriter, int level);
	}

    /// <summary>
    /// DvtToXml class provides static conversion methods for XML output.
    /// </summary>
    public class DvtToXml
    {
        /// <summary>
        /// Convert the incoming string into an XML format by replacing some characters by their
        /// XML coding.
        /// </summary>
        /// <param name="inString">String to be converted.</param>
        /// <returns>Converted string.</returns>
        public static System.String ConvertString(System.String inString)
        {
            // convert character values so that they can be displayed in XML
            System.String outString = null;

            if (inString != null)
            {
                outString = System.String.Empty;

                for (int i = 0; i < inString.Length; i++)
                {
                    System.String valueString = String.Empty;
                    System.Int32 charValue = Convert.ToInt32(inString[i]);

					if ((charValue >= 0) && 
						(charValue < 32))
					{
						// char in range 0..31
						switch(charValue)
						{
							case 9: valueString = "&#x09;"; break;
							case 10: valueString = "[LF]"; break;
							case 12: valueString = "[FF]"; break;
							case 13: valueString = "[CR]"; break;
							case 27: valueString = "[ESC]"; break;
							default: 
							{
								System.String charValueString = charValue.ToString("X");
								while (charValueString.Length < 2)
								{
									charValueString = "0" + charValueString;
								}

								valueString = "\\" + charValueString;
								break;
							}
						}
					}
					else if ((charValue > 126) &&
						(charValue <= 255))
					{
						// char in range 127..255
						System.String charValueString = charValue.ToString("X");
						while (charValueString.Length < 2)
						{
							charValueString = "0" + charValueString;
						}

						valueString = "\\" + charValueString;
					}
					else if (charValue > 255)
					{
						// the internal compiler marshalling used to convert the strings
						// from unmanaged to managed results in UNICODE values for these characters
						// - use a simple switch statement to display the required values
						switch(charValue)
						{
							case 8364: valueString = "\\80"; break;
							case 8218: valueString = "\\82"; break;
							case 402: valueString = "\\83"; break;
							case 8222: valueString = "\\84"; break;
							case 8230: valueString = "\\85"; break;
							case 8224: valueString = "\\86"; break;
							case 8225: valueString = "\\87"; break;
							case 710: valueString = "\\88"; break;
							case 8240: valueString = "\\89"; break;
							case 352: valueString = "\\8A"; break;
							case 8249: valueString = "\\8B"; break;
							case 338: valueString = "\\8C"; break;
							case 381: valueString = "\\8E"; break;
							case 8216: valueString = "\\91"; break;
							case 8217: valueString = "\\92"; break;
							case 8220: valueString = "\\93"; break;
							case 8221: valueString = "\\94"; break;
							case 8226: valueString = "\\95"; break;
							case 8211: valueString = "\\96"; break;
							case 8212: valueString = "\\97"; break;
							case 732: valueString = "\\98"; break;
							case 8482: valueString = "\\99"; break;
							case 353: valueString = "\\9A"; break;
							case 8250: valueString = "\\9B"; break;
							case 339: valueString = "\\9C"; break;
							case 382: valueString = "\\9E"; break;
							case 376: valueString = "\\9F"; break;
							default: break;
						}
					}
					else
					{
						switch(charValue)
						{
							case 38: valueString = "&#x26;"; break; // &
							case 60: valueString = "&#x3C;"; break; // <
							case 62: valueString = "&#x3E;"; break; // >
							default:
								// char in range 32..127
								valueString = inString[i].ToString();
								break;
						}
					}

                    // add the character value to the string
                    outString += valueString;
                }
            }
            return outString;
        }
    }
}
