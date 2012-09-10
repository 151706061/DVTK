using System;

namespace DvtkApplicationLayer {
    /// <summary>
    /// Summary description for ScriptInputObj.
    /// </summary>
    public class ScriptInput : BaseInput {

        #region Private Members

        private string scriptFileName = "";
        private bool continueOnError = true;
        private object [] arguments ;

        # endregion

        # region Properties
        /// <summary>
        /// Represents name of the script.
        /// </summary>
        public string FileName{
            get {
                return scriptFileName;
            }
            set {
                scriptFileName = value;
            }
        }
        /// <summary>
        /// Determines whether to continue on error.
        /// <return>
        /// return value = true, i.e continue validating even after encountering an error. 
        /// </return>
        /// </summary>
        public bool ContinueOnError {
            get {
                return continueOnError;
            }
            set{
                continueOnError = value;
            }
        }
        public object []Arguments{
            get {
                return arguments;
            }
            set {
                arguments = value;
            }
        }

        # endregion

        # region Constructor 
        /// <summary>
        /// Constructor.
        /// </summary>
        public ScriptInput() {
        }

        #  endregion
    }
}
