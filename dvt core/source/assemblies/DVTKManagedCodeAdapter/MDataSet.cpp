// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#include "StdAfx.h"
#include ".\mdataset.h"
#include "MDIMSEConvertors.h"
#include "MMediaConvertors.h"
#using <mscorlib.dll>

namespace Wrappers
{
	using namespace System::Runtime::InteropServices;

	MDataSet::MDataSet(void)
	{
	}

	MDataSet::~MDataSet(void)
	{
	}

	DvtkData::Dimse::DataSet __gc* MDataSet::ReadFile(System::String __gc* pFileName)
	{
		DCM_DATASET_CLASS* pDcmDataSet = new DCM_DATASET_CLASS();

        char* pAnsiStringFileName = (char*)(void*)Marshal::StringToHGlobalAnsi(pFileName);

		FILE_DATASET_CLASS* pFileDataSet = new FILE_DATASET_CLASS(pAnsiStringFileName);
		pFileDataSet->setStorageMode(SM_AS_MEDIA);
		pFileDataSet->read(pDcmDataSet);

		DvtkData::Dimse::DataSet __gc* dataSet = ManagedUnManagedDimseConvertors::ManagedUnManagedDimseConvertor::Convert(pDcmDataSet);
		dataSet->Filename = pFileName;

		delete pDcmDataSet;
		delete pFileDataSet;
		Marshal::FreeHGlobal(pAnsiStringFileName);

		return(dataSet);
	}

	DvtkData::Media::FileMetaInformation __gc* MDataSet::ReadFMI(System::String __gc* pFileName)
	{
		char* pAnsiStringFileName = (char*)(void*)Marshal::StringToHGlobalAnsi(pFileName);

		MEDIA_FILE_HEADER_CLASS* pFileMetaInfo = new MEDIA_FILE_HEADER_CLASS();

		FILE_DATASET_CLASS* pFileDataSet = new FILE_DATASET_CLASS(pAnsiStringFileName);

		bool ok = pFileDataSet->read();

		if(ok)
		{
			pFileMetaInfo = pFileDataSet->getFileMetaInformation();
		}

		DvtkData::Media::FileMetaInformation __gc* fmi = new DvtkData::Media::FileMetaInformation();
		ManagedUnManagedMediaConvertors::ManagedUnManagedMediaConvertor::Convert(fmi,pFileMetaInfo);
		
		//delete pFileMetaInfo;
		delete pFileDataSet;
		Marshal::FreeHGlobal(pAnsiStringFileName);

		return(fmi);
	}

	System::Boolean MDataSet::WriteFile(
        DvtkData::Media::DicomFile __gc* pDicomFile, System::String __gc* pFileName)
	{
		bool success = false;
        FILE_DATASET_CLASS* fileDataset_ptr = ManagedUnManagedMediaConvertors::ManagedUnManagedMediaConvertor::Convert(pDicomFile, pFileName);
        success = fileDataset_ptr->write();
        delete fileDataset_ptr;
        return success;
	}
}