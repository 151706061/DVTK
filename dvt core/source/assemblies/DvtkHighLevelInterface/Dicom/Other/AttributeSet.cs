using System;
using System.Collections;



namespace DvtkHighLevelInterface
{
	/// <summary>
	/// This abstract class represents a set of attributes.
	/// </summary>
	abstract public class AttributeSet
	{
		//
		// - Fields -
		//

		/// <summary>
		/// The encapsulated AttributeSet from the DvtkData librbary.
		/// </summary>
		protected DvtkData.Dimse.AttributeSet dvtkDataAttributeSet = null;



		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dvtkDataAttributeSet">The encapsulated DvtkData Attribute Set</param>
		internal AttributeSet(DvtkData.Dimse.AttributeSet dvtkDataAttributeSet)
		{
			this.dvtkDataAttributeSet = dvtkDataAttributeSet;
		}



		//
		// - Properties -
		//

		/// <summary>
		/// Get the number of attributes.
		/// </summary>
		public int Count
		{
			get
			{
				int count = 0;

				// Start Interface logging.
				// InterfaceLogging.Start(this);

				count = dvtkDataAttributeSet.Count;

				// End Interface logging.
				// InterfaceLogging.End(count);

				return(count);
			}
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Does the attribute exist.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/').</param>
		/// <returns>Boolean indicating if the attribute exists.</returns>
		public bool AttributeExists(String tagSequence)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, tagSequence);

			bool exists = false;

			Attribute attribute = GetAttribute(tagSequence);

			if (attribute == null)
			{
				exists = false;
			}
			else
			{
				exists = true;
			}

			// End Interface logging.
			// InterfaceLogging.End(exists);

			return (exists);
		}

		// Zero based index.
		internal Attribute GetAttribute(int index)
		{
			Attribute attribute = Attribute.GetAttribute("0x00000000", this.dvtkDataAttributeSet[index]);
			
			return attribute;
		}

		/// <summary>
		/// Get an Attribute from this AttributeSet.
		/// 
		/// Exception (of type Exception) is thrown if the supplied tag sequence is not valid.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/').</param>
		/// <returns>The Attribute if it exists, otherwise null.</returns>
		internal Attribute GetAttribute(String tagSequence)
		{
			Tag[] tags;
			Attribute attribute = null;

			tags = Tag.InterpretTagSequence(tagSequence);

			AttributeSet currentAttributeSet = this;
			String currentTagSequence = "";
			String currentTagSequenceLastTagNoIndex = "";
				
			foreach(Tag tag in tags)
			{
				// Set the currentTagSequence.
				if (currentTagSequence == "")
				{
					currentTagSequence = tag.TagAsString;
					currentTagSequenceLastTagNoIndex = tag.TagWithoutIndex;
				}
				else
				{
					currentTagSequence+= "/" + tag.TagAsString;
					currentTagSequenceLastTagNoIndex = currentTagSequence + "/" + tag.TagWithoutIndex;
				}

				// Create the correct Attribute object.
				DvtkData.Dimse.Attribute dvtkDataAttribute = currentAttributeSet.dvtkDataAttributeSet.GetAttribute(tag.TagAsUInt32);

				if (dvtkDataAttribute == null)
				{
					attribute = null;
					break;
				}
				else
				{
					attribute = Attribute.GetAttribute(tagSequence, dvtkDataAttribute);
				}

				// If this tag contains an index, check that the attribute is a sequence attribute
				// and retrieve the correct attribute set from it.
				if (tag.ContainsIndex)
				{
					if (attribute is SequenceAttribute)
					{
						SequenceAttribute sequenceAttribute = attribute as SequenceAttribute;

						if (tag.Index > sequenceAttribute.ItemCount)
						{
//								String errorText = "Sequence attribute with tag sequence " + currentTagSequenceLastTagNoIndex + " contains " +  sequenceAttribute.ItemCount.ToString() + " items.";								
//								errorText+= "\r\nThe script however is expecting it to have an item with index " + tag.Index.ToString() + ".";
//								// InterfaceLogging.WriteError(errorText);

							attribute = null;
							break;
						}
						else
						{
							currentAttributeSet = sequenceAttribute.GetItem(tag.Index);
						}
					}
					else
					{
						String errorText = "Attribute with tag sequence " + currentTagSequenceLastTagNoIndex + " is not a sequence attribute.";
						errorText+= "\r\nThe script however is expecting it because the tag sequence " + currentTagSequence + " contains an index.";

						// InterfaceLogging.WriteError(errorText);
						attribute = null;
						break;
					}
				}
			}
			
			return attribute;
		}




