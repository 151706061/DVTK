//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_ABORT_REQUEST_VALIDATOR_H
#define ACSE_ABORT_REQUEST_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_ab_reason.h"
#include "ACSE_ab_source.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class ABORT_RQ_CLASS;
class LOG_CLASS;

//>>***************************************************************************
class ABORT_RQ_VALIDATOR_CLASS
//  DESCRIPTION     : Abort Request validation class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ABORT_RQ_VALIDATOR_CLASS();
	~ABORT_RQ_VALIDATOR_CLASS();

	ACSE_PARAMETER_CLASS *getSourceParameter();
	ACSE_PARAMETER_CLASS *getReasonParameter();
	
	bool    validate(ABORT_RQ_CLASS*, ABORT_RQ_CLASS*);
	
private:
	ACSE_AB_SOURCE_CLASS    sourceM;
	ACSE_AB_REASON_CLASS    reasonM;
};

#endif /* ACSE_ABORT_REQUEST_VALIDATOR_H */