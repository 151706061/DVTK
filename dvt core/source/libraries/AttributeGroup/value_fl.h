// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef VALUE_FL_H
#define VALUE_FL_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include <string>

#include "Iglobal.h"      // Global component interface
#include "base_value.h"

//*****************************************************************************
//  VALUE_FL_CLASS definition
//*****************************************************************************
class VALUE_FL_CLASS : public BASE_VALUE_CLASS
{
public:
                            VALUE_FL_CLASS();
    virtual                ~VALUE_FL_CLASS();
            bool            operator = (BASE_VALUE_CLASS &value);
            ATTR_VR_ENUM    GetVRType (void);
            DVT_STATUS      Compare (LOG_MESSAGE_CLASS *messages,
                                     BASE_VALUE_CLASS * ref_value);
            DVT_STATUS      Check (UINT32 flags,
                                   BASE_VALUE_CLASS **value_list,
                                   LOG_MESSAGE_CLASS *messages,
                                   SPECIFIC_CHARACTER_SET_CLASS *specific_character_set = NULL);
            DVT_STATUS      Set (string value);
            DVT_STATUS      Set (float value);
            DVT_STATUS      Get (string &value, bool stripped=false);
            DVT_STATUS      Get (float &value);
            UINT32          GetLength (void);

private:
    float                   valueM;
};

#endif /* VALUE_FL_H */
