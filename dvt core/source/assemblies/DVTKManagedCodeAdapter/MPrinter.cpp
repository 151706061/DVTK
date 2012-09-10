// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include ".\MPrinter.h"
#include "Iprint.h"		// Printer component interface
#using <mscorlib.dll>

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    MPrinter::MPrinter(void)
    {
//        m_pC = new MYPRINTER();
    }

    MPrinter::~MPrinter(void)
    {
//        delete m_pC;
    }

    System::UInt32 MPrinter::getNrOfStatusInfoDefinedTerms()
    {
        return MYPRINTER->getNoStatusInfoDTs();
    }
    
    System::String __gc* MPrinter::getStatusInfoDefinedTerm(System::UInt32 index)
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getStatusInfoDT(index).c_str();
    }

    System::String __gc* MPrinter::get_Manufacturer ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getManufacturer();
    }
    System::String __gc* MPrinter::get_ModelName ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getModelName();
    }
    System::String __gc* MPrinter::get_Name ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getName();
    }
    System::String __gc* MPrinter::get_SerialNumber ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getSerialNumber();
    }
    System::String __gc* MPrinter::get_Status ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getStatus();
    }
    void MPrinter::set_Status (System::String __gc *value)
    {
        char* pAnsiString = (char*)(void*)Marshal::StringToHGlobalAnsi(value);
        MYPRINTER->setStatus(pAnsiString);
        Marshal::FreeHGlobal(pAnsiString);
    }
    System::String __gc* MPrinter::get_StatusInfo ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getStatusInfo();
    }
    void MPrinter::set_StatusInfo (System::String __gc *value)
    {
        char* pAnsiString = (char*)(void*)Marshal::StringToHGlobalAnsi(value);
        MYPRINTER->setStatusInfo(pAnsiString);
        Marshal::FreeHGlobal(pAnsiString);
    }
    System::DateTime MPrinter::get_CalibrationTime ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        // DICOM "HHMMSS"
        System::String __gc* pTimeString = MYPRINTER->getCalibrationTime();
        System::String __gc* pFormat = "HHmmss";
        System::DateTime time =
            System::DateTime::ParseExact(pTimeString, pFormat, NULL);
        return time;
    }
    System::String __gc* MPrinter::get_SoftwareVersions ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        return MYPRINTER->getSoftwareVersion();
    }
    System::DateTime MPrinter::get_CalibrationDate ()
    {
        // Implicit marshalling from const char* to System::String by Managed C++ compiler.
        // DICOM "yyyyMMdd"
        System::String __gc* pDateString = MYPRINTER->getCalibrationDate();
        System::String __gc* pFormat = "yyyyMMdd";
        System::DateTime date =
            System::DateTime::ParseExact(pDateString, pFormat, NULL);
        return date;
    }
}