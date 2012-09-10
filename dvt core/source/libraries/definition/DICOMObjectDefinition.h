//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DICOMOBJECTDEF_H
#define DICOMOBJECTDEF_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_ATTRIBUTE_CLASS;
class DEF_MODULE_CLASS;


//>>***************************************************************************

class DEF_DICOM_OBJECT_CLASS

//  DESCRIPTION     : Dicom object definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_DICOM_OBJECT_CLASS();
	DEF_DICOM_OBJECT_CLASS(const string);
	~DEF_DICOM_OBJECT_CLASS();

	void SetName(const string name) { nameM = name; }

	void SetDimseCmd(DIMSE_CMD_ENUM dimseCmd) { dimseCmdM = dimseCmd; }

	void AddModule(DEF_MODULE_CLASS*);

	string GetName() { return nameM; }

	DIMSE_CMD_ENUM GetDimseCmd() { return dimseCmdM; }
		
	UINT GetNrModules() { return modulesM.size(); }

	DEF_MODULE_CLASS* GetModule(UINT);
    DEF_MODULE_CLASS* GetModule(UINT16, UINT16);

    DEF_ATTRIBUTE_CLASS* GetAttribute(UINT16, UINT16);
    DEF_ATTRIBUTE_CLASS* GetAttribute(string, UINT16, UINT16);

private:
	string nameM;		
	DIMSE_CMD_ENUM dimseCmdM;
    vector<DEF_MODULE_CLASS*> modulesM;
};

#endif /* DICOMOBJECTDEF_H */

