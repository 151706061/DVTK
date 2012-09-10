using System;
using System.Collections;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// The Class represents the values of an attribute.
	/// 
	/// The single values themself are represented as Strings.
	/// The original VR information is lost when using this class.
	/// </summary>
	internal class ValidValues: Values
	{
		//
		// - Fields -
		//

		/// <summary>
		/// The actual values represented as Strings.
		/// </summary>
		ArrayList valuesAsString = new ArrayList();



		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tagSequence">The tag sequence of the attribute the values are retrieved from.</param>
		/// <param name="dvtkDataCollection">The original collection of values.</param>
		internal ValidValues(String tagSequence, IEnumerable dvtkDataCollection): base(tagSequence)
		{
			foreach(Object theObject in dvtkDataCollection)
			{
				valuesAsString.Add(theObject.ToString());
			}
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tagSequence">The tag sequence of the attribute the values are retrieved from.</param>
		/// <param name="theObject">The original value.</param>
		internal ValidValues(String tagSequence, Object theObject): base(tagSequence)
		{
			valuesAsString.Add(theObject.ToString());
		}



		//
		// - Properties -
		//

		/// <summary>
		/// Number of values.
		/// </summary>
		public override int Count
		{
			get
			{
				return(this.valuesAsString.Count);
			}
		}

		/// <summary>
		/// Check if the supplied values are equal to the content of this object.
		/// </summary>
		/// <param name="values">Values to compare with.</param>
		/// <returns>Boolean indicating if the Values are equal.</returns>
		public override bool EqualTo(Values values)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, values);

			bool equalTo = true;

			if (values is InvalidValues)
			{
				InvalidValues invalidValues = values as InvalidValues;
				// InterfaceLogging.WriteError("the values to compare with are from a non-existing attribute with tag sequence" + invalidValues.TagSequence + ".\r\nReturning false.");
				equalTo = false;
			}
			else if (values is ValidValues)
			{
				ValidValues validValues = values as ValidValues;

				if (Count == validValues.Count)
				{
					for (int index = 1; index <= Count; index++)
					{
						if (GetString(index) != validValues.GetString(index))
						{
							equalTo = false;
							break;
						}
					}
				}
				else
				{
					equalTo = false;
				}
			}
			else
			{
				// InterfaceLogging.WriteError("This type of Values descendant object not expected.\r\n Returning false.");
				equalTo = false;
			}

			// End Interface logging.
			// InterfaceLogging.End(equalTo);

			return equalTo;
		}

		/// <summary>
		/// Check if the supplied values are equal to the content of this object.
		/// </summary>
		/// <param name="parameters">
		/// Single values to compare with.
		/// Supplying multiple objects of type Values is not allowed.
		/// </param>
		/// <returns>Boolean indicating if the Values are equal.</returns>
		public override bool EqualTo(params Object[] parameters)
		{
			// Start Interface logging.
			// InterfaceLogging.Start(this, parameters);

			bool equalTo = true;

			// Check for correctness parameters.
			foreach (Object parameter in parameters)
			{
				if (parameter is Values)
				{
					DvtkHighLevelInterfaceException.Throw("Not allowed to supply multiple objects of type Values.");
				}
			}

			if (Count == parameters.Length)
			{
				// Note: the Get method of this object is 1-bases and the []operator of parameters is 0-based.
				for (int index = 1; index <= Count; index++)
				{
					if (GetString(index) != parameters[index-1].ToString())
					{
						equalTo = false;
						break;
					}
				}
			}
			else
			{
				equalTo = false;
			}

			// End Interface logging.
			// InterfaceLogging.End(equalTo);

			return equalTo;
		}

		/// <summary>
		/// Get a single string from this object.
		/// </summary>
		/// <param name="index">1-based index.</param>
		/// <returns>The indicated value as a String.</returns>
		public override String GetString(int index)
		{
			String singleValue = "";

			if ((index < 1) || (index > Count))
			{
				// InterfaceLogging.WriteError("The supplied index is an invalid index for this object containing " + Count.ToString() + " single values.\r\nReturning an empty string."); 
				singleValue = "";
			}
			else
			{
				singleValue = this.valuesAsString[index - 1] as String;
			}
			
			return(singleValue);
		}

		public override String ToString()
		{
			String returnValue = String.Empty;

			foreach(String singleString in this.valuesAsString)
			{
				returnValue+= singleString + ", ";
			}
		
			if (returnValue.Length > 0)
			{
				returnValue = returnValue.Substring(0, returnValue.Length - 2);
			}

			return(returnValue);
		}
	}
}
