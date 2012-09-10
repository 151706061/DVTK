//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	File based DICOM Dataset class.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "generateDICOMDIR.h"
#include "Idefinition.h"		// Definition component interface
#include "Inetwork.h"			// Network component interface

//>>===========================================================================

PATIENT_INFO_CLASS::PATIENT_INFO_CLASS(string id, string name, string Identifier)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	patientId = id;
	patientName = name;
	identifier = Identifier;
}
	
//>>===========================================================================

PATIENT_INFO_CLASS::~PATIENT_INFO_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// clean up any study data
	while (studyData.getSize())
	{
		delete studyData[0];
		studyData.removeAt(0);
	}
}

//>>===========================================================================

STUDY_INFO_CLASS *PATIENT_INFO_CLASS::searchStudy(string instanceUid)

//  DESCRIPTION     : Search the Patient for Study Data with an instance
//					: uid matching that given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// search study data
	for (UINT i = 0; i < studyData.getSize(); i++)
	{
		STUDY_INFO_CLASS *studyData_ptr = studyData[i];

		// check for match
		if ((studyData_ptr != NULL) && 
			(instanceUid == studyData_ptr->getInstanceUid()))
		{
			// match found - return it
			return studyData_ptr;
		}
	}

	// no match found
	return NULL;
}

//>>===========================================================================

bool PATIENT_INFO_CLASS::operator = (PATIENT_INFO_CLASS& sourceData)

//  DESCRIPTION     : Operator assignment - for assigning this patient to the 
//					  same value as the source.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	patientId = sourceData.getPatientId();
	patientName = sourceData.getPatientName();
	identifier = sourceData.getIdentifier();
	for (UINT i = 0; i < sourceData.noStudies(); i++)
	{
		studyData[i] = sourceData.getStudyData(i);
	}
	return true;
}

//>>===========================================================================

STUDY_INFO_CLASS::STUDY_INFO_CLASS(string Uid, string Id,string Date,string Time,string Descr,string AccessionNr,string Identifier)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	instanceUid = Uid;
	studyId = Id;
	studyDate = Date;
	studyTime = Time;
	studyDescr = Descr;
	accessionNr = AccessionNr;
	identifier = Identifier;
}
	
//>>===========================================================================

STUDY_INFO_CLASS::~STUDY_INFO_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// clean up any series data
	while (seriesData.getSize()) 
	{
		delete seriesData[0];
		seriesData.removeAt(0);
	}
}

//>>===========================================================================

SERIES_INFO_CLASS *STUDY_INFO_CLASS::searchSeries(string instanceUid)

//  DESCRIPTION     : Search Study for Series Data with an instance uid
//					: matching that given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// search series data
	for (UINT i = 0; i < seriesData.getSize(); i++)
	{
		SERIES_INFO_CLASS *seriesData_ptr = seriesData[i];

		// check for match
		if ((seriesData_ptr != NULL) &&
			(instanceUid == seriesData_ptr->getInstanceUid())) 
		{
			// match found - return it
			return seriesData_ptr;
		}
	}

	// no match found
	return NULL;
}

//>>===========================================================================

bool STUDY_INFO_CLASS::operator = (STUDY_INFO_CLASS& sourceData)

//  DESCRIPTION     : Operator assignment - for assigning this study to the 
//					  same value as the source.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	instanceUid = sourceData.getInstanceUid();
	studyId = sourceData.getStudyId();
	studyDate = sourceData.getStudyDate();
	studyTime = sourceData.getStudyTime();
	studyDescr = sourceData.getStudyDescr();
	accessionNr = sourceData.getAccessionNr();
	identifier = sourceData.getIdentifier();
	for (UINT i = 0; i < sourceData.noSeries(); i++)
	{
		seriesData[i] = sourceData.getSeriesData(i);
	}
	return true;
}

//>>===========================================================================

SERIES_INFO_CLASS::SERIES_INFO_CLASS(string InstanceUid, string Modality,INT32 Nr,string Identifier)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	instanceUid = InstanceUid;
	modality = Modality;
	seriesNr = Nr;
	identifier = Identifier;
}
	
//>>===========================================================================

SERIES_INFO_CLASS::~SERIES_INFO_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// clean up any sop instance data
	while (sopInstanceData.getSize())
	{
		delete sopInstanceData[0];
		sopInstanceData.removeAt(0);
	}
}

//>>===========================================================================

IMAGE_INFO_CLASS *SERIES_INFO_CLASS::searchImage(string instanceUid)

//  DESCRIPTION     : Serach the series for SOP Instance Data with an instance uid
//					: matching that given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// search sop instance data
	for (UINT i = 0; i < sopInstanceData.getSize(); i++) 
	{
		IMAGE_INFO_CLASS *sopInstanceData_ptr = sopInstanceData[i];

		// check for match
		if ((sopInstanceData_ptr != NULL) &&
			(instanceUid == sopInstanceData_ptr->getInstanceUid()))
		{
			// match found - return it
			return sopInstanceData_ptr;
		}
	}

	// no match found
	return NULL;
}

//>>===========================================================================

PRESENTATION_STATE_INFO_CLASS *SERIES_INFO_CLASS::searchPS(INT32 instanceNr)

//  DESCRIPTION     : Serach the series for PS Data with an instance number
//					: matching that given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// search sop instance data
	for (UINT i = 0; i < presentationStateData.getSize(); i++) 
	{
		PRESENTATION_STATE_INFO_CLASS *psData_ptr = presentationStateData[i];

		char base[100];
		_itoa(instanceNr,base,10);
		string instanceStr = base;

		// check for match
		if ((psData_ptr != NULL) &&
			(instanceStr == psData_ptr->getInstanceNr()))
		{
			// match found - return it
			return psData_ptr;
		}
	}

	// no match found
	return NULL;
}

//>>===========================================================================

bool SERIES_INFO_CLASS::operator = (SERIES_INFO_CLASS& sourceData)

//  DESCRIPTION     : Operator assignment - for assigning this series to the 
//					  same value as the source.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	instanceUid = sourceData.getInstanceUid();
	modality = sourceData.getModality();
	seriesNr = atoi(sourceData.getSeriesNr().c_str());
	identifier = sourceData.getIdentifier();
	for (UINT i = 0; i < sourceData.noSopInstances(); i++)
	{
		sopInstanceData[i] = sourceData.getSopInstanceData(i);
	}
	return true;
}

//>>===========================================================================

PRESENTATION_STATE_INFO_CLASS::PRESENTATION_STATE_INFO_CLASS(INT32 instanceNumber, string uid, string sopClassUid, string sopPClassInstanceUid, string date, string time, string lable, string Identifier)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	// Initialise class elements
	instanceNr = instanceNumber;
	refSeriesInstanceUid = uid;
	refSOPClassUid = sopClassUid;
	refSOPClassInstanceUid = sopPClassInstanceUid;
	psCreationDate = date;
	psCreationTime = time;
	contentLable = lable;
	identifier = Identifier;
	count = 1;
}

//>>===========================================================================

PRESENTATION_STATE_INFO_CLASS::~PRESENTATION_STATE_INFO_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// cleanup
}

//>>===========================================================================

bool PRESENTATION_STATE_INFO_CLASS::operator = (PRESENTATION_STATE_INFO_CLASS& sourceData)

