// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

namespace ManagedUnManagedMediaConvertors
{
    using namespace DvtkData::Media;

    //>>***************************************************************************

    class ManagedUnManagedMediaConvertor

        //  DESCRIPTION     : Managed Unmanaged Media Convertor Class.
        //  INVARIANT       :
        //  NOTES           :
        //<<***************************************************************************
    {
    public:
        ManagedUnManagedMediaConvertor(void);
        ~ManagedUnManagedMediaConvertor(void);

        //
        // Unmanaged to Managed
        //
    public:
        static 
            DvtkData::Media::DicomFile __gc* 
            Convert(::FILE_DATASET_CLASS *pFILE_DATASET_CLASS);

    private:
        static
            DvtkData::Media::FileHead __gc*
            Convert(::MEDIA_FILE_HEADER_CLASS *pMEDIA_FILE_HEADER_CLASS);

    public:
        static
            void
            Convert(
            /*dst*/ DvtkData::Media::FileMetaInformation __gc *pFileMetaInformation,
            /*src*/ ::MEDIA_FILE_HEADER_CLASS *pUMMEDIA_FILE_HEADER_CLASS);

            //
            // Managed to Unmanaged
            //
    public:
        static 
            ::FILE_DATASET_CLASS* 
            Convert(DvtkData::Media::DicomFile __gc* pDicomFile, System::String __gc* pFileName);

    private:
        static
            void
            Convert(
            /*dst*/ MEDIA_FILE_HEADER_CLASS *pUMMEDIA_FILE_HEADER_CLASS, 
            /*src*/ DvtkData::Media::FileHead __gc *pFileHead);

    private:
        static
            void
            Convert(
            /*dst*/ ::MEDIA_FILE_HEADER_CLASS *pUMMEDIA_FILE_HEADER_CLASS, 
            /*src*/ DvtkData::Media::FileMetaInformation __gc *pFileMetaInformation);
    };

}
