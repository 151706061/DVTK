// Part of DVTK.dll - .NET class library providing DICOM test capabilities
// Copyright © 2001-2006
// Philips Medical Systems NL B.V., Agfa-Gevaert N.V.

namespace Dvtk.Sessions
{
    using Dvtk;
    using DvtkData.Media;                               // Data classes
    //
    // Aliases for types
    //
    using SessionFileName = System.String;
    using MediaFileName = System.String;

    using System;
    using System.Threading;

    /// <summary>
    /// Access to commands that read and write media files.
    /// </summary>
    public interface IMediaStorage
    {
		/// <summary>
		/// Generate a DICOMDIR for the given media file names.
		/// </summary>
		/// <param name="mediaFileNames">List of media fully qualified file names to use for DICOMDIR generation.</param>
		/// <returns><see langword="false"/> if the generation process failed.</returns>
		/// <exception cref="System.ArgumentNullException">Argument <c>mediaFileNames</c> is a <see langword="null"/> reference.</exception>
		/// <exception cref="System.ArgumentException">
		/// Argument <c>mediaFileNames</c> is an empty array of media file names.
		/// </exception>
		bool GenerateDICOMDIR(System.String[] mediaFileNames);

    }
    /// <summary>
    /// Access to commands that validate media related dicom-file objects.
    /// </summary>
    public interface IMediaValidation
    {
        /// <summary>
        /// Validate a media related dicom-file object
        /// </summary>
        /// <param name="file">dicom file object to be validated</param>
        /// <param name="validationControlFlags">Control flags to steer the validation process</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">Argument <c>file</c> is a <see langword="null"/> reference.</exception>
        /// <exception cref="System.ArgumentException">
        /// Argument <c>validationControlFlags</c> is not set to 
        /// <see cref="ValidationControlFlags.UseReferences"/>.
        /// </exception>
        bool Validate(DicomFile file, ValidationControlFlags validationControlFlags);
    }
    /// <summary>
    /// Validation of a Media Storage File (.DCM or other).
    /// </summary>
    /// <remarks>
    /// <p>
    /// Before running this validation, load the proper definition files corresponding to the
    /// storage SOP classes.
    /// </p>
    /// </remarks>
    public interface IMediaValidator
    {
        /// <summary>
        /// Validate Media Storage Files.
        /// </summary>
        /// <remarks>
        /// Typically these files have the file-extension DCM. DVT does not check the file-extension.
        /// The file should have an internal byte-prefix with byte-values 'DICOM'.
        /// </remarks>
        /// <param name="mediaFileNames">List of media fully qualified file names to validate.</param>
        /// <returns><see langword="false"/> if the validation process failed.</returns>
        /// <exception cref="System.ArgumentNullException">Argument <c>mediaFileNames</c> is a <see langword="null"/> reference.</exception>
        /// <exception cref="System.ArgumentException">
        /// Argument <c>mediaFileNames</c> is an empty array of media file names.
        /// </exception>
        System.Boolean ValidateMediaFiles(System.String[] mediaFileNames);

        /// <summary>
        /// Asynchronously begin ValidateMediaFiles.
        /// </summary>
        /// <param name="mediaFileNames"></param>
        /// <param name="cb"></param>
        /// <returns></returns>
        System.IAsyncResult BeginValidateMediaFiles(System.String[] mediaFileNames, AsyncCallback cb);

        /// <summary>
        /// End asynchronous ValidateMediaFiles
        /// </summary>
        /// <param name="ar"></param>
        /// <returns></returns>
        bool EndValidateMediaFiles(IAsyncResult ar) ;
    }

