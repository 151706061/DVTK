//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Attribute results class for validation.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "val_attribute.h"
#include "val_attribute_group.h"
#include "val_base_value.h"
#include "val_value_sq.h"
#include "val_object_results.h"
#include "Idefinition.h"        // Definition component interface file.
#include "Idicom.h"             // Dicom component interface file.

//>>===========================================================================

VAL_ATTRIBUTE_CLASS::VAL_ATTRIBUTE_CLASS()

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    messagesM_ptr = NULL;
    parentM_ptr = NULL;
    dcmAttributeM_ptr = NULL;
    defAttributeM_ptr = NULL;
    refAttributeM_ptr = NULL;
    useConditionalTextDuringValidationM = false;
}

//>>===========================================================================

VAL_ATTRIBUTE_CLASS::~VAL_ATTRIBUTE_CLASS()

//  DESCRIPTION     : Class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    delete messagesM_ptr;

    for (UINT i = 0; i < valuesM.size(); i++)
    {
        delete valuesM[i];
    }
    valuesM.clear();
}

//>>===========================================================================

void VAL_ATTRIBUTE_CLASS::SetParent(VAL_ATTRIBUTE_GROUP_CLASS *parent_ptr)

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

VAL_ATTRIBUTE_GROUP_CLASS *VAL_ATTRIBUTE_CLASS::GetParent()

//  DESCRIPTION     : Get parent.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return parentM_ptr;
}

//>>===========================================================================

void VAL_ATTRIBUTE_CLASS::SetDefAttribute(DEF_ATTRIBUTE_CLASS *defAttr_ptr)

//  DESCRIPTION     : Set definition attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    defAttributeM_ptr = defAttr_ptr;
}

//>>===========================================================================

void VAL_ATTRIBUTE_CLASS::SetRefAttribute(DCM_ATTRIBUTE_CLASS *refAttr_ptr)

//  DESCRIPTION     : Set reference attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    refAttributeM_ptr = refAttr_ptr;
}

//>>===========================================================================

void VAL_ATTRIBUTE_CLASS::SetDcmAttribute(DCM_ATTRIBUTE_CLASS *dcmAttr_ptr)

//  DESCRIPTION     : Set dicom attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    dcmAttributeM_ptr = dcmAttr_ptr;
}

//>>===========================================================================
DEF_ATTRIBUTE_CLASS *VAL_ATTRIBUTE_CLASS::GetDefAttribute()

//  DESCRIPTION     : Get definition attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return defAttributeM_ptr;
}

//>>===========================================================================

DCM_ATTRIBUTE_CLASS *VAL_ATTRIBUTE_CLASS::GetRefAttribute()

//  DESCRIPTION     : Get reference attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return refAttributeM_ptr;
}

//>>===========================================================================

DCM_ATTRIBUTE_CLASS *VAL_ATTRIBUTE_CLASS::GetDcmAttribute()

//  DESCRIPTION     : Get dicom attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return dcmAttributeM_ptr;
}

//>>===========================================================================

bool VAL_ATTRIBUTE_CLASS::AddValValue(VAL_BASE_VALUE_CLASS *value_ptr)

//  DESCRIPTION     : Add attribute value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (value_ptr == NULL)
    {
        return false;
    }

    valuesM.push_back(value_ptr);

    return true;
}

//>>===========================================================================

int VAL_ATTRIBUTE_CLASS::GetNrValues()

//  DESCRIPTION     : Get number of attribute values present.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return valuesM.size();
}

//>>===========================================================================

VAL_BASE_VALUE_CLASS *VAL_ATTRIBUTE_CLASS::GetValue(int valueIndex)

//  DESCRIPTION     : Get indexed attribute value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (valueIndex >= 0);

    if ((unsigned int) valueIndex < valuesM.size())
    {
        return (valuesM[valueIndex]);
    }
    else
    {
        return NULL;
    }
}

//>>===========================================================================

LOG_MESSAGE_CLASS *VAL_ATTRIBUTE_CLASS::GetMessages()

//  DESCRIPTION     : Get log messages.
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

bool VAL_ATTRIBUTE_CLASS::HasMessages()

//  DESCRIPTION     : Check if attribute has validation messages.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (messagesM_ptr == NULL) ? false : true;
}

//>>===========================================================================

void VAL_ATTRIBUTE_CLASS::SetUseConditionalTextDuringValidation(bool value)

//  DESCRIPTION     : Set the useConditionalTextDuringValidationM flag to the given value.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    useConditionalTextDuringValidationM = value;
}

//>>===========================================================================

bool VAL_ATTRIBUTE_CLASS::GetUseConditionalTextDuringValidation()

//  DESCRIPTION     : Get the useConditionalTextDuringValidationM flag.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return useConditionalTextDuringValidationM;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::ValidateAgainstDef(UINT32 flags)

