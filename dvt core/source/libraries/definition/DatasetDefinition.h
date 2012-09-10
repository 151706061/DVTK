//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DATASET_DEFINITION_H
#define DATASET_DEFINITION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "DICOMObjectDefinition.h"


//>>***************************************************************************

class DEF_DATASET_CLASS : public DEF_DICOM_OBJECT_CLASS

//  DESCRIPTION     : Dataset definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_DATASET_CLASS();
	DEF_DATASET_CLASS(const string);

	~DEF_DATASET_CLASS();
};

#endif /* DATASET_DEFINITION_H */
