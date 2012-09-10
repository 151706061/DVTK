//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Base SCP emulator class.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "emulator.h"
#include "Isession.h"		// Session component interface
#include "Idefinition.h"	// Definition component interface
#include "Ivalidation.h"	// Validation component interface


//>>===========================================================================

BASE_SCP_EMULATOR_CLASS::~BASE_SCP_EMULATOR_CLASS()

//  DESCRIPTION     : Base class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
}

//>>===========================================================================

void BASE_SCP_EMULATOR_CLASS::setup(EMULATOR_SESSION_CLASS *session_ptr, BASE_SOCKET_CLASS* socket_ptr, bool logEmulation)

//  DESCRIPTION     : Set up all the logging and serialization.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	//
	// Interfaces towards managed code.
	//
	ACTIVITY_LOG_CLASS *activityLogger_ptr = NULL;
	BASE_SERIALIZER *serializer_ptr = NULL;

	//
	// Constructor activities.
	//
	sessionM_ptr = session_ptr;
	autoType2AttributesM = false;
	defineSqLengthM = false;
	addGroupLengthM = false;
	associationM.setSocket(socket_ptr);
	//
	// Determine which logger is assigned to the session.
	//
	LOG_CLASS *sessionLogger_ptr = session_ptr->getLogger();
	if (logEmulation &&
		(sessionLogger_ptr != NULL))
	{
		//
		// Setup a child activity logger.
		//
		activityLogger_ptr = new ACTIVITY_LOG_CLASS();
		activityLogger_ptr->setActivityReporter(sessionLogger_ptr->getActivityReporter());
		//
		// Setup session serializer. 
		// Spawn a child serializer based on the current parent serializer.
		// This serializer is the target for the validation output.
		//
		BASE_SERIALIZER *sessionSerializer = sessionLogger_ptr->getSerializer();
		if (sessionSerializer)
		{
			serializer_ptr = sessionSerializer->CreateAndRegisterChildSerializer(::SerializerNodeType_Thread);
			serializer_ptr->StartSerializer();
			activityLogger_ptr->setSerializer(serializer_ptr);
		}
		//
		// Apply log mask settings.
		//
		UINT32 logMask = sessionLogger_ptr->getLogMask();
		logMask |= (LOG_NONE | LOG_SCRIPT | LOG_MEDIA_FILENAME);
		activityLogger_ptr->setLogMask(logMask);
		activityLogger_ptr->setResultsRoot(sessionLogger_ptr->getResultsRoot());
		activityLogger_ptr->setStorageRoot(sessionLogger_ptr->getStorageRoot());
	}
	//
	// Apply logger.
	//
	setLogger(activityLogger_ptr);
	//
	// Apply serializer.
	//
    setSerializer(serializer_ptr);
}

//>>===========================================================================

void BASE_SCP_EMULATOR_CLASS::teardown()

//  DESCRIPTION     : Tear down all the logging and serialization.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	if (serializerM_ptr)
	{
		//
		// End the serializer.
		//
		serializerM_ptr->EndSerializer();
		//
		// Teardown session serializer.
		//
		// Determine which logger is assigned to the session.
		//
		BASE_SERIALIZER *sessionSerializer = NULL;
		LOG_CLASS *sessionLogger_ptr = sessionM_ptr->getLogger();
		if (sessionLogger_ptr != NULL)
		{
			sessionSerializer = sessionLogger_ptr->getSerializer();
		}
		if (sessionSerializer != NULL)
		{
			sessionSerializer->UnRegisterAndDestroyChildSerializer(serializerM_ptr);
			serializerM_ptr = NULL;
		}
	}
	if (loggerM_ptr)
	{
		// free the logger
		delete loggerM_ptr;
	}
}

//>>===========================================================================

bool BASE_SCP_EMULATOR_CLASS::emulateScp()