//  DESCRIPTION     : Validate the dicom attribute against the definition attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status;

    // check if the attribute is seen as an additional attribute
    if (flags & ATTR_FLAG_ADDITIONAL_ATTRIBUTE)
    {
		// check if any Group 0002, 0004 or 0006 attributes are included as additional attributes
		// - this is NOT allowed in a normal dataset
		if (dcmAttributeM_ptr != NULL)
		{
			if (dcmAttributeM_ptr->GetGroup() < GROUP_EIGHT)
			{
				char message[128];
				// additional attributes from Groups 0002, 0004 and 0006 are not allowed
				sprintf(message, 
					"Group %04X attribute not allowed as Additional attribute", 
					dcmAttributeM_ptr->GetGroup());
				this->GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_10, message);
			}
		}
        // check if strict validation is enabled
        if (flags & ATTR_FLAG_STRICT_VALIDATION)
        {
            // additional attributes are not allowed
            this->GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_1,
                "Additional attribute not allowed when Strict Validation ON");
            status = MSG_ERROR;
        }
        else
        {
            // treat as a type 3 attribute 
            // - only the VM will be validated
           status = MSG_OK;

            // We don't need to check VM when the VR is a UI and the
            // Validation flag is set that allows a list of UI values.
            // This is allowed in a C-FIND-RQ, C-MOVE-RQ and C-GET-RQ command.
            if (defAttributeM_ptr != NULL)
            {
                if ((this->defAttributeM_ptr->GetVR() != ATTR_VR_UI) ||
                    (!(flags & ATTR_FLAG_UI_LIST_ALLOWED)))
                {
                    status = CheckDefValueMultiplicity(flags);
                }
            }
        }
        return status;
    }

    if (defAttributeM_ptr == NULL)
    {
        return MSG_OK;
    }

    // Check the attribute presence - conditions will be taken into account
    status = CheckDefPresence(flags);
    if (status == MSG_OK)
    {
        status = this->CheckDefVRCompatibility(flags);
    }

    if (status == MSG_OK)
    {
        if (this->defAttributeM_ptr->GetVR() == ATTR_VR_SQ)
        {
            VAL_VALUE_SQ_CLASS *valSqValue_ptr = NULL;

            // Since a SQ attribute can have only 1 value, we don't need to loop
            // over the values.
            if (this->GetNrValues() == 1)
            {
#ifdef NDEBUG
            valSqValue_ptr = static_cast<VAL_VALUE_SQ_CLASS *>(this->GetValue(0));
#else
            valSqValue_ptr = dynamic_cast<VAL_VALUE_SQ_CLASS *>(this->GetValue(0));
#endif
            }

            // check if there is a sequence value returned
            if (valSqValue_ptr == NULL)
            {
                // Type 1C condition has been checked in the CheckDefPresence method. Only need
                // to check Type 1 here.
                if (this->defAttributeM_ptr->GetType() == ATTR_TYPE_1)
                {
                    // A type 1 SQ must always contain at least 1 item
                    this->GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_1,
                                        "Type 1 (mandatory) SQ attribute should be present with at least one item");
                    status = MSG_ERROR;
                }
                else
                {
                    // zero-length sequence is allowed
                    status = MSG_OK;
                }
 
                return status;
            }

            // Sequence is present
            DCM_VALUE_SQ_CLASS *dcmSqValue_ptr = static_cast<DCM_VALUE_SQ_CLASS *>(valSqValue_ptr->GetDcmValue());
            if (dcmSqValue_ptr != NULL)
            {
                // If the SQ attribute is of type 1c, 2, 2c, 3 or 3c and there are no items in the dcm value,
                // then we don't need to check the sq against the definition. Type 1c condition has already been
                // checked in the CheckDefPresence method.
                if (dcmSqValue_ptr->GetNrItems() == 0)
                {
                    if (this->defAttributeM_ptr->GetType() == ATTR_TYPE_1)
                    {
                        // A type 1 SQ must always contain at least 1 item
                        this->GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_1,
                                            "Type 1 (mandatory) SQ attribute should be present with at least one item");
                        status = MSG_ERROR;
                    }
                    else
                    {
                        // zero-length sequence is allowed
                        status = MSG_OK;
                    }
 
                    return status;
                }
                else
                {
                    status = valSqValue_ptr->ValidateAgainstDef(flags);
                }
            }
            else
            {
                if (this->defAttributeM_ptr->GetType() == ATTR_TYPE_1)
                {
                    // A type 1 SQ must always contain at least 1 item
                    this->GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_1,
                                        "Type 1 (mandatory) SQ attribute should be present with at least one item");
                    status = MSG_ERROR;
                }
            }
        }
    }

    if (status == MSG_OK)
    {
        status = CheckAgainstDTAndEV(flags);
    }
 
    DVT_STATUS status2 = MSG_OK;

    // We don't need to check VM when the VR is a UI and the
    // Validation flag is set that allows a list of UI values.
    // This is allowed in a C-FIND-RQ, C-MOVE-RQ and C-GET-RQ command.
    if ((this->defAttributeM_ptr->GetVR() != ATTR_VR_UI) ||
        (!(flags & ATTR_FLAG_UI_LIST_ALLOWED)))
    {
        status2 = CheckDefValueMultiplicity(flags);
    }
    if (status == MSG_OK) status = status2;

	// Check the pixel data length - if appropriate
	if (CheckPixelDataLength() != MSG_OK)
	{
		status = MSG_ERROR;
	}

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::ValidateAgainstRef(UINT32 flags)

