// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.


//*****************************************************************************
//  DESCRIPTION     :	File Data Transfer class.
//*****************************************************************************
#ifndef FILE_TF_H
#define FILE_TF_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "data_tf.h"		// Data Transfer


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************


//>>***************************************************************************

class ITEM_OFFSET_CLASS

//  DESCRIPTION     : Item Offset
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string	identifierM;
	UINT32	offsetM;

public:
	ITEM_OFFSET_CLASS();
	ITEM_OFFSET_CLASS(string, UINT32);

	~ITEM_OFFSET_CLASS() { }

	string getIdentifier()
		{ return identifierM; }

	UINT32 getOffset()
		{ return offsetM; }
};


//>>***************************************************************************

class FILE_TF_CLASS : public DATA_TF_CLASS

//  DESCRIPTION     : File Data Transfer Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	FILE						*fileM_ptr;
	UINT32						nr_bytes_readM;
	ARRAY<ITEM_OFFSET_CLASS>	itemOffsetM;
	LOG_CLASS					*loggerM_ptr;
	UINT32						logLengthM;

public:
	FILE_TF_CLASS();
	FILE_TF_CLASS(string, string);

	~FILE_TF_CLASS();

	bool initialiseDecode(bool);

	bool terminateDecode();

	bool initialiseEncode();

	bool terminateEncode();

	bool rewind(UINT);

	UINT getOffset();

	bool setOffset(UINT);

	bool isData();

	bool open(string, string);

	bool isOpen() 
		{ return (fileM_ptr == NULL) ? false : true; }

	void close();

	UINT getLength();

	UINT getRemainingLength();

	bool writeBinary(const BYTE *, UINT);
		
	INT	readBinary(BYTE *, UINT);

	void addItemOffset(string identifier, UINT32 offset)
		{
			ITEM_OFFSET_CLASS	itemOffset(identifier, offset);
			itemOffsetM.add(itemOffset);
		}

	UINT32 getItemOffset(string);

	void setLogger(LOG_CLASS *logger_ptr)
		{ loggerM_ptr = logger_ptr; }

	void setLogLength(UINT32 length)
		{ logLengthM = length; }

	LOG_CLASS *getLogger() { return loggerM_ptr; }
};

#endif /* FILE_TF_H */


