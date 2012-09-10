//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "val_value_sq.h"
#include "val_attribute_group.h"


//>>===========================================================================

VAL_VALUE_SQ_CLASS::VAL_VALUE_SQ_CLASS()

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
}

//>>===========================================================================

VAL_VALUE_SQ_CLASS::~VAL_VALUE_SQ_CLASS()

//  DESCRIPTION     : Class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
}


//>>===========================================================================

void VAL_VALUE_SQ_CLASS::AddValItem(VAL_ATTRIBUTE_GROUP_CLASS *valItem_ptr)

//  DESCRIPTION     : Add an item to this validation SQ value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    valItemsM.push_back(valItem_ptr);
}

//>>===========================================================================

VAL_ATTRIBUTE_GROUP_CLASS *VAL_VALUE_SQ_CLASS::GetValItem (int index)

//  DESCRIPTION     : Get the indexed item from this validation SQ value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (index >= 0);

    VAL_ATTRIBUTE_GROUP_CLASS *valItem_ptr = NULL;
    if (valItemsM.size() > (unsigned int)index)
    {
        valItem_ptr = valItemsM[index];
    }

    return valItem_ptr;
}

//>>===========================================================================

int VAL_VALUE_SQ_CLASS::GetNrValItems()

//  DESCRIPTION     : Get the number of items in this validation SQ value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return valItemsM.size();
}

//>>===========================================================================

DVT_STATUS VAL_VALUE_SQ_CLASS::Check(UINT32 flags, SPECIFIC_CHARACTER_SET_CLASS *specificCharacterSet_ptr)

//  DESCRIPTION     : Check the items VR in this validation SQ value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;

    for (UINT i = 0; i < valItemsM.size(); i++)
    {
        DVT_STATUS status2 = valItemsM[i]->ValidateVR(flags, specificCharacterSet_ptr);
        if (status2 != MSG_OK)
        {
            status = status2;
        }
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_VALUE_SQ_CLASS::ValidateAgainstDef(UINT32 flags)

//  DESCRIPTION     : Validate the items against the definition.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;

    for (UINT i = 0; i < valItemsM.size(); i++)
    {
        DVT_STATUS status2 = valItemsM[i]->ValidateAgainstDef(flags);
        if (status2 != MSG_OK)
        {
            status = status2;
        }
    }
    return status;
}

//>>===========================================================================

DVT_STATUS VAL_VALUE_SQ_CLASS::ValidateAgainstRef(UINT32 flags)

//  DESCRIPTION     : Validate the items against the reference.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;
    
    for (UINT i = 0; i < valItemsM.size(); i++)
    {
        DVT_STATUS status2 = valItemsM[i]->ValidateAgainstRef(flags);
        if (status2 != MSG_OK)
        {
            status = status2;
        }
    }

    return status;
}
