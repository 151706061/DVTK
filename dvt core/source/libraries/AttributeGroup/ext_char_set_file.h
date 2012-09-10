// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef EXT_CHAR_SET_FILE_H
#define EXT_CHAR_SET_FILE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Iutility.h"		// Utility component interface


//>>***************************************************************************

class EXT_CHAR_SET_FILE_CLASS : public BASE_FILE_CLASS

//  DESCRIPTION     : Extended Character Set File Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	EXT_CHAR_SET_FILE_CLASS(string filename);

	~EXT_CHAR_SET_FILE_CLASS();

	bool execute();
};


#endif /* EXT_CHAR_SET_FILE_H */
