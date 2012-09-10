// Part of DvtkComparator.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using Dvtk.Results;

namespace Dvtk.Comparator
{
	/// <summary>
	/// Summary description for BaseComparator.
	/// </summary>
	public abstract class BaseComparator
	{
		/// <summary>
		/// Compare the two messages.
		/// </summary>
		/// <param name="resultsReporter">Results reporter.</param>
		/// <param name="thatBaseComparator">Reference comparator.</param>
		/// <returns>bool - true = messages compared, false messages not compared</returns>
		public abstract bool Compare(ResultsReporter resultsReporter, BaseComparator thatBaseComparator);
	}
}
