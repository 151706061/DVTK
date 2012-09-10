//*****************************************************************************
// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.
//*****************************************************************************
#ifndef ACSE_PARAMETER_HPP
#define ACSE_PARAMETER_HPP

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"
#include "valdefinitions.h"


//>>***************************************************************************

class ACSE_PARAMETER_CLASS 

//  DESCRIPTION     : Base class for ACSE parameter validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
public:
	virtual ~ACSE_PARAMETER_CLASS() = 0;
		
	void setName(const string);
	
	void setValue(const string);
	void setValue(char*, ...);
	string getValue();
	
    void setMeaning(const string);
	string getMeaning();
	
	void addMessage(const string);
	void addMessage(const int, const string);
	void addMessage(char*, ...);
	void addMessage(const int, char*, ...);
	
	int noMessages();
    UINT32 getIndex(const int);
	UINT32 getCode(const int);
	string getMessage(const int);
		
	bool validate(string, string);
	
protected:
	string			nameM;
	string			valueM;
    string          meaningM;
	bool			quotedValueM;
	
	bool checkIntegerSyntax(int);
	
	bool checkStringDifferences(char*, char*, int, bool, bool);
	
	bool checkIntegerReference(string);
	
	virtual bool checkSyntax() = 0;
	
	virtual bool checkRange() = 0;
	
	virtual bool checkReference(string) = 0;
	
private:
	vector<CODE_MESSAGE_STRUCT> messagesM;
}; 

#endif /* ACSE_PARAMETER_HPP */
