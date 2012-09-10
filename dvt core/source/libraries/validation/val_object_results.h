//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef VAL_OBJECT_RESULTS_H
#define VAL_OBJECT_RESULTS_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_COMMAND_CLASS;
class LOG_MESSAGE_CLASS;
class VAL_ATTRIBUTE_GROUP_CLASS;
class VAL_ATTRIBUTE_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;

//>>***************************************************************************

class VAL_OBJECT_RESULTS_CLASS

//  DESCRIPTION     : Validation Object Results Class
//  NOTES           :
//<<***************************************************************************
{
    public:
        VAL_OBJECT_RESULTS_CLASS();
        virtual ~VAL_OBJECT_RESULTS_CLASS();

        DVT_STATUS AddModuleResults(VAL_ATTRIBUTE_GROUP_CLASS*);
        int GetNrModuleResults(void);
        VAL_ATTRIBUTE_GROUP_CLASS *GetModuleResults(int);
        VAL_ATTRIBUTE_CLASS *GetAttributeResults(UINT16, UINT16);
        VAL_ATTRIBUTE_CLASS *GetAttributeResults(UINT32);
        bool GetListOfAttributeResults(UINT16, UINT16, vector <VAL_ATTRIBUTE_CLASS*>*);

        VAL_ATTRIBUTE_GROUP_CLASS *GetAGWithAttributeInGroup(UINT16);
        VAL_ATTRIBUTE_GROUP_CLASS *GetAdditionalAttributeGroup();

        void SetName(string);
        string GetName();

        void SetCommand(DCM_COMMAND_CLASS*);
        DCM_COMMAND_CLASS *GetCommand();

		void SetFmi(DCM_ATTRIBUTE_GROUP_CLASS*);
		DCM_ATTRIBUTE_GROUP_CLASS *GetFmi();

        DVT_STATUS ValidateAgainstDef(UINT32);
        DVT_STATUS ValidateAgainstRef(UINT32);
        DVT_STATUS ValidateVR(UINT32, SPECIFIC_CHARACTER_SET_CLASS*);
        void HasReferenceObject(bool);

        LOG_MESSAGE_CLASS *GetMessages();
        bool HasMessages();

        void CleanUp();

    private:
        vector<VAL_ATTRIBUTE_GROUP_CLASS*> modulesM;
        VAL_ATTRIBUTE_GROUP_CLASS *additionalAttributesM_ptr;
        DCM_COMMAND_CLASS *commandM_ptr;
		DCM_ATTRIBUTE_GROUP_CLASS *fileMetaInfoM_ptr;
        LOG_MESSAGE_CLASS *messagesM_ptr;
		string nameM;
        bool hasReferenceObjectM;
};

#endif /* VAL_OBJECT_RESULTS_H */