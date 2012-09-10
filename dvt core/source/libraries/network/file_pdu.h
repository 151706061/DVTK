// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

//*****************************************************************************
//  DESCRIPTION     :	File PDU class.
//*****************************************************************************
#ifndef FILE_PDU_H
#define FILE_PDU_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Ilog.h"			// Log component interface
#include "Iutility.h"		// Utility component interface

#include "base_io.h"		// Base IO Class


//>>***************************************************************************

class FILE_PDU_CLASS

//  DESCRIPTION     : Class handling a file containing a single PDU.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	FILE	*fileM_ptr;
	string	filenameM;

public:
	FILE_PDU_CLASS();
	FILE_PDU_CLASS(string filename);

	~FILE_PDU_CLASS();

	bool open(string);

	void setFilename(string pduFile){filenameM = pduFile;}

	string getFilename(){return filenameM;}

	bool isOpen() 
		{ return (fileM_ptr == NULL) ? false : true; }

	void close();

	bool write(const BYTE*, UINT);
		
	INT	read(BYTE*, UINT);

	UINT getLength();

	UINT getOffset();
};

//>>***************************************************************************

class FILE_STREAM_CLASS : public BASE_IO_CLASS

//  DESCRIPTION     : Class for reading PDU data from a File Stream.
//					: Each file in the stream contains 1 PDU.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	ARRAY<FILE_PDU_CLASS>	filePduM;
	LOG_CLASS				*loggerM_ptr;
	UINT32					logLengthM;
	UINT					filePDUIndexM;

public:
	FILE_STREAM_CLASS();

	~FILE_STREAM_CLASS();

	void addFileToStream(string filename)
		{
			FILE_PDU_CLASS	filePdu(filename);
			filePduM.add(filePdu);
		}

	virtual	bool writeBinary(const BYTE*, UINT);
		
	virtual	INT	readBinary(BYTE*, UINT);

	void setLogger(LOG_CLASS *logger_ptr)
		{ loggerM_ptr = logger_ptr; }

	void setLogLength(UINT32 length)
		{ logLengthM = length; }

	LOG_CLASS *getLogger() { return loggerM_ptr; }

	UINT getNrOfPDUs() { return filePduM.getSize(); }

	UINT getCurrentPDUFileIndex() { return filePDUIndexM; }
};

#endif /* FILE_PDU_H */

