// Part of DvtkInformationEntity.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.Text;
using System.Text.RegularExpressions;

using DvtkData.Dimse;

namespace Dvtk.Dicom.InformationEntity
{
	/// <summary>
	/// Summary description for BaseInformationEntity.
	/// </summary>
	public abstract class BaseInformationEntity
	{
		private System.String _level;
		private DataSet _dataset = null;
		private DataSet _additionalDataset = null;
		private BaseInformationEntity _parent = null;
		private BaseInformationEntityList _children = new BaseInformationEntityList();
		private TagTypeList _tagTypeList = new TagTypeList();

		/// <summary>
		/// Class Constructor.
		/// </summary>
		/// <param name="level">Entity level in the Information Model.</param>
		public BaseInformationEntity(System.String level)
		{
			_level = level;
			_dataset = new DataSet(_level);
			_additionalDataset = new DataSet(_level + " Additional");

			// set up the default Tag Type List
			SetDefaultTagTypeList();
		}

		/// <summary>
		/// Get the Entity Level in the Information Model.
		/// </summary>
		public System.String Level
		{
			get
			{
				return _level;
			}
		}

		/// <summary>
		/// Get the local DataSet used to store the Entity attribute values.
		/// </summary>
		public DataSet DataSet
		{
			get
			{
				return _dataset;
			}
			set
			{
				_dataset = value;
			}
		}

		/// <summary>
		/// Get the Entity child list in the Information Model.
		/// </summary>
		public BaseInformationEntityList Children
		{
			get
			{
				return _children;
			}
		}

		/// <summary>
		/// Get the Entity parent in the Information Model.
		/// </summary>
		public BaseInformationEntity Parent
		{
			get
			{
				return _parent;
			}
		}

		/// <summary>
		/// Set/Get the Tag Type list used to interogate the Entity.
		/// </summary>
		public TagTypeList TagTypeList
		{
			set
			{
				_tagTypeList = value;
			}
			get
			{
				return _tagTypeList;
			}
		}

		/// <summary>
		/// Add a child Entity to this.
		/// </summary>
		/// <param name="informationEntity">Child Entity being added.</param>
		public void AddChild(BaseInformationEntity informationEntity)
		{
			informationEntity._parent = this;
			_children.Add(informationEntity);
		}

		/// <summary>
		/// Copy from the given source Dataset into the local Dataset as defined by the
		/// default Tag Type list.
		/// </summary>
		/// <param name="sourceDataset">Source Dataset used to populate the local Dataset.</param>
		public virtual void CopyFrom(AttributeSet sourceDataset)
		{
			CopyFrom(_tagTypeList, sourceDataset);
		}

		/// <summary>
		/// Copy from the given source Dataset into the local Dataset as defined by the
		/// given Tag Type list.
		/// </summary>
		/// <param name="tagTypeList">Tag Type list identifying attributes to copy.</param>
		/// <param name="sourceDataset">Source Dataset used to populate the local Dataset.</param>
		public void CopyFrom(TagTypeList tagTypeList, AttributeSet sourceDataset)
		{
			if (tagTypeList != null)
			{
				foreach (TagType tagType in tagTypeList)
				{
					DvtkData.Dimse.Attribute sourceAttribute = sourceDataset.GetAttribute(tagType.Tag);
					if (sourceAttribute != null)
					{
						_dataset.Add(sourceAttribute);
					}
				}
			}
		}

		/// <summary>
		/// Copy the attribute with a Unique tag from the local Dataset into the given destination Dataset.
		/// </summary>
		/// <param name="destinationDataset">Dataset being populated with the Unique tag attribute.</param>
		public void CopyUniqueTagTo(AttributeSet destinationDataset)
		{
			CopyTo(_tagTypeList, destinationDataset, true);
		}

		/// <summary>
		/// Copy from the local Dataset into the given destination Dataset as defined by the
		/// default Tag Type list.
		/// </summary>
		/// <param name="destinationDataset">Dataset being populated by the default Tag Type list.</param>
		public void CopyTo(AttributeSet destinationDataset)
		{
			CopyTo(_tagTypeList, destinationDataset, false);
		}

