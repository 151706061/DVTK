using System;
using System.Collections;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.UserInterfaces;


namespace DvtkHighLevelInterface
{
	/// <summary>
	/// This class contains the shared threading functionality of a DicomThread and Hl7Thread.
	/// </summary>
	public abstract class Thread
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property AttachedUserInterfaces.
		/// </summary>
		protected ArrayList attachedUserInterfaces = ArrayList.Synchronized(new ArrayList());

		/// <summary>
		/// The child Threads.
		/// </summary>
		protected ThreadCollection childs = null;

		/// <summary>
		/// This common thread lock is created by the ThreadManager of this object.
		/// This field contains a reference to it.
		/// It is used when:
		/// - Changing the state of a Thread object.
		/// - Accessing or changing lists of Thread objects in any class.
		/// </summary>
		protected Object commonThreadLock = null;

		/// <summary>
		/// The .Net thread that performs the actual execution in a seperate thread.
		/// </summary>
		protected System.Threading.Thread dotNetThread = null;

		/// <summary>
		/// See property HasBeenStarted.
		/// </summary>
		private bool hasBeenStarted = false;

		/// <summary>
		/// Initial number of milliseconds to wait before executing code in the thread.
		/// </summary>
		protected int initialMillisecondsToWait = 0;

		/// <summary>
		/// The parent thread if existing. Otherwise null.
		/// </summary>
		protected Thread parent = null;

		/// <summary>
		/// See property ThreadManager.
		/// </summary>
		private ThreadManager threadManager = null;

		/// <summary>
		/// See property Options.
		/// This field must be set by a descendant of this class.
		/// </summary>
		protected ThreadOptions threadOptions = null;

		/// <summary>
		/// The current state of the thread.
		/// Always use the ThreadState property to change this field.
		/// </summary>
		private ThreadState threadState = ThreadState.UnStarted;

		/// <summary>
		/// See property TopmostThread.
		/// </summary>
		private Thread topmostThread = null;



		protected Validator validator = null;


		//
		// - Delegates -
		//

		/// <summary>
		/// The delegate used for the events ErrorOutputEvent, InformationOutputEvent and WarningOutputEvent.
		/// </summary>
		public delegate void TextOutputEventHandler(Thread thread, String text);

		/// <summary>
		/// The delegate used for the ThreadStateChangeEvent.
		/// </summary>
		public delegate void ThreadStateChangeHandler(Thread thread, ThreadState oldThreadState, ThreadState newThreadState);



		//
		// - Events -
		//


		/// <summary>
		/// This event should be triggered by a descendant of this class when some error output is available.
		/// </summary>
		public event TextOutputEventHandler ErrorOutputEvent;

		/// <summary>
		/// This event should be triggered by a descendant of this class when some information output is available.
		/// </summary>
		public event TextOutputEventHandler InformationOutputEvent;

		/// <summary>
		/// This event should be triggered by a descendant of this class when some warning output is available.
		/// </summary>
		public event TextOutputEventHandler WarningOutputEvent;

		/// <summary>
		/// This event is triggered whenever the ThreadState changes for this object.
		/// </summary>
		public event ThreadStateChangeHandler ThreadStateChangeEvent;



		public Thread()
		{
			this.validator = new Validator(this);
		}



		//
		// - Properties -
		//

		/// <summary>
		/// The attached UserInterfaces of this object.
		/// 
		/// An example of a UserInterface is the HliConsole class, which just outputs the text available
		/// in the ErrorOutputEvent, InformationOutputEvent and WarningOutputEvent to the console.
		/// </summary>
		internal ArrayList AttachedUserInterfaces
		{
			get
			{
				return(this.attachedUserInterfaces);
			}
		}

		/// <summary>
		/// Gets the DataWarehouse of the ThreadManager that manages this object.
		/// See the DataWarehouse class for more information.
		/// </summary>
		public DataWarehouse DataWarehouse
		{
			get
			{
				return(this.threadManager.DataWarehouse );
			}
		}

