//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "filehead.h"
#include "Iutility.h"			// Utility component interface


//>>===========================================================================

FILEHEAD_CLASS::FILEHEAD_CLASS()

//  DESCRIPTION     : Constructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// constructor activities
	widTypeM = WID_FILEHEAD;

	// set up file preamble and prefix
	preambleValueM = FMI_PREAMBLE_VALUE;
	byteFill(preambleM, preambleValueM, FMI_PREAMBLE_LENGTH);
	byteZero(prefixM, sizeof(prefixM));
	byteCopy(prefixM, (BYTE*) FMI_PREFIX_VALUE, FMI_PREFIX_LENGTH);
	transferSyntaxUidM.set(EXPLICIT_VR_LITTLE_ENDIAN);
	filenameM = "";
	loggerM_ptr = NULL;
}

//>>===========================================================================

FILEHEAD_CLASS::~FILEHEAD_CLASS()

//  DESCRIPTION     : Destructor.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// destructor activities
}

//>>===========================================================================

void FILEHEAD_CLASS::setPreambleValue(char* preamble_ptr)

//  DESCRIPTION     : Method to set the Preamble.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// set up the preamble
	//preambleValueM = value;
	//byteFill(preambleM, preambleValueM, FMI_PREAMBLE_LENGTH);
	byteZero(preambleM, sizeof(preambleM));
	UINT length = byteStrLen((BYTE*) preamble_ptr);
	length = (length > FMI_PREAMBLE_LENGTH) ? FMI_PREAMBLE_LENGTH : length;
	byteCopy(preambleM, (BYTE*) preamble_ptr, length);
}

//>>===========================================================================

void FILEHEAD_CLASS::setPrefix(char *prefix_ptr)

//  DESCRIPTION     : Method to set the Dicom Prefix.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// set up the Dicom prefix
	byteZero(prefixM, sizeof(prefixM));
	UINT length = byteStrLen((BYTE*) prefix_ptr);
	length = (length > FMI_PREFIX_LENGTH) ? FMI_PREFIX_LENGTH : length;
	byteCopy(prefixM, (BYTE*) prefix_ptr, length);
}

//>>===========================================================================

bool FILEHEAD_CLASS::write(bool autoCreateDirectory)

//  DESCRIPTION     : Method to stream the media file preamble and DICOM prefix
//					  into the given file.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           : 
//<<===========================================================================
{
	// check the an absolute pathname has been given
	if (!isAbsolutePath(filenameM))
	{
		// see if a results root is defined
		if (loggerM_ptr)
		{
			// see if the path has been defined
			string pathname = loggerM_ptr->getStorageRoot();
			string filename = filenameM;

			if (pathname.length())
			{
				// set up filename by including path
                filenameM = pathname;
                if (pathname[pathname.length()-1] != '\\') filenameM += "\\";
                filenameM += filename;
			}
		}
	}

	if (loggerM_ptr)
	{
		loggerM_ptr->text(LOG_DEBUG, 1, "About to write FILE_HEAD to \"%s\"", filenameM.c_str()); 
	}

	if(autoCreateDirectory) //autocreate stuff should be here
	{
		createDirectory(filenameM);
	}

	// set up the file transfer
	FILE_TF_CLASS *fileTf_ptr = new FILE_TF_CLASS(filenameM, "wb");
	fileTf_ptr->setTsCode(TS_EXPLICIT_VR | TS_LITTLE_ENDIAN, EXPLICIT_VR_LITTLE_ENDIAN);

	// cascade the logger
	fileTf_ptr->setLogger(loggerM_ptr);

	// check the file is open
	if (!fileTf_ptr->isOpen()) 
	{
		if (loggerM_ptr)
		{
			loggerM_ptr->text(LOG_ERROR, 1, "Can't write FILE_HEAD to \"%s\"", filenameM.c_str());
			loggerM_ptr->text(LOG_NONE, 1, "Check directory path exists.");
		}

		delete fileTf_ptr;
		return false;
	}

	// write file preamble and DICOM prefix to file
	(void) fileTf_ptr->writeBinary(preambleM, FMI_PREAMBLE_LENGTH);
	(void) fileTf_ptr->writeBinary(prefixM, FMI_PREFIX_LENGTH);
	
	// clean up the file transfer
	delete fileTf_ptr;

	// encode file meta information
	return true;
}

//>>===========================================================================

bool FILEHEAD_CLASS::updateWid(BASE_WAREHOUSE_ITEM_DATA_CLASS *wid_ptr)

//  DESCRIPTION     : Update this file head with the contents of the file head given.
//  PRECONDITIONS   :
//  POSTCONDITIONS  :
//  EXCEPTIONS      : 
//  NOTES           :
//<<===========================================================================
{
	bool result = true;

	FILEHEAD_CLASS *updateFilehead_ptr = static_cast<FILEHEAD_CLASS*>(wid_ptr);

	// copy all fields - assume update of all values
	preambleValueM = updateFilehead_ptr->preambleValueM;
	byteFill(preambleM, preambleValueM, FMI_PREAMBLE_LENGTH);
	byteZero(prefixM, sizeof(prefixM));
	byteCopy(prefixM, updateFilehead_ptr->prefixM, FMI_PREFIX_LENGTH);
	transferSyntaxUidM = updateFilehead_ptr->transferSyntaxUidM;

	// return result
	return result;
}
