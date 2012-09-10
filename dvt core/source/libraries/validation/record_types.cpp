//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :   Record types Singleton class.
//                      Holds the knowledge of the supported DICOMDIR records.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "record_types.h"

#include "IAttributeGroup.h"    // Attribute Group interface file

//*****************************************************************************
//  INTERNAL DECLARATIONS
//*****************************************************************************

RECORD_TYPES_CLASS * RECORD_TYPES_CLASS::instanceM_ptr = NULL;


//>>===========================================================================

RECORD_TYPES_CLASS::RECORD_TYPES_CLASS()

//  DESCRIPTION     : Class constructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    FillRecordTypes();
}

//>>===========================================================================

RECORD_TYPES_CLASS::~RECORD_TYPES_CLASS()

//  DESCRIPTION     : Class destructor
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
}

//>>===========================================================================

RECORD_TYPES_CLASS *RECORD_TYPES_CLASS::instance()

//  DESCRIPTION     : Create instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceM_ptr == NULL) 
	{
		instanceM_ptr = new RECORD_TYPES_CLASS();
	}

	return instanceM_ptr;
}

//>>===========================================================================

void RECORD_TYPES_CLASS::FillRecordTypes()

//  DESCRIPTION     : This function creates the array of all supported record
//                    types within DVT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    // This must be the first entry.
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_ROOT,             "ROOT",             "PATIENT\nTOPIC\nPRIVATE",                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_PATIENT,          "PATIENT",          "STUDY\nPRIVATE",                                                                                                                                                                               TAG_PATIENT_ID,                          TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_STUDY,            "STUDY",            "SERIES\nVISIT\nRESULTS\nSTUDY COMPONENT\nPRIVATE",                                                                                                                                             TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, TAG_STUDY_INSTANCE_UID,                  false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_SERIES,           "SERIES",           "IMAGE\nOVERLAY\nMODALITY LUT\nVOI LUT\nCURVE\nSTORED PRINT\nRT DOSE\nRT STRUCTURE SET\nRT PLAN\nRT TREAT RECORD\nPRESENTATION\nWAVEFORM\nSR DOCUMENT\nKEY OBJECT DOC\nPRIVATE",                TAG_SERIES_INSTANCE_UID,                 TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_IMAGE,            "IMAGE",            "PRIVATE",                                                                                                                                                                                      TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_OVERLAY,          "OVERLAY",          "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_MODALITY_LUT,     "MODALITY LUT",     "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_VOI_LUT,          "VOI LUT",          "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_CURVE,            "CURVE",            "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_STORED_PRINT,     "STORED PRINT",     "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_RT_DOSE,          "RT DOSE",          "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_RT_STRUCTURE_SET, "RT STRUCTURE SET", "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_RT_PLAN,          "RT PLAN",          "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_RT_TREAT_RECORD,  "RT TREAT RECORD",  "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_PRESENTATION,     "PRESENTATION",     "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_WAVEFORM,         "WAVEFORM",         "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_SR_DOCUMENT,      "SR DOCUMENT",      "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_KEY_OBJECT_DOC,   "KEY OBJECT DOC",   "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_TOPIC,            "TOPIC",            "STUDY\nSERIES\nIMAGE\nOVERLAY\nMODALITY LUT\nVOI LUT\nCURVE\nSTORED PRINT\nRT DOSE\nRT STRUCTURE SET\nRT PLAN\nRT TREAT RECORD\nPRESENTATION\nWAVEFORM\nSR DOCUMENT\nKEY OBJECT DOC\nPRIVATE", TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_VISIT,            "VISIT",            "PRIVATE",                                                                                                                                                                                      TAG_ADMISSION_ID,                        TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_RESULTS,          "RESULTS",          "INTERPRETATION\nPRIVATE",                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_INTERPRETATION,   "INTERPRETATION",   "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_STUDY_COMPONENT,  "STUDY COMPONENT",  "PRIVATE",                                                                                                                                                                                      TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, TAG_STUDY_DESCRIPTION,                   false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_PRIVATE,          "PRIVATE",          "",                                                                                                                                                                                             TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, false));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_PRINT_QUEUE,      "PRINT QUEUE",      "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           true));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_FILM_SESSION,     "FILM SESSION",     "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           true));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_FILM_BOX,         "FILM BOX",         "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           true));
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_IMAGE_BOX,        "IMAGE BOX",        "PRIVATE",                                                                                                                                                                                      TAG_UNDEFINED,                           TAG_UNDEFINED,                           true));
    // This must be the last entry.
    recordTypesM.push_back(CreateRecordType(DICOMDIR_RECORD_TYPE_UNKNOWN,          "UNRECOGNIZED",     "",                                                                                                                                                                                             TAG_UNDEFINED,                           TAG_UNDEFINED,                           false));
}

