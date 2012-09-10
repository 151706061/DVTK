// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
#ifndef DATA_TF_PDU_H
#define DATA_TF_PDU_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"        // Global component interface
#include "pdu.h"            // Raw PDU

//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;


// PDV data structure
struct PDV_DATA
{
	UINT32	lengthM;
	BYTE	pcIdM;
	BYTE	mchM;
	BYTE	*dataM;
};

//>>***************************************************************************

class DATA_TF_PDU_CLASS : public PDU_CLASS

//  DESCRIPTION     : Data TF PDU Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	PDV_DATA	currentPdvM;

	LOG_CLASS	*loggerM_ptr;

protected:

public:
	DATA_TF_PDU_CLASS();
	DATA_TF_PDU_CLASS(UINT32);

	~DATA_TF_PDU_CLASS();		

	bool moveToFirstPdv(LOG_CLASS *logger_ptr = NULL);

	bool moveToNextPdv(LOG_CLASS *logger_ptr = NULL);

	bool updateFirstPdv(UINT32, BYTE, BYTE);

	bool readBody(BASE_SOCKET_CLASS*);

	bool isThereMorePdvData();

	bool isLast();

	void setPdvLength(UINT32 length)
		{ currentPdvM.lengthM = length + sizeof(currentPdvM.pcIdM) + sizeof(currentPdvM.mchM); }

	void setPresentationContextId(BYTE pcId)
		{ currentPdvM.pcIdM = pcId; }

	void setMessageControlHeader(BYTE messageControlHeader)
		{ currentPdvM.mchM = messageControlHeader; }

	UINT32 getCurrentPdvLength()
		{ return currentPdvM.lengthM - sizeof(currentPdvM.pcIdM) - sizeof(currentPdvM.mchM); }

	BYTE getCurrentPresentationContextId()
		{ return currentPdvM.pcIdM; }

	BYTE getCurrentMessageControlHeader()
		{ return currentPdvM.mchM; }

	BYTE *getCurrentPdvData()
		{ return currentPdvM.dataM; }

	void setLogger(LOG_CLASS *logger_ptr) { loggerM_ptr = logger_ptr; }
};

#endif /* DATA_TF_PDU_H */