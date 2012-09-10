// Part of DVTKManagedCodeAdapter.dll - .NET Managed Extension for C++ library that wraps core DVTK C/C++ routines
// Copyright � 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

#pragma once

#include "ActivityReportingAdapter.h"
#include "CountingAdapter.h"

namespace ManagedUnManagedDulValidationResultsConvertors
{
    using namespace DvtkData::Validation;

    //>>***************************************************************************

    class ManagedUnManagedDulValidationResultsConvertor

        //  DESCRIPTION     : Managed Unmanaged DUL Validation Results Convertor Class.
        //  INVARIANT       :
        //  NOTES           :
        //<<***************************************************************************
    {
    private:
        DvtkData::Validation::MessageType
            Convert(Wrappers::WrappedValidationMessageLevel);

    public:
        //
        // Constructor
        //
        ManagedUnManagedDulValidationResultsConvertor(void);
    public:
        //
        // Destructor
        //
        ~ManagedUnManagedDulValidationResultsConvertor(void);

        //
        // Unmanaged to Managed
        //
    public:
        DvtkData::Validation::ValidationAssociateRq __gc* 
            Convert(ASSOCIATE_RQ_VALIDATOR_CLASS *pUMAssociateRq);

    public:
        DvtkData::Validation::ValidationAssociateAc __gc* 
            Convert(ASSOCIATE_AC_VALIDATOR_CLASS *pUMAssociateAc);

    public:
        DvtkData::Validation::ValidationAssociateRj __gc* 
            Convert(ASSOCIATE_RJ_VALIDATOR_CLASS *pUMAssociateRj);

    public:
        DvtkData::Validation::ValidationReleaseRq __gc* 
            Convert(RELEASE_RQ_VALIDATOR_CLASS *pUMReleaseRq);

    public:
        DvtkData::Validation::ValidationReleaseRp __gc* 
            Convert(RELEASE_RP_VALIDATOR_CLASS *pUMReleaseRp);

    public:
        DvtkData::Validation::ValidationAbortRq __gc* 
            Convert(ABORT_RQ_VALIDATOR_CLASS *pUMAbortRq);

    private:
        DvtkData::Validation::SubItems::ValidationAcseUserInformation __gc*
            Convert(ACSE_USER_INFORMATION_VALIDATOR_CLASS *pUMUserInformation);

    private:
        DvtkData::Validation::SubItems::ValidationAcseScpScuRoleSelect __gc*
            Convert(ACSE_SCP_SCU_ROLE_SELECT_VALIDATOR_CLASS *pUMScpScuRoleSelect);

    private:
        DvtkData::Validation::SubItems::ValidationAcseAsynchronousOperationWindow __gc*
            Convert(ACSE_ASYNCHRONOUS_OPERATION_WINDOW_VALIDATOR_CLASS *pUMAsynchronousOperationWindow);

    private:
        DvtkData::Validation::SubItems::ValidationAcseSopClassExtended __gc*
            Convert(ACSE_SOP_CLASS_EXTENDED_VALIDATOR_CLASS *pUMSopClassExtended);

    private:
        DvtkData::Validation::SubItems::ValidationAcseUserIdentityNegotiation __gc*
            Convert(ACSE_USER_IDENTITY_NEGOTIATION_VALIDATOR_CLASS *pUMUserIdentityNegotiation);

    private:
        DvtkData::Validation::SubItems::ValidationAcseParameter __gc* 
            Convert(ACSE_PARAMETER_CLASS *pUMAcseParameter);

    public:
        void set_Rules(System::Uri __gc* pRulesUri);
    private:
        gcroot<System::Uri*> m_pRulesUri;
		gcroot<Wrappers::ICountingTarget*> m_pCountingTarget;

    public:
        void set_CountingTarget(Wrappers::ICountingTarget __gc* pCountingTarget);
    };
}
