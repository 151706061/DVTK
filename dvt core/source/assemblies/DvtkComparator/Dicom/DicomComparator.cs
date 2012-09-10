// Part of DvtkComparator.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using DvtkData.ComparisonResults;
using Dvtk.CommonDataFormat;
using Dvtk.Results;

namespace Dvtk.Comparator
{
	/// <summary>
	/// Summary description for DicomComparator.
	/// </summary>
	public class DicomComparator : BaseComparator
	{
		private System.String _name = System.String.Empty;
		private DicomComparisonTemplate _template = null;

		/// <summary>
		/// Class constructor.
		/// </summary>
		public DicomComparator(System.String name)
		{
			_name = name;
		}

		/// <summary>
		/// Initialize the DicomComparator
		/// </summary>
		/// <param name="dicomMessage"></param>
		/// <returns></returns>
		/// <returns>bool - true = template initialized, false template not initialized</returns>
		public bool Initialize(DvtkData.Dimse.DicomMessage dicomMessage)
		{
			DvtkData.Dimse.DimseCommand command = dicomMessage.CommandField;
			System.String sopClassUid = System.String.Empty;

			DvtkData.Dimse.Attribute attribute = dicomMessage.CommandSet.GetAttribute(DvtkData.Dimse.Tag.AFFECTED_SOP_CLASS_UID);
			if (attribute == null)
			{
				attribute = dicomMessage.CommandSet.GetAttribute(DvtkData.Dimse.Tag.REQUESTED_SOP_CLASS_UID);
			}
			if ((attribute != null) &&
				(attribute.Length != 0))
			{
				UniqueIdentifier uniqueIdentifier = (UniqueIdentifier)attribute.DicomValue;
				sopClassUid = uniqueIdentifier.Values[0];
			}

			// Try to initialise a template
			_template = new DicomComparisonTemplate();
			bool initialized = _template.Initialize(command, sopClassUid);
			if (initialized == true)
			{
				// Load the template with the corresponding attribute values
				initialized = LoadTemplate(dicomMessage.DataSet);
			}

			return initialized;
		}

		private bool LoadTemplate(DvtkData.Dimse.DataSet dataset)
		{
			if (dataset == null) return false;

			// try to find the template tag in the dataset
			foreach (DicomComparisonTag comparisonTag in this._template.ComparisonTags)
			{
				DvtkData.Dimse.Tag tag = comparisonTag.Tag;
				DvtkData.Dimse.Tag parentSequenceTag = comparisonTag.ParentSequenceTag;
				System.String attributeValue = System.String.Empty;

				if (parentSequenceTag != Tag.UNDEFINED)
				{
					DvtkData.Dimse.Attribute sequenceAttribute = dataset.GetAttribute(parentSequenceTag);
					if ((sequenceAttribute != null) &&
						(sequenceAttribute.ValueRepresentation == DvtkData.Dimse.VR.SQ))
					{
						SequenceOfItems sequenceOfItems = (SequenceOfItems)sequenceAttribute.DicomValue;
						if (sequenceOfItems.Sequence.Count == 1)
						{
							SequenceItem item = sequenceOfItems.Sequence[0];

							if (item != null)
							{
								DvtkData.Dimse.Attribute attribute = item.GetAttribute(tag);
								attributeValue = GetAttributeValue(attribute);
							}
						}
					}
				}
				else
				{
					DvtkData.Dimse.Attribute attribute = dataset.GetAttribute(tag);
					attributeValue = GetAttributeValue(attribute);
				}

				if (attributeValue != System.String.Empty)
				{
					comparisonTag.DataFormat.FromDicomFormat(attributeValue);
				}
			}

			return true;
		}

