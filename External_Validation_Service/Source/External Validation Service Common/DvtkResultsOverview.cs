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
    class DvtkResultsOverview
    {
        private String _oid = String.Empty;
        private String _standardName = String.Empty;
        private String _standardVersion = String.Empty;
        private String _validationServiceName = String.Empty;
        private String _validationServiceVersion = String.Empty;
        private String _validationServiceStatus = String.Empty;
        private String _validationServiceAdditionalStatusInfo = String.Empty;
        private String _validationDate = String.Empty;
        private String _validationTime = String.Empty;
        private String _validationTestId = String.Empty;
        private String _validationTestResult = String.Empty;

        public String ToXml()
        {
            String xmlStream = String.Empty;
            try
            {
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter writer = new XmlTextWriter(stringWriter);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);
                BodyToXml(writer);
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
                xmlStream = stringWriter.ToString();
            }
            catch (Exception e)
            {
                String message = String.Format("Failed to ToXml() DVTK DICOM EVS Validation Results Overview XML stream. Error: \"{0}\"", e.Message);
                throw new Exception(message, e);
            }

            return xmlStream;
        }

        public void BodyToXml(XmlTextWriter writer)
        {
            writer.WriteStartElement("ValidationResultsOverview");
            writer.WriteElementString("Oid", _oid);

            writer.WriteStartElement("ReferencedStandard");
            writer.WriteElementString("StandardName", _standardName);
            writer.WriteElementString("StandardVersion", _standardVersion);
            writer.WriteEndElement();

            writer.WriteElementString("ValidationServiceName", _validationServiceName);
            writer.WriteElementString("ValidationServiceVersion", _validationServiceVersion);

            writer.WriteStartElement("ValidationServiceStatus");
            writer.WriteElementString("Status", _validationServiceStatus);
            writer.WriteElementString("AdditionalStatusInfo", _validationServiceAdditionalStatusInfo);
            writer.WriteEndElement();

            writer.WriteElementString("ValidationDate", _validationDate);
            writer.WriteElementString("ValidationTime", _validationTime);
            writer.WriteElementString("ValidationTestId", _validationTestId);
            writer.WriteElementString("ValidationTest", _validationTestResult);

            writer.WriteEndElement();
        }

        #region Properties
        public String Oid
        {
            set
            {
                _oid = value;
            }
            get
            {
                return _oid;
            }
        }

        public String StandardName
        {
            set
            {
                _standardName = value;
            }
        }

        public String StandardVersion
        {
            set
            {
                _standardVersion = value;
            }
        }

        public String ValidationServiceName
        {
            set
            {
                _validationServiceName = value;
            }
        }

        public String ValidationServiceVersion
        {
            set
            {
                _validationServiceVersion = value;
            }
        }

        public String ValidationServiceStatus
        {
            set
            {
                _validationServiceStatus = value;
            }
        }

        public String ValidationServiceAdditionalStatusInfo
        {
            set
            {
                _validationServiceAdditionalStatusInfo = value;
            }
        }

        public String ValidationDate
        {
            set
            {
                _validationDate = value;
            }
        }

        public String ValidationTime
        {
            set
            {
                _validationTime = value;
            }
        }

        public String ValidationTestId
        {
            set
            {
                _validationTestId = value;
            }
        }

        public String ValidationTestResult
        {
            set
            {
                _validationTestResult = value;
            }
        }
        #endregion Properties
    }
}
