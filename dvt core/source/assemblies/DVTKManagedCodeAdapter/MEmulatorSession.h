// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "MBaseSession.h"
#include "MPrinter.h"
#define FileName System::String

namespace Wrappers
{
    public __gc class MEmulatorSession
        : public MBaseSession
        , public System::IDisposable
    {
    private:
        EMULATOR_SESSION_CLASS __nogc* m_pEMULATOR_SESSION_CLASS;
    private protected:
        __property BASE_SESSION_CLASS __nogc* get_m_pBASE_SESSION()
        {
            return m_pEMULATOR_SESSION_CLASS;
        }
    public:
        // <summary>
        // Constructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        MEmulatorSession(void);
    public:
        // <summary>
        // Destructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        ~MEmulatorSession(void);

    public:
        // <summary>
        // EmulateStorageSCU
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        bool EmulateStorageSCU(
            FileName* fileNames[], 
            System::UInt32 options, 
            System::UInt32 repetitions);

    public:
        bool EmulateVerificationSCU();

    private:
        MPrinter __gc* m_pPrinter;
    public:
        __property MPrinter __gc* get_MPrinter();

    private:
        // Track whether Dispose has been called.
        // m_pEMULATOR_SESSION_CLASS is treated as disposable resource.
        bool disposed;

    public:
        void Dispose();

    private:
        void Dispose(bool disposing);
    };
}