//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef BASE_H
#define BASE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
extern "C"
{
#include <errno.h>
#include <stdio.h>
#include <stdarg.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>
#include <malloc.h>
#include <memory.h>
};

#ifdef	_WINDOWS
#include <windows.h>
#include <winsock.h>
#include <share.h>
#pragma warning (disable : 4786)
#else
#include "unixsock.h"
#endif

#include <iostream>
#include <string>
#include <vector>
#include <map>
#include <stack>

using namespace std;

#endif /* BASE_H */

