//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "definitionfile.h"
#include "deffilecontent.h"
#include "definitiondetails.h"
#include "definition.h"
#include "Isession.h"			// Session component interface


extern FILE*      definitionin;
extern string     definitionfilename;
extern bool       definitionnewfile;
extern int	      definitionlineno;
extern void		  definitionrestart(FILE*);
extern int	      definitionparse(void);
extern UINT       definitionerrors;
extern UINT       definitionwarnings;
extern LOG_CLASS* definitionfilelogger_ptr;

extern void       resetDefinitionParser();

DEF_FILE_CONTENT_CLASS* defFileContent_ptr = NULL;


//>>===========================================================================

DEFINITION_FILE_CLASS::DEFINITION_FILE_CLASS(const string filename) 

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// initialise class members
	fileContentM_ptr = NULL;
	loadedM = false;
	sessionM_ptr = NULL;
	loggerM_ptr = NULL;
	pathnameM = "";
	filenameM = filename;
	onlyParseFileM = false;
	fdM_ptr = NULL;
}


//>>===========================================================================

DEFINITION_FILE_CLASS::DEFINITION_FILE_CLASS(BASE_SESSION_CLASS *session_ptr, const string filename) 

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// initialise class members
	fileContentM_ptr = NULL;
	loadedM = false;
	sessionM_ptr = session_ptr;
	loggerM_ptr = session_ptr->getLogger();

	// save the defintion file pathname
	pathnameM = sessionM_ptr->getDefinitionFileRoot();

	// save filename
	filenameM = filename;

	if (loggerM_ptr)
	{
		loggerM_ptr->text(LOG_DEBUG, 1, "Definition File: %s", filenameM.c_str());
	}

	onlyParseFileM = false;
	fdM_ptr = NULL;
}

//>>===========================================================================

DEFINITION_FILE_CLASS::~DEFINITION_FILE_CLASS()

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

bool DEFINITION_FILE_CLASS::execute()

//  DESCRIPTION     : Execute (parse) the definition file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	bool success = false;


    // Create a semaphore with initial and max. counts of 1.
    LONG cMax = 1;
    HANDLE hSemaphore = CreateSemaphore( 
        NULL,   // no security attributes
        cMax,   // initial count
        cMax,   // maximum count
        "DefinitionFile");  // unnamed semaphore
    if (hSemaphore == NULL) 
    {
        return false;
    }

    // Try to enter the semaphore gate.
    DWORD dwWaitResult = WaitForSingleObject( 
        hSemaphore,   // handle to semaphore
        10L);         // ten-second time-out interval
    if (dwWaitResult == WAIT_TIMEOUT)
    {
        return false;
    }

	// file must be open to be parsed
	if (open()) 
	{
		// set parser to read a Definition File
		definitionlineno = 1;
		definitionin = fdM_ptr;
		definitionnewfile = true;
		definitionfilename = filenameM;
		definitionfilelogger_ptr = loggerM_ptr;
		definitionrestart(definitionin);
		resetDefinitionParser();

		//initialize errors and warnings
		definitionerrors = 0;
		definitionwarnings = 0;

		// allocate the file contents class
		fileContentM_ptr = new DEF_FILE_CONTENT_CLASS();

		// copy address to static
		defFileContent_ptr = fileContentM_ptr;

		//call definition parser
		(void) definitionparse();

		//Get error statistics from definition parser
        setNrErrors(definitionerrors);
		setNrWarnings(definitionwarnings);

		// reset the static
		defFileContent_ptr = NULL;

		// close the file
	    close();

		// set status
		success = (nr_errorsM == 0 ? true : false);
	}

    // Increment the count of the semaphore.
    if (!ReleaseSemaphore( 
        hSemaphore,  // handle to semaphore
        1,           // increase count by one
        NULL))       // not interested in previous count
    {
        // Deal with the error.
    }

	return success;
}

//>>===========================================================================

bool DEFINITION_FILE_CLASS::execute(int& lineNumber)