		/// <summary>
		/// Indicates if this Thread has ever been started.
		/// </summary>
		public bool HasBeenStarted
		{
			get
			{
				return(this.hasBeenStarted);
			}
		}

		public bool IsStopped
		{
			get
			{
				bool isStopped = false;

				lock(this.commonThreadLock)
				{	
					if (this.threadState == ThreadState.Stopped)
					{
						isStopped = true;
					}
				}
	
				return(isStopped);
			}
		}

		/// <summary>
		/// Gets the options for this object.
		/// </summary>
		public ThreadOptions Options
		{
			get
			{
				return(this.threadOptions);
			}
		}

		public Thread Parent
		{
			get
			{
				return(this.parent);
			}
		}

		/// <summary>
		/// Gets the ThreadManager that manages this Thread.
		/// </summary>
		public ThreadManager ThreadManager
		{
			get
			{
				return(this.threadManager);
			}
		}

		/// <summary>
		/// Gets the current state of this object.
		/// </summary>
		public ThreadState ThreadState
		{
			get
			{
				ThreadState threadState = ThreadState.UnStarted;

				lock(this.commonThreadLock)
				{
					threadState = this.threadState;
				}

				return(threadState);
			}
			set
			{
				ThreadState oldThreadState = ThreadState.UnStarted;
				ThreadState newThreadState = ThreadState.UnStarted;

				lock(this.commonThreadLock)
				{
					oldThreadState = this.threadState;
					this.threadState = value;
					newThreadState = this.threadState;
				}

				if (ThreadStateChangeEvent != null)
				{
					ThreadStateChangeEvent(this, oldThreadState, newThreadState);
				}
			}
		}

		/// <summary>
		/// Gets the topmost Thread, considering the parent relation.
		/// If this thread does not have a parent, this object itself is the topmost thread.
		/// </summary>
		public Thread TopmostThread
		{
			get
			{
				return this.topmostThread;
			}
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Code that normally would be present in the constructor.
		/// This code is however put in a separate method to be able to have only
		/// one constructor in DicomThread. This way, it is easier to derive from a
		/// DicomThread class.
		/// 
		/// Use this method if this object should have a parent thread.
		/// </summary>
		/// <param name="parent">The parent Thread.</param>
		protected void Initialize(Thread parent)
		{
			this.parent = parent;
			this.dotNetThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadEntryPoint));
			this.threadManager = this.parent.ThreadManager;
			this.commonThreadLock = this.threadManager.CommonThreadLock;
			this.childs = new ThreadCollection(this.commonThreadLock);
			this.topmostThread = this.parent.TopmostThread;

			lock(this.commonThreadLock)
			{
				this.parent.childs.Add(this);
				this.parent.ThreadManager.Threads.Add(this);
			}

