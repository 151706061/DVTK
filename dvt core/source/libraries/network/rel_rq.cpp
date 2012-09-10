// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "rel_rq.h"
#include "pdu.h"			// PDU


//>>===========================================================================

RELEASE_RQ_CLASS::RELEASE_RQ_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	widTypeM = WID_RELEASE_RQ;
	itemTypeM = PDU_RELEASE_RQ;
	reservedM = 0;
	lengthM = sizeof(reserved1M);
	reserved1M = 0;
}

//>>===========================================================================

RELEASE_RQ_CLASS::~RELEASE_RQ_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// nothing explicit to do
}		

//>>===========================================================================

bool RELEASE_RQ_CLASS::encode(PDU_CLASS& pdu)

//  DESCRIPTION     : Encode release request as PDU.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// encode the pdu type and length
	pdu.setType(itemTypeM);
	pdu.setReserved(reservedM);
	if (!pdu.allocateBody(lengthM)) return false;

	pdu << reserved1M;			

	return true;
}

//>>===========================================================================

bool RELEASE_RQ_CLASS::decode(PDU_CLASS& pdu)

//  DESCRIPTION     : Decode release request from PDU.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// decode the Release Request PDU
	itemTypeM = pdu.getType();
	reservedM = pdu.getReserved();			
	lengthM = pdu.getLength();				

	pdu >> reserved1M;

	return true;
}

//>>===========================================================================

UINT32 RELEASE_RQ_CLASS::getLength()

//  DESCRIPTION     : Get PDU length.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// compute PDU length
	return sizeof(itemTypeM) + sizeof(reservedM) + sizeof(lengthM) + lengthM;
}

//>>===========================================================================

bool RELEASE_RQ_CLASS::updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*)

//  DESCRIPTION     : Update this object with the contents of the object given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = true;

	// return result
	return result;
}
