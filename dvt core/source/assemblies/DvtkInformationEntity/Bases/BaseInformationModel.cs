// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;
using DvtkData.Dimse;

namespace Dvtk.Dicom.InformationEntity
{
	/// <summary>
	/// Summary description for BaseInformationModel.
	/// </summary>
	public abstract class BaseInformationModel
	{
		private System.String _name;
		private BaseInformationEntityList _root = null;
		private System.String _dataDirectory;
		private DataSet _defaultDataset = null;
		private DataSet _additionalDataset = null;

		public BaseInformationModel(System.String name)
		{
			_name = name;
			_root = new BaseInformationEntityList();
			_dataDirectory = ".";
			_defaultDataset = new DataSet("Default Dataset");
			_additionalDataset = new DataSet("Additional Dataset");
		}

		protected BaseInformationEntityList Root
		{
			get
			{
				return _root;
			}
		}

		/// <summary>
		/// Load the Information Model by reading all the .DCM and .RAW files
		/// present in the given directory. The data read is normalised into the
		/// Information Model.
		/// </summary>
		/// <param name="dataDirectory">Source data directory containing the .DCm and .RAW files.</param>
		public void LoadInformationModel(System.String dataDirectory)
		{
			_dataDirectory = dataDirectory;
			DirectoryInfo directoryInfo = new DirectoryInfo(_dataDirectory);
			foreach(FileInfo fileInfo in directoryInfo.GetFiles())
			{
				if ((fileInfo.Extension.ToLower().Equals(".dcm")) ||
					(fileInfo.Extension.ToLower().Equals(".raw")))
				{
					DataSet dataset = Dvtk.DvtkDataHelper.ReadDataSetFromFile(fileInfo.FullName);
					AddToInformationModel(dataset);
				}
			}
		}

		public void RefreshInformationModel()
		{
			// re-load the information model from the data directory and
			// any default/additional attributes
			_root = new BaseInformationEntityList();
			LoadInformationModel(_dataDirectory);

			// apply any default values
			// iterate of all Information Entities
			foreach (BaseInformationEntity baseInformationEntity in Root)
			{
				baseInformationEntity.AddDefaultAttributes(_defaultDataset);
			}

			// apply any additional values
			foreach (BaseInformationEntity baseInformationEntity in Root)
			{
				baseInformationEntity.AddAdditionalAttributes(_additionalDataset);
			}
		}

		/// <summary>
		/// Add the given Dataset to the Information Model. The data is normalised into the Information Model.
		/// </summary>
		/// <param name="dataset">Dataset to add to Informatio Model.</param>
		public abstract void AddToInformationModel(DataSet dataset);

		/// <summary>
		/// Query the Information Model using the given Query Dataset.
		/// </summary>
		/// <param name="queryDataset">Query Dataset.</param>
		/// <returns>A collection of zero or more query reponse datasets.</returns>
		public abstract DataSetCollection QueryInformationModel(DataSet queryDataset);

		/// <summary>
		/// Copy the default dataset attributes to the Information Entities in the Information
		/// Model that define them. Do not overrule any attribute with the same tag as the default
		/// attribute that may already be in the Information Entity.
		/// </summary>
		/// <param name="defaultDataset">Dataset containing all the default values.</param>
		public void AddDefaultAttributesToInformationModel(DataSet defaultDataset)
		{
			if (defaultDataset == null) return;

			// iterate of all Information Entities
			foreach (BaseInformationEntity baseInformationEntity in Root)
			{
				baseInformationEntity.AddDefaultAttributes(defaultDataset);
			}

			// save these default attributes - in case of refresh
			foreach (DvtkData.Dimse.Attribute defaultAttribute in defaultDataset)
			{
				DvtkData.Dimse.Attribute lDefaultAttribute = _defaultDataset.GetAttribute(defaultAttribute.Tag);
				if (lDefaultAttribute == null)
				{
					_defaultDataset.Add(defaultAttribute);
				}
			}
		}

		/// <summary>
		/// Add the attributes in this additional dataset to all Information Entities in the Information
		/// Model. Do not overrule any attribute with the same tag as the additional attribute that may
		/// already be in the Information Entity.
		/// </summary>
		/// <param name="additionalDataset">Dataset containing the additional values.</param>
		public void AddAdditionalAttributesToInformationModel(DataSet additionalDataset)
		{
			if (additionalDataset == null) return;

			// iterate of all Information Entities
			foreach (BaseInformationEntity baseInformationEntity in Root)
			{
				baseInformationEntity.AddAdditionalAttributes(additionalDataset);
			}

			// save these additional attributes - in case of refresh
			foreach (DvtkData.Dimse.Attribute additionalAttribute in additionalDataset)
			{
				DvtkData.Dimse.Attribute lAdditionalAttribute = _additionalDataset.GetAttribute(additionalAttribute.Tag);
				if (lAdditionalAttribute == null)
				{
					_additionalDataset.Add(additionalAttribute);
				}
			}
		}

		/// <summary>
		/// Display the Information Model to the Console - for debugging purposes.
		/// </summary>
		public void ConsoleDisplay()
		{
			Console.WriteLine("{0} - {1} entries", _name, Root.Count);

			int entry = 1;
			foreach (BaseInformationEntity baseInformationEntity in Root)
			{
				Console.WriteLine("Entry: {0}", entry++);
				baseInformationEntity.ConsoleDisplay();
			}
		}
	}
}
