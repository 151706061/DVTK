// Part of DVTK.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using Dvtk.Sessions;
using DvtkData.ComparisonResults;

namespace Dvtk.Results
{
	/// <summary>
	/// Summary description for ResultsReporter.
	/// </summary>
	public class ResultsReporter
	{
		private Dvtk.Sessions.ScriptSession _scriptSession = null;

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="resultsDirectory">Directory where the results files are to be written.</param>
		public ResultsReporter(System.String resultsDirectory)
		{
			_scriptSession = new ScriptSession();
			_scriptSession.ResultsRootDirectory = resultsDirectory;
		}

		/// <summary>
		/// Start the results reporting to the given file.
		/// </summary>
		/// <param name="filename">Results filename.</param>
		public void Start(System.String filename)
		{
			_scriptSession.StartResultsGathering(filename);
		}

		/// <summary>
		/// Stop the results reporting.
		/// </summary>
		public void Stop()
		{
			_scriptSession.EndResultsGathering();
		}

		/// <summary>
		/// Write the Message Comparision Results to the results reporter.
		/// </summary>
		/// <param name="messageComparisonResults">Message comparison results.</param>
		public void WriteMessageComparisonResults(MessageComparisonResults messageComparisonResults)
		{
			_scriptSession.WriteMessageComparisonResults(messageComparisonResults);
		}

		/// <summary>
		/// Write the Validation Error message to the results reporter.
		/// </summary>
		/// <param name="message">Error message.</param>
		public void WriteValidationError(String message)
		{
			_scriptSession.WriteValidationError(message);
		}

		/// <summary>
		/// Write the Validation Information message to the results reporter.
		/// </summary>
		/// <param name="message">Information message.</param>
		public void WriteValidationInformation(String message)
		{
			_scriptSession.WriteValidationInformation(message);
		}

		/// <summary>
		/// Write the HTML Information message to the results reporter.
		/// </summary>
		/// <param name="htmlMessage">HTML message.</param>
		public void WriteHtmlInformation(String htmlMessage)
		{
			_scriptSession.WriteHtmlInformation(htmlMessage);
		}

		/// <summary>
		/// Write the Validation Warning message to the results reporter.
		/// </summary>
		/// <param name="message">Warning message.</param>
		public void WriteValidationWarning(String message)
		{
			_scriptSession.WriteValidationWarning(message);
		}
	}
}
