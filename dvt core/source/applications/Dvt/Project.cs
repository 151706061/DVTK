// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;
using System.Xml;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using Dvtk;

namespace Dvt
{
	/// <summary>
	/// Summary description for project.
	/// </summary>
	public class Project
	{
		public Project()
		{
			_HasProjectChanged = false;
			_HasUserCancelledLastOperation = false;
			_IsProjectConstructed = false;
			_ProjectFileName = "";
		}

        public delegate void CallBackMessageDisplay (string message);

        public string ProjectFileName
        {
            get { return this._ProjectFileName; }
            set { this._ProjectFileName = value; }
        }

        public bool HasProjectChanged
        {
            get { return this._HasProjectChanged; }
        }

		public void AddSession(string theSessionFullFileName)
		{
			Dvtk.Sessions.Session theLoadedSession = LoadSession(theSessionFullFileName);

			if (theLoadedSession != null)
			{
				// Search the sessions loaded in project file for same ResultsRootDirectory 
				foreach(SessionInformation theSessionInformation in _LoadedSessions)
				{
					if (theSessionInformation.Session.ResultsRootDirectory == theLoadedSession.ResultsRootDirectory)
					{
						string msg =
							string.Format(
							"The {0} have the same results directory \n as session {1} in the project.", 
							theLoadedSession.SessionFileName,theSessionInformation.Session.SessionFileName);

						MessageBox.Show(msg, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

//						this.display_message (msg);
//
//						// Open the browser dialog for changing the result directory
//						FolderBrowserDialog theFolderBrowserDialog = new FolderBrowserDialog();
//
//						theFolderBrowserDialog.Description = "Select or Create the new result directory";
//
//						// Show the browser dialog.
//						// If the user pressed the OK button...save the modified result directory
//						if (theFolderBrowserDialog.ShowDialog() == DialogResult.OK)
//						{
//							theLoadedSession.ResultsRootDirectory = theFolderBrowserDialog.SelectedPath;
//							if (theSessionInformation.Session.ResultsRootDirectory == theLoadedSession.ResultsRootDirectory)
//							{
//								msg =
//									string.Format(
//									"Failed to load session file: {0}.\n The result directory is not changed.", 
//									theLoadedSession.SessionFileName);
//								this.display_message (msg);
//							}
//							else
//							{
//								SessionInformation theSessionInfo = new SessionInformation(theLoadedSession);
//								theSessionInfo.HasChanged = true;
//								_LoadedSessions.Add(theSessionInfo);
//								_HasProjectChanged = true;
//							}							
//						}
//						else
//						{
//							msg =
//								string.Format(
//								"Failed to load session file: {0}.\n The result directory is not changed.", 
//								theLoadedSession.SessionFileName);
//							this.display_message (msg);
//						}
//						return;
					}
				}

				_LoadedSessions.Add(new SessionInformation(theLoadedSession));
				_HasProjectChanged = true;
			}
		}

		public Dvtk.Sessions.Session LoadSession(string theSessionFullFileName)
		{
			FileInfo theSessionFileInfo = new FileInfo (theSessionFullFileName);
			Dvtk.Sessions.Session theSession = null;

			if (theSessionFileInfo.Exists)
			{
				try
				{
					theSession = Dvtk.Sessions.SessionFactory.TheInstance.Load (theSessionFullFileName);

					// In the Dvt UI, always generate summary validation results.
					theSession.SummaryValidationResults = true;
				}
				catch (Exception e)
				{
					string msg = string.Empty;
					msg += string.Format("Failed to load session file: {0}\n", theSessionFullFileName);
					msg += e.Message;
					this.display_message (msg);
					theSession = null;
				}
			}
			else
				this.display_message(
					string.Format(
					"Session file \"{0}\" does not exist.",
					theSessionFullFileName)
					);

			return(theSession);
		}


		// TODO: Implement a function in Dvtk to unload a session file.
		public void RemoveSession(Dvtk.Sessions.Session theSession)
		{
			SessionInformation theSessionInformation = GetSessionInformation(theSession);

			if (theSessionInformation == null)
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{
				_LoadedSessions.Remove(theSessionInformation);
				_HasProjectChanged = true;
			}
		}

        public bool ContainsSession (string session_file)
        {
			bool theReturnValue = false;

			foreach(SessionInformation theSessionInformation in _LoadedSessions)
			{
				if (theSessionInformation.Session.SessionFileName == session_file)
				{
					theReturnValue = true;
				}
			}

            return theReturnValue;
        }

        public int GetNrSessions ()
        {
            return _LoadedSessions.Count;
        }

		public bool GetSessionChanged (Dvtk.Sessions.Session theSession)
		{
			bool theReturnValue = false;

			SessionInformation theSessionInformation = GetSessionInformation(theSession);
	
			if (theSessionInformation == null)
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{
				theReturnValue = theSessionInformation.HasChanged;
			}

			return(theReturnValue);
		}

		public void SetSessionChanged (Dvtk.Sessions.Session theSession, bool changed)
		{
			SessionInformation theSessionInformation = GetSessionInformation(theSession);
	
			if (theSessionInformation == null)
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{
				theSessionInformation.HasChanged = changed;
			}
		}

        public Dvtk.Sessions.Session GetSession (int index)
        {
			return(GetSessionInformation(index).Session);
        }

		/// <summary>
		/// Save the Project to the specified Project file name. 
		/// The current project file name will also change to the specified project file name.
		/// Sessions will not be saved.
		/// </summary>
		/// <param name="theProjectFileName">The name of the Project file ot save to.</param>
		/// <returns>Boolean indicating if the saving was successfull.</returns>
		public bool SaveProject(string theProjectFileName)
		{
			_ProjectFileName = theProjectFileName;
			return(SaveProject());
		}

		/// <summary>
		/// Save the Project to the current Project file name.
		/// Sessions will not be saved.
		/// </summary>
		/// <returns>Boolean indicating if the saving was successfull.</returns>
        public bool SaveProject()
        {
            bool success = false;
            XmlTextWriter writer = null;
            try
            {
                writer = new XmlTextWriter(this._ProjectFileName, System.Text.Encoding.ASCII);

				// The written .xml file will be more readable
				writer.Formatting = Formatting.Indented;

                // Start the document
                writer.WriteStartDocument (true);

                // Write the session element containing all session files
                writer.WriteStartElement ("Sessions");

                // Write the session filenames to the document
                for (int i = 0; i < _LoadedSessions.Count; i++)
                {
                    writer.WriteElementString ("Session", GetSessionInformation(i).Session.SessionFileName);
                }

                // End the sessions element
                writer.WriteEndElement ();

                // End the document
                writer.WriteEndDocument ();

				success = true;
            }
            catch (Exception e)
            {
                string msg =
                    string.Format(
                    "Failed to write project file: {0}. Due to exception:{1}\n", 
                    this._ProjectFileName,
                    e.Message);
                this.display_message (msg);

				success = false;
            }
            finally
            {
                // Close Flushes the document to the stream
                if (writer != null) writer.Close();
            }

			if (success)
			{
				_HasProjectChanged = false;
				_HasUserCancelledLastOperation = false;
			}

            return success;
        }

        public bool Load(string theProjectFileName)
        {
            bool success = false;

			_ProjectFileName = theProjectFileName;

            //
            // Check that the file exists.
            //
            FileInfo projectFileInfo = new FileInfo(this._ProjectFileName);

			if (!projectFileInfo.Exists)
			{
				string msg =
					string.Format(
					"Failed to load project file: {0}\n. File does not exist.", this._ProjectFileName);
				this.display_message (msg);
				success = false;
			}
			else
			{
				XmlTextReader reader = null;
				try
				{
					reader = new XmlTextReader(this._ProjectFileName);
					string theSessionFileName = "";
					reader.WhitespaceHandling = WhitespaceHandling.None;
					reader.MoveToContent();
					do
					{
						theSessionFileName = reader.ReadElementString ();
						//
						// Check for the rare occasion that a project is opened which
						// doesn't contain a single session file.
						//
						if (theSessionFileName != "") 
						{
							if (PathUtils.IsRelativePath(theSessionFileName))
							{
								theSessionFileName = PathUtils.ConvertToAbsolutePath(Path.GetDirectoryName(this._ProjectFileName), theSessionFileName);
							}

							// If the session file name was already a full file name, or the conversion
							// from a relative filename to an absolute one succeeded, load the session.
							if (theSessionFileName != "")
							{
								this.AddSession (theSessionFileName);
							}
						}
					} while (reader.Name == "Session");
					
					success = true;
				}
				catch (Exception e)
				{
					string msg =
						string.Format(
						"Failed to load project file: {0}. Due to exception:{1}\n", 
						this._ProjectFileName,
						e.Message);
					this.display_message (msg);
					success = false;
				}
				finally
				{
					if (reader != null) reader.Close();
				}
			}

			if (success)
			{
				_HasProjectChanged = false;
				_HasUserCancelledLastOperation = false;
				_IsProjectConstructed = true;
			}
			else
			{
				Close(false);
			}

            return success;
        }

		public  CallBackMessageDisplay  display_message;

		private bool _HasProjectChanged;
		public bool _HasUserCancelledLastOperation = false;
		private bool _IsProjectConstructed = false;
		private ArrayList _LoadedSessions = new ArrayList();
        private string _ProjectFileName;


		private int GetNumberOfChangedSessions()
		{
			int theNumberOfChangedSessions = 0;

			for (int i = 0 ; i < GetNrSessions() ; i++)
			{
				if (GetSessionChanged(GetSession(i)))
				{
					theNumberOfChangedSessions++;
				}
			}

			return(theNumberOfChangedSessions);
		}

		public bool AreProjectOrSessionsChanged()
		{
			bool areChanged = false;
			
			if (HasProjectChanged)
			{
				areChanged = true;
			}
			else if (GetNumberOfChangedSessions() != 0)
			{
				areChanged = true;
			}

			return (areChanged);
		}

		public void Save(bool showSaveUnsavedChangesQuestion)
		{
			if (!AreProjectOrSessionsChanged())
			{
				// Sanity check.
				// The UI should not call this method when nothing has changed.
				Debug.Assert (false);
			}
			else
			{
				_HasUserCancelledLastOperation = false;
				bool mustSaveChanges = false;

				if (showSaveUnsavedChangesQuestion)
				{
					DialogResult theDialogResult = ShowSaveUnsavedChangesQuestion();

					if (theDialogResult == DialogResult.Yes)
					{
						mustSaveChanges = true;
					}
					else if (theDialogResult == DialogResult.No)
					{
						mustSaveChanges = false;
					}
					else
					{
						_HasUserCancelledLastOperation = true;
					}
				}
		
				if ( (!showSaveUnsavedChangesQuestion) ||
					(showSaveUnsavedChangesQuestion && mustSaveChanges)
					)
				{
					SaveForm theSaveForm = new SaveForm ();

					// Fill the save form with all files that have been changed.
					if (_HasProjectChanged)
					{
						theSaveForm.AddChangedItem(ProjectFileName);
					}

					for (int i = 0 ; i < GetNrSessions(); i++)
					{
						if (GetSessionChanged(GetSession (i)))
						{
							theSaveForm.AddChangedItem (GetSession(i).SessionFileName);
						}
					}

					// Show the save form to the user.
					DialogResult theDialogResult = theSaveForm.ShowDialog();

					if (theDialogResult == DialogResult.Cancel)
						// User pressed cancel.
					{
						_HasUserCancelledLastOperation = true;
						return;
					}
					else
						// User did not press cancel.
					{
						foreach (string theFileName in theSaveForm.ItemsToSave)
						{
							if (theFileName == ProjectFileName)
							{
								SaveProject();
							}
							else
							{
								SaveSession(theFileName);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Save the session.
		/// </summary>
		/// <param name="theSessionFileName">File name of the session.</param>
		public void SaveSession(string theSessionFileName)
		{
			foreach (SessionInformation theSessionInformation in _LoadedSessions)
			{
				// If this is the session corresponding to the supplied session file naam, save it.
				if (theSessionInformation.Session.SessionFileName == theSessionFileName)
				{
					bool isSaved = false;

					if ((File.GetAttributes(theSessionFileName) & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
					{
						isSaved = theSessionInformation.Session.SaveToFile();
						if (!isSaved)
						{
							MessageBox.Show(string.Format("File \"{0}\" is not saved successfully.", theSessionFileName));
						}
					}
					else
					{
						MessageBox.Show(string.Format("File \"{0}\" is not saved successfully.\nIt is Read only", theSessionFileName));
					}

					if (isSaved)
					{
						theSessionInformation.HasChanged = false;
					}
				}
			}		
		}

		/// <summary>
		/// Save the session under a new file name.
		/// 
		/// A new session object will be created from this new saved file (and returned by this method) 
		/// and added to the project. The original session wil not be saved.
		/// </summary>
		/// <param name="theCurrentSession"></param>
		/// <returns>The new created session object, null if save as a new session has been cancelled or failed.</returns>
		public Dvtk.Sessions.Session SaveSessionAs(Dvtk.Sessions.Session theCurrentSession)
		{
			Dvtk.Sessions.Session theNewSession = null;

			SaveFileDialog theSaveFileDialog = new SaveFileDialog();
			theSaveFileDialog.AddExtension = true;
			theSaveFileDialog.DefaultExt = ".ses";
			theSaveFileDialog.OverwritePrompt = false;
			theSaveFileDialog.Filter = "All session files (*.ses)|*.ses";

			DialogResult theDialogResult = theSaveFileDialog.ShowDialog();

			// User has specified a file name and has pressed the OK button.
			if (theDialogResult == DialogResult.OK)
			{
				if (File.Exists(theSaveFileDialog.FileName))
				{
					MessageBox.Show(string.Format("File name \"{0}\" already exists.\nOperation cancelled", theSaveFileDialog.FileName));
				}
				else
				{
					// Save the current session to a new file.
					string theCurrentSessionFullFileName = theCurrentSession.SessionFileName;

					theCurrentSession.SessionFileName = theSaveFileDialog.FileName;
					theCurrentSession.SaveToFile();
					theCurrentSession.SessionFileName = theCurrentSessionFullFileName;


					// Create a new session object from this new saved file and replace the current session.
					theNewSession = LoadSession(theSaveFileDialog.FileName);

					if (theNewSession != null)
					{
						int theCurrentIndex = GetLoadedSessionsIndex(theCurrentSession);

						_LoadedSessions[theCurrentIndex]  = new SessionInformation(theNewSession);
						_HasProjectChanged = true;
					}
				}
			}

			return(theNewSession);
		}

		public void Close(bool saveChanges)
		{
			_HasUserCancelledLastOperation = false;

			if (AreProjectOrSessionsChanged() && saveChanges)
			{
				Save(true);
			}

			if (!_HasUserCancelledLastOperation)
			{
				_HasProjectChanged = false;
				_HasUserCancelledLastOperation = false;
				_IsProjectConstructed = false;
				_LoadedSessions.Clear();
				_ProjectFileName = "";
			}
		}

		public bool HasUserCancelledLastOperation()
		{
			return(_HasUserCancelledLastOperation);
		}

		private SessionInformation GetSessionInformation(Dvtk.Sessions.Session theSession)
		{
			SessionInformation theSessionInformation = null;

			int theIndex = GetLoadedSessionsIndex(theSession);

			if (theIndex != -1)
			{
				theSessionInformation = GetSessionInformation(theIndex);
			}

			return(theSessionInformation);
		}

		private SessionInformation GetSessionInformation(int theIndex)
		{
			SessionInformation theSessionInformation = null;

			if ((theIndex < 0) || (theIndex >= _LoadedSessions.Count))
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{
				theSessionInformation = (SessionInformation)(_LoadedSessions[theIndex]);
			}

			return(theSessionInformation);
		}
		
		private int GetLoadedSessionsIndex(Dvtk.Sessions.Session theSession)
		{
			int theReturnValue = -1;

			for (int theIndex = 0; theIndex < _LoadedSessions.Count; theIndex++)
			{
				SessionInformation theSessionInformation = (SessionInformation)_LoadedSessions[theIndex];

				if (theSessionInformation.Session == theSession)
				{
					theReturnValue = theIndex;
					break;
				}
			}

			return(theReturnValue);
		}

		/// <summary>
		/// Show a dialog in which the user can determine to save (some) changes, save no changes or cancel
		/// the operation.
		/// 
		/// Precondition: unsaved changes exist.
		/// </summary>
		/// <returns>Boolean indicating if the operation should continue.</returns>
		private DialogResult ShowSaveUnsavedChangesQuestion()
		{
			DialogResult theDialogResult = DialogResult.OK;
			string whatHasChangedText = "";
			int theNumberOfChangesSessions = GetNumberOfChangedSessions();

			if (HasProjectChanged)
			{
				if (theNumberOfChangesSessions > 0)
				{
					if (theNumberOfChangesSessions == 1)
					{
						whatHasChangedText = "The project and one session have unsaved changes.";
					}
					else
					{
						whatHasChangedText = string.Format("The project and {0} sessions have unsaved changes.", theNumberOfChangesSessions);
					}
				}
				else
				{
					whatHasChangedText = "The Project has unsaved changes.";
				}
			}
			else
			{
				if (theNumberOfChangesSessions > 0)
				{
					if (theNumberOfChangesSessions == 1)
					{
						whatHasChangedText = "One session has unsaved changes.";
					}
					else
					{
						whatHasChangedText = string.Format("{0} sessions have unsaved changes.", theNumberOfChangesSessions);
					}
				}
				else
				{
					// Sanity check.
					theDialogResult = DialogResult.None;
					Debug.Assert(false);
					return(theDialogResult);
				}
			}

			theDialogResult = MessageBox.Show(whatHasChangedText + "\n\n" + "Do you want to save the changes?", "Unsaved changes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation);

			return(theDialogResult);
		}

		private class SessionInformation
		{
			private Dvtk.Sessions.Session _Session = null;
			private bool _HasChanged = false;

			public SessionInformation(Dvtk.Sessions.Session theSession)
			{
				_Session = theSession;
			}

			public bool HasChanged
			{
				get
				{
					return _HasChanged;
				}
				set
				{
					_HasChanged = value;
				}
			}

			public Dvtk.Sessions.Session Session
			{
				get
				{
					return _Session;
				}
			}
		}

		/// <summary>
		/// Property indicating if a project is loaded.
		/// </summary>
		public bool IsProjectConstructed
		{
			get
			{
				return _IsProjectConstructed;
			}
		}

		/// <summary>
		/// Construct a new empty Project that has not been saved yet to the supplied Project file.
		/// 
		/// Precondition: no project is constructed.
		/// </summary>
		/// <param name="theProjectFileName">The name of the Project file.</param>
		public void New(string theProjectFileName)
		{
			Debug.Assert(_IsProjectConstructed == false, "Project is constructed when calling Project.New(...)");

			_HasProjectChanged = true;
			_HasUserCancelledLastOperation = false;
			_IsProjectConstructed = true;
			_LoadedSessions.Clear();
			_ProjectFileName = theProjectFileName;
		}
	}
}
