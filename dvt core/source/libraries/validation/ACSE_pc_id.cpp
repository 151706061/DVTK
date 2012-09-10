//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_pc_id.h"
#include "Iglobal.h"		// Global component interface
#include "valrules.h"


//>>===========================================================================		

ACSE_PC_ID_CLASS::ACSE_PC_ID_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
	nameM = " Presentation Context ID";
	quotedValueM = false;
} 

//>>===========================================================================		

ACSE_PC_ID_CLASS::~ACSE_PC_ID_CLASS()

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

bool ACSE_PC_ID_CLASS::checkSyntax()

//  DESCRIPTION     : Check parameter syntax.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// check parameter syntax
	bool result = checkIntegerSyntax(3);
	if (result)
	{
		// check that the value is odd
		int value = atoi((char*) valueM.c_str());
		if (!(value & 1))
		{
			addMessage(VAL_RULE_D_PARAM_4, "Value must be odd", valueM.c_str());
			result = false;
		}
	}
	
	// return result
	return result;
} 

//>>===========================================================================		

bool ACSE_PC_ID_CLASS::checkRange()

//  DESCRIPTION     : Check parameter range.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	bool result = true;
	
	// check parameter range
	if (atoi(valueM.c_str()) > 255)
	{
		addMessage(VAL_RULE_D_PARAM_3, "Value %s out of range", valueM.c_str());
		result = false;
	}
	
	// return result
	return result;
} 

//>>===========================================================================		

bool ACSE_PC_ID_CLASS::checkReference(string refValue)

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
