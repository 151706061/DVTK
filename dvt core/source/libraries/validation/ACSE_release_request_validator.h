//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_RELEASE_REQUEST_VALIDATOR_H
#define ACSE_RELEASE_REQUEST_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class RELEASE_RQ_CLASS;
class LOG_CLASS;

//>>***************************************************************************
class RELEASE_RQ_VALIDATOR_CLASS
//  DESCRIPTION     : Release Request validation class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	RELEASE_RQ_VALIDATOR_CLASS();
	~RELEASE_RQ_VALIDATOR_CLASS();
	
	bool validate(RELEASE_RQ_CLASS*, RELEASE_RQ_CLASS*);
};

#endif /* ACSE_RELEASE_REQUEST_VALIDATOR_H */