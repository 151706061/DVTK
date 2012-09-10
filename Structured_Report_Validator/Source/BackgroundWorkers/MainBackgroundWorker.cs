using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Dvtk.Xml.Transformation;

namespace Dvtk.StructuredReportValidator.BackgroundWorkers
{
    class MainBackgroundWorker: BackgroundWorker
    {
        private bool validationCompleted = false;

        private bool xmlToHtmlCompleted = false;

        private Exception exception = null;

        private string fieldLock = string.Empty;

        private string templatesPath = string.Empty;

        /// <summary>
        /// The background worker that generates .xml files containing the validation results.
        /// </summary>
        private ValidatorBackgroundWorker validatorBackgroundWorker = null;

        /// <summary>
        /// The background worker that converts .xml files to .html files.
        /// </summary>
        private XsltProcessor xsltProcessor = null;


        
        /// <summary>
        /// Hide default constructor.
        /// </summary>
        private MainBackgroundWorker()
        {
            // Do nothing.
        }


        public MainBackgroundWorker(string templatesPath)
        {
            this.templatesPath = templatesPath;

            WorkerReportsProgress = true;


            //
            // Create the background workers.
            //

            this.validatorBackgroundWorker = new ValidatorBackgroundWorker();
            this.validatorBackgroundWorker.WorkerReportsProgress = true;

            this.xsltProcessor = new XsltProcessor();
            

            //
            // Assign methods to handle these events.
            //

            DoWork += HandleDoWork;
            this.validatorBackgroundWorker.ProgressChanged += ValidatorBackgroundWorkerHandleProgressChanged;
            this.validatorBackgroundWorker.RunWorkerCompleted += ValidatorBackgroundWorkerHandleRunWorkerCompleted;
            this.xsltProcessor.ProgressChanged += XsltProcessorHandleProgressChanged;
            this.xsltProcessor.ProcessingCompleted += XsltProcessorHandleRunWorkerCompleted;
        }

        private void HandleDoWork(object sender, DoWorkEventArgs e)
        {
            MainBackgroundWorkerArgument argument = (MainBackgroundWorkerArgument)e.Argument;

            lock (this.fieldLock)
            {
                this.validationCompleted = false;
                this.xmlToHtmlCompleted = false;
                this.exception = null;
            }


            //
            // Perform the validation in the background.
            //

            ValidatorBackgroundWorkerArgument validatorBackgroundWorkerArgument = new ValidatorBackgroundWorkerArgument();
            validatorBackgroundWorkerArgument.structuredReportPath = argument.structuredReportPath;
            validatorBackgroundWorkerArgument.xmlPath = argument.xmlPath;
            this.validatorBackgroundWorker.RunWorkerAsync(validatorBackgroundWorkerArgument);


            //
            // Wait until validation has been completed.
            //

            bool wait = true;

            while (wait)
            {
                lock (this.fieldLock)
                {
                    wait = !this.validationCompleted;
                }

                if (wait)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }


            //
            // If an exception has been thrown during validation, rethrow it again.
            //

            lock (this.fieldLock)
            {
                if (this.exception != null)
                {
                    throw (this.exception);
                }
            }


            //
            // Convert the .xml files to .html files.
            //

            // Iterate through all template files in the templates folder.
            foreach (string templatePath in Directory.GetFiles(this.templatesPath))
            {
                // Create for each template a Xslt Transformation Workitem and place it in the queue.
                string xmlFile = Path.Combine(argument.xmlPath, "Output.xml");
                string htmlFile = Path.Combine(argument.htmlPath, Path.GetFileNameWithoutExtension(templatePath) + ".html");
                xsltProcessor.AddWorkItem(new WorkItem(xmlFile, templatePath, htmlFile));
            }

            string part3ValidationStyleSheetTransformationFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DVT_RESULTS.xslt");

            string part3ValidationSummaryXmlFile = Path.Combine(argument.xmlPath, "Summary_Part 3 validation.xml");
            string part3ValidationSummaryHtmlFile = Path.Combine(argument.htmlPath, "Part 3 validation summary.html");
            xsltProcessor.AddWorkItem(new WorkItem(part3ValidationSummaryXmlFile, part3ValidationStyleSheetTransformationFile, part3ValidationSummaryHtmlFile));

            string part3ValidationDetailXmlFile = Path.Combine(argument.xmlPath, "Detail_Part 3 validation.xml");
            string part3ValidationDetailHtmlFile = Path.Combine(argument.htmlPath, "Part 3 validation detail.html");
            xsltProcessor.AddWorkItem(new WorkItem(part3ValidationDetailXmlFile, part3ValidationStyleSheetTransformationFile, part3ValidationDetailHtmlFile));

            // Process the queue.
            xsltProcessor.StartAsync();


            //
            // Wait until .xml to .html conversion has been completed.
            //

            wait = true;

            while (wait)
            {
                lock (this.fieldLock)
                {
                    wait = !this.xmlToHtmlCompleted;
                }

                if (wait)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }


            //
            // If an exception has been thrown during the xml to html conversion, rethrow it again.
            //

            lock (this.fieldLock)
            {
                if (this.exception != null)
                {
                    throw (this.exception);
                }
            }
        }

        private void ValidatorBackgroundWorkerHandleProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportProgress(e.ProgressPercentage / 2, e.UserState);
        }

        private void ValidatorBackgroundWorkerHandleRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                lock (this.fieldLock)
                {
                    this.exception = e.Error;
                }
            }
            else if (e.Cancelled)
            {
                // Not yet implemented.
            }

            lock (this.fieldLock)
            {
                this.validationCompleted = true;
            }
        }

        private void XsltProcessorHandleProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ReportProgress(50 + (e.ProgressPercentage) / 2, e.UserState);
        }

        private void XsltProcessorHandleRunWorkerCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                this.exception = e.Error;
            }
            else if (e.Cancelled)
            {
                // Not yet implemented.
            }
            
            lock (this.fieldLock)
            {
                this.xmlToHtmlCompleted = true;
            }
        }
    }
}
