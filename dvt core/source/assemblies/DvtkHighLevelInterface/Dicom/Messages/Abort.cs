using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents an abort.
	/// </summary>
	public class Abort: DulMessage
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataAbort">The encapsulated DvtkData A_ABORT.</param>
		internal Abort(DvtkData.Dul.A_ABORT dvtkDataAbort): base(dvtkDataAbort)
		{
		}
	}
}
