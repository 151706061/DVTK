// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "value_da.h"

//>>===========================================================================

VALUE_DA_CLASS::VALUE_DA_CLASS()

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

VALUE_DA_CLASS::~VALUE_DA_CLASS()

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

bool VALUE_DA_CLASS::operator = (BASE_VALUE_CLASS &value)

//  DESCRIPTION     : Equal operator.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (value.GetVRType() == ATTR_VR_DA)
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

DVT_STATUS VALUE_DA_CLASS::Check(UINT32 flags,
                                 BASE_VALUE_CLASS **,
                                 LOG_MESSAGE_CLASS *messages,
                                 SPECIFIC_CHARACTER_SET_CLASS *)

//  DESCRIPTION     : Check DA format.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS  result = MSG_OK;
    UINT        length = valueM.length();
    UINT        index;
    bool        range;
    string      date1;
    string      date2;
    string      conv_date1;
    string      conv_date2;
    int         date1_index;
    int         date2_index;
    bool        date1_old_format;
    bool        date2_old_format;
    char        message[100];

    if (length == 0)
        return (MSG_OK);

    if (length > DA_LENGTH)
    {
        sprintf (message, "value length %i exceeds maximum length %i - truncated",
                 length, DA_LENGTH);
        messages->AddMessage (VAL_RULE_D_DA_1, message);
        length = DA_LENGTH;
        result= MSG_ERROR;
    }

    date1_index = -1;
    date2_index = -1;
    date1_old_format = false;
    date2_old_format = false;
    range = false;

    for (index=0 ; index < length ; index++)
    {
        if ((valueM[index] >= '0') && (valueM[index] <= '9'))
        {
            if (range == false)
            {
                // We don't know if we're parsing a range. We do know this is
                // the start date in case of a range.
                date1 += valueM[index];

                if (date1_index < 0)
                {
                    // store the index at which the first date starts
                    date1_index = index;
                }
            }
            else
            {
                // We're parsing a date range. The date to parse now is the
                // end date of the range.
                date2 += valueM[index];

                if (date2_index < 0)
                {
                    // store the index at which the second date starts
                    date2_index = index;
                }
            }
        }
        else
        {
            switch (valueM[index])
            {
            case '.':
                if (range == false)
                {
                    if (date1_index < 0)
                    {
                        // '.' is not allowed as a starting character
                        sprintf (message, "period '.' not allowed at position %d",
                                 index);
                        messages->AddMessage (VAL_RULE_D_DA_2, message);
                        return MSG_ERROR;
                    }
                    date1 += '.';
                    date1_old_format = true;
                }
                else
                {
                    if (date2_index < 0)
                    {
                        // '.' is not allowed as a starting character
                        sprintf (message, "period '.' not allowed at position %d",
                                index);
                        messages->AddMessage (VAL_RULE_D_DA_2, message);
                        return MSG_ERROR;
                    }
                    date2 += '.';
                    date2_old_format = true;
                }
                break;
            case '-':
                if (!(flags & ATTR_FLAG_RANGES_ALLOWED))
                {
                    sprintf (message, "date range not allowed");
                    messages->AddMessage (VAL_RULE_D_DA_3, message);
                    return MSG_ERROR;
                }
                if (range == true)
                {
                    sprintf (message, "more than one '-' Character found at position %d",
                            index);
                    messages->AddMessage (VAL_RULE_D_DA_7, message);
                    return MSG_ERROR;
                }
                range = true;
                break;
            case ' ':
                if ((index + 1 < length) || // A space char can only occur at the end of the time string.
                    ((index + 1) % 2 != 0)) // A space char may only be used for padding
                {
                    sprintf (message, "unexpected SPACE Character at offset %d",
                            index);
                    messages->AddMessage (VAL_RULE_D_DA_2, message);
                    return MSG_ERROR;
                }
                break;
			case 0x00:
                sprintf (message, "unexpected Character [NUL]=0x00 at offset %d",
                         index);
                messages->AddMessage (VAL_RULE_D_DA_2, message);
				return MSG_ERROR;
			case 0x0a: // Fall through
			case 0x0d:
                sprintf (message, "unexpected Character 0x%02X at offset %d",
                         (int)(valueM[index]), index);
                messages->AddMessage (VAL_RULE_D_DA_2, message);
				return MSG_ERROR;
            default:
                sprintf (message, "unexpected Character %c=0x%02X at offset %d",
                         valueM[index], (int)(valueM[index]), index);
                messages->AddMessage (VAL_RULE_D_DA_2, message);
                return MSG_ERROR;
            }
        }
    }

    if ((date1_old_format == true) || 
		(date2_old_format == true))
    {
        sprintf (message, "old style date format - 'YYYY.MM.DD' should not be used");
        messages->AddMessage (VAL_RULE_D_DA_6, message);
    }

    // convert the dates from the old format to the new format. This can be
    // even if the dates are already in the new format.
    ConvertOldDate (date1, conv_date1);
    ConvertOldDate (date2, conv_date2);

    if (IsDate (conv_date1, date1_index, messages) != MSG_OK)
    {
        result = MSG_ERROR;
    }

    if (range == true)
    {
        if (IsDate (conv_date2, date2_index, messages) != MSG_OK)
        {
            result = MSG_ERROR;
        }

        if (result == MSG_OK)
        {
            if ((conv_date2.length() > 0) && 
                (conv_date1 > conv_date2))
            {
                sprintf (message, "start date is later than end date in range");
                messages->AddMessage (VAL_RULE_D_DA_8, message);
                result = MSG_ERROR;
            }
        }
    }

    return result;
}

