using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for DicomThreadTriggerLoop.
	/// </summary>
	public abstract class DicomThreadTriggerLoop: DicomThread
	{
		private bool usesPollingMechanism = true;

		private int loopDelay = 500;

		private bool keepLooping = true;

		private System.Collections.Queue triggerQueue = null;

		public DicomThreadTriggerLoop(bool usesPollingMechanism)
		{
			this.usesPollingMechanism = usesPollingMechanism;

			triggerQueue = System.Collections.Queue.Synchronized(new System.Collections.Queue());
		}

		public int LoopDelay
		{
			get
			{
				return(this.loopDelay);
			}
			set
			{
				this.loopDelay = value;
			}
		}

		protected override void Execute()
		{
			// Calling the Stop method while break through this loop.
			while (this.keepLooping)
			{
				// Delay before checking if a new trigger is available
				System.Threading.Thread.Sleep(this.loopDelay);

				Object trigger = null;

				if (this.usesPollingMechanism)
				{
					// Get the trigger from the overridden GetTrigger()
					while ((trigger = GetTrigger()) != null)
					{
						// Process the trigger
						ProcessTrigger(trigger);
							
						// Wait awhile before processing new Trigger
						System.Threading.Thread.Sleep(500);
					}
				}
				else
				{
					// Check if anything has been queued
					while (this.triggerQueue.Count != 0)
					{
						// Get the trigger
						trigger = this.triggerQueue.Dequeue();
						if (trigger != null)
						{
							// Process the trigger
							ProcessTrigger(trigger);

							// Wait awhile before processing new Trigger
							System.Threading.Thread.Sleep(500);
						}
					}
				}
			}
		}
	
		// Call this when usesPollingMechanism is set to false.
		public void Trigger(Object trigger)
		{
			if (!this.usesPollingMechanism)
			{
				this.triggerQueue.Enqueue(trigger);
			}
		}

		// Override this when usesPollingMechanism is set to true.
		protected virtual Object GetTrigger()
		{
			return(null);
		}

		public override void Stop()
		{
			this.keepLooping = false;

			// Give the loop in the Execute method time to stop.
			System.Threading.Thread.Sleep(1500);

			// In case the loop hasn't stopped by itself, stop the thread.
			base.Stop();
		}

		public abstract void ProcessTrigger(Object trigger);
	}
}
