using System;
using System.Collections;
using System.Diagnostics;


namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for Tag.
	/// </summary>
	internal class Tag
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property Index.
		/// </summary>
		private Int32 index = 0;

		/// <summary>
		/// See property TagAsString.
		/// </summary>
		private String tagAsString = "";

		/// <summary>
		/// See property TagAsUInt32.
		/// </summary>
		private UInt32 tagAsUInt32 = 0;

		/// <summary>
		/// See property TagOnlyAsString.
		/// </summary>
		private String tagWithoutIndex = "";



		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		public Tag()
		{
		}



		//
		// - Properties -
		//

		/// <summary>
		/// Boolean indicating if the tag contains an index.
		/// </summary>
		public bool ContainsIndex
		{
			get
			{
				return (this.index > 0);
			}
		}

		/// <summary>
		/// The index (if existing) of this tag. A value of 0 means no index exists.
		/// </summary>
		public Int32 Index
		{
			get
			{
				return this.index;
			}
			set
			{
				if (value > 0)
				{
					this.index = value;
				}
				else
				{
					Debug.Assert(true, "Index must be greater than 0");
				}
			}
		}

		/// <summary>
		/// The tag as string. If this tag contains an index, this is also included in this string.
		/// </summary>
		public String TagAsString
		{
			get
			{
				return this.tagAsString;
			}
			set
			{
				this.tagAsString = value;
			}
		}


		/// <summary>
		/// The UInt32 representation of the tag.
		/// </summary>
		public UInt32 TagAsUInt32
		{
			get
			{
				return this.tagAsUInt32;
			}
			set
			{
				this.tagAsUInt32 = value;
			}
		}

		/// <summary>
		/// The tag as string, not containing an index.
		/// </summary>
		public String TagWithoutIndex
		{
			get
			{
				return this.tagWithoutIndex;
			}
			set
			{
				this.tagWithoutIndex = value;
			}
		}



		// - Methods -


		static public bool IsCommandSetTag(String tagSequence)
		{			
			Tag[] tags = Tag.InterpretTagSequence(tagSequence);
	
			return (IsCommandSetTag(tags[tags.Length - 1].TagAsUInt32));
		}

		static public bool IsCommandSetTag(UInt32 tagAsUInt32)
		{
			UInt32 groupPart = tagAsUInt32 / 65536;

			if ((groupPart == 0) || (groupPart == 2) || (groupPart == 4) || (groupPart == 6))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		static public UInt32 ConvertToUInt32(String tagAsString)
		{
			return (Convert.ToUInt32(tagAsString, 16));
		}

		static public Tag[] InterpretTagSequence(String tagSequence)
		{
			bool interpreted = true;
			ArrayList tagArrayList = new ArrayList();

			String[] tagsAsString = tagSequence.Split('/');

			foreach (String tagAsString in tagsAsString)
			{
				Tag tag = new Tag();
				tag.tagAsString = tagAsString;

				// Check if the tag starts with "0x".
				if (!tagAsString.StartsWith("0x"))
				{
					// InterfaceLogging.WriteError("Invalid syntax: " + tagAsString + " does not start with \"0x\".");
					interpreted = false;
					break;
				}

				// If this tag contains an index, determine it.
				int indexOfBracket = tagAsString.IndexOf("[");

				if (indexOfBracket != -1)
				{
					try
					{
						tag.tagWithoutIndex = tagAsString.Substring(0, indexOfBracket);

						String indexAsString = tagAsString.Substring(indexOfBracket + 1);

						indexAsString = indexAsString.Substring(0, indexAsString.Length - 1);

						// indexAsString should now contain the actual index number.

						tag.Index = Convert.ToInt32(indexAsString);

						if (tag.Index < 1)
						{
							// InterfaceLogging.WriteError("Invalid syntax: index of " + tagAsString + " must be greater than 0.");
							interpreted = false;
							break;
						}
					}
					catch
					{
						// InterfaceLogging.WriteError("Invalid syntax for tag " + tagAsString + ".");
						interpreted = false;
						break;
					}
				}
				else
				{
					tag.tagWithoutIndex = tagAsString;
				}

				try
				{
					tag.tagAsUInt32 = Convert.ToUInt32(tag.tagWithoutIndex, 16);
				}
				catch
				{
					// InterfaceLogging.WriteError("Invalid syntax for tag " + tagAsString + ".");
					interpreted = false;
					break;
				}


				tagArrayList.Add(tag);
			}

			if (interpreted)
			{
				// Final checks:
				// - At leats one tag must be present.
				// - Last tag may not contain an index and.
				// - All tags besides the last one must contain an index.
				if (tagArrayList.Count == 0)
				{
					// InterfaceLogging.WriteError("Tag sequence must contain at least one tag.");
					interpreted = false;
				}
				else
				{
					if ( ((tagArrayList[tagArrayList.Count - 1]) as Tag).ContainsIndex )
					{
						// InterfaceLogging.WriteError("Last tag of tag sequence may not contain an index.");
						interpreted = false;					
					}

					for (int tagIndex = 0; tagIndex < tagArrayList.Count - 1; tagIndex++)
					{
						if (!((tagArrayList[tagIndex]) as Tag).ContainsIndex)
						{
							// InterfaceLogging.WriteError("All tags besides the last one in tag sequence must contain an index.");
							interpreted = false;					
							break;
						}
					}
				}
			}

			if (!interpreted)
			{
				DvtkHighLevelInterfaceException.Throw("Invalid Syntax for tag sequence.");
			}

			return(tagArrayList.ToArray(typeof(Tag)) as Tag[]);
		}
	}
}
