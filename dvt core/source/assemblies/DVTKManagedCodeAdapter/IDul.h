// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
    public __gc __interface IDul
    {
        __property System::String __gc* get_DvtAeTitle();
        __property void set_DvtAeTitle(System::String __gc* value);
        
        __property System::String __gc* get_SutAeTitle();
        __property void set_SutAeTitle(System::String __gc* value);

        __property System::UInt32 get_DvtMaximumLengthReceived();
        __property void set_DvtMaximumLengthReceived(System::UInt32 value);
        
        __property System::UInt32 get_SutMaximumLengthReceived();
        __property void set_SutMaximumLengthReceived(System::UInt32 value);

        __property System::String __gc* get_DvtImplementationClassUid();
        __property void set_DvtImplementationClassUid(System::String __gc* value);

        __property System::String __gc* get_SutImplementationClassUid();
        __property void set_SutImplementationClassUid(System::String __gc* value);

        __property System::String __gc* get_DvtImplementationVersionName();
        __property void set_DvtImplementationVersionName(System::String __gc* value);

        __property System::String __gc* get_SutImplementationVersionName();
        __property void set_SutImplementationVersionName(System::String __gc* value);
    };
}
