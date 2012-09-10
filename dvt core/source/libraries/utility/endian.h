// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.


//  Handle the big/little endian translations.

#ifndef ENDIAN_H
#define ENDIAN_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"			// C Header Files / Base Data Templates 


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************


//>>***************************************************************************

class ENDIAN_CLASS

//  DESCRIPTION     : Class used to handle the big/little endian translations.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	UINT	endianM;

public:

	virtual	~ENDIAN_CLASS() = 0;		

	void setEndian(UINT endian) { endianM = endian; }
		
	inline UINT	getEndian() { return endianM; }
	
	bool operator >> (BYTE&);
	bool operator >> (UINT16&);
	bool operator >> (UINT32&);
	bool operator >> (float&);
	bool operator >> (double&);

	inline bool operator >> (char& x) { return ((*this) >> (BYTE&) x); }
	inline bool operator >> (INT16& x) { return ((*this) >> (UINT16&) x); }
	inline bool operator >> (INT32& x) { return ((*this) >> (UINT32&) x); }

	bool operator << (BYTE&);
	bool operator << (UINT16&);
	bool operator << (UINT32&);
	bool operator << (float&);
	bool operator << (double&);

	inline bool operator << (char& x) { return ((*this) << (BYTE&) x); }
	inline bool operator << (INT16& x) { return ((*this) << (UINT16&) x); }
	inline bool operator << (INT32& x) { return ((*this) << (UINT32&) x); }

	virtual INT	readBinary(BYTE*, UINT) = 0;

	virtual bool writeBinary(const BYTE*, UINT) = 0;
};


#endif /* ENDIAN_H */


