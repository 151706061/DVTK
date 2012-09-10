//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_MACRO_H
#define DEF_MACRO_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "AttributeGroupDefinition.h"


//>>***************************************************************************

class DEF_MACRO_CLASS : public DEF_ATTRIBUTE_GROUP_CLASS

//  DESCRIPTION     : Macro definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_MACRO_CLASS();
	DEF_MACRO_CLASS(const string);
	DEF_MACRO_CLASS(const string, const MOD_USAGE_ENUM);

	~DEF_MACRO_CLASS();
};		

#endif /* DEF_MACRO_H */
