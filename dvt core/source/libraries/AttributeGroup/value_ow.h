// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef VALUE_OW_H
#define VALUE_OW_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include <string>

#include "Iglobal.h"      // Global component interface
#include "other_value.h"

//*****************************************************************************
//  VALUE_OW_CLASS definition
//*****************************************************************************
class VALUE_OW_CLASS : public OTHER_VALUE_CLASS
{
public:
                            VALUE_OW_CLASS();
    virtual                ~VALUE_OW_CLASS();
            bool            operator = (BASE_VALUE_CLASS &value);
            UINT32          GetLength (void);
            ATTR_VR_ENUM    GetVRType (void);
            DVT_STATUS      Compare (LOG_MESSAGE_CLASS *messages,
                                     BASE_VALUE_CLASS * ref_value);
            DVT_STATUS      Check (UINT32 flags,
                                   BASE_VALUE_CLASS **value_list,
                                   LOG_MESSAGE_CLASS *messages,
                                   SPECIFIC_CHARACTER_SET_CLASS *specific_character_set = NULL);
			void			SetCompressed(bool flag);
			bool			IsCompressed();
private:
	bool isCompressedM;
};

#endif /* VALUE_OW_H */
