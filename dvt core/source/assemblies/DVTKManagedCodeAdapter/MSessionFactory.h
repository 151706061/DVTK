// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "ISessions.h"
#include "MBaseSession.h"
#define LogMask System::UInt32

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;
    public __gc class MSessionFactory
    {
    public:
        static MBaseSession __gc* Load (System::String __gc* session_filename);
    private:
        MSessionFactory () {};
    };
}