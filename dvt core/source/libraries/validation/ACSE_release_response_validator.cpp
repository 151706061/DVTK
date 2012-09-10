//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_release_response_validator.h"
#include "Iglobal.h"      // Global component interface file
#include "Ilog.h"         // Logging component interface file

//>>===========================================================================		

RELEASE_RP_VALIDATOR_CLASS::RELEASE_RP_VALIDATOR_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
} 

//>>===========================================================================		

RELEASE_RP_VALIDATOR_CLASS::~RELEASE_RP_VALIDATOR_CLASS()

//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// destructor activities
} 

//>>===========================================================================		
bool
RELEASE_RP_VALIDATOR_CLASS::validate(RELEASE_RP_CLASS *srcReleaseRp_ptr,
                                     RELEASE_RP_CLASS *)
									 //  DESCRIPTION     : Validate Release Response.
									 //  PRECONDITIONS   :
									 //  POSTCONDITIONS  :
									 //  EXCEPTIONS      : 
									 //  NOTES           :
									 //<<===========================================================================		
{
	// check for valid source
	if (srcReleaseRp_ptr == NULL) return false;
	
	// nothing to validate
	return true;
} 