//  DESCRIPTION     : Execute (parse) the definition file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	bool success = false;


    // Create a semaphore with initial and max. counts of 1.
    LONG cMax = 1;
    HANDLE hSemaphore = CreateSemaphore( 
        NULL,   // no security attributes
        cMax,   // initial count
        cMax,   // maximum count
        "DefinitionFile");  // unnamed semaphore
    if (hSemaphore == NULL) 
    {
        return false;
    }

    // Try to enter the semaphore gate.
    DWORD dwWaitResult = WaitForSingleObject( 
        hSemaphore,   // handle to semaphore
        10L);         // ten-second time-out interval
    if (dwWaitResult == WAIT_TIMEOUT)
    {
        return false;
    }

	// file must be open to be parsed
	if (open()) 
	{
		// set parser to read a Definition File
		definitionlineno = 1;
		definitionin = fdM_ptr;
		definitionnewfile = true;
		definitionfilename = filenameM;
		definitionfilelogger_ptr = loggerM_ptr;
		definitionrestart(definitionin);
		resetDefinitionParser();

		//initialize errors and warnings
		definitionerrors = 0;
		definitionwarnings = 0;

		// allocate the file contents class
		fileContentM_ptr = new DEF_FILE_CONTENT_CLASS();

		// copy address to static
		defFileContent_ptr = fileContentM_ptr;

		//call definition parser
		(void) definitionparse();

		lineNumber = definitionlineno;

		//Get error statistics from definition parser
        setNrErrors(definitionerrors);
		setNrWarnings(definitionwarnings);

		// reset the static
		defFileContent_ptr = NULL;

		// close the file
	    close();

		// set status
		success = (nr_errorsM == 0 ? true : false);
	}

    // Increment the count of the semaphore.
    if (!ReleaseSemaphore( 
        hSemaphore,  // handle to semaphore
        1,           // increase count by one
        NULL))       // not interested in previous count
    {
        // Deal with the error.
    }

	return success;
}

//>>===========================================================================

bool DEFINITION_FILE_CLASS::Load()

//  DESCRIPTION     : Load the defintion file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	if (loggerM_ptr)
	{
		loggerM_ptr->text(LOG_INFO, 1, "Reading definition file: %s", filenameM.c_str());
	}

	// parse the file
	bool success = execute();
	if (success)
	{
		// install the definition file contents
		success = DEFINITION->Install(absoluteFilenameM, fileContentM_ptr);
	}

    if (success)
    {
        // on successful load the pointer is taken over by the DEFINITION singleton
        // - set the file content to NULL
    	fileContentM_ptr = NULL;
    }
	else
	{
		// cleanup
		delete fileContentM_ptr;
        fileContentM_ptr = NULL;
	}

/*
	if (loggerM_ptr)
	{
		if (nr_errorsM > 0)
		{
			loggerM_ptr->text(LOG_ERROR, 1, "%d error(s), %d warning(s)", nr_errorsM, nr_warningsM);
		}
		else if (nr_warningsM > 0)
		{
			loggerM_ptr->text(LOG_WARNING, 1, "%d error(s), %d warning(s)", nr_errorsM, nr_warningsM);
		}
	}
*/
	// check for successful load
	if (nr_errorsM == 0)
	{
		// indicate that the defintion file is now loaded
		loadedM = true;
	}
	else
	{
		// not loaded successfully
		loadedM = false;
		success = false;
	}

	return success;
}

//>>===========================================================================

bool DEFINITION_FILE_CLASS::Unload()

//  DESCRIPTION     : Unload the defintion file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// uninstall the definition file contents
	bool success = DEFINITION->Uninstall(filenameM);
	if (success)
	{
		// set flag to indicate that the file is no longer loaded
		loadedM = false;

		// reset the definition file root
		// - this maybe the reason why the file was unloaded
		if (sessionM_ptr)
		{
			// save the defintion file pathname
			pathnameM = sessionM_ptr->getDefinitionFileRoot();

		}
		else
		{
			pathnameM = "";
		}
	}

	return success;
}

//>>===========================================================================

bool DEFINITION_FILE_CLASS::GetDetails(DEF_DETAILS_CLASS& details)

//  DESCRIPTION     : Get the defintion file details.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// try to execute the file to load the details locally
	int lineNumber = 0;
	bool success = execute(lineNumber);
	if (success)
	{
		// get the detail locally

		try
		{
			details.SetAEName(fileContentM_ptr->GetAEName());
		}
		catch(...)
		{
			throw "Error determining Application Entity name.";
		}

		try
		{
			details.SetAEVersion(fileContentM_ptr->GetAEVersion());
		}
		catch(...)
		{
			throw "Error determining Application Entity version.";
		}

		try
		{
			details.SetSOPClassName(fileContentM_ptr->GetSOPClassName());
		}
		catch(...)
		{
			throw "Error determining SOP Class name.";
		}

		try
		{
			details.SetSOPClassUID(fileContentM_ptr->GetSOPClassUID());
		}
		catch(...)
		{
			throw "Error determining SOP Class UID.";
		}

		try
		{
			details.SetIsMetaSOPClass(fileContentM_ptr->IsMetaSOPClass());
		}
		catch(...)
		{
			throw "Error determining if this is a meta SOP Class.";
		}		
	}
	else
	{
		// delete the file content
		delete fileContentM_ptr;
		fileContentM_ptr = NULL;

		char* errorText = new char[100];

		sprintf(errorText, "Parse error in line %i.", lineNumber);
		throw errorText;
	}

	// delete the file content
	delete fileContentM_ptr;
	fileContentM_ptr = NULL;

	return success;
}
