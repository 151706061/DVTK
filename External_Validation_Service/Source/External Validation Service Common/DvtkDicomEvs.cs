// ------------------------------------------------------
// DVTk - The Healthcare Validation Toolkit (www.dvtk.org)
// Copyright © 2009 DVTk
// ------------------------------------------------------
// This file is part of DVTk.
//
// DVTk is free software; you can redistribute it and/or modify it under the terms of the GNU
// Lesser General Public License as published by the Free Software Foundation; either version 3.0
// of the License, or (at your option) any later version. 
// 
// DVTk is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even
// the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU Lesser
// General Public License for more details. 
// 
// You should have received a copy of the GNU Lesser General Public License along with this
// library; if not, see <http://www.gnu.org/licenses/>

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Dvtk.Sessions;

namespace EvsService
{
    class DvtkDicomEvs
    {
        private static String BinaryDataFilename = "binaryData";
        private static String ResultFilename = "result.xml";
        private static String DvtkDetailedResultFilename = "Detail_" + ResultFilename;
        private static String DvtkSummaryResultFilename = "Summary_" + ResultFilename;
        private static String EvsDetailedResultFilename = "EvsDetail_" + ResultFilename;
        private static String EvsSummaryResultFilename = "EvsSummary_" + ResultFilename;

        private EvsServiceLogger _evsServiceLogger = new EvsServiceLogger();
        private DvtkDicomEvsConfig _dvtkDicomEvsConfig = new DvtkDicomEvsConfig();
        private MediaSession _mediaSession = null;
        private DvtkReferencedStandard _dvtkReferencedStandard = null;
        private DvtkValidationContext _dvtkValidationContext = null;
        private DvtkObjectMetaData _dvtkObjectMetaData = null;
        private DvtkValidationServiceStatus _dvtkValidationServiceStatus = new DvtkValidationServiceStatus();

        public DvtkDicomEvs()
        {
            Initialize();
        }

