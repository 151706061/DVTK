using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;

namespace DvtkHighLevelInterface
{
	/// <summary>
	/// Summary description for Validator.
	/// </summary>
	public class Validator
	{
		private Thread thread = null;

		public Validator(Thread thread)
		{
			this.thread = thread;
		}

		public void Validate(DicomThread dicomThread, DicomProtocolMessage dicomProtocolMessage)
		{
			if (dicomProtocolMessage is DulMessage)
			{
				Validate(dicomThread, dicomProtocolMessage as DulMessage);
			}
			else
			{
				Validate(dicomThread, dicomProtocolMessage as DicomMessage);
			}
		}

		/// <summary>
		/// Validate the Dicom Message by inspecting the VR's.
		/// </summary>
		/// <param name="dicomMessage">The Dicom Message.</param>

		public void Validate(DicomThread dicomThread, DicomMessage dicomMessage)
		{
			this.thread.WriteInformationInternal("Validate Dicom message...");

			Dvtk.Sessions.ValidationControlFlags validationFlags = Dvtk.Sessions.ValidationControlFlags.None;

			// Determine if the VR's need to be validated.
			validationFlags |= ValidateValueRepresentations(dicomMessage);

			// Determine if the DicomMessage can or needs to be validated against a definition file.
			validationFlags |= ValidateUseDefinitions(dicomThread, dicomMessage);
	
			// If validation is needed, do it now.
			if (validationFlags != Dvtk.Sessions.ValidationControlFlags.None)
			{
				dicomThread.DvtkScriptSession.Validate(dicomMessage.DvtkDataDicomMessage, null, validationFlags);
			}
			else
			{
				dicomThread.WriteInformationInternal("No validation performed.");
			}
		}

		public void Validate(DicomThread dicomThread, DicomMessage dicomMessage1, DicomMessage dicomMessage2)
		{
			this.thread.WriteInformationInternal("Validate Dicom message...");

			Dvtk.Sessions.ValidationControlFlags validationFlags = Dvtk.Sessions.ValidationControlFlags.None;

			// Determine if the VR's need to be validated.
			validationFlags |= ValidateValueRepresentations(dicomMessage1);

			// Determine if the DicomMessage can or needs to be validated against a definition file.
			validationFlags |= ValidateUseDefinitions(dicomThread, dicomMessage1);

			this.thread.WriteInformationInternal("- Using reference Dicom message.");
			dicomThread.DvtkScriptSession.Validate(dicomMessage1.DvtkDataDicomMessage, dicomMessage2.DvtkDataDicomMessage, validationFlags | Dvtk.Sessions.ValidationControlFlags.UseReferences);
		}

		/// <summary>
		/// Validate the Dicom Message using a reference Dicom message.
		/// </summary>
		/// <param name="dicomMessage1">The Dicom message.</param>
		/// <param name="dicomMessage2">The reference Dicom message.</param>
		/// 

		public void Validate(DicomThread dicomThread, DicomMessage dicomMessage1, DicomMessage dicomMessage2, String iodName)
		{
			this.thread.WriteInformationInternal("Validate Dicom message...");

			Dvtk.Sessions.ValidationControlFlags validationFlags = Dvtk.Sessions.ValidationControlFlags.None;

			// Determine if the VR's need to be validated.
			validationFlags |= ValidateValueRepresentations(dicomMessage1);

			// Determine if the DicomMessage can or needs to be validated against a definition file.
			if (iodName.Length > 0)
			{
				validationFlags |= ValidateUseDefinitions(dicomMessage1, iodName);
			}
	
			this.thread.WriteInformationInternal("- Using reference Dicom message.");
			dicomThread.DvtkScriptSession.Validate(dicomMessage1.DvtkDataDicomMessage, dicomMessage2.DvtkDataDicomMessage, validationFlags | Dvtk.Sessions.ValidationControlFlags.UseReferences);
		}

		/// <summary>
		/// Validate the Dicom Message using a definition file.
		/// </summary>
		/// <param name="dicomMessage">The Dicom Message.</param>
		/// <param name="iodId">The IOD ID to use, implicitly determining which definition file to use.</param>
		/// 

