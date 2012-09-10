using System;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Represents a command set.
	/// </summary>
	public class CommandSet: AttributeSet
	{
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="dimseCommand">The dimse command.</param>
		internal CommandSet(DvtkData.Dimse.DimseCommand dimseCommand): base(new DvtkData.Dimse.CommandSet(dimseCommand))
		{
		}

		internal CommandSet(DvtkData.Dimse.CommandSet dvtkDataCommandSet): base(dvtkDataCommandSet)
		{
			if (dvtkDataCommandSet == null)
			{
				DvtkHighLevelInterfaceException.Throw("Parameter may not be null/Nothing.");
			}
		}

		/// <summary>
		/// DimseCommand property.
		/// </summary>
		public DvtkData.Dimse.DimseCommand DimseCommand
		{
			get
			{
				return DvtkDataCommandSet.CommandField;
			}
		}

		internal DvtkData.Dimse.CommandSet DvtkDataCommandSet
		{
			get
			{
				return this.dvtkDataAttributeSet as DvtkData.Dimse.CommandSet;
			}
		}

		// returns empty string if not found.
		public String GetSopClassUid()
		{
			String sopClassUid = String.Empty;

			// Try to get the SOP Class UID
			DvtkData.Dimse.Attribute sopClassUidAttribute = this.DvtkDataCommandSet.GetAttribute(DvtkData.Dimse.Tag.AFFECTED_SOP_CLASS_UID);

			if (sopClassUidAttribute == null)
			{
				sopClassUidAttribute = this.DvtkDataCommandSet.GetAttribute(DvtkData.Dimse.Tag.REQUESTED_SOP_CLASS_UID);
			}
			if (sopClassUidAttribute != null)
			{
				// Get the value of the SOP Class UID
				DvtkData.Dimse.UniqueIdentifier uniqueIdentifier = (DvtkData.Dimse.UniqueIdentifier)sopClassUidAttribute.DicomValue;
				sopClassUid = uniqueIdentifier.Values[0];
			}

			return(sopClassUid);
		}

	}
}
