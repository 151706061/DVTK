// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "Idefinition.h"
#define LogMask System::UInt32

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    public __value struct MDefinitionFileDetails
    {
        System::String __gc* AEName;
        System::String __gc* AEVersion;
        System::String __gc* SOPClassName;
        System::String __gc* SOPClassUID;
        System::Boolean IsMetaSOPClass;
    };

    public __abstract __gc class MDefinition 
    {
    public:
        // <summary>
        // Definition File Details
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property static MDefinitionFileDetails get_FileDetails(System::String __gc* pFileName);
    };
}