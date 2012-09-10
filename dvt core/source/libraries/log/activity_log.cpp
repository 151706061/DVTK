//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
// EXTERNAL DECLARATIONS
//*****************************************************************************
#include "activity_log.h"

//>>===========================================================================

ACTIVITY_LOG_CLASS::ACTIVITY_LOG_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	activityReporterM_ptr = NULL;
    serializerM_ptr = NULL;
	logMaskM = LOG_ERROR;
	logLevelM = LOG_ERROR;
}

//>>===========================================================================

ACTIVITY_LOG_CLASS::~ACTIVITY_LOG_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
}

//>>===========================================================================

void ACTIVITY_LOG_CLASS::displayText()

//  DESCRIPTION     : Display text in line log.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
    // display the current text to the activity reporter
   ReportLevel reportLevel = ReportLevel_None;
   bool report = false;
   switch(logLevelM)
   {
   case LOG_ERROR:             
       reportLevel = ReportLevel_Error;
       report = true; 
       break;
   case LOG_DEBUG:             
       reportLevel = ReportLevel_Debug; 
       report = true;  
       break;
   case LOG_WARNING:           
       reportLevel = ReportLevel_Warning; 
       report = true;  
       break;
   case LOG_INFO:              
       reportLevel = ReportLevel_Information; 
       report = true;  
       break;
   case LOG_SCRIPT:            
       reportLevel = ReportLevel_Scripting; 
       report = true;  
       break;
   case LOG_SCRIPT_NAME:            
       reportLevel = ReportLevel_ScriptName; 
       report = false;  
       break;
   case LOG_MEDIA_FILENAME:            
       reportLevel = ReportLevel_MediaFilename; 
       report = false;  
       break;
   case LOG_IMAGE_RELATION:    
       reportLevel = ReportLevel_DicomObjectRelationship; 
       report = true;  
       break;
   case LOG_DULP_FSM:          
       reportLevel = ReportLevel_DulpStateMachine; 
       report = true;  
       break;
   case LOG_LABEL:             
       reportLevel = ReportLevel_WareHouseLabel; 
       report = true;  
       break;
   case LOG_NONE:              
       reportLevel = ReportLevel_None; 
       //
       // Do NOT report level none in activity reporter.
       // Suppress this level to avoid information overflow in the activity report.
       //
       report = false;  
       break;
   default:
       reportLevel = ReportLevel_None; 
       report = false;
       break;
   }
 
	// display the current text to the activity reporter
	if ((activityReporterM_ptr) &&
        (report))
	{
		// report the activity
		activityReporterM_ptr->ReportActivity(reportLevel, bufferM);
	}

    // display the current text to the serializer
    if (serializerM_ptr)
    {
        // serialize the message
        serializerM_ptr->SerializeApplicationReport(reportLevel, bufferM);
    }
}
