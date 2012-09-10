//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef DEF_AE_H
#define DEF_AE_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"		// Global component interface


//*****************************************************************************
//  FORWARD DECLARATION
//*****************************************************************************
class DEF_INSTANCE_CLASS;
class DEF_FILE_CONTENT_CLASS;
class DEF_DETAILS_CLASS;
class DEF_METASOPCLASS_CLASS;
class DEF_SOPCLASS_CLASS;
class DEF_DATASET_CLASS;


//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************
typedef vector<DEF_INSTANCE_CLASS*>	DEF_INSTANCE_VECTOR;


//>>***************************************************************************

class DEF_AE_CLASS

//  DESCRIPTION     : AE definition class.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	DEF_AE_CLASS();  
	DEF_AE_CLASS(const string, const string);
    ~DEF_AE_CLASS();       
          
	void SetName(const string);

	void SetVersion(const string);

    string GetName();

	string GetVersion();

	bool AddInstance(const string, DEF_FILE_CONTENT_CLASS*);

	bool RemoveInstance(const string);

	bool ContainsInstance(const string);

	bool GetDetails(const string, DEF_DETAILS_CLASS&);

	UINT GetNrMetaSops();

	DEF_METASOPCLASS_CLASS* GetMetaSop(UINT);
    
	DEF_METASOPCLASS_CLASS* GetMetaSopByUid(const string);

	DEF_SOPCLASS_CLASS* GetSopByUid(const string);
    
	DEF_SOPCLASS_CLASS* GetSopByName(const string);

	DEF_SOPCLASS_CLASS* GetSopByFilename(const string);
	
	DEF_DATASET_CLASS* GetDataset(const string);
	DEF_DATASET_CLASS* GetDataset(DIMSE_CMD_ENUM, const string);

private:
	string				nameM;
	string				versionM;
	DEF_INSTANCE_VECTOR instanceM;
};


#endif /* DEF_AE_H */
