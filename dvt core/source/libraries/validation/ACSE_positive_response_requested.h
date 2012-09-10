//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_POSITIVE_RESPONSE_REQUESTED_H
#define ACSE_POSITIVE_RESPONSE_REQUESTED_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_parameter.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_POSITIVE_RESPONSE_REQUESTED_CLASS : public ACSE_PARAMETER_CLASS

//  DESCRIPTION     : Positive Response Requested class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_POSITIVE_RESPONSE_REQUESTED_CLASS();
	~ACSE_POSITIVE_RESPONSE_REQUESTED_CLASS();
	
protected:
	bool checkSyntax();
	
	bool checkRange();
	
	bool checkReference(string);
};

#endif /* ACSE_POSITIVE_RESPONSE_REQUESTED_H */
