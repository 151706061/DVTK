// Part of DvtkDicomPrintMessageHandlers.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.DvtkDicomEmulators.PrintMessageHandlers
{
	/// <summary>
	/// Summary description for NDeleteHandler.
	/// </summary>
	public class NDeleteHandler : MessageHandler
	{
		private PrintContext _printContext = null;

		public NDeleteHandler(PrintContext printContext)
		{
			_printContext = printContext;
		}

		public override bool HandleNDeleteRequest(DicomMessage dicomMessage)
		{
			DicomMessage responseMessage = new DicomMessage(DvtkData.Dimse.DimseCommand.NDELETERSP);
			this.Send(responseMessage);
			return true;
		}
	}
}
