// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	Sniffer PDUs class.
//*****************************************************************************
#ifndef SNIFFER_PDUS_H
#define SNIFFER_PDUS_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Ilog.h"			// Log component interface
#include "Iutility.h"		// Utility component interface

#include "accepted.h"       // Accepted Presentation Contexts
#include "file_pdu.h"		// File PDU Class
#include "network_tf.h"     // Network Data Transfer Class


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class RECEIVE_MESSAGE_UNION_CLASS;
class AE_SESSION_CLASS;


//>>***************************************************************************

class SNIFFER_PDUS_CLASS

//  DESCRIPTION     : Class handling the PDUS sniffed (into files) from the network.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	FILE_STREAM_CLASS	*fileStreamM_ptr;
	NETWORK_TF_CLASS	networkTransferM;
	ACCEPTED_PC_CLASS	acceptedPCM;
	string				sopClassUidM;
	string				sopInstanceUidM;
	LOG_CLASS			*loggerM_ptr;
    BASE_SERIALIZER     *serializerM_ptr;

private:
	PDU_CLASS *getPdu();

	bool getCommandDataset(DCM_COMMAND_CLASS **, DCM_DATASET_CLASS **);

	bool getCommand(DCM_COMMAND_CLASS **);

	bool getDataset(DCM_DATASET_CLASS **);

public:
	SNIFFER_PDUS_CLASS();

	~SNIFFER_PDUS_CLASS();

	void addFileToStream(string filename);

	void removeFileStream();

	bool getMessage(RECEIVE_MESSAGE_UNION_CLASS**);

	void setStorageMode(STORAGE_MODE_ENUM storageMode)
		{ networkTransferM.setStorageMode(storageMode); }

	void setLogger(LOG_CLASS *logger_ptr)
		{ loggerM_ptr = logger_ptr;
		  acceptedPCM.setLogger(logger_ptr);
		}

	LOG_CLASS *getLogger() { return loggerM_ptr; }

	void setSerializer(BASE_SERIALIZER *serializer_ptr)
		{ serializerM_ptr = serializer_ptr; }
};

#endif /* SNIFFER_PDUS_H */


