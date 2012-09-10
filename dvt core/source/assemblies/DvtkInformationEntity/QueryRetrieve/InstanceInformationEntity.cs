// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.QueryRetrieve
{
	/// <summary>
	/// Summary description for InstanceInformationEntity.
	/// </summary>
	public class InstanceInformationEntity : BaseInformationEntity
	{
		private System.String _filename;

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="filename">Filename containing this entity and all parent entities (as a composite).</param>
		public InstanceInformationEntity(System.String filename) : base("IMAGE")
		{
			_filename = filename;
		}

		/// <summary>
		/// Get the name of the file containing this entity and all parents as a composite.
		/// </summary>
		public System.String Filename	
		{
			get 
			{
				return _filename;
			}
		}

		/// <summary>
		/// Set the Default Tag Type List for this Entity.
		/// </summary>
		protected override void SetDefaultTagTypeList()
		{
			TagTypeList.Add(new TagType(Tag.SPECIFIC_CHARACTER_SET, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.INSTANCE_NUMBER, TagTypeEnum.TagRequired));
			TagTypeList.Add(new TagType(Tag.OVERLAY_NUMBER, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.CURVE_NUMBER, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.LUT_NUMBER, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.SOP_INSTANCE_UID, TagTypeEnum.TagUnique));
			// plus all other attributes at an instance level!
			TagTypeList.Add(new TagType(Tag.SOP_CLASS_UID, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.ROWS, TagTypeEnum.TagOptional));
			TagTypeList.Add(new TagType(Tag.COLUMNS, TagTypeEnum.TagOptional));

			// Add the Query Retrieve Level Attribute
			DvtkData.Dimse.Attribute attribute = new DvtkData.Dimse.Attribute(0x00080052, VR.CS, "IMAGE");
			DataSet.Add(attribute);
		}
	}
}
