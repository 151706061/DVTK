// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//  Validation Message Level Lookup.
#pragma once

#include "ActivityReportingAdapter.h"

namespace Wrappers
{
    __abstract __gc class ValidationMessageInfo
    {
    private:
        static System::Collections::Hashtable __gc* pLevelTable;
    private:
        static System::Collections::Hashtable __gc* pLevelTableStrict;
    private:
        static System::Collections::Hashtable __gc* pLevelTableStandard;
    public:
        static ValidationMessageInfo()
        {
            ValidationMessageInfo::_StaticConstructor();
        }
    private:
        static void _StaticConstructor();
    public:
        static
            WrappedValidationMessageLevel GetLevel(
            System::UInt32 messageUID, System::Uri __gc* pRulesUri);

    private:
        static System::Uri __gc* pRuleUriStandardRules;
        static System::Uri __gc* pRuleUriStrictRules;
    };
}
