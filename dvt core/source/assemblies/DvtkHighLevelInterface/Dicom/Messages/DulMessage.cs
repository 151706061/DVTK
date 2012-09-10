using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents a Dul message.
	/// </summary>
	public abstract class DulMessage: DicomProtocolMessage
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property DvtkDataDulMessage.
		/// </summary>
		private DvtkData.Dul.DulMessage dvtkDataDulMessage = null;



		//
		// - Constructors -
		//		

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataDulMessage">The encapsulated DvtkData DulMessage.</param>
		internal DulMessage(DvtkData.Dul.DulMessage dvtkDataDulMessage)
		{
			this.dvtkDataDulMessage = dvtkDataDulMessage;
		}



		//
		// - Properties -
		//

		/// <summary>
		/// The encapsulated DvtkData DulMessage.
		/// </summary>
		public DvtkData.Dul.DulMessage DvtkDataDulMessage
		{
			get
			{
				return(this.dvtkDataDulMessage);
			}
		}
	}
}
