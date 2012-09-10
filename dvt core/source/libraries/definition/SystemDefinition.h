//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_SYSTEM_H
#define DEF_SYSTEM_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATIONS
//*****************************************************************************
class DEF_AE_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
typedef vector<DEF_AE_CLASS*>	DEF_AE_LIST;


//>>***************************************************************************

class DEF_SYSTEM_CLASS

//  DESCRIPTION     : System Definition Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_SYSTEM_CLASS();  
	DEF_SYSTEM_CLASS(const string, const string);
    ~DEF_SYSTEM_CLASS();
          
	void SetName(const string);

	void SetVersion(const string);

	string GetName();

	string GetVersion();

	void AddAE(DEF_AE_CLASS*);
		
	UINT GetNrAes();

	DEF_AE_CLASS* GetAE(UINT);
    DEF_AE_CLASS* GetAE(const string, const string);
	DEF_AE_CLASS* GetAE(const string);

private:
	string nameM;
	string versionM;
    DEF_AE_LIST aesM;
};


#endif /* SYSTEM_DEFINITION_H */
