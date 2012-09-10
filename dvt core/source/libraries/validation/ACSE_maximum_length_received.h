//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_MAXIMUM_LENGTH_RECEIVED_H
#define ACSE_MAXIMUM_LENGTH_RECEIVED_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_parameter.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS : public ACSE_PARAMETER_CLASS

//  DESCRIPTION     : Maximum Length Received class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS();
	~ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS();
	
protected:
	bool checkSyntax();
	
	bool checkRange();
	
	bool checkReference(string);
};

#endif /* ACSE_MAXIMUM_LENGTH_RECEIVED_H */
