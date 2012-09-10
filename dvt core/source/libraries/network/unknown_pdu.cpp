// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "unknown_pdu.h"
#include "pdu.h"			// PDU


//>>===========================================================================

UNKNOWN_PDU_CLASS::UNKNOWN_PDU_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	widTypeM = WID_UNKNOWN_PDU;
	itemTypeM = PDU_UNKNOWN;
	reservedM = 0;
	lengthM = 0;
	bodyM_ptr = NULL;
}

//>>===========================================================================

UNKNOWN_PDU_CLASS::~UNKNOWN_PDU_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// free up resources
	if (bodyM_ptr) {
		delete [] bodyM_ptr;
	}
}		

//>>===========================================================================

bool UNKNOWN_PDU_CLASS::encode(PDU_CLASS& pdu)

//  DESCRIPTION     : Encode unknown PDU.
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

	// write PDU body
	if (bodyM_ptr)
	{
		// write it as binary data
		pdu.writeBinary(bodyM_ptr, lengthM);
	}

	return true;
}

//>>===========================================================================

bool UNKNOWN_PDU_CLASS::decode(PDU_CLASS& pdu)

//  DESCRIPTION     : Decode unknown PDU.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// decode the Unknown PDU
	itemTypeM = pdu.getType();
	reservedM = pdu.getReserved();			
	lengthM = pdu.getLength();
				
	// allocate storage for the PDU body
	bodyM_ptr = new BYTE [lengthM];

	// read PDU body
	if (bodyM_ptr)
	{
		// just read it as binary data
		pdu.readBinary(bodyM_ptr, lengthM);
	}

	return true;
}

//>>===========================================================================

UINT32 UNKNOWN_PDU_CLASS::getLength()

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

bool UNKNOWN_PDU_CLASS::updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*)

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
