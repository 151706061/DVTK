using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents an associate request.
	/// </summary>
	public class AssociateRq: DulMessage
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataAssociateRq">The encapsulated DvtkData A_ASSOCIATE_RQ</param>
		internal AssociateRq(DvtkData.Dul.A_ASSOCIATE_RQ dvtkDataAssociateRq): base(dvtkDataAssociateRq)
		{
		}

		//
		// - Properties -
		//

		/// <summary>
		/// Get the encapsulated DvtkData A_ASSOCIATE_RQ.
		/// </summary>
		internal DvtkData.Dul.A_ASSOCIATE_RQ DvtkDataAssociateRq
		{
			get
			{
				return(DvtkDataDulMessage as DvtkData.Dul.A_ASSOCIATE_RQ);
			}
		}
	}
}