//  DESCRIPTION     : Operator assignment - for assigning this image to the 
//					  same value as the source.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	instanceNr = atoi(sourceData.getInstanceNr().c_str());
	refSeriesInstanceUid = sourceData.getRefSeriesInstanceUid();
	refSOPClassUid = sourceData.getRefSOPClassUid();
	refSOPClassInstanceUid = sourceData.getRefSOPClassInstanceUid();
	psCreationDate = sourceData.getPSCreationDate();
	psCreationTime = sourceData.getPSCreationTime();
	contentLable = sourceData.getContentLable();
	identifier = sourceData.getIdentifier();
	count = sourceData.count;
	return true;
}

//>>===========================================================================

IMAGE_INFO_CLASS::IMAGE_INFO_CLASS(string uid, string fileId, string sopClassUid, string sopPClassInstanceUid, string tsUid, INT32 instanceNumber,string Identifier)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// constructor activities
	// Initialise class elements
	instanceUid = uid;
	refFileId = fileId;
	refSOPClassUid = sopClassUid;
	refSOPClassInstanceUid = sopPClassInstanceUid;
	refTSUid = tsUid;
	instanceNr = instanceNumber;
	identifier = Identifier;
	count = 1;
}

//>>===========================================================================

IMAGE_INFO_CLASS::~IMAGE_INFO_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	// cleanup
}

//>>===========================================================================

bool IMAGE_INFO_CLASS::operator = (IMAGE_INFO_CLASS& sourceData)

//  DESCRIPTION     : Operator assignment - for assigning this image to the 
//					  same value as the source.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	instanceUid = sourceData.getInstanceUid();
	refFileId = sourceData.getRefFileId();
	refSOPClassUid = sourceData.getRefSOPClassUid();
	refSOPClassInstanceUid = sourceData.getRefSOPClassInstanceUid();
	refTSUid = sourceData.getRefTSUid();
	instanceNr = atoi(sourceData.getInstanceNr().c_str());
	identifier = sourceData.getIdentifier();
	count = sourceData.count;
	return true;
}

//>>===========================================================================

GENERATE_DICOMDIR_CLASS::GENERATE_DICOMDIR_CLASS(string resultRootDirectory)

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{	
	dicomdirPathM = resultRootDirectory;
	isPSPresent = true;
}

//>>===========================================================================

GENERATE_DICOMDIR_CLASS::~GENERATE_DICOMDIR_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// destructor activities    
}

//>>===========================================================================

bool GENERATE_DICOMDIR_CLASS::generateDICOMDIR(vector<string>* filenames)

//  DESCRIPTION     : Set the media filename.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{ 
	bool result = false;

	if (loggerM_ptr)
    {
    	loggerM_ptr->text(LOG_INFO, 2, "Reading the DCM files.");
    }

	// Read DCM files we only do this once
	vector<string>::iterator it;
	for (it = filenames->begin(); it < filenames->end(); ++it)
	{
        // successful reads are stored in filedatasetsM
		result = readDCMFiles(*it);
	}

	if (loggerM_ptr)
    {
    	loggerM_ptr->text(LOG_INFO, 2, "Building the DICOMDIR directory structure in the Warehouse.");
    }

	if(result)
	{
		// Analyze DCM files and fill all necessary information
		vector<FMI_DATASET_STRUCT>::iterator datasetIt;
		for (datasetIt = filedatasetsM.begin(); datasetIt < filedatasetsM.end(); ++datasetIt)
		{
			// successful reads are stored in filedatasetsM
			analyseStorageDataset((*datasetIt).dat_ptr, (*datasetIt).filename, (*datasetIt).transferSyntax);
		}
	}
	else
	{
		if (loggerM_ptr)
		{
    		loggerM_ptr->text(LOG_ERROR, 1, "Error in reading DCM files.");
		}
		return false;
	}	

	// Provide identifier to each record(dataset)
	char base[10];
	string baseString;
	for (UINT i = 0; i < patientData.getSize(); i++)
	{
		PATIENT_INFO_CLASS* patientData_ptr = patientData[i];

		if (patientData_ptr != NULL)
		{
			_itoa(i,base,10);
			baseString = base;
			string identifier = "PATIENT" + baseString;
			patientData[i]->setIdentifier(identifier);
		}

		for (UINT j = 0; j < patientData_ptr->noStudies(); j++)
		{
			STUDY_INFO_CLASS* studyData_ptr = patientData_ptr->getStudyData(j);

			if (studyData_ptr != NULL)
			{
				_itoa((i*10)+j,base,10);
				baseString = base;
				string identifier = "STUDY" + baseString;
				studyData_ptr->setIdentifier(identifier);
			}

			for (UINT k = 0; k < studyData_ptr->noSeries(); k++)
			{
				SERIES_INFO_CLASS* seriesData_ptr = studyData_ptr->getSeriesData(k);

				if (seriesData_ptr != NULL)
				{
					_itoa((((i*10)+j)*10)+k,base,10);
					baseString = base;
					string identifier = "SERIES" + baseString;
					seriesData_ptr->setIdentifier(identifier);
				}

				for (UINT l = 0; l < seriesData_ptr->noSopInstances(); l++)
				{
					IMAGE_INFO_CLASS* imageData_ptr = seriesData_ptr->getSopInstanceData(l);

					if (imageData_ptr != NULL)
					{
						_itoa((((((i*10)+j)*10)+k)*10)+l,base,10);
						baseString = base;
						string identifier = "IMAGE" + baseString;
						imageData_ptr->setIdentifier(identifier);
					}

					// Check if Presentation Data is present, if it presents provide 
					// identifier to PS record.
					if(isPSPresent)
					{
						PRESENTATION_STATE_INFO_CLASS* psData_ptr = seriesData_ptr->getPSData(l);
						if (psData_ptr != NULL)
						{
							_itoa((((((i*10)+j)*10)+k)*10)+l,base,10);
							baseString = base;
							string identifier = "PRESENTATION" + baseString;
							psData_ptr->setIdentifier(identifier);
						}
					}
				}
			}
		}
	}

	result = CreateDICOMObjects();
	
	if(result)
	{
		result = CreateAndStoreRecords();
	}
	else
	{
		if (loggerM_ptr)
		{
    		loggerM_ptr->text(LOG_ERROR, 1, "Error in creating DICOMDIR Header, FMI.");
		}
		return false;
	}

	if(result)
	{
		result = CreateAndStoreDirectorySequenceObject();
	}
	else
	{
		if (loggerM_ptr)
		{
    		loggerM_ptr->text(LOG_ERROR, 1, "Error in creating Records.");
		}
		return false;
	}

	if(result)
	{
		result = writeDICOMDIR((dicomdirPathM + "DICOMDIR"));
	}
	else
	{
		if (loggerM_ptr)
		{
    		loggerM_ptr->text(LOG_ERROR, 1, "Error in creating Directory Sequence Object.");
		}
		return false;
	}

	if(result)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_INFO, 2, "DICOMDIR created sucessfully and it is available in directory: \"%s\".", dicomdirPathM.c_str());
		}
	}

	return result;
}

//>>===========================================================================

bool GENERATE_DICOMDIR_CLASS::readDCMFiles(string filename)

