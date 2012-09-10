//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "stored_sop_list.h"
#include "stored_sop_instance.h"
#include "Ilog.h"				// Log component interface
#include "Idicom.h"				// Dicom component interface


//>>***************************************************************************

STORED_SOP_LIST_CLASS::STORED_SOP_LIST_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
}

//>>***************************************************************************

STORED_SOP_LIST_CLASS::~STORED_SOP_LIST_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
	cleanup();
}

//>>***************************************************************************

void STORED_SOP_LIST_CLASS::cleanup()

//  DESCRIPTION     : Cleanup the stored sop list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
	while (sopInstanceM.getSize())
	{
		delete sopInstanceM[0];
		sopInstanceM.removeAt(0);
	}
}

//>>***************************************************************************

STORED_SOP_INSTANCE_CLASS *STORED_SOP_LIST_CLASS::search(string sopClassUid,
														 string sopInstanceUid,
														 LOG_CLASS *logger_ptr)

//  DESCRIPTION     : Search the stored list for a match - check class and instance
//					: combinations.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	STORED_SOP_INSTANCE_CLASS *storedSopInstance_ptr = NULL;

	// seach list of stored sop instances for a match
	for (UINT i = 0; i < sopInstanceM.getSize(); i++)
	{
		if ((sopInstanceM[i]->getSopClassUid() == sopClassUid) &&
			(sopInstanceM[i]->getSopInstanceUid() == sopInstanceUid))
		{
			// matching entry found - return it
			storedSopInstance_ptr = sopInstanceM[i];
			break;
		}
		else
		{
			if (sopInstanceM[i]->getSopInstanceUid() == sopInstanceUid)
			{
				// instance uid has already been used in a different sop class
				if (logger_ptr)
				{
					logger_ptr->text(LOG_ERROR, 1,
									 "(0008,0018) SOP Instance UID %s is used in two different SOP Classes - %s and %s",
									 sopInstanceUid.c_str(),
									 sopInstanceM[i]->getSopClassUid().c_str(),
									 sopClassUid.c_str());
				}
			}
		}
	}

	// return sop instance
	return storedSopInstance_ptr;
}

//>>***************************************************************************

void STORED_SOP_LIST_CLASS::analyseStorageDataset(DCM_DATASET_CLASS *dataset_ptr,
												  LOG_CLASS		*logger_ptr)

//  DESCRIPTION     : Analyse the Storage Dataset for SOP Class UID and SOP
//                    Instance UID.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	STORED_SOP_INSTANCE_CLASS *storedSopInstance_ptr;
	string sopClassUid;
	string sopInstanceUid;

	// check that the appriopriate attributes are available
	if (!dataset_ptr->getUIValue(TAG_SOP_CLASS_UID, sopClassUid))
	{
		if (logger_ptr)
		{
			logger_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,0016) SOP (Image) Class UID not available for Object Relationship Analysis");
		}
		return;
	}

	if (!dataset_ptr->getUIValue(TAG_SOP_INSTANCE_UID, sopInstanceUid))
	{
		if (logger_ptr)
		{
			logger_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,0018) SOP (Image) Instance UID not available for Object Relationship Analysis");
		}
		return;
	}
	
	// check if the sop instance has already been seen
	storedSopInstance_ptr = search(sopClassUid, sopInstanceUid, logger_ptr);
	if (storedSopInstance_ptr == NULL)
	{
		// store new SOP instance uid
		storedSopInstance_ptr = new STORED_SOP_INSTANCE_CLASS(sopClassUid, sopInstanceUid);
		sopInstanceM.add(storedSopInstance_ptr);
	}
	else
	{
		if (logger_ptr)
		{
			logger_ptr->text(LOG_WARNING, 1,
							 "Duplicate SOP Class/Instance UID found for SOP Class UID %s and SOP Instance UID %s",
							 storedSopInstance_ptr->getSopClassUid().c_str(),
							 storedSopInstance_ptr->getSopInstanceUid().c_str());
		}

		// sop instance appears more than once
		storedSopInstance_ptr->incrementCount();
	}
}

//>>***************************************************************************

void STORED_SOP_LIST_CLASS::log(LOG_CLASS *logger_ptr)

//  DESCRIPTION     : Log the stored sop list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	if (logger_ptr == NULL) return;

	if (sopInstanceM.getSize() > 0) 
	{
		logger_ptr->text(LOG_NONE, 1, "Stored SOP Instance List");

		// log stored instances
		for (UINT i = 0; i < sopInstanceM.getSize(); i++) 
		{
			logger_ptr->text(LOG_NONE, 1, "\tSTORED SOP INSTANCE %d of %d", i + 1, sopInstanceM.getSize());
			sopInstanceM[i]->log(logger_ptr);
		}
	}
}

