//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef SERIES_DATA_H
#define SERIES_DATA_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class SOP_INSTANCE_DATA_CLASS;
class LOG_CLASS;


//>>***************************************************************************

class SERIES_DATA_CLASS

//  DESCRIPTION     : Series Data Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string instanceUidM;
	ARRAY<SOP_INSTANCE_DATA_CLASS*>	sopInstanceDataM;

  public:
	SERIES_DATA_CLASS(string);
		
	~SERIES_DATA_CLASS();
		
	string getInstanceUid() 
		{ return instanceUidM; }

	UINT noSopInstances() 
		{ return sopInstanceDataM.getSize(); }

	SOP_INSTANCE_DATA_CLASS *getSopInstanceData(UINT i) 
		{ return sopInstanceDataM[i]; }

	void addSopInstanceData(SOP_INSTANCE_DATA_CLASS *sopInstanceData_ptr)
		{ sopInstanceDataM.add(sopInstanceData_ptr); }

	SOP_INSTANCE_DATA_CLASS *search(string);

	void log(LOG_CLASS*);
};

#endif /* SERIES_DATA_H */
