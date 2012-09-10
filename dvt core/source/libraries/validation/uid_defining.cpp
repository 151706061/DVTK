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
#include "uid_defining.h"

//*****************************************************************************
//  INTERNAL DECLARATIONS
//*****************************************************************************

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instancePatientFileM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instancePatientRecordM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyFileM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyRecordM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceVisitFileM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceVisitRecordM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyComponentFileM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyComponentRecordM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceXaPrivateFileM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceXaPrivateRecordM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceImageFileM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceImageRecordM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceSeriesRecordM_ptr = NULL;
UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceUnknownRecordM_ptr = NULL;


//>>===========================================================================

int UID_DEFINING_CLASS::GetNrUids()

//  DESCRIPTION     : Returns the number of UID structs in the requested
//                    uid definition object.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    return uidDefinitionsM.size();
}

//>>===========================================================================

UID_DEFINITION_STRUCT *UID_DEFINING_CLASS::GetUidStruct(UINT index)

//  DESCRIPTION     : Returns the UID struct with the requested index.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    UID_DEFINITION_STRUCT *uidDefinition_ptr = NULL;
    if (index < uidDefinitionsM.size())
    {
        uidDefinition_ptr = uidDefinitionsM[index];
    }

    return uidDefinition_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS::UID_DEFINING_CLASS(vector<UID_DEFINITION_STRUCT*> uids)

//  DESCRIPTION     : Class constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    for (UINT i = 0; i < uids.size(); i++)
    {
        uidDefinitionsM.push_back(uids[i]);
    }
}

//>>===========================================================================

UID_DEFINING_CLASS::~UID_DEFINING_CLASS()

//  DESCRIPTION     : Class destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instancePatientFile()

//  DESCRIPTION     : Get patient file instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instancePatientFileM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_SOP_INSTANCE_UID;
        uid_ptr->uidType = RECORD_IOD_TYPE_PATIENT;
        uids.push_back(uid_ptr);

		instancePatientFileM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instancePatientFileM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instancePatientRecord()

//  DESCRIPTION     : Get patient record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instancePatientRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_UNDEFINED;
        uid_ptr->uidType = RECORD_IOD_TYPE_PATIENT;
        uids.push_back(uid_ptr);

        instancePatientRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instancePatientRecordM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyFile()

//  DESCRIPTION     : Get study file instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceStudyFileM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_SOP_INSTANCE_UID;
        uid_ptr->uidType = RECORD_IOD_TYPE_STUDY;
        uids.push_back(uid_ptr);

        instanceStudyFileM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceStudyFileM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyRecord()

//  DESCRIPTION     :  Get study record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceStudyRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_UNDEFINED;
        uid_ptr->uidType = RECORD_IOD_TYPE_STUDY;
        uids.push_back(uid_ptr);

        instanceStudyRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceStudyRecordM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceVisitFile()

//  DESCRIPTION     : Get visit file instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceVisitFileM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_SOP_INSTANCE_UID;
        uid_ptr->uidType = RECORD_IOD_TYPE_VISIT;
        uids.push_back(uid_ptr);

        instanceVisitFileM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceVisitFileM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceVisitRecord()

//  DESCRIPTION     :  Get visit record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceVisitRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_UNDEFINED;
        uid_ptr->uidType = RECORD_IOD_TYPE_VISIT;
        uids.push_back(uid_ptr);

		instanceVisitRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceVisitRecordM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyComponentFile()

//  DESCRIPTION     : Get study component file instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceStudyComponentFileM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_SOP_INSTANCE_UID;
        uid_ptr->uidType = RECORD_IOD_TYPE_STUDY_COMPONENT;
        uids.push_back(uid_ptr);

		instanceStudyComponentFileM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceStudyComponentFileM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceStudyComponentRecord()

//  DESCRIPTION     :  Get study component record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceStudyComponentRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_UNDEFINED;
        uid_ptr->uidType = RECORD_IOD_TYPE_STUDY_COMPONENT;
        uids.push_back(uid_ptr);

		instanceStudyComponentRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceStudyComponentRecordM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceXaPrivateFile()

//  DESCRIPTION     : Get xa private file instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceXaPrivateFileM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_SOP_INSTANCE_UID;
        uid_ptr->uidType = RECORD_IOD_TYPE_PRIVATE;
        uids.push_back(uid_ptr);

		instanceXaPrivateFileM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceXaPrivateFileM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceXaPrivateRecord()

//  DESCRIPTION     :  Get xa private record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceXaPrivateRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_UNDEFINED;
        uid_ptr->uidType = RECORD_IOD_TYPE_PRIVATE;
        uids.push_back(uid_ptr);

		instanceXaPrivateRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceXaPrivateRecordM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceImageFile()

//  DESCRIPTION     : Get image file instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceImageFileM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_SOP_INSTANCE_UID;
        uid_ptr->uidType = RECORD_IOD_TYPE_IMAGE;
        uids.push_back(uid_ptr);

		instanceImageFileM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceImageFileM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceImageRecord()

//  DESCRIPTION     :  Get image record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceImageRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_UNDEFINED;
        uid_ptr->uidType = RECORD_IOD_TYPE_IMAGE;
        uids.push_back(uid_ptr);

		instanceImageRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceImageRecordM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceSeriesRecord()

//  DESCRIPTION     :  Get series record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceSeriesRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT *uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_SERIES_INSTANCE_UID;
        uid_ptr->uidType = RECORD_IOD_TYPE_SERIES;
        uids.push_back(uid_ptr);

		instanceSeriesRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceSeriesRecordM_ptr;
}

//>>===========================================================================

UID_DEFINING_CLASS *UID_DEFINING_CLASS::instanceUnknownRecord()

//  DESCRIPTION     : Get unknown record instance.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// is this the first time ?
	if (instanceUnknownRecordM_ptr == NULL) 
	{
        vector<UID_DEFINITION_STRUCT*> uids;
        UID_DEFINITION_STRUCT * uid_ptr = new UID_DEFINITION_STRUCT();
        uid_ptr->uidTag = TAG_UNDEFINED;
        uid_ptr->uidType = RECORD_IOD_TYPE_PRIVATE;
        uids.push_back(uid_ptr);

		instanceUnknownRecordM_ptr = new UID_DEFINING_CLASS(uids);
	}

	return instanceUnknownRecordM_ptr;
}
