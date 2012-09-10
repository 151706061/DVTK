// Part of Dvtk Libraries - Internal Native Library Code
// Copyright © 2001-2005
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#ifndef SPECIFIC_CHARACTER_SET_H
#define SPECIFIC_CHARACTER_SET_H

//*****************************************************************************
//  EXTERNAL DECLARATIONS
//*****************************************************************************
#include "Iglobal.h"


//>>***************************************************************************

class SPECIFIC_CHARACTER_SET_CLASS

//  DESCRIPTION     : Specific Character Set description used during extended character
//                  : set validation.
//  INVARIANT       :
//  NOTES           :
//<<***************************************************************************
{
	public:
		SPECIFIC_CHARACTER_SET_CLASS();
        ~SPECIFIC_CHARACTER_SET_CLASS();

		void   ClearCharacterSets();

		void   AddCharacterSet(UINT val_nr, const string& char_set);

		int    GetNrCharacterSets();

		string GetCharacterSetName(UINT nr);

		bool   IsValidCharacterSet(const string& def_term);
	
	private:
		typedef struct
		{
			UINT   value_nrM;
			string character_setM;
		} CHARACTER_SET_STRUCT;

		vector<CHARACTER_SET_STRUCT> character_setsM; // list of defined terms of available
		                                              // character sets
		                                              // the contents are determined by the
		                                              // specific character set attribute
};

#endif /* SPECIFIC_CHARACTER_SET_H */
