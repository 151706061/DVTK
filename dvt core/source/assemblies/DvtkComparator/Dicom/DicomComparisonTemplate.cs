// Part of DvtkComparator.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.CommonDataFormat;

namespace Dvtk.Comparator
{
	/// <summary>
	/// Summary description for DicomComparisonTemplate.
	/// </summary>
	public class DicomComparisonTemplate
	{
		private DvtkData.Dimse.DimseCommand _command = DvtkData.Dimse.DimseCommand.UNDEFINED;
		private System.String _sopClassUid = System.String.Empty;
		private DicomComparisonTagCollection _comparisonTags = new DicomComparisonTagCollection();

		/// <summary>
		/// Class constructor.
		/// </summary>
		public DicomComparisonTemplate() {}

		/// <summary>
		/// Class constructor.
		/// </summary>
		/// <param name="command">DIMSE Command ID</param>
		/// <param name="sopClassUid">SOP Class UID</param>
		/// <returns>bool - true = template initialized, false template not initialized</returns>
		public bool Initialize(DvtkData.Dimse.DimseCommand command, System.String sopClassUid)
		{
			bool initialized = true;
			_command = command;
			_sopClassUid = sopClassUid;

			// Only certain template available
			// Use command and sopClassUid to determine if we can set one up
			if ((command == DvtkData.Dimse.DimseCommand.CFINDRSP) &&
				(sopClassUid == DvtkData.Dul.AbstractSyntax.Modality_Worklist_Information_Model_FIND.UID))
			{
				// Add the comparison tags - these tags will be used to extract the values out of the
				// Dicom Dataset for comparison with other Datasets containing the same tags
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0008, 0x0050), new CommonIdFormat())); // Accession Number
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0008, 0x0060), new CommonStringFormat())); // Modality
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0008, 0x1110), new CommonUidFormat())); // Referenced Study Sequence
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0010), new CommonNameFormat())); // Patient's Name
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0020), new CommonIdFormat())); // Patient ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0030), new CommonDateFormat())); // Patient's Birth Date
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0040), new CommonStringFormat())); // Patient's Sex
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0020, 0x000D), new CommonUidFormat())); // Study Instance UID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x1002), new CommonIdFormat())); // Requested Procedure ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0100), new Tag(0x0040, 0x0009), new CommonIdFormat())); // Scheduled Procedure Step ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0032, 0x1060), new CommonStringFormat())); // Requested Procedure Description
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0100), new Tag(0x0040, 0x0007), new CommonStringFormat())); // Scheduled Procedure Step Description
			}
			else if (command == DvtkData.Dimse.DimseCommand.CSTORERQ)
			{
				// Add the comparison tags - these tags will be used to extract the values out of the
				// Dicom Dataset for comparison with other Datasets containing the same tags
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0008, 0x0050), new CommonIdFormat())); // Accession Number
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0008, 0x0060), new CommonStringFormat())); // Modality
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0008, 0x1110), new CommonUidFormat())); // Referenced Study Sequence
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0010), new CommonNameFormat())); // Patient's Name
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0020), new CommonIdFormat())); // Patient ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0030), new CommonDateFormat())); // Patient's Birth Date
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0040), new CommonStringFormat())); // Patient's Sex
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0020, 0x000D), new CommonUidFormat())); // Study Instance UID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0275), new Tag(0x0040, 0x1002), new CommonIdFormat())); // Requested Procedure ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0275), new Tag(0x0040, 0x0009), new CommonIdFormat())); // Scheduled Procedure Step ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0032, 0x1060), new CommonStringFormat())); // Requested Procedure Description
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0275), new Tag(0x0040, 0x0007), new CommonStringFormat())); // Scheduled Procedure Step Description
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0253), new CommonIdFormat())); // Performed Procedure Step ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0254), new CommonIdFormat())); // Performed Procedure Step Description
			}
			else if ((command == DvtkData.Dimse.DimseCommand.NCREATERQ) &&
				(sopClassUid == DvtkData.Dul.AbstractSyntax.Modality_Performed_Procedure_Step.UID))
			{
				// Add the comparison tags - these tags will be used to extract the values out of the
				// Dicom Dataset for comparison with other Datasets containing the same tags
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0270), new Tag(0x0008, 0x0050), new CommonIdFormat())); // Accession Number
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0008, 0x0060), new CommonStringFormat())); // Modality
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0270), new Tag(0x0008, 0x1110), new CommonUidFormat())); // Referenced Study Sequence
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0010), new CommonNameFormat())); // Patient's Name
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0020), new CommonIdFormat())); // Patient ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0030), new CommonDateFormat())); // Patient's Birth Date
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0010, 0x0040), new CommonStringFormat())); // Patient's Sex
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0270), new Tag(0x0020, 0x000D), new CommonUidFormat())); // Study Instance UID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0270), new Tag(0x0040, 0x1002), new CommonIdFormat())); // Requested Procedure ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0270), new Tag(0x0040, 0x0009), new CommonIdFormat())); // Scheduled Procedure Step ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0270), new Tag(0x0032, 0x1060), new CommonStringFormat())); // Requested Procedure Description
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0270), new Tag(0x0040, 0x0007), new CommonStringFormat())); // Scheduled Procedure Step Description
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0253), new CommonIdFormat())); // Performed Procedure Step ID
				_comparisonTags.Add(new DicomComparisonTag(new Tag(0x0040, 0x0254), new CommonStringFormat())); // Performed Procedure Step Description
			}
			else
			{
				initialized = false;
			}

			return initialized;
		}

		#region properties
		/// <summary>
		/// Command property.
		/// </summary>
		public DvtkData.Dimse.DimseCommand Command
		{
			set
			{
				_command = value;
			}
			get
			{
				return _command;
			}
		}

		/// <summary>
		/// SopClassUid property.
		/// </summary>
		public System.String SopClassUid
		{
			set
			{
				_sopClassUid = value;
			}
			get
			{
				return _sopClassUid;
			}
		}

		/// <summary>
		/// ComparisonTags property.
		/// </summary>
		public DicomComparisonTagCollection ComparisonTags
		{
			get
			{
				return _comparisonTags;
			}
		}
		#endregion
	}
}
