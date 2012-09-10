// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	Base IO class.
//*****************************************************************************
#ifndef BASE_IO_H
#define BASE_IO_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Ilog.h"			// Log component interface
#include "Iutility.h"		// Utility component interface


//>>***************************************************************************

class BASE_IO_CLASS : public ENDIAN_CLASS

//  DESCRIPTION     : Abstract base class for the input/output interfaces.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	virtual	~BASE_IO_CLASS();
};

#endif /* BASE_IO_H */