//  DESCRIPTION     : Reads a file, and add pointers to the fmi and
//                    the dataset to the filedataset list. 
//                    Optionally the file is validated
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = false;

	// create local file object 
	FILE_DATASET_CLASS fileDataset(filename);

	// cascade the logger 
	fileDataset.setLogger(loggerM_ptr);

	// log action
    if (loggerM_ptr)
    {
    	loggerM_ptr->text(LOG_INFO, 2, "Parsing the DCM file: \"%s\"", filename.c_str());
    }

	result = fileDataset.read();
	if (!result)
	{
		if (loggerM_ptr)
		{
    		loggerM_ptr->text(LOG_ERROR, 1, "Can't read file %s ", filename.c_str());
		}
		return false;
	}

	FMI_DATASET_STRUCT file_info;
	file_info.filename = filename;

	// copy the FMI pointer from the file dataset
	file_info.fmi_ptr = fileDataset.getFileMetaInformation();
	fileDataset.clearFileMetaInformationPtr();

	// copy the dataset pointer from the file dataset
	// - remove any Dataset Trailing Padding
	DCM_DATASET_CLASS	*dataset_ptr = fileDataset.getDataset();
	if (dataset_ptr)
	{
		dataset_ptr->removeTrailingPadding();
	}

	file_info.dat_ptr = dataset_ptr;
	fileDataset.clearDatasetPtr();
	file_info.transferSyntax = fileDataset.getTransferSyntax();

	if (file_info.dat_ptr == NULL)
	{
		if (loggerM_ptr)
		{
    		loggerM_ptr->text(LOG_ERROR, 1, "File %s does not contain a dataset.", filename.c_str());
		}
		result = false;
	}
	else
	{
		filedatasetsM.push_back(file_info);

		// log action
		if (loggerM_ptr)
		{
    		loggerM_ptr->text(LOG_INFO, 1, "Media file dataset was encoded with Transfer Syntax: \"%s\"", file_info.transferSyntax.c_str());
		}
	}

	// return result
	return result;
}

//>>===========================================================================

PATIENT_INFO_CLASS *GENERATE_DICOMDIR_CLASS::searchPatient(string id, string name)

//  DESCRIPTION     : Search the Patient List for Patient Data with an id (and
//					: optional name) matching that given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	// search patient data
	for (UINT i = 0; i < patientData.getSize(); i++)
	{
		PATIENT_INFO_CLASS *patientData_ptr = patientData[i];

		// check for match
		if ((patientData_ptr != NULL) &&
			(id == patientData_ptr->getPatientId()))
		{
			// check if name defined
			if ((name.length() != 0) &&
				(patientData_ptr->getPatientName().length() != 0) &&
				(name != patientData_ptr->getPatientName()))
			{
				// patient ids match - but names do not
				// - issue warning
				if (loggerM_ptr)
				{
					loggerM_ptr->text(LOG_WARNING, 1,"(0010,0010) Patient Name mis-match during Object Relationship Analysis: \"%s\" and \"%s\"", name.c_str(), patientData_ptr->getPatientName().c_str());
				}
			}

			// match on Patient Id
			return patientData_ptr;
		}
	}

	// no match found
	return NULL;
}

//>>===========================================================================

void GENERATE_DICOMDIR_CLASS::analyseStorageDataset(DCM_DATASET_CLASS* dataset_ptr, string fileName, string transferSyntax)

