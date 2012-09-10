// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace Dvt
{
	/// <summary>
	/// Class implementing the functionality of the validation results tab.
	/// </summary>
	public class ValidationResultsManager
	{
		private const int _FIND_MATCH_WHOLE_WORD = 2;
		private const int _FIND_MATCH_CASE = 4;
		private const string _ERROR = "Error:";
		private const string _WARNING = "Warning:";


		private AxSHDocVw.AxWebBrowser _AxWebBrowser;
		private bool _BackEnabled = false;
		private bool _ForwardEnabled = false;
		private bool _ContainsErrors = false;
		private bool _ContainsWarnings = false;

		/// <summary>
		/// The Html document that is currently shown.
		/// </summary>
		private mshtml.HTMLDocument _HTMLDocument = null;
	
		/// <summary>
		/// The body of the Html document that is currently shown.
		/// </summary>
		private mshtml.HTMLBody _HTMLBody = null;

		/// <summary>
		/// The remaining text part of the displayed Html in which to find a next occurence of a string.
		/// </summary>
		private mshtml.IHTMLTxtRange _FindRemainingText = null;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="theAxWebBrowser">The web browser activeX control.</param>
		public ValidationResultsManager(AxSHDocVw.AxWebBrowser theAxWebBrowser)
		{
			_AxWebBrowser = theAxWebBrowser;
		}

		/// <summary>
		/// Convert a XML file into a HTML file.
		/// </summary>
		/// <param name="theDirectory">The directory in which the XML file resides and in which the HTML file will be written.</param>
		/// <param name="theXmlFileNameOnly">The XML file name only.</param>
		/// <param name="theHtmlFileNameOnly">The HTNL file name only.</param>
		public void ConvertXmlToHtml(string theDirectory, string theXmlFileNameOnly, string theHtmlFileNameOnly)
		{
			string theXmlFullFileName;
			string theHtmlFullFileName;
			string theResultsStyleSheetFullFileName;

			theXmlFullFileName = System.IO.Path.Combine(theDirectory, theXmlFileNameOnly);
			theHtmlFullFileName = theXmlFullFileName.Replace(".xml", ".html");
			theResultsStyleSheetFullFileName = System.IO.Path.Combine(Application.StartupPath, "DVT_RESULTS.xslt");

			XslTransform xslt = new XslTransform ();

			xslt.Load(theResultsStyleSheetFullFileName);

			XPathDocument xpathdocument = new XPathDocument (theXmlFullFileName);

			/* This code does not work with the style sheet needed for the Session.WriteHtmlInformation method.
			XmlTextWriter writer = new XmlTextWriter (theHtmlFullFileName, System.Text.Encoding.UTF8);
			writer.Formatting = Formatting.None;
			xslt.Transform (xpathdocument, null, writer, null);
			writer.Flush ();
			writer.Close ();
			*/

			FileStream fileStream = new FileStream(theHtmlFullFileName, FileMode.Create, FileAccess.ReadWrite);

			xslt.Transform(xpathdocument, null, fileStream, null);
			fileStream.Close();
		}

		/// <summary>
		/// Show a HTML link.
		/// </summary>
		/// <param name="theHtmlFullFileName">The HTML link.</param>
		/// <param name="isNewURL">Indicates if this is a new link or an existing link (back/forward button is used)</param>
		public void ShowHtml(string theHtmlFullFileName)
		{
			object Zero = 0;
			object EmptyString = "";

			_AxWebBrowser.Navigate (theHtmlFullFileName, ref Zero, ref EmptyString, ref EmptyString, ref EmptyString);
           
		}

		/// <summary>
		/// Remove, if needed, characters from a Html link to get a valid full file name.
		/// </summary>
		/// <param name="theHtmlLink">The HTML link</param>
		/// <returns>The full file name.</returns>
		public string GetFullFileNameFromHtmlLink(string theHtmlLink)
		{
			string theReturnValue = theHtmlLink;

			// If the string starts with "file:///", remove it.
			if (theReturnValue.StartsWith("file:///"))
			{
				theReturnValue = theReturnValue.Remove(0, "file:///".Length);
			}

			// If the string contains a '#', remove it and all following characters.
			int theCrossIndex = theReturnValue.IndexOf('#');

			if (theCrossIndex != -1)
			{
				theReturnValue = theReturnValue.Substring(0, theCrossIndex);
			}

			theReturnValue = theReturnValue.Replace("%20", " ");

			return(theReturnValue);
		}

		/// <summary>
		/// Indicates if it is possible to navigate back.
		/// </summary>
		/// <returns></returns>
		public bool IsBackEnabled()
		{
			return(_BackEnabled);
		}

		/// <summary>
		/// Indicates if it is possible to navigate forward.
		/// </summary>
		/// <returns></returns>
		public bool IsForwardEnabled()
		{
			return(_ForwardEnabled);
		}

		/// <summary>
		/// Show the previous URL link.
		/// </summary>
		public void Back()
		{
			if (IsBackEnabled())
			{
				//_VisitedURLsCurrentIndex--;

				_AxWebBrowser.GoBack();
               
			}
		}

		/// <summary>
		/// Show the next URL link.
		/// </summary>
		public void Forward()
		{
			if (IsForwardEnabled())
			{
				//_VisitedURLsCurrentIndex++;
				_AxWebBrowser.GoForward();
				
			}
		}

		/// <summary>
		/// Handles the command state change, to determine if the Back and Forward are enabled or disabled.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void CommandStateChange(object sender, AxSHDocVw.DWebBrowserEvents2_CommandStateChangeEvent e)
		{
			switch(e.command)
			{
				case 1:
					_ForwardEnabled = e.enable;
					break;

				case 2:
					_BackEnabled = e.enable;
					break;

				default:
					// Do nothing.
					break;
			}
		}

		/// <summary>
		/// Handler of the NavigateComplete2 event.
		/// </summary>
		public void NavigateComplete2Handler()
		{
			mshtml.IHTMLTxtRange theIHTMLTxtRange = null;

			_HTMLDocument = (mshtml.HTMLDocument)_AxWebBrowser.Document;
			_HTMLBody = (mshtml.HTMLBody)_HTMLDocument.body;

			// Determine if errors exist in the displayed Html.
			theIHTMLTxtRange = _HTMLBody.createTextRange();
			_ContainsErrors = theIHTMLTxtRange.findText(_ERROR, theIHTMLTxtRange.text.Length, _FIND_MATCH_CASE | _FIND_MATCH_WHOLE_WORD);
 
			// Determine if warnings exist in the displayed Html.
			theIHTMLTxtRange = _HTMLBody.createTextRange();
			_ContainsWarnings= theIHTMLTxtRange.findText(_WARNING, theIHTMLTxtRange.text.Length, _FIND_MATCH_CASE | _FIND_MATCH_WHOLE_WORD);

			_FindRemainingText = _HTMLBody.createTextRange();
			((mshtml.IHTMLSelectionObject)_HTMLDocument.selection).empty();
		}

		public bool ContainsErrors
		{
			get
			{
				return _ContainsErrors;
			}
		}

		public bool ContainsWarnings
		{
			get
			{
				return _ContainsWarnings;
			}
		}

		public void FindNextWarning()
		{
			FindNextText(_WARNING, true, true);
		}


		public void FindNextError()
		{
			FindNextText(_ERROR, true, true);
		}

		public void FindNextText(string theText, bool mustMatchWholeWord, bool mustMatchCase)
		{
			// define the search options
			int theSearchOption = 0;

			if (mustMatchWholeWord)
			{
				theSearchOption += _FIND_MATCH_WHOLE_WORD;
			}

			if (mustMatchCase)
			{
				theSearchOption += _FIND_MATCH_CASE;
			}

			if ( (_FindRemainingText == null) || (_FindRemainingText.text == null) )
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{

				// perform the search operation
				if (_FindRemainingText.findText(theText, _FindRemainingText.text.Length, theSearchOption))
					// String has been found.
				{
					// Select the found text within the document
					_FindRemainingText.select();

					// Limit the new find range to be from the newly found text
					mshtml.IHTMLTxtRange theFoundRange = (mshtml.IHTMLTxtRange)_HTMLDocument.selection.createRange();
					_FindRemainingText = (mshtml.IHTMLTxtRange)_HTMLBody.createTextRange();
					_FindRemainingText.setEndPoint("StartToEnd", theFoundRange);
				}
				else
				{
					// Reset the find ranges
					_FindRemainingText = _HTMLBody.createTextRange();
					((mshtml.IHTMLSelectionObject)_HTMLDocument.selection).empty();

					MessageBox.Show("Finished searching the document", string.Format("Find text \"{0}\"", theText), MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
		}
	}
}
