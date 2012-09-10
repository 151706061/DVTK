// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.QueryRetrieve
{
	/// <summary>
	/// Summary description for StudyRootInformationModel.
	/// </summary>
	public class StudyRootInformationModel : BaseInformationModel, ICommitInformationModel, IRetrieveInformationModel
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public StudyRootInformationModel() : base("PatientRootInformationModel") {}

		#region BaseInformationModel Overrides
		/// <summary>
		/// Add the given Dataset to the Information Model. The data is normalised into the Information Model.
		/// </summary>
		/// <param name="dataset">Dataset to add to Informatio Model.</param>
		public override void AddToInformationModel(DataSet dataset)
		{
			// STUDY level
			PatientStudyInformationEntity patientStudyInformationEntity = null;

			// check if the patient/study IE is already in the studyRootList
			foreach (PatientStudyInformationEntity lPatientStudyInformationEntity in Root)
			{
				if (lPatientStudyInformationEntity.IsFoundIn(dataset))
				{
					patientStudyInformationEntity = lPatientStudyInformationEntity;
					break;
				}
			}

			// patient/study IE is not already in the studyRootList
			if (patientStudyInformationEntity == null)
			{
				// create a new patient/study IE from the dataset and add to the studyRootList
				patientStudyInformationEntity = new PatientStudyInformationEntity();
				patientStudyInformationEntity.CopyFrom(dataset);
				Root.Add(patientStudyInformationEntity);
			}

			// SERIES level
			SeriesInformationEntity seriesInformationEntity = null;

			// check if the series IE is already in the patient/study IE children
			foreach (SeriesInformationEntity lSeriesInformationEntity in patientStudyInformationEntity.Children)
			{
				if (lSeriesInformationEntity.IsFoundIn(dataset))
				{
					seriesInformationEntity = lSeriesInformationEntity;
					break;
				}
			}

			// series IE is not already in the patient/study IE children
			if (seriesInformationEntity == null)
			{
				// create a new series IE from the dataset and add to the patient/study IE children
				seriesInformationEntity = new SeriesInformationEntity();
				seriesInformationEntity.CopyFrom(dataset);
				patientStudyInformationEntity.AddChild(seriesInformationEntity);
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

			// query at the STUDY level
			if (queryRetrieveLevel == "STUDY")
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


				foreach (StudyInformationEntity studyInformationEntity in Root)
				{
					if (studyInformationEntity.IsFoundIn(queryTagTypeList, queryDataset))
					{
						// STUDY level matches
						DataSet queryResponse = new DataSet();
						studyInformationEntity.CopyTo(returnTagTypeList, queryResponse);
						queryResponses.Add(queryResponse);
					}
				}
			}
			else
			{
				// find the matching STUDY
				StudyInformationEntity studyInformationEntity = null;
				foreach (StudyInformationEntity lStudyInformationEntity in Root)
				{
					if (lStudyInformationEntity.IsUniqueTagFoundIn(queryDataset))
					{
						studyInformationEntity = lStudyInformationEntity;
						break;
					}
				}

				if (studyInformationEntity != null)
				{
					// query at the SERIES level
					if (queryRetrieveLevel == "SERIES")
					{
						TagTypeList queryTagTypeList = new TagTypeList();
						TagTypeList returnTagTypeList = new TagTypeList();
						foreach (DvtkData.Dimse.Attribute attribute in queryDataset)
						{
							// do not add higher level tags
							if (attribute.Tag == Tag.STUDY_INSTANCE_UID) continue;

							if (attribute.Length != 0)
							{
								queryTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagRequired));
							}
							returnTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagOptional));
						}

						foreach (SeriesInformationEntity seriesInformationEntity in studyInformationEntity.Children)
						{
							if (seriesInformationEntity.IsFoundIn(queryTagTypeList, queryDataset))
							{
								// SERIES level matches
								DataSet queryResponse = new DataSet();
								studyInformationEntity.CopyUniqueTagTo(queryResponse);
								seriesInformationEntity.CopyTo(returnTagTypeList, queryResponse);
								queryResponses.Add(queryResponse);
							}
						}
					}
					else
					{
						// find the matching SERIES
						SeriesInformationEntity seriesInformationEntity = null;
						foreach (SeriesInformationEntity lSeriesInformationEntity in studyInformationEntity.Children)
						{
							if (lSeriesInformationEntity.IsUniqueTagFoundIn(queryDataset))
							{
								seriesInformationEntity = lSeriesInformationEntity;
								break;
							}
						}

						if (seriesInformationEntity != null)
						{
							// query at the IMAGE level
							if (queryRetrieveLevel == "IMAGE")
							{
								TagTypeList queryTagTypeList = new TagTypeList();
								TagTypeList returnTagTypeList = new TagTypeList();
								foreach (DvtkData.Dimse.Attribute attribute in queryDataset)
								{
									// do not add higher level tags
									if ((attribute.Tag == Tag.STUDY_INSTANCE_UID) ||
										(attribute.Tag == Tag.SERIES_INSTANCE_UID)) continue;

									if (attribute.Length != 0)
									{
										queryTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagRequired));
									}
									returnTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagOptional));
								}

								foreach (InstanceInformationEntity instanceInformationEntity in seriesInformationEntity.Children)
								{
									if (instanceInformationEntity.IsFoundIn(queryTagTypeList, queryDataset))
									{
										// IMAGE level matches
										DataSet queryResponse = new DataSet();
										studyInformationEntity.CopyUniqueTagTo(queryResponse);
										seriesInformationEntity.CopyUniqueTagTo(queryResponse);
										instanceInformationEntity.CopyTo(returnTagTypeList, queryResponse);
										queryResponses.Add(queryResponse);
									}
								}
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
			foreach (StudyInformationEntity studyInformationEntity in Root)
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

			// find the matching STUDY
			StudyInformationEntity studyInformationEntity = null;
			foreach (StudyInformationEntity lStudyInformationEntity in Root)
			{
				if (lStudyInformationEntity.IsUniqueTagFoundIn(retrieveDataset))
				{
					studyInformationEntity = lStudyInformationEntity;
					break;
				}
			}

			if (studyInformationEntity != null)
			{
				// retrieve at the STUDY level
				if (queryRetrieveLevel == "STUDY")
				{
					foreach (SeriesInformationEntity seriesInformationEntity in studyInformationEntity.Children)
					{
						foreach (InstanceInformationEntity instanceInformationEntity in seriesInformationEntity.Children)
						{
							fileList.Add(instanceInformationEntity.Filename);
						}
					}
				}
				else
				{
					// find the matching SERIES
					SeriesInformationEntity seriesInformationEntity = null;
					foreach (SeriesInformationEntity lSeriesInformationEntity in studyInformationEntity.Children)
					{
						if (lSeriesInformationEntity.IsUniqueTagFoundIn(retrieveDataset))
						{
							seriesInformationEntity = lSeriesInformationEntity;
							break;
						}
					}
					if (seriesInformationEntity != null)
					{
						// retrieve at the SERIES level
						if (queryRetrieveLevel == "SERIES")
						{
							foreach (InstanceInformationEntity instanceInformationEntity in seriesInformationEntity.Children)
							{
								fileList.Add(instanceInformationEntity.Filename);
							}
						}
						else
						{
							// find the matching IMAGE
							InstanceInformationEntity instanceInformationEntity = null;
							foreach (InstanceInformationEntity lInstanceInformationEntity in seriesInformationEntity.Children)
							{
								if (lInstanceInformationEntity.IsUniqueTagFoundIn(retrieveDataset))
								{
									instanceInformationEntity = lInstanceInformationEntity;
									break;
								}
							}

							// retrieve at the IMAGE level
							if ((instanceInformationEntity != null) &&
								(queryRetrieveLevel == "IMAGE"))
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