//  DESCRIPTION     : Analyse the DCM dataset. The identification information 
//					: will be used to check whether any relationship exists
//					: between this and previous/future objects.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	PATIENT_INFO_CLASS *patientData_ptr;
	STUDY_INFO_CLASS *studyData_ptr;
	SERIES_INFO_CLASS *seriesData_ptr;
	PRESENTATION_STATE_INFO_CLASS *psData_ptr;
	IMAGE_INFO_CLASS *sopInstanceData_ptr;
	
	// check that the appriopriate attributes are available
	// Patient ID
	string patientId;
	dataset_ptr->getLOValue(TAG_PATIENT_ID, patientId);
	if (patientId.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0010,0020) Patient ID is not available");
		}
	}

	// Patient Name
	string patientName;
	dataset_ptr->getPNValue(TAG_PATIENTS_NAME, patientName);
	if (patientName.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0010,0010) Patient Name is not available");
		}
	}

	// Study Instance UID
	string studyInstanceUid;
	dataset_ptr->getUIValue(TAG_STUDY_INSTANCE_UID, studyInstanceUid);
	if (studyInstanceUid.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0020,000D) Study Instance UID is not available");
		}
	}

	// Study ID
	string studyId;
	dataset_ptr->getSHValue(TAG_STUDY_ID, studyId);
	if (studyId.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0020,0010) Study ID is not available");
		}
	}

	// Study Description
	string studyDescr;
	dataset_ptr->getLOValue(TAG_STUDY_DESCRIPTION, studyDescr);
	if (studyDescr.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,1030) Study Description is not available");
		}
	}

	// Accession Nr
	string accessionNr;
	dataset_ptr->getSHValue(TAG_ACCESSION_NUMBER, accessionNr);
	if (accessionNr.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,0050) Accession Nr is not available");
		}
	}

	// Study Date
	char study_date[DA_LENGTH + 1];
	study_date[0] = NULLCHAR;
	dataset_ptr->getDAValue(TAG_STUDY_DATE, (BYTE*)study_date, DA_LENGTH);
	string studyDate = study_date;
	if (studyDate.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,0020) Study Date is not available");
		}
	}

	// Study Time
	char study_time[TM_LENGTH + 1];
	study_time[0] = NULLCHAR;
	dataset_ptr->getTMValue(TAG_STUDY_TIME, (BYTE*)study_time, TM_LENGTH);
	string studyTime = study_time;
	if (studyTime.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,0030) Study Time is not available");
		}
	}

	// Series Instance UID
	string seriesInstanceUid;
	dataset_ptr->getUIValue(TAG_SERIES_INSTANCE_UID, seriesInstanceUid);
	if (seriesInstanceUid.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0020,000E) Series Instance UID is not available");
		}
	}

	// Series Modality
	string seriesModality;
	dataset_ptr->getCSValue(TAG_MODALITY, seriesModality);
	if (seriesModality.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,0060) Series Modality is not available");
		}
	}

	// Series Nr
	INT32 seriesNr = 0;
	dataset_ptr->getISValue(TAG_SERIES_NUMBER, &seriesNr);
	
	// Object SOP Instance UID
	string sopInstanceUid;
	dataset_ptr->getUIValue(TAG_SOP_INSTANCE_UID, sopInstanceUid);
	if (sopInstanceUid.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,0018) SOP (Image) Instance UID is not available");
		}
	}

	// Instance Nr
	INT32 instanceNr = -1;
	dataset_ptr->getISValue(TAG_IMAGE_NUMBER, &instanceNr);
	if (instanceNr == -1)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0020,0013) Instance number is not available");
		}
	}

	// Content Lable
	string contLable;
	dataset_ptr->getCSValue(TAG_CONTENT_LABLE, contLable);
	if (contLable.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0070,0080) Content Lable is not available");
		}
	}

	// Reference SOP Class UID
	string refSopUid;
	dataset_ptr->getUIValue(TAG_REFERENCED_SOP_CLASS_UID, refSopUid);
	if (refSopUid.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,1150) Reference SOP Class UID is not available");
		}
	}

	// Reference SOP Instance UID
	string refSopInstUid;
	dataset_ptr->getUIValue(TAG_REFERENCED_SOP_INSTANCE_UID, refSopInstUid);
	if (refSopInstUid.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0008,1155) Reference SOP Instance UID is not available");
		}
	}

	// PS Creation Date
	char ps_date[DA_LENGTH + 1];
	ps_date[0] = NULLCHAR;
	dataset_ptr->getDAValue(TAG_PS_CREATION_DATE, (BYTE*)ps_date, DA_LENGTH);
	string psDate = ps_date;
	if (psDate.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0070,0082) PS Creation Date is not available");
		}
	}

	// PS Creation Time
	char ps_time[TM_LENGTH + 1];
	ps_time[0] = NULLCHAR;
	dataset_ptr->getTMValue(TAG_PS_CREATION_TIME, (BYTE*)ps_time, TM_LENGTH);
	string psTime = ps_time;
	if (psTime.length() == 0)
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_IMAGE_RELATION, 1, "(0070,0083) PS Creation Time is not available");
		}
	}

	if((contLable.length() == 0) && 
	   (refSopUid.length() == 0) &&
	   (refSopInstUid.length() == 0) &&
	   (psDate.length() == 0) &&
	   (psTime.length() == 0))
	{
		isPSPresent = false;
	}

	// Reference File ID
	int prefix_pos = dicomdirPathM.length();
	int fileLength = fileName.length() - 1;
	string refFileId;

	// Extract the Reference file name
    if ((prefix_pos > 0) && (fileLength > prefix_pos))
    {
        refFileId = fileName.substr (prefix_pos, fileLength);
    }

	// SOP Class UID
	string sopClassInstanceUid;
	dataset_ptr->getUIValue(TAG_SOP_CLASS_UID, sopClassInstanceUid);

	// SOP Instance UID
	string refSopInstanceUid;
	dataset_ptr->getUIValue(TAG_SOP_INSTANCE_UID, refSopInstanceUid);

	// Reference Transfer Syntax UID
	string refTSUid = transferSyntax;
	
	// now check if a matching Patient has already been set up
	if ((patientData_ptr = searchPatient(patientId, patientName)) == NULL)
	{
		// create SOP Instance Data
		sopInstanceData_ptr = new IMAGE_INFO_CLASS(sopInstanceUid, refFileId, sopClassInstanceUid, refSopInstanceUid, refTSUid, instanceNr, "");

		// create Presentation Data if it present
		if(isPSPresent)
		{
			psData_ptr = new PRESENTATION_STATE_INFO_CLASS(instanceNr, seriesInstanceUid, refSopUid, refSopInstUid, psDate, psTime, contLable, "");
		}

		// create Series Data
		seriesData_ptr = new SERIES_INFO_CLASS(seriesInstanceUid, seriesModality,seriesNr, "");
		seriesData_ptr->addSopInstanceData(sopInstanceData_ptr);
		if(psData_ptr != NULL)
		{
			seriesData_ptr->addPSData(psData_ptr);
		}
	
		// create Study Data
		studyData_ptr = new STUDY_INFO_CLASS(studyInstanceUid, studyId, studyDate, studyTime, studyDescr, accessionNr,"");
		studyData_ptr->addSeriesData(seriesData_ptr);

		// create Patient Data
		patientData_ptr = new PATIENT_INFO_CLASS(patientId, patientName, "");
		patientData_ptr->addStudyData(studyData_ptr);

		// add Patient Data
		patientData.add(patientData_ptr);
	}
	else 
	{
		// patient already exists - check for study
		if ((studyData_ptr = patientData_ptr->searchStudy(studyInstanceUid)) == NULL) 
		{
			// create SOP Instance Data
			sopInstanceData_ptr = new IMAGE_INFO_CLASS(sopInstanceUid, refFileId, sopClassInstanceUid, refSopInstanceUid, refTSUid, instanceNr,"");

			// create Presentation Data if it present
			if(isPSPresent)
			{
				psData_ptr = new PRESENTATION_STATE_INFO_CLASS(instanceNr, seriesInstanceUid, refSopUid, refSopInstUid, psDate, psTime, contLable, "");
			}

			// create Series Data
			seriesData_ptr = new SERIES_INFO_CLASS(seriesInstanceUid, seriesModality,seriesNr, "");
			seriesData_ptr->addSopInstanceData(sopInstanceData_ptr);
			if(psData_ptr != NULL)
			{
				seriesData_ptr->addPSData(psData_ptr);
			}
		
			// create Study Data
			studyData_ptr = new STUDY_INFO_CLASS(studyInstanceUid, studyId, studyDate, studyTime, studyDescr, accessionNr,"");
			studyData_ptr->addSeriesData(seriesData_ptr);

			// add Study Data
			patientData_ptr->addStudyData(studyData_ptr);
		}
		else 
		{
			// study already exists - check for series
			if ((seriesData_ptr = studyData_ptr->searchSeries(seriesInstanceUid)) == NULL)
			{
				// create SOP Instance Data
				sopInstanceData_ptr = new IMAGE_INFO_CLASS(sopInstanceUid, refFileId, sopClassInstanceUid, refSopInstanceUid, refTSUid, instanceNr,"");

				// create Presentation Data if it present
				if(isPSPresent)
				{
					psData_ptr = new PRESENTATION_STATE_INFO_CLASS(instanceNr, seriesInstanceUid, refSopUid, refSopInstUid, psDate, psTime, contLable, "");
				}

				// create Series Data
				seriesData_ptr = new SERIES_INFO_CLASS(seriesInstanceUid, seriesModality,seriesNr, "");
				seriesData_ptr->addSopInstanceData(sopInstanceData_ptr);
				if(psData_ptr != NULL)
				{
					seriesData_ptr->addPSData(psData_ptr);
				}

				// add Series Data
				studyData_ptr->addSeriesData(seriesData_ptr);
			}
			else 
			{
				// series already exists - check for sop instance
				if ((sopInstanceData_ptr = seriesData_ptr->searchImage(sopInstanceUid)) == NULL)
				{
					// create SOP Instance Data
					sopInstanceData_ptr = new IMAGE_INFO_CLASS(sopInstanceUid, refFileId, sopClassInstanceUid, refSopInstanceUid, refTSUid, instanceNr,"");

					// Search the Presentation Data for new image
					if ((isPSPresent) && ((psData_ptr = seriesData_ptr->searchPS(instanceNr)) == NULL))
					{
						psData_ptr = new PRESENTATION_STATE_INFO_CLASS(instanceNr, seriesInstanceUid, refSopUid, refSopInstUid, psDate, psTime, contLable, "");
					}

					// add SOP Instance Data
					seriesData_ptr->addSopInstanceData(sopInstanceData_ptr);
					if(psData_ptr != NULL)
					{
						seriesData_ptr->addPSData(psData_ptr);
					}
				}
				else 
				{
					// sop instance exists - we've got the same one again!!
					sopInstanceData_ptr->incrementCount();
				}
			}
		}
	}
}

//>>===========================================================================

bool GENERATE_DICOMDIR_CLASS::CreateDICOMObjects()

