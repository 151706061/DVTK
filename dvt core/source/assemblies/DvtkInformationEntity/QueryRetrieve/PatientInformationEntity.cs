// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.QueryRetrieve
{
	/// <summary>
	/// Summary description for PatientInformationEntity.
	/// </summary>
	public class PatientInformationEntity : BaseInformationEntity
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public PatientInformationEntity() : base("PATIENT") {}

		/// <summary>
		/// Set the Default Tag Type List for this Entity.
		/// </summary>
		protected override void SetDefaultTagTypeList()
		{
			TagTypeList.Add(new TagType(Tag.SPECIFIC_CHARACTER_SET, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_NAME, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.PATIENT_ID, TagTypeEnum.TagUnique));
			TagTypeList.Add(new TagType(Tag.REFERENCED_PATIENT_SEQUENCE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_BIRTH_DATE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_BIRTH_TIME, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_SEX, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.OTHER_PATIENT_IDS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.OTHER_PATIENT_NAMES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.ETHNIC_GROUP, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENT_COMMENTS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_PATIENT_RELATED_STUDIES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_PATIENT_RELATED_SERIES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_PATIENT_RELATED_INSTANCES, TagTypeEnum.TagOptional));

			// Add the Query Retrieve Level Attribute
			DvtkData.Dimse.Attribute attribute = new DvtkData.Dimse.Attribute(0x00080052, VR.CS, "PATIENT");
			DataSet.Add(attribute);
		}
	}
}
