using System;
using Dvtk.Comparator;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface.Comparator
{
	/// <summary>
	/// Summary description for Comparator.
	/// </summary>
	public class Comparator
	{
		private System.String _name = System.String.Empty;

		public Comparator(System.String name)
		{
			_name = name;
		}

		public DicomComparator InitializeDicomComparator(DicomMessage dicomMessage)
		{
			DicomComparator dicomComparator = new DicomComparator(_name);
			bool initialized = dicomComparator.Initialize(dicomMessage.DvtkDataDicomMessage);
			if (initialized == false)
			{
				dicomComparator = null;
			}

			return dicomComparator;
		}
	}
}
