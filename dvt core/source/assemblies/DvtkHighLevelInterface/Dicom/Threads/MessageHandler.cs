using System;
using System.Diagnostics;
using DvtkHighLevelInterface.Messages;



namespace DvtkHighLevelInterface
{
	/// <summary>
	/// A descendant of this class is used in combination with a MessageIterator
	/// to handle specific Messages that are received by the MessageIterator. An
	/// instance of this class will typically implement sending one or more messages
	/// as a reaction on specific received message. This class should normally not
	/// be concerned with control of flow e.g. like accepting/rejecting an association.
	/// </summary>
	abstract public class MessageHandler
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property DicomThread.
		/// </summary>
		private DicomThread dicomThread = null;



		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		public MessageHandler()
		{
			// Do nothing.
		}



		//
		// - Properties -
		//

		/// <summary>
		/// The DicomThread this object operates on. It is the responsibility of the
		/// MessageIterator class to set this to the correct DicomThread before using
		/// this object.
		/// </summary>
		public DicomThread DicomThread
		{
			get
			{
				return(this.dicomThread);
			}
			set
			{
				this.dicomThread = value;
			}
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Override this method to handle an A-ABORT.
		/// </summary>
		/// <param name="abort">The received A-ABORT.</param>
		/// <returns>Return true when this methods has handled the received A-ABORT, otherwise false.</returns>
		public virtual bool HandleAbort(Abort abort)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle an A-ASSOCIATE-AC.
		/// </summary>
		/// <param name="associateAc">The received A-ASSOCIATE-AC.</param>
		/// <returns>Return true when this methods has handled the received A-ASSOCIATE-AC, otherwise false.</returns>
		public virtual bool HandleAssociateAccept(AssociateAc associateAc)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle an A-ASSOCIATE-RJ.
		/// </summary>
		/// <param name="associateRj">The received A-ASSOCIATE-RJ.</param>
		/// <returns>Return true when this methods has handled the received A-ASSOCIATE-RJ, otherwise false.</returns>
		public virtual bool HandleAssociateReject(AssociateRj associateRj)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle an A-ASSOCIATE-RQ.
		/// </summary>
		/// <param name="associateRq">The received A-ASSOCIATE-RQ.</param>
		/// <returns>Return true when this methods has handled the received A-ASSOCIATE-RQ, otherwise false.</returns>
		public virtual bool HandleAssociateRequest(AssociateRq associateRq)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-CANCEL-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received C-CANCEL-RQ message.</param>
		/// <returns>Return true when this methods has handled the received C-CANCEL-RQ message , otherwise false.</returns>
		public virtual bool HandleCCancelRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-ECHO-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received C-ECHO-RQ message.</param>
		/// <returns>Return true when this methods has handled the received C-ECHO-RQ message , otherwise false.</returns>
		public virtual bool HandleCEchoRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-ECHO-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received C-ECHO-RSP message.</param>
		/// <returns>Return true when this methods has handled the received C-ECHO-RSP message , otherwise false.</returns>
		public virtual bool HandleCEchoResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-FIND-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received C-FIND-RQ message.</param>
		/// <returns>Return true when this methods has handled the received C-FIND-RQ message , otherwise false.</returns>
		public virtual bool HandleCFindRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-FIND-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received C-FIND-RSP message.</param>
		/// <returns>Return true when this methods has handled the received C-FIND-RSP message , otherwise false.</returns>
		public virtual bool HandleCFindResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-GET-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received C-GET-RQ message.</param>
		/// <returns>Return true when this methods has handled the received C-GET-RQ message , otherwise false.</returns>
		public virtual bool HandleCGetRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-GET-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received C-GET-RSP message.</param>
		/// <returns>Return true when this methods has handled the received C-GET-RSP message , otherwise false.</returns>
		public virtual bool HandleCGetResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-MOVE-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received C-MOVE-RQ message.</param>
		/// <returns>Return true when this methods has handled the received C-MOVE-RQ message , otherwise false.</returns>
		public virtual bool HandleCMoveRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-MOVE-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received C-MOVE-RSP message.</param>
		/// <returns>Return true when this methods has handled the received C-MOVE-RSP message , otherwise false.</returns>
		public virtual bool HandleCMoveResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-STORE-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received C-STORE-RQ message.</param>
		/// <returns>Return true when this methods has handled the received C-STORE-RQ message , otherwise false.</returns>
		public virtual bool HandleCStoreRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a C-STORE-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received C-STORE-RSP message.</param>
		/// <returns>Return true when this methods has handled the received C-STORE-RSP message , otherwise false.</returns>
		public virtual bool HandleCStoreResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// This method is indirectly called by a MessageIterator to let this object try
		/// to handle the received DicomMessage.
		/// </summary>
		/// <param name="dicomMessage">The received DicomMessage.</param>
		/// <returns>Returns true if this object has handled the DicomMessage, otherwise false.</returns>
		private bool HandleDicomMessage(DicomMessage dicomMessage)
		{
			bool handled = false;

			switch(dicomMessage.CommandSet.DimseCommand)
			{
				case DvtkData.Dimse.DimseCommand.CCANCELRQ:
					handled = HandleCCancelRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CECHORQ:
					handled = HandleCEchoRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CECHORSP:
					handled = HandleCEchoResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CFINDRQ:
					handled = HandleCFindRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CFINDRSP:
					handled = HandleCFindResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CGETRQ:
					handled = HandleCGetRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CGETRSP:
					handled = HandleCGetResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CMOVERQ:
					handled = HandleCMoveRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CMOVERSP:
					handled = HandleCMoveResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CSTORERQ:
					handled = HandleCStoreRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.CSTORERSP:
					handled = HandleCStoreResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NACTIONRQ:
					handled = HandleNActionRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NACTIONRSP:
					handled = HandleNActionResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NCREATERQ:
					handled = HandleNCreateRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NCREATERSP:
					handled = HandleNCreateResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NDELETERQ:
					handled = HandleNDeleteRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NDELETERSP:
					handled = HandleNDeleteResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NEVENTREPORTRQ:
					handled = HandleNEventReportRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NEVENTREPORTRSP:
					handled = HandleNEventReportResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NGETRQ:
					handled = HandleNGetRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NGETRSP:
					handled = HandleNGetResponse(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NSETRQ:
					handled = HandleNSetRequest(dicomMessage);
					break;

				case DvtkData.Dimse.DimseCommand.NSETRSP:
					handled = HandleNSetResponse(dicomMessage);
					break;

				default:
					Debug.Assert(true, "Not yet implemented.");
					break;
			}

			return(handled);
		}

		/// <summary>
		/// This method is indirectly called by a MessageIterator to let this object try
		/// to handle the received DulMessage.
		/// </summary>
		/// <param name="dulMessage">The received DulMessage.</param>
		/// <returns>Returns true if this object has handled the DulMessage, otherwise false.</returns>
		private bool HandleDulMessage(DulMessage dulMessage)
		{
			bool handled = false;

			if (dulMessage is Abort)
			{
				handled = HandleAbort(dulMessage as Abort);
			}
			else if (dulMessage is AssociateAc)
			{
				handled = HandleAssociateAccept(dulMessage as AssociateAc);
			}
			else if (dulMessage is AssociateRj)
			{
				handled = HandleAssociateReject(dulMessage as AssociateRj);
			}
			else if (dulMessage is AssociateRq)
			{
				handled = HandleAssociateRequest(dulMessage as AssociateRq);
			}
			else if (dulMessage is ReleaseRp)
			{
				handled = HandleReleaseResponse(dulMessage as ReleaseRp);
			}
			else if (dulMessage is ReleaseRq)
			{
				handled = HandleReleaseRequest(dulMessage as ReleaseRq);
			}
			else
			{
				Debug.Assert(true, "Not implemented yet.");
			}

			return(handled);
		}

		/// <summary>
		/// This method is called by a MessageIterator to let this object try
		/// to handle the received message.
		/// </summary>
		/// <param name="message">The received message.</param>
		/// <returns>Returns true if this object has handled the message, otherwise false.</returns>
		internal bool HandleMessage(Message message)
		{
			bool handled = false;

			if (message is DulMessage)
			{
				handled = HandleDulMessage(message as DulMessage);
			}
			else if (message is DicomMessage)
			{
				handled = HandleDicomMessage(message as DicomMessage);
			}
			else
			{
				Debug.Assert(true, "Not supposed to get here.");
			}

			return(handled);
		}

		/// <summary>
		/// Override this method to handle a N-ACTION-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received N-ACTION-RQ message.</param>
		/// <returns>Return true when this methods has handled the received N-ACTION-RQ message , otherwise false.</returns>
		public virtual bool HandleNActionRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-ACTION-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received N-ACTION-RSP message.</param>
		/// <returns>Return true when this methods has handled the received N-ACTION-RSP message , otherwise false.</returns>
		public virtual bool HandleNActionResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-CREATE-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received N-CREATE-RQ message.</param>
		/// <returns>Return true when this methods has handled the received N-CREATE-RQ message , otherwise false.</returns>
		public virtual bool HandleNCreateRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-CREATE-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received N-CREATE-RSP message.</param>
		/// <returns>Return true when this methods has handled the received N-CREATE-RSP message , otherwise false.</returns>
		public virtual bool HandleNCreateResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-DELETE-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received N-DELETE-RQ message.</param>
		/// <returns>Return true when this methods has handled the received N-DELETE-RQ message , otherwise false.</returns>
		public virtual bool HandleNDeleteRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-DELETE-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received N-DELETE-RSP message.</param>
		/// <returns>Return true when this methods has handled the received N-DELETE-RSP message , otherwise false.</returns>
		public virtual bool HandleNDeleteResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-EVENT-REPORT-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received N-EVENT-REPORT-RQ message.</param>
		/// <returns>Return true when this methods has handled the received N-EVENT-REPORT-RQ message , otherwise false.</returns>
		public virtual bool HandleNEventReportRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-EVENT-REPORT-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received N-EVENT-REPORT-RSP message.</param>
		/// <returns>Return true when this methods has handled the received N-EVENT-REPORT-RSP message , otherwise false.</returns>
		public virtual bool HandleNEventReportResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-GET-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received N-GET-RQ message.</param>
		/// <returns>Return true when this methods has handled the received N-GET-RQ message , otherwise false.</returns>
		public virtual bool HandleNGetRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-GET-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received N-GET-RSP message.</param>
		/// <returns>Return true when this methods has handled the received N-GET-RSP message , otherwise false.</returns>
		public virtual bool HandleNGetResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-SET-RQ message.
		/// </summary>
		/// <param name="dicomMessage">The received N-SET-RQ message.</param>
		/// <returns>Return true when this methods has handled the received N-SET-RQ message , otherwise false.</returns>
		public virtual bool HandleNSetRequest(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle a N-SET-RSP message.
		/// </summary>
		/// <param name="dicomMessage">The received N-SET-RSP message.</param>
		/// <returns>Return true when this methods has handled the received N-SET-RSP message , otherwise false.</returns>
		public virtual bool HandleNSetResponse(DicomMessage dicomMessage)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle an A-RELEASE-RQ.
		/// </summary>
		/// <param name="releaseRq">The received A-RELEASE-RQ.</param>
		/// <returns>Return true when this methods has handled the received A-RELEASE-RQ, otherwise false.</returns>
		public virtual bool HandleReleaseRequest(ReleaseRq releaseRq)
		{
			return false;
		}

		/// <summary>
		/// Override this method to handle an A-RELEASE-RP.
		/// </summary>
		/// <param name="releaseRp">The received A-RELEASE-RP.</param>
		/// <returns>Return true when this methods has handled the received A-RELEASE-RP, otherwise false.</returns>
		public virtual bool HandleReleaseResponse(ReleaseRp releaseRp)
		{
			return false;
		}

		/// <summary>
		/// Send a DicomMessage.
		/// </summary>
		/// <param name="dicomMessage">The DicomMessage.</param>
		public void Send(DicomMessage dicomMessage)
		{
			this.DicomThread.Send(dicomMessage);
		}

		// TODO!!!: split up in three different methods and add code comments.
		public AssociateAc SendAssociateAc(params Object[] parameters)
		{
			return(this.DicomThread.SendAssociateAc(parameters));
		}

		/// <summary>
		/// Send a A-ASSOCIATE-RQ.
		/// </summary>
		/// <param name="presentationContexts">One or more PresentationContext objects.</param>
		/// <returns>The A-ASSOCIATE-RQ</returns>
		public AssociateRq SendAssociateRq(params PresentationContext[] presentationContexts)
		{
			return(this.DicomThread.SendAssociateRq(presentationContexts));
		}

		/// <summary>
		/// Send a A-RELEASE-RSP.
		/// </summary>
		/// <returns>The A-RELEASE-RSP.</returns>
		public ReleaseRp SendReleaseRp()
		{
			return(this.DicomThread.SendReleaseRp());
		}

		/// <summary>
		/// Send a A-RELEASE-RQ.
		/// </summary>
		/// <returns>The A-RELEASE-RQ.</returns>
		public ReleaseRq SendReleaseRq()
		{
			return(this.DicomThread.SendReleaseRq());
		}

		/// <summary>
		/// Write an error to the results.
		/// </summary>
		/// <param name="text">The error text.</param>
		public void WriteError(String text)
		{
			this.DicomThread.WriteErrorInternal(text);
		}

		/// <summary>
		/// Write information to the results.
		/// </summary>
		/// <param name="text">The information text.</param>
		public void WriteInformation(String text)
		{
			this.DicomThread.WriteInformationInternal(text);
		}

		/// <summary>
		/// Write a warning to the results.
		/// </summary>
		/// <param name="text">The warning text.</param>
		public void WriteWarning(String text)
		{
			this.DicomThread.WriteWarningInternal(text);
		}
	}
}
