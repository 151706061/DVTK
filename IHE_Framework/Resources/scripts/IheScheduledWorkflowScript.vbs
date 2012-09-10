'===============================================================================
' Filename:     AdtPatientRegistrationScript.vbs
'===============================================================================
'
'Description:   VBScript showing basic test case execution framework for IHE
'				integration profiles - ADT Patient Registration.
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
Imports Dvtk.IheActors.IheFramework
Imports DvtkApplicationLayer.UserInterfaces

'===============================================================================
'
' Test Case Class
'
Class TestCase
    Inherits TestCaseBase
    
    '
    ' Define global actor names used in the test case
    '
    Dim adtPatientRegistrationActorName As ActorName = New ActorName(ActorTypeEnum.AdtPatientRegistration, "ADT_ID1")		
    Dim orderPlacerActorName As ActorName = New ActorName(ActorTypeEnum.OrderPlacer, "OP_ID1")				
    Dim dssOrderFillerActorName As ActorName = New ActorName(ActorTypeEnum.DssOrderFiller, "OF_ID1")
 	Dim acquisitionModalityActorName As ActorName = New ActorName(ActorTypeEnum.AcquisitionModality, "AM_ID1")
	Dim imageArchiveActorName As ActorName = New ActorName(ActorTypeEnum.ImageArchive, "IA_ID1")				
	Dim imageManagerActorName As ActorName = New ActorName(ActorTypeEnum.ImageManager, "IM_ID1")				

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
		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.PERFORMED_STATION_NAME, "MyPerformedStationName"))
		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.PERFORMED_LOCATION, "MyPerformedLocation"))
		
		' Add default value for these DICOM Tags - no in-built value defined
		IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValue(Tag.COMMENTS_ON_THE_PERFORMED_PROCEDURE_STEPS, "My comments on PPS"))
		
		' Delete In-built default values for these DICOM Tags
        IheFramework.AddUserDefinedDefaultTagValue(New DicomTagValueDelete(Tag.PERFORMED_STATION_AE_TITLE))
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
		Dim adtPatientRegistrationIcon As String = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_adtpatientregistration.png"
		Dim orderPlacerIcon As String = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_orderplacer.png"
		Dim dssOrderFillerIcon As String = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_dssorderfiller.png"		
		Dim imageManagerIcon As String = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\scripts\\icons\\img_imagemanager.png"
		
		'
		' Define the InputForm and CommunicationBetweenActorsForm
		'
        Dim inputForm As InputForm
		Dim communicationBetweenActorsForm As CommunicationBetweenActorsForm
		
		'
		' Define the local variables and initial values
		'
		Dim patientId As String = "000001"
		Dim patientSurname As String = "Surname"
		Dim patientFirstname As String = "Firstname"
		Dim patientBirthDate As String = "19901012"
		Dim patientSex As String = "M"
		Dim patientAddress As String = "Address1^Address2^Address3"
		Dim orderPlacerNumber As String = "OP12345"
		Dim orderFillerNumber As String = "OF12345"

		'
		' Get the ADT Patient Registration
		'
		Dim adtPatientRegistration As AdtPatientRegistrationActor = CType(IheFramework.GetActor(adtPatientRegistrationActorName), AdtPatientRegistrationActor)
		If Not (adtPatientRegistration Is Nothing) Then
		
		    '
		    ' Display the input form only if the framework is configured as interactive
		    '
		    If (IheFramework.Config.CommonConfig.Interactive = True) Then

		        '
		        ' Create a new input form for the ADT Patient Registration details
		        ' - define the title as the Caption
		        ' - add an image to the form - representation of the ADT device
    	        inputForm = New InputForm
                inputForm.Caption = "ADT Patient Registration"
                inputForm.AddImage(Image.FromFile(adtPatientRegistrationIcon))
                '
                ' - add text boxes to allow user input of the values defined
                '   - the first argument is the text box name, the second the text box initial value (defined above)
                '     and the third is a boolean indicating whether the text box is ReadOnly - i.e. editable
		    	inputForm.AddTextBoxEntry("Patient ID", patientId, False)
			    inputForm.AddTextBoxEntry("Surname", patientSurname, False)
			    inputForm.AddTextBoxEntry("Firstname", patientfirstname, False)
			    inputForm.AddTextBoxEntry("Birthdate", patientBirthDate, False)
			    '
			    ' - add a drop down list with the choice values M, F and O
			    '
			    Dim patientSexList As ArrayList = New ArrayList
			    patientSexList.Add("M")
			    patientSexList.Add("F")
			    patientSexList.Add("O")
			    '
			    ' - add the drop down list to the input form with the given name and initial value of M
			    inputForm.AddDropDownListEntry("Gender", patientSexList, "M")
			    '
			    ' - display the input form to the user
			    inputForm.ShowDialog()
			    '
			    ' - retrieve the input values from each text box using the name 
    			patientId = inputForm.GetTextValue("Patient ID")
	    		patientSurname = inputForm.GetTextValue("Surname")
		    	patientFirstname = inputForm.GetTextValue("Firstname")
			    patientBirthDate = inputForm.GetTextValue("Birthdate")
			    patientSex = inputForm.GetTextValue("Gender")
		
		    End If
		    
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

            '
            ' Show the user that there is some communication between actors
            ' - this is done simply to give the script some animation - feedback for the user
            '
		    If (IheFramework.Config.CommonConfig.Interactive = True) Then
			    communicationBetweenActorsForm = New CommunicationBetweenActorsForm("[1] Patient Registration", "ADT Patient Registration", adtPatientRegistrationIcon, "Order Placer and DSS/Order Filler", orderPlacerIcon)
			    communicationBetweenActorsForm.ShowDialog()
            End If
            				
		End If	
		
		'
		' Get the Order Placer
		'
		Dim orderPlacer As OrderPlacerActor = CType(IheFramework.GetActor(orderPlacerActorName), OrderPlacerActor)
		If Not (orderPlacer Is Nothing) Then
		
			' Place an Order
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
			orm.ORC(2) = orderPlacerNumber
			
			orm.OBR(2) = orm.ORC(2)
			orm.OBR(4) = "HANDL^HAND LEFT XRAY"
			orm.ORC(7) = "^^^" + System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
			
			orderPlacer.SendPlacerOrderManagement(orm)

		    If (IheFramework.Config.CommonConfig.Interactive = True) Then
    			MessageBox.Show("Order Placed...")
            End If
		End If	
		
		'
		' Get the Dss / Order Filler
		'
		Dim dssOrderFiller As DssOrderFillerActor = CType(IheFramework.GetActor(dssOrderFillerActorName), DssOrderFillerActor)
		If Not (dssOrderFiller Is Nothing) Then
		
			' Procedure Scheduled
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
			orm.ORC(2) = orderPlacerNumber
			orm.ORC(3) = orderFillerNumber
			orm.ORC(5) = "SC"
			orm.ORC(7) = "^^^" + System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
			
			orm.OBR(2) = orm.ORC(2)
			orm.OBR(3) = orm.ORC(3)
			orm.OBR(4) = "OPHNDL^OP HAND LEFT XRAY^OP^SPSHNDL^SPS HAND LEFT XRAY^SPS"
			orm.OBR(15) = "L"			
			orm.OBR(18) = "ACN12345"
			orm.OBR(19) = "RP12345"
			orm.OBR(20) = "SPS12345"
			orm.OBR(24) = "CR"
			orm.OBR(27) = orm.ORC(7)
			orm.OBR(44) = "RPHNDL^RP HAND LEFT XRAY^RP^^RP HAND LEFT XRAY"

			orm.ZDS(1) = "1.2.3.4^^Application^DICOM"
			
			dssOrderFiller.SendProcedureScheduled(orm)

		    If (IheFramework.Config.CommonConfig.Interactive = True) Then
			    MessageBox.Show("Procedure Scheduled...")
            End If
            
			' Procedure Updated
			orm = New OrmMessage("O01")
			orm.PID(3) = patientId
			orm.PID(5) = patientSurname + "^" + patientFirstname
			orm.PID(7) = patientBirthDate
			orm.PID(8) = patientSex
			orm.PID(11) = patientAddress
			
			orm.PV1(2) = "I"
			orm.PV1(3) = "Ward 3"			
			orm.PV1(8) = "Referring^Doctor"
			orm.PV1(19) = "Visit Number"
			
			orm.ORC(1) = "SC"
			orm.ORC(2) = orderPlacerNumber
			orm.ORC(3) = orderFillerNumber
			orm.ORC(5) = "XO"
			orm.ORC(7) = "^^^" + System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture)
			
			orm.OBR(2) = orm.ORC(2)
			orm.OBR(3) = orm.ORC(3)
			orm.OBR(4) = "OPHNDR^OP HAND RIGHT XRAY^OP^SPSHNDR^SPS HAND RIGHT XRAY^SPS"
			orm.OBR(15) = "R"
			orm.OBR(18) = "ACN12345"
			orm.OBR(19) = "RP12345"
			orm.OBR(20) = "SPS12345"
			orm.OBR(24) = "CR"
			orm.OBR(27) = orm.ORC(7)
			orm.OBR(44) = "RPHNDR^RP HAND RIGHT XRAY^RP^^RP HAND RIGHT XRAY"

			orm.ZDS(1) = "1.2.3.4^^Application^DICOM"
			
			dssOrderFiller.SendProcedureUpdated(orm)

		    If (IheFramework.Config.CommonConfig.Interactive = True) Then
			    MessageBox.Show("Procedure Updated...")
            End If
		End If	
					
		'
		' Get the Acquisition Modality
		'
		Dim acquisitionModality As AcquisitionModalityActor = CType(IheFramework.GetActor(acquisitionModalityActorName), AcquisitionModalityActor)
		If Not (acquisitionModality Is Nothing) Then
		
		    ' Set up worklist item - storage directory mapping
		    acquisitionModality.MapWorklistItemToStorageDirectory.MapTag = Tag.SCHEDULED_PROCEDURE_STEP_DESCRIPTION
		    acquisitionModality.MapWorklistItemToStorageDirectory.AddMapping("Default", IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\data\\acquisitionModality\\default")
		    
			' Get a modality worklist
			Dim queryTags As TagValueCollection = New TagValueCollection()
'			queryTags.Add(New DicomTagValue(Tag.PATIENT_ID, "SC-I1"))
			queryTags.Add(New DicomTagValue(Tag.SCHEDULED_PROCEDURE_STEP_START_DATE, System.DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture)))

			Dim storageDirectory As String = IheFramework.Config.CommonConfig.RootedBaseDirectory + "\\data\\acquisitionModality\\default"

		    ' Send DICOM Verification (C-ECHO) to all actors
		    acquisitionModality.TriggerActorDicomClientVerificationInstances(ActorTypeEnum.DssOrderFiller)
		    acquisitionModality.TriggerActorDicomClientVerificationInstances(ActorTypeEnum.PerformedProcedureStepManager)
		    acquisitionModality.TriggerActorDicomClientVerificationInstances(ActorTypeEnum.ImageArchive)
		    acquisitionModality.TriggerActorDicomClientVerificationInstances(ActorTypeEnum.ImageManager)

			' Send the worklist query
			acquisitionModality.SendQueryModalityWorklist(queryTags)

			Dim modalityWorklistItem As DicomQueryItem						
			For Each modalityWorklistItem In acquisitionModality.ModalityWorklistItems

				' Send the MPPS in-progress
				acquisitionModality.SendNCreateModalityProcedureStepInProgress(modalityWorklistItem)

				' Send the images - read images from storageDirectory and convert
