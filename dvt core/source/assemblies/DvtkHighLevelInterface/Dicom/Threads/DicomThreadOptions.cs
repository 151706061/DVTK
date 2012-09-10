using System;
using System.IO;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for DicomThreadOptions.
	/// </summary>
	public class DicomThreadOptions: ThreadOptions
	{
		private bool autoValidate = true;

		private DicomThread dicomThread = null;

		private String resultsFileName = null;

		private bool startAndEndResultsGathering = true;

		private bool resultsFilePerAssociation = false;

		private bool showResults = false;

		private int subResultsFileNameIndex = 0;

		private DicomThreadOptions()
		{
		}

		public DicomThreadOptions(DicomThread dicomThread)
		{
			this.dicomThread = dicomThread;
		}

		public bool AutoValidate
		{
			get
			{
				return(this.autoValidate);
			}
			set
			{
				this.autoValidate = value;
			}
		}

		public bool ResultsFilePerAssociation
		{
			set
			{
				this.resultsFilePerAssociation = value;
			}
			get
			{
				return this.resultsFilePerAssociation;
			}
		}

		public int NextSubResultsFileNameIndex
		{
			get
			{
				return this.subResultsFileNameIndex++;
			}
		}

		public ushort SessionId
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.SessionId);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.SessionId = value;
			}
		}

		/// <summary>
		/// The directory in which the results file(s) will be written.
		/// </summary>
		public String ResultsDirectory
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.ResultsRootDirectory);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.ResultsRootDirectory = value;
			}
		}

		public Dvtk.Sessions.ScriptSession DvtkScriptSession
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession);
			}
			set
			{
				this.dicomThread.DvtkScriptSession = value;
			}
		}

		public String ResultsFileName
		{
			get
			{
				return(this.resultsFileName);
			}
			set
			{
				this.resultsFileName = value;
			}
		}

		public bool StartAndEndResultsGathering
		{
			get
			{
				return(this.startAndEndResultsGathering);
			}
			set
			{
				this.startAndEndResultsGathering = value;
			}
		}


		public bool GenerateDetailedResults
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.DetailedValidationResults);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.DetailedValidationResults = value;
			}
		}


		// TODO!!!! Nu alleen absolute paden ondersteunen. Dit ook doen voor relatieve paden. Welk path
		// hiervoor dan gebruiken?

		private Object loadFromFileLock = new Object();

		
		public void LoadFromFile(String sessionFileName)
		{
			if (!File.Exists(sessionFileName))
			{
				throw new HliException("Script session file \"" + sessionFileName + "\" does not exist.");
			}

			// Because of the use of statics in Dvtk.Sessions.ScriptSession.LoadFromFile, only one thread at a time
			// should load a session file.
			lock(this.loadFromFileLock)
			{
				this.dicomThread.DvtkScriptSession = Dvtk.Sessions.ScriptSession.LoadFromFile(sessionFileName);
			}
		}
		
		

		/*
		public void CopyFrom(Dvtk.Sessions.ScriptSession dvtkScriptSession)
		{
			// Get a full file name to temporarily store the session content.
			String tempSessionFullFileName = Path.GetTempFileName();

			// Save the session content to a file.
			String currentSessionFullFileName = dvtkScriptSession.SessionFileName;
			dvtkScriptSession.SessionFileName = tempSessionFullFileName;
			dvtkScriptSession.SaveToFile();
			dvtkScriptSession.SessionFileName = currentSessionFullFileName;

			// Load the session content in Dvtk Session field of this object.
			this.dicomThread.DvtkScriptSession = Dvtk.Sessions.ScriptSession.LoadFromFile(tempSessionFullFileName);

			// Remove the temporary file.
			File.Delete(tempSessionFullFileName);
		}
		*/

		public Int32 DvtPort
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.DvtSystemSettings.Port);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.DvtSystemSettings.Port = Convert.ToUInt16(value);
			}
		}

		public bool ShowResults
		{
			get
			{
				return(this.showResults);
			}
			set
			{
				this.showResults = value;

				if (this.showResults)
				{
					this.DvtkScriptSession.DetailedValidationResults = true;
				}
			}
		}


		public Int32 SutPort
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.SutSystemSettings.Port);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.SutSystemSettings.Port = Convert.ToUInt16(value);
			}
		}

		public System.String SutIpAddress
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.SutSystemSettings.HostName);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.SutSystemSettings.HostName = value;
			}
		}

		public String DataDirectory
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.DataDirectory);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.DataDirectory = value;
			}
		}

		public bool ScrictValidation
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.StrictValidation);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.StrictValidation = value;
			}
		}

		public String DvtAeTitle
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.DvtSystemSettings.AeTitle);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.DvtSystemSettings.AeTitle = value;
			}
		}

		public String SutAeTitle
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.SutSystemSettings.AeTitle);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.SutSystemSettings.AeTitle = value;
			}
		}

		public Dvtk.Sessions.StorageMode StorageMode
		{
			get
			{
				return(this.dicomThread.DvtkScriptSession.StorageMode);
			}
			set
			{
				this.dicomThread.DvtkScriptSession.StorageMode = value;
			}
		}

		public bool LoadDefinitionFile(System.String filename)
		{
            return this.dicomThread.DvtkScriptSession.DefinitionManagement.LoadDefinitionFile(filename);
		}
		
		public void DeepCopyFrom(DicomThreadOptions dicomThreadOptions)
		{
			// Copy all settings from the Dvtk ScriptSession object.

			// Get a full file name to temporarily store the session content.
			String tempSessionFullFileName = Path.GetTempFileName();

			// Save the session content to a file.
			String currentSessionFullFileName = dicomThreadOptions.DvtkScriptSession.SessionFileName;
			dicomThreadOptions.DvtkScriptSession.SessionFileName = tempSessionFullFileName;
			dicomThreadOptions.DvtkScriptSession.SaveToFile();
			dicomThreadOptions.DvtkScriptSession.SessionFileName = currentSessionFullFileName;

			// Load the session content in Dvtk Session field of this object.
			this.dicomThread.DvtkScriptSession = Dvtk.Sessions.ScriptSession.LoadFromFile(tempSessionFullFileName);

			// Remove the temporary file.
			File.Delete(tempSessionFullFileName);

			
			// Copy all other settings not contained in the Dvtk ScriptSession object..

			AttachChildsToUserInterfaces = dicomThreadOptions.AttachChildsToUserInterfaces;
			AutoValidate = dicomThreadOptions.autoValidate;
			ShowResults = dicomThreadOptions.showResults;
		}
	}
}
