//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ModuleDefinition.h"


//>>===========================================================================

DEF_MODULE_CLASS::DEF_MODULE_CLASS()

//  DESCRIPTION     : Default constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
}

//>>===========================================================================

DEF_MODULE_CLASS::DEF_MODULE_CLASS(const string name)

//  DESCRIPTION     : Constructor with name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	SetName(name);
}

//>>===========================================================================

DEF_MODULE_CLASS::DEF_MODULE_CLASS(const string name, const MOD_USAGE_ENUM usage)

//  DESCRIPTION     : Constructor with name and usage.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	SetName(name);
	SetUsage(usage);
}

//>>===========================================================================

DEF_MODULE_CLASS::~DEF_MODULE_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
}
