//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef SOP_INSTANCE_DATA_H
#define SOP_INSTANCE_DATA_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;


//>>***************************************************************************

class SOP_INSTANCE_DATA_CLASS

//  DESCRIPTION     : SOP Instance Data Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string instanceUidM;
	string frameOfReferenceUidM;
	string imageTypeValue3M;
	string refImageInstanceUidM;
	UINT countM;

public:
	SOP_INSTANCE_DATA_CLASS(string, string, string, string);

	~SOP_INSTANCE_DATA_CLASS();

	void incrementCount() 
		{ countM++; }

	string getInstanceUid() 
		{ return instanceUidM; }

	UINT getCount() 
		{ return countM; }

	void log(LOG_CLASS*);
};

#endif /* SOP_INSTANCE_DATA_H */
