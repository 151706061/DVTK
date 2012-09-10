// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
#ifndef ABORT_RQ_H
#define ABORT_RQ_H

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

class ABORT_RQ_CLASS : public PDU_ITEM_CLASS, public BASE_WAREHOUSE_ITEM_DATA_CLASS

//  DESCRIPTION     : Abort Request Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	UINT32	lengthM;
	BYTE	reserved1M;
	BYTE	reserved2M;
	BYTE	sourceM;
	BYTE	reasonM;

public:
	ABORT_RQ_CLASS();
	ABORT_RQ_CLASS(BYTE, BYTE);

	~ABORT_RQ_CLASS();		

	void setSource(BYTE source)
		{ sourceM = source; }
		
	void setReason(BYTE reason)
		{ reasonM = reason; }

	BYTE getSource()
		{ return sourceM; }
		
	BYTE getReason()
		{ return reasonM; }

	bool encode(PDU_CLASS&);

	bool decode(PDU_CLASS&);

	UINT32 getLength();

	bool updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*);
};

#endif /* ABORT_RQ_H */