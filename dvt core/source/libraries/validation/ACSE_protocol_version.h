//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_PROTOCOL_VERSION_H
#define ACSE_PROTOCOL_VERSION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_parameter.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_PROTOCOL_VERSION_CLASS : public ACSE_PARAMETER_CLASS

//  DESCRIPTION     : Protocol Version class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_PROTOCOL_VERSION_CLASS();
	~ACSE_PROTOCOL_VERSION_CLASS();
	
protected:
	bool checkSyntax();
	
	bool checkRange();
	
	bool checkReference(string);
}; 

#endif /* ACSE_PROTOCOL_VERSION_H */
