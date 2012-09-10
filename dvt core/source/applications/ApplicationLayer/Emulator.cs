using System;
using System.Collections;

namespace DvtkApplicationLayer
{
	/// <summary>
	/// Summary description for Emulator.
	/// </summary>
	public class Emulator : PartOfSession
	{   
        /// <summary>
        /// 
        /// </summary>
        public enum EmulatorTypes {
            /// <summary>
            /// 
            /// </summary>
            STORAGE_SCP,
            /// <summary>
            /// 
            /// </summary>
            STORAGE_SCU,
            /// <summary>
            /// 
            /// </summary>
            PRINT_SCP
        }

        /// <summary>
        /// 
        /// </summary>
        public Emulator() {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="session"></param>
        /// <param name="emulatorName"></param>
		public Emulator(Session session, String emulatorName): base(session)
		{
            this.emulatorName = emulatorName;
		}

		private string emulatorName; 
		private IList emulatorResultFiles = new ArrayList();
        private EmulatorTypes emulatorType ;
        /// <summary>
        /// Represents the name of the emulator.
        /// </summary>
		public string EmulatorName 
		{
			get 
			{
				return emulatorName ;}
			set 
			{
				emulatorName = value;}
		}
        /// <summary>
        /// Represents Collection of Results for an emulator.
        /// </summary>
		public IList Result
		{
			get 
			{ 
				return emulatorResultFiles ;}
			set 
			{
				emulatorResultFiles = value;
			}
		}
       /// <summary>
       /// Represent the type of emulator
       /// </summary>

        public EmulatorTypes EmulatorType{
            get { 
                return emulatorType ;
            }
            set {
                emulatorType = value;
            }
        }

	}
}

