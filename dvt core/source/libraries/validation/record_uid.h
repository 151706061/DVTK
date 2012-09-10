//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef RECORD_UID_H
#define RECORD_UID_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_ATTRIBUTE_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;
class LOG_MESSAGE_CLASS;
class UID_DEFINING_CLASS;
class UID_REFERRING_CLASS;
class UID_REF_CLASS;
struct UID_REFERENCE_STRUCT;

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
typedef vector<UID_REF_CLASS *>   UID_REF_VECTOR;

class RECORD_UID_CLASS
{
    public:
        RECORD_UID_CLASS();
        virtual ~RECORD_UID_CLASS();

        bool InstallDefiningUid(DCM_ATTRIBUTE_GROUP_CLASS*, UID_DEFINING_CLASS*);

        bool InstallReferringUid(DCM_ATTRIBUTE_GROUP_CLASS*, UID_REFERRING_CLASS*);

        void Check(LOG_MESSAGE_CLASS*);

    private:
        UID_REF_VECTOR uidRefsM;

        void StoreReferringUid(string, UID_REFERENCE_STRUCT*, string);
};

#endif /* RECORD_UID_H */
