// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

#include "ActivityReportingAdapter.h"
#include "CountingAdapter.h"

namespace ManagedUnManagedDimseValidationResultsConvertors
{
    using namespace DvtkData::Validation;

    //>>***************************************************************************

    class ManagedUnManagedDimseValidationResultsConvertor

        //  DESCRIPTION     : Managed Unmanaged DIMSE Validation Results Convertor Class.
        //  INVARIANT       :
        //  NOTES           :
        //<<***************************************************************************
    {
    private:
        //
        // static enum conversion from unmanaged to managed
        //
        static 
            DvtkData::Validation::DataElementType
            Convert(::ATTR_TYPE_ENUM dataElementType);

    private:
        //
        // static enum conversion from unmanaged to managed
        //
        static 
            DvtkData::Validation::DirectoryRecordType
            Convert(::DICOMDIR_RECORD_TYPE_ENUM directoryRecordType);

    private:
        DvtkData::Validation::MessageType
            Convert(Wrappers::WrappedValidationMessageLevel);

    private:
        //
        // static enum conversion from unmanaged to managed
        //
        static 
            DvtkData::Validation::ModuleUsageType
            Convert(::MOD_USAGE_ENUM moduleUsage);

    public:
        //
        // Constructor
        //
        ManagedUnManagedDimseValidationResultsConvertor(void);

    public:
        //
        // Destructor
        //
        ~ManagedUnManagedDimseValidationResultsConvertor(void);

        //
        // Unmanaged to Managed
        //
    public:
        DvtkData::Validation::ValidationObjectResult __gc*
            Convert(VAL_OBJECT_RESULTS_CLASS *pUMValidationObjectResult, UINT flags);

    private:
        DvtkData::Validation::SubItems::ValidationAttributeGroupResult __gc*
            Convert(VAL_ATTRIBUTE_GROUP_CLASS *pUMValidationAttributeGroupResult, UINT flags);

    public:
        DvtkData::Validation::ValidationDirectoryRecordResult __gc*
            Convert(RECORD_RESULTS_CLASS *pUMValidationDirectoryRecordResult, UINT flags);

    public:
        DvtkData::Validation::TypeSafeCollections::ValidationDirectoryRecordLinkCollection __gc*
            Convert(RECORD_LINK_VECTOR* pUMRecordLinkVector);

    private:
        DvtkData::Validation::SubItems::ValidationAttributeResult __gc*
            Convert(VAL_ATTRIBUTE_CLASS *pUMValidationAttributeResult, UINT flags);

    private:
        DvtkData::Validation::SubItems::ValidationValueResult __gc*
            Convert(::VAL_BASE_VALUE_CLASS *pUMValBaseValue, ::ATTR_VR_ENUM vr, UINT flags);

    public:
        void set_Rules(System::Uri __gc* pRulesUri);
    private:
        gcroot<System::Uri*> m_pRulesUri;
		gcroot<Wrappers::ICountingTarget*> m_pCountingTarget;

    public:
        void set_CountingTarget(Wrappers::ICountingTarget __gc* pCountingTarget);
    };
}
