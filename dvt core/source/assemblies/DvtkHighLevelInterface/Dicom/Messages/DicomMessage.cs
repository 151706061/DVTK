using System;
using System.Collections;



namespace DvtkHighLevelInterface.Messages
{
	/// <summary>
	/// An object of this class represents a Dicom Message.
	/// </summary>
	public class DicomMessage: DicomProtocolMessage
	{
		//
		// - Fields -
		//

		/// <summary>
		/// See property CommandSet.
		/// </summary>
		private CommandSet commandSet = null;

		/// <summary>
		/// See property DataSet.
		/// </summary>
		private DataSet dataSet = null;

		/// <summary>
		/// See property IodNamesValidatedAgainst.
		/// </summary>
		private ArrayList iodNamesValidatedAgainst = new ArrayList();



		//
		// - Constructors -
		//

		/// <summary>
		/// Constructor.
		/// 
		/// Use this constructor to create a DicomMessage with an empty command set
		/// and empty data set.
		/// </summary>
		/// <param name="dimseCommand">The Dimse command.</param>
		public DicomMessage(DvtkData.Dimse.DimseCommand dimseCommand)
		{		
			this.commandSet = new CommandSet(dimseCommand);
			this.dataSet = new DataSet();
		}
		
		/// <summary>
		/// Constructor.
		/// 
		/// Use this constructor to construct the command set and data set based on the 
		/// command set and data set contained in the encapsulated DvtkData DicomMessage.
		/// </summary>
		/// <param name="dvtkDataDicomMessage">The encapsulated DvtkData DicomMessage.</param>
		internal DicomMessage(DvtkData.Dimse.DicomMessage dvtkDataDicomMessage)
		{
			// Sanity check.
			if (dvtkDataDicomMessage == null)
			{
				DvtkHighLevelInterfaceException.Throw("Parameter may not be null.");
			}

			// Create the CommandSet object.
			this.commandSet = new CommandSet(dvtkDataDicomMessage.CommandSet);

			// Create the DataSet object.
			if (dvtkDataDicomMessage.DataSet == null)
			{
				this.dataSet = new DataSet();
			}
			else
			{
				this.dataSet = new DataSet(dvtkDataDicomMessage.DataSet);
			}
		}



		//
		// - Properties -
		//

		/// <summary>
		/// The CommandSet of this DicomMessage.
		/// </summary>
		public CommandSet CommandSet
		{
			get
			{
				return commandSet;
			}
		}

		/// <summary>
		/// The DataSet of this DicomMessage.
		/// </summary>
		public DataSet DataSet
		{
			get
			{
				return dataSet;
			}
		}

		/// <summary>
		/// The encapsulated DvtkData DicomMessage.
		/// 
		/// This DvtkData DicomMessage is not stored inside this class but is reconstructed from
		/// the encapsulated DvtkData CommandSet and encapsulated DvtkData DataSet.
		/// </summary>
		internal DvtkData.Dimse.DicomMessage DvtkDataDicomMessage
		{
			get
			{
				DvtkData.Dimse.DicomMessage dvtkDataDicomMessage = new DvtkData.Dimse.DicomMessage();

				if (this.dataSet.DvtkDataDataSet.Count > 0)
				{
					dvtkDataDicomMessage.Apply(this.commandSet.DvtkDataCommandSet, this.dataSet.DvtkDataDataSet);
				}
				else
				{
					dvtkDataDicomMessage.Apply(this.commandSet.DvtkDataCommandSet);
				}

				return dvtkDataDicomMessage;
			}
		}

		/// <summary>
		/// The IOD names already used to validate against definition files.
		/// </summary>
		internal ArrayList IodNamesValidatedAgainst
		{
			get
			{
				return(this.iodNamesValidatedAgainst);
			}
		}



		//
		// - Methods -
		//

		/// <summary>
		/// Indicates if the specified attribute exists.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (see class TagSequence for the format).</param>
		/// <returns>Boolean indicating if the specified attribute exists.</returns>
		public bool AttributeExists(String tagSequence)
		{
			bool attributeExists = true;
			
			if (Tag.IsCommandSetTag(tagSequence))
			{
				attributeExists = this.CommandSet.AttributeExists(tagSequence);
			}
			else
			{
				attributeExists = this.DataSet.AttributeExists(tagSequence);
			}

			return(attributeExists);
		}

		/// <summary>
		/// Display all attributes on the console.
		/// </summary>
		public void ConsoleDisplay()
		{
			if (CommandSet != null)
			{
				Console.WriteLine("Command:");
				CommandSet.DvtkDataCommandSet.ConsoleDisplay();
			}
			if (DataSet != null)
			{
				Console.WriteLine("Dataset:");
				DataSet.DvtkDataDataSet.ConsoleDisplay();
			}
		}

		/// <summary>
		/// Get the values for the specified attribute.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (see class TagSequence for the format).</param>
		/// <returns>The values.</returns>
		public Values GetAttributeValues(String tagSequence)
		{
			Values values = null;

			if (Tag.IsCommandSetTag(tagSequence))
			{
				values = this.CommandSet.GetAttributeValues(tagSequence);
			}
			else
			{
				values = this.DataSet.GetAttributeValues(tagSequence);
			}

			return(values);
		}

		/// <summary>
		/// Get the VM for the specified attribute.
		/// </summary>
		/// <param name="tagSequence">The tag sequence (see class TagSequence for the format).</param>
		/// <returns>The VM.</returns>
		public int GetAttributeVm(String tagSequence)
		{
			int vM = 0;

			if (Tag.IsCommandSetTag(tagSequence))
			{
				vM = this.CommandSet.GetAttributeVm(tagSequence);
			}
			else
			{
				vM = this.DataSet.GetAttributeVm(tagSequence);
			}

			return vM;
		}

		/// <summary>
		/// Set one or more attributes.
		/// </summary>
		/// <param name="parameters">
		/// Format of this method:
		/// Tag1, VR1, zero or more values,
		/// Tag2, VR2, zero or more values,
		/// ...
		/// Tagn, VRn, zero or more values
		/// 
		/// Example:
		/// dicomMessageToSend.Set
        /// ( 
        ///		"0x00210006", SS,
        ///		"0x00210008", SS, 1,
        ///		"0x00210010", SS, 1, 2
        /// ) 
		/// </param>
		public void Set(params Object[] parameters)
		{
			SetMethod.Set(this.commandSet, true, false, parameters);
			SetMethod.Set(this.dataSet, false, true, parameters);
		}
	}
}
