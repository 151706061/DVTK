//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ATTRIBUTE_DEFINITION_H
#define ATTRIBUTE_DEFINITION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"			// Global component interface
#include "IAttributeGroup.h"	// Attribute Group interface
#include "Icondition.h"			// Condition interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class CONDITION_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;


//>>***************************************************************************

class DEF_ATTRIBUTE_CLASS : public ATTRIBUTE_CLASS

//  DESCRIPTION     : Attribute definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_ATTRIBUTE_CLASS();
		
	DEF_ATTRIBUTE_CLASS(UINT16, UINT16);

	DEF_ATTRIBUTE_CLASS(UINT16, UINT16, ATTR_VR_ENUM);
		
	~DEF_ATTRIBUTE_CLASS();
	
	void SetSecondVR(ATTR_VR_ENUM vr) { secondVrM = vr; }

	void SetVmMin(UINT min) { vmM.ATTR_VM_MIN = min; }

	void SetVmMax(UINT max) { vmM.ATTR_VM_MAX = max; }

	void SetVmRestriction(ATTR_VM_RESTRICT_ENUM restriction) { vmM.ATTR_VM_RESTRICTION = restriction; }
	
	void SetName(const string name);

	void SetCondition(CONDITION_CLASS *cond_ptr) { conditionM_ptr = cond_ptr; }

	void SetTextualCondition(const string message) { textual_conditionM = message; }

	UINT32 GetTag() { return ((((UINT32)GetGroup()) << 16) + ((UINT32)GetElement())); }

	ATTR_VR_ENUM GetSecondVR() { return secondVrM; }

	UINT GetVmMin() { return vmM.ATTR_VM_MIN; }

	UINT GetVmMax() { return vmM.ATTR_VM_MAX; }

    ATTR_VM_RESTRICT_ENUM GetVmRestriction() { return vmM.ATTR_VM_RESTRICTION; }

	string GetName()    { return nameM; }

	string GetXMLName() { return xml_nameM; }

	CONDITION_CLASS* GetCondition() { return conditionM_ptr; }

	string GetConditionResultMessage();

	string GetTextualCondition() { return textual_conditionM; }

	bool operator = (DEF_ATTRIBUTE_CLASS&);

private:
	ATTR_VR_ENUM secondVrM; //some attributes can have a second VR
	ATTR_VM_STRUCT vmM;
	string nameM;
	string xml_nameM;
	CONDITION_CLASS *conditionM_ptr;
	string textual_conditionM;
};


#endif /* ATTRIBUTE_DEFINITION_H */
