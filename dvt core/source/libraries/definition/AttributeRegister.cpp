//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "AttributeDefinition.h"
#include "AttributeRegister.h"


//>>===========================================================================

DEF_ATTRIBUTE_REGISTER_CLASS::DEF_ATTRIBUTE_REGISTER_CLASS(DEF_ATTRIBUTE_CLASS *referenceAttribute_ptr)

//  DESCRIPTION     : Constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	referenceAttributeM = *referenceAttribute_ptr;
	referenceCountM = 1;
}

//>>===========================================================================

DEF_ATTRIBUTE_REGISTER_CLASS::~DEF_ATTRIBUTE_REGISTER_CLASS()

//  DESCRIPTION     : Destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// destructor activities
}

//>>===========================================================================
		
DEF_ATTRIBUTE_CLASS *DEF_ATTRIBUTE_REGISTER_CLASS::GetReferenceAttribute()

//  DESCRIPTION     : Get the referenced attribute
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// return referenced attribute
	return &referenceAttributeM;
}

//>>===========================================================================

void DEF_ATTRIBUTE_REGISTER_CLASS::IncrementReferenceCount()

//  DESCRIPTION     : Increment the reference attribute count
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// increment the reference count
	referenceCountM++;
}

//>>===========================================================================
		
void DEF_ATTRIBUTE_REGISTER_CLASS::DecrementReferenceCount()

//  DESCRIPTION     : Decrement the reference attribute count
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// decrement the reference count - don't let it go below zero
	if (referenceCountM)
	{
		referenceCountM--;
	}
}

//>>===========================================================================
		
int DEF_ATTRIBUTE_REGISTER_CLASS::GetReferenceCount()

//  DESCRIPTION     : Get the reference attribute count
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// return the reference count
	return referenceCountM;
}
