'===============================================================================
' Filename:     PC2-1.vbs
'===============================================================================
'
'Description:   IHE NL - Workflow Script 1.
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
Imports Dvtk.Hl7
Imports Dvtk.Hl7.Messages
Imports Dvtk.IheActors.Bases
Imports Dvtk.IheActors.Actors
Imports Dvtk.IheActors.Dicom
Imports Dvtk.IheActors.Hl7
Imports Dvtk.IheActors.IntegrationProfile
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
						
		' Add User Defined default values for these DICOM Tags
		IntegrationProfile.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.PERFORMED_STATION_NAME, "MyPerformedStationName"))
		IntegrationProfile.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.PERFORMED_LOCATION, "MyPerformedLocation"))
		
		' Add default value for these DICOM Tags - no in-built value defined
		IntegrationProfile.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.COMMENTS_ON_THE_PERFORMED_PROCEDURE_STEPS, "My comments on PPS"))
		
		' Delete In-built default values for these DICOM Tags
'		IntegrationProfile.AddUserDefinedDefaultTagValue(New DicomTagValueDelete(Tag.PERFORMED_STATION_AE_TITLE))
'		IntegrationProfile.AddUserDefinedDefaultTagValue(New DicomTagValueDelete(Tag.PERFORMED_PROCEDURE_STEP_ID))

		IntegrationProfile.AddUserDefinedDefaultTagValue(New DicomTagValueAutoIncrement(AffectedEntityEnum.PerformedProcedureStepEntity, Tag.PERFORMED_PROCEDURE_STEP_ID , "Prefix", 0, 1, 3))
		
    End Sub ' SetUp()

    '
    ' Test Case Specific Execute subroutine
    '
    Public Sub Execute()
		'
		' Execute Test Case
		'
		Dim adtPatientRegistrationIcon As String = IntegrationProfile.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_adtpatientregistration.png"
		Dim orderPlacerIcon As String = IntegrationProfile.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_orderplacer.png"
		Dim dssOrderFillerIcon As String = IntegrationProfile.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_dssorderfiller.png"		
		Dim imageManagerIcon As String = IntegrationProfile.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_imagemanager.png"
		
        Dim inputForm As InputForm
		Dim communicationBetweenActorsForm As CommunicationBetweenActorsForm
		Dim patientId As String = "000001"
		Dim patientSurname As String = "Surname"
		Dim patientFirstname As String = "Firstname"
		Dim patientBirthDate As String = "19901012"
		Dim patientSex As String = "M"
		Dim patientAddress As String = "Address1^Address2^Address3"
		Dim placerNumber As String = "PL1234"
		Dim placerDescription As String = "PlacerDescription"
		Dim spsDescription As String = "SpsDescription"
		Dim accessionNumber As String = "1234"
		Dim reqProcId As String = "RP-1234"
		Dim spsId As String = "SPS-1234"
		Dim studyUidRoot As String = "1.2.3.4."
				
		'
		' Get the ADT Patient Registration
		'		
		Dim adtPatientRegistration As AdtPatientRegistrationActor = CType(IntegrationProfile.GetActor(ActorNameEnum.AdtPatientRegistration), AdtPatientRegistrationActor)
		If Not (adtPatientRegistration Is Nothing) Then
	
            inputForm = New InputForm
            inputForm.Caption = "ADT Patient Registration"
            inputForm.AddImage(Image.FromFile(adtPatientRegistrationIcon))
			inputForm.AddTextBoxEntry("Patient ID", patientId, False)
			inputForm.AddTextBoxEntry("Surname", patientSurname, False)
			inputForm.AddTextBoxEntry("Firstname", patientfirstname, False)
			inputForm.AddTextBoxEntry("Birthdate", patientBirthDate, False)
			Dim patientSexList As ArrayList = New ArrayList
			patientSexList.Add("M")
			patientSexList.Add("F")
			patientSexList.Add("O")
			inputForm.AddDropDownListEntry("Gender", patientSexList, "M")
			inputForm.ShowDialog()
			patientId = inputForm.GetTextValue("Patient ID")
			patientSurname = inputForm.GetTextValue("Surname")
			patientFirstname = inputForm.GetTextValue("Firstname")
			patientBirthDate = inputForm.GetTextValue("Birthdate")
			patientSex = inputForm.GetTextValue("Gender")
		
			' Register a patient
			Dim adt As AdtMessage = New AdtMessage("A01")
			adt.EVN(1) = "A01"
			
			adt.PID(3) = patientId
			adt.PID(5) = patientSurname + "^" + patientFirstname
			adt.PID(7) = patientBirthDate
			adt.PID(8) = patientSex
			adt.PID(11) = patientAddress
			
			adt.PV1(2) = "I"
			adt.PV1(3) = "Ward 3"			
				
			adtPatientRegistration.SendPatientRegistration(adt)

			communicationBetweenActorsForm = New CommunicationBetweenActorsForm("[1] Patient Registration", "ADT Patient Registration", adtPatientRegistrationIcon, "Order Placer and DSS/Order Filler", orderPlacerIcon)
			communicationBetweenActorsForm.ShowDialog()

		End If	
		
		'
		' Get the Order Placer
		'
		Dim orderPlacer As OrderPlacerActor = CType(IntegrationProfile.GetActor(ActorNameEnum.OrderPlacer), OrderPlacerActor)
		If Not (orderPlacer Is Nothing) Then

            inputForm = New InputForm
            inputForm.Caption = "Order Placer"
            inputForm.AddImage(Image.FromFile(orderPlacerIcon))
			inputForm.AddTextBoxEntry("Patient ID", patientId, True)
			inputForm.AddTextBoxEntry("Surname", patientSurname, True)
			inputForm.AddTextBoxEntry("Firstname", patientfirstname, True)
			inputForm.AddTextBoxEntry("Placer Number", placerNumber, False)
			Dim placerDescriptionList As ArrayList = New ArrayList
			placerDescriptionList.Add("Shoulders")
			placerDescriptionList.Add("Chest")
			placerDescriptionList.Add("Abdomen")
			placerDescriptionList.Add("Pelvis")
			placerDescriptionList.Add("Legs")
			inputForm.AddDropDownListEntry("Placer Description", placerDescriptionList, "Shoulders")		
			inputForm.ShowDialog()
			placerNumber = inputForm.GetTextValue("Placer Number")
			placerDescription = inputForm.GetTextValue("Placer Description")
		
			' Place an Order
			Dim orm As OrmMessage = New OrmMessage("O01")
			orm.PID(3) = patientId
			orm.PID(5) = patientSurname + "^" + patientFirstname
			orm.PID(7) = patientBirthDate
			orm.PID(8) = patientSex
			orm.PID(11) = patientAddress
			
			orm.PV1(2) = "I"
			orm.PV1(3) = "Ward 3"
			orm.PV1(8) = "Referring^Doctor"
			orm.PV1(19) = "Visit Number"
			
			orm.ORC(1) = "NW"
			orm.ORC(2) = placerNumber
			
			orm.OBR(2) = placerNumber
			orm.OBR(4) = placerDescription
			orm.OBR(7) = System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
			
			orderPlacer.SendPlacerOrderManagement(orm)
			
			communicationBetweenActorsForm = New CommunicationBetweenActorsForm("[2] Placer Order Management", "Order Placer", orderPlacerIcon, "DSS/Order Filler", dssOrderFillerIcon)
			communicationBetweenActorsForm.ShowDialog()

		End If	
		
		'
		' Get the Dss / Order Filler
		'
		Dim dssOrderFiller As DssOrderFillerActor = CType(IntegrationProfile.GetActor(ActorNameEnum.DssOrderFiller), DssOrderFillerActor)
		If Not (dssOrderFiller Is Nothing) Then
		
            inputForm = New InputForm
            inputForm.Caption = "DSS / Order Filler"
            inputForm.AddImage(Image.FromFile(dssOrderFillerIcon))
			inputForm.AddTextBoxEntry("Patient ID", patientId, True)
			inputForm.AddTextBoxEntry("Surname", patientSurname, True)
			inputForm.AddTextBoxEntry("Firstname", patientfirstname, True)
			inputForm.AddTextBoxEntry("Placer Number", placerNumber, True)
			inputForm.AddTextBoxEntry("Placer Description", placerDescription, True)
			Dim spsDescriptionList As ArrayList = New ArrayList
			spsDescriptionList.Add("Shoulder - Left")
			spsDescriptionList.Add("Shoulder - Right")
			spsDescriptionList.Add("Shoulders - Both")
			spsDescriptionList.Add("Chest")
			spsDescriptionList.Add("Abdomen")
			spsDescriptionList.Add("Pelvis")
			spsDescriptionList.Add("Leg - Left")
			spsDescriptionList.Add("Leg - Right")
			inputForm.AddDropDownListEntry("SPS Description", spsDescriptionList, "Shoulder - Left")
			inputForm.AddTextBoxEntry("Accession Number", accessionNumber, False)
			inputForm.AddTextBoxEntry("Req Proc Id", reqProcId, False)
			inputForm.AddTextBoxEntry("Sps Id", spsId, False)
			inputForm.ShowDialog()
			spsDescription = inputForm.GetTextValue("SPS Description")
			accessionNumber = inputForm.GetTextValue("Accession Number")
			reqProcId = inputForm.GetTextValue("Req Proc Id")
			spsId = inputForm.GetTextValue("Sps Id")
	
			' Procedure Scheduled
			Dim orm As OrmMessage = New OrmMessage("O01")
			orm.MSH(9) = "ORM^O01^ORM_O01"
						
			orm.PID(3) = patientId
			orm.PID(5) = patientSurname + "^" + patientFirstname
			orm.PID(7) = patientBirthDate
			orm.PID(8) = patientSex
			orm.PID(11) = patientAddress
			
			orm.PV1(2) = "I"
			orm.PV1(3) = "Ward 3"
			orm.PV1(8) = "Referring^Doctor"
			orm.PV1(19) = "Visit Number"
			
			orm.ORC(1) = "NW"
			orm.ORC(2) = placerNumber
			orm.ORC(3) = "OF12345"
			orm.ORC(5) = "SC"
			orm.ORC(7) = "^^^" + System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
			
			orm.OBR(2) = orm.ORC(2)
			orm.OBR(3) = orm.ORC(3)
			orm.OBR(4) = "RPHNDL^" + placerDescription + "^RP^SPSHNDL^" + spsDescription + "^SPS"
			orm.OBR(15) = "L"			
			orm.OBR(18) = accessionNumber
			orm.OBR(19) = reqProcId
			orm.OBR(20) = spsId
			orm.OBR(24) = "CR"
			orm.OBR(27) = orm.ORC(7)
			orm.OBR(44) = "RPHNDL^" + placerDescription + "^RP^^" +  placerDescription

			orm.ZDS(1) = studyUidRoot + accessionNumber + "^^Application^DICOM"
				
			dssOrderFiller.SendProcedureScheduled(orm)

			communicationBetweenActorsForm = New CommunicationBetweenActorsForm("[4] Procedure Scheduled", "DSS/Order Filler", dssOrderFillerIcon, "Image Manager", imageManagerIcon)
			communicationBetweenActorsForm.ShowDialog()

		End If

		MessageBox.Show("Stop DVTK IHE Scheduled Workflow Test...")
				
		'
		' Add filters (as Tag Values) to the Comparator
		'
		' Universal Match on these DICOM Tags
