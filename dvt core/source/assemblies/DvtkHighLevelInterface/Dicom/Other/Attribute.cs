using System;
using DvtkData.Dimse;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// This abstract base class represents an attribute.
	/// </summary>
	abstract internal class Attribute
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property DvtkDataAttribute.
		/// </summary>
		protected DvtkData.Dimse.Attribute dvtkDataAttribute = null;

		/// <summary>
		/// The tag sequence (one or more tags seperated with an '/') of this attribue.
		/// </summary>
		protected String tagSequence = "";



		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/') of this attribue.</param>
		/// <param name="attribute">
		/// The encapsulated Attribute from the DvtkData librbary.
		/// May not be null.
		/// </param>
		public Attribute(String tagSequence, DvtkData.Dimse.Attribute dvtkDataAttribute)
		{
			this.tagSequence = tagSequence;

			if (dvtkDataAttribute == null)
			{
				// Sanity check.
				DvtkHighLevelInterfaceException.Throw("Parameter may not be null.");
			}
			else
			{
				this.dvtkDataAttribute = dvtkDataAttribute;
			}
		}



		//
		// - Properties -
		//

		/// <summary>
		/// The encapsulated Attribute from the DvtkData librbary.
		/// </summary>
		public DvtkData.Dimse.Attribute DvtkDataAttribute
		{
			get
			{
				return this.dvtkDataAttribute;
			}
		}

		/// <summary>
		/// The tag as UInt32.
		/// </summary>
		public UInt32 TagAsUInt32
		{
			get
			{
				return(Convert.ToUInt32(this.dvtkDataAttribute.Tag.ElementNumber + (65536 * this.dvtkDataAttribute.Tag.GroupNumber)));
			}
		}



		//
		// - Methods -
		//




		/// <summary>
		/// Get the HLI Attribute, given a DvtkData attribute.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/') of this attribue.</param>
		/// <param name="dvtkDataAttribute">The DvtkData attribute.</param>
		/// <returns>The HLI Attribute.</returns>
		public static Attribute GetAttribute(String tagSequence, DvtkData.Dimse.Attribute dvtkDataAttribute)
		{
			Attribute attribute = null;

			switch(dvtkDataAttribute.ValueRepresentation)
			{
					// These cases handles the "simple" attributes.
				case VR.AE: // Application Entity
				case VR.AS: // Age String
				case VR.AT: // Attribute Tag
				case VR.CS: // Code String
				case VR.DA: // Date
				case VR.DS: // Decimal String
				case VR.DT: // Date Time
				case VR.FL: // Floating Point Single
				case VR.FD: // Floating Point Double
				case VR.IS: // Integer String
				case VR.LO: // Long String
				case VR.LT: // Long Text
				case VR.PN: // Person Name
				case VR.SH: // Short String
				case VR.SL: // Signed Long
				case VR.SS: // Signed Short
				case VR.ST: // Short Text
				case VR.TM: // Time
				case VR.UI: // Unique Identifier (UID)
				case VR.UL: // Unsigned Long
				case VR.US: // Unsigned Short
				case VR.UT: // Unlimited Text
					attribute = new SimpleAttribute(tagSequence, dvtkDataAttribute);
					break;

					// These cases handles the pixel data attributes.
				case VR.OB: // Other Byte String
				case VR.OF: // Other Float String
				case VR.OW: // Other Word String
					attribute = new PixelDataAttribute(tagSequence, dvtkDataAttribute);
					break;

					// This case handles the sequence attributes.
				case VR.SQ: // Sequence of Items
					attribute = new SequenceAttribute(tagSequence, dvtkDataAttribute);
					break;

					// This case handles the unknown attributes.
				case VR.UN: // Unknown
					attribute = new UnknownAttribute(tagSequence, dvtkDataAttribute);
					break;

				default: 
					attribute = null;
					break;
			}

			return attribute;
		}

		public static bool IsSimpleAttribute(VR vR)
		{	
			bool isSimpleAttribute = true;

			switch(vR)
			{
					// These cases handles the "simple" attributes.
				case VR.AE: // Application Entity
				case VR.AS: // Age String
				case VR.AT: // Attribute Tag
				case VR.CS: // Code String
				case VR.DA: // Date
				case VR.DS: // Decimal String
				case VR.DT: // Date Time
				case VR.FL: // Floating Point Single
				case VR.FD: // Floating Point Double
				case VR.IS: // Integer String
				case VR.LO: // Long String
				case VR.LT: // Long Text
				case VR.PN: // Person Name
				case VR.SH: // Short String
				case VR.SL: // Signed Long
				case VR.SS: // Signed Short
				case VR.ST: // Short Text
				case VR.TM: // Time
				case VR.UI: // Unique Identifier (UID)
				case VR.UL: // Unsigned Long
				case VR.US: // Unsigned Short
				case VR.UT: // Unlimited Text
					isSimpleAttribute = true;
						break;

				default:
					isSimpleAttribute = false;
					break;
			}

			return(isSimpleAttribute);
		}






	}
}
