//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Attribute results include file for validation.
//*****************************************************************************
#ifndef VAL_ATTRIBUTE_H
#define VAL_ATTRIBUTE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class BASE_VALUE_CLASS;
class VALUE_LIST_CLASS;
class DEF_ATTRIBUTE_CLASS;
class DCM_ATTRIBUTE_CLASS;
class VAL_BASE_VALUE_CLASS;
class LOG_MESSAGE_CLASS;
class VAL_ATTRIBUTE_GROUP_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;


//>>***************************************************************************

class VAL_ATTRIBUTE_CLASS

//  DESCRIPTION     : Validation Attribute Class
//  NOTES           :
//<<***************************************************************************
{
    public:
	    VAL_ATTRIBUTE_CLASS();
	    virtual ~VAL_ATTRIBUTE_CLASS();

        void SetParent(VAL_ATTRIBUTE_GROUP_CLASS*);
        VAL_ATTRIBUTE_GROUP_CLASS *GetParent();

        void SetDefAttribute(DEF_ATTRIBUTE_CLASS*);
        void SetRefAttribute(DCM_ATTRIBUTE_CLASS*);
        void SetDcmAttribute(DCM_ATTRIBUTE_CLASS*);
        DEF_ATTRIBUTE_CLASS *GetDefAttribute();
        DCM_ATTRIBUTE_CLASS *GetRefAttribute();
        DCM_ATTRIBUTE_CLASS *GetDcmAttribute();

        bool AddValValue(VAL_BASE_VALUE_CLASS*);
        int GetNrValues();
        VAL_BASE_VALUE_CLASS *GetValue(int valueIndex = 0);

        LOG_MESSAGE_CLASS *GetMessages();
        bool HasMessages();

        DVT_STATUS ValidateAgainstDef(UINT32);
        DVT_STATUS ValidateAgainstRef(UINT32);
        DVT_STATUS ValidateVR(UINT32, SPECIFIC_CHARACTER_SET_CLASS*);

        void SetUseConditionalTextDuringValidation(bool);
        bool GetUseConditionalTextDuringValidation();

    protected:
        VAL_ATTRIBUTE_GROUP_CLASS *parentM_ptr;
        DEF_ATTRIBUTE_CLASS *defAttributeM_ptr;
        DCM_ATTRIBUTE_CLASS *refAttributeM_ptr;
        DCM_ATTRIBUTE_CLASS *dcmAttributeM_ptr;
        LOG_MESSAGE_CLASS *messagesM_ptr;
        vector<VAL_BASE_VALUE_CLASS*> valuesM;
        bool useConditionalTextDuringValidationM;

        DVT_STATUS CheckDefPresence(UINT32);
        DVT_STATUS CheckDefVRCompatibility(UINT32);
        DVT_STATUS CheckRefPresence(UINT32);
        DVT_STATUS CheckAgainstDTAndEV(UINT32);
        DVT_STATUS CheckDefValueMultiplicity(UINT32);
        DVT_STATUS CheckVRCompatibility(UINT32);
        DVT_STATUS CheckRefVM(UINT32);
        DVT_STATUS CheckRefContent(UINT32);
		DVT_STATUS CheckPixelDataLength();
};

#endif /* VAL_ATTRIBUTE_H */