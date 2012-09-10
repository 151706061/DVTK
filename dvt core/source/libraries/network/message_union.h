// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
#ifndef MESSAGE_UNION_H
#define MESSAGE_UNION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"        // Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class ASSOCIATE_RQ_CLASS;
class ASSOCIATE_AC_CLASS;
class ASSOCIATE_RJ_CLASS;
class RELEASE_RP_CLASS;
class RELEASE_RQ_CLASS;
class ABORT_RQ_CLASS;
class DCM_COMMAND_CLASS;
class DCM_DATASET_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

enum RX_MSG_TYPE
{
   RX_MSG_FAILURE,
   RX_MSG_ASSOCIATE_REQUEST,
   RX_MSG_ASSOCIATE_ACCEPT,
   RX_MSG_ASSOCIATE_REJECT,
   RX_MSG_RELEASE_REQUEST,
   RX_MSG_RELEASE_RESPONSE,
   RX_MSG_ABORT_REQUEST,
   RX_MSG_DICOM_COMMAND,
   RX_MSG_DICOM_COMMAND_DATASET
};


//>>***************************************************************************

class RECEIVE_MESSAGE_UNION_CLASS

//  DESCRIPTION     : Received Message Union Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	enum RX_MSG_TYPE	rxMessageTypeM;
	ASSOCIATE_RQ_CLASS	*associateRqM_ptr;
	ASSOCIATE_AC_CLASS	*associateAcM_ptr;
	ASSOCIATE_RJ_CLASS	*associateRjM_ptr;
	RELEASE_RQ_CLASS	*releaseRqM_ptr;
	RELEASE_RP_CLASS	*releaseRpM_ptr;
	ABORT_RQ_CLASS		*abortRqM_ptr;
	DCM_COMMAND_CLASS	*commandM_ptr;
	DCM_DATASET_CLASS	*datasetM_ptr;

	void cleanup();

public:
	RECEIVE_MESSAGE_UNION_CLASS();
	~RECEIVE_MESSAGE_UNION_CLASS();

	enum RX_MSG_TYPE getRxMsgType();

	void setAssociateRequest(ASSOCIATE_RQ_CLASS*);
	ASSOCIATE_RQ_CLASS *getAssociateRequest();

	void setAssociateAccept(ASSOCIATE_AC_CLASS*);
	ASSOCIATE_AC_CLASS *getAssociateAccept();
	
	void setAssociateReject(ASSOCIATE_RJ_CLASS*);
	ASSOCIATE_RJ_CLASS *getAssociateReject();
	
	void setReleaseRequest(RELEASE_RQ_CLASS*);
	RELEASE_RQ_CLASS *getReleaseRequest();
	
	void setReleaseResponse(RELEASE_RP_CLASS*);
	RELEASE_RP_CLASS *getReleaseResponse();
	
	void setAbortRequest(ABORT_RQ_CLASS*);
	ABORT_RQ_CLASS *getAbortRequest();
	
	void setCommandDataset(DCM_COMMAND_CLASS*, DCM_DATASET_CLASS*);
	DCM_COMMAND_CLASS *getCommand();
	DCM_DATASET_CLASS *getDataset();
	
	void setFailure();
};

#endif /* MESSAGE_UNION_H */