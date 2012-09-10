using System;
using System.Collections;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for TransferSyntaxes.
	/// </summary>
	public class TransferSyntaxes
	{
		public ArrayList list = null;

		public TransferSyntaxes(params String[] list)
		{
			this.list = new ArrayList(list);
		}
	}
}