//  DESCRIPTION     : Create DICOMDIR Head, File Meta & Tail DICOM and store them
//					: into warehouse.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	//Create DICOMDIR Header & store into Data Warehouse.
	if (loggerM_ptr)
    {
    	loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the DICOMDIR Header in Warehouse.");
    }

	FILEHEAD_CLASS* fileHead_ptr = new FILEHEAD_CLASS();

	// cascade the logger
	if (fileHead_ptr)
	{
		fileHead_ptr->setLogger(loggerM_ptr);
		fileHead_ptr->setPreambleValue("");
		fileHead_ptr->setPrefix("DICM");
		UID_CLASS uid(EXPLICIT_VR_LITTLE_ENDIAN);
		fileHead_ptr->setTransferSyntaxUid(uid);
	}

	// try to store the DICOMDIR Header in the warehouse
	bool result = WAREHOUSE->store("", fileHead_ptr);
	if (result)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 2, "CREATE %s (in Data Warehouse)", WIDName(fileHead_ptr->getWidType()));
		}
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s in Data Warehouse", WIDName(fileHead_ptr->getWidType()));
		}
	}

	//Create File Meta Info object & store into Data Warehouse.
	if (loggerM_ptr)
    {
    	loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the DICOMDIR directory meta information in Warehouse.");
    }

	DCM_DATASET_CLASS* fileMetaInfo_ptr = new DCM_DATASET_CLASS();

	// cascade the logger
	if (fileMetaInfo_ptr)
	{
		fileMetaInfo_ptr->setLogger(loggerM_ptr);
		fileMetaInfo_ptr->setDefineGroupLengths(true);
		fileMetaInfo_ptr->setOBValue(TAG_FILE_META_INFORMATION_VERSION, 0, 0, 1);
		fileMetaInfo_ptr->setUIValue(TAG_MEDIA_STORAGE_SOP_CLASS_UID, MEDIA_STORAGE_DIRECTORY_SOP_CLASS_UID);
		fileMetaInfo_ptr->setUIValue(TAG_MEDIA_STORAGE_SOP_INSTANCE_UID, "1.3.46.670589.17.997345933.692.1.4.3");
		fileMetaInfo_ptr->setUIValue(TAG_TRANSFER_SYNTAX_UID, EXPLICIT_VR_LITTLE_ENDIAN);
		fileMetaInfo_ptr->setUIValue(TAG_IMPLEMENTATION_CLASS_UID, IMPLEMENTATION_CLASS_UID);
		fileMetaInfo_ptr->setSHValue(TAG_IMPLEMENTATION_VERSION_NAME, IMPLEMENTATION_VERSION_NAME);
		fileMetaInfo_ptr->setAEValue(TAG_SOURCE_APPLICATION_ENTITY_TITLE,"DVT");
		fileMetaInfo_ptr->setWidType(WID_META_INFO);
	}

	// try to store the File Meta Info object in the warehouse
	result = WAREHOUSE->store(fileMetaIdentifier, fileMetaInfo_ptr);
	if (result)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 2, "CREATE %s %s (in Data Warehouse)", WIDName(fileMetaInfo_ptr->getWidType()), fileMetaIdentifier);
		}
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s %s in Data Warehouse", WIDName(fileMetaInfo_ptr->getWidType()), fileMetaIdentifier);
		}
	}

	//Create DICOMDIR tail & store into Data Warehouse.
	if (loggerM_ptr)
    {
    	loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the DICOMDIR Tail in Warehouse.");
    }
	FILETAIL_CLASS* fileTail_ptr = new FILETAIL_CLASS();

	// cascade the logger
	if (fileTail_ptr)
	{
		fileTail_ptr->setLogger(loggerM_ptr);
		fileTail_ptr->setTrailingPadding(true);
		fileTail_ptr->setSectorSize(2048);
		fileTail_ptr->setPaddingValue((BYTE)0);
	}

	// try to store the DICOMDIR Tail in the warehouse
	result = WAREHOUSE->store("", fileTail_ptr);
	if (result)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 2, "CREATE %s (in Data Warehouse)", WIDName(fileTail_ptr->getWidType()));
		}
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s in Data Warehouse", WIDName(fileTail_ptr->getWidType()));
		}
	}

	return result;
}

//>>===========================================================================

bool GENERATE_DICOMDIR_CLASS::CreateAndStoreRecords()

