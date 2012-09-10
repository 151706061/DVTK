using System;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for SCP.
	/// </summary>
	public class SCP: MessageIterator
	{
		public override void AfterHandlingAssociateRequest(AssociateRq associateRq)
		{
			if (!IsMessageHandled)
			{
		        SendAssociateAc(new TransferSyntaxes("1.2.840.10008.1.2", "1.2.840.10008.1.2.1", "1.2.840.10008.1.2.2"));
				IsMessageHandled = true;
			}
		}

		public override void AfterHandlingReleaseRequest(ReleaseRq releaseRq)
		{
			if (!IsMessageHandled)
			{
				SendReleaseRp();

				CheckForNewResultsFile();

				IsMessageHandled = true;
			}			
		}
	}
}
