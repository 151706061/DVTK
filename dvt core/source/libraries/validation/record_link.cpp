//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "record_link.h"
#include "valrules.h"

#include "Iglobal.h"        // Global component interface
#include "Ilog.h"           // Logger component interface

//>>===========================================================================

RECORD_LINK_CLASS::RECORD_LINK_CLASS(UINT32 recordOffset)

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    downLinkOffsetM = DICOMDIR_ILLEGAL_OFFSET;
    horLinkOffsetM = DICOMDIR_ILLEGAL_OFFSET;
    inUseFlagM = 0;
    refCountM = 0;
    recordOffsetM = recordOffset;
    recordTypeM = DICOMDIR_RECORD_TYPE_UNKNOWN;
    messagesM_ptr = NULL;
}

//>>===========================================================================

RECORD_LINK_CLASS::~RECORD_LINK_CLASS()

//  DESCRIPTION     : Class destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    delete messagesM_ptr;
}

//>>===========================================================================

UINT32 RECORD_LINK_CLASS::GetDownLinkOffset()

//  DESCRIPTION     : Get the down link offset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return downLinkOffsetM;
}

//>>===========================================================================

void RECORD_LINK_CLASS::SetDownLinkOffset(UINT32 offset)

//  DESCRIPTION     : Set the down link offset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    downLinkOffsetM = offset;
}

//>>===========================================================================

UINT32 RECORD_LINK_CLASS::GetHorLinkOffset()

//  DESCRIPTION     : Get the horizontal link.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return horLinkOffsetM;
}

//>>===========================================================================

void RECORD_LINK_CLASS::SetHorLinkOffset(UINT32 offset)

//  DESCRIPTION     : Set the horizontal link.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    horLinkOffsetM = offset;
}

//>>===========================================================================

UINT32 RECORD_LINK_CLASS::GetRecordOffset()

//  DESCRIPTION     : Get the record offset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return recordOffsetM;
}

//>>===========================================================================

UINT16 RECORD_LINK_CLASS::GetInUseFlag()

//  DESCRIPTION     : Get the in-use flag.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return inUseFlagM;
}

//>>===========================================================================

void RECORD_LINK_CLASS::SetInUseFlag(UINT16 inUseFlag)

//  DESCRIPTION     : Set the in-use flag.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    inUseFlagM = inUseFlag;
}

//>>===========================================================================

UINT16 RECORD_LINK_CLASS::GetReferenceCount()

//  DESCRIPTION     : Get the reference count.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return refCountM;
}

//>>===========================================================================

void RECORD_LINK_CLASS::IncreaseReferenceCount()

//  DESCRIPTION     : Increment the reference count.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    refCountM++;
}

//>>===========================================================================

DICOMDIR_RECORD_TYPE_ENUM RECORD_LINK_CLASS::GetRecordType()

//  DESCRIPTION     : Get the record type.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return recordTypeM;
}

//>>===========================================================================

void RECORD_LINK_CLASS::SetRecordType(DICOMDIR_RECORD_TYPE_ENUM recordType)

//  DESCRIPTION     : Set the record type.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    recordTypeM = recordType;
}

//>>===========================================================================

bool RECORD_LINK_CLASS::CheckUnreferencedRecords()

//  DESCRIPTION     : Check the unreferenced records.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    bool result = true;

    if ((refCountM == 0) &&
        (inUseFlagM == 0))
    {
        if (messagesM_ptr)
		{
			messagesM_ptr->AddMessage(VAL_RULE_A_MEDIA_7, "This record is never referenced.");
		}
        result = false;
    }
    return result;
}

//>>===========================================================================

LOG_MESSAGE_CLASS *RECORD_LINK_CLASS::GetMessages()

//  DESCRIPTION     : Get the log messages.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (messagesM_ptr == NULL)
    {
        messagesM_ptr = new LOG_MESSAGE_CLASS();
    }

    return messagesM_ptr;
}

//>>===========================================================================

bool RECORD_LINK_CLASS::HasMessages()

//  DESCRIPTION     : Check if the record link has messages.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return (messagesM_ptr == NULL) ? false : true;
}
