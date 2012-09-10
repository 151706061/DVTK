// ------------------------------------------------------
// DVTk - The Healthcare Validation Toolkit (www.dvtk.org)
// Copyright © 2009 DVTk
// ------------------------------------------------------
// This file is part of DVTk.
//
// DVTk is free software; you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License as published by the Free Software Foundation; either version 3.0
// of the License, or (at your option) any later version. 
// 
// DVTk is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even
// the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser
// General Public License for more details. 
// 
// You should have received a copy of the GNU Lesser General Public License along with this
// library; if not, see <http://www.gnu.org/licenses/>

using System;
using System.Collections;
using System.IO;
using System.Data;

using DvtkData.Dimse;
using DvtkHighLevelInterface.Common.Threads;
using DvtkHighLevelInterface.Common.Other;
using DvtkHighLevelInterface.Dicom.Files;
using DvtkHighLevelInterface.Dicom.Threads;
using DvtkHighLevelInterface.Common.Messages;
using DvtkHighLevelInterface.Dicom.Messages;

namespace AnonymizationUtility
{
	using HLI = DvtkHighLevelInterface.Dicom.Other;
	using sequenceTag = DvtkData.Dimse.Tag;

	/// <summary>
	/// Summary description for Utility.
	/// </summary>
	public class Utility
	{
		public static HLI.DataSet dcmDataset = new HLI.DataSet();
		private static Hashtable patientInfo = new Hashtable();
		private static Hashtable allUIDInfo = new Hashtable();
		private static ThreadManager threadMgr = new ThreadManager();
		private static bool nullPatientID = false;
		string PATIENT_ID = TagString(sequenceTag.PATIENT_ID);
		private bool anonymizationType = true ;
		
		public bool AnonymizationType 
		{
			get 
			{ 
				return anonymizationType; 
			}
			set 
			{ 
				anonymizationType = value; 
			}
		}
		
		public Utility(){}
		
		public ArrayList GetFilesRecursively(DirectoryInfo directory) 
		{
			ArrayList allDCMFiles = new ArrayList();
			try
			{
				// Get all the subdirectories
				FileSystemInfo[] infos = directory.GetFileSystemInfos();
				foreach (FileSystemInfo f in infos)
				{
					if (f is FileInfo) 
					{
						if ((f.Extension.ToLower() == ".dcm")||(f.Extension == null) || (f.Extension == ""))
						{
							if ((f.Name.ToLower() == "dicomdir")&&((f.Extension == null) || (f.Extension == "")))
							{
								// do nothing.
							}
							else 
							{
								allDCMFiles.Add (f.FullName);
							}
						}
					} 
					else 
					{
						allDCMFiles.AddRange(GetFilesRecursively((DirectoryInfo)f));
					}
				}
			}
			catch ( Exception e3)
			{
				Console.WriteLine(e3.Message);
			}
			return allDCMFiles;
		}

		# region Anonymization

