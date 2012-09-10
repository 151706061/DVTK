// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;

namespace Dvtk.Dicom.InformationEntity
{
	public enum TagTypeEnum 
	{
		TagRequired,
		TagUnique,
		TagOptional
	}

	/// <summary>
	/// Summary description for TagType.
	/// </summary>
	public class TagType
	{
		private Tag _tag;
		private TagTypeEnum _type;

		/// <summary>
		/// Class Constructor.
		/// </summary>
		/// <param name="group">Tag - group number.</param>
		/// <param name="element">Tag - element number.</param>
		public TagType(System.UInt16 group, System.UInt16 element)
		{
			_tag = new Tag(group, element);
			_type = TagTypeEnum.TagOptional;
		}

		/// <summary>
		/// Class Constructor.		
		/// </summary>
		/// <param name="group">Tag - group number.</param>
		/// <param name="element">Tag - element number.</param>
		/// <param name="type">Tag Type.</param>
		public TagType(System.UInt16 group, System.UInt16 element, TagTypeEnum type)
		{
			_tag = new Tag(group, element);
			_type = type;
		}

		/// <summary>
		/// Class Constructor.
		/// </summary>
		/// <param name="tag">Tag (group/element combination).</param>
		public TagType(Tag tag)
		{
			_tag = new Tag();
			_tag.GroupNumber = tag.GroupNumber;
			_tag.ElementNumber = tag.ElementNumber;
			_type = TagTypeEnum.TagOptional;
		}

		/// <summary>
		/// Class Constructor.
		/// </summary>
		/// <param name="tag">Tag (group/element combination).</param>
		/// <param name="type">Tag Type.</param>
		public TagType(Tag tag, TagTypeEnum type)
		{
			_tag = new Tag();
			_tag.GroupNumber = tag.GroupNumber;
			_tag.ElementNumber = tag.ElementNumber;
			_type = type;
		}

		/// <summary>
		/// Get the Tag.
		/// </summary>
		public Tag Tag
		{
			get
			{
				return _tag;
			}
		}

		/// <summary>
		/// Get the Tag Type.
		/// </summary>
		public TagTypeEnum Type
		{
			get
			{
				return _type;
			}
		}
	}
}
