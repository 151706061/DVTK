// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//  Media Test Session class.

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "media_session.h"
#include "Idefinition.h"
#include "Idicom.h"
#include "Imedia.h"
#include "Ivalidation.h"

#include <time.h>

//>>===========================================================================

MEDIA_SESSION_CLASS::MEDIA_SESSION_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    // constructor activities
    ACTIVITY_LOG_CLASS *activityLogger_ptr = new ACTIVITY_LOG_CLASS();
    logMaskM = LOG_ERROR | LOG_WARNING | LOG_INFO;
    setLogger(activityLogger_ptr);
    setActivityReporter(NULL);
    setSerializer(NULL);

    runtimeSessionTypeM = ST_MEDIA;
    sessionTypeM = ST_MEDIA;
    sessionFileVersionM = 0;
    sessionTitleM = "";
    isOpenM = false;
    filenameM = "";
    setSessionId(1);
    manufacturerM = MANUFACTURER;
    modelNameM = MODEL_NAME;
    softwareVersionsM = IMPLEMENTATION_VERSION_NAME;
    applicationEntityNameM = APPLICATION_ENTITY_NAME;
    applicationEntityVersionM = APPLICATION_ENTITY_VERSION;
    testedByM = TESTED_BY;
    dateM = DATE;
    detailedValidationResultsM = true;
    summaryValidationResultsM = true;
    includeType3NotPresentInResultsM = false;
	validateReferencedFileM = true;

    setDefinitionFileRoot(".\\");
	dataDirectoryM = ".\\";
    resultsRootM = ".\\";
    appendToResultsFileM = false;
    counterM = 0;
    instanceIdM = 1;
}

//>>===========================================================================

MEDIA_SESSION_CLASS::~MEDIA_SESSION_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    // destructor activities
    // call the base class cleanup()
    BASE_SESSION_CLASS::cleanup();
    setLogger(NULL);
}

//>>===========================================================================

bool MEDIA_SESSION_CLASS::serialise(FILE *file_ptr)

//  DESCRIPTION     : Serialise the media session to file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    // write the file contents
    fprintf(file_ptr, "SESSION\n\n");
    fprintf(file_ptr, "SESSION-TYPE media\n");
    fprintf(file_ptr, "SESSION-FILE-VERSION %d\n", CURRENT_SESSION_FILE_VERSION);

    fprintf(file_ptr, "\n# Product Test Session Properties\n");
    fprintf(file_ptr, "SESSION-TITLE \"%s\"\n", getSessionTitle());
    fprintf(file_ptr, "SESSION-ID %03d\n", getSessionId());
    fprintf(file_ptr, "MANUFACTURER \"%s\"\n", getManufacturer());
    fprintf(file_ptr, "MODEL-NAME \"%s\"\n", getModelName());
    fprintf(file_ptr, "SOFTWARE-VERSIONS \"%s\"\n", getSoftwareVersions());
    fprintf(file_ptr, "APPLICATION-ENTITY-NAME \"%s\"\n", getApplicationEntityName());
    fprintf(file_ptr, "APPLICATION-ENTITY-VERSION \"%s\"\n", getApplicationEntityVersion());
    fprintf(file_ptr, "TESTED-BY \"%s\"\n", getTestedBy());
    fprintf(file_ptr, "DATE \"%s\"\n", getDate());

    fprintf(file_ptr, "\n# Test Session Properties\n");
    fprintf(file_ptr, "LOG-ERROR %s\n", (isLogLevel(LOG_ERROR)) ? "true" : "false");
    fprintf(file_ptr, "LOG-WARNING %s\n", (isLogLevel(LOG_WARNING)) ? "true" : "false");
    fprintf(file_ptr, "LOG-INFO %s\n", (isLogLevel(LOG_INFO)) ? "true" : "false");
    fprintf(file_ptr, "LOG-RELATION %s\n", (isLogLevel(LOG_IMAGE_RELATION)) ? "true" : "false");
    fprintf(file_ptr, "LOG-DEBUG %s\n", (isLogLevel(LOG_DEBUG)) ? "true" : "false");

    fprintf(file_ptr, "PDU-DUMP %s\n", (isLogLevel(LOG_PDU_BYTES)) ? "true" : "false");

    fprintf(file_ptr, "STORAGE-MODE ");
    switch(getStorageMode())
    {
    case SM_AS_MEDIA:
        fprintf(file_ptr, "as-media\n");
        break;
    case SM_AS_DATASET:
        fprintf(file_ptr, "as-dataset\n");
        break;
    case SM_NO_STORAGE:
    default:
        fprintf(file_ptr, "no-storage\n");
        break;
    }

    fprintf(file_ptr, "DETAILED-VALIDATION-RESULTS %s\n", (getDetailedValidationResults()) ? "true" : "false");
    fprintf(file_ptr, "SUMMARY-VALIDATION-RESULTS %s\n", (getSummaryValidationResults()) ? "true" : "false");
    fprintf(file_ptr, "INCLUDE-TYPE-3-NOTPRESENT-INRESULTS %s\n", (getIncludeType3NotPresentInResults()) ? "true" : "false");

    fprintf(file_ptr, "\n# Definitions\n");
    for (UINT i = 0; i < noDefinitionDirectories(); i++)
    {
        fprintf(file_ptr, "DEFINITION-DIRECTORY \"%s\"\n", getDefinitionDirectory(i).c_str());
    }

    for (UINT i = 0; i < noDefinitionFiles(); i++)
    {
        DEFINITION_FILE_CLASS *definitionFile_ptr = getDefinitionFile(i);
        fprintf(file_ptr, "DEFINITION \"%s\"\n", definitionFile_ptr->getFilename());
    }

	fprintf(file_ptr, "\n# Results\n");
    fprintf(file_ptr, "RESULTS-ROOT \"%s\"\n", getResultsRoot());
    fprintf(file_ptr, "APPEND-TO-RESULTS-FILE %s\n", (getAppendToResultsFile()) ? "true" : "false");

	fprintf(file_ptr, "\n# Data Directory\n");
	fprintf(file_ptr, "DATA-DIRECTORY \"%s\"\n", getDataDirectory());

    fprintf(file_ptr, "\n# DICOMScript Description Directory\n");
    fprintf(file_ptr, "DESCRIPTION-DIRECTORY \"%s\"\n", getDescriptionDirectory().c_str());

    fprintf(file_ptr, "\nENDSESSION\n");

    // return success
    return true;
}

