using System;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for MessageFilter.
	/// </summary>
	abstract public class OutboundMessageFilter
	{
		abstract public void Apply(DicomMessage dicomMessage);
	}
}
