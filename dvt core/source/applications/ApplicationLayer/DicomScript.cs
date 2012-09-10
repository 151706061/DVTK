using System;
using System.Collections;

namespace DvtkApplicationLayer
{
	/// <summary>
	/// Summary description for DicomScript.
	/// </summary>
	public class DicomScript: Script
	{
		public DicomScript(Session session, String scriptFileName): base(session, scriptFileName)
		{
		}

		// Marco!!! Not needed anymore, now inheriting from Script class.
		/*
		private string dicomScriptName; 
		private IList resultDicomScripts = new ArrayList();
        /// <summary>
        /// Name of the script file (that ends with .dss or .ds).
        /// </summary>
		public string DicomScriptName 
		{
			get 
			{
				return dicomScriptName ;}
			set 
			{
				dicomScriptName = value;}
		}
        /// <summary>
        /// Represents collection of results for a dicom script.
        /// </summary>
		public IList Result
		{
			get 
			{ 
				return resultDicomScripts ;}
			set 
			{
				resultDicomScripts = value;
			}
		}
		*/

	}
}
	