//>>===========================================================================

bool MEDIA_SESSION_CLASS::beginMediaValidation()

//  DESCRIPTION     : Initialise the media validation.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{

    // enable logging
    if (loggerM_ptr)
    {
        // set the storage root as the data directory
        loggerM_ptr->setStorageRoot(getDataDirectory());

        // enable the base level logging
        UINT32 logMask = loggerM_ptr->getLogMask();
        logMask |= (LOG_NONE | LOG_SCRIPT | LOG_MEDIA_FILENAME);
        loggerM_ptr->setLogMask(logMask);
    }

    // return result
    return true;
}

//>>===========================================================================

bool MEDIA_SESSION_CLASS::validateMediaFile(string fileName)

//  DESCRIPTION     : Validate the given media file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (loggerM_ptr)
    {
        loggerM_ptr->text(LOG_SCRIPT, 1, "Reading media file: \"%s\"", fileName.c_str());
    }

    // read media - flags use session storage mode = true and log it = false
    FILE_DATASET_CLASS* fileDataset_ptr = this->readMedia(fileName, true, false);
    bool result = (fileDataset_ptr != NULL);

    if (result)
    {
        AE_SESSION_CLASS ae_session;

        ae_session.SetName (this->applicationEntityNameM);
        ae_session.SetVersion (this->applicationEntityVersionM);

        MEDIA_FILE_HEADER_CLASS *fileMetaInfo_ptr = fileDataset_ptr->getFileMetaInformation();

        // check if we are validating a DICOMDIR or ordinary media file
        DCM_DIR_DATASET_CLASS *dicomdirDataset_ptr = fileDataset_ptr->getDicomdirDataset();
        if (dicomdirDataset_ptr)
        {
			// get the number of directory records, it's only valid for DICOMDIR
			int noDirectoryRecords = getNumberOfDirectoryRecords(fileName);
			if (noDirectoryRecords == -1) return false; 

            // get the start time
            time_t tStart;
            time(&tStart);

            MEDIA_VALIDATOR_CLASS * validator = new MEDIA_VALIDATOR_CLASS(fileName);
            DEF_ATTRIBUTE_GROUP_CLASS *record = NULL;

            // serialize the fileDataset (as display)
            BASE_SERIALIZER *serializer_ptr = getSerializer();
            if (serializer_ptr)
            {
                serializer_ptr->SerializeDisplay(fileDataset_ptr);
            }

            if (loggerM_ptr) 
            {
                string startTime = ctime(&tStart);
                loggerM_ptr->text(LOG_INFO, 1, "VALIDATE DICOMDIR header to first Directory Record at time %s", startTime.c_str());
            }

            validator->SetLogger(loggerM_ptr);
            if (getIncludeType3NotPresentInResults())
            {
                validator->SetFlags(0);
            }
            else
            {
                validator->SetFlags(ATTR_FLAG_DO_NOT_INCLUDE_TYPE3);
            }

            // validate the DICOMDIR header - this pointer is freed by the fileDataset_ptr destructor
            validator->Validate(dicomdirDataset_ptr, NULL, fileMetaInfo_ptr, ALL, &ae_session, &record);

            // serialise the result
            validator->Serialize(serializerM_ptr);
            validator->CleanResults();

            if (loggerM_ptr) 
            {
                loggerM_ptr->text(LOG_INFO, 1, "VALIDATE %d Directory Records...", noDirectoryRecords);
            }

            // now start on the directory records
            DCM_ITEM_CLASS *item_ptr = NULL;
            //
            // Use zero-based indexer.
            //
            int index = 0;
            do {
                // get the next Directory Record
                item_ptr = fileDataset_ptr->getNextDicomdirRecord();
                if (item_ptr)
                {
                    if (loggerM_ptr) 
                    {
                        loggerM_ptr->text(
                            LOG_INFO, 
                            1, 
                            "VALIDATE Directory Record %d of %d, index %d", 
                            index+1, // One-based index
                            noDirectoryRecords,
                            index);
                    }
                    //
                    // set up a directoryRecord logger / serializer.
                    //
                    ACTIVITY_LOG_CLASS *dirRecordLogger_ptr = new ACTIVITY_LOG_CLASS();
                    BASE_SERIALIZER* dirRecordSerializer_ptr = NULL;
                    if (loggerM_ptr)
                    {
                        dirRecordLogger_ptr->setActivityReporter(loggerM_ptr->getActivityReporter());
                        UINT32 logMask = loggerM_ptr->getLogMask();
                        logMask |= (LOG_NONE | LOG_SCRIPT);
                        dirRecordLogger_ptr->setLogMask(logMask);
                        dirRecordLogger_ptr->setResultsRoot(loggerM_ptr->getResultsRoot());
                        dirRecordLogger_ptr->setStorageRoot(loggerM_ptr->getStorageRoot());
                    }
                    if (serializerM_ptr)
                    {
                        dirRecordSerializer_ptr = serializerM_ptr->CreateAndRegisterChildSerializer(::SerializerNodeType_DirectoryRecord);
                        dirRecordSerializer_ptr->StartSerializer();						
                        dirRecordLogger_ptr->setSerializer(dirRecordSerializer_ptr);
                    }

					// serialize the Record (as display)
					if (dirRecordSerializer_ptr)
					{
						dirRecordSerializer_ptr->SerializeDisplay(item_ptr);
					}

                    //
                    // validate the Directory Record.
                    //
                    result = validator->ValidateRecord(item_ptr, NULL, record, ALL, &ae_session, dirRecordLogger_ptr, dirRecordSerializer_ptr, index, storageModeM, validateReferencedFileM);
                    dirRecordSerializer_ptr->EndSerializer();
                    //
                    // clean up
                    //
                    serializerM_ptr->UnRegisterAndDestroyChildSerializer(dirRecordSerializer_ptr);
                    dirRecordSerializer_ptr = NULL;
                    delete dirRecordLogger_ptr;
                    validator->CleanResults();
                    delete item_ptr;

                    index++;
                }
            } while (item_ptr != NULL);

            if (loggerM_ptr)
            {
                loggerM_ptr->text(LOG_INFO, 1, "VALIDATE record links...");
            }
            validator->ValidateRecordReferences();

            if (loggerM_ptr)
            {
                loggerM_ptr->text(LOG_INFO, 1, "VALIDATE complete DICOMDIR validation...");
            }

            // serialise the result
            validator->Serialize(serializerM_ptr);

            // get the end time
            time_t tEnd;
            time(&tEnd);

            if (loggerM_ptr)
            {
                string endTime = ctime(&tEnd);
                loggerM_ptr->text(LOG_INFO, 1, "VALIDATE completed at time %s - media validation took %ld seconds", endTime.c_str(), tEnd - tStart);
            }
        }
        else
        {
            // try to get reference dataset from warehouse based on reference tag stored
            DCM_DATASET_CLASS *refDataset_ptr = NULL;
            DCM_DATASET_CLASS *dataset_ptr = fileDataset_ptr->getDataset();

            if ((dataset_ptr) &&
                (dataset_ptr->setIdentifierByTag(WAREHOUSE->getReferenceTag())))
            {
                // get the identifier
                string identifier = dataset_ptr->getIdentifier();

                // try to find matching object
                BASE_WAREHOUSE_ITEM_DATA_CLASS *wid_ptr = WAREHOUSE->retrieve(identifier, WID_DATASET);
                if (wid_ptr)
                {
                    refDataset_ptr = static_cast<DCM_DATASET_CLASS*>(wid_ptr);

                    if (loggerM_ptr)
                    {
                        loggerM_ptr->text(LOG_INFO, 1, "Reference Dataset with identifier \"%s\" found in Warehouse", identifier.c_str());
                    }
                }
            }

            // serialize the fileDataset (as display)
            BASE_SERIALIZER *serializer_ptr = getSerializer();
            if (serializer_ptr)
            {
                serializer_ptr->SerializeDisplay(fileDataset_ptr);
            }

            // validate the dataset - possibly using a reference dataset
            VALIDATION->setLogger(loggerM_ptr);
            VALIDATION->setStrictValidation(getStrictValidation());
            VALIDATION->setIncludeType3NotPresentInResults(getIncludeType3NotPresentInResults());
            result = VALIDATION->validate(fileDataset_ptr, refDataset_ptr, ALL, serializerM_ptr, &ae_session);
        }
    }

    delete fileDataset_ptr;

    // return result
    return result;
}

