'===============================================================================
' Filename:     AppendCaseWorkflowScript.vbs
'===============================================================================
'
'Description:   VBScript showing basic test case execution framework for IHE
'				integration profiles - acquisition modality.
'
'===============================================================================
'
' System namespaces
'
Imports System
Imports System.IO
Imports System.Windows.Forms

'
' DVTK namespaces
'
Imports DvtkData
Imports DvtkData.Dimse
Imports Dvtk.IheActors.Bases
Imports Dvtk.IheActors.Actors
Imports Dvtk.IheActors.Dicom
Imports Dvtk.IheActors.Hl7
Imports Dvtk.IheActors.IheFramework

'===============================================================================
'
' Test Case Class
'
Class TestCase
    Inherits TestCaseBase

    '
    ' Test Case Specific SetUp subroutine
    '
    Public Sub SetUp()
		'
		' SetUp Test Case
		'
				
		'
		' Add filters (as Tag Values) to the Comparator
		'
		' Universal Match on these DICOM Tags
'		IheFramework.AddComparisonTagValueFilter(New DicomTagValue(Tag.PATIENT_ID))
'		IheFramework.AddComparisonTagValueFilter(New DicomTagValue(Tag.ACCESSION_NUMBER))

		' Single Value Match on these DICOM Tags
'		IheFramework.AddComparisonTagValueFilter(New DicomTagValue(Tag.PATIENTS_BIRTH_DATE, "19000101"))
'		IheFramework.AddComparisonTagValueFilter(New DicomTagValue(Tag.REQUESTED_PROCEDURE_ID, "RPQ2"))
'		IheFramework.AddComparisonTagValueFilter(New DicomTagValue(Tag.PATIENT_ID, "SC-I1"))
		
		' Add User Defined default values for these DICOM Tags
'		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.PERFORMED_STATION_NAME, "MyPerformedStationName"))
'		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.PERFORMED_LOCATION, "MyPerformedLocation"))
		
		' Add default value for these DICOM Tags - no in-built value defined
'		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.COMMENTS_ON_THE_PERFORMED_PROCEDURE_STEPS, "My comments on PPS"))
		
		' Delete In-built default values for these DICOM Tags
'		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValueDelete(Tag.PERFORMED_STATION_AE_TITLE))
'		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValueDelete(Tag.PERFORMED_PROCEDURE_STEP_ID))

		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValueAutoIncrement(AffectedEntityEnum.PerformedProcedureStepEntity, Tag.PERFORMED_PROCEDURE_STEP_ID , "Prefix", 0, 1, 3))

			
    End Sub ' SetUp()

    '
    ' Test Case Specific Execute subroutine
    '
    Public Sub Execute()
		'
		' Execute Test Case
		'
				
		'
		' Get the Acquisition Modality
		'
 		Dim actorName As ActorName = New ActorName(ActorTypeEnum.AcquisitionModality, "AM_ID1")				
		Dim acquisitionModality As AcquisitionModalityActor = CType(IheFramework.GetActor(actorName), AcquisitionModalityActor)		
		If Not (acquisitionModality Is Nothing) Then
		
		    ' Set up worklist item - storage directory mapping
		    acquisitionModality.MapWorklistItemToStorageDirectory.MapTag = Tag.SCHEDULED_PROCEDURE_STEP_DESCRIPTION
		    acquisitionModality.MapWorklistItemToStorageDirectory.AddMapping("Default", IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\data\\storageData")

            ' Define a storage directory - location of images to send
			Dim storageDirectory As String = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\data\\acquisitionModality\\default"
		    
			' Get a modality worklist
			Dim queryTags As TagValueCollection = New TagValueCollection()
			queryTags.Add(New DicomTagValue(Tag.SCHEDULED_PROCEDURE_STEP_START_DATE, System.DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)))
			acquisitionModality.SendQueryModalityWorklist(queryTags)

			' Handle each of the returned worklist items - iterate over acquisitionModality.ModalityWorklistItems
			Dim modalityWorklistItem As DicomQueryItem						
			For Each modalityWorklistItem In acquisitionModality.ModalityWorklistItems

			    ' Send the MPPS in-progress
			    acquisitionModality.SendNCreateModalityProcedureStepInProgress(modalityWorklistItem)

			    ' Send the images - read images from storageDirectory and convert
			    acquisitionModality.SendModalityImagesStored(storageDirectory, modalityWorklistItem, True)
		
			    ' Send the mpps completed
			    acquisitionModality.SendNSetModalityProcedureStepCompleted()

			    ' Send the storage commitment
			    acquisitionModality.SendStorageCommitment(false, 0)

			    ' Update / change default PPS attributes before starting append case
			    IheFramework.UpdateInstantiatedDefaultTagValue(New DicomTagValue(Tag.PERFORMED_PROCEDURE_STEP_DESCRIPTION, "UpdatedDesc"))
			    IheFramework.UpdateInstantiatedDefaultTagValue(New DicomTagValue(Tag.PROTOCOL_NAME, "UpdatedProtName"))

			    ' Send the MPPS in-progress
			    acquisitionModality.SendNCreateModalityProcedureStepInProgress(modalityWorklistItem)

			    ' Send the images - read images from storageDirectory and convert
			    acquisitionModality.SendModalityImagesStored(storageDirectory, modalityWorklistItem, True)
		
			    ' Send the mpps completed
			    acquisitionModality.SendNSetModalityProcedureStepCompleted()

			    ' Send the storage commitment
			    acquisitionModality.SendStorageCommitment(false, 0)
			Next
			
			'
			' Pend the script until the test is completed by
			' an outside entity
			'
			IheFramework.PendTestCompletion()

		End If	
    End Sub ' Execute()

    '
    ' Test Case New subroutine
    '
    Public Sub New()
    End Sub ' New()

