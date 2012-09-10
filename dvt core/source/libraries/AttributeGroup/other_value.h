// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef OTHER_VALUE_H
#define OTHER_VALUE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include <string>

#include "Iglobal.h"      // Global component interface
#include "base_value.h"

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class ATTRIBUTE_GROUP_CLASS;

//*****************************************************************************
//  OTHER_VALUE_CLASS definition
//*****************************************************************************
class OTHER_VALUE_CLASS : public BASE_VALUE_CLASS
{
public:
                    OTHER_VALUE_CLASS();
    virtual         ~OTHER_VALUE_CLASS();
    DVT_STATUS      Set(ATTRIBUTE_GROUP_CLASS * parent);
    DVT_STATUS      Set(UINT32);
    DVT_STATUS      Set(string filename);
	void            SetBitsAllocated(UINT16 bitsAllocated);
    void            SetSamplesPerPixel(UINT16 samplesPerPixel);
    void            SetPlanarConfiguration(UINT16 planarConfiguration);

    DVT_STATUS      Add (UINT32);

    DVT_STATUS      Get(ATTRIBUTE_GROUP_CLASS **parent, int index=0);
	DVT_STATUS		Get(UINT32 &value);
    DVT_STATUS      Get(string &filename, bool stripped=false);
    DVT_STATUS      Get(UINT32 index, UINT32 &value);
	UINT16          GetBitsAllocated();
    UINT16          GetSamplesPerPixel();
    UINT16          GetPlanarConfiguration();
	virtual	bool	IsCompressed();

    void            SetLogger(LOG_CLASS*);
    LOG_CLASS       *GetLogger();

protected:
    LOG_CLASS               *loggerM_ptr;
    ATTRIBUTE_GROUP_CLASS   *parentGroupM_ptr;

public:
    bool                    lengthOkM;
    string                  filenameM;
    UINT32                  lengthM;
    UINT16                  bitsAllocatedM;
    UINT16                  samplesPerPixelM;
    UINT16                  planarConfigurationM;
    UINT32                  indexM;
    UINT32                  rowsM;
    UINT32                  columnsM;
    UINT32                  start_valueM;
    UINT32                  rows_incrementM;
    UINT32                  columns_incrementM;
    UINT32                  rows_sameM;
    UINT32                  columns_sameM;
};

#endif /* OTHER_VALUE_H */
