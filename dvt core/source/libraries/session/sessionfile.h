// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//  Session file class.

#ifndef SESSION_FILE_H
#define SESSION_FILE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Iutility.h"		// Utility component interface

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class BASE_SESSION_CLASS;


//>>***************************************************************************

class SESSION_FILE_CLASS : public BASE_FILE_CLASS

//  DESCRIPTION     : Session File Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	BASE_SESSION_CLASS	*sessionM_ptr;

public:
	SESSION_FILE_CLASS(BASE_SESSION_CLASS*, string);

	~SESSION_FILE_CLASS();

	bool execute();

	bool save();
};


#endif /* SESSION_FILE_H */


