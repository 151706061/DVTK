//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEFFILECONTENT_H
#define DEFFILECONTENT_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_METASOPCLASS_CLASS;
class DEF_SOPCLASS_CLASS;
class DEF_COMMAND_CLASS;
class DEF_DATASET_CLASS;
class DEF_ATTRIBUTE_CLASS;
class DEF_ITEM_CLASS;
class DEF_MACRO_CLASS;

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************


//>>***************************************************************************

class DEF_FILE_CONTENT_CLASS

//  DESCRIPTION     : Definition File Content Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_FILE_CONTENT_CLASS();
	~DEF_FILE_CONTENT_CLASS();

	void SetSystem(const string, const string);

	void SetAE(const string, const string);

	void SetMetaSop(DEF_METASOPCLASS_CLASS*);

	void AddSopClass(DEF_SOPCLASS_CLASS*);

	void AddCommand(DEF_COMMAND_CLASS*);

	bool Register();

	bool Unregister();

	string GetSystemName();

	string GetSystemVersion();

	string GetAEName();

	string GetAEVersion();

	string GetSOPClassName();

	string GetSOPClassUID();

	bool IsMetaSOPClass();

	DEF_METASOPCLASS_CLASS* GetMetaSop();

	DEF_METASOPCLASS_CLASS* GetMetaSopByUid(const string);

	DEF_SOPCLASS_CLASS* GetSopByUid(const string);
    
	DEF_SOPCLASS_CLASS* GetSopByName(const string);
	
	DEF_SOPCLASS_CLASS* GetFirstSop();

	DEF_DATASET_CLASS* GetDataset(const string);
	DEF_DATASET_CLASS* GetDataset(DIMSE_CMD_ENUM, const string);

private:
	string							systemNameM;
	string							systemVersionM;
	string							aeNameM;
	string							aeVersionM;
	DEF_METASOPCLASS_CLASS*			metaSopClassM_ptr;
	vector<DEF_SOPCLASS_CLASS*>		sopClassM;
	vector<DEF_COMMAND_CLASS*>		commandM;

	void RegisterIod(DEF_SOPCLASS_CLASS*, bool);

	void RegisterMacroInMacro(DEF_MACRO_CLASS*, bool);

	void RegisterAttributesInSequence(DEF_ATTRIBUTE_CLASS*, bool);

	void RegisterAttributesInItem(DEF_ITEM_CLASS*, bool);

	bool IsPrivateAttribute(UINT16, UINT16);
};

#endif /* DEFFILECONTENT_H */

