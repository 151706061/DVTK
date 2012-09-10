//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef PATIENT_LIST_H
#define PATIENT_LIST_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_DATASET_CLASS;
class LOG_CLASS;
class PATIENT_DATA_CLASS;


//>>***************************************************************************

class PATIENT_LIST_CLASS

//  DESCRIPTION     : Patient List Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	ARRAY<PATIENT_DATA_CLASS*>	patientDataM;

	UINT noPatients(void);

	PATIENT_DATA_CLASS *getPatientData(UINT i);

	void addPatientData(PATIENT_DATA_CLASS *patData_ptr);

	PATIENT_DATA_CLASS *search(string id, string name, LOG_CLASS *logger_ptr);

public:
	PATIENT_LIST_CLASS();

	~PATIENT_LIST_CLASS();

	void cleanup(void);

	void analyseStorageDataset(DCM_DATASET_CLASS *dataset_ptr, LOG_CLASS *logger_ptr);

	void log(LOG_CLASS *logger_ptr);
};

#endif /* PATIENT_LIST_H */
