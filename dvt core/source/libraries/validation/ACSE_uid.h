//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_UID_H
#define ACSE_UID_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_parameter.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_UID_CLASS : public ACSE_PARAMETER_CLASS

//  DESCRIPTION     : UID class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_UID_CLASS();
	~ACSE_UID_CLASS();
	
protected:
	bool checkSyntax();
	
	virtual bool checkRange();
	
	bool checkReference(string);
	
private:
	bool checkUidSyntax(char*);
}; 

#endif /* ACSE_UID_H */
