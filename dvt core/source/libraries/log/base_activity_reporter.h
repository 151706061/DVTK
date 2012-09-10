//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef BASE_ACTIVITY_REPORTER_H
#define BASE_ACTIVITY_REPORTER_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"				// global component interface

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
enum ReportLevel
{
    ReportLevel_None,
    ReportLevel_Error,
    ReportLevel_Debug,
    ReportLevel_Warning,
    ReportLevel_Information,
    ReportLevel_Scripting,
	ReportLevel_ScriptName,
    ReportLevel_MediaFilename,
    ReportLevel_DicomObjectRelationship,
    ReportLevel_DulpStateMachine,
    ReportLevel_WareHouseLabel,
};

//>>***************************************************************************
//<<abstract>>
class BASE_ACTIVITY_REPORTER

//  DESCRIPTION     : BASE_ACTIVITY_REPORTER class.
//  INVARIANT       :
//  NOTES           : Derived classes should implement the defined methods.
//<<***************************************************************************
{
protected:

public:
    virtual void ReportActivity(ReportLevel level, const char* message) = 0;
public:
	virtual ~BASE_ACTIVITY_REPORTER() = 0;		
};

#endif /* BASE_ACTIVITY_REPORTER_H */

