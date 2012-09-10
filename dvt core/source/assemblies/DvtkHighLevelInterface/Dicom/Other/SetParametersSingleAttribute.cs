using System;
using System.Collections;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for SetParametersSingleAttribute.
	/// </summary>
	internal class SetParameterGroup
	{
		public SetParameterGroup()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public String tagAsString = "";

		public UInt32 tagAsUInt32 = 0;

		public DvtkData.Dimse.VR vR = DvtkData.Dimse.VR.UN;

		public ArrayList values = new ArrayList();
	}
}
