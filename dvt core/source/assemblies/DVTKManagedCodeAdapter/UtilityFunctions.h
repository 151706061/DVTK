// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
    void MarshalString ( System::String* s, std::string& os );

    std::string ConvertString(std::string inString);
}
