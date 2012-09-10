// Part of DvtkDicomPrintClientServers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.UserInterfaces;
using Dvtk.DvtkDicomEmulators.PrintMessageHandlers;

namespace Dvtk.DvtkDicomEmulators.PrintClientServers
{
	/// <summary>
	/// Summary description for PrintScp.
	/// </summary>
	public class PrintScp : SCP
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public PrintScp() 		
		{
			HliConsole output = new HliConsole();
			output.Attach(this);
		}

		/// <summary>
		/// Add the default message handlers.
		/// </summary>
		public void AddDefaultMessageHandlers()
		{
			PrintContext printContext = new PrintContext();
			AddToBack(new NActionHandler(printContext));
			AddToBack(new NCreateHandler(printContext));
			AddToBack(new NDeleteHandler(printContext));
			AddToBack(new NGetHandler(printContext));
			AddToBack(new NSetHandler(printContext));
		}
	}
}
