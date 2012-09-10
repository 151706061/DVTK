// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "Ilog.h"
#include "ISessions.h"
#include "CountingAdapter.h"
#include <vcclr.h>

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    public __value enum WrappedValidationMessageLevel
    {
        None,
        Error,
        Debug,
        Warning,
        Information,
        Scripting,
		ScriptName,
        MediaFilename,
        DicomObjectRelationship,
        DulpStateMachine,
        WareHouseLabel,
    };
    inline WrappedValidationMessageLevel _Convert(ReportLevel reportLevel)
    {
        switch (reportLevel)
        {
        case ::ReportLevel_None             : return WrappedValidationMessageLevel::None;
        case ::ReportLevel_Error            : return WrappedValidationMessageLevel::Error;
        case ::ReportLevel_Debug            : return WrappedValidationMessageLevel::Debug;
        case ::ReportLevel_Warning          : return WrappedValidationMessageLevel::Warning;
        case ::ReportLevel_Information      : return WrappedValidationMessageLevel::Information;
        case ::ReportLevel_Scripting        : return WrappedValidationMessageLevel::Scripting;
        case ::ReportLevel_ScriptName       : return WrappedValidationMessageLevel::ScriptName;
        case ::ReportLevel_MediaFilename    : return WrappedValidationMessageLevel::MediaFilename;
		case ::ReportLevel_DicomObjectRelationship :
            return WrappedValidationMessageLevel::DicomObjectRelationship;
        case ::ReportLevel_DulpStateMachine : return WrappedValidationMessageLevel::DulpStateMachine;
        case ::ReportLevel_WareHouseLabel   : return WrappedValidationMessageLevel::WareHouseLabel;
            // Unknown Report level
        default                             : throw new System::NotImplementedException();
        }
    }
    
    public __gc __interface IActivityReportingTarget
    {
        void ReportActivity(WrappedValidationMessageLevel level, System::String __gc* message);
    };
    
    __nogc class ActivityReportingAdapter
        : public BASE_ACTIVITY_REPORTER
    {
    public:
        // ctor
        ActivityReportingAdapter(
			IActivityReportingTarget __gc* activityReportingTarget,
			ICountingTarget __gc* countingTarget);
        // dtor
        ~ActivityReportingAdapter(void);

        void ReportActivity(ReportLevel reportLevel, const char* message);

    private:
		gcroot<Wrappers::ICountingTarget*> m_pCountingTarget;
        gcroot<IActivityReportingTarget*> m_pActivityReportingTarget;
    };
}