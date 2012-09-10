// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "value_is.h"

//>>===========================================================================

VALUE_IS_CLASS::VALUE_IS_CLASS()

//  DESCRIPTION     : Default constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    valueM.erase();
}

//>>===========================================================================

VALUE_IS_CLASS::~VALUE_IS_CLASS()

//  DESCRIPTION     : Default destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    valueM.erase();
}

//>>===========================================================================

bool VALUE_IS_CLASS::operator = (BASE_VALUE_CLASS &value)

//  DESCRIPTION     : Equal operator.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (value.GetVRType() == ATTR_VR_IS)
    {
        value.Get(valueM);
        return (true);
    }
    else
    {
        return (false);
    }
}

//>>===========================================================================

DVT_STATUS VALUE_IS_CLASS::Check (UINT32,
                                  BASE_VALUE_CLASS **,
                                  LOG_MESSAGE_CLASS *messages,
                                  SPECIFIC_CHARACTER_SET_CLASS *)

//  DESCRIPTION     : Check IS format.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    int         length  = valueM.length();
    DVT_STATUS  result = MSG_OK;
    int         index;
    bool        integer_part=false;
    long        digit;
    long        val = 0;
    char        message[100];

    if (length == 0)
        return (MSG_OK);

    // check length
    if (length > IS_LENGTH)
    {
        sprintf (message, "value length %i exceeds maximum length %i - truncated",
                length, IS_LENGTH);
        messages->AddMessage (VAL_RULE_D_IS_1, message);
        length = IS_LENGTH;
        result = MSG_ERROR;
    }

    if (length > 0)
    {
        index = 0;
        // Skip optional leading spaces.
        while (valueM[index] == SPACECHAR)
        {
            index++;
        }

        // Optional sign.
        if ((valueM[index] == '+') || (valueM[index] == '-'))
        {
            index++;
        }

        while ((valueM[index] >= '0') && (valueM[index] <= '9'))
        {
            integer_part = true;
            digit = valueM[index] - '0';

            if ((val < 214748364) || ((val == 214748364) && (digit <= 7)))
            {
                val = 10 * val + digit;
            }
            else
            {
                sprintf (message, "\"%s\" out of specified range -(2^31 - 1)..(2^31 - 1)",
                         valueM.c_str());
                messages->AddMessage (VAL_RULE_D_IS_3, message);
                return (MSG_ERROR);
            }
            index++;
        }

        // Skip optional trailing spaces.
        while (valueM[index] == SPACECHAR)
        {
            index++;
        }

        // Check end of string.
        if (valueM[index] != NULLCHAR)
        {
            sprintf (message, "unexpected Character %c=0x%02X at offset %d",
                    valueM[index], valueM[index], index);
            messages->AddMessage (VAL_RULE_D_IS_2, message);
            result = MSG_ERROR;
        }

        // Check for integer part.
        if (integer_part == false)
        {
            sprintf (message, "no integer digits found");
            messages->AddMessage (VAL_RULE_D_IS_2, message);
            result = MSG_ERROR;
        }
    }

    return result;
}

//>>===========================================================================

DVT_STATUS VALUE_IS_CLASS::Compare(LOG_MESSAGE_CLASS*, BASE_VALUE_CLASS *ref_value)

//  DESCRIPTION     : Compare values.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    string          ref;
    DVT_STATUS      rval = MSG_OK;

    if (ref_value == NULL) return MSG_OK;

    if (ref_value->GetVRType() != ATTR_VR_IS)
    {
        return (MSG_INCOMPATIBLE);
    }

    rval = ref_value->Get(ref);

    if (rval != MSG_OK)
    {
        ref.erase();
        return (rval);
    }

    // Check for zero length
    if ((valueM.length() == 0) && (ref.length() == 0))
    {
        // values considered the same
        return (MSG_EQUAL);
    }

    // We know we have data, and we have the reference value. Now compare these.
    rval = CompareStringValues (ref, false, false);

    ref.erase();
    return (rval);
}

//>>===========================================================================

DVT_STATUS VALUE_IS_CLASS::Compare(string value)

//  DESCRIPTION     : Compare value to string.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS         rval;

    // Check for zero length
    if ((valueM.length() == 0) && (value.length() == 0))
    {
        // values considered the same
        return (MSG_EQUAL);
    }

    // We know we have data, and we have the reference value. Now compare these.
    rval = CompareStringValues (value, false, false);

    return (rval);
}

//>>===========================================================================

ATTR_VR_ENUM VALUE_IS_CLASS::GetVRType(void)

//  DESCRIPTION     : Get VR.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return (ATTR_VR_IS);
}

//>>===========================================================================

DVT_STATUS VALUE_IS_CLASS::Get(INT32 & value)

//  DESCRIPTION     : Get value as INT32.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    value = (INT32) atoi (valueM.c_str());
    return (MSG_OK);
}

//>>===========================================================================

string VALUE_IS_CLASS::GetStripped(void)

//  DESCRIPTION     : Get stripped string.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    string  stripped;
    StringStrip (valueM, valueM.length(), false, false, stripped);

    return stripped;
}
