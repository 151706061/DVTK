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

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace EvsService
{
    class EvsSummaryResults
    {
        private DvtkResultsOverview _validationResultsOverview = null;
        private String _xmlFmiValidationResults = String.Empty;
        private String _xmlDatasetResults = String.Empty;
        private int _validationErrorCount = 0;
        private int _validationWarningCount = 0;
        private int _validationConditionCount = 0;
        private String _validationResult = String.Empty;

        public void Save(String filename)
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(filename, System.Text.Encoding.ASCII);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);
                ToXml(writer);
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                String message = String.Format("Failed to write DVTK DICOM EVS Summary Results file: \"{0}\". Error: \"{1}\"", filename, e.Message);
                throw new Exception(message, e);
            }
        }

        private void ToXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("SummaryResults");

            // write the results overview
            _validationResultsOverview.BodyToXml(writer);

            // check if there is an FMI result
            if (_xmlFmiValidationResults != String.Empty)
            {
                writer.WriteRaw(_xmlFmiValidationResults);
            }

            // write the dataset results
            writer.WriteRaw(_xmlDatasetResults);

            // write the counters
            writer.WriteStartElement("ValidationCounters");
            writer.WriteElementString("NrOfValidationErrors", _validationErrorCount.ToString());
            writer.WriteElementString("NrOfValidationWarnings", _validationWarningCount.ToString());
            writer.WriteElementString("NrOfValidationConditions", _validationConditionCount.ToString());
            writer.WriteElementString("ValidationTest", _validationResult);
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        #region Properties
        public DvtkResultsOverview ValidationResultsOverview
        {
            set
            {
                _validationResultsOverview = value;
            }
        }

        public String XmlFmiValidationResults
        {
            set
            {
                _xmlFmiValidationResults = value;
            }
        }

        public String XmlDatasetResults
        {
            set
            {
                _xmlDatasetResults = value;
            }
        }

        public int ValidationErrorCount
        {
            set
            {
                _validationErrorCount = value;
            }
        }

        public int ValidationWarningCount
        {
            set
            {
                _validationWarningCount = value;
            }
        }

        public int ValidationConditionCount
        {
            set
            {
                _validationConditionCount = value;
            }
        }

        public String ValidationResult
        {
            set
            {
                _validationResult = value;
            }
        }
        #endregion Properties
    }
}
