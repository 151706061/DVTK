using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Abstract class representing attribute values.
	/// </summary>
	abstract public class Values
	{
		/// <summary>
		/// See property TagSequence.
		/// </summary>
		protected String tagSequence = "";
		
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="tagSequence">The tag sequence of the attribute the values are retrieved from.</param>
		internal Values(String tagSequence)
		{
			this.tagSequence = tagSequence;
		}

		/// <summary>
		/// Number of values.
		/// </summary>
		public abstract int Count
		{
			get;
		}
	
		/// <summary>
		/// Check if the supplied values are equal to the content of this object.
		/// </summary>
		/// <param name="values">Values to compare with.</param>
		/// <returns>Boolean indicating if the Values are equal.</returns>
		public abstract bool EqualTo(Values values);

		/// <summary>
		/// Check if the supplied values are equal to the content of this object.
		/// </summary>
		/// <param name="parameters">
		/// Single values to compare with.
		/// Supplying multiple objects of type Values is not allowed.
		/// </param>
		/// <returns>Boolean indicating if the Values are equal.</returns>
		public abstract bool EqualTo(params Object[] parameters);

		/// <summary>
		/// Get a single value from this object.
		/// </summary>
		/// <param name="index">1-based index.</param>
		/// <returns>The single value as a String.</returns>
		public abstract String GetString(int index);

		/// <summary>
		/// The tag sequence of the attribute these values are retrieved from.
		/// </summary>
		public String TagSequence
		{
			get
			{
				return(this.tagSequence);
			}
		}
	}
}
