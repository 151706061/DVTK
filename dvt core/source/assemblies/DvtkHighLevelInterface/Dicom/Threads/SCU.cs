using System;
using System.Collections;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for SCU.
	/// </summary>
	public class SCU: DicomThreadTriggerLoop
	{
		public class DicomMessageCollectionPresentationContexts
		{
			internal DicomMessageCollection dicomMessageCollection = null;

			internal ArrayList presentationContexts = null;

			public DicomMessageCollectionPresentationContexts(DicomMessageCollection dicomMessageCollection, params PresentationContext[] presentationContexts)
			{
				this.dicomMessageCollection = dicomMessageCollection;
				this.presentationContexts = new ArrayList(presentationContexts);
			}
		}

		public SCU(bool usesPollingMechanism): base(usesPollingMechanism)
		{
		}

		public void Trigger(DicomMessage dicomMessage, params PresentationContext[] presentationContexts)
		{
			DicomMessageCollection dicomMessageCollection = new DicomMessageCollection();
			dicomMessageCollection.Add(dicomMessage);

			Trigger(dicomMessageCollection, presentationContexts);
		}

		public void Trigger(DicomMessageCollection dicomMessageCollection, params PresentationContext[] presentationContexts)
		{
			DicomMessageCollectionPresentationContexts trigger = new DicomMessageCollectionPresentationContexts(dicomMessageCollection, presentationContexts);

			base.Trigger(trigger);
		}
		
		public override void ProcessTrigger(Object trigger)
		{
			try
			{
				BeforeProcessTrigger(trigger);

				DicomMessageCollectionPresentationContexts typeCastTrigger = trigger as DicomMessageCollectionPresentationContexts;

				if (typeCastTrigger != null)
				{
					SendAssociation(typeCastTrigger.dicomMessageCollection, (PresentationContext[])typeCastTrigger.presentationContexts.ToArray(typeof(PresentationContext)));
				}
			}
			catch
			{
			}

			finally
			{
				AfterProcessTrigger(trigger);

				CheckForNewResultsFile();
			}
		}

		public virtual void BeforeProcessTrigger(Object trigger)
		{
		}

		public virtual void AfterProcessTrigger(Object trigger)
		{
		}
	}
}
