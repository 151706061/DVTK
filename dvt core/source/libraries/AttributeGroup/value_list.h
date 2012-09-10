// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef VALUE_LIST_H
#define VALUE_LIST_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Ilog.h"         // Log component interface
#include <string>
#include <vector>


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class BASE_VALUE_CLASS;

//*****************************************************************************
//  Type definitions
//*****************************************************************************
typedef vector<BASE_VALUE_CLASS *>  BASE_VALUE_VECTOR;


class VALUE_LIST_CLASS  
{
public:
                                VALUE_LIST_CLASS();
    virtual                    ~VALUE_LIST_CLASS();
	        BASE_VALUE_CLASS*   GetValue (int index);
            int                 GetNrValues (void);
	        DVT_STATUS          AddValue (BASE_VALUE_CLASS * value);
	        void                SetValueType (ATTR_VAL_TYPE_ENUM val_type);
	        ATTR_VAL_TYPE_ENUM  GetValueType (void);
            DVT_STATUS          Replace (BASE_VALUE_CLASS * value,
                                         int index=0);
            bool                HasValue (BASE_VALUE_CLASS * value);

private:
	ATTR_VAL_TYPE_ENUM          value_typeM;
    BASE_VALUE_VECTOR           valuesM;
};

#endif /* VALUE_LIST_H */