//>>===========================================================================

DVT_STATUS VALUE_DA_CLASS::Compare(LOG_MESSAGE_CLASS*, BASE_VALUE_CLASS *ref_value)

//  DESCRIPTION     : Compare values.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    string          ref;
    DVT_STATUS         rval;

    if (ref_value == NULL) return MSG_OK;

    if (ref_value->GetVRType() != ATTR_VR_DA)
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
    rval = CompareStringValues (ref, true, false);

    ref.erase();
    return (rval);
}

//>>===========================================================================

DVT_STATUS VALUE_DA_CLASS::Compare(string value)

//  DESCRIPTION     : Compare to string value.
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
    rval = CompareStringValues (value, true, false);

    return (rval);
}

//>>===========================================================================

ATTR_VR_ENUM VALUE_DA_CLASS::GetVRType(void)

//  DESCRIPTION     : Get VR.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return (ATTR_VR_DA);
}

//>>===========================================================================

DVT_STATUS VALUE_DA_CLASS::IsDate(string date, int, LOG_MESSAGE_CLASS * messages)

//  DESCRIPTION     : Check if date.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    int         year;
    int         month;
    int         day;
    char        message[100];

    switch (date.length())
    {
    case 8:
        year = GetNumeric(date, 4);
        month = GetNumeric(&date[4], 2);
        day = GetNumeric(&date[6], 2);

        if ( IsDateValid(year, month, day) != true) 
        {
            sprintf (message, "invalid date");
            messages->AddMessage (VAL_RULE_D_DA_5, message);
            return (MSG_ERROR);
        }
        break;

    case 0:
        break;

    default:
        sprintf (message, "invalid date format - expected 'YYYYMMDD'");
        messages->AddMessage (VAL_RULE_D_DA_2, message);
        return (MSG_ERROR);
    }
    return (MSG_OK);
}

//>>===========================================================================

void VALUE_DA_CLASS::ConvertOldDate(string date, string& conv_date)

//  DESCRIPTION     : Convert old date format to new.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT index=0 ; index<date.length() ; index++)
    {
        if (date[index] != '.')
        {
            conv_date += date[index];
        }
    }
}

//>>===========================================================================

string VALUE_DA_CLASS::GetStripped(void)

//  DESCRIPTION     : Get stripped string.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    string  stripped;
    StringStrip (valueM, valueM.length(), true, false, stripped);

    return stripped;
}