//  DESCRIPTION     : Validate the dicom attribute against the reference attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// check if the source attribute has been imported with a ILE TS and has a VR of UN
	// - if so then we will only validate against the reference attribute if this also has
	// a VR of UN - otherwise don't do any reference comparison.
	if ((this->dcmAttributeM_ptr != NULL) &&
		(this->refAttributeM_ptr != NULL))
	{
		if ((this->dcmAttributeM_ptr->getTransferVR() == TRANSFER_ATTR_VR_IMPLICIT) &&
			(this->dcmAttributeM_ptr->GetVR() == ATTR_VR_UN) &&
			(this->refAttributeM_ptr->GetVR() != ATTR_VR_UN))
		{
			return MSG_OK;
		}
	}

    DVT_STATUS status = CheckRefPresence(flags);
    if ((status == MSG_OK) && 
        (this->refAttributeM_ptr != NULL) &&
        (this->refAttributeM_ptr->GetVR() == ATTR_VR_SQ))
    {
        VAL_VALUE_SQ_CLASS *valSqValue_ptr = NULL;

        // Since a SQ attribute can have only 1 value, we don't need to loop
        // over the values.
        if (this->GetNrValues() == 1)
        {
#ifdef NDEBUG
            valSqValue_ptr = static_cast<VAL_VALUE_SQ_CLASS *>(this->valuesM[0]);
#else
            valSqValue_ptr = dynamic_cast<VAL_VALUE_SQ_CLASS *>(this->valuesM[0]);
#endif
        }

        if (valSqValue_ptr != NULL)
        {
            status = valSqValue_ptr->ValidateAgainstRef(flags);
        }
    }

    if (status == MSG_OK)
    {
        status = CheckVRCompatibility(flags);
    }

    if (status == MSG_OK)
    {
        status = CheckRefVM(flags);
        DVT_STATUS status2 = CheckRefContent(flags);
        if (status == MSG_OK) status = status2;
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::ValidateVR(UINT32 flags, SPECIFIC_CHARACTER_SET_CLASS *specificCharacterSet_ptr)

//  DESCRIPTION     : Validate the VR of the dicom attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;

    if (dcmAttributeM_ptr != NULL)
    {
        for (UINT i = 0; i < valuesM.size(); i++)
        {
            DVT_STATUS status2 = valuesM[i]->Check(flags, specificCharacterSet_ptr);

            if (status2 != MSG_OK)
            {
                status = status2;
            }
        }
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckDefPresence(UINT32)

//  DESCRIPTION     : Check the presence of the dicom attribute against the definition attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;

    ATTR_TYPE_ENUM attrType = defAttributeM_ptr->GetType();

    switch (attrType)
    {
    case ATTR_TYPE_1:
        if ((dcmAttributeM_ptr == NULL) || 
			(dcmAttributeM_ptr->getPaddedLength() == 0))
        {
			if ((dcmAttributeM_ptr != NULL) &&
				((defAttributeM_ptr->GetVR() == ATTR_VR_OB) ||
				(defAttributeM_ptr->GetVR() == ATTR_VR_OF) ||
				(defAttributeM_ptr->GetVR() == ATTR_VR_OW)))
			{
                if(dcmAttributeM_ptr->GetNrValues() == 0)
				{
					GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_13,
						"OB/OF/OW Data length is zero - Type 1 (mandatory) attribute should have at least one value");
				}
				else
				{
					GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_13,
						"Type 1 (mandatory) attribute should have at least one value with non-zero length OB/OF/OW Data");
				}
			}
            else if (useConditionalTextDuringValidationM)
            {
                GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_9,
                    "Textual Module/Macro Condition may affect validation result: Type 1 (mandatory) attribute should be present with at least one value");
            }
            else
            {
                GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_1,
                                  "Type 1 (mandatory) attribute should be present with at least one value");
            }
            status = MSG_ERROR;
        }
        break;
    case ATTR_TYPE_2:
        if (dcmAttributeM_ptr == NULL)
        {
            if (useConditionalTextDuringValidationM)
            {
 			    GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_9,
                                  "Textual Module/Macro Condition may affect validation result: Type 2 attribute should be present with at least a zero-length");
            }
            else
            {
			    GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_1,
                                  "Type 2 attribute should be present with at least a zero-length");
            }
            status = MSG_ERROR;
        }
        break;
    case ATTR_TYPE_1C:  // Fall through
    case ATTR_TYPE_2C:  // Fall through
    case ATTR_TYPE_3C:
        if (defAttributeM_ptr->GetCondition() == NULL)
        {
            if (defAttributeM_ptr->GetTextualCondition().length() == 0)
            {
                GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_5,
                                      "Condition not defined in definition - cannot be further validated");
                status = MSG_ERROR;
            }
			else
			{
				char message[1024];
				sprintf(message,
					"Text Condition - \"%s\"",
                    defAttributeM_ptr->GetTextualCondition().c_str());
                GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_12, message);
			}
        }
        else
        {
			// define the co-object - for a dataset this may either be the command (for network) or
			// the file meta information (for media). The co-object may be needed during the condition
			// evaluation.
            DCM_ATTRIBUTE_GROUP_CLASS *coObject_ptr = NULL;

			if (parentM_ptr)
			{
				VAL_OBJECT_RESULTS_CLASS *parent_ptr = parentM_ptr->GetParentObject();
				if (parent_ptr)
                {
					// see if the co-object is the command
                    coObject_ptr = parent_ptr->GetCommand();
                    if (coObject_ptr == NULL)
                    {
						// see if the co-object is the file meta information
						coObject_ptr = parent_ptr->GetFmi();
                    }
                }
			}

            char message[1024];
			CONDITION_RESULT_ENUM conditionResult = CONDITION_FALSE;
			CONDITION_CLASS *condition_ptr = defAttributeM_ptr->GetCondition();
			if (condition_ptr)
			{
				// evaluate the condition using the co-object (if present - may be NULL)
				conditionResult = condition_ptr->Evaluate(parentM_ptr->GetDcmAttributeGroup(), coObject_ptr, NULL);
			}

            if (conditionResult == CONDITION_TRUE)
            {
                if (dcmAttributeM_ptr == NULL)
                {
					// Type 3C attributes are optional - so even though the condition evaluates true the
					// attribute does not need to be present
					if (attrType != ATTR_TYPE_3C)
					{
						// The attribute should be present, but is not.
						sprintf(message,
							     "Attribute expected according to condition: %s",
								 defAttributeM_ptr->GetConditionResultMessage().c_str());
						GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_2, message);
						status = MSG_ERROR;
					}
                }
            }
            else if (conditionResult == CONDITION_TRUE_REQUIRES_MANUAL_INTERPRETATION)
			{
               if (dcmAttributeM_ptr == NULL)
                {
                    // The attribute should be present, but is not.
                    sprintf(message,
                             "Attribute expected according to condition: %s",
                             defAttributeM_ptr->GetConditionResultMessage().c_str());
                    GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_2, message);
                    status = MSG_ERROR;
                }
			   else
			   {
				   // The attribute is present but requires further manual interpretation
                    sprintf(message,
                             "Attribute present - but result requires manual interpretation: %s",
                             defAttributeM_ptr->GetConditionResultMessage().c_str());
                    GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_11, message);
			   }
			}
            else if (conditionResult == CONDITION_FALSE)
            {
                if (dcmAttributeM_ptr != NULL)
                {
					// The attribute should not be present, but it is present.
		            sprintf(message,
		                    "Attribute NOT expected according to condition: %s",
			                defAttributeM_ptr->GetConditionResultMessage().c_str());
					GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_3, message);
                }
            }
			else // condition_result == CONDITION_UNIMPORTANT
			{
				// include logging simply for debug purposes for now - should not be in final build
               if (dcmAttributeM_ptr != NULL)
                {
                    // The attribute is present - doesn't matter.
                    sprintf(message,
                             "Attribute present - but presence not important: %s",
                             defAttributeM_ptr->GetConditionResultMessage().c_str());
                    GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_7, message);
                }
			   else
			   {
                    // The attribute is not present - doesn't matter.
                    sprintf(message,
                             "Attribute not present - but presence not important: %s",
                             defAttributeM_ptr->GetConditionResultMessage().c_str());
                    GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_8, message);
			   }
			}
        }
        break;
    }

    if ((status == MSG_OK) &&
        ((attrType == ATTR_TYPE_1 || 
		  attrType == ATTR_TYPE_1C) && 
        dcmAttributeM_ptr != NULL))
    {
        if (dcmAttributeM_ptr->GetNrValues() == 0)
        {
            if (useConditionalTextDuringValidationM)
            {
                GetMessages()->AddMessage(VAL_RULE_D_VM_5,
                                    "Textual Module/Macro Condition may affect validation result: Zero-length not allowed for type 1 attribute");
            }
            else
            {
                GetMessages()->AddMessage(VAL_RULE_D_VM_1,
                                    "Zero-length not allowed for type 1 attribute");
            }
            status = MSG_ERROR;
        }
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckDefVRCompatibility(UINT32)

//  DESCRIPTION     : Check the compatibility between the definition VR and that of
//                  : the dicom attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;

    if ((dcmAttributeM_ptr == NULL) || 
        (defAttributeM_ptr == NULL))
    {
        // Don't perform any further checks. The presense of either the def or
        // dcm value has already been checked before. Just return.
        return MSG_OK;
    }

    ATTR_VR_ENUM defVr = defAttributeM_ptr->GetVR();
    ATTR_VR_ENUM dcmVr = dcmAttributeM_ptr->GetVR();

    if (defVr == dcmVr)
    {
        // VR is the same. Leave asap.
        return MSG_OK;
    }

    UINT32 tag = (dcmAttributeM_ptr->GetMappedGroup() << 16) + (dcmAttributeM_ptr->GetMappedElement());
    if ((((tag & 0xFF00FFFF) == TAG_AUDIO_SAMPLE_DATA) ||
		((tag & 0xFF00FFFF) == TAG_CURVE_DATA) ||
        ((tag & 0xFF00FFFF) == TAG_OVERLAY_DATA)) &&
        ((dcmVr == ATTR_VR_OB) || 
         (dcmVr == ATTR_VR_OF) ||
         (dcmVr == ATTR_VR_OW)))
    {
        return MSG_OK;
    }


    switch (tag)
    {
    case TAG_PIXEL_DATA:
        if ((dcmVr == ATTR_VR_OB) || 
            (dcmVr == ATTR_VR_OF) ||
            (dcmVr == ATTR_VR_OW))
        {
            return MSG_OK;
        }
        break;
    case TAG_SMALLEST_IMAGE_PIXEL_VALUE:
    case TAG_LARGEST_IMAGE_PIXEL_VALUE:
    case TAG_SMALLEST_PIXEL_VALUE_IN_SERIES:
    case TAG_LARGEST_PIXEL_VALUE_IN_SERIES:
    case TAG_SMALLEST_IMAGE_PIXEL_VALUE_IN_PLANE:
    case TAG_LARGEST_IMAGE_PIXEL_VALUE_IN_PLANE:
    case TAG_PIXEL_PADDING_VALUE:
		{
			DCM_ATTRIBUTE_GROUP_CLASS *parent_ptr = dcmAttributeM_ptr->getParent();
			if (parent_ptr != NULL)
			{
				UINT16 pixelRepresentation = parent_ptr->getPixelRepresentation();

				// Any of the following statements don't actually change
				// the VR of the dcm value, it just enables to check if the
				// alternate VR is allowed.
				if (pixelRepresentation == 0)
				{
				    dcmVr = ATTR_VR_US;
				}
				if (pixelRepresentation == 1)
				{
				    dcmVr = ATTR_VR_SS;
				}
			}
		}
        break;
    case TAG_RED_PALETTE_COLOR_LOOKUP_TABLE_DESCRIPTOR:
    case TAG_GREEN_PALETTE_COLOR_LOOKUP_TABLE_DESCRIPTOR:
    case TAG_BLUE_PALETTE_COLOR_LOOKUP_TABLE_DESCRIPTOR:
        if (dcmVr == ATTR_VR_OW)
        {
            return MSG_OK;
        }
        // Fall through. Don't add break!
    case TAG_LUT_DESCRIPTOR:
    case TAG_LUT_DATA:
        if ((dcmVr == ATTR_VR_US) || 
			(dcmVr == ATTR_VR_SS))
        {
            return MSG_OK;
        }
        break;
    default:
        break;
    }

    if (defVr != dcmVr)
    {
		char message[128];
        sprintf (message, 
                 "Attribute VR of %s differs from definition VR of %s",
                 stringVr(dcmVr),
                 stringVr(defVr));
        GetMessages()->AddMessage(VAL_RULE_D_VR_1, message);
        status = MSG_ERROR;
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckAgainstDTAndEV(UINT32)

//  DESCRIPTION     : Check the dicom attribute against any definition attribute
//                  : enumerated values or defined terms.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;

    if (dcmAttributeM_ptr == NULL)
    {
        return MSG_OK;
    }

    // A SQ attribute always has a value list linked. This value list
    // contains the attributes within the sequence. However, the content
    // of the SQ is not within the list of Defined Terms. So return with an
    // ok.
    if (defAttributeM_ptr->GetVR() == ATTR_VR_SQ)
    {
        return MSG_OK;
    }

    // TODO: Need to build in a check for comparing the defined terms with the contents of the OB, OF, OW files
    // Don't check these for now
    if ((defAttributeM_ptr->GetVR() == ATTR_VR_OB) ||
        (defAttributeM_ptr->GetVR() == ATTR_VR_OF) ||
        (defAttributeM_ptr->GetVR() == ATTR_VR_OW))
    {
        return MSG_OK;
    }

    int noOfLists = defAttributeM_ptr->GetNrLists();

    if (noOfLists == 0)
    {
        // No defined terms or enumerated values are present for the
        // attribute in the definition component.
        return MSG_OK;
    }

    ATTR_VAL_TYPE_ENUM valueType;
    if (noOfLists > 1)
    {
        // With more than one list of defined terms and/or enumerated
        // values, it is not allowed that one of those lists are of type
        // ENUMERATED_LIST or DEFINED_LIST.
        // NOTE: This check may be obsolete as it may have been done
        //       already in the definition component.
        for (int i = 0; i < noOfLists; i++)
        {
            valueType = defAttributeM_ptr->GetValueList(i)->GetValueType();
            if ((valueType == ATTR_VAL_TYPE_ENUMERATED_LIST) ||
                (valueType == ATTR_VAL_TYPE_DEFINED_LIST))
            {
                GetMessages()->AddMessage(VAL_RULE_D_RANGE_3,
                    "Error in definition file, no EL or DL list " \
                    "allowed with more than one list of defined terms " \
                    "or enumerated values.");
            }
        }
    }

    VALUE_LIST_CLASS *valueList_ptr = defAttributeM_ptr->GetValueList();
    valueType = valueList_ptr->GetValueType();
    if ((noOfLists == 1) &&
        ((valueType == ATTR_VAL_TYPE_ENUMERATED_LIST) ||
         (valueType == ATTR_VAL_TYPE_DEFINED_LIST)))
    {
        for (int i = 0; i < dcmAttributeM_ptr->GetNrValues(); i++)
        {
            BASE_VALUE_CLASS *value_ptr = dcmAttributeM_ptr->GetValue(i);
            if (valueList_ptr->HasValue(value_ptr) == false)
            {
                char message[256];
                string valueString;
                value_ptr->Get(valueString);

                if (valueType == ATTR_VAL_TYPE_ENUMERATED_LIST)
                {
                    sprintf(message,
                            "value \"%s\" not in list of enumerated values list",
                            valueString.c_str());
                    GetMessages()->AddMessage(VAL_RULE_D_RANGE_1, message);
                }
                else // valueType == ATTR_VAL_TYPE_DEFINED_LIST
                {
                    sprintf(message,
                            "value \"%s\" not in list of defined terms list",
                            valueString.c_str());
                    GetMessages()->AddMessage(VAL_RULE_D_RANGE_2, message);
                }
                status = MSG_ERROR;
            }
        }
        return status;
    }

    int noOfIterations;

    if (noOfLists < dcmAttributeM_ptr->GetNrValues())
    {
        noOfIterations = noOfLists;
    }
    else
    {
        noOfIterations = dcmAttributeM_ptr->GetNrValues();
    }

    for (int i = 0; i < noOfIterations; i++)
    {
        valueList_ptr = defAttributeM_ptr->GetValueList(i);
        BASE_VALUE_CLASS *value_ptr = dcmAttributeM_ptr->GetValue(i);
        if (valueList_ptr->HasValue(value_ptr) == false)
        {
            char message[256];
            string valueString;
            value_ptr->Get(valueString);

            if (valueType == ATTR_VAL_TYPE_ENUMERATED)
            {
                sprintf(message,
                        "value \"%s\" not in list of enumerated values list",
                        valueString.c_str());
                GetMessages()->AddMessage(VAL_RULE_D_RANGE_1, message);
            }
            else // valueType == ATTR_VAL_TYPE_DEFINED
            {
                sprintf(message,
                        "value \"%s\" not in list of defined terms list",
                        valueString.c_str());
                GetMessages()->AddMessage(VAL_RULE_D_RANGE_2, message);
            }
            status = MSG_ERROR;
        }
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckDefValueMultiplicity(UINT32)

//  DESCRIPTION     : Check the dicom attribute value multiplicity against the definition
//                  : attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    DVT_STATUS status = MSG_OK;
    char message[128];

    if (dcmAttributeM_ptr == NULL)
    {
        return MSG_OK;
    }

    int noOfValues = dcmAttributeM_ptr->GetNrValues();
    if (noOfValues == 0)
    {
        // If there are 0 values available, then a possible message has
        // been generated during the Presence check.
        return MSG_OK;
    }

    UINT vmMin = defAttributeM_ptr->GetVmMin();
    UINT vmMax = defAttributeM_ptr->GetVmMax();

    // Check VM range.
    if (((UINT)noOfValues < vmMin) || 
        ((UINT)noOfValues > vmMax))
    {
        sprintf(message, 
				"Number of attribute values %d out of Value Multiplicity range (%d..%d)",
                noOfValues, vmMin, vmMax);
        GetMessages()->AddMessage(VAL_RULE_D_VM_2, message);
        status = MSG_ERROR;
    }

    // Check restrictions.
    switch (defAttributeM_ptr->GetVmRestriction())
    {
    case ATTR_VM_RESTRICT_EVEN:
        if (noOfValues % 2 != 0)
        {
            sprintf(message, 
					"Number of attribute values (VM) of %d expected to be even",
                    noOfValues);
            GetMessages()->AddMessage(VAL_RULE_D_VM_3, message);
            status = MSG_ERROR;
        }
        break;
    case ATTR_VM_RESTRICT_TRIPLE:
        if (noOfValues % 3 != 0)
        {
            sprintf(message, 
					"Number of attribute values (VM) of %d expected to be divisible by 3",
                    noOfValues);
            GetMessages()->AddMessage(VAL_RULE_D_VM_4, message);
            status = MSG_ERROR;
        }
        break;
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckRefPresence(UINT32)

//  DESCRIPTION     : Check the dicom attribute against the reference attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if ((dcmAttributeM_ptr == NULL) && 
        (refAttributeM_ptr == NULL))
    {
        return MSG_NO_VALUE;
    }

    if (dcmAttributeM_ptr == NULL)
    {
        GetMessages()->AddMessage(VAL_RULE_R_PRESENCE_1,
							"Reference attribute not present in source object");
        return MSG_ERROR;
    }

    if (refAttributeM_ptr == NULL)
    {
        GetMessages()->AddMessage(VAL_RULE_D_PRESENCE_6,
							"Source attribute not present in reference object");
        return MSG_ERROR;
    }
    return MSG_OK;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckVRCompatibility(UINT32)

//  DESCRIPTION     : Check the VR compatibility of the dicom attribute and the
//                  : reference attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if ((dcmAttributeM_ptr == NULL) ||
        (refAttributeM_ptr == NULL))
    {
        return MSG_ERROR;
    }

    ATTR_VR_ENUM dcmVr = dcmAttributeM_ptr->GetVR();
    ATTR_VR_ENUM refVr = refAttributeM_ptr->GetVR();

    // explicit check on OB / OW - these can be compatible
    if (((dcmVr == ATTR_VR_OB) &&
        (refVr == ATTR_VR_OW)) ||
        ((dcmVr == ATTR_VR_OW) &&
        (refVr == ATTR_VR_OB)))
    {
        return MSG_OK;
    }

    // check source vr against reference vr
    if (dcmVr != refVr)
    {
		char message[128];
        sprintf (message,
                 "Attribute VR of %s differs from reference attribute VR of %s",
                 stringVr(dcmVr), 
				 stringVr(refVr));
        GetMessages()->AddMessage(VAL_RULE_R_VR_1, message);
        return MSG_ERROR;
    }

    return MSG_OK;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckRefVM(UINT32)

//  DESCRIPTION     : Check the VM of the dicom attribute and the reference attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if ((dcmAttributeM_ptr == NULL) ||
        (refAttributeM_ptr == NULL))
    {
       return MSG_ERROR;
    }

    int dcmVm = dcmAttributeM_ptr->GetNrValues();
    int refVm = refAttributeM_ptr->GetNrValues();

    if ((dcmVm == 0) && 
        (refVm == 1))
    {
        // special case if the reference is zero length
        // - it has a single value that is zero-length - consequence of script parser
        BASE_VALUE_CLASS *value_ptr = refAttributeM_ptr->GetValue(0);
        if ((value_ptr) &&
            (value_ptr->GetLength() == 0))
        {
            // reference has a zero length - same as source with no value
            return MSG_OK;
        }
    }

    if (dcmVm != refVm)
    {
		char message[128];
        sprintf(message,
            "Number of attribute values (VM) of %d differs from number of reference attribute values of %d",
            dcmVm, 
			refVm);
        GetMessages()->AddMessage(VAL_RULE_R_VM_1, message);
        return MSG_ERROR;
    }

    return MSG_OK;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckRefContent(UINT32)

//  DESCRIPTION     : Check the values of the dicom attribute and the reference attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if ((dcmAttributeM_ptr == NULL) ||
        (refAttributeM_ptr == NULL))
    {
        return MSG_ERROR;
    }

    // The check for VM is done in another function. Check only the number
    // of values that are available in the dcm or ref attribute. Take the lowest.
    int noOfValues;
    if (dcmAttributeM_ptr->GetNrValues() < refAttributeM_ptr->GetNrValues())
    {
        noOfValues = dcmAttributeM_ptr->GetNrValues();
    }
    else
    {
        noOfValues = refAttributeM_ptr->GetNrValues();
    }

    DVT_STATUS status = MSG_OK;

    for (int i = 0; i < noOfValues; i++)
    {
        VAL_BASE_VALUE_CLASS *value_ptr = GetValue(i);
        if (value_ptr)
        {
            DVT_STATUS status2 = value_ptr->CompareRef();
            if ((status2 != MSG_OK) && 
				(status2 != MSG_EQUAL))
            {
                status = status2;
            }
        }
    }

    return status;
}

