// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include "ActivityReportingAdapter.h"
#include < vcclr.h >
#using <mscorlib.dll>

namespace Wrappers
{
    // ctor
    ActivityReportingAdapter::ActivityReportingAdapter(
		IActivityReportingTarget __gc* activityReportingTarget,
		ICountingTarget __gc* countingTarget)
    {
        m_pActivityReportingTarget = activityReportingTarget;
		m_pCountingTarget = countingTarget;
    }
    // dtor
    ActivityReportingAdapter::~ActivityReportingAdapter(void)
    {}

    void ActivityReportingAdapter::ReportActivity(ReportLevel reportLevel, const char* message)
    {
        // Translation from unmanaged -> managed CLR
        Wrappers::WrappedValidationMessageLevel level = _Convert(reportLevel);
        m_pActivityReportingTarget->ReportActivity(level, message);
   } 
}