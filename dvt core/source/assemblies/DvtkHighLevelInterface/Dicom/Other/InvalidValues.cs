using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// NoAttribute represents the results of getting the Values for a non-exiting attribute.
	/// </summary>
	internal class InvalidValues: Values
	{
		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tagSequence">The tag sequence of the attribute the values are retrieved from.</param>
		internal InvalidValues(String tagSequence): base(tagSequence)
		{
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
				// Logging.
				// InterfaceLogging.Start(this);

				// InterfaceLogging.WriteError("Attribute with tag " + tagSequence + " does not exist.\r\nCount of the values of this attribute does therefore not exist.\r\nReturning 0.");

				// Logging.
				// InterfaceLogging.End(0);

				return 0;
			}
		}



		//
		// - Methods -
		//
	
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
				InvalidValues attributeToCompareWith = values as InvalidValues;
				// InterfaceLogging.WriteError("Comparing the values of two non-existing attributes with tag sequence " + this.TagSequence + " and " + attributeToCompareWith.TagSequence + "\r\nReturning true.");
				equalTo = true;
			}
			else if (values is ValidValues)
			{
				ValidValues validValues = values as ValidValues;
				// InterfaceLogging.WriteError("Comparing the value of a non-existing sttribute with tag " + this.TagSequence + " with valid values from attribute with tag " + validValues.TagSequence + ".\r\nReturning false.");
				equalTo = false;
			}
			else
			{
				// InterfaceLogging.WriteError("This type of Values descendant object not expected. Returning false.");
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

			// InterfaceLogging.WriteError("Comparing the value a non-existing attributes with tag sequence " + this.TagSequence + " to " + parameters.Length.ToString() + " single values.\r\nReturning true.");
			equalTo = false;

			// End Interface logging.
			// InterfaceLogging.End(equalTo);

			return(equalTo);
		}

		/// <summary>
		/// Get a single value from this object.
		/// </summary>
		/// <param name="index">1-based index.</param>
		/// <returns>The single value as a String.</returns>
		public override String GetString(int index)
		{
			// InterfaceLogging.WriteError("Getting a single value from a non-existing sttribute with tag " + this.TagSequence + ".\r\nReturning empty string.");			

			return "";
		}
	}

}
