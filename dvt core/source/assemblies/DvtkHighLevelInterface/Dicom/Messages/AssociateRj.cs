using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents an associate reject.
	/// </summary>
	public class AssociateRj: DulMessage
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataAssociateRj">The encapsulated DvtkData A_ASSOCIATE_RJ</param>
		internal AssociateRj(DvtkData.Dul.A_ASSOCIATE_RJ dvtkDataAssociateRj): base(dvtkDataAssociateRj)
		{
		}
	}
}