//  DESCRIPTION     : Create DICOMDIR records and store them into warehouse.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	//Create File Meta Info object & store into Data Warehouse.	
	bool result = false;

	if (patientData.getSize() != 0)
	{
		for (UINT i = 0; i < patientData.getSize(); i++)
		{
			if (loggerM_ptr)
			{
    			loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the patient records with unique identifier in Warehouse.");
			}
			DCM_ITEM_CLASS* patientRecord_ptr = new DCM_ITEM_CLASS();

			// cascade the logger
			patientRecord_ptr->setLogger(loggerM_ptr);
			patientRecord_ptr->setIdentifier(patientData[i]->getIdentifier());
			patientRecord_ptr->setPopulateWithAttributes(false);

			if((1 == patientData.getSize()) || ((i+1) == patientData.getSize()))
			{
				patientRecord_ptr->setULValue(TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD, 0x00000000);
			}
			else
			{
				DCM_ATTRIBUTE_CLASS *nextRecordAttribute_ptr = getULAttribute(patientData[i+1]->getIdentifier(),
																TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD);
				patientRecord_ptr->addAttribute(nextRecordAttribute_ptr);
			}			

			patientRecord_ptr->setUSValue(TAG_RECORD_IN_USE_FLAG, 0xFFFF);
			patientRecord_ptr->setCSValue(TAG_DIRECTORY_RECORD_TYPE, "PATIENT");
			patientRecord_ptr->setPNValue(TAG_PATIENTS_NAME, patientData[i]->getPatientName());
			patientRecord_ptr->setLOValue(TAG_PATIENT_ID, patientData[i]->getPatientId());

			for (UINT j = 0; j < patientData[i]->noStudies(); j++)
			{
				if (loggerM_ptr)
				{
    				loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the study records with unique identifier in Warehouse.");
				}
				DCM_ITEM_CLASS* studyRecord_ptr = new DCM_ITEM_CLASS();
				STUDY_INFO_CLASS *studyData_ptr = patientData[i]->getStudyData(j);

				if((j == 0) && (studyData_ptr != NULL))
				{
					DCM_ATTRIBUTE_CLASS *lowerRecordAttribute_ptr = getULAttribute(studyData_ptr->getIdentifier(),
																TAG_OFFSET_OF_REFERENCED_LOWER_LEVEL_DIRECTORY_ENTITY);
					patientRecord_ptr->addAttribute(lowerRecordAttribute_ptr);
				}

				// cascade the logger
				studyRecord_ptr->setLogger(loggerM_ptr);
				studyRecord_ptr->setIdentifier(studyData_ptr->getIdentifier());
				studyRecord_ptr->setPopulateWithAttributes(false);

				if((1 == patientData[i]->noStudies()) || ((j+1) == patientData[i]->noStudies()))
				{
					studyRecord_ptr->setULValue(TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD, 0x00000000);
				}
				else
				{
					DCM_ATTRIBUTE_CLASS *nextRecordAttribute_ptr = getULAttribute(patientData[i]->getStudyData(j+1)->getIdentifier(),
																TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD);
					studyRecord_ptr->addAttribute(nextRecordAttribute_ptr);
				}

				studyRecord_ptr->setUSValue(TAG_RECORD_IN_USE_FLAG, 0xFFFF);
				studyRecord_ptr->setCSValue(TAG_DIRECTORY_RECORD_TYPE, "STUDY");
				studyRecord_ptr->setUIValue(TAG_STUDY_INSTANCE_UID, studyData_ptr->getInstanceUid());
				studyRecord_ptr->setSHValue(TAG_STUDY_ID, studyData_ptr->getStudyId());
				studyRecord_ptr->setLOValue(TAG_STUDY_DESCRIPTION, studyData_ptr->getStudyDescr());
				studyRecord_ptr->setSHValue(TAG_ACCESSION_NUMBER, studyData_ptr->getAccessionNr());
				studyRecord_ptr->setDAValue(TAG_STUDY_DATE, studyData_ptr->getStudyDate());
				studyRecord_ptr->setTMValue(TAG_STUDY_TIME, studyData_ptr->getStudyTime());

				for (UINT k = 0; k < studyData_ptr->noSeries(); k++)
				{
					if (loggerM_ptr)
					{
    					loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the series records with unique identifier in Warehouse.");
					}
					DCM_ITEM_CLASS* seriesRecord_ptr = new DCM_ITEM_CLASS();
					SERIES_INFO_CLASS *seriesData_ptr = studyData_ptr->getSeriesData(k);

					if((k == 0) && (seriesData_ptr != NULL))
					{
						DCM_ATTRIBUTE_CLASS *lowerRecordAttribute_ptr = getULAttribute(seriesData_ptr->getIdentifier(),
																TAG_OFFSET_OF_REFERENCED_LOWER_LEVEL_DIRECTORY_ENTITY);
						studyRecord_ptr->addAttribute(lowerRecordAttribute_ptr);
					}

					// cascade the logger
					seriesRecord_ptr->setLogger(loggerM_ptr);
					seriesRecord_ptr->setIdentifier(seriesData_ptr->getIdentifier());
					seriesRecord_ptr->setPopulateWithAttributes(false);

					if((1 == studyData_ptr->noSeries()) || ((k+1) == studyData_ptr->noSeries()))
					{
						seriesRecord_ptr->setULValue(TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD, 0x00000000);
					}
					else
					{
						DCM_ATTRIBUTE_CLASS *nextRecordAttribute_ptr = getULAttribute(studyData_ptr->getSeriesData(k+1)->getIdentifier(),
																TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD);
						seriesRecord_ptr->addAttribute(nextRecordAttribute_ptr);
					}

					seriesRecord_ptr->setUSValue(TAG_RECORD_IN_USE_FLAG,0xFFFF);
					seriesRecord_ptr->setCSValue(TAG_DIRECTORY_RECORD_TYPE,"SERIES");
					seriesRecord_ptr->setUIValue(TAG_SERIES_INSTANCE_UID, seriesData_ptr->getInstanceUid());
					seriesRecord_ptr->setCSValue(TAG_MODALITY, seriesData_ptr->getModality());
					seriesRecord_ptr->setISValue(TAG_SERIES_NUMBER, seriesData_ptr->getSeriesNr());

					for (UINT l = 0; l < seriesData_ptr->noSopInstances(); l++)
					{
						if (loggerM_ptr)
						{
    						loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the image records with unique identifier in Warehouse.");
						}
						DCM_ITEM_CLASS* imageRecord_ptr = new DCM_ITEM_CLASS();
						IMAGE_INFO_CLASS *imageData_ptr = seriesData_ptr->getSopInstanceData(l);

						if((l == 0) && (imageData_ptr != NULL))
						{
							DCM_ATTRIBUTE_CLASS *lowerRecordAttribute_ptr = getULAttribute(imageData_ptr->getIdentifier(),
																TAG_OFFSET_OF_REFERENCED_LOWER_LEVEL_DIRECTORY_ENTITY);
							seriesRecord_ptr->addAttribute(lowerRecordAttribute_ptr);
						}

						// cascade the logger
						imageRecord_ptr->setLogger(loggerM_ptr);
						imageRecord_ptr->setIdentifier(imageData_ptr->getIdentifier());
						imageRecord_ptr->setPopulateWithAttributes(false);

						if((1 == seriesData_ptr->noSopInstances()) || ((l+1) == seriesData_ptr->noSopInstances()))
						{
							imageRecord_ptr->setULValue(TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD, 0x00000000);
						}
						else
						{
							DCM_ATTRIBUTE_CLASS *nextRecordAttribute_ptr = getULAttribute(seriesData_ptr->getSopInstanceData(l+1)->getIdentifier(),
																TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD);
							imageRecord_ptr->addAttribute(nextRecordAttribute_ptr);
						}

						imageRecord_ptr->setULValue(TAG_OFFSET_OF_REFERENCED_LOWER_LEVEL_DIRECTORY_ENTITY, 0x00000000);
						imageRecord_ptr->setUSValue(TAG_RECORD_IN_USE_FLAG, 0xFFFF);
						imageRecord_ptr->setCSValue(TAG_DIRECTORY_RECORD_TYPE, "IMAGE");
						imageRecord_ptr->setCSValue(TAG_REFERENCED_FILE_ID, imageData_ptr->getRefFileId());
						imageRecord_ptr->setUIValue(TAG_REFERENCED_SOP_CLASS_UID_IN_FILE, imageData_ptr->getRefSOPClassUid());
						imageRecord_ptr->setUIValue(TAG_REFERENCED_SOP_INSTANCE_UID_IN_FILE, imageData_ptr->getRefSOPClassInstanceUid());
						imageRecord_ptr->setUIValue(TAG_REFERENCED_TRANSFER_SYNTAX_UID_IN_FILE, imageData_ptr->getRefTSUid());
						imageRecord_ptr->setCSValue(TAG_SPECIFIC_CHARACTER_SET,"ISO_IR 100");
						imageRecord_ptr->setUIValue(TAG_SOP_INSTANCE_UID, imageData_ptr->getInstanceUid());
						imageRecord_ptr->setISValue(TAG_IMAGE_NUMBER, imageData_ptr->getInstanceNr());

						// try to store the Patient record in the warehouse
						string imageIdentifier = imageData_ptr->getIdentifier();
						imageRecord_ptr->SortAttributes();
						result = WAREHOUSE->store(imageIdentifier, imageRecord_ptr);
						if (result)
						{
							// log the action
							if (loggerM_ptr)
							{
								loggerM_ptr->text(LOG_INFO, 2, "CREATE %s %s (in Data Warehouse)", WIDName(imageRecord_ptr->getWidType()), imageIdentifier.c_str());
							}
						}
						else
						{
							if (loggerM_ptr)
							{
								loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s %s in Data Warehouse", WIDName(imageRecord_ptr->getWidType()), imageIdentifier.c_str());
							}
						}
					}

					for (UINT l = 0; (l < seriesData_ptr->noPresentationStates()) && isPSPresent; l++)
					{
						if (loggerM_ptr)
						{
    						loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the presentation records with unique identifier in Warehouse.");
						}
						DCM_ITEM_CLASS* psRecord_ptr = new DCM_ITEM_CLASS();
						PRESENTATION_STATE_INFO_CLASS *psData_ptr = seriesData_ptr->getPSData(l);

						if((l == 0) && (psData_ptr != NULL))
						{
							DCM_ATTRIBUTE_CLASS *lowerRecordAttribute_ptr = getULAttribute(psData_ptr->getIdentifier(),
																TAG_OFFSET_OF_REFERENCED_LOWER_LEVEL_DIRECTORY_ENTITY);
							seriesRecord_ptr->addAttribute(lowerRecordAttribute_ptr);
						}

						// cascade the logger
						psRecord_ptr->setLogger(loggerM_ptr);
						psRecord_ptr->setIdentifier(psData_ptr->getIdentifier());
						psRecord_ptr->setPopulateWithAttributes(false);

						if((1 == seriesData_ptr->noPresentationStates()) || ((l+1) == seriesData_ptr->noPresentationStates()))
						{
							psRecord_ptr->setULValue(TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD, 0x00000000);
						}
						else
						{
							DCM_ATTRIBUTE_CLASS *nextRecordAttribute_ptr = getULAttribute(seriesData_ptr->getPSData(l+1)->getIdentifier(),
																TAG_OFFSET_OF_THE_NEXT_DIRECTORY_RECORD);
							psRecord_ptr->addAttribute(nextRecordAttribute_ptr);
						}

						psRecord_ptr->setULValue(TAG_OFFSET_OF_REFERENCED_LOWER_LEVEL_DIRECTORY_ENTITY, 0x00000000);
						psRecord_ptr->setUSValue(TAG_RECORD_IN_USE_FLAG, 0xFFFF);
						psRecord_ptr->setCSValue(TAG_DIRECTORY_RECORD_TYPE, "PRESENTATION");
						psRecord_ptr->setCSValue(TAG_CONTENT_LABLE, psData_ptr->getContentLable());
						psRecord_ptr->setUIValue(TAG_SERIES_INSTANCE_UID, psData_ptr->getRefSeriesInstanceUid());
						psRecord_ptr->setUIValue(TAG_REFERENCED_SOP_CLASS_UID, psData_ptr->getRefSOPClassUid());
						psRecord_ptr->setUIValue(TAG_REFERENCED_SOP_INSTANCE_UID, psData_ptr->getRefSOPClassInstanceUid());
						psRecord_ptr->setDAValue(TAG_PS_CREATION_DATE, psData_ptr->getPSCreationDate());
						psRecord_ptr->setTMValue(TAG_PS_CREATION_TIME,psData_ptr->getPSCreationTime());
						psRecord_ptr->setISValue(TAG_IMAGE_NUMBER, psData_ptr->getInstanceNr());

						// try to store the Patient record in the warehouse
						string psIdentifier = psData_ptr->getIdentifier();
						psRecord_ptr->SortAttributes();
						result = WAREHOUSE->store(psIdentifier, psRecord_ptr);
						if (result)
						{
							// log the action
							if (loggerM_ptr)
							{
								loggerM_ptr->text(LOG_INFO, 2, "CREATE %s %s (in Data Warehouse)", WIDName(psRecord_ptr->getWidType()), psIdentifier.c_str());
							}
						}
						else
						{
							if (loggerM_ptr)
							{
								loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s %s in Data Warehouse", WIDName(psRecord_ptr->getWidType()), psIdentifier.c_str());
							}
						}
					}

					// try to store the Patient record in the warehouse
					string seriesIdentifier = seriesData_ptr->getIdentifier();
					seriesRecord_ptr->SortAttributes();
					result = WAREHOUSE->store(seriesIdentifier, seriesRecord_ptr);
					if (result)
					{
						// log the action
						if (loggerM_ptr)
						{
							loggerM_ptr->text(LOG_INFO, 2, "CREATE %s %s (in Data Warehouse)", WIDName(seriesRecord_ptr->getWidType()), seriesIdentifier.c_str());
						}
					}
					else
					{
						if (loggerM_ptr)
						{
							loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s %s in Data Warehouse", WIDName(seriesRecord_ptr->getWidType()), seriesIdentifier.c_str());
						}
					}
				}

				// try to store the Study record in the warehouse
				string studyIdentifier = studyData_ptr->getIdentifier();
				studyRecord_ptr->SortAttributes();
				result = WAREHOUSE->store(studyIdentifier, studyRecord_ptr);
				if (result)
				{
					// log the action
					if (loggerM_ptr)
					{
						loggerM_ptr->text(LOG_INFO, 2, "CREATE %s %s (in Data Warehouse)", WIDName(studyRecord_ptr->getWidType()), studyIdentifier.c_str());
					}
				}
				else
				{
					if (loggerM_ptr)
					{
						loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s %s in Data Warehouse", WIDName(studyRecord_ptr->getWidType()), studyIdentifier.c_str());
					}
				}
			}

			// try to store the Patient record in the warehouse
			string patIdentifier = patientData[i]->getIdentifier();
			patientRecord_ptr->SortAttributes();
			result = WAREHOUSE->store(patIdentifier, patientRecord_ptr);
			if (result)
			{
				// log the action
				if (loggerM_ptr)
				{
					loggerM_ptr->text(LOG_INFO, 2, "CREATE %s %s (in Data Warehouse)", WIDName(patientRecord_ptr->getWidType()), patIdentifier.c_str());
				}
			}
			else
			{
				if (loggerM_ptr)
				{
					loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s %s in Data Warehouse", WIDName(patientRecord_ptr->getWidType()), patIdentifier.c_str());
				}
			}
		}
	}

	return result;
}

