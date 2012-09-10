// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	Value Representation classes.
//*****************************************************************************
#ifndef VR_H
#define VR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"        // Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class PDU_CLASS;


//>>***************************************************************************

class BYTE_STRING_CLASS

//  DESCRIPTION     : Byte String Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
protected:
	UINT32	lengthM;
	BYTE*	valueM_ptr;

	void cleanup();

public:
	BYTE_STRING_CLASS();
	BYTE_STRING_CLASS(BYTE_STRING_CLASS&);
	BYTE_STRING_CLASS(BYTE*, UINT32);
	BYTE_STRING_CLASS(char*);

	virtual ~BYTE_STRING_CLASS();

	virtual void set(BYTE*, UINT32);
	virtual void set(char*);

	bool operator = (BYTE_STRING_CLASS&);

	BYTE* get()
		{ return valueM_ptr; }

	UINT32 getLength()
		{ return lengthM; }
};


//>>***************************************************************************

class AE_TITLE_CLASS : public BYTE_STRING_CLASS

//  DESCRIPTION     : AT Title Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	AE_TITLE_CLASS();
	AE_TITLE_CLASS(AE_TITLE_CLASS&);
	AE_TITLE_CLASS(BYTE*, UINT32);
	AE_TITLE_CLASS(char*);

	~AE_TITLE_CLASS();

	void set(BYTE*, UINT32);
	void set(char*);

	bool operator = (AE_TITLE_CLASS&);

	bool encode(PDU_CLASS&);

	bool decode(PDU_CLASS&, UINT32);
};


//>>***************************************************************************

class UID_CLASS : public BYTE_STRING_CLASS

//  DESCRIPTION     : UID Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	UID_CLASS();
	UID_CLASS(UID_CLASS&);
	UID_CLASS(BYTE*, UINT32);
	UID_CLASS(char*);

	~UID_CLASS();

	void set(UID_CLASS&);
	void set(BYTE* uid_ptr, UINT32 length)
		{ BYTE_STRING_CLASS::set(uid_ptr, length); }
	void set(char* uid_ptr)
		{ BYTE_STRING_CLASS::set(uid_ptr); }

	bool isValid()
		{
			// todo: implement this
			return true; 
		}

	bool validate();

	bool operator = (UID_CLASS&);
	bool operator == (UID_CLASS&);
	bool operator == (const char*);
	bool operator != (UID_CLASS&);
};

#endif /* VR_H */