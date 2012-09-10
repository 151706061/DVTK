// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include ".\UtilityFunctions.h"
#include < vcclr.h >
#using <mscorlib.dll>

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    void MarshalString ( System::String* s, std::string& os )
    {
        using namespace System::Runtime::InteropServices;
        const char* chars = 
            (const char*)(Marshal::StringToHGlobalAnsi(s)).ToPointer();
        os = chars;
        Marshal::FreeHGlobal(System::IntPtr((void*)chars));
    }

    std::string ConvertString(std::string inString)
    {
        std::string outString;
        for (UINT i = 0; i < inString.length(); i++)
        {
            char valueString[16];
            BYTE value = (BYTE) inString[i];

            if ((value >= 0) && 
                (value < 32))
            {
                // char in range 0..31
                switch(value)
                {
                case 9: sprintf(valueString, "&#x09;"); break;
                case 10: sprintf(valueString, "[LF]"); break;
                case 12: sprintf(valueString, "[FF]"); break;
                case 13: sprintf(valueString, "[CR]"); break;
                case 27: sprintf(valueString, "[ESC]"); break;
                default: sprintf(valueString, "\\%02X", value); break;
                }
            }
            else if ((value > 126) &&
                (value <= 255))
            {
                // char in range 127..255
               sprintf(valueString, "\\%02X", value);
            }
            else
            {
                switch(value)
                {
                case 38: sprintf(valueString, "&#x26;"); break; // &
                case 60: sprintf(valueString, "&#x3C;"); break; // <
                case 62: sprintf(valueString, "&#x3E;"); break; // >
                default:
                    // char in range 32..127
                    valueString[0] = value;
                    valueString[1] = 0x00;
                    break;
                }
            }

            // add the character value to the string
            outString += valueString;
        }

        return outString;
    }
}