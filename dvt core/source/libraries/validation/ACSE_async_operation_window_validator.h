//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_ASYNC_OPERATION_WINDOW_VALIDATOR_H
#define ACSE_ASYNC_OPERATION_WINDOW_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_operation.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class USER_INFORMATION_CLASS;
class LOG_CLASS;

//>>***************************************************************************

class ACSE_ASYNCHRONOUS_OPERATION_WINDOW_VALIDATOR_CLASS

//  DESCRIPTION     : Asynchronous Operation Window class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_ASYNCHRONOUS_OPERATION_WINDOW_VALIDATOR_CLASS();
	~ACSE_ASYNCHRONOUS_OPERATION_WINDOW_VALIDATOR_CLASS();
	
	ACSE_PARAMETER_CLASS *getOperationsInvokedParameter();
	ACSE_PARAMETER_CLASS *getOperationsPerformedParameter();

	bool validate(UINT16, UINT16, USER_INFORMATION_CLASS*);

private:
	ACSE_OPERATION_CLASS	operationsInvokedM;
	ACSE_OPERATION_CLASS	operationsPerformedM;
};

#endif /* ACSE_ASYNC_OPERATION_WINDOW_VALIDATOR_H */