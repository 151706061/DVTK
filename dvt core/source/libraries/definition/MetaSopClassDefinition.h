//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef METASOPCLASSDEF_H
#define METASOPCLASSDEF_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  CONSTANTS AND TYPE DEFS
//*****************************************************************************
typedef map<string, string> METASOPCLASS_MAP;


//>>***************************************************************************

class DEF_METASOPCLASS_CLASS

//  DESCRIPTION     :
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_METASOPCLASS_CLASS(const string, const string);

	~DEF_METASOPCLASS_CLASS();

	void AddSopClass(const string, const string);

	bool HasSopClass(const string);
		
	inline string GetUid() { return uidM; };

	inline string GetName() { return nameM; };

	UINT GetNoSopClasses();
	void GetSopClass(UINT, string&, string&);

    string GetImageBoxSopUid();
	string GetImageBoxSopName();

private:
	string uidM;
	string nameM;

	METASOPCLASS_MAP sopClassesM;
};


#endif /* METASOPCLASSDEF_H */
