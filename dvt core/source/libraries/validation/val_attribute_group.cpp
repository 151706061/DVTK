//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Attribute group results class for validation.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "val_attribute_group.h"
#include "val_attribute.h"
#include "IAttributeGroup.h"    // AttributeGroup component interface file.
#include "Idefinition.h"        // Definition component interface file.
#include "Idicom.h"             // Dicom component interface file.

//>>===========================================================================

VAL_ATTRIBUTE_GROUP_CLASS::VAL_ATTRIBUTE_GROUP_CLASS()

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    messagesM_ptr = NULL;
    parentObjectM_ptr = NULL;
    parentSqM_ptr = NULL;
    dcmAttributeGroupM_ptr = NULL;
    defAttributeGroupM_ptr = NULL;
    refAttributeGroupM_ptr = NULL;
}

//>>===========================================================================

VAL_ATTRIBUTE_GROUP_CLASS::~VAL_ATTRIBUTE_GROUP_CLASS()

//  DESCRIPTION     : Class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    delete messagesM_ptr;

    for (UINT i = 0; i < attributesM.size(); i++)
    {
        delete attributesM[i];
    }
    attributesM.clear();
}

//>>===========================================================================

void VAL_ATTRIBUTE_GROUP_CLASS::SetParentObject(VAL_OBJECT_RESULTS_CLASS *parent_ptr)

//  DESCRIPTION     : Set the parent.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    parentObjectM_ptr = parent_ptr;
}

//>>===========================================================================

void VAL_ATTRIBUTE_GROUP_CLASS::SetParentSQ(VAL_VALUE_SQ_CLASS *parentSq_ptr)

//  DESCRIPTION     : Set the parent Sequence.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    parentSqM_ptr = parentSq_ptr;
}

//>>===========================================================================

VAL_OBJECT_RESULTS_CLASS *VAL_ATTRIBUTE_GROUP_CLASS::GetParentObject()

//  DESCRIPTION     : Get the parent.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return parentObjectM_ptr;
}

//>>===========================================================================

void VAL_ATTRIBUTE_GROUP_CLASS::SetDefAttributeGroup(DEF_ATTRIBUTE_GROUP_CLASS *defAttrGroup_ptr)

//  DESCRIPTION     : Set the definition attribute group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    defAttributeGroupM_ptr = defAttrGroup_ptr;
}

//>>===========================================================================

void VAL_ATTRIBUTE_GROUP_CLASS::SetRefAttributeGroup(DCM_ATTRIBUTE_GROUP_CLASS *refAttrGroup_ptr)

//  DESCRIPTION     : Set the reference attribute group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    refAttributeGroupM_ptr = refAttrGroup_ptr;
}

//>>===========================================================================

void VAL_ATTRIBUTE_GROUP_CLASS::SetDcmAttributeGroup(DCM_ATTRIBUTE_GROUP_CLASS *dcmAttrGroup_ptr)

//  DESCRIPTION     : Set the dicom attribute group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    dcmAttributeGroupM_ptr = dcmAttrGroup_ptr;
}

//>>===========================================================================

DEF_ATTRIBUTE_GROUP_CLASS *VAL_ATTRIBUTE_GROUP_CLASS::GetDefAttributeGroup()

//  DESCRIPTION     : Get the definition attribute group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return defAttributeGroupM_ptr;
}

//>>===========================================================================

DCM_ATTRIBUTE_GROUP_CLASS * VAL_ATTRIBUTE_GROUP_CLASS::GetRefAttributeGroup()

//  DESCRIPTION     : Get the reference attribute group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return refAttributeGroupM_ptr;
}

//>>===========================================================================

DCM_ATTRIBUTE_GROUP_CLASS *VAL_ATTRIBUTE_GROUP_CLASS::GetDcmAttributeGroup()

//  DESCRIPTION     : Get the dicom attribute group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return dcmAttributeGroupM_ptr;
}

//>>===========================================================================

bool VAL_ATTRIBUTE_GROUP_CLASS::AddValAttribute(VAL_ATTRIBUTE_CLASS *valAttr_ptr)

//  DESCRIPTION     : Add a validation attribute to the group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (valAttr_ptr == NULL)
    {
        return false;
    }

    attributesM.push_back(valAttr_ptr);

    return true;
}

//>>===========================================================================

int VAL_ATTRIBUTE_GROUP_CLASS::GetNrAttributes()

//  DESCRIPTION     : Return the number of validation attributes in the group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return attributesM.size();
}

//>>===========================================================================

VAL_ATTRIBUTE_CLASS *VAL_ATTRIBUTE_GROUP_CLASS::GetAttribute(int index)

//  DESCRIPTION     : Get the indexed validation attribute from the group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (index >= 0);

    VAL_ATTRIBUTE_CLASS *valAttr_ptr = NULL;
    if ((unsigned int) index < attributesM.size())
    {
        valAttr_ptr = attributesM[index];
    }
 
    return valAttr_ptr;
}

//>>===========================================================================

VAL_ATTRIBUTE_CLASS *VAL_ATTRIBUTE_GROUP_CLASS::GetAttribute(UINT16 group, UINT16 element)

//  DESCRIPTION     : This function returns the validation attribute with the
//                    group and element specified. If the requested attribute
//                    is not found, NULL will be returned.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT i = 0; i < attributesM.size(); i++)
    {
        ATTRIBUTE_CLASS *attr_ptr = attributesM[i]->GetDefAttribute();
        if (attr_ptr == NULL)
		{
            attr_ptr = attributesM[i]->GetDcmAttribute();
		}
        if (attr_ptr == NULL)
		{
            attr_ptr = attributesM[i]->GetRefAttribute();
		}

        if ((attr_ptr != NULL) &&
			(attr_ptr->GetMappedGroup() == group) &&
            (attr_ptr->GetMappedElement() == element))
        {
            return attributesM[i];
        }
    }

    return NULL;
}

//>>===========================================================================

VAL_ATTRIBUTE_CLASS *VAL_ATTRIBUTE_GROUP_CLASS::GetAttributeByTag(UINT32 tag)

//  DESCRIPTION     : This function returns the validation attribute with the
//                    group and element specified. If the requested attribute
//                    is not found, NULL will be returned.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return GetAttribute((UINT16)((tag & 0xFFFF0000) >> 16), (UINT16)(tag & 0x0000FFFF));
}

//>>===========================================================================

LOG_MESSAGE_CLASS *VAL_ATTRIBUTE_GROUP_CLASS::GetMessages()

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

bool VAL_ATTRIBUTE_GROUP_CLASS::HasMessages(void)

//  DESCRIPTION     : Check if the group has any log messages.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (messagesM_ptr == NULL) ? false : true;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_GROUP_CLASS::ValidateAgainstDef(UINT32 flags)

//  DESCRIPTION     : Validate the group against the definition group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT i = 0; i < attributesM.size(); i++)
    {
        attributesM[i]->ValidateAgainstDef(flags);
    }

    return MSG_OK;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_GROUP_CLASS::ValidateAgainstRef(UINT32 flags)

//  DESCRIPTION     : Validate the group against the reference group.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT i = 0; i < attributesM.size(); i++)
    {
        attributesM[i]->ValidateAgainstRef(flags);
    }

    return MSG_OK;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_GROUP_CLASS::ValidateVR(UINT32 flags, SPECIFIC_CHARACTER_SET_CLASS *specificCharacterSet_ptr)

//  DESCRIPTION     : Validate the group attribute VRs.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT i = 0; i < attributesM.size(); i++)
    {
        attributesM[i]->ValidateVR(flags, specificCharacterSet_ptr);
    }

    return MSG_OK;
}