//>>===========================================================================

RECORD_TYPE_STRUCT *RECORD_TYPES_CLASS::CreateRecordType(DICOMDIR_RECORD_TYPE_ENUM recordType,
                                     string recordTypeName,
                                     string lowerList,
                                     UINT32 recordIdentifyingTag1,
                                     UINT32 recordIdentifyingTag2,
                                     bool isRetired)

//  DESCRIPTION     : This function creates the array of all supported record
//                    types within DVT.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    RECORD_TYPE_STRUCT *record_ptr = new RECORD_TYPE_STRUCT();

    record_ptr->recordType = recordType;
    record_ptr->recordTypeName = recordTypeName;
    record_ptr->lowerList = lowerList;
    record_ptr->recordIdentifyingTag1 = recordIdentifyingTag1;
    record_ptr->recordIdentifyingTag2 = recordIdentifyingTag2;
    record_ptr->isRetired = isRetired;

    return record_ptr;
}

//>>===========================================================================

UINT RECORD_TYPES_CLASS::GetRecordTypeIndex(DICOMDIR_RECORD_TYPE_ENUM recordType)

//  DESCRIPTION     : Returns the record type of the DICOMDIR record.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT i = 0; i < recordTypesM.size(); i++)
    {
        if (recordTypesM[i]->recordType == recordType)
        {
            return i;
        }
    }

    // This should never happen actually.
    // All DICOMDIR_RECORD_TYPE_ENUM values are available in the
    // record_typesM array.
    return 0xffffffff;
}

//>>===========================================================================

DICOMDIR_RECORD_TYPE_ENUM RECORD_TYPES_CLASS::GetRecordTypeIndexWithRecordName(BASE_VALUE_CLASS *value_ptr)

//  DESCRIPTION     : Get record type index with the given name.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    unsigned int index = 0;
    while (index < recordTypesM.size())
    {
        DVT_STATUS status = value_ptr->Compare(recordTypesM[index]->recordTypeName);
        if ((status == MSG_OK) ||
            (status == MSG_EQUAL))
        {
            return recordTypesM[index]->recordType;
        }

        index++;
    }

    return DICOMDIR_RECORD_TYPE_UNKNOWN;
}

//>>===========================================================================

string RECORD_TYPES_CLASS::GetLowerListOfRecordTypeWithIdx(UINT index)

//  DESCRIPTION     : Get lower list of record type with the given index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    string lowerList = "";
    if (index < recordTypesM.size())
    {
        lowerList = recordTypesM[index]->lowerList;
    }

    return lowerList;
}

//>>===========================================================================

string RECORD_TYPES_CLASS::GetRecordTypeNameOfRecordTypeWithIdx(UINT index)

//  DESCRIPTION     : Get record type name of record type with the given index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    string recordTypeName = "";
    if (index < recordTypesM.size())
    {
        recordTypeName = recordTypesM[index]->recordTypeName;
    }

    return recordTypeName;
}

//>>===========================================================================

UINT32 RECORD_TYPES_CLASS::GetIdentifyingTag1(UINT index)

//  DESCRIPTION     : Get identifying tag 1.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    UINT32 recordIdentifyingTag = TAG_UNDEFINED;
    if (index < recordTypesM.size())
    {
        recordIdentifyingTag = recordTypesM[index]->recordIdentifyingTag1;
    }

    return recordIdentifyingTag;
}

//>>===========================================================================

UINT32 RECORD_TYPES_CLASS::GetIdentifyingTag2(UINT index)

//  DESCRIPTION     : Get identifying tag 2.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    UINT32 recordIdentifyingTag = TAG_UNDEFINED;
    if (index < recordTypesM.size())
    {
        recordIdentifyingTag = recordTypesM[index]->recordIdentifyingTag2;
    }

    return recordIdentifyingTag;
}

//>>===========================================================================

bool RECORD_TYPES_CLASS::IsRecordRetired(UINT index)

//  DESCRIPTION     : Check if record is retired.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    bool isRetired = true;
    if (index < recordTypesM.size())
    {
        isRetired = recordTypesM[index]->isRetired;
    }

    return isRetired;
}

//>>===========================================================================

bool RECORD_TYPES_CLASS::IsChildAllowedInParent(UINT parentIndex, UINT childIndex)

//  DESCRIPTION     : Returns true when the record type with index 'parent_idx'
//                    allows to have the record_type_name of the record type
//                    with index 'childIndex' as a child record type.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    // Do a check on the limits of both index parameters.
    if ((parentIndex >= recordTypesM.size()) || 
        (childIndex >= recordTypesM.size()))
    {
        return false;
    }

    if (strstr(recordTypesM[parentIndex]->lowerList.c_str(),
			recordTypesM[childIndex]->recordTypeName.c_str()) == 0)
    {
        return false;
    }

    return true;
}
