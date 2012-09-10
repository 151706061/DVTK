// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace Dvtk.IheActors
{
	/// <summary>
	/// Summary description for MasterData.
	/// </summary>
	public class MasterData
	{
		Dvtk.Sessions.ScriptSession _masterDataSession = new Dvtk.Sessions.ScriptSession();

		public MasterData()
		{
			_masterDataSession.SessionId = 1;
			_masterDataSession.SessionTitle = "Master Data Session";
			_masterDataSession.DefinitionManagement.LoadDefinitionFile("C:\\Program Files\\DVT20\\definitions\\ModalityWorklistTemplateData.def");
			_masterDataSession.DicomScriptRootDirectory = "C:\\Program Files\\DVT20\\results";
			_masterDataSession.ResultsRootDirectory = "C:\\Program Files\\DVT20\\results";
		}

		public void LoadTemplates(System.String templateFile)
		{
			_masterDataSession.ExecuteScript(templateFile, true);
		}
	}
}
