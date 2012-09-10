//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_H
#define ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_uid.h"
#include "ACSE_role.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class SCP_SCU_ROLE_SELECT_CLASS;
class USER_INFORMATION_CLASS;
class LOG_CLASS;

//>>***************************************************************************
class ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_CLASS
//  DESCRIPTION     : SCP/SCU Role Select class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
	
public:
	ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_CLASS();
	~ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_CLASS();
	
	ACSE_PARAMETER_CLASS *getUidParameter();
	ACSE_PARAMETER_CLASS *getScpRoleParameter();
	ACSE_PARAMETER_CLASS *getScuRoleParameter();

	bool validate(SCP_SCU_ROLE_SELECT_CLASS*, USER_INFORMATION_CLASS*);

private:
	ACSE_UID_CLASS	uidM;
	ACSE_ROLE_CLASS	scpRoleM;
	ACSE_ROLE_CLASS	scuRoleM;
};

#endif /* ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_H */