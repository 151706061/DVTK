// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef BASE_VALUE_H
#define BASE_VALUE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"	// Global component interface
#include "Ilog.h"       // Log component interface
#include "../validation/valrules.h"

#include <string>
#include <vector>

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class ATTRIBUTE_GROUP_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;


//*****************************************************************************
//  BASE_VALUE_CLASS definition
//*****************************************************************************
class BASE_VALUE_CLASS  
{
public:
    virtual                ~BASE_VALUE_CLASS();
    virtual bool            operator = (BASE_VALUE_CLASS &value);

    UINT32 GetMaximumVrLength();

    // The Get functions will return a reference to the actual value. If the
    // caller wants to update the value, he has to copy the value.
    virtual DVT_STATUS      Get (string &, bool stripped=false);
    virtual DVT_STATUS      Get (float &);
    virtual DVT_STATUS      Get (double &);
    virtual DVT_STATUS      Get (unsigned char **, UINT32 &);
    virtual DVT_STATUS      Get (UINT32 &);
    virtual DVT_STATUS      Get (UINT16 &);
    virtual DVT_STATUS      Get (INT32 &);
    virtual DVT_STATUS      Get (INT16 &);
    virtual DVT_STATUS      Get (ATTRIBUTE_GROUP_CLASS **, int index=0);
    virtual DVT_STATUS      Get (UINT32 index, UINT32 &);

    virtual DVT_STATUS      Set (string);
    virtual DVT_STATUS      Set (float);
    virtual DVT_STATUS      Set (double);
    virtual DVT_STATUS      Set (unsigned char *, UINT32);
    virtual DVT_STATUS      Set (UINT32);
    virtual DVT_STATUS      Set (UINT16);
    virtual DVT_STATUS      Set (INT32);
    virtual DVT_STATUS      Set (INT16);
    virtual DVT_STATUS      Set (ATTRIBUTE_GROUP_CLASS *);

    virtual DVT_STATUS      Add (UINT32);

    virtual UINT32          GetLength (void) = 0;

    virtual DVT_STATUS      Compare (LOG_MESSAGE_CLASS *messages,
                                     BASE_VALUE_CLASS * ref_value) = 0;
    virtual DVT_STATUS      Compare (string);
    virtual DVT_STATUS      Check (UINT32 flags,
                                   BASE_VALUE_CLASS **value_list,
                                   LOG_MESSAGE_CLASS *messages,
                                   SPECIFIC_CHARACTER_SET_CLASS *specific_character_set = NULL) = 0;
    virtual ATTR_VR_ENUM    GetVRType (void) = 0;

    virtual int             GetNrItems (void);

protected:
    int                     DaysInMonth(int month);
    int                     GetNumeric (string value, int nr_digits);
    bool                    IsNumeric(string num_value, int nr_digits);
    bool                    IsTimeValid(int hour, int minute, int second);
    bool                    IsDateValid (int year, int month, int day);
};

//*****************************************************************************
//  GLOBAL FUNCTION DECLARATION
//*****************************************************************************
BASE_VALUE_CLASS * CreateNewValue (ATTR_VR_ENUM vr);

#endif /* BASE_VALUE_H */
