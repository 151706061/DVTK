using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for FileMetaInformation.
	/// </summary>
	public class FileMetaInformation: AttributeSet
	{

		public FileMetaInformation(): base(new DvtkData.Dimse.DataSet())
		{
		}
		
		internal FileMetaInformation(DvtkData.Dimse.AttributeSet dvtkDataAttributeSet): base(dvtkDataAttributeSet)
		{
			if (dvtkDataAttributeSet == null)
			{
				DvtkHighLevelInterfaceException.Throw("Parameter may not be null/Nothing.");
			}		
		}


		internal DvtkData.Media.FileMetaInformation DvtkDataFileMetaInformation
		{
			set
			{
				this.dvtkDataAttributeSet = value;
			}
			get
			{
				return this.dvtkDataAttributeSet as DvtkData.Media.FileMetaInformation;
			}
		}


	}
}
