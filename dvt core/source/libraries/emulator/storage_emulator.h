//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Storage SCP emulator class.
//*****************************************************************************
#ifndef STORAGE_SCP_EMULATOR_H
#define STORAGE_SCP_EMULATOR_H

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
class MEDIA_FILE_HEADER_CLASS;
class STORAGE_COMMITMENT_CLASS;


//>>***************************************************************************

class STORAGE_SCP_EMULATOR_CLASS : public BASE_SCP_EMULATOR_CLASS 

//  DESCRIPTION     : Storage emulator class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	STORAGE_COMMITMENT_CLASS *storageCommitmentM_ptr;

	bool processStore(DCM_DATASET_CLASS*);

	bool processAction(DCM_DATASET_CLASS*);

protected:
	bool addSupportedPresentationContexts();
	
	bool processCommandDataset(DCM_COMMAND_CLASS*, DCM_DATASET_CLASS*);
	
	bool postProcess();

	void completeLogging();

public:
	STORAGE_SCP_EMULATOR_CLASS(EMULATOR_SESSION_CLASS*, BASE_SOCKET_CLASS*, bool);
	~STORAGE_SCP_EMULATOR_CLASS();
};

#endif /* STORAGE_SCP_EMULATOR_H */
