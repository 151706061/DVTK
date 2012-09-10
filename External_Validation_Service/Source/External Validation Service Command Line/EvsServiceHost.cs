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
using System.ServiceModel;

// A WCF service consists of a contract (defined below), 
// a class which implements that interface, and configuration 
// entries that specify behaviors and endpoints associated with 
// that implementation (see <system.serviceModel> in your application
// configuration file).

internal class EvsServiceHost
{
    internal static ServiceHost evsServiceHost = null;
    
    internal static void StartService()
    {
        //Get the base address
        String evsUri = System.Configuration.ConfigurationManager.AppSettings.Get("evsUri");
        Uri baseAddress = new Uri(evsUri);

        //Instantiate new ServiceHost 
        evsServiceHost = new ServiceHost(typeof(EvsService.service1), baseAddress);

        //Open myServiceHost
        evsServiceHost.Open();
    }

    internal static void StopService()
    {
        //Call StopService from your shutdown logic (i.e. dispose method)
        if (evsServiceHost.State != CommunicationState.Closed)
            evsServiceHost.Close();
    }
}

