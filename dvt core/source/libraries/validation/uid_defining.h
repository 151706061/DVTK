//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef UID_DEFINING_H
#define UID_DEFINING_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
typedef struct
{
    UINT32 uidTag;
    RECORD_IOD_TYPE_ENUM uidType;
} UID_DEFINITION_STRUCT;

#define UID_DEFINING_PATIENT_FILE           UID_DEFINING_CLASS::instancePatientFile()
#define UID_DEFINING_PATIENT_RECORD         UID_DEFINING_CLASS::instancePatientRecord()
#define UID_DEFINING_STUDY_FILE             UID_DEFINING_CLASS::instanceStudyFile()
#define UID_DEFINING_STUDY_RECORD           UID_DEFINING_CLASS::instanceStudyRecord()
#define UID_DEFINING_VISIT_FILE             UID_DEFINING_CLASS::instanceVisitFile()
#define UID_DEFINING_VISIT_RECORD           UID_DEFINING_CLASS::instanceVisitRecord()
#define UID_DEFINING_STUDY_COMPONENT_FILE   UID_DEFINING_CLASS::instanceStudyComponentFile()
#define UID_DEFINING_STUDY_COMPONENT_RECORD UID_DEFINING_CLASS::instanceStudyComponentRecord()
#define UID_DEFINING_XA_PRIVATE_FILE        UID_DEFINING_CLASS::instanceXaPrivateFile()
#define UID_DEFINING_XA_PRIVATE_RECORD      UID_DEFINING_CLASS::instanceXaPrivateRecord()
#define UID_DEFINING_IMAGE_FILE             UID_DEFINING_CLASS::instanceImageFile()
#define UID_DEFINING_IMAGE_RECORD           UID_DEFINING_CLASS::instanceImageRecord()
#define UID_DEFINING_SERIES_RECORD          UID_DEFINING_CLASS::instanceSeriesRecord()
#define UID_DEFINING_UNKNOWN_RECORD         UID_DEFINING_CLASS::instanceUnknownRecord()

typedef vector<UID_DEFINITION_STRUCT*> UID_DEFINITION_VECTOR;

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class BASE_VALUE_CLASS;

class UID_DEFINING_CLASS
{
    public:
        static UID_DEFINING_CLASS *instancePatientFile();
        static UID_DEFINING_CLASS *instancePatientRecord();
        static UID_DEFINING_CLASS *instanceStudyFile();
        static UID_DEFINING_CLASS *instanceStudyRecord();
        static UID_DEFINING_CLASS *instanceVisitFile();
        static UID_DEFINING_CLASS *instanceVisitRecord();
        static UID_DEFINING_CLASS *instanceStudyComponentFile();
        static UID_DEFINING_CLASS *instanceStudyComponentRecord();
        static UID_DEFINING_CLASS *instanceXaPrivateFile();
        static UID_DEFINING_CLASS *instanceXaPrivateRecord();
        static UID_DEFINING_CLASS *instanceImageFile();
        static UID_DEFINING_CLASS *instanceImageRecord();
        static UID_DEFINING_CLASS *instanceSeriesRecord();
        static UID_DEFINING_CLASS *instanceUnknownRecord();

        int GetNrUids();
        UID_DEFINITION_STRUCT *GetUidStruct(UINT);

    protected:
        UID_DEFINING_CLASS(vector<UID_DEFINITION_STRUCT*>);
        virtual ~UID_DEFINING_CLASS();

    private:
        UID_DEFINITION_VECTOR  uidDefinitionsM;

        static UID_DEFINING_CLASS *instancePatientFileM_ptr;
        static UID_DEFINING_CLASS *instancePatientRecordM_ptr;
        static UID_DEFINING_CLASS *instanceStudyFileM_ptr;
        static UID_DEFINING_CLASS *instanceStudyRecordM_ptr;
        static UID_DEFINING_CLASS *instanceVisitFileM_ptr;
        static UID_DEFINING_CLASS *instanceVisitRecordM_ptr;
        static UID_DEFINING_CLASS *instanceStudyComponentFileM_ptr;
        static UID_DEFINING_CLASS *instanceStudyComponentRecordM_ptr;
        static UID_DEFINING_CLASS *instanceXaPrivateFileM_ptr;
        static UID_DEFINING_CLASS *instanceXaPrivateRecordM_ptr;
        static UID_DEFINING_CLASS *instanceImageFileM_ptr;
        static UID_DEFINING_CLASS *instanceImageRecordM_ptr;
        static UID_DEFINING_CLASS *instanceSeriesRecordM_ptr;
        static UID_DEFINING_CLASS *instanceUnknownRecordM_ptr;
};

#endif /* UID_DEFINING_H */
