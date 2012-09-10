'===============================================================================
' Filename:     RetrieveImagesScript.vbs
'===============================================================================
'
'Description:   VBScript showing basic test case execution framework for IHE
'				integration profiles - image display.
'
'===============================================================================
'
' System namespaces
'
Imports System
Imports System.IO
Imports System.Windows.Forms
Imports System.Collections
Imports System.Drawing

'
' DVTK namespaces
'
Imports DvtkData
Imports DvtkData.Dimse
Imports DvtkHighLevelInterface.InformationModel
Imports Dvtk.IheActors.Bases
Imports Dvtk.IheActors.Actors
Imports Dvtk.IheActors.Dicom
Imports Dvtk.IheActors.Hl7
Imports Dvtk.IheActors.IheFramework
Imports DvtkApplicationLayer.UserInterfaces

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
			
    End Sub ' SetUp()

    '
    ' Test Case Specific Execute subroutine
    '
    Public Sub Execute()
		'
		' Execute Test Case
		'
		Dim imageDisplayIcon As String = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_imagedisplay.png"

        Dim inputForm As InputForm
		Dim patientId As String = "000001"
				
		'
		' Get the Image Archive Name
		'
		Dim imageArchiveActorName As ActorName = New ActorName(ActorTypeEnum.ImageArchive, "IA_ID1")				
		
		'
		' Get the Image Display
		'
 		Dim imageDisplayActorName As ActorName = New ActorName(ActorTypeEnum.ImageDisplay, "ID_ID1")				
		Dim imageDisplay As ImageDisplayActor = CType(IheFramework.GetActor(imageDisplayActorName), ImageDisplayActor)		
		If Not (imageDisplay Is Nothing) Then
	
	        ' Set up query tags for PATIENT level query
			Dim queryTags As TagValueCollection = New TagValueCollection()
			queryTags.Add(New DicomTagValue(Tag.PATIENTS_NAME, ""))
			queryTags.Add(New DicomTagValue(Tag.PATIENT_ID, ""))
			queryTags.Add(New DicomTagValue(Tag.PATIENTS_BIRTH_DATE, ""))
			queryTags.Add(New DicomTagValue(Tag.PATIENTS_SEX, ""))

			' First do a PATIENT level query - Patient Root Information Object Model
			' to chosen a patient id for the study to retrieve
            IheFramework.SetUiStatusText("PATIENT level query...")
            
			Dim informationModel As QueryRetrieveInformationModelEnum = QueryRetrieveInformationModelEnum.PatientRootQueryRetrieveInformationModel
			imageDisplay.SendQueryImages(informationModel, QueryRetrieveLevelEnum.PatientQueryRetrieveLevel, queryTags)

            IheFramework.SetUiStatusText("No patients returned: " + imageDisplay.QueryItems(QueryRetrieveLevelEnum.PatientQueryRetrieveLevel).Count.ToString())
	
			' Get Patient Id
	        If (IheFramework.Config.CommonConfig.Interactive = True) Then
                inputForm = New InputForm
                inputForm.Caption = "Image Display"
                inputForm.AddImage(Image.FromFile(imageDisplayIcon))
			    Dim patientList As ArrayList = New ArrayList
			    Dim patientQueryItem As DicomQueryItem						
			    For Each patientQueryItem In imageDisplay.QueryItems(QueryRetrieveLevelEnum.PatientQueryRetrieveLevel)
				    patientId = patientQueryItem.GetValue(Tag.PATIENT_ID)
				    patientList.Add(patientId)
			    Next
			    patientQueryItem = imageDisplay.QueryItems(QueryRetrieveLevelEnum.PatientQueryRetrieveLevel)(0)
			    patientId = patientQueryItem.GetValue(Tag.PATIENT_ID)
			    inputForm.AddDropDownListEntry("Query Patient ID", patientList, patientId)
			    inputForm.ShowDialog()
			    patientId = inputForm.GetTextValue("Query Patient ID")
            Else
                Dim patientQueryItem As DicomQueryItem						
			    patientQueryItem = imageDisplay.QueryItems(QueryRetrieveLevelEnum.PatientQueryRetrieveLevel)(0)
			    patientId = patientQueryItem.GetValue(Tag.PATIENT_ID)
            End IF
            
            ' Clear the Image Display store data directory - delete all files
            imageDisplay.ClearDicomStoreDataDirectory(imageArchiveActorName)
            
	        ' Set up query tags for PATIENT level retrieve
            Dim retrieveTags As TagValueCollection = New TagValueCollection()
			retrieveTags.Add(New DicomTagValue(Tag.PATIENT_ID, patientId))

            IheFramework.SetUiStatusText("PATIENT level retieve...")
            
			' Do a PATIENT level retrieve - Patient Root Information Object Model
			informationModel = QueryRetrieveInformationModelEnum.PatientRootQueryRetrieveInformationModel
			imageDisplay.SendRetrieveImages(informationModel,  QueryRetrieveLevelEnum.PatientQueryRetrieveLevel, "ID_AE", retrieveTags)
			
            IheFramework.SetUiStatusText("No C-MOVE-RSP returned: " + imageDisplay.RetrieveItems.Count.ToString())

            ' Handle the returned C-MOVE-RSPs - Archive may use intermediate responses
            Dim patientRetrieveCMoveRsp As DicomQueryItem						
            For Each patientRetrieveCMoveRsp In imageDisplay.RetrieveItems
                Dim remCount As String = patientRetrieveCMoveRsp.GetValue(Tag.NUMBER_OF_REMAINING_SUBOPERATIONS)
                Dim compCount As String = patientRetrieveCMoveRsp.GetValue(Tag.NUMBER_OF_COMPLETED_SUBOPERATIONS)
                Dim failCount As String = patientRetrieveCMoveRsp.GetValue(Tag.NUMBER_OF_FAILED_SUBOPERATIONS)
                Dim warnCount As String = patientRetrieveCMoveRsp.GetValue(Tag.NUMBER_OF_WARNING_SUBOPERATIONS)
                IheFramework.SetUiStatusText("C-MOVE-RSP - remain: " + remCount + " complete: " + compCount + " failed: " + failCount + " warning: " + warnCount)
			Next

            ' Compare the retrieved images with the originals
            IheFramework.SetUiStatusText("Compare Archived files...")
            
			Dim storeDataDirectory As New StoreDataDirectory()
		    storeDataDirectory.HtmlOutputFilename = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\results\\CompareArchiveResults.html"
		    storeDataDirectory.DisplayGroupLength = True
		    storeDataDirectory.CompareVR = True
		    storeDataDirectory.IncludeDetailedResults = True
		    
		    Dim differenceCount As Integer
		    differenceCount = storeDataDirectory.Compare(imageDisplay.GetDicomStoreDataDirectory(imageArchiveActorName), IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\data\\imageArchive", Tag.SOP_INSTANCE_UID)

            IheFramework.SetUiStatusText("Compare Archived files - differences: " + differenceCount.ToString())

			'IheFramework.SetUiStatusText("")
			
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
