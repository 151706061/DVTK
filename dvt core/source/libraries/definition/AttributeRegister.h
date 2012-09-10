//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ATTRIBUTE_REGISTER_H
#define ATTRIBUTE_REGISTER_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"			// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_ATTRIBUTE_CLASS;


//>>***************************************************************************

class DEF_ATTRIBUTE_REGISTER_CLASS

//  DESCRIPTION     : Attribute register class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_ATTRIBUTE_REGISTER_CLASS(DEF_ATTRIBUTE_CLASS*);

	~DEF_ATTRIBUTE_REGISTER_CLASS();

	DEF_ATTRIBUTE_CLASS *GetReferenceAttribute();

	void IncrementReferenceCount();

	void DecrementReferenceCount();

	int GetReferenceCount();

private:
	DEF_ATTRIBUTE_CLASS	referenceAttributeM;
	int	referenceCountM;
};


#endif /* ATTRIBUTE_REGISTER_H */
