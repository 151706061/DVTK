// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include "ConfirmInteractionAdapter.h"
#include < vcclr.h >
#using <mscorlib.dll>

namespace Wrappers
{
    // ctor
    ConfirmInteractionAdapter::ConfirmInteractionAdapter(IConfirmInteractionTarget __gc* value)
    {
        m_pConfirmInteractionTarget = value;
    }
    // dtor
    ConfirmInteractionAdapter::~ConfirmInteractionAdapter(void)
    {}

    void ConfirmInteractionAdapter::ConfirmInteraction()
    {
        // Translation from unmanaged -> managed CLR
        m_pConfirmInteractionTarget->ConfirmInteraction();
    } 
}