End Class ' TestCase


'===============================================================================
'
' Test Case Base Class
'
MustInherit Class TestCaseBase

	Private Dim _iheFramework As IheFramework = Nothing

	'
	' Get Ihe Framework Property
	'
	Public ReadOnly Property IheFramework As IheFramework
		Get
			IheFramework = _iheFramework
		End Get
	End Property
   	
    '
    ' Test Case Start subroutine
    '
    Public Sub Start()
		'
		' Start the test
		'
		_iheFramework.StartTest()	
    End Sub ' Start()

    '
    ' Test Case Initialize subroutine
    '
    Public Sub Initialize(ByVal configurationFilename As String)
		'
		' Initialize the test
		'
		_iheFramework = New IheFramework("Scheduled Workflow")	
		_iheFramework.Config.Load(configurationFilename)
		_iheFramework.ApplyConfig()
		_iheFramework.OpenResults()
    End Sub ' Initialize()

    '
    ' Test Case Finalize subroutine
    '
    Public Sub Finalize()
		'
		' Finalize the test
		'
		_iheFramework.StopTest()
		_iheFramework.EvaluateTest()
		_iheFramework.CleanUpCurrentWorkingDirectory()
		
		Dim resultsFilename As String
		
		resultsFilename = _iheFramework.CloseResults()
		
'		Dim process As System.Diagnostics.Process = new System.Diagnostics.Process()
'		process.StartInfo.FileName = resultsFilename
'		process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized
'		process.Start()	
    End Sub ' Finalize()

    '
    ' TestCaseBase New subroutine
    '
    Public Sub New()
    End Sub ' New()

End Class ' TestCaseBase


'===============================================================================
'
' Script entry point
'
Module DvtkIheScript

    '
    ' This routine is called when executing this script from the IHE Command Line Application
    '
    Sub DvtIheMain(ByVal configurationFilename As String, ByVal arg2 As String)
		'
		' Instantiate a new TestCase
		'
        Dim testCase As TestCase = New TestCase

		'
		' Do the generic Initialization
		'
        testCase.Initialize(configurationFilename)
        
        '
        ' Do the specific SetUp
        '
        testCase.SetUp()
        
        '
        ' Do the generic Start
        '
        testCase.Start()
        
        '
        ' Do the specific Execute
        '
        testCase.Execute()
        
        '
        ' Do the generic Finalization
        '
        testCase.Finalize()
    End Sub ' DvtIheMain()

End Module ' DvtkScript
'===============================================================================
