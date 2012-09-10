using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// The options for the abstract Thread class.
	/// </summary>
	public class ThreadOptions
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property AttachChildsToUserInterfaces.
		/// </summary>
		private bool attachChildsToUserInterfaces = true;

		/// <summary>
		/// See property Identifier.
		/// </summary>
		private String identifier = null;

		/// <summary>
		/// See property Name.
		/// </summary>
		private String name = null;

//		/// <summary>
//		/// See property ResultsDirectory.
//		/// </summary>
//		private String resultsDirectory = null;



		//
		// - Properties -
		//

		public bool AttachChildsToUserInterfaces
		{
			get
			{
				return(this.attachChildsToUserInterfaces);
			}
			set
			{
				this.attachChildsToUserInterfaces = value;
			}
		}


		/// <summary>
		/// The unique identifier of this object.
		/// It is used to set the name of the underlying .Net thread.
		/// When displaying activity logging (if enabled) for this thread in a dialog,
		/// the Identifier property is also used to uniquely identify logging from this
		/// thread. Besides this, in a DicomThread, the Identifier may also be used
		/// to create a unique results file name.
		/// 
		/// If this property is set, the calling code has to make sure that this Identifier
		/// is unique.
		/// If not set, the Name property is appended with a number to create a unique identifier.
		/// </summary>
		public String Identifier
		{
			get
			{
				return(this.identifier);
			}
			set
			{
				this.identifier = value;
			}
		}

		/// <summary>
		/// The (possible not unique) name of this object.
		/// If this property is not set, the Class name will be used as the name.
		/// 
		/// See also the property Identifier for the usage of this property.
		/// </summary>
		public String Name
		{
			get
			{
				return(this.name);
			}
			set
			{
				this.name = value;
			}
		}
	}
}
