// Part of DvtkDicomPrintMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.DvtkDicomEmulators.PrintMessageHandlers
{
	/// <summary>
	/// Summary description for NGetHandler.
	/// </summary>
	public class NGetHandler : MessageHandler
	{
		private PrintContext _printContext = null;

		public NGetHandler(PrintContext printContext)
		{
			_printContext = printContext;
		}

		public override bool HandleNGetRequest(DicomMessage dicomMessage)
		{
			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.NGETRSP);
			this.Send(responseMessage);
			return true;
		}
	}
}
