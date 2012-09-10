//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_USER_INFORMATION_VALIDATOR_H
#define ACSE_USER_INFORMATION_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_maximum_length_received.h"
#include "ACSE_uid.h"
#include "ACSE_scp_scu_role_select_validator.h"
#include "ACSE_async_operation_window_validator.h"
#include "ACSE_sop_class_extended_validator.h"
#include "ACSE_user_identity_negotiation_validator.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class USER_INFORMATION_CLASS;
class ACSE_PROPERTIES_CLASS;
class ACSE_IMPLEMENTATION_VERSION_NAME_CLASS;
class LOG_CLASS;

//>>***************************************************************************
class ACSE_USER_INFORMATION_VALIDATOR_CLASS
//  DESCRIPTION     : User Information class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_USER_INFORMATION_VALIDATOR_CLASS();
	~ACSE_USER_INFORMATION_VALIDATOR_CLASS();
	
	ACSE_PARAMETER_CLASS *getMaximumLengthReceivedParameter();
	ACSE_PARAMETER_CLASS *getImplementationClassUidParameter();
	ACSE_PARAMETER_CLASS *getImplementationVersionNameParameter();

	UINT noScpScuRoleSelects();
	ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_CLASS& getScpScuRoleSelect(UINT);

	UINT noSopClassExtendeds();
	ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS& getSopClassExtended(UINT);

	ACSE_ASYNCHRONOUS_OPERATION_WINDOW_VALIDATOR_CLASS *getAsynchronousOperationWindow();

	ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS *getUserIdentityNegotiation();

	bool validate(USER_INFORMATION_CLASS * srcUserInfo_ptr,
		USER_INFORMATION_CLASS * refUserInfo_ptr,
		ACSE_PROPERTIES_CLASS * acseProp_ptr);
	
private:
	ACSE_MAXIMUM_LENGTH_RECEIVED_CLASS					maximumLengthReceivedM;
	ACSE_UID_CLASS										implementationClassUidM;
	ACSE_IMPLEMENTATION_VERSION_NAME_CLASS				*implementationVersionNameM_ptr;
	ARRAY<ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_CLASS>		scpScuRoleSelectM;
	ACSE_ASYNCHRONOUS_OPERATION_WINDOW_VALIDATOR_CLASS  *asynchronousOperationWindowM_ptr;
	ARRAY<ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS>		sopClassExtendedM;
	ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS		*userIdentityNegotiationM_ptr;	
};

#endif /* ACSE_USER_INFORMATION_VALIDATOR_H */