//  DESCRIPTION     : Provide scp emulation for a single association.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	RECEIVE_MSG_ENUM status;
	bool result = false;

    //
    // As we are acting as an SCP - it is safe to assume that the SUT is the Requester
    // and DVT is the Accepter.
    //
	associationM.setCalledAeTitle(sessionM_ptr->getDvtAeTitle());
	associationM.setCallingAeTitle(sessionM_ptr->getSutAeTitle());
	associationM.setMaximumLengthReceived(sessionM_ptr->getDvtMaximumLengthReceived());
	associationM.setImplementationClassUid(sessionM_ptr->getDvtImplementationClassUid());
	associationM.setImplementationVersionName(sessionM_ptr->getDvtImplementationVersionName());
	associationM.setStorageMode(sessionM_ptr->getStorageMode());
	autoType2AttributesM = sessionM_ptr->getAutoType2Attributes();
	defineSqLengthM = sessionM_ptr->getDefineSqLength();
	addGroupLengthM = sessionM_ptr->getAddGroupLength();

	// add support for the verification sop class
	for (int i = 0 ; i < sessionM_ptr->noSupportedTransferSyntaxes(); i++)
	{
		// add the verification sop class to the presentation contexts
		associationM.setSupportedPresentationContext(VERIFICATION_SOP_CLASS_UID, (char*) sessionM_ptr->getSupportedTransferSyntax(i));
	}

	// add the additional presentation contexts that should be supported
	if (!addSupportedPresentationContexts()) 
	{
		goto done;
	}

	// wait for an association to be established
	status = associationM.waitForAssociation();
	switch (status)
	{
	case RECEIVE_MSG_CONNECTION_CLOSED:
		// normal closedown - no longer emulating
        result = true;
		break;

	case RECEIVE_MSG_ASSOC_REJECTED:
	default:
		// some kind of failure - stop
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_ERROR, 1, "Failed to accept Association from remote SCU");
		}
        result = false;
		break;

	case RECEIVE_MSG_SUCCESSFUL:
		// once an association has been established
		// - loop while message handling - until it is either released, aborted or an error occurs
        bool associated = true;
		while (associated)
		{
			DCM_COMMAND_CLASS *command_ptr;
			DCM_DATASET_CLASS *dataset_ptr;
			AE_SESSION_CLASS ae_session;

			if (sessionM_ptr->isSessionStopped())
    		{
	    		// session is being stopped
                result = true;

	    		goto done;
		    }

			// set the default ae session
			ae_session.SetName(sessionM_ptr->getApplicationEntityName());
			ae_session.SetVersion(sessionM_ptr->getApplicationEntityVersion());

			// receive the next command (and dataset)
			status = associationM.receiveCommandDataset(&command_ptr, &dataset_ptr, &ae_session, true, false);
	
			switch(status)
			{
			case RECEIVE_MSG_SUCCESSFUL:
				{
					// check if SOP Class UID attribute is available - try both affected and requested
					// sop class uids - the command has already been validated to ensuer the correct one defined
					if (!command_ptr->getUIValue(TAG_AFFECTED_SOP_CLASS_UID, sopClassUidM)) 
					{
						if (!command_ptr->getUIValue(TAG_REQUESTED_SOP_CLASS_UID, sopClassUidM))
						{
							// SOP Class UID not available
							sopClassUidM = "";
						}
					}

					// check if SOP Instance UID attribute is available - try both affected and requested
					// sop instance uids - the command has already been validated to ensure the correct one defined
					if (!command_ptr->getUIValue(TAG_AFFECTED_SOP_INSTANCE_UID, sopInstanceUidM)) 
					{
						if (!command_ptr->getUIValue(TAG_REQUESTED_SOP_INSTANCE_UID, sopInstanceUidM))
						{
							// SOP Instance UID not available
							sopInstanceUidM = "";
						}
					}	

					// log action
					if (loggerM_ptr)
					{
						if (dataset_ptr)
						{
							string sopName = DEFINITION->GetSopName(sopClassUidM);
							loggerM_ptr->text(LOG_SCRIPT, 2, "RECEIVED %s %s (%s)", mapCommandName(command_ptr->getCommandId()), sopName.c_str(), timeStamp());

                            // serialize it
                            if (serializerM_ptr)
                            {
                                serializerM_ptr->SerializeReceive(command_ptr, dataset_ptr);
                            }
                        }
						else
						{
							loggerM_ptr->text(LOG_SCRIPT, 2, "RECEIVED %s (%s)", mapCommandName(command_ptr->getCommandId()), timeStamp());

                            // serialize it
                            if (serializerM_ptr)
                            {
                                serializerM_ptr->SerializeReceive(command_ptr, NULL);
                            }
                        }
					}

					// validate the received command now
					VALIDATION->setStrictValidation(sessionM_ptr->getStrictValidation());
					VALIDATION->setIncludeType3NotPresentInResults(sessionM_ptr->getIncludeType3NotPresentInResults());
					result = VALIDATION->validate(command_ptr, NULL, ALL, serializerM_ptr);
					if (!result)
					{
						// if strict validation is enabled we should stop immediately
						if (sessionM_ptr->getStrictValidation())
						{
							result = false;

                            goto done;
						}
					}

					// check if we got a dataset too
					if (dataset_ptr)
					{
						DCM_DATASET_CLASS *refDataset_ptr = NULL;

						if (dataset_ptr->setIdentifierByTag(WAREHOUSE->getReferenceTag()))
						{
							// get the identifier
							string identifier = dataset_ptr->getIdentifier();

							// try to find matching object
							BASE_WAREHOUSE_ITEM_DATA_CLASS *wid_ptr = WAREHOUSE->retrieve(identifier, WID_DATASET);
							if (wid_ptr)
							{
								refDataset_ptr = static_cast<DCM_DATASET_CLASS*>(wid_ptr);

								if (loggerM_ptr)
								{
									loggerM_ptr->text(LOG_INFO, 1, "Reference Dataset with identifier \"%s\" found in Warehouse", identifier.c_str());
								}
							}
						}

						// validate the received dataset now
						VALIDATION->setStrictValidation(sessionM_ptr->getStrictValidation());
						VALIDATION->setIncludeType3NotPresentInResults(sessionM_ptr->getIncludeType3NotPresentInResults());
						result = VALIDATION->validate(dataset_ptr, refDataset_ptr, command_ptr, ALL, serializerM_ptr, &ae_session);
						if (!result)
						{
							// if strict validation is enabled we should stop immediately
							if (sessionM_ptr->getStrictValidation())
							{
                                result = false;

								goto done;
							}
						}
					}

					// check for the verification sop class
					if (command_ptr->getCommandId() == DIMSE_CMD_CECHO_RQ)
					{
						// reply with echo response
						result = sendResponse(DIMSE_CMD_CECHO_RSP);
					}
					else
					{
						// process the received command (and dataset);
						result = processCommandDataset(command_ptr, dataset_ptr);
					}

					if ((!result) &&
						(loggerM_ptr))
					{
						loggerM_ptr->text(LOG_ERROR, 1, "Failed to process received Command (and Dataset)");
					}
				}
				break;

			case RECEIVE_MSG_FAILURE:
			default:
				// some kind of failure - stop
				if (loggerM_ptr)
				{
					loggerM_ptr->text(LOG_ERROR, 1, "Failed to receive Command (and Dataset)");
				}

                // no longer associated
                associated = false;

				result = false;
				break;

			case RECEIVE_MSG_ASSOC_RELEASED:
				// normal association release
				if (loggerM_ptr)
				{
					loggerM_ptr->text(LOG_INFO, 1, "Assocation has been released");
				}

				// make call to do any association release post processing
				result = postProcess();

                // no longer associated
                associated = false;
				break;

			case RECEIVE_MSG_ASSOC_ABORTED:
				// association aborted
				if (loggerM_ptr)
				{
					loggerM_ptr->text(LOG_WARNING, 1, "Assocation has been aborted");
				}

                // no longer associated
                associated = false;

                result = true;
				break;
			}
		}
		break;
	}

