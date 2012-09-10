// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

// CrtWrapper.cpp

// This code verifies that DllMain is not called by the Loader
#pragma once

#include "stdafx.h"
// automatically when linked with /noentry. It also checks some
// functions that the CRT initializes.

#include <windows.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <math.h>
#include "_vcclrit.h"

#using <mscorlib.dll>
using namespace System;

public __gc class CrtWrapper {
public:
   static int minitialize() {
      int retval = 0;
      try {
            __crt_dll_initialize();
      } catch(System::Exception* e) {
         Console::WriteLine(e);
         retval = 1;
      }
      return retval;
   }
   static int mterminate() {
      int retval = 0;
      try {
            __crt_dll_terminate();
      } catch(System::Exception* e) {
         Console::WriteLine(e);
         retval = 1;
      }
      return retval;
   }
};

//BOOL WINAPI DllMain(
//                    HINSTANCE hModule,
//                    DWORD dwReason,
//                    LPVOID lpvReserved)
//{
//   Console::WriteLine(S"DllMain is called...");
//   return TRUE;
//};/* DllMain */
