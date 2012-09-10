using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// Abstract base class for all Dicom protocol messages.
	/// </summary>
	public abstract class DicomProtocolMessage: Message
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property AreValueRepresentationsValidated.
		/// </summary>
		private bool areValueRepresentationsValidated = false;



		//
		// - Properties -
		//

		/// <summary>
		/// Indicates of the VR's are already validated.
		/// </summary>
		internal bool AreValueRepresentationsValidated
		{
			get
			{
				return(this.areValueRepresentationsValidated);
			}
			set
			{
				this.areValueRepresentationsValidated = value;
			}
		}
	}
}
