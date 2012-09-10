//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Base value results include file for validation.
//*****************************************************************************
#ifndef VAL_BASE_VALUE_H
#define VAL_BASE_VALUE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class VALUE_LIST_CLASS;
class BASE_VALUE_CLASS;
class LOG_MESSAGE_CLASS;
class VAL_BASE_VALUE_CLASS;
class VAL_ATTRIBUTE_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;

//>>***************************************************************************
class VAL_BASE_VALUE_CLASS
//  DESCRIPTION     :
//  NOTES           :
//<<***************************************************************************
{
    public:
        VAL_BASE_VALUE_CLASS();
        virtual ~VAL_BASE_VALUE_CLASS();

        void SetParent(VAL_ATTRIBUTE_CLASS*);

        void SetDefValueList(VALUE_LIST_CLASS*);
        void SetRefValue(BASE_VALUE_CLASS*);
        void SetDcmValue(BASE_VALUE_CLASS*);

        VALUE_LIST_CLASS *GetDefValueList();
        BASE_VALUE_CLASS *GetRefValue();
        BASE_VALUE_CLASS *GetDcmValue();

        virtual DVT_STATUS Check(UINT32, SPECIFIC_CHARACTER_SET_CLASS*);
        DVT_STATUS CompareRef();

        LOG_MESSAGE_CLASS *GetMessages();
        bool HasMessages();

    private:
        VAL_ATTRIBUTE_CLASS *parentM_ptr;
        VALUE_LIST_CLASS *defValueM_ptr;
        BASE_VALUE_CLASS *refValueM_ptr;
        BASE_VALUE_CLASS *dcmValueM_ptr;
        LOG_MESSAGE_CLASS *messagesM_ptr;
};

#endif /* VAL_BASE_VALUE_H */