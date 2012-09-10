// Part of DvtkIheActors.dll - .NET class library providing basic data-classes for DVTK
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using DvtkHighLevelInterface;
using DvtkHighLevelInterface.Messages;
using DvtkHighLevelInterface.InformationEntity;
using Dvtk.DvtkDicomEmulators.WorklistClientServers;
using Dvtk.DvtkDicomEmulators.WorklistMessageHandlers;

namespace Dvtk.IheActors.Dicom
{
	/// <summary>
	/// Summary description for DicomWorklistServer.
	/// </summary>
	public class DicomWorklistServer : DicomServer
	{
		public DicomWorklistServer(BaseActor parentActor, ActorNameEnum actorName) : base (parentActor, actorName) {}

		public override void ApplyConfig(DicomConfig config)
		{
			// set up the Modality Worklist information models
			ModalityWorklistInformationModel modalityWorklistInformationModel = new ModalityWorklistInformationModel();

			// load the information models
			modalityWorklistInformationModel.LoadInformationModel(config.DataDirectory);

			// add any default attribute values to the information models
			modalityWorklistInformationModel.AddDefaultAttributeToInformationModel("0x00400001", DvtkData.Dimse.VR.AE, config.SutAeTitle);
			modalityWorklistInformationModel.AddDefaultAttributeToInformationModel("0x00400002", DvtkData.Dimse.VR.DA, System.DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture));
			modalityWorklistInformationModel.AddDefaultAttributeToInformationModel("0x00400002", DvtkData.Dimse.VR.TM, System.DateTime.Now.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture));

			// add any additional attribute values to the information models
			//			modalityWorklistInformationModel.AddAdditionalAttributeToInformationModel("0x00080054", DvtkData.Dimse.VR.AE, config.DvtAeTitle);


			// set up the worklist SCP
			WorklistScp worklistScp = new WorklistScp();
			Scp = worklistScp;

			// apply the remaining configuration
			base.ApplyConfig(config);

			// add the default message handlers with the information model
			worklistScp.AddDefaultMessageHandlers(modalityWorklistInformationModel);
		}

		public override void AssociationReleasedEventHandler(DicomThread worklistScp)
		{
			// do SCP specific post association processing here

			// call base implementation to generate event, update transaction log and cleanup data warehouse
			base.AssociationReleasedEventHandler(worklistScp);
		}
	}
}
