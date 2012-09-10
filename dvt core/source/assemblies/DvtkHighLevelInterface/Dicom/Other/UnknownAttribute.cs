using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// An object of this class represents an unknown attribute.
	/// </summary>
	internal class UnknownAttribute: Attribute
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/') of this attribue.</param>
		/// <param name="dvtkDataAttribute">The encapsulated DvtkData attribute</param>
		public UnknownAttribute(String tagSequence, DvtkData.Dimse.Attribute dvtkDataAttribute): base(tagSequence, dvtkDataAttribute)
		{
		}







		// DvtkData.Dimse.Unknown theUnknown = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.Unknown;
		// valueAsString = theUnknown.ByteArray.ToString();


	}
}
