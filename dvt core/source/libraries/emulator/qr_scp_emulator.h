//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Query/Retrieve SCP emulator class.
//*****************************************************************************
#ifndef QR_SCP_EMULATOR_H
#define QR_SCP_EMULATOR_H

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

class QUERY_RETRIEVE_SCP_EMULATOR_CLASS : public BASE_SCP_EMULATOR_CLASS 

//  DESCRIPTION     : Query/Retrieve emulator class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	bool processFind(DCM_DATASET_CLASS*);

	bool processMove(DCM_DATASET_CLASS*);

	bool processGet(DCM_DATASET_CLASS*);

protected:
	bool addSupportedPresentationContexts();
	
	bool processCommandDataset(DCM_COMMAND_CLASS*, DCM_DATASET_CLASS*);

	void completeLogging();

public:
	QUERY_RETRIEVE_SCP_EMULATOR_CLASS(EMULATOR_SESSION_CLASS*, BASE_SOCKET_CLASS*, bool);
	~QUERY_RETRIEVE_SCP_EMULATOR_CLASS();
};

#endif /* QR_SCP_EMULATOR_H */
