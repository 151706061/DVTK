// Part of DVT.exe - .NET user interface application to perform DICOM testing
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;

namespace DvtTreeNodeTag
{
	/// <summary>
	/// 
	/// </summary>
	abstract public class TreeNodeTag: object
	{
		public TreeNodeTag(Dvtk.Sessions.Session theSession)
		{
			_Session = theSession;
		}

		public virtual bool IsRepresentingSame(TreeNodeTag theTreeNodeTag)
		{
			return(false);
		}

		public Dvtk.Sessions.Session _Session;
	}

	/// <summary>
	/// 
	/// </summary>
	abstract public class SessionTag: TreeNodeTag
	{
		public SessionTag(Dvtk.Sessions.Session theSession): base(theSession)
		{
				
		}

		public override bool IsRepresentingSame(TreeNodeTag theTreeNodeTag)
		{
			bool isRepresentingSame = false;

			SessionTag theSessionTag = theTreeNodeTag as SessionTag;

			if (theSessionTag != null)
			{
				if (theSessionTag._Session == _Session)
				{
					isRepresentingSame = true;
				}
			}

			return(isRepresentingSame);
		}	
	}

	/// <summary>
	/// 
	/// </summary>
	public class ScriptSessionTag: SessionTag
	{
		public ScriptSessionTag(Dvtk.Sessions.Session theSession): base(theSession)
		{
				
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class MediaSessionTag: SessionTag
	{
		public MediaSessionTag(Dvtk.Sessions.Session theSession): base(theSession)
		{

		}
	}

	/// <summary>
	/// 
	/// </summary>
	public class EmulatorSessionTag: SessionTag
	{
		public EmulatorSessionTag(Dvtk.Sessions.Session theSession): base(theSession)
		{
				
		}
	}

	public class EmulatorTag: TreeNodeTag
	{
		public enum EmulatorType
		{
			STORAGE_SCP,
			STORAGE_SCU,
			PRINT_SCP
		}

		public EmulatorTag(Dvtk.Sessions.Session theSession, EmulatorType theEmulatorType): base(theSession)
		{
			_EmulatorType = theEmulatorType;
		}

		public override bool IsRepresentingSame(TreeNodeTag theTreeNodeTag)
		{
			bool isRepresentingSame = false;

			EmulatorTag theEmulatorTag = theTreeNodeTag as EmulatorTag;

			if (theEmulatorTag != null)
			{
				if ((theEmulatorTag._Session == _Session) && (theEmulatorTag._EmulatorType == _EmulatorType))
				{
					isRepresentingSame = true;
				}
			}

			return(isRepresentingSame);
		}	

		public EmulatorType _EmulatorType;
	}

	/// <summary>
	/// 
	/// </summary>
	public class ScriptFileTag: TreeNodeTag
	{
		public ScriptFileTag(Dvtk.Sessions.Session theSession): base(theSession)
		{
				
		}

		public override bool IsRepresentingSame(TreeNodeTag theTreeNodeTag)
		{
			bool isRepresentingSame = false;

			ScriptFileTag theScriptFileTag = theTreeNodeTag as ScriptFileTag;

			if (theScriptFileTag != null)
			{
				if ((theScriptFileTag._Session == _Session) && (theScriptFileTag._ScriptFileName == _ScriptFileName))
				{
					isRepresentingSame = true;
				}
			}

			return(isRepresentingSame);
		}	

		public string _ScriptFileName;
	}

	/// <summary>
	/// 
	/// </summary>
	public class ResultsFileTag: TreeNodeTag
	{
		public ResultsFileTag(Dvtk.Sessions.Session theSession): base(theSession)
		{
				
		}

		public override bool IsRepresentingSame(TreeNodeTag theTreeNodeTag)
		{
			bool isRepresentingSame = false;

			ResultsFileTag theResultsFileTag = theTreeNodeTag as ResultsFileTag;

			if (theResultsFileTag != null)
			{
				if ((theResultsFileTag._Session == _Session) && (theResultsFileTag._ResultsFileName == _ResultsFileName))
				{
					isRepresentingSame = true;
				}
			}

			return(isRepresentingSame);
		}	

		public string _ResultsFileName;

	}

	/// <summary>
	/// 
	/// </summary>
	public class ResultsCollectionTag: TreeNodeTag
	{
		public ResultsCollectionTag(Dvtk.Sessions.Session theSession): base(theSession)
		{
				
		}

		public override bool IsRepresentingSame(TreeNodeTag theTreeNodeTag)
		{
			bool isRepresentingSame = false;

			ResultsCollectionTag theResultsCollectionTag = theTreeNodeTag as ResultsCollectionTag;

			if (theResultsCollectionTag != null)
			{
			if ( (theResultsCollectionTag._Session == _Session) && (theResultsCollectionTag._ResultsCollectionName == _ResultsCollectionName) )

			{
					isRepresentingSame = true;
				}
			}

			return(isRepresentingSame);
		}	

		public string _ResultsCollectionName;
	}
}
