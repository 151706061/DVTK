//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef BASE_CONFIRMER_H
#define BASE_CONFIRMER_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"				// global component interface

//*****************************************************************************
//  FORWARD DECLARATIONS
//*****************************************************************************

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************
//<<abstract>>
class BASE_CONFIRMER

//  DESCRIPTION     : BASE_CONFIRMER class.
//  INVARIANT       :
//  NOTES           : Derived classes should implement the defined methods.
//<<***************************************************************************
{
public:
    virtual void ConfirmInteraction() = 0;
public:
    ~BASE_CONFIRMER();
};

#endif /* BASE_CONFIRMER_H */

