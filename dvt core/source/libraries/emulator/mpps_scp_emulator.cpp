//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Mpps SCP emulator class.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "mpps_scp_emulator.h"
#include "Idefinition.h"			// Definition component interface
#include "Imedia.h"					// Media component interface
#include "Isession.h"				// Session component interface
#include "Ivalidation.h"			// Validation component interface


//>>===========================================================================

MPPS_SCP_EMULATOR_CLASS::MPPS_SCP_EMULATOR_CLASS(EMULATOR_SESSION_CLASS *session_ptr, BASE_SOCKET_CLASS* socket_ptr, bool logEmulation)

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	//
	// constructor activities
	// - setup the logging, etc
	//
	setup(session_ptr, socket_ptr, logEmulation);
}

//>>===========================================================================

MPPS_SCP_EMULATOR_CLASS::~MPPS_SCP_EMULATOR_CLASS()

//  DESCRIPTION     : Class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	//
	// destructor activities
	// - cleanup the logging, etc
	teardown();
}

//>>===========================================================================

bool MPPS_SCP_EMULATOR_CLASS::addSupportedPresentationContexts()

//  DESCRIPTION     : Add the supported Storage presentation contexts.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	AE_SESSION_CLASS ae_session;
	
	// set ae session
	ae_session.SetName(sessionM_ptr->getApplicationEntityName());
	ae_session.SetVersion(sessionM_ptr->getApplicationEntityVersion());

	// use loaded definition files
	for (UINT i = 0; i < sessionM_ptr->noDefinitionFiles(); i++) 
	{
		DEFINITION_FILE_CLASS *definitionFile_ptr = sessionM_ptr->getDefinitionFile(i);

		// use the sop class uid
		DEF_DETAILS_CLASS file_details;
		if (definitionFile_ptr->GetDetails(file_details))
		{
			string sopClassUid = file_details.GetSOPClassUID();

			// check if this is a mpps sop class
			if (DEFINITION->IsMppsSop(sopClassUid, &ae_session))
			{
				// add support for this sop class
				for (int i = 0 ; i < sessionM_ptr->noSupportedTransferSyntaxes(); i++)
				{
					// add this sop class to the presentation contexts
					associationM.setSupportedPresentationContext((char*) sopClassUid.c_str(), (char*) sessionM_ptr->getSupportedTransferSyntax(i));
				}
			}
		}
	}

	// return result
	return true;
}

//>>===========================================================================

bool MPPS_SCP_EMULATOR_CLASS::processCommandDataset(DCM_COMMAND_CLASS *command_ptr, DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the Storage command and dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result;

	// handle individual commands
	switch(command_ptr->getCommandId())
	{
	case DIMSE_CMD_NCREATE_RQ:
		if (dataset_ptr)
		{
			// process the CREATE command
			result = processCreate(dataset_ptr);
		}
		else
		{
			// missing dataset
			UINT16 status = DCM_STATUS_PROCESSING_FAILURE;
			result = sendResponse(DIMSE_CMD_NCREATE_RSP, status);
		}
		break;

	case DIMSE_CMD_NSET_RQ:
		if (dataset_ptr)
		{
			// process the SET command
			result = processSet(dataset_ptr);
		}
		else
		{
			// missing dataset
			UINT16 status = DCM_STATUS_PROCESSING_FAILURE;
			result = sendResponse(DIMSE_CMD_NSET_RSP, status);
		}
		break;

	default:
		{
			// unknown command
			UINT16 status = DCM_STATUS_UNRECOGNIZED_OPERATION;
			result = sendResponse(command_ptr->getCommandId(), status);
		}
		break;
	}

	// return result
	return result;
}

//>>===========================================================================

bool MPPS_SCP_EMULATOR_CLASS::processCreate(DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the N-CREATE-RQ with dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	UINT16 status = DCM_STATUS_PROCESSING_FAILURE;

	// set status successful
	status = DCM_STATUS_SUCCESS;

	// return the N-CREATE-RSP
	return sendResponse(DIMSE_CMD_NCREATE_RSP, status);
}

//>>===========================================================================

bool MPPS_SCP_EMULATOR_CLASS::processSet(DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the N-SET-RQ with dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	UINT16 status = DCM_STATUS_PROCESSING_FAILURE;

	// set status successful
	status = DCM_STATUS_SUCCESS;

	// return the N-SET-RSP
	return sendResponse(DIMSE_CMD_NSET_RSP, status);
}

//>>===========================================================================

void MPPS_SCP_EMULATOR_CLASS::completeLogging()

//  DESCRIPTION     : Complete the emulation logging.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
}

