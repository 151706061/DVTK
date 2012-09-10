//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "stored_sop_instance.h"
#include "Ilog.h"					// Log component interface

//>>===========================================================================

STORED_SOP_INSTANCE_CLASS::STORED_SOP_INSTANCE_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	countM = 1;
	committedM = false;
}

//>>===========================================================================

STORED_SOP_INSTANCE_CLASS::STORED_SOP_INSTANCE_CLASS(string sopClassUid, string sopInstanceUid)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	sopClassUidM = sopClassUid;
	sopInstanceUidM = sopInstanceUid;
	countM = 1;
	committedM = false;
}

//>>===========================================================================

STORED_SOP_INSTANCE_CLASS::~STORED_SOP_INSTANCE_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
}

//>>===========================================================================

void STORED_SOP_INSTANCE_CLASS::log(LOG_CLASS *logger_ptr)

//  DESCRIPTION     : Log the SOP Instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// check for valid logger
	if (logger_ptr == NULL) return;

	// display sop data
	if (committedM)
	{
		logger_ptr->text(LOG_NONE, 1, "\t\tSOP Instance has been COMMITTED for storage");
	}
	logger_ptr->text(LOG_NONE, 1, "\t\t(0008,0016) Object(Image) SOP Class UID: %s", sopClassUidM.c_str());
	logger_ptr->text(LOG_NONE, 1, "\t\t(0008,0018) Object(Image) SOP Instance UID: %s", sopInstanceUidM.c_str());
	
	if (countM > 1) 
	{
		logger_ptr->text(LOG_IMAGE_RELATION, 1, "%d objects with identical identification", countM);
	}
}
