using System;
using DvtkData.Dimse;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface.InformationEntity
{
	/// <summary>
	/// Summary description for TempGenerateTriggers.
	/// </summary>
	public class TempGenerateTriggers
	{
		public static DvtkHighLevelInterface.Messages.DicomMessage MakeStorageCommitEvent(QueryRetrieveInformationModels informationModels, DvtkHighLevelInterface.Messages.DicomMessage actionMessage)
		{
			// refresh the information models
			informationModels.Refresh();

			DvtkHighLevelInterface.Messages.DicomMessage eventMessage = new DvtkHighLevelInterface.Messages.DicomMessage(DvtkData.Dimse.DimseCommand.NEVENTREPORTRQ);
			eventMessage.Set("0x00000002", DvtkData.Dimse.VR.UI, "1.2.840.10008.1.20.1");
			eventMessage.Set("0x00001000", DvtkData.Dimse.VR.UI, "1.2.840.10008.1.20.1.1");
			eventMessage.Set("0x00001002", DvtkData.Dimse.VR.US, 1);

			DvtkData.Dimse.DataSet actionDataset = actionMessage.DataSet.DvtkDataDataSet;

			DvtkData.Dimse.DataSet eventDataset = new DvtkData.Dimse.DataSet();

			DvtkData.Dimse.Attribute eventReferenceSopSequence = new DvtkData.Dimse.Attribute(0x00081199, DvtkData.Dimse.VR.SQ);
			SequenceOfItems eventReferenceSopSequenceOfItems = new SequenceOfItems();
			eventReferenceSopSequence.DicomValue = eventReferenceSopSequenceOfItems;

			DvtkData.Dimse.Attribute eventFailedSopSequence = new DvtkData.Dimse.Attribute(0x00081198, DvtkData.Dimse.VR.SQ);
			SequenceOfItems eventFailedSopSequenceOfItems = new SequenceOfItems();
			eventFailedSopSequence.DicomValue = eventFailedSopSequenceOfItems;

			if (actionDataset != null)
			{
				DvtkData.Dimse.Attribute transactionUid = actionDataset.GetAttribute(DvtkData.Dimse.Tag.TRANSACTION_UID);
				if (transactionUid != null)
				{
					eventDataset.Add(transactionUid);
				}

				DvtkData.Dimse.Attribute referencedSopSequence = actionDataset.GetAttribute(DvtkData.Dimse.Tag.REFERENCED_SOP_SEQUENCE);
				if (referencedSopSequence != null)
				{
					SequenceOfItems sequenceOfItems = (SequenceOfItems)referencedSopSequence.DicomValue;
					foreach(DvtkData.Dimse.SequenceItem item in sequenceOfItems.Sequence)
					{
						System.String sopClassUid = "";
						System.String sopInstanceUid = "";

						DvtkData.Dimse.Attribute attribute = item.GetAttribute(DvtkData.Dimse.Tag.REFERENCED_SOP_CLASS_UID);
						if (attribute != null)
						{
							UniqueIdentifier uniqueIdentifier = (UniqueIdentifier)attribute.DicomValue;
							sopClassUid = uniqueIdentifier.Values[0];
						}

						attribute = item.GetAttribute(DvtkData.Dimse.Tag.REFERENCED_SOP_INSTANCE_UID);
						if (attribute != null)
						{
							UniqueIdentifier uniqueIdentifier = (UniqueIdentifier)attribute.DicomValue;
							sopInstanceUid = uniqueIdentifier.Values[0];
						}

						if (informationModels.PatientRoot.IsInstanceInInformationModel(sopClassUid, sopInstanceUid))
						{
							DvtkData.Dimse.SequenceItem itemOk = new DvtkData.Dimse.SequenceItem();
							itemOk.AddAttribute(0x00081150, DvtkData.Dimse.VR.UI, sopClassUid);
							itemOk.AddAttribute(0x00081155, DvtkData.Dimse.VR.UI, sopInstanceUid);

							// add instance to committed list
							eventReferenceSopSequenceOfItems.Sequence.Add(itemOk);
						}
						else
						{
							DvtkData.Dimse.SequenceItem itemNotOk = new DvtkData.Dimse.SequenceItem();
							itemNotOk.AddAttribute(0x00081150, DvtkData.Dimse.VR.UI, sopClassUid);
							itemNotOk.AddAttribute(0x00081155, DvtkData.Dimse.VR.UI, sopInstanceUid);
							itemNotOk.AddAttribute(0x00081197, DvtkData.Dimse.VR.US, 0x0110);

							// add instance to failed list
							eventFailedSopSequenceOfItems.Sequence.Add(itemNotOk);
						}
					}
				}

				if (eventReferenceSopSequenceOfItems.Sequence.Count > 0)
				{
					eventDataset.Add(eventReferenceSopSequence);
				}

				if (eventFailedSopSequenceOfItems.Sequence.Count > 0)
				{
					eventDataset.Add(eventFailedSopSequence);
				}
			}

			eventMessage.DataSet.DvtkDataDataSet = eventDataset;

			return eventMessage;
		}
	}
}
