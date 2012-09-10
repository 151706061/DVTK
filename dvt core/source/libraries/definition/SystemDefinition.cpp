//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "SystemDefinition.h"
#include "AEDefinition.h"


//>>===========================================================================

DEF_SYSTEM_CLASS::DEF_SYSTEM_CLASS()

//  DESCRIPTION     : Default contructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
}

//>>===========================================================================

DEF_SYSTEM_CLASS::DEF_SYSTEM_CLASS(const string name, const string version)

//  DESCRIPTION     : Default contructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
    nameM = name;
	versionM = version;
}

//>>===========================================================================

DEF_SYSTEM_CLASS::~DEF_SYSTEM_CLASS()

//  DESCRIPTION     : Default destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
	// - delete AE definitions
    for (UINT i = 0; i < aesM.size(); i++)
	{
		delete aesM[i];
	}
	aesM.clear();
}

//>>===========================================================================

void DEF_SYSTEM_CLASS::SetName(const string name)

//  DESCRIPTION     : Set Name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
	nameM = name; 
}

//>>===========================================================================

void DEF_SYSTEM_CLASS::SetVersion(const string version)

//  DESCRIPTION     : Set Version.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
	versionM = version; 
}

//>>===========================================================================

string DEF_SYSTEM_CLASS::GetName()

//  DESCRIPTION     : Get Name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
	return nameM; 
}

//>>===========================================================================

string DEF_SYSTEM_CLASS::GetVersion()

//  DESCRIPTION     : Get Version.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{ 
	return versionM; 
}

//>>===========================================================================

void DEF_SYSTEM_CLASS::AddAE(DEF_AE_CLASS* ae_ptr)

//  DESCRIPTION     : Adds Application Entity to system
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	aesM.push_back(ae_ptr);
}

//>>===========================================================================

UINT DEF_SYSTEM_CLASS::GetNrAes()

//  DESCRIPTION     : Get number of AEs known to the System
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return aesM.size(); 
}

//>>===========================================================================

DEF_AE_CLASS* DEF_SYSTEM_CLASS::GetAE(UINT i)

//  DESCRIPTION     : Get the indexed AE from the System.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DEF_AE_CLASS *ae_ptr = NULL;

	if (i < aesM.size())
	{
		ae_ptr = aesM[i];
	}

	return ae_ptr;
}

//>>===========================================================================

DEF_AE_CLASS* DEF_SYSTEM_CLASS::GetAE(const string AEname, const string AEversion)

//  DESCRIPTION     : Finds Application Entity in a system
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DEF_AE_CLASS* ae_ptr = NULL;

	for (UINT i = 0; i < aesM.size(); i++)
	{
		if ((aesM[i]->GetName() == AEname) && 
			(aesM[i]->GetVersion() == AEversion))
		{
			ae_ptr = aesM[i];
			break;
		}
	}

    return ae_ptr;
}

//>>===========================================================================

DEF_AE_CLASS* DEF_SYSTEM_CLASS::GetAE(const string filename)

//  DESCRIPTION     : Finds Application Entity that contains a definition instance
//					: with the given filename
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	DEF_AE_CLASS* ae_ptr = NULL;

	for (UINT i = 0; i < aesM.size(); i++)
	{
		if (aesM[i]->ContainsInstance(filename))
		{
			ae_ptr = aesM[i];
			break;
		}
	}

    return ae_ptr;
}