    /// <summary>
    /// Summary description for Session.
    /// </summary>
    public class MediaSession 
        : Session
        /* Not available: IDataSetEncodingSettings applies to writes. Media only reads.
         * , IDataSetEncodingSettings
         */
        , IMediaStorage
        , IMediaValidation
        , IMediaValidator
        /* Not available: IConfigurableDvt, IConfigurableSut, ISecure applies to network iso media.
         * , IConfigurableDvt
         * , IConfigurableSut
         * , ISecure
         */
	{

        // Validation services

        private Wrappers.MMediaSession m_adaptee = null;
        override internal Wrappers.MBaseSession m_MBaseSession
        {
            get { return m_adaptee; }
        }
        internal Wrappers.MMediaSession m_MMediaSession
        {
            get { return m_adaptee; }
        }
        //
        // Touch the AppUnloadListener abstract class to trigger its static-constructor.
        //
        static MediaSession()
        {
            AppUnloadListener.Touch();
        }
        /// <summary>
        /// Validate DICOM media files.
        /// </summary>
        /// <remarks>
        /// The Dicom Validation Tool (DVT) can also create and validate DICOM media files.
        /// </remarks>
        public MediaSession()
        {
            m_adaptee = new Wrappers.MMediaSession();
            Wrappers.MDisposableResources.AddDisposable(m_adaptee);
            _Initialize();
        }
        /// <summary>
        /// Finalizer
        /// </summary>
        ~MediaSession()
        {
            Wrappers.MDisposableResources.RemoveDisposable(m_adaptee);
            m_adaptee = null;
        }
        internal MediaSession(Wrappers.MBaseSession adaptee)
        {
            if (adaptee == null) throw new System.ArgumentNullException("adaptee");
            // Check type
            m_adaptee = (Wrappers.MMediaSession)adaptee;
            Wrappers.MDisposableResources.AddDisposable(m_adaptee);
            _Initialize();
        }
        /// <summary>
        /// Load a new session from file.
        /// </summary>
        /// <param name="sessionFileName">file with extension <c>.ses</c>.</param>
        /// <returns>new session</returns>
        /// <remarks>
        /// Session settings may be written to a file with extension <c>.ses</c> by
        /// means of <see cref="Dvtk.Sessions.ISessionFileManagement"/>.
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">Argument <c>sessionFileName</c> is a <see langword="null"/> reference.</exception>
        static public MediaSession LoadFromFile(SessionFileName sessionFileName)
        {
            if (sessionFileName == null) throw new System.ArgumentNullException("sessionFileName");
            Session session = SessionFactory.TheInstance.Load(sessionFileName);
            System.Diagnostics.Trace.Assert(session is MediaSession);
            return (MediaSession)session;
        }

        #region IMediaStorage
		/// <summary>
		/// <see cref="IMediaStorage.GenerateDICOMDIR"/>
		/// </summary>
		public bool GenerateDICOMDIR(System.String[] mediaFileNames)
		{
			bool success = false;
			try
			{
				//
				// Check argument mediaFileNames
				//
				if (mediaFileNames == null) throw new System.ArgumentNullException("mediaFileNames");
				if (mediaFileNames.Length == 0) throw new System.ArgumentException("No media files specified", "mediaFileNames");
				foreach (System.String mediaFileName in mediaFileNames)
				{
					System.IO.FileInfo fileInfo = new System.IO.FileInfo(mediaFileName);
					if (!fileInfo.Exists) throw new System.ArgumentException();
				}
				//
				// Generate the DICOMDIR.
				//
				success = this.m_adaptee.GenerateDICOMDIR(mediaFileNames);
			}
			catch(System.Exception e)
			{
				String msg = e.Message;
			}
			return success;
		}

        #endregion

        #region IMediaValidation
        /// <summary>
        /// <see cref="IMediaValidation.Validate"/>
        /// </summary>
        public bool Validate(DicomFile file, ValidationControlFlags validationControlFlags)
        {
            if (file == null) throw new System.ArgumentNullException("file");
            if ((validationControlFlags & ValidationControlFlags.UseReferences) != 0)
                throw new System.ArgumentException();
            //
            // Convert flags
            //
            Wrappers.ValidationControlFlags
                wrappersValidationControlFlags = Wrappers.ValidationControlFlags.None;
            if ((validationControlFlags & ValidationControlFlags.UseDefinitions) != 0)
                wrappersValidationControlFlags |= Wrappers.ValidationControlFlags.UseDefinitions;
            if ((validationControlFlags & ValidationControlFlags.UseReferences) != 0)
                wrappersValidationControlFlags |= Wrappers.ValidationControlFlags.UseReferences;
            if ((validationControlFlags & ValidationControlFlags.UseValueRepresentations) != 0)
                wrappersValidationControlFlags |= Wrappers.ValidationControlFlags.UseValueRepresentations;

            return this.m_adaptee.Validate(file, wrappersValidationControlFlags);
        }
        #endregion

        #region IMediaValidator

        private Mutex validateMediaFilesMutex = new Mutex();

        /// <summary>
        /// <see cref="IMediaValidator.ValidateMediaFiles"/>
        /// </summary>
        public System.Boolean ValidateMediaFiles(System.String[] mediaFileNames)
        {
            bool success = false;
            try
            {
                // Wait until it is safe to enter.
                validateMediaFilesMutex.WaitOne();
                //
                // Check argument mediaFileNames
                //
                if (mediaFileNames == null) throw new System.ArgumentNullException("mediaFileNames");
                if (mediaFileNames.Length == 0) throw new System.ArgumentException("No media files specified", "mediaFileNames");
                foreach (System.String mediaFileName in mediaFileNames)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(mediaFileName);
                    if (!fileInfo.Exists) throw new System.ArgumentException();
                }
                //
                // Begin validation
                //
                success = this.m_adaptee.BeginMediaValidation();
                //
                // Validate each media file in turn.
                //
                foreach (System.String mediaFileName in mediaFileNames)
                {
                    success &= this.m_adaptee.ValidateMediaFile(mediaFileName);
                    if (!success) break;
                }
                success &= this.m_adaptee.EndMediaValidation();
            }
            catch(System.Exception e)
            {
                String msg = e.Message;
            }
            finally
            {
                // Release the Mutex.
                validateMediaFilesMutex.ReleaseMutex();
            }
            return success;
        }

        /// <summary>
        /// Asynchronous ValidateMediaFiles delegate.
        /// </summary>
        /// <remarks>
        /// The delegate must have the same signature as the method you want to call asynchronously.
        /// </remarks>
        private delegate bool AsyncValidateMediaFilesDelegate(System.String[] mediaFileNames);

        /// <summary>
        /// <see cref="IMediaValidator.BeginValidateMediaFiles"/>
        /// </summary>
        public System.IAsyncResult BeginValidateMediaFiles(
            System.String[] mediaFileNames, 
            AsyncCallback cb) 
        {
            // Create the delegate.
            AsyncValidateMediaFilesDelegate dlgt = new AsyncValidateMediaFilesDelegate(this.ValidateMediaFiles);
            // Initiate the asychronous call.
            object asyncState = dlgt;
            IAsyncResult ar = dlgt.BeginInvoke(
                mediaFileNames, 
                cb, 
                asyncState);
            return ar;
        }
        
        /// <summary>
        /// <see cref="IMediaValidator.EndValidateMediaFiles"/>
        /// </summary>
        public bool EndValidateMediaFiles(
            IAsyncResult ar) 
        {
            // Retrieve the delegate.
            AsyncValidateMediaFilesDelegate dlgt = (AsyncValidateMediaFilesDelegate)ar.AsyncState;
            // Call EndInvoke to retrieve the results.
            bool retValue = dlgt.EndInvoke(ar);
            return retValue;
        }

        #endregion

    }
}