'		IntegrationProfile.AddComparisonTagValueFilter(New DicomTagValue(Tag.PATIENT_ID))
'		IntegrationProfile.AddComparisonTagValueFilter(New DicomTagValue(Tag.ACCESSION_NUMBER))

		' Single Value Match on these DICOM Tags
		IntegrationProfile.AddComparisonTagValueFilter(New DicomTagValue(Tag.PATIENT_ID, patientId))
'		IntegrationProfile.AddComparisonTagValueFilter(New DicomTagValue(Tag.ACCESSION_NUMBER, accessionNumber))
				
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

	Private Dim _integrationProfile As IntegrationProfile = Nothing

	'
	' Get Integration Profile Property
	'
	Public ReadOnly Property IntegrationProfile As IntegrationProfile
		Get
			IntegrationProfile = _integrationProfile
		End Get
	End Property
   	
    '
    ' Test Case Start subroutine
    '
    Public Sub Start()
		'
		' Start the test
		'	
		_integrationProfile.StartTest()	
    End Sub ' Start()

    '
    ' Test Case Initialize subroutine
    '
    Public Sub Initialize(ByVal configurationFilename As String)
		'
		' Initialize the test
		'
		_integrationProfile = New IntegrationProfile("Scheduled Workflow")	
		_integrationProfile.Config.Load(configurationFilename)
		_integrationProfile.ApplyConfig()
		_integrationProfile.OpenResults()
    End Sub ' Initialize()

    '
    ' Test Case Finalize subroutine
    '
    Public Sub Finalize()
		'
		' Display a message box as a means of stopping the test
		'
'		MessageBox.Show("Stop DVTK IHE Actor Test...")

		'
		' Finalize the test
		'
		_integrationProfile.StopTest()
		_integrationProfile.EvaluateTest()
		_integrationProfile.CleanUpCurrentWorkingDirectory()
		
		Dim resultsFilename As String
		
		resultsFilename = _integrationProfile.CloseResults()
		
		Dim process As System.Diagnostics.Process = new System.Diagnostics.Process()
		process.StartInfo.FileName = resultsFilename
		process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized
		process.Start()	
		
		Dim scheduledWorfklowIcon As String = IntegrationProfile.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\scheduledworkflow.png"
	    Dim banner As Banner = New Banner
	    
        banner.Caption = "IHE Scheduled Workflow"
        banner.AddImage(Image.FromFile(scheduledWorfklowIcon))
		banner.ShowDialog()
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
