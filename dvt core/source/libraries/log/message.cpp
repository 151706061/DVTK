//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     : This file contains the implementation of all functions
//                    for the LOG_MESSAGE_CLASS. This class is used for storing
//                    all messages of a specific object (can be attribute,
//                    item, global messages, ...).
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "message.h"

static UINT32 messageIndex = 1;

//>>===========================================================================

UINT32 GetNextMessageIndex()
//  DESCRIPTION     : Get the next message index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return messageIndex++;
}

//>>===========================================================================
LOG_MESSAGE_CLASS::LOG_MESSAGE_CLASS()
//  DESCRIPTION     : Class constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
}

//>>===========================================================================
LOG_MESSAGE_CLASS::~LOG_MESSAGE_CLASS()
//  DESCRIPTION     : Class destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    idsM.clear();
    messagesM.clear();
}

//>>===========================================================================
void
LOG_MESSAGE_CLASS::AddMessage (UINT32 id, string message)
//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           : The message is automatically assigned an index here.
//<<===========================================================================
{
    messagesM.push_back (message);
    idsM.push_back (id);
    indexesM.push_back (GetNextMessageIndex());
}

//>>===========================================================================
void
LOG_MESSAGE_CLASS::AddMessage (UINT32 index, UINT32 id, string message)
//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    messagesM.push_back (message);
    idsM.push_back (id);
    indexesM.push_back (index);
}

//>>===========================================================================
int
LOG_MESSAGE_CLASS::GetNrMessages (void)
//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return (messagesM.size());
}

//>>===========================================================================
UINT32
LOG_MESSAGE_CLASS::GetIndex (int i)
//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (i >= 0);

	if (messagesM.size() > (unsigned int) i)
        return (indexesM[i]);
    else
	    return (0);
}

//>>===========================================================================
UINT32
LOG_MESSAGE_CLASS::GetMessageId (int i)
//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (i >= 0);

	if (messagesM.size() > (unsigned int) i)
        return (idsM[i]);
    else
	    return (0);
}

//>>===========================================================================
string
LOG_MESSAGE_CLASS::GetMessage (int i)
//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	assert (i >= 0);

	if (messagesM.size() > (unsigned int) i)
        return (messagesM[i]);
    else
	    return (NULL);
}

//>>===========================================================================
bool
LOG_MESSAGE_CLASS::operator = (LOG_MESSAGE_CLASS &messages)
//  DESCRIPTION     :
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (int i=0 ; i< messages.GetNrMessages () ; i++)
    {
        int index = messages.GetIndex (i);
        int id = messages.GetMessageId (i);
        string message = messages.GetMessage (i);
        AddMessage (index, id, message);
    }
    return (true);
}
