//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_PROPERTIES_HPP
#define ACSE_PROPERTIES_HPP

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "IGlobal.h"

//*****************************************************************************
//  CONSTANTS AND TYPE DEFINITIONS
//*****************************************************************************

//>>***************************************************************************
class ACSE_PROPERTIES_CLASS
//  DESCRIPTION     : Class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	ACSE_PROPERTIES_CLASS();
	~ACSE_PROPERTIES_CLASS();
	
	void	setCalledAeTitle (const string calledAeTitle);
	string	getCalledAeTitle (void);
	
	void	setCallingAeTitle (const string callingAeTitle);
	string	getCallingAeTitle (void);
	
	void	setMaximumLengthReceived (UINT32 maximumLengthReceived);
	UINT32	getMaximumLengthReceived (void);
	
	void	setImplementationClassUid (const string implementationClassUid);
	string	getImplementationClassUid (void);
	
	void	setImplementationVersionName (const string implementationVersionName);
	string	getImplementationVersionName (void);
	
protected:
	
private:
	string		calledAeTitleM;
	string		callingAeTitleM;
	UINT32		maximumLengthReceivedM;
	string		implementationClassUidM;
	string		implementationVersionNameM;
};

#endif /* ACSE_PROPERTIES_HPP */
