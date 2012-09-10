// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.Worklist
{
	/// <summary>
	/// Summary description for VisitInformationEntity.
	/// </summary>
	public class VisitInformationEntity : BaseInformationEntity
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public VisitInformationEntity() : base("VISIT") {}

		/// <summary>
		/// Set the Default Tag Type List for this Entity.
		/// </summary>
		protected override void SetDefaultTagTypeList()
		{
			TagTypeList.Add(new TagType(Tag.ADMISSION_ID, TagTypeEnum.TagOptional));
			// plus all other attributes from the Visit Identification Module

			TagTypeList.Add(new TagType(Tag.CURRENT_PATIENT_LOCATION, TagTypeEnum.TagOptional));
			// plus all other attributes from the Visit Status Module

			TagTypeList.Add(new TagType(Tag.REFERENCED_PATIENT_SEQUENCE, TagTypeEnum.TagOptional));
			// plus all other attributes from the Visit Relationship Module

			// plus all other attributes from the Visit Admission Module
		}
	}
}
