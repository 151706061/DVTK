//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_MODULE_H
#define DEF_MODULE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "AttributeGroupDefinition.h"


//>>***************************************************************************

class DEF_MODULE_CLASS : public DEF_ATTRIBUTE_GROUP_CLASS

//  DESCRIPTION     : Module definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_MODULE_CLASS();
	DEF_MODULE_CLASS(const string);
	DEF_MODULE_CLASS(const string, const MOD_USAGE_ENUM);

	~DEF_MODULE_CLASS();
};

#endif /* DEF_MODULE_H */
