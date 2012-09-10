//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "DefinitionDetails.h"

//>>===========================================================================

DEF_DETAILS_CLASS::DEF_DETAILS_CLASS()

//  DESCRIPTION     : Default constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	IsMetaSOPClassM = false;
}

//>>===========================================================================

DEF_DETAILS_CLASS::~DEF_DETAILS_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
}

//>>===========================================================================

void DEF_DETAILS_CLASS::SetAEName(const string name)

//  DESCRIPTION     : Set AE Name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	AENameM = name;
}

//>>===========================================================================

void DEF_DETAILS_CLASS::SetAEVersion(const string version)

//  DESCRIPTION     : Set AE Version.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	AEVersionM = version;
}

//>>===========================================================================

void DEF_DETAILS_CLASS::SetSOPClassName(const string name)

//  DESCRIPTION     : Set SOP Class Name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	SOPClassNameM = name;
}

//>>===========================================================================

void DEF_DETAILS_CLASS::SetSOPClassUID(const string uid)

//  DESCRIPTION     : Set SOP Class UID.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	SOPClassUIDM = uid;
}

//>>===========================================================================

void DEF_DETAILS_CLASS::SetIsMetaSOPClass(bool value)

//  DESCRIPTION     : Indicate if the Class is a Meta SOP Class or not.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	IsMetaSOPClassM = value;
}

//>>===========================================================================

string DEF_DETAILS_CLASS::GetAEName()

//  DESCRIPTION     : Get AE Name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return AENameM;
}

//>>===========================================================================

string DEF_DETAILS_CLASS::GetAEVersion()

//  DESCRIPTION     : Get AE Version.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return AEVersionM;
}

//>>===========================================================================

string DEF_DETAILS_CLASS::GetSOPClassName()

//  DESCRIPTION     : Get SOP Class Name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return SOPClassNameM;
}

//>>===========================================================================

string DEF_DETAILS_CLASS::GetSOPClassUID()

//  DESCRIPTION     : Get SOP Class UID.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return SOPClassUIDM;
}

//>>===========================================================================

bool DEF_DETAILS_CLASS::GetIsMetaSOPClass()

//  DESCRIPTION     : Get indication if SOP Class is a Meta SOP Class or not.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	return IsMetaSOPClassM;
}
