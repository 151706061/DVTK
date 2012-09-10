// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright � 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "MCertificate.h"

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    public __value enum WrappedSecurityItemType
    {
        Credential,
        Certificate,
    };

    namespace Exceptions
    {
    public __gc class SecureSocketLibraryNotAvailableException : public System::ApplicationException
    {
    public:
        SecureSocketLibraryNotAvailableException() {};
    public:
        SecureSocketLibraryNotAvailableException(System::String __gc* message) : System::ApplicationException(message) {};
    public:
        SecureSocketLibraryNotAvailableException(System::String __gc* message, System::Exception __gc* inner) : System::ApplicationException(message, inner) {};
    };

    public __gc class CredentialFileLoadExpection : public System::ApplicationException
    {
    public:
        CredentialFileLoadExpection() {};
    public:
        CredentialFileLoadExpection(System::String __gc* message) : System::ApplicationException(message) {};
    public:
        CredentialFileLoadExpection( System::String __gc* message, System::Exception __gc* inner) : System::ApplicationException(message, inner) {};
    };

    public __gc class CertificateFileLoadExpection : public System::ApplicationException
    {
    public:
        CertificateFileLoadExpection() {};
    public:
        CertificateFileLoadExpection(System::String __gc* message) : System::ApplicationException(message) {};
    public:
        CertificateFileLoadExpection(System::String __gc* message, System::Exception __gc* inner) : System::ApplicationException(message, inner) {};
    };

    public __gc class PasswordExpection : public System::ApplicationException
    {
    public:
        PasswordExpection() {};
    public:
        PasswordExpection(System::String __gc* message) : System::ApplicationException(message) {};
    public:
        PasswordExpection(System::String __gc* message, System::Exception __gc* inner) : System::ApplicationException(message, inner) {};
    };
    }

    public __gc class MCertificateFile
    {
    private protected:
        CERTIFICATE_FILE_CLASS __nogc* m_pC;

    public:
        // throws on dvt error status
        MCertificateFile(
            Wrappers::WrappedSecurityItemType securityFileType,
            System::String __gc* pFileName,
            System::String __gc* pPassword);
    public:
        ~MCertificateFile(void);

    public:
        // throws on dvt error status
        void AddCertificate(
            Wrappers::WrappedSecurityItemType securityItemType,
            System::String __gc* pFileName,
            System::String __gc* pPassword);
        
    public:
        System::Boolean RemoveCertificate(System::UInt32 index);
        
    public:
        // <summary>
        // Get number of certificates
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::UInt32 get_NumberOfCertificates();

    public:
        // <summary>
        // Get certificate
        // </summary>
        // <remarks>
        // Get Indexed Property
        // </remarks>
        __property Wrappers::MCertificate __gc* get_Certificate(System::UInt32 index);
        
    public:
        // <summary>
        // Get the password
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_Password();

    public:
        // <summary>
        // Get the HasChanged state
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::Boolean get_HasChanged();

    public:
        void WriteFile(System::String __gc* pPassword);

    public:
        System::Boolean Verify(
            Wrappers::WrappedSecurityItemType securityItemType,
            [System::Runtime::InteropServices::Out] System::String __gc*__gc* ppVerificationMessage);
    };
}