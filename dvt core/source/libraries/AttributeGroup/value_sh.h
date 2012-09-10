// Part of Dvtk Libraries - Internal Native Library Code
// Copyright � 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef VALUE_SH_H
#define VALUE_SH_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include <string>

#include "Iglobal.h"      // Global component interface
#include "base_string.h"

//*****************************************************************************
//  VALUE_SH_CLASS definition
//*****************************************************************************
class VALUE_SH_CLASS : public BASE_STRING_CLASS  
{
public:
                            VALUE_SH_CLASS();
    virtual                ~VALUE_SH_CLASS();
            bool            operator = (BASE_VALUE_CLASS &value);
            ATTR_VR_ENUM    GetVRType (void);
            DVT_STATUS      Compare (LOG_MESSAGE_CLASS *messages,
                                     BASE_VALUE_CLASS * ref_value);
            DVT_STATUS      Compare (string);
            DVT_STATUS      Check (UINT32 flags,
                                   BASE_VALUE_CLASS **value_list,
                                   LOG_MESSAGE_CLASS *messages,
                                   SPECIFIC_CHARACTER_SET_CLASS *specific_character_set = NULL);
private:
            string          GetStripped (void);
};

#endif /* VALUE_SH_H */