//>>===========================================================================

int MEDIA_SESSION_CLASS::getNumberOfDirectoryRecords( string fileName)

//  DESCRIPTION     : Get the number of directory records in a DICOMDIR file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           : We don't know if the given file is a DICOMDIR. If not
//                  : return 0 records. On error return -1.
//<<===========================================================================
{
    FILE_DATASET_CLASS* fileDataset_ptr = NULL;
    int noDirectoryRecords = -1;

	// read media - flags use session storage mode = false and log it = true
    fileDataset_ptr = this->readMedia(fileName, false, true);
    if (fileDataset_ptr != NULL)
    {
        noDirectoryRecords = 0;

        // check if we are validating a DICOMDIR or ordinary media file
        DCM_DIR_DATASET_CLASS *dicomdirDataset_ptr = fileDataset_ptr->getDicomdirDataset();
        if (dicomdirDataset_ptr)
        {
            // now start on the directory records
            DCM_ITEM_CLASS *item_ptr = NULL;
            do {
                // get the next Directory Record
                item_ptr = fileDataset_ptr->getNextDicomdirRecord();
                if (item_ptr)
                {
                    // delete the record
                    // - this should be done here to release the resources
                    delete item_ptr;

                    // increment record count
                    noDirectoryRecords++;
                }
            } while (item_ptr != NULL);
        }
    }

    delete fileDataset_ptr;

    // return number of Directory Records
    return noDirectoryRecords;
}

