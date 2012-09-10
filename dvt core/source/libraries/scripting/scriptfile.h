//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************

#ifndef SCRIPT_FILE_H
#define SCRIPT_FILE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface
#include "Iutility.h"		// Utility component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class LOG_CLASS;
class SCRIPT_SESSION_CLASS;


#define MYPARSER			PARSER_INSTANCE_CLASS::instance()

#define MAX_PARSERS			5

//>>***************************************************************************

class PARSER_INSTANCE_CLASS

//  DESCRIPTION     : Parser instance (Singleton) class
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	static PARSER_INSTANCE_CLASS	*instanceM_ptr;		// Singleton
	bool							freeM[MAX_PARSERS];

	void initialise();

protected:
	PARSER_INSTANCE_CLASS();

public:
	static PARSER_INSTANCE_CLASS *instance();

	void cleanup();

	int allocateParser();

	void freeParser(int);

	bool isParserAvailable();
};


//>>***************************************************************************

class DICOM_SCRIPT_CLASS : public BASE_FILE_CLASS

//  DESCRIPTION     : DICOMScript Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	int						parserInstanceM;
	SCRIPT_SESSION_CLASS	*sessionM_ptr;
	LOG_CLASS				*loggerM_ptr;
	bool					inSuperScriptM;

public:
	DICOM_SCRIPT_CLASS(SCRIPT_SESSION_CLASS*, string);

	~DICOM_SCRIPT_CLASS();

	bool parse();
	bool execute();

	void setInSuperScript(bool flag)
		{ inSuperScriptM = flag; }
};

//>>***************************************************************************

class DICOM_SUPER_SCRIPT_CLASS : public BASE_FILE_CLASS 

//  DESCRIPTION     : DICOMScript Class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
private:
	SCRIPT_SESSION_CLASS	*sessionM_ptr;
	LOG_CLASS				*loggerM_ptr;

	int cleanUpLine(char*);

public:
	DICOM_SUPER_SCRIPT_CLASS(SCRIPT_SESSION_CLASS*, string);

	~DICOM_SUPER_SCRIPT_CLASS();

	bool parse();
	bool execute();
};


#endif /* SCRIPT_FILE_H */


