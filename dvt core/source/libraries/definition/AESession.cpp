//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "AESession.h"
#include "definition.h"


//>>===========================================================================

AE_SESSION_CLASS::AE_SESSION_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	nameM = DEFAULT_AE_NAME;
	versionM = DEFAULT_AE_VERSION;
}

//>>===========================================================================		

AE_SESSION_CLASS::AE_SESSION_CLASS(const string name, const string version)

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	nameM = name;
	versionM = version;
}

//>>===========================================================================		

AE_SESSION_CLASS::~AE_SESSION_CLASS()

//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// destructor activities
}
