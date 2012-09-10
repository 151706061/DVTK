//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DCM_ATTRIBUTE_H
#define DCM_ATTRIBUTE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"			// Global component interface
#include "Ilog.h"				// Log component interface
#include "Iutility.h"			// Utility component interface
#include "IAttributeGroup.h"	// Attribute Group interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DCM_VALUE_SQ_CLASS;
class DCM_ATTRIBUTE_GROUP_CLASS;
class PRIVATE_ATTRIBUTE_HANDLER_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
#define MAX_LOGGED_VALUES 34


//>>***************************************************************************

class DCM_ATTRIBUTE_CLASS : public ATTRIBUTE_CLASS

//  DESCRIPTION     : Attribute class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	UINT16							mappedGroupM;	
	UINT16 							mappedElementM;
	TRANSFER_ATTR_VR_ENUM			transferVrM;
	int								nestingDepthM;
	bool							definedLengthM;
	LOG_CLASS						*loggerM_ptr;
	DCM_ATTRIBUTE_GROUP_CLASS		*parentM_ptr;
	PRIVATE_ATTRIBUTE_HANDLER_CLASS	*pahM_ptr;

	bool encodeValue(DATA_TF_CLASS&, UINT32);
	
	bool decodeValue(DATA_TF_CLASS&, UINT32*);
	
	bool findBackslash(BYTE*, UINT, UINT, UINT*);

public:
	DCM_ATTRIBUTE_CLASS();
	DCM_ATTRIBUTE_CLASS(UINT16, UINT16);
	DCM_ATTRIBUTE_CLASS(UINT16, UINT16, ATTR_VR_ENUM);
	DCM_ATTRIBUTE_CLASS(UINT32, ATTR_VR_ENUM);

	~DCM_ATTRIBUTE_CLASS();

	void SetMappedGroup(UINT16 group)
		{ mappedGroupM = group; }

	void SetMappedElement(UINT16 element)
		{ mappedElementM = element; }

	void setDefineGroupLengths(bool);

	void setTransferVR(TRANSFER_ATTR_VR_ENUM);

	void setNestingDepth(int);

	void setDefinedLength(bool);

	bool replaceValue(int index, BASE_VALUE_CLASS *value_ptr);

	UINT16 GetMappedGroup() 
		{ return mappedGroupM; }

	UINT16 GetMappedElement() 
		{ return mappedElementM; }

	TRANSFER_ATTR_VR_ENUM getTransferVR()
		{ return transferVrM; }

	bool getDefinedLength()
		{ return definedLengthM; }

	void addSqValue(DCM_VALUE_SQ_CLASS*);

	bool encode(DATA_TF_CLASS&);

	bool decode(DATA_TF_CLASS&, UINT16, UINT16, UINT32*);
		
	UINT32 getPaddedLength();

    bool operator > (ATTRIBUTE_CLASS&);

	bool operator = (DCM_ATTRIBUTE_CLASS&);

	void setLogger(LOG_CLASS *logger_ptr);

	LOG_CLASS* getLogger()
		{ return loggerM_ptr; }

	void setParent(DCM_ATTRIBUTE_GROUP_CLASS *parent_ptr)
		{ parentM_ptr = parent_ptr; }

	DCM_ATTRIBUTE_GROUP_CLASS* getParent()
		{ return parentM_ptr; }

	void setPAH(PRIVATE_ATTRIBUTE_HANDLER_CLASS *pah_ptr)
		{ pahM_ptr = pah_ptr; }
};


#endif /* DCM_ATTRIBUTE_H */
