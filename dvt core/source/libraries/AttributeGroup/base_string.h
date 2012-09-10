// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef BASE_STRING_H
#define BASE_STRING_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include <string>
#include "Iglobal.h"      // Global component interface
#include "base_value.h"

class BASE_STRING_CLASS : public BASE_VALUE_CLASS  
{
public:
                        BASE_STRING_CLASS();
    virtual            ~BASE_STRING_CLASS();
    virtual DVT_STATUS  Get (string &value, bool stripped=false);
    virtual DVT_STATUS  Get (unsigned char ** value, UINT32 &length);
    virtual DVT_STATUS  Set (string value);
    virtual DVT_STATUS  Set (unsigned char * value, UINT32 length);
    virtual bool        operator = (BASE_STRING_CLASS &value);
    virtual UINT32      GetLength (void);

protected:
    string              valueM;

    virtual string      GetStripped (void) = 0;
            int         StringStrip (string     src_string,
                                     int        max_length,
                                     bool       lead_spc,
                                     bool       trail_spc,
                                     string    &result);

            DVT_STATUS  CompareStringValues (string     ref_value,
                                             bool       lead_spc,
                                             bool       trail_spc);
};

#endif /* BASE_STRING_H */
