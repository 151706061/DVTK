//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DCM_COMMAND_H
#define DCM_COMMAND_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "dcm_attribute_group.h"

//>>***************************************************************************

class DCM_COMMAND_CLASS : public DCM_ATTRIBUTE_GROUP_CLASS

    //  DESCRIPTION     : DICOM command class.
    //  INVARIANT       :
    //  NOTES           :
    //<<***************************************************************************
{
private:
    static bool isRequest(DIMSE_CMD_ENUM);
    DIMSE_CMD_ENUM	commandIdM;
    bool			isRequestM;	// request = true & response = false
public:
    DCM_COMMAND_CLASS();
    DCM_COMMAND_CLASS(DIMSE_CMD_ENUM);
    DCM_COMMAND_CLASS(DIMSE_CMD_ENUM, string);
    ~DCM_COMMAND_CLASS();
    DIMSE_CMD_ENUM getCommandId();
    void setCommandId(DIMSE_CMD_ENUM commandId);
    bool getIsRequest();
    void setIsRequest(bool flag);
    bool encode(DATA_TF_CLASS&);
    bool updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*);
	DCM_COMMAND_CLASS *cloneAttributes();
};

#endif /* DCM_COMMAND_H */
