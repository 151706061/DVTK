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
using System.Collections;
using System.Collections.Generic;
using System.Text;

using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace EvsService
{
    class DvtkDicomEvsConfig
    {
        private String _baseCacheDirectory = String.Empty;
        private String _messageSubDirectory = String.Empty;
        private String _configurationSubDirectory = String.Empty;
        private String _definitionSubDirectory = String.Empty;
        private bool _defaultDvtkXmlResultsFormat = true;
        private LogLevelEnum _evsLogLevel = LogLevelEnum.logNothing;

        public void LoadConfig()
        {
            _baseCacheDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("baseCacheDirectory");
            _messageSubDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("messageSubDirectory");
            _configurationSubDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("configurationSubDirectory");
            _definitionSubDirectory = System.Configuration.ConfigurationManager.AppSettings.Get("definitionSubDirectory");
            String defaultDvtkXmlResultsFormat = System.Configuration.ConfigurationManager.AppSettings.Get("defaultDvtkXmlResultsFormat");
            _defaultDvtkXmlResultsFormat = (defaultDvtkXmlResultsFormat == "True" ? true : false);
            String evsLogLevel = System.Configuration.ConfigurationManager.AppSettings.Get("evsLogLevel");
            switch(evsLogLevel)
            {
                case "Nothing": _evsLogLevel = LogLevelEnum.logNothing; break;
                case "Error": _evsLogLevel = LogLevelEnum.logError; break;
                case "Warning": _evsLogLevel = LogLevelEnum.logWarning; break;
                case "Debug": _evsLogLevel = LogLevelEnum.logDebug; break;
                default: break;
            }
        }

        #region Properties
        public String BaseCacheDirectory
        {
            get
            {
                return _baseCacheDirectory;
            }
        }

        public String MessageSubDirectory
        {
            get
            {
                return _messageSubDirectory;
            }
        }

        public String ConfigurationSubDirectory
        {
            get
            {
                return _configurationSubDirectory;
            }
        }

        public String DefinitionSubDirectory
        {
            get
            {
                return _definitionSubDirectory;
            }
        }

        public bool DefaultDvtkXmlResultsFormat
        {
            get
            {
                return _defaultDvtkXmlResultsFormat;
            }

        }

        public LogLevelEnum EvsLogLevel
        {
            get
            {
                return _evsLogLevel;
            }

        }
        #endregion Properties
    }
}
