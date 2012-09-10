using System;



namespace DvtkHighLevelInterface.UserInterfaces
{
	/// <summary>
	/// Represents a HLI User Interface that shows output events, from Threads attached to,
	/// on a form. The actual form is executed on a seperate thread, so an object of this
	/// class will not block on the actual .Net form.
	/// 
	/// It also offers a Stop button by which all attached Threads may be stopped.
	/// </summary>
	public class HliForm: UserInterface
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property AutoExit. 
		/// </summary>
		private bool autoExit = true;

		/// <summary>
		/// The thread in which the "actual" .Net form will be started. 
		/// </summary>
		private System.Threading.Thread dotNetThread = null;

		/// <summary>
		/// The "actual" .Net form that is displayed. 
		/// </summary>
		private HliInternalForm hliInternalForm = null;

		/// <summary>
		/// See property LockObject. 
		/// </summary>
		private Object lockObject = new Object();

		/// <summary>
		/// Holds the count of the different ThreadStates of the attached Threads.
		/// </summary>
		private int[] threadStateCount = new int[4];

		/// <summary>
		/// Handler for ThreadStateChangeEvents from attached Threads. 
		/// </summary>
		private Thread.ThreadStateChangeHandler threadStateChangeHandler = null;



		//
		// - Constructors -
		//

