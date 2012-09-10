//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_INSTANCE_H
#define DEF_INSTANCE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_FILE_CONTENT_CLASS;

//>>***************************************************************************

class DEF_INSTANCE_CLASS

//  DESCRIPTION     : Definition instance class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_INSTANCE_CLASS(const string, DEF_FILE_CONTENT_CLASS*);

	~DEF_INSTANCE_CLASS();

	string GetFilename();

	DEF_FILE_CONTENT_CLASS *GetFileContent();

	int GetReferenceIndex();

	void IncrementReferenceIndex();

	void DecrementReferenceIndex();

private:
	DEF_FILE_CONTENT_CLASS	*fileContentM_ptr;
	string					filenameM;
	int						referenceIndexM;
};		

#endif /* DEF_INSTANCE_H */
