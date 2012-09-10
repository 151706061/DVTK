using System;
using DvtkHighLevelInterface.Messages;
using System.Collections;
using VR = DvtkData.Dimse.VR;


namespace DvtkHighLevelInterface
{


	/// <summary>
	/// Summary description for MessageFilterSetAttribute.
	/// </summary>
	public class OutboundMessageFilterSetAttribute: OutboundMessageFilter
	{
		private ArrayList parameters;

		public OutboundMessageFilterSetAttribute(String tag, VR vR, params Object[] values)
		{
			this.parameters = new ArrayList(values);
			this.parameters.Insert(0, vR);
			this.parameters.Insert(0, tag);
		}

		override public void Apply(DicomMessage dicomMessage)
		{
			dicomMessage.Set(this.parameters.ToArray());
		}
	}
}
