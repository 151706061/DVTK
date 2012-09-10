// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "IDimse.h"
#include "ISockets.h"
#include "IDul.h"
#include "ISessions.h"
#include "MBaseSession.h"
#include <vcclr.h>
#include "ConfirmInteractionAdapter.h"

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    public __value enum ReceiveReturnCode
    {
        Success = 0,
        Failure,
        AssociationRejected,
        AssociationReleased,
        AssociationAborted,
        SocketClosed,
        NoSocketConnection,
    };

    public __value enum SendReturnCode
    {
        Success = 0,
        Failure,
        AssociationRejected,
        AssociationReleased,
        AssociationAborted,
        SocketClosed,
        NoSocketConnection,
    };

    public __gc class MScriptSession
        : public MBaseSession
        , public IDimse
        , public ISockets
        , public IDul
        , public ISession
        , public System::IDisposable
    {
    private protected:
        ConfirmInteractionAdapter __nogc* m_pConfirmInteractionAdapter;
    private:
        SCRIPT_SESSION_CLASS __nogc* m_pSCRIPT_SESSION_CLASS;
    private protected:
        __property BASE_SESSION_CLASS __nogc* get_m_pBASE_SESSION()
        {
            return m_pSCRIPT_SESSION_CLASS;
        }
    public:
        // <summary>
        // IConfirmInteractionTarget
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_ConfirmInteractionTarget(IConfirmInteractionTarget __gc* value);
    public:
        // <summary>
        // Constructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        MScriptSession(void);
    public:
        // <summary>
        // Destructor
        // </summary>
        // <remarks>
        // None
        // </remarks>
        ~MScriptSession(void);

    public:
        // <summary>
        // No Of Dicom Scripts
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::UInt16 get_NrOfDicomScripts();

    public:
        ReceiveReturnCode Receive([Out] DvtkData::Message __gc*__gc* ppMessage);
    public:
        ReceiveReturnCode Receive([Out] DvtkData::Dul::DulMessage __gc*__gc* ppDulMessage);
    public:
        ReceiveReturnCode Receive([Out] DvtkData::Dimse::DicomMessage __gc*__gc* ppDicomMessage);
    public:
        SendReturnCode Send(DvtkData::Message __gc* pMessage);
    public:
        bool Validate(
            DvtkData::Dimse::DicomMessage __gc* pMessage,
            DvtkData::Dimse::DicomMessage __gc* pReferenceMessage,
            Wrappers::ValidationControlFlags validationControlFlags);
    public:
        bool Validate(
            DvtkData::Dul::DulMessage __gc* pMessage,
            DvtkData::Dul::DulMessage __gc* pReferenceMessage,
            Wrappers::ValidationControlFlags validationControlFlags);

    private:
        // Track whether Dispose has been called.
        // m_pSCRIPT_SESSION_CLASS is treated as disposable resource.
        bool disposed;

    public:
        void Dispose();

    private:
        void Dispose(bool disposing);
    };
}
