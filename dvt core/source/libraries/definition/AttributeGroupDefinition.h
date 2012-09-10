//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_ATTRIBUTE_GROUP_H
#define DEF_ATTRIBUTE_GROUP_H

#include "Iglobal.h"			// Global component interface
#include "IAttributeGroup.h"	// Attribute Group component interface
#include "Icondition.h"			// Condition component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class CONDITION_CLASS;
class DEF_ATTRIBUTE_CLASS;
class DEF_MACRO_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
struct MACRO_REF_STRUCT
{
	string name;
	DEF_MACRO_CLASS *macro_ptr;
	CONDITION_CLASS *cond_ptr;
    string textualCondition;
};


//>>***************************************************************************

class DEF_ATTRIBUTE_GROUP_CLASS : public ATTRIBUTE_GROUP_CLASS

//  DESCRIPTION     : Attribute Group Definition Class
//                    Abstract Base Class for attribute groups such as Modules
//                    Macro's and Items
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_ATTRIBUTE_GROUP_CLASS();

	DEF_ATTRIBUTE_GROUP_CLASS(const string);

	DEF_ATTRIBUTE_GROUP_CLASS(const string, const MOD_USAGE_ENUM);

	~DEF_ATTRIBUTE_GROUP_CLASS();

	void SetUsage(const MOD_USAGE_ENUM);

	void SetContentRegistered(const bool);

	void SetCondition(CONDITION_CLASS*);

	void SetTextualCondition(const string);
		
	MOD_USAGE_ENUM GetUsage();

	bool GetContentRegistered();

	CONDITION_CLASS* GetCondition();

    bool EvaluateAddUserOptionalModule (ATTRIBUTE_GROUP_CLASS * actual_data);

	string GetTextualCondition();

	string GetConditionResultMessage();

	void AddMacroReference(const string, CONDITION_CLASS*, const string);

	void ResolveMacroReference(const string, DEF_MACRO_CLASS*);

	bool CheckMacroReferences();

	DEF_ATTRIBUTE_CLASS* GetAttribute(UINT);
	DEF_ATTRIBUTE_CLASS* GetAttribute(UINT, UINT);
    DEF_ATTRIBUTE_CLASS* GetAttribute(UINT, UINT, DEF_MACRO_CLASS*);

	int GetNrMacros() { return macro_refsM.size(); }
		
	DEF_MACRO_CLASS* GetMacro(UINT);

	CONDITION_CLASS* GetMacroCondition(UINT);

    string GetMacroTextualCondition(UINT);

private:
	MOD_USAGE_ENUM usageM;
	bool contentRegisteredM;

	CONDITION_CLASS *conditionM_ptr;
	string textual_conditionM;
		
	vector<MACRO_REF_STRUCT> macro_refsM;
};


#endif /* DEF_ATTRIBUTE_GROUP_H */

