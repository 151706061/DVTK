'===============================================================================
' Filename:     QueryImagesScript.vbs
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
		' Get the Image Display
		'
 		Dim actorName As ActorName = New ActorName(ActorTypeEnum.ImageDisplay, "ID_ID1")				
		Dim imageDisplay As ImageDisplayActor = CType(IheFramework.GetActor(actorName), ImageDisplayActor)		
		If Not (imageDisplay Is Nothing) Then
	
	        ' Set up query tags for PATIENT level query
			Dim queryTags As TagValueCollection = New TagValueCollection()
			queryTags.Add(New DicomTagValue(Tag.PATIENTS_NAME, ""))
			queryTags.Add(New DicomTagValue(Tag.PATIENT_ID, ""))
			queryTags.Add(New DicomTagValue(Tag.PATIENTS_BIRTH_DATE, ""))
			queryTags.Add(New DicomTagValue(Tag.PATIENTS_SEX, ""))

			' Do a PATIENT level query - Patient Root Information Object Model
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
            				
	        ' Set up query tags for STUDY level query
			queryTags = New TagValueCollection()
			queryTags.Add(New DicomTagValue(Tag.PATIENT_ID, patientId))
			queryTags.Add(New DicomTagValue(Tag.ACCESSION_NUMBER, ""))
			queryTags.Add(New DicomTagValue(Tag.STUDY_INSTANCE_UID, ""))
			queryTags.Add(New DicomTagValue(Tag.STUDY_DATE, ""))
			queryTags.Add(New DicomTagValue(Tag.STUDY_TIME, ""))

			' Do a STUDY level query - using chosen patientId
            IheFramework.SetUiStatusText("STUDY level query using patient id " + patientId + " ...")
            
			imageDisplay.SendQueryImages(informationModel, QueryRetrieveLevelEnum.StudyQueryRetrieveLevel, queryTags)

            IheFramework.SetUiStatusText("No studies returned: " + imageDisplay.QueryItems(QueryRetrieveLevelEnum.StudyQueryRetrieveLevel).Count.ToString())

			Dim studyQueryItem As DicomQueryItem						
			For Each studyQueryItem In imageDisplay.QueryItems(QueryRetrieveLevelEnum.StudyQueryRetrieveLevel)

				' Query all Series belonging to this Study
				Dim studyUid As String = studyQueryItem.GetValue(Tag.STUDY_INSTANCE_UID)

	            ' Set up query tags for SERIES level query
				queryTags = New TagValueCollection()
				queryTags.Add(New DicomTagValue(Tag.PATIENT_ID, patientId))
				queryTags.Add(New DicomTagValue(Tag.STUDY_INSTANCE_UID, studyUid))
				queryTags.Add(New DicomTagValue(Tag.SERIES_INSTANCE_UID, ""))
				queryTags.Add(New DicomTagValue(Tag.MODALITY, ""))
				queryTags.Add(New DicomTagValue(Tag.SERIES_NUMBER, ""))

				' Do a SERIES level query - using chosen patientId and studyUid
                IheFramework.SetUiStatusText("SERIES level query using study uid " + studyUid + " ...")
                
				imageDisplay.SendQueryImages(informationModel, QueryRetrieveLevelEnum.SeriesQueryRetrieveLevel, queryTags)
            
                IheFramework.SetUiStatusText("No series returned: " + imageDisplay.QueryItems(QueryRetrieveLevelEnum.SeriesQueryRetrieveLevel).Count.ToString())

				Dim seriesQueryItem As DicomQueryItem						
				For Each seriesQueryItem In imageDisplay.QueryItems(QueryRetrieveLevelEnum.SeriesQueryRetrieveLevel)

					' Query all Instances belonging to this Series
					Dim seriesUid As String = seriesQueryItem.GetValue(Tag.SERIES_INSTANCE_UID)
					
	                ' Set up query tags for IMAGE level query
					queryTags = New TagValueCollection()
					queryTags.Add(New DicomTagValue(Tag.PATIENT_ID, patientId))
					queryTags.Add(New DicomTagValue(Tag.STUDY_INSTANCE_UID, studyUid))
					queryTags.Add(New DicomTagValue(Tag.SERIES_INSTANCE_UID, seriesUid))
					queryTags.Add(New DicomTagValue(Tag.SOP_INSTANCE_UID, ""))
					queryTags.Add(New DicomTagValue(Tag.INSTANCE_NUMBER, ""))

					' Do an IMAGE level query - using chosen patientId, studyUid and seriesUid
                    IheFramework.SetUiStatusText("IMAGE level query using series uid " + seriesUid + " ...")
                    
					imageDisplay.SendQueryImages(informationModel, QueryRetrieveLevelEnum.InstanceQueryRetrieveLevel, queryTags)

                    IheFramework.SetUiStatusText("No images returned: " + imageDisplay.QueryItems(QueryRetrieveLevelEnum.InstanceQueryRetrieveLevel).Count.ToString())
								
				Next ' Series

			Next ' Study		
			
			IheFramework.SetUiStatusText("")

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
