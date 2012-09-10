// Part of DvtkComparator.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.CommonDataFormat;

namespace Dvtk.Comparator
{
	/// <summary>
	/// Summary description for DicomComparisonTag.
	/// </summary>
	public class DicomComparisonTag
	{
		private DvtkData.Dimse.Tag _parentSequenceTag = Tag.UNDEFINED;
		private DvtkData.Dimse.Tag _tag = null;
		private BaseCommonDataFormat _commonDataFormat = null;

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="tag">Comparison Tag</param>
		/// <param name="commonDataFormat">Data Format for Tag</param>
		public DicomComparisonTag(DvtkData.Dimse.Tag tag, BaseCommonDataFormat commonDataFormat)
		{
			_tag = tag;
			_commonDataFormat = commonDataFormat;
		}

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="parentSequenceTag">Parent Sequence Tag</param>
		/// <param name="tag">Comparison Tag</param>
		/// <param name="commonDataFormat">Data Format for Tag</param>
		public DicomComparisonTag(DvtkData.Dimse.Tag parentSequenceTag, 
									DvtkData.Dimse.Tag tag, 
									BaseCommonDataFormat commonDataFormat)
		{
			_parentSequenceTag = parentSequenceTag;
			_tag = tag;
			_commonDataFormat = commonDataFormat;
		}

		#region properties
		/// <summary>
		/// ParentSequenceTag property.
		/// </summary>
		public DvtkData.Dimse.Tag ParentSequenceTag
		{
			get
			{
				return _parentSequenceTag;
			}
		}

		/// <summary>
		/// Tag property.
		/// </summary>
		public DvtkData.Dimse.Tag Tag
		{
			get
			{
				return _tag;
			}
		}

		/// <summary>
		/// DataFormat property.
		/// </summary>
		public BaseCommonDataFormat DataFormat
		{
			get
			{
				return _commonDataFormat;
			}
		}
		#endregion
	}
}