		// Zero based index.
		public String GetAttributeName(int index)
		{
			return(this.dvtkDataAttributeSet[index].Name);
		}




		/// <summary>
		/// Get the attribute set this attribute belongs to.
		/// TODO!!: merge this code with GetAttribute method.
		/// </summary>
		/// <param name="tagSequence"></param>
		/// <returns></returns>
		internal AttributeSet GetAttributeSet(String tagSequence)
		{
			Tag[] tags;
			Attribute attribute = null;

			tags = Tag.InterpretTagSequence(tagSequence);

			AttributeSet currentAttributeSet = this;
			String currentTagSequence = "";
			String currentTagSequenceLastTagNoIndex = "";
				
			foreach(Tag tag in tags)
			{
				// Set the currentTagSequence.
				if (currentTagSequence == "")
				{
					currentTagSequence = tag.TagAsString;
					currentTagSequenceLastTagNoIndex = tag.TagWithoutIndex;
				}
				else
				{
					currentTagSequence+= "/" + tag.TagAsString;
					currentTagSequenceLastTagNoIndex = currentTagSequence + "/" + tag.TagWithoutIndex;
				}

				// Create the correct Attribute object.
				DvtkData.Dimse.Attribute dvtkDataAttribute = currentAttributeSet.dvtkDataAttributeSet.GetAttribute(tag.TagAsUInt32);

				if (dvtkDataAttribute == null)
				{
					attribute = null;
					break;
				}
				else
				{
					attribute = Attribute.GetAttribute(tagSequence, dvtkDataAttribute);
				}

				// If this tag contains an index, check that the attribute is a sequence attribute
				// and retrieve the correct attribute set from it.
				if (tag.ContainsIndex)
				{
					if (attribute is SequenceAttribute)
					{
						SequenceAttribute sequenceAttribute = attribute as SequenceAttribute;

						if (tag.Index > sequenceAttribute.ItemCount)
						{
							String errorText = "Sequence attribute with tag sequence " + currentTagSequenceLastTagNoIndex + " contains " +  sequenceAttribute.ItemCount.ToString() + " items.";								
							errorText+= "\r\nThe script however is expecting it to have an item with index " + tag.Index.ToString() + ".";

							// InterfaceLogging.WriteError(errorText);
							attribute = null;
							break;
						}
						else
						{
							currentAttributeSet = sequenceAttribute.GetItem(tag.Index);
						}
					}
					else
					{
						String errorText = "Attribute with tag sequence " + currentTagSequenceLastTagNoIndex + " is not a sequence attribute.";
						errorText+= "\r\nThe script however is expecting it because the tag sequence " + currentTagSequence + " contains an index.";

						// InterfaceLogging.WriteError(errorText);
						attribute = null;
						break;
					}
				}
			}
			
			return currentAttributeSet;
		}


		// Zero based index.
		public UInt32 GetAttributeTagAsUInt32(int index)
		{
			DvtkData.Dimse.Tag dvtkDataTag = this.dvtkDataAttributeSet[index].Tag;

			return((UInt32)(dvtkDataTag.ElementNumber + (dvtkDataTag.GroupNumber * 65536)));
		}












