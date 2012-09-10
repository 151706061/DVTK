#include "StdAfx.h"
#include ".\MDICOMDIRGenerator.h"
#using <mscorlib.dll>

namespace Wrappers
{
	using namespace System::Runtime::InteropServices;

	MDICOMDIRGenerator::MDICOMDIRGenerator(void)
	{
	}

	MDICOMDIRGenerator::~MDICOMDIRGenerator(void)
	{
	}

	bool MDICOMDIRGenerator::WCreateDICOMDIR(System::String* dcmFileNames[])
	{
		GENERATE_DICOMDIR_CLASS* pDICOMDIR = new GENERATE_DICOMDIR_CLASS();

		vector<string> fileNameVector;
        int size = dcmFileNames->Length;
        for (int idx = 0; idx < size; idx++)
        {
            System::String* value = dcmFileNames[idx];
            char* pAnsiString = (char*)(void*)Marshal::StringToHGlobalAnsi(value);
            string file = pAnsiString;
            fileNameVector.push_back(file);
            Marshal::FreeHGlobal(pAnsiString);
        }
        
		bool ok = pDICOMDIR->generateDICOMDIR(&fileNameVector);
		delete pDICOMDIR;
        return ok;
	}
}