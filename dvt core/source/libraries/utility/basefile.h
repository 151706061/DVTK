// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef BASEFILE_H
#define BASEFILE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"			// C Header Files / Base Data Templates


//>>***************************************************************************

class BASE_FILE_CLASS

//  DESCRIPTION     : Abstract Base Class for various "Script" Files.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
protected:
	string	pathnameM;
	string	filenameM;
	string	absoluteFilenameM;
	FILE	*fdM_ptr;
	bool	onlyParseFileM;
	UINT    nr_errorsM;
	UINT    nr_warningsM;
		
public:
	virtual ~BASE_FILE_CLASS() = 0;

	const char *getFilename() 
		{ return filenameM.c_str(); }

	const char *getAbsoluteFilename();

	bool isFullPathname();

	void setOnlyParseFile(bool onlyParseFile)
		{ onlyParseFileM = onlyParseFile; }

	void setNrErrors(UINT nr)   { nr_errorsM = nr; }
	void setNrWarnings(UINT nr) { nr_warningsM = nr; }
	UINT getNrErrors()   { return nr_errorsM; }
	UINT getNrWarnings() { return nr_warningsM; }

	virtual bool open(bool forReading = true);

	virtual bool execute() = 0;

	virtual bool save();

	virtual void close();
};

#endif /* BASEFILE_H */


