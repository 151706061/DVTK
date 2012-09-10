using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents a release response.
	/// </summary>
	public class ReleaseRp: DulMessage
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataReleaseRp">The encapsulated DvtkData A_RELEASE_RP</param>
		public ReleaseRp(DvtkData.Dul.A_RELEASE_RP dvtkDataReleaseRp): base(dvtkDataReleaseRp)
		{
		}
	}
}
