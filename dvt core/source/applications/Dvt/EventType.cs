// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Windows.Forms;
using DvtTreeNodeTag;

namespace Dvt
{
	/// <summary>
	/// 
	/// </summary>
	public class EventType: object
	{
		public EventType()
		{
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class EventTypeWithTreeNode: EventType
	{
		public EventTypeWithTreeNode(TreeNode theTreeNode)
		{
			_TreeNode = theTreeNode;
		}

		public TreeNode TreeNode
		{
			get
			{
				return(_TreeNode);
			}
		}

		private TreeNode _TreeNode;
	}

	/// <summary>
	/// 
	/// </summary>
	public class SessionTreeViewSelectionChange: EventTypeWithTreeNode
	{
		public SessionTreeViewSelectionChange(TreeNode theTreeNode): base(theTreeNode)
		{

		}
	}

	/// <summary>
	/// 
	/// </summary>	
	public class UpdateAll: EventType
	{
		public bool _RestoreSessionTreeState = false;
	}

	/// <summary>
	/// 
	/// </summary>	
	public class ClearAll: EventType
	{
		public bool _StoreSessionTreeState = false;
	}

	/// <summary>
	/// 
	/// </summary>
	public class SessionChange: EventType
	{
		public enum SessionChangeSubTypEnum
		{
			RESULTS_DIR,
			SCRIPTS_DIR,
			DESCRIPTION_DIR,
			SOP_CLASSES_OTHER,
			SOP_CLASSES_AE_TITLE_VERSION,
			SOP_CLASSES_LOADED_STATE,
			OTHER
		}

		public Dvtk.Sessions.Session Session
		{
			get
			{
				return(_Session);
			}
		}		

		public SessionChangeSubTypEnum SessionChangeSubTyp
		{
			get
			{
				return(_SessionChangeSubTyp);
			}
		}		

		public SessionChange(Dvtk.Sessions.Session theSession, SessionChangeSubTypEnum theSessionChangeSubTyp)
		{
			_Session = theSession;
			_SessionChangeSubTyp = theSessionChangeSubTyp;
		}


		private Dvtk.Sessions.Session _Session;
		private SessionChangeSubTypEnum _SessionChangeSubTyp;
	}

	/// <summary>
	/// Use this event to notify that an execution is stopped (because e.g. the stop button has been pressed).
	/// </summary>
	public class StopExecution: EventType
	{
		public StopExecution(ProjectForm2 theProjectForm)
		{
			_ProjectForm = theProjectForm;
		}

		public ProjectForm2 _ProjectForm;

	}

	/// <summary>
	/// Use this event to notify that all exection needs to be stopped (because e.g. the project is closed).
	/// </summary>
	public class StopAllExecution: EventType
	{

	}

	/// <summary>
	/// Use this event to notify that an execution (of a script, emulator) is started.
	/// </summary>
	public class StartExecution: EventTypeWithTreeNode
	{
		public StartExecution(TreeNode theTreeNode): base(theTreeNode)
		{

		}
	}

	/// <summary>
	/// Use this event to notify that an execution (of a script, emulator) is ended.
	/// </summary>
	public class EndExecution: EventType
	{
		public EndExecution(TreeNodeTag theTag)
		{
			_Tag = theTag;
		}

		public TreeNodeTag _Tag;
	}

	/// <summary>
	/// Use this event to notify that the user has selected a different tab in the tab control.
	/// </summary>
	public class SelectedTabChangeEvent: EventType
	{
	}

	/// <summary>
	/// Use this event to notify that the user has set focus to a different project form.
	/// </summary>
	public class ProjectFormGetsFocusEvent: EventType
	{
	}

	/// <summary>
	/// Use this event to notify that the session has been removed.
	/// </summary>
	public class SessionRemovedEvent: EventType
	{
		public SessionRemovedEvent(Dvtk.Sessions.Session theSession)
		{
			_Session = theSession;
		}

		public Dvtk.Sessions.Session _Session;
	}

	public class WebNavigationComplete: EventType
	{
		public WebNavigationComplete()
		{
		}
	}

	public class axWebNavigationComplete: EventType
	{
		public axWebNavigationComplete()
		{
		}
	}

	/// <summary>
	/// Use this event to notify that the last project form is closing.
	/// </summary>
	public class ProjectClosed: EventType
	{
		public ProjectClosed()
		{
		}
	}

	/// <summary>
	/// Use this event to notify that a session has been replaced with another session.
	/// </summary>
	public class SessionReplaced: EventType
	{
		public SessionReplaced(Dvtk.Sessions.Session theOldSession, Dvtk.Sessions.Session theNewSession)
		{
			_OldSession = theOldSession;
			_NewSession = theNewSession;
		}

		public Dvtk.Sessions.Session _OldSession;
		public Dvtk.Sessions.Session _NewSession;
	}

	/// <summary>
	/// Use this event to notify that (some) session(s) or the project has been saved.
	/// </summary>
	public class Saved: EventType
	{
		public Saved()
		{
		}
	}
}

