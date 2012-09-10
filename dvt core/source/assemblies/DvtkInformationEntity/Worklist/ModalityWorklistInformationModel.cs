// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkData.Dimse;
using Dvtk.Dicom.InformationEntity;

namespace Dvtk.Dicom.InformationEntity.Worklist
{
	/// <summary>
	/// Summary description for ModalityWorklistInformationModel.
	/// </summary>
	public class ModalityWorklistInformationModel : BaseInformationModel
	{
		/// <summary>
		/// Class Constructor.
		/// </summary>
		public ModalityWorklistInformationModel() : base("ModalityWorklistInformationModel") {}

		#region BaseInformationModel Overrides
		/// <summary>
		/// Add the given Dataset to the Information Model. The data is normalised into the Information Model.
		/// </summary>
		/// <param name="dataset">Dataset to add to Informatio Model.</param>
		public override void AddToInformationModel(DataSet dataset)
		{
			// PATIENT level
			PatientInformationEntity patientInformationEntity = null;

			// check if the patient IE is already in the patientRootList
			foreach (PatientInformationEntity lPatientInformationEntity in Root)
			{
				if (lPatientInformationEntity.IsFoundIn(dataset))
				{
					patientInformationEntity = lPatientInformationEntity;
					break;
				}
			}

			// patient IE is not already in the patientRootList
			if (patientInformationEntity == null)
			{
				// create a new patient IE from the dataset and add to the patientRootList
				patientInformationEntity = new PatientInformationEntity();
				patientInformationEntity.CopyFrom(dataset);
				Root.Add(patientInformationEntity);
			}

			// VISIT level
			VisitInformationEntity visitInformationEntity = null;

			// check if the visit IE is already in the patient IE children
			foreach (VisitInformationEntity lVisitInformationEntity in patientInformationEntity.Children)
			{
				if (lVisitInformationEntity.IsFoundIn(dataset))
				{
					visitInformationEntity = lVisitInformationEntity;
					break;
				}
			}

			// visit IE is not already in the patient IE children
			if (visitInformationEntity == null)
			{
				// create a new visit IE from the dataset and add to the patient IE children
				visitInformationEntity = new VisitInformationEntity();
				visitInformationEntity.CopyFrom(dataset);
				patientInformationEntity.AddChild(visitInformationEntity);
			}

			// IMAGING SERVICE REQUEST level
			ImagingServiceRequestInformationEntity imagingServiceRequestInformationEntity = null;

			// check if the imaging service request IE is already in the visit IE children
			foreach (ImagingServiceRequestInformationEntity lImagingServiceRequestInformationEntity in visitInformationEntity.Children)
			{
				if (lImagingServiceRequestInformationEntity.IsFoundIn(dataset))
				{
					imagingServiceRequestInformationEntity = lImagingServiceRequestInformationEntity;
					break;
				}
			}

			// imaging service request IE is not already in the visit IE children
			if (imagingServiceRequestInformationEntity == null)
			{
				// create a new imaging service request IE from the dataset and add to the visit IE children
				imagingServiceRequestInformationEntity = new ImagingServiceRequestInformationEntity();
				imagingServiceRequestInformationEntity.CopyFrom(dataset);
				visitInformationEntity.AddChild(imagingServiceRequestInformationEntity);
			}

			// REQUESTED PROCEDURE level
			RequestedProcedureInformationEntity requestedProcedureInformationEntity = null;

			// check if the requested procedure IE is already in the imaging service request IE children
			foreach (RequestedProcedureInformationEntity lRequestedProcedureInformationEntity in imagingServiceRequestInformationEntity.Children)
			{
				if (lRequestedProcedureInformationEntity.IsFoundIn(dataset))
				{
					requestedProcedureInformationEntity = lRequestedProcedureInformationEntity;
					break;
				}
			}

			// requested procedure IE is not already in the imaging service request IE children
			if (requestedProcedureInformationEntity == null)
			{
				// create a new requested procedure IE from the dataset and add to the imaging service request IE children
				requestedProcedureInformationEntity = new RequestedProcedureInformationEntity();
				requestedProcedureInformationEntity.CopyFrom(dataset);
				imagingServiceRequestInformationEntity.AddChild(requestedProcedureInformationEntity);
			}

			// SCHEDULED PROCEDURE STEP level
			ScheduledProcedureStepInformationEntity scheduledProcedureStepInformationEntity = null;

			// check if the scheduled procedure step IE is already in the requested procedure IE children
			foreach (ScheduledProcedureStepInformationEntity lScheduledProcedureStepInformationEntity in requestedProcedureInformationEntity.Children)
			{
				if (lScheduledProcedureStepInformationEntity.IsFoundIn(dataset))
				{
					scheduledProcedureStepInformationEntity = lScheduledProcedureStepInformationEntity;
					break;
				}
			}

			// scheduled procedure step IE is not already in the requested procedure IE children
			if (scheduledProcedureStepInformationEntity == null)
			{
				// create a new scheduled procedure step IE from the dataset and add to the requested procedure IE children
				scheduledProcedureStepInformationEntity = new ScheduledProcedureStepInformationEntity();
				scheduledProcedureStepInformationEntity.CopyFrom(dataset);
				requestedProcedureInformationEntity.AddChild(scheduledProcedureStepInformationEntity);
			}
		}

