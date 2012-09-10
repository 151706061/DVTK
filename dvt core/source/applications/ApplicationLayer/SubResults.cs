using System;

namespace DvtkApplicationLayer
{
	/// <summary>
	/// Summary description for SubResults.
	/// </summary>
	public class SubResults: Results
	{
		public SubResults(String baseName, String date, String time, String sutVersion, PassedStateEnum passedState): base(baseName, date, time, sutVersion, passedState)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Remove the sub results files (one summary and/or detail sub results files this object represents).
		/// </summary>
		public void Remove()
		{
			// TODO.
		}
	}
}
