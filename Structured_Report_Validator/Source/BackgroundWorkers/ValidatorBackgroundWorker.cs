using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Dvtk;
using Dvtk.Dicom.StructuredReporting;
using Dvtk.Dicom.StructuredReporting.Specification;
using Dvtk.Dicom.StructuredReporting.Validation;
using DvtkHighLevelInterface.Dicom.Files;

namespace Dvtk.StructuredReportValidator.BackgroundWorkers
{
    class ValidatorBackgroundWorker : BackgroundWorker
    {
        private Dvtk.Sessions.MediaSession mediaSession = null;

        private ContextGroups contextGroups = null;


        public ValidatorBackgroundWorker()
        {
            //
            // Assign methods to handle these events.
            //

            DoWork += HandleDoWork;
        }


        /// <summary>
        /// Initialize the media session.
        /// </summary>
        private void InitializeMediaSession()
        {
            try
            {
                // Report Progress.
                ReportProgress(0, "Load definition files...");

                this.mediaSession = new Dvtk.Sessions.MediaSession();

                string definitionFilesPath = Path.Combine(Environment.GetEnvironmentVariable("COMMONPROGRAMFILES"), @"DVTk\Definition Files\DICOM");
                string[] definitionFilePaths = Directory.GetFiles(definitionFilesPath);

                foreach (string definitionFilePath in definitionFilePaths)
                {
                    this.mediaSession.DefinitionManagement.LoadDefinitionFile(definitionFilePath);
                }
            }
            catch (Exception exception)
            {
                this.mediaSession = null;
                MessageBox.Show("An exception occured while loading the definition files: " + exception.Message);
            }

        }

        /// <summary>
        /// Initialize the Context Groups.
        /// </summary>
        private void InitializeContextGroups()
        {
            try
            {
                // Report Progress.
                ReportProgress(0, "Loading Context Groups...");

                this.contextGroups = new ContextGroups();

                string contextGroupsFolder;
                contextGroupsFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
                contextGroupsFolder = Path.Combine(contextGroupsFolder, @"DVTk\Definition Files\DICOM\Context Groups\");
                this.contextGroups.LoadContextGroupsFromXml(contextGroupsFolder, "*.xml");
            }
            catch (Exception exception)
            {
                this.contextGroups = null;
                MessageBox.Show("An exception occured while loading the Context Groups: " + exception.Message);
            }
        }

        private void HandleDoWork(object sender, DoWorkEventArgs e)
        {
            // Report Progress
            ReportProgress(0, "Start");

            
            ValidatorBackgroundWorkerArgument argument = (ValidatorBackgroundWorkerArgument)e.Argument;


            //
            // Initialization of the media session and context groups.
            //

            if (this.mediaSession == null)
            {
                InitializeMediaSession();
            }

            if (this.contextGroups == null)
            {
                InitializeContextGroups();
            }


            //
            // Read DICOM file and check if this is a Structured Report file.
            //

            // Report Progress
            ReportProgress(0, "Reading DICOM file: " + argument.structuredReportPath);

            DicomFile dicomFile = new DicomFile();
            dicomFile.Read(argument.structuredReportPath);

            // Perform sanity check if this is a valid Structured Report.
            bool valueTypeRootContentItemCorrect = false;
            DvtkHighLevelInterface.Dicom.Other.Attribute valueType = dicomFile.DataSet["0x0040A040"];
            if (valueType.Exists)
            {
                if (valueType.Values[0].Trim() == "CONTAINER")
                {
                    valueTypeRootContentItemCorrect = true;
                }
            }


            //
            // Validate against the loaded Context Groups and loaded definition files.
            //

            if (valueTypeRootContentItemCorrect)
            {
                // Report Progress.
                ReportProgress(40, "Parsing DICOM Structured Report: " + argument.structuredReportPath);

                // Parse the DICOM Structured Report.
                StructuredReport structuredReport = new StructuredReport(dicomFile.DataSet);

                // Report Progress.
                ReportProgress(60, "Validating content item values...");

                // Validate Content Items Value.
                ContentItemValueValidationRule contentItemValueValidationRule = new ContentItemValueValidationRule(contextGroups);
                structuredReport.RootContentItem.Accept(contentItemValueValidationRule);

                // Report Progress.
                ReportProgress(85, "Saving results: " + e.Argument.ToString());

                // Export structured report results to Xml file.
                String xmlFullFileName = Path.Combine(argument.xmlPath, "Output.xml");
                structuredReport.ToXml(xmlFullFileName);

                // Perform part 3 validation.
                this.mediaSession.ResultsRootDirectory = argument.xmlPath;
                this.mediaSession.StartResultsGathering("Part 3 validation.xml");
                //this.mediaSession.Validate(dicomFile.DvtkDataDicomFile, Dvtk.Sessions.ValidationControlFlags.None);
                this.mediaSession.ValidateMediaFiles(new string[] { argument.structuredReportPath });
                this.mediaSession.EndResultsGathering();

                // Report Progress.
                ReportProgress(100, "");
            }
            else
            {
                // Report Progress.
                ReportProgress(100, "");
                
                throw new Exception("Aborting validation because DICOM file does not contain Attribute Value Type (0040,A040) with value \"CONTAINER\" for root Content Item.");
            }
        }
    }
}
