using System;
using DvtkData.Dimse;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// This class represents a simple attribute, e.g. not a Sequence or pixel data.
	/// </summary>
	internal class SimpleAttribute: Attribute
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor. Use it to construct a new simple attribute.
		/// </summary>
		/// <param name="tagAsUInt32">Tag as UInt32.</param>
		/// <param name="vr">The VR.</param>
		/// <param name="parameters">The values.</param>
		public SimpleAttribute(UInt32 tagAsUInt32, VR vR, params Object[] parameters): base(tagAsUInt32.ToString(), new DvtkData.Dimse.Attribute(tagAsUInt32, vR, parameters))
		{
			
		}

		/// <summary>
		/// Constructor. Use this to encapsulate an existing DvtkData simple attribute.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/') of this attribue.</param>
		/// <param name="dvtkDataAttribute">The encapsulated DvtkData attribute</param>
		public SimpleAttribute(String tagSequence, DvtkData.Dimse.Attribute dvtkDataAttribute): base(tagSequence, dvtkDataAttribute)
		{
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Get the values from this attribute.
		/// </summary>
		/// <returns>The values.</returns>
		public ValidValues GetValues()
		{
			ValidValues validValues = null;

			switch(dvtkDataAttribute.ValueRepresentation)
			{
				case VR.AE: // Application Entity
					DvtkData.Dimse.ApplicationEntity theApplicationEntity = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.ApplicationEntity;
					validValues = new ValidValues(this.tagSequence, theApplicationEntity.Values);
					break;

				case VR.AS: // Age String
					DvtkData.Dimse.AgeString theAgeString = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.AgeString;
					validValues = new ValidValues(this.tagSequence, theAgeString.Values);
					break;

				case VR.AT: // Attribute Tag
					DvtkData.Dimse.AttributeTag theAttributeTag = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.AttributeTag;
					validValues = new ValidValues(this.tagSequence, theAttributeTag.Values);
					break;

				case VR.CS: // Code String
					DvtkData.Dimse.CodeString theCodeString = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.CodeString;
					validValues = new ValidValues(this.tagSequence, theCodeString.Values);
					break;

				case VR.DA: // Date
					DvtkData.Dimse.Date theDate = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.Date;
					validValues = new ValidValues(this.tagSequence, theDate.Values);
					break;

				case VR.DS: // Decimal String
					DvtkData.Dimse.DecimalString theDecimalString = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.DecimalString;
					validValues = new ValidValues(this.tagSequence, theDecimalString.Values);
					break;

				case VR.DT: // Date Time
					DvtkData.Dimse.DateTime theDateTime = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.DateTime;
					validValues = new ValidValues(this.tagSequence, theDateTime.Values);
					break;

				case VR.FD: // Floating Point Double
					DvtkData.Dimse.FloatingPointDouble theFloatingPointDouble = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.FloatingPointDouble;
					validValues = new ValidValues(this.tagSequence, theFloatingPointDouble.Values);
					break;

				case VR.FL: // Floating Point Single
					DvtkData.Dimse.FloatingPointSingle theFloatingPointSingle = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.FloatingPointSingle;
					validValues = new ValidValues(this.tagSequence, theFloatingPointSingle.Values);
					break;

				case VR.IS: // Integer String
					DvtkData.Dimse.IntegerString theIntegerString = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.IntegerString;
					validValues = new ValidValues(this.tagSequence, theIntegerString.Values);
					break;

				case VR.LO: // Long String
					DvtkData.Dimse.LongString theLongString = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.LongString;
					validValues = new ValidValues(this.tagSequence, theLongString.Values);
					break;

				case VR.LT: // Long Text
					DvtkData.Dimse.LongText theLongText = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.LongText;
					validValues = new ValidValues(this.tagSequence, theLongText.Value);
					break;

				case VR.PN: // Person Name
					DvtkData.Dimse.PersonName thePersonName = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.PersonName;
					validValues = new ValidValues(this.tagSequence, thePersonName.Values);
					break;

				case VR.SH: // Short String
					DvtkData.Dimse.ShortString theShortString = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.ShortString;
					validValues = new ValidValues(this.tagSequence, theShortString.Values);
					break;

				case VR.SL: // Signed Long
					DvtkData.Dimse.SignedLong theSignedLong = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.SignedLong;
					validValues = new ValidValues(this.tagSequence, theSignedLong.Values);
					break;

				case VR.SS: // Signed Short
					DvtkData.Dimse.SignedShort theSignedShort = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.SignedShort;
					validValues = new ValidValues(this.tagSequence, theSignedShort.Values);
					break;

				case VR.ST: // Short Text
					DvtkData.Dimse.ShortText theShortText = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.ShortText;
					validValues = new ValidValues(this.tagSequence, theShortText.Value);
					break;

				case VR.TM: // Time
					DvtkData.Dimse.Time theTime = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.Time;
					validValues = new ValidValues(this.tagSequence, theTime.Values);
					break;

				case VR.UI: // Unique Identifier (UID)
					DvtkData.Dimse.UniqueIdentifier theUniqueIdentifier = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.UniqueIdentifier;
					validValues = new ValidValues(this.tagSequence, theUniqueIdentifier.Values);
					break;

				case VR.UL: // Unsigned Long
					DvtkData.Dimse.UnsignedLong theUnsignedLong = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.UnsignedLong;
					validValues = new ValidValues(this.tagSequence, theUnsignedLong.Values);
					break;

				case VR.US: // Unsigned Short
					DvtkData.Dimse.UnsignedShort theUnsignedShort = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.UnsignedShort;
					validValues = new ValidValues(this.tagSequence, theUnsignedShort.Values);
					break;

				case VR.UT: // Unlimited Text
					DvtkData.Dimse.UnlimitedText theUnlimitedText = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.UnlimitedText;
					validValues = new ValidValues(this.tagSequence, theUnlimitedText.Value);
					break;

				default:
					DvtkHighLevelInterfaceException.Throw("VR " + dvtkDataAttribute.ValueRepresentation.ToString() + " is not handled by class SimpleAttribute.");
					break;
			}

			return(validValues);
		}
	}
}
