//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef PRIVATE_ATTRIBUTE_H
#define PRIVATE_ATTRIBUTE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"			// Global component interface
#include "Ilog.h"				// Log component interface
#include "Iutility.h"			// Utility component interface
#include "IAttributeGroup.h"	// Attribute Group component interface


//*****************************************************************************
//  LOCAL DECLARATIONS
//*****************************************************************************
#define MAX_FREE_CODES			0x100


//*****************************************************************************
//  FORWARD DECLARATIONS
//*****************************************************************************
class BASE_VALUE_CLASS;


//>>***************************************************************************

class PRIVATE_RECOGNITION_CODE_CLASS

//  DESCRIPTION     : Private attribute recognition code class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
protected:
	bool					streamedInM;
	BASE_VALUE_CLASS		*recognitionCodeM_ptr;
	UINT16					inGroupM;
	UINT16					inElementM;
	UINT16					mappedGroupM;
	UINT16					mappedElementM;

public:
	PRIVATE_RECOGNITION_CODE_CLASS();
	PRIVATE_RECOGNITION_CODE_CLASS(BYTE*, UINT16, UINT16, UINT16, UINT16);
	PRIVATE_RECOGNITION_CODE_CLASS(PRIVATE_RECOGNITION_CODE_CLASS&);

	~PRIVATE_RECOGNITION_CODE_CLASS();

	void setStreamedIn(bool flag)
		{ streamedInM = flag; }

	void setRecognitionCode(BYTE*);

	void setInTag(UINT16 group, UINT16 element)
		{ inGroupM = group; inElementM = element; }

	void setMappedTag(UINT16 group, UINT16 element)
		{ mappedGroupM = group; mappedElementM = element; }

	bool isStreamedIn()
		{ return streamedInM; }

	BYTE *getRecognitionCode();

	UINT16 getInGroup()
		{ return inGroupM; }

	UINT16 getInElement()
		{ return inElementM; }

	UINT16 getMappedGroup()
		{ return mappedGroupM; }

	UINT16 getMappedElement()
		{ return mappedElementM; }

	bool operator = (PRIVATE_RECOGNITION_CODE_CLASS&);
};

//>>***************************************************************************

class PRIVATE_RECOGNITION_CODE_TABLE_CLASS

//  DESCRIPTION     : Private attribute recognition code table class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
protected:
	ARRAY<PRIVATE_RECOGNITION_CODE_CLASS>	recCodeM;

public:
	PRIVATE_RECOGNITION_CODE_TABLE_CLASS();
	PRIVATE_RECOGNITION_CODE_TABLE_CLASS(PRIVATE_RECOGNITION_CODE_TABLE_CLASS&);

	~PRIVATE_RECOGNITION_CODE_TABLE_CLASS();

	void addRecCode(PRIVATE_RECOGNITION_CODE_CLASS recCode)
		{ recCodeM.add(recCode); }

	UINT noRecCodes()
		{ return recCodeM.getSize(); }

	PRIVATE_RECOGNITION_CODE_CLASS &getRecCode(UINT i)
		{ return recCodeM[i]; }

	bool operator = (PRIVATE_RECOGNITION_CODE_TABLE_CLASS&);

    bool setStreamedIn(UINT i, bool streamedIn);

    bool setInTag(UINT i, UINT16 group, UINT16 element);

    UINT16 getMappedGroup(UINT i);

    UINT16 getMappedElement(UINT i);
};

//>>***************************************************************************

class PRIVATE_ATTRIBUTE_HANDLER_CLASS

//  DESCRIPTION     : Private attribute handler class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
protected:
	ARRAY<PRIVATE_RECOGNITION_CODE_TABLE_CLASS>	recCodeTableM;
	bool										freeCodesM[MAX_FREE_CODES];
	LOG_CLASS									*loggerM_ptr;

public:
	PRIVATE_ATTRIBUTE_HANDLER_CLASS();

	~PRIVATE_ATTRIBUTE_HANDLER_CLASS();

	bool install();

	bool remove();

	bool pushRecognitionCodeTable();

	bool popRecognitionCodeTable();

	bool registerRecognitionCode(BYTE*, UINT16, UINT16, UINT16*, UINT16*);

	bool mapTagValue(UINT16, UINT16, UINT16*, UINT16*);

	void setLogger(LOG_CLASS *logger_ptr)
		{ loggerM_ptr = logger_ptr; }
};

#endif /* PRIVATE_ATTRIBUTE_H */