		private string[] anonymizedAttributes = {
													TagString(sequenceTag.INSTITUTION_NAME),
													TagString(sequenceTag.INSTITUTION_ADDRESS),
													TagString(sequenceTag.REFERRING_PHYSICIANS_NAME),
													TagString(sequenceTag.REFERRING_PHYSICIANS_ADDRESS),
													TagString(sequenceTag.REFERRING_PHYSICIANS_TELEPHONE_NUMBERS),
													TagString(sequenceTag.STATION_NAME),
													TagString(sequenceTag.INSTITUTIONAL_DEPARTMENT_NAME),
													TagString(sequenceTag.PHYSICIANS_OF_RECORD),
													TagString(sequenceTag.PERFORMING_PHYSICIANS_NAME),
													TagString(sequenceTag.NAME_OF_PHYSICIANS_READING_STUDY),
													TagString(sequenceTag.OPERATORS_NAME),
													TagString(sequenceTag.ADMITTING_DIAGNOSIS_DESCRIPTION),
													TagString(sequenceTag.PATIENTS_NAME),
													TagString(sequenceTag.PATIENTS_BIRTH_DATE),
													TagString(sequenceTag.PATIENTS_BIRTH_TIME),
													TagString(sequenceTag.PATIENTS_SEX),
													TagString(sequenceTag.OTHER_PATIENT_IDS),
													TagString(sequenceTag.OTHER_PATIENT_NAMES ),
													TagString(sequenceTag.PATIENTS_AGE),
													TagString(sequenceTag.PATIENTS_SIZE),
													TagString(sequenceTag.PATIENTS_WEIGHT),
													TagString(sequenceTag.MEDICAL_RECORD_LOCATOR),
													TagString(sequenceTag.ETHNIC_GROUP),
													TagString(sequenceTag.OCCUPATION),
													TagString(sequenceTag.ADDITIONAL_PATIENT_HISTORY),
													TagString(sequenceTag.PATIENT_COMMENTS),
													TagString(sequenceTag.DEVICE_SERIAL_NUMBER),
													TagString(sequenceTag.REQUEST_ATTRIBUTES_SEQUENCE),
													TagString(sequenceTag.STORAGE_MEDIA_FILE_SET_UID)};

		private string[] anonymizedAttributesComplete = {
															TagString(sequenceTag.INSTANCE_CREATOR_UID),
															TagString(sequenceTag.INSTITUTION_NAME),
															TagString(sequenceTag.INSTITUTION_ADDRESS),
															TagString(sequenceTag.REFERRING_PHYSICIANS_NAME),
															TagString(sequenceTag.REFERRING_PHYSICIANS_ADDRESS),
															TagString(sequenceTag.REFERRING_PHYSICIANS_TELEPHONE_NUMBERS),
															TagString(sequenceTag.STATION_NAME),
															TagString(sequenceTag.STUDY_DESCRIPTION),
															TagString(sequenceTag.SERIES_DESCRIPTION),
															TagString(sequenceTag.INSTITUTIONAL_DEPARTMENT_NAME),
															TagString(sequenceTag.PHYSICIANS_OF_RECORD),
															TagString(sequenceTag.PERFORMING_PHYSICIANS_NAME),
															TagString(sequenceTag.NAME_OF_PHYSICIANS_READING_STUDY),
															TagString(sequenceTag.OPERATORS_NAME),
															TagString(sequenceTag.ADMITTING_DIAGNOSIS_DESCRIPTION),
															TagString(sequenceTag.DERIVATION_DESCRIPTION),
															TagString(sequenceTag.PATIENTS_NAME),
															TagString(sequenceTag.PATIENTS_BIRTH_DATE),
															TagString(sequenceTag.PATIENTS_BIRTH_TIME),
															TagString(sequenceTag.PATIENTS_SEX),
															TagString(sequenceTag.OTHER_PATIENT_IDS),
															TagString(sequenceTag.OTHER_PATIENT_NAMES ),
															TagString(sequenceTag.PATIENTS_AGE),
															TagString(sequenceTag.PATIENTS_SIZE),
															TagString(sequenceTag.PATIENTS_WEIGHT),
															TagString(sequenceTag.MEDICAL_RECORD_LOCATOR),
															TagString(sequenceTag.ETHNIC_GROUP),
															TagString(sequenceTag.OCCUPATION),
															TagString(sequenceTag.ADDITIONAL_PATIENT_HISTORY),
															TagString(sequenceTag.PATIENT_COMMENTS),
															TagString(sequenceTag.DEVICE_SERIAL_NUMBER),
															TagString(sequenceTag.PROTOCOL_NAME),
															TagString(sequenceTag.STUDY_ID),
															TagString(sequenceTag.FRAME_OF_REFERENCE_UID),
															TagString(sequenceTag.SYNCHRONIZATION_FRAME_OF_REFERENCE_UID),
															TagString(sequenceTag.IMAGE_COMMENTS),
															TagString(sequenceTag.REQUEST_ATTRIBUTES_SEQUENCE),
															TagString(sequenceTag.STORAGE_MEDIA_FILE_SET_UID)};


		
		// Key tags that will be used to maintain patient hierarchy
		private string[] compositeKeyTags = {													 
													   TagString(sequenceTag.ACCESSION_NUMBER),
		};
		private string[] compositeKeyTagsComplete = {													 
														TagString(sequenceTag.STUDY_INSTANCE_UID),
														TagString(sequenceTag.SERIES_INSTANCE_UID),
														TagString(sequenceTag.SOP_INSTANCE_UID),
														TagString(sequenceTag.ACCESSION_NUMBER),
														TagString(sequenceTag.REFERENCED_SOP_INSTANCE_UID),
		};

