// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    public __gc class MCertificate
    {
    private:
        static inline bool _RangeCheckDateTimeInput(
            int year,
            int mon,
            int mday,
            int hour,
            int min,
            int sec);

    private protected:
        CERTIFICATE_CLASS __nogc* m_pC;

    public:
        MCertificate(void);
        ~MCertificate(void);

    private public:
        void Wrap(CERTIFICATE_CLASS* pNewCERTIFICATE_CLASS);

    public:
        // <summary>
        // EffectiveDate
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::DateTime get_EffectiveDate();
    public:
        // <summary>
        // EffectiveDate
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_EffectiveDate(System::DateTime value);

    public:
        // <summary>
        // ExpirationDate
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::DateTime get_ExpirationDate();
    public:
        // <summary>
        // ExpirationDate
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_ExpirationDate(System::DateTime value);

    public:
        // <summary>
        // Issuer
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* MCertificate::get_Issuer();
    public:
        // <summary>
        // Issuer
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_Issuer(System::String __gc* value);

    public:
        // <summary>
        // SerialNumber
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_SerialNumber();
    public:
        // <summary>
        // SerialNumber
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_SerialNumber(System::String __gc* value);

    public:
        // <summary>
        // SignatureAlgorithm
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_SignatureAlgorithm();
    public:
        // <summary>
        // SignatureAlgorithm
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_SignatureAlgorithm(System::String __gc* value);

    public:
        // <summary>
        // SignatureKeyLength
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::Int32 get_SignatureKeyLength();
    public:
        // <summary>
        // SignatureKeyLength
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_SignatureKeyLength(System::Int32 value);

    public:
        // <summary>
        // Subject
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* MCertificate::get_Subject();
    public:
        // <summary>
        // Subject
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_Subject(System::String __gc* value);

    public:
        // <summary>
        // Version
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::Int32 get_Version();
    public:
        // <summary>
        // Version
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_Version(System::Int32 value);

    public:
        void GenerateFiles(
            System::String __gc* pSignerCredentialsFile,
            System::String __gc* pCredentialsPassword,
            System::String __gc* pKeyPassword,
            System::String __gc* pKeyFile,
            System::String __gc* pCertificateFile);
    };
}