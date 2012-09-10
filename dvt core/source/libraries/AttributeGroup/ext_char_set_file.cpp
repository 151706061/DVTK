// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.


#ifdef _WINDOWS
#pragma warning (disable : 4786)
#endif

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ext_char_set_file.h"


//*****************************************************************************
//  EXTERNAL REFERENCES
//*****************************************************************************
extern FILE		*extcharsetin;
extern int		extcharsetlineno;
extern void		extcharsetrestart(FILE*);
extern int		extcharsetparse(void);


//>>===========================================================================

EXT_CHAR_SET_FILE_CLASS::EXT_CHAR_SET_FILE_CLASS(string filename)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// initialise class members
	filenameM = filename;
	onlyParseFileM = false;
	fdM_ptr = NULL;
}

//>>===========================================================================

EXT_CHAR_SET_FILE_CLASS::~EXT_CHAR_SET_FILE_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// close open character set file
	close();
}

//>>===========================================================================

bool EXT_CHAR_SET_FILE_CLASS::execute()

//  DESCRIPTION     : Execute character set file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// set up input for YACC parser
	if (!open()) return false;

	// set parser to read a character set file
	extcharsetlineno = 1;
	extcharsetin = fdM_ptr;
	extcharsetrestart(extcharsetin);

	// call YACC parser
	int error = extcharsetparse();

	// close the file
	close();

	if (error) return false;

	return true;
}
