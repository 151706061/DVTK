//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_ASSOCIATE_REJECT_VALIDATOR_H
#define ACSE_ASSOCIATE_REJECT_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_rj_reason.h"
#include "ACSE_rj_result.h"
#include "ACSE_rj_source.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class ASSOCIATE_RJ_CLASS;
class LOG_CLASS;

//>>***************************************************************************
class ASSOCIATE_RJ_VALIDATOR_CLASS
//  DESCRIPTION     : Associate Reject validation class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ASSOCIATE_RJ_VALIDATOR_CLASS();
	~ASSOCIATE_RJ_VALIDATOR_CLASS();
	
	ACSE_PARAMETER_CLASS *getResultParameter();
	ACSE_PARAMETER_CLASS *getSourceParameter();
	ACSE_PARAMETER_CLASS *getReasonParameter();

	bool validate(ASSOCIATE_RJ_CLASS*, ASSOCIATE_RJ_CLASS*);

private:
	ACSE_RJ_RESULT_CLASS	resultM;
	ACSE_RJ_SOURCE_CLASS	sourceM;
	ACSE_RJ_REASON_CLASS	reasonM;
};

#endif /* ACSE_ASSOCIATE_REJECT_VALIDATOR_H */