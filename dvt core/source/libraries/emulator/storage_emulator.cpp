//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Storage SCP emulator class.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "storage_emulator.h"
#include "storage_scu_emulator.h"
#include "commitment.h"
#include "Idefinition.h"			// Definition component interface
#include "Imedia.h"					// Media component interface
#include "Isession.h"				// Session component interface
#include "Ivalidation.h"			// Validation component interface


//>>===========================================================================

STORAGE_SCP_EMULATOR_CLASS::STORAGE_SCP_EMULATOR_CLASS(EMULATOR_SESSION_CLASS *session_ptr, BASE_SOCKET_CLASS* socket_ptr, bool logEmulation)

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
	storageCommitmentM_ptr = NULL;
}

//>>===========================================================================

STORAGE_SCP_EMULATOR_CLASS::~STORAGE_SCP_EMULATOR_CLASS()

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

bool STORAGE_SCP_EMULATOR_CLASS::addSupportedPresentationContexts()

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

			// check if this is a storage sop class
			// - or the Storage Commitment Push Model SOP Class
			if ((DEFINITION->IsStorageSop(sopClassUid, &ae_session)) ||
				(sopClassUid == STORAGE_COMMITMENT_PUSH_MODEL_SOP_CLASS_UID))
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

bool STORAGE_SCP_EMULATOR_CLASS::processCommandDataset(DCM_COMMAND_CLASS *command_ptr, DCM_DATASET_CLASS *dataset_ptr)

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
	case DIMSE_CMD_CSTORE_RQ:
		if (dataset_ptr)
		{
        	UINT16	cs_group;
	        UINT16	cs_element;
	        UINT16	ds_group;
	        UINT16	ds_element;
	        string	ds_attr_name;
	        string	cs_attr_name;
        	string	uid_ds;
        	string	uid_cs;

            // check if the command and dataset SOP Class UIDs are the same
        	if (dataset_ptr->getUIValue(TAG_SOP_CLASS_UID, uid_ds) == true)
        	{
        		if (command_ptr->getUIValue(TAG_AFFECTED_SOP_CLASS_UID, uid_cs) == true)
        		{
        			if ((loggerM_ptr) && 
						(uid_ds != uid_cs))
        			{
        				cs_group = ((UINT16) (TAG_AFFECTED_SOP_CLASS_UID >> 16));
        				cs_element = ((UINT16) (TAG_AFFECTED_SOP_CLASS_UID & 0x0000FFFF));
        				ds_group = ((UINT16) (TAG_SOP_CLASS_UID >> 16));
        				ds_element = ((UINT16) (TAG_SOP_CLASS_UID & 0x0000FFFF));

        				ds_attr_name = DEFINITION->GetAttributeName (ds_group, ds_element);
        				cs_attr_name = DEFINITION->GetAttributeName (cs_group, cs_element);
        				loggerM_ptr->text(LOG_ERROR, 1,
        								  "\"%s\" (%s) is not equal to \"%s\" (%s)",
        								  ds_attr_name.c_str(),
        								  uid_ds.c_str(),
        								  cs_attr_name.c_str(),
        								  uid_cs.c_str());
        			}
        		}
        	}

            // check if the command and dataset SOP Instance UIDs are the same
        	if (dataset_ptr->getUIValue(TAG_SOP_INSTANCE_UID, uid_ds) == true)
        	{
        		if (command_ptr->getUIValue (TAG_AFFECTED_SOP_INSTANCE_UID, uid_cs) == true)
        		{
        			if ((loggerM_ptr) &&
						(uid_ds != uid_cs))
        			{
        				cs_group = ((UINT16) (TAG_AFFECTED_SOP_INSTANCE_UID >> 16));
        				cs_element = ((UINT16) (TAG_AFFECTED_SOP_INSTANCE_UID & 0x0000FFFF));
        				ds_group = ((UINT16) (TAG_SOP_INSTANCE_UID >> 16));
        				ds_element = ((UINT16) (TAG_SOP_INSTANCE_UID & 0x0000FFFF));

        				ds_attr_name = DEFINITION->GetAttributeName (ds_group, ds_element);
        				cs_attr_name = DEFINITION->GetAttributeName (cs_group, cs_element);
        				loggerM_ptr->text(LOG_ERROR, 1,
        								  "\"%s\" (%s) is not equal to \"%s\" (%s)",
        								  ds_attr_name.c_str(),
        								  uid_ds.c_str(),
        								  cs_attr_name.c_str(),
        								  uid_cs.c_str());
        			}
        		}
        	}

			// process the STORE command
			result = processStore(dataset_ptr);
		}
		else
		{
			// missing dataset
			UINT16 status = DCM_STATUS_PROCESSING_FAILURE;
			result = sendResponse(DIMSE_CMD_CSTORE_RSP, status);
		}
		break;

	case DIMSE_CMD_NACTION_RQ:
		if (dataset_ptr)
		{
			// process the ACTION command
			result = processAction(dataset_ptr);
		}
		else
		{
			// missing dataset
			UINT16 status = DCM_STATUS_PROCESSING_FAILURE;
			result = sendResponse(DIMSE_CMD_NACTION_RSP, status);
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

bool STORAGE_SCP_EMULATOR_CLASS::processStore(DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the C-STORE-RQ with dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	UINT16 status = DCM_STATUS_PROCESSING_FAILURE;

	AE_SESSION_CLASS ae_session;

	// set ae session
	ae_session.SetName(sessionM_ptr->getApplicationEntityName());
	ae_session.SetVersion(sessionM_ptr->getApplicationEntityVersion());

	// ensure a storage object
	if (DEFINITION->IsStorageSop(sopClassUidM, &ae_session))
	{
		// analyse the dataset
		RELATIONSHIP->analyseStorageDataset(dataset_ptr, loggerM_ptr);

		// set status successful
		status = DCM_STATUS_SUCCESS;
	}

	// return the C-STORE-RSP
	return sendResponse(DIMSE_CMD_CSTORE_RSP, status);
}

//>>===========================================================================

bool STORAGE_SCP_EMULATOR_CLASS::processAction(DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Process the N-ACTION-RQ with dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	UINT16 status = DCM_STATUS_PROCESSING_FAILURE;

	// ensure that we have the Storage Commitment SOP Class
	if (sopClassUidM == STORAGE_COMMITMENT_PUSH_MODEL_SOP_CLASS_UID)
	{
		// cleanup any old storage commitment
		if (storageCommitmentM_ptr)
		{
			delete storageCommitmentM_ptr;
		}

		// instantiate the storage commitment
		storageCommitmentM_ptr = new STORAGE_COMMITMENT_CLASS();

		// process the N-ACTION-RQ command
		status = storageCommitmentM_ptr->action(dataset_ptr, loggerM_ptr);
	}

	// return the N-ACTION-RSP
	return sendResponse(DIMSE_CMD_NACTION_RSP, status);
}

//>>===========================================================================

bool STORAGE_SCP_EMULATOR_CLASS::postProcess()

//  DESCRIPTION     : Perform any association release post processing.
//					: For storage do nothing.
//					: For storage commitment send an event report.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = true;

	// for storage commitment - send an event report
	if ((storageCommitmentM_ptr) &&
		(storageCommitmentM_ptr->isEventToSend()))
	{
		// delay before sending the event report
		int delay = 5;

		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_INFO, 2, "Delaying for %d seconds before sending Storage Commitment N-EVENT-REPORT-RQ...", delay);
		}