//>>===========================================================================

bool GENERATE_DICOMDIR_CLASS::CreateAndStoreDirectorySequenceObject()

//  DESCRIPTION     : Create Directory Sequence Object and store them into warehouse.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      :
//  NOTES           :
//<<===========================================================================
{
	bool result = false;

	//Create Directory Sequence Object object & store into Data Warehouse.
	if (loggerM_ptr)
	{
    	loggerM_ptr->text(LOG_INFO, 2, "Creating & storing the Directory Sequence Object in Warehouse.");
	}
	DCM_DATASET_CLASS* directoryObject_ptr = new DCM_DATASET_CLASS();

	// cascade the logger
	directoryObject_ptr->setLogger(loggerM_ptr);
	if (directoryObject_ptr)
	{
		directoryObject_ptr->setCSValue(TAG_FILE_SET_ID, "DICOMDIR");
		directoryObject_ptr->setCSValue(TAG_SPECIFIC_CHARACTER_SET_OF_FILE_SET_DESCRIPTOR_FILE, "ISO_IR 100");

		int nrOfPatients = patientData.getSize();
		string firstRecordIdentifier;
		string lastRecordIdentifier;

		if (nrOfPatients == 1)
		{
			firstRecordIdentifier = patientData[0]->getIdentifier();
			lastRecordIdentifier = patientData[0]->getIdentifier();
		}
		else if(nrOfPatients > 1)
		{
			firstRecordIdentifier = patientData[0]->getIdentifier();
			lastRecordIdentifier = patientData[nrOfPatients-1]->getIdentifier();
		}
		else
		{
			if (loggerM_ptr)
			{
    			loggerM_ptr->text(LOG_ERROR, 1, "There is no patient data in the DCM files.");
			}
			return false;
		}

		DCM_ATTRIBUTE_CLASS *firstRecordAttribute_ptr = getULAttribute(firstRecordIdentifier,
						TAG_OFFSET_OF_THE_FIRST_DIRECTORY_RECORD_OF_THE_ROOT_DIRECTORY_ENTITY);
		directoryObject_ptr->addAttribute(firstRecordAttribute_ptr);
		DCM_ATTRIBUTE_CLASS *lastRecordAttribute_ptr = getULAttribute(lastRecordIdentifier,
						TAG_OFFSET_OF_THE_LAST_DIRECTORY_RECORD_OF_THE_ROOT_DIRECTORY_ENTITY);
		directoryObject_ptr->addAttribute(lastRecordAttribute_ptr);

		directoryObject_ptr->setUSValue(TAG_FILE_SET_CONSISTENCY_FLAG, 0x0000);

		directoryObject_ptr->addAttribute(getSQAttribute());
		directoryObject_ptr->setWidType(WID_DATASET);
	}

	// try to store the File Meta Info object in the warehouse
	string directoryObjectIdentifier = "DICOMDIR";
	result = WAREHOUSE->store(directoryObjectIdentifier, directoryObject_ptr);
	if (result)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_INFO, 2, "CREATE %s %s (in Data Warehouse)", WIDName(directoryObject_ptr->getWidType()), directoryObjectIdentifier.c_str());
		}
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't create %s %s in Data Warehouse", WIDName(directoryObject_ptr->getWidType()), directoryObjectIdentifier.c_str());
		}
	}

	return result;
}

//>>===========================================================================

DCM_ATTRIBUTE_CLASS *GENERATE_DICOMDIR_CLASS::getULAttribute(string identifier, UINT32 tag)

//  DESCRIPTION     : Get the UL attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// get the attribute with the given tag
	DCM_ATTRIBUTE_CLASS *attribute_ptr = new DCM_ATTRIBUTE_CLASS(tag, ATTR_VR_UL);
	attribute_ptr->SetType(ATTR_TYPE_1);

	DCM_VALUE_UL_CLASS *valueUL_ptr = new DCM_VALUE_UL_CLASS();
	valueUL_ptr->setIdentifier(identifier);
	attribute_ptr->AddValue(valueUL_ptr);
	return attribute_ptr;
}

//>>===========================================================================

