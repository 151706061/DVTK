//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Query/Retrieve SCP emulator class.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "qr_scp_emulator.h"
#include "Idefinition.h"			// Definition component interface
#include "Imedia.h"					// Media component interface
#include "Isession.h"				// Session component interface
#include "Ivalidation.h"			// Validation component interface


//>>===========================================================================

QUERY_RETRIEVE_SCP_EMULATOR_CLASS::QUERY_RETRIEVE_SCP_EMULATOR_CLASS(EMULATOR_SESSION_CLASS *session_ptr, BASE_SOCKET_CLASS* socket_ptr, bool logEmulation)

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

QUERY_RETRIEVE_SCP_EMULATOR_CLASS::~QUERY_RETRIEVE_SCP_EMULATOR_CLASS()

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

bool QUERY_RETRIEVE_SCP_EMULATOR_CLASS::addSupportedPresentationContexts()

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

			// check if this is a query/retrieve sop class
			if (DEFINITION->IsQueryRetrieveSop(sopClassUid, &ae_session))
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

bool QUERY_RETRIEVE_SCP_EMULATOR_CLASS::processCommandDataset(DCM_COMMAND_CLASS *command_ptr, DCM_DATASET_CLASS *dataset_ptr)

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
	case DIMSE_CMD_CFIND_RQ:
		if (dataset_ptr)
		{
			// process the FIND command
			result = processFind(dataset_ptr);
		}
		else
		{
			// missing dataset
			UINT16 status = DCM_STATUS_PROCESSING_FAILURE;
			result = sendResponse(DIMSE_CMD_CFIND_RSP, status);
		}
		break;

	case DIMSE_CMD_CMOVE_RQ:
		if (dataset_ptr)
		{
			// process the MOVE command
			result = processMove(dataset_ptr);
		}
		else
		{
			// missing dataset
			UINT16 status = DCM_STATUS_PROCESSING_FAILURE;
			result = sendResponse(DIMSE_CMD_CMOVE_RSP, status);
		}
		break;

	case DIMSE_CMD_CGET_RQ:
		if (dataset_ptr)
		{
			// process the GET command
			result = processGet(dataset_ptr);
		}
		else
		{
			// missing dataset
			UINT16 status = DCM_STATUS_PROCESSING_FAILURE;
			result = sendResponse(DIMSE_CMD_CGET_RSP, status);
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

bool QUERY_RETRIEVE_SCP_EMULATOR_CLASS::processFind(DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the C-FIND-RQ with dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	UINT16 status = DCM_STATUS_PROCESSING_FAILURE;

	// set status successful
	status = DCM_STATUS_SUCCESS;

	// return the C-FIND-RSP
	return sendResponse(DIMSE_CMD_CFIND_RSP, status);
}

//>>===========================================================================

bool QUERY_RETRIEVE_SCP_EMULATOR_CLASS::processMove(DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the C-MOVE-RQ with dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	UINT16 status = DCM_STATUS_PROCESSING_FAILURE;

	// set status successful
	status = DCM_STATUS_SUCCESS;

	// return the C-MOVE-RSP
	return sendResponse(DIMSE_CMD_CMOVE_RSP, status);
}

//>>===========================================================================

bool QUERY_RETRIEVE_SCP_EMULATOR_CLASS::processGet(DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the C-GET-RQ with dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	UINT16 status = DCM_STATUS_PROCESSING_FAILURE;

	// set status successful
	status = DCM_STATUS_SUCCESS;

	// return the C-GET-RSP
	return sendResponse(DIMSE_CMD_CGET_RSP, status);
}

//>>===========================================================================

void QUERY_RETRIEVE_SCP_EMULATOR_CLASS::completeLogging()

//  DESCRIPTION     : Complete the emulation logging.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
}

