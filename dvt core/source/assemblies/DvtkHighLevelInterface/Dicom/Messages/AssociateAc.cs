using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents an associate ac.
	/// </summary>
	public class AssociateAc: DulMessage
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataAssociateAc">The encapsulated DvtkData A_ASSOCIATE_AC</param>
		internal AssociateAc(DvtkData.Dul.A_ASSOCIATE_AC dvtkDataAssociateAc): base(dvtkDataAssociateAc)
		{
		}



		//
		// - Properties -
		//

		/// <summary>
		/// Get the encapsulated DvtkData A_ASSOCIATE_AC.
		/// </summary>
		internal DvtkData.Dul.A_ASSOCIATE_AC DvtkDataAssociateAc
		{
			get
			{
				return(DvtkDataDulMessage as DvtkData.Dul.A_ASSOCIATE_AC);
			}
		}
	}
}
