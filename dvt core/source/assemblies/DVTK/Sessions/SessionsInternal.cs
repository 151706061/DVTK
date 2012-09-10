using System;

namespace DicomValidationToolKit.Sessions
{
    internal abstract class ChildService
    {
        internal protected Session m_parentSession = null;
        internal ChildService(Session parentSession)
        {
            m_parentSession = parentSession;
        }
    }
}
namespace DicomValidationToolKit.Sessions.Services
{
    using Key = System.String;
    using System.Collections;
    using DicomValidationToolKit;
    using DefinitionFileRoot = System.String;
    using DefinitionFileName = System.String;
    using DicomValidationToolKit.Sessions;
    using FileName = System.String;
    internal class Dvt1ScriptSessionService
        : ChildService
        , Dvt1.IDvt1ScriptSessionCommands
    {
        private Wrappers.MBaseSession m_delegate;
        internal Dvt1ScriptSessionService(Session parentSession, Wrappers.MBaseSession baseSessionDelegate)
            : base(parentSession)
        {
            m_delegate = baseSessionDelegate;
        }
        //        public System.Boolean serialise(FILE f);
        public System.Boolean executeScript(System.String s)
        {
            return this.m_delegate.ExecuteScript(s);
        }
        public System.Boolean parseScript(System.String s)
        {
            return this.m_delegate.ParseScript(s);
        }
        //        public void setScriptDoneCallBack(void (*)(void*), void*);
        //        public void resetScriptDoneCallBack(void);
        public System.Boolean terminateConnection()
        {
            return this.m_delegate.TerminateConnection();
        }
        public void resetAssociation()
        {
            this.m_delegate.ResetAssociation();
            return;
        }
        //        public System.Boolean connectOnTcpIp();
        //        public System.Boolean listenOnTcpIp();
        //        public System.Boolean begin(ref System.Boolean definitionFileLoaded);
        //        public System.Boolean importCommand(Dimse_CMD_ENUM e, string s);
        //        public System.Boolean importCommandDataset(Dimse_CMD_ENUM e, string s1, string s2, string s3);
        //        public void end();
    }
    internal class Dvt1MediaSessionService
        : ChildService
        , Dvt1.IDvt1MediaSessionCommands
    {
        private Wrappers.MBaseSession m_delegate;
        internal Dvt1MediaSessionService(Session parentSession, Wrappers.MBaseSession baseSessionDelegate)
            : base(parentSession)
        {
            m_delegate = baseSessionDelegate;
        }
        public System.Boolean beginMediaValidation()
        {
            return this.m_delegate.BeginMediaValidation();
        }
        public System.Boolean validateMediaFile(System.String s)
        {
            return this.m_delegate.ValidateMediaFile(s);
        }
        public System.Boolean endMediaValidation()
        {
            return this.m_delegate.EndMediaValidation();
        }
        //        public System.Boolean serialise(FILE f);
    }
}