			if (this.parent.Options.AttachChildsToUserInterfaces)
			{
				foreach(UserInterface userInterface in this.parent.AttachedUserInterfaces)
				{
					userInterface.Attach(this);
				}
			}
		}

		/// <summary>
		/// Code that normally would be present in the constructor.
		/// This code is however put in a separate method to be able to have only
		/// one constructor in DicomThread. This way, it is easier to derive from a
		/// DicomThread class.
		/// 
		/// Use this method if this threads should not have a parent thread.
		/// </summary>
		/// <param name="threadManager">The ThreadManager that manages this object.</param>
		protected void Initialize(ThreadManager threadManager)
		{
			this.parent = null;
			this.dotNetThread = new System.Threading.Thread(new System.Threading.ThreadStart(this.ThreadEntryPoint));
			this.threadManager = threadManager;
			this.commonThreadLock = this.threadManager.CommonThreadLock;
			this.childs = new ThreadCollection(this.commonThreadLock);
			this.topmostThread = this;

			lock(this.commonThreadLock)
			{
				this.threadManager.Childs.Add(this);
				this.threadManager.Threads.Add(this);
			}
		}

		protected void ShowAndContinue()
		{
			foreach (UserInterface userInterface in attachedUserInterfaces)
			{
				userInterface.ShowAndContinueInternal(this, "");
			}
		}

		protected void ShowAndContinue(String text)
		{
			foreach (UserInterface userInterface in attachedUserInterfaces)
			{
				userInterface.ShowAndContinueInternal(this, text);
			}
		}

		/// <summary>
		/// Start the Thread without an initial number of milliseconds to wait.
		/// 
		/// The Thread will only start when it has not already been started.
		/// </summary>
		public void Start()
		{
			Start(0);
		}

		/// <summary>
		/// Start the Thread and wait an initial number of milleseconds before
		/// executing the code that is defined in the overriden Execute method.
		/// 
		/// The Thread will only start when it has not already been started.
		/// </summary>
		/// <param name="initialMillisecondsToWait">Number of milliseconds.</param>
		public void Start(int initialMillisecondsToWait)
		{
			lock(commonThreadLock)
			{
				if (ThreadState == ThreadState.UnStarted)
				{
					this.threadManager.ChangeThreadState(this, ThreadState.Running);

					this.initialMillisecondsToWait = initialMillisecondsToWait;

					this.hasBeenStarted = true;

					this.dotNetThread.Start();
				}
			}
		}

		/// <summary>
		/// Stop this thread.
		///  
		/// This method needs to be implemented in a descendant of this class.
		/// Do this in such a way that child threads that have been started and
		/// have not yet stopped are stopped first.
		/// </summary>
		public abstract void Stop();

		/// <summary>
		/// The thread entry point used by the .Net thread.
		/// </summary>
		protected abstract void ThreadEntryPoint();

		/// <summary>
		/// Call this method when this object should output an error. 
		/// </summary>
		/// <param name="text">The error text.</param>
		protected void TriggerErrorOutputEvent(String text)
		{
			if (ErrorOutputEvent != null)
			{
				ErrorOutputEvent(this, text);
			}
		}

		/// <summary>
		/// Call this method when this object should output information. 
		/// </summary>
		/// <param name="text">The information text.</param>
		protected void TriggerInformationOutputEvent(String text)
		{
			if (InformationOutputEvent != null)
			{
				InformationOutputEvent(this, text);
			}
		}

		/// <summary>
		/// Call this method when this object should output a warning. 
		/// </summary>
		/// <param name="text">The warning text.</param>
		protected void TriggerWarningOutputEvent(String text)
		{
			if (WarningOutputEvent != null)
			{
				WarningOutputEvent(this, text);
			}
		}

		/// <summary>
		/// Wait until this object has the ThreadState UnStarted or Stopped.
		/// </summary>
		public void WaitForCompletion()
		{
			bool wait = true;

			while(wait)
			{
				wait = false;

				lock(this.commonThreadLock)
				{
					if ((this.threadState == ThreadState.Running) || (this.threadState == ThreadState.Stopping))
					{
						wait = true;
					}
				}

				if (wait)
				{
					System.Threading.Thread.Sleep(500);
				}
			}
		}

		/// <summary>
		/// Write an error to the results.
		/// </summary>
		/// <param name="text">The error text.</param>
		public abstract void WriteError(String text);

		/// <summary>
		/// Make it possible for another class within this assembly to write
		/// an error.
		/// </summary>
		/// <param name="text"></param>
		internal void WriteErrorInternal(String text)
		{
			WriteError(text);
		}

		/// <summary>
		/// Write information to the results.
		/// </summary>
		/// <param name="text">The information text.</param>
		public abstract void WriteInformation(String text);

		/// <summary>
		/// Make it possible for another class within this assembly to write
		/// information.
		/// </summary>
		/// <param name="text"></param>
		internal void WriteInformationInternal(String text)
		{
			WriteInformation(text);
		}

		/// <summary>
		/// Write a warning to the results.
		/// </summary>
		/// <param name="text">The warning text.</param>
		public abstract void WriteWarning(String text);

		/// <summary>
		/// Make it possible for another class within this assembly to write
		/// a warning.
		/// </summary>
		/// <param name="text"></param>
		internal void WriteWarningInternal(String text)
		{
			WriteWarning(text);
		}
	}
}