		/// <summary>
		/// Copy from the local Dataset into the given destination Dataset as defined by the
		/// given Tag Type list.
		/// </summary>
		/// <param name="tagTypeList">Tag Type list used to define copy.</param>
		/// <param name="destinationDataset">Dataset being populated by the given Tag Type list.</param>
		public void CopyTo(TagTypeList tagTypeList, AttributeSet destinationDataset)
		{
			CopyTo(tagTypeList, destinationDataset, false);
		}

		/// <summary>
		/// Copy from the local Dataset into the given destination Dataset as defined by the
		/// given Tag Type list. If the copyUniqueTagOnly parameter is true - only copy the Unique Tag attribute.
		/// </summary>
		/// <param name="tagTypeList">Tag Type list used to define copy.</param>
		/// <param name="destinationDataset">Dataset being populated by the given Tag Type list.</param>
		/// <param name="copyUniqueTagOnly">Boolean indicator to define use of Unique Tag.</param>
		private void CopyTo(TagTypeList tagTypeList, AttributeSet destinationDataset, bool copyUniqueTagOnly)
		{
			if (tagTypeList != null)
			{
				foreach (TagType tagType in tagTypeList)
				{
					// check if we should only copy the unique TagType
					if (copyUniqueTagOnly)
					{
						if (tagType.Type != TagTypeEnum.TagUnique) continue;
					}

					DvtkData.Dimse.Attribute destinationAttribute = _dataset.GetAttribute(tagType.Tag);
					if (destinationAttribute != null)
					{
						destinationDataset.Add(destinationAttribute);
					}
				}
			}
		}

		/// <summary>
		/// Copy the defined Additional Attributes from the local additional attributes to the given
		/// dataset.
		/// </summary>
		/// <param name="destinationDataset">Destinaion dataset for loacl additional attributes.</param>
		public void CopyAdditionalAttributes(AttributeSet destinationDataset)
		{
			// try adding all additional attributes
			foreach (DvtkData.Dimse.Attribute additionalAttribute in _additionalDataset)
			{
				// check that the attribute is not already in the destination dataset
				if (destinationDataset.GetAttribute(additionalAttribute.Tag) == null)
				{
					destinationDataset.Add(additionalAttribute);
				}
			}
		}

		/// <summary>
		/// Check if a Universal Match is possible on the local dataset using the Tag Type list given.
		/// </summary>
		/// <param name="tagTypeList">Tag type list used for Universal Match.</param>
		/// <returns>Boolean indicating if a Universal Match is true oe false.</returns>
		public bool UniversalMatch(TagTypeList tagTypeList)
		{
			bool UniversalMatch = true;
			if (tagTypeList.Count != 0)
			{
				foreach(TagType tagType in tagTypeList)
				{
					DvtkData.Dimse.Attribute thisAttribute = _dataset.GetAttribute(tagType.Tag);
					if (thisAttribute != null)
					{
						if (thisAttribute.Length != 0)
						{
							UniversalMatch = false;
							break;
						}
						else if (IsTagTypeIn(tagType) == false)
						{
							UniversalMatch = false;
							break;
						}
					}					
				}
			}

			return UniversalMatch;
		}

		/// <summary>
		/// Check if the given Tag Type is in the local Tag Type list.
		/// </summary>
		/// <param name="tagType">Tag Type.</param>
		/// <returns>Boolean indicating if the Tag Type is in the local Tag Type list - true or false.</returns>
		private bool IsTagTypeIn(TagType tagType)
		{
			bool isTagTypeIn = false;
			foreach(TagType lTagType in _tagTypeList)
			{
				if (lTagType.Tag == tagType.Tag)
				{
					isTagTypeIn = true;
					break;
				}
			}

			return isTagTypeIn;
		}

		/// <summary>
		/// Check if the Unique Tag as defined in the local Tag Type list is present in the given match dataset.
		/// </summary>
		/// <param name="matchDataset">Dataset to check for match.</param>
		/// <returns>Boolean indicating if the match dataset contains the default Unique Tag.</returns>
		public bool IsUniqueTagFoundIn(AttributeSet matchDataset)
		{
			return IsFoundIn(_tagTypeList, matchDataset, true);
		}

