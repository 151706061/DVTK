using System;
using System.IO;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for DicomFile.
	/// </summary>
	public class DicomFile
	{
		private DataSet dataSet = null;

		private FileMetaInformation fileMetaInformation = null;

		private DvtkData.Media.FileHead dvtkDataFileHead = null;

		public DicomFile()
		{
			this.dataSet = new DataSet();
			this.fileMetaInformation = new FileMetaInformation();
			this.dvtkDataFileHead = new DvtkData.Media.FileHead();
		}

		public DataSet DataSet
		{
			get
			{
				return(this.dataSet);
			}			
		}

		public String TransferSyntax
		{
			get
			{
				return(this.dvtkDataFileHead.TransferSyntax.UID);
			}
			set
			{
				this.dvtkDataFileHead.TransferSyntax = new DvtkData.Dul.TransferSyntax(value);
			}
		}

		public System.Byte[] FilePreamble
		{
			get
			{
				return(this.dvtkDataFileHead.FilePreamble);
			}
			set
			{
				this.dvtkDataFileHead.FilePreamble = value;
			}
		}

		public FileMetaInformation FileMetaInformation
		{
			get
			{
				return(this.fileMetaInformation);
			}			
		}


		public void Read(String fullFileName, DicomThread dicomThread)
		{
			DvtkData.Media.DicomFile dvtkDataDicomFile = null;

			if (!File.Exists(fullFileName))
			{
				throw new HliException("Dicom file or raw file \"" + fullFileName + "\" not found.");
			}

			dvtkDataDicomFile = dicomThread.DvtkScriptSession.ReadFile(fullFileName);

			this.dataSet.DvtkDataDataSet = dvtkDataDicomFile.DataSet;
			this.fileMetaInformation.DvtkDataFileMetaInformation = dvtkDataDicomFile.FileMetaInformation;
			this.dvtkDataFileHead = dvtkDataDicomFile.FileHead;
		}

		public void Read(String fullFileName, params String[] definitionFilesFullName)
		{
			DvtkData.Media.DicomFile dvtkDataDicomFile = null;

			if (!File.Exists(fullFileName))
			{
				throw new HliException("Dicom file or raw file \"" + fullFileName + "\" not found.");
			}

			String tempPath = Path.GetTempPath();

			Dvtk.Sessions.ScriptSession dvtkScriptSession = new Dvtk.Sessions.ScriptSession();
			
			dvtkScriptSession.StorageMode = Dvtk.Sessions.StorageMode.AsMedia;
			dvtkScriptSession.DataDirectory = tempPath;
			dvtkScriptSession.ResultsRootDirectory = tempPath;

			foreach(String definitionFileFullName in definitionFilesFullName)
			{
				dvtkScriptSession.DefinitionManagement.LoadDefinitionFile(definitionFileFullName);
			}

			dvtkDataDicomFile = dvtkScriptSession.ReadFile(fullFileName);

			this.dataSet.DvtkDataDataSet = dvtkDataDicomFile.DataSet;
			this.FileMetaInformation.DvtkDataFileMetaInformation = dvtkDataDicomFile.FileMetaInformation;
			this.dvtkDataFileHead = dvtkDataDicomFile.FileHead;
		}

		public bool Write(String fullFileName)
		{
			Dvtk.Sessions.ScriptSession dvtkScriptSession = new Dvtk.Sessions.ScriptSession();

			DvtkData.Media.DicomFile dvtkDataDicomFile = new DvtkData.Media.DicomFile();

			dvtkDataDicomFile.DataSet = this.dataSet.DvtkDataDataSet;
			dvtkDataDicomFile.FileMetaInformation = this.fileMetaInformation.DvtkDataFileMetaInformation;
			dvtkDataDicomFile.FileHead = this.dvtkDataFileHead;

			return dvtkScriptSession.WriteFile(dvtkDataDicomFile, fullFileName);
		}

		public override String ToString()
		{
			String returnValue = String.Empty;

			returnValue+= "Dump of DicomFile\n";
			returnValue+= "Transfer syntax: " + this.dvtkDataFileHead.TransferSyntax.UID + ".\n";
			returnValue+= "FileMetaInformation contains " + this.fileMetaInformation.Count.ToString() + " attributes.\n";
			returnValue+= "DataSet contains " + this.dataSet.Count.ToString() + " attributes.\n";

			return(returnValue);
		}
	}
}
