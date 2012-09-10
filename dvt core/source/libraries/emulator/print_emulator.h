//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Print SCP emulator class.
//*****************************************************************************
#ifndef PRINT_EMULATOR_H
#define PRINT_EMULATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "emulator.h"		// Base emulator class

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class BASIC_FILM_SESSION_CLASS;
class DCM_COMMAND_CLASS;
class DCM_DATASET_CLASS;
class LOG_CLASS;

//>>***************************************************************************

class PRINT_SCP_EMULATOR_CLASS : public BASE_SCP_EMULATOR_CLASS 

//  DESCRIPTION     : Print emulator class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	BASIC_FILM_SESSION_CLASS	*filmSessionM_ptr;

	bool processAction();

	bool processCreate(DCM_DATASET_CLASS*);

	bool processDelete();

	bool processGet(DCM_COMMAND_CLASS*);

	bool processSet(DCM_DATASET_CLASS*);

	void makeSopInstanceUid();

protected:
	bool addSupportedPresentationContexts();
	
	bool processCommandDataset(DCM_COMMAND_CLASS*, DCM_DATASET_CLASS*);

	void completeLogging() { }

public:
	PRINT_SCP_EMULATOR_CLASS(EMULATOR_SESSION_CLASS*, BASE_SOCKET_CLASS*, bool);
	~PRINT_SCP_EMULATOR_CLASS();

	bool sendStatusEvent();
};

#endif /* PRINT_EMULATOR_H */


