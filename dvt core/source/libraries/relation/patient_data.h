//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef PATIENT_DATA_H
#define PATIENT_DATA_H


//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class STUDY_DATA_CLASS;
class LOG_CLASS;


//>>***************************************************************************

class PATIENT_DATA_CLASS

//  DESCRIPTION     : Patient Data Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string patientNameM;
	string patientIdM;
	ARRAY<STUDY_DATA_CLASS*> studyDataM;

public:
	PATIENT_DATA_CLASS(string, string);

	~PATIENT_DATA_CLASS();

	string getPatientName() 
		{ return patientNameM; }

	string getPatientId() 
		{ return patientIdM; }

	UINT noStudies()
		{ return studyDataM.getSize(); }

	STUDY_DATA_CLASS *getStudyData(UINT i) 
		{ return studyDataM[i]; }

	void addStudyData(STUDY_DATA_CLASS *studyData_ptr)
		{ studyDataM.add(studyData_ptr); }

	STUDY_DATA_CLASS *search(string);

	void log(LOG_CLASS*);
};

#endif /* PATIENT_DATA_H */
