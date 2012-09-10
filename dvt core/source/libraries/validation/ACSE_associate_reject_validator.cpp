//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_associate_reject_validator.h"
#include "Iglobal.h"      // Global component interface file
#include "Ilog.h"         // Logging component interface file
#include "Inetwork.h"     // Network component interface file

//*****************************************************************************
//  STATIC DECLARATIONS
//*****************************************************************************
static T_BYTE_TEXT_MAP	TARJResult[] = {
{REJECTED_PERMANENT,	"rejected-permanent"},
{REJECTED_TRANSIENT,	"rejected-transient"},
{BYTE_SENTINAL,			"unknown"}
};

static T_BYTE_TEXT_MAP TARJSource[] = {
{DICOM_UL_SERVICE_USER,						"DICOM UL service-user"},
{DICOM_UL_SERVICE_PROVIDER_ACSE,			"DICOM UL service-provider (ACSE related function)"},	
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	"DICOM UL service-provider (Presentation related function)"},
{BYTE_SENTINAL,								"unknown"}
};

static T_BYTE_BYTE_TEXT_MAP TARJReason[] = {
{DICOM_UL_SERVICE_USER,						NO_REASON_GIVEN,						"no-reason-given"},
{DICOM_UL_SERVICE_USER,						APPLICATION_CONTEXT_NAME_NOT_SUPPORTED,	"application-context-name-not-supported"},
{DICOM_UL_SERVICE_USER,						CALLING_AE_TITLE_NOT_RECOGNISED,		"calling-AE-title-not-recognized"},
{DICOM_UL_SERVICE_USER,						4,										"reserved"},
{DICOM_UL_SERVICE_USER,						5,										"reserved"},
{DICOM_UL_SERVICE_USER,						6,										"reserved"},
{DICOM_UL_SERVICE_USER,						CALLED_AE_TITLE_NOT_RECOGNISED,			"called-AE-title-not-recognized"},
{DICOM_UL_SERVICE_USER,						8,										"reserved"},
{DICOM_UL_SERVICE_USER,						9,										"reserved"},
{DICOM_UL_SERVICE_USER,						10,										"reserved"},
	
{DICOM_UL_SERVICE_PROVIDER_ACSE,			NO_REASON_GIVEN,						"no-reason-given"},
{DICOM_UL_SERVICE_PROVIDER_ACSE,			PROTOCOL_VERSION_NOT_SUPPORTED,			"protocol-version-not-supported"},

{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	0,										"reserved"},
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	TEMPORARY_CONGESTION,					"temporary-congestion"},
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	LOCAL_LIMIT_EXCEEDED,					"local-limit-exceeded"},
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	3,										"reserved"},
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	4,										"reserved"},
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	5,										"reserved"},
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	6,										"reserved"},
{DICOM_UL_SERVICE_PROVIDER_PRESENTATION,	7,										"reserved"},	

{BYTE_SENTINAL,								BYTE_SENTINAL,							"unknown"}
};


//>>===========================================================================

char *ARJResult(BYTE result)

//  DESCRIPTION     : Associate Reject - result LUT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	int		index = 0;

	while ((TARJResult[index].code != result)
	  && (TARJResult[index].code != BYTE_SENTINAL))
		index++;

	return TARJResult[index].text;
}

//>>===========================================================================

char *ARJSource(BYTE source)

//  DESCRIPTION     : Associate Reject - source LUT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	int		index = 0;

	while ((TARJSource[index].code != source)
	  && (TARJSource[index].code != BYTE_SENTINAL))
		index++;

	return TARJSource[index].text;
}


//>>===========================================================================

char *ARJReason(BYTE source, BYTE reason)

//  DESCRIPTION     : Associate Reject - reason LUT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	int		index = 0;

	while (TARJReason[index].code1 != BYTE_SENTINAL) 
	{
		if ((TARJReason[index].code1 == source)
	     && (TARJReason[index].code2 == reason))
			break;

		index++;
	}

	return TARJReason[index].text;
}

//>>===========================================================================		

ASSOCIATE_RJ_VALIDATOR_CLASS::ASSOCIATE_RJ_VALIDATOR_CLASS()

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

ASSOCIATE_RJ_VALIDATOR_CLASS::~ASSOCIATE_RJ_VALIDATOR_CLASS()

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

ACSE_PARAMETER_CLASS *ASSOCIATE_RJ_VALIDATOR_CLASS::getResultParameter() 

//  DESCRIPTION     : Get Result
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &resultM; 
}

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ASSOCIATE_RJ_VALIDATOR_CLASS::getSourceParameter()

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

ACSE_PARAMETER_CLASS *ASSOCIATE_RJ_VALIDATOR_CLASS::getReasonParameter()

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

bool ASSOCIATE_RJ_VALIDATOR_CLASS::validate(ASSOCIATE_RJ_CLASS *srcAssocRj_ptr, ASSOCIATE_RJ_CLASS *refAssocRj_ptr)

//  DESCRIPTION     : Validate Associate Reject.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	char buffer[MAXIMUM_LINE_LENGTH];
	string refResult;
	string refSource;
	string refReason;
	
	// check for valid source
	if (srcAssocRj_ptr == NULL) return false;
	
	// check if reference provided
	if (refAssocRj_ptr)
	{
		// set up reference values
		if (refAssocRj_ptr->getResult() != UNDEFINED_REJECT_RESULT)
		{
			sprintf(buffer, "%d", refAssocRj_ptr->getResult());
			refResult = buffer;
		}
		
		if (refAssocRj_ptr->getSource() != UNDEFINED_REJECT_SOURCE)
		{
			sprintf(buffer, "%d", refAssocRj_ptr->getSource());
			refSource = buffer;
		}
		
		if (refAssocRj_ptr->getReason() != UNDEFINED_REJECT_REASON)
		{
			sprintf(buffer, "%d", refAssocRj_ptr->getReason());
			refReason = buffer;
		}
	}
	
	// validate the parameters
	sprintf(buffer, "%d", srcAssocRj_ptr->getResult());
	bool result1 = resultM.validate(buffer, refResult);
    resultM.setMeaning(ARJResult(srcAssocRj_ptr->getResult()));
	
	sprintf(buffer, "%d", srcAssocRj_ptr->getSource());
	bool result2 = sourceM.validate(buffer, refSource);
    sourceM.setMeaning(ARJSource(srcAssocRj_ptr->getSource()));

	sprintf(buffer, "%d", srcAssocRj_ptr->getReason());
	bool result3 = reasonM.validate(buffer, refReason);
    reasonM.setMeaning(ARJReason(srcAssocRj_ptr->getSource(), srcAssocRj_ptr->getReason()));

	// set result
	bool result = true;
	if ((!result1) ||
		(!result2) ||
		(!result3))
	{
		result = false;
	}
	
	// resturn result
	return result;
} 
