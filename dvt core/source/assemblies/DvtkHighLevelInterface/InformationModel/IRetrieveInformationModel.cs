using System;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for IRetrieveInformationModel.
	/// </summary>
	public interface IRetrieveInformationModel
	{
		/// <summary>
		/// Retrieve data from the Information Model using the given retrieve message.
		/// </summary>
		/// <param name="retrieveMessage">Message used to retrieve the Information Model.</param>
		/// <returns>File list - containing the filenames of all instances matching the retrieve dataset attributes.</returns>
		DvtkData.Collections.StringCollection RetrieveInformationModel(DicomMessage retrieveMessage);
	}
}
