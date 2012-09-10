//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   SQ value results include file for validation.
//*****************************************************************************
#ifndef VAL_VALUE_SQ_H
#define VAL_VALUE_SQ_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"
#include "val_base_value.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class VAL_ATTRIBUTE_GROUP_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;

//>>***************************************************************************

class VAL_VALUE_SQ_CLASS : public VAL_BASE_VALUE_CLASS

//  DESCRIPTION     : Validation SQ Value Class
//  NOTES           :
//<<***************************************************************************
{
    public:
        VAL_VALUE_SQ_CLASS();
        virtual ~VAL_VALUE_SQ_CLASS();

        void AddValItem(VAL_ATTRIBUTE_GROUP_CLASS*);
        VAL_ATTRIBUTE_GROUP_CLASS *GetValItem(int);
        int GetNrValItems();

        DVT_STATUS ValidateAgainstDef(UINT32);
        DVT_STATUS ValidateAgainstRef(UINT32);

        DVT_STATUS Check (UINT32, SPECIFIC_CHARACTER_SET_CLASS*);

    private:
        vector<VAL_ATTRIBUTE_GROUP_CLASS*> valItemsM;
};

#endif /* VAL_VALUE_SQ_H */