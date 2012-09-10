// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	TCP/IP Standard Socket class.
//*****************************************************************************
#ifndef SOCKET_H
#define SOCKET_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Ilog.h"			// Log component interface
#include "Iutility.h"		// Utility component interface
#include "base_socket.h"	// Socket base class interface


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************


//>>***************************************************************************

class SOCKET_SOCKET_CLASS : public BASE_SOCKET_CLASS

//  DESCRIPTION     : Class used to handle the standard (non-secure) socket interface.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	int			socketFdM;	
	bool		connectedM;
	bool		listeningM;

	SOCKET_SOCKET_CLASS(const SOCKET_SOCKET_CLASS& socket, int socketFd);

public:
	SOCKET_SOCKET_CLASS();

	SOCKET_SOCKET_CLASS(const SOCKET_PARAMETERS& socketParams, LOG_CLASS* logger_ptr);
		
	virtual	~SOCKET_SOCKET_CLASS();		

	virtual bool socketParametersChanged(const SOCKET_PARAMETERS& socketParams);

	virtual bool connect();

	virtual bool listen();

	virtual bool accept(BASE_SOCKET_CLASS** acceptedSocket_ptr_ptr);

	virtual void close();

	virtual	bool writeBinary(const BYTE*, UINT);
		
	virtual	INT	readBinary(BYTE*, UINT);

	virtual bool isConnected() { return connectedM; }

	virtual bool isSecure() { return false; }
};

#endif /* SOCKET_H */


