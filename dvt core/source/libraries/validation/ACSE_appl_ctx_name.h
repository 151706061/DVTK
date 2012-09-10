//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_APPL_CTX_NAME_H
#define ACSE_APPL_CTX_NAME_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_uid.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class ACSE_APPL_CTX_NAME_CLASS : public ACSE_UID_CLASS

//  DESCRIPTION     : Application Context Name class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_APPL_CTX_NAME_CLASS();
	~ACSE_APPL_CTX_NAME_CLASS();
	
protected:
	bool checkRange();
};

#endif /* ACSE_APPL_CTX_NAME_H */
