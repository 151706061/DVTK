// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include "MSessionFactory.h"
#include "MEmulatorSession.h"
#include "MMediaSession.h"
#include "MScriptSession.h"
#include < vcclr.h >
#using <mscorlib.dll>

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;
    MBaseSession __gc* MSessionFactory::Load (System::String __gc* sessionFileName)
    {
        ABSTRACT_SESSION_CLASS  abstractSession;
        bool definitionFileLoaded = true;
        MBaseSession __gc* session = NULL;
        char* pAnsiString = (char*)(void*)Marshal::StringToHGlobalAnsi(sessionFileName);
        bool sessionLoaded = abstractSession.load (pAnsiString, definitionFileLoaded, false);
        Marshal::FreeHGlobal(pAnsiString);

        if (!sessionLoaded) 
            throw new System::ApplicationException(
            System::String::Format("Could not load session {0}.", sessionFileName)
            );

        switch (abstractSession.getSessionType())
        {
        case ST_SCRIPT:
            session = new MScriptSession ();
            break;
        case ST_EMULATOR:
            session = new MEmulatorSession ();
            break;
        case ST_MEDIA:
            session = new MMediaSession ();
            break;
        default:
            // Unknown session type
            throw new System::NotImplementedException();
        }
        session->Load (sessionFileName, definitionFileLoaded, true);
        return session;
    }
}