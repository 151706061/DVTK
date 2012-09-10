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
    class DvtkValidationContext
    {
        private bool _generateSummaryResults = false;
        private bool _generateDetailedResults = false;

        public DvtkValidationContext(String xmlStream)
        {
            FromXml(xmlStream);
        }

        private void FromXml(String xmlStream)
        {
            try
            {
                StringReader stringReader = new StringReader(xmlStream);
                XmlTextReader reader = new XmlTextReader(stringReader);
                while (reader.EOF == false)
                {
                    reader.ReadStartElement("ValidationContext");
                    String generateSummaryResults = reader.ReadElementString("GenerateSummaryResults");
                    if (generateSummaryResults == "true")
                    {
                        _generateSummaryResults = true;
                    }
                    String generateDetailedResults = reader.ReadElementString("GenerateDetailedResults");
                    if (generateDetailedResults == "true")
                    {
                        _generateDetailedResults = true;
                    }
                    // ...
                    // EVS Specific Context properties
                    // ...
                    reader.ReadElementString("EvsSpecificContext");

                    reader.ReadEndElement();
                }
                reader.Close();
            }
            catch (Exception e)
            {
                String message = String.Format("Failed to FromXml() DVTK DICOM EVS Validation Context XML stream: \"{0}\". Error: \"{1}\"", xmlStream, e.Message);
                throw new Exception(message, e);
            }
        }

        #region Properties
        public bool GenerateSummaryResults
        {
            get
            {
                return _generateSummaryResults;
            }
        }

        public bool GenerateDetailedResults
        {
            get
            {
                return _generateDetailedResults;
            }
        }
        #endregion Properties
    }
}
