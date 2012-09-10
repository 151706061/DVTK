//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Class for storing all messages of a single object.
//*****************************************************************************
#ifndef MESSAGE_H
#define MESSAGE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"          // global component interface


//*****************************************************************************
//  Type definitions
//*****************************************************************************
typedef vector<UINT32>    INDEX_VECTOR;
typedef vector<string>    STRING_VECTOR;
typedef vector<UINT32>    ID_VECTOR;

UINT32 GetNextMessageIndex();

//>>***************************************************************************
class LOG_MESSAGE_CLASS
//  DESCRIPTION     :
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
    public:
                            LOG_MESSAGE_CLASS();
        virtual             ~LOG_MESSAGE_CLASS();

        void        AddMessage      (UINT32 id, string message);
        int         GetNrMessages   (void);
        UINT32      GetIndex        (int i);
        UINT32      GetMessageId    (int i);
        string      GetMessage      (int i);
        bool        operator = (LOG_MESSAGE_CLASS &messages);
    protected:
    private:
        void        AddMessage      (UINT32 index, UINT32 id, string message);

        INDEX_VECTOR        indexesM;
        STRING_VECTOR       messagesM;
        ID_VECTOR           idsM;
};

#endif /* MESSAGE_H */
