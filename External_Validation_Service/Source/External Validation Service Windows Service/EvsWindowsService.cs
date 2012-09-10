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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;

namespace EvsWindowsServiceHost
{
    public partial class EvsWindowsService : ServiceBase
    {
        internal static ServiceHost evsServiceHost = null;

        public EvsWindowsService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            //
            // Use temp directory to redirect Console.Out and Console.Error.
            //
            string tempPath = Path.GetTempPath();
            string eventMessage;

            this.EventLog.WriteEntry("OnStart", System.Diagnostics.EventLogEntryType.Information);

            if (args.Length > 0)
            {
                eventMessage =
                    string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    "Service OnStart Arguments:{0}: ", args);
                EventLog.WriteEntry(eventMessage, System.Diagnostics.EventLogEntryType.Information);
            }
            
            eventMessage =
                string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "BaseDirectory:{0}",
                System.AppDomain.CurrentDomain.BaseDirectory);
            EventLog.WriteEntry(eventMessage, System.Diagnostics.EventLogEntryType.Information);

            eventMessage =
                string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "CurrentDirectory:{0}",
                System.Environment.CurrentDirectory);
            EventLog.WriteEntry(eventMessage, System.Diagnostics.EventLogEntryType.Information);

            eventMessage =
                string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "TempPath:{0}",
                tempPath);

            EventLog.WriteEntry(eventMessage, System.Diagnostics.EventLogEntryType.Information);

            // start the EVS Web Service
            EvsServiceHost.StartService();
        }

        protected override void OnStop()
        {
            this.EventLog.WriteEntry("OnStop", System.Diagnostics.EventLogEntryType.Information);

            // stop the EVS Web Service
            EvsServiceHost.StopService();

            base.OnStop();
        }
    }
}
