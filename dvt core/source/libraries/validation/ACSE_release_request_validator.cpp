//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_release_request_validator.h"
#include "Iglobal.h"      // Global component interface file
#include "Ilog.h"         // Logging component interface file

//>>===========================================================================		

RELEASE_RQ_VALIDATOR_CLASS::RELEASE_RQ_VALIDATOR_CLASS()

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

RELEASE_RQ_VALIDATOR_CLASS::~RELEASE_RQ_VALIDATOR_CLASS()

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
RELEASE_RQ_VALIDATOR_CLASS::validate(RELEASE_RQ_CLASS *srcReleaseRq_ptr,
                                     RELEASE_RQ_CLASS *)
									 //  DESCRIPTION     : Validate Release Request.
									 //  PRECONDITIONS   :
									 //  POSTCONDITIONS  :
									 //  EXCEPTIONS      : 
									 //  NOTES           :
									 //<<===========================================================================		
{
	// check for valid source
	if (srcReleaseRq_ptr == NULL) return false;
	
	// nothing to validate
	return true;
} 
