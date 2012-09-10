//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Base SCP emulator class.
//*****************************************************************************
#ifndef EMULATOR_H
#define EMULATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Idicom.h"			// Dicom component interface
#include "Inetwork.h"		// Network component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class EMULATOR_SESSION_CLASS;
class LOG_CLASS;

//>>***************************************************************************

class BASE_SCP_EMULATOR_CLASS 

//  DESCRIPTION     : Abstract base SCP emulator class for various SOP class SCP emulations
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
protected:
	EMULATOR_SESSION_CLASS		*sessionM_ptr;
	int							connectedSocketIdM;
	ASSOCIATION_CLASS			associationM;
	string						sopClassUidM;
	string						sopInstanceUidM;
	LOG_CLASS					*loggerM_ptr;
    BASE_SERIALIZER             *serializerM_ptr;
	bool						autoType2AttributesM;
	bool						defineSqLengthM;
	bool						addGroupLengthM;

	void setup(EMULATOR_SESSION_CLASS*, BASE_SOCKET_CLASS*, bool);

	void teardown();

	virtual bool addSupportedPresentationContexts() = 0;
	
	virtual bool processCommandDataset(DCM_COMMAND_CLASS*, DCM_DATASET_CLASS*) = 0;

	bool sendResponse(DIMSE_CMD_ENUM, UINT16 status = DCM_STATUS_SUCCESS, DCM_DATASET_CLASS *dataset_ptr = NULL);

	virtual void completeLogging() = 0;

public:
	virtual ~BASE_SCP_EMULATOR_CLASS() = 0;

	bool emulateScp();

	virtual bool postProcess()
		{ return true; }

	virtual bool terminate();

	virtual bool sendStatusEvent()
		{ return true; }

	EMULATOR_SESSION_CLASS *getSession() { return sessionM_ptr; }

	void setLogger(LOG_CLASS*);

    void setSerializer(BASE_SERIALIZER*);

	bool setSocketOwnerThreadId(THREAD_TYPE tid);
};

#endif /* EMULATOR_H */


