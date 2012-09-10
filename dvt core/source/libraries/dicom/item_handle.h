//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ITEM_HANDLE_H
#define ITEM_HANDLE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "dcm_item.h"


//>>***************************************************************************

class SEQUENCE_REF_CLASS

//  DESCRIPTION     : Sequence Reference class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string	iodNameM;
	string	identifierM;
	UINT16	groupM;	
	UINT16 	elementM;
	int		itemNumberM;

public:
	SEQUENCE_REF_CLASS(string, string, UINT16, UINT16, UINT);
	SEQUENCE_REF_CLASS(UINT16, UINT16, UINT);

	~SEQUENCE_REF_CLASS();

	string getIodName()
		{ return iodNameM; }

	string getIdentifier()
		{ return identifierM; }

	UINT16 getGroup()
		{ return groupM; }
	
	UINT16 getElement()
		{ return elementM; }

	int getItemNumber()
		{ return itemNumberM; }
};


//>>***************************************************************************

class ITEM_HANDLE_CLASS : public BASE_WAREHOUSE_ITEM_DATA_CLASS

//  DESCRIPTION     : Item Handle class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string						identifierM;
	string						nameM;
	ARRAY<SEQUENCE_REF_CLASS*>	sequenceRefM;

public:
	ITEM_HANDLE_CLASS();

	~ITEM_HANDLE_CLASS();

	void setIdentifier(string identifier)
		{ identifierM = identifier; }

	string getIdentifier()
		{ return identifierM; }

	void setName(string name)
		{ nameM = name; }

	string getName()
		{ return nameM; }

	void add(SEQUENCE_REF_CLASS *sqReference_ptr)
	{	
		if (sqReference_ptr)
		{
			sequenceRefM.add(sqReference_ptr);
		}
	}

	UINT getNoSequenceRefs()
		{ return sequenceRefM.getSize(); }

	SEQUENCE_REF_CLASS *getSequenceRef(UINT i)
	{
		SEQUENCE_REF_CLASS *sequenceRef_ptr = NULL;
		if (i < sequenceRefM.getSize())
		{
			sequenceRef_ptr = sequenceRefM[i];
		}
		return sequenceRef_ptr;
	}

	DCM_ITEM_CLASS *resolveReference();

	bool updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*);
};


#endif /* ITEM_HANDLE_H */
