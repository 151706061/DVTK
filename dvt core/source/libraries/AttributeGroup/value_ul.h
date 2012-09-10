// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef VALUE_UL_H
#define VALUE_UL_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include <string>

#include "Iglobal.h"      // Global component interface
#include "base_value.h"

//*****************************************************************************
//  VALUE_UL_CLASS definition
//*****************************************************************************
class VALUE_UL_CLASS : public BASE_VALUE_CLASS
{
public:
                            VALUE_UL_CLASS();
    virtual                ~VALUE_UL_CLASS();
            bool            operator = (BASE_VALUE_CLASS &value);
            ATTR_VR_ENUM    GetVRType (void);
            DVT_STATUS      Compare (LOG_MESSAGE_CLASS *messages,
                                     BASE_VALUE_CLASS * ref_value);
            DVT_STATUS      Check (UINT32 flags,
                                   BASE_VALUE_CLASS **value_list,
                                   LOG_MESSAGE_CLASS *messages,
                                   SPECIFIC_CHARACTER_SET_CLASS *specific_character_set = NULL);
            DVT_STATUS      Set (string value);
            DVT_STATUS      Set (UINT32 value);
            DVT_STATUS      Get (string &value, bool);
            DVT_STATUS      Get (UINT32 &value);
            UINT32          GetLength (void);

private:
    UINT32                  valueM;
};

#endif /* VALUE_UL_H */
