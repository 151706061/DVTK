using System;

namespace DvtkApplicationLayer {
    /// <summary>
    /// Summary description for PartOfSession.
    /// </summary>
    public class PartOfSession {
        /// <summary>
        /// Constructor .
        /// </summary>
        /// <param name="session"> DvtkApplicationLayer.Sesion</param>
        public PartOfSession(Session session) {
			this.session = session;
        }
        public PartOfSession() {
        }
        protected DvtkApplicationLayer.Session session;
        /// <summary>
        /// Property to get and set the ParentSession .
        /// </summary>
        public DvtkApplicationLayer.Session  ParentSession {
            get {
                return session;
            }
            set {
                session = value;
            }
        }
    }
}
