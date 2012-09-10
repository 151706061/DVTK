using System;
using System.Collections;
using DvtkHighLevelInterface;



namespace DvtkHighLevelInterface.UserInterfaces
{
	/// <summary>
	/// Abstract base class for all classes that implement a HLI User Interface.
	/// 
	/// A HLI User Interface at least shows output events and shows instructions 
	/// as a result of Threads calling ShowAndContinue from Threads attached to.
	/// </summary>
	public abstract class UserInterface
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property AttachedThreads.
		/// </summary>
		private ArrayList attachedThreads = ArrayList.Synchronized(new ArrayList());

		/// <summary>
		/// Event handler that can handle an error output event from a Thread.
		/// </summary>
		private Thread.TextOutputEventHandler errorOutputEventHandler = null;

		/// <summary>
		/// Event handler that can handle an information output event from a Thread.
		/// </summary>
		private Thread.TextOutputEventHandler informationOutputEventHandler = null;

		/// <summary>
		/// Event handler that can handle a warning output event from a Thread.
		/// </summary>
		private Thread.TextOutputEventHandler warningOutputEventHandler = null;



		//
		// - Constructors -
		//

		/// <summary>
		/// Default constructor.
		/// </summary>
		public UserInterface()
		{
			errorOutputEventHandler = new Thread.TextOutputEventHandler(this.HandleErrorOutputEvent);
			informationOutputEventHandler = new Thread.TextOutputEventHandler(this.HandleInformationOutputEvent);
			warningOutputEventHandler = new Thread.TextOutputEventHandler(this.HandleWarningOutputEvent);
		}



		//
		// - Properties -
		//

		/// <summary>
		/// Returns the attached Threads.
		/// </summary>
		internal ArrayList AttachedThreads
		{
			get
			{
				return(this.attachedThreads);
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
		/// </summary>
		/// <param name="thread">The Thread to attach to.</param>
		public virtual void Attach(Thread thread)
		{
			this.attachedThreads.Add(thread);
			thread.AttachedUserInterfaces.Add(this);
			thread.ErrorOutputEvent += this.errorOutputEventHandler;
			thread.InformationOutputEvent += this.informationOutputEventHandler;
			thread.WarningOutputEvent += this.warningOutputEventHandler;
		}

		/// <summary>
		/// Handle an error output event from a Thread.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected abstract void HandleErrorOutputEvent(Thread thread, String text);

		/// <summary>
		/// Handle an information output event from a Thread.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected abstract void HandleInformationOutputEvent(Thread thread, String text);

		/// <summary>
		/// Handle a warning output event from a Thread.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected abstract void HandleWarningOutputEvent(Thread thread, String text);

		/// <summary>
		/// This method is called when an attached Thread calls the ShowAndContinue method.
		/// </summary>
		/// <param name="thread">The Thread.</param>
		/// <param name="text">The text to show.</param>
		protected abstract void ShowAndContinue(Thread thread, String text);

		/// <summary>
		/// By having this method and let HLI classes call this, the ShowAndContinu method may
		/// be kept protected instead of public.
		/// </summary>
		/// <param name="thread">The Thread.</param>
		/// <param name="text">The text to show.</param>
		internal void ShowAndContinueInternal(Thread thread, String text)
		{
			ShowAndContinue(thread, text);
		}
	}
}