DCM_ATTRIBUTE_CLASS *GENERATE_DICOMDIR_CLASS::getSQAttribute()

//  DESCRIPTION     : Get the UL attribute.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	DCM_ATTRIBUTE_CLASS *sqAttribute_ptr = new DCM_ATTRIBUTE_CLASS(TAG_DIRECTORY_RECORD_SEQUENCE, ATTR_VR_SQ);
	sqAttribute_ptr->SetType(ATTR_TYPE_1);
	
	// get SQ value
	DCM_VALUE_SQ_CLASS* sq_value_ptr = new DCM_VALUE_SQ_CLASS(UNDEFINED_LENGTH);

	// cascade the logger
	sq_value_ptr->setLogger(loggerM_ptr);

	sq_value_ptr->setDefinedLength(true);
	
	for (UINT i = 0; i < patientData.getSize(); i++)
	{
		PATIENT_INFO_CLASS *patientData_ptr = patientData[i];

		if (patientData_ptr != NULL)
		{
			// instantiate the item
			DCM_ITEM_CLASS *item_ptr = new DCM_ITEM_CLASS();

			// cascade the logger
			item_ptr->setLogger(loggerM_ptr);
			item_ptr->setDefinedLength(true);
			item_ptr->setIdentifier(patientData_ptr->getIdentifier());
			item_ptr->setValueByReference(true);

			sq_value_ptr->addItem(item_ptr);
		}

		for (UINT j = 0; j < patientData_ptr->noStudies(); j++)
		{
			STUDY_INFO_CLASS *studyData_ptr = patientData_ptr->getStudyData(j);

			if (studyData_ptr != NULL)
			{
				// instantiate the item
				DCM_ITEM_CLASS *item_ptr = new DCM_ITEM_CLASS();

				// cascade the logger
				item_ptr->setLogger(loggerM_ptr);
				item_ptr->setDefinedLength(true);
				item_ptr->setIdentifier(studyData_ptr->getIdentifier());
				item_ptr->setValueByReference(true);

				sq_value_ptr->addItem(item_ptr);
			}

			for (UINT k = 0; k < studyData_ptr->noSeries(); k++)
			{
				SERIES_INFO_CLASS *seriesData_ptr = studyData_ptr->getSeriesData(k);

				if (seriesData_ptr != NULL)
				{
					// instantiate the item
					DCM_ITEM_CLASS *item_ptr = new DCM_ITEM_CLASS();

					// cascade the logger
					item_ptr->setLogger(loggerM_ptr);
					item_ptr->setDefinedLength(true);
					item_ptr->setIdentifier(seriesData_ptr->getIdentifier());
					item_ptr->setValueByReference(true);

					sq_value_ptr->addItem(item_ptr);
				}

				for (UINT l = 0; l < seriesData_ptr->noSopInstances(); l++)
				{
					IMAGE_INFO_CLASS *imageData_ptr = seriesData_ptr->getSopInstanceData(l);

					if (imageData_ptr != NULL)
					{
						// instantiate the item
						DCM_ITEM_CLASS *item_ptr = new DCM_ITEM_CLASS();

						// cascade the logger
						item_ptr->setLogger(loggerM_ptr);
						item_ptr->setDefinedLength(true);
						item_ptr->setIdentifier(imageData_ptr->getIdentifier());
						item_ptr->setValueByReference(true);

						sq_value_ptr->addItem(item_ptr);
					}
				}
			}
		}
	}

	sqAttribute_ptr->addSqValue(sq_value_ptr);

	return sqAttribute_ptr;
}

//>>===========================================================================

bool GENERATE_DICOMDIR_CLASS::writeDICOMDIR(string filename)

//  DESCRIPTION     : Function to write the DICOMDIR to the given file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = false;

	if (loggerM_ptr)
	{
		loggerM_ptr->text(LOG_INFO, 2, "Writing DICOMDIR to the file %s", filename.c_str());
	}

	// try to retrive the file head from the warehouse
	BASE_WAREHOUSE_ITEM_DATA_CLASS *widFileHead_ptr = WAREHOUSE->retrieve("", WID_FILEHEAD);
	if (widFileHead_ptr)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 2, "WRITE %s (from Data Warehouse) to %s", WIDName(WID_FILEHEAD), filename.c_str());
		}

		FILEHEAD_CLASS *filehead_ptr = static_cast<FILEHEAD_CLASS*>(widFileHead_ptr);

		// set up the write file
		filehead_ptr->setFilename(correctPathnameForOS(filename));

		// write the file head to the file
		result = filehead_ptr->write(false);
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't write %s from Data Warehouse to %s", WIDName(WID_FILEHEAD), filename.c_str());
		}
	}

	// try to retrive the file meta from the warehouse
	BASE_WAREHOUSE_ITEM_DATA_CLASS *widFileMeta_ptr = WAREHOUSE->retrieve(fileMetaIdentifier, WID_META_INFO);
	if (widFileMeta_ptr)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 2, "WRITE %s (from Data Warehouse) to %s", WIDName(WID_META_INFO), filename.c_str());
		}

		DCM_DATASET_CLASS *filemeta_ptr = static_cast<DCM_DATASET_CLASS*>(widFileMeta_ptr);

		// set up the write file
		FILE_DATASET_CLASS	fileDataset(correctPathnameForOS(filename));

		// cascade the logger
		fileDataset.setLogger(loggerM_ptr);

		// write the dataset to the file
		result = fileDataset.write(filemeta_ptr);
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't write %s from Data Warehouse to %s", WIDName(WID_META_INFO), filename.c_str());
		}
	}

	// try to retrive the file meta from the warehouse
	BASE_WAREHOUSE_ITEM_DATA_CLASS *widMediaDirectory_ptr = WAREHOUSE->retrieve("DICOMDIR", WID_DATASET);
	if (widMediaDirectory_ptr)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 2, "WRITE %s (from Data Warehouse) to %s", WIDName(WID_DATASET), filename.c_str());
		}

		DCM_DATASET_CLASS *mediaDirectory_ptr = static_cast<DCM_DATASET_CLASS*>(widMediaDirectory_ptr);

		// set up the write file
		FILE_DATASET_CLASS	mediaDirectoryDataset(correctPathnameForOS(filename));

		// cascade the logger
		mediaDirectoryDataset.setLogger(loggerM_ptr);

		// write the dataset to the file
		result = mediaDirectoryDataset.write(mediaDirectory_ptr);
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't write %s from Data Warehouse to %s", WIDName(WID_DATASET), filename.c_str());
		}
	}

	// try to retrive the file tail from the warehouse
	BASE_WAREHOUSE_ITEM_DATA_CLASS *widFileTail_ptr = WAREHOUSE->retrieve("", WID_FILETAIL);
	if (widFileTail_ptr)
	{
		// log the action
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 2, "WRITE %s (from Data Warehouse) to %s", WIDName(WID_FILETAIL), filename.c_str());
		}

		FILETAIL_CLASS *filetail_ptr = static_cast<FILETAIL_CLASS*>(widFileTail_ptr);

		// set up the write file
		filetail_ptr->setFilename(correctPathnameForOS(filename));

		// write the file tail to the file
		result = filetail_ptr->write(false);
	}
	else
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_DEBUG, 1, "Can't write %s from Data Warehouse to %s", WIDName(WID_FILETAIL), filename.c_str());
		}
	}

	// return result
	return result;
}

//>>===========================================================================

void GENERATE_DICOMDIR_CLASS::setLogger(LOG_CLASS *logger_ptr)

//  DESCRIPTION     : Set the Logger.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{ 
    loggerM_ptr = logger_ptr; 
}
