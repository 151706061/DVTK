using System;
using System.Collections;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for DataWarehouse.
	/// Maak duidelijk dat het om snapshots gaat!!!
	/// </summary>
	public class DataWarehouse
	{
		private MessageCollection globalMessages = new MessageCollection();

		public Object lockObject = new Object();

		public DataWarehouse()
		{
			// Nothing.
		}

		public DicomProtocolMessageCollection Messages(DicomThread dicomThread)
		{
			DicomProtocolMessageCollection dicomProtocolMessageCollection = new DicomProtocolMessageCollection();

			lock(lockObject)
			{
				foreach(DicomProtocolMessage dicomProtocolMessage in dicomThread.Messages)
				{
					dicomProtocolMessageCollection.Add(dicomProtocolMessage);
				}
			}

			return(dicomProtocolMessageCollection);
		}

		public void ClearMessages(DicomThread dicomThread)
		{
			lock(lockObject)
			{
				dicomThread.Messages.Clear();
			}
		}

		public MessageCollection GlobalMessages()
		{
			MessageCollection messageCollection = new MessageCollection();

			lock(lockObject)
			{
				foreach(Message message in globalMessages)
				{
					messageCollection.Add(message);
				}
			}

			return(messageCollection);
		}

		internal void AddMessage(DicomThread dicomThread, DicomProtocolMessage message)
		{
			lock(lockObject)
			{
				dicomThread.Messages.Add(message);
				globalMessages.Add(message);
			}
		}
	}
}
