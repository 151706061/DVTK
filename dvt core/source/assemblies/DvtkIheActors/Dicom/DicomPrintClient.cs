// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace Dvtk.IheActors.Dicom
{
	/// <summary>
	/// Summary description for DicomPrintClient.
	/// </summary>
	public class DicomPrintClient : DicomClient
	{
		public DicomPrintClient(BaseActor parentActor, ActorNameEnum actorName) : base (parentActor, actorName)
		{
			_presentationContexts = new PresentationContext[1];

			PresentationContext presentationContext = new PresentationContext("1.2.840.10008.3.1.2.3.3", // Abstract Syntax Name
				"1.2.840.10008.1.2"); // Transfer Syntax Name(s)
			_presentationContexts[0] = presentationContext;
		}
	}
}
