// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
    public __gc class MPrinter
    {
    private public:
        MPrinter(void);
    private public:
        ~MPrinter(void);
    public:
        static System::UInt32 getNrOfStatusInfoDefinedTerms();
    public:
        static System::String __gc* getStatusInfoDefinedTerm(System::UInt32 index);
    public:
        // <summary>
        // Get the Manufacturer
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_Manufacturer();
        // <summary>
        // Get the ModelName
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_ModelName();
        // <summary>
        // Get the Name
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_Name();
        // <summary>
        // Get the SerialNumber
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_SerialNumber();
        // <summary>
        // Get the Status
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_Status();
        // <summary>
        // Set the Status
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_Status(System::String __gc* value);
        // <summary>
        // Get the StatusInfo
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_StatusInfo();
        // <summary>
        // Set the StatusInfo
        // </summary>
        // <remarks>
        // Set Property
        // </remarks>
        __property void set_StatusInfo(System::String __gc* value);
        // <summary>
        // Get the CalibrationTime
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::DateTime get_CalibrationTime();
        // <summary>
        // Get the SoftwareVersions
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::String __gc* get_SoftwareVersions();
        // <summary>
        // Get the CalibrationDate
        // </summary>
        // <remarks>
        // Get Property
        // </remarks>
        __property System::DateTime get_CalibrationDate();
    };
}