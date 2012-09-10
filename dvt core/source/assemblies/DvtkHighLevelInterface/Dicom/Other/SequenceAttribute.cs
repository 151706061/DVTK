using System;
using DvtkData.Dimse;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// An object of this class represents a sequence attribute.
	/// </summary>
	internal class SequenceAttribute: Attribute
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor. Use it to construct a new empty sequence attribute.
		/// </summary>
		/// <param name="tagAsUInt32"></param>
		public SequenceAttribute(UInt32 tagAsUInt32): base(tagAsUInt32.ToString(), new DvtkData.Dimse.Attribute(tagAsUInt32, VR.SQ))
		{
		}

		/// <summary>
		/// Constructor. Use this to encapsulate an existing DvtkData sequence attribute.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/') of this attribue.</param>
		/// <param name="dvtkDataAttribute">The encapsulated DvtkData attribute</param>
		/// 
		public SequenceAttribute(String tagSequence, DvtkData.Dimse.Attribute dvtkDataAttribute): base(tagSequence, dvtkDataAttribute)
		{
		}



		//
		// - Properties -
		//

		/// <summary>
		/// The encapsulated DvtkData sequence.
		/// </summary>
		public DvtkData.Dimse.Sequence DvtkDataSequence
		{
			get
			{
				DvtkData.Dimse.SequenceOfItems dvtkDataSequenceValue = this.dvtkDataAttribute.DicomValue as DvtkData.Dimse.SequenceOfItems;

				if (dvtkDataSequenceValue == null)
				{
					// Sanity check.
					DvtkHighLevelInterfaceException.Throw("Expecting field to be of type DvtkData.Dimse.Sequence.");
				}

				return(dvtkDataSequenceValue.Sequence);
			}
		}

		/// <summary>
		/// Number of items contained in this sequence.
		/// </summary>
		public int ItemCount
		{
			get
			{
				return DvtkDataSequence.Count;
			}
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Get an item from this sequence.
		/// </summary>
		/// <param name="index">1-based index.</param>
		/// <returns>The item.</returns>
		public DataSet GetItem(int index)
		{
			DataSet dataSet = new DataSet(DvtkDataSequence[index - 1]);

			return dataSet;
		}

		/// <summary>
		/// Add an item to this sequence.
		/// </summary>
		/// <param name="sequenceItem"></param>
		public void AddItem(SequenceItem  sequenceItem)
		{
			DvtkDataSequence.Add(sequenceItem.DvtkDataSequenceItem);
		}
	}
}
