//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEFINITIONFILE_H
#define DEFINITIONFILE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Iutility.h"		// Utility component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_DETAILS_CLASS;
class DEF_FILE_CONTENT_CLASS;
class BASE_SESSION_CLASS;
class LOG_CLASS;


//>>***************************************************************************

class DEFINITION_FILE_CLASS : public BASE_FILE_CLASS

//  DESCRIPTION     : Definition File Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEFINITION_FILE_CLASS(const string);
	DEFINITION_FILE_CLASS(BASE_SESSION_CLASS*, const string);

	~DEFINITION_FILE_CLASS();

	bool IsLoaded() { return loadedM; }

	void SetLogger(LOG_CLASS* logger_ptr) { loggerM_ptr = logger_ptr;}

	LOG_CLASS* GetLogger() { return loggerM_ptr; }

	bool Load();

	bool Unload();

	bool GetDetails(DEF_DETAILS_CLASS&);

private:
	DEF_FILE_CONTENT_CLASS	*fileContentM_ptr;
	bool					loadedM;
	BASE_SESSION_CLASS		*sessionM_ptr;
	LOG_CLASS				*loggerM_ptr;

	bool execute();
	bool execute(int& lineNumber);
};

#endif /* DEFINITIONFILE_H */


