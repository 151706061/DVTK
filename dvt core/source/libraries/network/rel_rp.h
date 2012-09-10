// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	Release Response class.
//*****************************************************************************
#ifndef REL_RP_H
#define REL_RP_H

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

class RELEASE_RP_CLASS : public PDU_ITEM_CLASS, public BASE_WAREHOUSE_ITEM_DATA_CLASS


//  DESCRIPTION     : Release Response Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	UINT32	lengthM;
	UINT32	reserved1M;

public:
	RELEASE_RP_CLASS();

	~RELEASE_RP_CLASS();

	bool encode(PDU_CLASS&);

	bool decode(PDU_CLASS&);

	UINT32 getLength();

	bool updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*);
};

#endif /* REL_RP_H */