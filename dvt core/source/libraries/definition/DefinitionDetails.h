//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_DETAIL_H
#define DEF_DETAIL_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//>>***************************************************************************

class DEF_DETAILS_CLASS

//  DESCRIPTION     : Definition details class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_DETAILS_CLASS();

	~DEF_DETAILS_CLASS();

	void SetAEName(const string);

	void SetAEVersion(const string);

	void SetSOPClassName(const string);

	void SetSOPClassUID(const string);

	void SetIsMetaSOPClass(bool);

	string GetAEName();

	string GetAEVersion();

	string GetSOPClassName();

	string GetSOPClassUID();

	bool GetIsMetaSOPClass();

private:
	string AENameM;
	string AEVersionM;
	string SOPClassNameM;
	string SOPClassUIDM;
	bool   IsMetaSOPClassM;
};		

#endif /* DEF_DETAIL_H */
