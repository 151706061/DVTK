// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.Worklist
{
	/// <summary>
	/// Summary description for ImagingServiceRequestInformationEntity.
	/// </summary>
	public class ImagingServiceRequestInformationEntity : BaseInformationEntity
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public ImagingServiceRequestInformationEntity() : base("IMAGING SERVICE REQUEST") {}

		/// <summary>
		/// Set the Default Tag Type List for this Entity.
		/// </summary>
		protected override void SetDefaultTagTypeList()
		{
			TagTypeList.Add(new TagType(Tag.ACCESSION_NUMBER, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.REQUESTING_PHYSICIAN, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.REFERRING_PHYSICIANS_NAME, TagTypeEnum.TagOptional));
			// plus all other attributes from the Imaging Service Request Module
		}
	}
}
