// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "Ilog.h"
#include "ISessions.h"
#include <vcclr.h>

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;
    
    public __gc __interface IConfirmInteractionTarget
    {
        void ConfirmInteraction();
    };
    
    __nogc class ConfirmInteractionAdapter
        : public BASE_CONFIRMER
    {
    public:
        // ctor
        ConfirmInteractionAdapter(IConfirmInteractionTarget __gc* value);
        // dtor
        ~ConfirmInteractionAdapter(void);

        void ConfirmInteraction();

    private:
        gcroot<IConfirmInteractionTarget*> m_pConfirmInteractionTarget;
    };
}