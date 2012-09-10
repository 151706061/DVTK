// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once
#include "ISessions.h"
#include <vcclr.h>

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    public __value enum CountGroup
    {
        Validation = 0,
        General = 1,
        User = 2,
    };

    public __value enum CountType
    {
        Error = 0,
        Warning = 1,
//        Info,
    };

    // Interface to implement on the call-back counting class.
    public __gc __interface ICountingTarget
    {
        void Increment(CountGroup group, CountType type);
        __property System::UInt32 get_NrOfErrors();
        __property System::UInt32 get_NrOfWarnings();

		/// <summary>
        /// Number of validation errors.
        /// </summary>
        __property System::UInt32 get_NrOfValidationErrors();
        /// <summary>
        /// Number of validation warnings.
        /// </summary>
        __property System::UInt32 get_NrOfValidationWarnings();
        /// <summary>
        /// Number of general errors.
        /// </summary>
        __property System::UInt32 get_NrOfGeneralErrors();
        /// <summary>
        /// Number of general warnings.
        /// </summary>
        __property System::UInt32 get_NrOfGeneralWarnings();
        /// <summary>
        /// Number of user errors.
        /// </summary>
        __property System::UInt32 get_NrOfUserErrors();
        /// <summary>
        /// Number of user warnings.
        /// </summary>
        __property System::UInt32 get_NrOfUserWarnings();

		/// <summary>
        /// Total number of validation errors - including any child counts.
        /// </summary>
        __property System::UInt32 get_TotalNrOfValidationErrors();
        /// <summary>
        /// Total number of validation warnings - including any child counts.
        /// </summary>
        __property System::UInt32 get_TotalNrOfValidationWarnings();
        /// <summary>
        /// Total number of general errors - including any child counts.
        /// </summary>
        __property System::UInt32 get_TotalNrOfGeneralErrors();
        /// <summary>
        /// Total number of general warnings - including any child counts.
        /// </summary>
        __property System::UInt32 get_TotalNrOfGeneralWarnings();
        /// <summary>
        /// Total number of user errors - including any child counts.
        /// </summary>
        __property System::UInt32 get_TotalNrOfUserErrors();
        /// <summary>
        /// Total number of user warnings - including any child counts.
        /// </summary>
        __property System::UInt32 get_TotalNrOfUserWarnings();

        /// <summary>
        /// Reset counters.
        /// </summary>
		void Init();

		ICountingTarget __gc* CreateChildCountingTarget();
    };

    __nogc class CountingAdapter
    {
    public:
        // ctor
        CountingAdapter(ICountingTarget __gc* value);
        // dtor
        ~CountingAdapter(void);

	public:
        gcroot<ICountingTarget*> m_pCountingTarget;
    };
}