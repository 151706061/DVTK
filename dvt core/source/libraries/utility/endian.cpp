// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.


//  Handle the big/little endian translations.

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "endian.h"


//>>===========================================================================

ENDIAN_CLASS::~ENDIAN_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	// nothing to do explicitly
}


//>>===========================================================================

bool ENDIAN_CLASS::operator >> (BYTE& x)

//  DESCRIPTION     : Read BYTE operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	BYTE	s[2];

	// read required bytes
	if (readBinary(s, 1) != 1) return false;

	x = s[0];

	return true;
}


//>>===========================================================================

bool ENDIAN_CLASS::operator >> (UINT16& x)

//  DESCRIPTION     : Read UINT16 operator according to defined endian
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[US_LENGTH];
	INT offset = 0;

	// read required bytes
	INT length = US_LENGTH;
	while (length)
	{
		INT lengthRead = readBinary(&s[offset], length);
		if (lengthRead <= 0) return false;
		offset += lengthRead;
		length-= lengthRead;
	}

	// check required endianess
	if (endianM == NATIVE_ENDIAN)
	{
		// stream in native endian
		for (i = 0; i < US_LENGTH; i++)
			op[i] = s[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = US_LENGTH; i < US_LENGTH; i++)
			op[i] = s[--j];
	}

	return true;
}


//>>===========================================================================

bool ENDIAN_CLASS::operator >> (UINT32& x)

//  DESCRIPTION     : Read UINT32 operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[UL_LENGTH];
	INT offset = 0;

	// read required bytes
	INT length = UL_LENGTH;
	while (length)
	{
		INT lengthRead = readBinary(&s[offset], length);
		if (lengthRead <= 0) return false;
		offset += lengthRead;
		length-= lengthRead;
	}

	// check required endianess
	if (endianM == NATIVE_ENDIAN) 
	{
		// stream in native endian
		for (i = 0; i < UL_LENGTH; i++)
			op[i] = s[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = UL_LENGTH; i < UL_LENGTH; i++)
			op[i] = s[--j];
	}

	return true;
}


//>>===========================================================================

bool ENDIAN_CLASS::operator >> (float& x)

//  DESCRIPTION     : Read float operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[FL_LENGTH];
	INT offset = 0;

	// read required bytes
	INT length = FL_LENGTH;
	while (length)
	{
		INT lengthRead = readBinary(&s[offset], length);
		if (lengthRead <= 0) return false;
		offset += lengthRead;
		length-= lengthRead;
	}

	// check required endianess
	if (endianM == NATIVE_ENDIAN) 
	{
		// stream in native endian
		for (i = 0; i < FL_LENGTH; i++)
			op[i] = s[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = FL_LENGTH; i < FL_LENGTH; i++)
			op[i] = s[--j];
	}

	return true;
}


//>>===========================================================================

bool ENDIAN_CLASS::operator >> (double& x)

//  DESCRIPTION     : Read double operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[FD_LENGTH];
	INT offset = 0;

	// read required bytes
	INT length = FD_LENGTH;
	while (length)
	{
		INT lengthRead = readBinary(&s[offset], length);
		if (lengthRead <= 0) return false;
		offset += lengthRead;
		length-= lengthRead;
	}

	// check required endianess
	if (endianM == NATIVE_ENDIAN) 
	{
		// stream in native endian
		for (i = 0; i < FD_LENGTH; i++)
			op[i] = s[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = FD_LENGTH; i < FD_LENGTH; i++)
			op[i] = s[--j];
	}

	return true;
}


//>>===========================================================================

bool ENDIAN_CLASS::operator << (BYTE& x)

//  DESCRIPTION     : Write BYTE operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	BYTE	s[2];

	s[0] = x;

	// write required bytes
	return writeBinary(s, 1);
}


//>>===========================================================================

bool ENDIAN_CLASS::operator << (UINT16& x)

//  DESCRIPTION     : Write UINT16 operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[US_LENGTH];

	// check required endianess
	if (endianM == NATIVE_ENDIAN) 
	{
		// stream in native endian
		for (i = 0; i < US_LENGTH; i++)
			s[i] = op[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = US_LENGTH; i < US_LENGTH; i++)
			s[--j] = op[i];
	}

	// write required bytes
	return writeBinary(s, US_LENGTH);
}


//>>===========================================================================

bool ENDIAN_CLASS::operator << (UINT32& x)

//  DESCRIPTION     : Write UINT32 operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[UL_LENGTH];

	// check required endianess
	if (endianM == NATIVE_ENDIAN) 
	{
		// stream in native endian
		for (i = 0; i < UL_LENGTH; i++)
			s[i] = op[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = UL_LENGTH; i < UL_LENGTH; i++)
			s[--j] = op[i];
	}

	// write required bytes
	return writeBinary(s, UL_LENGTH);
}


//>>===========================================================================

bool ENDIAN_CLASS::operator << (float& x)

//  DESCRIPTION     : Write float operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[FL_LENGTH];

	// check required endianess
	if (endianM == NATIVE_ENDIAN) 
	{
		// stream in native endian
		for (i = 0; i < FL_LENGTH; i++)
			s[i] = op[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = FL_LENGTH; i < FL_LENGTH; i++)
			s[--j] = op[i];
	}

	// write required bytes
	return writeBinary(s, FL_LENGTH);
}


//>>===========================================================================

bool ENDIAN_CLASS::operator << (double& x)

//  DESCRIPTION     : Write double operator according to defined endian.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : Abstract Base class.
//<<===========================================================================
{
	int		i, j;
	BYTE	*op = (BYTE *) &x;
	BYTE	s[FD_LENGTH];

	// check required endianess
	if (endianM == NATIVE_ENDIAN) 
	{
		// stream in native endian
		for (i = 0; i < FD_LENGTH; i++)
			s[i] = op[i];
	}
	else 
	{
		// stream in reverse endian
		for (i = 0, j = FD_LENGTH; i < FD_LENGTH; i++)
			s[--j] = op[i];
	}

	// write required bytes
	return writeBinary(s, FD_LENGTH);
}
