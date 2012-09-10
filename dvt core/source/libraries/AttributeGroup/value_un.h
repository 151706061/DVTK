// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef VALUE_UN_H
#define VALUE_UN_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include <string>

#include "Iglobal.h"      // Global component interface
#include "base_value.h"

//*****************************************************************************
//  VALUE_UN_CLASS definition
//*****************************************************************************
class VALUE_UN_CLASS : public BASE_VALUE_CLASS
{
public:
                            VALUE_UN_CLASS();
    virtual                ~VALUE_UN_CLASS();
            bool            operator = (BASE_VALUE_CLASS &value);
            ATTR_VR_ENUM    GetVRType (void);
            DVT_STATUS      Compare (LOG_MESSAGE_CLASS *messages,
                                     BASE_VALUE_CLASS * ref_value);
            DVT_STATUS      Check (UINT32 flags,
                                   BASE_VALUE_CLASS **value_list,
                                   LOG_MESSAGE_CLASS *messages,
                                   SPECIFIC_CHARACTER_SET_CLASS *specific_character_set = NULL);
            DVT_STATUS      Set (string value);
            DVT_STATUS      Set (unsigned char * value, UINT32 length);
            DVT_STATUS      Get (unsigned char ** value, UINT32 &length);
            DVT_STATUS      Get (string &value, bool stripped=false);
            UINT32          GetLength (void);

private:
            UINT32          sizeM;
            unsigned char * valueM;
};

#endif /* VALUE_UN_H */