//>>===========================================================================

DVT_STATUS VAL_ATTRIBUTE_CLASS::CheckPixelDataLength()

//  DESCRIPTION     : Check the pixel data length is consistent with the image
//					: dimensions as defined by the rows, columns and bits allocated
//					: attributes.
//					: The dimensions should match the pixel data length if the image
//					: is uncompressed. If the image is compressed we assume that the
//					: pixel data length will be less than the dimensions.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    char message[512];

	// check that we are dealing with the pixel attribute
    DCM_ATTRIBUTE_CLASS	*dcmAttr_ptr = this->GetDcmAttribute();
    if (dcmAttr_ptr == NULL)
    {
        return MSG_OK;
    }

	// check if pixel data is stored
	if (dcmAttr_ptr->getPaddedLength() == 0)
	{
		return MSG_OK;
	}

    UINT32 tag = (dcmAttr_ptr->GetMappedGroup() << 16) + (dcmAttr_ptr->GetMappedElement());
	if (tag != TAG_PIXEL_DATA)
	{
		return MSG_OK;
	}

	// get the parent to allow access to the rows, columns and bits allocated attributes
    DCM_ATTRIBUTE_GROUP_CLASS *parent = dcmAttr_ptr->getParent();
	if (parent == NULL)
	{
        sprintf(message, 
                "Cannot get the Dataset for the Rows (0028,0010), Columns (0028,0011) and Bits Allocated (0028,0100) Attributes needed for the Pixel Data length validation");
        GetMessages()->AddMessage(VAL_RULE_PIXEL_DATA_1, message);
        return MSG_ERROR;

	}

	// get the image dimensions
	UINT16 rows;
	if (!parent->getUSValue(TAG_ROWS, &rows))
	{
        sprintf(message, 
                "Dataset does not contain the Rows (0028,0010) Attribute needed for the Pixel Data length validation");
        GetMessages()->AddMessage(VAL_RULE_PIXEL_DATA_2, message);
        return MSG_ERROR;
	}

	UINT16 columns;
	if (!parent->getUSValue(TAG_COLUMNS, &columns))
	{
        sprintf(message, 
                "Dataset does not contain the Columns (0028,0011) Attribute needed for the Pixel Data length validation");
        GetMessages()->AddMessage(VAL_RULE_PIXEL_DATA_3, message);
        return MSG_ERROR;
	}

	UINT16 bitsAllocated;
	if (!parent->getUSValue(TAG_BITS_ALLOCATED, &bitsAllocated))
	{
        sprintf(message, 
                "Dataset does not contain the Bits Allocated (0028,0100) Attribute needed for the Pixel Data length validation");
        GetMessages()->AddMessage(VAL_RULE_PIXEL_DATA_4, message);
        return MSG_ERROR;
	}

    // get optional samples per pixel
    UINT16 samplesPerPixel;
	if (!parent->getUSValue(TAG_SAMPLES_PER_PIXEL, &samplesPerPixel))
    {
        // value not present - set default of 1
        samplesPerPixel = 1;
    }

    // get optional number of frames
    INT32 noOfFrames;
	if (!parent->getISValue(TAG_NUMBER_OF_FRAMES, &noOfFrames))
    {
        // value not present - set default of 1
        noOfFrames = 1;
    }

	// get the pixel value
	bool compressed = false;
	if (dcmAttr_ptr->GetNrValues() == 1)
	{
		switch(dcmAttr_ptr->GetVR())
		{
		case ATTR_VR_OB: 
			{
				// check if the data is compressed
				VALUE_OB_CLASS *obValue_ptr = (VALUE_OB_CLASS*)dcmAttr_ptr->GetValue(0);
				if (obValue_ptr != NULL)
				{
					compressed = obValue_ptr->IsCompressed();
				}
			}
			break;
		case ATTR_VR_OF: 
			{
				// check if the data is compressed
				VALUE_OF_CLASS *ofValue_ptr = (VALUE_OF_CLASS*)dcmAttr_ptr->GetValue(0);
				if (ofValue_ptr != NULL)
				{
					compressed = ofValue_ptr->IsCompressed();
				}
			}
			break;
		case ATTR_VR_OW: 
			{
				// check if the data is compressed
				VALUE_OW_CLASS *owValue_ptr = (VALUE_OW_CLASS*)dcmAttr_ptr->GetValue(0);
				if (owValue_ptr != NULL)
				{
					compressed = owValue_ptr->IsCompressed();
				}
			}
			break;
		default: break;
		}
	}
	
	// compute the pixel data length
	UINT32 computedPixelDataLength = noOfFrames * (UINT32) samplesPerPixel * (UINT32) rows * (UINT32) columns * (UINT32) (bitsAllocated / 8);

	DVT_STATUS status = MSG_OK;
	if (compressed)
	{
		// pixel data is compressed - so length should be less than (or equal?)
		if (dcmAttr_ptr->getPaddedLength() > computedPixelDataLength)
		{
	        sprintf (message, 
		             "Compressed Pixel Data Length of %d larger than maximum computed length of %d (from Number Of Frames=%d, Samples Per Pixel=%d, Rows=%d, Columns=%d, Bits Allocated=%d)",
					 dcmAttr_ptr->getPaddedLength(),
					 computedPixelDataLength,
                     noOfFrames,
                     samplesPerPixel,
					 rows,
					 columns,
					 bitsAllocated);
			GetMessages()->AddMessage(VAL_RULE_PIXEL_DATA_5, message);
			status = MSG_ERROR;
		}
	}
	else
	{
		// add 1 to computed length if odd
		if (computedPixelDataLength & 0x1)
		{
			computedPixelDataLength++;
		}
		// pixel data is not compressed - so lengths should match
		if (dcmAttr_ptr->getPaddedLength() != computedPixelDataLength)
		{
	        sprintf (message, 
		             "Uncompressed Pixel Data Length of %d does not match computed length of %d (from Number Of Frames=%d, Samples Per Pixel=%d, Rows=%d, Columns=%d, Bits Allocated=%d)",
					 dcmAttr_ptr->getPaddedLength(),
					 computedPixelDataLength,
                     noOfFrames,
                     samplesPerPixel,
                     rows,
					 columns,
					 bitsAllocated);
			GetMessages()->AddMessage(VAL_RULE_PIXEL_DATA_6, message);
			status = MSG_ERROR;
		}
	}

    return status;
}