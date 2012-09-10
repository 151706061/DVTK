// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "MBaseSession.h"

namespace Wrappers
{
    public __gc class MSnifferSession
        : public MBaseSession
        , public System::IDisposable
    {
    private:
        SNIFFER_SESSION_CLASS __nogc* m_pSNIFFER_SESSION_CLASS;
    private protected:
        __property BASE_SESSION_CLASS __nogc* get_m_pBASE_SESSION()
        {
            return m_pSNIFFER_SESSION_CLASS;
        }
    public:
        // <summary>
        // Constructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        MSnifferSession(void);
    public:
        // <summary>
        // Destructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        ~MSnifferSession(void);

    public:
        System::Boolean Validate(
            DvtkData::Dimse::DicomMessage __gc* pDicomMessage, 
            Wrappers::ValidationControlFlags validationControlFlags);

		System::Boolean Validate(
            DvtkData::Dul::DulMessage __gc* pDicomMessage, 
            Wrappers::ValidationControlFlags validationControlFlags);

		void ReadPDUs(System::String __gc* pduFileNames[]);

		bool ReceiveMessage([Out] DvtkData::Message __gc*__gc* ppMessage);

    private:
        // Track whether Dispose has been called.
        // m_pMEDIA_SESSION_CLASS is treated as disposable resource.
        bool disposed;

    public:
        void Dispose();

    private:
        void Dispose(bool disposing);
    };
}