		public void Validate(DicomThread dicomThread, DicomMessage dicomMessage, String iodName)
		{
			this.thread.WriteInformationInternal("Validate Dicom message...");

			Dvtk.Sessions.ValidationControlFlags validationFlags = Dvtk.Sessions.ValidationControlFlags.None;

			// Determine if the VR's need to be validated.
			validationFlags |= ValidateValueRepresentations(dicomMessage);

			// Determine if the DicomMessage can or needs to be validated against a definition file.
			if (iodName.Length > 0)
			{
				validationFlags |= ValidateUseDefinitions(dicomMessage, iodName);
			}
	
			// If validation is needed, do it now.
			if (validationFlags != Dvtk.Sessions.ValidationControlFlags.None)
			{
				dicomThread.DvtkScriptSession.Validate(dicomMessage.DvtkDataDicomMessage, null, validationFlags);
			}
			else
			{
				dicomThread.WriteInformationInternal("No validation performed.");
			}
		}


		/// <summary>
		/// Validate the Dul Message by inspecting the VR's and the definition.
		/// </summary>
		/// <param name="dulMessage">The Dul Message.</param>

		public void Validate(DicomThread dicomThread, DulMessage dulMessage)
		{
			this.thread.WriteInformationInternal("Validate Dul message...");

			if (!dulMessage.AreValueRepresentationsValidated)
			{
				dicomThread.DvtkScriptSession.Validate(dulMessage.DvtkDataDulMessage, null, Dvtk.Sessions.ValidationControlFlags.UseValueRepresentations);
				dulMessage.AreValueRepresentationsValidated = true;
			}
			else
			{
				dicomThread.WriteInformationInternal("VR's of DulMessage will not be validated again.\r\nNo validation performed.");
			}
		}

		private Dvtk.Sessions.ValidationControlFlags ValidateUseDefinitions(DicomThread dicomThread, DicomMessage dicomMessage)
		{
			Dvtk.Sessions.ValidationControlFlags validationFlag = Dvtk.Sessions.ValidationControlFlags.None;

			String iodName = dicomThread.GetIodNameFromDefinition(dicomMessage);

			if (iodName.Length == 0)
			{
				this.thread.WriteWarningInternal("- Skipping definition file validation: unable to find correct definition file for DimseCommand " + dicomMessage.CommandSet.DimseCommand.ToString() + " and SOP Class UID " + dicomMessage.CommandSet.GetSopClassUid() + ".  Are the correct definition files loaded?");
			}
			else
			{
				if (dicomMessage.IodNamesValidatedAgainst.Contains(iodName))
				{
					this.thread.WriteInformationInternal("- Skipping definition file validation: already performed for \"" + iodName + "\".");
				}
				else
				{
					dicomMessage.DataSet.IodId = iodName;
					dicomMessage.IodNamesValidatedAgainst.Add(iodName);
					validationFlag = Dvtk.Sessions.ValidationControlFlags.UseDefinitions;
					this.thread.WriteInformationInternal("- Using \"" + iodName + "\" for definition file validation.");
				}
			}

			return(validationFlag);
		}

		private Dvtk.Sessions.ValidationControlFlags ValidateUseDefinitions(DicomMessage dicomMessage, String iodName)
		{
			Dvtk.Sessions.ValidationControlFlags validationFlag = Dvtk.Sessions.ValidationControlFlags.None;

			if (dicomMessage.IodNamesValidatedAgainst.Contains(iodName))
			{
				this.thread.WriteInformationInternal("- Skipping definition file validation: already performed for \"" + iodName + "\".");
			}
			else
			{
				dicomMessage.DataSet.IodId = iodName;
				dicomMessage.IodNamesValidatedAgainst.Add(iodName);
				validationFlag = Dvtk.Sessions.ValidationControlFlags.UseDefinitions;
				this.thread.WriteInformationInternal("- Using \"" + iodName + "\" for definition file validation.");
			}

			return(validationFlag);
		}

		private Dvtk.Sessions.ValidationControlFlags ValidateValueRepresentations(DicomMessage dicomMessage)
		{
			Dvtk.Sessions.ValidationControlFlags validationFlag = Dvtk.Sessions.ValidationControlFlags.None;

			if (!dicomMessage.AreValueRepresentationsValidated)
			{
				validationFlag = Dvtk.Sessions.ValidationControlFlags.UseValueRepresentations;
				dicomMessage.AreValueRepresentationsValidated = true;
				this.thread.WriteInformationInternal("- Validating VR's");
			}
			else
			{
				this.thread.WriteInformationInternal("- Skipping VR's validation: already performed.");
			}

			return(validationFlag);
		}



















	}
}
