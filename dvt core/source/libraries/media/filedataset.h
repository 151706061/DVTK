//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	File based DICOM Dataset class.
//*****************************************************************************
#ifndef FILEDATASET_H
#define FILEDATASET_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_DATASET_CLASS;
class DCM_DIR_DATASET_CLASS;
class FILE_TF_CLASS;
class LOG_CLASS;
class MEDIA_FILE_HEADER_CLASS;
class DCM_ITEM_CLASS;


//>>***************************************************************************

class FILE_DATASET_CLASS

//  DESCRIPTION     : File based DICOM Dataset
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string					filenameM;
    string                  transferSyntaxM;
	STORAGE_MODE_ENUM		storageModeM;
    FILE_TF_CLASS           *fileTfM_ptr;
	MEDIA_FILE_HEADER_CLASS	*fmiM_ptr;
	DCM_DATASET_CLASS		*datasetM_ptr;
    DCM_DIR_DATASET_CLASS   *dicomdirDatasetM_ptr;
	LOG_CLASS				*loggerM_ptr;

	bool isFileMetaInformation(FILE_TF_CLASS*, string&);

    MEDIA_FILE_HEADER_CLASS* readFileMetaInformation(FILE_TF_CLASS*);

	bool deduceTransferSyntax(FILE_TF_CLASS*, string&);

public:
	FILE_DATASET_CLASS();
	FILE_DATASET_CLASS(string);

	~FILE_DATASET_CLASS();

	void setFilename(string);

	const char* getFilename();

    string getTransferSyntax();

    void setFileMetaInformation(MEDIA_FILE_HEADER_CLASS*);

	MEDIA_FILE_HEADER_CLASS* getFileMetaInformation();

    void setDataset(DCM_DATASET_CLASS*);

	DCM_DATASET_CLASS* getDataset();

    DCM_DIR_DATASET_CLASS* getDicomdirDataset();

    DCM_ITEM_CLASS* getNextDicomdirRecord();

	void clearFileMetaInformationPtr();

	void clearDatasetPtr();

	bool read(DCM_DATASET_CLASS*);

	bool read();

	bool write(DCM_DATASET_CLASS*, bool autoCreateDirectory = true);

    bool write(bool autoCreateDirectory = true);

	void setStorageMode(STORAGE_MODE_ENUM);

	void setLogger(LOG_CLASS*);
};

#endif /* FILEDATASET_H */