		/// <summary>
		/// Query the Information Model using the given Query Dataset.
		/// </summary>
		/// <param name="queryDataset">Query Dataset.</param>
		/// <returns>A collection of zero or more query reponse datasets.</returns>
		public override DataSetCollection QueryInformationModel(DataSet queryDataset)
		{
			DataSetCollection queryResponses = new DataSetCollection();

			BaseInformationEntityList matchingScheduledProcedureSteps = new BaseInformationEntityList();

			SequenceItem queryItem = null;
			TagTypeList queryTagTypeList = new TagTypeList();
			TagTypeList returnTagTypeList = new TagTypeList();
			foreach (DvtkData.Dimse.Attribute attribute in queryDataset)
			{
				// special check for the Scheduled Procedure Step Sequence
				if (attribute.Tag == Tag.SCHEDULED_PROCEDURE_STEP_SEQUENCE)
				{
					SequenceOfItems sequenceOfItems = (SequenceOfItems)attribute.DicomValue;
					if (sequenceOfItems.Sequence.Count == 1)
					{
						queryItem = sequenceOfItems.Sequence[0];

						foreach (DvtkData.Dimse.Attribute itemAttribute in queryItem)
						{
							if (itemAttribute.Length != 0)
							{
								queryTagTypeList.Add(new TagType(itemAttribute.Tag, TagTypeEnum.TagRequired));
							}
							returnTagTypeList.Add(new TagType(itemAttribute.Tag, TagTypeEnum.TagOptional));
						}
					}
				}
				else
				{
					if (attribute.Length != 0)
					{
						queryTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagRequired));
					}
					returnTagTypeList.Add(new TagType(attribute.Tag, TagTypeEnum.TagOptional));
				}
			}
			
			// iterate over the Modality Worklist Information Model and save all the matching
			// Scheduled Procedure Steps
			// iterate of all Information Entities
			foreach (PatientInformationEntity patientInformationEntity in Root)
			{
				if ((patientInformationEntity.IsFoundIn(queryTagTypeList, queryDataset)) ||
					(patientInformationEntity.UniversalMatch(queryTagTypeList)))
				{
					foreach (VisitInformationEntity visitInformationEntity in patientInformationEntity.Children)
					{
						if ((visitInformationEntity.IsFoundIn(queryTagTypeList, queryDataset)) ||
							(visitInformationEntity.UniversalMatch(queryTagTypeList)))
						{
							foreach (ImagingServiceRequestInformationEntity imagingServiceRequestInformationEntity in visitInformationEntity.Children)
							{
								if ((imagingServiceRequestInformationEntity.IsFoundIn(queryTagTypeList, queryDataset)) ||
									(imagingServiceRequestInformationEntity.UniversalMatch(queryTagTypeList)))
								{
									foreach (RequestedProcedureInformationEntity requestedProcedureInformationEntity in imagingServiceRequestInformationEntity.Children)
									{
										if ((requestedProcedureInformationEntity.IsFoundIn(queryTagTypeList, queryDataset)) ||
											(requestedProcedureInformationEntity.UniversalMatch(queryTagTypeList)))
										{
											foreach (ScheduledProcedureStepInformationEntity scheduledProcedureStepInformationEntity in requestedProcedureInformationEntity.Children)
											{
												if (scheduledProcedureStepInformationEntity.IsFoundIn(queryTagTypeList, queryItem))
												{
													// add the scheduled procedure step to the matched list
													matchingScheduledProcedureSteps.Add(scheduledProcedureStepInformationEntity);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}

			// we now have a list of all the matching scheduled procedure steps
			foreach (ScheduledProcedureStepInformationEntity matchingScheduledProcedureStepInformationEntity in matchingScheduledProcedureSteps)
			{
				SequenceItem responseItem = new SequenceItem();
				matchingScheduledProcedureStepInformationEntity.CopyTo(returnTagTypeList, responseItem);
				DvtkData.Dimse.Attribute attribute = new DvtkData.Dimse.Attribute(0x00400100, VR.SQ, responseItem);

				DataSet queryResponse = new DataSet();
				queryResponse.Add(attribute);

				RequestedProcedureInformationEntity matchingRequestedProcedureInformationEntity 
					= (RequestedProcedureInformationEntity)matchingScheduledProcedureStepInformationEntity.Parent;
				matchingRequestedProcedureInformationEntity.CopyTo(returnTagTypeList, queryResponse);

				ImagingServiceRequestInformationEntity matchingImagingServiceRequestInformationEntity 
					= (ImagingServiceRequestInformationEntity)matchingRequestedProcedureInformationEntity.Parent;
				matchingImagingServiceRequestInformationEntity.CopyTo(returnTagTypeList, queryResponse);

				VisitInformationEntity matchingVisitInformationEntity 
					= (VisitInformationEntity)matchingImagingServiceRequestInformationEntity.Parent;
				matchingVisitInformationEntity.CopyTo(returnTagTypeList, queryResponse);

				PatientInformationEntity matchingPatientInformationEntity 
					= (PatientInformationEntity)matchingVisitInformationEntity.Parent;
				matchingPatientInformationEntity.CopyTo(returnTagTypeList, queryResponse);

				queryResponses.Add(queryResponse);
			}

			return queryResponses;
		}
		#endregion
	}
}
