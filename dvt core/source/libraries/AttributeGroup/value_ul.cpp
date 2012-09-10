// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "value_ul.h"


//>>===========================================================================

VALUE_UL_CLASS::VALUE_UL_CLASS()

//  DESCRIPTION     : Default constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
}

//>>===========================================================================

VALUE_UL_CLASS::~VALUE_UL_CLASS()

//  DESCRIPTION     : Default destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
}

//>>===========================================================================

bool VALUE_UL_CLASS::operator = (BASE_VALUE_CLASS &value)

//  DESCRIPTION     : Equal operator.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (value.GetVRType() == ATTR_VR_UL)
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

ATTR_VR_ENUM VALUE_UL_CLASS::GetVRType (void)

//  DESCRIPTION     : Get VR.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return (ATTR_VR_UL);
}

//>>===========================================================================

DVT_STATUS VALUE_UL_CLASS::Compare (LOG_MESSAGE_CLASS*, BASE_VALUE_CLASS * ref_value)

//  DESCRIPTION     : Compare values.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    UINT32      ref_ul;

    if (ref_value == NULL) return MSG_OK;

    if (ref_value->GetVRType() != ATTR_VR_UL)
    {
        return (MSG_INCOMPATIBLE);
    }

    ref_value->Get(ref_ul);

    if (valueM == ref_ul)
    {
        return (MSG_EQUAL);
    }
    if (valueM > ref_ul)
    {
        return (MSG_GREATER);
    }
    if (valueM < ref_ul)
    {
        return (MSG_SMALLER);
    }

    // This line should never be executed.
    return (MSG_NOT_EQUAL);
}

//>>===========================================================================

DVT_STATUS VALUE_UL_CLASS::Check (UINT32,
                                  BASE_VALUE_CLASS **,
                                  LOG_MESSAGE_CLASS *,
                                  SPECIFIC_CHARACTER_SET_CLASS *)

//  DESCRIPTION     : Check UL format.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return (MSG_OK);
}

//>>===========================================================================

DVT_STATUS VALUE_UL_CLASS::Set (string value)

//  DESCRIPTION     : Set value from string.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    sscanf (value.c_str(), "%ld", &valueM);
    return (MSG_OK);
}

//>>===========================================================================

DVT_STATUS VALUE_UL_CLASS::Set (UINT32 value)

//  DESCRIPTION     : Set value from UINT32.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    valueM = value;
    return (MSG_OK);
}

//>>===========================================================================

DVT_STATUS VALUE_UL_CLASS::Get (string &value, bool)

//  DESCRIPTION     : Get value as UINT32.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    char    str_value[32];

    sprintf (str_value, "%ld", valueM);
    value = str_value;

    return (MSG_OK);
}

//>>===========================================================================

DVT_STATUS VALUE_UL_CLASS::Get (UINT32 &value)

//  DESCRIPTION     : Get value as UINT32.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    value = valueM;
    return (MSG_OK);
}

//>>===========================================================================

UINT32 VALUE_UL_CLASS::GetLength (void)

//  DESCRIPTION     : Get length.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return sizeof(valueM);
}
