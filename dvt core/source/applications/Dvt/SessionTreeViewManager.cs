// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using DvtkScriptSupport;
using Dvtk.Sessions;
using DvtTreeNodeTag;

namespace Dvt
{

	/// <summary>
	/// 
	/// </summary>
	public class SessionTreeViewManager
	{
		/// <summary>
		/// The parent form in which the tree view is embedded.
		/// </summary>
		ProjectForm2 _ParentForm = null;

		MainForm _MainForm = null;

		Project _Project = null;
		TreeNode tempTreeNodeValue = null;

		/// <summary>
		/// The tree view that is manager by this class.
		/// </summary>
		System.Windows.Forms.TreeView _SessionTreeView; 

		EndExecution _EndExecution = null;
		NotifyDelegate _NotifyDelegate;

		StorageSCUEmulatorForm  _StorageSCUEmulatorForm;
		System.AsyncCallback _StorageSCUEmulatorFormAsyncCallback;

		string _FirstMediaFileToValidate = "";

		NodesInformation _NodesInformation = new NodesInformation();

        bool reloadHtml = true;

		public void AfterSelect()
		{
            if (reloadHtml) {
                if (_UpdateCount == 0) {
                    NotifySessionTreeViewSelectionChange(GetSelectedNode());

                    // Make sure the tree node regains the focus.
                    _SessionTreeView.Focus();
                }
            }
		}

		/// <summary>
		/// Must be called when a tree node is double clicked.
		/// </summary>
		public void DoubleClick()
		{
			TreeNodeTag theTreeNodeTag = GetSelectedTreeNodeTag();

			if ( (theTreeNodeTag is ScriptFileTag) ||
				(theTreeNodeTag is EmulatorTag) ||
				(theTreeNodeTag is MediaSessionTag)
				)
			{
				Execute();
			}
		}

