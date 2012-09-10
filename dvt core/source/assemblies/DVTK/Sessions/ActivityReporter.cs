// Part of DVTK.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

using System;
using System.IO;

namespace Dvtk.Sessions
{
    using Dvtk.Events;

    internal class ActivityReporter
        : Wrappers.IActivityReportingTarget
    {
        internal ActivityReporter(Session session)
        {
            if (session == null) throw new System.ArgumentNullException();
            this.m_parentSession = session;
        }
        private Session m_parentSession;

        #region Wrappers.IActivityReportingTarget
        static private Dvtk.Events.ReportLevel _Convert(
            Wrappers.WrappedValidationMessageLevel activityReportLevel)
        {
            switch (activityReportLevel)
            {
                case Wrappers.WrappedValidationMessageLevel.None:
                    return Dvtk.Events.ReportLevel.None;
                case Wrappers.WrappedValidationMessageLevel.Error:
                    return Dvtk.Events.ReportLevel.Error;
                case Wrappers.WrappedValidationMessageLevel.Debug:
                    return Dvtk.Events.ReportLevel.Debug;
                case Wrappers.WrappedValidationMessageLevel.Warning:
                    return Dvtk.Events.ReportLevel.Warning;
                case Wrappers.WrappedValidationMessageLevel.Information:
                    return Dvtk.Events.ReportLevel.Information;
                case Wrappers.WrappedValidationMessageLevel.Scripting:
                    return Dvtk.Events.ReportLevel.Scripting;
                case Wrappers.WrappedValidationMessageLevel.ScriptName:
                    return Dvtk.Events.ReportLevel.ScriptName;
                case Wrappers.WrappedValidationMessageLevel.MediaFilename:
                    return Dvtk.Events.ReportLevel.MediaFilename;
                case Wrappers.WrappedValidationMessageLevel.DicomObjectRelationship:
                    return Dvtk.Events.ReportLevel.DicomObjectRelationship;
                case Wrappers.WrappedValidationMessageLevel.DulpStateMachine:
                    return Dvtk.Events.ReportLevel.DulpStateMachine;
                case Wrappers.WrappedValidationMessageLevel.WareHouseLabel:
                    return Dvtk.Events.ReportLevel.WareHouseLabel;
                default: throw new System.ArithmeticException();
            }
        }
        public void ReportActivity(
            Wrappers.WrappedValidationMessageLevel activityReportLevel,
            System.String message)
        {
            ActivityReportEventArgs e =
                new ActivityReportEventArgs(_Convert(activityReportLevel), message);
            this.m_parentSession._OnActivityReport(e);
        }
        #endregion
    }

}
