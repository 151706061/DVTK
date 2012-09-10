//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "DICOMObjectDefinition.h"
#include "ModuleDefinition.h"


//>>===========================================================================

DEF_DICOM_OBJECT_CLASS::DEF_DICOM_OBJECT_CLASS()

//  DESCRIPTION     : Default constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
}

//>>===========================================================================

DEF_DICOM_OBJECT_CLASS::DEF_DICOM_OBJECT_CLASS(const string name)

//  DESCRIPTION     : Constructor with name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	nameM = name;
}

//>>===========================================================================

DEF_DICOM_OBJECT_CLASS::~DEF_DICOM_OBJECT_CLASS()

//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
	for (UINT i = 0; i < modulesM.size(); i++)
	{
		delete modulesM[i];
	}
	modulesM.clear();
}

//>>===========================================================================

void DEF_DICOM_OBJECT_CLASS::AddModule(DEF_MODULE_CLASS* mod_ptr)

//  DESCRIPTION     : Adds module to DICOM Object.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	modulesM.push_back(mod_ptr);
}

//>>===========================================================================

DEF_MODULE_CLASS* DEF_DICOM_OBJECT_CLASS::GetModule(UINT index)

//  DESCRIPTION     : Returns indexed module.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DEF_MODULE_CLASS* module_ptr = NULL;

	// check index in range
	if (index < modulesM.size())
	{
		module_ptr = modulesM[index];
	}

	// return module pointer
	return module_ptr;
}

//>>===========================================================================

DEF_MODULE_CLASS* DEF_DICOM_OBJECT_CLASS::GetModule(UINT16 group, UINT16 element)

//  DESCRIPTION     : Searches for a module containing the requested attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DEF_MODULE_CLASS* module_ptr = NULL;

	// seach through all modules
	for (UINT i = 0; i < modulesM.size(); i++)
	{
		module_ptr = modulesM[i];
		if (module_ptr == NULL) continue;

		// check if attribute is in this module
		if (module_ptr->GetAttribute(group, element)) break;
	}

    // return module pointer
	return module_ptr;
}

//>>===========================================================================

DEF_ATTRIBUTE_CLASS* DEF_DICOM_OBJECT_CLASS::GetAttribute(UINT16 group, UINT16 element)

//  DESCRIPTION     : Searches for an attribute in the object definition.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DEF_ATTRIBUTE_CLASS* attribute_ptr = NULL;

	// seach through all modules
	for (UINT i = 0; i < modulesM.size(); i++)
	{
		DEF_MODULE_CLASS *module_ptr = modulesM[i];
		if (module_ptr == NULL) continue;

		attribute_ptr = module_ptr->GetAttribute(group, element);
		if (attribute_ptr) break;
	}
    
	// return attribute pointer
	return attribute_ptr;
}

//>>===========================================================================

DEF_ATTRIBUTE_CLASS* DEF_DICOM_OBJECT_CLASS::GetAttribute(string moduleName, UINT16 group, UINT16 element)

//  DESCRIPTION     : Searches for an attribute in the object definition.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DEF_ATTRIBUTE_CLASS *attribute_ptr = NULL;

	// seach through all modules
	for (UINT i = 0; i < modulesM.size(); i++)
	{
		DEF_MODULE_CLASS *module_ptr = modulesM[i];
		if (module_ptr == NULL) continue;

		// check if module name matches
		if (module_ptr->GetName() == moduleName)
		{
			attribute_ptr = module_ptr->GetAttribute(group, element);
			if (attribute_ptr) break;
		}
	}

	// return attribute pointer
	return attribute_ptr;
}