//>>===========================================================================

bool MEDIA_SESSION_CLASS::endMediaValidation()

//  DESCRIPTION     : Terminate the media validation.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    // return result
    return true;
}

//>>===========================================================================

bool MEDIA_SESSION_CLASS::validate(FILE_DATASET_CLASS* fileDataset_ptr, VALIDATION_CONTROL_FLAG_ENUM validationFlag)

//  DESCRIPTION     : Validate the the Media File Dataset.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
    if (fileDataset_ptr == NULL) return false;

    AE_SESSION_CLASS ae_session;

    ae_session.SetName (this->applicationEntityNameM);
    ae_session.SetVersion (this->applicationEntityVersionM);

    // validate the dataset
    VALIDATION->setStrictValidation(getStrictValidation());
    VALIDATION->setIncludeType3NotPresentInResults(getIncludeType3NotPresentInResults());
    bool result = VALIDATION->validate(fileDataset_ptr, NULL, validationFlag, serializerM_ptr, &ae_session);

    return result;
}

//>>===========================================================================

bool MEDIA_SESSION_CLASS::generateDICOMDIR(vector<string>* filenames)

//  DESCRIPTION     : Generate a DICOMDIR for the given file list.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	GENERATE_DICOMDIR_CLASS* pDICOMDIR = new GENERATE_DICOMDIR_CLASS(resultsRootM);

	// generate the DICOMDIR
	pDICOMDIR->setLogger(loggerM_ptr);
	bool result = pDICOMDIR->generateDICOMDIR(filenames);

	// clean up
	delete pDICOMDIR;

	return result;
}

//>>===========================================================================

void MEDIA_SESSION_CLASS::setValidateReferencedFile(bool flag)

//  DESCRIPTION     : Set flag indicating that the files referenced by a DICOMDIR
//					: should be validated with the DICOMDIR.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	validateReferencedFileM = flag;
}

//>>===========================================================================

bool MEDIA_SESSION_CLASS::getValidateReferencedFile()

//  DESCRIPTION     : Get flag indicating that the files referenced by a DICOMDIR
//					: should be validated with the DICOMDIR.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	return validateReferencedFileM;
}
