//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef UID_REF_H
#define UID_REF_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_MESSAGE_CLASS;

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
struct ENTITY_NAME_STRUCT
{
    UINT32 tag;
    string name;
    RECORD_IOD_TYPE_ENUM iodType;
};
static const string iodTypes[] =
{
    "IMAGE",
    "PATIENT",
    "PRIVATE",
    "SERIES",
    "STUDY",
    "STUDY COMPONENT",
    "VISIT"
};

typedef vector<ENTITY_NAME_STRUCT*> ENTITY_NAME_VECTOR;

class UID_REF_CLASS
{
    public:
		UID_REF_CLASS();
        virtual ~UID_REF_CLASS();

        string GetUid();
        void SetUid(string);

        void AddDefining(ENTITY_NAME_STRUCT*);
        void AddReferring(ENTITY_NAME_STRUCT*);

        void Check(LOG_MESSAGE_CLASS*);

    private:
        string uidM;
        ENTITY_NAME_VECTOR definingM;
        ENTITY_NAME_VECTOR referringM;
};

#endif /* UID_REF_H */