'				acquisitionModality.SendModalityImagesStored(storageDirectory, modalityWorklistItem, True)

				' Send the images - read images based on worklist map to storage directory
				acquisitionModality.SendModalityImagesStored(modalityWorklistItem, False)
				
				' Send the mpps completed
				acquisitionModality.SendNSetModalityProcedureStepCompleted()

				' Send the storage commitment
				acquisitionModality.SendStorageCommitment(False, 0)
			Next
		End If	
				
    End Sub ' Execute()

    '
    ' Test Case Specific TearDown subroutine
    '
    Public Sub TearDown()
		'
		' Set the Test Case Assertions
		'
		' - check the C-STORE-RQ status is 1234 
		'   - status is actually 0000 - so assertion will be seen in results.
		IheFramework.AssertDicomAttributeValueBetweenActors(acquisitionModalityActorName, imageArchiveActorName, "C-STORE-RSP", Tag.STATUS, 1234)

        '
        ' - check that 2 N-EVENT-REPORT-RQ messages have been sent between the acquisition modality actor and imanager actor
        '   - only 1 should be sent - so assertion will be seen in results.
		IheFramework.AssertMessageCountBetweenActors(acquisitionModalityActorName, imageManagerActorName, "N-EVENT-REPORT-RQ", 2)

        '
        ' - check that the C-STORE-RSP contains the modality (0008,0050) attribute
        ' - it doesn't - so assertion will be seen in the results.
		IheFramework.AssertDicomAttributePresentBetweenActors(acquisitionModalityActorName, imageArchiveActorName, "C-STORE-RSP", Tag.MODALITY)

    End Sub ' TestAssertions()
    
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
	' Get Integration Profile Property
	'
	Public ReadOnly Property IheFramework As IheFramework
		Get
			IheFramework = _iheFramework
		End Get
	End Property
   	
    '
    ' Test Case StartTest subroutine
    '
    Public Sub StartTest()
		'
		' Start the test
		'
		_iheFramework.StartTest()	
    End Sub ' StartTest()

    '
    ' Test Case Initialize subroutine
    '
    Public Sub Initialize(ByVal configurationFilename As String)
		'
		' Initialize the test
		'
		_iheFramework = New IheFramework("DVTK")	
		_iheFramework.Config.Load(configurationFilename)
		_iheFramework.ApplyConfig()
		_iheFramework.OpenResults()
    End Sub ' Initialize()

    '
    ' Test Case Stop subroutine
    '
    Public Sub StopTest()
		'
		' Display a message box as a means of stopping the test
		'
		If (_iheFramework.Config.CommonConfig.Interactive = True) Then
			MessageBox.Show("Stop DVTK IHE Actor Test...")
		End If

		'
		' Stop the test
		'
		_iheFramework.StopTest()
    End Sub ' StopTest
    
    '
    ' Test Case Finalize subroutine
    '
    Public Sub Finalize()        
        '
        ' Evaluate the test
        '
		_iheFramework.EvaluateTest()
		_iheFramework.CleanUpCurrentWorkingDirectory()
		
		Dim resultsFilename As String
		resultsFilename = _iheFramework.CloseResults()

		If (_iheFramework.Config.CommonConfig.Interactive = True) Then
		    Dim process As System.Diagnostics.Process = new System.Diagnostics.Process()
		    process.StartInfo.FileName = resultsFilename
		    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized
		    process.Start()	
		End If
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
        testCase.StartTest()
        
        '
        ' Do the specific Execute
        '
        testCase.Execute()

        '
        ' Do the generic StopTest
        '
        testCase.StopTest()

        '
        ' Do the specific TearDown
        '
        testCase.TearDown()
        
        '
        ' Do the generic Finalization
        '
        testCase.Finalize()
    End Sub ' DvtIheMain()

End Module ' DvtkScript
'===============================================================================
