//*****************************************************************************
//  FILENAME        : ConditionDefinition.h
//  PACKAGE         : DVT
//  COMPONENT       : DEFINITION
//  DESCRIPTION     : Condition Definition Class
//  COPYRIGHT(c)    : 2000, Philips Electronics N.V.
//                    2000, Agfa Gevaert N.V.
//*****************************************************************************
#ifndef CONDITION_DEFINITION_H
#define CONDITION_DEFINITION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;
class DEF_COND_NODE_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;


//>>***************************************************************************

class DEF_CONDITION_CLASS

//  DESCRIPTION     : Defines Conditions for the DICOM definition files.
//                    Conditions can be applied for example for Attributes, 
//                    Modules, Macro's and Items
//  INVARIANT       :
//  NOTES           : 
//                    
//<<***************************************************************************
{
    private:
        DEF_COND_NODE_CLASS* nodeM_ptr;

	public:
		DEF_CONDITION_CLASS();
		~DEF_CONDITION_CLASS();

		void SetNode(DEF_COND_NODE_CLASS* node_ptr) { nodeM_ptr = node_ptr; }

		string GetResultMessage();

		bool Evaluate(DCM_ATTRIBUTE_GROUP_CLASS*, DCM_ATTRIBUTE_GROUP_CLASS*, LOG_CLASS*);

		void Log(LOG_CLASS*);
};


#endif /* CONDITION_DEFINITION_H */

