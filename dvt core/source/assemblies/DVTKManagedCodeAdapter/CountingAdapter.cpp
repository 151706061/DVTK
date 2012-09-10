// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include "CountingAdapter.h"
#include < vcclr.h >
#using <mscorlib.dll>

namespace Wrappers
{
    CountingAdapter::CountingAdapter(ICountingTarget __gc* countingTarget)
    {
        m_pCountingTarget = countingTarget;
    }

    CountingAdapter::~CountingAdapter(void)
    {
    }	
}