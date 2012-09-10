//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_IMPLEMENTATION_VERSION_NAME_H
#define ACSE_IMPLEMENTATION_VERSION_NAME_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_parameter.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_IMPLEMENTATION_VERSION_NAME_CLASS : public ACSE_PARAMETER_CLASS

//  DESCRIPTION     : Implementation Version Name class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_IMPLEMENTATION_VERSION_NAME_CLASS();
	~ACSE_IMPLEMENTATION_VERSION_NAME_CLASS();
	
protected:
	bool checkSyntax();
	
	bool checkRange();
	
	bool checkReference(string);
};

#endif /* ACSE_IMPLEMENTATION_VERSION_NAME_H */
