//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_RELEASE_RESPONSE_VALIDATOR_H
#define ACSE_RELEASE_RESPONSE_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class RELEASE_RP_CLASS;
class LOG_CLASS;

//>>***************************************************************************
class RELEASE_RP_VALIDATOR_CLASS
//  DESCRIPTION     : Release Response validation class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	RELEASE_RP_VALIDATOR_CLASS();
	~RELEASE_RP_VALIDATOR_CLASS();
	
	bool validate(RELEASE_RP_CLASS*, RELEASE_RP_CLASS*);
};

#endif /* ACSE_RELEASE_RESPONSE_VALIDATOR_H */