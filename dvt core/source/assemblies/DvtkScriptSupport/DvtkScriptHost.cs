// Part of DvtkScriptSupport.dll - .NET script engine that runs MS-script based DICOM test scripts
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using Dvtk;

namespace DvtkScriptSupport
{
    /// <summary>
    /// Supported script languages.
    /// </summary>
    public enum DvtkScriptLanguage
    {
        VB,
        VisualBasic,
        JScript,
        JScriptNET,
    }

    /// <summary>
    /// Hosting environment, supporting execution of scripts against the
    /// Dicom Validation Tool(Kit).
    /// Scripts are run using a <see cref="Dvtk.Sessions.ScriptSession"/> 
    /// which is identified within the script by means of global a variable <c>session</c>.
    /// </summary>
    public class DvtkScriptHost
        : VsaScriptHost
    {
        /// <summary>
        /// List of reserved key words to be used in scripts run by this script environment.
        /// </summary>
        private static readonly string SessionKeyWord = "session";
        private static readonly string[] ReservedKeyWords = 
            new string[] {
                             SessionKeyWord 
                         };

        /// <summary>
        /// Collection of reference namespaces. These namespaces are imported for each script.
        /// </summary>
        private static readonly System.Collections.Hashtable 
            References = new System.Collections.Hashtable();
        private static readonly string[] NameSpaceImports = 
            new string[] {
                             "Dvtk",  
                             "Dvtk.Sessions",
                             "DvtkData",
                             "DvtkData.Dimse",
                             "DvtkData.Dul",
                             "DvtkData.Media",
		};

        /// <summary>
        /// Static constructor.
        /// </summary>
        static DvtkScriptHost()
        {
            // Hashtable of identifiers as used by the VSA engine and
            // the referenced assemblies (DLLs).
            References.Add("Dvtk", "DVTK.dll");
            References.Add("DvtkData", "DvtkData.dll");
            References.Add("DVTKManagedCodeAdapter", "DVTKManagedCodeAdapter.dll");
			References.Add("DvtkHighLevelInterface", "DvtkHighLevelInterface.dll");
        }

        private static string _Convert(DvtkScriptLanguage language)
        {
            switch (language)
            {
                case DvtkScriptLanguage.VB          : return "VB";
                case DvtkScriptLanguage.VisualBasic : return "Visual Basic";
                case DvtkScriptLanguage.JScript     : return "JScript";
                case DvtkScriptLanguage.JScriptNET  : return "JScript.NET";
                default:
                    throw new System.ApplicationException();
            }
        }

        /// <summary>
        /// Collection of used monikers in the current application.
        /// Each ScriptHost instance must be identified by a unique moniker (nickname/alias/identifier).
        /// </summary>
        private static System.Collections.ArrayList MonikerList = new System.Collections.ArrayList();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="language">The language used in the script.</param>
        /// <param name="moniker">The unique identifier of the script engine.</param>
        /// <param name="importedAssemblyDir">The directory of the used DVTK component/assemblies.</param>
        public DvtkScriptHost(
            DvtkScriptLanguage language,
            string moniker,
            string importedAssemblyDir) 
            : base(_Convert(language), moniker, "DvtkScript")
        {
			bool dvtkHighLevelInterfaceAvailable = true;

            if (MonikerList.Contains(moniker)) throw new System.ArgumentException("Moniker was already used.");
            MonikerList.Add(moniker);
            System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(importedAssemblyDir);
            if (!directoryInfo.Exists)
            {
                throw new System.ArgumentException("Imported Assembly Directory does not exist.");
            }
            foreach (object key in References.Keys)
            {
                string itemName = (string)key;
                string assemblyName = 
                    System.IO.Path.Combine(importedAssemblyDir, (string)References[key]);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(assemblyName);
				if (!fileInfo.Exists)
				{
					// The file DvtkHighLevelInterface.dll may be absent.
					// In this case, no high level interface will be available.
					// All other dll's must be available.
					if (System.IO.Path.GetFileName(assemblyName) == "DvtkHighLevelInterface.dll")
					{
						dvtkHighLevelInterfaceAvailable = false;
					}
					else
					{
						throw new System.ArgumentException(
							string.Format("Required assembly {0} was not found", assemblyName));
					}

				}
				else
				{
					this.AddReference((string)key, assemblyName);
				}
            }

            foreach (string nameSpace in NameSpaceImports)
            {
                this._AddImport(nameSpace);
            }

			// Only if the DvtkHighLevelInterface is available, add the corresponding namespace.
			if (dvtkHighLevelInterfaceAvailable)
			{
				this._AddImport("DvtkHighLevelInterface");
			}
        }

        /// <summary>
        /// The assigned session used by the scripts. The session is used in
        /// the script by means of a global variable keyword <c>session</c>.
        /// </summary>
        public Dvtk.Sessions.ScriptSession Session
        {
            get 
            {
                return this._Session;
            }
            set
            {
                RemoveGlobalInstance(SessionKeyWord);
                this._Session = value;
                AddGlobalInstance(SessionKeyWord, this.Session);
            }
        }
        private Dvtk.Sessions.ScriptSession _Session = null;
    }
}
