using System;
using System.Diagnostics;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// An object of this class represents a Sequence Item.
	/// </summary>
	public class SequenceItem: AttributeSet
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor. Use it to construct a new empty sequence item.
		/// </summary>
		public SequenceItem(): base(new DvtkData.Dimse.SequenceItem())
		{

		}

		/// <summary>
		/// Constructor. Use this to encapsulate an existing DvtkData sequence item.
		/// </summary>
		/// <param name="dvtkDataSequenceItem"></param>
		internal SequenceItem(DvtkData.Dimse.SequenceItem dvtkDataSequenceItem): base(dvtkDataSequenceItem)
		{
		}



		//
		// - Properties -
		//

		/// <summary>
		/// The encapsulated DvtkData sequence item.
		/// </summary>
		internal DvtkData.Dimse.SequenceItem DvtkDataSequenceItem
		{
			get
			{
				Debug.Assert(this.dvtkDataAttributeSet is DvtkData.Dimse.SequenceItem, "Object of type DvtkData.Dimse.SequenceItem expected.");

				return(this.dvtkDataAttributeSet as DvtkData.Dimse.SequenceItem);
			}		
		}
	}
}
