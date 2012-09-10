//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_user_identity_negotiation_validator.h"
#include "Iglobal.h"      // Global component interface file
#include "Ilog.h"         // Logging component interface file
#include "Inetwork.h"     // Network component interface file

//*****************************************************************************
//  STATIC DECLARATIONS
//*****************************************************************************
static T_BYTE_TEXT_MAP	TUIType[] = {
{UIN_UIT_USERNAME,			"Username as a string in UTF-8"},
{UIN_UIT_USERNAME_PASSCODE,	"Username as a string in UTF-8 and passcode"},
{UIN_UIT_KERBEROS,			"Kerberos Service ticket"},
{BYTE_SENTINAL,				"unknown"}
};

static T_BYTE_TEXT_MAP	TPRRequested[] = {
{UIN_PRR_NO_RESPONSE,		"No response requested"},
{UIN_PRR_POSITIVE_RESPONSE,	"Positive response requested"},
{BYTE_SENTINAL,				"unknown"}
};

//>>===========================================================================

char *UIType(BYTE result)

//  DESCRIPTION     : User Identity - type LUT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	int		index = 0;

	while ((TUIType[index].code != result)
	  && (TUIType[index].code != BYTE_SENTINAL))
		index++;

	return TUIType[index].text;
}

//>>===========================================================================

char *PRRequested(BYTE result)

//  DESCRIPTION     : Positive Response - requested LUT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	int		index = 0;

	while ((TPRRequested[index].code != result)
	  && (TPRRequested[index].code != BYTE_SENTINAL))
		index++;

	return TPRRequested[index].text;
}

//>>===========================================================================		

ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS()

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

ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::~ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS()

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

ACSE_PARAMETER_CLASS *ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::getUserIdentityTypeParameter()

//  DESCRIPTION     : Get User Identity Type
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &userIdentityTypeM; 
}

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::getPositiveResponseRequestedParameter()

//  DESCRIPTION     : Get Positive Response Requested
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &positiveResponseRequestedM; 
}

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::getPrimaryFieldParameter()

//  DESCRIPTION     : Get Primary Field
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &primaryFieldM; 
}

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::getSecondaryFieldParameter()

//  DESCRIPTION     : Get Secondary Field
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &secondaryFieldM; 
}

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::getServerResponseParameter()

//  DESCRIPTION     : Get Server Response
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &serverResponseM; 
}

//>>===========================================================================		

bool ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::validate(BYTE srcUserIdentityType, BYTE srcPositiveResponseRequested, char *srcPrimaryField_ptr, char *srcSecondaryField_ptr, USER_INFORMATION_CLASS *refUserInfo_ptr)

//  DESCRIPTION     : Validate User Identity Negotiation.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	char buffer[MAXIMUM_LINE_LENGTH];
	string refUserIdentityType;
	string refPositiveResponseRequested;
	string refPrimaryField;
	string refSecondaryField;
	
	// check for matching reference values
	if ((refUserInfo_ptr) &&
		(refUserInfo_ptr->getUserIdentityNegotiationPrimaryField()))
	{
		BYTE userIdentityType = refUserInfo_ptr->getUserIdentityNegotiationUserIdentityType();
		sprintf(buffer, "%d", userIdentityType);
		refUserIdentityType = buffer;

		BYTE positiveResponseRequested = refUserInfo_ptr->getUserIdentityNegotiationPositiveResponseRequested();
		sprintf(buffer, "%d", positiveResponseRequested);
		refPositiveResponseRequested = buffer;

		refPrimaryField = refUserInfo_ptr->getUserIdentityNegotiationPrimaryField();

		if (refUserInfo_ptr->getUserIdentityNegotiationSecondaryField())
		{
			refSecondaryField = refUserInfo_ptr->getUserIdentityNegotiationSecondaryField();
		}
	}

	// validate parameters
	sprintf(buffer, "%d", srcUserIdentityType);
	bool result1 = userIdentityTypeM.validate(buffer, refUserIdentityType);
    userIdentityTypeM.setMeaning(UIType(srcUserIdentityType));

	sprintf(buffer, "%d", srcPositiveResponseRequested);
	bool result2 = positiveResponseRequestedM.validate(buffer, refPositiveResponseRequested);
    positiveResponseRequestedM.setMeaning(PRRequested(srcPositiveResponseRequested));

	bool result3 = true;
	if (srcPrimaryField_ptr)
	{
		result3 = primaryFieldM.validate(srcPrimaryField_ptr, refPrimaryField);
	}
	bool result4 = true;
	if (srcSecondaryField_ptr)
	{
		result4 = secondaryFieldM.validate(srcSecondaryField_ptr, refSecondaryField);
	}

	// set result
	bool result = true;
	if ((!result1) ||
		(!result2) ||
		(!result3) ||
		(!result4))
	{
		result = false;
	}
	
	// return result
	return result;
}

//>>===========================================================================		

bool ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS::validate(char *srcServerResponse_ptr, USER_INFORMATION_CLASS *refUserInfo_ptr)

//  DESCRIPTION     : Validate Asynchronous Operation Window.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	string refServerResponse;
	
	// check for matching reference values
	if ((refUserInfo_ptr) &&
		(refUserInfo_ptr->getUserIdentityNegotiationServerResponse()))
	{
		refServerResponse = refUserInfo_ptr->getUserIdentityNegotiationServerResponse();
	}
	
	// validate parameters
	bool result = true;
	if (srcServerResponse_ptr)
	{
		result = serverResponseM.validate(srcServerResponse_ptr, refServerResponse);
	}
	
	// return result
	return result;
}