		/// <summary>
		/// Update the attributes in the composite with the new values
		/// </summary>
		/// <param name="composite">
		/// composite for Anonymization
		/// </param>
		public void UpdateAnonymizedAttributes( HLI.Attribute composite) 
		{
			//Iterate through all attributes specified for anonymization by DICOM
			//and anonymize them.
			try
			{
				string[] tempAnonymizedAttributes = null;
				if(anonymizationType)
				{
					tempAnonymizedAttributes = anonymizedAttributes ;
				}
				else 
				{
					tempAnonymizedAttributes = anonymizedAttributesComplete;
				}
				string attributeValue = TagString(composite.GroupNumber , composite.ElementNumber);
				for (int i = 0; i < tempAnonymizedAttributes.Length; i++) 
				{
					if (attributeValue == tempAnonymizedAttributes[i]) 
					{

						HLI.Attribute attrValue = composite ;

						string anonymised = null;
						if (attrValue != null) 
						{
							DvtkData.Dimse.VR dicomVR = attrValue.VR;
							switch (dicomVR) 
							{
								case VR.PN:
									if (tempAnonymizedAttributes[i] == TagString(sequenceTag.PATIENTS_NAME))
									{
										// Tag not found in in-params
										// assigning patient id
										if (anonymised == null) 
										{
											//todo Later
											anonymised = Guid.NewGuid().ToString().Replace("-","p").Substring(1,15);
										}
										composite.Values.Clear();
										composite.Values.Add(anonymised);

									}
									else 
									{
										HLI.Values tempValue = attrValue.Values;
										if(tempValue.Count>0)
										{
											for( int j = 0 ; j<tempValue.Count ;j++)
											{
												tempValue[j] = anonymised;
											}
										}
										composite.Values.Clear();
										composite.Values.Add(tempValue);
									}
								
									break;
								case VR.UI:
								
									composite.Values.Clear();
									composite.Values.Add(anonymised);
									
									break;
								case VR.SH:
									if (
										tempAnonymizedAttributes[i] == 
										TagString(sequenceTag.ACCESSION_NUMBER) && 
										anonymised == null
										) 
									{
										anonymised = Guid.NewGuid().ToString().Replace("-","a").Substring(1,9);
									}  
									composite.Values.Clear();
									composite.Values.Add(anonymised);
									break;
								case VR.AS:
									composite.Values.Clear();
									composite.Values.Add(anonymised);
									break;
								case VR.LO:
									composite.Values.Clear();
									composite.Values.Add(anonymised);
									break;
								case VR.DA:
									if (tempAnonymizedAttributes[i] == TagString(sequenceTag.PATIENTS_BIRTH_DATE))
									{
										// Tag not found in in-params
										// assigning patient id
										if (anonymised == null) 
										{
											//todo Later
											anonymised = System.DateTime.Today.Year.ToString() + System.DateTime.Today.Month.ToString("00") + System.DateTime.Today.Day.ToString("00");
										}
										composite.Values.Clear();
										composite.Values.Add(anonymised);
									}
									else 
									{
										composite.Values.Clear();
										composite.Values.Add(anonymised);
									}
									break;
								case VR.TM:
									if (tempAnonymizedAttributes[i] == TagString(sequenceTag.PATIENTS_BIRTH_TIME))
									{
										// Tag not found in in-params
										// assigning patient id
										if (anonymised == null) 
										{
											//todo Later
											anonymised = System.DateTime.Now.TimeOfDay.Hours.ToString("00") + System.DateTime.Now.TimeOfDay.Minutes.ToString("00") + System.DateTime.Now.TimeOfDay.Seconds.ToString("00");
										}
										composite.Values.Clear();
										composite.Values.Add(anonymised);
									}
									else 
									{
										composite.Values.Clear();
										composite.Values.Add(anonymised);
									}
									break;
								case VR.LT:
									composite.Values.Clear();
									composite.Values.Add(anonymised);
									break;
								case VR.ST:
									composite.Values.Clear();
									composite.Values.Add(anonymised);
									break;
								case VR.CS:
									if (tempAnonymizedAttributes[i] == TagString(sequenceTag.PATIENTS_SEX))
									{
										// Tag not found in in-params
										// assigning patient id
										if (anonymised == null) 
										{
											//todo Later
											anonymised = "O" ;
										}
										composite.Values.Clear();
										composite.Values.Add(anonymised);
									}
									else 
									{
										composite.Values.Clear();
										composite.Values.Add(anonymised);
									}
									break;
								case VR.DS:
									composite.Values.Clear();
									composite.Values.Add(anonymised);
									break;
							}
						}
					}
				}
			}
			catch ( Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		// <summary>
		/// Cache the patient information for reference updation in Presentation 
		/// State later.
		/// </summary>
		/// <param name="composite">
		/// The composite to be anonymized
		/// </param>
		/// <param name="isImage">
		/// if isImage is true then we are updating for image otherwise we are 
		/// updating for ps
		/// when the composite is PS object the SeriesInstaceUID will be repaired later.
		///</param>
		public void CacheAndRepairIdentifyingAttribute(HLI.Attribute composite) 
		{
			try
			{
				string attributeValue = TagString(composite.GroupNumber , composite.ElementNumber);
				string oldValue;
				string[] tempcompositeKeyTags = null;
				if(!anonymizationType)
				{
					tempcompositeKeyTags = compositeKeyTagsComplete ;
				}
				else 
				{
					tempcompositeKeyTags = compositeKeyTags;
				}
			
				for (int i = 0; i < tempcompositeKeyTags.Length; i++) 
				{
					if (attributeValue == tempcompositeKeyTags[i]) 
					{
						HLI.Attribute attrValue = composite;
						if (attrValue != null) 
						{
							HLI.Values initialValues = composite.Values ;
							oldValue = initialValues[0];
							DvtkData.Dimse.VR dicomVR = attrValue.VR;
							switch (dicomVR) 
							{
								case VR.UI:
									string tempUIID ;
									if (nullPatientID)
									{
										tempUIID = HLI.UID.Create();
									} 
									else 
									{
										tempUIID = (string)allUIDInfo[oldValue];
										if (tempUIID == null) 
										{
											tempUIID = UIDModifier(oldValue);
										}
									}
									// In case of Series always store the current ID in the map. 
									// This will be used while updating the series reference.
									allUIDInfo[oldValue] = tempUIID;
									composite.Values.Clear();
									composite.Values.Add(tempUIID); 
									break;
						
								case VR.SH:
									string uniqueAccesionNumber ;
									if (nullPatientID)
									{
										uniqueAccesionNumber = Guid.NewGuid().ToString().Replace("-","a").Substring(1,9);
									} 
									else 
									{
										uniqueAccesionNumber = (string)allUIDInfo[oldValue];
										if (uniqueAccesionNumber == null) 
										{
											uniqueAccesionNumber = Guid.NewGuid().ToString().Replace("-","a").Substring(1,9);
										}
									}
									// In case of Series always store the current ID in the map. 
									// This will be used while updating the series reference.
									allUIDInfo[oldValue] = uniqueAccesionNumber;
									//dcmDataset.Set(compositeKeyTags[i],VR.SH,uniqueAccesionNumber);
									composite.Values.Clear();
									composite.Values.Add(uniqueAccesionNumber); 
									break;
							}
						}
					}
				}
			}
			catch ( Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}


		public string UIDModifier( string oldValue)
		{
			string[] initialTemp = null;
			string initialTemporary = "";
			initialTemp = oldValue.Split('.');
			if ( initialTemp.Length > 5)
			{
				initialTemporary = initialTemp[0];
				for( int i =1; i <5; i++ )
				{
					initialTemporary = initialTemporary + "." + initialTemp[i];
				}
			}
			else 
			{
				initialTemporary = oldValue ;
			}
									
			string tempUIID = UIDGenerator(initialTemporary);
			return tempUIID ;

		}
		
	
		/// <summary>
		/// Conversion function for Tag to string format
		/// </summary>
		/// <param name="groupNr"></param>
		/// <param name="elementNr"></param>
		/// <param name="display"></param>
		/// <returns></returns>
		public static string TagString(ushort groupnr , ushort elementnr)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.Byte[] groupByteArray = System.BitConverter.GetBytes(groupnr);
			System.Byte[] elementByteArray = System.BitConverter.GetBytes(elementnr);
			if (System.BitConverter.IsLittleEndian)
			{
				// Display as Big Endian
				System.Array.Reverse(groupByteArray);
				System.Array.Reverse(elementByteArray);
			}
			string hexByteStr0, hexByteStr1;

			hexByteStr0 = groupByteArray[0].ToString("x");
			if (hexByteStr0.Length == 1) hexByteStr0 = "0" + hexByteStr0; // prepend with leading zero
			hexByteStr1 = groupByteArray[1].ToString("x");
			if (hexByteStr1.Length == 1) hexByteStr1 = "0" + hexByteStr1; // prepend with leading zero
			
			sb.AppendFormat("0x{0}{1}", hexByteStr0, hexByteStr1);
			
			hexByteStr0 = elementByteArray[0].ToString("x");
			if (hexByteStr0.Length == 1) hexByteStr0 = "0" + hexByteStr0; // prepend with leading zero
			hexByteStr1 = elementByteArray[1].ToString("x");
			if (hexByteStr1.Length == 1) hexByteStr1 = "0" + hexByteStr1; // prepend with leading zero
			
			sb.AppendFormat("{0}{1}", hexByteStr0, hexByteStr1);
			return sb.ToString();
		}


		/// <summary>
		/// Conversion function for Tag to string format
		/// </summary>
		/// <param name="groupNr"></param>
		/// <param name="elementNr"></param>
		/// <param name="display"></param>
		/// <returns></returns>
		public static string TagString(sequenceTag tag )
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.Byte[] groupByteArray = System.BitConverter.GetBytes(tag.GroupNumber);
			System.Byte[] elementByteArray = System.BitConverter.GetBytes(tag.ElementNumber);
			if (System.BitConverter.IsLittleEndian)
			{
				// Display as Big Endian
				System.Array.Reverse(groupByteArray);
				System.Array.Reverse(elementByteArray);
			}
			string hexByteStr0, hexByteStr1;

			hexByteStr0 = groupByteArray[0].ToString("x");
			if (hexByteStr0.Length == 1) hexByteStr0 = "0" + hexByteStr0; // prepend with leading zero
			hexByteStr1 = groupByteArray[1].ToString("x");
			if (hexByteStr1.Length == 1) hexByteStr1 = "0" + hexByteStr1; // prepend with leading zero
			
			sb.AppendFormat("0x{0}{1}", hexByteStr0, hexByteStr1);
			
			hexByteStr0 = elementByteArray[0].ToString("x");
			if (hexByteStr0.Length == 1) hexByteStr0 = "0" + hexByteStr0; // prepend with leading zero
			hexByteStr1 = elementByteArray[1].ToString("x");
			if (hexByteStr1.Length == 1) hexByteStr1 = "0" + hexByteStr1; // prepend with leading zero
			
			sb.AppendFormat("{0}{1}", hexByteStr0, hexByteStr1);
			return sb.ToString();
		}
		
		public void PatientAttributes(HLI.DataSet dcmDataset)
		{
			try
			{
					HLI.Values	initialPatientName = dcmDataset.GetValues(TagString(sequenceTag.PATIENTS_NAME));
					HLI.Values	initialPatientId = dcmDataset.GetValues(TagString(sequenceTag.PATIENT_ID));
					string patientHashKey = initialPatientName[0] + "*!#@" + initialPatientId[0];
					string patientName = "";
					string patientId = "";
					if (allUIDInfo.Contains(patientHashKey)) 
					{
						string temp = allUIDInfo[patientHashKey].ToString();
						string tempName = temp.Substring(0,temp.IndexOf("*!#@"));
						string tempPatientId = temp.Substring(temp.IndexOf("*!#@")+4);
						if ( dcmDataset.Exists(TagString(sequenceTag.PATIENTS_NAME)))
						{
							dcmDataset.Set(TagString(sequenceTag.PATIENTS_NAME), VR.PN,tempName);
						}
						if ( dcmDataset.Exists(TagString(sequenceTag.PATIENT_ID)))
						{
							dcmDataset.Set(TagString(sequenceTag.PATIENT_ID), VR.LO, tempPatientId);
						}
					} 
					else 
					{
						if ( dcmDataset.Exists(TagString(sequenceTag.PATIENTS_NAME)))
						{
							patientName = Guid.NewGuid().ToString().Replace("-","p0").Substring(0,15);
							dcmDataset.Set(TagString(sequenceTag.PATIENTS_NAME), VR.PN,patientName);
						}
						if ( dcmDataset.Exists(TagString(sequenceTag.PATIENT_ID)))
						{
							patientId = Guid.NewGuid().ToString().Replace("-","p1").Substring(0,15);
							dcmDataset.Set(TagString(sequenceTag.PATIENT_ID), VR.LO, patientId);
						}

						allUIDInfo[patientHashKey] = patientName + "*!#@" + patientId;
					}
			}
			catch(Exception ex)
			{
				string MessageText = ex.Message ;
				Console.WriteLine(MessageText);
			}

		}

		public void SequenceAttribute_recursive (HLI.Attribute seqAttr)
		{

			int numberOfItems = seqAttr.ItemCount;
			for( int i=0; i<numberOfItems;i++)
			{
				HLI.SequenceItem item = seqAttr.GetItem(i+1);
				int numberOfattributes = item.Count ;
				for (int j=0; j <numberOfattributes ; j++)
				{
					HLI.Attribute attrInItem = item[j];
					if(attrInItem.VR != VR.SQ)
					{
						CacheAndRepairIdentifyingAttribute(attrInItem);
						UpdateAnonymizedAttributes(attrInItem);
						
					}
					else 
					{
						SequenceAttribute_recursive(attrInItem);
					}
				}
			}
		}
		
		public UInt32 uniqid = 0;
		
		public Byte uniq8odd()
		{
			// generate next odd number
			if ((uniqid & 0x01) != 0) 
			{
				// make counter even
				uniqid++;
			}

			// make counter odd
			uniqid++;

			return (Byte) (uniqid & 0x000000ff);
		}

		public string UIDGenerator(string implementaionUid)
		{
			
			string tempUID = implementaionUid + ".01." + System.DateTime.Now.TimeOfDay.Milliseconds + "." +System.DateTime.Now.TimeOfDay.TotalSeconds + "." + uniq8odd();
			return tempUID;
		}
		public void cleanup()
		{
			try
			{
				DirectoryInfo dirInfo = new DirectoryInfo(Environment.CurrentDirectory);
				foreach ( FileInfo  file in dirInfo.GetFiles())
				{
					if ((file.Extension.ToLower() == ".pix") ||(file.Extension.ToLower() == ".idx"))
					{
						file.Delete();
					}
				}
			}
			catch ( Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}
		# endregion		
	}
	
}
