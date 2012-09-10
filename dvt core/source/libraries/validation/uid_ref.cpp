//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Referring UID types Singleton class.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "uid_ref.h"
#include "valrules.h"
#include "Ilog.h"               // Logger component interface file.

//>>===========================================================================

UID_REF_CLASS::UID_REF_CLASS()

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    definingM.clear();
    referringM.clear();
    uidM = "";
}

//>>===========================================================================

UID_REF_CLASS::~UID_REF_CLASS(void)

//  DESCRIPTION     : Class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT i = 0; i < definingM.size(); i++)
    {
        delete definingM[i];
    }

    for (i = 0; i < referringM.size(); i++)
    {
        delete referringM[i];
    }

    definingM.clear();
    referringM.clear();
}

//>>===========================================================================

string UID_REF_CLASS::GetUid()

//  DESCRIPTION     : Return the UID of the reference object.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return uidM;
}

//>>===========================================================================

void UID_REF_CLASS::SetUid(string uid)

//  DESCRIPTION     : Set the uid of the UID reference object.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    uidM = uid;
}

//>>===========================================================================

void UID_REF_CLASS::AddDefining(ENTITY_NAME_STRUCT *entityName_ptr)

//  DESCRIPTION     : Adds a new defining entity to the UID reference object.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    definingM.push_back(entityName_ptr);
}

//>>===========================================================================

void UID_REF_CLASS::AddReferring(ENTITY_NAME_STRUCT *entityName_ptr)

//  DESCRIPTION     : Adds a new referring entity to the UID reference object.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    referringM.push_back(entityName_ptr);
}

//>>===========================================================================

void UID_REF_CLASS::Check(LOG_MESSAGE_CLASS *logMessage_ptr)

//  DESCRIPTION     : Check the uid references.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    char message[256];

    // Check if the UID has been defined only once.
    if (definingM.size() > 1)
    {
        sprintf(message, 
			"UID \"%s\" has been defined %d times - should only be once",
            uidM.c_str(), 
			definingM.size());
        logMessage_ptr->AddMessage(VAL_RULE_A_MEDIA_B, message);

        for (UINT i = 0; i < definingM.size(); i++)
        {
            logMessage_ptr->AddMessage(VAL_RULE_A_MEDIA_B, "UID defined in \"" + definingM[i]->name + "\"");
        }
    }

    // Check if a defining UID has been referenced.
    if ((definingM.size() > 0) &&
        (referringM.size() == 0))
    {
        for (UINT i = 0; i < definingM.size(); i++)
        {
            logMessage_ptr->AddMessage(VAL_RULE_A_MEDIA_C,
                "\"" + definingM[i]->name + "\" defines non-referenced \"" +
                iodTypes[definingM[i]->iodType] + "\" UID \"" +
                uidM + "\"");
        }
    }

    // Check if a UID that is referenced, actually has been defined.
    if ((referringM.size() > 0) &&
        (definingM.size() == 0))
    {
        for (UINT i = 0; i < referringM.size(); i++)
        {
            logMessage_ptr->AddMessage(VAL_RULE_A_MEDIA_C,
                "\"" + referringM[i]->name + "\" references non-defined \"" +
                iodTypes[referringM[i]->iodType] + "\" UID \"" +
                uidM + "\"");
        }
    }

    // Check if the iod types of the referring and defining UIDs are the same.
    if ((definingM.size() == 1) && 
        (referringM.size() > 0))
    {
        for (UINT i = 0; i < referringM.size(); i++)
        {
            if (definingM[0]->iodType != referringM[i]->iodType)
            {
                logMessage_ptr->AddMessage(VAL_RULE_A_MEDIA_C,
                    "UID \"" + uidM + "\" referenced by " + referringM[i]->name +
                    " as \"" + iodTypes[referringM[i]->iodType] +
                    "\"; defined by " + definingM[0]->name + " as \"" +
                    iodTypes[definingM[0]->iodType] + "\"");
            }
        }
    }
}
