// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Windows.Forms;
using System.Diagnostics;


namespace DvtkApplicationLayer
{
	/// <summary>
	/// Class adding extra methods concerning paths not present in the .NET Path class.
	/// </summary>
	class PathUtils
	{
		/// <summary>
		/// Returns a boolean indicating if the path is relative.
		/// </summary>
		/// <param name="thePath">The path.</param>
		/// <returns>Boolean indicating if the path is relative.</returns>
		public static bool IsRelativePath(string thePath)
		{
			bool isRelativePath = false;

			if (thePath.StartsWith(".\\"))
			{
				isRelativePath = true;
			}
			else if (thePath.StartsWith("..\\"))
			{
				isRelativePath = true;
			}
			else
			{
				isRelativePath = false;
			}

			return(isRelativePath);
		}

		/// <summary>
		/// Converts a relative path to an absolute path.
		/// </summary>
		/// <param name="theBasePath">The path to which the relative path is relative to.</param>
		/// <param name="theRelativePath">The relative path (must start with ".\" or "..\".</param>
		/// <returns>
		/// Returns the absolute path.
		/// If "theBasePath" directory does not exist or if "theRelativePath" directory is not relative,
		/// an empty string is returned.
		/// </returns>
		public static string ConvertToAbsolutePath(string theBasePath, string theRelativePath)
		{
			string theAbsolutePath = "";

			if (!IsRelativePath(theRelativePath))
			{
				// Sanity check.
				Debug.Assert(false, "Relative path expected!");

				theAbsolutePath = "";
			}
			else
			{
				try
				{
					string theOldCurrentDirectory = System.IO.Directory.GetCurrentDirectory();

					System.IO.Directory.SetCurrentDirectory(theBasePath);
					theAbsolutePath = System.IO.Path.GetFullPath(theRelativePath);
					System.IO.Directory.SetCurrentDirectory(theOldCurrentDirectory);
				}
				catch (Exception theException)
				{
					Debug.Assert(false, theException.Message);

					theAbsolutePath = "";
				}
			}

			return(theAbsolutePath);
		}
	}
}
