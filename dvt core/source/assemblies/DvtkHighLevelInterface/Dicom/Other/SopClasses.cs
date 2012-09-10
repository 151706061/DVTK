using System;
using System.Collections;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for SopClasses.
	/// </summary>
	public class SopClasses
	{
		public ArrayList list = null;

		public SopClasses(params String[] list)
		{
			this.list = new ArrayList(list);
		}
	}
}
