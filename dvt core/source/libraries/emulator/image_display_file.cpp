//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifdef _WINDOWS
#pragma warning (disable : 4786)
#endif

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "image_display_file.h"


//*****************************************************************************
//  EXTERNAL REFERENCES
//*****************************************************************************
extern FILE		*imagedisplayin;
extern int		imagedisplaylineno;
extern void		imagedisplayrestart(FILE*);
extern int		imagedisplayparse(void);


//>>===========================================================================

IMAGE_DISPLAY_FILE_CLASS::IMAGE_DISPLAY_FILE_CLASS(string filename)

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

IMAGE_DISPLAY_FILE_CLASS::~IMAGE_DISPLAY_FILE_CLASS()

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

bool IMAGE_DISPLAY_FILE_CLASS::execute()

//  DESCRIPTION     : Execute image display file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// set up input for YACC parser
	if (!open()) return false;

	// set parser to read a character set file
	imagedisplaylineno = 1;
	imagedisplayin = fdM_ptr;
	imagedisplayrestart(imagedisplayin);

	// call YACC parser
	int error = imagedisplayparse();

	// close the file
	close();

	if (error) return false;

	return true;
}
