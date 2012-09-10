//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "base_log.h"		// Base Log Classes


//>>===========================================================================

LOG_CLASS::~LOG_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// empty virtual destructor
}

//>>===========================================================================

void LOG_CLASS::setLogMask(UINT32 logMask)

//  DESCRIPTION     : Set the Log Mask.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    logMaskM = logMask; 
}

//>>===========================================================================

UINT32 LOG_CLASS::getLogMask()

//  DESCRIPTION     : Get the Log Mask.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    return logMaskM; 
}

//>>===========================================================================

UINT32 LOG_CLASS::logLevel(UINT32 newLogLevel)

//  DESCRIPTION     : Set/Get the log level.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
	// set log level to given value
	UINT32 oldLogLevel = logLevelM;
	logLevelM = newLogLevel; 
	
	// return old log level
	return oldLogLevel;
}

//>>===========================================================================

void LOG_CLASS::text(BYTE, char* format_ptr, ...)

//  DESCRIPTION     : Log text.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	va_list	arguments;

	// check if global log level enabled
	if (logLevelM & logMaskM) 
	{
		// handle the variable arguments
		va_start(arguments, format_ptr);
		vsprintf(bufferM, format_ptr, arguments);
		va_end(arguments);

		// call the corresponding display method
		displayText();
	}
}

//>>===========================================================================

void LOG_CLASS::text(UINT32 logLevel, BYTE, char* format_ptr, ...)

//  DESCRIPTION     : Log text.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	va_list	arguments;

	// check if given log level enabled
	if (logLevel & logMaskM) 
	{
		// handle the variable arguments
		va_start(arguments, format_ptr);
		vsprintf(bufferM, format_ptr, arguments);
		va_end(arguments);

		// temporarily set the global log level to that given for call to derrived class::displayText()
		UINT32 oldLogLevel = logLevelM;
		logLevelM = logLevel;

		// call the corresponding display method
		displayText();

		// restore old log level
		logLevelM = oldLogLevel;
	}
}

//>>===========================================================================

void LOG_CLASS::setActivityReporter(BASE_ACTIVITY_REPORTER *activityReporter_ptr)

//  DESCRIPTION     : Set the Activity Reporter.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	activityReporterM_ptr = activityReporter_ptr;
}

//>>===========================================================================

BASE_ACTIVITY_REPORTER* LOG_CLASS::getActivityReporter()

//  DESCRIPTION     : Get the Activity Reporter.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return activityReporterM_ptr;
}

//>>===========================================================================

void LOG_CLASS::setSerializer(BASE_SERIALIZER *serializer_ptr)

//  DESCRIPTION     : Set the Serializer.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	serializerM_ptr = serializer_ptr;
}

//>>===========================================================================

BASE_SERIALIZER* LOG_CLASS::getSerializer()

//  DESCRIPTION     : Get the Serializer.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
    return serializerM_ptr;
}

//>>===========================================================================

void LOG_CLASS::setStorageRoot(string storageRoot)

//  DESCRIPTION     : Get the Storage Root directory.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    storageRootM = storageRoot; 
}

//>>===========================================================================

const char *LOG_CLASS::getStorageRoot()

//  DESCRIPTION     : Get the Storage Root directory.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    return storageRootM.c_str(); 
}

//>>===========================================================================

void LOG_CLASS::setResultsRoot(string resultsRoot)

//  DESCRIPTION     : Set the Results Root directory.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    //
    // Add backslash at the end of the resultsRoot directory path
    //
    if (resultsRoot[resultsRoot.length()-1] != '\\')
    {
        resultsRoot += "\\";
    }
    resultsRootM = resultsRoot; 
}

//>>===========================================================================

const char *LOG_CLASS::getResultsRoot()

//  DESCRIPTION     : Get the Results Root directory.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
    return resultsRootM.c_str(); 
}
