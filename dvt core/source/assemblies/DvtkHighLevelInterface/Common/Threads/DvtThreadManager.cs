using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for DvtThreadManager.
	/// </summary>
	public class DvtThreadManager: ThreadManager
	{
		private String baseName = "";

		public DvtThreadManager(String baseName)
		{
			this.baseName = baseName;
		}

		public override void SetResultsOptions(Thread thread)
		{
			Thread topmostThread = thread.TopmostThread;

			if ((thread is DicomThread) && (topmostThread is DicomThread))
			{
				DicomThread dicomThread = thread as DicomThread;
				DicomThread topmostDicomThread = topmostThread as DicomThread;

				if (topmostDicomThread == dicomThread)
				{
					if (dicomThread.Options.ResultsFileName == null)
					{
						dicomThread.Options.ResultsFileName = String.Format("{0:000}_{1}_res.xml", dicomThread.Options.SessionId, this.baseName.Replace(".", "_"));
					}
				}
				else
				{
					dicomThread.Options.ResultsDirectory = topmostDicomThread.Options.ResultsDirectory;

					if (dicomThread.Options.ResultsFileName == null)
					{
						dicomThread.Options.ResultsFileName = String.Format("{0:000}_{1}_{2}_res.xml", dicomThread.Options.SessionId, this.baseName.Replace(".", "_"), dicomThread.Options.Identifier);
					}
				}
			}
		}
	}
}
