// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
    public __gc __interface IDimse
    {
        __property bool get_AutoType2Attributes();
        __property void set_AutoType2Attributes(bool value);
        __property bool get_DefineSqLength();
        __property void set_DefineSqLength(bool value);
        __property bool get_AddGroupLength();
        __property void set_AddGroupLength(bool value);
    };
}
