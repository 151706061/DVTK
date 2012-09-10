//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Image Display Format File Class
//*****************************************************************************
#ifndef IMAGE_DISPLAY_FILE_H
#define IMAGE_DISPLAY_FILE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Iutility.h"		// Utility component interface


//>>***************************************************************************

class IMAGE_DISPLAY_FILE_CLASS : public BASE_FILE_CLASS

//  DESCRIPTION     : Image Display Format File Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	IMAGE_DISPLAY_FILE_CLASS(string filename);

	~IMAGE_DISPLAY_FILE_CLASS();

	bool execute();
};


#endif /* IMAGE_DISPLAY_FILE_H */
