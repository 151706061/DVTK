using System;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;
using DvtkHighLevelInterface;
using DvtkData;

namespace DCMCompare
{
	/// <summary>
	/// Summary description for ScriptSession.
	/// </summary>
	public class MainSession : DvtkHighLevelInterface.ScriptSession
	{
		public const string SCRIPT_FILE_NAME = "run.ds";

		public MainSession()
		{}

		protected override void Execute()
		{
			DicomMessage store1 = new DicomMessage(DvtkData.Dimse.DimseCommand.CSTORERQ);
			DicomMessage store2 = new DicomMessage(DvtkData.Dimse.DimseCommand.CSTORERQ);
			store1.DataSet.Read(DCMCompareForm.firstDCMFile);
			store2.DataSet.Read(DCMCompareForm.secondDCMFile);
			Validate(store1, store2, "All other attributes");			
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			GlobalOptions.AutoExit = true;

			MainSession theMainScriptSession = new MainSession();
			theMainScriptSession.Options.ResultsDirectory = Application.StartupPath + "\\Results\\";

			DirectoryInfo resultDirectory = new DirectoryInfo (theMainScriptSession.Options.ResultsDirectory);
			if(!resultDirectory.Exists)
			{
				resultDirectory.Create();
			}

			theMainScriptSession.Options.ScrictValidation = true;
			theMainScriptSession.LoadDefinitionFile(Application.StartupPath + "\\Allotherattributes.def");
			DvtkHighLevelInterface.Setup.Initialize(MainSession.SCRIPT_FILE_NAME, true);
			theMainScriptSession.StartAndEndResultsGathering = true;

			DvtkHighLevelInterface.Setup.MainScriptSession = theMainScriptSession;

			DCMCompareForm form = new DCMCompareForm();

			form.ShowDialog();

			theMainScriptSession.ExecuteThread(1000);

			DvtkHighLevelInterface.UserInteraction.Singleton.ShowDialog();

			DvtkHighLevelInterface.Setup.Terminate();

			form.ShowDialog();
		}
	}
}
