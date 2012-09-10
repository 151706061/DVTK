//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef STORED_SOP_INSTANCE_H
#define STORED_SOP_INSTANCE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;


//>>***************************************************************************

class STORED_SOP_INSTANCE_CLASS

//  DESCRIPTION     : Class used to store the sop class/instance detail.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string	sopClassUidM;
	string	sopInstanceUidM;
	UINT	countM;
	bool	committedM;

public:
	STORED_SOP_INSTANCE_CLASS();
	STORED_SOP_INSTANCE_CLASS(string, string);
	
	~STORED_SOP_INSTANCE_CLASS();

	void setSopClassUid(string sopClassUid)
		{ sopClassUidM = sopClassUid; }

	void setSopInstanceUid(string sopInstanceUid)
		{ sopInstanceUidM = sopInstanceUid; }

	void commit() { committedM = true; }

	string getSopClassUid() { return sopClassUidM; }

	string getSopInstanceUid() { return sopInstanceUidM; }

	void incrementCount() 
		{ countM++; }

	UINT getCount() 
		{ return countM; }

	void log(LOG_CLASS*);
};

#endif /* STORED_SOP_INSTANCE_H */
