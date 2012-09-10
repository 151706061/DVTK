//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACTIVITY_LOG_H
#define ACTIVITY_LOG_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "base_log.h"


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************


//>>***************************************************************************

class ACTIVITY_LOG_CLASS : public LOG_CLASS

//  DESCRIPTION     : Base Line Log Display Class.
//  INVARIANT       :
//  NOTES           : Display methods.
//<<***************************************************************************
{
public:
	ACTIVITY_LOG_CLASS();
	~ACTIVITY_LOG_CLASS();

	void displayText();
};

#endif /* ACTIVITY_LOG_LOG_H */
