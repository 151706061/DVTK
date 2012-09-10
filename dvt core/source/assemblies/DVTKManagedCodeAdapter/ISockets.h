// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
    public __value enum SutRole
    {
        SutRoleAcceptorRequestor,
        SutRoleAcceptor,
        SutRoleRequestor,
    };

    public __gc __interface ISockets
    {
        __property Wrappers::SutRole get_SutRole();
        __property void set_SutRole(Wrappers::SutRole value);
        __property System::String __gc* get_SutHostname();
        __property void set_SutHostname(System::String __gc* value);
        __property System::UInt16 get_SutPort();
        __property void set_SutPort(System::UInt16 value);
        __property System::UInt16 get_DvtPort();
        __property void set_DvtPort(System::UInt16 value);
        __property System::UInt16 get_DvtSocketTimeOut();
        __property void set_DvtSocketTimeOut(System::UInt16 value);
        __property bool get_UseSecureSockets();
        __property void set_UseSecureSockets(bool value);
        __property System::String __gc* get_TlsPassword();
        __property void set_TlsPassword(System::String __gc* value);
        __property System::String __gc* get_TlsVersion();
        __property void set_TlsVersion(System::String __gc* value);
        __property bool get_CheckRemoteCertificate();
        __property void set_CheckRemoteCertificate(bool value);
        __property System::String __gc* get_CipherList();
        __property void set_CipherList(System::String __gc* value);
        __property bool get_CacheTlsSessions();
        __property void set_CacheTlsSessions(bool value);
        __property System::UInt16 get_TlsCacheTimeout();
        __property void set_TlsCacheTimeout(System::UInt16 value);
        __property System::String __gc* get_CredentialsFileName();
        __property void set_CredentialsFileName(System::String __gc* value);
        __property System::String __gc* get_CertificateFileName();
        __property void set_CertificateFileName(System::String __gc* value);
        __property void set_SocketParametersChanged(bool value);
    };
}
