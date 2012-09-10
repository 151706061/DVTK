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
    class DvtkValidationServiceStatus
    {
        private String _status = "OK";
        private String _additionalStatusInfo = String.Empty;

        public String ToXml()
        {
            String xmlStream = String.Empty;
            try
            {
                StringWriter stringWriter = new StringWriter();
                XmlTextWriter writer = new XmlTextWriter(stringWriter);
                writer.Formatting = Formatting.Indented;
                writer.WriteStartDocument(true);
                writer.WriteStartElement("ValidationServiceStatus");
                writer.WriteElementString("Status", _status);
                writer.WriteElementString("AdditionalStatusInfo", _additionalStatusInfo);
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Flush();
                writer.Close();
                xmlStream = stringWriter.ToString();
            }
            catch (Exception e)
            {
                String message = String.Format("Failed to ToXml() DVTK DICOM EVS Validation Service Status XML stream. Error: \"{0}\"", e.Message);
                throw new Exception(message, e);
            }

            return xmlStream;
        }

        #region Properties
        public String Status
        {
            set
            {
                _status = value;
            }
            get
            {
                return _status;
            }
        }

        public String AdditionalStatusInfo
        {
            set
            {
                _additionalStatusInfo = value;
            }
            get
            {
                return _additionalStatusInfo;
            }
        }

        #endregion Properties
    }
}
