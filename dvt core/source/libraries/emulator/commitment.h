//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Storage Commitment emulation classes.
//*****************************************************************************
#ifndef COMMITMENT_H
#define COMMITMENT_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Irelationship.h"	// Relation component interface

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_DATASET_CLASS;
class LOG_CLASS;


//>>***************************************************************************

class STORAGE_COMMITMENT_CLASS 

//  DESCRIPTION     : Storage Commitment class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
protected:
	bool								eventToSendM;
	string								transactionUidM;
	ARRAY<STORED_SOP_INSTANCE_CLASS*>	storedInstanceM;
	ARRAY<STORED_SOP_INSTANCE_CLASS*>	failedInstanceM;
	STORED_SOP_INSTANCE_CLASS			studyComponentM;

	void setTransactionUid(string transactionUid)
		{ transactionUidM = transactionUid; }

	void addStoredInstance(string, string);

	void addFailedInstance(string, string);

	void setStudyComponentUids(string sopClassUid, string sopInstanceUid)
	{ 
		studyComponentM.setSopClassUid(sopClassUid);
		studyComponentM.setSopInstanceUid(sopInstanceUid);
	}

	string getTransactionUid() { return transactionUidM; }

	UINT noStoredInstances() { return storedInstanceM.getSize(); }

	STORED_SOP_INSTANCE_CLASS *getStoredInstance(UINT);

	STORED_SOP_INSTANCE_CLASS *getFailedInstance(UINT);

	string getStudyComponentSopClassUid()
		{ return studyComponentM.getSopClassUid(); }

	string getStudyComponentSopInstanceUid()
		{ return studyComponentM.getSopInstanceUid(); }

public:
	STORAGE_COMMITMENT_CLASS();
	~STORAGE_COMMITMENT_CLASS();

	void setEventToSend(bool eventToSend) { eventToSendM = eventToSend; }

	bool isEventToSend() { return eventToSendM; }

	UINT16 action(DCM_DATASET_CLASS*, LOG_CLASS*);

	UINT16 event(DCM_DATASET_CLASS**, LOG_CLASS*);

	UINT noFailedInstances() { return failedInstanceM.getSize(); }
};

#endif /* COMMITMENT_H */


