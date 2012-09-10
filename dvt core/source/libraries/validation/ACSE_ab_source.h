//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright � 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_AB_SOURCE_H
#define ACSE_AB_SOURCE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_parameter.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_AB_SOURCE_CLASS : public ACSE_PARAMETER_CLASS

//  DESCRIPTION     : Abort Request Source class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_AB_SOURCE_CLASS();
	~ACSE_AB_SOURCE_CLASS();
	
protected:
	bool checkSyntax();
	
	bool checkRange();
	
	bool checkReference(string);
};

#endif /* ACSE_AB_SOURCE_H */
