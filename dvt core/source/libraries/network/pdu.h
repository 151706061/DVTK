// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	RAW PDU class.
//*****************************************************************************
#ifndef PDU_H
#define PDU_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"        // Global component interface
#include "Iutility.h"       // Utility component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class BASE_SOCKET_CLASS;


//>>***************************************************************************

class PDU_CLASS : public ENDIAN_CLASS

//  DESCRIPTION     : PDU Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	BYTE*	bodyM_ptr;
	UINT32	maxLengthM;
	UINT32	offsetM;
	UINT32	logLengthM;
    UINT32  maxLengthToReceiveM;


protected:
	BYTE	pduTypeM;
	BYTE	reservedM;
	UINT32	lengthM;

	void setOffset(UINT32 offset)
		{ offsetM = offset; }


	BYTE* getData()
		{ return bodyM_ptr; }

public:
	PDU_CLASS();
	PDU_CLASS(BYTE);

	virtual ~PDU_CLASS();		

	bool write(BASE_SOCKET_CLASS*);

	bool readType(BASE_SOCKET_CLASS*);

	virtual bool readBody(BASE_SOCKET_CLASS*);

	bool writeBinary(const BYTE *, UINT);
		
	INT	readBinary(BYTE *, UINT);

	void setType(BYTE type)
		{ pduTypeM = type; }

	void setReserved(BYTE reserved)
		{ reservedM = reserved; }

	bool allocateBody(UINT32);

	bool setLength(UINT32);

    void setMaxLengthToReceive(UINT32 length);

	BYTE getType()
		{ return pduTypeM; }

	BYTE getReserved()
		{ return reservedM; }

	UINT32 getLength()
		{ return lengthM; }

	void setLogLength(UINT32 length)
		{ logLengthM = length; }

	UINT32 getOffset()
		{ return offsetM; }

	void logRaw(LOG_CLASS*);
};

#endif /* PDU_H */