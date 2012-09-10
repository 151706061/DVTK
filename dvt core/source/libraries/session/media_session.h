// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

// Media Test Session class.

#ifndef MEDIA_SESSION_H
#define MEDIA_SESSION_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "session.h"			// Base Session include

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************

class MEDIA_SESSION_CLASS : public BASE_SESSION_CLASS

    //  DESCRIPTION     : Media Test Session Class.
    //  INVARIANT       :
    //  NOTES           :
    //<<***************************************************************************
{
private:
    // Test Properties
    bool	validateReferencedFileM;

public:
    MEDIA_SESSION_CLASS();

    ~MEDIA_SESSION_CLASS();

    bool serialise(FILE*);

    bool beginMediaValidation();

    bool validateMediaFile(string);

    bool endMediaValidation();

    bool validate(FILE_DATASET_CLASS*, VALIDATION_CONTROL_FLAG_ENUM);

	bool generateDICOMDIR(vector<string>*);

	void setValidateReferencedFile(bool);

	bool getValidateReferencedFile();

private:
    int getNumberOfDirectoryRecords(string);
};

#endif /* MEDIA_SESSION_H */


