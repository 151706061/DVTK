// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef OB_VALUE_STREAM_H
#define OB_VALUE_STREAM_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "other_value_stream.h"

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
class LOG_MESSAGE_CLASS;


//>>***************************************************************************

class DATA_FRAGMENT_CLASS

//  DESCRIPTION     : Data fragment class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	UINT32		file_offsetM;
	UINT32		offsetM;
	UINT32		lengthM;

public:
	DATA_FRAGMENT_CLASS()
		{
			// constructor details
			file_offsetM = 0;
			offsetM = 0;
			lengthM = 0;
		}

	DATA_FRAGMENT_CLASS(UINT32 file_offset, UINT32 offset, UINT32 length)
		{
			// constructor details
			file_offsetM = file_offset;
			offsetM = offset;
			lengthM = length;
		}

	~DATA_FRAGMENT_CLASS() { }

	UINT32 GetFileOffset() { return file_offsetM; }

	UINT32 GetOffset() { return offsetM; }

	UINT32 GetLength() { return lengthM; }
};


//>>***************************************************************************

class OB_VALUE_STREAM_CLASS : public OTHER_VALUE_STREAM_CLASS

//  DESCRIPTION     : OB value stream class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	// ov basic offset table data - only applicable to file based OB data
	UINT32						no_basic_offset_table_entriesM;
	UINT32						*basic_offset_tableM_ptr;
	vector<DATA_FRAGMENT_CLASS>	data_fragmentM;

	// private methods
	void AllocateBasicOffsetTable(UINT no_values);
	void AddBasicOffsetTableValue(UINT index, UINT32 offset);
	void AddEncodedDataFragment(UINT32 file_offset, UINT32 offset, UINT32 length);

	bool ReadBOTAndDataFragments();

	UINT32 ReadDataFragment(UINT index, BYTE* buffer_ptr, UINT32 offset, UINT32 length);

    DVT_STATUS CompareCompressed(LOG_MESSAGE_CLASS*, OB_VALUE_STREAM_CLASS&  refObStream);

public:
	OB_VALUE_STREAM_CLASS();
	~OB_VALUE_STREAM_CLASS();

	bool SetFilename(string filename);

	ATTR_VR_ENUM GetVR();

	bool IsByteSwapRequired();

	bool StreamPatternTo(DATA_TF_CLASS& data_transfer);

    UINT NoBasicOffsetTableEntries();
	bool GetBasicOffsetTableEntry(UINT index, UINT32& offset);

	UINT NoDataFragments();
	bool GetDataFragment(UINT index, UINT32& offset, UINT32& length);

    DVT_STATUS Compare(LOG_MESSAGE_CLASS*, OB_VALUE_STREAM_CLASS& refObStream);
};


#endif /* OB_VALUE_STREAM_H */
