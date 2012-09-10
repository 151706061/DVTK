//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef STUDY_DATA_H
#define STUDY_DATA_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;
class SERIES_DATA_CLASS;


//>>***************************************************************************

class STUDY_DATA_CLASS

//  DESCRIPTION     : Study Data Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string instanceUidM;
	ARRAY<SERIES_DATA_CLASS*> seriesDataM;

public:
	STUDY_DATA_CLASS(string);

	~STUDY_DATA_CLASS();

	string getInstanceUid() 
		{ return instanceUidM; }

	UINT noSeries()
		{ return seriesDataM.getSize(); }

	SERIES_DATA_CLASS *getSeriesData(UINT i)
		{ return seriesDataM[i]; }

	void addSeriesData(SERIES_DATA_CLASS *seriesData_ptr)
		{ seriesDataM.add(seriesData_ptr); }

	SERIES_DATA_CLASS *search(string);

	void log(LOG_CLASS*);
};

#endif /* STUDY_DATA_H */
