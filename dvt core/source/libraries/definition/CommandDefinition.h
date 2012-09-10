//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef COMMAND_DEFINITION_H
#define COMMAND_DEFINITION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "DICOMObjectDefinition.h"


//>>***************************************************************************

class DEF_COMMAND_CLASS : public DEF_DICOM_OBJECT_CLASS

//  DESCRIPTION     : Command definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_COMMAND_CLASS();
	~DEF_COMMAND_CLASS();
};

#endif /* COMMAND_DEFINITION_H */
