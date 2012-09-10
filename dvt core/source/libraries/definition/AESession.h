//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef SESSION_AE_H
#define SESSION_AE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//>>***************************************************************************

class AE_SESSION_CLASS

//  DESCRIPTION     : Application Entity Session Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	AE_SESSION_CLASS();
	AE_SESSION_CLASS(const string, const string);
	~AE_SESSION_CLASS();

	void SetName(const string name) { nameM = name; }
	void SetVersion(const string version) { versionM = version; }

	string GetName() { return nameM; }
	string GetVersion() { return versionM; }

private:
	string nameM;
	string versionM;
};

#endif /* SESSION_AE_H */

