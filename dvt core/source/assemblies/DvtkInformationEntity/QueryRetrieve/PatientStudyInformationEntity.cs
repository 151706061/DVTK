// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.QueryRetrieve
{
	/// <summary>
	/// Summary description for PatientStudyInformationEntity.
	/// </summary>
	public class PatientStudyInformationEntity : BaseInformationEntity
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public PatientStudyInformationEntity() : base("STUDY") {}

		/// <summary>
		/// Set the Default Tag Type List for this Entity.
		/// </summary>
		protected override void SetDefaultTagTypeList()
		{
			TagTypeList.Add(new TagType(Tag.SPECIFIC_CHARACTER_SET, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.STUDY_DATE, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.STUDY_TIME, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.ACCESSION_NUMBER, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.PATIENTS_NAME, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.PATIENT_ID, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.STUDY_ID, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.STUDY_INSTANCE_UID, TagTypeEnum.TagUnique));
			TagTypeList.Add(new TagType(Tag.MODALITIES_IN_STUDY, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.REFERRING_PHYSICIANS_NAME, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.STUDY_DESCRIPTION, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PROCEDURE_CODE_SEQUENCE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NAME_OF_PHYSICIANS_READING_STUDY, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.ADMITTING_DIAGNOSIS_DESCRIPTION, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.REFERENCED_STUDY_SEQUENCE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.REFERENCED_PATIENT_SEQUENCE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_BIRTH_DATE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_BIRTH_TIME, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_SEX, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.OTHER_PATIENT_IDS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.OTHER_PATIENT_NAMES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_AGE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_SIZE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_WEIGHT, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.ETHNIC_GROUP, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.OCCUPATION, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.ADDITIONAL_PATIENT_HISTORY, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENT_COMMENTS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.OTHER_STUDY_NUMBERS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_PATIENT_RELATED_STUDIES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_PATIENT_RELATED_SERIES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_PATIENT_RELATED_INSTANCES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_STUDY_RELATED_SERIES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_STUDY_RELATED_INSTANCES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.INTERPRETATION_AUTHOR, TagTypeEnum.TagOptional));

			// Add the Query Retrieve Level Attribute
			DvtkData.Dimse.Attribute attribute = new DvtkData.Dimse.Attribute(0x00080052, VR.CS, "STUDY");
			DataSet.Add(attribute);
		}
	}
}