		/// <summary>
		/// Default constructor.
		/// </summary>
		public HliForm()
		{
			threadStateCount[(int)ThreadState.UnStarted] = 0;
			threadStateCount[(int)ThreadState.Running] = 0;
			threadStateCount[(int)ThreadState.Stopping] = 0;
			threadStateCount[(int)ThreadState.Stopped] = 0;
		
			this.threadStateChangeHandler = new DvtkHighLevelInterface.Thread.ThreadStateChangeHandler(this.HandleThreadStateChange);
			this.dotNetThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadEntryPoint));

			// Start the actual .Net form in a seperate thread.
			this.dotNetThread.Start();
		}



		//
		// - Properties -
		//

		/// <summary>
		/// Indicates if this form must automatically be closed when the total number
		/// of running and stopping Threads becomes 0.
		/// </summary>
		public bool AutoExit
		{
			get
			{
				return(this.autoExit);
			}
			set
			{
				this.autoExit = value;
			}
		}

		/// <summary>
		/// Boolean indicating if the internal form is available, meaning
		/// if it is allowed to invoke methods on the internal form.
		/// </summary>
		private bool IsInternalFormAvailable
		{
			get
			{
				bool isInternalFormAvailable = false;

				if (hliInternalForm != null)
				{
					if (hliInternalForm.IsAvailable)
					{
						isInternalFormAvailable = true;
					}
				}
			
				return(isInternalFormAvailable);
			}
		}

		/// <summary>
		/// Use this property in combination with a lock when:
		/// - Accessing the field threadStateCount.
		/// - Accessing the property IsInternalFormAvailable.
		/// - Calling the Invoke method on the internal Form.
		/// </summary>
		internal Object LockObject
		{
			get
			{
				return(this.lockObject);
			}
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Attach to a Thread.
		/// 
		/// By doing so, this object may react to information, warning and error output events
		/// from the Thread.
		/// 
		/// Also Thread.ShowAndContinue method calls from the Thread are now handled by this object.
		/// 
		/// Threads may not be already started when attaching.
		/// </summary>
		/// <param name="thread">The Thread to attach to.</param>
		public override void Attach(Thread thread)
		{
			base.Attach(thread);

			lock(this.lockObject)
			{
				threadStateCount[(int)ThreadState.UnStarted]++;
			}
			
			thread.ThreadStateChangeEvent += this.threadStateChangeHandler;
		}

		/// <summary>
		/// Handle an error output event from a Thread by writing it to the form.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected override void HandleErrorOutputEvent(Thread thread, String text)
		{
			WriteActivityLogging(thread, text, System.Drawing.Color.Red);
		}

		/// <summary>
		/// Handle an information output event from a Thread by writing it to the form.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected override void HandleInformationOutputEvent(Thread thread, String text)
		{
			WriteActivityLogging(thread, text, System.Drawing.Color.Black);
		}

		/// <summary>
		/// Handle the changing of the ThreadState of one of the attached Threads.
		/// </summary>
		/// <param name="thread">The Thread which ThreadState is changed.</param>
		/// <param name="oldThreadState">The old ThreadState.</param>
		/// <param name="newThreadState">The new ThreadState.</param>
		private void HandleThreadStateChange(Thread thread, ThreadState oldThreadState, ThreadState newThreadState)
		{
			int numberOfRunningAndStoppingThreads = 0;

			// Determine the new number of running and stopping Threads.
			lock(this.lockObject)
			{
				threadStateCount[(int)oldThreadState]--;
				threadStateCount[(int)newThreadState]++;

				numberOfRunningAndStoppingThreads = 
					threadStateCount[(int)ThreadState.Running] + 
					threadStateCount[(int)ThreadState.Stopping];
			}

			// Update the text (caption) of the form.
			for (int index = 0; index < 10; index++)
			{
				lock(this.lockObject)
				{
					if (IsInternalFormAvailable)
					{
						int attachedThreadsCount = 
							threadStateCount[(int)ThreadState.UnStarted] + 
							threadStateCount[(int)ThreadState.Running] + 
							threadStateCount[(int)ThreadState.Stopping] + 
							threadStateCount[(int)ThreadState.Stopped];

						String captionText = 
							attachedThreadsCount.ToString() + " attached thread(s) (" + 
							threadStateCount[(int)ThreadState.UnStarted].ToString() + " unstarted, " +
							threadStateCount[(int)ThreadState.Running].ToString() + " running, " +
							threadStateCount[(int)ThreadState.Stopping].ToString() + " stopping, " +
							threadStateCount[(int)ThreadState.Stopped].ToString() + " stopped)";

						this.hliInternalForm.Invoke(this.hliInternalForm.SetTextHandler, new object[]{captionText});
							
						// Break the for loop.
						break;
					}
				}

				// Try again after 50 milliseconds.
				System.Threading.Thread.Sleep(50);
			}

			// If the new number of running and stopping Threads becomes 0,
			// close the internal Form or change the text on the Stop button.
			// This depends on the value of field autoExit.
			if (numberOfRunningAndStoppingThreads == 0)
			{
				for (int index = 0; index < 10; index++)
				{
					lock(this.lockObject)
					{
						if (IsInternalFormAvailable)
						{
							if (this.autoExit)
							{
								this.hliInternalForm.Invoke(this.hliInternalForm.CloseHandler);
							}
							else
							{
								this.hliInternalForm.Invoke(this.hliInternalForm.SetStopButtonTextHandler, new object[]{"Exit"});
							}
							
							// Break the for loop.
							break;
						}
					}

					// Try again after 50 milliseconds.
					System.Threading.Thread.Sleep(50);
				}
			}
		}

		/// <summary>
		/// Handle a warning output event from a Thread by writing it to the form.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected override void HandleWarningOutputEvent(Thread thread, String text)
		{
			WriteActivityLogging(thread, text, System.Drawing.Color.Orange);
		}

		/// <summary>
		/// Handle the Thread.ShowAndContinue method call from one of the attached Threads.
		/// 
		/// If the text argument is non-empty, show the text to the user.
		/// Previously shown text from this Thread will be removed.
		/// Also the Instructions Tab will become the selected one.
		/// 
		/// If the text argument is empty, remove the previously shown text from this Thread.
		/// If no text is shown anymore from any attached Threads, the Activity logging Tab becomes
		/// the selected one.
		/// </summary>
		/// <param name="thread">The thread on which the ShowAndContinue method was called.</param>
		/// <param name="text">The text to show.</param>
		protected override void ShowAndContinue(Thread thread, String text)
		{
			for (int index = 0; index < 10; index++)
			{
				lock(this.lockObject)
				{
					if (IsInternalFormAvailable)
					{	
						this.hliInternalForm.Invoke(this.hliInternalForm.ShowAndContinueHandler, new object[]{thread, text});

						// Break the for loop.
						break;
					}
				}

				// Try again after 50 milliseconds.
				System.Threading.Thread.Sleep(50);
			}
		}

		/// <summary>
		/// The entry point of the .Net thread, in which the .Net form is started.
		/// </summary>
		private void ThreadEntryPoint()
		{
			System.Threading.Thread.CurrentThread.Name = "HliInternalForm";

			// Remark: any calls to .Net form outside this .Net threads must be performed using an Invoke.
			hliInternalForm = new HliInternalForm(this);

			// Show the form and wait until it is closed.
			this.hliInternalForm.ShowDialog();
		}

		/// <summary>
		/// The caller of this method will be halted until the internal .Net form is closed.
		/// </summary>
		public void WaitUntilClosed()
		{
			bool wait = true;

			while(wait)
			{
				lock(this.lockObject)
				{
					if (!IsInternalFormAvailable)
					{
						wait = false;
					}
				}

				if (wait)
				{
					System.Threading.Thread.Sleep(1000);
				}
			}
		}

		/// <summary>
		/// Write activity logging to the .Net form.
		/// </summary>
		/// <param name="thread">The Thread.</param>
		/// <param name="text">The text.</param>
		/// <param name="color">The color of the text.</param>
		private void WriteActivityLogging(Thread thread, String text, System.Drawing.Color color)
		{
			for (int index = 0; index < 20; index++)
			{
				lock(this.lockObject)
				{
					if (IsInternalFormAvailable)
					{
						UserControlActivityLogging activityLoggingControl = this.hliInternalForm.ActivityLoggingControl;

						// Because the thread of the caller of this method is different from the thread that started
						// the internal form, an Invoke must be used.
						activityLoggingControl.Invoke(activityLoggingControl.WriteHandler, new Object[]{thread, text, color});
						// Break the for loop.
						break;
					}
				}

				// Try again after 50 milliseconds.
				System.Threading.Thread.Sleep(50);
			}
		}
	}
}

