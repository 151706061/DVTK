// Part of DvtkDicomPrintMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.DvtkDicomEmulators.PrintMessageHandlers
{
	/// <summary>
	/// Summary description for NActionHandler.
	/// </summary>
	public class NActionHandler : MessageHandler
	{
		private PrintContext _printContext = null;

		public NActionHandler(PrintContext printContext)
		{
			_printContext = printContext;
		}

		public override bool HandleNActionRequest(DicomMessage dicomMessage)
		{
			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.NACTIONRSP);
			this.Send(responseMessage);
			return true;
		}
	}
}
