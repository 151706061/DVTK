//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef FILETAIL_H
#define FILETAIL_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Idicom.h"			// Dicom component interface


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
#define DSTP_SECTOR_SIZE	512
#define DSTP_PADDING_VALUE	0


//>>***************************************************************************

class FILETAIL_CLASS : public DCM_ATTRIBUTE_GROUP_CLASS

//  DESCRIPTION     : File Tail.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	string			filenameM;
	bool			trailingPaddingM;
	UINT			sectorSizeM;
	BYTE			paddingValueM;

public:
	FILETAIL_CLASS();

	~FILETAIL_CLASS();

	void setFilename(string filename)
		{ filenameM = filename; }

	void setTrailingPadding(bool flag)
		{ trailingPaddingM = flag; }

	void setSectorSize(UINT sectorSize)
		{ sectorSizeM = sectorSize; }
	
	void setPaddingValue(BYTE paddingValue)
	{ paddingValueM = paddingValue; }

	const char* getFilename()
		{ return filenameM.c_str(); }

	bool isTrailingPadding()
		{ return trailingPaddingM; }

	UINT getSectorSize()
		{ return sectorSizeM; }

	BYTE getPaddingValue()
		{ return paddingValueM; }

	bool write(bool autoCreateDirectory);

	bool updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS*);
};

#endif /* FILETAIL_H */


