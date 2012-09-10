//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_H
#define ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_operation.h"
#include "ACSE_user_identity_type.h"
#include "ACSE_positive_response_requested.h"
#include "ACSE_primary_field.h"
#include "ACSE_secondary_field.h"
#include "ACSE_server_response.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class USER_INFORMATION_CLASS;
class LOG_CLASS;

//>>***************************************************************************

class ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS

//  DESCRIPTION     : User Identity Negotiation class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS();
	~ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS();

	ACSE_PARAMETER_CLASS *getUserIdentityTypeParameter();

	ACSE_PARAMETER_CLASS *getPositiveResponseRequestedParameter();

	ACSE_PARAMETER_CLASS *getPrimaryFieldParameter();

	ACSE_PARAMETER_CLASS *getSecondaryFieldParameter();

	ACSE_PARAMETER_CLASS *getServerResponseParameter();

	bool validate(BYTE, BYTE, char*, char*, USER_INFORMATION_CLASS*);
	bool validate(char*, USER_INFORMATION_CLASS*);

private:
	ACSE_USER_IDENTITY_TYPE_CLASS			userIdentityTypeM;
	ACSE_POSITIVE_RESPONSE_REQUESTED_CLASS	positiveResponseRequestedM;
	ACSE_PRIMARY_FIELD_CLASS				primaryFieldM;
	ACSE_SECONDARY_FIELD_CLASS				secondaryFieldM;
	ACSE_SERVER_RESPONSE_CLASS				serverResponseM;
};

#endif /* ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_H */