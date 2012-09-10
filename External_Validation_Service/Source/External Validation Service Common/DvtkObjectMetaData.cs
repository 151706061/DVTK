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
    enum DicomBinaryObjectDataType
    {
        MediaFile,
        CommandSet,
        DataSet,
        Unknown
    }

    class DvtkObjectMetaData
    {
        private DicomBinaryObjectDataType _dicomBinaryObjectDataType = DicomBinaryObjectDataType.Unknown;
        private String _sopClassUid = String.Empty;
        private String _sopInstanceUid = String.Empty;
        private String _transferSyntaxUid = String.Empty;

        public DvtkObjectMetaData(String xmlStream)
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
                    reader.ReadStartElement("ObjectMetaData");
                    reader.ReadStartElement("EvsSpecificMetaData");
                    String dicomBinaryObjectDataType = reader.ReadElementString("DicomBinaryObjectDataType");
                    switch(dicomBinaryObjectDataType)
                    {
                        case "MediaFile":
                            _dicomBinaryObjectDataType = DicomBinaryObjectDataType.MediaFile;
                            break;
                        case "CommandSet":
                            _dicomBinaryObjectDataType = DicomBinaryObjectDataType.CommandSet;
                            break;
                        case "DataSet":
                            _dicomBinaryObjectDataType = DicomBinaryObjectDataType.DataSet;
                            break;
                        default:
                            String message = String.Format("ObjectMetaData.EvsSpecificMetaData.DicomBinaryObjectDataType is \"{0}\" - should be \"MediaFile\", \"CommandSet\" or \"DataSet\"", dicomBinaryObjectDataType);
                            throw new Exception(message);
                    }
                    _sopClassUid = reader.ReadElementString("DicomSopClassUid");
                    _sopInstanceUid = reader.ReadElementString("DicomSopInstanceUid");
                    _transferSyntaxUid = reader.ReadElementString("DicomTransferSyntaxUid");
                    reader.ReadEndElement();
                    reader.ReadEndElement();
                }
                reader.Close();
            }
            catch (Exception e)
            {
                String message = String.Format("Failed to FromXml() DVTK DICOM EVS Object Meta Data XML stream: \"{0}\". Error: \"{1}\"", xmlStream, e.Message);
                throw new Exception(message, e);
            }
        }

        #region Properties
        public DicomBinaryObjectDataType DicomBinaryObjectDataType
        {
            get
            {
                return _dicomBinaryObjectDataType;
            }
        }
        public String SopClassUid
        {
            get
            {
                return _sopClassUid;
            }
        }

        public String SopInstanceUid
        {
            get
            {
                return _sopInstanceUid;
            }
        }

        public String TransferSyntaxUid
        {
            get
            {
                return _transferSyntaxUid;
            }
        }
        #endregion Properties
    }
}