		/// <summary>
		/// Get the Value Multiplicity of an Attribute.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/').</param>
		/// <returns>The Value Multiplicity.</returns>
		public int GetAttributeVm(String tagSequence)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, tagSequence);

			int vm = 0;

			Attribute attribute = GetAttribute(tagSequence);

			// If attribute does not exist...
			if (attribute == null)
			{
				// InterfaceLogging.WriteError("Attribute does not exist.\r\nReturning 0 as VM.");

				vm = 0;
			}
				// If attribute exists...
			else
			{
				if (attribute is SimpleAttribute)
				{
					SimpleAttribute simpleAttribute = attribute as SimpleAttribute;

					vm = simpleAttribute.GetValues().Count;
				}
				else if (attribute is PixelDataAttribute)
				{
					// InterfaceLogging.WriteWarning("Determining of VM not implemented for pixel data attributes in the High Level Interface./r/nReturning 1");
					vm = 1;
				}
				else if (attribute is SequenceAttribute)
				{
					// InterfaceLogging.WriteWarning("Determining of VM not implemented for sequence attributes with VR in the High Level Interface./r/nReturning 1");
					vm = 1;
				}
				else if (attribute is UnknownAttribute)
				{
					// InterfaceLogging.WriteWarning("Determining of VM not implemented for attributes with VR Unknown in the High Level Interface./r/nReturning 1");
					vm = 1;					
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Not expecting this type of attribute in the code");
				}
			}

			// End Interface logging.
			// InterfaceLogging.End(vm);

			return (vm);
		}

		// Zero based index.
		public DvtkData.Dimse.VR GetAttributeVr(int index)
		{
			return(this.dvtkDataAttributeSet[index].ValueRepresentation);
		}



		public Values GetAttributeValues(int index)
		{
			Values theValues = null;

			Attribute attribute = GetAttribute(index);

			if (attribute == null)
			{
				// InterfaceLogging.WriteError("Attribute does not exist.");
				// Temporary.
				theValues = new InvalidValues("0x00000000");
			}
			else
			{
				if (attribute is SimpleAttribute)
				{
					SimpleAttribute simpleAttribute = attribute as SimpleAttribute;

					theValues = simpleAttribute.GetValues();
				}
				else if (attribute is PixelDataAttribute)
				{
					// InterfaceLogging.WriteWarning("Getting values not implemented for pixel data attributes in the High Level Interface./r/nReturning single empty string");
					// Temporary.
					theValues = new ValidValues("0x00000000", "");
				}
				else if (attribute is SequenceAttribute)
				{
					// InterfaceLogging.WriteError("Attribute with tag sequence " + tagSequence + " is a sequence attribute.");
					// Temporary.
					theValues = new InvalidValues("0x00000000");
				}
				else if (attribute is UnknownAttribute)
				{
					// InterfaceLogging.WriteWarning("Getting values not implemented for attributes with VR Unknown in the High Level Interface./r/nReturning single empty string");
					// Temporary.
					theValues = new ValidValues("0x00000000", "");					
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Not expecting this type of attribute in the code");
				}
			}

			// End Interface logging.
			// InterfaceLogging.End(theValues);

			return theValues;
		}



		/// <summary>
		/// Get the values for an attribute.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (one or more tags seperated with an '/').</param>
		/// <returns>The values.</returns>
		public Values GetAttributeValues(String tagSequence)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, tagSequence);

			Values theValues = null;

			Attribute attribute = GetAttribute(tagSequence);

			if (attribute == null)
			{
				// InterfaceLogging.WriteError("Attribute does not exist.");
				theValues = new InvalidValues(tagSequence);
			}
			else
			{
				if (attribute is SimpleAttribute)
				{
					SimpleAttribute simpleAttribute = attribute as SimpleAttribute;

					theValues = simpleAttribute.GetValues();
				}
				else if (attribute is PixelDataAttribute)
				{
					// InterfaceLogging.WriteWarning("Getting values not implemented for pixel data attributes in the High Level Interface./r/nReturning single empty string");
					theValues = new ValidValues(tagSequence, "");
				}
				else if (attribute is SequenceAttribute)
				{
					// InterfaceLogging.WriteError("Attribute with tag sequence " + tagSequence + " is a sequence attribute.");
					theValues = new InvalidValues(tagSequence);
				}
				else if (attribute is UnknownAttribute)
				{
					// InterfaceLogging.WriteWarning("Getting values not implemented for attributes with VR Unknown in the High Level Interface./r/nReturning single empty string");
					theValues = new ValidValues(tagSequence, "");					
				}
				else
				{
					DvtkHighLevelInterfaceException.Throw("Not expecting this type of attribute in the code");
				}
			}

			// End Interface logging.
			// InterfaceLogging.End(theValues);

			return theValues;
		}


