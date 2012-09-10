//*****************************************************************************
//  FILENAME        :	ConditionDefinition.cpp
//  PACKAGE         :	DVT
//  COMPONENT       :	DEFINITION
//  DESCRIPTION     :	Condition Definition Class
//  COPYRIGHT(c)    :   2000, Philips Electronics N.V.
//                      2000, Agfa Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ConditionDefinition.h"
#include "ConditionNode.h"
#include "Idicom.h"			// DICOM component interface


//>>===========================================================================

DEF_CONDITION_CLASS::DEF_CONDITION_CLASS() 

//  DESCRIPTION     : Default constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
    nodeM_ptr = NULL;
}

//>>===========================================================================

DEF_CONDITION_CLASS::~DEF_CONDITION_CLASS()

//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
	if (nodeM_ptr)
	{
		delete nodeM_ptr;
	}
}

//>>===========================================================================

bool DEF_CONDITION_CLASS::Evaluate(DCM_ATTRIBUTE_GROUP_CLASS* obj1_ptr, 
								DCM_ATTRIBUTE_GROUP_CLASS* obj2_ptr,
								LOG_CLASS* logger_ptr)

//  DESCRIPTION     : Evaluates condition
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = false;

	if (nodeM_ptr != NULL)
	{
		result = nodeM_ptr->Evaluate(obj1_ptr, obj2_ptr, logger_ptr);
	}

	return result;
}

//>>===========================================================================

string DEF_CONDITION_CLASS::GetResultMessage()

//  DESCRIPTION     : Returns overall message for this condition
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	string resultMessage;

	if (nodeM_ptr != NULL)
	{
		resultMessage = nodeM_ptr->GetResultMessage(); 
	}

	return resultMessage;
}