		public void Execute()
		{
			string errorTextFromIsFileInUse;

			if (!Directory.Exists(GetSelectedSession().ResultsRootDirectory))
			{
				MessageBox.Show("The results directory specified for this session is not valid.\nExecution is cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			else if (IsFileInUse(GetSelectedTreeNodeTag(), out errorTextFromIsFileInUse))
			{
				string firstPartWarningText = "";

				if (GetSelectedTreeNodeTag() is ScriptFileTag)
				{
					firstPartWarningText = "Unable to execute script.\n\n";
				}
				else if (GetSelectedTreeNodeTag() is EmulatorTag)
				{
					firstPartWarningText = "Unable to execute emulator.\n\n";
				}

				MessageBox.Show(firstPartWarningText + errorTextFromIsFileInUse + "\n\n(hint: change the session ID to obtain a different results file name)", "Warning");
			}
			else
			{
				_TagThatIsBeingExecuted  = GetSelectedTreeNodeTag();

				// Update the UI.
				StartExecution theStartExecution = new StartExecution(GetSelectedNode());
				Notify(theStartExecution);

				// If this is a script file tag, start execution of the script.
				if (_TagThatIsBeingExecuted is ScriptFileTag)
				{
					ExecuteSelectedScript();
				}

				// If this is a emulator tag, start execution of the correct emulator.
				if (_TagThatIsBeingExecuted is EmulatorTag)
				{
					ExecuteSelectedEmulator();
				}

				// If this is a media session tag, start execution of the media validator.
				if (_TagThatIsBeingExecuted is MediaSessionTag)
				{
					ExecuteSelectedMediaSession();
				}
			}
		}

		public void ExecuteSelectedScript()
		{
			ScriptFileTag theScriptFileTag = GetSelectedTreeNodeTag() as ScriptFileTag;

			if (theScriptFileTag == null)
				// Sanity check.
			{
				Debug.Assert(false);
			}
			else
			{
				bool isExecutionCancelled = false;

				// Remove the current results files for this script file.
				// If results files exists that will be removed, ask the user what to do with them.
				ArrayList theResultsFilesToRemove = ResultsFile.GetAllNamesForSession(theScriptFileTag._Session);
				theResultsFilesToRemove = ResultsFile.GetNamesForScriptFile(theScriptFileTag._ScriptFileName, theResultsFilesToRemove);
				theResultsFilesToRemove = ResultsFile.GetNamesForCurrentSessionId(theScriptFileTag._Session, theResultsFilesToRemove);

				if (theResultsFilesToRemove.Count != 0)
				{
					string theWarningMessage = string.Format("Results files exist that will be removed before execution of script file {0}.\nCopy these results files to backup files?", theScriptFileTag._ScriptFileName);
					DialogResult theDialogResult = DialogResult.No;
	
					// Only ask to backup the results file if this is configured.
					if (_MainForm._UserSettings.AskForBackupResultsFile)
					{
						theDialogResult = MessageBox.Show(theWarningMessage, "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					}

					if (theDialogResult == DialogResult.Yes)
					{
						ResultsFile.BackupFiles(theScriptFileTag._Session, theResultsFilesToRemove);
						ResultsFile.Remove(theScriptFileTag._Session, theResultsFilesToRemove);
					}
					else if (theDialogResult == DialogResult.No)
					{
						ResultsFile.Remove(theScriptFileTag._Session, theResultsFilesToRemove);
					}
					else
					{
						_TagThatIsBeingExecuted  = null;

						// Update the UI.
						EndExecution theEndExecution = new EndExecution(GetSelectedTreeNodeTag());
						Notify(theEndExecution);

						isExecutionCancelled = true;
					}
				}

				if (!isExecutionCancelled)
				{
					if ( (System.IO.Path.GetExtension(theScriptFileTag._ScriptFileName).ToLower() == ".dss") ||
						(System.IO.Path.GetExtension(theScriptFileTag._ScriptFileName).ToLower() == ".ds")
						)
					{
						ExecuteDicomScriptInThread(theScriptFileTag);
					}
					else if (System.IO.Path.GetExtension(theScriptFileTag._ScriptFileName).ToLower() == ".vbs")
					{
						ExecuteVisualBasicScriptInThread(theScriptFileTag);
					}
					else
					{
						MessageBox.Show("Execution of this type of file not supported (yet)");

						_TagThatIsBeingExecuted = null;

						// Update the UI.
						EndExecution theEndExecution = new EndExecution(GetSelectedTreeNodeTag());
						Notify(theEndExecution);
					}
				}
			}
		}

		private void ExecuteSelectedEmulator()
		{
			Dvtk.Sessions.EmulatorSession theEmulatorSession = GetSelectedSession() as Dvtk.Sessions.EmulatorSession;

			if (theEmulatorSession == null)
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{		
				EmulatorTag theEmulatorTag = (EmulatorTag)GetSelectedTreeNodeTag();
				string theResultsFileName = null;
				bool isExecutionCancelled = false;
				
				// Remove the current results files for the emulator.
				// If results files exists that will be removed, ask the user what to do with them.
				ArrayList theResultsFilesToRemove = ResultsFile.GetAllNamesForSession(theEmulatorSession);
				string theEmulatorBaseName = ResultsFile.GetBaseNameForEmulator(theEmulatorTag._EmulatorType);
				theResultsFilesToRemove = ResultsFile.GetNamesForBaseName(theEmulatorBaseName, theResultsFilesToRemove);
				theResultsFilesToRemove = ResultsFile.GetNamesForCurrentSessionId(theEmulatorSession, theResultsFilesToRemove);

				if (theResultsFilesToRemove.Count != 0)
				{
					string theWarningMessage = string.Format("Results files exist that will be removed before execution of the emulator.\nCopy these results files to backup files?");
					DialogResult theDialogResult = DialogResult.No;
	
					// Only ask to backup the results file if this is configured.
					if (_MainForm._UserSettings.AskForBackupResultsFile)
					{
						theDialogResult = MessageBox.Show(theWarningMessage, "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					}

					if (theDialogResult == DialogResult.Yes)
					{
						ResultsFile.BackupFiles(theEmulatorSession, theResultsFilesToRemove);
						ResultsFile.Remove(theEmulatorSession, theResultsFilesToRemove);
					}
					else if (theDialogResult == DialogResult.No)
					{
						ResultsFile.Remove(theEmulatorSession, theResultsFilesToRemove);
					}
					else
					{
						_TagThatIsBeingExecuted  = null;

						// Update the UI.
						EndExecution theEndExecution = new EndExecution(GetSelectedTreeNodeTag());
						Notify(theEndExecution);

						isExecutionCancelled = true;
					}
				}
			
				if (!isExecutionCancelled)
				{
					// Determine the results file name.
					theResultsFileName =  ResultsFile.GetExpandedNameForEmulator(theEmulatorSession, theEmulatorTag._EmulatorType);

					// Set the correct SCP type.
					if (theEmulatorTag._EmulatorType == EmulatorTag.EmulatorType.PRINT_SCP)
					{
						theEmulatorSession.ScpEmulatorType = DvtkData.Results.ScpEmulatorType.Printing;
					}

					if (theEmulatorTag._EmulatorType == EmulatorTag.EmulatorType.STORAGE_SCP)
					{
						theEmulatorSession.ScpEmulatorType = DvtkData.Results.ScpEmulatorType.Storage;
					}

					// Start the results gathering.
					theEmulatorSession.StartResultsGathering(theResultsFileName);

					// If this is the print SCP emulator or the storage SCP emulator...
					if ( (theEmulatorTag._EmulatorType == EmulatorTag.EmulatorType.PRINT_SCP) ||
						(theEmulatorTag._EmulatorType == EmulatorTag.EmulatorType.STORAGE_SCP) )
					{
						// Perform the actual execution of the script.
						AsyncCallback theAsyncCallback = new AsyncCallback(this.ResultsFromExecutingEmulatorScpAsynchronously);
						theEmulatorSession.BeginEmulateSCP(theAsyncCallback);
					}

					// If this is the storage SCU emulator...
					if (theEmulatorTag._EmulatorType == EmulatorTag.EmulatorType.STORAGE_SCU)
					{
						DialogResult theDialogResult = _StorageSCUEmulatorForm.ShowDialog(_ParentForm, theEmulatorSession);

						if (theDialogResult == DialogResult.Cancel)
						{
							// No sending of Dicom files is happening now.

							// Save the results.
							GetSelectedSession().EndResultsGathering();

							_TagThatIsBeingExecuted  = null;

							// Update the UI.
							EndExecution theEndExecution= new EndExecution(GetSelectedTreeNodeTag());
							Notify(theEndExecution);
						}
						else
						{
							// Dicom files are being send in another thread.
							// Do nothing, let the call back method handle the enabling of the session in the UI.
						}
					}
				}
			}
		}

		private System.Collections.Queue mediaFilesToBeValidated =
			System.Collections.Queue.Synchronized(new System.Collections.Queue());

		private void ExecuteSelectedMediaSession()
		{
			ArrayList theMediaFilesToBeValidatedLocalList = new ArrayList();

			Dvtk.Sessions.MediaSession theMediaSession = GetSelectedSession() as Dvtk.Sessions.MediaSession;

			if (theMediaSession == null)
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{
				mediaFilesToBeValidated.Clear();

				OpenFileDialog theOpenFileDialog = new OpenFileDialog();

				theOpenFileDialog.Filter = "All files (*.*)|*.*";
				theOpenFileDialog.Multiselect = true;
				theOpenFileDialog.ReadOnlyChecked = true;
				theOpenFileDialog.Title = "Select media files to validate";

				// Show the file dialog.
				// If the user pressed the OK button...
				if (theOpenFileDialog.ShowDialog() == DialogResult.OK)
				{
					// Validate all files selected.
					foreach (string theFullFileName in theOpenFileDialog.FileNames)
					{
						mediaFilesToBeValidated.Enqueue(theFullFileName);
						theMediaFilesToBeValidatedLocalList.Add(theFullFileName);
					}
				}

				if (mediaFilesToBeValidated.Count == 0)
					// No files selected, so no media validation to perform.
					// Update UI.
				{
					_TagThatIsBeingExecuted  = null;
				
					EndExecution theEndExecution = new EndExecution(GetSelectedTreeNodeTag());
					Notify(theEndExecution);
				}
				else
				{
					bool isExecutionCancelled = false;

					// Remove the current results files for the selected media files.
					// If results files exists that will be removed, ask the user what to do with them.
					ArrayList theResultsFilesForSession = ResultsFile.GetAllNamesForSession(theMediaSession);
					ArrayList theResultsFilesToRemove = new ArrayList();

					foreach(string theMediaFullFileName in theMediaFilesToBeValidatedLocalList)
					{
						string theMediaFileBaseName = ResultsFile.GetBaseNameForMediaFile(theMediaFullFileName);

						ArrayList theResultsFilesToRemoveForMediaFile = ResultsFile.GetNamesForBaseName(theMediaFileBaseName, theResultsFilesForSession);
						theResultsFilesToRemoveForMediaFile = ResultsFile.GetNamesForCurrentSessionId(theMediaSession, theResultsFilesToRemoveForMediaFile);

						theResultsFilesToRemove.AddRange(theResultsFilesToRemoveForMediaFile);
					}

					if (theResultsFilesToRemove.Count != 0)
					{
						string theWarningMessage = string.Format("Results files exist that will be removed before media validation.\nCopy these results files to backup files?");
						DialogResult theDialogResult = DialogResult.No;
	
						// Only ask to backup the results file if this is configured.
						if (_MainForm._UserSettings.AskForBackupResultsFile)
						{
							theDialogResult = MessageBox.Show(theWarningMessage, "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						}

						if (theDialogResult == DialogResult.Yes)
						{
							ResultsFile.BackupFiles(theMediaSession, theResultsFilesToRemove);
							ResultsFile.Remove(theMediaSession, theResultsFilesToRemove);
						}
						else if (theDialogResult == DialogResult.No)
						{
							ResultsFile.Remove(theMediaSession, theResultsFilesToRemove);
						}
						else
						{
							_TagThatIsBeingExecuted  = null;

							// Update the UI.
							EndExecution theEndExecution = new EndExecution(GetSelectedTreeNodeTag());
							Notify(theEndExecution);

							isExecutionCancelled = true;
						}
					}

					if (!isExecutionCancelled)
					{
						_FirstMediaFileToValidate = (string)mediaFilesToBeValidated.Peek();
						ValidateMediaFiles();
					}
				}
			}
			
		}

		public void GenerateDICOMDIR()
		{
			OpenFileDialog theOpenFileDialog = new OpenFileDialog();

			theOpenFileDialog.Filter = "DICOM media files (*.dcm)|*.dcm|All files (*.*)|*.*";
			theOpenFileDialog.Title = "Select DCM files to create DICOMDIR";
			theOpenFileDialog.Multiselect = true;
			theOpenFileDialog.ReadOnlyChecked = true;

			// Show the file dialog.
			// If the user pressed the OK button...
			if (theOpenFileDialog.ShowDialog() == DialogResult.OK)
			{
				// Add all DCM files selected.
				string [] dcmFiles = new string [theOpenFileDialog.FileNames.Length];
				Dvtk.Sessions.MediaSession theMediaSession = GetSelectedSession() as Dvtk.Sessions.MediaSession;
				if (theMediaSession == null)
				{
					// Sanity check.
					Debug.Assert(false);
				}

				// Move all selected DCM files to directory "DICOM" in result root directory.
				int i = 0;
				DirectoryInfo theDirectoryInfo = null;
				theDirectoryInfo = new DirectoryInfo(theMediaSession.ResultsRootDirectory + "DICOM\\");

				// Create "DICOM" directory if it doesn't exist
				if(!theDirectoryInfo.Exists)
				{
					theDirectoryInfo.Create();
				}
				else // Remove existing DCM files from "DICOM" directory
				{
					FileInfo[] files = theDirectoryInfo.GetFiles();
					foreach(FileInfo file in files)
					{
						file.Delete();
					}
				}

				foreach(string dcmFile in theOpenFileDialog.FileNames)
				{
					FileInfo theFileInfo =  new FileInfo(dcmFile);
					string destFileName = theDirectoryInfo.FullName + string.Format("IM{0:D4}",i);//theFileInfo.Name;
					theFileInfo.CopyTo(destFileName,true);
					dcmFiles.SetValue(destFileName,i);
					i++;
				}
	
				theMediaSession.StartResultsGathering("DICOMDIR_res.xml");
				theMediaSession.GenerateDICOMDIR(dcmFiles);
				theMediaSession.EndResultsGathering();
				
				// Update the UI.
				_TagThatIsBeingExecuted  = null;

				EndExecution theEndExecution = new EndExecution(GetSelectedTreeNodeTag());
				Notify(theEndExecution);
			}
		}

		private void ValidateMediaFiles()
		{
			lock (this)
			{
				MediaSessionTag theMediaSessionTag = (MediaSessionTag)_TagThatIsBeingExecuted;
				Dvtk.Sessions.MediaSession theMediaSession = 
					(Dvtk.Sessions.MediaSession)theMediaSessionTag._Session;

				if (mediaFilesToBeValidated.Count > 0)
				{
					string theFullFileName = (string)mediaFilesToBeValidated.Dequeue();

					// Determine the results file name.
					string theExpandedResultsFileName = ResultsFile.GetExpandedNameForMediaFile(theMediaSession, theFullFileName);

					// Start the results gathering.
					theMediaSession.StartResultsGathering(theExpandedResultsFileName);

					string[] mediaFilesToValidate = new string[] { theFullFileName };

					// Perform the actual execution of the script.
					AsyncCallback theValidateMediaFilesAsyncCallback = new AsyncCallback(this.ResultsFromValidateMediaFilesAsynchronously);
					theMediaSession.BeginValidateMediaFiles(mediaFilesToValidate, theValidateMediaFilesAsyncCallback);				
				} // foreach: Validate all files selected.
			}
		}

		public void ResultsFromValidateMediaFilesAsynchronously(IAsyncResult theIAsyncResult)
		{
			Dvtk.Sessions.MediaSession theMediaSession = (Dvtk.Sessions.MediaSession)GetExecutingSession();

			try
			{
				// Obligated to call the following method according to the asynchronous design pattern.
				theMediaSession.EndValidateMediaFiles(theIAsyncResult);
			}
			catch (Exception ex)
			{
				//
				// Problem:
				// Errors thrown from a workerthread are eaten by the .NET 1.x CLR.
				// Workaround:
				// Directly call the global (untrapped) exception handler callback.
				// Do NOT rely on 
				// either
				// - System.AppDomain.CurrentDomain.UnhandledException
				// or
				// - System.Windows.Forms.Application.ThreadException
				// These events will only be triggered for the main thread not for worker threads.
				//
				CustomExceptionHandler eh = new CustomExceptionHandler();
				System.Threading.ThreadExceptionEventArgs args = new ThreadExceptionEventArgs(ex);
				eh.OnThreadException(this, args);
				//
				// Rethrow. This rethrow may work in the future .NET 2.x CLR.
				// Currently eaten.
				//
				throw ex;
			}

			theMediaSession.EndResultsGathering();

			if (mediaFilesToBeValidated.Count > 0)
			{
				ValidateMediaFiles();
			}
			else
			{
				// Update the UI. Do this with an invoke, because the thread that is calling this
				// method is NOT the thread that created all controls used!
				_EndExecution = new EndExecution(_TagThatIsBeingExecuted);

				_TagThatIsBeingExecuted  = null;

				_NotifyDelegate = new NotifyDelegate(_ParentForm.Notify);
				_ParentForm.Invoke(_NotifyDelegate, new object[]{_EndExecution});
			}
		}

		public void Notify(object theEvent)
		{
			_ParentForm.Notify(theEvent);
		}

		public void Update(object theSource, object theEvent)
		{
			_UpdateCount++;

			if (theEvent is UpdateAll)
			{
				OnUpdateAll(theSource, theEvent);
			}

			if (theEvent is ClearAll)
			{
				OnClearAll(theSource, theEvent);
			}

			if (theEvent is StartExecution)
			{
				OnStartExecution(theSource, theEvent);
			}

			if (theEvent is EndExecution)
			{
				OnEndExecution(theSource, theEvent);
			}

			if ( (theEvent is StopExecution) ||
				(theEvent is StopAllExecution)
				)
			{
				OnStopExecution(theSource, theEvent);
			}

			if (theEvent is SessionChange)
			{
				OnSessionChanged(theSource, theEvent);
			}

			if (theEvent is SessionRemovedEvent)
			{
				OnSessionRemovedEvent(theSource, theEvent);
			}

			if (theEvent is SessionReplaced)
			{
				OnSessionReplaced(theSource, theEvent);
			}
			
			if (theEvent is Saved)
			{
				OnSaved(theSource, theEvent);
			}
			
			_UpdateCount--;
		}

		private void OnUpdateAll(object theSource, object theEvent)
		{
			// Disable drawing of the session tree.
			_SessionTreeView.BeginUpdate();

			UpdateAll theUpdateAllEvent = (UpdateAll)theEvent;

			UpdateSessionTreeView();

			if (theUpdateAllEvent._RestoreSessionTreeState)
			{
				foreach (TreeNode theTreeNode in _SessionTreeView.Nodes)
				{
					_NodesInformation.RestoreExpandInformation(theTreeNode);
				}

				_NodesInformation.RemoveAllExpandInformation();

				_NodesInformation.RestoreSelectedNode(_SessionTreeView);
			}
			else
			{
				if (_SessionTreeView.Nodes.Count > 0)
				{
					_SessionTreeView.SelectedNode = _SessionTreeView.Nodes[0];
				}
			}

			// Enable drawing of the session tree.
			_SessionTreeView.EndUpdate();

			SessionTreeViewSelectionChange theSessionTreeViewSelectionChange = new SessionTreeViewSelectionChange(GetSelectedNode());
			Notify(theSessionTreeViewSelectionChange);
		}

		private void OnClearAll(object theSource, object theEvent)
		{
			ClearAll theClearAllEvent = (ClearAll)theEvent;

			if (theClearAllEvent._StoreSessionTreeState)
			{
				foreach (TreeNode theTreeNode in _SessionTreeView.Nodes)
				{
					_NodesInformation.StoreExpandInformation(theTreeNode);
				}

				_NodesInformation.StoreSelectedNode(_SessionTreeView);

			}
		
			ClearSessionTreeView();

			SessionTreeViewSelectionChange theSessionTreeViewSelectionChange = new SessionTreeViewSelectionChange(GetSelectedNode());
			Notify(theSessionTreeViewSelectionChange);
		}
	
		private void OnStartExecution(object theSource, object theEvent)
		{
			TreeNodeTag theOriginallySelectedTag = GetSelectedTreeNodeTag();

			// The tree node that is being executed.
			TreeNode theTreeNode = ((StartExecution)theEvent).TreeNode;

			// The tag of the tree node that is being executed.
			TreeNodeTag theTreeNodeTag = (TreeNodeTag)theTreeNode.Tag;

			// If the session tree view managed by this class has initiated the execution,
			// disable the complete session tree view.
			if (theSource == _ParentForm)
			{
				// Store the expand information for the node and all sub nodes.
				TreeNode theSessionTreeNode = GetSessionNode(theTreeNodeTag._Session);
				_NodesInformation.StoreExpandInformation(theSessionTreeNode);

				if 	(theTreeNodeTag is ScriptFileTag)
				{
					UpdateScriptFileNode(theTreeNode, theTreeNodeTag._Session, ((ScriptFileTag)theTreeNodeTag)._ScriptFileName, new ArrayList(), true);
				}
				else if (theTreeNodeTag is EmulatorTag)
				{
					UpdateEmulatorNode(theTreeNode, theTreeNodeTag._Session, new ArrayList(), ((EmulatorTag)theTreeNodeTag)._EmulatorType, true);
				} 
				else if (theTreeNodeTag is MediaSessionTag)
				{
					UpdateMediaSessionNode(theTreeNode, theTreeNodeTag._Session);
				}
				else
				{
					throw new System.ApplicationException("Unknown tag in OnStartExecution");
				}

				_SessionTreeView.Enabled = false;
			}
				// If the session tree view managed by this class has NOT initiated the execution,
				// disable one session.
			else
			{
				TreeNode theSessionTreeNodeToDisable = GetSessionNode(theTreeNodeTag._Session);

				// Store the expand information for the node and all sub nodes.
				_NodesInformation.StoreExpandInformation(theSessionTreeNodeToDisable);

				if (theSessionTreeNodeToDisable != null)
				{
					bool isEmpty = false;

					UpdateSessionNode(theSessionTreeNodeToDisable, theTreeNodeTag._Session, ref isEmpty);
					theSessionTreeNodeToDisable.Collapse();
				}
			}

			// If, by calling an Update...Node, the selected tag has changed, 
			// the control tab also needs updating.
			if (theOriginallySelectedTag != GetSelectedTreeNodeTag())
			{
				NotifySessionTreeViewSelectionChange(GetSelectedNode());
			}
		}
		
		private void OnEndExecution(object theSource, object theEvent)
		{
			TreeNodeTag theOriginallySelectedTag = GetSelectedTreeNodeTag();

			// The tag of the tree node that is being executed.
			TreeNodeTag theTreeNodeTag = ((EndExecution)theEvent)._Tag;

			// Refresh the complete session node.
			TreeNode theSessionTreeNodeToRefresh = GetSessionNode(theTreeNodeTag._Session);

			if (theSessionTreeNodeToRefresh != null)
			{
				bool isEmpty = false;

				UpdateSessionNode(theSessionTreeNodeToRefresh, theTreeNodeTag._Session, ref isEmpty);

				// Expand back the nodes and sub nodes.
				_NodesInformation.RestoreExpandInformation(theSessionTreeNodeToRefresh);
				_NodesInformation.RemoveExpandInformationForSession(theTreeNodeTag._Session);

				if (theSource == _ParentForm)
				{
					// After execution, select and expand a summary results file.
					string theCurrentSummaryResultsFileName = "";

					if (theTreeNodeTag is ScriptFileTag)
					{
						ScriptFileTag theScriptFileTag = (ScriptFileTag)theTreeNodeTag;
						theCurrentSummaryResultsFileName =  ResultsFile.GetSummaryNameForScriptFile(theTreeNodeTag._Session, theScriptFileTag._ScriptFileName);
					}
					else if (theTreeNodeTag is EmulatorTag)
					{
						EmulatorTag theEmulatorTag = (EmulatorTag)theTreeNodeTag;
						theCurrentSummaryResultsFileName = ResultsFile.GetSummaryNameForEmulator(theTreeNodeTag._Session, theEmulatorTag._EmulatorType);
					}
					else if (theTreeNodeTag is MediaSessionTag)
					{
						MediaSessionTag theMediaSessionTag = (MediaSessionTag)theTreeNodeTag;
						theCurrentSummaryResultsFileName =  ResultsFile.GetSummaryNameForMediaFile(theTreeNodeTag._Session, _FirstMediaFileToValidate);
					}
					else
					{
						// Not supposed to get here.
						MessageBox.Show("OnEndExecution: Not supposed to get here" );
						Debug.Assert(false);
					}

					ExpandAndSelectResultFile(theSessionTreeNodeToRefresh, theCurrentSummaryResultsFileName);
					
					_SessionTreeView.Enabled = true;
				}

				// If, by calling an Update...Node, the selected tag has changed, 
				// the control tab also needs updating.
				if (theOriginallySelectedTag != GetSelectedTreeNodeTag())
				{
					NotifySessionTreeViewSelectionChange(GetSelectedNode());
				}	
			}
			else
			{
				// Sanity check.
				MessageBox.Show("Session node not found.");
			}
		}

		private void OnStopExecution(object theSource, object theEvent)
		{
			if ( ((theEvent is StopExecution) && ( ((StopExecution)theEvent)._ProjectForm == _ParentForm)) ||
			     (theEvent is StopAllExecution)
			   )
			{	
				// If execution is going on...
				if (_MainForm.IsExecuting(GetSelectedSession()))
				{
					Dvtk.Sessions.ScriptSession theScriptSession = GetSelectedSession() as Dvtk.Sessions.ScriptSession;
					Dvtk.Sessions.EmulatorSession theEmulatorSession = GetSelectedSession() as Dvtk.Sessions.EmulatorSession;

					// If the selected node is a script file...
					if (theScriptSession != null)
					{
						theScriptSession.TerminateConnection();

						theScriptSession.WriteError("stop button pressed.");
					}

					// If the selected node is an emulator...
					if (theEmulatorSession != null)
					{
						theEmulatorSession.TerminateConnection();
					}
				}
			}
		}

		private void OnSessionChanged(object theSource, object theEvent)
		{
			SessionChange theSessionChange = (SessionChange)theEvent;

			TreeNode theTreeNode = GetSessionNode(theSessionChange.Session);

			if (theTreeNode != null)
			{
				if ( (theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.RESULTS_DIR) ||
			 	     (theSessionChange.SessionChangeSubTyp == SessionChange.SessionChangeSubTypEnum.SCRIPTS_DIR)
				   )
				{
					bool isEmpty = false;

					UpdateSessionNode(theTreeNode, theSessionChange.Session, ref isEmpty);
				}
				else
				{
					UpdateSessionNodeTextMainNodeOnly(theTreeNode, theSessionChange.Session);
				}
			}
		}

		private void OnSessionRemovedEvent(object theSource, object theEvent)
		{
			SessionRemovedEvent theSessionRemovedEvent = (SessionRemovedEvent)theEvent;

			for (int theIndex = 0; theIndex < _SessionTreeView.Nodes.Count; theIndex++)
			{
				TreeNodeTag theTreeNodeTag = (TreeNodeTag)(_SessionTreeView.Nodes[theIndex].Tag);

				if (theTreeNodeTag._Session == theSessionRemovedEvent._Session)
				{
					_SessionTreeView.Nodes.RemoveAt(theIndex);
					break;
				}
			}
		}

		private void OnSessionReplaced(object theSource, object theEvent)
		{
			bool isEmpty = false;

			SessionReplaced theSessionReplaced = (SessionReplaced)theEvent;

			TreeNode theSessionNodeToUpdate = GetSessionNode(theSessionReplaced._OldSession);

			UpdateSessionNode(theSessionNodeToUpdate, theSessionReplaced._NewSession, ref isEmpty);

			NotifySessionTreeViewSelectionChange(theSessionNodeToUpdate);
		}

		private void OnSaved(object theSource, object theEvent)
		{
			foreach(TreeNode theTreeNode in _SessionTreeView.Nodes)
			{
				TreeNodeTag theTreeNodeTag = (TreeNodeTag)theTreeNode.Tag;

				UpdateSessionNodeTextMainNodeOnly(theTreeNode, theTreeNodeTag._Session);
			}
		}

		public void NotifySessionTreeViewSelectionChange(TreeNode theTreeNode)
		{
			SessionTreeViewSelectionChange theSessionTreeViewSelectionChange;
			theSessionTreeViewSelectionChange= new SessionTreeViewSelectionChange(theTreeNode);
			Notify(theSessionTreeViewSelectionChange);
		}


		public void ClearSessionTreeView()
		{
			_SessionTreeView.Nodes.Clear();
		}

		public void UpdateSessionTreeView()
		{
			_SessionTreeView.Nodes.Clear();

			// For each session present in the project, create a session node in the session tree view.
			for (int theIndex = 0; theIndex < _Project.GetNrSessions() ; theIndex++)
			{
				bool isEmpty = false;

				Dvtk.Sessions.Session theSession = _Project.GetSession(theIndex);

				TreeNode theTreeNode = new TreeNode();

				UpdateSessionNode(theTreeNode, theSession, ref isEmpty);

				if ((_MainForm._UserSettings.ShowEmptySessions) || (!isEmpty))
				{
					_SessionTreeView.Nodes.Add(theTreeNode);
				}
			}
		}

		/// <summary>
		/// Update a Session node.
		/// </summary>
		/// <param name="theTreeNode">The tree node to update.</param>
		/// <param name="theSession">The Session the node is representing.</param>
		public void UpdateSessionNode(TreeNode theTreeNode, Dvtk.Sessions.Session theSession, ref bool isEmpty)
		{
			isEmpty = false;

			UpdateSessionNodeTextMainNodeOnly(theTreeNode, theSession);

			if (theSession is Dvtk.Sessions.ScriptSession)
			{
				UpdateScriptSessionNode(theTreeNode, theSession, ref isEmpty);
			}

			if (theSession is Dvtk.Sessions.MediaSession)
			{
				UpdateMediaSessionNode(theTreeNode, theSession);
			}

			if (theSession is Dvtk.Sessions.EmulatorSession)
			{
				UpdateEmulatorSessionNode(theTreeNode, theSession);
			}
		}

		public void UpdateSessionNodeTextMainNodeOnly(TreeNode theTreeNode, Dvtk.Sessions.Session theSession)
		{
			bool isSessionExecuting = _MainForm.IsExecuting(theSession);
			bool isSessionExecutingInOtherSessionTreeView = (isSessionExecuting && (theSession != GetExecutingSession()));

			theTreeNode.Text = System.IO.Path.GetFileName(theSession.SessionFileName);

			if ( (theSession is Dvtk.Sessions.ScriptSession) ||
                 (theSession is Dvtk.Sessions.EmulatorSession)
			   )
			{
				if (isSessionExecutingInOtherSessionTreeView)
				{
					theTreeNode.Text+= " (disabled)";
				}
			}

			if (theSession is Dvtk.Sessions.MediaSession)
			{
				// If this session is executing...
				if (isSessionExecuting)
				{
					// If the executing session is executed by this session tree view...
					if (theSession == GetExecutingSession())
					{
						theTreeNode.Text+= " (executing)";
					}
						// If the executing session is not executed by this session tree view...
					else
					{
						theTreeNode.Text+= " (disabled)";
					}
				}			
			}

			if (_Project.GetSessionChanged(theSession))
			{
				theTreeNode.Text+= " *";
			}
		}

		/// <summary>
		/// Update the Script Session node (update the supplied node and create sub-nodes if necessary).
		/// 
		/// Pre-condition:
		/// - The supplied session is not executing.
		/// - The supplied session is executing in another Session Tree View.
		/// </summary>
		/// <param name="theEmulatorSessionTreeNode">The tree node representing the Script Session.</param>
		/// <param name="theSession">The session.</param>
		public void UpdateScriptSessionNode(TreeNode theScriptSessionTreeNode, Dvtk.Sessions.Session theSession, ref bool isEmpty)
		{
			bool isSessionExecuting = _MainForm.IsExecuting(theSession);
			bool isSessionExecutingInOtherSessionTreeView = (isSessionExecuting && (theSession != GetExecutingSession()));

			// Set the tag for this session tree node.
			ScriptSessionTag theScriptSessionTag = new ScriptSessionTag(theSession);
			theScriptSessionTreeNode.Tag = theScriptSessionTag;

			// Remove the old tree nodes that may be present under this session tree node.
			theScriptSessionTreeNode.Nodes.Clear();
			
			// Determine the visible scripts.
			ArrayList theVisibleScripts = GetVisibleScripts(theSession);
			isEmpty = (theVisibleScripts.Count == 0);

			// If this session is executing...
			if (isSessionExecutingInOtherSessionTreeView)
			{
				// Do nothing.
			}
			else if (!isSessionExecuting)
			{
				ArrayList theResultsFiles; // The result files belonging to this session.

				// Get all visible results file names from this session.
				theResultsFiles = ResultsFile.GetVisibleNamesForSession(theSession);

				// Create a sub-node for each script file contained in this session.
				foreach(string theVisibleScript in theVisibleScripts)
				{
					TreeNode theTreeNode = new TreeNode();
					theScriptSessionTreeNode.Nodes.Add(theTreeNode);

					UpdateScriptFileNode(theTreeNode, theSession, theVisibleScript, theResultsFiles, false);
				}
			}
			else
			{
				// Sanity check, pre-condition of this method is not fullfilled.
				Debug.Assert(false);
			}
		}

		public void UpdateScriptFileNode(TreeNode theScriptFileTreeNode, Dvtk.Sessions.Session theSession, string theScriptFileName, ArrayList theResultsFiles, bool isExecuting)
		{		
			// Set the text on this script file tree node.
			if (isExecuting)
			{
				theScriptFileTreeNode.Text = theScriptFileName + " (executing)";
			}
			else
			{
				theScriptFileTreeNode.Text = theScriptFileName;
			}

			// Set the tag for this script file tree node.
			ScriptFileTag theScriptFileTag = new ScriptFileTag(theSession);
			theScriptFileTag._ScriptFileName = theScriptFileName;

			theScriptFileTreeNode.Tag = theScriptFileTag;

			// Remove the old tree nodes that may be present under this script file tree node.
			theScriptFileTreeNode.Nodes.Clear();

			if (!isExecuting)
			{
				// For all result files that belong to this script file, create a sub node.
				// The theResultsFiles object contains all results files that have not yet been added to
				// already processed script file nodes.

				ArrayList theResultsFilesForThisScriptFile = ResultsFile.GetNamesForScriptFile(theScriptFileName, theResultsFiles);
				theResultsFiles = ResultsFile.FilterOutResultsFileNames(theResultsFiles, theResultsFilesForThisScriptFile);

				foreach (string theResultsFile in theResultsFilesForThisScriptFile)
				{
					TreeNode theResultsFileTreeNode = new TreeNode();
					theScriptFileTreeNode.Nodes.Add(theResultsFileTreeNode);

					UpdateResultsFileNode(theResultsFileTreeNode, theSession, theResultsFile);
				}
			}
		}

		public void UpdateResultsFileNode(TreeNode theResultsFileTreeNode, Dvtk.Sessions.Session theSession, string theResultsFileName)
		{
			// Set the text on this script file tree node.
			theResultsFileTreeNode.Text = theResultsFileName;

			// Set the tag for this script file tree node.
			ResultsFileTag theResultsFileTag = new ResultsFileTag(theSession);
			theResultsFileTag._ResultsFileName = theResultsFileName;

			theResultsFileTreeNode.Tag = theResultsFileTag;
		}


		public void UpdateResultsCollectionNode(TreeNode theResultsCollectionNode, string theNodeText, Dvtk.Sessions.Session theSession, ArrayList theResultsFiles)
		{
			// Set the text on this script file tree node.
			theResultsCollectionNode.Text = theNodeText;

			// Set the tag for this script file tree node.
			ResultsCollectionTag theResultsCollectionTag = new ResultsCollectionTag(theSession);
			theResultsCollectionTag._ResultsCollectionName = theNodeText;
			theResultsCollectionNode.Tag = theResultsCollectionTag;

			// Remove the old tree nodes that may be present under this node.
			theResultsCollectionNode.Nodes.Clear();

			// Add all results files not belonging to a script file to this tree node.
			foreach (string theResultFile in theResultsFiles)
			{
				TreeNode theResultsFileTreeNode = new TreeNode();
				theResultsCollectionNode.Nodes.Add(theResultsFileTreeNode);

				UpdateResultsFileNode(theResultsFileTreeNode, theSession, theResultFile);
			}
		}

		public void UpdateMediaSessionNode(TreeNode theMediaSessionTreeNode, Dvtk.Sessions.Session theSession)
		{
			bool isSessionExecuting = _MainForm.IsExecuting(theSession);

			// Set the tag for this session tree node.
			MediaSessionTag theMediaSessionTag = new MediaSessionTag(theSession);
			theMediaSessionTreeNode.Tag = theMediaSessionTag;

			// Remove the old tree nodes that may be present under this session tree node.
			theMediaSessionTreeNode.Nodes.Clear();

			if (!isSessionExecuting)
			{
				ArrayList theResultsFileNames = ResultsFile.GetVisibleNamesForSession(theSession);
				ArrayList theResultsFileBaseNames = ResultsFile.GetBaseNamesForResultsFiles(theResultsFileNames);

				foreach(string theBaseName in theResultsFileBaseNames)
				{
					string theNodeText = "";
					ArrayList theResultsFileNamesForBaseName = ResultsFile.GetNamesForBaseName(theBaseName, theResultsFileNames);
					theResultsFileNames = ResultsFile.FilterOutResultsFileNames(theResultsFileNames, theResultsFileNamesForBaseName);

					TreeNode theTreeNode = new TreeNode();
					theMediaSessionTreeNode.Nodes.Add(theTreeNode);

					if (theBaseName.ToLower().EndsWith("_DCM"))
					{
						theNodeText = theBaseName.Substring(0, theBaseName.Length - 4);
					}
					else
					{
						theNodeText = theBaseName;
					}

					UpdateResultsCollectionNode(theTreeNode, theNodeText, theSession, theResultsFileNamesForBaseName);
				}
			}
		}

		/// <summary>
		/// Update the emulator session node (update the supplied node and create sub-nodes if necessary).
		/// 
		/// Pre-condition:
		/// - The supplied session is not executing.
		/// - The supplied session is executing in another Session Tree View.
		/// </summary>
		/// <param name="theEmulatorSessionTreeNode">The tree node representing the Emulator Session.</param>
		/// <param name="theSession">The session.</param>
		public void UpdateEmulatorSessionNode(TreeNode theEmulatorSessionTreeNode, Dvtk.Sessions.Session theSession)
		{
			bool isSessionExecuting = _MainForm.IsExecuting(theSession);
			bool isSessionExecutingInOtherSessionTreeView = (isSessionExecuting && (theSession != GetExecutingSession()));

			// Set the tag for this session tree node.
			EmulatorSessionTag theEmulatorSessionTag = new EmulatorSessionTag(theSession);
			theEmulatorSessionTreeNode.Tag = theEmulatorSessionTag;

			// Remove the old tree nodes that may be present under this session tree node.
			theEmulatorSessionTreeNode.Nodes.Clear();

			// If this session is executing...
			if (isSessionExecutingInOtherSessionTreeView)
			{
				// Do nothing.
			}
			else if (!isSessionExecuting)
			{
				ArrayList theResultsFiles; // The result files belonging to this session.

				// Get all result files from this session.
				theResultsFiles = ResultsFile.GetVisibleNamesForSession(theSession);
				
				// Add the Storage SCP emulator tree node.
				TreeNode theStorageScpEmulatorTreeNode = new TreeNode();
				theEmulatorSessionTreeNode.Nodes.Add(theStorageScpEmulatorTreeNode);

				string theStorageScpBaseName = ResultsFile.GetBaseNameForEmulator(EmulatorTag.EmulatorType.STORAGE_SCP);
				ArrayList theStorageScpEmulatorResultsFiles = ResultsFile.GetNamesForBaseName(theStorageScpBaseName, theResultsFiles);
				theResultsFiles = ResultsFile.FilterOutResultsFileNames(theResultsFiles, theStorageScpEmulatorResultsFiles);

				UpdateEmulatorNode(theStorageScpEmulatorTreeNode, theSession, theStorageScpEmulatorResultsFiles, EmulatorTag.EmulatorType.STORAGE_SCP, false);


				// Add the Storage SCU emulator tree node.
				TreeNode theStorageScuEmulatorTreeNode = new TreeNode();
				theEmulatorSessionTreeNode.Nodes.Add(theStorageScuEmulatorTreeNode);

				string theStorageScuBaseName = ResultsFile.GetBaseNameForEmulator(EmulatorTag.EmulatorType.STORAGE_SCU);
				ArrayList theStorageScuEmulatorResultsFiles = ResultsFile.GetNamesForBaseName(theStorageScuBaseName, theResultsFiles);
				theResultsFiles = ResultsFile.FilterOutResultsFileNames(theResultsFiles, theStorageScuEmulatorResultsFiles);

				UpdateEmulatorNode(theStorageScuEmulatorTreeNode, theSession, theStorageScuEmulatorResultsFiles, EmulatorTag.EmulatorType.STORAGE_SCU, false);


				// Add the Print SCP emulator tree node.
				TreeNode thePrintScpEmulatorTreeNode = new TreeNode();
				theEmulatorSessionTreeNode.Nodes.Add(thePrintScpEmulatorTreeNode);

				string thePrintScpBaseName = ResultsFile.GetBaseNameForEmulator(EmulatorTag.EmulatorType.PRINT_SCP);
				ArrayList thePrintScpEmulatorResultsFiles = ResultsFile.GetNamesForBaseName(thePrintScpBaseName, theResultsFiles);
				theResultsFiles = ResultsFile.FilterOutResultsFileNames(theResultsFiles, thePrintScpEmulatorResultsFiles);

				UpdateEmulatorNode(thePrintScpEmulatorTreeNode, theSession, thePrintScpEmulatorResultsFiles, EmulatorTag.EmulatorType.PRINT_SCP, false);
			}
			else
			{
				// Sanity check, pre-condition of this method is not fullfilled.
				Debug.Assert(false);
			}
		}

		public void UpdateEmulatorNode(TreeNode theEmulatorTreeNode, Dvtk.Sessions.Session theSession, ArrayList theResultsFiles, EmulatorTag.EmulatorType theEmulatorType, bool isExecuting)
		{
			// Set the text on this session tree node.
			switch(theEmulatorType)
			{
				case EmulatorTag.EmulatorType.PRINT_SCP:
				{
					theEmulatorTreeNode.Text = "Print SCP Emulator";
				}
				break;

				case EmulatorTag.EmulatorType.STORAGE_SCP:
				{
					theEmulatorTreeNode.Text = "Storage SCP Emulator";
				}
				break;

				case EmulatorTag.EmulatorType.STORAGE_SCU:
				{
					theEmulatorTreeNode.Text = "Storage SCU Emulator";
				}
				break;
			}

			if (isExecuting)
			{
				theEmulatorTreeNode.Text += " (executing)";
			}

			// Set the tag for this session tree node.
			EmulatorTag theEmulatorTag = new EmulatorTag(theSession, theEmulatorType);
			theEmulatorTreeNode.Tag = theEmulatorTag;

			// Remove the old tree nodes that may be present under this session tree node.
			theEmulatorTreeNode.Nodes.Clear();

			if (!isExecuting)
			{
				foreach (string theResultsFile in theResultsFiles)
				{
					TreeNode theTreeNode = new TreeNode();
					theEmulatorTreeNode.Nodes.Add(theTreeNode);

					UpdateResultsFileNode(theTreeNode, theSession, theResultsFile);
				}
			}
		}

		private ArrayList GetVisibleScripts(Dvtk.Sessions.Session theSession)
		{
			ArrayList theVisibleScripts = new ArrayList();
			Dvtk.Sessions.ScriptSession theScriptSession = null;
			string theScriptRootDirectory = "";
			DirectoryInfo theDirectoryInfo = null;
			FileInfo[] theFilesInfo;

			theScriptSession = (Dvtk.Sessions.ScriptSession) theSession;
			theScriptRootDirectory = theScriptSession.DicomScriptRootDirectory;
			theDirectoryInfo = new DirectoryInfo(theScriptRootDirectory);

			if (theDirectoryInfo.Exists)
			{
				theFilesInfo = theDirectoryInfo.GetFiles();

				foreach (FileInfo theFileInfo in theFilesInfo)
				{
					bool showScriptFile = false;
					string theFileExtension = theFileInfo.Extension.ToLower();

					if ((theFileExtension == ".ds") && (_MainForm._UserSettings.ShowDicomScripts)) 
					{
						showScriptFile = true;
					}
					else if ((theFileExtension == ".dss") && (_MainForm._UserSettings.ShowDicomSuperScripts))
					{
						showScriptFile = true;
					}
					else if ((theFileExtension == ".vbs") && (_MainForm._UserSettings.ShowVisualBasicScripts))
					{
						showScriptFile = true;
					}
					else
					{
						showScriptFile = false;
					}

					if (showScriptFile)
					{
						theVisibleScripts.Add(theFileInfo.Name);
					}
				}
			}

			return(theVisibleScripts);
		}

		/// <summary>
		/// Get the selected session.
		/// </summary>
		/// <returns>Selected session.</returns>
		public Dvtk.Sessions.Session GetSelectedSession()
		{
			Dvtk.Sessions.Session theSelectedSession = null;

			if (GetSelectedTreeNodeTag() != null)
			{
				theSelectedSession = GetSelectedTreeNodeTag()._Session;
			}
		
			return theSelectedSession;
		}

		/// <summary>
		/// Get the selected tree node.
		/// </summary>
		/// <returns>Selected tree node.</returns>
		public TreeNode GetSelectedNode()
		{
			TreeNode theTreeNode = null;
			
			if (_SessionTreeView.Nodes.Count > 0)
			{
				theTreeNode = _SessionTreeView.SelectedNode;
				if( theTreeNode == null)
					theTreeNode = tempTreeNodeValue;
				 
			}

			tempTreeNodeValue = theTreeNode;

			return(theTreeNode);
		}

		/// <summary>
		/// Get the selected tree node tag.
		/// </summary>
		/// <returns>Selected tree node tag.</returns>
		public TreeNodeTag GetSelectedTreeNodeTag()
		{
			TreeNodeTag theTreeNodeTag = null;

			if (GetSelectedNode() != null)
			{
				if (GetSelectedNode().Tag is TreeNodeTag)
				{
					theTreeNodeTag = (TreeNodeTag)GetSelectedNode().Tag;
				}
				else
				{
					throw new System.ApplicationException("Get a tag that is not a TreeNodeTag.");
				}
			}

			return (theTreeNodeTag);
		}

		public ArrayList GetAndRemoveStringsWithSubString(ArrayList theStrings, string theSubString, bool ignoreCase)
		{
			ArrayList theStringsContainingTheSubString = new ArrayList();
		
			if (ignoreCase)
			{
				string theLowerCaseSubString = theSubString.ToLower();

				foreach (string theString in theStrings)
				{
					if (theString.ToLower().IndexOf(theLowerCaseSubString) != -1)
					{
						theStringsContainingTheSubString.Add(theString);
					}
				}
			}
			else
			{
				foreach (string theString in theStrings)
				{
					if (theString.IndexOf(theSubString) != -1)
					{
						theStringsContainingTheSubString.Add(theString);
					}
				}
			}

			// Remove all strings containing the substring.
			foreach (string theStringContainingTheSubString in theStringsContainingTheSubString)
			{
				theStrings.Remove(theStringContainingTheSubString);
			}

			return theStringsContainingTheSubString;
		}

		public TreeNode GetSessionNode(Dvtk.Sessions.Session theSession)
		{
			TreeNode theSessionNodeToFind = null;

			foreach (TreeNode theNode in _SessionTreeView.Nodes)
			{
				if (theNode.Tag is SessionTag)
				{
					SessionTag theSessionTag = (SessionTag)theNode.Tag;

					if (theSessionTag._Session == theSession)
					{
						theSessionNodeToFind = theNode;
						break;
					}
				}
			}

			return(theSessionNodeToFind);
		}

		public void ResultsFromExecutingScriptAsynchronously(IAsyncResult theIAsyncResult)
		{
			Dvtk.Sessions.ScriptSession theScriptSession = (Dvtk.Sessions.ScriptSession)GetExecutingSession();

			try
			{
				// Obligated to call the following method according to the asynchronous design pattern.
				theScriptSession.EndExecuteScript(theIAsyncResult);
			}
			catch (Exception ex)
			{
				//
				// Problem:
				// Errors thrown from a workerthread are eaten by the .NET 1.x CLR.
				// Workaround:
				// Directly call the global (untrapped) exception handler callback.
				// Do NOT rely on 
				// either
				// - System.AppDomain.CurrentDomain.UnhandledException
				// or
				// - System.Windows.Forms.Application.ThreadException
				// These events will only be triggered for the main thread not for worker threads.
				//
				CustomExceptionHandler eh = new CustomExceptionHandler();
				System.Threading.ThreadExceptionEventArgs args = new ThreadExceptionEventArgs(ex);
				eh.OnThreadException(this, args);
				//
				// Rethrow. This rethrow may work in the future .NET 2.x CLR.
				// Currently eaten.
				//
				throw ex;
			}

			theScriptSession.EndResultsGathering();

			// Update the UI. Do this with an invoke, because the thread that is calling this
			// method is NOT the thread that created all controls used!
			_EndExecution = new EndExecution(_TagThatIsBeingExecuted);

			_TagThatIsBeingExecuted  = null;
			
			_NotifyDelegate = new NotifyDelegate(_ParentForm.Notify);
			_ParentForm.Invoke(_NotifyDelegate, new object[]{_EndExecution});
		}

		public void ResultsFromExecutingEmulatorScpAsynchronously(IAsyncResult theIAsyncResult)
		{
			Dvtk.Sessions.EmulatorSession theEmulatorSession = (Dvtk.Sessions.EmulatorSession)GetExecutingSession();

			try
			{
				// Obligated to call the following method according to the asynchronous design pattern.
				theEmulatorSession.EndEmulateSCP(theIAsyncResult);
			}
			catch (Exception ex)
			{
				//
				// Problem:
				// Errors thrown from a workerthread are eaten by the .NET 1.x CLR.
				// Workaround:
				// Directly call the global (untrapped) exception handler callback.
				// Do NOT rely on 
				// either
				// - System.AppDomain.CurrentDomain.UnhandledException
				// or
				// - System.Windows.Forms.Application.ThreadException
				// These events will only be triggered for the main thread not for worker threads.
				//
				CustomExceptionHandler eh = new CustomExceptionHandler();
				System.Threading.ThreadExceptionEventArgs args = new ThreadExceptionEventArgs(ex);
				eh.OnThreadException(this, args);
				//
				// Rethrow. This rethrow may work in the future .NET 2.x CLR.
				// Currently eaten.
				//
				throw ex;
			}

			// Save the results.
			theEmulatorSession.EndResultsGathering();

			// Update the UI. Do this with an invoke, because the thread that is calling this
			// method is NOT the thread that created all controls used!
			_EndExecution = new EndExecution(_TagThatIsBeingExecuted);

			_TagThatIsBeingExecuted  = null;
			
			_NotifyDelegate = new NotifyDelegate(_ParentForm.Notify);
			_ParentForm.Invoke(_NotifyDelegate, new object[]{_EndExecution});
		}

		public void ResultsFromExecutingEmulatorStorageScuAsynchronously(IAsyncResult theIAsyncResult)
		{
			Dvtk.Sessions.EmulatorSession theEmulatorSession = (Dvtk.Sessions.EmulatorSession)GetExecutingSession();

			try
			{
				// Obligated to call the following method according to the asynchronous design pattern.
				theEmulatorSession.EndEmulateStorageSCU(theIAsyncResult);
			}
			catch (Exception ex)
			{
				//
				// Problem:
				// Errors thrown from a workerthread are eaten by the .NET 1.x CLR.
				// Workaround:
				// Directly call the global (untrapped) exception handler callback.
				// Do NOT rely on 
				// either
				// - System.AppDomain.CurrentDomain.UnhandledException
				// or
				// - System.Windows.Forms.Application.ThreadException
				// These events will only be triggered for the main thread not for worker threads.
				//
				CustomExceptionHandler eh = new CustomExceptionHandler();
				System.Threading.ThreadExceptionEventArgs args = new ThreadExceptionEventArgs(ex);
				eh.OnThreadException(this, args);
				//
				// Rethrow. This rethrow may work in the future .NET 2.x CLR.
				// Currently eaten.
				//
				throw ex;
			}

			// Save the results.
			theEmulatorSession.EndResultsGathering();

			// Update the UI. Do this with an invoke, because the thread that is calling this
			// method is NOT the thread that created all controls used!
			_EndExecution = new EndExecution(_TagThatIsBeingExecuted);

			_TagThatIsBeingExecuted  = null;
			
			_NotifyDelegate = new NotifyDelegate(_ParentForm.Notify);
			_ParentForm.Invoke(_NotifyDelegate, new object[]{_EndExecution});
		}

		// Needed for the method ResultsFromExecutingScriptAsynchronously.
		delegate void NotifyDelegate(object theEvent);


		/// <summary>
		/// Search the correct results file node, expand its path to the top most session node.
		/// When this is done, select the results file node.
		/// 
		/// This method must be called with a tree node that is a session tree node.
		/// This method internally may call itself with a tree node that is not a session tree node.
		/// </summary>
		/// <param name="">The tree node to start searching for the results file node.</param>
		/// <param name="theResultsFileName">The filetheTreeNode name only of the results file.</param>
		/// <returns>null if no results file node has been found, otherwise the found results file node.</returns>
		public TreeNode ExpandAndSelectResultFile(TreeNode theTreeNode, string theResultsFileName)
		{
			TreeNode theResult = null;

			foreach(TreeNode theSubTreeNode in theTreeNode.Nodes)
			{
				if (theSubTreeNode.Tag is ResultsFileTag)
				{
					ResultsFileTag theResultsFileTag = (ResultsFileTag)theSubTreeNode.Tag;
                    string tempResultName = theResultsFileTag._ResultsFileName.Substring(0 ,theResultsFileTag._ResultsFileName.IndexOf(".xml"));

					if ((theResultsFileTag._ResultsFileName == theResultsFileName) || (theResultsFileName.StartsWith(tempResultName)))
                    {
						// This is the correct results file node.
						// Stop further searching.
						theResult = theSubTreeNode;
						break;
					}
					else
					{
						// This is not the correct results file node.
						// Continue with searching.
					}
				}
				else
				{
					theResult = ExpandAndSelectResultFile(theSubTreeNode, theResultsFileName);

					if (theResult != null)
					{
						// The correct results file name is present in this sub node.
						// Stop further searching.
						theSubTreeNode.Expand();
						break;
					}
					else
					{
						// The correct results file node has not yet been found.
						// Continue with searching.					
					}
				}
			}

			if (theResult != null)
			{
				if (theTreeNode.Tag is SessionTag)
				{
                    reloadHtml = false;
					_SessionTreeView.SelectedNode = theResult;
                    reloadHtml = true;
				}
			}

			return (theResult);
		}

		/// <summary>
		/// Search the correct results file node.
		/// When this is done, select the results file node.
		/// </summary>
		/// <param name="theResultsFileName">The filetheTreeNode name only of the results file.</param>
		public void SearchAndSelectResultNode(string theDirectory, string theResultsFileName)
		{
			TreeNode theResult = null;

			foreach(TreeNode theSessionNode in _SessionTreeView.Nodes)
			{
				SessionTag theSessionTag = theSessionNode.Tag as SessionTag;
				if(theSessionTag._Session.ResultsRootDirectory == theDirectory)
				{
					theResult = ExpandAndSelectResultFile(theSessionNode, theResultsFileName);
					if (theResult != null)
					{
						break;
					}
				}
			}
		}

		public SessionTreeViewManager(ProjectForm2 theParentForm, Project theProject, System.Windows.Forms.TreeView theSessionTreeView)
		{
			_ParentForm = theParentForm;
			_MainForm = (MainForm)_ParentForm._MainForm;
			_Project = theProject;
			_SessionTreeView = theSessionTreeView;

			_StorageSCUEmulatorFormAsyncCallback = new System.AsyncCallback(this.ResultsFromExecutingEmulatorStorageScuAsynchronously);
			_StorageSCUEmulatorForm = new StorageSCUEmulatorForm(_StorageSCUEmulatorFormAsyncCallback);
		}

		/// <summary>
		/// The member is set when starting execution of a script file.
		/// The reason for keeping this information is, that the ResultsFromExecutingScriptAsynchronously 
		/// method is called from another thread and we don't want to "ask" information from the session 
		/// tree view.
		/// </summary>
		TreeNodeTag _TagThatIsBeingExecuted = null;

		private int _UpdateCount = 0;

		private Thread _ScriptThread = null;

		private void ExecuteDicomScriptInThread(ScriptFileTag theScriptFileTag)
		{
			string theResultsFileName = null;
			Dvtk.Sessions.ScriptSession theScriptSession = (Dvtk.Sessions.ScriptSession)theScriptFileTag._Session;
			
			// Determine the results file name.
			theResultsFileName = ResultsFile.GetExpandedNameForScriptFile(theScriptSession, theScriptFileTag._ScriptFileName);

			// Start the results gathering.
			theScriptSession.StartResultsGathering(theResultsFileName);

			// Perform the actual execution of the script.
			AsyncCallback theExecuteScriptAsyncCallback = new AsyncCallback(this.ResultsFromExecutingScriptAsynchronously);
			theScriptSession.BeginExecuteScript(theScriptFileTag._ScriptFileName, false, theExecuteScriptAsyncCallback);				
		}

		public void ExecuteVisualBasicScriptInThread(ScriptFileTag theScriptFileTag)
		{
			_ScriptThread = new Thread (new ThreadStart (this.ExecuteVisualBasicScript));
			_ScriptThread.Start();
		}

		private void ExecuteVisualBasicScript()
		{
			try
			{
				ScriptFileTag theScriptFileTag = _TagThatIsBeingExecuted as ScriptFileTag;

				if (theScriptFileTag == null)
					// Sanity check.
				{
					Debug.Assert(false);
				}
				else
				{
					// TODO!!!!!
					// The following code should be removed when the business layer is completely implemented!
					// For now, construct a business layer object that does the execution of the VBS.
					// BEGIN
					
					DvtkApplicationLayer.VisualBasicScript applicationLayerVisualBasicScript = 
						new DvtkApplicationLayer.VisualBasicScript(theScriptFileTag._Session as ScriptSession, theScriptFileTag._ScriptFileName);
					// END
					
					String[] emptyArray = {};
					ArrayList listContainingExmptyArray = new ArrayList();
					listContainingExmptyArray.Add(emptyArray);

					applicationLayerVisualBasicScript.Execute(listContainingExmptyArray.ToArray());
				}

				// Update the UI. Do this with an invoke, because the thread that is calling this
				// method is NOT the thread that created all controls used!
				_EndExecution = new EndExecution(_TagThatIsBeingExecuted);

				_TagThatIsBeingExecuted  = null;
			
				_NotifyDelegate = new NotifyDelegate(_ParentForm.Notify);
				_ParentForm.Invoke(_NotifyDelegate, new object[]{_EndExecution});
			}
			catch (Exception ex)
			{
				//
				// Problem:
				// Errors thrown from a workerthread are eaten by the .NET 1.x CLR.
				// Workaround:
				// Directly call the global (untrapped) exception handler callback.
				// Do NOT rely on 
				// either
				// - System.AppDomain.CurrentDomain.UnhandledException
				// or
				// - System.Windows.Forms.Application.ThreadException
				// These events will only be triggered for the main thread not for worker threads.
				//
				CustomExceptionHandler eh = new CustomExceptionHandler();
				System.Threading.ThreadExceptionEventArgs args = new ThreadExceptionEventArgs(ex);
				eh.OnThreadException(this, args);
				//
				// Rethrow. This rethrow may work in the future .NET 2.x CLR.
				// Currently eaten.
				//
				throw ex;
			}
		}

		public void EditSelectedScriptFile()
		{
			ScriptFileTag theScriptFileTag = GetSelectedTreeNodeTag() as ScriptFileTag;

			if (theScriptFileTag == null)
			{
				// Sanity check.
				Debug.Assert(false);
			}
			else
			{
				ScriptSession theScriptSession = (ScriptSession)theScriptFileTag._Session;

				System.Diagnostics.Process theProcess  = new System.Diagnostics.Process();

				theProcess.StartInfo.FileName= "Notepad.exe";
				theProcess.StartInfo.Arguments = System.IO.Path.Combine(theScriptSession.DicomScriptRootDirectory, theScriptFileTag._ScriptFileName);

				theProcess.Start();
			}
		}
	
		/// <summary>
		/// Get (if exisiting) the session that is executed by this Session Tree View.
		/// </summary>
		/// <returns>Session that is executed by this Session Tree View, null if this Session Tree View is not executing a session.</returns>
		public Dvtk.Sessions.Session GetExecutingSession()
		{
			Dvtk.Sessions.Session theExecutingSession = null;

			if (_TagThatIsBeingExecuted != null)
			{
				theExecutingSession = _TagThatIsBeingExecuted._Session;
			}

			return theExecutingSession;
		}

		/// <summary>
		/// Is the main results file for the selected tree node tag in use when a script or emulator
		/// will be executed or media files will be validated?
		/// </summary>
		/// <param name="treeNodeTag">The selected tree node tag.</param>
		/// <returns>Indicates if the main results file for the selected tree node tag is in use.</returns>
		public bool IsFileInUse(TreeNodeTag treeNodeTag, out string errorText)
		{
			bool isFileInUse = false;
			string resultsFileNameOnly = "";
			errorText = "";

			if (treeNodeTag is ScriptFileTag)
			{
				ScriptFileTag scriptFileTag = treeNodeTag as ScriptFileTag;

				resultsFileNameOnly = ResultsFile.GetSummaryNameForScriptFile(scriptFileTag._Session, scriptFileTag._ScriptFileName);
			}
			else if (treeNodeTag is EmulatorTag)
			{
				EmulatorTag emulatorTag = treeNodeTag as EmulatorTag;

				resultsFileNameOnly = ResultsFile.GetSummaryNameForEmulator(emulatorTag._Session, emulatorTag._EmulatorType);
			}

			if (resultsFileNameOnly != "")
			{
				string resultsFullFileName = Path.Combine(treeNodeTag._Session.ResultsRootDirectory, resultsFileNameOnly);
				FileStream fileStream = null;

				try
				{
					fileStream = File.OpenRead(resultsFullFileName);
				}
				catch(IOException exception)
				{
					if (!(exception is FileNotFoundException))
					{
						isFileInUse = true;
						errorText = exception.Message;
					}
				}
				finally
				{	
					if (fileStream != null)
					{
						fileStream.Close();
					}
				}
			}

			return isFileInUse;
		}

		public void ViewExpandedScriptFile()
		{
			ScriptFileTag selectedScriptFileTag = GetSelectedTreeNodeTag() as ScriptFileTag;

			if (selectedScriptFileTag != null)
			{
				if (selectedScriptFileTag._ScriptFileName.ToLower().EndsWith(".vbs"))
				{
					// TODO!!!!!
					// The following code should be removed when the business layer is completely implemented!
					// For now, construct a business layer object that does the execution of the VBS.
					// BEGIN
					
					DvtkApplicationLayer.VisualBasicScript applicationLayerVisualBasicScript = 
						new DvtkApplicationLayer.VisualBasicScript(selectedScriptFileTag._Session as ScriptSession, selectedScriptFileTag._ScriptFileName);

					applicationLayerVisualBasicScript.ViewExpanded();
					// END	
				}
			}		
		}
	}
}
