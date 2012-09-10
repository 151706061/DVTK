//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef RELATION_H
#define RELATION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "patient_list.h"
#include "stored_sop_list.h"


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_DATASET_CLASS;
class LOG_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
#define RELATIONSHIP	RELATIONSHIP_CLASS::instance()

//>>***************************************************************************

class RELATIONSHIP_CLASS

//  DESCRIPTION     : Class used to store the relationships.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	static RELATIONSHIP_CLASS	*instanceM_ptr;	// Singleton
	PATIENT_LIST_CLASS			patientListM;
	STORED_SOP_LIST_CLASS		storedSopListM;

	RELATIONSHIP_CLASS();

	~RELATIONSHIP_CLASS();

	void		initSemaphore					(void);
	void		postSemaphore					(void);
	void		waitSemaphore					(void);
	void		termSemaphore					(void);

public:

	static RELATIONSHIP_CLASS *	instance		(void);

	void		cleanup							(void);

	void		analyseStorageDataset			(DCM_DATASET_CLASS*, LOG_CLASS*);

	bool		commit							(string		  sopClassUid,
												 string		  sopInstanceUid,
												 LOG_CLASS	* logger_ptr);

	void		logObjectRelationshipAnalysis	(LOG_CLASS	* logger_ptr);
};

#endif /* RELATION_H */
