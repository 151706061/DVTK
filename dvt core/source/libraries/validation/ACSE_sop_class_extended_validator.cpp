//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ACSE_sop_class_extended_validator.h"
#include "Iglobal.h"      // Global component interface file
#include "Ilog.h"         // Logging component interface file
#include "Inetwork.h"     // Network component interface file

//>>===========================================================================		

ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS()

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

ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS(ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS& sopExtended)

//  DESCRIPTION     : Copy Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// constructor activities
	(*this) = sopExtended;
}

//>>===========================================================================		

ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::~ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS()

//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	// destructor activities
	while (applicationInformationM.getSize())
	{
		applicationInformationM.removeAt(0);
	}
} 

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::getUidParameter()

//  DESCRIPTION     : Get UID
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &uidM; 
}

//>>===========================================================================		

UINT ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::noAapplicationInformations() 

//  DESCRIPTION     : Get number of Application Informations
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return applicationInformationM.getSize(); 
}

//>>===========================================================================		

ACSE_PARAMETER_CLASS *ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::getAapplicationInformationParameter(UINT i)

//  DESCRIPTION     : Get Application Information
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{ 
	return &applicationInformationM[i]; 
}

//>>===========================================================================

bool ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::operator = (ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS& sopExtended)

//  DESCRIPTION     : Operator assignment.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = true;
	
	// copy individual fields
	uidM = sopExtended.uidM;
	for (UINT i = 0; i < sopExtended.applicationInformationM.getSize(); i++)
	{
		ACSE_BYTE_CLASS applicationInformation = sopExtended.applicationInformationM[i];
		applicationInformationM.add(applicationInformation);
	}
	
	return result;
}

//>>===========================================================================		

bool ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS::validate(SOP_CLASS_EXTENDED_CLASS *srcExtended_ptr, USER_INFORMATION_CLASS *refUserInfo_ptr)

//  DESCRIPTION     : Validate SOP Class Extended.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================		
{
	char buffer[MAXIMUM_LINE_LENGTH];
	string refUid;
	
	// check for valid role
	if (srcExtended_ptr == NULL) return false;
	
	UID_CLASS srcUid = srcExtended_ptr->getUid();
	SOP_CLASS_EXTENDED_CLASS *refExtended_ptr = NULL;
	
	// check for matching reference values
	if (refUserInfo_ptr)
	{
		for (UINT i = 0; i < refUserInfo_ptr->noSopClassExtendeds(); i++)
		{
			refExtended_ptr = &refUserInfo_ptr->getSopClassExtended(i);
			if (srcUid == refExtended_ptr->getUid())
			{
				refUid = (char*) refExtended_ptr->getUid().get();
				break;
			}
		}
	}
	
	// validate the parameters
	bool result1 = uidM.validate((char*) srcUid.get(), refUid);
	
	bool result2 = true;
	for (UINT j = 0; j < srcExtended_ptr->getNoApplicationInformation(); j++)
	{
		ACSE_BYTE_CLASS byteValidator;
		string refInfo = "";
		
		// check if there is a reference value
		if ((refUid.length()) &&
			(j < refExtended_ptr-> getNoApplicationInformation()))
		{
			sprintf(buffer, "%d", refExtended_ptr->getApplicationInformation(j));
			refInfo = buffer;
		}
		
		sprintf(buffer, "%d", srcExtended_ptr->getApplicationInformation(j));
		if (!byteValidator.validate(buffer, refInfo))
		{
			result2 = false;
		}
		
		applicationInformationM.add(byteValidator);
	}
	
	// set result
	bool result = true;
	if ((!result1) ||
		(!result2))
	{
		result = false;
	}
	
	// return result
	return result;
}
