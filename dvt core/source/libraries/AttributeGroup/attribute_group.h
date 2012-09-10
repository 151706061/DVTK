// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef ATTRIBUTE_GROUP_H
#define ATTRIBUTE_GROUP_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"	// Global component interface
#include "Ilog.h"       // Log component interface
#include <string>
#include <vector>


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class ATTRIBUTE_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;


//*****************************************************************************
//  Type definitions
//*****************************************************************************
typedef vector<ATTRIBUTE_CLASS *>  ATTRIBUTE_VECTOR;


class ATTRIBUTE_GROUP_CLASS  
{
public:
                                ATTRIBUTE_GROUP_CLASS();
    virtual                    ~ATTRIBUTE_GROUP_CLASS();

			void				DeleteAttributes();

            string              GetName (void);
            DVT_STATUS          SetName (string name);

            DVT_STATUS          AddAttribute (ATTRIBUTE_CLASS * attribute);

            ATTRIBUTE_CLASS   * GetAttribute (unsigned short group,
                                              unsigned short element);
            ATTRIBUTE_CLASS   * GetMappedAttribute (unsigned short group,
                                                    unsigned short element);
            ATTRIBUTE_CLASS   * GetAttribute (int index);
            ATTRIBUTE_CLASS   * GetAttributeByTag (unsigned int tag);

            DVT_STATUS          DeleteAttribute (unsigned short group,
                                                 unsigned short element);
            DVT_STATUS          DeleteMappedAttribute (unsigned short group,
                                                       unsigned short element);
            DVT_STATUS          DeleteAttribute (int index);

            DVT_STATUS          DeleteAttributeIndex (unsigned short group,
                                                      unsigned short element);
            DVT_STATUS          DeleteMappedAttributeIndex (unsigned short group,
                                                      unsigned short element);
            DVT_STATUS          DeleteAttributeIndex (int index);

            int                 GetNrAttributes (void);

            bool                IsSorted (void);
            DVT_STATUS          SortAttributes();

            DVT_STATUS          Check (UINT32 flags,
                                       ATTRIBUTE_GROUP_CLASS * ref_ag,
                                       LOG_MESSAGE_CLASS *messages,
                                       SPECIFIC_CHARACTER_SET_CLASS *specific_character_set = NULL);

protected:
    ATTRIBUTE_VECTOR            attributesM;

private:
    string                      nameM;
};

#endif /* ATTRIBUTE_GROUP_H */
