using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for ThreadState.!!!!!!!!!!!!!!!!!
	/// </summary>
	public enum ThreadState
	{
		UnStarted,	// Start has not yet been called.

		Running,	// Start has been called.

		Stopping,	// Execution is now in the final block of the ThreadEntryPoint method,
					// or the Stop method has been called and execution of the thread has
					// still not not finished.

		Stopped		// Execution of the thread has finished.
	}
}
