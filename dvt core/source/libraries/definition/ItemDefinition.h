//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_ITEM_H
#define DEF_ITEM_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "AttributeGroupDefinition.h"


//>>***************************************************************************

class DEF_ITEM_CLASS : public DEF_ATTRIBUTE_GROUP_CLASS

//  DESCRIPTION     : Item definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_ITEM_CLASS();
	DEF_ITEM_CLASS(const string);

	~DEF_ITEM_CLASS();
};

#endif /* DEF_ITEM_H */

