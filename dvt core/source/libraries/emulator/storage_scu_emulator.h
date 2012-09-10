//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	Storage SCU emulator class.
//*****************************************************************************
#ifndef STORAGE_SCU_EMULATOR_H
#define STORAGE_SCU_EMULATOR_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"				// Global component interface
#include "Idicom.h"			// Dicom component interface
#include "Inetwork.h"		// Network component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class EMULATOR_SESSION_CLASS;
class LOG_CLASS;
class DCM_DATASET_CLASS;
class MEDIA_FILE_HEADER_CLASS;


const UINT SCU_STORAGE_EML_OPTION_ASSOC_SINGLE       = 0x0001;
const UINT SCU_STORAGE_EML_OPTION_ASSOC_MULTI        = 0x0002;
const UINT SCU_STORAGE_EML_OPTION_VALIDATE_ON_IMPORT = 0x0004;
const UINT SCU_STORAGE_EML_OPTION_DATA_UNDER_NEWSTUDY= 0x0008;
const UINT SCU_STORAGE_EML_OPTION_REPEAT             = 0x0010;

struct FMI_DATASET_STRUCT
{
	string                   filename;
    string                   transferSyntax;
	MEDIA_FILE_HEADER_CLASS* fmi_ptr;
	DCM_DATASET_CLASS*       dat_ptr;
};


//>>***************************************************************************

class STORAGE_SCU_EMULATOR_CLASS

//  DESCRIPTION     : Storage emulator class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	EMULATOR_SESSION_CLASS		*sessionM_ptr;
	ASSOCIATION_CLASS			associationM;
	LOG_CLASS					*loggerM_ptr;
    BASE_SERIALIZER             *serializerM_ptr;
	bool						autoType2AttributesM;
	bool						defineSqLengthM;
	bool						addGroupLengthM;
	vector<string>              filenamesM;
	UINT                        optionsM;
	UINT                        nr_repetitionsM;
	UINT                        message_idM;
    vector<FMI_DATASET_STRUCT>  filedatasetsM;

	bool sendFilesInMultipleAssociations(int repetition_index);

    bool sendFilesInSingleAssociation(int repetition_index);

	bool importFile(string filename);

	bool importRefFilesFromDicomdir(FILE_DATASET_CLASS* fileDataset,string filename);

	string extractDirectoryName (string filename);

	bool CheckFileExistance (string filename);

	void cleanup();

public:
	STORAGE_SCU_EMULATOR_CLASS(EMULATOR_SESSION_CLASS*);
	~STORAGE_SCU_EMULATOR_CLASS();

	bool emulate();

	bool verify();

	bool eventReportStorageCommitment(UINT16, DCM_DATASET_CLASS*);

	void addFile(string filename);

	void removeFile(string filename);

	UINT resetOptions();

	UINT setOption(UINT option); 
	UINT addOption(UINT option); 
	void setNrRepetitions(UINT nr);

	void setLogger(LOG_CLASS*);

    void setSerializer(BASE_SERIALIZER*);
};

#endif /* STORAGE_SCU_EMULATOR_H */
