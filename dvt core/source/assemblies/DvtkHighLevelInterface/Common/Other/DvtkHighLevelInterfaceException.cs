using System;
using System.Diagnostics;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for DvtkHighLevelInterfaceException.
	/// </summary>
	internal class DvtkHighLevelInterfaceException: Exception
	{
		public DvtkHighLevelInterfaceException(String exceptionText): base(exceptionText)
		{
		}

		public static void Throw(String description)
		{
			throw new DvtkHighLevelInterfaceException(description);
		}
	}
}
