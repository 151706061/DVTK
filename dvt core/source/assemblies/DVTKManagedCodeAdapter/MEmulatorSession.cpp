// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright � 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include ".\memulatorsession.h"
#include ".\MPrinter.h"
#using <mscorlib.dll>

namespace Wrappers
{
    MEmulatorSession::MEmulatorSession(void)
    {
        m_pEMULATOR_SESSION_CLASS = new EMULATOR_SESSION_CLASS();
        m_pPrinter = new Wrappers::MPrinter();
        disposed = false;
        this->Initialize();
        return;
    }

    // Use C# destructor syntax for finalization code.
    // This destructor will run only if the Dispose method
    // does not get called.
    // It gives your base class the opportunity to finalize.
    // Do not provide destructors in types derived from this class.
    MEmulatorSession::~MEmulatorSession(void)
    {
        // Do not re-create Dispose clean-up code here.
        Dispose(false);
        m_pPrinter = NULL;
        return;
    }

    // Implement IDisposable*
    // Do not make this method virtual.
    // A derived class should not be able to this method.
    void MEmulatorSession::Dispose() {
        Dispose(true);
        // This object will be cleaned up by the Dispose method.
        // Therefore, you should call GC::SupressFinalize to
        // take this object off the finalization queue
        // and prevent finalization code for this object
        // from executing a second time.
        System::GC::SuppressFinalize(this);
    }

    // Dispose(bool disposing) executes in two distinct scenarios.
    // If disposing equals true, the method has been called directly
    // or indirectly by a user's code. Managed and unmanaged resources
    // can be disposed.
    // If disposing equals false, the method has been called by the
    // runtime from inside the finalizer and you should not reference
    // other objects. Only unmanaged resources can be disposed.
    void MEmulatorSession::Dispose(bool disposing) {
        // If disposing equals true, dispose all managed
        // and unmanaged resources.
        if (disposing) {
            // Dispose managed resources.
        }
        // Check to see if Dispose has already been called.
        if (!this->disposed) {
            // Call the appropriate methods to clean up
            // unmanaged resources here.
            // If disposing is false,
            // only the following code is executed.
            delete m_pEMULATOR_SESSION_CLASS;
            m_pEMULATOR_SESSION_CLASS = NULL;
        }
        disposed = true;
    }

    MPrinter __gc* MEmulatorSession::get_MPrinter()
    {
        return m_pPrinter;
    }

    bool MEmulatorSession::EmulateStorageSCU(
        FileName* fileNames[],
        System::UInt32 options,
        System::UInt32 repetitions)
    {
        vector<string> fileNameVector;
        int size = fileNames->Length;
        for (int idx = 0; idx < size; idx++)
        {
            FileName* value = fileNames[idx];
            char* pAnsiString = (char*)(void*)Marshal::StringToHGlobalAnsi(value);
            string file = pAnsiString;
            fileNameVector.push_back(file);
            Marshal::FreeHGlobal(pAnsiString);
        }
        // TODO: Check this assumption:
        // It appears as if fileNames are passed-by-value in nested call
        // void STORAGE_SCU_EMULATOR_CLASS::addFile(string filename)
        // Therefor no heap-allocation with new is needed for vector<string> fileNameVector
        // no cleanup needed either. Cleaned by out-of-scope destruction.
        return m_pEMULATOR_SESSION_CLASS->emulateStorageSCU(&fileNameVector, options, repetitions);
    }

    bool MEmulatorSession::EmulateVerificationSCU()
    {
        return m_pEMULATOR_SESSION_CLASS->emulateVerificationSCU();
    }
}