        public string Validate(string oid,
            string xmlReferencedStandard,
            string xmlValidationContext,
            string xmlObjectMetaData,
            byte[] binaryObjectData)
        {
            // check if oid is defined
            if (oid == String.Empty)
            {
                oid = "DVTK-DICOM-EVS-" + Guid.NewGuid().ToString();
            }

            // set up the results overview to return
            DvtkResultsOverview resultsOverview = new DvtkResultsOverview();
            resultsOverview.Oid = oid;
            resultsOverview.ValidationServiceName = "DVTK DICOM EVS";
            resultsOverview.ValidationServiceVersion = "1.0.1";
            resultsOverview.ValidationDate = System.DateTime.Now.ToString("yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
            resultsOverview.ValidationTime = System.DateTime.Now.ToString("HHmmss", System.Globalization.CultureInfo.InvariantCulture);
            resultsOverview.ValidationTestId = oid;
            resultsOverview.ValidationTestResult = "FAILED";

            try
            {
                _evsServiceLogger.LogDebug("DVTK-DICOM-EVS Validate:");
                _evsServiceLogger.LogDebug("oid = {0}", oid);
                _evsServiceLogger.LogDebug("xmlReferencedStandard = {0}", xmlReferencedStandard);
                _evsServiceLogger.LogDebug("xmlValidationContext = {0}", xmlValidationContext);
                _evsServiceLogger.LogDebug("xmlObjectMetaData = {0}", xmlObjectMetaData);
                _evsServiceLogger.LogDebug("binaryObjectData length = {0}", binaryObjectData.Length);

                // parse the referenced standard xml
                _dvtkReferencedStandard = new DvtkReferencedStandard(xmlReferencedStandard);
                resultsOverview.StandardName = _dvtkReferencedStandard.Name;
                resultsOverview.StandardVersion = _dvtkReferencedStandard.Version;

                // not sure what to do with the referenced standard properties yet

                // parse the validation context xml
                _dvtkValidationContext = new DvtkValidationContext(xmlValidationContext);

                // set the results files to be generated
                _mediaSession.DetailedValidationResults = _dvtkValidationContext.GenerateDetailedResults;
                _mediaSession.SummaryValidationResults = _dvtkValidationContext.GenerateSummaryResults;

                // parse the object meta data xml
                _dvtkObjectMetaData = new DvtkObjectMetaData(xmlObjectMetaData);

                // create the binary data file extension
                String fileExtension = String.Empty;
                switch (_dvtkObjectMetaData.DicomBinaryObjectDataType)
                {
                    case DicomBinaryObjectDataType.MediaFile:
                        fileExtension = ".dcm";
                        break;
                    case DicomBinaryObjectDataType.CommandSet:
                    case DicomBinaryObjectDataType.DataSet:
                        fileExtension = ".raw";
                        break;
                    default:
                        break;
                }

                // ensure that the message directory is present
                String messageDirectory = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory;
                DirectoryInfo dirInfo = new DirectoryInfo(messageDirectory);
                if (dirInfo.Exists != true)
                {
                    dirInfo.Create();
                }

                // create the oid storage directory for the message to be validated and the corresponding results
                String oidDirectory = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory + @"\" + oid;
                dirInfo = new DirectoryInfo(oidDirectory);
                if (dirInfo.Exists == true)
                {
                    // delete the directory and any contents if it already exists - this is the case when the same OID is used to 
                    // validate files without cleaning the cache in between
                    dirInfo.Delete(true);
                }
                // create the directory
                dirInfo.Create();

                // save the binary data to a file in the message directory
                String binaryDataFilename = oidDirectory + @"\" + BinaryDataFilename + fileExtension;
                _evsServiceLogger.LogDebug("Cached Binary Data Filename = \"{0}\"", binaryDataFilename);
                FileStream stream = new FileStream(binaryDataFilename, FileMode.CreateNew);
                BinaryWriter writer = new BinaryWriter(stream);
                writer.Write(binaryObjectData);
                writer.Flush();
                writer.Close();
                stream.Close();

                // create the results directory
                String resultsDirectory = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory + @"\" + oid + @"\results";
                dirInfo = new DirectoryInfo(resultsDirectory);
                if (dirInfo.Exists != true)
                {
                    dirInfo.Create();
                }

                // use the DVTK core to validate the binary data file
                _mediaSession.ResultsRootDirectory = resultsDirectory;
                _mediaSession.StartResultsGathering(ResultFilename);
                String[] list = new String[1];
                list[0] = binaryDataFilename;
                _mediaSession.ValidateMediaFiles(list);
                _mediaSession.EndResultsGathering();

                // validation result is PASSED when no errors and warnings are present
                resultsOverview.ValidationTestResult = ((_mediaSession.NrOfErrors == 0) && (_mediaSession.NrOfWarnings == 0)) ? "PASSED" : "FAILED";
            }
            catch (System.Exception e)
            {
                _dvtkValidationServiceStatus.Status = "Error";
                _dvtkValidationServiceStatus.AdditionalStatusInfo = e.Message;
                _evsServiceLogger.LogError(e.Message);
            }

            resultsOverview.ValidationServiceStatus = _dvtkValidationServiceStatus.Status;
            resultsOverview.ValidationServiceAdditionalStatusInfo = _dvtkValidationServiceStatus.AdditionalStatusInfo;

            // convert the results files
            if ((_dvtkDicomEvsConfig.DefaultDvtkXmlResultsFormat == false) &&
                (_dvtkValidationContext.GenerateSummaryResults == true))
            {
                ConvertSummaryResults(resultsOverview);
            }
            if ((_dvtkDicomEvsConfig.DefaultDvtkXmlResultsFormat == false) &&
                (_dvtkValidationContext.GenerateDetailedResults == true))
            {
                ConvertDetailedResults(resultsOverview);
            }

            return resultsOverview.ToXml();
        }

        public string GetSummaryResults(string oid)
        {
            _evsServiceLogger.LogDebug("DVTK-DICOM-EVS GetSummaryResults:");
            _evsServiceLogger.LogDebug("oid = {0}", oid);

            String summaryResultsFilename = GetEvsSummaryResultsFilename(oid);
            FileInfo fileInfo = new FileInfo(summaryResultsFilename);
            if (fileInfo.Exists == false)
            {
                String message = String.Format("DVTK-DICOM-EVS - Summary Results File \"{0}\" not found.", summaryResultsFilename);
                throw new System.Exception(message);
            }
            StreamReader streamReader = new StreamReader(summaryResultsFilename);
            return streamReader.ReadToEnd();
        }

        public string GetDetailedResults(string oid)
        {
            _evsServiceLogger.LogDebug("DVTK-DICOM-EVS GetDetailedResults:");
            _evsServiceLogger.LogDebug("oid = {0}", oid);

            String detailedResultsFilename = GetEvsDetailedResultsFilename(oid);
            FileInfo fileInfo = new FileInfo(detailedResultsFilename);
            if (fileInfo.Exists == false)
            {
                String message = String.Format("DVTK-DICOM-EVS - Detailed Results File \"{0}\" not found.", detailedResultsFilename);
                throw new System.Exception(message);
            }
            StreamReader streamReader = new StreamReader(detailedResultsFilename);
            return streamReader.ReadToEnd();
        }

        public void ClearResultsCache()
        {
            _evsServiceLogger.LogDebug("DVTK-DICOM-EVS ClearResultsCache:");
            RemoveCachedData();
        }

        public string GetValidationServiceStatus()
        {
            _evsServiceLogger.LogDebug("DVTK-DICOM-EVS GetValidationServiceStatus:");
            return _dvtkValidationServiceStatus.ToXml();
        }

        #region private methods
        private void Initialize()
        {
            String baseDirectory = System.AppDomain.CurrentDomain.BaseDirectory;

            // set up the logfile
            _evsServiceLogger.Filename = baseDirectory + "DvtkDicomEvsLogFile.txt";

            // load the fixed configuration filename from the base directory
            _dvtkDicomEvsConfig.LoadConfig();

            // define the log level
            _evsServiceLogger.LogLevel = _dvtkDicomEvsConfig.EvsLogLevel;

            try
            {
                // check all the required directories / files are present
                CheckDirectoryPresence(baseDirectory);

                // create the DVTK media session - using the default session file
                _mediaSession = MediaSession.LoadFromFile(baseDirectory + _dvtkDicomEvsConfig.ConfigurationSubDirectory + @"\" + "DvtkDicomEvs.ses");

                // set the remaining session parameters
                _mediaSession.ResultsRootDirectory = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory + @"\results";
                _mediaSession.DefinitionManagement.DefinitionFileRootDirectory = baseDirectory + @"\" + _dvtkDicomEvsConfig.DefinitionSubDirectory;

                // load the definition files
                LoadDefinitionFiles(baseDirectory);

                // set the validation service status
                _dvtkValidationServiceStatus.Status = "OK";
            }
            catch (System.Exception e)
            {
                _evsServiceLogger.LogError("Initialize Exception: {0}", e.Message);
                _evsServiceLogger.LogError("with Inner Exception: {0}", e.InnerException.Message);
                throw e;
            }
        }

        private void CheckDirectoryPresence(String baseDirectory)
        {
            // check that all configured directories are present
            // - Base cache directory
            DirectoryInfo dirInfo = new DirectoryInfo(_dvtkDicomEvsConfig.BaseCacheDirectory);
            if (dirInfo.Exists == false)
            {
                dirInfo.Create();
            }

            // ensure that the message subdirectory is present
            dirInfo = new DirectoryInfo(_dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory);
            if (dirInfo.Exists == false)
            {
                dirInfo.Create();
            }

            // ensure that the definition subdirectory is present - it should be as there ought to be definition files present
            dirInfo = new DirectoryInfo(baseDirectory + _dvtkDicomEvsConfig.DefinitionSubDirectory);
            if (dirInfo.Exists == false)
            {
                dirInfo.Create();
            }
        }

        private void LoadDefinitionFiles(String baseDirectory)
        {
            try
            {
                DirectoryInfo dirInfo = new DirectoryInfo(baseDirectory + _dvtkDicomEvsConfig.DefinitionSubDirectory);
                FileInfo[] fileList = dirInfo.GetFiles("*.def");
                foreach (FileInfo fileInfo in fileList)
                {
                    _mediaSession.DefinitionManagement.LoadDefinitionFile(fileInfo.Name);
                }
            }
            catch (System.Exception e)
            {
                String message = String.Format("DVTK-DICOM-EVS - Problem loading Definition Files: {0}.", e.Message);
                throw new System.Exception(message, e.InnerException);
            }
        }

        private void RemoveCachedData()
        {
            try
            {
                // ensure that the message subdirectory is present
                DirectoryInfo dirInfo = new DirectoryInfo(_dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory);
                dirInfo.Delete(true);
            }
            catch (System.Exception)
            {
                // exception thrown if some files are currently being accessed by another process - these will be picked up on the next call.
            }
        }

        private void ConvertSummaryResults(DvtkResultsOverview resultsOverview)
        {
            // parse the dvtk summary results
            DvtkSummaryResults dvtkSummaryResults = new DvtkSummaryResults();
            String dvtkSummaryResultsFilename = GetDvtkSummaryResultsFilename(resultsOverview.Oid);
            dvtkSummaryResults.FromXml(dvtkSummaryResultsFilename);

            // set up the evs summary results
            EvsSummaryResults evsSummaryResults = new EvsSummaryResults();
            evsSummaryResults.ValidationResultsOverview = resultsOverview;
            evsSummaryResults.XmlFmiValidationResults = dvtkSummaryResults.XmlFmiValidationResults;
            evsSummaryResults.XmlDatasetResults = dvtkSummaryResults.XmlDatasetResults;
            evsSummaryResults.ValidationErrorCount = dvtkSummaryResults.ValidationErrorCount;
            evsSummaryResults.ValidationWarningCount = dvtkSummaryResults.ValidationWarningCount;
            evsSummaryResults.ValidationConditionCount = dvtkSummaryResults.ValidationConditionCount;
            evsSummaryResults.ValidationResult = dvtkSummaryResults.ValidationResult;

            // save the evs summary results file
            String evsSummaryResultsFilename = GetEvsSummaryResultsFilename(resultsOverview.Oid);
            evsSummaryResults.Save(evsSummaryResultsFilename);

            // delete the dvtk summary results file
            try
            {
                FileInfo dvtkSummaryResultsFileInfo = new FileInfo(dvtkSummaryResultsFilename);
                dvtkSummaryResultsFileInfo.Delete();
            }
            catch (System.Exception)
            {
                // exception thrown if the file is currently being accessed by another process - this will be picked up on the next RemoveCachedData() call.
            }
        }

        private void ConvertDetailedResults(DvtkResultsOverview resultsOverview)
        {
            // parse the dvtk detailed results
            DvtkDetailedResults dvtkDetailedResults = new DvtkDetailedResults();
            String dvtkDetailedResultsFilename = GetDvtkDetailedResultsFilename(resultsOverview.Oid);
            dvtkDetailedResults.FromXml(dvtkDetailedResultsFilename);

            // set up the evs detailed results
            EvsDetailedResults evsDetailedResults = new EvsDetailedResults();
            evsDetailedResults.ValidationResultsOverview = resultsOverview;
            evsDetailedResults.XmlFmiValidationResults = dvtkDetailedResults.XmlFmiValidationResults;
            evsDetailedResults.XmlDatasetResults = dvtkDetailedResults.XmlDatasetResults;
            evsDetailedResults.ValidationErrorCount = dvtkDetailedResults.ValidationErrorCount;
            evsDetailedResults.ValidationWarningCount = dvtkDetailedResults.ValidationWarningCount;
            evsDetailedResults.ValidationConditionCount = dvtkDetailedResults.ValidationConditionCount;
            evsDetailedResults.ValidationResult = dvtkDetailedResults.ValidationResult;

            // save the evs detailed results file
            String evsDetailedResultsFilename = GetEvsDetailedResultsFilename(resultsOverview.Oid);
            evsDetailedResults.Save(evsDetailedResultsFilename);

            // delete the dvtk detailed results file
            try
            {
                FileInfo dvtkDetailedResultsFileInfo = new FileInfo(dvtkDetailedResultsFilename);
                dvtkDetailedResultsFileInfo.Delete();
            }
            catch (System.Exception)
            {
                // exception thrown if the file is currently being accessed by another process - this will be picked up on the next RemoveCachedData() call.
            }
        }

        private String GetDvtkSummaryResultsFilename(String oid)
        {
            String dvtkSummaryResultsFilename = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory + @"\" + oid + @"\results\" + DvtkSummaryResultFilename;
            return dvtkSummaryResultsFilename;
        }

        private String GetDvtkDetailedResultsFilename(String oid)
        {
            String dvtkDetailedResultsFilename = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory + @"\" + oid + @"\results\" + DvtkDetailedResultFilename;
            return dvtkDetailedResultsFilename;
        }

        private String GetEvsSummaryResultsFilename(String oid)
        {
            String evsSummaryResultsFilename = String.Empty;
            if (_dvtkDicomEvsConfig.DefaultDvtkXmlResultsFormat == true)
            {
                evsSummaryResultsFilename = GetDvtkSummaryResultsFilename(oid);
            }
            else
            {
                evsSummaryResultsFilename = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory + @"\" + oid + @"\results\" + EvsSummaryResultFilename;
            }
            return evsSummaryResultsFilename;
        }

        private String GetEvsDetailedResultsFilename(String oid)
        {
            String evsDetailedResultsFilename = String.Empty;
            if (_dvtkDicomEvsConfig.DefaultDvtkXmlResultsFormat == true)
            {
                evsDetailedResultsFilename = GetDvtkDetailedResultsFilename(oid);
            }
            else
            {
                evsDetailedResultsFilename = _dvtkDicomEvsConfig.BaseCacheDirectory + @"\" + _dvtkDicomEvsConfig.MessageSubDirectory + @"\" + oid + @"\results\" + EvsDetailedResultFilename;
            }
            return evsDetailedResultsFilename;
        }
        #endregion private methods
    }
}
