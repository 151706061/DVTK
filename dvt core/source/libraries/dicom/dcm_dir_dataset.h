//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DCM_DIR_DATASET_H
#define DCM_DIR_DATASET_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Idicom.h"     // DICOM library interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_ITEM_CLASS;

//>>***************************************************************************

class DCM_DIR_DATASET_CLASS : public DCM_DATASET_CLASS

//  DESCRIPTION     : DICOM dataset class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
    DCM_VALUE_SQ_CLASS *sqValueM_ptr;

public:
    DCM_DIR_DATASET_CLASS();
	~DCM_DIR_DATASET_CLASS();

    bool decodeToFirstRecord(DATA_TF_CLASS&);

    DCM_ITEM_CLASS *getNextDirRecord(DATA_TF_CLASS&);
};

#endif /* DCM_DIR_DATASET_H */

