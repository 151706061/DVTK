//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "relation.h"
#include "stored_sop_instance.h"
#include "Ilog.h"					// Log component interface

//*****************************************************************************
// initialise static pointers
//*****************************************************************************
RELATIONSHIP_CLASS *RELATIONSHIP_CLASS::instanceM_ptr = NULL;


//>>===========================================================================
RELATIONSHIP_CLASS::RELATIONSHIP_CLASS()
//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	// - initialise the access semaphore
	initSemaphore();
}

//>>===========================================================================
RELATIONSHIP_CLASS::~RELATIONSHIP_CLASS()
//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
	cleanup();

	// terminate the access semaphore
	termSemaphore();
}

//>>===========================================================================
RELATIONSHIP_CLASS *RELATIONSHIP_CLASS::instance()
//  DESCRIPTION     : Singleton instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceM_ptr == NULL) {
		instanceM_ptr = new RELATIONSHIP_CLASS();
	}

	return instanceM_ptr;
}

//
// set up mutual exclusion
//
#ifdef _WINDOWS
//CCriticalSection	RelationshipAccess;

//>>===========================================================================
void RELATIONSHIP_CLASS::initSemaphore()
//  DESCRIPTION     : Initialize the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// do nothing
}

//>>===========================================================================
void RELATIONSHIP_CLASS::postSemaphore()
//  DESCRIPTION     : Post the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// post the semaphore
	//RelationshipAccess.Unlock();
}

//>>===========================================================================
void RELATIONSHIP_CLASS::waitSemaphore()
//  DESCRIPTION     : Wait for the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// wait for the semaphore
	//RelationshipAccess.Lock();
}

//>>===========================================================================
void RELATIONSHIP_CLASS::termSemaphore()
//  DESCRIPTION     : Terminate the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// do nothing
}

#else
sem_t	RelationshipSemaphoreId;

//>>===========================================================================
void RELATIONSHIP_CLASS::initSemaphore()
//  DESCRIPTION     : Initialize the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// initialise the semaphore
	if (sem_init(&RelationshipSemaphoreId, 0, 1) != 0) {
		printf("\nFailed to create a semaphore for database locking - exiting...");
		exit(0);
	}
}

//>>===========================================================================
void RELATIONSHIP_CLASS::postSemaphore()
//  DESCRIPTION     : Post the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// post the semaphore
	if (sem_post(&RelationshipSemaphoreId) != 0) {
		printf("\nFailed to post to semaphore for database locking - exiting...");
		exit(0);
	}
}

//>>===========================================================================
void RELATIONSHIP_CLASS::waitSemaphore()
//  DESCRIPTION     : Wait for the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// wait for the semaphore
	if (sem_wait(&RelationshipSemaphoreId) != 0) {
		printf("\nFailed to wait on semaphore for database locking - exiting...");
		exit(0);	
	}
}

//>>===========================================================================
void RELATIONSHIP_CLASS::termSemaphore()
//  DESCRIPTION     : Terminate the relationship access semaphore.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destroy the semaphore
	if (sem_destroy(&RelationshipSemaphoreId) != 0) {
		printf("\nFailed to destroy the semaphore for database locking - exiting...");
		exit(0);	
	}
}

#endif

//>>===========================================================================
void RELATIONSHIP_CLASS::cleanup()
//  DESCRIPTION     : Cleanup the relationship data.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// wait for access to warehouse
	waitSemaphore();

	// cleanup the patient list - instantiate it again
	patientListM.cleanup();

	// cleanup the stored sop list - instantiate it again
	storedSopListM.cleanup();

	// release access to warehouse
	postSemaphore();
}

//>>===========================================================================
void RELATIONSHIP_CLASS::analyseStorageDataset(DCM_DATASET_CLASS *dataset_ptr, LOG_CLASS *logger_ptr)
//  DESCRIPTION     : Analyse the Storage Dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// wait for access to warehouse
	waitSemaphore();

	// analyse the Storage Dataset for the patient relationship
	patientListM.analyseStorageDataset(dataset_ptr, logger_ptr);

	// analyse the Storage Dataset for the stored sop
	storedSopListM.analyseStorageDataset(dataset_ptr, logger_ptr);

	// release access to warehouse
	postSemaphore();
}

//>>===========================================================================
bool RELATIONSHIP_CLASS::commit(string		  sopClassUid,
								string		  sopInstanceUid,
								LOG_CLASS	* logger_ptr)
//  DESCRIPTION     : Commit the storage if the sop instance can be found.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	STORED_SOP_INSTANCE_CLASS	* storedSopInstance_ptr;
	bool						  result = false;

	// wait for access to warehouse
	waitSemaphore();

	// check if the sop is stored
	storedSopInstance_ptr = storedSopListM.search(sopClassUid,
												  sopInstanceUid,
												  logger_ptr);
	if (storedSopInstance_ptr)
	{
		// commit the storage
		storedSopInstance_ptr->commit();

		// return success
		result = true;
	}

	// release access to warehouse
	postSemaphore();

	// return result
	return result;
}

//>>===========================================================================
void RELATIONSHIP_CLASS::logObjectRelationshipAnalysis(LOG_CLASS *logger_ptr)
//  DESCRIPTION     : Log the Object Relationship Analysis.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// wait for access to warehouse
	waitSemaphore();

	// log the patient list
	patientListM.log(logger_ptr);

	// log the stored sop list
	storedSopListM.log(logger_ptr);

	// release access to warehouse
	postSemaphore();
}
