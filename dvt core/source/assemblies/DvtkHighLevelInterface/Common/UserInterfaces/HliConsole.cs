using System;



namespace DvtkHighLevelInterface.UserInterfaces
{
	/// <summary>
	/// Represents a HLI User Interface that shows output events, from Threads attached to,
	/// on the console.
	/// </summary>
	public class HliConsole: UserInterface
	{	
		//
		// - Fields -
		//

		/// <summary>
		/// The identifier of the Thread, which output event has been handled the last time.
		/// If no output event has been handled yet, this contains String.Empty.
		/// </summary>
		private String identifierLastHandledThread = String.Empty;



		//
		// - Methods -
		//

		/// <summary>
		/// Handle an error output event from a Thread by writing it to the console.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected override void HandleErrorOutputEvent(Thread thread, String text)
		{
			Write(thread, text, "Error");
		}

		/// <summary>
		/// Handle an information output event from a Thread by writing it to the console.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected override void HandleInformationOutputEvent(Thread thread, String text)
		{
			Write(thread, text, "Information");
		}

		/// <summary>
		/// Handle a warning output event from a Thread by writing it to the console.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		protected override void HandleWarningOutputEvent(Thread thread, String text)
		{
			Write(thread, text, "Warning");
		}

		/// <summary>
		/// Handle the Thread.ShowAndContinue method by writing the text to the console.
		/// </summary>
		/// <param name="thread">The Thread that called the Thread.ShowAndContinue method.</param>
		/// <param name="text">The text to show.</param>
		protected override void ShowAndContinue(Thread thread, String text)
		{
			if (text.Length == 0)
			{
				Write(thread, "", "Instruction finished");
			}
			else
			{
				Write(thread, text, "Instruction");
			}
		}

		/// <summary>
		/// Write text, from a Thread output event, to the console.
		/// </summary>
		/// <param name="thread">The Thread that generated the output event.</param>
		/// <param name="text">The text from the output event.</param>
		/// <param name="type">
		/// String that indicates if the is information, a warning or an error.
		/// </param>
		private void Write(Thread thread, String text, String type)
		{
			if (this.identifierLastHandledThread != thread.Options.Identifier)
			{
				Console.WriteLine("-----");
				Console.WriteLine("----- [" + thread.Options.Identifier + "]");
				this.identifierLastHandledThread = thread.Options.Identifier;
			}

			Console.WriteLine("-----");
			Console.WriteLine("(" + type + ")");
			Console.WriteLine(text + "");
		}
	}
}
