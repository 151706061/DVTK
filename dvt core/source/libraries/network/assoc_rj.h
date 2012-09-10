// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	Associate Reject class.
//*****************************************************************************
#ifndef ASSOC_RJ_H
#define ASSOC_RJ_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"        // Global component interface
#include "Iwarehouse.h"     // Warehouse component interface
#include "pdu_items.h"      // PDU Items


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class PDU_CLASS;


//>>***************************************************************************

class ASSOCIATE_RJ_CLASS : public PDU_ITEM_CLASS, public BASE_WAREHOUSE_ITEM_DATA_CLASS

//  DESCRIPTION     :
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	UINT32	lengthM;
	BYTE	reserved1M;
	BYTE	resultM;
	BYTE	sourceM;
	BYTE	reasonM;

public:
	ASSOCIATE_RJ_CLASS();
	ASSOCIATE_RJ_CLASS(BYTE, BYTE, BYTE);

	~ASSOCIATE_RJ_CLASS();		

	void setResult(BYTE result)
		{ resultM = result; }

	void setSource(BYTE source)
		{ sourceM = source; }
		
	void setReason(BYTE reason)
		{ reasonM = reason; }

	BYTE getResult()
		{ return resultM; }

	BYTE getSource()
		{ return sourceM; }
		
	BYTE getReason()
		{ return reasonM; }

	bool encode(PDU_CLASS&);

	bool decode(PDU_CLASS&);

	UINT32 getLength();

	bool updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*);
};

#endif /* ASSOC_RJ_H */
