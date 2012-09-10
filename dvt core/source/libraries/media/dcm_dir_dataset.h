//*****************************************************************************
//  FILENAME        :	DCM_DIR_DATASET.H
//  PACKAGE         :	DVT
//  COMPONENT       :	DICOM
//  DESCRIPTION     :	DICOM Dataset Class.
//  COPYRIGHT(c)    :   2004, Philips Electronics N.V.
//                      2004, Agfa Gevaert N.V.
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


//>>***************************************************************************

class DCM_DIR_DATASET_CLASS : public DCM_DIR_DATASET_CLASS

//  DESCRIPTION     : DICOM dataset class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	DIMSE_CMD_ENUM	commandIdM;
	string			iodNameM;
	PRIVATE_ATTRIBUTE_HANDLER_CLASS	pahM;

	void populateWithAttributes();

public:
	DCM_DIR_DATASET_CLASS();
	~DCM_DIR_DATASET_CLASS();

	bool decode(DATA_TF_CLASS&, UINT16 lastGroup = 0xFFFF);
};

#endif /* DCM_DIR_DATASET_H */

