//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef STORED_SOP_LIST_H
#define STORED_SOP_LIST_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_DATASET_CLASS;
class LOG_CLASS;
class STORED_SOP_INSTANCE_CLASS;


//>>***************************************************************************

class STORED_SOP_LIST_CLASS

//  DESCRIPTION     : Class used to store the sop class/instance detail.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	ARRAY<STORED_SOP_INSTANCE_CLASS*>	sopInstanceM;

public:
	STORED_SOP_LIST_CLASS();
	
	~STORED_SOP_LIST_CLASS();

	void						cleanup			(void);

	STORED_SOP_INSTANCE_CLASS *	search			(string			  sopClassUid,
												 string			  sopInstanceUid,
												 LOG_CLASS		* logger_ptr = NULL);

	void				analyseStorageDataset	(DCM_DATASET_CLASS	* dataset_ptr,
												 LOG_CLASS		* logger_ptr);

	void						log				(LOG_CLASS		* logger_ptr);
};

#endif /* STORED_SOP_LIST_H */
