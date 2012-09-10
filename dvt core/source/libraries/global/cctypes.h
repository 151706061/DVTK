//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef CCTYPES_H
#define CCTYPES_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "ucdavis.h"


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
#ifdef	SOLARIS
#define	SYSTEM_V
#endif

typedef unsigned int     UINT;
typedef unsigned short   UINT16;
typedef unsigned char    UINT8;

#ifndef	_WINDOWS
typedef unsigned char    BYTE;
#endif

typedef signed char     INT8;
typedef signed short    INT16;

#ifndef	_WINDOWS
typedef unsigned long   UINT32;
typedef signed long     INT32;
typedef signed int      INT;
#endif

#ifdef _WINDOWS
typedef DWORD			THREAD_TYPE;
#else
typedef pthread_t		THREAD_TYPE;
#endif

#ifdef	LITTLE_ENDIAN
#undef	LITTLE_ENDIAN
#endif

#ifdef	BIG_ENDIAN
#undef	BIG_ENDIAN
#endif

#define LITTLE_ENDIAN   1
#define BIG_ENDIAN      2

#ifndef	NATIVE_ENDIAN
#define	NATIVE_ENDIAN	LITTLE_ENDIAN
#endif

#endif /* CCTYPES_H */


