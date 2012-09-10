//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef VALIDATOR_H
#define VALIDATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_DICOM_OBJECT_CLASS;
class DEF_MODULE_CLASS;
class DEF_ATTRIBUTE_GROUP_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;
class VAL_ATTRIBUTE_GROUP_CLASS;
class VAL_OBJECT_RESULTS_CLASS;
class VAL_ATTRIBUTE_CLASS;
class DEF_ATTRIBUTE_CLASS;
class DCM_ATTRIBUTE_CLASS;
class DCM_COMMAND_CLASS;
class DCM_DATASET_CLASS;
class DEF_DATASET_CLASS;
class DEF_MACRO_CLASS;
class DCM_VALUE_SQ_CLASS;
class VAL_VALUE_SQ_CLASS;
class VALUE_SQ_CLASS;
class BASE_SERIALIZER;
class AE_SESSION_CLASS;
class LOG_CLASS;
class SPECIFIC_CHARACTER_SET_CLASS;


//>>***************************************************************************
class VALIDATOR_CLASS
//  DESCRIPTION     : Validator class
//  NOTES           :
//<<***************************************************************************
{
    public:
		VALIDATOR_CLASS();
        virtual ~VALIDATOR_CLASS();

        virtual bool CreateResultsObject();

        void CreateModuleResultsFromDef(DEF_DICOM_OBJECT_CLASS*,
								DCM_ATTRIBUTE_GROUP_CLASS*);

        bool CreateCommandResultsFromDef(DCM_COMMAND_CLASS*);

        bool CreateDatasetResultsFromDef(DCM_COMMAND_CLASS*,
								DCM_DATASET_CLASS*,
								AE_SESSION_CLASS*);

        void SetModuleResultsFromDcm(DCM_ATTRIBUTE_GROUP_CLASS*,
								bool);

        virtual void ValidateResults(VALIDATION_CONTROL_FLAG_ENUM);

        RESULTS_TYPE GetResultsType();

        virtual void Serialize(BASE_SERIALIZER*);

        void SetFlags(UINT);

        void SetLogger(LOG_CLASS*);

    protected:
        void CreateAttributeGroupResultsFromDef(DEF_ATTRIBUTE_GROUP_CLASS*,
										DCM_ATTRIBUTE_GROUP_CLASS*,
                                        VAL_ATTRIBUTE_GROUP_CLASS*,
                                        bool); 

        void CreateSQResultsFromDef(VALUE_SQ_CLASS*,
							DCM_VALUE_SQ_CLASS*,
                            VAL_VALUE_SQ_CLASS*,
                            bool);

        void CreateMacroResultsFromDef(DEF_MACRO_CLASS*,
							DCM_ATTRIBUTE_GROUP_CLASS*,
                            VAL_ATTRIBUTE_GROUP_CLASS*,
                            bool);

        void CreateAttributeResultsFromDef(DEF_ATTRIBUTE_CLASS*,
							DCM_ATTRIBUTE_GROUP_CLASS*,
                            VAL_ATTRIBUTE_CLASS*,
                            bool);

        void CreateValueResultsFromDef(DEF_ATTRIBUTE_CLASS*,
							VAL_ATTRIBUTE_CLASS*);

        void SetAttributeGroupResultsFromDcm(DCM_ATTRIBUTE_GROUP_CLASS*,
							VAL_ATTRIBUTE_GROUP_CLASS*,
                            bool);

        void SetValueResultsFromDcm(DCM_ATTRIBUTE_CLASS*,
							VAL_ATTRIBUTE_CLASS*,
                            bool);

        void SetSQResultsFromDcm(DCM_ATTRIBUTE_CLASS*,
							VAL_ATTRIBUTE_CLASS*,
                            bool);

        void LogSystemDefinitions();

    private:
        void UpdateSpecificCharacterSet(DCM_ATTRIBUTE_GROUP_CLASS*);

    protected:
        RESULTS_TYPE					resultsTypeM;
        VAL_OBJECT_RESULTS_CLASS		*objectResultsM_ptr;
        UINT							flagsM;
        SPECIFIC_CHARACTER_SET_CLASS	*specificCharacterSetM_ptr;
		DEF_DATASET_CLASS				*defDatasetM_ptr;

    private:
        LOG_CLASS						*loggerM_ptr;
};

#endif /* VALIDATOR_H */
