//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DCM_VALUE_UL_H
#define DCM_VALUE_UL_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"			// Global component interface
#include "Ilog.h"				// Log component interface
#include "Iutility.h"			// Utility component interface
#include "Iwarehouse.h"			// Warehouse component interface
#include "IAttributeGroup.h"	// Attribute component interface


//>>***************************************************************************
class DCM_VALUE_UL_CLASS : public VALUE_UL_CLASS

//  DESCRIPTION     : DCM UL value class.
//  INVARIANT       :
//  NOTES           : Identifier is used to compute Offsets in Media.
//<<***************************************************************************
{
private:
	string	identifierM;

public:
	DCM_VALUE_UL_CLASS();

	~DCM_VALUE_UL_CLASS();

	void setIdentifier(string identifier) { identifierM = identifier; }

	string getIdentifier() { return identifierM; }

    bool operator = (DCM_VALUE_UL_CLASS&);
};

#endif /* DCM_VALUE_UL_H */
