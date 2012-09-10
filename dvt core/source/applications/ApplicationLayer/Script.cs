// Part of ApplicationLayer.dll - .NET class library
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Collections;
using System.IO;
using DvtkSession = Dvtk.Sessions;

namespace DvtkApplicationLayer
{
	/// <summary>
	/// Summary description for Script.
	/// </summary>

	public class Script: PartOfSession

	{
		public Script(Session session, String scriptFileName): base(session)
		{
			this.scriptFileName = scriptFileName;

		}

		private string scriptFileName; 
		private IList resultScripts = new ArrayList();
        
        /// <summary>
        /// represents the script name.
        /// </summary>
		public string ScriptFileName 
		{
			get 
			{
				return scriptFileName ;}
			set 
			{
				scriptFileName = value;}
		}
        /// <summary>
        /// Collection of results for a script.
        /// </summary>
		public IList Result
		{
			get 
			{ 
				return resultScripts ;}
			set 
			{
				resultScripts = value;
			}
		}
        public void RemoveResultFiles(){
        }
        /// <summary>
        /// Method to Create the results for a script.
        /// </summary>
        /// <param name="scriptFile"> Name of the script.</param>

        
        public void CreateScriptResult(Script scriptFile) {
            ArrayList scripts = new ArrayList();
            DirectoryInfo directoryInf = new DirectoryInfo(session.ResultsRootDirectory);
			string scriptFileName = scriptFile.ScriptFileName.Replace(".", "_");
            FileInfo[] filesInf = directoryInf.GetFiles("summary_" + "*" + scriptFileName + "*.xml");
            ArrayList tempResultFiles = new ArrayList();
           
             					
            foreach (FileInfo fileInf in filesInf) {
                Result correctResult = null;
                String sessionId = session.GetSessionId (fileInf.Name);

                foreach(Result result in scriptFile.Result) {
                    if (result.SessionId == sessionId) {
                        correctResult = result;
                        break;
                    }
                }
						
                if (correctResult == null) {
                    correctResult = new Result(this.ParentSession);
                    correctResult.SessionId = sessionId;

                    scriptFile.Result.Add(correctResult);
                }

                bool isSummaryFile = true;
                bool isMainResult = true;

                if (fileInf.Name.ToLower().StartsWith("summary_")) {
                    isSummaryFile = true;
                }
                else {
                    isSummaryFile = false;
                }
		
                if (fileInf.Name.ToLower().EndsWith(scriptFileName.ToLower() +"_res.xml")) {
                    isMainResult = true;
                }
                else {
                    isMainResult = false;
                }

                if (isSummaryFile) {
                    if (isMainResult) {
                        correctResult.SummaryFile = fileInf.Name;
                    }
                    else {
                        correctResult.SubSummaryResultFiles.Add(fileInf.Name);
                    }
                }
                else {
                    if (isMainResult) {
                        correctResult.DetailFile = fileInf.Name;
                    }
                    else {
                        correctResult.SubDetailResultFiles.Add(fileInf.Name);
                    }
                }
            }				     
        }
	}
}

