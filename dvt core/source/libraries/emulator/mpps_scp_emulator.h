//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Mpps SCP emulator class.
//*****************************************************************************
#ifndef MPPS_SCP_EMULATOR_H
#define MPPS_SCP_EMULATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"				// Global component interface
#include "emulator.h"				// Base emulator class

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_COMMAND_CLASS;
class DCM_DATASET_CLASS;


//>>***************************************************************************

class MPPS_SCP_EMULATOR_CLASS : public BASE_SCP_EMULATOR_CLASS 

//  DESCRIPTION     : Mpps emulator class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	bool processCreate(DCM_DATASET_CLASS*);

	bool processSet(DCM_DATASET_CLASS*);

protected:
	bool addSupportedPresentationContexts();
	
	bool processCommandDataset(DCM_COMMAND_CLASS*, DCM_DATASET_CLASS*);
	
	void completeLogging();

public:
	MPPS_SCP_EMULATOR_CLASS(EMULATOR_SESSION_CLASS*, BASE_SOCKET_CLASS*, bool);
	~MPPS_SCP_EMULATOR_CLASS();
};

#endif /* MPPS_SCP_EMULATOR_H */
