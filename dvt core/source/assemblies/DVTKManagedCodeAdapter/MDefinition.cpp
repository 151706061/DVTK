// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include ".\MDefinition.h"
#include < vcclr.h >
#using <mscorlib.dll>

namespace Wrappers
{
    using namespace System::Runtime::InteropServices;

    MDefinitionFileDetails 
        MDefinition::get_FileDetails(System::String __gc* pFileName)
    {
        DEF_DETAILS_CLASS retDEF_DETAILS_CLASS;
        std::string sFileName;
		bool success = false;

        Wrappers::MarshalString(pFileName, sFileName);

		try
		{
			success = DEFINITION->GetDetails(sFileName, retDEF_DETAILS_CLASS);
		}
		catch(char* exception)
		{
			throw new System::ApplicationException(exception);
		}

		if (!success) 
		{
			throw new System::ApplicationException("Definition File Not Found");
		}

        MDefinitionFileDetails definitionFileDetails;

		// Application Entity name.
		definitionFileDetails.AEName = retDEF_DETAILS_CLASS.GetAEName().c_str();

		if (definitionFileDetails.AEName->Length == 0)
		{
			throw new System::ApplicationException("Application Entity name is empty.");
		}

		// Application Entity version.
		definitionFileDetails.AEVersion = retDEF_DETAILS_CLASS.GetAEVersion().c_str();

		if (definitionFileDetails.AEVersion->Length  == 0)
		{
			throw new System::ApplicationException("Application Entity version is empty.");
		}

		// SOP Class name.
		definitionFileDetails.SOPClassName = retDEF_DETAILS_CLASS.GetSOPClassName().c_str();

		if (definitionFileDetails.SOPClassName->Length == 0)
		{
			throw new System::ApplicationException("SOP Class name is empty.");
		}

		//  SOP Class UID.
		definitionFileDetails.SOPClassUID = retDEF_DETAILS_CLASS.GetSOPClassUID().c_str();

		if (definitionFileDetails.SOPClassUID->Length == 0)
		{
			throw new System::ApplicationException("SOP Class UID is empty.");
		}

		// Is meta SOP class.
		definitionFileDetails.IsMetaSOPClass = retDEF_DETAILS_CLASS.GetIsMetaSOPClass();

        return definitionFileDetails;
    }
}