//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Base value results class for validation.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "val_base_value.h"
#include "IAttributeGroup.h"    // AttributeGroup component interface file.

//>>===========================================================================

VAL_BASE_VALUE_CLASS::VAL_BASE_VALUE_CLASS()

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    messagesM_ptr = NULL;
    parentM_ptr = NULL;
    dcmValueM_ptr = NULL;
    defValueM_ptr = NULL;
    refValueM_ptr = NULL;
}

//>>===========================================================================

VAL_BASE_VALUE_CLASS::~VAL_BASE_VALUE_CLASS()

//  DESCRIPTION     : Class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    delete messagesM_ptr;
}

//>>===========================================================================

void VAL_BASE_VALUE_CLASS::SetParent(VAL_ATTRIBUTE_CLASS *parent_ptr)

//  DESCRIPTION     : Set parent.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    parentM_ptr = parent_ptr;
}

//>>===========================================================================

void VAL_BASE_VALUE_CLASS::SetDefValueList(VALUE_LIST_CLASS *defValueList_ptr)

//  DESCRIPTION     : Set the definition attribute value list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    defValueM_ptr = defValueList_ptr;
}

//>>===========================================================================

void VAL_BASE_VALUE_CLASS::SetRefValue(BASE_VALUE_CLASS *refValue_ptr)

//  DESCRIPTION     : Set the reference attribute value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    refValueM_ptr = refValue_ptr;
}

//>>===========================================================================

void VAL_BASE_VALUE_CLASS::SetDcmValue(BASE_VALUE_CLASS *dcmValue_ptr)

//  DESCRIPTION     : Set the dicom attribute value
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    dcmValueM_ptr = dcmValue_ptr;
}

//>>===========================================================================

VALUE_LIST_CLASS *VAL_BASE_VALUE_CLASS::GetDefValueList()

//  DESCRIPTION     : Get the definition attribute value list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return defValueM_ptr;
}

//>>===========================================================================

BASE_VALUE_CLASS *VAL_BASE_VALUE_CLASS::GetRefValue()

//  DESCRIPTION     : Get the reference attribute value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return refValueM_ptr;
}

//>>===========================================================================

BASE_VALUE_CLASS *VAL_BASE_VALUE_CLASS::GetDcmValue()

//  DESCRIPTION     : Get the dicom attribute value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return dcmValueM_ptr;
}

//>>===========================================================================

DVT_STATUS VAL_BASE_VALUE_CLASS::Check(UINT32 flags, SPECIFIC_CHARACTER_SET_CLASS *specificCharacterSet_ptr)

//  DESCRIPTION     : Check the dicom attribute value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_NO_VALUE;;

    if (dcmValueM_ptr != NULL)
    {
        LOG_MESSAGE_CLASS *logMessage_ptr = new LOG_MESSAGE_CLASS();

        status = dcmValueM_ptr->Check(flags, NULL, logMessage_ptr, specificCharacterSet_ptr);
        if (logMessage_ptr->GetNrMessages() > 0)
        {
            for (int i = 0; i < logMessage_ptr->GetNrMessages(); i++)
            {
                GetMessages()->AddMessage(logMessage_ptr->GetMessageId(i), logMessage_ptr->GetMessage(i));
            }
        }
        delete logMessage_ptr;
    }

    return status;
}

//>>===========================================================================

LOG_MESSAGE_CLASS *VAL_BASE_VALUE_CLASS::GetMessages()

//  DESCRIPTION     : Get the log messages.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (messagesM_ptr == NULL)
    {
        messagesM_ptr = new LOG_MESSAGE_CLASS();
    }

    return messagesM_ptr;
}

//>>===========================================================================

bool VAL_BASE_VALUE_CLASS::HasMessages()

//  DESCRIPTION     : Check if the value has log messages.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (messagesM_ptr == NULL) ? false : true;
}

//>>===========================================================================

DVT_STATUS VAL_BASE_VALUE_CLASS::CompareRef()

//  DESCRIPTION     :
//  PRECONDITIONS   : The ref and dcm values are both available.
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    LOG_MESSAGE_CLASS *logMessage_ptr = new LOG_MESSAGE_CLASS();

    DVT_STATUS status = dcmValueM_ptr->Compare(logMessage_ptr, refValueM_ptr);
        
    if (logMessage_ptr->GetNrMessages() > 0)
    {
        for (int i = 0; i < logMessage_ptr->GetNrMessages(); i++)
        {
            GetMessages()->AddMessage(logMessage_ptr->GetMessageId(i), logMessage_ptr->GetMessage(i));
        }
    }
    delete logMessage_ptr;

    if ((status != MSG_OK) && 
        (status != MSG_EQUAL))
    {
        char message[1024];
        string dcmStringValue;
        string refStringvalue;

        dcmValueM_ptr->Get(dcmStringValue);
        refValueM_ptr->Get(refStringvalue);

        if ((dcmStringValue.length() + refStringvalue.length()) < 1024)
		{
			sprintf(message,
					"Value of \"%s\" different from reference value of \"%s\"",
					dcmStringValue.c_str(), 
                    refStringvalue.c_str());
		}
		else
		{
			sprintf(message,
					"Streamed DICOM value is different from reference value of attribute");
		}

        GetMessages()->AddMessage(VAL_RULE_R_VALUE_1, message);
    }

    return status;
}
