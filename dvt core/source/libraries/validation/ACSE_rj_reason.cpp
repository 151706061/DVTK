//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_rj_reason.h"
#include "valrules.h"


//>>===========================================================================		

ACSE_RJ_REASON_CLASS::ACSE_RJ_REASON_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
	nameM = "Reason";
	quotedValueM = false;
} 

//>>===========================================================================		

ACSE_RJ_REASON_CLASS::~ACSE_RJ_REASON_CLASS()

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

bool ACSE_RJ_REASON_CLASS::checkSyntax()

//  DESCRIPTION     : Check parameter syntax.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// check parameter syntax
	return checkIntegerSyntax(1);
} 

//>>===========================================================================		

bool ACSE_RJ_REASON_CLASS::checkRange()

//  DESCRIPTION     : Check parameter range.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	bool result = true;
	
	// check parameter range
	int value = atoi(valueM.c_str());
	switch(value)
	{
	case NO_REASON_GIVEN:
		// case TEMPORARY_CONGESTION:
	case APPLICATION_CONTEXT_NAME_NOT_SUPPORTED:
		// case PROTOCOL_VERSION_NOT_SUPPORTED:
		// case LOCAL_LIMIT_EXCEEDED:
	case CALLING_AE_TITLE_NOT_RECOGNISED:
	case CALLED_AE_TITLE_NOT_RECOGNISED:
		break;
	default:
		addMessage(VAL_RULE_D_PARAM_3, "Value %d out of range", value);
		result = false;
		break;
	}
	
	// return result
	return result;
} 

//>>===========================================================================		

bool ACSE_RJ_REASON_CLASS::checkReference(string refValue)

//  DESCRIPTION     : Check parameter against reference value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// check parameter against reference value
	return checkIntegerReference(refValue);
} 
