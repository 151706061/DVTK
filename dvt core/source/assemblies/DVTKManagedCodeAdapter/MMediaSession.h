// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "MBaseSession.h"

namespace Wrappers
{
    public __gc class MMediaSession
        : public MBaseSession
        , public System::IDisposable
    {
    private:
        MEDIA_SESSION_CLASS __nogc* m_pMEDIA_SESSION_CLASS;
    private protected:
        __property BASE_SESSION_CLASS __nogc* get_m_pBASE_SESSION()
        {
            return m_pMEDIA_SESSION_CLASS;
        }
    public:
        // <summary>
        // Constructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        MMediaSession(void);
    public:
        // <summary>
        // Destructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        ~MMediaSession(void);

    public:
        System::Boolean Validate(
            DvtkData::Media::DicomFile __gc* pDicomFile, 
            Wrappers::ValidationControlFlags validationControlFlags);

	public:
		System::Boolean GenerateDICOMDIR(
			System::String __gc* dcmFileNames[]);

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
