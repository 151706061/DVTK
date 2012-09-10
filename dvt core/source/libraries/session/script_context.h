// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef SCRIPT_CONTEXT_H
#define SCRIPT_CONTEXT_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"	// Global component interface
#include "Ilog.h"       // Logging component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class SCRIPT_SESSION_CLASS;


//>>***************************************************************************

class SCRIPT_EXECUTION_CONTEXT_CLASS

//  DESCRIPTION     : Script Execution Context Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	// Script execution context flags
	VALIDATION_CONTROL_FLAG_ENUM validationFlagM;
	bool strictValidationM;
	bool defineSqLengthM;
	bool addGroupLengthM;
	bool autoType2AttributesM;
	bool populateWithAttributesM;
    string applicationEntityNameM;
    string applicationEntityVersionM;

public:
	SCRIPT_EXECUTION_CONTEXT_CLASS(SCRIPT_SESSION_CLASS*);

	~SCRIPT_EXECUTION_CONTEXT_CLASS();
		
	void setValidationFlag(VALIDATION_CONTROL_FLAG_ENUM);

	void setStrictValidation(bool);

	void setDefineSqLength(bool);

	void setAddGroupLength(bool);
	
	void setAutoType2Attributes(bool);

	void setPopulateWithAttributes(bool);

    void setApplicationEntityName(string);

    void setApplicationEntityVersion(string);

	VALIDATION_CONTROL_FLAG_ENUM getValidationFlag();

	bool getStrictValidation();

	bool getDefineSqLength();

	bool getAddGroupLength();

	bool getPopulateWithAttributes();

    string getApplicationEntityName();

    string getApplicationEntityVersion();
};

#endif /* SCRIPT_CONTEXT_H */