		private System.String GetAttributeValue(DvtkData.Dimse.Attribute attribute)
		{
			System.String attributeValue = System.String.Empty;
			if ((attribute == null) ||
				(attribute.Length == 0))
			{
				return attributeValue;
			}

			switch(attribute.ValueRepresentation)
			{
				case VR.AE:
				{
					ApplicationEntity applicationEntity = (ApplicationEntity)attribute.DicomValue;
					attributeValue = applicationEntity.Values[0];
					break;
				}
				case VR.AS:
				{
					AgeString ageString = (AgeString)attribute.DicomValue;
					attributeValue = ageString.Values[0];
					break;
				}
				case VR.CS:
				{
					CodeString codeString = (CodeString)attribute.DicomValue;
					attributeValue = codeString.Values[0];
					break;
				}
				case VR.DA:
				{
					Date date = (Date)attribute.DicomValue;
					attributeValue = date.Values[0];
					break;
				}
				case VR.DS:
				{
					DecimalString decimalString = (DecimalString)attribute.DicomValue;
					attributeValue = decimalString.Values[0];
					break;
				}
				case VR.DT:
				{
					DvtkData.Dimse.DateTime dateTime = (DvtkData.Dimse.DateTime)attribute.DicomValue;
					attributeValue = dateTime.Values[0];
					break;
				}
				case VR.IS:
				{
					IntegerString integerString = (IntegerString)attribute.DicomValue;
					attributeValue = integerString.Values[0];
					break;
				}
				case VR.LO:
				{
					LongString longString = (LongString)attribute.DicomValue;
					attributeValue = longString.Values[0];
					break;
				}
				case VR.LT:
				{
					LongText longText = (LongText)attribute.DicomValue;
					attributeValue = longText.Value;
					break;
				}
				case VR.PN:
				{
					PersonName personName = (PersonName)attribute.DicomValue;
					attributeValue = personName.Values[0];
					break;
				}
				case VR.SH:
				{
					ShortString shortString = (ShortString)attribute.DicomValue;
					attributeValue = shortString.Values[0];
					break;
				}
				case VR.SQ:
				{
					// Special case looking for the SOP Class UID
					SequenceOfItems sequenceOfItems = (SequenceOfItems)attribute.DicomValue;
					if ((sequenceOfItems != null) &&
						(sequenceOfItems.Sequence.Count == 1))
					{
						// Special case looking for the SOP Class UID
						SequenceItem item = sequenceOfItems.Sequence[0];
						attribute = item.GetAttribute(new Tag(0x0008, 0x1150));
						attributeValue = GetAttributeValue(attribute);
					}
					break;
				}
				case VR.ST:
				{
					ShortText shortText = (ShortText)attribute.DicomValue;
					attributeValue = shortText.Value;
					break;
				}
				case VR.TM:
				{
					Time time = (Time)attribute.DicomValue;
					attributeValue = time.Values[0];
					break;
				}
				case VR.UI:
				{
					UniqueIdentifier uniqueIdentifier = (UniqueIdentifier)attribute.DicomValue;
					attributeValue = uniqueIdentifier.Values[0];
					break;
				}
				default:
					break;
			}

			return attributeValue;
		}

		/// <summary>
		/// Compare the two messages.
		/// </summary>
		/// <param name="resultsReporter">Results reporter.</param>
		/// <param name="thatBaseComparator">Reference comparator.</param>
		/// <returns>bool - true = messages compared, false messages not compared</returns>
		public override bool Compare(ResultsReporter resultsReporter, BaseComparator thatBaseComparator)
		{
			bool compared = false;

			if (thatBaseComparator is DicomComparator)
			{
				DicomComparator thatDicomComparator = (DicomComparator)thatBaseComparator;

				// Check if both templates have been initialized correctly
				if ((this._template == null) ||
					(thatDicomComparator._template == null))
				{
					return false;
				}

				// Check for comparator equality
				if (this == thatDicomComparator)
				{
					return true;
				}

				MessageComparisonResults messageComparisonResults 
					= new MessageComparisonResults(this._name, 
												thatDicomComparator._name, 
												this._template.Command,
												thatDicomComparator._template.Command, 
												this._template.SopClassUid,
												thatDicomComparator._template.SopClassUid);

				// Iterate over this comparator
				foreach (DicomComparisonTag thisComparisonTag in this._template.ComparisonTags)
				{
					// try to get the equivalent tag in thatDicomComparator
					DicomComparisonTag thatComparisonTag = thatDicomComparator._template.ComparisonTags.Find(thisComparisonTag.Tag);
					if (thatComparisonTag != null)
					{
						AttributeComparisonResults attributeComparisonResults 
							= new AttributeComparisonResults(thisComparisonTag.Tag, 
															thisComparisonTag.DataFormat.ToDicomFormat(), 
															thatComparisonTag.DataFormat.ToDicomFormat());

						if (thisComparisonTag.DataFormat.Equals(thatComparisonTag.DataFormat) == false)
						{
							DvtkData.Validation.ValidationMessage validationMessage = new DvtkData.Validation.ValidationMessage();
							validationMessage.Type = DvtkData.Validation.MessageType.Error;
							validationMessage.Message = "Attribute values do not match.";

							attributeComparisonResults.Messages.Add(validationMessage);
						}
						messageComparisonResults.Add(attributeComparisonResults);
					}
				}

				resultsReporter.WriteMessageComparisonResults(messageComparisonResults);

				compared = true;
			}

			return compared;
		}
	}
}
