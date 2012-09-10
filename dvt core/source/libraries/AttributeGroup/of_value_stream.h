// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//  DICOM Other Float Attribute Value Stream Class.
#ifndef OF_VALUE_STREAM_H
#define OF_VALUE_STREAM_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "other_value_stream.h"

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
class LOG_MESSAGE_CLASS;


//>>***************************************************************************

class OF_VALUE_STREAM_CLASS : public OTHER_VALUE_STREAM_CLASS

//  DESCRIPTION     : OF value stream class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	// private methods

public:
	OF_VALUE_STREAM_CLASS();
	~OF_VALUE_STREAM_CLASS();

	bool SetFilename(string filename);

	ATTR_VR_ENUM GetVR();

	bool IsByteSwapRequired();

	void SwapBytes(BYTE* buffer_ptr, UINT32 length);

	bool StreamPatternTo(DATA_TF_CLASS& data_transfer);

    DVT_STATUS Compare(LOG_MESSAGE_CLASS*, OF_VALUE_STREAM_CLASS&);
};


#endif /* OF_VALUE_STREAM_H */
