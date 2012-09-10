using System;
using System.Collections;

namespace DvtkApplicationLayer {
    /// <summary>
    /// Summary description for MediaInputObj.
    /// </summary>
    public class MediaInput : BaseInput {

        # region Private Members

        private ArrayList mediaFileNames = new ArrayList();

        # endregion

        # region Properties
        /// <summary>
        /// Collection of media files to be validated.
        /// </summary>
        public ArrayList FileNames {
            get {
                return mediaFileNames;
            }
            set {
                mediaFileNames = value;
            }
        }

        # endregion

        # region Constructor
        /// <summary>
        /// Constructor.
        /// </summary>
        public MediaInput() {
        }
        # endregion
    }
}
