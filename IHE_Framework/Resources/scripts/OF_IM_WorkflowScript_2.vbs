'===============================================================================
' Filename:     OF_IM_WorkflowScript.vbs
'===============================================================================
'
'Description:   DSS/OrderFiller, ImageManager - Workflow Script.
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
		
    End Sub ' SetUp()

    '
    ' Test Case Specific Execute subroutine
    '
    Public Sub Execute()
		'
		' Execute Test Case
		'
				
		'
		' Get the Dss / Order Filler
		'
		Dim actorName As ActorName = New ActorName(ActorTypeEnum.DssOrderFiller, "OF_ID1")		
		Dim dssOrderFiller As DssOrderFillerActor = CType(IntegrationProfile.GetActor(actorName), DssOrderFillerActor)
		If Not (dssOrderFiller Is Nothing) Then

            ' Get the HL7 messages from the HL7 directory reader
            Dim hl7DirectoryReader As Hl7DirectoryReader = New Hl7DirectoryReader("C:\\Program Files\\DVTk\\data", "*.txt")
            
            Dim index As Integer = 0
            
            While (index < hl7DirectoryReader.Count)
            
                Dim orm As OrmMessage = CType(hl7DirectoryReader.GetHl7Message(index), OrmMessage)
						
			    orm.PID(3) = "Updated PID:" + index.ToString()
				
			    dssOrderFiller.SendProcedureScheduled(orm)
			    
			    index += 1

            End While

		End If

		MessageBox.Show("Stop DVTK IHE Workflow Test...")
								
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
		_integrationProfile = New IntegrationProfile("OF and IM")	
		_integrationProfile.Config.Load(configurationFilename)
		_integrationProfile.ApplyConfig()
		_integrationProfile.OpenResults()
    End Sub ' Initialize()

    '
    ' Test Case Finalize subroutine
    '
    Public Sub Finalize()
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