/*
		/// <summary>
		/// Get the number of items in a sequence attribute.
		/// </summary>
		/// <param name="tagSequence"></param>
		/// <returns>
		/// The number of sequence items.
		/// 
		/// If the attribute does not exist or is not a sequence, 0 is returned.
		/// </returns>
		public int GetSequenceItemCount(String tagSequence)
		{
			int sequenceItemCount = 0;

			Attribute attribute = GetAttribute(tagSequence);

			if (attribute == null)
			{
				// InterfaceLogging.WriteError(this, "Attribute does not exist. Returning 0.", tagSequence);
				sequenceItemCount = 0;
			}
			else
			{
				if (attribute is SequenceAttribute)
				{
					SequenceAttribute sequenceAttribute = attribute as SequenceAttribute;

					sequenceItemCount = sequenceAttribute.ItemCount;
				}
				else
				{
					// InterfaceLogging.WriteError(this, "Attribute is not a sequence", tagSequence);
					sequenceItemCount = 0;
				}
			}

			return(sequenceItemCount);
		}
*/

		/*
		// First index determines sequence attribute: zero bas, second index determines : one based.
		public SequenceItem GetSequenceItem(int index1, int index2)
		{
			SequenceItem sequenceItem = null;

			Attribute attribute = GetAttribute(index1);

			if (attribute == null)
			{
				// InterfaceLogging.WriteError(this, "Attribute does not exist. Returning 0.", tagSequence);
				sequenceItem = null;
			}
			else
			{
				if (attribute is SequenceAttribute)
				{
					SequenceAttribute sequenceAttribute = attribute as SequenceAttribute;

					sequenceItem = 
				}
				else
				{
					// InterfaceLogging.WriteError(this, "Attribute is not a sequence", tagSequence);
					sequenceItem = null;
				}
			}

			return(sequenceItem);
		}
		*/

		public int GetSequenceItemCount(int index)
		{
			int sequenceItemCount = 0;

			Attribute attribute = GetAttribute(index);

			if (attribute == null)
			{
				// InterfaceLogging.WriteError(this, "Attribute does not exist. Returning 0.", tagSequence);
				sequenceItemCount = 0;
			}
			else
			{
				if (attribute is SequenceAttribute)
				{
					SequenceAttribute sequenceAttribute = attribute as SequenceAttribute;

					sequenceItemCount = sequenceAttribute.ItemCount;
				}
				else
				{
					// InterfaceLogging.WriteError(this, "Attribute is not a sequence", tagSequence);
					sequenceItemCount = 0;
				}
			}

			return(sequenceItemCount);
		}



		/// <summary>
		/// Set the value of attributes.
		/// </summary>
		/// <param name="parameters"></param>
		public void Set(params Object[] parameters)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, parameters);

			SetMethod.Set(this, true, true, parameters);

			// End Interface logging.
			// InterfaceLogging.End();

		}

		internal void Set(Attribute attribute)
		{
			DvtkData.Dimse.Attribute dvtkDataAttribute = null;

			// First remove the (if exisiting) attribute(s).
			do
			{
				dvtkDataAttribute = this.dvtkDataAttributeSet.GetAttribute(attribute.TagAsUInt32);

				if (dvtkDataAttribute != null)
				{
					this.dvtkDataAttributeSet.Remove(dvtkDataAttribute);
				}
			}
			while (dvtkDataAttribute != null);

			this.dvtkDataAttributeSet.Add(attribute.DvtkDataAttribute);
		}

		public void DeleteAttribute(String tagSequence)
		{
			AttributeSet attributeSet = GetAttributeSet(tagSequence);


		}

		public override String ToString()
		{
			String returnValue = String.Empty;

			try
			{

				for (int index = 0; index < Count; index++)
				{
					String tagAsHexString = Convert.ToString(GetAttributeTagAsUInt32(index), 16);
					returnValue+= tagAsHexString + " - ";
					returnValue+= GetAttributeVr(index).ToString() + " - ";
					returnValue+= GetAttributeValues(index).ToString() + " - ";
					returnValue+= GetAttributeName(index).ToString() + "\r\n";
				}
			}
			catch
			{

			}

			return(returnValue);
		}
	}
}
