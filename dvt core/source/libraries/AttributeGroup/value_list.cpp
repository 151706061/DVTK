// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "value_list.h"
#include "base_value.h"

//>>===========================================================================

VALUE_LIST_CLASS::VALUE_LIST_CLASS()

//  DESCRIPTION     : Default constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    value_typeM = ATTR_VAL_TYPE_NOVALUE;
    valuesM.clear();
}

//>>===========================================================================

VALUE_LIST_CLASS::~VALUE_LIST_CLASS()

//  DESCRIPTION     : Default destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT index=0 ; index<valuesM.size() ; index++)
    {
        delete (valuesM[index]);
    }
    valuesM.clear();
}

//>>===========================================================================

int VALUE_LIST_CLASS::GetNrValues(void)

//  DESCRIPTION     : Get number of values in list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return (valuesM.size());
}

//>>===========================================================================

BASE_VALUE_CLASS* VALUE_LIST_CLASS::GetValue(int index)

//  DESCRIPTION     : Get indexed value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (index >= 0);

    if (valuesM.size() <= (unsigned int) index)
    {
        // The requested value at index does not exist
        return (NULL);
    }
    return (valuesM.at(index));
}

//>>===========================================================================

ATTR_VAL_TYPE_ENUM VALUE_LIST_CLASS::GetValueType(void)

//  DESCRIPTION     : Get value type.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return (value_typeM);
}

//>>===========================================================================

void VALUE_LIST_CLASS::SetValueType(ATTR_VAL_TYPE_ENUM val_type)

//  DESCRIPTION     : Set value type.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    value_typeM = val_type;
}

//>>===========================================================================

DVT_STATUS VALUE_LIST_CLASS::AddValue(BASE_VALUE_CLASS *value)

//  DESCRIPTION     : Add value to list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    // There is no sanity check done on the argument value.
    // Since this class is only called from within the AttributeGroup
    // component, any sanity check has to be done by the calling function.
    valuesM.push_back (value);

    return (MSG_OK);
}

//>>===========================================================================

DVT_STATUS VALUE_LIST_CLASS::Replace (BASE_VALUE_CLASS *value, int index)

//  DESCRIPTION     : Replace value in list at given index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (index >= 0);

    if (valuesM.size()-1 < (unsigned int) index)
    {
        return (MSG_OUT_OF_BOUNDS);
    }

    delete (valuesM[index]);
    valuesM[index] = value;

    return (MSG_OK);
}

//>>===========================================================================

bool VALUE_LIST_CLASS::HasValue (BASE_VALUE_CLASS * value)

//  DESCRIPTION     : Check if given value exists in list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    bool    in_list = false;
    int     i = 0;

    while ((i<GetNrValues()) && 
		(in_list == false))
    {
        DVT_STATUS      rval;
        rval = value->Compare (NULL, GetValue (i));
        if ((rval == MSG_OK) || 
			(rval == MSG_EQUAL))
		{
            in_list = true;
		}
        i++;
    }
    return in_list;
}
