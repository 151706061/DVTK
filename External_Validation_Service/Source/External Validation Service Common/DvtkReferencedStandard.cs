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
    class DvtkReferencedStandard
    {
        private String _name = String.Empty;
        private String _version = String.Empty;

        public DvtkReferencedStandard(String xmlStream)
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
                    reader.ReadStartElement("ReferencedStandard");
                    _name = reader.ReadElementString("StandardName");
                    if (_name != "DICOM")
                    {
                        String message = String.Format("ReferencedStandard.StandardName is \"{0}\" - should be \"DICOM\"", _name);
                        throw new Exception(message);
                    }
                    _version = reader.ReadElementString("StandardVersion");
                    reader.ReadEndElement();
                }
                reader.Close();
            }
            catch (Exception e)
            {
                String message = String.Format("Failed to FromXml() DVTK DICOM EVS Referenced Standard XML stream: \"{0}\". Error: \"{1}\"", xmlStream, e.Message);
                throw new Exception(message, e);
            }
        }

        #region Properties
        public String Name
        {
            get
            {
                return _name;
            }
        }

        public String Version
        {
            get
            {
                return _version;
            }
        }
        #endregion Properties
    }
}
