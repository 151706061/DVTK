//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  DESCRIPTION     :	File Meta Information class.
//*****************************************************************************
#ifndef MEDIA_H
#define MEDIA_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Idicom.h"			// Dicom component interface


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
#define FMI_PREAMBLE_LENGTH		128
#define FMI_PREAMBLE_VALUE		0
#define FMI_PREFIX_LENGTH		4
#define FMI_PREFIX_VALUE		"DICM"


//>>***************************************************************************

class MEDIA_FILE_HEADER_CLASS : public DCM_ATTRIBUTE_GROUP_CLASS

//  DESCRIPTION     : Media File Header - includes preamble, prefix and file meta information.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string			     filenameM;
	BYTE			     preambleValueM;
	BYTE			     preambleM[FMI_PREAMBLE_LENGTH];
	BYTE			     prefixM[FMI_PREFIX_LENGTH + 1];
	string			     transferSyntaxUidM;
	FILE_TF_CLASS*       fileTfM_ptr;

	bool encode(DATA_TF_CLASS&);

public:
	MEDIA_FILE_HEADER_CLASS();
	MEDIA_FILE_HEADER_CLASS(int, string, string, string, LOG_CLASS*);
	MEDIA_FILE_HEADER_CLASS(string, bool openForReading = true);

	~MEDIA_FILE_HEADER_CLASS();

    void setFilename(string);

    void setFileTFpointer(FILE_TF_CLASS*);

	void setPreambleValue(BYTE);

    const BYTE* getPreamble();

	void setPrefix(char*);

    const BYTE* getPrefix();

	void setTransferSyntaxUid(string);

    const char* getTransferSyntaxUid();

	const char* getFilename();

	DCM_ATTRIBUTE_GROUP_CLASS *read();
	bool read(FILE_TF_CLASS*);

	bool write();
	bool write(FILE_TF_CLASS*);

	bool decode(DATA_TF_CLASS&, UINT16 lastGroup = GROUP_EIGHT);

	bool updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*);
};

#endif /* MEDIA_H */


