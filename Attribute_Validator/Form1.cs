using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

using DvtkApplicationLayer.UserInterfaces;
using DvtkHighLevelInterface.Common.Threads;
using DvtkHighLevelInterface.Dicom.Threads;
using DataSetSpecification = Dvtk.Dicom.AttributeLayer.Specification.DataSet;
using SopClass = Dvtk.Dicom.AttributeLayer.Specification.SopClass;
using SopClasses = Dvtk.Dicom.AttributeLayer.Specification.SopClasses;
using SopClassDocumentRelationshipMacroFixVisitor = Dvtk.Dicom.AttributeLayer.Specification.SopClassDocumentRelationshipMacroFixVisitor;
using AttributeSetResultsLoggingVisitor = Dvtk.Dicom.AttributeLayer.Validation.AttributeSetResultsLoggingVisitor;
using DimseDataSetPairSpecification = Dvtk.Dicom.AttributeLayer.Specification.DimseDataSetPair;

namespace Attribute_Validator
{
    public partial class Form1 : Form
    {
        private SopClasses sopClasses = new SopClasses();

        public Form1()
        {
            InitializeComponent();

            string styleSheetPath = Path.Combine(Application.StartupPath, "DVT_RESULTS.xslt");
            this.dvtkWebBrowserSummary.XmlStyleSheetFullFileName = styleSheetPath;
            this.dvtkWebBrowserDetail.XmlStyleSheetFullFileName = styleSheetPath;

            try
            {
                this.sopClasses.Load(Path.Combine(Application.StartupPath, "DicomRawXml"), "*.xml");
            }
            catch (Exception exception)
            {
                MessageBox.Show("While loading the raw xml files, the following exception occured: \n" + exception.Message);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            DialogResult dialogResult = fileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    Validate(fileDialog.FileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("While validating file, the following exception occured: \n" + exception.Message);
                }
            } 
        }

        private void Validate(string dicomFilePath)
        {
            // Dvtk.Setup.Initialize();


            //
            // Read the DICOM file and determine the SOP class
            //

            string sopClassUid = string.Empty;

            DvtkHighLevelInterface.Dicom.Files.DicomFile hliDicomFile = new DvtkHighLevelInterface.Dicom.Files.DicomFile();
            hliDicomFile.Read(dicomFilePath);

            sopClassUid = hliDicomFile.FileMetaInformation.MediaStorageSOPClassUID;


            //
            // Determine if exactly one DataSet is present in the specification for this SOP Class UID
            // and DimseCommand C-STORE-RQ.
            //

            List<DimseDataSetPairSpecification> dimseDataSetPairSpecifications = this.sopClasses.GetDimseDataSetPairs(sopClassUid, "C-STORE-RQ");

            if (dimseDataSetPairSpecifications.Count == 0)
            {
                MessageBox.Show("No data set specification found for SOP Class UID " + sopClassUid + " and Dimse Command C-STORE-RQ");
            }
            else if (dimseDataSetPairSpecifications.Count > 1)
            {
                string text = "Unable to determine which data set specification to use.\nFollowing xml files contain data sets for SOP Class UID " + sopClassUid + " and Dimse Command C-STORE-RQ:\n";

                foreach (DimseDataSetPairSpecification dimseDataSetPairSpecification in dimseDataSetPairSpecifications)
                {
                    text += "- " + dimseDataSetPairSpecification.Parent.Path;
                }

                MessageBox.Show(text);
            }
            else
            {
                // Exactly one Data Set specification has been found to validate against.

                DimseDataSetPairSpecification dimseDataSetPairSpecification = dimseDataSetPairSpecifications[0];


                //
                // Convert the Data Set from the loaded definition file.
                //

                Dvtk.Dicom.AttributeLayer.DataSet dataSet = new Dvtk.Dicom.AttributeLayer.DataSet();
                Dvtk.Dicom.Conversion.Convert.ToAttributeSet(hliDicomFile.DataSet, dataSet);


                //
                // Perform the following fix.
                //

                SopClassDocumentRelationshipMacroFixVisitor sopClassDocumentRelationshipMacroFixVisitor = new SopClassDocumentRelationshipMacroFixVisitor();
                dimseDataSetPairSpecification.Accept(sopClassDocumentRelationshipMacroFixVisitor);


                //
                // Transform the current data object structure to contain extra information
                // and make sure the Attribute lists are sorted ascending using the tag.
                //

                dimseDataSetPairSpecification.CreateAttributeStructure();


                //
                // Perform the actual mapping.
                //

                Dvtk.Dicom.AttributeLayer.Validation.AttributeMapper.Map(dataSet, dimseDataSetPairSpecification.DataSet);


                //
                // Write the results in the summary and detailed logging.
                //

                string resultsDirectory = Path.Combine(Application.StartupPath, "Results");

                Directory.CreateDirectory(resultsDirectory);

                ThreadManager threadManager = new ThreadManager();
                string description =  "<b>Validating " + dicomFilePath + " with SOP Class UID " + sopClassUid + " using " + dimseDataSetPairSpecification.Parent.Path + "</b>";
                ValidatorDicomThread validatorDicomThread = new ValidatorDicomThread(description, dataSet);
                validatorDicomThread.Initialize(threadManager);

                validatorDicomThread.Options.ResultsDirectory = resultsDirectory;
                validatorDicomThread.Options.Name = "Validator";
                validatorDicomThread.Options.Identifier = validatorDicomThread.Options.Name;

                validatorDicomThread.Start();
                validatorDicomThread.WaitForCompletion();


                //
                // Update the two web controls with the newly created summary and detail results.
                //

                this.dvtkWebBrowserSummary.Navigate(validatorDicomThread.Options.SummaryResultsFullFileName);
                this.dvtkWebBrowserDetail.Navigate(validatorDicomThread.Options.DetailResultsFullFileName);
            }
        }

    }

    public class ValidatorDicomThread : DicomThread
    {
        private Dvtk.Dicom.AttributeLayer.DataSet dataSet = null;

        private string description = string.Empty;

        private ValidatorDicomThread()
        {

        }

        public ValidatorDicomThread(string description, Dvtk.Dicom.AttributeLayer.DataSet dataSet)
        {
            this.description = description;
            this.dataSet = dataSet;
        }

        protected override void Execute()
        {
            WriteHtml(this.description, true, true);
            WriteHtml("", true, true);

            AttributeSetResultsLoggingVisitor attributeSetResultsLoggingVisitor = new AttributeSetResultsLoggingVisitor(this);

            this.dataSet.Accept(attributeSetResultsLoggingVisitor);
        }
    }
}