using System;
using System.IO;


namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Represents a data set.
	/// </summary>
	public class DataSet: AttributeSet
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		public DataSet(): base(new DvtkData.Dimse.DataSet())
		{
		}

		internal DataSet(DvtkData.Dimse.AttributeSet dvtkDataAttributeSet): base(dvtkDataAttributeSet)
		{
			if (dvtkDataAttributeSet == null)
			{
				DvtkHighLevelInterfaceException.Throw("Parameter may not be null/Nothing.");
			}
		}

		/// <summary>
		/// Read a Dicom file or a raw file (containing only a data set).
		/// Previously present attributes will not be available after this method call.
		/// 
		/// The dicomThread.Option.StorageMode will determine if pixel data will
		/// be accessable after reading. The dicomThread.Option.DataDirectory will
		/// determine where it will be stored, if configured so and if available.
		/// 
		/// When definition files are loaded by the supplied DicomThread, the
		/// content of these definition files are used to change the VR UN of 
		/// attributes to the VR's found in the definition files.		
		/// </summary>
		/// <param name="fullFileName">The full file name of the Dicom file or raw file.</param>
		/// <param name="dicomThread">The DicomThread.</param>
		public void Read(String fullFileName, DicomThread dicomThread)
		{
			DicomFile dicomFile = new DicomFile();

			dicomFile.Read(fullFileName, dicomThread);

			this.dvtkDataAttributeSet = dicomFile.DataSet.DvtkDataDataSet;
		}

		/// <summary>
		/// Read a Dicom file or a raw file (containing only a data set).
		/// Previously present attributes will not be available after this method call.
		/// 
		/// If dataDirectory is a String, the pixel data (if existing) will be written
		/// to this directory. If dataDirectory is null, no pixel data will be stored.
		/// 
		/// When one or more definition files are supplied, the
		/// content of these definition files are used to change the VR UN of 
		/// attributes to the VR's found in the definition files.		
		/// </summary>
		/// <param name="fullFileName">The full file name of the Dicom file or raw file.</param>
		/// <param name="definitionFilesFullName">A list of zero or more definition files.</param>
		public void Read(String fullFileName, params String[] definitionFilesFullName)
		{
			DicomFile dicomFile = new DicomFile();

			dicomFile.Read(fullFileName, definitionFilesFullName);

			this.dvtkDataAttributeSet = dicomFile.DataSet.DvtkDataDataSet;
		}

		public bool Write(String fullFileName)
		{
			Dvtk.Sessions.ScriptSession dvtkScriptSession = new Dvtk.Sessions.ScriptSession();

			DvtkData.Media.DicomFile dvtkDataDicomFile = new DvtkData.Media.DicomFile();

			dvtkDataDicomFile.FileHead = null;
			dvtkDataDicomFile.FileMetaInformation = null;
			dvtkDataDicomFile.DataSet = this.DvtkDataDataSet;

			return(dvtkScriptSession.WriteFile(dvtkDataDicomFile, fullFileName));
		}



		/*
		/// <summary>
		/// Read the content of the specified data set file.
		/// Previously present attributes will not be available after this method call.
		/// </summary>
		/// <param name="dataSetFullFileName"></param>
		public void Read(String dataSetFullFileName)
		{
			if (!File.Exists(dataSetFullFileName))
			{
				DvtkHighLevelInterfaceException.Throw("Data set file not found");
			}

			this.dvtkDataAttributeSet = Dvtk.DvtkDataHelper.ReadDataSetFromFile(dataSetFullFileName);
		}
		*/



		
		internal DvtkData.Dimse.DataSet DvtkDataDataSet
		{
			set
			{
				this.dvtkDataAttributeSet = value;
			}
			get
			{
				return this.dvtkDataAttributeSet as DvtkData.Dimse.DataSet;
			}
		}


		// !!!!!!!!!!!! Tijdelijk
		internal String IodId
		{
			set
			{
				this.DvtkDataDataSet.IodId = value;
			}
		}

	}
}
