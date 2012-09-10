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
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace EvsService
{
    public enum LogLevelEnum 
    {
        logNothing,
        logError,
        logWarning,
        logDebug
    }

    class EvsServiceLogger
    {
        LogLevelEnum _logLevel = LogLevelEnum.logNothing;
        String _filename = String.Empty;

        public LogLevelEnum LogLevel
        {
            set
            {
                _logLevel = value;
            }
        }

        public String Filename
        {
            set
            {
                _filename = value;
            }
        }

        public void LogError(String format, params object[] args)
        {
            LogMessage(LogLevelEnum.logError, format, args);
        }

        public void LogWarning(String format, params object[] args)
        {
            LogMessage(LogLevelEnum.logWarning, format, args);
        }

        public void LogDebug(String format, params object[] args)
        {
            LogMessage(LogLevelEnum.logDebug, format, args);
        }

        public void LogMessage(LogLevelEnum messageLogLevel, String format, params object[] args)
        {
            if (_logLevel == LogLevelEnum.logNothing) return;

            String message = String.Format(format, args);
            switch (messageLogLevel)
            {
                case LogLevelEnum.logError:
                    // Always log an error
                    LogToFile(messageLogLevel, message);
                    break;
                case LogLevelEnum.logWarning:
                    if ((_logLevel == LogLevelEnum.logWarning) ||
                        (_logLevel == LogLevelEnum.logDebug))
                    {
                        // Only log a warning at warning and debug level
                        LogToFile(messageLogLevel, message);
                    }
                    break;
                case LogLevelEnum.logDebug:
                    if (_logLevel == LogLevelEnum.logDebug)
                    {
                        // Only log a debug at debug level
                        LogToFile(messageLogLevel, message);
                    }
                    break;
                default:
                    break;
            }
        }

        private void LogToFile(LogLevelEnum messageLogLevel, String message)
        {
            if (_filename == String.Empty) return;

            System.IO.StreamWriter writer = new StreamWriter(_filename, true);
            String logLevel = "E";
            switch(messageLogLevel)
            {
                case LogLevelEnum.logWarning:
                    logLevel = "W";
                    break;
                case LogLevelEnum.logDebug:
                    logLevel = "D";
                    break;
                default:
                    break;
            }
            writer.WriteLine("{0} {1}: {2}",
                logLevel,
                System.DateTime.Now.ToString("yyyyMMddHHmmss", System.Globalization.CultureInfo.InvariantCulture),
                message);
            writer.Flush();
            writer.Close();
        }
    }
}
