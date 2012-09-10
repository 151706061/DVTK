//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_properties.h"
#include "Iglobal.h"		// Global component interface

//*****************************************************************************
//  EXTERNAL DEFINITIONS
//*****************************************************************************

//>>===========================================================================
ACSE_PROPERTIES_CLASS::ACSE_PROPERTIES_CLASS()
//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
}

//>>===========================================================================
ACSE_PROPERTIES_CLASS::~ACSE_PROPERTIES_CLASS()
//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
}

//>>===========================================================================
void ACSE_PROPERTIES_CLASS::setCalledAeTitle (const string calledAeTitle)
//  DESCRIPTION     : set the CalledAeTitle member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	this->calledAeTitleM = calledAeTitle;
}

//>>===========================================================================
string ACSE_PROPERTIES_CLASS::getCalledAeTitle (void)
//  DESCRIPTION     : returns the CalledAeTitle member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (this->calledAeTitleM);
}

//>>===========================================================================
void ACSE_PROPERTIES_CLASS::setCallingAeTitle (const string callingAeTitle)
//  DESCRIPTION     : set the CalledAeTitle member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	this->callingAeTitleM = callingAeTitle;
}

//>>===========================================================================
string ACSE_PROPERTIES_CLASS::getCallingAeTitle (void)
//  DESCRIPTION     : returns the CalledAeTitle member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (this->callingAeTitleM);
}

//>>===========================================================================
void ACSE_PROPERTIES_CLASS::setMaximumLengthReceived (UINT32 maximumLengthReceived)
//  DESCRIPTION     : set the MaximumLengthReceived member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	this->maximumLengthReceivedM = maximumLengthReceived;
}

//>>===========================================================================
UINT32 ACSE_PROPERTIES_CLASS::getMaximumLengthReceived (void)
//  DESCRIPTION     : returns the MaximumLengthReceived member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (this->maximumLengthReceivedM);
}

//>>===========================================================================
void ACSE_PROPERTIES_CLASS::setImplementationClassUid (const string implementationClassUid)
//  DESCRIPTION     : set the ImplementationClassUid member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	this->implementationClassUidM = implementationClassUid;
}

//>>===========================================================================
string ACSE_PROPERTIES_CLASS::getImplementationClassUid (void)
//  DESCRIPTION     : returns the ImplementationClassUid member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (this->implementationClassUidM);
}

//>>===========================================================================
void ACSE_PROPERTIES_CLASS::setImplementationVersionName (const string implementationVersionName)
//  DESCRIPTION     : set the ImplementationVersionName member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	this->implementationVersionNameM = implementationVersionName;
}

//>>===========================================================================
string ACSE_PROPERTIES_CLASS::getImplementationVersionName (void)
//  DESCRIPTION     : returns the ImplementationVersionName member variable
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (this->implementationVersionNameM);
}
