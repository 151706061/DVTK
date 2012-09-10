using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// An object of this class represents a pixel data attribute.
	/// </summary>
	internal class PixelDataAttribute: Attribute
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/') of this attribue.</param>
		/// <param name="dvtkDataAttribute">The encapsulated DvtkData attribute</param>
		public PixelDataAttribute(String tagSequence, DvtkData.Dimse.Attribute dvtkDataAttribute): base(tagSequence, dvtkDataAttribute)
		{
		}
	}
}
