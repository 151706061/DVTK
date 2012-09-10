//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Attribute group results include file for validation.
//*****************************************************************************
#ifndef VAL_ATTRIBUTE_GROUP_H
#define VAL_ATTRIBUTE_GROUP_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_ATTRIBUTE_GROUP_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;
class VAL_ATTRIBUTE_CLASS;
class LOG_MESSAGE_CLASS;
class VAL_OBJECT_RESULTS_CLASS;
class VAL_VALUE_SQ_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;

//>>***************************************************************************

class VAL_ATTRIBUTE_GROUP_CLASS

//  DESCRIPTION     : Validation Attribute Group Class
//  NOTES           :
//<<***************************************************************************
{
    public:
        VAL_ATTRIBUTE_GROUP_CLASS();
        virtual ~VAL_ATTRIBUTE_GROUP_CLASS();

        void SetParentObject(VAL_OBJECT_RESULTS_CLASS*);
        void SetParentSQ(VAL_VALUE_SQ_CLASS*);
        VAL_OBJECT_RESULTS_CLASS * GetParentObject();

        void SetDefAttributeGroup(DEF_ATTRIBUTE_GROUP_CLASS*);
        void SetRefAttributeGroup(DCM_ATTRIBUTE_GROUP_CLASS*);
        void SetDcmAttributeGroup(DCM_ATTRIBUTE_GROUP_CLASS*);
        DEF_ATTRIBUTE_GROUP_CLASS *GetDefAttributeGroup();
        DCM_ATTRIBUTE_GROUP_CLASS *GetRefAttributeGroup();
        DCM_ATTRIBUTE_GROUP_CLASS *GetDcmAttributeGroup();

        bool AddValAttribute(VAL_ATTRIBUTE_CLASS*);
        int GetNrAttributes();
        VAL_ATTRIBUTE_CLASS *GetAttribute(int);
        VAL_ATTRIBUTE_CLASS *GetAttribute(UINT16, UINT16);
        VAL_ATTRIBUTE_CLASS *GetAttributeByTag(UINT32);

        DVT_STATUS ValidateAgainstDef(UINT32);
        DVT_STATUS ValidateAgainstRef(UINT32);
        DVT_STATUS ValidateVR(UINT32, SPECIFIC_CHARACTER_SET_CLASS*);

        LOG_MESSAGE_CLASS *GetMessages();
        bool HasMessages();
        
    private:
        VAL_OBJECT_RESULTS_CLASS *parentObjectM_ptr;
        VAL_VALUE_SQ_CLASS *parentSqM_ptr;
        DEF_ATTRIBUTE_GROUP_CLASS *defAttributeGroupM_ptr;
        DCM_ATTRIBUTE_GROUP_CLASS *refAttributeGroupM_ptr;
        DCM_ATTRIBUTE_GROUP_CLASS *dcmAttributeGroupM_ptr;
        LOG_MESSAGE_CLASS *messagesM_ptr;
        vector<VAL_ATTRIBUTE_CLASS*> attributesM;
};

#endif /* VAL_ATTRIBUTE_GROUP_H */
