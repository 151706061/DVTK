//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Query Retrieve Validator include file.
//*****************************************************************************
#ifndef QR_VALIDATOR_H
#define QR_VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"
#include "validator.h"
#include "val_object_results.h"     /* Remove when VAL_QR_OBJECT_RESULTS_CLASS is moved to a seperate file. */
#include "val_attribute_group.h"    /* Remove when VAL_QR_ATTRIBUTE_GROUP_CLASS is moved to a seperate file. */
#include "val_attribute.h"          /* Remove when VAL_QR_ATTRIBUTE_CLASS is moved to a seperate file. */

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_DATASET_CLASS;

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class QR_VALIDATOR_CLASS : public VALIDATOR_CLASS

//  DESCRIPTION     : QR Validator Class
//  NOTES           :
//<<***************************************************************************
{
    public:
		QR_VALIDATOR_CLASS();
        virtual ~QR_VALIDATOR_CLASS();

        void ValidateResults(VALIDATION_CONTROL_FLAG_ENUM);

        bool CreateResultsObject();

        void SetReqDataSetResultsFromDcm(DCM_DATASET_CLASS*);
};

//>>***************************************************************************

class VAL_QR_OBJECT_RESULTS_CLASS : public VAL_OBJECT_RESULTS_CLASS

//  DESCRIPTION     : QR Object Validation Results Class
//  NOTES           :
//<<***************************************************************************
{
    public:
		VAL_QR_OBJECT_RESULTS_CLASS();
        virtual ~VAL_QR_OBJECT_RESULTS_CLASS();

        DVT_STATUS ValidateAgainstReq(UINT32);
};

//>>***************************************************************************

class VAL_QR_ATTRIBUTE_CLASS : public VAL_ATTRIBUTE_CLASS

//  DESCRIPTION     : QR Attribute Validation Results Class
//  NOTES           :
//<<***************************************************************************
{
    public:
        VAL_QR_ATTRIBUTE_CLASS();
        virtual ~VAL_QR_ATTRIBUTE_CLASS();

        void SetReqAttribute(DCM_ATTRIBUTE_CLASS*);

        DCM_ATTRIBUTE_CLASS *GetReqAttribute();

        void CheckAgainstRequested();

    private:
        DCM_ATTRIBUTE_CLASS *reqAttributeM_ptr;
};

#endif /* QR_VALIDATOR_H */
