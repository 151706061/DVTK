// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.QueryRetrieve
{
	/// <summary>
	/// Summary description for SeriesInformationEntity.
	/// </summary>
	public class SeriesInformationEntity : BaseInformationEntity
	{
		/// <summary>
		/// Class constructor.
		/// </summary>
		public SeriesInformationEntity() : base("SERIES") {}

		/// <summary>
		/// Set the Default Tag Type List for this Entity.
		/// </summary>
		protected override void SetDefaultTagTypeList()
		{
			TagTypeList.Add(new TagType(Tag.SPECIFIC_CHARACTER_SET, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.MODALITY, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.SERIES_NUMBER, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.SERIES_INSTANCE_UID, TagTypeEnum.TagUnique));
			TagTypeList.Add(new TagType(Tag.NUMBER_OF_SERIES_RELATED_INSTANCES, TagTypeEnum.TagOptional));
			// plus all other attributes at a series level!

			// Add the Query Retrieve Level Attribute
			DvtkData.Dimse.Attribute attribute = new DvtkData.Dimse.Attribute(0x00080052, VR.CS, "SERIES");
			DataSet.Add(attribute);
		}	
	}
}
