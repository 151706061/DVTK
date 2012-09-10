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
using System.ServiceModel;
using System.Runtime.Serialization;
using System.IO;

using Microsoft.Web.Services3;

/*

    HOW TO HOST THE WCF SERVICE IN THIS LIBRARY IN ANOTHER PROJECT
    You will need to do the following things: 
    1)    Add a Host project to your solution
        a.    Right click on your solution
        b.    Select Add
        c.    Select New Project
        d.    Choose an appropriate Host project type (e.g. Console Application)
    2)    Add a new source file to your Host project
        a.    Right click on your Host project
        b.    Select Add
        c.    Select New Item
        d.    Select "Code File"
    3)    Paste the contents of the "MyServiceHost" class below into the new Code File
    4)    Add an "Application Configuration File" to your Host project
        a.    Right click on your Host project
        b.    Select Add
        c.    Select New Item
        d.    Select "Application Configuration File"
    5)    Paste the contents of the App.Config below that defines your service endoints into the new Config File
    6)    Add the code that will host, start and stop the service
        a.    Call MyServiceHost.StartService() to start the service and MyServiceHost.EndService() to end the service
    7)    Add a Reference to System.ServiceModel.dll
        a.    Right click on your Host Project
        b.    Select "Add Reference"
        c.    Select "System.ServiceModel.dll"
    8)    Add a Reference from your Host project to your Service Library project
        a.    Right click on your Host Project
        b.    Select "Add Reference"
        c.    Select the "Projects" tab
    9)    Set the Host project as the "StartUp" project for the solution
        a.    Right click on your Host Project
        b.    Select "Set as StartUp Project"

    ################# START MyServiceHost.cs #################

    using System;
    using System.ServiceModel;

    // A WCF service consists of a contract (defined below), 
    // a class which implements that interface, and configuration 
    // entries that specify behaviors and endpoints associated with 
    // that implementation (see <system.serviceModel> in your application
    // configuration file).

    internal class MyServiceHost
    {
        internal static ServiceHost myServiceHost = null;

        internal static void StartService()
        {
            //Consider putting the baseAddress in the configuration system
            //and getting it here with AppSettings
            Uri baseAddress = new Uri("http://localhost:8080/service1");

            //Instantiate new ServiceHost 
            myServiceHost = new ServiceHost(typeof(EvsService.service1), baseAddress);

            //Open myServiceHost
            myServiceHost.Open();
        }

        internal static void StopService()
        {
            //Call StopService from your shutdown logic (i.e. dispose method)
            if (myServiceHost.State != CommunicationState.Closed)
                myServiceHost.Close();
        }
    }

    ################# END MyServiceHost.cs #################
    ################# START App.config or Web.config #################

    <system.serviceModel>
    <services>
         <service name="EvsService.service1">
           <endpoint contract="EvsService.IService1" binding="wsHttpBinding"/>
         </service>
       </services>
    </system.serviceModel>

    ################# END App.config or Web.config #################

*/
namespace EvsService
{
    // You have created a class library to define and implement your WCF service.
    // You will need to add a reference to this library from another project and add 
    // the code to that project to host the service as described below.  Another way
    // to create and host a WCF service is by using the Add New Item, WCF Service 
    // template within an existing project such as a Console Application or a Windows 
    // Application.

    // [ServiceContract()]
    [ServiceContractAttribute(Namespace = "http://ihe.net.gazelle/")]
    public interface IService1
    {
        [OperationContract]
        string Validate(string oid,
                    string xmlReferencedStandard,
                    string xmlValidationContext,
                    string xmlObjectMetaData,
                    byte[] binaryObjectData);
        [OperationContract]
        string GetSummaryResults(string oid);
        [OperationContract]
        string GetDetailedResults(string oid);
        [OperationContract]
        void ClearResultsCache();
        [OperationContract]
        string GetValidationServiceStatus();
    }

    public class service1 : IService1
    {
        static DvtkDicomEvs _dvtkDicomEvs = null;

        public service1()
        {
            // instantiate the DVTK DICOM EVS once
            if (_dvtkDicomEvs == null)
            {
                _dvtkDicomEvs = new DvtkDicomEvs();
            }
        }

        public string Validate(string oid,
            string xmlReferencedStandard,
            string xmlValidationContext,
            string xmlObjectMetaData,
            byte[] binaryObjectData)
        {
            return _dvtkDicomEvs.Validate(oid, xmlReferencedStandard, xmlValidationContext, xmlObjectMetaData, binaryObjectData);
        }

        public string GetSummaryResults(string oid)
        {
            return _dvtkDicomEvs.GetSummaryResults(oid);
        }

        public string GetDetailedResults(string oid)
        {
            return _dvtkDicomEvs.GetDetailedResults(oid);
        }

        public void ClearResultsCache()
        {
            _dvtkDicomEvs.ClearResultsCache();
        }

        public string GetValidationServiceStatus()
        {
            return _dvtkDicomEvs.GetValidationServiceStatus();
        }
    }
}
