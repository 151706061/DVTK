// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.QueryRetrieve
{
	/// <summary>
	/// Summary description for PatientStudyOnlyInformationModel.
	/// </summary>
	public class PatientStudyOnlyInformationModel : BaseInformationModel, ICommitInformationModel, IRetrieveInformationModel
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public PatientStudyOnlyInformationModel() : base("PatientStudyOnlyInformationModel") {}

		#region BaseInformationModel Overrides
		/// <summary>
		/// Add the given Dataset to the Information Model. The data is normalised into the Information Model.
		/// </summary>
		/// <param name="dataset">Dataset to add to Informatio Model.</param>
		public override void AddToInformationModel(DataSet dataset)
		{
			// PATIENT level
			PatientInformationEntity patientInformationEntity = null;

			// check if the patient IE is already in the patientRootList
			foreach (PatientInformationEntity lPatientInformationEntity in Root)
			{
				if (lPatientInformationEntity.IsFoundIn(dataset))
				{
					patientInformationEntity = lPatientInformationEntity;
					break;
				}
			}

			// patient IE is not already in the patientRootList
			if (patientInformationEntity == null)
			{
				// create a new patient IE from the dataset and add to the patientRootList
				patientInformationEntity = new PatientInformationEntity();
				patientInformationEntity.CopyFrom(dataset);
				Root.Add(patientInformationEntity);
			}

			// STUDY level
			StudyInformationEntity studyInformationEntity = null;

			// check if the study IE is already in the patient IE children
			foreach (StudyInformationEntity lStudyInformationEntity in patientInformationEntity.Children)
			{
				if (lStudyInformationEntity.IsFoundIn(dataset))
				{
					studyInformationEntity = lStudyInformationEntity;
					break;
				}
			}

			// study IE is not already in the patient IE children
			if (studyInformationEntity == null)
			{
				// create a new study IE from the dataset and add to the patient IE children
				studyInformationEntity = new StudyInformationEntity();
				studyInformationEntity.CopyFrom(dataset);
				patientInformationEntity.AddChild(studyInformationEntity);
			}

			// SERIES level
			SeriesInformationEntity seriesInformationEntity = null;

			// check if the series IE is already in the study IE children
			foreach (SeriesInformationEntity lSeriesInformationEntity in studyInformationEntity.Children)
			{
				if (lSeriesInformationEntity.IsFoundIn(dataset))
				{
					seriesInformationEntity = lSeriesInformationEntity;
					break;
				}
			}

			// series IE is not already in the study IE children
			if (seriesInformationEntity == null)
			{
				// create a new series IE from the dataset and add to the study IE children
				seriesInformationEntity = new SeriesInformationEntity();
				seriesInformationEntity.CopyFrom(dataset);
				studyInformationEntity.AddChild(seriesInformationEntity);
			}

			// IMAGE (Instance) level
			InstanceInformationEntity instanceInformationEntity = null;

			// check if the instance IE is already in the series IE children
			foreach (InstanceInformationEntity lInstanceInformationEntity in seriesInformationEntity.Children)
			{
				if (lInstanceInformationEntity.IsFoundIn(dataset))
				{
					instanceInformationEntity = lInstanceInformationEntity;
					break;
				}
			}

			// instance IE is not already in the series IE children
			if (instanceInformationEntity == null)
			{
				// create a new instance IE from the dataset and add to the series IE children
				instanceInformationEntity = new InstanceInformationEntity(dataset.Filename);
				instanceInformationEntity.CopyFrom(dataset);
				seriesInformationEntity.AddChild(instanceInformationEntity);
			}
		}

		/// <summary>
		/// Query the Information Model using the given Query Dataset.
		/// </summary>
		/// <param name="queryDataset">Query Dataset.</param>
		/// <returns>A collection of zero or more query reponse datasets.</returns>
		public override DataSetCollection QueryInformationModel(DataSet queryDataset)
		{
			DataSetCollection queryResponses = null;

			// get the query/retrieve level
			String queryRetrieveLevel = "UNKNOWN";
			DvtkData.Dimse.Attribute queryRetrieveLevelAttribute = queryDataset.GetAttribute(Tag.QUERY_RETRIEVE_LEVEL);
			if (queryRetrieveLevelAttribute != null)
			{
				CodeString codeString = (CodeString)queryRetrieveLevelAttribute.DicomValue;
				if (codeString.Values.Count == 1)
				{
					queryRetrieveLevel = codeString.Values[0].Trim();
				}
			}

			// query at the PATIENT level
			if (queryRetrieveLevel == "PATIENT")
			{
				TagTypeList queryTagTypeList = new TagTypeList();
				TagTypeList returnTagTypeList = new TagTypeList();
				foreach (DvtkData.Dimse.Attribute attribute in queryDataset)
				{
					if (attribute.Length != 0)
					{
						queryTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagRequired));
					}
					returnTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagOptional));
				}

				foreach (PatientInformationEntity patientInformationEntity in Root)
				{
					if (patientInformationEntity.IsFoundIn(queryTagTypeList, queryDataset))
					{
						// PATIENT level matches
						DataSet queryResponse = new DataSet();
						patientInformationEntity.CopyTo(returnTagTypeList, queryResponse);
						queryResponses.Add(queryResponse);
					}
				}
			}
			else
			{
				// find the matching PATIENT
				PatientInformationEntity patientInformationEntity = null;
				foreach (PatientInformationEntity lPatientInformationEntity in Root)
				{
					if (lPatientInformationEntity.IsUniqueTagFoundIn(queryDataset))
					{
						patientInformationEntity = lPatientInformationEntity;
						break;
					}
				}
				if (patientInformationEntity != null)
				{
					// query at the STUDY level
					if (queryRetrieveLevel == "STUDY")
					{
						TagTypeList queryTagTypeList = new TagTypeList();
						TagTypeList returnTagTypeList = new TagTypeList();
						foreach (DvtkData.Dimse.Attribute attribute in queryDataset)
						{
							// do not add higher level tag
							if (attribute.Tag == Tag.PATIENT_ID) continue;

							if (attribute.Length != 0)
							{
								queryTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagRequired));
							}
							returnTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagOptional));
						}

						foreach (StudyInformationEntity studyInformationEntity in patientInformationEntity.Children)
						{
							if (studyInformationEntity.IsFoundIn(queryTagTypeList, queryDataset))
							{
								// STUDY level matches
								DataSet queryResponse = new DataSet();
								patientInformationEntity.CopyUniqueTagTo(queryResponse);
								studyInformationEntity.CopyTo(returnTagTypeList, queryResponse);
								queryResponses.Add(queryResponse);
							}
						}
					}
				}
			}

			return queryResponses;
		}
		#endregion

		#region ICommitInformationModel
		/// <summary>
		/// Check if the given instance is present in the Information Model. The instance will be at the leaf nodes of the Information Model.
		/// </summary>
		/// <param name="sopClassUid">SOP Class UID to search for.</param>
		/// <param name="sopInstanceUid">SOP Instance UID to search for.</param>
		/// <returns>Boolean - true if instance found in the Information Model, otherwise false.</returns>
		public bool IsInstanceInInformationModel(System.String sopClassUid, System.String sopInstanceUid)
		{
			bool isInstanceInInformationModel = false;

			// set up the commit tag list for comparing with the leaf
			TagTypeList commitTagTypeList = new TagTypeList();
			commitTagTypeList.Add(new TagType(Tag.SOP_INSTANCE_UID, TagTypeEnum.TagRequired));
			commitTagTypeList.Add(new TagType(Tag.SOP_CLASS_UID, TagTypeEnum.TagRequired));

			// set up the commit dataset
			DvtkData.Dimse.DataSet commitDataset = new DvtkData.Dimse.DataSet();
			DvtkData.Dimse.Attribute attribute = new DvtkData.Dimse.Attribute(0x00080016, DvtkData.Dimse.VR.UI, sopClassUid);
			commitDataset.Add(attribute);
			attribute = new DvtkData.Dimse.Attribute(0x00080018, DvtkData.Dimse.VR.UI, sopInstanceUid);
			commitDataset.Add(attribute);

			// iterate over the whole information model - we are interested in the leaf nodes
			foreach (PatientInformationEntity patientInformationEntity in Root)
			{
				foreach (StudyInformationEntity studyInformationEntity in patientInformationEntity.Children)
				{
					foreach (SeriesInformationEntity seriesInformationEntity in studyInformationEntity.Children)
					{
						foreach (InstanceInformationEntity instanceInformationEntity in seriesInformationEntity.Children)
						{
							if (instanceInformationEntity.IsFoundIn(commitTagTypeList, commitDataset))
							{
								// an instance has been found with the matching commit uids
								isInstanceInInformationModel = true;
								break;
							}
						}
					}
				}
			}

			return isInstanceInInformationModel;
		}
		#endregion

		#region IRetrieveInformationModel
		/// <summary>
		/// Retrieve a list of filenames from the Information Model. The filenames match the
		/// individual instances matching the retrieve dataset attributes.
		/// </summary>
		/// <param name="retrieveDataset">Retrive dataset.</param>
		/// <returns>File list - containing the filenames of all instances matching the retrieve dataset attributes.</returns>
		public DvtkData.Collections.StringCollection RetrieveInformationModel(DataSet retrieveDataset)
		{
			DvtkData.Collections.StringCollection fileList = new DvtkData.Collections.StringCollection();

			// get the query/retrieve level
			String queryRetrieveLevel = "UNKNOWN";
			DvtkData.Dimse.Attribute queryRetrieveLevelAttribute = retrieveDataset.GetAttribute(Tag.QUERY_RETRIEVE_LEVEL);
			if (queryRetrieveLevelAttribute != null)
			{
				CodeString codeString = (CodeString)queryRetrieveLevelAttribute.DicomValue;
				if (codeString.Values.Count == 1)
				{
					queryRetrieveLevel = codeString.Values[0].Trim();
				}
			}

			// find the matching PATIENT
			PatientInformationEntity patientInformationEntity = null;
			foreach (PatientInformationEntity lPatientInformationEntity in Root)
			{
				if (lPatientInformationEntity.IsUniqueTagFoundIn(retrieveDataset))
				{
					patientInformationEntity = lPatientInformationEntity;
					break;
				}
			}

			if (patientInformationEntity != null)
			{
				// retrieve at the PATIENT level
				if (queryRetrieveLevel == "PATIENT")
				{
					foreach (StudyInformationEntity studyInformationEntity in patientInformationEntity.Children)
					{
						foreach (SeriesInformationEntity seriesInformationEntity in studyInformationEntity.Children)
						{
							foreach (InstanceInformationEntity instanceInformationEntity in seriesInformationEntity.Children)
							{
								fileList.Add(instanceInformationEntity.Filename);
							}
						}
					}
				}
				else
				{
					// find the matching STUDY
					StudyInformationEntity studyInformationEntity = null;
					foreach (StudyInformationEntity lStudyInformationEntity in patientInformationEntity.Children)
					{
						if (lStudyInformationEntity.IsUniqueTagFoundIn(retrieveDataset))
						{
							studyInformationEntity = lStudyInformationEntity;
							break;
						}
					}

					// retrieve at the STUDY level
					if ((studyInformationEntity != null) &&
						(queryRetrieveLevel == "STUDY"))
					{
						foreach (SeriesInformationEntity seriesInformationEntity in studyInformationEntity.Children)
						{
							foreach (InstanceInformationEntity instanceInformationEntity in seriesInformationEntity.Children)
							{
								fileList.Add(instanceInformationEntity.Filename);
							}
						}
					}
				}
			}

			return fileList;
		}
		#endregion
	}
}
