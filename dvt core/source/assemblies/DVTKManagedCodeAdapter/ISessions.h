// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
    public __value enum StorageMode
    {
        StorageModeAsMedia,
        StorageModeAsDataSet,
        StorageModeTemporaryPixelOnly,
        StorageModeNoStorage,
    };

    public __gc __interface ISession
    {
        __property Wrappers::StorageMode get_StorageMode();
        __property void set_StorageMode(Wrappers::StorageMode value);
        __property bool get_StrictValidation();
        __property void set_StrictValidation(bool value);
        __property bool get_ContinueOnError();
        __property void set_ContinueOnError(bool value);
        __property bool get_ValidateReferencedFile();
        __property void set_ValidateReferencedFile(bool value);
    };
}
