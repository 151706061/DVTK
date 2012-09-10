//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	File based DICOM Dataset class.
//*****************************************************************************
#ifndef GENERATEDICOMDIR_H
#define GENERATEDICOMDIR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Iemulator.h"		// Definition component interface
#include "Imedia.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;
#define fileMetaIdentifier  "FILE METAINFO"

//>>***************************************************************************

class IMAGE_INFO_CLASS

//  DESCRIPTION     : Image Data Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string instanceUid;
	string refFileId;
	string refSOPClassUid;
	string refSOPClassInstanceUid;
	string refTSUid;
	INT32 instanceNr;
	string identifier;
	UINT count;

public:
	IMAGE_INFO_CLASS(string, string, string, string, string, INT32, string);

	~IMAGE_INFO_CLASS();

	void incrementCount() 
		{ count++; }

	string getInstanceUid() 
		{ return instanceUid; }

	string getRefFileId() 
		{ return refFileId; }

	string getRefSOPClassUid() 
		{ return refSOPClassUid; }

	string getRefSOPClassInstanceUid() 
		{ return refSOPClassInstanceUid; }

	string getRefTSUid() 
		{ return refTSUid;	 }

	string getInstanceNr() 
		{
			char base[100];
			_itoa(instanceNr,base,10);
			string instanceStr = base;
			return instanceStr;
		}

	UINT getCount() 
		{ return count; }

	string getIdentifier() 
		{ return identifier; }

	void setIdentifier(string Identifier) 
		{ identifier = Identifier; }

	bool operator = (IMAGE_INFO_CLASS&);
};

//>>***************************************************************************

class PRESENTATION_STATE_INFO_CLASS

//  DESCRIPTION     : Presentation State Data Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	INT32 instanceNr;
	string refSeriesInstanceUid;
	string refSOPClassUid;
	string refSOPClassInstanceUid;
	string psCreationDate;
	string psCreationTime;
	string contentLable;
	string identifier;
	UINT count;

public:
	PRESENTATION_STATE_INFO_CLASS(INT32, string, string, string, string, string, string, string);

	~PRESENTATION_STATE_INFO_CLASS();

	void incrementCount() 
		{ count++; }

	string getInstanceNr() 
		{
			char base[100];
			_itoa(instanceNr,base,10);
			string instanceStr = base;
			return instanceStr;
		}

	string getRefSeriesInstanceUid() 
		{ return refSeriesInstanceUid; }

	string getRefSOPClassUid() 
		{ return refSOPClassUid; }

	string getRefSOPClassInstanceUid() 
		{ return refSOPClassInstanceUid; }

	string getPSCreationDate() 
		{ return psCreationDate;	 }

	string getPSCreationTime() 
		{ return psCreationTime;	 }

	string getContentLable() 
		{ return contentLable; }

	UINT getCount() 
		{ return count; }

	string getIdentifier() 
		{ return identifier; }

	void setIdentifier(string Identifier) 
		{ identifier = Identifier; }

	bool operator = (PRESENTATION_STATE_INFO_CLASS&);
};

//>>***************************************************************************

class SERIES_INFO_CLASS

//  DESCRIPTION     : Study Data Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string instanceUid;
	string modality;
	INT32 seriesNr;
	string identifier;
	ARRAY<IMAGE_INFO_CLASS*> sopInstanceData;
	ARRAY<PRESENTATION_STATE_INFO_CLASS*> presentationStateData;

public:
	SERIES_INFO_CLASS(string,string,INT32,string);
		
	~SERIES_INFO_CLASS();
		
	string getInstanceUid() 
		{ return instanceUid; }

	string getModality() 
		{ return modality; }

	string getSeriesNr() 
		{
			char base[100];
			_itoa(seriesNr,base,10);
			string seriesStr = base;
			return seriesStr; 
		}

	string getIdentifier() 
		{ return identifier; }

	void setIdentifier(string Identifier) 
		{ identifier = Identifier; }

	UINT noSopInstances() 
		{ return sopInstanceData.getSize(); }

	IMAGE_INFO_CLASS *getSopInstanceData(UINT i) 
		{ return sopInstanceData[i]; }

	void addSopInstanceData(IMAGE_INFO_CLASS *sopInstanceData_ptr)
		{ sopInstanceData.add(sopInstanceData_ptr); }

	IMAGE_INFO_CLASS *searchImage(string);

	UINT noPresentationStates() 
		{ return presentationStateData.getSize(); }

	PRESENTATION_STATE_INFO_CLASS *getPSData(UINT i) 
		{ return presentationStateData[i]; }

	void addPSData(PRESENTATION_STATE_INFO_CLASS *psData_ptr)
		{ presentationStateData.add(psData_ptr); }

	PRESENTATION_STATE_INFO_CLASS *searchPS(INT32);

	bool operator = (SERIES_INFO_CLASS&);
};

