using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents a release request.
	/// </summary>
	public class ReleaseRq: DulMessage
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataReleaseRq">The encapsulated DvtkData A_RELEASE_RQ</param>
		public ReleaseRq(DvtkData.Dul.A_RELEASE_RQ dvtkDataReleaseRq): base(dvtkDataReleaseRq)
		{
		}
	}
}
