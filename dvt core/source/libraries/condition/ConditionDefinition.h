//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
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
class CONDITION_NODE_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;


//>>***************************************************************************

class CONDITION_CLASS

//  DESCRIPTION     : Defines Conditions for the DICOM definition files.
//                    Conditions can be applied for example for Attributes, 
//                    Modules, Macro's and Items
//  INVARIANT       :
//  NOTES           : The primary node is the normal condition which is
//					  always evaluated. The secondary condition is evaluated
//					  if it is present and the primary condition evaluates
//					  false.
//                    
//<<***************************************************************************
{
    private:
        CONDITION_NODE_CLASS* primaryNodeM_ptr;
        CONDITION_NODE_CLASS* secondaryNodeM_ptr;

	public:
		CONDITION_CLASS();
		~CONDITION_CLASS();

		void SetPrimaryNode(CONDITION_NODE_CLASS* node_ptr) { primaryNodeM_ptr = node_ptr; }

		void SetSecondaryNode(CONDITION_NODE_CLASS* node_ptr) { secondaryNodeM_ptr = node_ptr; }

		string GetResultMessage();

		CONDITION_RESULT_ENUM Evaluate(DCM_ATTRIBUTE_GROUP_CLASS*, DCM_ATTRIBUTE_GROUP_CLASS*, LOG_CLASS*);

		void Log(LOG_CLASS*);
};


#endif /* CONDITION_DEFINITION_H */

