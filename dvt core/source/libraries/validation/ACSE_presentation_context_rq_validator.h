//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-206
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_H
#define ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"
#include "ACSE_pc_id.h"
#include "ACSE_uid.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class ASSOCIATE_RQ_CLASS;
class LOG_CLASS;
class PRESENTATION_CONTEXT_RQ_CLASS;

//>>***************************************************************************
class ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_CLASS
//  DESCRIPTION     : Presentation Context Request class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_CLASS();
	ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_CLASS(ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_CLASS&);
	~ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_CLASS();
	
	string getPresentationContextId();
	
	string getAbstractSyntaxName();
	
	UINT noTransferSyntaxNames();
	
	string getTransferSyntaxName(UINT);

	ACSE_PARAMETER_CLASS *getPresentationContextIdParameter();
	ACSE_PARAMETER_CLASS *getAbstractSyntaxNameParameter();
	ACSE_PARAMETER_CLASS *getTransferSyntaxNameParameter(UINT);

	bool operator = (ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_CLASS&);
	
	bool validate(PRESENTATION_CONTEXT_RQ_CLASS*, ASSOCIATE_RQ_CLASS*);
	
    void tooManyPresentationContexts();

	void notReferenced();
	
	void notUniquePcId();
	
	void notUniqueSOP(string, string);
	
	void notSourced(PRESENTATION_CONTEXT_RQ_CLASS*);
	
	bool equalTransferSyntaxes(ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_CLASS&);
	
private:
	ACSE_PC_ID_CLASS		presentationContextIdM;
	ACSE_UID_CLASS			abstractSyntaxNameM;
	ARRAY<ACSE_UID_CLASS>	transferSyntaxNameM;
};

#endif /* ACSE_PRESENTATION_CONTEXT_RQ_VALIDATOR_H */