//>>***************************************************************************

class STUDY_INFO_CLASS

//  DESCRIPTION     : Series Data Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string instanceUid;
	string studyId;
	string studyDate;
	string studyTime;
	string studyDescr;
	string accessionNr;
	string identifier;
	ARRAY<SERIES_INFO_CLASS*> seriesData;

public:
	STUDY_INFO_CLASS(string,string,string,string,string,string,string);

	~STUDY_INFO_CLASS();

	string getInstanceUid() 
		{ return instanceUid; }

	string getStudyId() 
		{ return studyId; }

	string getStudyDate() 
		{ return studyDate; }

	string getStudyTime() 
		{ return studyTime; }

	string getStudyDescr() 
		{ return studyDescr; }

	string getAccessionNr() 
		{ return accessionNr; }

	string getIdentifier() 
		{ return identifier; }

	void setIdentifier(string Identifier) 
		{ identifier = Identifier; }

	UINT noSeries()
		{ return seriesData.getSize(); }

	SERIES_INFO_CLASS *getSeriesData(UINT i)
		{ return seriesData[i]; }

	void addSeriesData(SERIES_INFO_CLASS *seriesData_ptr)
		{ seriesData.add(seriesData_ptr); }

	SERIES_INFO_CLASS *searchSeries(string);

	bool operator = (STUDY_INFO_CLASS&);
};

//>>***************************************************************************

class PATIENT_INFO_CLASS

//  DESCRIPTION     : Patient Data Class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string patientName;
	string patientId;
	string identifier;
	ARRAY<STUDY_INFO_CLASS*> studyData;

public:
	PATIENT_INFO_CLASS(string, string, string);

	~PATIENT_INFO_CLASS();

	string getPatientName() 
		{ return patientName; }

	string getPatientId() 
		{ return patientId; }

	string getIdentifier() 
		{ return identifier; }

	void setIdentifier(string Identifier) 
		{ identifier = Identifier; }

	UINT noStudies()
		{ return studyData.getSize(); }

	STUDY_INFO_CLASS *getStudyData(UINT i) 
		{ return studyData[i]; }

	void addStudyData(STUDY_INFO_CLASS *studyData_ptr)
		{ studyData.add(studyData_ptr); }

	STUDY_INFO_CLASS *searchStudy(string);

	bool operator = (PATIENT_INFO_CLASS&);
};

//>>***************************************************************************

class GENERATE_DICOMDIR_CLASS

//  DESCRIPTION     : File based DICOM Dataset
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	LOG_CLASS				*loggerM_ptr;
	ARRAY<PATIENT_INFO_CLASS*>	patientData;
    vector<FMI_DATASET_STRUCT>  filedatasetsM;
	string dicomdirPathM;
	bool isPSPresent;

	bool readDCMFiles(string filename);

	PATIENT_INFO_CLASS *searchPatient(string id, string name);

	void analyseStorageDataset(DCM_DATASET_CLASS* dataset_ptr, string fileName, string ts);

	bool CreateDICOMObjects();

	bool CreateAndStoreRecords();

	bool CreateAndStoreDirectorySequenceObject();

	DCM_ATTRIBUTE_CLASS *getULAttribute(string identifier, UINT32 tag);

	DCM_ATTRIBUTE_CLASS *getSQAttribute();

	bool writeDICOMDIR(string filename);

public:
	GENERATE_DICOMDIR_CLASS(string);

	~GENERATE_DICOMDIR_CLASS();

	bool generateDICOMDIR(vector<string>* filenames);

	void setLogger(LOG_CLASS*);
};

#endif /* FILEDATASET_H */
