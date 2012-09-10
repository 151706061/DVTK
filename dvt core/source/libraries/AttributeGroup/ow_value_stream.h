// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef OW_VALUE_STREAM_H
#define OW_VALUE_STREAM_H


//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "other_value_stream.h"


//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
class LOG_MESSAGE_CLASS;
class OB_VALUE_STREAM_CLASS;

//>>***************************************************************************

class OW_VALUE_STREAM_CLASS : public OTHER_VALUE_STREAM_CLASS

//  DESCRIPTION     : OW value stream class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	// private methods

public:
	OW_VALUE_STREAM_CLASS();
	~OW_VALUE_STREAM_CLASS();

	bool SetFilename(string filename);

	ATTR_VR_ENUM GetVR();

	bool IsByteSwapRequired();

	bool StreamPatternTo(DATA_TF_CLASS& data_transfer);

    DVT_STATUS Compare(LOG_MESSAGE_CLASS*, OB_VALUE_STREAM_CLASS&);
    DVT_STATUS Compare(LOG_MESSAGE_CLASS*, OW_VALUE_STREAM_CLASS&);
};


#endif /* OW_VALUE_STREAM_H */
