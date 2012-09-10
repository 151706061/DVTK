//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Record link include file for validation.
//*****************************************************************************
#ifndef RECORD_LINK_H
#define RECORD_LINK_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_MESSAGE_CLASS;

//*****************************************************************************
//  Type definitions
//*****************************************************************************

//>>***************************************************************************
class RECORD_LINK_CLASS
{
    public:
		RECORD_LINK_CLASS(UINT32);
        ~RECORD_LINK_CLASS();

        UINT32 GetDownLinkOffset();
        void SetDownLinkOffset(UINT32);

        UINT32 GetHorLinkOffset();
        void SetHorLinkOffset(UINT32);

        UINT32 GetRecordOffset();

        UINT16 GetInUseFlag();
        void SetInUseFlag(UINT16);

        UINT16 GetReferenceCount();
        void IncreaseReferenceCount();

        DICOMDIR_RECORD_TYPE_ENUM GetRecordType();
        void SetRecordType(DICOMDIR_RECORD_TYPE_ENUM);

        LOG_MESSAGE_CLASS *GetMessages();
        bool HasMessages();

        bool CheckUnreferencedRecords();

    private:
        UINT32 recordOffsetM;
        UINT32 horLinkOffsetM;
        UINT32 downLinkOffsetM;
        UINT16 inUseFlagM;
        UINT16 refCountM;
        DICOMDIR_RECORD_TYPE_ENUM recordTypeM;
        LOG_MESSAGE_CLASS *messagesM_ptr;
};

#endif /* RECORD_LINK_H */