done:
	// complete any emulator specific logging
	completeLogging();

	// return result
	return result;
}

//>>===========================================================================

bool BASE_SCP_EMULATOR_CLASS::terminate()

//  DESCRIPTION     : Terminate the emulation.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// terminate the emulation by resetting the association
	associationM.reset();

	// return success
	return true;
}

//>>===========================================================================

bool BASE_SCP_EMULATOR_CLASS::sendResponse(DIMSE_CMD_ENUM commandId, UINT16 status, DCM_DATASET_CLASS *dataset_ptr)

//  DESCRIPTION     : Send a DIMSE response over the current association.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DCM_COMMAND_CLASS command(commandId);
	bool result;

	// cascade the logger
	command.setLogger(loggerM_ptr);

	// set the status value
	command.setUSValue(TAG_STATUS, status);

	// check if action type should be set
	if (commandId == DIMSE_CMD_NACTION_RSP)
	{
		// fix for problem - PMSim21710
		command.setUIValue(TAG_AFFECTED_SOP_CLASS_UID, sopClassUidM);
		command.setUSValue(TAG_ACTION_TYPE_ID, 0x0001);
	}

	// set the sop class and instance uids in the association
	associationM.setSopClassUid(sopClassUidM);
	associationM.setSopInstanceUid(sopInstanceUidM);

	// return the response
	if (dataset_ptr)
	{
		// cascade the logger
		dataset_ptr->setLogger(loggerM_ptr);

		// set up some encoding flags
		dataset_ptr->setPopulateWithAttributes(autoType2AttributesM);
		dataset_ptr->setDefineGroupLengths(addGroupLengthM);
		dataset_ptr->setDefineSqLengths(defineSqLengthM);

		// log action
		if (loggerM_ptr)
		{
			string sopName = DEFINITION->GetSopName(sopClassUidM);
			loggerM_ptr->text(LOG_SCRIPT, 2, "SENT %s %s (%s)", mapCommandName(command.getCommandId()), sopName.c_str(), timeStamp());
		}

		// return response command with dataset
		result = associationM.send(&command, dataset_ptr);

		// clean up the dataset
		delete dataset_ptr;
	}
	else
	{
		// log action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_SCRIPT, 2, "SENT %s (%s)", mapCommandName(command.getCommandId()), timeStamp());
		}

		// return response command only
		result = associationM.send(&command);
	}

	if ((result) &&
		(status != DCM_STATUS_SUCCESS) &&
		(loggerM_ptr))
	{
		loggerM_ptr->text(LOG_WARNING, 1, "Returning non-zero status of 0x%04X in DIMSE Response", status);
	}

	// return result
	return result;
}

//>>===========================================================================

void BASE_SCP_EMULATOR_CLASS::setLogger(LOG_CLASS *logger_ptr)

//  DESCRIPTION     : Set the logger.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    loggerM_ptr = logger_ptr;
	associationM.setLogger(logger_ptr); 
}

//>>===========================================================================

void BASE_SCP_EMULATOR_CLASS::setSerializer(BASE_SERIALIZER *serializer_ptr)

//  DESCRIPTION     : Set the serializer.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    serializerM_ptr = serializer_ptr;
	associationM.setSerializer(serializer_ptr); 
}

//>>===========================================================================

bool BASE_SCP_EMULATOR_CLASS::setSocketOwnerThreadId(THREAD_TYPE tid)

//  DESCRIPTION     : Set the owner thread ID for the association's socket.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	if (associationM.getSocket() != NULL)
	{
		associationM.getSocket()->setOwnerThread(tid);
		return true;
	}
	else
	{
		// socket not defined
		return false;
	}
}

