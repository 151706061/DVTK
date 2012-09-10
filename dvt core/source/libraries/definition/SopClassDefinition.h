//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_SOPCLASS_H
#define DEF_SOPCLASS_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_DATASET_CLASS;
class DEF_MACRO_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
typedef vector<DEF_DATASET_CLASS*>	DEF_DATASET_LIST;
typedef vector<DEF_MACRO_CLASS*>	DEF_MACRO_LIST;


//>>***************************************************************************

class DEF_SOPCLASS_CLASS

//  DESCRIPTION     : SOP Class Definition Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_SOPCLASS_CLASS();

	DEF_SOPCLASS_CLASS(const string, const string);

	~DEF_SOPCLASS_CLASS();

	string GetUid() { return uidM; }

	string GetName() { return nameM; }

	bool AddDataset(DEF_DATASET_CLASS*);

	void AddMacro(DEF_MACRO_CLASS*);

	UINT GetNrDatasets() { return datasetsM.size(); }

	DEF_DATASET_CLASS* GetDataset(UINT i) { return datasetsM[i]; }
	DEF_DATASET_CLASS* GetDataset(DIMSE_CMD_ENUM);
	DEF_DATASET_CLASS* GetDataset(const string);
	DEF_DATASET_CLASS* GetDataset(const string, DIMSE_CMD_ENUM);

private:
	string uidM;
	string nameM;
	DEF_DATASET_LIST datasetsM;
	DEF_MACRO_LIST macrosM;
};

#endif /* DEF_SOPCLASS_H */

