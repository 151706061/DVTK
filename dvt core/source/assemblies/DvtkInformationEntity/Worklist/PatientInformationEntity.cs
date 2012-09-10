// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.Worklist
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
			// plus all other attributes from the Patient Relationship Module

			TagTypeList.Add(new TagType(Tag.PATIENTS_NAME, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.PATIENT_ID, TagTypeEnum.TagRequired));
			// plus all other attributes from the Patient Identification Module

			TagTypeList.Add(new TagType(Tag.PATIENTS_BIRTH_DATE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_SEX, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PATIENTS_WEIGHT, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.CONFIDENTIALITY_CONSTRAINT_ON_PATIENT_DATA_DESCRIP, TagTypeEnum.TagOptional));
			// plus all other attributes from the Patient Demographic Module

			TagTypeList.Add(new TagType(Tag.PATIENT_STATE, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.PREGNANCY_STATUS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.MEDICAL_ALERTS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.CONTRAST_ALLERGIES, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.SPECIAL_NEEDS, TagTypeEnum.TagOptional));
			// plus all other attributes from the Patient Medical Module
		}
	}
}
