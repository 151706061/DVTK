//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_ac_result.h"
#include "valrules.h"


//>>===========================================================================		

ACSE_AC_RESULT_CLASS::ACSE_AC_RESULT_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
	nameM = " Result/Reason";
	quotedValueM = false;
} 

//>>===========================================================================		

ACSE_AC_RESULT_CLASS::~ACSE_AC_RESULT_CLASS()

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

bool ACSE_AC_RESULT_CLASS::checkSyntax()

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

bool ACSE_AC_RESULT_CLASS::checkRange()

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
	case ACCEPTANCE:
	case USER_REJECTION:
	case NO_REASON:
	case ABSTRACT_SYNTAX_NOT_SUPPORTED:
	case TRANSFER_SYNTAXES_NOT_SUPPORTED:
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

bool ACSE_AC_RESULT_CLASS::checkReference(string refValue)

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
