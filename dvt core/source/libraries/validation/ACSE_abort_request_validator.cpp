//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_abort_request_validator.h"
#include "Iglobal.h"		// Global component interface
#include "Ilog.h"
#include "Inetwork.h"

//*****************************************************************************
//  STATIC DECLARATIONS
//*****************************************************************************
static T_BYTE_TEXT_MAP TABRQSource[] = {
{DICOM_UL_SERVICE_USER_INITIATED,		"DICOM UL service-user (initiated abort)"},
{1,										"reserved"},
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	"DICOM UL service-provider (initiated abort)"},
{BYTE_SENTINAL,							"unknown"}
};

static T_BYTE_BYTE_TEXT_MAP TABRQReason[] = {
{DICOM_UL_SERVICE_USER_INITIATED,		0,							"not significant"},
	
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	REASON_NOT_SPECIFIED,		"reason-not-specified"},
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	UNRECOGNIZED_PDU,			"unrecognized-pdu"},
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	UNEXPECTED_PDU,				"unexpected-pdu"},
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	3,							"reserved"},
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	UNRECOGNIZED_PDU_PARAMETER,	"unrecognized-pdu-parameter"},
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	UNEXPECTED_PDU_PARAMETER,	"unexpected-pdu-parameter"},
{DICOM_UL_SERVICE_PROVIDER_INITIATED,	INVALID_PDU_PARAMETER_VALUE,"invalid-pdu-parameter-value"},

{BYTE_SENTINAL,							BYTE_SENTINAL,				"unknown"}
};


//>>===========================================================================

char *ABRQSource(BYTE source)

//  DESCRIPTION     : Associate Abort - source LUT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	int		index = 0;

	while ((TABRQSource[index].code != source)
	  && (TABRQSource[index].code != BYTE_SENTINAL))
		index++;

	return TABRQSource[index].text;
}


//>>===========================================================================

char *ABRQReason(BYTE source, BYTE reason)

//  DESCRIPTION     : Associate Abort - reason LUT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	int		index = 0;

	while (TABRQReason[index].code1 != BYTE_SENTINAL) 
	{
		if ((TABRQReason[index].code1 == source)
		 && (TABRQReason[index].code2 == reason))
			break;

		index++;
	}

	return TABRQReason[index].text;
}


//>>===========================================================================		

ABORT_RQ_VALIDATOR_CLASS::ABORT_RQ_VALIDATOR_CLASS()

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
} 

//>>===========================================================================		

ABORT_RQ_VALIDATOR_CLASS::~ABORT_RQ_VALIDATOR_CLASS()

//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// destructor activities
} 

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ABORT_RQ_VALIDATOR_CLASS::getSourceParameter()

//  DESCRIPTION     : Get Source
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &sourceM; 
}

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ABORT_RQ_VALIDATOR_CLASS::getReasonParameter()

//  DESCRIPTION     : Get Reason
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &reasonM; 
}

//>>===========================================================================		

bool ABORT_RQ_VALIDATOR_CLASS::validate(ABORT_RQ_CLASS *srcAbortRq_ptr, ABORT_RQ_CLASS *refAbortRq_ptr)

//  DESCRIPTION     : Validate Abort Request.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	char buffer[MAXIMUM_LINE_LENGTH];
	string refSource;
	string refReason;
	
	// check for valid source
	if (srcAbortRq_ptr == NULL) return false;
	
	// check if reference provided
	if (refAbortRq_ptr)
	{
		// set up reference values
		if (refAbortRq_ptr->getSource() != UNDEFINED_ABORT_SOURCE)
		{
			sprintf(buffer, "%d", refAbortRq_ptr->getSource());
			refSource = buffer;
		}
		
		if (refAbortRq_ptr->getReason() != UNDEFINED_ABORT_REASON)
		{
			sprintf(buffer, "%d", refAbortRq_ptr->getReason());
			refReason = buffer;
		}
	}
	
	// validate the parameters
	sprintf(buffer, "%d", srcAbortRq_ptr->getSource());
	bool result1 = sourceM.validate(buffer, refSource);
    sourceM.setMeaning(ABRQSource(srcAbortRq_ptr->getSource()));
	
	sprintf(buffer, "%d", srcAbortRq_ptr->getReason());
	bool result2 = reasonM.validate(buffer, refReason);
    reasonM.setMeaning(ABRQReason(srcAbortRq_ptr->getSource(), srcAbortRq_ptr->getReason()));
	
	// set result
	bool result = true;
	if ((!result1) ||
		(!result2))
	{
		result = false;
	}
	
	// resturn result
	return result;
}