#ifdef _WINDOWS
		Sleep(delay * 1000);
#else
		sleep(delay);
#endif

		DCM_DATASET_CLASS *dataset_ptr = NULL;

		// build the event report message
		storageCommitmentM_ptr->event(&dataset_ptr, loggerM_ptr);

		// instantiate a SCU emulator to send the event
		STORAGE_SCU_EMULATOR_CLASS	storageScu(sessionM_ptr);
		storageScu.setLogger(loggerM_ptr);
		storageScu.setSerializer(serializerM_ptr);

		// send the event report
		UINT16 eventTypeId = (storageCommitmentM_ptr->noFailedInstances() > 0) ? 2 : 1;
		result = storageScu.eventReportStorageCommitment(eventTypeId, dataset_ptr);
		
		// indicate that the event has now been sent
		storageCommitmentM_ptr->setEventToSend(false);
	}

	// return result
	return result;
}

//>>===========================================================================

void STORAGE_SCP_EMULATOR_CLASS::completeLogging()

//  DESCRIPTION     : Complete the emulation logging by displaying any relationships.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// log the object relationship analysis
	if (sessionM_ptr->isLogLevel(LOG_IMAGE_RELATION))
	{
		RELATIONSHIP->logObjectRelationshipAnalysis(loggerM_ptr);
	}
}

