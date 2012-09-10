using System;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// Abstract base class for all types of messages.
	/// </summary>
	public abstract class Message
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property IsReceived.
		/// </summary>
		private bool isReceived = false;

		/// <summary>
		/// See property IsSend.
		/// </summary>
		private bool isSend = false;

		/// <summary>
		/// See property IsProcessed.
		/// </summary>
		private bool isProcessed = false;



		//
		// - Properties -
		//

		/// <summary>
		/// The Abort (use the property IsAbort to check if this message is really an Abort).
		/// </summary>
		public Abort Abort
		{
			get
			{
				Abort abort = null;

				if (IsAbort)
				{
					abort = this as Abort;
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Script is using the Abort property for a Message that is actually of type " + this.ToString() + ".");
				}

				return(abort);
			}
		}

		/// <summary>
		/// The AssociateAc (use the property IsAssociateAc to check if this message is really an AssociateAc).
		/// </summary>
		public AssociateAc AssociateAc
		{
			get
			{
				AssociateAc associateAc = null;

				if (IsAssociateAc)
				{
					associateAc = this as AssociateAc;
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Script is using the AssociateAc property for a Message that is actually of type " + this.ToString() + ".");
				}

				return(associateAc);
			}
		}

		/// <summary>
		/// The AssociateRj (use the property IsAssociateRj to check if this message is really an AssociateRj).
		/// </summary>
		public AssociateRj AssociateRj
		{
			get
			{
				AssociateRj associateRj = null;

				if (IsAssociateRj)
				{
					associateRj = this as AssociateRj;
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Script is using the AssociateRj property for a Message that is actually of type " + this.ToString() + ".");
				}

				return(associateRj);
			}
		}

		/// <summary>
		/// The AssociateRq (use the property IsAssociateRq to check if this message is really an AssociateRq).
		/// </summary>
		public AssociateRq AssociateRq
		{
			get
			{
				AssociateRq associateRq = null;

				if (IsAssociateRq)
				{
					associateRq = this as AssociateRq;
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Script is using the AssociateRq property for a Message that is actually of type " + this.ToString() + ".");
				}

				return(associateRq);
			}
		}

		/// <summary>
		/// The DicomMessage (use the property IsDicomMessage to check if this message is really an DicomMessage).
		/// </summary>
		public DicomMessage DicomMessage
		{
			get
			{
				DicomMessage dicomMessage = null;

				if (IsDicomMessage)
				{
					dicomMessage = this as DicomMessage;
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Script is using the DicomMessage property for a Message that is actually of type " + this.ToString() + ".");
				}

				return(dicomMessage);
			}
		}

		/// <summary>
		/// Indicates if the message is an Abort.
		/// </summary>
		public bool IsAbort
		{
			get
			{
				bool isAbort = false;

				if (this is Abort)
				{
					isAbort = true;
				}
				else
				{
					isAbort = false;
				}

				return(isAbort);
			}
		}

		/// <summary>
		/// Indicates if the message is an AssociateAc.
		/// </summary>
		public bool IsAssociateAc
		{
			get
			{
				bool isAssociateAc = false;

				if (this is AssociateAc)
				{
					isAssociateAc = true;
				}
				else
				{
					isAssociateAc = false;
				}

				return(isAssociateAc);
			}		
		}

		/// <summary>
		/// Indicates if the message is an AssociateRj.
		/// </summary>
		public bool IsAssociateRj
		{
			get
			{
				bool isAssociateRj = false;

				if (this is AssociateRj)
				{
					isAssociateRj = true;
				}
				else
				{
					isAssociateRj = false;
				}

				return(isAssociateRj);
			}		
		}

		/// <summary>
		/// Indicates if the message is an AssociateRq.
		/// </summary>
		public bool IsAssociateRq
		{
			get
			{
				bool isAssociateRq = false;

				if (this is AssociateRq)
				{
					isAssociateRq = true;
				}
				else
				{
					isAssociateRq = false;
				}

				return(isAssociateRq);
			}		
		}

		/// <summary>
		/// Indicates if the message is a DicomMessage.
		/// </summary>
		public bool IsDicomMessage
		{
			get
			{
				bool isDicomMessage = false;

				if (this is DicomMessage)
				{
					isDicomMessage = true;
				}
				else
				{
					isDicomMessage = false;
				}

				return(isDicomMessage);
			}		
		}

		/// <summary>
		/// Indicates if this message has been processed.
		/// </summary>
		public bool IsProcessed
		{
			get
			{
				return this.isProcessed;
			}
			set
			{
				this.isProcessed = value;
			}
		}

		/// <summary>
		/// Indicates if this message has been received.
		/// </summary>
		public bool IsReceived
		{
			get
			{
				return this.isReceived;
			}
			set
			{
				this.isReceived = value;
			}
		}

		/// <summary>
		/// Indicates if the message is a ReleaseRp.
		/// </summary>
		public bool IsReleaseRp
		{
			get
			{
				bool isReleaseRp = false;

				if (this is ReleaseRp)
				{
					isReleaseRp = true;
				}
				else
				{
					isReleaseRp = false;
				}

				return(isReleaseRp);
			}		
		}
		
		/// <summary>
		/// Indicates if the message is a ReleaseRq.
		/// </summary>
		public bool IsReleaseRq
		{
			get
			{
				bool isReleaseRq = false;

				if (this is ReleaseRq)
				{
					isReleaseRq = true;
				}
				else
				{
					isReleaseRq = false;
				}

				return(isReleaseRq);
			}		
		}

		/// <summary>
		/// Indicates if this message has been send.
		/// </summary>
		public bool IsSend
		{
			get
			{
				return this.isSend;
			}
			set
			{
				this.isSend = value;
			}
		}

		/// <summary>
		/// The ReleaseRp (use the property IsDicomMessage to check if this message is really an DicomMessage).
		/// </summary>
		public ReleaseRp ReleaseRp
		{
			get
			{
				ReleaseRp releaseRp = null;

				if (IsReleaseRp)
				{
					releaseRp = this as ReleaseRp;
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Script is using the ReleaseRp property for a Message that is actually of type " + this.ToString() + ".");
				}

				return(releaseRp);
			}
		}

		/// <summary>
		/// The ReleaseRp (use the property IsDicomMessage to check if this message is really an DicomMessage).
		/// </summary>
		public ReleaseRq ReleaseRq
		{
			get
			{
				ReleaseRq releaseRq = null;

				if (IsReleaseRq)
				{
					releaseRq = this as ReleaseRq;
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Script is using the ReleaseRq property for a Message that is actually of type " + this.ToString() + ".");
				}

				return(releaseRq);
			}
		}
	}
}
