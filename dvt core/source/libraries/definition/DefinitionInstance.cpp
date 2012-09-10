//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "DefFileContent.h"
#include "DefinitionInstance.h"

//>>===========================================================================

DEF_INSTANCE_CLASS::DEF_INSTANCE_CLASS(const string filename, DEF_FILE_CONTENT_CLASS *fileContent_ptr)

//  DESCRIPTION     : Default constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	filenameM = filename;
	fileContentM_ptr = fileContent_ptr;
	referenceIndexM = 1;
}

//>>===========================================================================

DEF_INSTANCE_CLASS::~DEF_INSTANCE_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
	if (fileContentM_ptr)
	{
		delete fileContentM_ptr;
	}
}

//>>===========================================================================

string DEF_INSTANCE_CLASS::GetFilename()

//  DESCRIPTION     : Get filename.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return filenameM;
}

//>>===========================================================================

DEF_FILE_CONTENT_CLASS *DEF_INSTANCE_CLASS::GetFileContent()

//  DESCRIPTION     : Get file content.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return fileContentM_ptr;
}

//>>===========================================================================

int DEF_INSTANCE_CLASS::GetReferenceIndex()

//  DESCRIPTION     : Get Reference Index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return referenceIndexM;
}

//>>===========================================================================

void DEF_INSTANCE_CLASS::IncrementReferenceIndex()

//  DESCRIPTION     : Increment the Reference Index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	referenceIndexM++;
}

//>>===========================================================================

void DEF_INSTANCE_CLASS::DecrementReferenceIndex()

//  DESCRIPTION     : Decrement the Reference Index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	referenceIndexM--;
}