		/// <summary>
		/// Check if the given match dataset is found in the local dataset using the default Tag Type list. 
		/// A check is made to see if all the attributes in the given match dataset are present in the local
		/// dataset.
		/// </summary>
		/// <param name="matchDataset">Match dataset to check.</param>
		/// <returns>Boolean indicating if the match attributes are present in the local dataset.</returns>
		public virtual bool IsFoundIn(AttributeSet matchDataset)
		{
			return IsFoundIn(_tagTypeList, matchDataset, false);
		}

		/// <summary>
		/// Check if the given match dataset is found in the local dataset using the given Tag Type list. 
		/// A check is made to see if all the attributes in the given match dataset are present in the local
		/// dataset.
		/// </summary>
		/// <param name="tagTypeList">Match Tag Type list.</param>
		/// <param name="matchDataset">Match dataset to check.</param>
		/// <returns>Boolean indicating if the match attributes are present in the local dataset.</returns>
		public bool IsFoundIn(TagTypeList tagTypeList, AttributeSet matchDataset)
		{
			return IsFoundIn(tagTypeList, matchDataset, false);
		}

		/// <summary>
		/// Check if the given match dataset is found in the local dataset using the given Tag Type list. 
		/// A check is made to see if all the attributes in the given match dataset are present in the local
		/// dataset. if the given matchOnUniqueTagOnly parameter is true only the Unique Tags will be checked.
		/// </summary>
		/// <param name="tagTypeList">Match Tag Type list.</param>
		/// <param name="matchDataset">Match dataset to check.</param>
		/// <param name="matchOnUniqueTagOnly">Boolean indicating if only the Unique Tag should be checked.</param>
		/// <returns>Boolean indicating if the match attributes are present in the local dataset.</returns>
		private bool IsFoundIn(TagTypeList tagTypeList, AttributeSet matchDataset, bool matchOnUniqueTagOnly)
		{
			bool isFound = true;
			bool uniqueTagFound = false;

			if (tagTypeList != null)
			{
				foreach (TagType tagType in tagTypeList)
				{
					// check if we should try to match on the unique TagType only
					if (matchOnUniqueTagOnly)
					{
						if (tagType.Type != TagTypeEnum.TagUnique) continue;
					}

					DvtkData.Dimse.Attribute thisAttribute = _dataset.GetAttribute(tagType.Tag);
					DvtkData.Dimse.Attribute matchAttribute = matchDataset.GetAttribute(tagType.Tag);

					if ((thisAttribute != null) &&
						(matchAttribute == null))
					{
						isFound = false;
					}
					else if ((thisAttribute == null) &&
						(matchAttribute != null))
					{
						isFound = false;
					}
					else if ((thisAttribute != null) &&
						(matchAttribute != null))
					{
						// set the unique tag used flag - if we get this far in the code when matching the
						// unique tag only then we must have found it
						if (matchOnUniqueTagOnly)
						{
							uniqueTagFound = true;
						}

						if (thisAttribute.ValueRepresentation != matchAttribute.ValueRepresentation)
						{
							isFound = false;
						}
						else
						{
							if ((thisAttribute.Length == 0) &&
								(matchAttribute.Length ==0))
							{
								// found
							}
							else if (thisAttribute.Length == 0)
							{
								// not found
								isFound = false;
							}
							else if (matchAttribute.Length == 0)
							{
								// not found
								isFound = false;
							}
							else
							{
								switch(thisAttribute.ValueRepresentation)
								{
									case VR.AE:
									{
										ApplicationEntity thisApplicationEntity = (ApplicationEntity)thisAttribute.DicomValue;
										ApplicationEntity matchApplicationEntity = (ApplicationEntity)matchAttribute.DicomValue;
										if (thisApplicationEntity.Values.Count != matchApplicationEntity.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisApplicationEntity.Values.Count; i++)
											{
												if (!WildCardMatchString(matchApplicationEntity.Values[i], thisApplicationEntity.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.AS:
									{
										AgeString thisAgeString = (AgeString)thisAttribute.DicomValue;
										AgeString matchAgeString = (AgeString)matchAttribute.DicomValue;
										if (thisAgeString.Values.Count != matchAgeString.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisAgeString.Values.Count; i++)
											{
												if (!MatchString(matchAgeString.Values[i], thisAgeString.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.AT:
									{
										AttributeTag thisAttributeTag = (AttributeTag)thisAttribute.DicomValue;
										AttributeTag matchAttributeTag = (AttributeTag)matchAttribute.DicomValue;
										if (thisAttributeTag.Values.Count != matchAttributeTag.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisAttributeTag.Values.Count; i++)
											{
												if (matchAttributeTag.Values[i] != thisAttributeTag.Values[i])
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.CS:
									{
										CodeString thisCodeString = (CodeString)thisAttribute.DicomValue;
										CodeString matchCodeString = (CodeString)matchAttribute.DicomValue;
										if (thisCodeString.Values.Count != matchCodeString.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisCodeString.Values.Count; i++)
											{
												if (!WildCardMatchString(matchCodeString.Values[i], thisCodeString.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.DA:
									{
										Date thisDate = (Date)thisAttribute.DicomValue;
										Date matchDate = (Date)matchAttribute.DicomValue;
										if (thisDate.Values.Count != matchDate.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisDate.Values.Count; i++)
											{
												System.String thisDateString = thisDate.Values[i].Trim();
												System.String matchDateString = matchDate.Values[i].Trim();
												switch (matchDateString.Length)
												{
												case 9:
													// ToDate = -YYYYMMDD
													// FromDate = YYYYMMDD-
													if (matchDateString.StartsWith("-"))
													{
														System.String date = matchDateString.Substring(1,8);
														int comparison = thisDateString.CompareTo(date);
														if (comparison > 0)
														{
															isFound = false;
														}
													}
													else if (matchDateString.EndsWith("-"))
													{
														System.String date = matchDateString.Substring(0,8);
														int comparison = thisDateString.CompareTo(date);
														if (comparison < 0)
														{
															isFound = false;
														}
													}
													break;
												case 17:
													// DateRange = YYYYMMDD-YYYYMMDD
													System.String[] dates = matchDateString.Split('-');
													int comparison1 = thisDateString.CompareTo(dates[0]);
													int comparison2 = thisDateString.CompareTo(dates[1]);
													if ((comparison1 < 0) ||
													    (comparison2 > 0))
													{
														isFound = false;
													}
													break;
												case 8:
													// Date = YYYYMMDD
												default:
													if (!MatchString(matchDateString, thisDateString))
													{
														isFound = false;
													}
													break;
												}

												if (isFound == false) break;
											}
										}
										break;
									}
									case VR.DS:
									{
										DecimalString thisDecimalString = (DecimalString)thisAttribute.DicomValue;
										DecimalString matchDecimalString = (DecimalString)matchAttribute.DicomValue;
										if (thisDecimalString.Values.Count != matchDecimalString.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisDecimalString.Values.Count; i++)
											{
												if (!MatchString(matchDecimalString.Values[i], thisDecimalString.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.DT:
									{
										DvtkData.Dimse.DateTime thisDateTime = (DvtkData.Dimse.DateTime)thisAttribute.DicomValue;
										DvtkData.Dimse.DateTime matchDateTime = (DvtkData.Dimse.DateTime)matchAttribute.DicomValue;
										if (thisDateTime.Values.Count != matchDateTime.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisDateTime.Values.Count; i++)
											{
												if (!MatchString(matchDateTime.Values[i], thisDateTime.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.FD:
									{
										FloatingPointDouble thisFloatingPointDouble = (FloatingPointDouble)thisAttribute.DicomValue;
										FloatingPointDouble matchFloatingPointDouble = (FloatingPointDouble)matchAttribute.DicomValue;
										if (thisFloatingPointDouble.Values.Count != matchFloatingPointDouble.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisFloatingPointDouble.Values.Count; i++)
											{
												if (matchFloatingPointDouble.Values[i] != thisFloatingPointDouble.Values[i])
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.FL:
									{
										FloatingPointSingle thisFloatingPointSingle = (FloatingPointSingle)thisAttribute.DicomValue;
										FloatingPointSingle matchFloatingPointSingle = (FloatingPointSingle)matchAttribute.DicomValue;
										if (thisFloatingPointSingle.Values.Count != matchFloatingPointSingle.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisFloatingPointSingle.Values.Count; i++)
											{
												if (matchFloatingPointSingle.Values[i] != thisFloatingPointSingle.Values[i])
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.IS:
									{
										IntegerString thisIntegerString = (IntegerString)thisAttribute.DicomValue;
										IntegerString matchIntegerString = (IntegerString)matchAttribute.DicomValue;
										if (thisIntegerString.Values.Count != matchIntegerString.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisIntegerString.Values.Count; i++)
											{
												if (!MatchString(matchIntegerString.Values[i], thisIntegerString.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.LO:
									{
										LongString thisLongString = (LongString)thisAttribute.DicomValue;
										LongString matchLongString = (LongString)matchAttribute.DicomValue;
										if (thisLongString.Values.Count != matchLongString.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisLongString.Values.Count; i++)
											{
												if (!WildCardMatchString(matchLongString.Values[i], thisLongString.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.LT:
									{
										break;
									}
									case VR.OB:
									{
										break;
									}
									case VR.OF:
									{
										break;
									}
									case VR.OW:
									{
										break;
									}
									case VR.PN:
									{
										PersonName thisPersonName = (PersonName)thisAttribute.DicomValue;
										PersonName matchPersonName = (PersonName)matchAttribute.DicomValue;
										if (thisPersonName.Values.Count != matchPersonName.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisPersonName.Values.Count; i++)
											{
												if (!WildCardMatchString(matchPersonName.Values[i], thisPersonName.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.SH:
									{
										ShortString thisShortString = (ShortString)thisAttribute.DicomValue;
										ShortString matchShortString = (ShortString)matchAttribute.DicomValue;
										if (thisShortString.Values.Count != matchShortString.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisShortString.Values.Count; i++)
											{
												if (!WildCardMatchString(matchShortString.Values[i], thisShortString.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.SL:
									{
										SignedLong thisSignedLong = (SignedLong)thisAttribute.DicomValue;
										SignedLong matchSignedLong = (SignedLong)matchAttribute.DicomValue;
										if (thisSignedLong.Values.Count != matchSignedLong.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisSignedLong.Values.Count; i++)
											{
												if (matchSignedLong.Values[i] != thisSignedLong.Values[i])
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.SQ:
									{
//										SequenceOfItems sequenceOfItems = (SequenceOfItems)attribute.DicomValue;
//										foreach (SequenceItem item in sequenceOfItems.Sequence)
//										{
//										}
										break;
									}
									case VR.SS:
									{
										SignedShort thisSignedShort = (SignedShort)thisAttribute.DicomValue;
										SignedShort matchSignedShort = (SignedShort)matchAttribute.DicomValue;
										if (thisSignedShort.Values.Count != matchSignedShort.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisSignedShort.Values.Count; i++)
											{
												if (matchSignedShort.Values[i] != thisSignedShort.Values[i])
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.ST:
									{
										break;
									}
									case VR.TM:
									{
										Time thisTime = (Time)thisAttribute.DicomValue;
										Time matchTime = (Time)matchAttribute.DicomValue;
										if (thisTime.Values.Count != matchTime.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisTime.Values.Count; i++)
											{
												System.String thisTimeString = thisTime.Values[i].Trim();
												System.String matchTimeString = matchTime.Values[i].Trim();
												switch (matchTimeString.Length)
												{
													case 7:
														// ToDate = -HHMMSS
														// FromDate = HHMMSS-
														if (matchTimeString.StartsWith("-"))
														{
															System.String time = matchTimeString.Substring(1,6);
															int comparison = thisTimeString.CompareTo(time);
															if (comparison > 0)
															{
																isFound = false;
															}
														}
														else if (matchTimeString.EndsWith("-"))
														{
															System.String time = matchTimeString.Substring(0,6);
															int comparison = thisTimeString.CompareTo(time);
															if (comparison < 0)
															{
																isFound = false;
															}
														}
														break;
													case 13:
														// DateRange = HHMMSS-HHMMSS
														System.String[] times = matchTimeString.Split('-');
														int comparison1 = thisTimeString.CompareTo(times[0]);
														int comparison2 = thisTimeString.CompareTo(times[1]);
														if ((comparison1 < 0) ||
															(comparison2 > 0))
														{
															isFound = false;
														}
														break;
													case 6:
														// Date = HHMMSS
													default:
														if (!MatchString(matchTimeString, thisTimeString))
														{
															isFound = false;
														}
														break;
												}

												if (isFound == false) break;
											}
										}
										break;
									}
									case VR.UI:
									{
										UniqueIdentifier thisUniqueIdentifier = (UniqueIdentifier)thisAttribute.DicomValue;
										UniqueIdentifier matchUniqueIdentifier = (UniqueIdentifier)matchAttribute.DicomValue;

										// check for list of UID matching
										if ((thisUniqueIdentifier.Values.Count == 1) && 
											(matchUniqueIdentifier.Values.Count > 1))
										{
											isFound = false;

											// iterate over all the possible matches
											for (int i = 0; i < matchUniqueIdentifier.Values.Count; i++)
											{
												if (MatchString(matchUniqueIdentifier.Values[i], thisUniqueIdentifier.Values[0]))
												{
													isFound = true;
													break;
												}
											}
										}
										else if (thisUniqueIdentifier.Values.Count == matchUniqueIdentifier.Values.Count)
										{
											for (int i = 0; i < thisUniqueIdentifier.Values.Count; i++)
											{
												if (!MatchString(matchUniqueIdentifier.Values[i], thisUniqueIdentifier.Values[i]))
												{
													isFound = false;
													break;
												}
											}
										}
										else
										{
											isFound = false;
										}
										break;
									}
									case VR.UL:
									{
										UnsignedLong thisUnsignedLong = (UnsignedLong)thisAttribute.DicomValue;
										UnsignedLong matchUnsignedLong = (UnsignedLong)matchAttribute.DicomValue;
										if (thisUnsignedLong.Values.Count != matchUnsignedLong.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisUnsignedLong.Values.Count; i++)
											{
												if (matchUnsignedLong.Values[i] != thisUnsignedLong.Values[i])
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.UN:
									{
										break;
									}
									case VR.US:
									{
										UnsignedShort thisUnsignedShort = (UnsignedShort)thisAttribute.DicomValue;
										UnsignedShort matchUnsignedShort = (UnsignedShort)matchAttribute.DicomValue;
										if (thisUnsignedShort.Values.Count != matchUnsignedShort.Values.Count)
										{
											isFound = false;
										}
										else
										{
											for (int i = 0; i < thisUnsignedShort.Values.Count; i++)
											{
												if (matchUnsignedShort.Values[i] != thisUnsignedShort.Values[i])
												{
													isFound = false;
													break;
												}
											}
										}
										break;
									}
									case VR.UT:
									{
										break;
									}

									default:
										isFound = false;
										break;
								}
							}
						}
						if (isFound == false)
						{
							break;
						}
					}
				}
			}

			// check for special case where we should match on the Unique Tag only - but it was not found
			if ((matchOnUniqueTagOnly == true) &&
				(uniqueTagFound == false))
			{
				// no match
				isFound = false;
			}

			return isFound;
		}

		/// <summary>
		/// Implement is any sub-classes - define the default Tag Type list.
		/// </summary>
		protected abstract void SetDefaultTagTypeList();

		/// <summary>
		/// Display this Entity to the Console - useful when debugging.
		/// </summary>
		public void ConsoleDisplay()
		{
			Console.WriteLine("Level: {0}", _level);
			if (_dataset != null)
			{
				_dataset.ConsoleDisplay();
			}

			Console.WriteLine("Children: {0}", _children.Count);
			int child = 1;
			foreach (BaseInformationEntity informationEntity in _children)
			{
				Console.WriteLine("Child: {0}", child++);
				informationEntity.ConsoleDisplay();
			}
		}

		/// <summary>
		/// Add the default attribute values to the local dataset if the attribute values
		/// are not already present in the local dataset. A zero-length attribute in the local
		/// dataset will be replaced by a value of the same attribute (tag) in the defaultDataset.
		/// All child Entities are also processed.
		/// </summary>
		/// <param name="defaultDataset"></param>
		public void AddDefaultAttributes(DataSet defaultDataset)
		{
			// use tag list to determine which attributes should take default values
			foreach (TagType tagType in _tagTypeList)
			{
				// only add a default value it the attribute is not already in the dataset.
				DvtkData.Dimse.Attribute attribute = _dataset.GetAttribute(tagType.Tag);
				if ((attribute == null) ||
					(attribute.Length == 0))
				{
					// check if a default value is available for this tag
					DvtkData.Dimse.Attribute defaultAttribute = defaultDataset.GetAttribute(tagType.Tag);
					if (defaultAttribute != null)
					{
						// need to remove any zero-length attribute
						if ((attribute != null) &&
						    (attribute.Length == 0))
						{
							_dataset.Remove(attribute);
						}

						// add default value to the dataset
						_dataset.Add(defaultAttribute);
					}
				}
			}

			// include all children
			foreach (BaseInformationEntity informationEntity in _children)
			{
				informationEntity.AddDefaultAttributes(defaultDataset);
			}
		}

		/// <summary>
		/// Add the additional attributes to this Entity and any children.
		/// </summary>
		/// <param name="additionalDataset"></param>
		public void AddAdditionalAttributes(DataSet additionalDataset)
		{
			// add the additional attribute
			foreach (DvtkData.Dimse.Attribute additionalAttribute in additionalDataset)
			{
				_additionalDataset.Add(additionalAttribute);
			}

			// include all children
			foreach (BaseInformationEntity informationEntity in _children)
			{
				informationEntity.AddAdditionalAttributes(additionalDataset);
			}
		}

		/// <summary>
		/// Check if the searchKey matches the candidateValue.
		/// </summary>
		/// <param name="searchKey">Key to match against.</param>
		/// <param name="candidateValue">Value to try to match against the searchKey.</param>
		/// <returns>Bool indicating the result of the match.</returns>
		private bool MatchString(String searchKey, String candidateValue)
		{
			bool matchesSearchKey = false;
			String lSearchKey = searchKey.Trim();
			String lCandidateValue = candidateValue.Trim();

			// Check for simple string equivalence
			if (lSearchKey == lCandidateValue)
			{
				// Strings the same
				matchesSearchKey = true;
			}

			// Return matching result
			return matchesSearchKey;
		}

		/// <summary>
		/// Check if the searchKey matches the candidateValue. A "*" can be present as the last
		/// character of the searchKey to indciate a wildcard match from that point in the string
		/// onwards.
		/// </summary>
		/// <param name="searchKey">Key to match against - last character maybe a "*" wildcard.</param>
		/// <param name="candidateValue">Value to try to match against the searchKey.</param>
		/// <returns>Bool indicating the result of the match.</returns>
		private bool WildCardMatchString(String searchKey, String candidateValue)
		{
			bool matchesSearchKey = false;
			String lSearchKey = searchKey.Trim();
			String lCandidateValue = candidateValue.Trim();

			// Check if the searchKey ends in a wildcard
			if (lSearchKey.EndsWith("*"))
			{
				// Only wildcard in saerch key
				if (lSearchKey.Length == 1)
				{
					// Everything matches
					matchesSearchKey = true;
				}
				else
				{
					// Set up the regular expression
					// Need to replace the "*" wildcard with the equivalent wildcard in RegEx syntax - i.e. "+"
					string regExSearchKey = lSearchKey.Replace("*","+");

					// Check if the candidate matches the regular expression
					Regex regEx = new Regex(regExSearchKey);
					Match match = regEx.Match(lCandidateValue);
					if (match.Length != 0)
					{
						// Search key matches
						matchesSearchKey = true;
					}
				}
			}
			else
			{
				// Check for simple string equivalence
				if (lSearchKey == lCandidateValue)
				{
					// Strings the same
					matchesSearchKey = true;
				}
			}
			
			// Return matching result
			return matchesSearchKey;
		}
	}
}
