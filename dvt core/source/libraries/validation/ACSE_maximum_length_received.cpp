//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_maximum_length_received.h"
#include "valrules.h"


//>>===========================================================================		

ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS::ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
	nameM = "Maximum Length Received";
	quotedValueM = false;
} 

//>>===========================================================================		

ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS::~ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS()

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

bool ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS::checkSyntax()

//  DESCRIPTION     : Check parameter syntax.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// check parameter syntax
	return checkIntegerSyntax(8);
} 

//>>===========================================================================		

bool ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS::checkRange()

//  DESCRIPTION     : Check parameter range.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// check parameter range
	// - just flag if value is zero
	if (atoi(valueM.c_str()) == 0)
	{
		addMessage(VAL_RULE_D_PARAM_5, "Unlimited maximum PDU length specified");
	}
	
	// return result
	return true;
} 

//>>===========================================================================		

bool ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS::checkReference(string refValue)

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
