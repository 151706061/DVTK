using System;
using System.Collections;

namespace DvtkApplicationLayer
{
	/// <summary>
	/// Represents all results files for a test execution: main results file(s) and maybe some sub results file(s).
	/// </summary>
	public class MainResults: Results
	{
		private ArrayList subResultsList = new ArrayList();

		public MainResults(String baseName, String date, String time, String sutVersion, PassedStateEnum passedState): base(baseName, date, time, sutVersion, passedState)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Remove all results files (also the sub results files).
		/// </summary>
		public void Remove()
		{
			// TODO

			foreach(SubResults subResults in subResultsList)
			{
				subResults.Remove();
			}
		}

		/// <summary>
		/// Add a sub results to this main results.
		/// </summary>
		/// <param name="subResults"></param>
		public void Add(SubResults subResults)
		{
			subResultsList.Add(subResults);
		}
	}
}
