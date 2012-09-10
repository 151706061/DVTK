//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_USER_IDENTITY_TYPE_H
#define ACSE_USER_IDENTITY_TYPE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_parameter.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_USER_IDENTITY_TYPE_CLASS : public ACSE_PARAMETER_CLASS

//  DESCRIPTION     : User Identity Type class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_USER_IDENTITY_TYPE_CLASS();
	~ACSE_USER_IDENTITY_TYPE_CLASS();
	
protected:
	bool checkSyntax();
	
	bool checkRange();
	
	bool checkReference(string);
};

#endif /* ACSE_USER_IDENTITY_TYPE_H */
