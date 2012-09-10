// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace Wrappers
{
	public __gc class MDataSet
	{
	public:
		MDataSet(void);

		~MDataSet(void);

		static DvtkData::Dimse::DataSet __gc* ReadFile(System::String __gc* pFileName);

		static DvtkData::Media::FileMetaInformation __gc* ReadFMI(System::String __gc* pFileName);

		static System::Boolean WriteFile(
            DvtkData::Media::DicomFile __gc* pDicomFile, System::String __gc* pFileName);
	};
}
