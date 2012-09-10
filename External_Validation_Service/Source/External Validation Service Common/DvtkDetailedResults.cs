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
    class DvtkDetailedResults
    {
        private String _xmlFmiValidationResults = String.Empty;
        private String _xmlDatasetResults = String.Empty;
        private int _validationErrorCount = 0;
        private int _validationWarningCount = 0;
        private int _validationConditionCount = 0;
        private String _validationResult = String.Empty;

        public void FromXml(String filename)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(filename);
                while (reader.EOF == false)
                {
                    reader.ReadStartElement("DvtDetailedResultsFile");
                    reader.ReadToFollowing("ValidationResults");
                    _xmlFmiValidationResults = reader.ReadOuterXml();
                    reader.ReadToFollowing("ValidationResults");
                    _xmlDatasetResults = reader.ReadOuterXml();
                    reader.ReadToFollowing("ValidationCounters");
                    reader.ReadStartElement("ValidationCounters");
                    _validationErrorCount = int.Parse(reader.ReadElementString("NrOfValidationErrors"));
                    _validationWarningCount = int.Parse(reader.ReadElementString("NrOfValidationWarnings"));
                    _validationConditionCount = 0; // int.Parse(reader.ReadElementString("NrOfValidationConditions"));
                    reader.ReadElementString("NrOfGeneralErrors");
                    reader.ReadElementString("NrOfGeneralWarnings");
                    reader.ReadElementString("NrOfUserErrors");
                    reader.ReadElementString("NrOfUserWarnings");
                    _validationResult = reader.ReadElementString("ValidationTest");
                    reader.ReadEndElement();
                    reader.ReadEndElement();
                }
                reader.Close();
            }
            catch (Exception)
            {
                // do nothing here - we always end with an exception when reading the DVTK core detailed results due to a final CR LF in the file.
            }
        }

        #region Properties
        public String XmlFmiValidationResults
        {
            get
            {
                return _xmlFmiValidationResults;
            }
        }

        public String XmlDatasetResults
        {
            get
            {
                return _xmlDatasetResults;
            }
        }

        public int ValidationErrorCount
        {
            get
            {
                return _validationErrorCount;
            }
        }

        public int ValidationWarningCount
        {
            get
            {
                return _validationWarningCount;
            }
        }

        public int ValidationConditionCount
        {
            get
            {
                return _validationConditionCount;
            }
        }

        public String ValidationResult
        {
            get
            {
                return _validationResult;
            }
        }
        #endregion Properties
    }
}
