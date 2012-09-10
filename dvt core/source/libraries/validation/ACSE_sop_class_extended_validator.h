//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_SOP_CLASS_EXTENDED_VALIDATOR_H
#define ACSE_SOP_CLASS_EXTENDED_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_uid.h"
#include "ACSE_byte.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class SOP_CLASS_EXTENDED_CLASS;
class USER_INFORMATION_CLASS;
class LOG_CASS;

//>>***************************************************************************
class ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS
//  DESCRIPTION     : SOP Class Extended class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS();
	ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS(ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS&);
	~ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS();
	
	ACSE_PARAMETER_CLASS *getUidParameter();

	UINT noAapplicationInformations();
	ACSE_PARAMETER_CLASS *getAapplicationInformationParameter(UINT);

	bool operator = (ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS&);
	
	bool validate(SOP_CLASS_EXTENDED_CLASS*, USER_INFORMATION_CLASS*);

private:
	ACSE_UID_CLASS			uidM;
	ARRAY<ACSE_BYTE_CLASS>	applicationInformationM;
};

#endif /* ACSE_SOP_CLASS_EXTENDED_VALIDATOR_H */