using System;
using System.IO;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for ThreadManager.
	/// </summary>
	public abstract class ThreadManager
	{
		/// <summary>
		/// See property ThreadLock.
		/// </summary>
		private Object commonThreadLock = new Object();

		private ThreadCollection childs = null;

		private DataWarehouse dataWarehouse = new DataWarehouse();

		private ThreadCollection threads = null;

		// TODO!!!! Explain why the threadlock needs to be used for the specied cases.

		/// <summary>
		/// Use this property in combination with a lock when:
		/// - Changing the state of a thread.
		/// - Access or change a list of Thread objects.
		/// </summary>
		public Object CommonThreadLock
		{
			get
			{
				return(this.commonThreadLock);
			}
		}

		public ThreadCollection Childs
		{
			get
			{
				return(this.childs);
			}
		}

		
		/// <summary>
		/// !!!!!This contains all the messages from each thread that is owned by the ThreadManager
		/// of this object.
		/// </summary>
		public DataWarehouse DataWarehouse
		{
			get
			{
				return(this.dataWarehouse);
			}
		}


		public ThreadCollection Threads
		{
			get
			{
				return(this.threads);
			}
		}

		public ThreadManager()
		{
			this.childs = new ThreadCollection(this.commonThreadLock);
			this.threads = new ThreadCollection(this.commonThreadLock);
		}

		internal void ChangeThreadState(Thread thread, ThreadState newThreadState)
		{
			lock(this.commonThreadLock)
			{
				thread.ThreadState = newThreadState;
			}
		}

		internal String GetUniqueIdentifier(Thread thread)
		{
			String uniqueIdentifier = "";

			lock(this.commonThreadLock)
			{
				int index = 1;

				while (IdentifierExists(thread.Options.Name + index.ToString()))
				{
					index++;
				}

				uniqueIdentifier = thread.Options.Name + index.ToString();
			}

			return (uniqueIdentifier);
		}

		private bool IdentifierExists(String identifier)
		{
			bool identifierExists = false;

			foreach (Thread thread in this.threads)
			{	
				if (thread.Options.Identifier != null)
				{
					if (thread.Options.Identifier == identifier)
					{
						identifierExists = true;
						break;
					}
				}
			}

			return (identifierExists);
		}

		public abstract void SetResultsOptions(Thread thread);

		public void WaitForCompletionThreads()
		{
			this.childs.WaitForCompletion();
		}
	}
}
