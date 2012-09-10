// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "assoc_rj.h"
#include "pdu.h"			// PDU


//>>===========================================================================

ASSOCIATE_RJ_CLASS::ASSOCIATE_RJ_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	widTypeM = WID_ASSOCIATE_RJ;
	itemTypeM = PDU_ASSOCIATE_RJ;
	reservedM = 0;
	lengthM = sizeof(reserved1M) + sizeof(resultM) + sizeof(sourceM) + sizeof(reasonM);
	reserved1M = 0;
	resultM = UNDEFINED_REJECT_RESULT;
	sourceM = UNDEFINED_REJECT_SOURCE;
	reasonM = UNDEFINED_REJECT_REASON;
}

//>>===========================================================================

ASSOCIATE_RJ_CLASS::ASSOCIATE_RJ_CLASS(BYTE result, BYTE source, BYTE reason)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	widTypeM = WID_ASSOCIATE_RJ;
	itemTypeM = PDU_ASSOCIATE_RJ;
	reservedM = 0;
	lengthM = sizeof(reserved1M) + sizeof(resultM) + sizeof(sourceM) + sizeof(reasonM);
	reserved1M = 0;
	resultM = result;
	sourceM = source;
	reasonM = reason;
}

//>>===========================================================================

ASSOCIATE_RJ_CLASS::~ASSOCIATE_RJ_CLASS()

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

bool ASSOCIATE_RJ_CLASS::encode(PDU_CLASS& pdu)

//  DESCRIPTION     : Encode associate reject as PDU.
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

	// encode the result source and reason
	pdu << resultM;
	pdu << sourceM;
	pdu << reasonM;

	return true;
}

//>>===========================================================================

bool ASSOCIATE_RJ_CLASS::decode(PDU_CLASS& pdu)

//  DESCRIPTION     : Decode associate reject from PDU.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// decode the Associate Reject PDU
	itemTypeM = pdu.getType();
	reservedM = pdu.getReserved();			
	lengthM = pdu.getLength();				

	pdu >> reserved1M;
	
	// decode the result, source & reason
	pdu >> resultM;
	pdu >> sourceM;
	pdu >> reasonM;

	return true;
}

//>>===========================================================================

UINT32 ASSOCIATE_RJ_CLASS::getLength()

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

bool ASSOCIATE_RJ_CLASS::updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS *wid_ptr)

//  DESCRIPTION     : Update this object with the contents of the object given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = false;

	// ensure update WID is an associate reject
	if (wid_ptr->getWidType() == widTypeM)
	{
		ASSOCIATE_RJ_CLASS *updateAssociateRj_ptr = static_cast<ASSOCIATE_RJ_CLASS*>(wid_ptr);

		// update parameters
		resultM = updateAssociateRj_ptr->getResult();
		sourceM = updateAssociateRj_ptr->getSource();
		reasonM = updateAssociateRj_ptr->getReason();

		// result is OK
		result = true;
	}

	// return result
	return result;
}
